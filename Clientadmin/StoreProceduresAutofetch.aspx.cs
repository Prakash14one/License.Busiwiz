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

public partial class storedprocedueautofetch : System.Web.UI.Page
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //  Button3.Visible = true;
        // Button12.Visible = false;
        GridView3.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void fillgrid()
    {
        string str1 = "";
        if (DropDownList2.SelectedIndex > 0)
        {
            str1 += " and SyncProcedureLog.ProductID='" + DropDownList2.SelectedValue + "'";

        }
        if (DropDownList3.SelectedIndex > 0)
        {
            str1 += " and SyncProcedureLog.Datasbaseid='" + DropDownList3.SelectedValue + "'";

        }
        if (TextBox6.Text != "")
        {
            str1 += " and ((ProductMaster.ProductName like '%" + TextBox6.Text.Replace("'", "''") + "%') or (Database_InformationTBL.DatabaseName like '%" + TextBox6.Text.Replace("'", "''") + "%') )";

        }


        string str = "select SyncProcedureLog.DateandTime,SyncProcedureLog.id,SyncProcedureLog.Result,Database_InformationTBL.DatabaseName ,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion  FROM  ProductMaster INNER JOIN  VersionInfoMaster ON ProductMaster.ProductId = VersionInfoMaster.ProductId  inner join SyncProcedureLog on SyncProcedureLog.ProductID=VersionInfoMaster.VersionInfoId inner join Database_InformationTBL on Database_InformationTBL.DatabaseID=SyncProcedureLog.Datasbaseid where SyncProcedureLog.id>0 " + str1 + "";
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
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
            encstr = "&%#@?,:*";




            string serversqlserverip = dtre.Rows[0]["Sqlserverurl"].ToString();
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

                //conn.ConnectionString = @"Data Source =" + dtre.Rows[0]["PublicIp"] + "," + dtcheck1.Rows[0]["Port"] + "; Initial Catalog = " + dtcheck1.Rows[0]["DatabaseName"] + "; Integrated Security = true";
                // conn = new SqlConnection(@"Data Source =" + Convert.ToString(dtre.Rows[0]["sqlurl"]) + "\\" + Convert.ToString(dtre.Rows[0]["DefaultsqlInstance"]) + "," + dtre.Rows[0]["port"] + "; Initial Catalog = " + Convert.ToString(dtre.Rows[0]["DefaultDatabaseName"]) + "; User ID=Sa; Password=" + Decrypted(Convert.ToString(dtre.Rows[0]["Sapassword"])) + "; Persist Security Info=true;");

                //  conn = new SqlConnection(@"Data Source =" + Convert.ToString(dtre.Rows[0]["PublicIp"]) + "\\" + Convert.ToString(dtre.Rows[0]["DefaultsqlInstance"]) + "," + dtre.Rows[0]["port"] + "; Initial Catalog = " + Convert.ToString(dtre.Rows[0]["DefaultDatabaseName"]) + "; User ID=Sa; Password=" + Decrypted(Convert.ToString(dtre.Rows[0]["Sapassword"])) + "");
                //conn = new SqlConnection(@"Data Source=192.168.6.49,2810;Initial Catalog=BlankcopyUserLog;Integrated Security=true");
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                conn.Close();
                encstr = "";



                int inv = 0;
                string strnew = "Select name from sys.procedures  ";
                SqlCommand cmdnew = new SqlCommand(strnew, conn);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnew.Rows.Count; i++)                  
                    {
                        con.Open();

                        string srtr = " SELECT DISTINCT  o.id, o.name AS 'Procedure_Name' , oo.name AS 'Table_Name', d.depid FROM sysdepends d, sysobjects o, sysobjects oo WHERE    o.id=d.id  AND o.name= '" + dtnew.Rows[i]["name"].ToString()+ "'  AND oo.id=d.depid  ORDER BY o.name,oo.name ";
                        SqlCommand cmdr = new SqlCommand(srtr, conn);
                        SqlDataAdapter dar = new SqlDataAdapter(cmdr);
                        DataTable dtr = new DataTable();
                        dar.Fill(dtr);
                          
                        string strda= "SELECT Object_definition(object_id) as procedurecode FROM   sys.procedures WHERE  name = '"+dtnew.Rows[i]["name"].ToString()+"'";
                        SqlCommand cmdda= new SqlCommand(strda,conn);
                        SqlDataAdapter daas= new SqlDataAdapter(cmdda);
                        DataTable dts= new DataTable();
                        daas.Fill(dts);


                        if (dtr.Rows.Count > 0)
                        {



                            string str32 = "select  proce_id from Proceduremaster where Proceduremaster.Name='" + dtnew.Rows[i]["name"].ToString() + "' and product_id='" + ddlProductname.SelectedValue + "' and databaseid='" + DropDownList1.SelectedValue + "'";
                            SqlCommand cmd32 = new SqlCommand(str32, con);
                            SqlDataAdapter da32 = new SqlDataAdapter(cmd32);
                            DataTable dt32 = new DataTable();
                            da32.Fill(dt32);


                           
                            if (dt32.Rows.Count == 0)
                            {




                                string dt = System.DateTime.Now.ToString();
                                SqlCommand cmdst = new SqlCommand("insertprocedure", con);
                                cmdst.CommandType = CommandType.StoredProcedure;
                                cmdst.Parameters.AddWithValue("@Name", dtnew.Rows[i]["name"].ToString());
                                cmdst.Parameters.AddWithValue("@Type", "");
                                cmdst.Parameters.AddWithValue("@Description", "");
                                cmdst.Parameters.AddWithValue("@Proce_code", dts.Rows[0]["procedurecode"].ToString());
                                cmdst.Parameters.AddWithValue("@Date", dt.ToString());
                                cmdst.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session[" userid"]));
                                cmdst.Parameters.AddWithValue("@product_id", ddlProductname.SelectedItem.Value);
                                cmdst.Parameters.AddWithValue("@databaseid", DropDownList1.SelectedItem.Value);





                                object ob = new object();
                                ob = cmdst.ExecuteScalar();

                                Session["prod_id"] = ob;

                                for (int j = 0; j < dtr.Rows.Count; j++)
                                {


                                    string str31 = "select Id from ClientProductTableMaster  where ClientProductTableMaster.TableName='" + dtr.Rows[j]["Table_Name"].ToString() + "' and VersionInfoId='" + ddlProductname.SelectedValue + "'";
                                    SqlCommand cmd31 = new SqlCommand(str31, con);
                                    SqlDataAdapter da31 = new SqlDataAdapter(cmd31);
                                    DataTable dt31 = new DataTable();
                                    da31.Fill(dt31);
                                    if (dt31.Rows.Count > 0)
                                    {




                                        SqlCommand cmd1 = new SqlCommand("insert_usingtable", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@Proce_id", ob);



                                        cmd1.Parameters.AddWithValue("@table_id", dt31.Rows[0][0].ToString());
                                        cmd1.ExecuteNonQuery();

                                        inv = 1;

                                    }



                                }


                               
                                //string st34 = "select MAX(Id) from ClientProductTableMaster";
                                //SqlCommand cmd34 = new SqlCommand(st34, con);
                                //SqlDataAdapter da34 = new SqlDataAdapter(cmd34);
                                //DataTable dt34 = new DataTable();
                                //da34.Fill(dt34);


                                //object ob = new object();
                                //ob = dt34.Rows[0][0].ToString();
                                //Session["ob"] = ob;
                            }

                            else 
                            {


                                Session["prod_id"] = dt32.Rows[0][0].ToString();
                                string dt = System.DateTime.Now.ToString();
                                SqlCommand cmdst = new SqlCommand("update procedure", con);
                                cmdst.CommandType = CommandType.StoredProcedure;
                                cmdst.Parameters.AddWithValue("@Name", dtnew.Rows[i]["name"].ToString());
                                cmdst.Parameters.AddWithValue("@Type", "");
                                cmdst.Parameters.AddWithValue("@Description", "");
                                cmdst.Parameters.AddWithValue("@Proce_code", dts.Rows[0]["procedurecode"].ToString());
                                cmdst.Parameters.AddWithValue("@Date", dt.ToString());
                                cmdst.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session[" userid"]));
                                cmdst.Parameters.AddWithValue("@product_id", ddlProductname.SelectedItem.Value);
                                cmdst.Parameters.AddWithValue("@databaseid", DropDownList1.SelectedItem.Value);
                                cmdst.Parameters.AddWithValue("@Proce_id", Session["prod_id"]);
                                cmdst.ExecuteNonQuery();


                                string strdelete = "delete  from storeproc_usingtable where storeproc_usingtable.Proce_id='" + Session["prod_id"] + "' ";
                                SqlCommand cmddelete = new SqlCommand(strdelete, con);
                                cmddelete.ExecuteNonQuery();

                                for (int j = 0; j < dtr.Rows.Count; j++)
                                {


                                    string str31 = "select Id from ClientProductTableMaster  where ClientProductTableMaster.TableName='" + dtr.Rows[j]["Table_Name"].ToString() + "' and VersionInfoId='" + ddlProductname.SelectedValue + "'";
                                    SqlCommand cmd31 = new SqlCommand(str31, con);
                                    SqlDataAdapter da31 = new SqlDataAdapter(cmd31);
                                    DataTable dt31 = new DataTable();
                                    da31.Fill(dt31);
                                    if (dt31.Rows.Count > 0)
                                    {                                   


                                        SqlCommand cmd1 = new SqlCommand("insert_usingtable", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@Proce_id", Session["prod_id"]);
                                        cmd1.Parameters.AddWithValue("@table_id", dt31.Rows[0][0].ToString());
                                        cmd1.ExecuteNonQuery();                                    
                                        inv = 1;
                                    }



                                }


                                


                            
                            }


                           

                        }

                        con.Close();
    



                        }


                    }

                    if (inv == 1)
                    {
                        con.Open();
                        string str23 = "select * from SyncProcedureLog where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "' ";
                        SqlCommand cmd23 = new SqlCommand(str23, con);
                        SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                        DataTable dt23 = new DataTable();
                        da23.Fill(dt23);

                        if (dt23.Rows.Count == 0)
                        {


                            string str14 = "insert into SyncProcedureLog values('" + ddlProductname.SelectedValue + "','" + DropDownList1.SelectedValue + "','','" + System.DateTime.Now.ToString() + "','Syncronise Successfully')";
                            SqlCommand cmd14 = new SqlCommand(str14, con);
                            cmd14.ExecuteNonQuery();
                            lblmsg.Text = "Sync successfully";


                            fillgrid();
                        }
                        else
                        {
                            string str24 = "update SyncProcedureLog set  ProductID='" + ddlProductname.SelectedValue + "',Datasbaseid='" + DropDownList1.SelectedValue + "',DateandTime='" + System.DateTime.Now.ToString() + "' where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "'";
                            SqlCommand cmd24 = new SqlCommand(str24, con);
                            cmd24.ExecuteNonQuery();
                            fillgrid();
                        }



                        lblmsg.Text = "Sync successfully";
                    }
                }












           // 
            catch (Exception ex)
            {
            
                            con.Open();


                            string str23 = "select * from SyncProcedureLog where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "' ";
                SqlCommand cmd23 = new SqlCommand(str23, con);
                SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                DataTable dt23 = new DataTable();
                da23.Fill(dt23);

                if (dt23.Rows.Count == 0)
                {


                    string str14 = "insert into SyncProcedureLog values('" + ddlProductname.SelectedValue + "','" + DropDownList1.SelectedValue + "','','" + System.DateTime.Now.ToString() + "','Not Syncronise Successfully')";
                    SqlCommand cmd14 = new SqlCommand(str14, con);
                    cmd14.ExecuteNonQuery();
                    lblmsg.Text = "Not Syncronise Successfully";
                    fillgrid();
                }

                else 
                {
                    string str24 = "update SyncProcedureLog set  ProductID='" + ddlProductname.SelectedValue + "',Datasbaseid='" + DropDownList1.SelectedValue + "',DateandTime='" + System.DateTime.Now.ToString() + "'  where ProductID='" + ddlProductname.SelectedValue + "' and Datasbaseid='" + DropDownList1.SelectedValue + "'";
                    SqlCommand cmd24 = new SqlCommand(str24, con);
                    cmd24.ExecuteNonQuery();
                    fillgrid();
                
                }
                con.Close();

                lblmsg.Text = "Not Syncronise Successfully";
            
            
            
            }

            finally
                {
                    con.Close();
                
                }
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}