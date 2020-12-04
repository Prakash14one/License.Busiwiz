using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for PageMgmt
/// </summary>
public class PageMgmt
{
    SqlConnection con;
    SqlConnection con11;
    SqlConnection con1;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;
    SqlDataReader dr;

	public PageMgmt()
	{
        con11 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ToString());

        con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        //con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString_ifilecabinate1"].ConnectionString);
		
	}

    public DataTable fillPageName(string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageId,PageName FROM PageMaster left outer join Plan_page_Access on  Plan_page_Access.Page_id=PageMaster.PageId  order by PageName";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable fillPageName1()
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageId,PageName FROM PageMaster left outer join Plan_page_Access on  Plan_page_Access.Page_id=PageMaster.PageId   order by PageName";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable fillcontrltype()
    {

        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT Type_id,Type_name FROM Control_type_Master ORDER BY Type_name";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }
    public void insertPagecontrolmaster(int Page_id, String ControlName, int Active, int typeid1)
    {
        
            con.Close();
            con.Open();
            cmd = new SqlCommand("InsertPageControlMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Page_id",Page_id );
            cmd.Parameters.AddWithValue("@ControlName", ControlName);
            cmd.Parameters.AddWithValue("@ControlType_id", typeid1);
            if (Active == 1)
            {
                cmd.Parameters.AddWithValue("@ActiveDeactive", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ActiveDeactive", false);
            }

            cmd.ExecuteNonQuery();
           
           
            con.Close();
            
       
    }

    public void insertplancontrolmaster(int Page_id, int Plan_Id, int ControlMasterId, int Active, int typeid1)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("InsertPageControlMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        //.Parameters.AddWithValue("@Plan_Id", Plan_Id);
        cmd.Parameters.AddWithValue("@ControlMasterId", ControlMasterId);
        cmd.Parameters.AddWithValue("@ControlType_id", typeid1);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }
    //public void updatePagecontrolmaster(int Page_id, int ControlMasterId, int Active, int Plan_Id, int typeid)
    //{

    //    con.Close();
    //    con.Open();
    //    cmd = new SqlCommand("UpdatePageControlMaster", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Page_id", Page_id);
    //    cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
    //    cmd.Parameters.AddWithValue("@ControlMasterId", ControlMasterId);
    //    cmd.Parameters.AddWithValue("@ControlType_id", typeid);
    //    if (Active == 1)
    //    {
    //        cmd.Parameters.AddWithValue("@ActiveDeactive", true);
    //    }
    //    else
    //    {
    //        cmd.Parameters.AddWithValue("@ActiveDeactive", false);
    //    }
    //  //  cmd.Parameters.AddWithValue("@id", id);
    //    cmd.ExecuteNonQuery();


    //    con.Close();


    //}
    public void updatePagecontrolmaster(int Page_id, String ControlName, int Active, int typeid1)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("UpdatePageControlMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        //cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
        //cmd.Parameters.AddWithValue("@ControlMasterId", ControlMasterId);
        cmd.Parameters.AddWithValue("@ControlName", ControlName);

        cmd.Parameters.AddWithValue("@ControlType_id", typeid1);
        int active = Active;
        //  if (Active == 1)
        if (active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }
        //  cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();


        con.Close();


    }




    public DataTable selectGriddata(string compid)
    {

        con.Close();
        con.Open();
        string k1;
       // k1 = "SELECT PageControlMaster.PageControl_id , PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId";
        //k1 = "SELECT PageControlMaster.PageControl_id, PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,Control_type_Master.Type_name FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where PageMaster.Compid='"+compid+"'";
        k1 = "SELECT PageControlMaster.PageControl_id, PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,Control_type_Master.Type_name FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id ";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;


     
  
    }
    public DataTable selectGriddata1()
    {

        con.Close();
        con.Open();
        string k1;
        // k1 = "SELECT PageControlMaster.PageControl_id , PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId";
        //k1 = "SELECT PageControlMaster.PageControl_id, PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,Control_type_Master.Type_name FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id where PageMaster.Compid='"+compid+"'";
        k1 = "SELECT PageControlMaster.PageControl_id,PageControlMaster.ControlTitle, PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,Control_type_Master.Type_name FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId INNER JOIN Control_type_Master ON PageControlMaster.ControlType_id = Control_type_Master.Type_id ";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;




    }

    public SqlDataReader SelectPageControls(int id,string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageControlMaster.PageControl_id , PageControlMaster.ControlName, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,PageControlMaster.ControlType_id FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId where PageControlMaster.PageControl_id ="+id+"";

        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();
        
        return dr;

    }
    public SqlDataReader SelectPageControls1(int id)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PageControlMaster.PageControl_id , PageControlMaster.ControlName,PageControlMaster.ControlTitle, PageControlMaster.ActiveDeactive, PageControlMaster.Page_id, PageMaster.PageName,PageControlMaster.ControlType_id FROM PageControlMaster INNER JOIN PageMaster ON PageControlMaster.Page_id = PageMaster.PageId where PageControlMaster.PageControl_id =" + id + " ";

        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();

        return dr;

    }
    //////////////////////////roleMaster////////////////////
    public void insertrolemaster(String RoleName, int Active,string compid)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("insertRoleMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Role_name", RoleName);
        cmd.Parameters.AddWithValue("@compid", compid);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }

    public DataTable selectGriddata_role(string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "' and Role_name<>'Admin' order by Role_name";
       // k1 = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + compid + "'  order by Role_name";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
       
        return dt;

    }
    public SqlDataReader Selectrole_id(int id,string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where [Role_id] = " + id +" and compid='"+compid+"' ";

        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();
        
        return dr;

    }
    public void updateRolemaster(int Role_id, String RoleName, int Active)
    {

        con.Close();

        con.Open();
        cmd = new SqlCommand("UpdateRoleMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@Role_name", RoleName);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }
        
        cmd.ExecuteNonQuery();


        con.Close();


    }
///////////////////////Select usrid , name//////////////


    public DataTable selectUser(string compid)
    {
        con.Close();
        con.Open();
        string k1;
        //k1 = "SELECT User_master.UserID, User_master.Name FROM User_master left outer join DepartmentmasterMNC  on  User_master.Department=DepartmentmasterMNC.Id where  DepartmentmasterMNC.Companyid='" + compid + "' and User_master.Department<>9";
        //////k1 = "SELECT User_master.UserID,PartytTypeMaster.PartType+':'+User_master.Name+':'+User_master.Phoneno as Name FROM [User_master]  inner join Party_master  on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId left outer join  DepartmentmasterMNC  on  User_master.Department=DepartmentmasterMNC.Id  where Party_master.id='10011' and Party_master.Account<>30000 ";

        k1 = "Select clientLoginMaster.Id as ClientUserId,EmployeeMaster.Name,EmployeeMaster.Id as UserId1 from clientLoginMaster inner join EmployeeMaster on EmployeeMaster.ClientId=clientLoginMaster.clientId and  EmployeeMaster.UserId=clientLoginMaster.UserId where clientLoginMaster.clientId='" + compid + "'";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }


    public void insertUserRoleMaster(int User_id,int Role_id, int Active)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("insertUserRoleMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@User_id", User_id);
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }

    public DataTable selectuserrole(string compid)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT distinct EmployeeMaster.Name , RoleMaster.Role_name, User_Role.ActiveDeactive, User_Role.User_Role_id "+
       " FROM User_Role INNER JOIN RoleMaster ON User_Role.Role_id = RoleMaster.Role_id "+
       " INNER JOIN EmployeeMaster ON  EmployeeMaster.Id = User_Role.User_id  where RoleMaster.compid='" + compid + "' ";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }
    public SqlDataReader Selectroleuser_id(int id)
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT ActiveDeactive, User_Role_id, User_id, Role_id FROM User_Role where User_Role.User_Role_id = " + id;
        
        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();
       
        return dr;

    }


    public void updateUserRolemaster(int User_id,int Role_id, int id, int Active)
    {

        con.Close();

        con.Open();
        cmd = new SqlCommand("UpdateUserRoleMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@User_id", User_id);
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@id", id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }


    public DataTable SelectPageRoleAccess(int id,string compid)
    {

        DataTable dt = new DataTable();
        con.Open();
        string k1;
        k1 = " SELECT [Role_Page_Access].[id],[Role_Page_Access].[Page_id],[Role_Page_Access].[Role_id],[Role_Page_Access].[ActiveDeactive] FROM [Role_Page_Access] left outer join PageMaster on  Role_Page_Access.Page_id=PageMaster.PageId where [Role_id] = " + id + " ";
        
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        con.Close();
        adp.Fill(dt);



        DataTable dt2 = new DataTable();
        dt2 = fillPageName(compid);

        if ((dt != null) && (dt.Rows.Count > 0))
        {
            if (dt.Rows.Count != dt2.Rows.Count)
            {
                for (int m = 0; m < dt2.Rows.Count; m++)
                {
                   int page_id;
                   page_id = Int16.Parse(dt2.Rows[m]["PageId"].ToString());
                   int flg = 0;
                   for (int s = 0; s < dt.Rows.Count; s++)
                   {
                       if (page_id == Int16.Parse(dt.Rows[s]["Page_id"].ToString()))
                       {
                           flg = 1;
                           break;
                       }
                      
                   }
                   if (flg == 0)
                   {

                       con.Open();
                       cmd = new SqlCommand("insertRolePageAccess", con);
                       cmd.CommandType = CommandType.StoredProcedure;

                       cmd.Parameters.AddWithValue("@Page_id", page_id);
                       cmd.Parameters.AddWithValue("@Role_id", id);

                       cmd.Parameters.AddWithValue("@ActiveDeactive", true);



                       cmd.ExecuteNonQuery();


                       con.Close();
                   }

                }

            }

        }
        else
        {
            //DataTable dt2 = new DataTable();
            //dt2 = fillPageName();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                
                int page_id;

                page_id = Int16.Parse(dt2.Rows[i]["PageId"].ToString());


                con.Open();
                cmd = new SqlCommand("insertRolePageAccess", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Page_id", page_id);
                cmd.Parameters.AddWithValue("@Role_id", id);
              
                cmd.Parameters.AddWithValue("@ActiveDeactive", true);
               
              

                cmd.ExecuteNonQuery();


                con.Close();

            }

            

        }



        return dt;
    }


    public DataTable SelectPageRoleAccess1(int id)
    {

        DataTable dt = new DataTable();
        con.Open();
        string k1;
        k1 = " SELECT [Role_Page_Access].[id],[Role_Page_Access].[Page_id],[Role_Page_Access].[Role_id],[Role_Page_Access].[ActiveDeactive] FROM [Role_Page_Access] left outer join PageMaster on  Role_Page_Access.Page_id=PageMaster.PageId where [Role_id] = " + id + " ";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        con.Close();
        adp.Fill(dt);



        DataTable dt2 = new DataTable();
        //dt2 = fillPageName(compid);
        dt2 = fillPageName1();
        if ((dt != null) && (dt.Rows.Count > 0))
        {
            if (dt.Rows.Count != dt2.Rows.Count)
            {
                for (int m = 0; m < dt2.Rows.Count; m++)
                {
                    int page_id;
                    page_id = Int16.Parse(dt2.Rows[m]["PageId"].ToString());
                    int flg = 0;
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        if (page_id == Int16.Parse(dt.Rows[s]["Page_id"].ToString()))
                        {
                            flg = 1;
                            break;
                        }

                    }
                    if (flg == 0)
                    {

                        con.Open();
                        cmd = new SqlCommand("insertRolePageAccess", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Page_id", page_id);
                        cmd.Parameters.AddWithValue("@Role_id", id);

                        cmd.Parameters.AddWithValue("@ActiveDeactive", true);



                        cmd.ExecuteNonQuery();


                        con.Close();
                    }

                }

            }

        }
        else
        {
            //DataTable dt2 = new DataTable();
            //dt2 = fillPageName();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                int page_id;

                page_id = Int16.Parse(dt2.Rows[i]["PageId"].ToString());


                con.Open();
                cmd = new SqlCommand("insertRolePageAccess", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Page_id", page_id);
                cmd.Parameters.AddWithValue("@Role_id", id);

                cmd.Parameters.AddWithValue("@ActiveDeactive", true);



                cmd.ExecuteNonQuery();


                con.Close();

            }



        }



        return dt;
    }

    
    /// <summary>
    /// //////////////////////
    /// </summary>
    /// <returns></returns>


    public DataTable selectRolePageAcceessTrue(int role_id,string compid)
    {

        con.Close();
        con.Open();

        string k1;
        k1 = "SELECT Role_Page_Access.id,Role_Page_Access.Page_id,Role_Page_Access.Role_id,Role_Page_Access.ActiveDeactive FROM Role_Page_Access left outer join PageMaster on  Role_Page_Access.Page_id=PageMaster.PageId  where  Role_id = " + role_id + " and ActiveDeactive = 1";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }


    public DataTable selectRolePageAcceessTrue1(int role_id)
    {

        con.Close();
        con.Open();

        string k1;
        k1 = "SELECT Role_Page_Access.id,Role_Page_Access.Page_id,Role_Page_Access.Role_id,Role_Page_Access.ActiveDeactive FROM Role_Page_Access left outer join PageMaster on  Role_Page_Access.Page_id=PageMaster.PageId  where  Role_id = " + role_id + "  and ActiveDeactive = 1";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }


    public void UpdateRolePageAccess(int Page_id, int Role_id, int Active)
    {

        con.Close();

        con.Open();
        cmd = new SqlCommand("UpdateRolePageAccess", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
       
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }
    ////////////////////Control_type_master


    public void InsertControl_type_Master(String Type_name)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("InsertControl_type_Master", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Type_name", Type_name);
        cmd.ExecuteNonQuery();
        con.Close();


    }

    public DataTable selectControl_type_Master()
    {

        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT Type_id,Type_name FROM Control_type_Master";        
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }
    public void UpdateControl_type_Master(String Type_name,int Type_id)
    {

        con.Close();

        con.Open();
        cmd = new SqlCommand("UpdateControl_type_Master", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Type_id", Type_id);
        cmd.Parameters.AddWithValue("@Type_name", Type_name);


        cmd.ExecuteNonQuery();


        con.Close();


    }

    public DataTable selectControl_type_Masterbyid(int id)
    {

        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT Type_name FROM Control_type_Master where Type_id = " + id;
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }
    /////////////page roleControl Access

    public DataTable selectRolebypage(int role_id)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  distinct Role_Page_Access.Page_id, PageMaster.PageName FROM Role_Page_Access INNER JOIN PageMaster ON Role_Page_Access.Page_id = PageMaster.PageId  order by PageMaster.PageName";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    //public DataTable getControlnameBypage(int page_id, string compid)
    //{


    //    con.Close();
    //    con.Open();
    //    string k1;

    //    //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

    //    k1 = "SELECT  ControlName, PageControl_id FROM PageControlMaster inner join PageMaster on  PageControlMaster.PageControl_id=PageMaster.PageId WHERE (Page_id = " + page_id + ") and PageMaster.Compid='" + compid + "'";

    //    cmd = new SqlCommand(k1, con);
    //    adp = new SqlDataAdapter(cmd);
    //    dt = new DataTable();
    //    adp.Fill(dt);
    //    con.Close();
    //    return dt;
    //}
    public DataTable getControlnameBypage(int page_id,string compid)
    {


        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  ControlName, PageControl_id FROM PageControlMaster inner join PageMaster on  PageControlMaster.PageControl_id=PageMaster.PageId WHERE (Page_id = " + page_id + ")";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable getControlnameBypage1(int page_id)
    {


        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT  ControlName, PageControl_id FROM PageControlMaster inner join PageMaster on  PageControlMaster.PageControl_id=PageMaster.PageId WHERE (Page_id = " + page_id + ")";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }



    public DataTable selectrolepagecontrolAceess(string compid)
    {
        con.Close();
        con.Open();

        string k1;
        k1 = "SELECT Role_Page_Contreol_Access.id, PageMaster.PageName, PageControlMaster.ControlName, RoleMaster.Role_name, Role_Page_Contreol_Access.ActiveDeactive FROM PageControlMaster INNER JOIN Role_Page_Contreol_Access INNER JOIN RoleMaster ON Role_Page_Contreol_Access.Role_id = RoleMaster.Role_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId ON PageControlMaster.PageControl_id = Role_Page_Contreol_Access.Page_Control_id";
        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public void InsertrolepagecontrolAccess(int Role_id, int Page_Control_id, int Page_id, int Active)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("InsertrolepagecontrolAccess", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@Page_Control_id", Page_Control_id);
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }
        

        cmd.ExecuteNonQuery();
        con.Close();


    }



    public void UpdaterolepagecontrolAccess(int Role_id, int Page_Control_id, int Page_id, int Active,int id)
    {

        con.Close();
        con.Open();
        cmd = new SqlCommand("UpdaterolepagecontrolAccess", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Role_id", Role_id);
        cmd.Parameters.AddWithValue("@Page_Control_id", Page_Control_id);
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        cmd.Parameters.AddWithValue("@id", id);
        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }


        cmd.ExecuteNonQuery();
        con.Close();


    }

    public SqlDataReader selectrolepagecontrolAceess_1(int id)
    {
        con.Close();
        con.Open();
        string k1;
        //k1 = "SELECT Role_Page_Contreol_Access.id, PageMaster.PageName, PageControlMaster.ControlName, RoleMaster.Role_name, Role_Page_Contreol_Access.ActiveDeactive FROM PageControlMaster INNER JOIN Role_Page_Contreol_Access INNER JOIN RoleMaster ON Role_Page_Contreol_Access.Role_id = RoleMaster.Role_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId ON PageControlMaster.PageControl_id = Role_Page_Contreol_Access.Page_Control_id ";
        k1 = "SELECT id, ActiveDeactive, Page_Control_id, Page_id, Role_id FROM Role_Page_Contreol_Access where id = " + id;
        SqlCommand cmdremhr = new SqlCommand(k1, con);
        SqlDataReader dr = cmdremhr.ExecuteReader();
      
        return dr;

    }
    //--------------plan page access


    public DataTable selectGriddata_plan()
    {
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT PricePlanId,PricePlanName FROM PricePlanMaster";
        cmd = new SqlCommand(k1, con11);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;

    }


    public DataTable SelectPageplanAccess(int id,string compid)
    {

        DataTable dt = new DataTable();
        con11.Open();
        string k1;
        k1 = " SELECT [id],[Page_id],[Plan_id],[ActiveDeactive] FROM [Plan_page_Access] where [Plan_id] = " + id;
        
        cmd = new SqlCommand(k1, con11);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        con11.Close();
        adp.Fill(dt);



        DataTable dt2 = new DataTable();
        dt2 = fillPageName(compid);

        if ((dt != null) && (dt.Rows.Count > 0))
        {
            if (dt.Rows.Count != dt2.Rows.Count)
            {
                for (int m = 0; m < dt2.Rows.Count; m++)
                {
                    int page_id;
                    page_id = Int16.Parse(dt2.Rows[m]["PageId"].ToString());
                    int flg = 0;
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        if (page_id == Int16.Parse(dt.Rows[s]["Page_id"].ToString()))
                        {
                            flg = 1;
                            break;
                        }

                    }
                    if (flg == 0)
                    {

                        con11.Open();
                        cmd = new SqlCommand("insertplanPageAccess", con11);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Page_id", page_id);
                        cmd.Parameters.AddWithValue("@Plan_id", id);

                        cmd.Parameters.AddWithValue("@ActiveDeactive", true);



                        cmd.ExecuteNonQuery();


                        con11.Close();
                    }

                }

            }

        }
        else
        {
            //DataTable dt2 = new DataTable();
            //dt2 = fillPageName();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                int page_id;

                page_id = Int16.Parse(dt2.Rows[i]["PageId"].ToString());


                con11.Open();
                cmd = new SqlCommand("insertplanPageAccess", con11);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Page_id", page_id);
                cmd.Parameters.AddWithValue("@Plan_id", id);

                cmd.Parameters.AddWithValue("@ActiveDeactive", true);



                cmd.ExecuteNonQuery();


                con11.Close();

            }



        }



        return dt;
    }
    public DataTable selectplanPageAcceessTrue(int role_id)
    {

        con11.Close();
        con11.Open();

        string k1;
        k1 = "SELECT id,Page_id,Plan_id,ActiveDeactive FROM Plan_page_Access where Plan_id = " + role_id + " and ActiveDeactive = 1";
        cmd = new SqlCommand(k1, con11);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con11.Close();
        return dt;

    }

    public void UpdateplanPageAccess(int Page_id, int Role_id, int Active)
    {

        con11.Close();

        con11.Open();
        cmd = new SqlCommand("UpdatePlanPageAccess", con11);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Page_id", Page_id);
        cmd.Parameters.AddWithValue("@Plan_id", Role_id);

        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con11.Close();


    }



    public DataTable selectpagenameandcontrolname(string compid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable selectpagenameandcontrolname(int id,string compid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + " ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable selectpagenameandcontrolname_true(int id,string compid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + "and  Role_Page_Contreol_Access.ActiveDeactive = 1 ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }


    public DataTable selectpagenameandcontrolname_pagename(int id,int pageid,string compid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + " and Role_Page_Contreol_Access.Page_id = " + pageid + "  ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    public void UpdateRole_Page_Contreol_Access(int id, int Active)
    {

        con.Close();

        con.Open();
        cmd = new SqlCommand("UpdateRole_Page_Contreol_Access", con);
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.AddWithValue("@id", id);
        

        if (Active == 1)
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", true);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ActiveDeactive", false);
        }

        cmd.ExecuteNonQuery();


        con.Close();


    }
    public DataTable selectpagenameandcontrolname_false(int id)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + " and Role_Page_Contreol_Access.ActiveDeactive = 0 ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable selectpagenameandcontrolname_pagename_false(int id, int pageid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + " and Role_Page_Contreol_Access.Page_id = " + pageid + " and Role_Page_Contreol_Access.ActiveDeactive = 0 ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable selectpagenameandcontrolname_pagename_true(int id, int pageid)
    {

        con.Close();
        con.Open();
        string k1;

        //k1 = "SELECT id,Page_id,Role_id,ActiveDeactive FROM Role_Page_Access where Role_id = " + role_id + " and ActiveDeactive = 1";

        k1 = "SELECT (PageMaster.PageName + '/' + PageControlMaster.ControlName) as name, Role_Page_Contreol_Access.ActiveDeactive, Role_Page_Contreol_Access.Role_id, Role_Page_Contreol_Access.id, Role_Page_Contreol_Access.Page_id FROM Role_Page_Contreol_Access INNER JOIN PageControlMaster ON Role_Page_Contreol_Access.Page_Control_id = PageControlMaster.PageControl_id INNER JOIN PageMaster ON Role_Page_Contreol_Access.Page_id = PageMaster.PageId  where Role_Page_Contreol_Access.Role_id = " + id + " and Role_Page_Contreol_Access.Page_id = " + pageid + " and Role_Page_Contreol_Access.ActiveDeactive = 1 ORDER BY PageMaster.PageName, PageControlMaster.ControlName, Role_Page_Contreol_Access.ActiveDeactive";

        cmd = new SqlCommand(k1, con);
        adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        con.Close();
        return dt;
    }
    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strtxt;
            //  throw ex;
        }

    }
    public static string Encrypted(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
        //&%#@?,:*
        //c7171b1e96fc3bbZ8wAS

    }

    private static string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strText;
            //throw ex;
        }
    }

    public static string Decrypted(string str)
    {
        //c7171b1e96fc3bbZ8wAS
        return Decrypt(str, "&%#@?,:*");

    }


}
