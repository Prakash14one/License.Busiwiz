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
using System.Data.OleDb;
using System.Text;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
public partial class ShoppingCart_Admin_Default4 : System.Web.UI.Page
{


    SqlConnection con;
    public int upfile = 0;
    public int rejfile = 0;
    string FilePath;
    string ConnectionString;

    OleDbConnection oledbConn;

    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;



    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        string compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (ViewState["up"] != null)
        {
            upfile = Convert.ToInt32(ViewState["up"]);
        }
        if (ViewState["rej"] != null)
        {
            rejfile = Convert.ToInt32(ViewState["rej"]);
        }
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            Button11.Visible = false;
            ViewState["sortOrder"] = "";

            fillstudy();
            filleducation();
            filteredu();
            fillgrid();
        }
    }


    protected void fillstudy()
    {
        SqlCommand cmd = new SqlCommand("select ID,Name from AreaofStudiesTbl where Active='1' order by Name", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);

        ddlareaofstudy.DataSource = dt1;
        ddlareaofstudy.DataTextField = "Name";
        ddlareaofstudy.DataValueField = "ID";
        ddlareaofstudy.DataBind();

        ddlareaofstudy.Items.Insert(0, "-Select-");
        ddlareaofstudy.SelectedItem.Value = "0";
    }

    protected void filleducation()
    {
        SqlCommand cmd = new SqlCommand("select Id,Name from LevelofEducationTBl where AreaofStudyID='" + ddlareaofstudy.SelectedValue + "' Order by Name", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);

        ddllevelofedu.DataSource = dt2;
        ddllevelofedu.DataTextField = "Name";
        ddllevelofedu.DataValueField = "Id";
        ddllevelofedu.DataBind();

        ddllevelofedu.Items.Insert(0, "-Select-");
        ddllevelofedu.SelectedItem.Value = "0";
    }

    protected void fillgrid()
    {
        Label4.Text = "All";

        lblstat.Text = "All";

        string str = "select dbo.EducationDegrees.ID,dbo.AreaofStudiesTbl.Name as AreaofStudy,dbo.EducationDegrees.DegreeName,dbo.LevelofEducationTBL.Name as LevelofEducation,case when(EducationDegrees.Active = '1') then 'Active' else 'Inactive' end as Status from dbo.EducationDegrees INNER JOIN dbo.AreaofStudiesTbl ON dbo.EducationDegrees.AreaofStudyID=dbo.AreaofStudiesTbl.ID INNER JOIN dbo.LevelofEducationTBL ON dbo.EducationDegrees.LevelofEducationTblID=dbo.LevelofEducationTBL.Id where LevelofEducationTBl.Active='1'";

        if (ddlareaaaa.SelectedIndex > 0)
        {
            Label4.Text = ddlareaaaa.SelectedItem.Text;

            str += " and EducationDegrees.AreaofStudyID='" + ddlareaaaa.SelectedValue + "'";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;

            str += " and EducationDegrees.Active='" + ddlstatus_search.SelectedValue + "'";
        }

        str += " order by AreaofStudiesTbl.Name,LevelofEducationTBL.Name,EducationDegrees.DegreeName asc";
       
        da = new SqlDataAdapter(str, con);
        DataTable dt3 = new DataTable();
        da.Fill(dt3);

        DataView myDataView = new DataView();
        myDataView = dt3.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("insert into EducationDegrees(AreaofStudyID,DegreeName,LevelofEducationTblID,Active) values('" + ddlareaofstudy.SelectedValue + "' , '" + txtdegreename.Text + "' , '" + ddllevelofedu.SelectedValue + "' , '" + chkstatus.Checked + "')", con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        lblmessage.Text = "Record inserted successfully.";
        lblmessage.Visible = true;
        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }

    protected void clear()
    {
        ddlareaofstudy.SelectedIndex = 0;
        txtdegreename.Text = "";
        ddllevelofedu.SelectedIndex = 0;
        chkstatus.Checked = false;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clseducationdegrees objeducation = new clseducationdegrees();

        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Education Degree";

            Button1.Visible = false;
            Panel1.Visible = true;
            int i = Convert.ToInt32(e.CommandArgument);
            Label10.Text = i.ToString();
            lblmessage.Text = "";

            objeducation.ID = i;

            // da = new SqlDataAdapter("select * from EducationDegrees where ID=" +i,con);
            dt = new DataTable();
            dt = objeducation.educa_se();
            //  da.Fill(dt);

            fillstudy();
            ddlareaofstudy.SelectedIndex = ddlareaofstudy.Items.IndexOf(ddlareaofstudy.Items.FindByValue(dt.Rows[0]["AreaofStudyID"].ToString()));

            txtdegreename.Text = dt.Rows[0]["DegreeName"].ToString();

            filleducation();
            ddllevelofedu.SelectedIndex = ddllevelofedu.Items.IndexOf(ddllevelofedu.Items.FindByValue(dt.Rows[0]["LevelofEducationTblID"].ToString()));

            chkstatus.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);

            ddlareaofstudy.Focus();
        }

        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            cmd = new SqlCommand("delete from EducationDegrees where ID=" + i, con);
            if (cmd.Connection.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully.";
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("update EducationDegrees set AreaofStudyID='" + ddlareaofstudy.SelectedValue + "',DegreeName='" + txtdegreename.Text + "',LevelofEducationTblID='" + ddllevelofedu.SelectedValue + "',Active='" + chkstatus.Checked + "' where ID='" + Label10.Text + "'", con);
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        lblmessage.Text = "Record updated successfully.";
        lblmessage.Visible = true;
        btnupdate.Visible = false;
        btnsubmit.Visible = true;

        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Visible == true)
        {
            clear();
        }
        if (btnupdate.Visible == true)
        {

            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            clear();

        }
        lblmessage.Visible = false;
        Panel1.Visible = false;
        Button1.Visible = true;
        lbllegend.Text = "";
    }


    protected void btnPrintVersion_Click(object sender, EventArgs e)
    {
        if (btnPrintVersion.Text == "Printable Version")
        {
            btnPrintVersion.Text = "Hide Printable Version";
            btnPrint.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {
            btnPrintVersion.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button1.Visible = false;
        lblmessage.Visible = false;
        lbllegend.Text = "Add New Education Degree";

        fillstudy();
        ddlareaofstudy_SelectedIndexChanged(sender, e);
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "LevelofEducationtbl.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        filleducation();
    }
    protected void ddlareaofstudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        filleducation();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlareaaaa_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void filteredu()
    {
        cmd = new SqlCommand("select ID,Name from AreaofStudiesTbl where Active='1' Order by Name", con);
        da = new SqlDataAdapter(cmd);
        DataTable dt2 = new DataTable();
        da.Fill(dt2);

        ddlareaaaa.DataSource = dt2;
        ddlareaaaa.DataTextField = "Name";
        ddlareaaaa.DataValueField = "ID";
        ddlareaaaa.DataBind();

        ddlareaaaa.Items.Insert(0, "All");
        ddlareaaaa.SelectedItem.Value = "0";
    }
    protected void btnfileupload_Click(object sender, EventArgs e)
    {
        // lblmessage.Visible = false;
        //SqlCommand cmd = new SqlCommand("SELECT * FROM FileNameTbl where Filename='" + File12.FileName + "'", con);
        //SqlDataAdapter oleda = new SqlDataAdapter();
        //oleda.SelectCommand = cmd;
        ViewState["fl"] = File12.FileName;
        //DataTable ds1 = new DataTable();
        //oleda.Fill(ds1);
        //if (ds1.Rows.Count == 0)
        //{
        if (File12.HasFile)
        {
            upfile = 0;
            rejfile = 0;
            ViewState["up"] = null;
            ViewState["rej"] = null;
            string FileName = Path.GetFileName(File12.PostedFile.FileName);
            string Extension = Path.GetExtension(File12.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            FilePath = Server.MapPath(FolderPath + "ExelFile\\" + FileName);
            File12.SaveAs(FilePath);

            //SqlCommand cmd1 = new SqlCommand("Insert into FileNameTbl(Filename,Uploaddate)values('" + File12.FileName + "','" + DateTime.Now.ToShortDateString() + "')", con);
            //con.Open();
            //cmd1.ExecuteNonQuery();
            //con.Close();
            //  fillgrid();
        }

        string FolderPath12 = ConfigurationManager.AppSettings["FolderPath"];
        string FilePath12 = Server.MapPath(FolderPath12 + "ExelFile\\" + File12.FileName);
        string SourceFilePath = FilePath12;
        Session["storesourcefilepath"] = SourceFilePath;
        //  ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SourceFilePath + ";Extended Properties=Excel 8.0;";
        //  String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=pricelist.xlsx;Extended Properties=""Excel 12.0 Xml;HDR=YES""");
        ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SourceFilePath + ";Extended Properties=Excel 12.0 Xml;";
        oledbConn = new OleDbConnection(ConnectionString);
        if (oledbConn.State.ToString() != "Open")
        {
            oledbConn.Open();
        }
        //Get the Sheets in Excel WorkBoo
        ConnectionString = String.Format(ConnectionString, FilePath);
        OleDbConnection connExcel = new OleDbConnection(ConnectionString);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        //Bind the Sheets to DropDownList
        ddlSheets.Items.Clear();
        ddlSheets.Items.Add(new ListItem("--Select Sheet--", ""));
        ddlSheets.DataSource = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        ddlSheets.DataTextField = "TABLE_NAME";
        ddlSheets.DataValueField = "TABLE_NAME";
        ddlSheets.DataBind();
        ddlSheets.Items.Insert(0, "-Select-");
        ddlSheets.Items[0].Value = "0";
        connExcel.Close();
        oledbConn.Close();
        lblmessage.Visible = true;
        lblmessage.Text = "File Uploaded Successfully.";
        pnltransfer.Visible = true;
        //}
        //else
        //{
        //    ddlSheets.Items.Clear();
        //    ddlSheets.Items.Insert(0, "-Select-");
        //    ddlSheets.Items[0].Value = "0";
        //    lblmessage.Text = "This FileName is already used.";
        //}
    }

    protected void bnt123_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = true;
        lblmessage.Text = "";
        string sheetname = ddlSheets.SelectedItem.ToString();
        try
        {
            // oledbConn.Close();
            string finalpath12 = Session["storesourcefilepath"].ToString();
            //ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + finalpath12 + ";Extended Properties=Excel 8.0;";
            ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + finalpath12 + ";Extended Properties=Excel 12.0;";
            oledbConn = new OleDbConnection(ConnectionString);
            //   oledbConn.Open();
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetname + "]", oledbConn);
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            oleda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            DataTable ds1 = new DataTable();
            oleda.Fill(ds);
            DataTable dt56 = new DataTable();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string DegreeName = ds.Tables[0].Rows[i][0].ToString().Trim();
               
                //string phonecode = ds.Tables[0].Rows[i][1].ToString().Trim();
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                if (DegreeName == null)
                {
                    //rejfile += 1;
                }
                if (DegreeName == "")
                {
                    //rejfile += 1;
                }
                else
                {
                    int kk = 0;
                    string str3 = "select * from [EducationDegrees] where DegreeName='" + DegreeName + "'";
                    SqlCommand cmdy3 = new SqlCommand(str3, con);
                    SqlDataAdapter oleday3 = new SqlDataAdapter();
                    oleday3.SelectCommand = cmdy3;
                    DataTable ds1y3 = new DataTable();
                    oleday3.Fill(ds1y3);


                    if (ds1y3.Rows.Count > 0)
                    {
                        kk += 1;
                    }
                    if (ds1y3.Rows.Count > 0)
                    {
                        lblmessage.Visible = true;

                        //lblmessage.Text = "Data Uploaded Successfully.";
                        //rejfile += 1;
                        if (kk > 0)
                        {
                            rejfile += 1;

                            if (Convert.ToString(ViewState["data"]) == "" && dt56.Rows.Count == 0)
                            {
                                dt56 = dydata();
                            }
                            else
                            {
                                dt56 = (DataTable)ViewState["data"];
                            }
                            DataRow Drow = dt56.NewRow();
                            Drow["No"] = (i + 1).ToString();
                            Drow["DegreeName"] = DegreeName;
                            dt56.Rows.Add(Drow);
                            ViewState["data"] = dt56;
                        }
                    }
                    else
                    {
                        SqlCommand cmd121 = new SqlCommand("Insert Into EducationDegrees(DegreeName,AreaofStudyID,LevelofEducationTblID,Active)Values('" + DegreeName + "','" + ddlareaofstudy.SelectedValue + "','" + ddllevelofedu.SelectedValue + "','"+chkstatus.Checked+"')", con);
                        cmd121.ExecuteNonQuery();
                        lblmessage.Text = "";
                        lblmessage.Visible = true;
                        lblmessage.Text = "Record inserted Successfully.";
                        con.Close();
                        upfile += 1;
                        lblmessage.Visible = true;
                        lblmessage.Text = "Data Uploaded Successfully.";
                    }

                }
                

            }

           

            lblsucmsg.Text = upfile.ToString() + " file and sheet name " + ddlSheets.SelectedItem.Text;
            ViewState["up"] = upfile.ToString();
            ViewState["rej"] = rejfile.ToString();
            lblnoofrecord.Text = upfile.ToString();
            lblnotimport.Text = rejfile.ToString();
            pnlsucedata.Visible = true;

            if (rejfile > 0)
            {
                Button22.Visible = true;

            }
            else
            {

                Button22.Visible = false;
            }
            fillgrid();
        }

        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Error : " + ex.Message;
        }
        finally
        {
            oledbConn.Close();
        }

        ViewState["up"] = 0;
        ViewState["rej"] = 0;
        Button11.Visible = true;
    }
    protected DataTable dydata()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "No";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "DegreeName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "Active";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);


        return dt;
    }
    protected void grderrorlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grderrorlist.PageIndex = e.NewPageIndex;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
    }
    protected void Button22_Click(object sender, EventArgs e)
    {
        pnlgrd.Visible = true;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
        //pnldis.Visible = false;
        
        Button11.Visible = true;
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        Button1.Visible = true;
        lbllegend.Text = "";
        Panel1.Visible = false;
        pnltransfer.Visible = false;
        ViewState["data"] = "";
        grderrorlist.DataSource = null;
        grderrorlist.DataBind();
        pnlsucedata.Visible = false;
        pnlgrd.Visible = false;
        Button11.Visible = false;
        pnlsucedata.Visible = false;
    }
}

