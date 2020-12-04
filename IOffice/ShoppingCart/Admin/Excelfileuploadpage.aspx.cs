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

public partial class Admin_Excelfileuploadpage : System.Web.UI.Page
{
    Int32 TyId;
    Int32 maxdetailid12;
    Int32 maxinvenid;
    string FilePath;
    string ConnectionString;
    SqlConnection cn;
    OleDbConnection oledbConn;
    public int upfile = 0;
    public int rejfile = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        cn = pgcon.dynconn;

        //cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        if (ViewState["up"] != null)
        {
            upfile = Convert.ToInt32(ViewState["up"]);
        }
        if (ViewState["rej"] != null)
        {
            rejfile = Convert.ToInt32(ViewState["rej"]);
        }
        Label1.Text = "";

        if (!IsPostBack)
        {
            filladdresstype();
            ddlAddType.SelectedIndex = ddlAddType.Items.IndexOf(ddlAddType.Items.FindByText("Business Address"));
            fillddlCat();

            ddlSheets.Items.Insert(0, "-Select-");
            ddlSheets.Items[0].Value = "0";

            //  fillgrid();
        }
    }

    protected void fillddlCat()
    {
        ddlCat.Items.Clear();

        string strc = "select BusinessSubSubCat.B_SubSubCatID,BusinessCategory.B_Category +' : '+ BusinessSubCat.B_SubCategory +' : '+ BusinessSubSubCat.B_SubSubCategory as categoryname from BusinessCategory inner join BusinessSubCat on BusinessSubCat.B_CatID=BusinessCategory.B_CatID inner join BusinessSubSubCat on BusinessSubSubCat.B_SubCatID=BusinessSubCat.B_SubCatID where BusinessSubSubCat.Active='1'";
        SqlCommand cmdddlc = new SqlCommand(strc, cn);
        SqlDataAdapter dac = new SqlDataAdapter(cmdddlc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            ddlCat.DataSource = dtc;
            ddlCat.DataTextField = "categoryname";
            ddlCat.DataValueField = "B_SubSubCatID";
            ddlCat.DataBind();
        }
        ddlCat.Items.Insert(0, "-Select-");
        ddlCat.Items[0].Value = "0";
    }

    public void filladdresstype()
    {
        string sg90 = "select * from AddressType order by AddressType";
        SqlCommand cmd23490 = new SqlCommand(sg90, cn);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);

        if (dt23490.Rows.Count > 0)
        {
            ddlAddType.DataSource = dt23490;
            ddlAddType.DataTextField = "AddressType";
            ddlAddType.DataValueField = "AddressTypeID";
            ddlAddType.DataBind();
        }
        ddlAddType.Items.Insert(0, "-Select-");
        ddlAddType.Items[0].Value = "0";
    }

    protected void btnfileupload_Click(object sender, EventArgs e)
    {
        // Label1.Visible = false;

        //SqlCommand cmd = new SqlCommand("SELECT * FROM FileNameTbl where Filename='" + File12.FileName + "'", cn);
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

            //SqlCommand cmd1 = new SqlCommand("Insert into FileNameTbl(Filename,Uploaddate)values('" + File12.FileName + "','" + DateTime.Now.ToShortDateString() + "')", cn);
            //cn.Open();
            //cmd1.ExecuteNonQuery();
            //cn.Close();

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
        //****************

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

        Label1.Text = "File Uploaded Successfully.";
        //}
        //else
        //{
        //    ddlSheets.Items.Clear();
        //    ddlSheets.Items.Insert(0, "-Select-");
        //    ddlSheets.Items[0].Value = "0";

        //    Label1.Text = "This FileName is already used.";
        //}
    }

    //public void fillgrid()
    //{
    //    SqlCommand cmd = new SqlCommand("SELECT * FROM FileNameTbl order by Id desc", cn);
    //    SqlDataAdapter oleda = new SqlDataAdapter();
    //    oleda.SelectCommand = cmd;

    //    DataTable ds1 = new DataTable();
    //    oleda.Fill(ds1);

    //    if (ds1.Rows.Count > 0)
    //    {
    //        GVC.DataSource = ds1;
    //        GVC.DataBind();
    //    }
    //    else
    //    {
    //        GVC.DataSource = null;
    //        GVC.DataBind();
    //    }
    //}


    protected void bnt123_Click(object sender, EventArgs e)
    {
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

                string BusinessName = ds.Tables[0].Rows[i][0].ToString().Trim();
                string address = ds.Tables[0].Rows[i][1].ToString().Trim();
                string city = ds.Tables[0].Rows[i][2].ToString().Trim();
                string state = ds.Tables[0].Rows[i][3].ToString().Trim();
                string country = ds.Tables[0].Rows[i][4].ToString().Trim();
                string zip = ds.Tables[0].Rows[i][5].ToString().Trim();
                string phone1 = ds.Tables[0].Rows[i][6].ToString().Trim();
                string email = ds.Tables[0].Rows[i][7].ToString().Trim();

                if (BusinessName == "" && address == "" && country == "" && state == "" && city == "" && phone1 == "" && zip == "" && email == "")
                {
                    //rejfile += 1;
                }
                else
                {
                    int kk = 0;

                    //string str = "select * from BusinessTempMaster where ((BusinessName='" + BusinessName + "' and email='" + email + "') OR (BusinessName='" + BusinessName + "' and phone='" + phone1 + "') OR (BusinessName='" + BusinessName + "' and zipcode='" + zip + "'))";
                    SqlCommand cmdy = new SqlCommand("selectBusinessTempMaster", cn);

                    cmdy.CommandType = CommandType.StoredProcedure;
                    cmdy.CommandText = "selectBusinessTempMaster";

                    cmdy.Parameters.AddWithValue("@BusinessName", BusinessName);
                    cmdy.Parameters.AddWithValue("@email", email);
                    cmdy.Parameters.AddWithValue("@phone", phone1);
                    cmdy.Parameters.AddWithValue("@zipcode", zip);

                    SqlDataAdapter oleday = new SqlDataAdapter();
                    oleday.SelectCommand = cmdy;
                    DataTable ds1y = new DataTable();
                    oleday.Fill(ds1y);

                    if (ds1y.Rows.Count > 0)
                    {
                        kk += 1;
                    }

                    else
                    {
                        if (country == "U.S.A." || country == "U S A" || country == "USA" || country == "United States of America" || country == "United States" || country == "U.S." || country == "US" || country == "u.s.a." || country == "u s a" || country == "usa" || country == "united states of america" || country == "united states" || country == "u.s." || country == "us" || country == "UNITED STATES OF AMERICA")
                        {
                            country = "USA";
                        }
                        if (country == "U.A.E." || country == "U A E" || country == "UAE" || country == "United Arab Emirates" || country == "u.a.e." || country == "u a e" || country == "uae" || country == "united arab emirates" || country == "UNITED ARAB EMIRATES")
                        {
                            country = "UNITED ARAB EMIRATES";
                        }
                        if (country == "U.K." || country == "U K" || country == "UK" || country == "United Kingdom" || country == "u.k." || country == "u k" || country == "uk" || country == "united kingdom" || country == "UNITED KINGDOM")
                        {
                            country = "UNITED KINGDOM";
                        }


                        SqlCommand cmda2 = new SqlCommand("selectcountry", cn);
                        cmda2.CommandType = CommandType.StoredProcedure;
                        cmda2.CommandText = "selectcountry";
                        cmda2.Parameters.AddWithValue("@Countryname", country);
                        SqlDataAdapter da2 = new SqlDataAdapter();
                        da2.SelectCommand = cmda2;
                        //SqlDataAdapter da2 = new SqlDataAdapter("select CountryId from CountryMaster where CountryName='" + country + "'", cn);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);



                        SqlCommand cmda3 = new SqlCommand("selectstate", cn);
                        cmda3.CommandType = CommandType.StoredProcedure;
                        cmda3.CommandText = "selectstate";
                        cmda3.Parameters.AddWithValue("@statename", state);
                        SqlDataAdapter da3 = new SqlDataAdapter();
                        da3.SelectCommand = cmda3;
                        //SqlDataAdapter da3 = new SqlDataAdapter("select StateId from StateMasterTbl where StateName='" + state + "'", cn);
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);



                        SqlCommand cmda4 = new SqlCommand("selectcity", cn);
                        cmda4.CommandType = CommandType.StoredProcedure;
                        cmda4.CommandText = "selectcity";
                        cmda4.Parameters.AddWithValue("@cityname", city);
                        SqlDataAdapter da4 = new SqlDataAdapter();
                        da4.SelectCommand = cmda4;
                        //SqlDataAdapter da4 = new SqlDataAdapter("select CityId from CityMasterTbl where CityName='" + city + "'", cn);
                        DataTable dt4 = new DataTable();
                        da4.Fill(dt4);


                        if (dt2.Rows.Count == 0 || dt3.Rows.Count == 0 || dt4.Rows.Count == 0)
                        {
                            kk += 1;
                        }
                        else
                        {

                        }
                        if (BusinessName.Length > 100 && address.Length > 500 && phone1.Length > 50 && zip.Length > 50 && email.Length > 50)
                        {
                            kk += 1;
                        }
                        else
                        {
                            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                            Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);

                            if (match.Success)
                            {

                            }
                            else
                            {
                                kk += 1;
                            }


                            string pattern1 = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$*";

                            Match match1 = Regex.Match(phone1.Trim(), pattern1, RegexOptions.IgnoreCase);

                            if (match1.Success)
                            {

                            }
                            else
                            {
                                kk += 1;
                            }


                            string pattern2 = @"^[a-zA-Z0-9\s]+$";
                            //@"^(\d{5}-\d{4}|\d{5}|\d{9})$|^([azA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$*";

                            Match match2 = Regex.Match(zip.Trim(), pattern2, RegexOptions.IgnoreCase);

                            if (match2.Success)
                            {

                            }
                            else
                            {
                                kk += 1;
                            }


                            string pattern3 = @"^[)('_&+-.@a-zA-Z0-9\s]+$";

                            Match match3 = Regex.Match(BusinessName.Trim(), pattern3, RegexOptions.IgnoreCase);

                            if (match3.Success)
                            {

                            }
                            else
                            {
                                kk += 1;
                            }

                            string pattern4 = @"^[)(#'&._a-z,-/A-Z0-9\s]+$";

                            Match match4 = Regex.Match(address.Trim(), pattern4, RegexOptions.IgnoreCase);

                            if (match4.Success)
                            {

                            }
                            else
                            {
                                kk += 1;
                            }
                        }
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
                            Drow["BusinessName"] = BusinessName;
                            Drow["address1"] = address;
                            Drow["city"] = city;
                            Drow["state"] = state;
                            Drow["country"] = country;
                            Drow["zip"] = zip;
                            Drow["phone1"] = phone1;
                            Drow["email"] = email;

                            dt56.Rows.Add(Drow);

                            ViewState["data"] = dt56;
                        }
                        else
                        {

                            SqlCommand cmd133 = new SqlCommand("insertBusinessTempMaster", cn);

                            if (cn.State.ToString() != "Open")
                            {
                                cn.Open();
                            }
                            cmd133.CommandType = CommandType.StoredProcedure;
                            cmd133.CommandText = "insertBusinessTempMaster";

                            cmd133.Parameters.AddWithValue("@BusinessName", BusinessName.ToUpperInvariant());
                            cmd133.Parameters.AddWithValue("@subcatID", ddlCat.SelectedValue);
                            cmd133.Parameters.AddWithValue("@addressID", ddlAddType.SelectedValue);
                            cmd133.Parameters.AddWithValue("@contactname", "");
                            cmd133.Parameters.AddWithValue("@designation", "");
                            cmd133.Parameters.AddWithValue("@address", address);
                            cmd133.Parameters.AddWithValue("@country", dt2.Rows[0]["CountryId"].ToString());
                            cmd133.Parameters.AddWithValue("@state", dt3.Rows[0]["StateId"].ToString());
                            cmd133.Parameters.AddWithValue("@city", dt4.Rows[0]["CityId"].ToString());
                            cmd133.Parameters.AddWithValue("@phone", phone1);
                            cmd133.Parameters.AddWithValue("@Mobileno", "");
                            cmd133.Parameters.AddWithValue("@zipcode", zip);
                            cmd133.Parameters.AddWithValue("@email", email);
                            cmd133.Parameters.AddWithValue("@fax", "");
                            cmd133.Parameters.AddWithValue("@status", 1);
                            cmd133.Parameters.AddWithValue("@datetime", System.DateTime.Now.ToString());
                            cmd133.Parameters.AddWithValue("@website", "");
                            cmd133.Parameters.AddWithValue("@linkedin", "");
                            cmd133.Parameters.AddWithValue("@facebook", "");
                            cmd133.Parameters.AddWithValue("@yahoo", "");
                            cmd133.Parameters.AddWithValue("@twitter", "");
                            cmd133.Parameters.AddWithValue("@hotmail", "");

                            //str = "insert into BusinessTempMaster([BusinessName],[subcatID],[addressID],[contactname],[designation],[address],[country],[state],[city],[phone],[Mobileno],[zipcode],[email],[fax],[status],[datetime]) values('" + BusinessName.ToUpperInvariant() + "','" + ddlCat.SelectedValue + "','" + ddlAddType.SelectedValue + "','','','" + address + "','" + dt2.Rows[0]["CountryId"].ToString() + "','" + dt3.Rows[0]["StateId"].ToString() + "','" + dt4.Rows[0]["CityId"].ToString() + "','" + phone1 + "','','" + zip + "','" + email + "','','1','" + System.DateTime.Now.ToString() + "')";

                            //SqlCommand cmd133 = new SqlCommand(str, cn);
                            //if (cn.State.ToString() != "Open")
                            //{
                            //    cn.Open();
                            //}
                            cmd133.ExecuteNonQuery();
                            cn.Close();

                            SqlDataAdapter da = new SqlDataAdapter("select max(ID) as ID from BusinessTempMaster", cn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {

                                SqlCommand cmd1 = new SqlCommand("insertBusinessTempDetail", cn);

                                if (cn.State.ToString() != "Open")
                                {
                                    cn.Open();
                                }
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.CommandText = "insertBusinessTempDetail";

                                cmd1.Parameters.AddWithValue("@businessid", Convert.ToString(dt.Rows[0]["ID"]));
                                cmd1.Parameters.AddWithValue("@product", "");
                                cmd1.Parameters.AddWithValue("@description", "");

                                //SqlCommand cmd1 = new SqlCommand("insert into BusinessTempDetail values('" + Convert.ToString(dt.Rows[0]["ID"]) + "','','')", cn);
                                //if (cn.State.ToString() != "Open")
                                //{
                                //    cn.Open();
                                //}
                                cmd1.ExecuteNonQuery();
                                cn.Close();

                                string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                string publicip = Request.UserHostAddress;
                                string computeripadd = System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0].ToString();
                                string browser = Request.Browser.Browser.ToString();
                                string computerusername = HttpContext.Current.User.Identity.Name.ToString();
                                string computername = System.Environment.MachineName;


                                SqlCommand cmd122 = new SqlCommand("insertuserlog", cn);

                                if (cn.State.ToString() != "Open")
                                {
                                    cn.Open();
                                }
                                cmd122.CommandType = CommandType.StoredProcedure;
                                cmd122.CommandText = "insertuserlog";

                                cmd122.Parameters.AddWithValue("@businessid", Convert.ToString(dt.Rows[0]["ID"]));
                                cmd122.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                                cmd122.Parameters.AddWithValue("@employeeid", Session["EmployeeId"].ToString());
                                cmd122.Parameters.AddWithValue("@PublicIP", ipaddress);
                                cmd122.Parameters.AddWithValue("@ComputerIP", computeripadd);
                                cmd122.Parameters.AddWithValue("@ComputerName", computername);
                                cmd122.Parameters.AddWithValue("@ComputerUserName", computerusername);
                                cmd122.Parameters.AddWithValue("@BrowserName", browser);
                                cmd122.Parameters.AddWithValue("@datetime", System.DateTime.Now.ToString());

                                //SqlCommand cmd122 = new SqlCommand("insert into [UserLoginLog] values('" + Convert.ToString(dt.Rows[0]["ID"]) + "','" + Session["userid"].ToString() + "','" + Session["EmployeeId"].ToString() + "','" + ipaddress + "','" + computeripadd + "','" + computername + "','" + computerusername + "','" + browser + "','" + System.DateTime.Now.ToString() + "')", cn);
                                //if (cn.State.ToString() != "Open")
                                //{
                                //    cn.Open();
                                //}
                                cmd122.ExecuteNonQuery();
                                cn.Close();
                            }

                            upfile += 1;

                            Label1.Visible = true;
                            Label1.Text = "Data Uploaded Successfully.";
                        }
                    }
                }
            }

            //SqlCommand cmd1a = new SqlCommand("Update FileNameTbl set Noofrecordupload='" + upfile + "',Noofrecordreject='" + rejfile + "' where Filename='" + ViewState["fl"] + "'", cn);
            //if (cn.State.ToString() != "Open")
            //{
            //    cn.Open();
            //}
            //cmd1a.ExecuteNonQuery();
            //cn.Close();

            lblsucmsg.Text = upfile.ToString() + " file and sheet name " + ddlSheets.SelectedItem.Text;

            ViewState["up"] = upfile.ToString();
            ViewState["rej"] = rejfile.ToString();

            lblnoofrecord.Text = upfile.ToString();
            lblnotimport.Text = rejfile.ToString();
            pnlsucedata.Visible = true;

            if (rejfile > 0)
            {
                Button2.Visible = true;

            }
            else
            {

                Button2.Visible = false;
            }


            //  fillgrid();
        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = "Error : " + ex.Message;
        }
        finally
        {
            oledbConn.Close();
        }

        ViewState["up"] = 0;
        ViewState["rej"] = 0;
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
        Dcom1.ColumnName = "BusinessName";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "address1";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "country";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "state";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "city";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;


        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "phone1";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;

        DataColumn Dcom11 = new DataColumn();
        Dcom11.DataType = System.Type.GetType("System.String");
        Dcom11.ColumnName = "zip";
        Dcom11.AllowDBNull = true;
        Dcom11.Unique = false;
        Dcom11.ReadOnly = false;

        DataColumn Dcom12 = new DataColumn();
        Dcom12.DataType = System.Type.GetType("System.String");
        Dcom12.ColumnName = "email";
        Dcom12.AllowDBNull = true;
        Dcom12.Unique = false;
        Dcom12.ReadOnly = false;


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);

        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom11);
        dt.Columns.Add(Dcom12);

        return dt;

    }

    protected void ImageButtondsfdsfdsf123_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void ImageButton4123_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void btlnote_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void GVC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVC.PageIndex = e.NewPageIndex;
        //  fillgrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        pnlgrd.Visible = true;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
        //pnldis.Visible = false;
    }
    protected void grderrorlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grderrorlist.PageIndex = e.NewPageIndex;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ViewState["data"] = "";
        grderrorlist.DataSource = null;
        grderrorlist.DataBind();
        pnlsucedata.Visible = false;
        pnlgrd.Visible = false;
    }
}