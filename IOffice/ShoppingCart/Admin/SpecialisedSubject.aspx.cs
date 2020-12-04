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

public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
    DataSet ds;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    public int upfile = 0;
    public int rejfile = 0;
    string FilePath;
    string ConnectionString;

    OleDbConnection oledbConn;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
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
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            

            fillddl_Studies(ddlStudies);

            //fillwarehouse1();
            //test1();
            //ddlbusiness1_SelectedIndexChanged(sender, e);
            filteredu();
            fillgrid();
            Button11.Visible = false;

            ViewState["sortOrder"] = "";
        }
    }

    //protected void fillddl_business(DropDownList ddl)
    //{
    //    String s = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";
    //    da = new SqlDataAdapter(s, con);

    //    ds = new DataSet();
    //    da.Fill(ds);

    //    ddl.DataSource = ds;
    //    ddl.DataTextField = "Name";
    //    ddl.DataValueField = "WareHouseId";
    //    ddl.DataBind();
    //}

    protected void fillddl_Studies(DropDownList ddl)
    {
        String s = "Select * from AreaofStudiesTbl where [Active]='1' Order by Name";
        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);

        ddl.DataSource = ds;
        ddl.DataTextField = "Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();

        ddl.Items.Insert(0, "-Select-");
        ddl.SelectedItem.Value = "0";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlStudies.SelectedIndex = 0;
        txtSubjectName.Text = "";
        Panel4.Visible = false;
        btnaddnew.Visible = true;
        lbllegend.Text = "";
        btnSubmit.Text = "Submit";
        CheckBox1.Checked = false;
        lblmsg.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            String s = "select * from SpecialisedSubjectTBL where SubjectName = '" + txtSubjectName.Text + "'";
            da = new SqlDataAdapter(s, con);
            ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Subject Name already exist!";
            }
            else
            {
                String s1 = "insert into SpecialisedSubjectTBL values(" + ddlStudies.SelectedValue + ",'" + txtSubjectName.Text + "','" + CheckBox1.Checked + "','" + Session["Comid"].ToString() + "')";
                cmd = new SqlCommand(s1, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";

                ddlStudies.SelectedIndex = 0;
                txtSubjectName.Text = "";
                Panel4.Visible = false;
                btnaddnew.Visible = true;
                lbllegend.Text = "";
                CheckBox1.Checked = false;
            }
        }

        if (btnSubmit.Text == "Update")
        {
            string str1 = "SELECT * from SpecialisedSubjectTBL  where SubjectName ='" + txtSubjectName.Text + "' and Id !=" + ViewState["EID"] + "";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Subject Name already exist";
            }

            else
            {
                string sr51 = ("update SpecialisedSubjectTBL set AreaofStudiesId =" + ddlStudies.SelectedValue + ",Status='" + CheckBox1.Checked + "',SubjectName ='" + txtSubjectName.Text + "' where Id=" + ViewState["EID"] + " ");
                SqlCommand cmd801 = new SqlCommand(sr51, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd801.ExecuteNonQuery();
                con.Close();

                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";

                btnSubmit.Text = "Submit";

                ddlStudies.SelectedIndex = 0;
                txtSubjectName.Text = "";
                Panel4.Visible = false;
                btnaddnew.Visible = true;
                lbllegend.Text = "";
                CheckBox1.Checked = false;
            }
        }
    }

    protected void fillgrid()
    {
        Label1.Text = "All";

        lblstat.Text = "All";

        String s = "Select SpecialisedSubjectTBL.Id,AreaofStudiesTbl.Name as sname,SpecialisedSubjectTBL.SubjectName,case when(SpecialisedSubjectTBL.Status = '1') then 'Active' else 'Inactive' end as Status from SpecialisedSubjectTBL,AreaofStudiesTbl where AreaofStudiesTbl.ID = SpecialisedSubjectTBL.AreaofStudiesId  and SpecialisedSubjectTBL.compid = '" + Session["Comid"].ToString() + "' ";

        //if (ddlbusiness1.SelectedIndex > -1)
        //{
        //    Label1.Text = ddlbusiness1.SelectedItem.Text;

        //    s += " and s.BusinessId='" + ddlbusiness1.SelectedValue + "'";
        //}
        if (ddlareaaaa.SelectedIndex > 0)
        {
            Label1.Text = ddlareaaaa.SelectedItem.Text;

            s += " and SpecialisedSubjectTBL.AreaofStudiesId='" + ddlareaaaa.SelectedValue + "'";
        }
        if (ddlstatus_search.SelectedIndex > 0)
        {
            lblstat.Text = ddlstatus_search.SelectedItem.Text;

            s += " and SpecialisedSubjectTBL.Status='" + ddlstatus_search.SelectedValue + "'";
        }
        s += " order by AreaofStudiesTbl.Name,SpecialisedSubjectTBL.SubjectName asc";

        da = new SqlDataAdapter(s, con);
        ds = new DataSet();
        da.Fill(ds);
        dt = ds.Tables[0];
        GridView1.DataSource = dt;

        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            String s = "delete from SpecialisedSubjectTBL where Id = " + i;
            SqlCommand cmd = new SqlCommand(s, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";

            fillgrid();
        }

        if (e.CommandName == "Edit")
        {
            lbllegend.Text = "Edit Subject";

            Panel4.Visible = true;
            btnaddnew.Visible = false;
            btnSubmit.Text = "Update";
            lblmsg.Text = "";

            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["EID"] = i;

            string str = "select * from SpecialisedSubjectTBL where Id = '" + i + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            //fillddl_business(ddlbusiness);

            //string ee = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            //SqlCommand cmdeeed = new SqlCommand(ee, con);
            //SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            //DataTable dteeed = new DataTable();
            //adpeeed.Fill(dteeed);

            //ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dt.Rows[0]["BusinessId"].ToString()));

            fillddl_Studies(ddlStudies);

            ddlStudies.SelectedIndex = ddlStudies.Items.IndexOf(ddlStudies.Items.FindByValue(dt.Rows[0]["AreaofStudiesId"].ToString()));

            txtSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();

            CheckBox1.Checked = Convert.ToBoolean(dt.Rows[0]["Status"].ToString());

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //try
        //{

        //    int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        //    TextBox txtsub1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtsub");

        //    DropDownList ddlBusiness11 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlBusiness1");

        //    DropDownList ddlstudy1 = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlstudy");




        //    //CLS_SpecialisedSubject ss = new CLS_SpecialisedSubject();
        //    //ss.Studyid = ddlstudy1.SelectedValue.ToString();
        //    //ss.SubjectName = txtsub1.Text;
        //    //DataTable dt1 = ss.cls_SpecialisedSubject5();


        //}
        //catch (Exception ert)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Error :" + ert.Message;

        //}
    }
    //protected void ddlBusiness1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillddl_Studies(ddlStudies);
    //}

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
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[3].Visible = false;
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
                GridView1.Columns[3].Visible = true;
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
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Panel4.Visible = true;
        lblmsg.Visible = false;
        btnaddnew.Visible = false;
        lbllegend.Text = "Add New Subject";
    }
    //protected void ddlbusiness1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}
    protected void ddlareaaaa_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    //protected void fillwarehouse1()
    //{
    //    string str1 = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["Comid"].ToString() + "' and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

    //    DataTable ds1 = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter(str1, con);
    //    da.Fill(ds1);
    //    if (ds1.Rows.Count > 0)
    //    {
    //        ddlbusiness1.DataSource = ds1;
    //        ddlbusiness1.DataTextField = "Name";
    //        ddlbusiness1.DataValueField = "WarehouseId";
    //        ddlbusiness1.DataBind();
    //    }
    //}
    //protected void test1()
    //{
    //    string ee = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
    //    SqlCommand cmdeeed = new SqlCommand(ee, con);
    //    SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
    //    DataTable dteeed = new DataTable();
    //    adpeeed.Fill(dteeed);
    //    if (dteeed.Rows.Count > 0)
    //    {
    //        ddlbusiness1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
    //    }
    //}

    protected void filteredu()
    {
        cmd = new SqlCommand("select ID,Name from AreaofStudiesTbl where Active='1' order by Name", con);
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
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
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
        ddlSheets.Items.Insert(0,"-Select-");
        ddlSheets.Items[0].Value = "0";
        connExcel.Close();
        oledbConn.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "File Uploaded Successfully.";
        pnltransfer.Visible = true;
        //}
        //else
        //{
        //    ddlSheets.Items.Clear();
        //    ddlSheets.Items.Insert(0, "-Select-");
        //    ddlSheets.Items[0].Value = "0";
        //    lblmessage.Text = "This FileName is already used.";
       
    }

    protected void bnt123_Click(object sender, EventArgs e)
    {
        lblmsg.Visible = true;
        lblmsg.Text = "";
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

                string SubjectName = ds.Tables[0].Rows[i][0].ToString().Trim();

                //string phonecode = ds.Tables[0].Rows[i][1].ToString().Trim();
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                if (SubjectName == null)
                {
                    //rejfile += 1;
                }
                if (SubjectName == "")
                {
                    //rejfile += 1;
                }
                else
                {
                    int kk = 0;
                    string str3 = "select * from [SpecialisedSubjectTBL] where [SubjectName]='" + SubjectName + "'";
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
                        lblmsg.Visible = true;

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
                            Drow["SubjectName"] = SubjectName;
                            dt56.Rows.Add(Drow);
                            ViewState["data"] = dt56;
                        }
                    }
                    else
                    {
                        SqlCommand cmd121 = new SqlCommand("Insert Into SpecialisedSubjectTBL([AreaofStudiesId],[SubjectName],[Status],[compid])Values('" + ddlStudies.SelectedValue + "','" + SubjectName + "','" + CheckBox1.Checked + "','" + Session["comid"].ToString() + "')", con);
                        cmd121.ExecuteNonQuery();
                        lblmsg.Text = "";
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record inserted Successfully.";
                        con.Close();
                        upfile += 1;
                        lblmsg.Visible = true;
                        lblmsg.Text = "Data Uploaded Successfully.";
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
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + ex.Message;
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
        Dcom1.ColumnName = "SubjectName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "Status";
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
        Button11.Visible = true;
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        btnaddnew.Visible = true;
        lbllegend.Text = "";
        Panel4.Visible = false;
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


