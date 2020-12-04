using System;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.DirectoryServices;
using System.IO.Compression;
using Ionic.Zip;
using System.Security.Cryptography;

using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml;
using System.Net;

using System.Net.Sockets;

public partial class CreateCompanyconnectionString : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string strEnc = "";
    public static string strser = "";
    public static string  datasource= "";
    public static string catalog = "";
    public static string userid = "";
    public static string password = "";


   


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["comid"] != null)
            {
                Session["comid"] = Request.QueryString["comid"];
            SqlConnection servermasterconn = new SqlConnection();

            string strcomp = "select dbo.CompanyMaster.CompanyLoginId, ServerMasterTbl.*,CompanyMaster.Encryptkeycomp from CompanyMaster inner join ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId where CompanyMaster.CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["comid"]) + "'  ";
            SqlCommand cmdcomp = new SqlCommand(strcomp, con);
            DataTable dtcomp = new DataTable();
            SqlDataAdapter adpcomp = new SqlDataAdapter(cmdcomp);
            adpcomp.Fill(dtcomp);

            if (dtcomp.Rows.Count > 0)
            {
                datasource =  dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString();
                catalog = dtcomp.Rows[0]["DefaultDatabaseName"].ToString();
                userid = "sa";
                password = PageMgmt.Decrypted(dtcomp.Rows[0]["Sapassword"].ToString());
                strEnc = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);
                strser = Convert.ToString(dtcomp.Rows[0]["Enckey"]);

                DataTable dtmainurl = MyCommonfile.selectBZ(" Select DNSname as WebsiteURL From CompanyCreationNeedCode Where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["comid"]) + "' and AdditionalPageInserted=1 ");
                //wesittecretionreq();
                if (dtmainurl.Rows.Count > 0)
                {
                    Response.Redirect("http://" + Convert.ToString(HttpContext.Current.Session["comid"]) + "." + dtmainurl.Rows[0]["WebsiteURL"].ToString() + "/addwebconfig.aspx?datasource=" + datasource + "&catalog=" + catalog + "&userid=" + userid + "&password=" + password + "&strEnc=" + strEnc + "&strser=" + strser + "");
                }
               // servermasterconn.ConnectionString = @"Data Source =" + dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString() + "; Initial Catalog = " + dtcomp.Rows[0]["DefaultDatabaseName"].ToString() + "; User ID=sa; Password=" + PageMgmt.Decrypted(dtcomp.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true; ";
                // strEnc = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);
                //Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true; 
               // Response.Redirect("http://" + dtcomp.Rows[0]["CompanyLoginId"].ToString() + ".onlineaccounts.net/addwebconfig.aspx?datasource=" + datasource + "&catalog=" + catalog + "&userid=" + userid + "&password=" + password + "&strEnc=" + strEnc + "&strser=" + strser + "");
            }
            }

            if (Request.QueryString["sid"] != null )
            {
                Session["comid"] = Request.QueryString["comid"];
                SqlConnection servermasterconn = new SqlConnection();

                string strcomp = "select dbo.CompanyMaster.CompanyLoginId, ServerMasterTbl.*,CompanyMaster.Encryptkeycomp from CompanyMaster inner join ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId where CompanyMaster.CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["comid"]) + "'  ";
                SqlCommand cmdcomp = new SqlCommand(strcomp, con);
                DataTable dtcomp = new DataTable();
                SqlDataAdapter adpcomp = new SqlDataAdapter(cmdcomp);
                adpcomp.Fill(dtcomp);

                if (dtcomp.Rows.Count > 0)
                {
                    datasource = dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString();
                    catalog = dtcomp.Rows[0]["DefaultDatabaseName"].ToString();
                    userid = "sa";
                    password = PageMgmt.Decrypted(dtcomp.Rows[0]["Sapassword"].ToString());
                    strEnc = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);
                    strser = Convert.ToString(dtcomp.Rows[0]["Enckey"]);

                    DataTable dtmainurl = MyCommonfile.selectBZ(" Select DNSname as WebsiteURL From CompanyCreationNeedCode Where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["comid"]) + "' and AdditionalPageInserted=1 ");
                    //wesittecretionreq();
                    if (dtmainurl.Rows.Count > 0)
                    {
                        Response.Redirect("http://" + Convert.ToString(HttpContext.Current.Session["comid"]) + "." + dtmainurl.Rows[0]["WebsiteURL"].ToString() + "/addwebconfig.aspx?datasource=" + datasource + "&catalog=" + catalog + "&userid=" + userid + "&password=" + password + "&strEnc=" + strEnc + "&strser=" + strser + "");
                    }
                    // servermasterconn.ConnectionString = @"Data Source =" + dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString() + "; Initial Catalog = " + dtcomp.Rows[0]["DefaultDatabaseName"].ToString() + "; User ID=sa; Password=" + PageMgmt.Decrypted(dtcomp.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true; ";
                    // strEnc = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);
                    //Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true; 
                    // Response.Redirect("http://" + dtcomp.Rows[0]["CompanyLoginId"].ToString() + ".onlineaccounts.net/addwebconfig.aspx?datasource=" + datasource + "&catalog=" + catalog + "&userid=" + userid + "&password=" + password + "&strEnc=" + strEnc + "&strser=" + strser + "");
                }
            }

        }
        
    }

    public static SqlConnection licenseconn()
    {
        SqlConnection liceco = new SqlConnection();
        liceco.ConnectionString = "Data Source=TCP:192.168.1.219,2810;Initial Catalog=License.Busiwiz; User ID=BuzRead; Password=Busiwiz2013++; Persist Security Info=true; ";
        return liceco;
    }

    private void AddUpdateConnectionString(string name)
    {
        bool isNew = false;
        string path = Server.MapPath("~/Web.Config");
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
        XmlNode node;
        isNew = list.Count == 0;
        if (isNew)
        {
            node = doc.CreateNode(XmlNodeType.Element, "add", null);
            XmlAttribute attribute = doc.CreateAttribute("name");
            attribute.Value = name;
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("connectionString");
            attribute.Value = "";
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("providerName");
            attribute.Value = "System.Data.SqlClient";
            node.Attributes.Append(attribute);
        }
        else
        {
            node = list[0];
        }
        string conString = node.Attributes["connectionString"].Value;
        SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
        conStringBuilder.InitialCatalog = "C3SATELLITESERVER";
        String ss = "c3\\TCP:72.38.84.230,30000";
        conStringBuilder.DataSource = "c3\\TCP:72.38.84.230,30000";
        conStringBuilder.IntegratedSecurity = false;
        conStringBuilder.UserID = "sa";
        conStringBuilder.Password = "06De1963++";
        node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
        if (isNew)
        {
            doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
        }
        doc.Save(path);
    }

}