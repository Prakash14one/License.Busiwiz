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
public partial class Portaldesignationpage : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static int noofloop = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblmsg.Text = "";
            ViewState["sortOrder"] = "";
            FillProduct();
            ddlproduct_SelectedIndexChanged(sender, e);
        }
    }
    protected void FillProduct()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");
        ddlproduct.DataSource = dtcln;
        ddlproduct.DataValueField = "ProductId";
        ddlproduct.DataTextField = "productversion";
        ddlproduct.DataBind();
    }
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldesin();
        fillportal();
        FillWebsiteMaster();
        ddlportal_SelectedIndexChanged(sender, e);
    }
    protected void fillportal()
    {
        DataTable dtcln = selectBZ(" select Distinct * from PortalMasterTbl where ProductId='" + ddlproduct.SelectedValue + "' order by PortalName ");
        ddlportal.DataSource = dtcln;
        ddlportal.DataValueField = "Id";
        ddlportal.DataTextField = "PortalName";
        ddlportal.DataBind();
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        ViewState["data"] = null;
        GRDfa.DataSource = null;
        GRDfa.DataBind();
        DataTable dtc = selectBZ("Select * from PortaldesignationTbl where PortalId='" + ddlportal.SelectedValue + "'");
        if (dtc.Rows.Count > 0)
        {
            DataTable dtv = selectBZ(" select Distinct  PageTitle+'<=>'+PageName as PageName, PageName as PageNameval from PageMaster inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PageMaster.VersionInfoMasterId where   VersionInfoMaster.ProductId='" + ddlproduct.SelectedValue + "' ");          
            string Strsel = "Select Distinct PortaldesignationTbl.Id as PortalId,Portalname as PortalName, DesignationName,PortaldesignationTbl.DefaultPageName as DPN, PortaldesignationDetailTbl.PageName as PageNameSave,PageTitle+'<=>'+PortaldesignationDetailTbl.PageName as PageName from " +
                " VersionInfoMaster inner join PageMaster on PageMaster.VersionInfoMasterId= VersionInfoMaster.VersionInfoId  inner join PortaldesignationDetailTbl on PortaldesignationDetailTbl.PageName=PageMaster.PageName inner join PortaldesignationTbl on PortaldesignationTbl.Id=PortaldesignationDetailTbl.PortaldesignationTblId where  VersionInfoMaster.ProductId='" + ddlproduct.SelectedValue + "' and PortalId='" + ddlportal.SelectedValue + "'";
            DataTable dvtb = selectBZ(Strsel);
            if (dvtb.Rows.Count > 0)
            {
                GRDfa.DataSource = dvtb;
                GRDfa.DataBind();
                ViewState["data"] = dvtb;
            }
            btnsave.Visible = false;
            btnedit.Visible = true;
            ddlpage.Enabled = false;
            PNLAA.Enabled = false;
        }
        else
        {
            btnsave.Visible = true;
            btnedit.Visible = false;
            ddlpage.Enabled = true;
            PNLAA.Enabled = true;
        }
    }
    protected void FillWebsiteMaster()
    {
        string strcln = " SELECT DISTINCT dbo.WebsiteMaster.ID, dbo.WebsiteMaster.WebsiteName, dbo.WebsiteMaster.WebsiteUrl FROM dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId Where VersionInfoMaster.ProductId='" + ddlproduct.SelectedValue + "' and WebsiteMaster.Status=1 ";//
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DDLWebsiteC.DataSource = dtcln;
        DDLWebsiteC.DataValueField = "ID";
        DDLWebsiteC.DataTextField = "WebsiteName";
        DDLWebsiteC.DataBind();
        DDLWebsiteC.Items.Insert(0, "--Select--");
        DDLWebsiteC.Items[0].Value = "0";
        DDLWebsiteC.SelectedIndex = 0;
    }
    protected void DDLWebsiteC_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtv =MyCommonfile.selectBZ(" SELECT DISTINCT dbo.PageMaster.PageName + '<=>' + dbo.PageMaster.PageTitle AS PageName, dbo.PageMaster.PageName AS PageNameval FROM            dbo.PageMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.PageMaster.VersionInfoMasterId INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.pageplaneaccesstbl ON dbo.PageMaster.PageId = dbo.pageplaneaccesstbl.Pageid where  WebsiteSection.WebsiteMasterId='" + DDLWebsiteC.SelectedValue + "' ");
        ddlpage.DataSource = dtv;
        ddlpage.DataValueField = "PageNameval";
        ddlpage.DataTextField = "PageName";
        ddlpage.DataBind();

        ddlafterloginpage.DataSource = dtv;
        ddlafterloginpage.DataValueField = "PageNameval";
        ddlafterloginpage.DataTextField = "PageName";
        ddlafterloginpage.DataBind();
    }
    protected void FilterMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
  

   
    protected void filldesin()
    {
        string seld = "select Distinct  DefaultDesignationTbl.DesignationName from DefaultDesignationTbl inner join DefaultRole on DefaultRole.RoleId=DefaultDesignationTbl.RoleId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=DefaultRole.VersionId where  VersionInfoMaster.ProductId='" + ddlproduct.SelectedValue + "'  order by DefaultDesignationTbl.DesignationName";
        DataTable dtcv = selectBZ(seld);

        ddldes.DataSource = dtcv;
        ddldes.DataValueField = "DesignationName";
        ddldes.DataTextField = "DesignationName";
        ddldes.DataBind();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddlportal.SelectedIndex >= 0)
        {
            string Strsel = "Select Distinct PortaldesignationTblId  from PortaldesignationDetailTbl inner join PortaldesignationTbl on PortaldesignationTbl.Id=PortaldesignationDetailTbl.PortaldesignationTblId where PortalId='" + ddlportal.SelectedValue + "'";
            string stc = "Delete from PortaldesignationDetailTbl where PortaldesignationTblId in(" + Strsel + ")";
            SqlCommand cmdC = new SqlCommand(stc, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdC.ExecuteNonQuery();
            con.Close();
            //string stcv = "Delete from PortaldesignationTbl where PortalId='" + ddlportal.SelectedValue + "'";
            //SqlCommand cmd = new SqlCommand(stcv, con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmd.ExecuteNonQuery();
            //con.Close();
            DataTable dtcX = selectBZ("Select Id from PortaldesignationTbl where PortalId='" + ddlportal.SelectedValue + "'");
            if (dtcX.Rows.Count == 0)
            {
                string strin = "insert into PortaldesignationTbl(ProductId,PortalId,Portalname,DefaultPageName)Values " +
                    "('" + ddlproduct.SelectedValue + "','" + ddlportal.SelectedValue + "','" + ddlportal.SelectedItem.Text + "','" + ddlpage.SelectedValue + "')";

                SqlCommand cmddata = new SqlCommand(strin, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddata.ExecuteNonQuery();
                con.Close();
            }
            string Temp1 = "";
            string Temp1val = "";
            DataTable dtc = selectBZ("Select  Id from PortaldesignationTbl where PortalId='" + ddlportal.SelectedValue + "'");
            if (dtc.Rows.Count > 0)
            {
                foreach (GridViewRow item in GRDfa.Rows)
                {
                    Temp1 = "INSERT INTO PortaldesignationDetailTbl(PortaldesignationTblId,DesignationName,PageName)Values";

                    Label lblpnSave = (Label)item.FindControl("lblpnSave");
                    Label lblcb = (Label)item.FindControl("lblcb");
                    string StId = GRDfa.DataKeys[item.RowIndex].Value.ToString();



                    if (Temp1val.Length > 0)
                    {
                        Temp1val += ",";
                    }
                    Temp1val += "('" + dtc.Rows[0]["Id"] + "','" + lblcb.Text + "','" + lblpnSave.Text + "')";




                }
                if (Temp1val.Length > 0)
                {
                    Temp1 += Temp1val;
                    SqlCommand cd4 = new SqlCommand(Temp1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cd4.ExecuteNonQuery();
                    con.Close();
                }
            }
            ddlportal_SelectedIndexChanged(sender, e);
            lblmsg.Text = "Record saved successfully";
        }
    }

    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "PortalId";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "DesignationName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "PageName";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "DPN";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "PageNameSave";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcomp = new DataColumn();
        Dcomp.DataType = System.Type.GetType("System.String");
        Dcomp.ColumnName = "PortalName";
        Dcomp.AllowDBNull = true;
        Dcomp.Unique = false;
        Dcomp.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcomp);

        return dt;
    }
    protected void btntempadd_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (ddldes.SelectedIndex >= 0)
        {     int flag = 0;
                    foreach (GridViewRow item in GRDfa.Rows)
                    {
                        Label lblpnSave =(Label)item.FindControl("lblpnSave");
                        Label lblcb = (Label)item.FindControl("lblcb");
                        if (lblcb.Text.ToString() == ddldes.SelectedValue.ToString())
                        {
                           
                            lblmsg.Text = "You cannot use more than one page for the same designation.";
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        DataTable dt = new DataTable();
                        if (ViewState["data"] == null)
                        {
                            dt = CreateDatatable();
                        }
                        else
                        {
                            dt = (DataTable)ViewState["data"];

                        }
                        DataRow Drow = dt.NewRow();
                        Drow["PortalId"] = ddlportal.SelectedValue;
                        Drow["DesignationName"] = ddldes.SelectedValue;
                        Drow["DPN"] = ddlpage.SelectedValue;
                        Drow["PageName"] = ddlafterloginpage.SelectedItem.Text;
                        Drow["PortalName"] = ddlportal.SelectedItem.Text;

                        Drow["PageNameSave"] = ddlafterloginpage.SelectedValue;
                        dt.Rows.Add(Drow);
                        ViewState["data"] = dt;
                        GRDfa.DataSource = dt;
                        GRDfa.DataBind();
                    }
        }
    }
   
    protected void btnedit_Click(object sender, EventArgs e)
    {
        btnsave.Visible = true;
        btnedit.Visible = false;
        ddlpage.Enabled = true;
        PNLAA.Enabled = true;
    }
    protected void GRDfa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GRDfa.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(GRDfa.SelectedIndex.ToString()));

        }
    }
    protected void DeleteFromGrid(int rowindex)
    {
       
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        GRDfa.DataSource = dt;
        GRDfa.DataBind();
        ViewState["data"] = dt;
      
       // lblmsg.Text = "Record deleted successfully.";

    }
    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
       
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;


        DataTable dt1 = selectBZ("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id,ClientProductTableMaster.VersionInfoId FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=SatelliteSyncronisationrequiringTablesMaster.ProductVersionID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ( ClientProductTableMaster.TableName='PortaldesignationTbl' OR  ClientProductTableMaster.TableName='PortaldesignationDetailTbl' )");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string datetim = DateTime.Now.ToString();
                string arqid = dt1.Rows[i]["Id"].ToString();

                string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmn = new SqlCommand(str22, con);
                cmn.ExecuteNonQuery();
                con.Close();

                DataTable dt121 = selectBZ("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = selectBZ("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        transf = Convert.ToInt32(dt1.Rows[i]["VersionInfoId"]);
                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                    }
                }


            }

        }


        if (transf > 0)
        {
            string te = "SyncData.aspx?verId=" + transf;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
}
