﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using WKLearn.Module.BusinessObjects;

namespace WKLearn.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterCopyFromViewController : ViewController<ListView>
    {
        public FilterCopyFromViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if(View.IsRoot)
            {
                if(View.ObjectTypeInfo.Type == typeof(SalesOrderDetail))
                {
                    if(GeneralValues.CustomerIid <= 0)
                    {
                        View.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                    }
                    else
                    {
                        View.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("OrderID.CustomerID.Oid=?",GeneralValues.CustomerIid);
                        View.CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("OpenQuantity > 0");
                    }
                    GeneralValues.CustomerIid = 0;
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
