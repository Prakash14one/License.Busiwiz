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

using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;

using System.Text;

using System.IO;
public partial class UserRoleforePriceplan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    string LBproductversion = "32";
    string LBproductDB = "19067";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            FillProduct();
            //ddlProductname_SelectedIndexChanged(sender, e);
            //fillgd();
            FillDefault();
        }

    }
    protected void Cleare()
    {
        FillProduct();
        fillCodetype();
        FillGrid();
    }
    protected void FillDefault()
    {
        DataTable dtclnn = MyCommonfile.selectBZ(" SELECT TOP(1) * From SatelliteSyncronisationrequiringTablesMaster where ServerProductVersionID IS NOT NULL AND ServerDatabaseID IS NOT NULL ");
        if (dtclnn.Rows.Count > 0)
        {
            FillProduct();
            ddlProductname.SelectedValue = dtclnn.Rows[0]["ServerProductVersionID"].ToString();
            fillCodetype();
            ddlcodetype.SelectedValue = dtclnn.Rows[0]["ServerDatabaseID"].ToString();
            FillGrid();
        }
    }
    protected void FillProduct()
    {   

        string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM dbo.ClientProductTableMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active =1 order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
        ddlcodetype.SelectedIndex = 0;
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCodetype(); 
    }
    protected void fillCodetype()
    {
        ddlcodetype.DataSource = null;
        ddlcodetype.DataBind();
      
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlProductname.SelectedValue + "' and CodeTypeCategory.Id='2'  order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
            ddlcodetype.DataSource = dtcln;
            ddlcodetype.DataValueField = "Id";
            ddlcodetype.DataTextField = "CodeTypeName";
            ddlcodetype.DataBind();
            ddlcodetype.Items.Insert(0, "--Select--");
            ddlcodetype.Items[0].Value = "0";
            ddlcodetype.SelectedIndex = 0;
    }
    protected void ddlcodetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void chk_active_CheckedChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void FillGrid()
    {
            GV_ServerTable.DataSource = null;
            GV_ServerTable.DataBind();       
            string str = "";
            if (chk_active.Checked == true)
            {
                str = " and ClientProductTableMaster.Id In (Select ServerTableID as Id From SatelliteSyncronisationrequiringTablesMaster where DatabaseID='" + ddlcodetype.SelectedValue + "') ";
            }
            string strcln1 = " select distinct ClientProductTableMaster.TableName, ClientProductTableMaster.Id,ClientProductTableMaster.VersionInfoId ,  dbo.ProductCodeDetailTbl.CodeTypeName from dbo.ClientProductTableMaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id where ClientProductTableMaster.active=1 and  ClientProductTableMaster.Databaseid='" + ddlcodetype.SelectedValue + "' " + str + " order by ClientProductTableMaster.TableName ";
            SqlCommand cmdcln = new SqlCommand(strcln1, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GV_ServerTable.DataSource = dtcln;
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GV_ServerTable.DataBind();
    }
    protected void GV_ServerTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //VersionDate           
            Label lbl_TableID = (Label)e.Row.FindControl("lbl_TableID");
            Label lbl_LBDAtabaseName = (Label)e.Row.FindControl("lbl_LBDAtabaseName");
            Label lnl_LBInstance = (Label)e.Row.FindControl("lnl_LBInstance");
            Label lbl_serverid = (Label)e.Row.FindControl("lbl_serverid");            
            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));


            DropDownList DDLLiceTableName = (DropDownList)(e.Row.FindControl("DDLLiceTableName"));
            //DropDownList DDLLiceTablepkname = (DropDownList)(e.Row.FindControl("DDLLiceTablepkname"));
            TextBox txt_fkname = (TextBox)(e.Row.FindControl("txt_fkname"));   
            TextBox txt_addquery = (TextBox)(e.Row.FindControl("txt_addquery"));   


            
            DataTable dtcln = MyCommonfile.selectBZ(" select distinct ClientProductTableMaster.TableName, ClientProductTableMaster.Id,ClientProductTableMaster.VersionInfoId ,  dbo.ProductCodeDetailTbl.CodeTypeName,dbo.ProductCodeDetailTbl.CodeTypeName, dbo.CodeTypeTbl.Instancename ,dbo.ClientMaster.ServerId from   dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ClientProductTableMaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.ClientProductTableMaster.Databaseid = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.CodeTypeTbl ON dbo.ProductCodeDetailTbl.Id = dbo.CodeTypeTbl.ProductCodeDetailId ON  dbo.VersionInfoMaster.VersionInfoId = dbo.ClientProductTableMaster.VersionInfoId where ClientProductTableMaster.active=1 and  ClientProductTableMaster.Databaseid='" + LBproductDB + "'  order by ClientProductTableMaster.TableName ");
            if (dtcln.Rows.Count > 0)
            {
                DDLLiceTableName.DataSource = dtcln;
                DDLLiceTableName.DataValueField = "Id";
                DDLLiceTableName.DataTextField = "TableName";
                DDLLiceTableName.DataBind();
                DDLLiceTableName.Items.Insert(0, "--Select--");
                DDLLiceTableName.Items[0].Value = "0";
            }
            lbl_LBDAtabaseName.Text = dtcln.Rows[0]["CodeTypeName"].ToString();
            lnl_LBInstance.Text = dtcln.Rows[0]["Instancename"].ToString();
            lbl_serverid.Text = dtcln.Rows[0]["ServerId"].ToString();  

            DataTable ds12web = MyCommonfile.selectBZ(" SELECT * From SatelliteSyncronisationrequiringTablesMaster Where ServerTableID='" + lbl_TableID.Text + "'");
            if (ds12web.Rows.Count > 0)
            {
                cbItem.Checked = true;
                DDLLiceTableName.SelectedValue = ds12web.Rows[0]["TableID"].ToString(); 
            }
            else
            {
                cbItem.Checked = false;
            }

            DataTable ds12where = MyCommonfile.selectBZ(" Select SatelliteSyncronisationrequiringTablesMaster_SerWhere.*,SatelliteSyncronisationrequiringTablesMaster.TableID FROM  SatelliteSyncronisationrequiringTablesMaster_SerWhere INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON SatelliteSyncronisationrequiringTablesMaster_SerWhere.TableId = SatelliteSyncronisationrequiringTablesMaster.TableID Where SatelliteSyncronisationrequiringTablesMaster.ServerTableID='" + lbl_TableID.Text + "'");
            if (ds12where.Rows.Count > 0)
            {
                txt_addquery.Text = ds12where.Rows[0]["Select_Query"].ToString();
                //DataTable dty = MyCommonfile.selectBZ("select Id,feildname from tablefielddetail where tableId='" + ds12where.Rows[0]["TableID"] + "' Order by Id ");
                //if (dty.Rows.Count > 0)
                //{
                //    DDLLiceTablepkname.DataSource = dty;
                //    DDLLiceTablepkname.DataValueField = "Id";
                //    DDLLiceTablepkname.DataTextField = "feildname";
                //    DDLLiceTablepkname.DataBind();
                //    DDLLiceTablepkname.Items.Insert(0, "--Select--");
                //    DDLLiceTablepkname.Items[0].Value = "0";
                //    DDLLiceTablepkname.SelectedItem.Text = ds12where.Rows[0]["PKIdName"].ToString(); 
                //}    

                txt_fkname.Text = ds12where.Rows[0]["PKIdName"].ToString();
            }
        }
    }
    protected void DDLgvFKTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList lnkbtn = (DropDownList)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        //Label lblcandiID = (Label)GridView1.Rows[j].FindControl("lblcandiID");
        DropDownList DDLgvFKTableName = (DropDownList)GV_ServerTable.Rows[j].FindControl("DDLLiceTableName");//CheckBox6
        //DropDownList DDLgvFKFieldName = (DropDownList)GV_ServerTable.Rows[j].FindControl("DDLLiceTablepkname");
        //DataTable dty = MyCommonfile.selectBZ("select Id,feildname from tablefielddetail where tableId='" + DDLgvFKTableName.SelectedValue + "' Order by Id ");
        //if (dty.Rows.Count > 0)
        //{
        //    DDLgvFKFieldName.DataSource = dty;
        //    DDLgvFKFieldName.DataValueField = "Id";
        //    DDLgvFKFieldName.DataTextField = "feildname";
        //    DDLgvFKFieldName.DataBind();
        //    DDLgvFKFieldName.Items.Insert(0, "--Select--");
        //    DDLgvFKFieldName.Items[0].Value = "0";
        //}
        //else
        //{
        //}
    }  
    
    protected void GV_ServerTable_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GV_ServerTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_ServerTable.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GV_ServerTable_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void GV_ServerTable_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


  
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {         
        if (btn_submit.Text == "Update")
        {
            foreach (GridViewRow item in GV_ServerTable.Rows)
            {
                Label lbl_TableID = (Label)(item.FindControl("lbl_TableID"));
                Label lbl_LBDAtabaseName = (Label)(item.FindControl("lbl_LBDAtabaseName"));
                Label lnl_LBInstance = (Label)(item.FindControl("lnl_LBInstance"));
                Label lbl_Tablename = (Label)(item.FindControl("lbl_Tablename"));
                
                Label lbl_serverid = (Label)(item.FindControl("lbl_serverid"));
                CheckBox cbItem = (CheckBox)(item.FindControl("cbItem"));
                DropDownList DDLLiceTableName = (DropDownList)(item.FindControl("DDLLiceTableName"));

                //DropDownList DDLLiceTablepkname = (DropDownList)(item.FindControl("DDLLiceTablepkname"));


                TextBox txt_fkname = (TextBox)(item.FindControl("txt_fkname"));               
                TextBox txt_addquery = (TextBox)(item.FindControl("txt_addquery"));               
                
                string TbleName = DDLLiceTableName.SelectedItem.Text;
               
                           
                if (cbItem.Checked == true)
                {
                    //Create Temp Table
                   // Table_DatabaseSQL.Create_Temp_Table_Design_With_Change_Datatype(DDLLiceTableName.SelectedItem.Text);
                    string filepath = Server.MapPath("");
                    DataTable GetColum1 = MyCommonfile.selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + DDLLiceTableName.SelectedItem.Text + "'");
                    if (GetColum1.Rows.Count > 0)
                    {
                        string script1 = "";
                        string script2 = "";
                        string script3 = "";
                        Delete_TRIGGER(filepath, TbleName, GetColum1.Rows[0][0].ToString(), DDLLiceTableName.SelectedValue);
                        Insert_TRIGGER(filepath, TbleName, GetColum1.Rows[0][0].ToString(), DDLLiceTableName.SelectedValue);
                        Update_TRIGGER(filepath, TbleName, GetColum1.Rows[0][0].ToString(), DDLLiceTableName.SelectedValue);
                        script1 = File.ReadAllText(filepath + "\\sqlfile1.sql");
                        script2 = File.ReadAllText(filepath + "\\sqlfile2.sql");
                        script3 = File.ReadAllText(filepath + "\\sqlfile3.sql");
                        try
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            try
                            {
                                SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_DELETE]", con);
                                commandD1.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                            SqlCommand command1 = new SqlCommand(script1, con);
                            command1.ExecuteNonQuery();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            try
                            {
                                SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_INSERT]", con);
                                commandD1.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                            SqlCommand command2 = new SqlCommand(script2, con);
                            command2.ExecuteNonQuery();
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            try
                            {
                                SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_UPDATE]", con);
                                commandD1.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                            SqlCommand command3 = new SqlCommand(script3, con);
                            command3.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                           
                            lblmsg.Text = ex.ToString();
                        }
                    }

                    
                        SqlCommand cmd2 = new SqlCommand("SatelliteSyncronisationrequiringTablesMaster_AddDelUpdtSelect", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@StatementType", "DeleteSTBLID");
                        cmd2.Parameters.AddWithValue("@ServerTableID", lbl_TableID.Text);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    
   


                        SqlCommand cmd = new SqlCommand("SatelliteSyncronisationrequiringTablesMaster_AddDelUpdtSelect", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StatementType", "Insert");
                        cmd.Parameters.AddWithValue("@ProductVersionID", LBproductversion);
                        cmd.Parameters.AddWithValue("@TableId", DDLLiceTableName.SelectedValue);
                        cmd.Parameters.AddWithValue("@Name", DDLLiceTableName.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Status", cbItem.Checked);
                        cmd.Parameters.AddWithValue("@TableName", DDLLiceTableName.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@ServerTableID", lbl_TableID.Text);
                        cmd.Parameters.AddWithValue("@ServerDatabaseID", ddlcodetype.SelectedValue);
                        cmd.Parameters.AddWithValue("@ServerProductVersionID", ddlProductname.SelectedValue);
                        cmd.Parameters.AddWithValue("@ServerTableName", ddlProductname.SelectedValue);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                        SqlCommand cmd1 = new SqlCommand("SatelliteSyncronisationrequiringTablesMaster_SerWhere_AddDelUpdtSelect", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@StatementType", "Insert");
                        cmd1.Parameters.AddWithValue("@PKTableName", DDLLiceTableName.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@PKIdName", txt_fkname.Text);
                        cmd1.Parameters.AddWithValue("@Select_Query", txt_addquery.Text);
                        cmd1.Parameters.AddWithValue("@TableId", DDLLiceTableName.SelectedValue);                       
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close(); 
                  //  
                }
                else
                {
                    try
                    {
                        Table_DatabaseSQL.Delete_Temp_Table(lbl_Tablename.Text);
                    }
                    catch
                    {
                    }
                    SqlCommand cmd = new SqlCommand("SatelliteSyncronisationrequiringTablesMaster_AddDelUpdtSelect", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatementType", "DeleteSTBLID");
                    cmd.Parameters.AddWithValue("@ServerTableID", lbl_TableID.Text);                   
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd1 = new SqlCommand("SatelliteSyncronisationrequiringTablesMaster_SerWhere_AddDelUpdtSelect", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@StatementType", "DeleteTableId");
                    cmd1.Parameters.AddWithValue("@TableId", lbl_TableID.Text);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();  
                 




                        try
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_DELETE]", con);
                            commandD1.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {
                            con.Close();
                        }
                        try
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_INSERT]", con);
                            commandD1.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {
                            con.Close();
                        }
                        try
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand commandD1 = new SqlCommand("DROP TRIGGER [dbo].[" + TbleName + "_UPDATE]", con);
                            commandD1.ExecuteNonQuery();
                            con.Close();
                        }
                        catch
                        {
                            con.Close();
                        }                  
                }
            }
            lblmsg.Text = "Record Update Successfully";
            Cleare();
            FillDefault();

            btn_submit.Text = "Edit";
            pnlpr.Enabled = false;
        }
        else
        {
            btn_submit.Text = "Update";
            pnlpr.Enabled = true;
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
   
 

    protected void ch1_chachedChanged(object sender, EventArgs e)
    {        
        foreach (GridViewRow item in GV_ServerTable.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }


    protected void Delete_TRIGGER(string appcodepath,  string tablename, string pkId,string TableID)
    {
        string HashKey = "";

        string fileLoc = appcodepath + "\\sqlfile1.sql";

        using (StreamWriter sw = new StreamWriter(fileLoc))
            sw.Write
                (@" CREATE TRIGGER [dbo].[" + tablename + "_DELETE]" +
                     "      ON [dbo].["+tablename+"]"+
                    " AFTER DELETE"+
                    " AS"+
                    " BEGIN"+
                          " SET NOCOUNT ON;"+ 
                          " DECLARE @"+pkId+" INT"+
                          " SELECT @" + pkId + " = DELETED."+pkId+"" +
                          " FROM DELETED"+
                          " INSERT INTO Sync_Need_Logs" +
                          " VALUES(@" + pkId + ", 'Deleted','" + TableID + "','0')" +
                          " END ");
    }

    protected void Insert_TRIGGER(string appcodepath, string tablename, string pkId, string TableID)
    {

        string fileLoc = appcodepath + "\\sqlfile2.sql";
        using (StreamWriter sw = new StreamWriter(fileLoc))
            sw.Write
                (@" CREATE TRIGGER [dbo].[" + tablename + "_INSERT]" +
                     "      ON [dbo].[" + tablename + "]" +
                    " AFTER INSERT" +
                    " AS" +
                    " BEGIN" +
                          " SET NOCOUNT ON;" +
                          " DECLARE @" + pkId + " INT" +

                          " SELECT @" + pkId + " = INSERTED." + pkId + "" +
                          " FROM INSERTED" +

                          " INSERT INTO Sync_Need_Logs" +
                          " VALUES(@" + pkId + ", 'INSERT','" + TableID + "','0')" +
                    " END ");
     
    }

    protected void Update_TRIGGER(string appcodepath, string tablename, string pkId, string TableID)
    {

        string fileLoc = appcodepath + "\\sqlfile3.sql";
        using (StreamWriter sw = new StreamWriter(fileLoc))
            sw.Write
                (@" CREATE TRIGGER [dbo].[" + tablename + "_UPDATE]" +
                     "      ON [dbo].[" + tablename + "]" +
                    " AFTER UPDATE" +
                    " AS" +
                    " BEGIN" +
                          " SET NOCOUNT ON;" +
                          " DECLARE @" + pkId + " INT" +
                          " DECLARE @Action VARCHAR(50) "+

                          " SELECT @" + pkId + " = INSERTED." + pkId + "" +
                          " FROM INSERTED" +

                          "  SET @Action = 'Updated' "+

                          " INSERT INTO Sync_Need_Logs" +
                           " VALUES(@" + pkId + ", @Action,'" + TableID + "','0')" +
                    " END ");



    }
  
}

