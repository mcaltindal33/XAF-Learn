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
    //[DefaultClassOptions]
    public class SeriesDocNumbers : XPObject
    { 
        public SeriesDocNumbers(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string nextDocCode;
        int nextDocSeq;
        Series seriesID;
        Company companyID;

        [XafDisplayName("Company"), ToolTip("Select Company")]
        [Index(0)]
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

        [Association("Series-SeriesDocNum", typeof(Series))]
        [XafDisplayName("Series"), ToolTip("Select Series")]
        [Index(1)]
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

        [XafDisplayName("Next Document Sequence")]
        [Index(2)]
        public int NextDocSeq
        {
            get
            {
                return nextDocSeq;
            }
            set
            {
                SetPropertyValue(nameof(NextDocSeq), ref nextDocSeq, value);
            }
        }


        [XafDisplayName("Next Document Code")]
        [Index(3)]
        public string NextDocCode
        {
            get
            {
                return nextDocCode;
            }
            set
            {
                SetPropertyValue(nameof(NextDocCode), ref nextDocCode, value);
            }
        }


    }
}