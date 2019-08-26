using System;
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
    public partial class SalesOrderViewController : ViewController
    {
        public SalesOrderViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void CopyToDO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            SalesOrder currentObject = (SalesOrder)View.CurrentObject;
            if(ObjectSpace.IsModified  || ObjectSpace.IsNewObject(currentObject))
            {
                MessageOptions options = new MessageOptions();
                options.Message = "Please save the document 1st.";
                options.Duration = 3000;
                options.Type = InformationType.Error;
                Application.ShowViewStrategy.ShowMessage(options);
                return;
            }
            IObjectSpace ios = Application.CreateObjectSpace();
            DeliveryOrder deliveryOrder = ios.CreateObject<DeliveryOrder>();
            deliveryOrder.CustomerID = deliveryOrder.Session.FindObject<Customers>(new BinaryOperator("Oid", currentObject.CompanyID.Oid));
            foreach (SalesOrderDetail item in currentObject.OrderDetail)
            {
                if(item.OpenQuantity <= 0)
                    continue;
                DeliveryOrderDetail orderDetail = ios.CreateObject<DeliveryOrderDetail>();
                orderDetail.Item = ios.FindObject<Items>(new BinaryOperator("Oid", item.Item.Oid));
                orderDetail.Item = ios.GetObjectByKey<Items>(item.Item.Oid);
                orderDetail.Quantity = item.OpenQuantity;
                orderDetail.Price = item.Price;
                orderDetail.BaseObj = "Order";
                orderDetail.BaseLine = item.Oid;
                deliveryOrder.OrderDetail.Add(orderDetail);
            }

            ShowViewParameters svp = new ShowViewParameters();
            DetailView dv = Application.CreateDetailView(ios, deliveryOrder);
            dv.ViewEditMode = ViewEditMode.Edit;
            dv.IsRoot = true;
            svp.CreatedView = dv;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }
    }
}
