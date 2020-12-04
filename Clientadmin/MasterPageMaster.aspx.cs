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

public partial class SubMenuMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            FillProduct();
            Fillgrid();
            ViewState["sortOrder"] = "";
        }

    }

    protected void FillProduct()
    {
        // string strcln = "SELECT ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner  join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId  where ClientMasterId=" + Session["ClientId"].ToString() + " order  by ProductName,VersionInfoId";

        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'SECTION' + ' : ' +  WebsiteSection.SectionName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlWebsiteSection.DataSource = dtcln;

        ddlWebsiteSection.DataValueField = "WebsiteSectionId";
        ddlWebsiteSection.DataTextField = "productversion";
        ddlWebsiteSection.DataBind();
        ddlWebsiteSection.Items.Insert(0, "-Select-");
        ddlWebsiteSection.Items[0].Value = "0";

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string str1 = "select * from MasterPageMaster where MasterPageName='" + txtMasterPageName.Text + "'  and  WebsiteSectionId='" + ddlWebsiteSection.SelectedValue + "'";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }

        else
        {

            string MasterInsert = "Insert Into MasterPageMaster (MasterPageName,MasterPageDescription,WebsiteSectionId) values ('" + txtMasterPageName.Text + "','" +txtDes.Text + "','"+ddlWebsiteSection.SelectedValue+"')";
            SqlCommand cmd = new SqlCommand(MasterInsert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            clearall();
            Fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            ModernpopSync.Show();
        }

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";
    }
    protected void Fillgrid()
    {
        string str = " SELECT MasterPageMaster.*,VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId as WebsiteSectionId2 ,  " +
 " 'PRODUCT' + ': ' + ProductMaster.ProductName + ':  ' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' :   ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':  ' + 'SECTION' + ' : ' +  WebsiteSection.SectionName  as productversion "+
 "  from MasterPageMaster inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on  VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where VersionInfoMaster.Active='1' and ProductMaster.ClientMasterId='"+Session["ClientId"].ToString()+"'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
 
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

    protected void clearall()
    {
        txtMasterPageName.Text = "";
        txtDes.Text = "";
        ddlWebsiteSection.SelectedIndex = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();
        lblmsg.Visible = false;
        lblmsg.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";
        Button1.Visible = true;
        Button3.Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from MasterPageMaster where MasterPageId='" + ViewState["Did"] + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        Fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted succesfully";

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        


        //Fillgrid();
       
        ////DropDownList ddlmainmenu123 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlmainmenu123");
        ////Label lblddlmainmenuId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblddlmainmenuId");
        //TextBox txtMasterPageName = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMasterPageName");

        //TextBox txtdesEdit = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtdesEdit");

        //DropDownList ddlWebsiteSectionname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlWebsiteSectionname");
        //Label lblWebsiteSectionId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblWebsiteSectionId");


        //string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, 'PRODUCT' + ' : ' + ProductMaster.ProductName + ':' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' : ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ':' + 'VERSION' + ' : ' +  WebsiteSection.SectionName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' order  by productversion";
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        //ddlWebsiteSectionname.DataSource = dtcln;

        //ddlWebsiteSectionname.DataValueField = "WebsiteSectionId";
        //ddlWebsiteSectionname.DataTextField = "productversion";
        //ddlWebsiteSectionname.DataBind();
        //ddlWebsiteSectionname.Items.Insert(0, "-Select-");


        //ddlWebsiteSectionname.SelectedIndex = ddlWebsiteSectionname.Items.IndexOf(ddlWebsiteSectionname.Items.FindByValue(lblWebsiteSectionId.Text));

        ////GridView1.EditIndex = e.NewEditIndex;
        
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //TextBox txtMasterPageName = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtMasterPageName");

        //TextBox txtdesEdit = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtdesEdit");

        //DropDownList ddlWebsiteSectionname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlWebsiteSectionname");
        //Label lblWebsiteSectionId = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblWebsiteSectionId");

        //string str1 = "select * from MasterPageMaster where MasterPageName='" + txtMasterPageName.Text + "'  and MasterPageId<>'" + dk + "'  ";

        //SqlCommand cmd1 = new SqlCommand(str1, con);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt1.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record already exists";
        //}

        //else
        //{


        //    string sr51 = ("update MasterPageMaster set MasterPageName='" + txtMasterPageName.Text + "',MasterPageDescription='" + txtdesEdit.Text + "',WebsiteSectionId='" + ddlWebsiteSectionname.SelectedValue + "'  where MasterPageId='" + dk + "' ");
        //    SqlCommand cmd801 = new SqlCommand(sr51, con);

        //    con.Open();
        //    cmd801.ExecuteNonQuery();
        //    con.Close();
        //    GridView1.EditIndex = -1;
        //    Fillgrid();
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record update successfully";

        //}

        
    }

 

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ViewState["Did"] = Convert.ToInt32(e.CommandArgument);
        }

        if (e.CommandName == "Edit")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lblmsg.Text = "";
            lbllegend.Text = "Edit Master Page";

            Button1.Visible = false;
            Button3.Visible = true;

            int mm=Convert.ToInt32(e.CommandArgument);
            ViewState["update"]=mm;

            string hh = " SELECT MasterPageMaster.*,VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId as WebsiteSectionId2 ,  " +
                         " 'PRODUCT' + ' : ' + ProductMaster.ProductName + ' : ' + 'VERSION' + ' : ' +  VersionInfoMaster.VersionInfoName + ' :   ' +'WEBSITE' + ' : ' + WebsiteMaster.WebsiteName + ' :  ' + 'SECTION' + ' : ' +  WebsiteSection.SectionName  as productversion " +
                         "  from MasterPageMaster inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster on  VersionInfoMaster.VersionInfoId=WebsiteMaster.VersionInfoId inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where VersionInfoMaster.Active='1' and ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='"+mm+"'";
            SqlCommand cmd12 = new SqlCommand(hh, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable dtt = new DataTable();
            adp12.Fill(dtt);

            txtMasterPageName.Text = dtt.Rows[0]["MasterPageName"].ToString();
            txtDes.Text = dtt.Rows[0]["MasterPageDescription"].ToString();


            FillProduct();
        //    ddlWebsiteSection.SelectedItem.Text = dtt.Rows[0]["productversion"].ToString();
           // ddlWebsiteSection.SelectedValue = dtt.Rows[0]["productversion"].ToString();
         //   ddlWebsiteSection.SelectedItem.Text = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByText("productversion"));

            ddlWebsiteSection.SelectedIndex = ddlWebsiteSection.Items.IndexOf(ddlWebsiteSection.Items.FindByValue(dtt.Rows[0]["WebsiteSectionId2"].ToString()));


        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {



        if (btnprint.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btnprint.Text = "Hide Printable Version";
            btnin.Visible = true;
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            btnprint.Text = "Printable Version";
            btnin.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }




        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string str1 = "select * from MasterPageMaster where MasterPageName='" + txtMasterPageName.Text + "' and WebsiteSectionId='" + ddlWebsiteSection.SelectedValue + "'   and MasterPageId<>'" + ViewState["update"] + "'  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            string sr51 = ("update MasterPageMaster set MasterPageName='" + txtMasterPageName.Text + "',MasterPageDescription='" + txtDes.Text + "',WebsiteSectionId='" + ddlWebsiteSection.SelectedValue + "'  where MasterPageId='" + ViewState["update"] + "' ");
            SqlCommand cmd801 = new SqlCommand(sr51, con);

            con.Open();
            cmd801.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            Fillgrid();
            clearall();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            ModernpopSync.Show();

        }
        Button3.Visible = false;
        Button1.Visible = true;
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllegend.Text = "";

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New Master Page";
    }



    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='MasterPageMaster' ");
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

                DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                        transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }


            }

        }


        else
        {
           
        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
}
