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
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public partial class CandidateApplicationRegistration : System.Web.UI.Page
{
    int groupid = 0;
    string accid = "";
    int classid = 0;
    //SqlConnection con = new SqlConnection(PageConn.connnn);
    string lblempno = "";
    SqlConnection con;
    string logoname1 = "";
    string logoname = "";

    EmployeeCls clsEmployee = new EmployeeCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (Request.QueryString["Id"] != null)
            {
                DataTable dtname = select("select Name from WarehouseMaster where WarehouseID='" + ClsEncDesc.Decrypted(Request.QueryString["Id"].ToString()) + "'");

                if (dtname.Rows.Count > 0)
                {
                    lblwname.Text = dtname.Rows[0]["Name"].ToString();
                }

                DataTable dtcat = select("select BusinessDetails.BusinessCategory,BusinessDetails.ID,BusinessDetails.year,BusinessDetails.Title,BusinessDetails.kee,BusinessDetails.Corporate,BusinessDetails.Facts,BusinessDetails.noof from BusinessDetails where BusinessDetails.BusinessID='" + ClsEncDesc.Decrypted(Request.QueryString["Id"].ToString()) + "'");

                if (dtcat.Rows.Count > 0)
                {
                    ViewState["IJK"] = dtcat.Rows[0]["ID"].ToString();

                    lblyears.Text = dtcat.Rows[0]["year"].ToString();
                    lbldetails.Text = dtcat.Rows[0]["Title"].ToString();
                    lblkey.Text = dtcat.Rows[0]["kee"].ToString();
                    lblcorporate.Text = dtcat.Rows[0]["Corporate"].ToString();
                    lblfact.Text = dtcat.Rows[0]["Facts"].ToString();
                    lblnos.Text = dtcat.Rows[0]["noof"].ToString();

                    if (Convert.ToString(dtcat.Rows[0]["BusinessCategory"]) != "0")
                    {
                        DataTable dtsubsubcat = select("select B_SubCatID,B_SubSubCategory from BusinessSubSubCat where B_SubSubCatID='" + dtcat.Rows[0]["BusinessCategory"].ToString() + "'");

                        DataTable dtsubcat = select("select B_CatID,B_SubCategory from BusinessSubCat where B_SubCatID='" + dtsubsubcat.Rows[0]["B_SubCatID"].ToString() + "'");

                        DataTable dtcat1 = select("select B_Category from BusinessCategory where B_CatID='" + dtsubcat.Rows[0]["B_CatID"].ToString() + "'");

                        lblcategory.Text = dtcat1.Rows[0]["B_Category"].ToString() + " : " + dtsubcat.Rows[0]["B_SubCategory"].ToString() + " : " + dtsubsubcat.Rows[0]["B_SubSubCategory"].ToString();
                    }
                }

                DataTable dtlogo = select("select LogoUrl from CompanyWebsitMaster where whid='" + ClsEncDesc.Decrypted(Request.QueryString["Id"].ToString()) + "'");

                if (dtlogo.Rows.Count > 0)
                {
                    imgLogo.ImageUrl = "~/ShoppingCart/images/" + dtlogo.Rows[0]["LogoUrl"].ToString();
                }

                DataTable dtdetail = select("select CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Zip,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName from CompanyWebsiteAddressMaster inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where CompanyWebsiteAddressMaster.AddressTypeMasterId='1' and CompanyWebsiteMasterId='" + ClsEncDesc.Decrypted(Request.QueryString["Id"].ToString()) + "'");

                if (dtdetail.Rows.Count > 0)
                {
                    lbladdress.Text = dtdetail.Rows[0]["Address1"].ToString() + "," + dtdetail.Rows[0]["Address2"].ToString();
                    tbZipCode.Text = dtdetail.Rows[0]["Zip"].ToString();
                    Label18.Text = dtdetail.Rows[0]["CityName"].ToString();
                    Label17.Text = dtdetail.Rows[0]["StateName"].ToString();
                    Label16.Text = dtdetail.Rows[0]["CountryName"].ToString();
                }
            }
        }
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void lbllink_Click(object sender, EventArgs e)
    {
        string te = "ViewSlideShow.aspx?id=" + ViewState["IJK"].ToString();
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
}
