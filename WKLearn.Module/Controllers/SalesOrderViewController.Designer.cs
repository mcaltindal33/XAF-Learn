namespace WKLearn.Module.Controllers
{
    partial class SalesOrderViewController
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
            this.CopyToDO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CopyToDO
            // 
            this.CopyToDO.Caption = "Copy To Delivery";
            this.CopyToDO.ConfirmationMessage = null;
            this.CopyToDO.Id = "CopyToDO";
            this.CopyToDO.ToolTip = null;
            this.CopyToDO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CopyToDO_Execute);
            // 
            // SalesOrderViewController
            // 
            this.Actions.Add(this.CopyToDO);
            this.TargetObjectType = typeof(WKLearn.Module.BusinessObjects.SalesOrder);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CopyToDO;
    }
}
