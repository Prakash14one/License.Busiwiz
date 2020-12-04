using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.Sql;

public partial class AttendanceEmailNotification : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();


        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        compid = Session["Comid"].ToString();
        statuslable.Visible = false;
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblcompname.Text = Convert.ToString(Session["Cname"]);
            Pagecontrol.dypcontrol(Page, page);
            fillwarehouse();
            ddlwarehouse_SelectedIndexChanged(sender, e);
            fillgrid();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;

            chkemailnotif_CheckedChanged(sender, e);

            //Button6.Visible = false;
        }
    }
    protected void fillwarehouse()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }
    }
    public void fillgrid()
    {
        DataTable dt = new DataTable();
        string grd = "select distinct AttandanceRule.*,WarehouseMaster.Name as StoreName, WarehouseMaster.WareHouseId,AttandanceRule.AcceptableInTimeDeviationMinutes,AttandanceRule.AcceptableOutTimeDeviationMinutes  from AttandanceRule inner join WarehouseMaster on AttandanceRule.StoreId=WarehouseMaster.WareHouseId where WarehouseMaster.comid= '" + Session["Comid"] + "' and WarehouseMaster.Status='1' order by StoreName";

        SqlCommand cmd = new SqlCommand(grd, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();

    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        //    string str1 = "select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "'";
        //    SqlCommand cmd1 = new SqlCommand(str1, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        statuslable.Visible = true;
        //        statuslable.Text = "Record already exists";

        //    }
        //    else
        //    {
        //        string str = "";
        //        SqlCommand cmd = new SqlCommand();
        //        int opid = 0;
        //        if (rd3.Checked == true)
        //        {
        //            opid = 3;
        //        }
        //        else if (rd1.Checked == true)
        //        {
        //            opid = 1;
        //        }
        //        else if (rd2.Checked == true)
        //        {
        //            opid = 2;
        //        }


        //        if (opid == 3)
        //        {

        //            str = "Insert Into AttandanceRule(StoreId,AcceptableInTimeDeviationMinutes,AcceptableOutTimeDeviationMinutes,Considerwithinrangedeviationasstandardtime,ShowtheFieldtorecordthereasonfordeviation,Showreasonafterinstance,TakeapprovaloftheseniorEmployee,SeniorEmployeeID,Takeapprovalafterinstance,Donotallowemployeetomakeentryinregister,Donotallowemployeeinstance,CompId,overtimeruleno,OvertimepaymentRate,Overtimehours,Overtimeremunaration,Overtimepara,Generalapprovalrule,Considerinoutrangeintance,rulegreatertime,rulegreatertimeinstance,overtimerulerange,overtomeapproval,Maxuserhours)values('" + ddlwarehouse.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + CheckBox1.Checked + "','" + chkdeviationreason.Checked + "','" + txtreason.Text + "','" + chkempinstance.Checked + "','" + ddlemp.SelectedValue + "','" + txtempapproal.Text + "','" + chkMakeEntry.Checked + "','" + txtEntry.Text + "','" + compid + "','" + opid + "','" + txtRate.Text + "','" + txthour.Text + "','" + ddlramu.SelectedValue + "','1','" + chkgenralrule.Checked + "','" + txtconsi.Text + "','" + chkgreatertime.Checked + "','" + txtouttimegreater.Text + "','" + chkoverA.Checked + "','" + chkappsuper.Checked + "','" + txtallowusereg.Text + "')";



        //        }

        //        else if (opid == 2)
        //        {

        //            str = "Insert Into AttandanceRule(StoreId,AcceptableInTimeDeviationMinutes,AcceptableOutTimeDeviationMinutes,Considerwithinrangedeviationasstandardtime,ShowtheFieldtorecordthereasonfordeviation,Showreasonafterinstance,TakeapprovaloftheseniorEmployee,SeniorEmployeeID,Takeapprovalafterinstance,Donotallowemployeetomakeentryinregister,Donotallowemployeeinstance,Overtimepara,overtimeruleno,CompId,Generalapprovalrule,Considerinoutrangeintance,rulegreatertime,rulegreatertimeinstance,overtimerulerange,overtomeapproval,Overtimehours,Maxuserhours)values" +
        //                "('" + ddlwarehouse.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + CheckBox1.Checked + "','" + chkdeviationreason.Checked + "','" + txtreason.Text + "','" + chkempinstance.Checked + "','" + ddlemp.SelectedValue + "','" + txtempapproal.Text + "','" + chkMakeEntry.Checked + "','" + txtEntry.Text + "','1','" + opid + "','" + compid + "','" + chkgenralrule.Checked + "','" + txtconsi.Text + "','" + chkgreatertime.Checked + "','" + txtouttimegreater.Text + "','" + chlover1.Checked + "','" + chkover2.Checked + "','" + txtoverhour2.Text + "','" + txtallowusereg.Text + "')";




        //        }
        //        else if (opid == 1)
        //        {
        //            str = "Insert Into AttandanceRule(StoreId,AcceptableInTimeDeviationMinutes,AcceptableOutTimeDeviationMinutes,Considerwithinrangedeviationasstandardtime,ShowtheFieldtorecordthereasonfordeviation,Showreasonafterinstance,TakeapprovaloftheseniorEmployee,SeniorEmployeeID,Takeapprovalafterinstance,Donotallowemployeetomakeentryinregister,Donotallowemployeeinstance,Overtimepara,overtimeruleno,CompId,Generalapprovalrule,Considerinoutrangeintance,rulegreatertime,rulegreatertimeinstance,Maxuserhours)values" +
        //                "('" + ddlwarehouse.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + CheckBox1.Checked + "','" + chkdeviationreason.Checked + "','" + txtreason.Text + "','" + chkempinstance.Checked + "','" + ddlemp.SelectedValue + "','" + txtempapproal.Text + "','" + chkMakeEntry.Checked + "','" + txtEntry.Text + "','0','" + opid + "','" + compid + "','" + chkgenralrule.Checked + "','" + txtconsi.Text + "','" + chkgreatertime.Checked + "','" + txtouttimegreater.Text + "','"+txtallowusereg.Text+"')";


        //        }
        //        cmd = new SqlCommand(str, con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        string stratt = "select Role_id from RoleMaster where Role_name='Senior Employee-Attendence Matter' and compid='" + Session["comid"] + "' ";
        //        SqlCommand cmd3att = new SqlCommand(stratt, con);
        //        SqlDataAdapter adpatt = new SqlDataAdapter(cmd3att);
        //        DataTable dtatt = new DataTable();
        //        adpatt.Fill(dtatt);

        //        if (dtatt.Rows.Count > 0)
        //        {
        //            string roleid = dtatt.Rows[0]["Role_id"].ToString();
        //            string stratt1 = "Select distinct User_Master.UserId from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master on User_Master.PartyId=Party_Master.PartyId where EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "' ";
        //            SqlCommand cmd3att1 = new SqlCommand(stratt1, con);
        //            SqlDataAdapter adpatt1 = new SqlDataAdapter(cmd3att1);
        //            DataTable dtatt1 = new DataTable();
        //            adpatt1.Fill(dtatt1);

        //            if (dtatt1.Rows.Count > 0)
        //            {
        //                SqlCommand cmd8 = new SqlCommand("Insert into User_Role (User_id,Role_id,ActiveDeactive) values ('" + dtatt1.Rows[0]["UserId"] + "','" + roleid + "','true')", con);
        //                con.Open();
        //                cmd8.ExecuteNonQuery();
        //                con.Close();
        //            }
        //            string k1 = "SELECT * FROM PageMaster where PageName in('AttendenceDeviations.aspx','AttendenceApproval.aspx')";
        //            SqlCommand cmdpagem = new SqlCommand(k1, con);
        //            SqlDataAdapter adpag = new SqlDataAdapter(cmdpagem);
        //            DataTable dtpag = new DataTable();
        //            adpag.Fill(dtpag);
        //            foreach (DataRow dts in dtpag.Rows)
        //            {

        //                string str1ap = "insert into RolePageAccessRightTbl(PageId,RoleId,AccessRight,Edit_Right,Delete_Right,Download_Right,Insert_Right,Update_Right,View_Right,Go_Right,SendMail_Right) " +
        //               " Values( '" + Convert.ToInt32(dts["PageId"]) + "','" + Convert.ToInt32(roleid) + "','1','1','1','1','1','1','1','1','1')";
        //                SqlCommand cmdap = new SqlCommand(str1ap, con);
        //                con.Open();
        //                cmdap.ExecuteNonQuery();

        //                con.Close();
        //            }
        //        }
        //        TextBox1.Text = "15";
        //        TextBox2.Text = "15";

        //        txtreason.Text = "5";
        //        txtEntry.Text = "5";
        //        txtempapproal.Text = "5";
        //        txtconsi.Text = "3";
        //        txtallowusereg.Text = "4";
        //        // txtHalfday.Text = "";
        //        chkgenralrule.Checked = false;
        //        chkgreatertime.Checked = true;
        //        txtouttimegreater.Text = "6";
        //        CheckBox1.Checked = false;
        //        CheckBox1_CheckedChanged(sender, e);
        //        chkdeviationreason.Checked = false;
        //      //  CheckBox2_CheckedChanged(sender, e);
        //        chkempinstance.Checked = false;
        //       // CheckBox3_CheckedChanged(sender, e);
        //        chkMakeEntry.Checked = false;
        //       // CheckBox4_CheckedChanged(sender, e);

        //        statuslable.Visible = true;
        //        statuslable.Text = "Recored inserted successfully";

        //        fillgrid();
        // }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "15";
        TextBox2.Text = "15";
        pnlenb.Enabled = false;
        txtreason.Text = "5";
        txtEntry.Text = "5";
        txtempapproal.Text = "5";
        txtconsi.Text = "3";

        chkgenralrule.Checked = false;
        chkgreatertime.Checked = true;
        txtouttimegreater.Text = "6";
        //   CheckBox1.Checked = false;
        CheckBox1_CheckedChanged(sender, e);
        chkdeviationreason.Checked = false;

        chkempinstance.Checked = false;

        chkMakeEntry.Checked = false;

        statuslable.Visible = true;
        btnUpdate.Visible = false;
        pnladd.Visible = false;
        btnEdit.Visible = false;
        fillgrid();
    }


    protected void edit()
    {
        pnlenb.Enabled = false;
        string str1 = "Select AttandanceRule.*,WarehouseMaster.Name as StoreName, WarehouseMaster.WareHouseId,AcceptableInTimeDeviationMinutes, " +
                   " AcceptableOutTimeDeviationMinutes  from AttandanceRule inner join WarehouseMaster " +
               " on AttandanceRule.StoreId=WarehouseMaster.WareHouseId where AttandanceRule.Attendence_Rule_Id='" + ViewState["Id"] + "'";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        adp.Fill(dt);

        ddlwarehouse.SelectedValue = dt.Rows[0]["StoreId"].ToString();
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlwarehouse_SelectedIndexChanged(sender, e);

        ddlemp.SelectedValue = dt.Rows[0]["SeniorEmployeeID"].ToString();

        ViewState["ei"] = dt.Rows[0]["SeniorEmployeeID"].ToString();
        TextBox1.Text = dt.Rows[0]["AcceptableInTimeDeviationMinutes"].ToString();
        TextBox2.Text = dt.Rows[0]["AcceptableOutTimeDeviationMinutes"].ToString();
        TextBox3.Text = dt.Rows[0]["Timer"].ToString();
        //  CheckBox1.Checked = Convert.ToBoolean(dt.Rows[0]["Considerwithinrangedeviationasstandardtime"].ToString());


        if (Convert.ToString(dt.Rows[0]["op2graceperiod"]) == "2")
        {
            rdru2.SelectedIndex = 1;
            // ddlpertype.SelectedValue = Convert.ToString(dt.Rows[0]["op2intancepertype"]);
            txtconsi.Text = "";
            txtconsi.Text = Convert.ToString(dt.Rows[0]["Considerinoutrangeintance"]);
            chkexedlimit2.Checked = Convert.ToBoolean(dt.Rows[0]["payempbatchhours"]);
            chkwaeningemail2.Checked = Convert.ToBoolean(dt.Rows[0]["generalwarningmail"]);
            chkwaeningemail2_CheckedChanged(sender, e);
            chkemailatt.Checked = Convert.ToBoolean(dt.Rows[0]["attendencemail"]);
            chkmailsuper.Checked = Convert.ToBoolean(dt.Rows[0]["supermail"]);
            chkmailappadmin.Checked = Convert.ToBoolean(dt.Rows[0]["attadminmail"]);
            chkmailparent.Checked = Convert.ToBoolean(dt.Rows[0]["parentmail"]);
            txtpanliseemp.Text = Convert.ToString(dt.Rows[0]["panliseamt"]);
            chkpenaliseemp.Checked = Convert.ToBoolean(dt.Rows[0]["panliseapp"]);
        }
        else
        {
            rdru2.SelectedIndex = 0;
        }
        rdru2_SelectedIndexChanged(sender, e);
        if (Convert.ToString(dt.Rows[0]["withoutverification"]) != "")
        {

            if (Convert.ToBoolean(dt.Rows[0]["withoutverification"]) == true)
            {
                chkallowedempname.Checked = true;

            }
            else
            {
                chkallowedempname.Checked = false;
            }
        }

        if (Convert.ToString(dt.Rows[0]["allowedmultipleentry"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["allowedmultipleentry"]) == true)
            {
                chkinoutday.Checked = true;
            }
            else
            {
                chkinoutday.Checked = false;
            }
        }
        txtemailnotific.Text = Convert.ToString(dt.Rows[0]["notificemailallowedhours"]);
        if (Convert.ToString(dt.Rows[0]["generatenotificemail"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["generatenotificemail"]) == true)
            {
                chkemailnotif.Checked = true;
                chlemailsuper.Checked = Convert.ToBoolean(dt.Rows[0]["notificmailsuper"]);
                chkemaileppadd.Checked = Convert.ToBoolean(dt.Rows[0]["notificmailattendanceadmin"]);

            }
            else
            {
                chkemailnotif.Checked = false;
                chlemailsuper.Checked = false;
                chkemaileppadd.Checked = false;

            }
        }
        if (txtemailnotific.Text == "")
        {
            txtemailnotific.Text = "0";
        }
        txtblackin.Text = Convert.ToString(dt.Rows[0]["blackattendanceminit"]);
        if (txtblackin.Text == "")
        {
            txtblackin.Text = "0";
        }
        if (Convert.ToString(dt.Rows[0]["blockattendance"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["blockattendance"]) == true)
            {
                chkblachinout.Checked = true;
            }
            else
            {
                chkblachinout.Checked = false;
            }
        }








        if (Convert.ToString(dt.Rows[0]["Maxuserhours"].ToString()) != "")
        {

            txtallowusereg.Text = Convert.ToString(dt.Rows[0]["Maxuserhours"]);

        }
        if (Convert.ToBoolean(dt.Rows[0]["Considerwithinrangedeviationasstandardtime"].ToString()) == true)
        {
            // pnlconsider.Visible = true;
            // pnlrule2.Visible = true;
            txtconsi.Text = Convert.ToString(dt.Rows[0]["Considerinoutrangeintance"]);

        }
        else
        {
            pnlconsider.Visible = false;
            // pnlrule2.Visible = false;
        }

        if (Convert.ToBoolean(dt.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"].ToString()) == true)
        {
            Label17.Visible = true;
            txtreason.Visible = true;
            txtreason.Text = dt.Rows[0]["Showreasonafterinstance"].ToString();
            chkdeviationreason.Checked = Convert.ToBoolean(dt.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"].ToString());
        }

        else
        {
            chkdeviationreason.Checked = Convert.ToBoolean(dt.Rows[0]["ShowtheFieldtorecordthereasonfordeviation"].ToString());
        }

        if (Convert.ToBoolean(dt.Rows[0]["TakeapprovaloftheseniorEmployee"].ToString()) == true)
        {

            //Label20.Visible = true;
            Label18.Visible = true;
            ddlemp.Visible = true;
            txtempapproal.Visible = true;


            txtempapproal.Text = dt.Rows[0]["Takeapprovalafterinstance"].ToString();
            chkempinstance.Checked = Convert.ToBoolean(dt.Rows[0]["TakeapprovaloftheseniorEmployee"].ToString());
        }
        else
        {
            chkempinstance.Checked = Convert.ToBoolean(dt.Rows[0]["TakeapprovaloftheseniorEmployee"].ToString());
        }

        if (Convert.ToBoolean(dt.Rows[0]["Donotallowemployeetomakeentryinregister"].ToString()) == true)
        {

            Label21.Visible = true;
            txtEntry.Visible = true;

            txtEntry.Text = dt.Rows[0]["Donotallowemployeeinstance"].ToString();
            chkMakeEntry.Checked = Convert.ToBoolean(dt.Rows[0]["Donotallowemployeetomakeentryinregister"].ToString());
        }
        else
        {
            chkMakeEntry.Checked = Convert.ToBoolean(dt.Rows[0]["Donotallowemployeetomakeentryinregister"].ToString());
        }
        if (Convert.ToString(dt.Rows[0]["Generalapprovalrule"].ToString()) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["Generalapprovalrule"].ToString()) == true)
            {
                chkgenralrule.Checked = Convert.ToBoolean(dt.Rows[0]["Generalapprovalrule"].ToString());
            }
            else
            {
                chkgenralrule.Checked = false;
                if (Convert.ToString(dt.Rows[0]["Attaprovedemailadmin"]) == "True")
                {
                    chkadminmailapprove.Checked = true;
                }
                else
                {
                    chkadminmailapprove.Checked = false;
                }

            }
        }
        chkgenralrule_CheckedChanged(sender, e);
        if (Convert.ToString(dt.Rows[0]["rulegreatertime"].ToString()) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["rulegreatertime"].ToString()) == true)
            {
                chkgreatertime.Checked = Convert.ToBoolean(dt.Rows[0]["rulegreatertime"].ToString());
                txtouttimegreater.Text = Convert.ToString(dt.Rows[0]["rulegreatertimeinstance"]);
            }
        }
        if (Convert.ToString(dt.Rows[0]["lateentryallowed"].ToString()) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["lateentryallowed"].ToString()) == true)
            {
                chkerlateentry.Checked = Convert.ToBoolean(dt.Rows[0]["lateentryallowed"].ToString());

            }
        }
        if (Convert.ToString(dt.Rows[0]["nooverflunc"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["nooverflunc"]) == true)
            {
                checkbox.Checked = true;
            }
        }
        if (Convert.ToString(dt.Rows[0]["howdoovertime"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["howdoovertime"]) == true)
            {
                chkoverhowdo.Checked = true;

                if (Convert.ToBoolean(dt.Rows[0]["Overtimepara"]) == true)
                {

                    if (Convert.ToString(dt.Rows[0]["overtimeruleno"]) == "2")
                    {
                        rd2.Checked = true;

                        rd2_CheckedChanged(sender, e);
                        ddlrempt2.SelectedIndex = ddlrempt2.Items.IndexOf(ddlrempt2.Items.FindByText("Overtimeremunaration"));

                    }
                    else if (Convert.ToString(dt.Rows[0]["overtimeruleno"]) == "3")
                    {
                        remfill();
                        txtRate.Text = dt.Rows[0]["OvertimepaymentRate"].ToString();
                        txthour.Text = dt.Rows[0]["Overtimehours"].ToString();
                        pnloverA.Visible = true;
                        ddlramu.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Overtimeremunaration"));
                        if (Convert.ToString(dt.Rows[0]["overtomeapproval"]) != "")
                        {

                        }
                        if (Convert.ToString(dt.Rows[0]["overtimerulerange"]) != "")
                        {
                            chkoverA.Checked = Convert.ToBoolean(Convert.ToString(dt.Rows[0]["overtimerulerange"]));

                        }
                        rd3.Checked = true;
                        rd3_CheckedChanged(sender, e);
                    }



                }
            }
        }
        chkoverhowdo_CheckedChanged(sender, e);
        if (Convert.ToString(dt.Rows[0]["overtimerulerange"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["overtimerulerange"]) == true)
            {
                chkappsuper.Checked = true;
            }
        }


        txtdrreport.Text = Convert.ToString(dt.Rows[0]["allowattreportminits"]);
        if (Convert.ToString(dt.Rows[0]["allowattreport"]) != "")
        {
            if (Convert.ToBoolean(dt.Rows[0]["allowattreport"]) == true)
            {
                chkattreport.Checked = true;
                chkattreport_CheckedChanged(sender, e);
                if (Convert.ToString(dt.Rows[0]["reportallowperiod"]) != "")
                {
                    rddreport.SelectedValue = Convert.ToString(dt.Rows[0]["reportallowperiod"]);
                }
                chkrecreportsuper.Checked = Convert.ToBoolean(dt.Rows[0]["reportsuper"]);
                chkrecreportatteadmin.Checked = Convert.ToBoolean(dt.Rows[0]["reportattadmin"]);
                chkrecreportadmin.Checked = Convert.ToBoolean(dt.Rows[0]["reportadmin"]);

                if (Convert.ToString(dt.Rows[0]["reportceo"]) != "")
                {
                    CheckBox1.Checked = Convert.ToBoolean(dt.Rows[0]["reportceo"]);
                }
                if (Convert.ToString(dt.Rows[0]["allowedempreceived"]) != "")
                {
                    rdreportemprec.SelectedValue = Convert.ToString(dt.Rows[0]["allowedempreceived"]);
                    rdreportemprec_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                chkemailnotif.Checked = false;
                chlemailsuper.Checked = false;
                chkemaileppadd.Checked = false;

            }
        }
        else
        {

            chkattreport.Checked = false;
            chkattreport_CheckedChanged(sender, e);
        }
        if (txtdrreport.Text == "")
        {
            txtdrreport.Text = "0";
        }

        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnSubmit.Visible = false;
        pnladd.Visible = true;
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "Edit")
        {
            statuslable.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedDataKey.Value;
            enablecontrol(false);

            edit();

        }
    }
    public void enablecontrol(bool t)
    {

        TextBox1.Enabled = t;
        TextBox2.Enabled = t;

        txtreason.Enabled = t;
        //txtHalfday.Enabled = t;
        txtEntry.Enabled = t;
        txtempapproal.Enabled = t;
        txtallowusereg.Enabled = t;
        // CheckBox1.Enabled = t;
        chkdeviationreason.Enabled = t;
        chkempinstance.Enabled = t;
        chkMakeEntry.Enabled = t;
        //ChkHalfDay.Enabled = t;
        // CheckBox6.Enabled = t;
        txtconsi.Enabled = t;
        // RadioButtonList1.Enabled = t;
        ddlemp.Enabled = t;
        ddlwarehouse.Enabled = t;
        txthour.Enabled = t;
        txtRate.Enabled = t;
        ddlramu.Enabled = t;
        chkgenralrule.Enabled = t;
        chkgreatertime.Enabled = t;
        txtouttimegreater.Enabled = t;

        pnloverA.Enabled = t;


        rd1.Enabled = t;
        rd2.Enabled = t;
        rd3.Enabled = t;




    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        pnlenb.Enabled = true;
        enablecontrol(true);
        btnUpdate.Visible = true;
        btnEdit.Visible = false;
        btnSubmit.Visible = false;

    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str1 = "select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "' and Attendence_Rule_Id<>'" + ViewState["Id"] + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exists";
            Button5_Click(sender, e);
        }
        else
        {
            string remname = "";
            string str = "";
            SqlCommand cmd = new SqlCommand();
            int opid = 0;
            if (rd3.Checked == true)
            {
                remname = ddlramu.SelectedValue;
                opid = 3;
            }
            else if (rd1.Checked == true)
            {
                opid = 1;
            }
            else if (rd2.Checked == true)
            {
                remname = ddlrempt2.SelectedValue;
                opid = 2;
            }
            string upd = "";
            string granceperi = "";
            if (rdru2.SelectedIndex == 0)
            {
                granceperi = "3";
            }
            else
            {
                granceperi = txtconsi.Text;
            }
            upd = ",lateentryallowed='" + chkerlateentry.Checked + "' ,withoutverification='" + chkallowedempname.Checked + "',allowedmultipleentry='" + chkinoutday.Checked + "',generatenotificemail='" + chkemailnotif.Checked + "' " +
                ",notificemailallowedhours='" + txtemailnotific.Text + "',notificmailsuper='" + chlemailsuper.Checked + "',notificmailattendanceadmin='" + chkemaileppadd.Checked + "'" +
                ",blockattendance='" + chkblachinout.Checked + "',blackattendanceminit='" + txtblackin.Text + "',op2graceperiod='" + rdru2.SelectedValue + "'" +
                ",payempbatchhours='" + chkexedlimit2.Checked + "',generalwarningmail='" + chkwaeningemail2.Checked + "',attendencemail='" + chkemailatt.Checked + "',supermail='" + chkmailsuper.Checked + "',attadminmail='" + chkmailappadmin.Checked + "',parentmail='" + chkmailparent.Checked + "'" +
                ",panliseapp='" + chkpenaliseemp.Checked + "',panliseamt='" + txtpanliseemp.Text + "',allowattreport='" + chkattreport.Checked + "',allowattreportminits='" + txtdrreport.Text + "',reportsuper='" + chkrecreportsuper.Checked + "',reportattadmin='" + chkrecreportatteadmin.Checked + "',reportadmin='" + chkrecreportadmin.Checked + "',reportceo='" + CheckBox1.Checked + "',allowedempreceived='" + rdreportemprec.SelectedValue + "',reportallowperiod='" + rddreport.SelectedValue + "',Attaprovedemailadmin='" + chkadminmailapprove.Checked + "',Timer='" + TextBox3.Text + "'  where [AttandanceRule].Attendence_Rule_Id= '" + ViewState["Id"] + "' ";
            if (opid == 3)
            {

                str = " Update  AttandanceRule Set StoreId ='" + ddlwarehouse.SelectedValue + "',AcceptableInTimeDeviationMinutes='" + TextBox1.Text + "',AcceptableOutTimeDeviationMinutes='" + TextBox2.Text + "', " +
           " Considerwithinrangedeviationasstandardtime='" + true + "',ShowtheFieldtorecordthereasonfordeviation='" + chkdeviationreason.Checked + "',Showreasonafterinstance='" + txtreason.Text + "', " +
           " TakeapprovaloftheseniorEmployee = '" + chkempinstance.Checked + "',SeniorEmployeeID='" + ddlemp.SelectedValue + "',Takeapprovalafterinstance='" + txtempapproal.Text + "', " +
       " Donotallowemployeetomakeentryinregister='" + chkMakeEntry.Checked + "',Donotallowemployeeinstance='" + txtEntry.Text + "' " +
       " ,overtimeruleno='" + opid + "',OvertimepaymentRate='" + txtRate.Text + "',Overtimehours='" + txthour.Text + "',Overtimeremunaration='" + remname + "',Overtimepara='1',Generalapprovalrule='" + chkgenralrule.Checked + "',Considerinoutrangeintance='" + granceperi + "',rulegreatertime='" + chkgreatertime.Checked + "',rulegreatertimeinstance='" + txtouttimegreater.Text + "',overtimerulerange='" + chkoverA.Checked + "',Maxuserhours='" + txtallowusereg.Text + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";


            }

            else if (opid == 2)
            {

                str = " Update  AttandanceRule Set StoreId ='" + ddlwarehouse.SelectedValue + "',AcceptableInTimeDeviationMinutes='" + TextBox1.Text + "',AcceptableOutTimeDeviationMinutes='" + TextBox2.Text + "', " +
            " Considerwithinrangedeviationasstandardtime='" + true + "',ShowtheFieldtorecordthereasonfordeviation='" + chkdeviationreason.Checked + "',Showreasonafterinstance='" + txtreason.Text + "', " +
            " TakeapprovaloftheseniorEmployee = '" + chkempinstance.Checked + "',SeniorEmployeeID='" + ddlemp.SelectedValue + "',Takeapprovalafterinstance='" + txtempapproal.Text + "', " +
        " Donotallowemployeetomakeentryinregister='" + chkMakeEntry.Checked + "',Donotallowemployeeinstance='" + txtEntry.Text + "' " +
        " ,Overtimepara='1',overtimeruleno='" + opid + "',Generalapprovalrule='" + chkgenralrule.Checked + "',Considerinoutrangeintance='" + granceperi + "',Overtimeremunaration='" + remname + "',rulegreatertime='" + chkgreatertime.Checked + "',rulegreatertimeinstance='" + txtouttimegreater.Text + "',Maxuserhours='" + txtallowusereg.Text + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";



            }
            else if (opid == 1)
            {
                str = " Update  AttandanceRule Set StoreId ='" + ddlwarehouse.SelectedValue + "',AcceptableInTimeDeviationMinutes='" + TextBox1.Text + "',AcceptableOutTimeDeviationMinutes='" + TextBox2.Text + "', " +
         " Considerwithinrangedeviationasstandardtime='" + true + "',ShowtheFieldtorecordthereasonfordeviation='" + chkdeviationreason.Checked + "',Showreasonafterinstance='" + txtreason.Text + "', " +
         " TakeapprovaloftheseniorEmployee = '" + chkempinstance.Checked + "',SeniorEmployeeID='" + ddlemp.SelectedValue + "',Takeapprovalafterinstance='" + txtempapproal.Text + "', " +
     " Donotallowemployeetomakeentryinregister='" + chkMakeEntry.Checked + "',Donotallowemployeeinstance='" + txtEntry.Text + "' " +
     " ,Overtimepara='0',overtimeruleno='" + opid + "',Generalapprovalrule='" + chkgenralrule.Checked + "',Considerinoutrangeintance='" + granceperi + "',rulegreatertime='" + chkgreatertime.Checked + "',rulegreatertimeinstance='" + txtouttimegreater.Text + "',Maxuserhours='" + txtallowusereg.Text + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";

            }
            else
            {
                str = " Update  AttandanceRule Set StoreId ='" + ddlwarehouse.SelectedValue + "',AcceptableInTimeDeviationMinutes='" + TextBox1.Text + "',AcceptableOutTimeDeviationMinutes='" + TextBox2.Text + "', " +
      " Considerwithinrangedeviationasstandardtime='" + true + "',ShowtheFieldtorecordthereasonfordeviation='" + chkdeviationreason.Checked + "',Showreasonafterinstance='" + txtreason.Text + "', " +
      " TakeapprovaloftheseniorEmployee = '" + chkempinstance.Checked + "',SeniorEmployeeID='" + ddlemp.SelectedValue + "',Takeapprovalafterinstance='" + txtempapproal.Text + "', " +
  " Donotallowemployeetomakeentryinregister='" + chkMakeEntry.Checked + "',Donotallowemployeeinstance='" + txtEntry.Text + "' " +
  " ,Overtimepara='0',overtimeruleno='" + opid + "',Generalapprovalrule='" + chkgenralrule.Checked + "',Considerinoutrangeintance='" + granceperi + "',rulegreatertime='" + chkgreatertime.Checked + "',rulegreatertimeinstance='" + txtouttimegreater.Text + "',Maxuserhours='" + txtallowusereg.Text + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";

            }
            str = str + upd;
            cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            string updetail = "";
            if (chkattreport.Checked == true)
            {
                if (rdreportemprec.SelectedValue == "1" || rdreportemprec.SelectedValue == "2")
                {
                    foreach (GridViewRow item in GridView2.Rows)
                    {

                        string idgen = GridView2.DataKeys[item.RowIndex].Value.ToString();
                        CheckBox chkcheck = (CheckBox)item.FindControl("chkemp");
                        string idap = "";
                        if (chkcheck.Checked == true)
                        {
                            if (rdreportemprec.SelectedValue == "1")
                            {
                                idap = " and EmpId='" + idgen + "'";
                            }
                            else
                            {
                                idap = " and DesignationId='" + idgen + "'";
                            }
                            string asc = "Select * from Attendancerulesendingdailyreport where Whid='" + ddlwarehouse.SelectedValue + "'" + idap;
                            SqlCommand cmade = new SqlCommand(asc, con);
                            SqlDataAdapter asass = new SqlDataAdapter(cmade);
                            DataTable dtade = new DataTable();
                            asass.Fill(dtade);
                            SqlCommand cmd8 = new SqlCommand();
                            if (dtade.Rows.Count == 0)
                            {
                                if (rdreportemprec.SelectedValue == "1")
                                {
                                    cmd8 = new SqlCommand("insert into Attendancerulesendingdailyreport(Whid,DesignationId)Values('" + ddlwarehouse.SelectedValue + "','" + idgen + "')", con);

                                }
                                else
                                {
                                    cmd8 = new SqlCommand("insert into Attendancerulesendingdailyreport(Whid,EmpId)Values('" + ddlwarehouse.SelectedValue + "','" + idgen + "')", con);

                                }
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd8.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                            }
                        }
                        else
                        {

                            if (Convert.ToInt32(item.RowIndex) == (GridView2.Rows.Count))
                            {
                                updetail = updetail + idgen;
                            }
                            else
                            {
                                if (updetail == "")
                                {
                                    updetail = idgen;
                                }
                                else
                                {
                                    updetail = updetail + "," + idgen;
                                }
                            }




                        }
                    }
                    SqlCommand cmd81 = new SqlCommand();
                    if (rdreportemprec.SelectedValue == "1")
                    {
                        cmd81 = new SqlCommand("Delete from Attendancerulesendingdailyreport where  [DesignationId] in(" + updetail + ")", con);

                    }
                    else
                    {
                        cmd81 = new SqlCommand("Delete from Attendancerulesendingdailyreport where EmpId in(" + updetail + ")", con);

                    }
                    if (updetail.Length > 0)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd81.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }




            if (rdru2.SelectedValue == "2")
            {
                foreach (GridViewRow item in grdpay.Rows)
                {

                    string idgen = grdpay.DataKeys[item.RowIndex].Value.ToString();
                    TextBox txtpaytype = (TextBox)item.FindControl("txtpaytype");


                    string asc = "Select * from AttendancePayperiodtype where Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodtypeIdforrule='" + idgen + "'";
                    SqlCommand cmade = new SqlCommand(asc, con);
                    SqlDataAdapter asass = new SqlDataAdapter(cmade);
                    DataTable dtade = new DataTable();
                    asass.Fill(dtade);
                    SqlCommand cmd8 = new SqlCommand();
                    if (dtade.Rows.Count == 0)
                    {

                        cmd8 = new SqlCommand("insert into AttendancePayperiodtype(PayperiodtypeIdforrule,payruletime,Whid)Values('" + idgen + "','" + txtpaytype.Text + "','" + ddlwarehouse.SelectedValue + "')", con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd8.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        cmd8 = new SqlCommand("Update AttendancePayperiodtype Set payruletime='" + txtpaytype.Text + "' where PayperiodtypeIdforrule='" + idgen + "' and Whid='" + ddlwarehouse.SelectedValue + "'", con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd8.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }

            string stratt = "select Role_id from RoleMaster where Role_name='Senior Employee-Attendence Matter' and compid='" + Session["comid"] + "' ";
            SqlCommand cmd3att = new SqlCommand(stratt, con);
            SqlDataAdapter adpatt = new SqlDataAdapter(cmd3att);
            DataTable dtatt = new DataTable();
            adpatt.Fill(dtatt);

            if (dtatt.Rows.Count > 0)
            {
                string roleid = dtatt.Rows[0]["Role_id"].ToString();
                string stratt1 = "Select distinct User_Master.UserId from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master on User_Master.PartyId=Party_Master.PartyId where EmployeeMaster.EmployeeMasterId='" + ddlemp.SelectedValue + "' ";
                SqlCommand cmd3att1 = new SqlCommand(stratt1, con);
                SqlDataAdapter adpatt1 = new SqlDataAdapter(cmd3att1);
                DataTable dtatt1 = new DataTable();
                adpatt1.Fill(dtatt1);

                if (dtatt1.Rows.Count > 0)
                {
                    string stratt11 = "Select distinct User_Master.UserId from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join User_Master on User_Master.PartyId=Party_Master.PartyId where EmployeeMaster.EmployeeMasterId='" + ViewState["ei"] + "' ";
                    SqlCommand cmd3att11 = new SqlCommand(stratt11, con);
                    SqlDataAdapter adpatt11 = new SqlDataAdapter(cmd3att11);
                    DataTable dtatt11 = new DataTable();
                    adpatt1.Fill(dtatt11);
                    if (dtatt11.Rows.Count > 0)
                    {
                        SqlCommand cmd8 = new SqlCommand("Update User_Role Set User_id='" + dtatt1.Rows[0]["UserId"] + "',ActiveDeactive='true' where Role_id='" + roleid + "' and User_id='" + dtatt11.Rows[0]["UserId"] + "' ", con);
                        con.Open();
                        cmd8.ExecuteNonQuery();
                        con.Close();
                    }
                }



            }
            statuslable.Visible = true;
            statuslable.Text = "Record updated successfully";
            pnlenb.Enabled = false;
            fillgrid();


            TextBox1.Text = "15";
            TextBox2.Text = "15";
            txtallowusereg.Text = "4";
            txtreason.Text = "5";
            txtEntry.Text = "5";
            txtempapproal.Text = "5";
            txtconsi.Text = "3";
            // txtHalfday.Text = "";
            chkgenralrule.Checked = false;
            chkgreatertime.Checked = true;
            txtouttimegreater.Text = "6";
            //CheckBox1.Checked = false;
            CheckBox1_CheckedChanged(sender, e);
            chkdeviationreason.Checked = false;
            // CheckBox2_CheckedChanged(sender, e);
            chkempinstance.Checked = false;
            //CheckBox3_CheckedChanged(sender, e);
            chkMakeEntry.Checked = false;
            // CheckBox4_CheckedChanged(sender, e);
            //  ChkHalfDay.Checked = false;


            btnUpdate.Visible = false;
            // btnSubmit.Visible = true;
            btnEdit.Visible = false;
            fillgrid();

            pnladd.Visible = false;
        }
    }
    //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView1.EditIndex = e.NewEditIndex;
    //    fillgrid();

    //}
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        string str = "Select distinct EmployeeMasterID,EmployeeName from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId where Whid='" + ddlwarehouse.SelectedValue + "' and DesignationMaster.DesignationName in ('Office Manager','Administrative Assistant','CEO','Admin')  order by EmployeeName";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlemp.DataSource = ds;
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataValueField = "EmployeeMasterID";
        ddlemp.DataBind();
        //ddlemp.Items.Insert(0, "--Select--");
        //ddlemp.Items[0].Value = "0";



        remfill();



        fillgrid();
        string stmaster = "Select  * from CompanyWebsitMaster  where Whid='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmds = new SqlCommand(stmaster, con);

        SqlDataAdapter daa = new SqlDataAdapter(cmds);

        DataTable dsas = new DataTable();
        daa.Fill(dsas);
        if (dsas.Rows.Count > 0)
        {
            if (Convert.ToString(dsas.Rows[0]["MasterEmailId"]) == "")
            {
                txtemailnotific.Enabled = false;
                //chkemailnotif.Enabled = false;
                pnlemnot.Visible = false;
                pnlattapmail.Enabled = false;
            }
            else
            {
                txtemailnotific.Enabled = true;
                //chkemailnotif.Enabled = true;
                pnlemnot.Visible = true;
                pnlattapmail.Enabled = true;
            }

        }

    }


    protected void remfill()
    {
        ddlrempt2.Items.Clear();
        ddlramu.Items.Clear();
        string str = "Select   distinct Id,RemunerationName from RemunerationMaster  where Whid='" + ddlwarehouse.SelectedValue + "'  order by RemunerationName";
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable ds = new DataTable();
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddlramu.DataSource = ds;
            ddlramu.DataTextField = "RemunerationName";
            ddlramu.DataValueField = "Id";
            ddlramu.DataBind();
            ddlramu.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Salary&Wages"));
            ddlrempt2.DataSource = ds;
            ddlrempt2.DataTextField = "RemunerationName";
            ddlrempt2.DataValueField = "Id";
            ddlrempt2.DataBind();
            ddlrempt2.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Salary&Wages"));
        }
    }





    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //if (CheckBox1.Checked == true)
        //{
        //    pnlrule2.Visible = true;
        //    pnlconsider.Visible = true;
        //}
        //else
        //{
        //    pnlrule2.Visible = false;
        //    pnlconsider.Visible = false;
        //}
    }
    protected void rd2_CheckedChanged(object sender, EventArgs e)
    {
        if (rd2.Checked == true)
        {


            pnloverA.Visible = false;
            ddlrempt2.Visible = true;
            rd1.Checked = false;
            rd3.Checked = false;
        }

    }
    protected void rd1_CheckedChanged(object sender, EventArgs e)
    {

        pnloverA.Visible = false;
        ddlrempt2.Visible = false;
        rd3.Checked = false;
        rd2.Checked = false;

    }
    protected void rd3_CheckedChanged(object sender, EventArgs e)
    {
        if (rd3.Checked == true)
        {

            pnloverA.Visible = true;
            ddlrempt2.Visible = false;
            rd1.Checked = false;
            rd2.Checked = false;
            remfill();
        }


    }
    protected void rdru2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdru2.SelectedValue == "2")
        {
            pnlconsider.Visible = true;
            string str = "";


            str = "select payperiodtype.Id,Name,Case When(payruletime IS NULL) then '0' else payruletime end as aspayruletime from AttendancePayperiodtype Right Join  payperiodtype on payperiodtype.Id=AttendancePayperiodtype.PayperiodtypeIdforrule and AttendancePayperiodtype.Whid='" + ddlwarehouse.SelectedValue + "' where payperiodtype.Id in(2,3,4,5,10,11,12)   order by  payperiodtype.Id";

            SqlCommand cmd = new SqlCommand(str, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);
            grdpay.DataSource = ds;
            grdpay.DataBind();


        }
        else
        {
            pnlconsider.Visible = false;
        }
    }
    protected void chkwaeningemail2_CheckedChanged(object sender, EventArgs e)
    {
        if (chkwaeningemail2.Checked == true)
        {
            pnlemailgene.Visible = true;
            chkemailatt.Checked = true;
            chkemaileppadd.Checked = true;
            chkmailsuper.Checked = true;
            chkmailparent.Checked = false;
        }
        else
        {
            pnlemailgene.Visible = false;
        }
    }
    protected void chkoverhowdo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkoverhowdo.Checked == true)
        {
            pnloverop2radio.Visible = true;

        }
        else
        {
            pnloverop2radio.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {

            Button1.Text = "Hide Printable Version";
            Button3.Visible = true;



            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }

        }
        else
        {

            Button1.Text = "Printable Version";
            Button3.Visible = false;


            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }

        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
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
    protected void btnemaiview_Click(object sender, EventArgs e)
    {
        //string te = "EmailContentMaster.aspx?Wid="+ddlwarehouse.SelectedValue+"";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtender3.Show();
    }
    protected void rdreportemprec_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "";
        if (rdreportemprec.SelectedValue == "1")
        {

            str = "Select   distinct DesignationMasterId as EmpId,DesignationName as Empname,Case when(Attendancerulesendingdailyreport.DesignationId IS NULL) then cast( '0' as bit) else cast( '1' as bit) end as chapp  from Attendancerulesendingdailyreport Right join DesignationMaster on Attendancerulesendingdailyreport.DesignationId= DesignationMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=DesignationMaster.DeptId  where DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "'  order by DesignationName";
            GridView2.Columns[0].HeaderText = "Select Designation";
        }
        else
        {
            str = "Select   distinct EmployeeMasterID as EmpId,EmployeeName as Empname,Case when(Attendancerulesendingdailyreport.EmpId IS NULL) then cast( '0' as bit) else cast( '1' as bit) end as chapp  from Attendancerulesendingdailyreport Right join EmployeeMaster on Attendancerulesendingdailyreport.EmpId= EmployeeMaster.EmployeeMasterID   where EmployeeMaster.Whid='" + ddlwarehouse.SelectedValue + "'  order by EmployeeName";
            GridView2.Columns[0].HeaderText = "Select Employee(s)";
        }
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable ds = new DataTable();
        da.Fill(ds);
        GridView2.DataSource = ds;
        GridView2.DataBind();
        pnlcf.Visible = true;
        if (pnlcf.Visible == true)
        {
            pnlcf.ScrollBars = ScrollBars.Vertical;
            pnlcf.Height = new Unit(100);

        }
        else
        {
            pnlcf.ScrollBars = ScrollBars.Vertical;
            pnlcf.Height = new Unit(100);
        }
    }

    protected void chkattreport_CheckedChanged(object sender, EventArgs e)
    {
        if (chkattreport.Checked == true)
        {
            pnlattrepo.Visible = true;
        }
        else
        {
            pnlattrepo.Visible = false;
        }
    }

    protected void chkgenralrule_CheckedChanged(object sender, EventArgs e)
    {
        if (chkgenralrule.Checked == false)
        {
            pnlattapmail.Visible = true;
        }
        else
        {
            pnlattapmail.Visible = false;
        }

    }

    protected void chkemailnotif_CheckedChanged(object sender, EventArgs e)
    {
        if (chkemailnotif.Checked == true)
        {
            SqlDataAdapter da = new SqlDataAdapter("select MasterEmailId from CompanyWebsitMaster where whid='" + ddlwarehouse.SelectedValue + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["MasterEmailId"]) != "")
                {
                    pnlemnot.Visible = true;
                    txtemailnotific.Enabled = true;


                }
                else
                {
                    chkemailnotif.Checked = false;
                    pnlemnot.Visible = false;
                    ModalPopupExtender11.Show();
                }
            }
            else
            {
                chkemailnotif.Checked = false;
                pnlemnot.Visible = false;
                ModalPopupExtender11.Show();
            }
        }
        else
        {
            pnlemnot.Visible = false;
        }
    }
    protected void chkemp_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GridView2.Rows)
        {
            string idgen = GridView2.DataKeys[item.RowIndex].Value.ToString();
            CheckBox chkcheck = (CheckBox)item.FindControl("chkemp");

            Label Label67 = (Label)item.FindControl("Label67");

            if (chkcheck.Checked == true)
            {
                if (rdreportemprec.SelectedValue == "1")
                {
                    SqlDataAdapter da12 = new SqlDataAdapter("select EmailId from InOutCompanyEmail inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.DesignationMasterID='" + Label67.Text + "'", con);
                    DataTable dt12 = new DataTable();
                    da12.Fill(dt12);

                    if (dt12.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt12.Rows.Count; i++)
                        {

                            if (Convert.ToString(dt12.Rows[i]["EmailId"]) != "")
                            {
                                ViewState["emnam"] = "mas";
                            }

                        }
                    }
                    else
                    {
                        ViewState["emnam"] = "";

                        Label89.Text = " any employee with this designation.";
                        chkcheck.Checked = false;
                        ModalPopupExtender1.Show();
                    }


                    if (Convert.ToString(ViewState["emnam"]) != "mas")
                    {
                        ViewState["emnam"] = "";

                        Label89.Text = " any employee with this designation.";
                        chkcheck.Checked = false;
                        ModalPopupExtender1.Show();
                    }
                }
                else
                {
                    SqlDataAdapter da12 = new SqlDataAdapter("select EmailId from InOutCompanyEmail inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeID where EmployeeMaster.EmployeeMasterID='" + Label67.Text + "'", con);
                    DataTable dt12 = new DataTable();
                    da12.Fill(dt12);

                    if (dt12.Rows.Count > 0)
                    {
                        //if (Convert.ToString(dt12.Rows[0]["EmailId"]) != "")
                        //{

                        //}
                        //else
                        //{
                        //    Label89.Text = " this Employee.";
                        //    chkcheck.Checked = false;

                        //    ModalPopupExtender1.Show();
                        //}
                    }
                    else
                    {
                        Label89.Text = " this Employee.";
                        chkcheck.Checked = false;

                        ModalPopupExtender1.Show();
                    }
                }
            }
        }
    }
}
