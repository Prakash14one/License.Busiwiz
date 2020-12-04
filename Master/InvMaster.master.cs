using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class InvMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public string cssdyn = "Registrationstyle.css";
    public string VBF = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        VBF = Convert.ToString(Session["VBF"]);

        // pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ib = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ib - 1].ToString();
        filldatapage(page);

        // Page.Title = pg.getPageTitle(page); 
        
        if (!IsPostBack)
        {

            //Session["PortalName"] = "Onlineaccounts.net";
            string PrId = "";

            if (Request.QueryString["pid"] != null || Convert.ToString(Session["ProductId"]) != "")
            {
                if (Request.QueryString["pid"] != null)
                {
                    Session["ProductId"] = Request.QueryString["pid"];
                }
                string str = " select PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName,PricePlanMaster.PricePlanName,PricePlanAmount from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId   where PricePlanMaster.PricePlanId='" + Session["ProductId"] + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session["PrId"] = dt.Rows[0]["ProductId"].ToString();
                    ImageButton1.ImageUrl = "~/images/" + dt.Rows[0]["LogoPath"].ToString() + " ";
                    lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                    lblportalname.Text = dt.Rows[0]["PortalName"].ToString();
                    lbladresss.Text = dt.Rows[0]["Address1"].ToString();

                 //   lbldata.Text = "Price Plan selected  : " + dt.Rows[0]["PricePlanName"].ToString() + ", Amount : " + dt.Rows[0]["PricePlanAmount"].ToString();
                    string ext = "";
                    string tfext = "";
                    if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                    {
                        ext = " ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
                    }

                    if (Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != null)
                    {
                        tfext = " ext " + dt.Rows[0]["Tollfreeext"].ToString();
                    }
                    lblphoneno.Text = dt.Rows[0]["Supportteamphoneno"].ToString() + ext;
                    lbltollfree.Text = dt.Rows[0]["Tollfree"].ToString() + tfext;
                    lblemail.Text = dt.Rows[0]["Supportteamemailid"].ToString();
                    lblcity.Text = dt.Rows[0]["City"].ToString();
                    lblstate.Text = dt.Rows[0]["StateName"].ToString();
                    lblcountry.Text = dt.Rows[0]["CountryName"].ToString();
                    LinkButton1.Text = dt.Rows[0]["Portalmarketingwebsitename"].ToString();

                }


            }

        }
        if (Convert.ToString(Session["VBF"]) != "")
        {
            VBF = Convert.ToString(Session["VBF"]);
        }
        else
        {
            GNCLS();
        }
        if (!IsPostBack)
        {
            float A1 = 0;
            float A2 = 0;
            float A3 = 0;
            DataSet ds = new DataSet();
            ds = (DataSet)getCategory();
            foreach (DataRow pnode in ds.Tables[0].Rows)
            {
                TreeNode parentnode = new TreeNode(pnode["InventoryCatName"].ToString());
                parentnode.CollapseAll();

                //Tree1.Nodes.Add(parentnode);

                int i = Convert.ToInt32(pnode.GetChildRows("Children").Length);
                if (i == 0)
                {

                    goto A;
                }
                else
                {
                    Tree1.Nodes.Add(parentnode);

                }
                foreach (DataRow cnode in pnode.GetChildRows("Children"))
                {
                    TreeNode childnode = new TreeNode(cnode["InventorySubCatName"].ToString());
                    childnode.CollapseAll();
                    //parentnode.ChildNodes.Add(childnode);
                    //childnode.ImageUrl = "Image/txt.gif";

                    int j = Convert.ToInt32(cnode.GetChildRows("Children2").Length);
                    if (j == 0)
                    {
                        goto B;
                    }
                    else
                    {
                        parentnode.ChildNodes.Add(childnode);

                    }

                    foreach (DataRow lnode in cnode.GetChildRows("Children2"))
                    {
                        TreeNode leafnode = new TreeNode(lnode["InventorySubSubName"].ToString());

                        string stt = "    SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryImgMaster.Thumbnail, InventoryWarehouseMasterTbl.QtyOnHand, " +
"   InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, " +
"   InventoryDetails.Weight, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description, InventoruSubSubCategory.InventorySubSubId,  " +
"   InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
"   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
" FROM         InventorySubCategoryMaster LEFT OUTER JOIN " +
"   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
"   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
"   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId LEFT OUTER JOIN " +
"   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
"   InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId RIGHT OUTER JOIN " +
"   InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId " +
" WHERE     PortalId='" + Session["POr"] + "' and (InventoryWarehouseMasterTbl.Active = 1) AND (InventoryMaster.InventorySubSubId = '" + Convert.ToInt32(lnode["InventorySubSubId"]) + "') " +
                  "  and (InventoryMaster.MasterActiveStatus=1)and(InventoryWarehouseMasterTbl.Active = 1) ";


                        //SqlCommand cmd1 = new SqlCommand(" SELECT Name, InventoryMasterId, InventorySubSubId FROM  InventoryMaster WHERE (InventorySubSubId = '" + Convert.ToInt32(lnode["InventorySubSubId"]) + "')", con);
                        SqlCommand cmd1 = new SqlCommand(stt, con);

                        SqlDataAdapter ad = new SqlDataAdapter(cmd1);
                        DataSet dsnew = new DataSet();
                        ad.Fill(dsnew);
                        if (Convert.ToInt32(dsnew.Tables[0].Rows.Count) == 0)
                        {

                        }
                        else
                        {
                            childnode.ChildNodes.Add(leafnode);
                        }



                    }
                B: ;
                    int d = Convert.ToInt32(childnode.ChildNodes.Count);
                    if (d == 0)
                    {
                        parentnode.ChildNodes.Remove(childnode);
                    }
                }
            A: ;
                int k = Convert.ToInt32(parentnode.ChildNodes.Count);
                if (k == 0)
                {
                    Tree1.Nodes.Remove(parentnode);
                }
            }

            if (Session["parentnode"] != null)
            {
                Panel1.Visible = true;
                foreach (TreeNode node in Tree1.Nodes) //Tree1.Nodes[0].ChildNodes)
                {
                    if (node.Text == Session["parentnode"].ToString())
                    {
                        foreach (TreeNode child in node.ChildNodes)
                        {
                            if (child.Text == Session["childnode"].ToString())
                            {
                                foreach (TreeNode leaf in child.ChildNodes)
                                {
                                    if (leaf.Text == Session["leafnode"].ToString())
                                    {
                                        leaf.Select();
                                    }
                                }

                                child.Expand();
                                child.Parent.Expand();
                            }
                        }

                    }
                }
            }
        }

    }
    protected void GNCLS()
    {
        if (Convert.ToString(Session["PortalName"]) != "")
        {
            string str = " select Colorportal  from  PortalMasterTbl  where Upper(PortalMasterTbl.PortalName)='" + Session["PortalName"] + "' and ProductId='" + Session["PrId"] + "' ";

            //  string str = " select ClientMaster.*,ProductMaster.ProductId,PortalMasterTbl.LogoPath  from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Request.QueryString["id"] + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                VBF = Convert.ToString(dt.Rows[0]["Colorportal"]);
                Session["VBF"] = VBF;
                //VBF = "#d4f8ba";
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "http://" + LinkButton1.Text;

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "http://" + LinkButton1.Text;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblportalname_Click(object sender, EventArgs e)
    {
        string te = "http://" + LinkButton1.Text;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    public DataSet getCategory()
    {

        string str = "SELECT  InventeroyCatId, InventoryCatName FROM InventoryCategoryMaster where compid='" + Session["ClientId"] + "' ORDER BY InventoryCatName";

        SqlDataAdapter adpcat = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        adpcat.Fill(ds, "parent");
        ViewState["row"] = ds.Tables[0].Rows.Count;
        int a = Convert.ToInt32(ViewState["row"]);

        for (int i = 0; i < a; i++)
        {
            string str11 = "SELECT InventorySubCatId, InventorySubCatName,InventoryCategoryMasterId FROM InventorySubCategoryMaster " +
" WHERE  (InventoryCategoryMasterId = '" + ds.Tables[0].Rows[i]["InventeroyCatId"] + "' ) ORDER BY InventorySubCatName";

            SqlDataAdapter adpProduct = new SqlDataAdapter(str11, con);
            adpProduct.Fill(ds, "child");
            ds = (DataSet)ds;

        }
        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["rowSubnode"] = ds.Tables["child"].Rows.Count;
            int b = Convert.ToInt32(ViewState["rowSubnode"]);
            for (int i = 0; i < b; i++)
            {
                string str1 = "SELECT   InventorySubSubId, InventorySubSubName,InventorySubCatID FROM InventoruSubSubCategory " +
            " WHERE  (InventorySubCatID ='" + ds.Tables["child"].Rows[i]["InventorySubCatId"] + "') ORDER BY InventorySubSubName";

                SqlDataAdapter adp11 = new SqlDataAdapter(str1, con);
                adp11.Fill(ds, "leafchild");
                ds = (DataSet)ds;

            }

            ds.Relations.Add("Children", ds.Tables["parent"].Columns["InventeroyCatId"], ds.Tables["child"].Columns["InventoryCategoryMasterId"]);
            ds.Relations.Add("Children2", ds.Tables["child"].Columns["InventorySubCatId"], ds.Tables["leafchild"].Columns["InventorySubCatId"]);
        }
        //ViewState["Colour"] = ds.Tables[0].Rows[0]["BackColour"].ToString();
        return ds;
    }



    protected void Tree1_SelectedNodeChanged(object sender, EventArgs e)
    {

        string id = Tree1.SelectedValue;
        //string str = "SELECT  InventorySubSubId, InventorySubSubName FROM  InventoruSubSubCategory where " +          " InventorySubSubName='" + id +"' ";
        string str = "SELECT     InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName " +
                       " FROM         InventorySubCategoryMaster INNER JOIN " +
                           " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
                           " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                        " WHERE     (InventoruSubSubCategory. InventorySubSubName='" + id + "') and InventoryCategoryMaster.compid='" + Session["ClientId"] + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string str2 = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId, InventoryCategoryMaster.InventoryCatName " +
                        " FROM         InventorySubCategoryMaster INNER JOIN " +
                            " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID INNER JOIN " +
                            " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                         " WHERE     (InventoruSubSubCategory.InventorySubSubId = '" + ds.Tables[0].Rows[0]["InventorySubSubId"].ToString() + " ')";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);



            // Tree1.SelectedNode.Parent.Expand();
            Session["leafnode"] = ds.Tables[0].Rows[0]["InventorySubSubName"].ToString();
            Session["childnode"] = ds2.Tables[0].Rows[0]["InventorySubCatName"].ToString();
            Session["parentnode"] = ds2.Tables[0].Rows[0]["InventoryCatName"].ToString();
            Response.Redirect("ProductList.aspx?ProductID=" + ds.Tables[0].Rows[0]["InventorySubSubId"].ToString() + " ");

        }
        else
        {
            Tree1.CollapseAll();
            Tree1.SelectedNode.ToggleExpandState();
            foreach (TreeNode node in Tree1.Nodes)
            {
                foreach (TreeNode cnode in node.ChildNodes)
                {
                    if (Tree1.SelectedNode == cnode)
                    {
                        cnode.Parent.Expand();
                    }
                }
            }
        }
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }
   
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void filldatapage(string page)
    {
        Boolean ABC = false;

         ABC = page.Contains("ProductList");
         if (ABC == true)
         {
            // lblmsc.Text = "List 0f Items";
         }
         else if (page.Contains("ProductDetail") == true)
         {
             //lblmsc.Text = "Items Details";
         }
    }
}
