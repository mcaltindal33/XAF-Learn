﻿using System;
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
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class InvoiceDetail : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InvoiceDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Quantity = 1;
            OldQuantity = 0;
            CopyQuantity = 0;

            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        double price;
        double quantity;
        Items ıtem;
        Invoice invoiceID;
        [Browsable(false)]
        [Association("Invoice-InvoiceDetail", typeof(Invoice))]
        public Invoice InvoiceID
        {
            get
            {
                return invoiceID;
            }
            set
            {
                SetPropertyValue(nameof(InvoiceID), ref invoiceID, value);
            }
        }

        [XafDisplayName("Item Name"), ToolTip("Select Item")]
        [Index(0)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Items Item
        {
            get
            {
                return ıtem;
            }
            set
            {
                SetPropertyValue(nameof(Item), ref ıtem, value);
            }
        }
        [XafDisplayName("Item Quantity"), ToolTip("Enter Quantity")]
        [Index(1),ImmediatePostData]
        public double Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue(nameof(Quantity), ref quantity, value);
            }
        }
        
        [XafDisplayName("Item Price"),ToolTip("Enter Price")]
        [Index(2),ImmediatePostData]
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

        [XafDisplayName("Item Amount")]
        [Index(3)]
        public double Amount { get { return Quantity * Price; } }

        #region copy from and to columns
        double oldQuantity;
        string baseObj;
        int baseLine;
        double copyQuantity;


        [Browsable(false)]
        public double OldQuantity
        {
            get
            {
                return oldQuantity;
            }
            set
            {
                SetPropertyValue(nameof(OldQuantity), ref oldQuantity, value);
            }
        }

        [Browsable(false)]
        public double CopyQuantity
        {
            get
            {
                return copyQuantity;
            }
            set
            {
                SetPropertyValue(nameof(CopyQuantity), ref copyQuantity, value);
            }
        }

        public double OpenQuantity => Quantity - CopyQuantity;


        public string BaseObj
        {
            get
            {
                return baseObj;
            }
            set
            {
                SetPropertyValue(nameof(BaseObj), ref baseObj, value);
            }
        }


        public int BaseLine
        {
            get
            {
                return baseLine;
            }
            set
            {
                SetPropertyValue(nameof(BaseLine), ref baseLine, value);
            }
        }
        #endregion

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!(Session is NestedUnitOfWork) && (Session.DataLayer != null) && (Session.ObjectLayer is SimpleObjectLayer))
            {
                if (BaseObj == "Order")
            {
                SalesOrderDetail obj = Session.FindObject<SalesOrderDetail>(new BinaryOperator("Oid",
                                                                                               BaseLine,
                                                                                               BinaryOperatorType.Equal));
                obj.CopyQuantity = obj.CopyQuantity + this.Quantity - this.OldQuantity;
            }
            if (BaseObj == "Delivery")
            {
                DeliveryOrderDetail obj = Session.FindObject<DeliveryOrderDetail>(new BinaryOperator("Oid",
                                                                                               BaseLine,
                                                                                               BinaryOperatorType.Equal));
                obj.CopyQuantity = obj.CopyQuantity + this.Quantity - this.OldQuantity;
            }

            OldQuantity = Quantity;
            }
        }
        protected override void OnDeleting()
        {
            if(!(Session is NestedUnitOfWork) &&
                (Session.DataLayer != null) &&
                (Session.ObjectLayer is SimpleObjectLayer))
            {
                if(BaseObj == "Order")
                {
                    SalesOrderDetail obj = Session.FindObject<SalesOrderDetail>(new BinaryOperator("Oid",
                                                                                                   BaseLine,
                                                                                                   BinaryOperatorType.Equal));
                    obj.CopyQuantity = obj.CopyQuantity - this.OldQuantity;
                }
                if(BaseObj == "Delivery")
                {
                    DeliveryOrderDetail obj = Session.FindObject<DeliveryOrderDetail>(new BinaryOperator("Oid",
                                                                                                         BaseLine,
                                                                                                         BinaryOperatorType.Equal));
                    obj.CopyQuantity = obj.CopyQuantity - this.OldQuantity;
                }
            }
            base.OnDeleting();
        }

    }
}