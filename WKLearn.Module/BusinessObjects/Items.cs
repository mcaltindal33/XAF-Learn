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
    [NavigationItem("Setup")]
    [XafDefaultProperty(nameof(Name))]
    [XafDisplayName("Item Listing")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Items : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Items(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            IsActivate = true;
            MinStock = 1;
            MaxStock = 1000;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        bool ısActivate;
        double maxStock;
        double minStock;
        double price;
        string name;
        string code;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(1)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("Item Code"), ToolTip("Enter Text")]
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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Index(1)]
        [RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("Item Name"), ToolTip("Enter Text")]
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

        [Index(2)]
        [XafDisplayName("Item Price"), ToolTip("Enter Price")]
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue(nameof(Price), ref price, value);
            }
        }

        [Index(3), VisibleInListView(false)]
        [XafDisplayName("Item Min Stock"), ToolTip("Enter Minimum Stock")]
        public double MinStock
        {
            get
            {
                return minStock;
            }
            set
            {
                SetPropertyValue(nameof(MinStock), ref minStock, value);
            }
        }

        [Index(4), VisibleInListView(false)]
        [XafDisplayName("Item Max Stock"), ToolTip("Enter Maximum Stock")]
        public double MaxStock
        {
            get
            {
                return maxStock;
            }
            set
            {
                SetPropertyValue(nameof(MaxStock), ref maxStock, value);
            }
        }

        [Index(5),VisibleInListView(false)]
        [XafDisplayName("Activate"),ToolTip("Items is Activate")]
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

        [Action(Caption ="Activate / In Activate",ConfirmationMessage ="Are you sure you want to contuinue?", ImageName ="Attention", AutoCommit =true)]
        void ActionMethods()
        {
            if(IsActivate)
                IsActivate = false;
            else
                IsActivate = true;
        }

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && Session.IsNewObject(this)
                        && string.IsNullOrEmpty(Code))
            {
                int sequence = DistributedIdGeneratorHelper.Generate(Session.DataLayer,
                                                                     this.GetType().FullName,
                                                                     "ItemServerPrefix");
                Code = string.Format("{0:D8}", sequence);
            }
            base.OnSaving();
        }
    }
}