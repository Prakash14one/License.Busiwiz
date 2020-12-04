using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Resultset
/// </summary>
namespace DS.Common
{
    public class Resultset<T>
    {

        private T dSource;
        private DBParams outParams;

        public Resultset()
        {
        }

        public Resultset(T pDataSource, DBParams pOutParams)
        {
            dSource = pDataSource;
            outParams = pOutParams;
        }

        public T DataSource
        {
            get { return dSource; }
            set { dSource = value; }
        }
        public DBParams OutputParameters
        {
            get { return outParams; }
            set { outParams = value; }
        }

    }
}

