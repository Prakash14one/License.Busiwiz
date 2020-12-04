using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;

public partial class BusMaster : System.Web.UI.MasterPage
{
   
    SqlConnection con=new SqlConnection(PageConn.connnn);
    SqlConnection con1;
    private Control myC;
    protected string priceid;
    protected string verid;
    string pageiddd;
    //HttpCookieCollection cook;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Convert.ToString(con.ConnectionString) == "")
        {
            PageConn pgcon = new PageConn();
            con = pgcon.dynconn;
        }
        if (Convert.ToString(PageConn.busdatabase) == "")
        {
            PageConn.licenseconn();
        }
        con1 = PageConn.licenseconn();
        
        if (PageConn.bidname == "")
        {
            PageConn.busclient();
        }
       
        string strData = Request.Url.LocalPath.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["pagename"] = page.ToString();

      

        String pageurl = Request.Url.AbsoluteUri;
        
            


           


        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
        Response.Cache.SetValidUntilExpires(true);
        if (!IsPostBack)
        {
            ddlcountry.Items.Clear();
            DataTable dtc = select("select distinct CountryId,CountryName,Country_Code from CountryMaster Order by CountryName ");
            if (dtc.Rows.Count > 0)
            {
                ddlcountry.DataSource = dtc;
                ddlcountry.DataTextField = "CountryName";
                ddlcountry.DataValueField = "CountryId";
                ddlcountry.DataBind();
            }
            ddlcountry.Items.Insert(0, "Select Country");
            ddlcountry.Items[0].Value = "0";
            ddlcountry_SelectedIndexChanged(sender, e);
            PopulateMenu();

            
        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    private void PopulateMenu()
    {
        DataSet ds = GetDataSetForMenu();
      
        //MenuItem catMasterhome = new MenuItem((string)"Home");

        //Menu1.Items.Add(catMasterhome);

       // MenuItem childrenItem = new MenuItem(ClsEncDesc.DecDyn((string)childItem["SubMenuName"]));

        if (ds.Tables.Count > 0)
        {
            foreach (DataRow parentItem in ds.Tables["MasterP"].Rows)
            {
                //MenuItem ORM = new MenuItem();
             
                //ORM.Text = "MASTER DATA";

                //Menu1.Items.Add(ORM);
                MenuItem categoryItem = new MenuItem();
                categoryItem.Value = Convert.ToString(parentItem["MasParent"]);
                categoryItem.Text = "Search by Category";
                //categoryItem.Value = "";
                Menu1.Items.Add(categoryItem );

                foreach (DataRow parentItem1 in parentItem.GetChildRows("Children"))
                {
                    MenuItem categoryIt = new MenuItem();
                    categoryIt.Value = Convert.ToString(parentItem1["B_CatID"]);
                    categoryIt.Text = (string)parentItem1["B_Category"];
                    //categoryIt.Value = "";
                    categoryItem.ChildItems.Add(categoryIt);

                    foreach (DataRow childItem in parentItem1.GetChildRows("Children1"))
                    {

                        MenuItem childrenItem = new MenuItem();
                        childrenItem.Value = Convert.ToString(childItem["B_SubCatID"]);
                      
                        childrenItem.Text = (string)childItem["B_SubCategory"];

                        categoryIt.ChildItems.Add(childrenItem);

                        foreach (DataRow subchildItem in childItem.GetChildRows("Children2"))
                        {



                            MenuItem childrenItem11 = new MenuItem();

                           
                            childrenItem11.Value = Convert.ToString(subchildItem["B_SubSubCatID"]);
                            childrenItem11.Text = (string)subchildItem["B_SubSubCategory"];
                            childrenItem11.NavigateUrl = "~/Shoppingcart/admin/BusiDirbysearch.aspx?SSI=" + childrenItem11.Value;
                            childrenItem.ChildItems.Add(childrenItem11);

                        }

                    }
                    
                }

            }
        }
      
    }



    private DataSet GetDataSetForMenu()
    {
         DataSet ds=new DataSet();
         SqlDataAdapter adpProductc = new SqlDataAdapter("Select top(1) B_Category, case when(B_CatID IS NULL) then '0' else '0' end as MasParent from BusinessCategory   Order by B_Category", con);
         adpProductc.Fill(ds, "MasterP");

         string str = "Select B_CatID,B_Category,case when(B_CatID IS NULL) then '0' else '0' end as MasParent from BusinessCategory where Active='1' Order by B_Category";

            SqlDataAdapter adpcat = new SqlDataAdapter(str, con);

            adpcat.Fill(ds, "parent");


            SqlDataAdapter adpProduct = new SqlDataAdapter("Select B_SubCategory,B_SubCatID,BusinessSubCat.B_CatID from BusinessCategory inner join BusinessSubCat on BusinessSubCat.B_CatID=BusinessCategory.B_CatID where BusinessCategory.Active='1' and  BusinessSubCat.Active='1' Order by B_SubCategory", con);
            adpProduct.Fill(ds, "child");


            string str15 = "Select B_SubSubCatID,B_SubSubCategory,BusinessSubSubCat.B_SubCatID from BusinessCategory inner join BusinessSubCat on BusinessSubCat.B_CatID=BusinessCategory.B_CatID inner join BusinessSubSubCat on BusinessSubSubCat.B_SubCatID=BusinessSubCat.B_SubCatID where BusinessCategory.Active='1' and  BusinessSubCat.Active='1' and BusinessSubSubCat.Active='1'  Order by B_SubSubCategory";

            SqlDataAdapter adp115 = new SqlDataAdapter(str15, con);
            DataSet ds125 = new DataSet();
            adp115.Fill(ds, "leafchild");



            ds.Relations.Add("Children", ds.Tables["MasterP"].Columns["MasParent"], ds.Tables["parent"].Columns["MasParent"]);

            ds.Relations.Add("Children1", ds.Tables["parent"].Columns["B_CatID"], ds.Tables["child"].Columns["B_CatID"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["B_SubCatID"], ds.Tables["leafchild"].Columns["B_SubCatID"]);
         //   ds.Relations.Add("Children3", ds.Tables["parent"].Columns["MainMenuId"], ds.Tables["leafchild1"].Columns["MainMenuId"]);
        
        return ds;
    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstatecity.Items.Clear();
        if (ddlcountry.SelectedIndex > 0)
        {
                DataTable dtc = select("select distinct StateName+':'+CityName as SCName,CityId from StateMasterTbl inner join CityMasterTbl on CityMasterTbl.StateId=StateMasterTbl.StateId where CountryId='" + ddlcountry.SelectedValue + "' Order by SCName ");
                if (dtc.Rows.Count > 0)
                {
                    ddlstatecity.DataSource = dtc;
                    ddlstatecity.DataTextField = "SCName";
                    ddlstatecity.DataValueField = "CityId";
                    ddlstatecity.DataBind();
                }
           
        }
        ddlstatecity.Items.Insert(0, "Select State:City");
        ddlstatecity.Items[0].Value = "0";
    }

    protected void btnsearchgo_Click(object sender, EventArgs e)
    {
        Session["SSS"] = txtsearch.Text;
        Response.Redirect("BusiDirbysearch.aspx?SSS");
    }
}

