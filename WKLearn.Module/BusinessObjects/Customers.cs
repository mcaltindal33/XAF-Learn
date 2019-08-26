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
    [NavigationItem("Setup")]
    [XafDefaultProperty(nameof(Name))]
    [XafDisplayName("Customer Listing")]
    public class Customers : XPObject
    { 
        public Customers(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            IsActivate = true;
        }


        bool ısActivate;
        string address;
        string conactPerson;
        string name;
        string code;

        [XafDisplayName("Customer Code"), ToolTip("Enter Text")]
        [Appearance("CustomerCodeDisable", Enabled = false)]
        [Index(0)]//index = sequence, VisableInListView = show column in listview, VisableInDetailView = show column in detail view, VisableInLookupListView = show column in lookup list
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

        [XafDisplayName("Customer Name"), ToolTip("Enter Text")]
        [Index(1)]
        [RuleRequiredField(DefaultContexts.Save, CustomMessageTemplate = "Please fiil in Customer Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetPropertyValue(nameof(Name), ref name, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(2)]
        public string ConactPerson
        {
            get
            {
                return conactPerson;
            }
            set
            {
                SetPropertyValue(nameof(ConactPerson), ref conactPerson, value);
            }
        }

        [Size(350)]
        [Index(3)]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                SetPropertyValue(nameof(Address), ref address, value);
            }
        }

        [Index(4),VisibleInListView(false)]
        [Appearance("DisableIsActive", Enabled = false)]
        public bool IsActivate
        {
            get
            {
                return ısActivate;
            }
            set
            {
                SetPropertyValue(nameof(IsActivate), ref ısActivate, value);
            }
        }

        [Action(Caption = "Active / In Active", ConfirmationMessage = "Are you sure you to continue?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            if(IsActivate)
                IsActivate = false;
            else
                IsActivate = true;
        }

        protected override void OnSaving()
        {
            if(!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && Session.IsNewObject(this)
                        && string.IsNullOrEmpty(Code))
            {
                int sequence = DistributedIdGeneratorHelper.Generate(Session.DataLayer,
                                                                     this.GetType().FullName,
                                                                     "CustomerServerPrefix");
                Code = string.Format("{0:D8}", sequence);
            }
            base.OnSaving();
        }
        protected override void OnSaved()
        {
            this.Reload();
            base.OnSaved(); 
        }
    }
}