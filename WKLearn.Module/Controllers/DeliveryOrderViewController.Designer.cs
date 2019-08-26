namespace WKLearn.Module.Controllers
{
    partial class DeliveryOrderViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CopyFromSO = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CopyFromSO
            // 
            this.CopyFromSO.AcceptButtonCaption = null;
            this.CopyFromSO.CancelButtonCaption = null;
            this.CopyFromSO.Caption = "Copy From SO";
            this.CopyFromSO.ConfirmationMessage = null;
            this.CopyFromSO.Id = "CopyFromSO";
            this.CopyFromSO.ToolTip = null;
            this.CopyFromSO.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CopyFromSO_CustomizePopupWindowParams);
            this.CopyFromSO.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CopyFromSO_Execute);
            // 
            // DeliveryOrderViewController
            // 
            this.Actions.Add(this.CopyFromSO);
            this.TargetObjectType = typeof(WKLearn.Module.BusinessObjects.DeliveryOrder);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CopyFromSO;
    }
}
