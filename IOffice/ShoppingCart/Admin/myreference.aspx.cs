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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class ShoppingCart_Admin_myreferences : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    int groupid = 0;
    string accid = "";
    int classid = 0;
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            string id = "select CandidateMaster.CandidateId from CandidateMaster where PartyID='" + Session["PartyId"].ToString() + "'";
            SqlDataAdapter daid = new SqlDataAdapter(id, con);
            DataTable dt27 = new DataTable();
            daid.Fill(dt27);
            ViewState["candidateid"] = dt27.Rows[0][0].ToString();
            ViewState["comid"] = "jobcenter";
            ViewState["whid"] = "1";
            fillcountry();
            fillgrid();
            fillvacancy();
           
        }

    }
    public void fillvacancy()
    {
        string vacancy = " select VacancyPositionTitleMaster.ID,VacancyPositionTitleMaster.VacancyPositionTitle,VacancyTypeMaster.Name "+
                          "  from VacancyPositionTitleMaster "+
                         " inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyPositionTitleMaster.VacancyPositionTypeID "+
                         " inner join CandidateJobTitles on CandidateJobTitles.titleid=VacancyPositionTitleMaster.ID "+
                          " where candidateid= "+ ViewState["candidateid"]+"";
        SqlCommand cmd3 = new SqlCommand(vacancy, con);
        SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
        DataTable ds3 = new DataTable();
        adp3.Fill(ds3);
        if (ds3.Rows.Count > 0)
        {
            GridView3.DataSource = ds3;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSource = "";
            GridView3.DataBind();
        }
    }
    public void fillcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        SqlCommand cmd1 = new SqlCommand(qryStr, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["CountryId"] = "0";
        dr["CountryName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlcountry.DataSource = ds1;
        ddlcountry.DataBind();
        

        
    }
    public void fillstate()
    {
        string str2 = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry.SelectedValue + " order by StateName";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["StateId"] = "0";
        dr["StateName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlstate.DataSource = ds1;
        ddlstate.DataBind();
    


    }
    public void fillcity()
    {
        string str2 = "select CityId,CityName from CityMasterTbl where StateId=" + ddlstate.SelectedValue + " order by CityName";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["CityId"] = "0";
        dr["CityName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlcity.DataSource = ds1;
        ddlcity.DataBind();
        
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        btnadd.Visible = false;

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate();
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity();
    }
    public void clr()
    {
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlcity.SelectedIndex = 0;
        txtname.Text = "";
        txtdesig.Text = "";
        txtcompany.Text = "";
        txtphone.Text = "";
        txtemail.Text = "";
        txtaddress.Text = "";
        Label16.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clr();
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        if (txtemail.Text != "")
        {
            string sel = "select name,email from MyRefenceTbl where email='" + txtemail.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sel, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Label36.Text = "'" + dt1.Rows[0][1].ToString() + "' mail id is already used by '" + dt1.Rows[0][0].ToString() + "'.Do You Want to continue? ";
                ModalPopupExtender2.Show();
               // Label16.Text = "Email Already Used";
                ViewState["name"] = dt1.Rows[0][0].ToString();
                Label16.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Label16.Text = "Email Available";
                Label16.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            string insert = "insert into MyRefenceTbl(name,designation,company,contactno,email,address,countryid,stateid,cityid,candidateid,status,dateandtime) " +
                         " values('" + txtname.Text + "','" + txtdesig.Text + "','" + txtcompany.Text + "','" + txtphone.Text + "','" + txtemail.Text + "','" + txtaddress.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "'," + ViewState["candidateid"] + ",'1','" + DateTime.Now.ToString() + "')";
            SqlCommand cmd1 = new SqlCommand(insert, con);
            cmd1.ExecuteNonQuery();

            string referencid = "select max(refernceid) from MyRefenceTbl ";
            SqlCommand cmd2 = new SqlCommand(referencid, con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            adp2.Fill(dt2);

            if (RadioButtonList1.SelectedValue == "1")
            {
                string ins = "insert into Refernce_inputTbl(reference_id,input_type,Response,Dateandtime) values('" + dt2.Rows[0][0].ToString() + "','" + RadioButtonList1.SelectedValue + "','No','" + DateTime.Now.ToString() + "')";
                SqlCommand cmd4 = new SqlCommand(ins, con);
                cmd4.ExecuteNonQuery();
            }
            else
            {
                DataTable dt_vac = ViewState["vacancyid"] as DataTable;
                if (dt_vac.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_vac.Rows.Count; i++)
                    {
                        string ins3 = "insert into Refernce_inputTbl(reference_id,VacancyID,input_type,Response,Dateandtime) values('" + dt2.Rows[0][0].ToString() + "','" + dt_vac.Rows[i][0].ToString() + "','" + RadioButtonList1.SelectedValue + "','No','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmd3 = new SqlCommand(ins3, con);
                        cmd3.ExecuteNonQuery();
                    }
                }
            }
            con.Close();
/////////// checking///////////
            string sel = "select name,email from MyRefenceTbl where email='" + txtemail.Text + "'";
            SqlCommand cmd11 = new SqlCommand(sel, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable dt11 = new DataTable();
            adp11.Fill(dt11);
            if (dt11.Rows.Count > 1)
            {
                sendmail();
            }
            else
            {
                string strgetusername = "select max(PartyID) as PartyID from Party_master ";
                SqlCommand cmdusername = new SqlCommand(strgetusername, con);
                SqlDataAdapter adpusername = new SqlDataAdapter(cmdusername);
                DataTable dsusername = new DataTable();
                adpusername.Fill(dsusername);

                if (dsusername.Rows.Count > 0)
                {
                    string username;
                    string Password;
                    int i = 0;
                    int c = dsusername.Rows.Count + 1;
                    for (i = 0; ; i++)
                    {
                        username = txtemail.Text;
                        Password = "Reference" + dsusername.Rows[0]["PartyID"].ToString() + "++" + c;

                        string strusercheck = " select * from User_master where Username='" + username + "'";
                        SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
                        SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
                        DataTable dsusercheck = new DataTable();
                        adpusercheck.Fill(dsusercheck);

                        if (dsusercheck.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            username = txtemail.Text;// txtfirstname.Text + dsusername.Rows[0]["PartyID"].ToString() + i;
                            Password = "Reference" + dsusername.Rows[0]["PartyID"].ToString() + "++" + i;
                            break;
                        }
                    }

                    ViewState["username"] = username;
                    ViewState["password"] = Password;
                }

                

                string qryStr = " insert into AccountMaster(ClassId,AccountId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) " +
                                          " values ('" + classid + "','" + accid + "','" + groupid + "','" + txtname.Text + "','New reference','0'," + System.DateTime.Now.ToShortDateString() + ",'0','0','" + System.DateTime.Now.ToShortDateString() + "','1','" + ViewState["comid"].ToString() + "','" + ViewState["whid"] + "')";


                SqlCommand cm = new SqlCommand(qryStr, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cm.ExecuteNonQuery();
                con.Close();
                string str1113 = "select max(Id) as Aid from AccountMaster";
                SqlCommand cmd1113 = new SqlCommand(str1113, con);
                SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                DataTable ds1113 = new DataTable();
                adp1113.Fill(ds1113);

                Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();

                string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','1')";
                SqlCommand cmd4562 = new SqlCommand(str4562, con);
                con.Open();
                cmd4562.ExecuteNonQuery();
                con.Close();

                string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','1')";
                SqlCommand cmd456 = new SqlCommand(str456, con);
                con.Open();
                cmd456.ExecuteNonQuery();
                con.Close();

                string sel12 = "select PartyMasterCategoryNo from PartyMasterCategory where Name='Others'";
                SqlDataAdapter da12 = new SqlDataAdapter(sel12,con);
                DataTable datparcat = new DataTable();
                da12.Fill(datparcat);

                if (datparcat.Rows.Count > 0)
                {
                    string sel13 = "select PartyTypeId from PartytTypeMaster where compid='" + ViewState["comid"] + "' and PartyCategoryId='" + datparcat.Rows[0]["PartyMasterCategoryNo"].ToString() + "'";
                    SqlDataAdapter da13 = new SqlDataAdapter(sel13, con);
                    DataTable dtpartytype = new DataTable();
                    da13.Fill(dtpartytype);
                    ViewState["partytypeid"] = dtpartytype.Rows[0]["PartyTypeId"].ToString();
                }


                string ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID, " +
                                " PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId, " +
                                " AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,AccountBalanceLimitId,id,Whid,PartyTypeCategoryNo,Zipcode) " +
                                " values ( '" + accid + "','" + txtname.Text + "','', " +
                                "'" + txtaddress.Text + "','" + ddlcity.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "','', " +
                                " '' ,'','" + txtemail.Text + "','" + txtphone.Text + "','1', '" + ViewState["partytypeid"] + "' ,'" + 0 + "' ,'" + 0 + "', " +
                                " '" + 0 + "' , '" + 0 + "' , '" + 0 + "' ,'' , '' ,'1','','" + ViewState["comid"] + "','" + ViewState["whid"] + "','" + datparcat.Rows[0]["PartyMasterCategoryNo"].ToString() + "','')";

                SqlCommand cmd3 = new SqlCommand(ins1, con);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();

                string sel23 = "select max(PartyID) as PartyID from Party_master where Id='" + ViewState["comid"].ToString() + "' and Whid='" + ViewState["whid"] + "'";

                SqlCommand cmd5 = new SqlCommand(sel23, con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataSet ds5 = new DataSet();
                da5.Fill(ds5);

                string phofile = "";

                ViewState["partyidforemail"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString());


                string insdept = "Select id from DepartmentmasterMNC where Departmentname='Other' and Whid='" + ViewState["whid"] + "'";

                SqlDataAdapter dadept = new SqlDataAdapter(insdept, con);
                DataTable dtdept = new DataTable();
                dadept.Fill(dtdept);

                object dept = "";

                if (dtdept.Rows.Count > 0)
                {
                    ViewState["deptid"] = Convert.ToString(dtdept.Rows[0]["id"]);
                }

                else
                {
                    SqlCommand cmddept = new SqlCommand("DeptRetIdentity", con);
                    cmddept.CommandType = CommandType.StoredProcedure;
                    cmddept.Parameters.AddWithValue("@Departmentname", "Other");
                    cmddept.Parameters.AddWithValue("@Companyid", ViewState["comid"].ToString());

                    if (Request.QueryString["Id"] != null)
                    {
                        cmddept.Parameters.AddWithValue("@Whid", ViewState["whid"]);
                    }
                    else
                    {
                        cmddept.Parameters.AddWithValue("@Whid", ViewState["whid"]);
                    }


                    cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
                    cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                    cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    dept = (object)cmddept.ExecuteNonQuery();
                    dept = cmddept.Parameters["@Id"].Value;
                    ViewState["deptid"] = dept;
                    con.Close();
                }


                SqlDataAdapter darole = new SqlDataAdapter("Select Role_id from RoleMaster where Role_name='Other' and compid='" + ViewState["comid"].ToString() + "'", con);
                DataTable dtrole = new DataTable();
                darole.Fill(dtrole);

                if (dtrole.Rows.Count > 0)
                {
                    ViewState["roleid"] = Convert.ToString(dtrole.Rows[0]["Role_id"]);
                }
                else
                {
                    SqlCommand cmd3r = new SqlCommand("Insert into RoleMaster (Role_name,ActiveDeactive,compid) values ('Other','True','" + ViewState["comid"].ToString() + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd3r.ExecuteNonQuery();
                    con.Close();

                    string strd = "SELECT  Role_id from RoleMaster WHERE Role_name = 'Other' and compid='" + ViewState["comid"].ToString() + "'";
                    SqlDataAdapter adrol = new SqlDataAdapter(strd, con);
                    DataTable dtrol = new DataTable();
                    adrol.Fill(dtrol);

                    ViewState["roleid"] = Convert.ToString(dtrol.Rows[0]["Role_id"]);
                }

                string strdesg = "SELECT  DesignationMasterId from DesignationMaster WHERE DesignationName = 'Referance Provider' and DeptID='" + ViewState["deptid"] + "'";
                SqlDataAdapter dadesg = new SqlDataAdapter(strdesg, con);
                DataTable dtdesg = new DataTable();
                dadesg.Fill(dtdesg);

                if (dtdesg.Rows.Count > 0)
                {
                    ViewState["desgid"] = Convert.ToString(dtdesg.Rows[0]["DesignationMasterId"]);
                }

                else
                {
                    SqlCommand cmddegdes = new SqlCommand("Insert into DesignationMaster(DesignationName,DeptID,RoleId) values ('Referance Provider','" + ViewState["deptid"] + "','" + ViewState["roleid"] + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddegdes.ExecuteNonQuery();
                    con.Close();

                    string strdesg1 = "SELECT  DesignationMasterId from DesignationMaster WHERE DesignationName = 'Referance Provider' and DeptID='" + ViewState["deptid"] + "'";
                    SqlDataAdapter dadesg1 = new SqlDataAdapter(strdesg1, con);
                    DataTable dtdesg1 = new DataTable();
                    dadesg1.Fill(dtdesg1);

                    ViewState["desgid"] = Convert.ToString(dtdesg1.Rows[0]["DesignationMasterId"]);
                }


                SqlDataAdapter daprice = new SqlDataAdapter("select PricePlanId from CompanyMaster where CompanyLoginId='" + ViewState["comid"].ToString() + "'", PageConn.licenseconn());
                DataTable dtprice = new DataTable();
                daprice.Fill(dtprice);

                if (dtprice.Rows.Count > 0)
                {
                    ViewState["priceid"] = Convert.ToString(dtprice.Rows[0]["PricePlanId"]);
                }

                string strdw = "select PageMaster.pageid from [PageMaster] inner join [pageplaneaccesstbl] on [pageplaneaccesstbl].Pageid=[PageMaster].PageId  where [pageplaneaccesstbl].Priceplanid='" + ViewState["priceid"] + "' and PageMaster.PageName IN ('CandidateApplicationProfile.aspx','candidatelistcandidate.aspx','MessageCompose.aspx','MessageInbox.aspx','MessageSent.aspx','MessageDrafts.aspx','MessageDeletedItems.aspx','MessageView.aspx','vacancyprofile.aspx','EditCandidateProfile.aspx')";

                SqlDataAdapter adrolw = new SqlDataAdapter(strdw, PageConn.licenseconn());
                DataTable dtrolw = new DataTable();
                adrolw.Fill(dtrolw);

                foreach (DataRow it in dtrolw.Rows)
                {
                    string str11 = "insert into RolePageAccessRightTbl(PageId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) " +
                                  " Values( '" + it["pageid"] + "','" + ViewState["roleid"] + "','1','" + true + "','" + true + "','" + true + "','" + true + "','" + true + "','" + true + "','" + true + "','" + true + "')";

                    SqlCommand cmd22 = new SqlCommand(str11, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd22.ExecuteNonQuery();
                    con.Close();
                }


                string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode)" +
                                                         "values ('" + txtname.Text + "','" + txtaddress.Text + "','" + ddlcity.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "','" + txtphone.Text + "','" + txtemail.Text + "','" + ViewState["username"] + "','" + ViewState["deptid"] + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ViewState["desgid"] + "','' ,'1','','')";
                SqlCommand cmd6 = new SqlCommand(ins6, con);

                con.Open();
                cmd6.ExecuteNonQuery();
                con.Close();


                string sel11 = "select max(UserID) as UserID from User_master";
                SqlCommand cmd10 = new SqlCommand(sel11, con);

                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);

                DataSet ds10 = new DataSet();
                da10.Fill(ds10);


                string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + ViewState["username"] + "','" + ClsEncDesc.Encrypted(ViewState["password"].ToString()) + "','" + ViewState["deptid"] + "','1','" + ViewState["desgid"] + "','1')";
                SqlCommand cmd9 = new SqlCommand(ins7, con);
                cmd9.Connection = con;
                con.Open();
                cmd9.ExecuteNonQuery();
                con.Close();




                string instrole = "insert into User_Role(User_id,Role_id,ActiveDeactive) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + ViewState["roleid"] + "','1')";
                SqlCommand cmdid = new SqlCommand(instrole, con);
                cmdid.Connection = con;
                con.Open();
                cmdid.ExecuteNonQuery();
                con.Close();

                string str112 = "select max(UserID) as userid from Login_master";
                SqlCommand cmd112 = new SqlCommand(str112, con);

                SqlDataAdapter adp112 = new SqlDataAdapter(cmd112);
                DataTable ds11 = new DataTable();
                adp112.Fill(ds11);

                int id = 0;
                DataTable dtid = new DataTable();

              

                string iopp = "Select Id from AccountMaster where AccountId='" + accid + "' and Whid='" + ViewState["whid"] + "'";
                SqlDataAdapter daiop = new SqlDataAdapter(iopp,con);
                daiop.Fill(dtid);
               
                if (dtid.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dtid.Rows[0]["Id"].ToString());
                }


                string empins = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                        " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Active,WorkPhone,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + ViewState["partyidforemail"] + "','" + ViewState["deptid"] + "', " +
                       " '" + ViewState["desgid"] + "','','1','" + DateTime.Now.ToString() + "','" + txtaddress.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "', " +
                       " '" + txtphone.Text + "','" + txtemail.Text + "','" + id + "','" + accid + "','" + txtname.Text + "','" + ViewState["whid"] + "','1','','','','','','')";

                SqlCommand cmdemp = new SqlCommand(empins, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdemp.ExecuteNonQuery();
                con.Close();

                string str121qq = "select max(EmployeeMasterID) as EmployeeMasterID from EmployeeMaster";
                SqlCommand cmd121qq = new SqlCommand(str121qq, con);

                SqlDataAdapter adp121qq = new SqlDataAdapter(cmd121qq);
                DataTable ds121qq = new DataTable();
                adp121qq.Fill(ds121qq);

                ViewState["emplofff"] = ds121qq.Rows[0]["EmployeeMasterID"].ToString();


                string strware = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + ds121qq.Rows[0]["EmployeeMasterID"] + "','" + ViewState["whid"] + "','1')";
                SqlCommand cmd1ware = new SqlCommand(strware, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1ware.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dacategory = new SqlDataAdapter("select id from CandidateApplicationStatusMaster where PageStatusName='Application received'", con);
                DataTable dtcategory = new DataTable();
                dacategory.Fill(dtcategory);

                if (dtcategory.Rows.Count > 0)
                {
                    ViewState["StatusMaster"] = dtcategory.Rows[0]["id"].ToString();
                }

                string stremp = "Insert into CandidateMaster (PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId,AccountId,AccountNo,CountryId,StateId,City,SuprviserId,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,Whid,Compid,Active,LastName,FirstName,MiddleName,CandidateNumber,DOB,CandidatePhotoPath,Address,EffectiveFrom,ContactNo,MobileNo,Sex) " +
                            " values ('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ViewState["deptid"] + "','" + ViewState["desgid"] + "','" + ViewState["StatusMaster"] + "','','" + id + "','" + accid + "','" + ddlcountry.SelectedValue + "' " +
                             " ,'" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "','','','','','','" + ViewState["whid"] + "','" + ViewState["comid"].ToString() + "','1','" + txtname.Text + "','" + txtname.Text + "','" + txtname.Text + "','','','','" + txtaddress.Text + "','" + DateTime.Now.ToShortDateString() + "','" + txtphone.Text + "','','') ";
                SqlCommand cmdemp11 = new SqlCommand(stremp, con);
                con.Open();
                cmdemp11.ExecuteNonQuery();
                con.Close();


                ViewState["PartyMasterId"] = ds5.Tables[0].Rows[0]["PartyId"].ToString();
                Session["userid"] = ds11.Rows[0]["userid"].ToString();
                Session["username"] = ViewState["username"].ToString();
                //insert partyaddresstbl
                string partyaddress = "Insert into PartyAddressTbl(PartyMasterId,Address,Country,State,City,email,Phone,fax,zipcode,UserId,datetime,AddressActiveStatus) " +
                    " values ('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + txtaddress.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "', " +
                    " '" + txtemail.Text + "','" + txtphone.Text + "','','','" + Session["userid"] + "','" + System.DateTime.Now.ToString() + "','1')";
                SqlCommand cmdpartyaddress = new SqlCommand(partyaddress, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdpartyaddress.ExecuteNonQuery();
                con.Close();
                //end 
                sendmail2();
            }
            pnladd.Visible = false;
            btnadd.Visible = true;
            fillgrid();
            Label17.Text = "Insert Successfully";
            clr();
            }
        catch
        {
            Label17.Text = "Error..";
        }
    }
    public void sendmail()
    {
        //string ss = "SELECT DISTINCT  Priceplancategory.CategoryName, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password AS Pwd, CompanyMaster.Email, CompanyMaster.CompanyName, " +
        //                 "CompanyMaster.ContactPerson, CompanyMaster.Address, StateMasterTbl.StateName, CountryMaster.CountryName, PortalMasterTbl.Id, PortalMasterTbl.ProductId, PortalMasterTbl.PortalName," +
        //                " PortalMasterTbl.DefaultPagename, PortalMasterTbl.LogoPath, PortalMasterTbl.EmailDisplayname, PortalMasterTbl.EmailId, PortalMasterTbl.UserIdtosendmail, PortalMasterTbl.Password, " +
        //                 "PortalMasterTbl.Mailserverurl, PortalMasterTbl.Supportteamemailid, PortalMasterTbl.Supportteamphoneno, PortalMasterTbl.Supportteammanagername, PortalMasterTbl.Portalmarketingwebsitename, " +
        //                 "PortalMasterTbl.Address1, PortalMasterTbl.Address2, PortalMasterTbl.CountryId, PortalMasterTbl.StateId, PortalMasterTbl.City, PortalMasterTbl.Zip, PortalMasterTbl.PhoneNo, PortalMasterTbl.Fax," +
        //                 "PortalMasterTbl.Status, PortalMasterTbl.Supportteamphonenoext, PortalMasterTbl.Tollfree, PortalMasterTbl.Tollfreeext, PortalMasterTbl.CompanyCreationOption, PortalMasterTbl.DatabaseURL, " +
        //                " PortalMasterTbl.DatabaseName, PortalMasterTbl.PortNo, PortalMasterTbl.UserID, PortalMasterTbl.UserPassword, PortalMasterTbl.PortalRunningCompanyID, PortalMasterTbl.Colorportal, " +
        //                 "PricePlanMaster.PricePlanAmount, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanDesc" +
        //                 "  FROM            CompanyMaster INNER JOIN" +
        //                " PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId INNER JOIN" +
        //                " VersionInfoMaster ON VersionInfoMaster.VersionInfoId = PricePlanMaster.VersionInfoMasterId INNER JOIN" +
        //                " ProductMaster ON VersionInfoMaster.ProductId = ProductMaster.ProductId INNER JOIN" +
        //                " ClientMaster ON ProductMaster.ClientMasterId = ClientMaster.ClientMasterId INNER JOIN" +
        //                " Priceplancategory ON Priceplancategory.ID = PricePlanMaster.PriceplancatId INNER JOIN" +
        //                " PortalMasterTbl ON PortalMasterTbl.Id = Priceplancategory.PortalId INNER JOIN" +
        //                " OrderMaster ON CompanyMaster.CompanyLoginId = OrderMaster.CompanyLoginId INNER JOIN" +
        //                " OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId INNER JOIN" +
        //                " StateMasterTbl ON StateMasterTbl.StateId = PortalMasterTbl.StateId INNER JOIN" +
        //                " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId" +
        //                  "  WHERE        (CompanyMaster.CompanyLoginId = 'jobcenter')";
        //SqlCommand cmd = new SqlCommand(ss, PageConn.licenseconn());
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);

        string referencid = "select max(refernceid) from MyRefenceTbl ";
        SqlCommand cmd1 = new SqlCommand(referencid, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        string cand = "select CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,Party_master.Email from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MyRefenceTbl on CandidateMaster.CandidateId=MyRefenceTbl.candidateid where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "'";
        SqlCommand cmd2 = new SqlCommand(cand, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        adp2.Fill(dt2);


        //if (dt.Rows.Count > 0)
        //{
           StringBuilder strplan = new StringBuilder();

            string file = "job-center-logo.jpg";
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Dear " + txtname.Text + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">The following candidate has requested that you provide her/him a reference to use in her/his ijobcenter.com job search.</td></tr> ");
            strplan.Append("</table> ");



            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://www.ijobcenter.com/images/" + dt2.Rows[0]["CandidatePhotoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");

            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Name :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["canname"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Phone No :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["MobileNo"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Email ID :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["Email"].ToString() + "</td></tr>");

            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"></td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">References such as the one requested of you play an instrumental role in a candidate's job search. </td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Please note that you have to configure your account before you can start using this product.</td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">That being said, if you wish oblige this request, please click <a href=http://members.ijobcenter.com/ target=_blank >here</a>, or copy and paste the URL below into your internet browser. Please rest assured that information you choose to provide here will not be shared with the candidate in question.  </td></tr> ");
            strplan.Append("<tr><td align=\"left\"> http://members.ijobcenter.com/ </td></tr> ");
            strplan.Append("</table> ");




            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">With gratitude, </td></tr>  ");
            //string ext = "";
            //string tollfree = "";
            //string tollfreeext = "";

            //if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
            //{
            //    ext = "ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
            //}

            //if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            //{
            //    tollfree = dt.Rows[0]["Tollfree"].ToString();
            //}

            //if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
            //{
            //    tollfreeext = "ext " + dt.Rows[0]["Tollfreeext"].ToString();
            //}


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            strplan.Append("<tr><td align=\"left\">Irene Walsh- Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">JobCenter.com </td></tr> ");
            strplan.Append("<tr><td align=\"left\">079-26462230 ext 221 </td></tr> ");
            strplan.Append("<tr><td align=\"left\">18001800180 ext 222 </td></tr> ");
            strplan.Append("<tr><td align=\"left\">www.ijobcenter.com </td></tr> ");
            strplan.Append("<tr><td align=\"left\">Herrykem House, Nr. Acharya Travels, Nr. Havmore Railway Track </td></tr> ");
            strplan.Append("<tr><td align=\"left\">Ahmedabad Gujarat India 380014</td></tr> ");


            string bodyformate = "" + strplan + "";

            try
            {
                string str = "select [PortalMasterTbl].[EmailId],PortalMasterTbl.EmailDisplayname,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl from [PortalMasterTbl] inner join [CompanyMaster] on [PortalMasterTbl].[ProductId]=[CompanyMaster].[ProductId]  where  dbo.PortalMasterTbl.Id=7";

                SqlDataAdapter da = new SqlDataAdapter(str, PageConn.licenseconn());
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string email = Convert.ToString("info@ijobcenter.com");
                    string displayname = Convert.ToString(dt.Rows[0]["EmailDisplayname"]);
                    string password = Convert.ToString(dt.Rows[0]["Password"]);
                    string outgo = Convert.ToString(dt.Rows[0]["Mailserverurl"]);
                    //string body = TxtBody.Text;
                    //string Subject = TxtSubject.Text;
                    //  mail = "company1@safestmail.net";
                    MailAddress to = new MailAddress(txtemail.Text);
                    MailAddress from = new MailAddress(email, "Admin");
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Reference for candidate";  //Subject.ToString();
                    objEmail.Body = bodyformate;


                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("info@ijobcenter.com", "Om2012++");
                    client.Host = outgo;
                    client.Send(objEmail);
                }
                else
                {
                    // lblmsg.Text = "No Portal Email Available.";
                }
            }
            catch (Exception ex)
            {

                //Notsendmailmail += mail;
            }
          

        
    }

    public void sendmail2()
    {
      

        string referencid = "select max(refernceid) from MyRefenceTbl ";
        SqlCommand cmd1 = new SqlCommand(referencid, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        string cand = "select CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,Party_master.Email from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MyRefenceTbl on CandidateMaster.CandidateId=MyRefenceTbl.candidateid where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "'";
        SqlCommand cmd2 = new SqlCommand(cand, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        adp2.Fill(dt2);


        
            StringBuilder strplan = new StringBuilder();

            string file = "job-center-logo.jpg";
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Dear " + txtname.Text + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">The following candidate has requested that you provide her/him a reference to use in her/his ijobcenter.com job search.</td></tr> ");
            strplan.Append("</table> ");



            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://www.ijobcenter.com/images/" + dt2.Rows[0]["CandidatePhotoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");

            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Name :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["canname"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Phone No :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["MobileNo"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Email ID :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["Email"].ToString() + "</td></tr>");

            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"></td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">References such as the one requested of you play an instrumental role in a candidate's job search. </td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">Please note that you have to configure your account before you can start using this product.</td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
             strplan.Append("<tr><td align=\"left\">Your login information is as under:</td></tr>");
            strplan.Append("<tr><td align=\"left\">Login Information:</td></tr> ");
            strplan.Append("<tr><td align=\"left\">User ID: " + ViewState["username"] + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\">Password: " + ViewState["password"] + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\">That being said, if you wish oblige this request, please click <a href=http://members.ijobcenter.com/ target=_blank >here</a>, or copy and paste the URL below into your internet browser. Please rest assured that information you choose to provide here will not be shared with the candidate in question.  </td></tr> ");
            strplan.Append("<tr><td align=\"left\"> http://members.ijobcenter.com/ </td></tr> ");
            strplan.Append("</table> ");



            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">With gratitude, </td></tr>  ");
           


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");

            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            strplan.Append("<tr><td align=\"left\">Irene Walsh- Support Manager</td></tr> ");
            strplan.Append("<tr><td align=\"left\">JobCenter.com </td></tr> ");
            strplan.Append("<tr><td align=\"left\">079-26462230 ext 221 </td></tr> ");
            strplan.Append("<tr><td align=\"left\">18001800180 ext 222 </td></tr> ");
            strplan.Append("<tr><td align=\"left\">www.ijobcenter.com </td></tr> ");
            strplan.Append("<tr><td align=\"left\">Herrykem House, Nr. Acharya Travels, Nr. Havmore Railway Track </td></tr> ");
            strplan.Append("<tr><td align=\"left\">Ahmedabad Gujarat India 380014</td></tr> ");


            string bodyformate = "" + strplan + "";
           
             try
             {
                 string str = "select [PortalMasterTbl].[EmailId],PortalMasterTbl.EmailDisplayname,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl from [PortalMasterTbl] inner join [CompanyMaster] on [PortalMasterTbl].[ProductId]=[CompanyMaster].[ProductId]  where  dbo.PortalMasterTbl.Id=7";

                 SqlDataAdapter da = new SqlDataAdapter(str, PageConn.licenseconn());
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     string email = Convert.ToString("info@ijobcenter.com");
                     string displayname = Convert.ToString(dt.Rows[0]["EmailDisplayname"]);
                     string password = Convert.ToString(dt.Rows[0]["Password"]);
                     string outgo = Convert.ToString(dt.Rows[0]["Mailserverurl"]);
                    
                     MailAddress to = new MailAddress(txtemail.Text);
                     MailAddress from = new MailAddress(email, "Admin");
                     MailMessage objEmail = new MailMessage(from, to);
                     objEmail.Subject = "Reference for candidate";  //Subject.ToString();
                     objEmail.Body = bodyformate;


                     objEmail.IsBodyHtml = true;
                     objEmail.Priority = MailPriority.High;
                     SmtpClient client = new SmtpClient();
                     client.Credentials = new NetworkCredential("info@ijobcenter.com", "Om2012++");
                     client.Host = outgo;
                     client.Send(objEmail);
                 }
                 else
                 {
                     // lblmsg.Text = "No Portal Email Available.";
                 }
             }
             catch (Exception ex)
             {

                 //Notsendmailmail += mail;
             }
        
    }
    public void fillgrid()
    {

        string fillgrid = "  select distinct refernceid,name,designation,contactno,email,Response from MyRefenceTbl inner join Refernce_inputTbl on Refernce_inputTbl.reference_id=MyRefenceTbl.refernceid  where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "'";
            SqlCommand cmd12 = new SqlCommand(fillgrid, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable dt12 = new DataTable();
            adp12.Fill(dt12);
        
        if (dt12.Rows.Count > 0)
        {
            GridView1.DataSource = dt12;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string id = GridView1.Rows[j].Cells[0].Text;
        string name = GridView1.Rows[j].Cells[1].Text;
        string email = GridView1.Rows[j].Cells[4].Text;
        LinkButton res = (LinkButton)GridView1.Rows[j].FindControl("LinkButton1");
        if (res.Text!= "Yes")
        {
           



            string cand = "select distinct CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,Party_master.Email from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MyRefenceTbl on CandidateMaster.CandidateId=MyRefenceTbl.candidateid where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "'";
            SqlCommand cmd2 = new SqlCommand(cand, con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            adp2.Fill(dt2);


                StringBuilder strplan = new StringBuilder();

                string file = "job-center-logo.jpg";
                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
                strplan.Append("<br></table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Dear " + name + ",</td></tr>  ");
                strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">The following candidate has requested that you provide her/him a reference to use in her/his ijobcenter.com job search.</td></tr> ");
                strplan.Append("</table> ");



                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"> <img src=\"http://www.ijobcenter.com/images/" + dt2.Rows[0]["CandidatePhotoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");

                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Name :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["canname"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Phone No :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["MobileNo"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Email ID :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["Email"].ToString() + "</td></tr>");

                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"></td></tr>  ");
                strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");


                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">References such as the one requested of you play an instrumental role in a candidate's job search. </td></tr> ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Please note that you have to configure your account before you can start using this product.</td></tr> ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">That being said, if you wish oblige this request, please click <a href=http://members.ijobcenter.com/ target=_blank >here</a>, or copy and paste the URL below into your internet browser. Please rest assured that information you choose to provide here will not be shared with the candidate in question.  </td></tr> ");
                strplan.Append("<tr><td align=\"left\"> http://members.ijobcenter.com/ </td></tr> ");
                strplan.Append("</table> ");




                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");

               


                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">With gratitude, </td></tr> ");
                strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

                 string bodyformate = "" + strplan + "";
           
             try
             {
                 string str = "select [PortalMasterTbl].[EmailId],PortalMasterTbl.EmailDisplayname,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl from [PortalMasterTbl] inner join [CompanyMaster] on [PortalMasterTbl].[ProductId]=[CompanyMaster].[ProductId]  where  dbo.PortalMasterTbl.Id=7";

                 SqlDataAdapter da = new SqlDataAdapter(str, PageConn.licenseconn());
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     string mail = Convert.ToString("info@ijobcenter.com");
                     string displayname = Convert.ToString(dt.Rows[0]["EmailDisplayname"]);
                     string password = Convert.ToString(dt.Rows[0]["Password"]);
                     string outgo = Convert.ToString(dt.Rows[0]["Mailserverurl"]);
                     //string body = TxtBody.Text;
                     //string Subject = TxtSubject.Text;
                     //  mail = "company1@safestmail.net";
                     MailAddress to = new MailAddress(txtemail.Text);
                     MailAddress from = new MailAddress(mail, "Admin");
                     MailMessage objEmail = new MailMessage(from, to);
                     objEmail.Subject = "Reference for candidate";  //Subject.ToString();
                     objEmail.Body = bodyformate;


                     objEmail.IsBodyHtml = true;
                     objEmail.Priority = MailPriority.High;
                     SmtpClient client = new SmtpClient();
                     client.Credentials = new NetworkCredential("info@ijobcenter.com", "Om2012++");
                     client.Host = outgo;
                     client.Send(objEmail);
                 }
                 else
                 {
                     // lblmsg.Text = "No Portal Email Available.";
                 }
             }
             catch (Exception ex)
             {

                 //Notsendmailmail += mail;
             }
            
            Label17.Text = "Mail has been sent successfully";
        }
        else
        {
            Label17.Text = "Response Is Received Already From This Referencer";
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string id = GridView1.Rows[j].Cells[0].Text;
        LinkButton res = (LinkButton)GridView1.Rows[j].FindControl("LinkButton1");
        if (res.Text != "Yes")
        {
            Label17.Text = "Delete will not be allowed if a response has been received from the reference.";
        }
        else
        {
            con.Open();
            string del = "delete from  MyRefenceTbl  where refernceid='" + id + "'";
            SqlCommand cmd = new SqlCommand(del,con);
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            Label17.Text = "Deleted Successfully";
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;

            
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue != "1")
        {
            Panel6.Visible = true;
        }
        else
        {
            Panel6.Visible = false;
        }
    }
    
    protected void Button8_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow gr in GridView3.Rows)
        //{
        
            DataTable dt = new DataTable();

            for (int i = 0; i < GridView3.Rows.Count ; i++)
            {
                CheckBox cb = (CheckBox)GridView3.Rows[i].FindControl("CheckBox2");
                if (cb.Checked == true)
                {

                    Label lbl = (Label)GridView3.Rows[i].FindControl("Label20");
                    if (dt.Rows.Count < 1)
                    {
                        dt.Columns.Add("ID");
                    }
                    DataRow dr = dt.NewRow();
                    dr["ID"] = lbl.Text;
                    dt.Rows.Add(dr);
                    ViewState["vacancyid"] = dt;
                    Label37.Visible = false;
                }
            }
        }
    //}
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        fillvacancy();
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EditCandidateIJ.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[3].Visible = false;


        }
    }
    protected void btnyes_Click(object sender, EventArgs e)
    {
        string name = ViewState["name"].ToString();
        txtname.Text = name;
        ModalPopupExtender2.Hide();

    }
    protected void btnno_Click(object sender, EventArgs e)
    {
        txtname.Text = "";
        txtemail.Text = "";
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        txtname.Text = "";
        txtemail.Text = "";
        ModalPopupExtender2.Hide();
    }
}