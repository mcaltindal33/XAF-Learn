using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace WKLearn.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Invoice : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Invoice(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DocDate = DateTime.Today;
            CompanyID = Session.FindObject<Company>(new BinaryOperator("Oid", 1));
            SeriesID = Session.FindObject<Series>(new BinaryOperator("Code", "Invoice"));
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        Series seriesID;
        Company companyID;
        string remarks;
        Customers customerID;
        DateTime docDate;
        string docNum;
        string code;

        [XafDisplayName("Order Code")]
        [Index(0)]
        [Appearance("DisableCode", Enabled = false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                SetPropertyValue(nameof(Code), ref code, value);
            }
        }


        [XafDisplayName("Document No"), ToolTip("Enter Document No")]
        [Index(1)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string DocNum
        {
            get
            {
                return docNum;
            }
            set
            {
                SetPropertyValue(nameof(DocNum), ref docNum, value);
            }
        }

        [XafDisplayName("Document Date"), ToolTip("Enter Document Date")]
        [Index(2)]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DocDate
        {
            get
            {
                return docDate;
            }
            set
            {
                SetPropertyValue(nameof(DocDate), ref docDate, value);
            }
        }

        [XafDisplayName("Customer"), ToolTip("Select Customer")]
        [Index(3)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Customers CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                SetPropertyValue(nameof(CustomerID), ref customerID, value);
            }
        }


        [XafDisplayName("Remarks"), ToolTip("Enter Remarks")]
        [Index(4), VisibleInListView(false)]
        public string Remarks
        {
            get
            {
                return remarks;
            }
            set
            {
                SetPropertyValue(nameof(Remarks), ref remarks, value);
            }
        }

        [XafDisplayName("Company"), ToolTip("Select Company")]
        [Index(5), VisibleInListView(false)]
        [Appearance("DisableCompanyID", Enabled = false)]
        public Company CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                SetPropertyValue(nameof(CompanyID), ref companyID, value);
            }
        }

        [XafDisplayName("Series"), ToolTip("Select Series")]
        [Index(6), VisibleInListView(false)]
        [Appearance("DisableSeriesID", Enabled = false)]
        public Series SeriesID
        {
            get
            {
                return seriesID;
            }
            set
            {
                SetPropertyValue(nameof(SeriesID), ref seriesID, value);
            }
        }

        [Association("Invoice-InvoiceDetail", typeof(InvoiceDetail))]
        public XPCollection<InvoiceDetail> InvoiceDetail
        {
            get
            {
                return GetCollection<InvoiceDetail>(nameof(InvoiceDetail));
            }
        }

        protected override void OnSaving()
        {
            if(!(Session is NestedUnitOfWork) && (Session.DataLayer != null) &&  Session.IsNewObject(this) && (Session.ObjectLayer is SimpleObjectLayer) )
            {
                int sequence = 0;
                SeriesDocNumbers numbers = Session.FindObject<SeriesDocNumbers>(CriteriaOperator.Parse("CompanyID = ? and SeriesID = ?", CompanyID,SeriesID));
                if (numbers == null)
                {
                    numbers = new SeriesDocNumbers(Session);
                    numbers.CompanyID = CompanyID;
                    numbers.SeriesID = SeriesID;
                    numbers.NextDocCode = "INV";
                    numbers.NextDocSeq = 1;
                    sequence = 1;
                }
                else
                {
                    sequence = numbers.NextDocSeq + 1;
                    numbers.NextDocSeq++;
                }
                DocNum = string.Format("{0} {1:D5}", numbers.NextDocCode, sequence);
                int invoiceCode = DistributedIdGeneratorHelper.Generate(Session.DataLayer,
                                                      this.GetType().FullName,
                                                      "invoiceServerPrefix");
                Code = string.Format("{0}-{1}-{2}-{3:D4}", DocDate.Year, DocDate.Month, DocDate.Day, invoiceCode);

            }
            base.OnSaving();
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            this.Reload();
        }
    }
}