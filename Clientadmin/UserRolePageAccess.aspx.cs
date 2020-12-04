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

public partial class UserRolePageAccess : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);  
    public static string defaRoleid = "";
    public static string gtype = ""; 
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();
            fillportal();
            //ddlProductname_SelectedIndexChanged(sender, e);
            //fillgd();
        }
    }
    protected void FillProduct()
    {
        //Session["ClientId"] = "35";
        //Session["verId"] = "5";
        //string strcln = " SELECT * from  ProductMaster where ClientMasterId= " + Session["ClientId"].ToString();
        string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
         strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM dbo.ClientProductTableMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active =1 order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");        
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillportal();
    }
    protected void fillportal()
    {
        ddlportal.Items.Clear();
        if (ddlProductname.SelectedIndex > 0)
        {
            string activestr = "";
            activestr = " and PortalMasterTbl.Status=1";           
            string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname.SelectedValue + "' ) " + activestr + " order by PortalName";
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            ddlportal.DataSource = dtcln22v;

            ddlportal.DataValueField = "Id";
            ddlportal.DataTextField = "PortalName";
            ddlportal.DataBind();

            ddlportal.Items.Insert(0, "-Select-");
            ddlportal.Items[0].Value = "0";
        }
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPortalCategory();
        FillPricePlan();       
    }
    protected void FillPortalCategory()
    {
        string strcln = " SELECT distinct * FROM Priceplancategory inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId where PortalId='" + ddlportal.SelectedValue + "' order  by CategoryName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlpriceplancatagory.DataSource = dtcln;
        ddlpriceplancatagory.DataTextField = "CategoryName";
        ddlpriceplancatagory.DataValueField = "Id";
        ddlpriceplancatagory.DataBind();
    }
    protected void ddlpriceplancatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPricePlan();       
    }
    protected void FillPricePlan()
    {
        try
        {
            ddlpriceplan.DataSource = null;
            ddlpriceplan.DataBind();
            Label1.Text = "";            
            DataTable dts = MyCommonfile.selectBZ("SELECT dbo.ProductMaster.ProductName, dbo.PortalMasterTbl.PortalName + ' -- ' + dbo.Priceplancategory.CategoryName + ' -- ' + dbo.PricePlanMaster.PricePlanName AS PricePlanName,  dbo.PricePlanMaster.PricePlanId FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where  Priceplancategory.ID=" + ddlpriceplancatagory.SelectedValue + " and PricePlanMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and dbo.Priceplancategory.PortalId='" + ddlportal.SelectedValue + "' and PricePlanMaster.active='True'");
            if (dts.Rows.Count > 0)
            {
                lbl_noofpp.Text = "Total " + Convert.ToString(dts.Rows.Count) + " priceplan available in this category";
                ddlpriceplan.DataSource = dts;
                ddlpriceplan.DataValueField = "PricePlanId";
                ddlpriceplan.DataTextField = "PricePlanName";
                ddlpriceplan.DataBind();
                fillpl();
            }
            else
            {
                lbl_noofpp.Text = "";
            }
        }
        catch
        {
        }
    }
    protected void ddlpriceplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillpl();
    }
    protected void fillpl()
    {
        gtype = "";
        pnldisp.Visible = false;
        pnldisp.Visible = true;
        defaRoleid = "";
        filldl();
        Panel1.Visible = false;
        grdmain.DataSource = null;
        grdmain.DataBind();
        Grdsub.DataSource = null;
        Grdsub.DataBind();
        grdpage.DataSource = null;
        grdpage.DataBind();
        pnladddata.Visible = false;
        imgmainmanu.ImageUrl = "Images/plus.png";
        imgmainmanu.AlternateText = "Plush";
        pnlmain.Visible = false;
        pnlpage.Visible = false;
        pnlsubmanu.Visible = false;
        imgsubm.ImageUrl = "Images/plus.png";
        imgsubm.AlternateText = "Plush";
        imgpage.ImageUrl = "Images/plus.png";
        imgpage.AlternateText = "Plush";
    }
    protected void filldl()
    {
        string strcln = " select distinct DefaultRole.RoleId,DeptName+':'+DesignationName as RoleName from   DefaultRole inner join DefaultRolIdforePriceplan on DefaultRolIdforePriceplan.DefaultRoleId=DefaultRole.RoleId inner join DefaultDesignationTbl on DefaultDesignationTbl.RoleId=DefaultRole.RoleId inner join DefaultDept on DefaultDept.DeptId=DefaultDesignationTbl.DeptId where  DefaultRolIdforePriceplan.Priceplanid='" + ddlpriceplan.SelectedValue + "' order by RoleName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        Dataavail.DataSource = dtcln;
        Dataavail.DataBind();
        ddlrolemode.DataSource = dtcln;
        ddlrolemode.DataValueField = "RoleId";
        ddlrolemode.DataTextField = "RoleName";
        ddlrolemode.DataBind();
        ddlrolemode.Items.Insert(0, "Select");
        ddlrolemode.Items[0].Value = "0";
    }
    protected void ddlrolemode_SelectedIndexChanged(object sender, EventArgs e)
    {
        defaRoleid = "";
        Panel1.Visible = false;
        grdmain.DataSource = null;
        grdmain.DataBind();
        Grdsub.DataSource = null;
        Grdsub.DataBind();
        grdpage.DataSource = null;
        grdpage.DataBind();
        imgmainmanu.ImageUrl = "Images/plus.png";
        imgmainmanu.AlternateText = "Plush";
        pnlmain.Visible = false;
        pnlpage.Visible = false;
        pnlsubmanu.Visible = false;
        imgsubm.ImageUrl = "Images/plus.png";
        imgsubm.AlternateText = "Plush";
        imgpage.ImageUrl = "Images/plus.png";
        imgpage.AlternateText = "Plush";
        string rolid = "";
        if (ddlrolemode.SelectedIndex > 0)
        {
            if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
            {
                foreach (DataListItem item in Dataavail.Items)
                {
                    rolid = Dataavail.DataKeys[item.ItemIndex].ToString();
                    CheckBox chkde = (CheckBox)(Dataavail.Items[item.ItemIndex].FindControl("lbllist"));
                    if (Convert.ToString(rolid) == Convert.ToString(ddlrolemode.SelectedValue))
                    {
                        chkde.Checked = true;
                    }
                    else
                    {
                        chkde.Checked = false;
                    }
                }
            }
            Panel1.Visible = true;
            defaRoleid = ddlrolemode.SelectedValue;
            FillGrid();
            if (rdmode.SelectedValue == "3")
            {
                gtype = "";
                imgmainmanu.ImageUrl = "Images/minus.png";
                imgmainmanu.AlternateText = "Minus";
                pnlmain.Visible = true;
                pnlpage.Visible = true;
                pnlsubmanu.Visible = true;
                imgsubm.ImageUrl = "Images/minus.png";
                imgsubm.AlternateText = "Minus";
                imgpage.ImageUrl = "Images/minus.png";
                imgpage.AlternateText = "Minus";
                fillSubG();
                fillPageMain();
                pnladddata.Visible = false;
                Panel1.Enabled = false;
            }
            else if (rdmode.SelectedValue == "2")
            {
                pnladddata.Visible = false;
            }
        }
    }





    protected void filtermainmenu()
    {
        ddlmainfilter.Items.Clear();
        string strcln = " SELECT distinct MainMenuMaster.*, MainMenuMaster.MainMenuTitle as Page_Name from MainMenuMaster  inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId where pageplaneaccesstbl.Priceplanid='" + ddlpriceplan.SelectedValue + "'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlmainfilter.DataSource = dtcln;

        ddlmainfilter.DataValueField = "MainMenuId";
        ddlmainfilter.DataTextField = "Page_Name";
        ddlmainfilter.DataBind();
        ddlmainfilter.Items.Insert(0, "All");
        ddlmainfilter.Items[0].Value = "0";

        ddlmailpagefilter.DataSource = dtcln;
        ddlmailpagefilter.Items.Clear();
        ddlmailpagefilter.DataValueField = "MainMenuId";
        ddlmailpagefilter.DataTextField = "Page_Name";
        ddlmailpagefilter.DataBind();
        ddlmailpagefilter.Items.Insert(0, "All");
        ddlmailpagefilter.Items[0].Value = "0";
        ddlsubpagefilter.Items.Clear();
        ddlsubpagefilter.Items.Insert(0, "All");
        ddlsubpagefilter.Items[0].Value = "0";
    }
    protected void filtersubmenu()
    {
        ddlsubpagefilter.Items.Clear();
        string strcln = " SELECT distinct SubMenuMaster.* from  SubMenuMaster inner join MainMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  where  SubMenuMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlsubpagefilter.DataSource = dtcln;

        ddlsubpagefilter.DataValueField = "SubMenuId";
        ddlsubpagefilter.DataTextField = "SubMenuName";
        ddlsubpagefilter.DataBind();

        ddlsubpagefilter.Items.Insert(0, "All");
        ddlsubpagefilter.Items[0].Value = "0";
    }
   
    protected void FillGrid()
    {
        string RoleD = "";
        string par = "";
        string strcln = "";
        DataTable dtTemp = new DataTable();
       
            filtermainmenu();
           string strmode = "";
            if (rdmode.SelectedValue == "3")
            {
                strmode = " inner join DefaultRolewisePageAccess   on DefaultRolewisePageAccess.PageId=pageplaneaccesstbl.PageId and  DefaultRolewisePageAccess.Priceplanid='" + ddlpriceplan.SelectedValue + "' and RoleId='" + defaRoleid + "' ";
            }
            DataTable dts = MyCommonfile.selectBZ(" select Distinct MainMenuMaster.MainMenuId, MainMenuMaster.MainMenuName   from MainMenuMaster inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid "+strmode+"  where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' " + par + " order by MainMenuMaster.MainMenuName");
            if (dts.Rows.Count > 0)
            {
                dtTemp = CreatedataManu();
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                    if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                    {

                        int accesno1 = 0;
                        int accesno2 = 0;
                        int Edit_Right = 0;
                        int Delete_Right = 0;
                        int Download_Right = 0;
                        int Insert_Right = 0;
                        int Update_Right = 0;
                        int View_Right = 0;
                        int Go_Right = 0;
                        int SendMail_Right = 0;
                        DataTable dtcln1 = MyCommonfile.selectBZ(" select  Count(Distinct pageplaneaccesstbl.PageId) as CD from   pageplaneaccesstbl  inner join PageMaster  on PageMaster.PageId=pageplaneaccesstbl.PageId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "' and pageplaneaccesstbl.Priceplanid='" + ddlpriceplan.SelectedValue + "' ");
                        DataTable dtcln = new DataTable();
                        dtcln = MyCommonfile.selectBZ(" select  Distinct DefaultRolewisePageAccess.* from   DefaultRolewisePageAccess inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=DefaultRolewisePageAccess.PageId inner join PageMaster  on PageMaster.PageId=pageplaneaccesstbl.PageId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where  PageMaster.MainMenuId='" + dts.Rows[i]["MainMenuId"] + "' and DefaultRolewisePageAccess.Priceplanid='" + ddlpriceplan.SelectedValue + "' and RoleId='" + defaRoleid + "'");
                        foreach (DataRow item in dtcln.Rows)
                        {
                            if (Convert.ToString(item["AccessRight"]) == "1")
                            {
                                accesno1 = accesno1 + 1;
                            }
                            else if (Convert.ToString(item["AccessRight"]) == "2")
                            {
                                accesno2 = accesno2 + 1;
                            }
                            if (Convert.ToString(item["Edit_Right"]) == "True")
                            {
                                Edit_Right = Edit_Right + 1;
                            }
                            if (Convert.ToString(item["Delete_Right"]) == "True")
                            {
                                Delete_Right = Delete_Right + 1;
                            }
                            if (Convert.ToString(item["Download_Right"]) == "True")
                            {
                                Download_Right = Download_Right + 1;
                            }
                            if (Convert.ToString(item["Insert_Right"]) == "True")
                            {
                                Insert_Right = Insert_Right + 1;
                            }
                            if (Convert.ToString(item["Update_Right"]) == "True")
                            {
                                Update_Right = Update_Right + 1;
                            }
                            if (Convert.ToString(item["View_Right"]) == "True")
                            {
                                View_Right = View_Right + 1;
                            }
                            if (Convert.ToString(item["Go_Right"]) == "True")
                            {
                                Go_Right = Go_Right + 1;
                            }
                            if (Convert.ToString(item["SendMail_Right"]) == "True")
                            {
                                SendMail_Right = SendMail_Right + 1;
                            }

                        }
                        if (accesno1 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                        {
                            dtadd["AccessRight"] = "1";
                            dtadd["Edit_Right"] = Convert.ToBoolean(1);
                            dtadd["Delete_Right"] = Convert.ToBoolean(1);
                            dtadd["Download_Right"] = Convert.ToBoolean(1);
                            dtadd["Insert_Right"] = Convert.ToBoolean(1);
                            dtadd["Update_Right"] = Convert.ToBoolean(1);
                            dtadd["View_Right"] = Convert.ToBoolean(1);
                            dtadd["Go_Right"] = Convert.ToBoolean(1);
                            dtadd["SendMail_Right"] = Convert.ToBoolean(1);
                        }
                        else
                        {
                            if (accesno2 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                            {
                                dtadd["AccessRight"] = "2";
                                if (Edit_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                }
                                if (Delete_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                }
                                if (Download_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(0);
                                }
                                if (Insert_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                }
                                if (Update_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(0);
                                }
                                if (View_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(0);
                                }
                                if (Go_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(0);
                                }
                                if (SendMail_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                                }
                            }
                            else
                            {
                                dtadd["AccessRight"] = "0";
                                dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                dtadd["Download_Right"] = Convert.ToBoolean(0);
                                dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                dtadd["Update_Right"] = Convert.ToBoolean(0);
                                dtadd["View_Right"] = Convert.ToBoolean(0);
                                dtadd["Go_Right"] = Convert.ToBoolean(0);
                                dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                            }
                        }


                    }
                    else
                    {
                        dtadd["AccessRight"] = "0";
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);



                    }
                    dtTemp.Rows.Add(dtadd);
                }

            }

           

            grdmain.DataSource = dtTemp;
            DataView myDataView = new DataView();
            myDataView = dtTemp.DefaultView;
            hdnsortExp.Value = "MainMenuName";
            hdnsortDir.Value = "Asc";
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            ViewState["Maing"] = dtTemp;
            grdmain.DataBind();

        
    }
    public DataTable CreatedataManu()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);


        return dtTemp;
    }
    public DataTable CreatedataSubManu()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd1x = new DataColumn();
        prd1x.ColumnName = "SubMenuName";
        prd1x.DataType = System.Type.GetType("System.String");
        prd1x.AllowDBNull = true;
        dtTemp.Columns.Add(prd1x);

        DataColumn prd11z = new DataColumn();
        prd11z.ColumnName = "SubMenuId";
        prd11z.DataType = System.Type.GetType("System.String");
        prd11z.AllowDBNull = true;
        dtTemp.Columns.Add(prd11z);


        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);
        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);
        return dtTemp;
    }
    public DataTable CreatedataPage()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "MainMenuName";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "MainMenuId";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd1x = new DataColumn();
        prd1x.ColumnName = "SubMenuName";
        prd1x.DataType = System.Type.GetType("System.String");
        prd1x.AllowDBNull = true;
        dtTemp.Columns.Add(prd1x);

        DataColumn prd11z = new DataColumn();
        prd11z.ColumnName = "SubMenuId";
        prd11z.DataType = System.Type.GetType("System.String");
        prd11z.AllowDBNull = true;
        dtTemp.Columns.Add(prd11z);


        DataColumn prd1xo = new DataColumn();
        prd1xo.ColumnName = "PageName";
        prd1xo.DataType = System.Type.GetType("System.String");
        prd1xo.AllowDBNull = true;
        dtTemp.Columns.Add(prd1xo);

        DataColumn prd11zp = new DataColumn();
        prd11zp.ColumnName = "PageId";
        prd11zp.DataType = System.Type.GetType("System.String");
        prd11zp.AllowDBNull = true;
        dtTemp.Columns.Add(prd11zp);

        DataColumn prd11ti = new DataColumn();
        prd11ti.ColumnName = "PageTitle";
        prd11ti.DataType = System.Type.GetType("System.String");
        prd11ti.AllowDBNull = true;
        dtTemp.Columns.Add(prd11ti);


        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "AccessRight";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);
        /////1 
        DataColumn prd1f = new DataColumn();
        prd1f.ColumnName = "Edit_Right";
        prd1f.DataType = System.Type.GetType("System.Boolean");
        prd1f.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f);

        DataColumn ptrc = new DataColumn();
        ptrc.ColumnName = "Delete_Right";
        ptrc.DataType = System.Type.GetType("System.Boolean");
        ptrc.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc);

        DataColumn prd1c = new DataColumn();
        prd1c.ColumnName = "Download_Right";
        prd1c.DataType = System.Type.GetType("System.Boolean");
        prd1c.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c);

        /////2
        DataColumn prd1f2 = new DataColumn();
        prd1f2.ColumnName = "Insert_Right";
        prd1f2.DataType = System.Type.GetType("System.Boolean");
        prd1f2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f2);

        DataColumn ptrc2 = new DataColumn();
        ptrc2.ColumnName = "Update_Right";
        ptrc2.DataType = System.Type.GetType("System.Boolean");
        ptrc2.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc2);

        DataColumn prd1c2 = new DataColumn();
        prd1c2.ColumnName = "View_Right";
        prd1c2.DataType = System.Type.GetType("System.Boolean");
        prd1c2.AllowDBNull = true;
        dtTemp.Columns.Add(prd1c2);

        /////3
        DataColumn prd1f3 = new DataColumn();
        prd1f3.ColumnName = "Go_Right";
        prd1f3.DataType = System.Type.GetType("System.Boolean");
        prd1f3.AllowDBNull = true;
        dtTemp.Columns.Add(prd1f3);

        DataColumn ptrc3 = new DataColumn();
        ptrc3.ColumnName = "SendMail_Right";
        ptrc3.DataType = System.Type.GetType("System.Boolean");
        ptrc3.AllowDBNull = true;
        dtTemp.Columns.Add(ptrc3);


        return dtTemp;
    }
   
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }
            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
  
    protected void chkrole_chachedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        DataListItem row = (DataListItem)ch.NamingContainer;
        int rinrow = row.ItemIndex;
        string MasterRId = Dataavail.DataKeys[rinrow].ToString();
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");
        if (rdmode.SelectedValue == "2")
        {
            foreach (DataListItem item in Dataavail.Items)
            {
                CheckBox chkde = (CheckBox)(Dataavail.Items[item.ItemIndex].FindControl("lbllist"));
                chkde.Enabled = false;
            }
        }
        if (Panel1.Visible == false)
        {
            Panel1.Visible = true;
            defaRoleid = MasterRId;
            FillGrid();            
        }
    }
    protected void fillg(GridView grd)
    {
        foreach (GridViewRow gd in grd.Rows)
        {
            RadioButtonList rdlist = (RadioButtonList)gd.FindControl("RadioButtonList1");
            CheckBox Checksendmail_Allow = (CheckBox)gd.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)gd.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)gd.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)gd.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)gd.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)gd.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)gd.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)gd.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)gd.FindControl("CheckBoxSendMail1");
            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Enabled = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {                
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Enabled = true;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Enabled = true;
            }
        }
    }
    protected void chkedit_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxEdit1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkdelete_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDelete1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkDownload_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxDownload1"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void chkInsert_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxInsert1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }


    }
    protected void chkUpdate_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxUpdate1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkView_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkGo_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxGo1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxView1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkSendMail_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmain.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in Grdsub.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
        foreach (GridViewRow rowitem in grdpage.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBoxSendMail1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void rlheader_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList chk;
        if (pnlmain.Visible == true)
        {
            foreach (GridViewRow rowitem in grdmain.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                fillaccessgrid();

            }
        }
        if (pnlsubmanu.Visible == true)
        {
            foreach (GridViewRow rowitem in Grdsub.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                fillaccessgrid1();
            }
        }
        if (pnlpage.Visible == true)
        {
            foreach (GridViewRow rowitem in grdpage.Rows)
            {
                chk = (RadioButtonList)(rowitem.FindControl("RadioButtonList1"));
                chk.SelectedValue = ((RadioButtonList)sender).SelectedValue;
                fillaccessgrid2();
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(grdmain.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {

            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }


    }

  
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(grdpage.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)grdpage.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }


    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(Grdsub.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)Grdsub.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }


    }
    protected void RadioButtonListgrdmain_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((RadioButtonList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        // Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");

        RadioButtonList rdlist;

        rdlist = (RadioButtonList)(grdmain.Rows[rinrow].FindControl("RadioButtonList1"));
        CheckBox Checksendmail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        CheckBox CheckEdit_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxEdit1");
        CheckBox CheckDelete_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDelete1");
        CheckBox CheckDownload_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxDownload1");
        CheckBox CheckInsert_Allows = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxInsert1");
        CheckBox CheckUpdate_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxUpdate1");
        CheckBox CheckView_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxView1");
        CheckBox CheckGo_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxGo1");

        CheckBox CheckSendMail_Allow = (CheckBox)grdmain.Rows[rinrow].FindControl("CheckBoxSendMail1");
        if (rdlist.SelectedValue == "0")
        {
            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "1")
        {
            Checksendmail_Allow.Checked = true;
            Checksendmail_Allow.Enabled = false;
            CheckEdit_Allow.Checked = true;
            CheckEdit_Allow.Enabled = false;
            CheckDelete_Allow.Checked = true;
            CheckDelete_Allow.Enabled = false;
            CheckDownload_Allow.Checked = true;
            CheckDownload_Allow.Enabled = false;
            CheckInsert_Allows.Checked = true;
            CheckInsert_Allows.Enabled = false;
            CheckUpdate_Allow.Checked = true;
            CheckUpdate_Allow.Enabled = false;
            CheckView_Allow.Checked = true;
            CheckView_Allow.Enabled = false;
            Checksendmail_Allow.Checked = true;
            CheckSendMail_Allow.Enabled = false;
            CheckGo_Allow.Checked = true;
            CheckGo_Allow.Enabled = false;
        }
        else if (rdlist.SelectedValue == "2")
        {

            Checksendmail_Allow.Checked = false;
            Checksendmail_Allow.Enabled = true;
            CheckEdit_Allow.Checked = false;
            CheckEdit_Allow.Enabled = true;
            CheckDelete_Allow.Checked = false;
            CheckDelete_Allow.Enabled = true;
            CheckDownload_Allow.Checked = false;
            CheckDownload_Allow.Enabled = true;
            CheckInsert_Allows.Checked = false;
            CheckInsert_Allows.Enabled = true;
            CheckUpdate_Allow.Checked = false;
            CheckUpdate_Allow.Enabled = true;
            CheckView_Allow.Checked = false;
            CheckView_Allow.Enabled = true;
            Checksendmail_Allow.Checked = false;
            CheckSendMail_Allow.Enabled = true;
            CheckGo_Allow.Checked = false;
            CheckGo_Allow.Enabled = true;
        }


    }
    protected void fillaccessgrid()
    {



        foreach (GridViewRow dsc in grdmain.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {

                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void fillaccessgrid1()
    {



        foreach (GridViewRow dsc in Grdsub.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {

                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void fillaccessgrid2()
    {



        foreach (GridViewRow dsc in grdpage.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {
                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {
                Checksendmail_Allow.Checked = true;
                Checksendmail_Allow.Enabled = false;
                CheckEdit_Allow.Checked = true;
                CheckEdit_Allow.Enabled = false;
                CheckDelete_Allow.Checked = true;
                CheckDelete_Allow.Enabled = false;
                CheckDownload_Allow.Checked = true;
                CheckDownload_Allow.Enabled = false;
                CheckInsert_Allows.Checked = true;
                CheckInsert_Allows.Enabled = false;
                CheckUpdate_Allow.Checked = true;
                CheckUpdate_Allow.Enabled = false;
                CheckView_Allow.Checked = true;
                CheckView_Allow.Enabled = false;
                Checksendmail_Allow.Checked = true;
                CheckSendMail_Allow.Enabled = false;
                CheckGo_Allow.Checked = true;
                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {

                Checksendmail_Allow.Checked = false;
                Checksendmail_Allow.Enabled = true;
                CheckEdit_Allow.Checked = false;
                CheckEdit_Allow.Enabled = true;
                CheckDelete_Allow.Checked = false;
                CheckDelete_Allow.Enabled = true;
                CheckDownload_Allow.Checked = false;
                CheckDownload_Allow.Enabled = true;
                CheckInsert_Allows.Checked = false;
                CheckInsert_Allows.Enabled = true;
                CheckUpdate_Allow.Checked = false;
                CheckUpdate_Allow.Enabled = true;
                CheckView_Allow.Checked = false;
                CheckView_Allow.Enabled = true;
                Checksendmail_Allow.Checked = false;
                CheckSendMail_Allow.Enabled = true;
                CheckGo_Allow.Checked = false;
                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void grdmain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblma = (Label)e.Row.FindControl("lblma");
            lblma.Text = (lblma.Text);
        }
    }
    protected void Grdsub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblptypename = (Label)e.Row.FindControl("lblptypename");
            Label lblsubmanuname = (Label)e.Row.FindControl("lblsubmanuname");

            lblptypename.Text = (lblptypename.Text);
            lblsubmanuname.Text = (lblsubmanuname.Text);

        }
    }
    protected void grdpage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblptypename = (Label)e.Row.FindControl("lblptypename");
            Label lblsubmanuname = (Label)e.Row.FindControl("lblsubmanuname");
            Label lblpagename = (Label)e.Row.FindControl("lblpagename");
            lblptypename.Text = (lblptypename.Text);
            lblsubmanuname.Text = (lblsubmanuname.Text);
            lblpagename.Text = (lblpagename.Text);
        }
    }

    protected void ddlmainfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubG();
    }
    protected void ddlmailpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        filtersubmenu();
        ddlsubpagefilter_SelectedIndexChanged(sender, e);
    }
    protected void ddlsubpagefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gtype == "2")
        {
            fillPageSub();
        }
        else
        {
            fillPageMain();
        }
    }
    protected void imgsubm_Click(object sender, ImageClickEventArgs e)
    {
        pnlmain.Visible = false;
        pnlpage.Visible = false;
        if (imgsubm.AlternateText == "Plush")
        {
            imgsubm.ImageUrl = "Images/minus.png";
            imgsubm.AlternateText = "Minus";
            pnlsubmanu.Visible = true;

            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";

            fillSubG();

            gtype = "2";
            fillaccessgrid1A();
        }
        else
        {
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            pnlsubmanu.Visible = false;
            // pnlmain.Visible = true;

            gtype = "1";
        }
        //imgpage.AlternateText = "Minus";
        //if (imgpage.AlternateText == "Plush")
        //{
        //    imgpage.ImageUrl = "Images/minus.png";
        //    imgpage.AlternateText = "Minus";


        //}
        //else
        //{
        //    imgpage.ImageUrl = "Images/plus.png";
        //    imgpage.AlternateText = "Plush";



        //}
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnladddata.Visible = true;
        }
        else
        {
            pnladddata.Visible = false;
        }

    }
    protected void imgpage_Click(object sender, ImageClickEventArgs e)
    {
        pnlmain.Visible = false;
        pnlsubmanu.Visible = false;
        if (imgpage.AlternateText == "Plush")
        {
            imgpage.ImageUrl = "Images/minus.png";
            imgpage.AlternateText = "Minus";

            pnlpage.Visible = true;

            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            if (gtype == "2")
            {
                fillPageSub();
            }
            else
            {
                fillPageMain();

            }
            fillaccessgrid2A();
        }
        else
        {
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";
            // pnlmain.Visible = true;
            pnlpage.Visible = false;


        }
        //imgsubm.AlternateText = "Minus";
        //if (imgsubm.AlternateText == "Plush")
        //{
        //    imgsubm.ImageUrl = "Images/minus.png";
        //    imgsubm.AlternateText = "Minus";

        //}
        //else
        //{
        //    imgsubm.ImageUrl = "Images/plus.png";
        //    imgsubm.AlternateText = "Plush";


        //}
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnladddata.Visible = true;
        }
        else
        {
            pnladddata.Visible = false;
        }

    }
    protected void fillSubG()
    {
        DataTable dtTemp = CreatedataSubManu();
        string strmanuid = "";
        string filmanu = "";
        if (ddlmainfilter.SelectedIndex > 0)
        {
            filmanu = " and SubMenuMaster.MainMenuId='" + ddlmainfilter.SelectedValue + "'";
        }
        if (gtype != "")
        {
            foreach (GridViewRow item in grdmain.Rows)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
                Label lblmainmanu = (Label)item.FindControl("lblmainmanu");
                int flg = 0;
                CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
                CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
                CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
                CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
                CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
                CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
                CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
                CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
                if (filmanu.Length != 0)
                {
                    if (lblmainmanu.Text.ToString() == ddlmainfilter.SelectedValue.ToString())
                    {
                        flg = 1;
                    }
                }
                else
                {
                    flg = 1;
                }
                if (flg == 1)
                {
                    if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                    {
                        DataTable dts = MyCommonfile.selectBZ("select Distinct SubMenuMaster.SubMenuName,  MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId   from MainMenuMaster inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  inner join PageMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' and SubMenuMaster.MainMenuId='" + lblmainmanu.Text + "'  order by MainMenuName");
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {
                                DataRow dtadd = dtTemp.NewRow();
                                dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                                dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                                dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                                dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);

                                dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                                dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                                dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                                dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                                dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                                dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                                dtadd["View_Right"] = CheckView_Allow.Checked;
                                dtadd["Go_Right"] = CheckGo_Allow.Checked;
                                dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;

                                dtTemp.Rows.Add(dtadd);
                            }

                        }
                    }
                    else
                    {
                        if (strmanuid.Length != 0)
                        {
                            strmanuid = strmanuid + ",";
                        }
                        strmanuid = strmanuid + "'" + lblmainmanu.Text + "'";
                    }
                }
            }
        }
        if (strmanuid.Length > 0 || gtype == "")
        {
            string RoleD = "";
            if (rdmode.SelectedValue == "3")
            {
                RoleD = " inner join DefaultRolewisePageAccess on DefaultRolewisePageAccess.PageId= pageplaneaccesstbl.PageId and  DefaultRolewisePageAccess.Priceplanid='" + ddlpriceplan.SelectedValue + "' and RoleId='" + defaRoleid + "'";
            }
            string parm = "";
            if (gtype != "")
            {
                parm = "  and SubMenuMaster.MainMenuId in(" + strmanuid + ")";
            }

            DataTable dts = MyCommonfile.selectBZ("select Distinct  SubMenuMaster.SubMenuName, MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId   from MainMenuMaster inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId  inner join PageMaster on PageMaster.SubMenuId=SubMenuMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid " + RoleD + " where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' " + parm + "  order by MainMenuName");
            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);

                    if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                    {

                        int accesno1 = 0;
                        int accesno2 = 0;
                        int Edit_Right = 0;
                        int Delete_Right = 0;
                        int Download_Right = 0;
                        int Insert_Right = 0;
                        int Update_Right = 0;
                        int View_Right = 0;
                        int Go_Right = 0;
                        int SendMail_Right = 0;
                        DataTable dtcln1 = MyCommonfile.selectBZ(" select  Count(Distinct pageplaneaccesstbl.PageId) as CD from   pageplaneaccesstbl  inner join PageMaster  on PageMaster.PageId=pageplaneaccesstbl.PageId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where PageMaster.SubMenuId='" + dts.Rows[i]["SubMenuId"] + "' and pageplaneaccesstbl.Priceplanid='" + ddlpriceplan.SelectedValue + "' ");
                        DataTable dtcln = new DataTable();
                        dtcln = MyCommonfile.selectBZ(" select  Distinct DefaultRolewisePageAccess.* from   DefaultRolewisePageAccess inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=DefaultRolewisePageAccess.PageId inner join PageMaster  on PageMaster.PageId=pageplaneaccesstbl.PageId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId where  PageMaster.SubMenuId='" + dts.Rows[i]["SubMenuId"] + "' and DefaultRolewisePageAccess.Priceplanid='" + ddlpriceplan.SelectedValue + "' and RoleId='" + defaRoleid + "'");
                        foreach (DataRow item in dtcln.Rows)
                        {
                            if (Convert.ToString(item["AccessRight"]) == "1")
                            {
                                accesno1 = accesno1 + 1;
                            }
                            else if (Convert.ToString(item["AccessRight"]) == "2")
                            {
                                accesno2 = accesno2 + 1;
                            }
                            if (Convert.ToString(item["Edit_Right"]) == "True")
                            {
                                Edit_Right = Edit_Right + 1;
                            }
                            if (Convert.ToString(item["Delete_Right"]) == "True")
                            {
                                Delete_Right = Delete_Right + 1;
                            }
                            if (Convert.ToString(item["Download_Right"]) == "True")
                            {
                                Download_Right = Download_Right + 1;
                            }
                            if (Convert.ToString(item["Insert_Right"]) == "True")
                            {
                                Insert_Right = Insert_Right + 1;
                            }
                            if (Convert.ToString(item["Update_Right"]) == "True")
                            {
                                Update_Right = Update_Right + 1;
                            }
                            if (Convert.ToString(item["View_Right"]) == "True")
                            {
                                View_Right = View_Right + 1;
                            }
                            if (Convert.ToString(item["Go_Right"]) == "True")
                            {
                                Go_Right = Go_Right + 1;
                            }
                            if (Convert.ToString(item["SendMail_Right"]) == "True")
                            {
                                SendMail_Right = SendMail_Right + 1;
                            }

                        }
                        if (accesno1 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                        {
                            dtadd["AccessRight"] = "1";
                            dtadd["Edit_Right"] = Convert.ToBoolean(1);
                            dtadd["Delete_Right"] = Convert.ToBoolean(1);
                            dtadd["Download_Right"] = Convert.ToBoolean(1);
                            dtadd["Insert_Right"] = Convert.ToBoolean(1);
                            dtadd["Update_Right"] = Convert.ToBoolean(1);
                            dtadd["View_Right"] = Convert.ToBoolean(1);
                            dtadd["Go_Right"] = Convert.ToBoolean(1);
                            dtadd["SendMail_Right"] = Convert.ToBoolean(1);

                        }
                        else
                        {
                            if (accesno2 == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                            {
                                dtadd["AccessRight"] = "2";
                                if (Edit_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                }
                                if (Delete_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                }
                                if (Download_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Download_Right"] = Convert.ToBoolean(0);
                                }
                                if (Insert_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                }
                                if (Update_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Update_Right"] = Convert.ToBoolean(0);
                                }
                                if (View_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["View_Right"] = Convert.ToBoolean(0);
                                }
                                if (Go_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["Go_Right"] = Convert.ToBoolean(0);
                                }
                                if (SendMail_Right == Convert.ToInt32(dtcln1.Rows[0]["CD"]))
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(1);
                                }
                                else
                                {
                                    dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                                }

                            }
                            else
                            {

                                dtadd["AccessRight"] = "0";
                                dtadd["Edit_Right"] = Convert.ToBoolean(0);
                                dtadd["Delete_Right"] = Convert.ToBoolean(0);
                                dtadd["Download_Right"] = Convert.ToBoolean(0);
                                dtadd["Insert_Right"] = Convert.ToBoolean(0);
                                dtadd["Update_Right"] = Convert.ToBoolean(0);
                                dtadd["View_Right"] = Convert.ToBoolean(0);
                                dtadd["Go_Right"] = Convert.ToBoolean(0);
                                dtadd["SendMail_Right"] = Convert.ToBoolean(0);


                            }
                        }
                    }

                    else
                    {

                        dtadd["AccessRight"] = "0";
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);


                    }
                    dtTemp.Rows.Add(dtadd);
                }


            }
        }
        Grdsub.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["Subg"] = dtTemp;
        Grdsub.DataBind();
    }
    protected void fillPageMain()
    {
        DataTable dtTemp = CreatedataPage();
        string strmanuid = "";
        string filmanu = "";

        if (ddlmailpagefilter.SelectedIndex > 0)
        {
            filmanu = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        }
        string subd = "";
        if (ddlsubpagefilter.SelectedIndex > 0)
        {

            subd = " and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";

        }
        subd = filmanu + subd;
        if (gtype != "")
        {
            foreach (GridViewRow item in grdmain.Rows)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
                Label lblmainmanu = (Label)item.FindControl("lblmainmanu");
                int flg = 0;
                CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
                CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
                CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
                CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
                CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
                CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
                CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
                CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
                if (filmanu.Length != 0)
                {
                    if (ddlsubpagefilter.SelectedIndex == 0)
                    {
                        if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                        {
                            flg = 1;
                        }
                    }
                    if (ddlsubpagefilter.SelectedIndex > 0)
                    {
                        if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                        {
                            subd = "and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";
                            flg = 1;
                        }
                    }

                }
                else
                {
                    flg = 1;
                }
                if (flg == 1)
                {
                    if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                    {
                        DataTable dts = MyCommonfile.selectBZ("select Distinct SubMenuMaster.SubMenuName, MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId, PageMaster.PageName,PageMaster.PageId ,PageMaster.PageTitle  from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' and PageMaster.MainMenuId='" + lblmainmanu.Text + "' " + subd + " order by PageMaster.PageName");
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {
                                DataRow dtadd = dtTemp.NewRow();
                                dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                                dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                                dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                                dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                                dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                                dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                                dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);

                                dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                                dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                                dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                                dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                                dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                                dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                                dtadd["View_Right"] = CheckView_Allow.Checked;
                                dtadd["Go_Right"] = CheckGo_Allow.Checked;
                                dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;

                                dtTemp.Rows.Add(dtadd);
                            }

                        }
                    }
                    else
                    {
                        if (strmanuid.Length != 0)
                        {
                            strmanuid = strmanuid + ",";
                        }
                        strmanuid = strmanuid + "'" + lblmainmanu.Text + "'";
                    }
                }
            }
        }

        if (strmanuid.Length > 0 || gtype == "")
        {
            string RoleD = " Left join ";
            if (rdmode.SelectedValue == "3")
            {
                RoleD = " inner join ";
            }
            string parm = "";
            if (gtype != "")
            {
                parm = "  and PageMaster.MainMenuId in(" + strmanuid + ")";
            }

            DataTable dtsa = MyCommonfile.selectBZ("select Distinct   SubMenuMaster.SubMenuName,  MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId, PageMaster.PageName,PageMaster.PageId ,PageMaster.PageTitle ,Case when( DefaultRolewisePageAccess.AccessRight IS NULL) then '0' else DefaultRolewisePageAccess.AccessRight end as AccessRight, " +
                    " Case when( DefaultRolewisePageAccess.Edit_Right IS NULL) then '0' else DefaultRolewisePageAccess.Edit_Right end as Edit_Right,Case when( DefaultRolewisePageAccess.Delete_Right IS NULL) then '0' else DefaultRolewisePageAccess.Delete_Right end as Delete_Right," +
                    "Case when( DefaultRolewisePageAccess.Download_Right IS NULL) then '0' else DefaultRolewisePageAccess.Download_Right end as Download_Right,Case when( DefaultRolewisePageAccess.Insert_Right IS NULL) then '0' else DefaultRolewisePageAccess.Insert_Right end as Insert_Right," +
                                   "Case when( DefaultRolewisePageAccess.Update_Right IS NULL) then '0' else  DefaultRolewisePageAccess.Update_Right end as Update_Right,Case when( DefaultRolewisePageAccess.View_Right IS NULL) then '0' else DefaultRolewisePageAccess.View_Right end as View_Right," +
                                       " Case when(DefaultRolewisePageAccess.Go_Right IS NULL) then '0' else dbo.DefaultRolewisePageAccess.Go_Right end as Go_Right, Case when(DefaultRolewisePageAccess.SendMail_Right IS NULL) then '0' else dbo.DefaultRolewisePageAccess.SendMail_Right end as SendMail_Right  from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId left join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid " + RoleD + " DefaultRolewisePageAccess on DefaultRolewisePageAccess.PageId=pageplaneaccesstbl.PageId  and DefaultRolewisePageAccess.PriceplanId='" + ddlpriceplan.SelectedValue + "' and DefaultRolewisePageAccess.RoleId='" + defaRoleid + "' where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "'  " + parm + subd + " order by PageMaster.PageName");


            if (dtsa.Rows.Count > 0)
            {
                for (int i = 0; i < dtsa.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dtsa.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dtsa.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dtsa.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dtsa.Rows[i]["SubMenuName"]);
                    dtadd["PageId"] = Convert.ToString(dtsa.Rows[i]["PageId"]);
                    dtadd["PageName"] = Convert.ToString(dtsa.Rows[i]["PageName"]);
                    dtadd["PageTitle"] = Convert.ToString(dtsa.Rows[i]["PageTitle"]);

                    if (rdmode.SelectedValue == "2" || rdmode.SelectedValue == "3")
                    {
                        dtadd["AccessRight"] = Convert.ToString(dtsa.Rows[i]["AccessRight"]);
                        dtadd["Edit_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Edit_Right"]);
                        dtadd["Delete_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Delete_Right"]);
                        dtadd["Download_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Download_Right"]);
                        dtadd["Insert_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Insert_Right"]);
                        dtadd["Update_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Update_Right"]);
                        dtadd["View_Right"] = Convert.ToBoolean(dtsa.Rows[i]["View_Right"]);
                        dtadd["Go_Right"] = Convert.ToBoolean(dtsa.Rows[i]["Go_Right"]);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(dtsa.Rows[i]["SendMail_Right"]);
                    }
                    else if (rdmode.SelectedValue == "1")
                    {
                        dtadd["AccessRight"] = Convert.ToString("0");
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }
                    dtTemp.Rows.Add(dtadd);
                }

            }
        }

        grdpage.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName,Pagename";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["dtp"] = dtTemp;

        grdpage.DataBind();
    }
    protected void fillPageSub()
    {
        DataTable dtTemp = CreatedataPage();
        string strmanuid = "";
        string filmanu = "";

        if (ddlmailpagefilter.SelectedIndex > 0)
        {
            filmanu = " and PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "'";
        }
        string subd = "";
        foreach (GridViewRow item in Grdsub.Rows)
        {
            RadioButtonList RadioButtonList1 = (RadioButtonList)item.FindControl("RadioButtonList1");
            Label lblsubmanuid = (Label)item.FindControl("lblsubmanuid");
            Label lblmainmanu = (Label)item.FindControl("lblpid");
            int flg = 0;
            CheckBox CheckEdit_Allow = (CheckBox)item.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)item.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)item.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)item.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)item.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)item.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)item.FindControl("CheckBoxGo1");
            CheckBox CheckSendMail_Allow = (CheckBox)item.FindControl("CheckBoxSendMail1");
            if (filmanu.Length != 0)
            {
                if (ddlsubpagefilter.SelectedIndex == 0)
                {
                    if (lblmainmanu.Text.ToString() == ddlmailpagefilter.SelectedValue.ToString())
                    {
                        flg = 1;
                    }
                }
                if (ddlsubpagefilter.SelectedIndex > 0)
                {
                    if (lblsubmanuid.Text.ToString() == ddlsubpagefilter.SelectedValue.ToString())
                    {
                        subd = "and SubMenuMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "'";
                        flg = 1;
                    }
                }

            }
            else
            {
                flg = 1;
            }
            if (flg == 1)
            {
                if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "2")
                {
                    DataTable dts = MyCommonfile.selectBZ("select Distinct SubMenuMaster.SubMenuName,  MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId, PageMaster.PageName,PageMaster.PageId ,PageMaster.PageTitle  from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' and PageMaster.SubMenuId='" + lblsubmanuid.Text + "'  order by PageMaster.PageName");
                    if (dts.Rows.Count > 0)
                    {
                        for (int i = 0; i < dts.Rows.Count; i++)
                        {
                            DataRow dtadd = dtTemp.NewRow();
                            dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                            dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                            dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                            dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                            dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                            dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                            dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);

                            dtadd["AccessRight"] = RadioButtonList1.SelectedValue;
                            dtadd["Edit_Right"] = CheckEdit_Allow.Checked;
                            dtadd["Delete_Right"] = CheckDelete_Allow.Checked;
                            dtadd["Download_Right"] = CheckDownload_Allow.Checked;
                            dtadd["Insert_Right"] = CheckInsert_Allows.Checked;
                            dtadd["Update_Right"] = CheckUpdate_Allow.Checked;
                            dtadd["View_Right"] = CheckView_Allow.Checked;
                            dtadd["Go_Right"] = CheckGo_Allow.Checked;
                            dtadd["SendMail_Right"] = CheckSendMail_Allow.Checked;

                            dtTemp.Rows.Add(dtadd);
                        }

                    }
                }
                else
                {
                    if (strmanuid.Length != 0)
                    {
                        strmanuid = strmanuid + ",";
                    }
                    strmanuid = strmanuid + "'" + lblsubmanuid.Text + "'";
                }
            }
        }
        if (strmanuid.Length > 0)
        {
            DataTable dts = MyCommonfile.selectBZ("select Distinct SubMenuMaster.SubMenuName, MainMenuMaster.MainMenuName+':'+ SubMenuMaster.SubMenuName as MainMenuName,SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId,PageMaster.PageName,PageMaster.PageId ,PageMaster.PageTitle ,Case when( DefaultRolewisePageAccess.AccessRight IS NULL) then '0' else DefaultRolewisePageAccess.AccessRight end as AccessRight, " +
                    " Case when( DefaultRolewisePageAccess.Edit_Right IS NULL) then '0' else DefaultRolewisePageAccess.Edit_Right end as Edit_Right,Case when( DefaultRolewisePageAccess.Delete_Right IS NULL) then '0' else DefaultRolewisePageAccess.Delete_Right end as Delete_Right," +
                    "Case when( DefaultRolewisePageAccess.Download_Right IS NULL) then '0' else DefaultRolewisePageAccess.Download_Right end as Download_Right,Case when( DefaultRolewisePageAccess.Insert_Right IS NULL) then '0' else DefaultRolewisePageAccess.Insert_Right end as Insert_Right," +
                                   "Case when( DefaultRolewisePageAccess.Update_Right IS NULL) then '0' else  DefaultRolewisePageAccess.Update_Right end as Update_Right,Case when( DefaultRolewisePageAccess.View_Right IS NULL) then '0' else DefaultRolewisePageAccess.View_Right end as View_Right," +
                                       " Case when(DefaultRolewisePageAccess.Go_Right IS NULL) then '0' else dbo.DefaultRolewisePageAccess.Go_Right end as Go_Right, Case when(DefaultRolewisePageAccess.SendMail_Right IS NULL) then '0' else dbo.DefaultRolewisePageAccess.SendMail_Right end as SendMail_Right  from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid Left join DefaultRolewisePageAccess on DefaultRolewisePageAccess.PageId=pageplaneaccesstbl.PageId  and DefaultRolewisePageAccess.PriceplanId='" + ddlpriceplan.SelectedValue + "' and DefaultRolewisePageAccess.RoleId='" + defaRoleid + "' where  PricePlanMaster.PricePlanId='" + ddlpriceplan.SelectedValue + "' and PageMaster.SubMenuId in(" + strmanuid + ")  order by PageMaster.PageName");


            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    DataRow dtadd = dtTemp.NewRow();
                    dtadd["MainMenuId"] = Convert.ToString(dts.Rows[i]["MainMenuId"]);
                    dtadd["MainMenuName"] = Convert.ToString(dts.Rows[i]["MainMenuName"]);
                    dtadd["SubMenuId"] = Convert.ToString(dts.Rows[i]["SubMenuId"]);
                    dtadd["SubMenuName"] = Convert.ToString(dts.Rows[i]["SubMenuName"]);
                    dtadd["PageId"] = Convert.ToString(dts.Rows[i]["PageId"]);
                    dtadd["PageName"] = Convert.ToString(dts.Rows[i]["PageName"]);
                    dtadd["PageTitle"] = Convert.ToString(dts.Rows[i]["PageTitle"]);
                    if (rdmode.SelectedValue == "2")
                    {
                        dtadd["AccessRight"] = Convert.ToString(dts.Rows[i]["AccessRight"]);
                        dtadd["Edit_Right"] = Convert.ToBoolean(dts.Rows[i]["Edit_Right"]);
                        dtadd["Delete_Right"] = Convert.ToBoolean(dts.Rows[i]["Delete_Right"]);
                        dtadd["Download_Right"] = Convert.ToBoolean(dts.Rows[i]["Download_Right"]);
                        dtadd["Insert_Right"] = Convert.ToBoolean(dts.Rows[i]["Insert_Right"]);
                        dtadd["Update_Right"] = Convert.ToBoolean(dts.Rows[i]["Update_Right"]);
                        dtadd["View_Right"] = Convert.ToBoolean(dts.Rows[i]["View_Right"]);
                        dtadd["Go_Right"] = Convert.ToBoolean(dts.Rows[i]["Go_Right"]);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(dts.Rows[i]["SendMail_Right"]);
                    }
                    else if (rdmode.SelectedValue == "1")
                    {
                        dtadd["AccessRight"] = Convert.ToString("0");
                        dtadd["Edit_Right"] = Convert.ToBoolean(0);
                        dtadd["Delete_Right"] = Convert.ToBoolean(0);
                        dtadd["Download_Right"] = Convert.ToBoolean(0);
                        dtadd["Insert_Right"] = Convert.ToBoolean(0);
                        dtadd["Update_Right"] = Convert.ToBoolean(0);
                        dtadd["View_Right"] = Convert.ToBoolean(0);
                        dtadd["Go_Right"] = Convert.ToBoolean(0);
                        dtadd["SendMail_Right"] = Convert.ToBoolean(0);
                    }

                    dtTemp.Rows.Add(dtadd);
                }

            }
        }
        grdpage.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
        hdnsortExp.Value = "MainMenuName,Pagename";
        hdnsortDir.Value = "Asc";
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        ViewState["dtp"] = dtTemp;
        grdpage.DataBind();
    }


    protected void fillaccessgridA()
    {



        foreach (GridViewRow dsc in grdmain.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {


                Checksendmail_Allow.Enabled = true;

                CheckEdit_Allow.Enabled = true;

                CheckDelete_Allow.Enabled = true;

                CheckDownload_Allow.Enabled = true;

                CheckInsert_Allows.Enabled = true;

                CheckUpdate_Allow.Enabled = true;

                CheckView_Allow.Enabled = true;

                CheckSendMail_Allow.Enabled = true;

                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void fillaccessgrid1A()
    {



        foreach (GridViewRow dsc in Grdsub.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {


                Checksendmail_Allow.Enabled = true;

                CheckEdit_Allow.Enabled = true;

                CheckDelete_Allow.Enabled = true;

                CheckDownload_Allow.Enabled = true;

                CheckInsert_Allows.Enabled = true;

                CheckUpdate_Allow.Enabled = true;

                CheckView_Allow.Enabled = true;

                CheckSendMail_Allow.Enabled = true;

                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void fillaccessgrid2A()
    {



        foreach (GridViewRow dsc in grdpage.Rows)
        {
            RadioButtonList rdlist;

            rdlist = (RadioButtonList)(dsc.FindControl("RadioButtonList1"));
            CheckBox Checksendmail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");
            CheckBox CheckEdit_Allow = (CheckBox)dsc.FindControl("CheckBoxEdit1");
            CheckBox CheckDelete_Allow = (CheckBox)dsc.FindControl("CheckBoxDelete1");
            CheckBox CheckDownload_Allow = (CheckBox)dsc.FindControl("CheckBoxDownload1");
            CheckBox CheckInsert_Allows = (CheckBox)dsc.FindControl("CheckBoxInsert1");
            CheckBox CheckUpdate_Allow = (CheckBox)dsc.FindControl("CheckBoxUpdate1");
            CheckBox CheckView_Allow = (CheckBox)dsc.FindControl("CheckBoxView1");
            CheckBox CheckGo_Allow = (CheckBox)dsc.FindControl("CheckBoxGo1");

            CheckBox CheckSendMail_Allow = (CheckBox)dsc.FindControl("CheckBoxSendMail1");

            if (rdlist.SelectedValue == "0")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "1")
            {

                Checksendmail_Allow.Enabled = false;

                CheckEdit_Allow.Enabled = false;

                CheckDelete_Allow.Enabled = false;

                CheckDownload_Allow.Enabled = false;

                CheckInsert_Allows.Enabled = false;

                CheckUpdate_Allow.Enabled = false;

                CheckView_Allow.Enabled = false;

                CheckSendMail_Allow.Enabled = false;

                CheckGo_Allow.Enabled = false;
            }
            else if (rdlist.SelectedValue == "2")
            {


                Checksendmail_Allow.Enabled = true;

                CheckEdit_Allow.Enabled = true;

                CheckDelete_Allow.Enabled = true;

                CheckDownload_Allow.Enabled = true;

                CheckInsert_Allows.Enabled = true;

                CheckUpdate_Allow.Enabled = true;

                CheckView_Allow.Enabled = true;

                CheckSendMail_Allow.Enabled = true;

                CheckGo_Allow.Enabled = true;
            }
        }

    }
    protected void imgmainmanu_Click(object sender, ImageClickEventArgs e)
    {
        pnlpage.Visible = false;
        pnlsubmanu.Visible = false;
        if (imgmainmanu.AlternateText == "Plush")
        {
            gtype = "1";
            imgmainmanu.ImageUrl = "Images/minus.png";
            imgmainmanu.AlternateText = "Minus";
            pnlmain.Visible = true;
            pnlpage.Visible = false;
            pnlsubmanu.Visible = false;
            imgsubm.ImageUrl = "Images/plus.png";
            imgsubm.AlternateText = "Plush";
            imgpage.ImageUrl = "Images/plus.png";
            imgpage.AlternateText = "Plush";
            //FillGrid();
            fillaccessgridA();
        }
        else
        {
            imgmainmanu.ImageUrl = "Images/plus.png";
            imgmainmanu.AlternateText = "Plush";
            pnlmain.Visible = false;
            //  pnlpage.Visible = false;

        }
        if (pnlpage.Visible == true || pnlmain.Visible == true || pnlsubmanu.Visible == true)
        {
            pnladddata.Visible = true;
        }
        else
        {
            pnladddata.Visible = false;
        }
    }
    protected void rdmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Enabled = true;
        if (rdmode.SelectedValue == "1")
        {
            pnlrole.Visible = true;
            pnlviewm.Visible = false;
            btnsub.Text = "Submit";
        }
        else if (rdmode.SelectedValue == "2")
        {
            pnlrole.Visible = false;
            pnlviewm.Visible = true;
            btnsub.Text = "Update";
        }
        else if (rdmode.SelectedValue == "3")
        {
            pnlrole.Visible = false;
            pnlviewm.Visible = true;
            pnladddata.Visible = false;
            Panel1.Enabled = false;
        }
        fillpl();
        //foreach (DataListItem item in Dataavail.Items)
        //{

        //    CheckBox chkde = (CheckBox)(Dataavail.Items[item.ItemIndex].FindControl("lbllist"));
        //    chkde.Checked = false;
        //}
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        foreach (DataListItem item in Dataavail.Items)
        {
            string roleid = Dataavail.DataKeys[item.ItemIndex].ToString();
            CheckBox chkde = (CheckBox)(Dataavail.Items[item.ItemIndex].FindControl("lbllist"));
            if (chkde.Checked == true)
            {
                string Temp1 = "";
                string Temp1val = "";
                if (pnlmain.Visible == true)
                {
                    foreach (GridViewRow grd in grdmain.Rows)
                    {
                        #region                       
                        Label lblmainmanu = (Label)grd.FindControl("lblmainmanu");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");                      
                        #endregion                       
                        #region            
                        string str = "select * from PricePlanMaster where PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "' and active='1'";
                        SqlDataAdapter da = new SqlDataAdapter(str, con);
                        DataTable dtpp = new DataTable();
                        da.Fill(dtpp);
                        for (int i = 0; i < dtpp.Rows.Count; i++)
                        {
                            DataTable dts = MyCommonfile.selectBZ("select Distinct MainMenuMaster.MainMenuId,PageMaster.PageId from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid where  PricePlanMaster.PricePlanId='" + dtpp.Rows[i]["PricePlanId"] + "' and PageMaster.MainMenuId='" + lblmainmanu.Text + "'");
                            for (int k1 = 0; k1 < dts.Rows.Count; k1++)
                            {
                                if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count > 0)
                                    {
                                    }
                                    else
                                    {
                                        string SelectQur = " INSERT INTO DefaultRolewisePageAccess(PageId,PriceplanId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + dts.Rows[k1]["PageId"] + "','" + dtpp.Rows[i]["PricePlanId"] + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                        SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                        con.Open();
                                        cd4.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                if (rd1.SelectedValue == "0")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count > 0)
                                    {
                                        string delete = "delete from DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ";
                                        SqlCommand ccmm = new SqlCommand(delete, con);
                                        con.Open();
                                        ccmm.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }                           
                            #endregion 
                    }                    
                }
                else if (pnlsubmanu.Visible == true)
                {                    
                    foreach (GridViewRow grd in Grdsub.Rows)
                    {
                        #region
                        Label lblsubmanuid = (Label)grd.FindControl("lblsubmanuid");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        #endregion
                        #region  
                        string str = "select * from PricePlanMaster where PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "' and active='1'";
                        SqlDataAdapter da = new SqlDataAdapter(str, con);
                        DataTable dtpp = new DataTable();
                        da.Fill(dtpp);
                        for (int i = 0; i < dtpp.Rows.Count; i++)
                        {
                            DataTable dts = MyCommonfile.selectBZ("select Distinct SubMenuMaster.SubMenuId,MainMenuMaster.MainMenuId,PageMaster.PageId from MainMenuMaster   inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId=PageMaster.PageId inner join PricePlanMaster on PricePlanMaster.PricePlanId=pageplaneaccesstbl.Priceplanid where  PricePlanMaster.PricePlanId='" + dtpp.Rows[i]["PricePlanId"] + "' and PageMaster.SubMenuId='" + lblsubmanuid.Text + "'");
                            for (int k1 = 0; k1 < dts.Rows.Count; k1++)
                            {
                                if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count > 0)
                                    {
                                    }
                                    else
                                    {
                                        string SelectQur = " INSERT INTO DefaultRolewisePageAccess(PageId,PriceplanId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + dts.Rows[k1]["PageId"] + "','" + dtpp.Rows[i]["PricePlanId"] + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                        SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                        con.Open();
                                        cd4.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                if (rd1.SelectedValue == "0")
                                {
                                    DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                    if (Dts.Rows.Count > 0)
                                    {
                                        string delete = "delete from DefaultRolewisePageAccess where PageId='" + dts.Rows[k1]["PageId"] + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ";
                                        SqlCommand ccmm = new SqlCommand(delete, con);
                                        con.Open();
                                        ccmm.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                        #endregion                        
                    }
                    //string strfildel = "";
                    //if (ddlmainfilter.SelectedIndex > 0)
                    //{
                    //    strfildel = " and PageId  in(select Distinct PageMaster.PageId from  PageMaster  " +
                    //             "  where PageMaster.MainMenuId='" + ddlmainfilter.SelectedValue + "' )";
                    //}                   
                }
                else if (pnlpage.Visible == true)
                {
                    //string pidDe = "";
                    foreach (GridViewRow grd in grdpage.Rows)
                    {
                        Label lblpageid = (Label)grd.FindControl("lblpageid");
                        RadioButtonList rd1 = (RadioButtonList)grd.FindControl("RadioButtonList1");
                        CheckBox Checksendmail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");
                        CheckBox CheckEdit_Allow = (CheckBox)grd.FindControl("CheckBoxEdit1");
                        CheckBox CheckDelete_Allow = (CheckBox)grd.FindControl("CheckBoxDelete1");
                        CheckBox CheckDownload_Allow = (CheckBox)grd.FindControl("CheckBoxDownload1");
                        CheckBox CheckInsert_Allows = (CheckBox)grd.FindControl("CheckBoxInsert1");
                        CheckBox CheckUpdate_Allow = (CheckBox)grd.FindControl("CheckBoxUpdate1");
                        CheckBox CheckView_Allow = (CheckBox)grd.FindControl("CheckBoxView1");
                        CheckBox CheckGo_Allow = (CheckBox)grd.FindControl("CheckBoxGo1");
                        CheckBox CheckSendMail_Allow = (CheckBox)grd.FindControl("CheckBoxSendMail1");

                        #region  
                         string str = "select * from PricePlanMaster where PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "' and active='1'";
                        SqlDataAdapter da = new SqlDataAdapter(str, con);
                        DataTable dtpp = new DataTable();
                        da.Fill(dtpp);
                        for (int i = 0; i < dtpp.Rows.Count; i++)
                        {
                            if (rd1.SelectedValue == "1" || rd1.SelectedValue == "2")
                            {
                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + lblpageid.Text + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                if (Dts.Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    string SelectQur = " INSERT INTO DefaultRolewisePageAccess(PageId,PriceplanId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) Values ('" + lblpageid.Text + "','" + dtpp.Rows[i]["PricePlanId"] + "','" + roleid + "','" + rd1.SelectedValue + "','" + CheckEdit_Allow.Checked + "','" + CheckDelete_Allow.Checked + "','" + CheckDownload_Allow.Checked + "','" + CheckInsert_Allows.Checked + "','" + CheckUpdate_Allow.Checked + "','" + CheckView_Allow.Checked + "','" + CheckGo_Allow.Checked + "','" + CheckSendMail_Allow.Checked + "')";
                                    SqlCommand cd4 = new SqlCommand(SelectQur, con);
                                    con.Open();
                                    cd4.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            if (rd1.SelectedValue == "0")
                            {
                                DataTable Dts = MyCommonfile.selectBZ(" Select * From DefaultRolewisePageAccess where PageId='" + lblpageid.Text + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ");
                                if (Dts.Rows.Count > 0)
                                {
                                    string delete = "delete from DefaultRolewisePageAccess where PageId='" + lblpageid.Text + "' and PriceplanId='" + dtpp.Rows[i]["PricePlanId"] + "' and RoleId='" + roleid + "' ";
                                    SqlCommand ccmm = new SqlCommand(delete, con);
                                    con.Open();
                                    ccmm.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        #endregion                        
                    }
                    //string strfildel = "";
                    //if (ddlmailpagefilter.SelectedIndex > 0)
                    //{
                    //    strfildel = " and PageId  in(select Distinct PageMaster.PageId from  PageMaster  " +
                    //             "  where PageMaster.MainMenuId='" + ddlmailpagefilter.SelectedValue + "' )";
                    //}
                    //if (ddlsubpagefilter.SelectedIndex > 0)
                    //{
                    //    strfildel = " and PageId  in(select Distinct PageMaster.PageId from  PageMaster  " +
                    //             "  where PageMaster.SubMenuId='" + ddlsubpagefilter.SelectedValue + "' )";
                    //}                    
                }
            }
        }
        Label1.Text = "Record inserted successfully";
        Label1.Visible = true;
        rdmode_SelectedIndexChanged(sender, e);
    }
   
    protected void grdpage_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["dtp"];
        grdpage.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;
     
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
      

        grdpage.DataBind();
    }
    protected void Grdsub_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Subg"];
        Grdsub.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        Grdsub.DataBind();
    }
    protected void grdmain_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Maing"];
        grdmain.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        grdmain.DataBind();
    }

    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
     //   ModernpopSync.Show();
    }
    
 
   
}

