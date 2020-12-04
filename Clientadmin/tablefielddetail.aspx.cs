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
using System.Data.SqlClient;
using System.Data.Common;

public partial class tablefielddetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillProduct();
            FillGrid();
        }
    }
    protected void FillGrid()
    {
        string filte = "";
        if (ddlpversion.SelectedIndex > 0)
        {
            filte = " and ClientProductTableMaster.VersionInfoId='" + ddlpversion.SelectedValue + "'";
        }
        if (TextBox1.Text != "")
        {

            filte += " and ((ClientProductTableMaster.TableName like '%" + TextBox1.Text.Replace("'", "''") + "%' OR ClientProductTableMaster.TableTitle like '%" + TextBox1.Text.Replace("'", "''") + "%' ))";

        }

        string strcln = "SELECT distinct  ClientProductTableMaster.*  FROM  ProductMaster INNER JOIN  VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId INNER JOIN ClientProductTableMaster ON VersionInfoMaster.VersionInfoId = ClientProductTableMaster.VersionInfoId left join ParentTableAdd on ParentTableAdd.TableId =  ClientProductTableMaster.Id left join TableCategory on TableCategory.Id=ParentTableAdd.Category left join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=ClientProductTableMaster.Databaseid  inner join CodeTypeTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where ClientProductTableMaster.Id>0   " + filte + "";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            GridView1.DataSource = dtcln;
            GridView1.DataBind();
            foreach (GridViewRow gvbn in GridView1.Rows)
            {
               
                Label lbl1 = (Label)gvbn.FindControl("Label355");
                Label id = (Label)gvbn.FindControl("Label11");
                 DataTable du=select("select feildname from  tablefielddetail where TableId='"+id.Text+"'");
                 string jj = "";
                if(du.Rows.Count>0)
                {
                    for(int j=0;j<du.Rows.Count;j++)
                    {
                        
                        if (j == 0)
                        {
                            jj = du.Rows[j]["feildname"].ToString();
                        }
                        else
                        {
                            string aa = du.Rows[j]["feildname"].ToString();
                            jj = "" + jj + "," + aa + "";
                        }
                       
                    }
                }
                lbl1.Text=jj;
            }

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
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
    protected DataTable select1(string str)
    {
        SqlCommand cmd = new SqlCommand(str, conn1);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    public void FillProduct()
    {
        String strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName  as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            ddlpversion.DataSource = dtcln;
            ddlpversion.DataValueField = "VersionInfoId";
            ddlpversion.DataTextField = "productversion";
            ddlpversion.DataBind();
        }
        ddlpversion.Items.Insert(0, "All");
        ddlpversion.Items[0].Value = "0";


     
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
   
    protected void imgbtnedit_Click(object sender, ImageClickEventArgs e)
    {
        Label360.Text = "";
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label mm = (Label)GridView1.Rows[j].FindControl("Label11");//CheckBox6
        DataTable gg = select("select feildname as feildname,fieldtype as type,size as size,isprimarykey as primarykey ,notallownull as nullvalue,Isforeignkey as foreignkey,foreignkeytblid as keytableid,foreignkeyfieldId as keyfeildid,foreignkeytblid as keytable,foreignkeyfieldId as keyfeild from tablefielddetail where tableId='" + mm.Text + "' ");
        DataTable prod = select("select * from ClientProductTableMaster where id='" + mm.Text + "'");
        ViewState["tableid"] = mm.Text;
        ViewState["productid"] = prod.Rows[0]["VersionInfoId"].ToString();
        ViewState["databaseid"] = prod.Rows[0]["Databaseid"].ToString();
        ViewState["TableName"] = prod.Rows[0]["TableName"].ToString();
        if (gg.Rows.Count > 0)
        {

            Panel9.Visible = true;
            DataTable dtrf = new DataTable();
            ViewState["table"] = "";
            if (Convert.ToString(ViewState["table"]) == "")
            {
                dtrf = CreateDatatable1();
            }
            else
            {
                dtrf = (DataTable)ViewState["table"];
            }
            for (int k = 0; k < gg.Rows.Count; k++)
            {

                DataRow Drow = dtrf.NewRow();
                Drow["feildname"] = gg.Rows[k]["feildname"].ToString();
                Drow["type"] = gg.Rows[k]["type"].ToString();
                Drow["size"] = gg.Rows[k]["size"].ToString();
                Drow["primarykey"] = gg.Rows[k]["primarykey"].ToString();
                Drow["foreignkey"] = gg.Rows[k]["foreignkey"].ToString();

                DataTable dt23 = select("select * from ClientProductTableMaster where id='" + gg.Rows[k]["keytableid"].ToString() + "' ");
                if (dt23.Rows.Count > 0)
                {
                    Drow["keytable"] = dt23.Rows[0]["TableName"].ToString();
                    Drow["keytableid"] = gg.Rows[k]["keytableid"].ToString();
                }
                else
                {
                }
                DataTable dt231 = select("select * from tablefielddetail where Id='" + gg.Rows[k]["keyfeildid"].ToString() + "' ");
                if (dt231.Rows.Count > 0)
                {
                    Drow["keyfeild"] = dt231.Rows[0]["feildname"].ToString();
                    Drow["keyfeildid"] = gg.Rows[k]["keyfeildid"].ToString();
                }

                Drow["nullvalue"] = gg.Rows[k]["nullvalue"].ToString();
                dtrf.Rows.Add(Drow);


            }
            ViewState["table"] = dtrf;
            GridView8.DataSource = dtrf;
            GridView8.DataBind();
        }
    }
    public DataTable CreateDatatable1()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "feildname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "type";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "size";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "primarykey";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "foreignkey";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "keytable";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        DataColumn Dcom51 = new DataColumn();
        Dcom51.DataType = System.Type.GetType("System.String");
        Dcom51.ColumnName = "keytableid";
        Dcom51.AllowDBNull = true;
        Dcom51.Unique = false;
        Dcom51.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "keyfeild";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;


        DataColumn Dcom61 = new DataColumn();
        Dcom61.DataType = System.Type.GetType("System.String");
        Dcom61.ColumnName = "keyfeildid";
        Dcom61.AllowDBNull = true;
        Dcom61.Unique = false;
        Dcom61.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "nullvalue";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom51);
        dt.Columns.Add(Dcom61);
        return dt;
      
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        Panel10.Visible = true;
        CreateDatatable();
    }
    public void CreateDatatable()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "feildname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "type";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "size";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "primarykey";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "foreignkey";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "keytable";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "keyfeild";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "nullvalue";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);

        DataRow Drow = dt.NewRow();
        Drow["feildname"] = "";
        Drow["type"] = "";
        Drow["size"] = "";
        Drow["primarykey"] = "";
        Drow["keytable"] = "";
        Drow["keyfeild"] = "";
        Drow["nullvalue"] = "";
        Drow["foreignkey"] = "";
        dt.Rows.Add(Drow);



        gvaddnew.DataSource = dt;
        gvaddnew.DataBind();
        gvaddnew.HeaderRow.Cells[5].Visible = false;
        gvaddnew.Columns[5].Visible = false;
        gvaddnew.HeaderRow.Cells[6].Visible = false;
        gvaddnew.Columns[6].Visible = false;
        foreach (GridViewRow gr5 in gvaddnew.Rows)
        {
            DropDownList itemid = ((DropDownList)gr5.FindControl("DropDownList8"));
            CheckBox nul = ((CheckBox)gr5.FindControl("CheckBox6"));
            if (ViewState["sp"] == "1")
            {
                nul.Checked = true;
            }

            string strcln = " Select ClientProductTableMaster.TableName,ClientProductTableMaster.Id as tableid From ClientProductTableMaster Where  ClientProductTableMaster.Databaseid='" + ViewState["databaseid"].ToString() + "'";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                itemid.DataSource = dtcln;
                itemid.DataValueField = "tableid";
                itemid.DataTextField = "TableName";
                itemid.DataBind();
                itemid.Items.Insert(0, "--Select--");
                itemid.Items[0].Value = "0";



            }

        }
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)//foreign k
    {
        CheckBox lnkbtn = (CheckBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //Label lblcandiID = (Label)GridView1.Rows[j].FindControl("lblcandiID");
        CheckBox lblcandiID = (CheckBox)gvaddnew.Rows[j].FindControl("CheckBox5");


        gvaddnew.HeaderRow.Cells[5].Visible = true;
        gvaddnew.Columns[5].Visible = true;
        gvaddnew.HeaderRow.Cells[6].Visible = true;
        gvaddnew.Columns[6].Visible = true;
    }
    protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView8.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["table"];
            dt.Rows[Convert.ToInt32(GridView8.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            GridView8.DataSource = dt;
            GridView8.DataBind();
            ViewState["table"] = dt;
            //ModalPopupExtender4.Show();
            //  Label26.Text = "Record deleted successfully.";

        }
    }
    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList lnkbtn = (DropDownList)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //Label lblcandiID = (Label)GridView1.Rows[j].FindControl("lblcandiID");
        DropDownList DropDownList8 = (DropDownList)gvaddnew.Rows[j].FindControl("DropDownList8");//CheckBox6
        DropDownList DropDownList9 = (DropDownList)gvaddnew.Rows[j].FindControl("DropDownList9");
        DataTable dty = select("select Id,feildname from tablefielddetail where tableId='" + DropDownList8.SelectedValue + "'");
        if (dty.Rows.Count > 0)
        {
            DropDownList9.DataSource = dty;
            DropDownList9.DataValueField = "Id";
            DropDownList9.DataTextField = "feildname";
            DropDownList9.DataBind();
            DropDownList9.Items.Insert(0, "--Select--");
            DropDownList9.Items[0].Value = "0";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        filladdtable();
    }
    public void filladdtable()
    {


        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["table"]) == "")
        {
            dt = CreateDatatable1();
        }
        else
        {
            dt = (DataTable)ViewState["table"];
        }

        foreach (GridViewRow gr5 in gvaddnew.Rows)
        {
            TextBox txtfeild = ((TextBox)gr5.FindControl("TextBox2"));
            DropDownList ddltype = ((DropDownList)gr5.FindControl("ddlfiledtype"));
            TextBox txtsize = ((TextBox)gr5.FindControl("TextBox21"));
            CheckBox chkprimary = ((CheckBox)gr5.FindControl("CheckBox4"));
            CheckBox chkforign = ((CheckBox)gr5.FindControl("CheckBox5"));
            DropDownList ddlforigntable = ((DropDownList)gr5.FindControl("DropDownList8"));
            DropDownList ddlforignfeild = ((DropDownList)gr5.FindControl("DropDownList9"));
            CheckBox chknull = ((CheckBox)gr5.FindControl("CheckBox6"));

            DataRow Drow = dt.NewRow();
            Drow["feildname"] = txtfeild.Text;
            Drow["type"] = ddltype.SelectedValue;
            Drow["size"] = txtsize.Text;
            Drow["primarykey"] = chkprimary.Checked;
            Drow["foreignkey"] = chkforign.Checked;
            if (chkforign.Checked == true)
            {
                Drow["keytable"] = ddlforigntable.SelectedItem.Text;
                Drow["keytableid"] = ddlforigntable.SelectedValue;

                try
                {
                    Drow["keyfeild"] = ddlforignfeild.SelectedItem.Text;
                    Drow["keyfeildid"] = ddlforignfeild.SelectedValue;
                }
                catch
                {
                }
            }
            else
            {
                Drow["keytable"] = "";
                Drow["keyfeild"] = "";
                Drow["keytableid"] = "";
                Drow["keyfeildid"] = "";
            }
            Drow["nullvalue"] = chknull.Checked;
            dt.Rows.Add(Drow);
        }
        ViewState["table"] = dt;
        GridView8.DataSource = dt;
        GridView8.DataBind();
        gvaddnew.DataSource = "";
        gvaddnew.DataBind();
        Panel10.Visible = false;
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)//primary
    {
        CheckBox lnkbtn = (CheckBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //Label lblcandiID = (Label)GridView1.Rows[j].FindControl("lblcandiID");
        CheckBox chk = (CheckBox)gvaddnew.Rows[j].FindControl("CheckBox4");//CheckBox6
        CheckBox null1 = (CheckBox)gvaddnew.Rows[j].FindControl("CheckBox6");
        if (chk.Checked == true)
        {
            null1.Checked = false;

        }

    }
    protected void ddlpversion_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void GridView8_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView8_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView8_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void Button17_Click(object sender, EventArgs e)
    {

        string str131 = "delete from  tablefielddetail where  tableId='" + ViewState["tableid"] + "'";
        SqlCommand cmd131 = new SqlCommand(str131, con);
        con.Open();
        cmd131.ExecuteNonQuery();
        con.Close();
        foreach (GridViewRow gr51 in GridView8.Rows)
        {
            Label txtfeild = ((Label)gr51.FindControl("Label25"));
            Label ddltype = ((Label)gr51.FindControl("Label27"));
            Label txtsize = ((Label)gr51.FindControl("Label28"));
            Label chkprimary = ((Label)gr51.FindControl("Label29"));
            Label chkforign = ((Label)gr51.FindControl("Label31"));
            Label ddlforigntableid = ((Label)gr51.FindControl("Label351"));
            Label ddlforignfeildid = ((Label)gr51.FindControl("Label354"));
            Label chknull = ((Label)gr51.FindControl("Label34"));
            SqlCommand cmd33 = new SqlCommand("Insert into tablefielddetail(TableId,feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull)Values('" + Convert.ToInt32(ViewState["tableid"]) + "','" + txtfeild.Text + "','" + ddltype.Text + "','" + txtsize.Text + "','" + chkforign.Text + "','" + ddlforigntableid.Text + "','" + ddlforignfeildid.Text + "','" + chkprimary.Text + "','" + chknull.Text + "')", con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd33.ExecuteNonQuery();
        }



        CheckDbConn();
        SqlDataAdapter da = new SqlDataAdapter("SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name='" + ViewState["TableName"].ToString() + "'", conn1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DataTable dt_s = new DataTable();
        if (dt.Rows.Count > 0)
        {
            
            if (dt_s.Rows.Count < 1)
            {
                dt_s.Columns.Add("COLUMN_NAME");
                dt_s.Columns.Add("DATA_TYPE");
                
            }
            for (int k = 0; k < dt.Rows.Count;k++)
            {
              
                DataRow dr = dt_s.NewRow();
                dr["COLUMN_NAME"] = dt.Rows[k]["COLUMN_NAME"].ToString();
                dr["DATA_TYPE"] = dt.Rows[k]["DATA_TYPE"].ToString();
                dt_s.Rows.Add(dr);
            }

            if (dt_s.Rows.Count == GridView8.Rows.Count)
            {
                foreach (GridViewRow gr5 in GridView8.Rows)
                {
                    TextBox txtfeild = ((TextBox)gr5.FindControl("TextBox2"));
                }
            }
            else
            {
                ModalPopupExtender1.Show();
                GridView9.DataSource = dt_s;
                GridView9.DataBind();
                DataTable dtu = select("select feildname,fieldtype from tablefielddetail where TableId='" + ViewState["tableid"] + "'");
                GridView10.DataSource = dtu;
                GridView10.DataBind();

            }
        }
        else
        {
            
        }
        Panel9.Visible = false;
        Label360.Text = "Updated Successfully";
   
    }
    private void CheckDbConn()
    {

//        DataTable dtconn = select(@"SELECT ServerMasterTbl.ServerName,ServerMasterTbl.port,ServerMasterTbl.sqlserveruserid,ServerMasterTbl.Sapassword,Instancename 
//                                     FROM ServerMasterTbl INNER JOIN ClientMaster ON ServerMasterTbl.Id = ClientMaster.ServerId inner join ProductMaster on ProductMaster.ClientID=ClientMaster.ClientMasterId
//                                     inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ProductVersionId=VersionInfoMaster.VersionInfoId  where CodeTypeTbl.ProductCodeDetailId='" + ViewState["databaseid"] + "'");
//        if (dtconn.Rows.Count > 0)
//        {

//        }
        
//        string strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and ProductCodeDetailTbl.Id='" + ViewState["databaseid"] + "' and CodeTypeCategory.Id='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ";
//        // string strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  CodeTypeTbl.ProductVersionId='" + ddlProductVersion.SelectedValue + "' ";
//        SqlCommand cmdcln = new SqlCommand(strcln, con);
//        DataTable dtcln = new DataTable();
//        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
//        adpcln.Fill(dtcln);
//        try
//        {
//            // conn.ConnectionString = @"Data Source =" + dtconn.Rows[0]["SqlServerName"] + "\\" + dtconn.Rows[0]["instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog = " +  DropDownList1.SelectedItem.Text + "; Integrated Security = true";
//            // String strConn = System.Configuration.ConfigurationManager.ConnectionStrings["@Data Source=TCP:192.168.9.120,30000; Initial Catalog = Licensejobcenter.OADB; Persist Security Info=true;"].ToString();
//            //conn1.ConnectionString = "@Data Source=TCP:192.168.9.120,30000; Initial Catalog = Licensejobcenter.OADB; User ID=TVMDeveloper; Password=Om2015++; Persist Security Info=true;";
//            conn1 = new SqlConnection();
//          conn1.ConnectionString = @"Data Source =" + dtconn.Rows[0]["ServerName"] + "\\" + dtconn.Rows[0]["Instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=" + dtcln.Rows[0]["CodeTypeName"].ToString() + "; User ID=sa; Password=" +PageMgmt.Decrypted(dtconn.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true;";
//          //  conn1.ConnectionString = "Data Source =TCP:192.168.9.120,30000; Initial Catalog = jobcenter.OADB; User ID= TVMDeveloper; Password=Om2015++; Persist Security Info=true;";
          
          DataTable dtdatabaseins = select(@"SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId,CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ViewState["databaseid"] + "'");
          if (dtdatabaseins.Rows.Count > 0)
          {
              string serverid = dtdatabaseins.Rows[0]["ServerId"].ToString();
              string serversqlinstancename = dtdatabaseins.Rows[0]["Instancename"].ToString();
              DataTable dtserver = select(@" SELECT * FROM dbo.ServerMasterTbl Where Id='" + serverid + "' ");
              // Label360.Text = DropDownList1.SelectedItem.Text;
              string serversqlserverip = dtserver.Rows[0]["sqlurl"].ToString();
              //string serversqlinstancename = ddlinstance.SelectedItem.Text;
              string serversqldbname = dtserver.Rows[0]["CodeTypeName"].ToString();
              string serversqlpwd = dtserver.Rows[0]["Sapassword"].ToString();
              string serversqlport = dtserver.Rows[0]["port"].ToString();

              string Sqlinstancename = dtserver.Rows[0]["Sqlinstancename"].ToString();
              string DefaultsqlInstance = dtserver.Rows[0]["DefaultsqlInstance"].ToString();




              try
              {
                  conn1 = new SqlConnection();
                  if (serversqlinstancename == Sqlinstancename)
                  {

                      conn1.ConnectionString = @"Data Source=C3\\BUSIWIZSQL1;Initial Catalog=License.Busiwiz;Integrated Security=True";
                      conn1.ConnectionString = @"Data Source=" + serversqlserverip + "\\" + Sqlinstancename + ";Initial Catalog=" + serversqldbname + ";Integrated Security=True";
                  }
                  else
                  {

                     conn1.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                      // conn1.ConnectionString = @"Data Source =" + dtconn.Rows[0]["ServerName"] + "\\" + dtconn.Rows[0]["Instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=" + DropDownList1.SelectedItem.Text + "; User ID=sa; Password=" + PageMgmt.Decrypted(dtconn.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true;";
                  }




                  conn1.Open();
                  ConnectionState conState = conn1.State;

                  if (conState == ConnectionState.Closed || conState == ConnectionState.Broken)
                  {

                      //logger.Warn(LogTopicEnum.Agent, "Connection failed in DB connection test on CheckDBConnection");
                      //return false;
                  }
                  else
                  {

                  }
              }
              catch (Exception ex)
              {

              }
          }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckDbConn();
        conn1.Close();
        string aa = "";



        DataTable st1 = select("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where TableId='" + Convert.ToInt32(ViewState["tableid"]) + "' ");
        if (st1.Rows.Count > 0)
        {
            for (int i = 0; i < st1.Rows.Count; i++)
            {
                int f = 0;
                string size = st1.Rows[i]["fieldtype"].ToString();
                string si = "";
                if (size.ToString() == "int" || size.ToString() == "datetime" || size.ToString() == "bit")
                {
                  si = "";  
                }
                else 
                {
                    si = "(" + st1.Rows[i]["size"].ToString() + ")";
                }

                //(" + st1.Rows[i]["size"].ToString() + ")
                for (int j = 0; j < GridView9.Rows.Count;j++ )
                {
                    string hh = GridView9.Rows[j].Cells[0].Text;
                    if (hh.ToString() == st1.Rows[i]["feildname"].ToString())
                    {
                        string null1 = st1.Rows[i]["notallownull"].ToString();
                        string pk = st1.Rows[i]["isprimarykey"].ToString();
                        string nn = " NOT NULL";
                        if (null1.ToString() == "False")
                        {
                            nn = " IDENTITY(1,1) NOT NULL";
                        }
                        else
                        {
                            nn = "NULL";
                        }



                        if (pk.ToString() == "True")
                        {
                            //aa = "" + st1.Rows[0]["feildname"].ToString() + " [" + st1.Rows[0]["fieldtype"].ToString() + "] " + nn + "";
                            //string createtable = "ALTER TABLE [dbo]." + ViewState["TableName"].ToString() + " Alter Column " + aa + "";
                            //SqlCommand cmdf = new SqlCommand(createtable, conn1);

                            //cmdf.ExecuteNonQuery();
                            //conn1.Close();
                        }
                        else
                        {
                            aa = "" + st1.Rows[i]["feildname"].ToString() + " [" + st1.Rows[i]["fieldtype"].ToString() + "] " + si + " " + nn + "";
                            string createtable = "ALTER TABLE [dbo]." + ViewState["TableName"].ToString() + " Alter Column " + aa + "";
                            SqlCommand cmdf = new SqlCommand(createtable, conn1);
                            conn1.Open();
                            cmdf.ExecuteNonQuery();
                            conn1.Close();
                        }
                        f = 1;
                        break;
                    }
                }
                if (f == 0)
                {
                    string null1 = st1.Rows[i]["notallownull"].ToString();
                   
                    string nn = "NOT NULL";
                    if (null1.ToString() == "False")
                    {
                        nn = "NOT NULL";
                    }
                    else
                    {
                        nn = "NULL";
                    }
                    aa = "" + st1.Rows[i]["feildname"].ToString() + " [" + st1.Rows[i]["fieldtype"].ToString() + "] " + si + " " + nn + "";
                    string createtable = "ALTER TABLE [dbo]." + ViewState["TableName"].ToString() + " ADD " + aa + "";
                    SqlCommand cmdf = new SqlCommand(createtable, conn1);
                    conn1.Open();
                    cmdf.ExecuteNonQuery();
                    conn1.Close();
                }
            }

        }

        //DataTable st = select("select feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId,isprimarykey,notallownull from  tablefielddetail where TableId='" + Convert.ToInt32(ViewState["tableid"]) + "' and isprimarykey=0");
        //if (st.Rows.Count > 0)
        //{
        //    for (int i = 0; i < st.Rows.Count; i++)
        //    {
        //        string null1 = st.Rows[i]["notallownull"].ToString();
        //        string nn = "NOT NULL";
        //        if (null1.ToString() == "True")
        //        {
        //            nn = "NULL";
        //        }
        //        else
        //        {
        //            nn = "NOT NULL";
        //        }
        //        string bb = "";
        //        if (st.Rows[i]["fieldtype"].ToString() == "int")
        //        {
        //            bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] " + nn + "";
        //        }
        //        else
        //        {
        //            bb = "" + st.Rows[i]["feildname"].ToString() + " [" + st.Rows[i]["fieldtype"].ToString() + "] (" + st.Rows[i]["size"].ToString() + ") " + nn + "";
        //        }


        //        aa = "" + aa + "," + bb + "";
        //    }
        //}

        //string createtable = "ALTER TABLE [dbo]." + ViewState["TableName"].ToString() + " ADD (" + aa + ")";
        //SqlCommand cmdf = new SqlCommand(createtable, conn1);

        //cmdf.ExecuteNonQuery();
        //conn1.Close();


        DataTable dtconn = select(@"SELECT ServerMasterTbl.ServerName,ServerMasterTbl.port,ServerMasterTbl.sqlserveruserid,ServerMasterTbl.Sapassword,Instancename 
                                     FROM ServerMasterTbl INNER JOIN ClientMaster ON ServerMasterTbl.Id = ClientMaster.ServerId inner join ProductMaster on ProductMaster.ClientID=ClientMaster.ClientMasterId
                                     inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ProductVersionId=VersionInfoMaster.VersionInfoId  where CodeTypeTbl.ProductCodeDetailId='" + ViewState["databaseid"] + "'");
        string strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  ProductCodeDetailTbl.Active='1' and ProductCodeDetailTbl.Id='" + ViewState["databaseid"] + "' and CodeTypeCategory.Id='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ";
        // string strcln = " SELECT DISTINCT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id where  CodeTypeTbl.ProductVersionId='" + ddlProductVersion.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        string server = "" + dtconn.Rows[0]["ServerName"] + "\\" + dtconn.Rows[0]["Instancename"] + "";
        DataTable dtui = select("select category from ClientProductTableMaster where Id='" + Convert.ToInt32(ViewState["tableid"]) + "'");
        if (dtui.Rows[0][0].ToString() == "2")
        {

            string te = "scriptadd.aspx?server=" + PageMgmt.Encrypted(server.ToString()) + "&database=" + dtcln.Rows[0]["CodeTypeName"].ToString() + "&tablename=" + ViewState["TableName"].ToString() + "&productid=" + ViewState["productid"].ToString() + "&data=1";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        else
        {
            string te = "scriptadd.aspx?server=" + PageMgmt.Encrypted(server.ToString()) + "&database=" + dtcln.Rows[0]["CodeTypeName"].ToString() + "&tablename=" + ViewState["TableName"].ToString() + "&productid=" + ViewState["productid"].ToString() + "&data=2";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        ModalPopupExtender1.Hide();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Label360.Visible = true;
            int mm1 = Convert.ToInt32(e.CommandArgument);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlDataAdapter daf = new SqlDataAdapter("select * from ClientProductRecordsAllowed where ClientProductTblId='" + mm1 + "'", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);
            if (dtf.Rows.Count > 0)
            {

                Label360.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            }
            else
            {
                String str11 = "Delete From ClientProductTableMaster   where Id='" + e.CommandArgument.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(str11, con);
                //SqlCommand cmd1 = new SqlCommand("deleteproductdetaitable", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());

                cmd1.ExecuteNonQuery();
                con.Close();
                con.Open();
                String str12 = "Delete From ParentTableAdd   where TableID='" + e.CommandArgument.ToString() + "'";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                //SqlCommand cmd1 = new SqlCommand("deleteproductdetaitable", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());

                cmd12.ExecuteNonQuery();
                con.Close();

                Label360.Text = "Record deleted successfully.";
                FillGrid();
               

            }
        }
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        FillGrid();
        Label360.Text = "";
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {

        if (rdsync.SelectedValue == "1")
        {
            int transf = 0;


            DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='tablefielddetail' ");
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
                            transf = Convert.ToInt32(rdsync.SelectedValue);
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
}