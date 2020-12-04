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

public partial class EmployeeMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection con_Lice_Job = new SqlConnection(ConfigurationManager.ConnectionStrings["JobcenterLicense"].ConnectionString);
    string accid = "30000";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        if (!IsPostBack)
        {
           fillSupervisor();
            fillDesignation();
            FillddlCountry();
            Fillgrid();
            fillrole();
            filldes();
            fillddlsup();
            ViewState["sortOrder"] = "";
        }
    }
    protected void fillSupervisor()
    {
        string str = "Select SupervisorMaster.* from SupervisorMaster inner join EmployeeMaster on SupervisorMaster.EmployeeId=EmployeeMaster.Id where SupervisorMaster.Active='1' and  EmployeeMaster.ClientId='" + Session["ClientId"] + "' ORDER BY SupervisorMaster.Name ASC ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlsupervisor.DataSource = ds;
        ddlsupervisor.DataTextField = "Name";
        ddlsupervisor.DataValueField = "Id";
        ddlsupervisor.DataBind();
    }
    protected void fillDesignation()
    {
        string str = "select * from DesignationMaster where ClientId='" + Session["ClientId"].ToString() + "' and Active='1' ORDER BY DesignationMaster.Name ASC ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddldesignation.DataSource = ds;
        ddldesignation.DataTextField = "Name";
        ddldesignation.DataValueField = "Id";
        ddldesignation.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string st789 = "select * from EmployeeMaster where  ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + txtusername.Text + "'   ";

        SqlCommand cmd789 = new SqlCommand(st789, con);
        SqlDataAdapter ds789 = new SqlDataAdapter(cmd789);
        DataTable dt789 = new DataTable();
        ds789.Fill(dt789);
        if (dt789.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, This User Id Is Not Available";

        }
        else
        {


            string str1 = "select * from EmployeeMaster where Name='" + txtempname.Text + "'  and ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + txtusername.Text + "'   ";

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

                string SubMenuInsert = "Insert Into EmployeeMaster (Name,FTPServerURL,FTPPort,FTPUserId,FTPPassword,SupervisorId,DesignationId,UserId,Password,Active,ClientId,PhoneNo,PhoneExtension,MobileNo,CountryId,StateId,City,Email,Zipcode,RoleId,EffectiveRate) values ('" + txtempname.Text + "','" + txtftpserverurl.Text + "','" + txtftpport.Text + "','" + txtftpuserid.Text + "','" + PageMgmt.Encrypted(txtftppassword.Text) + "','" + ddlsupervisor.SelectedValue + "','" + ddldesignation.SelectedValue + "','" + txtusername.Text + "','" + PageMgmt.Encrypted(txtpassword.Text) + "','" + CheckBox1.Checked + "','" + Session["ClientId"].ToString() + "','" + txtphoneno.Text + "','" + txtphoneextension.Text + "','" + txtmobileno.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + txtcity.Text + "','" + txtemail.Text + "','" + txtzipcode.Text + "','" + ddlrolename.SelectedValue + "','"+txteffectiverate.Text+"')";
                SqlCommand cmd = new SqlCommand(SubMenuInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                string str2 = "select MAX(Id) as EmpID from EmployeeMaster where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "'";

                SqlCommand cmd2 = new SqlCommand(str2, con);
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);
                int i = 0;
                while (i < ListBox1.Items.Count)
                {
                    ViewState["Id"] = ds2.Tables[0].Rows[0]["EmpID"].ToString(); 

                    string str = "Insert Into Emp_IP (EmpId,IpAddress)values('" + ds2.Tables[0].Rows[0]["EmpID"] + "','" + ListBox1.Items[i].Text + "')";
                    SqlCommand cmd11 = new SqlCommand(str, con);
                    con.Open();
                    cmd11.ExecuteNonQuery();
                    con.Close();
                    i++;
                }
                string ClientInsert = "Insert Into clientLoginMaster (clientId,UserId,Password) values ('" + Session["ClientId"].ToString() + "','" + txtusername.Text + "','" + PageMgmt.Encrypted(txtpassword.Text) + "') ";
                SqlCommand cmd1123 = new SqlCommand(ClientInsert, con);
                con.Open();
                cmd1123.ExecuteNonQuery();
                con.Close();


                con.Close();
                con.Open();
                string insertdatabase = "insert into User_Role (User_id,Role_id,ActiveDeactive)values(" + ViewState["Id"] + "," + ddlrolename.SelectedValue  + ",'1')";
                SqlCommand cmdRole = new SqlCommand(insertdatabase, con);
                cmdRole.ExecuteNonQuery();
                con.Close();


                Button1_ClickSyncronice(sender ,e);//ioffice

                clearall();
                Fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";

                addnewpanel.Visible = true;
                pnladdnew.Visible = false;
                lbllgng.Text = "";
            }
        }
    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con_Lice_Job);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    protected void Button1_ClickSyncronice(object sender, EventArgs e)
    {

        ViewState["whid"] = "1";
        Session["comid"] = "jobcenter";
        ViewState["comid"] = "jobcenter";
        string str12 = "select * from Syncr_LicenseEmployee_With_JobcenterId where Jobcenter_Emp_id='" + ViewState["Id"] + "'";
        SqlCommand cmd1 = new SqlCommand(str12, con_Lice_Job);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            
        }
        else
        {

            string strgetusername = "select max(PartyID) as PartyID from Party_master ";
            SqlCommand cmdusername = new SqlCommand(strgetusername, con_Lice_Job);
            SqlDataAdapter adpusername = new SqlDataAdapter(cmdusername);
            DataTable dsusername = new DataTable();
            adpusername.Fill(dsusername);

            if (dsusername.Rows.Count > 0)
            {
                string username;
                string Password;
                int i = 0;
                int c = dsusername.Rows.Count + 1;
                username = "Candidate" + c; ;
                Password = "Candidate" + dsusername.Rows[0]["PartyID"].ToString() + "++" + c;


                tbUserName.Text = username;
                tbPassword.Text = Password;

            }
            int flag = 0;
            string strusernaemchk = " select * from User_master where Username='" + tbUserName.Text + "'";
            SqlCommand cmdusernaemchk = new SqlCommand(strusernaemchk, con_Lice_Job);
            SqlDataAdapter adpusernaemchk = new SqlDataAdapter(cmdusernaemchk);
            DataTable dsusernaemchk = new DataTable();
            adpusernaemchk.Fill(dsusernaemchk);

            Session["maxaid"] = "";

            string st153 = "";

            string st1531 = "";
            Session["reportid"] = "";
            Session["reportid1"] = "";
            ViewState["balid"] = "";

            string ins1 = "";
            DataTable datparcat = select("select PartyMasterCategoryNo from PartyMasterCategory where Name='Candidate'");

            if (datparcat.Rows.Count > 0)
            {
                DataTable dtpartytype = select("select PartyTypeId from PartytTypeMaster where compid='" + ViewState["comid"] + "' and PartyCategoryId='" + datparcat.Rows[0]["PartyMasterCategoryNo"].ToString() + "'");
                ViewState["partytypeid"] = dtpartytype.Rows[0]["PartyTypeId"].ToString();
            }


            ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID, " +
                       " PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId,  AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID," +
                       " AccountBalanceLimitId,id,Whid,PartyTypeCategoryNo,Zipcode) " +
                       " values ( '" + accid + "','" + txtempname.Text + "','', '','26551','220','222','',  ''  ,'', " +
                        " '" + txtemail.Text + "','" + txtmobileno.Text + "','1', " +
                        " '" + ViewState["partytypeid"] + "' ,'" + 0 + "' ,'" + 0 + "',  '" + 0 + "' , '" + 0 + "' , '" + 0 + "' ,'1' , '' ,'1', " +
                        "'" + ViewState["balid"] + "','" + ViewState["comid"] + "','" + ViewState["whid"] + "','" + datparcat.Rows[0]["PartyMasterCategoryNo"].ToString() + "','" + txtzipcode.Text + "')";


            SqlCommand cmd3 = new SqlCommand(ins1, con_Lice_Job);

            con_Lice_Job.Open();
            cmd3.ExecuteNonQuery();
            con_Lice_Job.Close();

            string sel = "";

            sel = "select max(PartyID) as PartyID from Party_master where Id='" + ViewState["comid"].ToString() + "' and Whid='" + ViewState["whid"] + "'";


            SqlCommand cmd5 = new SqlCommand(sel, con_Lice_Job);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);

            string phofile = "";

            ViewState["partyidforemail"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString());


            string insdept = "";


            insdept = "Select id from DepartmentmasterMNC where Departmentname='Other' and Whid='" + ViewState["whid"] + "'";


            SqlDataAdapter dadept = new SqlDataAdapter(insdept, con_Lice_Job);
            DataTable dtdept = new DataTable();
            dadept.Fill(dtdept);

            object dept = "";

            if (dtdept.Rows.Count > 0)
            {
                ViewState["deptid"] = Convert.ToString(dtdept.Rows[0]["id"]);
            }

            else
            {
                SqlCommand cmddept = new SqlCommand("DeptRetIdentity", con_Lice_Job);
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

                if (con_Lice_Job.State.ToString() != "Open")
                {
                    con_Lice_Job.Open();
                }
                dept = (object)cmddept.ExecuteNonQuery();
                dept = cmddept.Parameters["@Id"].Value;
                ViewState["deptid"] = dept;
                con_Lice_Job.Close();
            }


            SqlDataAdapter darole = new SqlDataAdapter("Select Role_id from RoleMaster where Role_name='Candidate' and compid='" + ViewState["comid"].ToString() + "'", con_Lice_Job);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            if (dtrole.Rows.Count > 0)
            {
                ViewState["roleid"] = Convert.ToString(dtrole.Rows[0]["Role_id"]);
            }
            else
            {
                SqlCommand cmd3r = new SqlCommand("Insert into RoleMaster (Role_name,ActiveDeactive,compid) values ('Candidate','True','" + ViewState["comid"].ToString() + "')", con_Lice_Job);
                if (con_Lice_Job.State.ToString() != "Open")
                {
                    con_Lice_Job.Open();
                }
                cmd3r.ExecuteNonQuery();
                con_Lice_Job.Close();

                string strd = "SELECT  Role_id from RoleMaster WHERE Role_name = 'Candidate' and compid='" + ViewState["comid"].ToString() + "'";
                SqlDataAdapter adrol = new SqlDataAdapter(strd, con_Lice_Job);
                DataTable dtrol = new DataTable();
                adrol.Fill(dtrol);

                ViewState["roleid"] = Convert.ToString(dtrol.Rows[0]["Role_id"]);
            }

            string strdesg = "SELECT  DesignationMasterId from DesignationMaster WHERE DesignationName = 'Candidate' and DeptID='" + ViewState["deptid"] + "'";
            SqlDataAdapter dadesg = new SqlDataAdapter(strdesg, con_Lice_Job);
            DataTable dtdesg = new DataTable();
            dadesg.Fill(dtdesg);

            if (dtdesg.Rows.Count > 0)
            {
                ViewState["desgid"] = Convert.ToString(dtdesg.Rows[0]["DesignationMasterId"]);
            }

            else
            {
                SqlCommand cmddegdes = new SqlCommand("Insert into DesignationMaster(DesignationName,DeptID,RoleId) values ('Candidate','" + ViewState["deptid"] + "','" + ViewState["roleid"] + "')", con_Lice_Job);
                if (con_Lice_Job.State.ToString() != "Open")
                {
                    con_Lice_Job.Open();
                }
                cmddegdes.ExecuteNonQuery();
                con_Lice_Job.Close();

                string strdesg1 = "SELECT  DesignationMasterId from DesignationMaster WHERE DesignationName = 'Candidate' and DeptID='" + ViewState["deptid"] + "'";
                SqlDataAdapter dadesg1 = new SqlDataAdapter(strdesg1, con_Lice_Job);
                DataTable dtdesg1 = new DataTable();
                dadesg1.Fill(dtdesg1);

                ViewState["desgid"] = Convert.ToString(dtdesg1.Rows[0]["DesignationMasterId"]);
            }

            string strrights = "";



            strrights = "insert into MessageCenterRightsTbl([CompanyID],[BusinessID],[DesignationID],[Business],[AdminRights],[Candidate],[Employee],[Customer],[Vendor],[Others],[Visitor]) values('" + ViewState["comid"].ToString() + "','" + ViewState["whid"] + "','" + ViewState["desgid"].ToString() + "','0','1','0','0','0','0','0','0')";


            SqlCommand cmdrigh = new SqlCommand(strrights, con_Lice_Job);
            if (con_Lice_Job.State.ToString() != "Open")
            {
                con_Lice_Job.Open();
            }
            cmdrigh.ExecuteNonQuery();
            con_Lice_Job.Close();


            SqlDataAdapter daprice = new SqlDataAdapter("select PricePlanId from CompanyMaster where CompanyLoginId='" + ViewState["comid"].ToString() + "'", PageConn.licenseconn());
            DataTable dtprice = new DataTable();
            daprice.Fill(dtprice);

            if (dtprice.Rows.Count > 0)
            {
                ViewState["priceid"] = Convert.ToString(dtprice.Rows[0]["PricePlanId"]);
            }




            string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode)" +
                                                     " values ('" + txtempname.Text + "','','','','','" + txtphoneno.Text + "','" + txtemail.Text + "','" + tbUserName.Text + "','" + ViewState["deptid"] + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ViewState["desgid"] + "','" + phofile + "' ,'1','','" + txtzipcode.Text + "')";
            SqlCommand cmd6 = new SqlCommand(ins6, con_Lice_Job);

            con_Lice_Job.Open();
            cmd6.ExecuteNonQuery();
            con_Lice_Job.Close();


            string sel11 = "select max(UserID) as UserID from User_master";
            SqlCommand cmd10 = new SqlCommand(sel11, con_Lice_Job);

            SqlDataAdapter da10 = new SqlDataAdapter(cmd10);

            DataSet ds10 = new DataSet();
            da10.Fill(ds10);


            string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + tbUserName.Text + "','" + ClsEncDesc.Encrypted(tbPassword.Text) + "','" + ViewState["deptid"] + "','1','" + ViewState["desgid"] + "','1')";
            SqlCommand cmd9 = new SqlCommand(ins7, con_Lice_Job);
            cmd9.Connection = con_Lice_Job;
            con_Lice_Job.Open();
            cmd9.ExecuteNonQuery();
            con_Lice_Job.Close();

            ViewState["username"] = tbUserName.Text;
            ViewState["password"] = tbPassword.Text;


            string instrole = "insert into User_Role(User_id,Role_id,ActiveDeactive) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + ViewState["roleid"] + "','1')";
            SqlCommand cmdid = new SqlCommand(instrole, con_Lice_Job);
            cmdid.Connection = con_Lice_Job;
            con_Lice_Job.Open();
            cmdid.ExecuteNonQuery();
            con_Lice_Job.Close();

            string str112 = "select max(UserID) as userid from Login_master";
            SqlCommand cmd11 = new SqlCommand(str112, con_Lice_Job);

            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable ds11 = new DataTable();
            adp11.Fill(ds11);

            int id = 0;
            DataTable dtid = new DataTable();

            string iopp = "";
            id = 1;
            string empins = "";


            empins = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Active,WorkPhone,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) " +
                " values('" + ViewState["partyidforemail"] + "','" + ViewState["deptid"] + "', " +
               " '" + ViewState["desgid"] + "','','1','" + DateTime.Today + "','','','','', " +
               " '" + txtmobileno.Text + "','" + txtemail.Text + "','" + id + "','" + accid + "','" + txtempname.Text + "','" + ViewState["whid"] + "','1','','','','','" + ViewState["Jobpositionid"] + "','1')";

            SqlCommand cmdemp = new SqlCommand(empins, con_Lice_Job);
            if (con_Lice_Job.State.ToString() != "Open")
            {
                con_Lice_Job.Open();
            }
            cmdemp.ExecuteNonQuery();
            con_Lice_Job.Close();

            string str121qq = "select max(EmployeeMasterID) as EmployeeMasterID from EmployeeMaster";
            SqlCommand cmd121qq = new SqlCommand(str121qq, con_Lice_Job);

            SqlDataAdapter adp121qq = new SqlDataAdapter(cmd121qq);
            DataTable ds121qq = new DataTable();
            adp121qq.Fill(ds121qq);

            ViewState["emplofff"] = ds121qq.Rows[0]["EmployeeMasterID"].ToString();

            string strware = "";
            strware = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + ds121qq.Rows[0]["EmployeeMasterID"] + "','" + ViewState["whid"] + "','1')";

            SqlCommand cmd1ware = new SqlCommand(strware, con_Lice_Job);
            if (con_Lice_Job.State.ToString() != "Open")
            {
                con_Lice_Job.Open();
            }
            cmd1ware.ExecuteNonQuery();
            con_Lice_Job.Close();

            string stremp = "";

            ViewState["EducationqualificationID"] = "1007";

            ViewState["SpecialSubjectID"] = "41";

            ViewState["Jobpositionid"] = "4";


            Session["phofile"] = "";
            phofile = Convert.ToString(Session["phofile"]);

            SqlDataAdapter dacategory = new SqlDataAdapter("select id from CandidateApplicationStatusMaster where PageStatusName='Application received'", con_Lice_Job);
            DataTable dtcategory = new DataTable();
            dacategory.Fill(dtcategory);

            if (dtcategory.Rows.Count > 0)
            {
                ViewState["StatusMaster"] = dtcategory.Rows[0]["id"].ToString();
            }
            //
            string photopath;

            photopath = "CandidateMale.png";
            stremp = "Insert into CandidateMaster (PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId,AccountId,AccountNo,CountryId,StateId,City,SuprviserId,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,Whid,Compid,Active,LastName,FirstName,MiddleName,CandidateNumber,DOB,CandidatePhotoPath,Address,EffectiveFrom,ContactNo,MobileNo,Sex) " +
                     " values ('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ViewState["deptid"] + "','" + ViewState["desgid"] + "','" + ViewState["StatusMaster"] + "','" + "" + "','" + id + "','" + accid + "','' " +
                      " ,'','','" + "" + "','" + ViewState["EducationqualificationID"].ToString() + "','" + ViewState["SpecialSubjectID"].ToString() + "','0','" + ViewState["Jobpositionid"].ToString() + "','" + ViewState["whid"] + "','" + ViewState["comid"].ToString() + "','1','" + txtempname.Text + "','','','" + Convert.ToString(Session["CandidateNumber"]) + "','" + "" + "','" + photopath + "','','" + DateTime.Now.ToShortDateString() + "','" + txtmobileno.Text + "','','1') ";


            SqlCommand cmdemp11 = new SqlCommand(stremp, con_Lice_Job);
            con_Lice_Job.Open();
            cmdemp11.ExecuteNonQuery();
            con_Lice_Job.Close();


            ViewState["PartyMasterId"] = ds5.Tables[0].Rows[0]["PartyId"].ToString();
            Session["userid"] = ds11.Rows[0]["userid"].ToString();
            Session["username"] = tbUserName.Text;


            string str121 = "select max(CandidateId) as EmployeeMasterID from CandidateMaster";
            SqlCommand cmd121 = new SqlCommand(str121, con_Lice_Job);

            SqlDataAdapter adp121 = new SqlDataAdapter(cmd121);
            DataTable ds121 = new DataTable();
            adp121.Fill(ds121);


            if (ds121.Rows.Count > 0)
            {
                ViewState["EmerEMID"] = ds121.Rows[0]["EmployeeMasterID"].ToString();
            }
            string insert = "";


            insert = "insert into CandidateforFranchiseTBL(CandidateMasterID,RegisteringFranchiseeID,CandidatesPrimaryFranchiseeID,DateandTime,Active)values('" + ViewState["EmerEMID"].ToString() + "','jobcenter','jobcenter','" + DateTime.Now.ToShortDateString() + "','Active')";


            SqlCommand ni = new SqlCommand(insert, con_Lice_Job);
            if (con_Lice_Job.State.ToString() != "Open")
            {
                con_Lice_Job.Open();
            }
            ni.ExecuteNonQuery();
            con_Lice_Job.Close();


            string insertSyncr = "insert into Syncr_LicenseEmployee_With_JobcenterId(License_Emp_id,Jobcenter_Emp_id)values('" + ViewState["Id"].ToString() +"','" + ViewState["emplofff"].ToString() + "')";
          SqlCommand nisync = new SqlCommand(insertSyncr, con_Lice_Job);
          if (con_Lice_Job.State.ToString() != "Open")
            {
                con_Lice_Job.Open();
            }
            nisync.ExecuteNonQuery();
            con_Lice_Job.Close();




           

            // sendmailtoadminforapproval();
            lblmsg.Text = " sucessfully registered " + tbUserName.Text + "-" + tbPassword.Text;
            lblmsg.Visible = true;
            //clear();

        }
        

                        
                    
              


    }
    protected void Fillgrid()
    {
        //string str = " select * from EmployeeMaster where ClientId='" + Session["ClientId"].ToString() + "'"; inner join SupervisorMaster on SupervisorMaster.Id=EmployeeMaster.SupervisorId left outer join DesignationMaster on DesignationMaster.Id=EmployeeMaster.DesignationId 

        
        
        string str = "select EmployeeMaster.*,SupervisorMaster.Name as SupervisorName,DesignationMaster.Name as DesignationName,RoleMaster.Role_name from EmployeeMaster  LEFT OUTER JOIN SupervisorMaster ON SupervisorMaster.Id = EmployeeMaster.SupervisorId LEFT OUTER JOIN RoleMaster ON EmployeeMaster.RoleId = RoleMaster.Role_id LEFT OUTER JOIN DesignationMaster ON DesignationMaster.Id = EmployeeMaster.DesignationId where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "'";


        if (ddldes.SelectedValue.ToString() == "All")
        { }
        else
        {
            str += " AND EmployeeMaster.DesignationId =" + ddldes.SelectedValue;
        }

        if (ddlsup.SelectedValue.ToString() == "All")
        { }
        else
        {
            str += " AND EmployeeMaster.SupervisorId =" + ddlsup.SelectedValue;
        }

        if (ddlactive.SelectedValue.ToString() == "All")
        { }
        else
        {
            str += " AND EmployeeMaster.Active=" + ddlactive.SelectedValue;
        }

        

        str += " ORDER BY EmployeeMaster.Id DESC";

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

    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();
        lblmsg.Visible = false;
        lblmsg.Text = "";
        btnEdit.Visible = false;

        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbllgng.Text = "";
        Button1.Visible = true;
        Button3.Visible = false;

    }
    protected void clearall()
    {
        ddlsupervisor.SelectedIndex = 0;
        ddldesignation.SelectedIndex = 0;
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlrolename.SelectedIndex = 0;
        txtempname.Text = "";
        txtusername.Text = "";
        // txtpassword.Text = "";

        txtpassword.Attributes.Clear();
        txtftpserverurl.Text = "";
        txtftpport.Text = "";
        txtftpuserid.Text = "";
        //txtftppassword.Text = "";
        txtftppassword.Attributes.Clear();
        txtphoneno.Text = "";
        txtphoneextension.Text = "";
        txtmobileno.Text = "";
        txtcity.Text = "";
        txtemail.Text = "";
        txtzipcode.Text = "";
        TextBox1.Text = "";
        ListBox1.Items.Clear();
    }
    protected void FillddlCountry()
    {
        string strcountry = "SELECT CountryId,CountryName,Country_Code FROM CountryMaster";
        SqlCommand cmdcountry = new SqlCommand(strcountry, con);
        DataTable dtcountry = new DataTable();
        SqlDataAdapter adpcountry = new SqlDataAdapter(cmdcountry);
        adpcountry.Fill(dtcountry);
        ddlcountry.DataSource = dtcountry;
        ddlcountry.DataTextField = "CountryName";
        ddlcountry.DataValueField = "CountryId";
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
    }
    protected void FillddlState()
    {

        string strstate = "SELECT * FROM StateMasterTbl where CountryId =" + Convert.ToInt32(ddlcountry.SelectedValue) + " ";
        SqlCommand cmdstate = new SqlCommand(strstate, con);
        DataTable dtstate = new DataTable();
        SqlDataAdapter adpstate = new SqlDataAdapter(cmdstate);
        adpstate.Fill(dtstate);
        ddlstate.DataSource = dtstate;
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "-Select-");
        ddlstate.Items[0].Value = "0";
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlState();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from EmployeeMaster where Id='" + ViewState["DID"] + "' ";
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        if (e.CommandName == "vi")
        {
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lblmsg.Text = "";
            lbllgng.Text = "Edit Employee";

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedDataKey.Value;
            btnEdit.Text = "Edit";
            btnEdit.Visible = true;
            Button3.Visible = true;
            Button1.Visible = false;

            string str1212 = "select * from Syncr_LicenseEmployee_With_JobcenterId where Jobcenter_Emp_id='" + ViewState["Id"] + "'";
            SqlCommand cmd112 = new SqlCommand(str1212, con_Lice_Job);
            SqlDataAdapter adp1212 = new SqlDataAdapter(cmd112);
            DataTable ds1212 = new DataTable();
            adp1212.Fill(ds1212);
            if (ds1212.Rows.Count > 0)
            {
                Btn_sycronce.Visible = false;
            }
            else
            {
                Btn_sycronce.Visible = true;
            }
            string str12 = "select * from EmployeeMaster where Id='" + ViewState["Id"] + "'";
            SqlCommand cmd1 = new SqlCommand(str12, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            adp1.Fill(ds1);


            string str122 = "select * from Emp_IP where EmpId='" + ViewState["Id"] + "'";
            SqlCommand cmd12 = new SqlCommand(str122, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            int i = 0;
            //for(int j=0;j< ds12.Rows.Count;j++)
            //{
            while (i < ds12.Rows.Count)
            {
                ListBox1.Items.Add(ds12.Rows[i]["IpAddress"].ToString());
                //string str = "Insert Into Emp_IP (EmpId,IpAddress)values('" + ds2.Tables[0].Rows[0]["EmpID"] + "','"+ListBox1.Items[i].Text+"')";
                //  SqlCommand cmd11 = new SqlCommand(str, con);
                //  con.Open();
                //  cmd11.ExecuteNonQuery();
                //  con.Close();
                //  i++;
                i++;
            }
            //}


            fillSupervisor();

            ddlsupervisor.SelectedIndex = ddlsupervisor.Items.IndexOf(ddlsupervisor.Items.FindByValue(ds1.Rows[0]["SupervisorId"].ToString()));

            fillDesignation();
            ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(ds1.Rows[0]["DesignationId"].ToString()));

            FillddlCountry();
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(ds1.Rows[0]["CountryId"].ToString()));

            FillddlState();
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(ds1.Rows[0]["StateId"].ToString()));

            fillrole();
            ddlrolename.SelectedIndex = ddlrolename.Items.IndexOf(ddlrolename.Items.FindByValue(ds1.Rows[0]["RoleId"].ToString()));


            txtempname.Text = ds1.Rows[0]["Name"].ToString();

            txtusername.Text = ds1.Rows[0]["UserId"].ToString();
            ViewState["UserId"] = txtusername.Text;

            string strqa = PageMgmt.Decrypted(ds1.Rows[0]["Password"].ToString());
            txtpassword.Attributes.Add("Value", strqa);

            txtftpserverurl.Text = ds1.Rows[0]["FTPServerURL"].ToString();
            txtftpport.Text = ds1.Rows[0]["FTPPort"].ToString();
            txtftpuserid.Text = ds1.Rows[0]["FTPUserId"].ToString();

            string strqa1 = PageMgmt.Decrypted(ds1.Rows[0]["FTPPassword"].ToString());
            txtftppassword.Attributes.Add("Value", strqa1);

            txtphoneno.Text = ds1.Rows[0]["PhoneNo"].ToString();
            txtphoneextension.Text = ds1.Rows[0]["PhoneExtension"].ToString();
            txtmobileno.Text = ds1.Rows[0]["MobileNo"].ToString();
            txtcity.Text = ds1.Rows[0]["City"].ToString();
            txtemail.Text = ds1.Rows[0]["Email"].ToString();
            txtzipcode.Text = ds1.Rows[0]["Zipcode"].ToString();
            CheckBox1.Checked = Convert.ToBoolean(ds1.Rows[0]["Active"].ToString());
            txteffectiverate.Text = ds1.Rows[0]["EffectiveRate"].ToString();










        }
        if (e.CommandName == "Delete")
        {
            ViewState["DID"] = Convert.ToInt32(e.CommandArgument);
        }

    }
    protected void fillrole()
    {
        string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + Session["ClientId"].ToString() + "' order by Role_name";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();

        darole.Fill(dtrole);
        ddlrolename.DataSource = dtrole;
        ddlrolename.DataTextField = "Role_name";
        ddlrolename.DataValueField = "Role_id";
        ddlrolename.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string st789 = "select * from EmployeeMaster where  ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + txtusername.Text + "'  and Id<> '" + ViewState["Id"] + "' ";

        SqlCommand cmd789 = new SqlCommand(st789, con);
        SqlDataAdapter ds789 = new SqlDataAdapter(cmd789);
        DataTable dt789 = new DataTable();
        ds789.Fill(dt789);
        if (dt789.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, This UserName Is Not Available";

        }
        else
        {
            Button3.Visible = false;
            Button1.Visible = true;

            string str1 = "select * from EmployeeMaster where Name='" + txtempname.Text + "'  and ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + txtusername.Text + "' and Id<> '" + ViewState["Id"] + "'   ";

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






                string sr51 = ("update EmployeeMaster set Name='" + txtempname.Text + "',FTPServerURL='" + txtftpserverurl.Text + "' ,FTPPort='" + txtftpport.Text + "',FTPUserId='" + txtftpuserid.Text + "',FTPPassword='" + PageMgmt.Encrypted(txtftppassword.Text) + "',SupervisorId='" + ddlsupervisor.SelectedValue + "',DesignationId='" + ddldesignation.SelectedValue + "',UserId='" + txtusername.Text + "',Password='" + PageMgmt.Encrypted(txtpassword.Text) + "',Active='" + CheckBox1.Checked + "',PhoneNo='" + txtphoneno.Text + "',PhoneExtension='" + txtphoneextension.Text + "',MobileNo='" + txtmobileno.Text + "',CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstate.SelectedValue + "',City='" + txtcity.Text + "',Email='" + txtemail.Text + "',Zipcode='" + txtzipcode.Text + "',RoleId='" + ddlrolename.SelectedValue + "',EffectiveRate='"+txteffectiverate.Text+"' where Id= '" + ViewState["Id"] + "' ");
                SqlCommand cmd801 = new SqlCommand(sr51, con);
                con.Open();
                cmd801.ExecuteNonQuery();
                con.Close();

                int i = 0;


                string str11 = "select * from Emp_IP where EmpId='" + ViewState["Id"] + "' ";
                SqlCommand cmd11 = new SqlCommand(str11, con);
                SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                DataTable dt11 = new DataTable();
                da11.Fill(dt11);

                while (i < ListBox1.Items.Count)
                {
                    if (i < dt11.Rows.Count)
                    {
                        if (dt11.Rows[i]["Id"] != "0" || dt11.Rows[i]["Id"] != null)
                        {
                            string str = "Update Emp_IP Set EmpId='" + ViewState["Id"] + "',IpAddress='" + ListBox1.Items[i].Text + "' where Id='" + dt11.Rows[i]["Id"] + "'";
                            SqlCommand cmd112 = new SqlCommand(str, con);
                            con.Open();
                            cmd112.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        string str = "Insert Into Emp_IP (EmpId,IpAddress)values('" + ViewState["Id"] + "','" + ListBox1.Items[i].Text + "')";
                        SqlCommand cmd112 = new SqlCommand(str, con);
                        con.Open();
                        cmd112.ExecuteNonQuery();
                        con.Close();
                    }
                    i++;


                }

                
                
                string str123 = ("update clientLoginMaster set UserId='" + txtusername.Text + "',Password='" + PageMgmt.Encrypted(txtpassword.Text) + "' where UserId='" + ViewState["UserId"].ToString() + "' and clientId='" + Session["ClientId"].ToString() + "' ");
                SqlCommand cmd123 = new SqlCommand(str123, con);
                con.Open();
                cmd123.ExecuteNonQuery();
                con.Close();

                con.Close();
                con.Open();
                string insertdatabase = " Update User_Role SET  Role_id=" + ddlrolename.SelectedValue + " Where User_id='" + ViewState["Id"].ToString() + "' ";
                SqlCommand cmdRole = new SqlCommand(insertdatabase, con);
                cmdRole.ExecuteNonQuery();
                con.Close();

                
                GridView1.EditIndex = -1;
                Fillgrid();

                clearall();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                btnEdit.Visible = false;

                addnewpanel.Visible = true;
                pnladdnew.Visible = false;
                lbllgng.Text = "";
            }
        }


    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string st789 = "select * from EmployeeMaster where  ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + txtusername.Text + "'   ";

        SqlCommand cmd789 = new SqlCommand(st789, con);
        SqlDataAdapter ds789 = new SqlDataAdapter(cmd789);
        DataTable dt789 = new DataTable();
        ds789.Fill(dt789);
        if (dt789.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, This UserName Is Not Available";

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "UserName Is  Available";
        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Add(TextBox1.Text);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ListBox1.Items.Remove(ListBox1.SelectedItem.Text);
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {


        if (btnEdit.Text == "Edit")
        {
            TextBox1.Text = ListBox1.SelectedItem.Text;

        }
        if (btnEdit.Text == "Update")
        {
            ListBox1.SelectedItem.Text = TextBox1.Text;
            //btnEdit.Text = "Edit";
        }

        if (btnEdit.Text == "Update")
        {
            btnEdit.Text = "Edit";
        }
        else
        {
            btnEdit.Text = "Update";
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
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[9].Visible = false;
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
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }




        }
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
        lbllgng.Text = "Add New Employee";
    }


    protected void filldes()
    {
        string dept = "select * from DesignationMaster where ClientId='" + Session["ClientId"].ToString() + "' and Active='1' ORDER BY DesignationMaster.Name ASC "; 
        SqlCommand cmddept = new SqlCommand(dept, con);
        SqlDataAdapter sadept = new SqlDataAdapter(cmddept);
        DataTable dtdept = new DataTable();

        sadept.Fill(dtdept);
        ddldes.DataSource = dtdept;
        ddldes.DataTextField = "Name";
        ddldes.DataValueField = "Id";
        ddldes.DataBind();
        //ddldes.Items.Insert(0, "All");
    }

    protected void fillddlsup()
    {
        string emprole = "Select SupervisorMaster.* from SupervisorMaster inner join EmployeeMaster on SupervisorMaster.EmployeeId=EmployeeMaster.Id where SupervisorMaster.Active='1' and  EmployeeMaster.ClientId='" + Session["ClientId"] + "' ORDER BY SupervisorMaster.Name ASC "; 
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();

        darole.Fill(dtrole);
        ddlsup.DataSource = dtrole;
        ddlsup.DataTextField = "Name";
        ddlsup.DataValueField = "Id";
        ddlsup.DataBind();
       // ddlsup.Items.Insert(0,"All");
    }
    protected void ddldes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void ddlsup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void ddlactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        string str = "select EmployeeMaster.*,SupervisorMaster.Name as SupervisorName,DesignationMaster.Name as DesignationName,RoleMaster.Role_name from EmployeeMaster  INNER JOIN SupervisorMaster ON SupervisorMaster.Id = EmployeeMaster.SupervisorId INNER JOIN RoleMaster ON EmployeeMaster.RoleId = RoleMaster.Role_id LEFT OUTER JOIN DesignationMaster ON DesignationMaster.Id = EmployeeMaster.DesignationId where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "' and EmployeeMaster.Name like '%" + txtsearch.Text + "%'";

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
}
