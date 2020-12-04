
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

public partial class AttendanceDevice : System.Web.UI.Page
{
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    string compid;
    Boolean only;
    Boolean allapp;
    DBCommands1 dbss1 = new DBCommands1();
    public int puraccid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

      
        Page.Title = pg.getPageTitle(page);
      
        if (!Page.IsPostBack)
        {
           ViewState["sortOrder"] = "";
           lblCompany.Text = Session["Cname"].ToString();

           fillstore();
            fillgrid();
                  
        }
    }





    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        //ddlstore.Items.Insert(0, "Select");
       
        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
        ddlbussnessfilter.DataSource = ds;
        ddlbussnessfilter.DataTextField = "Name";
        ddlbussnessfilter.DataValueField = "WareHouseId";
        ddlbussnessfilter.DataBind();
        ddlbussnessfilter.Items.Insert(0, "All");
        ddlbussnessfilter.Items[0].Value="0";
    }


    protected void fillgrid()
    {
        lblBusiness.Text = ddlbussnessfilter.SelectedItem.Text;
        //lblserttex.Text = txtserach.Text;
        if (txtserach.Text.Length > 0)
        {
            lblserttex.Text = txtserach.Text;
        }
        else
        {
            lblserttex.Text = "All";
        }
        string wfil = "";
        if (ddlbussnessfilter.SelectedIndex > 0)
        {
            wfil += " and Whid='" + ddlbussnessfilter.SelectedValue+ "'";
        }
        if (txtserach.Text.Length > 0)
        {
          wfil+= " and (AttendanceDeviceName Like '%"+txtserach.Text+"%' or AttendanceDeviceNo Like '%"+txtserach.Text+"%' or Location "+
              " Like '%"+txtserach.Text+"%' or SerialNo Like '%"+txtserach.Text+"%' or IpAddress Like '%"+txtserach.Text+"%' or VersionNo Like "+
              " '%"+txtserach.Text+"%' or Make Like '%"+txtserach.Text+"%' or Model Like '%"+txtserach.Text+"%')";

        }

        string strgrd = "select distinct Id, WareHouseMaster.Name as Wname,AttendanceDeviceName,AttendanceDeviceNo,Location,IpAddress  from AttendanceDeviceTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=AttendanceDeviceTbl.Whid where WareHouseMaster.comid='" + Session["Comid"] + "'" + wfil + " Order by AttendanceDeviceTbl.AttendanceDeviceName";
        DataTable dtg = dbss1.cmdSelect(strgrd);
        GridView1.DataSource = dtg;

        DataView myDataView = new DataView();
        myDataView = dtg.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
        //foreach (GridViewRow item in GridView1.Rows)
        //{
        //    Label lbltaxpers = (Label)item.FindControl("lbltaxpers");
        //    Label lblsingamt = (Label)item.FindControl("lblsingamt");
        //    Label lblsignpers = (Label)item.FindControl("lblsignpers");
        //    if (Convert.ToDecimal(lbltaxpers.Text) > 0)
        //    {
        //        lblsingamt.Visible = false;
        //        lblsignpers.Visible = true;
        //    }
        //    else
        //    {
        //        lblsingamt.Visible = true;
        //        lblsignpers.Visible = false;
        //    }

        //}
    }

    protected DataTable select(string str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        string Ipaddd = txtIpAddress.Text + "." + txtip1.Text + "." + txtip2.Text + "." + txtip3.Text;


        DataTable dtc = select("select * from AttendanceDeviceTbl where AttendanceDeviceName='" + txtattedevicename.Text + "' and AttendanceDeviceNo='" + txtattedeviceno.Text + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
           
            if (dtc.Rows.Count > 0)
            {
                Label1.Text = "Record already exists";
              
            }

            else
            {


                string str = "insert into  AttendanceDeviceTbl(CID,Whid,AttendanceDeviceName,AttendanceDeviceNo,Location,SerialNo,IpAddress,VersionNo,Make,Model)values('" + Session["Comid"] + "', " +
                   " '" +ddlwarehouse.SelectedValue + "','" + txtattedevicename.Text + "' ,' " + txtattedeviceno.Text + "','" + txtlocation.Text + "','" + txtserialno.Text + "','" + Ipaddd + "','" + txtverno.Text + "','" + txtmake.Text + "','" + txtmodel.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                int ik =cmd.ExecuteNonQuery();
                con.Close();
                if (ik == 1)
                {

                    Label1.Visible = true;
                    Label1.Text = "Record inserted successfully";
                    cle();
                    fillgrid();


                }
            }
        
    }
    public void cle()
    {
        pnlattdev.Enabled = true;
          lblms.Text = "";
          addinventoryroom.Visible = false;
          btnaddacc.Visible = true;
            txtverno.Text = "";
            txtattedeviceno.Text = "";
            txtattedevicename.Text = "";
            txtip1.Text = "";
            txtip2.Text = "";
            txtip3.Text = "";
                txtIpAddress.Text = "";
                txtlocation.Text = "";
                txtmake.Text = "";
                txtmodel.Text = "";
                txtserialno.Text = "";
                ImageButton9.Visible = false;
                ImageButton1.Visible = true;
    }
     
       
   
    protected void edit_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        
       // LinkButton lnk = (LinkButton)sender;
        ImageButton lnk = (ImageButton)sender;
        int i = Convert.ToInt32(lnk.CommandArgument);

        if (lnk.CommandName == "View")
        {
            lblms.Text = "View Attendance Device";
            ImageButton9.Visible = false;
            ImageButton1.Visible = false;
            pnlattdev.Enabled = false;
        }
        else
        {
            lblms.Text = "Edit Attendance Device";
            ImageButton9.Visible = true;
            ImageButton1.Visible = false;
            pnlattdev.Enabled = true;
        }
        ViewState["Id"] = i.ToString();
        String selectStr = "select * from AttendanceDeviceTbl where id='" + i + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(selectStr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Convert.ToString(dt.Rows[0]["Whid"]))); ;
            string[] separbm = new string[] { "." };
            string[] strSplitArrbm = Convert.ToString(dt.Rows[0]["IpAddress"]).Split(separbm, StringSplitOptions.RemoveEmptyEntries);



            txtverno.Text = Convert.ToString(dt.Rows[0]["VersionNo"]);
            txtattedeviceno.Text = Convert.ToString(dt.Rows[0]["AttendanceDeviceNo"]);
            txtattedevicename.Text = Convert.ToString(dt.Rows[0]["AttendanceDeviceName"]);
            txtip1.Text = Convert.ToString(strSplitArrbm[1]);
            txtip2.Text = Convert.ToString(strSplitArrbm[2]);
            txtip3.Text = Convert.ToString(strSplitArrbm[3]);
            txtIpAddress.Text = Convert.ToString(strSplitArrbm[0]);
            txtlocation.Text = Convert.ToString(dt.Rows[0]["Location"]);
            txtmake.Text = Convert.ToString(dt.Rows[0]["Make"]);
            txtmodel.Text = Convert.ToString(dt.Rows[0]["Model"]);
            txtserialno.Text = Convert.ToString(dt.Rows[0]["SerialNo"]);
         
            addinventoryroom.Visible = true;
            btnaddacc.Visible = false;
        }
       
        
    }

    protected void ImageButton9_Click(object sender, EventArgs e)
    {
        string Ipaddd = txtIpAddress.Text + "." + txtip1.Text + "." + txtip2.Text + "." + txtip3.Text;


        DataTable dtc = select("select * from AttendanceDeviceTbl where AttendanceDeviceName='" + txtattedevicename.Text + "' and AttendanceDeviceNo='" + txtattedeviceno.Text + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Id<>'" + ViewState["Id"] + "'");

        if (dtc.Rows.Count > 0)
        {
            Label1.Text = "Record already exists";

        }



        string str = "Update AttendanceDeviceTbl set AttendanceDeviceName='" + txtattedevicename.Text + "',AttendanceDeviceNo='" + txtattedeviceno.Text + "',Location='" + txtlocation.Text + "',SerialNo='" + txtserialno.Text + "',IpAddress='" + Ipaddd + "',VersionNo='" + txtverno.Text + "',Make='" + txtmake.Text + "',Model='" + txtmodel.Text + "',Whid='" + ddlwarehouse.SelectedValue + "' where Id='" + ViewState["Id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int ik = cmd.ExecuteNonQuery();
        con.Close();
        if (ik == 1)
        {

            Label1.Visible = true;
            Label1.Text = "Record updated successfully";
            pnlattdev.Enabled = true;
            fillgrid();
            cle();
           
        }
      
    }

                


            
        

    
 
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
       
        cle();
       
        Label1.Text = "";
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["wz"] != null)
        {
            Response.Redirect("StoreTaxmethodtbl.aspx");
        }
        else
        {
            Response.Redirect("wzStoreTaxmethodtbl.aspx");
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        fillgrid();
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
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button3.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Button4.Text = "Printable Version";
            Button3.Visible = false;
            if (ViewState["viewHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //con.Close();
        //LinkButton del = (LinkButton)sender;
        //int deli = Convert.ToInt32(del.CommandArgument);

       int deli= Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

        //string chkid = "select * from SalesOrderTempTax where TaxTypeMasterID=" + deli + "";
        //DataTable dtchkid = dbss1.cmdSelect(chkid);
        //if (dtchkid.Rows.Count > 0)
        //{
        //    Label1.Visible = true;
        //    Label1.Text = "You can not delete this Tax entry as some order /invoice exist for this tax entry. However you can make this entry inactive to stop using this tax entry for your sales or edit the entry for change in name or tax rates etc.";
        //}
        //else
        //{


       string str = "delete  from AttendanceDeviceTbl where Id='" + deli + "'";
       if (con.State.ToString() != "Open")
       {
           con.Open();
       }
                 SqlCommand dcmd = new SqlCommand(str, con);
                 dcmd.ExecuteNonQuery();
                 con.Close();


                 Label1.Text = "Record deleted successfully";
                 fillgrid();
                 cle();
                 
        //}
       
    }
  
 
    protected void ddlbussnessfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillgrid();
    }
    protected void btnaddacc_Click(object sender, EventArgs e)
    {
        pnlattdev.Enabled = true;
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;
        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;
        }
        btnaddacc.Visible = false;
        Label1.Text = "";
        lblms.Text = "Add Attendance Device";
    }
    protected void txtserach_TextChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}


