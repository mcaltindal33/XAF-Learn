using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;

namespace WKLearn.WorkflowServerService {
    public class ServerApplication : XafApplication {
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, true);
        }
        protected override DevExpress.ExpressApp.Layout.LayoutManager CreateLayoutManagerCore(bool simple) {
            throw new NotImplementedException();
        }
    }
}