using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class addRoom : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
           
            if (Request.QueryString["Roomtypeid"] != null)
            {
                pnladdd.Visible = true;
                btnadddd.Visible = false;
                string Roomtypeid = Request.QueryString["Roomtypeid"];
                SqlCommand cmdstr = new SqlCommand("Select * from tblroomtype where ID = '" + Roomtypeid + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    fillstore();
                    ddlschool.SelectedValue = ds.Rows[0]["BusinessID"].ToString();
                    fn_bind_building();
                    ddlbuilding.SelectedValue = ds.Rows[0]["Buildingid"].ToString();
                    fn_bind_floor();
                    ddlfloor.SelectedValue = ds.Rows[0]["floorid"].ToString();
                    fn_bind_Roomtype();
                    ddlRoomtype.SelectedValue = ds.Rows[0]["ID"].ToString();

                }

            }
            else
            {
                fillstore();
                fn_bind_building();
                fn_bind_floor();
                fn_bind_Roomtype();
                bindgrid();
            }
        }
      
    }
    protected void fillstore()
    {
        ddlschool.Items.Clear();
        filterschool.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlschool.DataSource = ds;
        ddlschool.DataTextField = "Name";
        ddlschool.DataValueField = "WareHouseId";

        ddlschool.DataBind();


        filterschool.DataSource = ds;
        filterschool.DataTextField = "Name";
        filterschool.DataValueField = "WareHouseId";

        filterschool.DataBind();
 
        filterschool.Items.Insert(0, "All");
        filterbuilding.Items.Insert(0, "All");
        filterfloor.Items.Insert(0, "All");
        filterroomtype.Items.Insert(0, "All");
        
        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
    }
    private void fn_bind_building()
    {

        SqlCommand cmd = new SqlCommand("select ID,BuildingName from tblBuilding where BusinessID = '" + ddlschool.SelectedValue + "'", con);

        con.Open();
        DataTable dt = new DataTable();

        dt.Load(cmd.ExecuteReader());
        con.Close();


        ddlbuilding.DataSource = dt;
        ddlbuilding.DataTextField = "BuildingName";
        ddlbuilding.DataValueField = "ID";
        ddlbuilding.DataBind();
        ddlbuilding.Items.Insert(0, new ListItem("Select", "0"));

    }
    private void fn_filterbuilding()
    {
        DataTable dt = new DataTable();
        if (filterschool.SelectedItem.Text != "All")
        {
            SqlCommand cmd = new SqlCommand("select ID,BuildingName from tblBuilding where BusinessID = '" + filterschool.SelectedValue + "'", con);

            con.Open();
           

            dt.Load(cmd.ExecuteReader());
            con.Close();
        }

        filterbuilding.DataSource = dt;
        filterbuilding.DataTextField = "BuildingName";
        filterbuilding.DataValueField = "ID";
        filterbuilding.DataBind();
        filterbuilding.Items.Insert(0, new ListItem("All", "0"));

    }
    private void fn_bind_floor()
    {
        DataTable dt = new DataTable();
              SqlCommand cmd = new SqlCommand("select ID,floorname from tblFloor where Buildingid = '" + ddlbuilding.SelectedValue + "'", con);

              con.Open();

              dt.Load(cmd.ExecuteReader());
              con.Close();

          
        ddlfloor.DataSource = dt;
        ddlfloor.DataTextField = "floorname";
        ddlfloor.DataValueField = "ID";
        ddlfloor.DataBind();
        ddlfloor.Items.Insert(0, new ListItem("Select", "0"));

    }
    private void fn_filterfloor()
    {
          DataTable dt = new DataTable();
          if (filterbuilding.SelectedItem.Text != "All")
          {
              SqlCommand cmd = new SqlCommand("select ID,floorname from tblFloor where Buildingid = '" + filterbuilding.SelectedValue + "'", con);

              con.Open();


              dt.Load(cmd.ExecuteReader());
              con.Close();
          }

        filterfloor.DataSource = dt;
        filterfloor.DataTextField = "floorname";
        filterfloor.DataValueField = "ID";
        filterfloor.DataBind();
        filterfloor.Items.Insert(0, new ListItem("All", "0"));


    }
    private void fn_bind_Roomtype()
    {

        SqlCommand cmd = new SqlCommand("select ID,RoomType from tblroomtype where floorid = '" + ddlfloor.SelectedValue + "'", con);

        con.Open();
        DataTable dt = new DataTable();

        dt.Load(cmd.ExecuteReader());
        con.Close();


        ddlRoomtype.DataSource = dt;
        ddlRoomtype.DataTextField = "RoomType";
        ddlRoomtype.DataValueField = "ID";
        ddlRoomtype.DataBind();
        ddlRoomtype.Items.Insert(0, new ListItem("Select", "0"));

    }
    private void fn_filterroomtype()
    {
         DataTable dt = new DataTable();
         if (filterfloor.SelectedItem.Text != "All")
         {
             SqlCommand cmd = new SqlCommand("select ID,RoomType from tblroomtype where floorid = '" + filterfloor.SelectedValue + "'", con);

             con.Open();
        

             dt.Load(cmd.ExecuteReader());
             con.Close();
         }

        filterroomtype.DataSource = dt;
        filterroomtype.DataTextField = "RoomType";
        filterroomtype.DataValueField = "ID";
        filterroomtype.DataBind();
        filterroomtype.Items.Insert(0, new ListItem("All", "0"));
       

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("select RoomNumber from RoomMasterTbl  where RoomNumber = '" + txtRoomno.Text + "' and BusinessID = '" + ddlschool.SelectedValue + "' and  floorid='" + ddlfloor.SelectedValue + "' and Buildingid= '" + ddlbuilding.SelectedValue + "' and RoomTypeID= '" + ddlRoomtype.SelectedValue + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";


        }
        else
        {
            fn_SaveGrade();
            bindgrid();

        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
         PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("select * from RoomMasterTbl  where RoomNumber = '" + txtRoomno.Text + "'and  Available = '" + ddlActiveInactive.SelectedValue + "' and  BusinessID = '" + ddlschool.SelectedValue + "' and  floorid='" + ddlfloor.SelectedValue + "' and Buildingid= '" + ddlbuilding.SelectedValue + "' and RoomTypeID= '" + ddlRoomtype.SelectedValue + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            string currentsection = ViewState["ID"].ToString();
            if (ds.Rows[0]["RoomID"].ToString() == currentsection)
            {

                lblmsg.Text = "Record updated successfully.";
            }
            else
            {
                lblmsg.Text = "Record already exist.";
            }
        }
        else
        {
            fn_update();

        }
        clear();

    }
    private void fn_update()
    {
        try
        {


            SqlCommand cmd = new SqlCommand("sp_updateRoom", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoomID", SqlDbType.VarChar).Value = ViewState["ID"];
            cmd.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = txtRoomno.Text;
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@Buildingid", SqlDbType.Int).Value = ddlbuilding.SelectedValue;
            cmd.Parameters.Add("@floorid", SqlDbType.Int).Value = ddlfloor.SelectedValue;
            cmd.Parameters.Add("@RoomTypeID", SqlDbType.Int).Value = ddlRoomtype.SelectedValue;
            cmd.Parameters.Add("@Available", SqlDbType.Bit).Value = Convert.ToInt32(ddlActiveInactive.SelectedValue);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
        }
        catch
        {
            lblmsg.Text = "Record not updated.";
        }
        bindgrid();

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
    private void fn_SaveGrade()
    {
        try
        {
          
            SqlCommand cmd = new SqlCommand("sp_insertRoom", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add("@RoomNumber", SqlDbType.VarChar).Value = txtRoomno.Text;
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@Buildingid", SqlDbType.Int).Value = ddlbuilding.SelectedValue;
            cmd.Parameters.Add("@floorid", SqlDbType.Int).Value = ddlfloor.SelectedValue;
            cmd.Parameters.Add("@RoomTypeID", SqlDbType.Int).Value = ddlRoomtype.SelectedValue;
            cmd.Parameters.Add("@Available", SqlDbType.Bit).Value = Convert.ToInt32(ddlActiveInactive.SelectedValue);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully.";
            clear();
            
        }
        catch
        {

        }


    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add New Room";
        lblmsg.Text = "";
    }
    protected void clear()
    {
       
        txtRoomno.Text = "";
        ddlbuilding.SelectedIndex = 0;
        ddlfloor.SelectedIndex = 0;
        ddlRoomtype.SelectedIndex = 0;
        ddlActiveInactive.SelectedIndex= 0;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        btnupdate.Visible = false;
    }
    protected void bindgrid()
    {
        string st1 ="",st2="",st3="",st4="",st5="";

        if (ddlstatus_search.SelectedIndex > 0)
        {
            st1 += "WHERE RoomMasterTbl.Available='" + ddlstatus_search.SelectedValue + "'";
        }
        if (filterschool.SelectedIndex > 0)
        {
            st2 += " and RoomMasterTbl.BusinessID='" + filterschool.SelectedValue + "'";
        }
        if (filterbuilding.SelectedIndex > 0)
        {
            st3 += " and RoomMasterTbl.Buildingid='" + filterbuilding.SelectedValue + "'";
        }
        if (filterfloor.SelectedIndex > 0)
        {
            st4 += " and RoomMasterTbl.floorid='" + filterfloor.SelectedValue + "'";
        }
        if (filterroomtype.SelectedIndex > 0)
        {
            st5 += " and RoomMasterTbl.RoomTypeID='" + filterroomtype.SelectedValue + "'";
        }

        lblstat.Text = ddlstatus_search.SelectedItem.Text;
         SqlCommand cmdc = new SqlCommand(" SELECT tblFloor.floorname,RoomMasterTbl.RoomID,RoomMasterTbl.RoomNumber,tblBuilding.BuildingName,WareHouseMaster.Name as School," +" "+
        "tblroomtype.RoomType,CASE(RoomMasterTbl.Available) WHEN 1 THEN 'Available' ELSE 'Unavailable'  end as Available FROM RoomMasterTbl "+" "+
        "inner join WareHouseMaster on RoomMasterTbl.BusinessID=WareHouseMaster.WareHouseId inner join tblBuilding on tblBuilding.ID=RoomMasterTbl.Buildingid" + " "+
        "inner join tblFloor ON tblFloor.ID=RoomMasterTbl.floorid inner join tblroomtype on tblroomtype.ID=RoomMasterTbl.RoomTypeID " + st1 + st2 + st3 + st4 + st5 +  "", con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        if (dtc.Rows.Count > 0)
        {
            GVC.DataSource = dtc;
            GVC.DataBind();
        }
        else
        {
            GVC.DataSource = null;
            GVC.DataBind();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;


            if (GVC.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVC.Columns[6].Visible = false;
            }
            if (GVC.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVC.Columns[7].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVC.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVC.Columns[7].Visible = true;
            }
        }
    }
    protected void GVC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVC.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GVC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            btnupdate.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lbllegend.Text = "Edit Room";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from RoomMasterTbl where RoomID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtRoomno.Text = dt.Rows[0]["RoomNumber"].ToString();
            ddlschool.SelectedValue = dt.Rows[0]["BusinessID"].ToString();
            fn_bind_building();
            ddlbuilding.SelectedValue = dt.Rows[0]["Buildingid"].ToString();
            fn_bind_floor();

            ddlfloor.SelectedValue = dt.Rows[0]["floorid"].ToString();
            fn_bind_Roomtype();
            ddlRoomtype.SelectedValue = dt.Rows[0]["RoomTypeID"].ToString();
           
           
         

            string Available = dt.Rows[0]["Available"].ToString();
            if (Available == "True")
            {
                ddlActiveInactive.SelectedValue = "1";
            }
            else
            {
                ddlActiveInactive.SelectedValue = "0";
            }
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            //SqlDataAdapter daf = new SqlDataAdapter("select * from tblSubjectMaster where SectionID='" + mm1 + "'", con);
            //DataTable dtf = new DataTable();
            //daf.Fill(dtf);

            //if (dtf.Rows.Count > 0)
            //{
            //    lblmsg.Visible = true;
            //    lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            //}
            //else
          //  {
            string str1 = "Delete  From RoomMasterTbl Where RoomID= " + mm1 + " ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully.";
            }
            bindgrid();
    }
    protected void GVC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void lnkPreious_Click(object sender, EventArgs e)
    {

    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {

    }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_bind_building();
    }
    protected void ddlbuilding_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_bind_floor();
    }
    protected void ddlRoomtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlfloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_bind_Roomtype();
    }
    protected void filterschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_filterbuilding();
        bindgrid();
    }
    protected void filterbuilding_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        fn_filterfloor();
        bindgrid();
    }
    protected void filterfloor_SelectedIndexChanged1(object sender, EventArgs e)
    {
       
        fn_filterroomtype();
        bindgrid();
    }


    protected void filterroomtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
}