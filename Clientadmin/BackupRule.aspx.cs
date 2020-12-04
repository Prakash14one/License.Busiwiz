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
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
public partial class BackupRule : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["GridFileAttach1"] = null;
            FillProduct();
            fillportal();
            fillcodedefaultcategory();
            DropDownList2_SelectedIndexChanged(sender, e);
            fillgrid();
        }

    }



    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (ViewState["data"] == null)
        {

            dt = CreateDatatable();

            DataRow Drow = dt.NewRow();
            Drow["TimeDisplay"] = DropDownList3.SelectedItem.Text;
            Drow["Time"] = DropDownList3.SelectedValue;
            dt.Rows.Add(Drow);

            ViewState["data"] = dt;
            GridView2.DataSource = dt;
            GridView2.DataBind();

            Label3.Visible = false;


        }
        else
        {
            dt = (DataTable)ViewState["data"];

            int flag = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string Time = dr["Time"].ToString();

                if (Time == DropDownList3.SelectedValue)
                {
                    Label3.Visible = true;
                    Label3.Text = "Record already exist";

                    flag = 1;
                    break;
                }
            }

            if (flag == 0)
            {

                DataRow Drow = dt.NewRow();
                Drow["TimeDisplay"] = DropDownList3.SelectedItem.Text;
                Drow["Time"] = DropDownList3.SelectedValue;
                dt.Rows.Add(Drow);

                ViewState["data"] = dt;

                GridView2.DataSource = dt;
                GridView2.DataBind();
                Label3.Visible = false;


            }

        }

    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "TimeDisplay";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Time";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        return dt;
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DeleteFromGrid(Convert.ToInt32(GridView2.SelectedIndex.ToString()));

        }
    }
    protected void DeleteFromGrid(int rowindex)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["data"];
        dt.Rows[rowindex].Delete();
        dt.AcceptChanges();
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ViewState["data"] = dt;

        Label3.Visible = true;
        Label3.Text = "Record deleted successfully.";



    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "1")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "2")
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "3")
        {
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "4")
        {
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = true;
        }

    }
    protected void ddlProductname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillportal();
        fillcodedefaultcategory();
        FillMainFOlderdown();
    }
    protected void FillProduct()
    {

        string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlProductname1.DataSource = dtcln;
        ddlProductname1.DataValueField = "VersionInfoId";
        ddlProductname1.DataTextField = "productversion";
        ddlProductname1.DataBind();

    }
    protected void fillportal()
    {
        ddlportal.Items.Clear();
        string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname1.SelectedValue + "' ) order by PortalName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);

        ddlportal.DataSource = dtcln22v;
        ddlportal.DataValueField = "Id";
        ddlportal.DataTextField = "PortalName";
        ddlportal.DataBind();
        ddlportal.Items.Insert(0, "Select");
        ddlportal.Items[0].Value = "0";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        string strcln = "select * from PortalBackupRuleMasterTbl where PortalId='" + ddlportal.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }

        else
        {

            string str = "";

            if (DropDownList2.SelectedValue == "1")
            {
                str = "Insert into PortalBackupRuleMasterTbl (PortalId,NoofCoppies,FrequecyTypeForBackup,NotificationEmail) values ('" + ddlportal.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + CheckBox1.Checked + "') ";

            }
            if (DropDownList2.SelectedValue == "2")
            {
                str = "Insert into PortalBackupRuleMasterTbl (PortalId,NoofCoppies,FrequecyTypeForBackup,NotificationEmail,EveryDayTime) values ('" + ddlportal.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + CheckBox1.Checked + "','" + DropDownList4.SelectedValue + "') ";
            }
            if (DropDownList2.SelectedValue == "3")
            {
                str = "Insert into PortalBackupRuleMasterTbl (PortalId,NoofCoppies,FrequecyTypeForBackup,NotificationEmail,WeeklyDay,WeeklyTime) values ('" + ddlportal.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + CheckBox1.Checked + "','" + DropDownList6.SelectedValue + "','" + DropDownList5.SelectedValue + "') ";
            }
            if (DropDownList2.SelectedValue == "4")
            {
                str = "Insert into PortalBackupRuleMasterTbl (PortalId,NoofCoppies,FrequecyTypeForBackup,NotificationEmail,MonthlyDay,MonthlyTime) values ('" + ddlportal.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + CheckBox1.Checked + "','" + DropDownList7.SelectedValue + "','" + DropDownList8.SelectedValue + "') ";
            }

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(str, con);

            cmd.ExecuteNonQuery();
            con.Close();
            //-----------------------------------------------------------------------------------------------------------------
            //--------------------------------------------------------------

            string strcln22v = "select Max(Id) as ID from PortalBackupRuleMasterTbl where PortalId='" + ddlportal.SelectedValue + "'   ";
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            //Backu Folder Name                  
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label lblFolderID = (Label)gdr.FindControl("lblFolderID");
                Label lblSubfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label lblSubsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                Label lblFolderCatName = (Label)gdr.FindControl("lblFolderCatName");
                Label lblFolderSubName = (Label)gdr.FindControl("lblFolderSubName");
                Label lblFolderSubSubName = (Label)gdr.FindControl("lblFolderSubSubName");
                Label lbllastfolder = (Label)gdr.FindControl("lbllastfolder");

                Label lblCodeTypeName = (Label)gdr.FindControl("lblCodeTypeName");
                Label lblProductCodeDetailid = (Label)gdr.FindControl("lblProductCodeDetailid");

                string path = "";
                path = lblFolderCatName.Text;
                if (lblFolderSubName.Text == "0")
                {
                    path += "\\" + lblFolderSubName.Text;
                    if (lblFolderSubSubName.Text == "0")
                    {
                        path += "\\" + lblFolderSubSubName.Text;
                    }
                }

                cmd = new SqlCommand("Inser_CompanyBackupFolderMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCodeDetailid", lblProductCodeDetailid.Text);
                cmd.Parameters.AddWithValue("@FolderID", lblFolderID.Text);
                cmd.Parameters.AddWithValue("@Subfolderid", lblSubfolderid.Text);
                cmd.Parameters.AddWithValue("@Subsubfolderid", lblSubsubfolderid.Text);
                cmd.Parameters.AddWithValue("@Folderath", path);
                cmd.Parameters.AddWithValue("@FolderName", lbllastfolder.Text);
                cmd.Parameters.AddWithValue("@PortalBackupRuleMasterId", dtcln22v.Rows[0]["ID"].ToString());
                con.Open();  
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //

            if (DropDownList2.SelectedValue == "1")
            {    
                    foreach (GridViewRow gdr in GridView2.Rows)
                    {
                        Label lbltime = (Label)gdr.FindControl("lbltime");

                        string strruledetail = "Insert into PortalBackupRuleMoreThanOnceTbl (PortalBackupRuleMasterId,BackupTime) values ('" + dtcln22v.Rows[0]["ID"].ToString() + "','" + lbltime.Text + "') ";

                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        SqlCommand cmdruledetail = new SqlCommand(strruledetail, con);

                        cmdruledetail.ExecuteNonQuery();
                        con.Close();
                    }
            }
            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully.";

            fillclear();
        }

    }

    protected void fillgrid()
    {
        string strcln22v = "select PortalBackupRuleMasterTbl.*,PortalMasterTbl.PortalName,ProductMaster.ProductName,case when ( NotificationEmail='1') then 'Yes' else 'No' end as NotificationEmaildisplay ,case when ( FrequecyTypeForBackup='1') then 'More than once a day' else case when (FrequecyTypeForBackup='2') then 'Once a day' else  case when (FrequecyTypeForBackup='3') then 'Weekly' else case when (FrequecyTypeForBackup='4') then 'Monthly' else '' End End end  end as FrequecyTypeForBackupDisplay from PortalBackupRuleMasterTbl inner join PortalMasterTbl on PortalMasterTbl.Id=PortalBackupRuleMasterTbl.PortalId inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId where  ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " ";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);

        GridView1.DataSource = dtcln22v;
        GridView1.DataBind();


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }

        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            Button5.Visible = true;
            Button2.Visible = false;

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
            ViewState["Id"] = i.ToString();

            string strcln = "select PortalBackupRuleMasterTbl.*,PortalMasterTbl.PortalName,ProductMaster.ProductId,case when ( FrequecyTypeForBackup='1') then 'More than once a day' else case when (FrequecyTypeForBackup='2') then 'Everyday' else  case when (FrequecyTypeForBackup='3') then 'Weekly' else case when (FrequecyTypeForBackup='4') then 'Monthly' else '' End End end  end as FrequecyTypeForBackupDisplay from PortalBackupRuleMasterTbl inner join PortalMasterTbl on PortalMasterTbl.Id=PortalBackupRuleMasterTbl.PortalId inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId where PortalBackupRuleMasterTbl.ID='" + i + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                FillProduct();
                ddlProductname1.SelectedIndex = ddlProductname1.Items.IndexOf(ddlProductname1.Items.FindByValue(dtcln.Rows[0]["ProductId"].ToString()));
                fillportal();
                ddlportal.SelectedIndex = ddlportal.Items.IndexOf(ddlportal.Items.FindByValue(dtcln.Rows[0]["PortalId"].ToString()));



                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dtcln.Rows[0]["NoofCoppies"].ToString()));
                DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(dtcln.Rows[0]["FrequecyTypeForBackup"].ToString()));

                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = false;

                if (dtcln.Rows[0]["FrequecyTypeForBackup"].ToString() == "1")
                {
                    Panel2.Visible = true;

                    string strdetailgrd = "select PortalBackupRuleMoreThanOnceTbl.*,BackupTime as Time,BackupTime as TimeDisplay from PortalBackupRuleMasterTbl inner join PortalBackupRuleMoreThanOnceTbl on PortalBackupRuleMoreThanOnceTbl.PortalBackupRuleMasterId=PortalBackupRuleMasterTbl.ID  where PortalBackupRuleMoreThanOnceTbl.PortalBackupRuleMasterId='" + i + "' ";
                    SqlCommand cmddetailgrd = new SqlCommand(strdetailgrd, con);
                    DataTable dtdetailgrd = new DataTable();
                    SqlDataAdapter adpdetailgrd = new SqlDataAdapter(cmddetailgrd);
                    adpdetailgrd.Fill(dtdetailgrd);

                    ViewState["data"] = dtdetailgrd;

                    GridView2.DataSource = dtdetailgrd;
                    GridView2.DataBind();

                }

                if (dtcln.Rows[0]["FrequecyTypeForBackup"].ToString() == "2")
                {
                    Panel3.Visible = true;

                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(dtcln.Rows[0]["EveryDayTime"].ToString()));

                }

                if (dtcln.Rows[0]["FrequecyTypeForBackup"].ToString() == "3")
                {
                    Panel4.Visible = true;

                    DropDownList6.SelectedIndex = DropDownList6.Items.IndexOf(DropDownList6.Items.FindByValue(dtcln.Rows[0]["WeeklyDay"].ToString()));
                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(dtcln.Rows[0]["WeeklyTime"].ToString()));
                }

                if (dtcln.Rows[0]["FrequecyTypeForBackup"].ToString() == "4")
                {
                    Panel5.Visible = true;

                    DropDownList7.SelectedIndex = DropDownList7.Items.IndexOf(DropDownList7.Items.FindByValue(dtcln.Rows[0]["MonthlyDay"].ToString()));
                    DropDownList8.SelectedIndex = DropDownList8.Items.IndexOf(DropDownList8.Items.FindByValue(dtcln.Rows[0]["MonthlyTime"].ToString()));
                }

                CheckBox1.Checked = Convert.ToBoolean(dtcln.Rows[0]["NotificationEmail"].ToString());
                CheckBox1_CheckedChanged(sender, e);
                //------------------

                fillcodedefaultcategory();
                FillMainFOlderdown();
                string stpageall = " SELECT dbo.CompanyBackupFolderMaster.ProductCodeDetailid,CompanyBackupFolderMaster.ProductCodeDetailid as ProductCodeDetailid,dbo.ProductCodeDetailTbl.CodeTypeName, CompanyBackupFolderMaster.FolderName, dbo.CompanyBackupFolderMaster.Folderath, dbo.FolderCategoryMaster1.FolderCatName as FolderCatName, dbo.FolderSubCatName.FolderSubName as FolderSubName, dbo.FolderSubSubCategory.FolderSubSubName, dbo.FolderCategoryMaster1.FolderMasterId as FolderID,  dbo.FolderSubSubCategory.FolderSubSubId as Subsubfolderid, dbo.FolderSubCatName.FolderSubId as Subfolderid FROM dbo.FolderSubCatName RIGHT OUTER JOIN dbo.ProductCodeDetailTbl INNER JOIN dbo.CompanyBackupFolderMaster INNER JOIN dbo.FolderCategoryMaster1 ON dbo.CompanyBackupFolderMaster.FolderID = dbo.FolderCategoryMaster1.FolderMasterId ON  dbo.ProductCodeDetailTbl.Id = dbo.CompanyBackupFolderMaster.ProductCodeDetailid ON  dbo.FolderSubCatName.FolderSubId = dbo.CompanyBackupFolderMaster.Subfolderid LEFT OUTER JOIN dbo.FolderSubSubCategory ON dbo.CompanyBackupFolderMaster.Subsubfolderid = dbo.FolderSubSubCategory.FolderSubSubId  Where CompanyBackupFolderMaster.PortalBackupRuleMasterId='" + ViewState["Id"] + "'";
                SqlCommand cmall = new SqlCommand(stpageall, con);
                DataTable dtall = new DataTable();
                SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                adpall.Fill(dtall);
                Session["GridFileAttach1"] = null;
                if (dtall.Rows.Count > 0)
                {
                    Session["GridFileAttach1"] = dtall;
                    gridFileAttach.DataSource = dtall;
                    gridFileAttach.DataBind();
                    rbfolder.SelectedValue = "1";
                    pnlatach.Visible = true;
                }
                else
                {
                    rbfolder.SelectedValue = "0";
                    pnlatach.Visible = false;
                }
            
            }
        }

        if (e.CommandName == "del1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            string str = " delete from PortalBackupRuleMasterTbl where ID='" + GridView1.SelectedIndex + "'";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully.";
            fillclear();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string strcln = "select * from PortalBackupRuleMasterTbl where PortalId='" + ddlportal.SelectedValue + "' and ID<>'" + ViewState["Id"].ToString() + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            string str = "";

            if (DropDownList2.SelectedValue == "1")
            {
                str = " update  PortalBackupRuleMasterTbl set PortalId='" + ddlportal.SelectedValue + "', NoofCoppies='" + DropDownList1.SelectedValue + "' ,FrequecyTypeForBackup='" + DropDownList2.SelectedValue + "',NotificationEmail='" + CheckBox1.Checked + "' where ID='" + ViewState["Id"].ToString() + "'  ";
            }
            if (DropDownList2.SelectedValue == "2")
            {
                str = " update  PortalBackupRuleMasterTbl set PortalId='" + ddlportal.SelectedValue + "', NoofCoppies='" + DropDownList1.SelectedValue + "' ,FrequecyTypeForBackup='" + DropDownList2.SelectedValue + "',NotificationEmail='" + CheckBox1.Checked + "',EveryDayTime='" + DropDownList4.SelectedValue + "' where ID='" + ViewState["Id"].ToString() + "'  ";
            }
            if (DropDownList2.SelectedValue == "3")
            {
                str = " update  PortalBackupRuleMasterTbl set PortalId='" + ddlportal.SelectedValue + "', NoofCoppies='" + DropDownList1.SelectedValue + "' ,FrequecyTypeForBackup='" + DropDownList2.SelectedValue + "',NotificationEmail='" + CheckBox1.Checked + "',WeeklyDay='" + DropDownList6.SelectedValue + "',WeeklyTime='" + DropDownList5.SelectedValue + "' where ID='" + ViewState["Id"].ToString() + "'  ";
            }
            if (DropDownList2.SelectedValue == "4")
            {
                str = " update  PortalBackupRuleMasterTbl set PortalId='" + ddlportal.SelectedValue + "', NoofCoppies='" + DropDownList1.SelectedValue + "' ,FrequecyTypeForBackup='" + DropDownList2.SelectedValue + "',NotificationEmail='" + CheckBox1.Checked + "',MonthlyDay='" + DropDownList7.SelectedValue + "',MonthlyTime='" + DropDownList8.SelectedValue + "' where ID='" + ViewState["Id"].ToString() + "'  ";
            }

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();


            string st2 = " Delete from CompanyBackupFolderMaster where PortalBackupRuleMasterId=" + ViewState["Id"].ToString();
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label lblFolderID = (Label)gdr.FindControl("lblFolderID");
                Label lblSubfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label lblSubsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                Label lblFolderCatName = (Label)gdr.FindControl("lblFolderCatName");
                Label lblFolderSubName = (Label)gdr.FindControl("lblFolderSubName");
                Label lblFolderSubSubName = (Label)gdr.FindControl("lblFolderSubSubName");
                Label lbllastfolder = (Label)gdr.FindControl("lbllastfolder");

                Label lblCodeTypeName = (Label)gdr.FindControl("lblCodeTypeName");
                Label lblProductCodeDetailid = (Label)gdr.FindControl("lblProductCodeDetailid");

                string path = "";
                path = lblFolderCatName.Text;
                if (lblFolderSubName.Text == "0")
                {
                    path += "\\" + lblFolderSubName.Text;
                    if (lblFolderSubSubName.Text == "0")
                    {
                        path += "\\" + lblFolderSubSubName.Text;
                    }
                }
                con.Open();
                 cmd = new SqlCommand("Inser_CompanyBackupFolderMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCodeDetailid", lblProductCodeDetailid.Text);
                cmd.Parameters.AddWithValue("@FolderID", lblFolderID.Text);
                cmd.Parameters.AddWithValue("@Subfolderid", lblSubfolderid.Text);
                cmd.Parameters.AddWithValue("@Subsubfolderid", lblSubsubfolderid.Text);
                cmd.Parameters.AddWithValue("@Folderath", path);
                cmd.Parameters.AddWithValue("@FolderName", lbllastfolder.Text);
                cmd.Parameters.AddWithValue("@PortalBackupRuleMasterId", ViewState["Id"].ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }    

            if (DropDownList2.SelectedValue == "1")
            {
                // for IP user Grid
                string strdelete = " Delete from PortalBackupRuleMoreThanOnceTbl where PortalBackupRuleMasterId='" + ViewState["Id"].ToString() + "' ";
                SqlCommand cmddelete = new SqlCommand(strdelete, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddelete.ExecuteNonQuery();
                con.Close();

                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    Label lbltime = (Label)gdr.FindControl("lbltime");


                    string strnoofuser = "insert into PortalBackupRuleMoreThanOnceTbl(PortalBackupRuleMasterId,BackupTime) values ('" + ViewState["Id"].ToString() + "','" + lbltime.Text + "')";
                    SqlCommand cmdnoofuser = new SqlCommand(strnoofuser, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdnoofuser.ExecuteNonQuery();
                    con.Close();

                }
                // End for IP user Grid
            }

            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
            fillclear();
        }


    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        fillclear();
    }
    protected void fillclear()
    {
        //TextBox1.Text = "";
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();
        Session["GridFileAttach1"] = null;


        GridView2.DataSource = null;
        GridView2.DataBind();
        ddlProductname1.SelectedIndex = 0;
        DropDownList1.SelectedIndex = 0;


        Panel1.Visible = false;

        Button2.Visible = true;
        Button5.Visible = false;
        CheckBox1.Checked = true;

        DropDownList2.SelectedValue = "2";

        if (DropDownList2.SelectedValue == "1")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "2")
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "3")
        {
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            Panel5.Visible = false;
        }
        if (DropDownList2.SelectedValue == "4")
        {
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = true;
        }
    }
      //----------------------------------------------------------------------
    //-------Folder------------------------------------------------------
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    
    protected void fillcodedefaultcategory()
    {
        DataTable dtcln = selectBZ("SELECT * from ProductCodeDetailTbl where ProductId='" + ddlProductname1.SelectedValue + "'"); // ViewState["ProductId"].ToString() replace 7/23 ninad
        ddlcodecate.DataSource = dtcln;
        ddlcodecate.DataValueField = "Id";
        ddlcodecate.DataTextField = "CodeTypeName";
        ddlcodecate.DataBind();
    }
    protected void ddlcodecate_SelectedIndexChangedMainFolder(object sender, EventArgs e)
    {
        
    }
    protected void FillMainFOlderdown()
    {
        if (ddlProductname1.SelectedIndex > 0)
        {
            string cmdstr = " SELECT FolderMasterId , FolderCatName from FolderCategoryMaster1 where  ProductId=" + ddlProductname1.SelectedValue + " and Activestatus='Active' ";
            SqlCommand cmdcln = new SqlCommand(cmdstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_MainFolder.DataSource = dtcln;
            ddl_MainFolder.DataValueField = "FolderMasterId";
            ddl_MainFolder.DataTextField = "FolderCatName";
            ddl_MainFolder.DataBind();
            ddl_MainFolder.Items.Insert(0, "-Select Main Folder-");
            ddl_MainFolder.Items[0].Value = "0";
        }
    }
    protected void FillSubfolder()
    {
        if (ddl_MainFolder.SelectedIndex > 0)
        {

            string cmdstr = "select  FolderSubId,FolderSubName from FolderSubCatName where  FolderMasterId='" + ddl_MainFolder.SelectedValue + "' and Activestatus='1'";
            SqlCommand cmdcln = new SqlCommand(cmdstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_subfolder.DataSource = dtcln;

            ddl_subfolder.DataValueField = "FolderSubId";
            ddl_subfolder.DataTextField = "FolderSubName";
            ddl_subfolder.DataBind();
            ddl_subfolder.Items.Insert(0, "-Select Sub Folder-");
            ddl_subfolder.Items[0].Value = "0";
        }
    }
    protected void FillSubSubFolder()
    {
        if (ddl_subfolder.SelectedIndex > 0)
        {
            ddl_SubSubfolder.Items.Clear();
            string strcln = " SELECT  * From FolderSubSubCategory Where Activestatus='1' and FolderSubId='" + ddl_subfolder.SelectedValue + "'  Order By FolderSubSubName ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddl_SubSubfolder.DataSource = dtcln;
            ddl_SubSubfolder.DataValueField = "FolderSubSubId";
            ddl_SubSubfolder.DataTextField = "FolderSubSubName";
            ddl_SubSubfolder.DataBind();
            ddl_SubSubfolder.Items.Insert(0, "---Select Subsub Folder---");
            ddl_SubSubfolder.Items[0].Value = "0";
        }



    }

    protected void ddlMainMenu_SelectedIndexChangedMainFolder(object sender, EventArgs e)
    {

        // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text;
        FillSubfolder();

    }
    protected void ddlMainMenu_SelectedIndexChangedsubFolder(object sender, EventArgs e)
    {
        // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text;
        FillSubSubFolder();
    }

    protected void ddlMainMenu_SelectedIndexChangedSubsubFolder(object sender, EventArgs e)
    {
        if (ddl_SubSubfolder.SelectedIndex > 0)
        {
            // txtFolderName.Text = ddl_MainFolder.SelectedItem.Text + "/" + ddl_subfolder.SelectedItem.Text + "/" + ddl_SubSubfolder.SelectedItem.Text;
        }
    }

    protected void Button2_ClickAdd(object sender, EventArgs e)
    {
        lblempmsg.Visible = false;
        if (ddl_MainFolder.SelectedIndex > 0)
        {

            String filename = "";
            string audiofile = "";
            DataTable dt = new DataTable();

            int add = 1;
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label FolderID = (Label)gdr.FindControl("lblFolderID");
                Label Subfolderid = (Label)gdr.FindControl("lblSubfolderid");
                Label Subsubfolderid = (Label)gdr.FindControl("lblSubsubfolderid");

                if (ddl_MainFolder.SelectedValue == FolderID.Text)
                {
                    if (ddl_subfolder.SelectedValue == Subfolderid.Text)
                    {
                        if (ddl_SubSubfolder.SelectedValue == Subsubfolderid.Text)
                        {
                            add = 0;
                        }
                    }
                }
            }
            if (add == 1)
            {
                if (Session["GridFileAttach1"] == null)
                {
                    lblempmsg.Visible = false;
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "ProductCodeDetailid";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "CodeTypeName";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "FolderID";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                    DataColumn dtcom5 = new DataColumn();
                    dtcom5.DataType = System.Type.GetType("System.String");
                    dtcom5.ColumnName = "FolderCatName";
                    dtcom5.ReadOnly = false;
                    dtcom5.Unique = false;
                    dtcom5.AllowDBNull = true;
                    dt.Columns.Add(dtcom5);

                    DataColumn dtcom6 = new DataColumn();
                    dtcom6.DataType = System.Type.GetType("System.String");
                    dtcom6.ColumnName = "Subfolderid";
                    dtcom6.ReadOnly = false;
                    dtcom6.Unique = false;
                    dtcom6.AllowDBNull = true;
                    dt.Columns.Add(dtcom6);

                    DataColumn dtcom7 = new DataColumn();
                    dtcom7.DataType = System.Type.GetType("System.String");
                    dtcom7.ColumnName = "FolderSubName";
                    dtcom7.ReadOnly = false;
                    dtcom7.Unique = false;
                    dtcom7.AllowDBNull = true;
                    dt.Columns.Add(dtcom7);

                    DataColumn dtcom8 = new DataColumn();
                    dtcom8.DataType = System.Type.GetType("System.String");
                    dtcom8.ColumnName = "Subsubfolderid";
                    dtcom8.ReadOnly = false;
                    dtcom8.Unique = false;
                    dtcom8.AllowDBNull = true;
                    dt.Columns.Add(dtcom8);

                    DataColumn dtcom9 = new DataColumn();
                    dtcom9.DataType = System.Type.GetType("System.String");
                    dtcom9.ColumnName = "FolderSubSubName";
                    dtcom9.ReadOnly = false;
                    dtcom9.Unique = false;
                    dtcom9.AllowDBNull = true;
                    dt.Columns.Add(dtcom9);

                    DataColumn dtcom10 = new DataColumn();
                    dtcom10.DataType = System.Type.GetType("System.String");
                    dtcom10.ColumnName = "FolderName";
                    dtcom10.ReadOnly = false;
                    dtcom10.Unique = false;
                    dtcom10.AllowDBNull = true;
                    dt.Columns.Add(dtcom10);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();

                dtrow["ProductCodeDetailid"] = ddlcodecate.SelectedValue;
                dtrow["CodeTypeName"] = ddlcodecate.SelectedItem.Text;
                dtrow["FolderID"] = ddl_MainFolder.SelectedValue;
                dtrow["FolderCatName"] = ddl_MainFolder.SelectedItem.Text;

                dtrow["FolderName"] = ddl_MainFolder.SelectedItem.Text;
                if (ddl_subfolder.SelectedIndex > 0)
                {
                    dtrow["FolderSubName"] = ddl_subfolder.SelectedItem.Text;
                    dtrow["Subfolderid"] = ddl_subfolder.SelectedValue;

                    dtrow["FolderName"] = ddl_subfolder.SelectedItem.Text;
                }
                else
                {
                    dtrow["FolderSubName"] = "";
                    dtrow["Subfolderid"] = "0";
                }

                if (ddl_SubSubfolder.SelectedIndex > 0)
                {
                    dtrow["FolderSubSubName"] = ddl_SubSubfolder.SelectedItem.Text;
                    dtrow["Subsubfolderid"] = ddl_SubSubfolder.SelectedValue;

                    dtrow["FolderName"] = ddl_SubSubfolder.SelectedItem.Text;
                }
                else
                {
                    dtrow["FolderSubSubName"] = "";
                    dtrow["Subsubfolderid"] = "0";
                }
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                if (Session["GridFileAttach1"] != null)
                {
                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                }
                ddl_MainFolder.SelectedIndex = 0;
                ddl_subfolder.SelectedIndex = 0;
                ddl_SubSubfolder.SelectedIndex = 0;
            }
            else
            {
                lblempmsg.Visible = true;
            }
        }
    }

    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                if (gridFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1"];

                    dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);
                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                    Session["GridFileAttach1"] = dt;
                }
            }

        }
    }

    protected void rbfolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbfolder.SelectedValue == "0")
        {
            pnlatach.Visible = false; 
        }
        else
        {
            pnlatach.Visible = true;
        }
    }
}
