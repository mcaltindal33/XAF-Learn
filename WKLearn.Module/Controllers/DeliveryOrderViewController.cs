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
    public partial class DeliveryOrderViewController : ViewController
    {
        public DeliveryOrderViewController()
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

        private void CopyFromSO_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if(View.ObjectTypeInfo.Type == typeof(DeliveryOrder))
            {
                if(((DetailView)View).ViewEditMode == ViewEditMode.View)
                {
                    MessageOptions messageOptions = new MessageOptions();
                    messageOptions.Duration = 3000;
                    messageOptions.Type = InformationType.Error;
                    messageOptions.Message = "View mode cannot proceed.";
                    Application.ShowViewStrategy.ShowMessage(messageOptions);
                }
                else
                {
                    DeliveryOrder currentObject = (DeliveryOrder)View.CurrentObject;
                    if(currentObject.CustomerID == null)
                    {
                        MessageOptions messageOptions = new MessageOptions();
                        messageOptions.Duration = 3000;
                        messageOptions.Type = InformationType.Error;
                        messageOptions.Message = "Must select customer";
                        Application.ShowViewStrategy.ShowMessage(messageOptions);
                    }
                    else
                    {
                        GeneralValues.CustomerIid = currentObject.CustomerID.Oid;
                    }
                }
                e.View = Application.CreateListView(typeof(SalesOrderDetail), true);

            }
        }

        private void CopyFromSO_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DeliveryOrder currentObject = (DeliveryOrder)View.CurrentObject;
            foreach (SalesOrderDetail item in e.PopupWindow.View.SelectedObjects)
            {
                DeliveryOrderDetail deliveriDetail = ObjectSpace.CreateObject<DeliveryOrderDetail>();

                deliveriDetail.BaseObj = "Order";
                deliveriDetail.BaseLine = item.Oid;
                deliveriDetail.Item = ObjectSpace.GetObjectByKey<Items>(item.Item.Oid);
                deliveriDetail.Quantity = item.OpenQuantity;
                deliveriDetail.Price = item.Price;

                currentObject.OrderDetail.Add(deliveriDetail);
            }
        }
    }
}
