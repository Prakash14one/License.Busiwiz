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
using System.Data.SqlClient;

public partial class customer_CustomerMaster : System.Web.UI.MasterPage
{
    SqlDataAdapter adpcat;
    SqlDataAdapter adpProduct;
    SqlDataAdapter nilesh;
    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        FillLogos();
        if (!IsPostBack)
        {


            string strData = Request.Url.LocalPath.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();




            Session["pagename"] = page.ToString();


            //mainloginlogo

            if (Session["userid"] != null)
            {
                if (Session["id"] != null)
                {
                    SqlCommand cmd4 = new SqlCommand("SELECT     Name FROM         User_master WHERE     UserID = '" + Convert.ToString(Session["id"]) + "'", connection);
                    cmd4.CommandType = CommandType.Text;
                    cmd4.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["Id"]));
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4);

                    Label1.Text = " Welcome " + ds4.Tables[0].Rows[0]["Name"].ToString();

                    SqlCommand cmd99 = new SqlCommand("SELECT      MasterPageId AS MasterPageId FROM  dbo.MasterPageMaster WHERE (MasterPagename = N'CustomerMaster.master')", connection);
                    SqlDataAdapter adp99 = new SqlDataAdapter(cmd99);
                    DataSet ds99 = new DataSet();
                    connection.Open();
                    cmd99.ExecuteNonQuery();
                    adp99.Fill(ds99);
                    if (ds99.Tables.Count > 0)
                    {
                        if (ds99.Tables[0].Rows.Count > 0)
                        {
                            ViewState["MaterPageId"] = ds99.Tables[0].Rows[0][0].ToString();
                        }
                    }

                    DataSet ds = new DataSet();
                    //ds = getmenu();
                    //foreach (DataRow parentitem in ds.Tables["Category"].Rows)
                    //{
                    //    MenuItem categoryItem = new MenuItem((string)parentitem["MainMenuName"]);

                    //    Menu1.Items.Add(categoryItem);
                    //    foreach (DataRow ChildrenItem in parentitem.GetChildRows("Children"))
                    //    {

                    //        MenuItem child = new MenuItem((string)ChildrenItem["SubMenuName"]);
                    //        categoryItem.ChildItems.Add(child);
                    //        //foreach (DataRow child2 in ChildrenItem.GetChildRows("Children2"))
                    //        //{

                    //        //    MenuItem child3 = new MenuItem((string)ChildrenItem["SubSubMenuName"]);
                    //        //    child.ChildItems.Add(child3);

                    //        //}
                    //        //foreach (DataRow parentitem2 in ds.Tables["Product"].Rows)
                    //        //{
                    //        //    MenuItem categoryItem2 = new MenuItem((string)parentitem2["SubMenuName"]);
                    //        //    child.ChildItems.Add(categoryItem2);

                    //        foreach (DataRow ChildrenItem2 in ChildrenItem.GetChildRows("Children2"))
                    //        {
                    //            MenuItem child2 = new MenuItem((string)ChildrenItem2["SubSubMenuName"]);
                    //            child.ChildItems.Add(child2);
                    //        }

                    //        // }
                    //    }
                    //}

                }
                else
                {
                    Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + " ");
                    //Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + "&Wid=" + Session["xx1"] + "&Cname=" + Session["Cname"] + " ");
                    //Response.Redirect("~/Shoppingcart/Default.aspx");
                }
                string a = Convert.ToString(ViewState["Colour"]);
                Menu1.DynamicMenuItemStyle.BackColor = System.Drawing.Color.FromName(a);
            }
            else
            {
                Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + " ");
                //Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + "&Wid=" + Session["xx1"] + "&Cname=" + Session["Cname"] + " ");
                //Response.Redirect("~/Shoppingcart/Default.aspx");
            }
        }

    }
    protected void FillLogos()
    {
        string strMainRedirect = " SELECT     CompanyWebsitMaster.Sitename, CompanyAddressMaster.WebSite, CompanyMaster.CompanyId, CompanyMaster.CompanyLogo, " +
                     "  CompanyWebsiteAddressMaster.LiveChatUrl,CompanyMaster.CompanyName " +
                     " FROM         CompanyWebsitMaster LEFT OUTER JOIN " +
                     " CompanyWebsiteAddressMaster ON  " +
                     " CompanyWebsitMaster.CompanyWebsiteMasterId = CompanyWebsiteAddressMaster.CompanyWebsiteMasterId RIGHT OUTER JOIN " +
                     " CompanyMaster ON CompanyWebsitMaster.CompanyId = CompanyMaster.CompanyId LEFT OUTER JOIN " +
                     " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId where CompanyMaster.compid='" + Session["comid"] + "'  ";
        SqlCommand cmdRedirect = new SqlCommand(strMainRedirect, connection);
        SqlDataAdapter adpRedirect = new SqlDataAdapter(cmdRedirect);
        DataTable dtRedirect = new DataTable();
        adpRedirect.Fill(dtRedirect);
        if (dtRedirect.Rows.Count > 0)
        {

            mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtRedirect.Rows[0]["CompanyLogo"].ToString();
            // RightImage.ImageUrl = "~/ShoppingCart/images/"+dtRedirect.Rows[0]["LiveChatUrl"].ToString();

        }
        else
        {

            mainloginlogo.ImageUrl = "#";
            // RightImage.ImageUrl = "#";

        }

    }

    public DataSet getmenu()
    {

        //adpcat = new SqlDataAdapter("Select * from MainMenuMaster where MasterPageId=" + ViewState["MaterPageId"] + "", connection);
        DataSet ds = new DataSet();
        //adpcat.Fill(ds, "Category");
        //ViewState["row"] = ds.Tables[0].Rows.Count;
        //int a = Convert.ToInt32(ViewState["row"]);
        ////DataSet ds = new DataSet();
        //for (int i = 0; i < a; i++)
        //{

        //    adpProduct = new SqlDataAdapter("Select * From SubMenuMaster where MainMenuId=" + ds.Tables[0].Rows[i]["MainMenuId"] + "", connection);
        //    //DataSet ds = new DataSet();
        //    adpProduct.Fill(ds, "Product");
        //    ds = (DataSet)ds;

        //}

        // adpProduct = new SqlDataAdapter("Select * From SubMenuMaster", connection);
        //ViewState["rowSubmenu"] = ds.Tables["Product"].Rows.Count;
        //int b = Convert.ToInt32(ViewState["rowSubmenu"]);
        //for (int i = 0; i < b; i++)
        //{
        //    nilesh = new SqlDataAdapter("Select * from SubSubMenuMaster where SubMenuId=" + ds.Tables[1].Rows[i]["SubMenuId"] + " order by SubSubMenuName", connection);

        //    nilesh.Fill(ds, "Nilesh");
        //    ds = (DataSet)ds;

        //} //DataSet ds = new DataSet();
        ////  adpcat.Fill(ds, "Category");
        //// adpProduct.Fill(ds, "Product");
        //// nilesh.Fill(ds, "Nilesh");

        //ds.Relations.Add("Children", ds.Tables["Category"].Columns["MainMenuId"], ds.Tables["product"].Columns["MainMenuId"]);
        //ds.Relations.Add("Children2", ds.Tables["Product"].Columns["SubMenuId"], ds.Tables["Nilesh"].Columns["SubMenuId"]);
        //ViewState["Colour"] = ds.Tables[0].Rows[0]["BackColour"].ToString();
        return ds;
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("customerafterlogin.aspx");
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //Session.Clear();
        //Response.Redirect("~/Shoppingcart/Default.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("customerafterlogin.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["Id"] = null;
        // Session.Clear();
        Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + " ");
        //Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + "&Wid=" + Session["xx1"] + "&Cname=" + Session["Cname"] + " ");
        //Response.Redirect("~/Shoppingcart/Default.aspx");
        //Session.Clear();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session.Clear();
        //    Response.Redirect("~/Shoppingcart/Default.aspx");
        //Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + "&Cname=" + Session["Cname"] + " ");
        Response.Redirect("~/Shoppingcart/Default.aspx?Cid=" + Session["Comid"] + " ");
    }
    protected override void AddedControl(Control control, int index)
    {
        if (Request.ServerVariables["http_user_agent"]
        .IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
        {
            this.Page.ClientTarget = "uplevel";
        }

        base.AddedControl(control, index);
    } 
}
