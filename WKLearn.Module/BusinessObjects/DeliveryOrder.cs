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

namespace WKLearn.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DeliveryOrder : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DeliveryOrder(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DocDate = DateTime.Today;
            CompanyID = Session.FindObject<Company>(new BinaryOperator("Code", "Demo"));
            SeriesID = Session.FindObject<Series>(new BinaryOperator("Code", "Delivery"));
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

        [Association("DeliveryOrder-OrderDetail", typeof(DeliveryOrderDetail))]
        public XPCollection<DeliveryOrderDetail> OrderDetail
        {
            get
            {
                return GetCollection<DeliveryOrderDetail>(nameof(OrderDetail));
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
                    numbers.NextDocCode = "DLV";
                    numbers.NextDocSeq = 1;
                    sequence = 1;
                }
                else
                {
                    sequence = numbers.NextDocSeq + 1;
                    numbers.NextDocSeq++;
                }
                DocNum = string.Format("{0} {1:D5}", numbers.NextDocCode, sequence);
                int deliveryCode = DistributedIdGeneratorHelper.Generate(Session.DataLayer,
                                                      this.GetType().FullName,
                                                      "deliveryServerPrefix");
                Code = string.Format("{0}-{1}-{2}-{3:D4}", DocDate.Year, DocDate.Month, DocDate.Day, deliveryCode);

            }
            base.OnSaving();
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            this.Reload();
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}