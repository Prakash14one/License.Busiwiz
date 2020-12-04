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


public partial class Sync_Table_Master : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);


    SqlConnection conn;
    public SqlConnection connweb;
    public static string encstr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            Session["ClientId"] = "35";

            Session[" userid"] = Convert.ToInt32(Session["Id"]);
           
            //if (Session["userloginname"] != null)
            //{

            //    string strcln = " SELECT * from EmployeeMaster where UserId ='" + ddlstatus.SelectedItem.Text + "'";
            //    SqlCommand cmdcln = new SqlCommand(strcln, con);
            //    DataTable dtcln = new DataTable();
            //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            //    adpcln.Fill(dtcln);

            //    // ddlsortdept.SelectedItem.Value = dtcln.Rows[0][""].ToString();
            //    Session["empid"] = dtcln.Rows[0]["Id"].ToString();
            //}


            // Session["ClientId"] = 35;

            Panel2.Visible = false;
            FillProduct();
            //fillgrid();

           // Panel8.Visible = false;
            // fillstatus();
            // fillgrid();
            ViewState["sortOrder"] = "";
            fillgrid();

        }

    }





    protected void FillProduct()
    {

        //  string strcln = " SELECT  distinct WebsiteMaster.ID as WebsiteMaster_ID,VersionInfoMaster.VersionInfoId,MasterPageMaster.MasterPageId,  ProductMaster.ProductName + ':' +   VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName + ':' +   MasterPageMaster.MasterPageName  as MasterPage_Name  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' ";
        string strcln = "SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion asc";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "--select--");
        ddlProductname.Items[0].Value = "0";


        DropDownList2.DataSource = dtcln;
        DropDownList2.DataValueField = "VersionInfoId";
        DropDownList2.DataTextField = "productversion";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "--select--");
        DropDownList2.Items[0].Value = "0";


    }






    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strcln = "select * from Database_InformationTBL where ProductID = '" + ddlProductname.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList1.DataSource = dtcln;
        DropDownList1.DataValueField = "DatabaseID";
        DropDownList1.DataTextField = "DatabaseName";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "--select--");
        DropDownList1.Items[0].Value = "0";
    }
  
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strcln = "select * from Database_InformationTBL where ProductID = '" + DropDownList2.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList3.DataSource = dtcln;
        DropDownList3.DataValueField = "DatabaseID";
        DropDownList3.DataTextField = "DatabaseName";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, "--select--");
        DropDownList3.Items[0].Value = "0";

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void fillgrid()
    {
        string str1 = "";
        if (DropDownList2.SelectedIndex > 0)
        {
            str1 += " and SyncTablelog.ProductID='" + DropDownList2.SelectedValue + "'";
        
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            str1 += " and SyncTablelog.Datasbaseid='" + DropDownList3.SelectedValue + "'";

        }
        if (TextBox6.Text != "")
        {
            str1 += " and ((ProductMaster.ProductName like '%" + TextBox6.Text.Replace("'", "''") + "%') or (Database_InformationTBL.DatabaseName like '%" + TextBox6.Text.Replace("'", "''") + "%') )";
        
        }


        string str = "select SyncTablelog.DateandTime,SyncTablelog.id,SyncTablelog.Result,Database_InformationTBL.DatabaseName ,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion  FROM  ProductMaster INNER JOIN  VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId  inner join SyncTablelog on SyncTablelog.ProductID=VersionInfoMaster.VersionInfoId inner join Database_InformationTBL on Database_InformationTBL.DatabaseID=SyncTablelog.Datasbaseid where SyncTablelog.id>0 " + str1 + "";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView3.DataSource = dt;
            GridView3.DataBind();

        }

        else 
        {

            GridView3.DataSource = null;
            GridView3.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        int totnoc = 0;
        string portid = "";
        string pcateid = "";
        string serId = "";
        string str = "select * from Database_InformationTBL where DatabaseID='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter da= new SqlDataAdapter(cmd);
        DataTable dtre = new DataTable();
        da.Fill(dtre);
        if (dtre.Rows.Count > 0)
        {
            encstr = "&%#@?,:*";            string serversqlserverip = dtre.Rows[0]["Sqlserverurl"].ToString();
            //string serversqlinstancename = dtre.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtre.Rows[0]["DatabaseName"].ToString();
            string serversqlpwd = dtre.Rows[0]["SAPassword"].ToString();
            string serversqlport = dtre.Rows[0]["Portnumber"].ToString();
            string serveruserid = dtre.Rows[0]["UserID"].ToString();
            try
            {
                totnoc = 1;
                conn = new SqlConnection();
                conn.ConnectionString = @"Data Source =" + serversqlserverip + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID='" + serveruserid + "'; Password=" + serversqlpwd + "; Persist Security Info=true;";
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                conn.Close();
                encstr = "";
                int inv = 0;
                string strnew = "SELECT name FROM sys.Tables";
                SqlCommand cmdnew = new SqlCommand(strnew, conn);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count > 0)
                {
                  for (int i = 0; i < dtnew.Rows.Count; i++)                  
                    {
                        con.Open();
                        string srtr = "select * from ClientProductTableMaster where VersionInfoId='" + ddlProductname.SelectedValue + "' and Databaseid='" + DropDownList1.SelectedValue + "' and TableName='" + dtnew.Rows[i]["name"].ToString() + "' ";
                        SqlCommand cmdr = new SqlCommand(srtr,con);
                        SqlDataAdapter dar = new SqlDataAdapter(cmdr);
                        DataTable dtr = new DataTable();
                        dar.Fill(dtr);                      
                        if (dtr.Rows.Count == 0)
                        {
                            string st = "insert into ClientProductTableMaster values('" + ddlProductname.SelectedValue + "','" + dtnew.Rows[i]["name"].ToString() + "','', '" + DropDownList1.SelectedValue + "') select @@identity ";                                                     
                            SqlCommand cmdst = new SqlCommand(st, con);
                            object ob = new object();
                            ob = cmdst.ExecuteScalar();
                            con.Close();                            

                            string strdata = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + dtnew.Rows[i][0].ToString() + "'";
                            SqlCommand cmddata = new SqlCommand(strdata, conn);
                            SqlDataAdapter dadata = new SqlDataAdapter(cmddata);
                            DataTable dtdata = new DataTable();
                            dadata.Fill(dtdata);
                            if (dtdata.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtdata.Rows.Count; j++)
                                {
                                    con.Open();
                                    string sytr = "select * from tablefielddetail where tableId = '" + ob + "' and feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                    SqlCommand cmdre = new SqlCommand(sytr, con);
                                    SqlDataAdapter daer = new SqlDataAdapter(cmdre);
                                    DataTable dts = new DataTable();
                                    daer.Fill(dts);
                                    if (dts.Rows.Count == 0)
                                    {
                                        if (dtdata.Rows[j]["DATA_TYPE"].ToString() == "int")
                                        {
                                            string str12 = "insert into tablefielddetail values('" + ob + "','" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "','" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"] + "','','','','" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "','" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "','" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "','" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "','" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "','" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "')";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                        }
                                        else
                                        {
                                            string str12 = "insert into tablefielddetail values('" + ob + "','" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "','" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_MAXIMUM_LENGTH"] + "','','','','" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "','" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "','" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "','" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "','" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "','" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "')";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                           
                                        }
                                    }
                                    else
                                    {
                                        if (dtdata.Rows[j]["DATA_TYPE"].ToString() == "int")
                                        {
                                            string str12 = "update tablefielddetail set tableId= '" + Session["ob"] + "',feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "',fieldtype='" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "',size='" + dtdata.Rows[j]["NUMERIC_PRECISION"] + "',Isforeignkey='',foreignkeytblid='',foreignkeyfieldId='',ORDINAL_POSITION='" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "',COLUMN_DEFAULT='" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "',IS_NULLABLE='" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "',CHARACTER_OCTET_LENGTH='" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "',NUMERIC_PRECISION='" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "',NUMERIC_PRECISION_RADIX='" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "',NUMERIC_SCALE='" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "',DATETIME_PRECISION='" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "',CHARACTER_SET_CATALOG='" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "',CHARACTER_SET_SCHEMA='" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "',CHARACTER_SET_NAME='" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "',COLLATION_CATALOG ='" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "',COLLATION_SCHEMA='" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "',COLLATION_NAME='" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "',DOMAIN_CATALOG='" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "',DOMAIN_SCHEMA='" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "',DOMAIN_NAME='" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "' where tableId= '" + Session["ob"] + "' and  feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                        }
                                        else
                                        {
                                            string str12 = "update tablefielddetail set tableId ='" + Session["ob"] + "',feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "',fieldtype='" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "',size='" + dtdata.Rows[j]["CHARACTER_MAXIMUM_LENGTH"] + "',Isforeignkey='',foreignkeytblid='',foreignkeyfieldId='',ORDINAL_POSITION='" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "',COLUMN_DEFAULT='" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "',IS_NULLABLE='" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "',CHARACTER_OCTET_LENGTH='" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "',NUMERIC_PRECISION='" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "',NUMERIC_PRECISION_RADIX='" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "',NUMERIC_SCALE='" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "',DATETIME_PRECISION='" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "',CHARACTER_SET_CATALOG='" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "',CHARACTER_SET_SCHEMA='" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "',CHARACTER_SET_NAME='" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "',COLLATION_CATALOG='" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "',COLLATION_SCHEMA='" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "',COLLATION_NAME='" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "',DOMAIN_CATALOG='" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "',DOMAIN_SCHEMA='" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "',DOMAIN_NAME='" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "' where tableId= '" + Session["ob"] + "' and  feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();                                           
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Session["ob"] = dtr.Rows[0]["Id"].ToString();
                            string gdd = dtr.Rows[0]["Id"].ToString();
                            string st = "update  ClientProductTableMaster set VersionInfoId='" + ddlProductname.SelectedValue + "',TableName='" + dtnew.Rows[i]["name"].ToString() + "',TableTitle='',Databaseid= '" + DropDownList1.SelectedValue + "' where VersionInfoId = '" + ddlProductname.SelectedValue + "' and Databaseid= '" + DropDownList1.SelectedValue + "' and TableName='" + dtnew.Rows[i]["name"].ToString() + "' ";
                            SqlCommand cmdst = new SqlCommand(st, con);
                            cmdst.ExecuteNonQuery();
                            con.Close();                          

                            string strdata = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + dtnew.Rows[i][0].ToString() + "'";
                            SqlCommand cmddata = new SqlCommand(strdata, conn);
                            SqlDataAdapter dadata = new SqlDataAdapter(cmddata);
                            DataTable dtdata = new DataTable();
                            dadata.Fill(dtdata);
                            if (dtdata.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtdata.Rows.Count; j++)
                                {
                                    con.Open();
                                    string sytr = "select * from tablefielddetail where tableId = '" + Session["ob"] + "' and  feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                    SqlCommand cmdre = new SqlCommand(sytr, con);
                                    SqlDataAdapter daer = new SqlDataAdapter(cmdre);
                                    DataTable dts = new DataTable();
                                    daer.Fill(dts);
                                    if (dts.Rows.Count == 0)
                                    {
                                        if (dtdata.Rows[j]["DATA_TYPE"].ToString() == "int")
                                        {
                                            string str12 = "insert into tablefielddetail values('" + Session["ob"] + "','" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "','" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"] + "','','','','" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "','" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "','" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "','" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "','" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "','" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "')";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                        }
                                        else
                                        {
                                            string str12 = "insert into tablefielddetail values('" + Session["ob"] + "','" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "','" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_MAXIMUM_LENGTH"] + "','','','','" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "','" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "','" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "','" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "','" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "','" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "','" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "','" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "')";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                          
                                        }
                                    }
                                    else
                                    {
                                        if (dtdata.Rows[j]["DATA_TYPE"].ToString() == "int")
                                        {
                                            string str12 = "update tablefielddetail set tableId= '" + Session["ob"] + "',feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "',fieldtype='" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "',size='" + dtdata.Rows[j]["NUMERIC_PRECISION"] + "',Isforeignkey='',foreignkeytblid='',foreignkeyfieldId='',ORDINAL_POSITION='" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "',COLUMN_DEFAULT='" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "',IS_NULLABLE='" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "',CHARACTER_OCTET_LENGTH='" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "',NUMERIC_PRECISION='" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "',NUMERIC_PRECISION_RADIX='" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "',NUMERIC_SCALE='" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "',DATETIME_PRECISION='" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "',CHARACTER_SET_CATALOG='" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "',CHARACTER_SET_SCHEMA='" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "',CHARACTER_SET_NAME='" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "',COLLATION_CATALOG ='" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "',COLLATION_SCHEMA='" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "',COLLATION_NAME='" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "',DOMAIN_CATALOG='" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "',DOMAIN_SCHEMA='" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "',DOMAIN_NAME='" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "' where tableId= '" + Session["ob"] + "' and  feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();
                                        }
                                        else
                                        {
                                            string str12 = "update tablefielddetail set tableId ='" + Session["ob"] + "',feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "',fieldtype='" + dtdata.Rows[j]["DATA_TYPE"].ToString() + "',size='" + dtdata.Rows[j]["CHARACTER_MAXIMUM_LENGTH"] + "',Isforeignkey='',foreignkeytblid='',foreignkeyfieldId='',ORDINAL_POSITION='" + dtdata.Rows[j]["ORDINAL_POSITION"].ToString() + "',COLUMN_DEFAULT='" + dtdata.Rows[j]["COLUMN_DEFAULT"].ToString() + "',IS_NULLABLE='" + dtdata.Rows[j]["IS_NULLABLE"].ToString() + "',CHARACTER_OCTET_LENGTH='" + dtdata.Rows[j]["CHARACTER_OCTET_LENGTH"].ToString() + "',NUMERIC_PRECISION='" + dtdata.Rows[j]["NUMERIC_PRECISION"].ToString() + "',NUMERIC_PRECISION_RADIX='" + dtdata.Rows[j]["NUMERIC_PRECISION_RADIX"].ToString() + "',NUMERIC_SCALE='" + dtdata.Rows[j]["NUMERIC_SCALE"].ToString() + "',DATETIME_PRECISION='" + dtdata.Rows[j]["DATETIME_PRECISION"].ToString() + "',CHARACTER_SET_CATALOG='" + dtdata.Rows[j]["CHARACTER_SET_CATALOG"].ToString() + "',CHARACTER_SET_SCHEMA='" + dtdata.Rows[j]["CHARACTER_SET_SCHEMA"].ToString() + "',CHARACTER_SET_NAME='" + dtdata.Rows[j]["CHARACTER_SET_NAME"].ToString() + "',COLLATION_CATALOG='" + dtdata.Rows[j]["COLLATION_CATALOG"].ToString() + "',COLLATION_SCHEMA='" + dtdata.Rows[j]["COLLATION_SCHEMA"].ToString() + "',COLLATION_NAME='" + dtdata.Rows[j]["COLLATION_NAME"].ToString() + "',DOMAIN_CATALOG='" + dtdata.Rows[j]["DOMAIN_CATALOG"].ToString() + "',DOMAIN_SCHEMA='" + dtdata.Rows[j]["DOMAIN_SCHEMA"].ToString() + "',DOMAIN_NAME='" + dtdata.Rows[j]["DOMAIN_NAME"].ToString() + "' where tableId= '" + Session["ob"] + "' and  feildname='" + dtdata.Rows[j]["COLUMN_NAME"].ToString() + "' ";
                                            SqlCommand cmd12 = new SqlCommand(str12, con);
                                            cmd12.ExecuteNonQuery();
                                            inv = 1;
                                            con.Close();                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (inv == 1)
                    {
                        con.Open();
                        string str23 = "select * from SyncTablelog where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "' ";
                        SqlCommand cmd23 = new SqlCommand(str23, con);
                        SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                        DataTable dt23 = new DataTable();
                        da23.Fill(dt23);
                        if (dt23.Rows.Count == 0)
                        {                          
                            string str14 = "insert into SyncTablelog values('" + ddlProductname.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + Session["ob"] + "','" + System.DateTime.Now.ToString() + "','Syncronise Successfully')";
                            SqlCommand cmd14 = new SqlCommand(str14, con);
                            cmd14.ExecuteNonQuery();
                            lblmsg.Text = "Sync successfully";
                            fillgrid();
                        }
                        else
                        {
                            string str24 = "update SyncTablelog set  ProductID='" + ddlProductname.SelectedValue + "',Datasbaseid='" + DropDownList1.SelectedValue + "',TabelID='" + Session["ob"] + "',DateandTime='" + System.DateTime.Now.ToString() + "' where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "'";
                            SqlCommand cmd24 = new SqlCommand(str24, con);
                            cmd24.ExecuteNonQuery();
                        }
                        con.Close();
                        lblmsg.Text = "Sync successfully";
                    }
                }
            }
            catch (Exception e1)
            {
                con.Open();
                string str23 = "select * from SyncTablelog where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "' ";
                SqlCommand cmd23 = new SqlCommand(str23, con);
                SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                DataTable dt23 = new DataTable();
                da23.Fill(dt23);
                if (dt23.Rows.Count == 0)
                {
                    string str14 = "insert into SyncTablelog values('" + ddlProductname.SelectedValue + "','" + DropDownList1.SelectedValue + "','" + Session["ob"] + "','" + System.DateTime.Now.ToString() + "','Not Syncronise Successfully')";
                    SqlCommand cmd14 = new SqlCommand(str14, con);
                    cmd14.ExecuteNonQuery();
                    lblmsg.Text = "Not Syncronise Successfully";
                    fillgrid();
                }
                else 
                {
                    string str24 = "update SyncTablelog set  ProductID='" + ddlProductname.SelectedValue + "',Datasbaseid='" + DropDownList1.SelectedValue + "',TabelID='" + Session["ob"] + "',DateandTime='" + System.DateTime.Now.ToString() + "' where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "'";
                    SqlCommand cmd24 = new SqlCommand(str24, con);
                    cmd24.ExecuteNonQuery();                
                }
                con.Close();
                lblmsg.Text = "Not Syncronise Successfully";
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //  Button3.Visible = true;
        // Button12.Visible = false;
        GridView3.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}