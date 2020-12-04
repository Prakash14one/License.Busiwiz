using System;
using System.Net;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
public partial class Admin_VaultDeviceDetailTbl : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    double numbers;
    string arith = "";
    int SeedNumber;
    int hopno;
    int counter;
    public StringBuilder strplan = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        //HyperLink1.Visible = false;
        lblmsg.Text = "";
        if (!IsPostBack)
        {

            fillvdm();
            //fillgrid();
        }
    }
    protected void fillvdm()
    {
        string strcln = "SELECT Id, Cast(Id as nvarchar(15))+':'+ Name as iname from VaultDeviceMaster where Active='True' order by iname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlvdm.DataSource = dtcln;
        ddlvdm.DataValueField = "Id";
        ddlvdm.DataTextField = "iname";
        ddlvdm.DataBind();
        ddlvdm.Items.Insert(0, "-Select-");
        ddlvdm.Items[0].Value = "0";
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "Select VaultDeviceMaster.*,VaultDeviceType.Name as name1 from VaultDeviceMaster inner join  VaultDeviceType on VaultDeviceMaster.DeviceTypeID=VaultDeviceType.Id order by VaultDeviceMaster.name";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        
        string str = "";
        try
        {
            if (btnadd.Text == "Update")
            {
                //str = "Update VaultDeviceDetailTbl Set number='" + Txtaddname.Text.Trim() + "',Used='" + txtused.Text.Trim() + "',vaultdevicemasterid='" + ddlvdm.SelectedValue + "' where ID='" + ViewState["ID"] + "'";

                lblmsg.Text = "Record Updated Successfully.";
            }
            else
            {

                
                string strcln = "Select * from VaultDeviceMaster where Id='"+ ddlvdm.SelectedValue+"'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count > 0)
                {
                    arith = dtcln.Rows[0]["Arithmaticoperator"].ToString();
                           SeedNumber =Convert.ToInt32(dtcln.Rows[0]["SeedNumber"].ToString());
                           hopno = Convert.ToInt32(dtcln.Rows[0]["HopNumber"].ToString());
                   counter=hopno;
                }
                for (int i = 0; i < 100; i++)
                {
                    string spp = "select Randomnumber from Randomnm where Id='" + hopno.ToString() + "'";
                    SqlCommand sqlco = new SqlCommand(spp, con);
                    con.Open();
                    object rnu = sqlco.ExecuteScalar();
                    con.Close();
                    if (arith == "*")
                    {
                        numbers = Convert.ToDouble(rnu) * (Convert.ToDouble(hopno));
                    }
                    else if (arith == "+")
                    {
                        numbers = Convert.ToDouble(rnu) + (Convert.ToDouble(hopno));
                    }
                    else if (arith == "-")
                    {
                        numbers = Convert.ToDouble(rnu) - (Convert.ToDouble(hopno));
                    }
                    else if (arith == "/")
                    {
                        numbers = Convert.ToDouble(rnu) / (Convert.ToDouble(hopno));
                    }
                    str = "INSERT INTO VaultDeviceDetailTbl(number,Used,vaultdevicemasterid)values('"+numbers+"','" + txtused.Text.Trim() + "','" + ddlvdm.SelectedValue + "')";
                    lblmsg.Text = "Record insert Successfully.";
                    SqlCommand cmd = new SqlCommand(str, con);
                    DataTable dt = new DataTable();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    btnadd.Text = "Submit";
                    hopno = hopno + counter;
                }
               
                
               
            }
            txtused.Text = "";
            ddlvdm.SelectedIndex = 0;        
            clearall();
        }
        catch
        {
        }

    }

    protected void clearall()
    {
      
    }
    
    protected void GridView1_PageIndexChanging(object sender,GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
    {
                                                                                                                                                                                                                                                                   
    }
    protected void btntxt_Click(object sender, EventArgs e)
    {
        string sth;
        StreamWriter objsw;
        objsw=File.CreateText(MapPath("TextFile/"+ddlvdm.SelectedValue+".txt"));
        sth=  ddlvdm.SelectedValue + ".txt";
        strplan.Append(sth);
        string strcln = "Select number from VaultDeviceDetailTbl where vaultdevicemasterid='" + ddlvdm.SelectedValue + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                for (int i = 0; i < dtcln.Rows.Count; i++)
                {
                    objsw.WriteLine("" + dtcln.Rows[i]["number"] + "");
                }
            
        objsw.Close();
        //HyperLink1.Visible = true;
    }
    protected void download_Click(object sender, EventArgs e)
    {
        HyperLink1.NavigateUrl = "~/Admin/TextFile/5.txt";
       
    }
}
