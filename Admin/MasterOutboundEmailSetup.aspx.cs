using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class WebMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    #region Pageload Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldata();
        }
    }
    #endregion

    #region filldata
    public void filldata()
    {
        string str = "Select * From MasterOutBoundEmail";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtemailid.Text = dt.Rows[0]["MasterOutBoundEmail_OutboundEmailId"].ToString();
            txtoutboundemailserver.Text = dt.Rows[0]["MasterOutBoundEmail_OutboundEmailServer"].ToString();
            txtpassword.Text = dt.Rows[0]["MasterOutBoundEmail_OutboundPassword"].ToString();
            txttitleofemailaccount.Text = dt.Rows[0]["MasterOutBoundEmail_OutboundEmailAccount"].ToString();
           
        }
    }

    #endregion

    #region btnsetupclick ButtonEvent
    protected void btnsetup_Click(object sender, EventArgs e)
    {
        //   String SubMenuInsert = "Insert Into MasterOutboundEmail (MasterOutBoundEmail_OutboundEmailServer,MasterOutBoundEmail_OutboundEmailId,MasterOutBoundEmail_OutboundPassword,MasterOutBoundEmail_OutboundEmailAccount) values ('" + txtoutboundemailserver.Text + "','" + txtemailid.Text + "','" + txtpassword.Text + "','" + txttitleofemailaccount.Text + "')";
        String SubMenuUpdate = "Update  MasterOutboundEmail set MasterOutBoundEmail_OutboundEmailServer='" + txtoutboundemailserver.Text + "',MasterOutBoundEmail_OutboundEmailId ='" + txtemailid.Text + "',MasterOutBoundEmail_OutboundPassword ='" + txtpassword.Text + "',MasterOutBoundEmail_OutboundEmailAccount ='" + txttitleofemailaccount.Text + "' ";
        SqlCommand cmd = new SqlCommand(SubMenuUpdate, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Your Data Successfully Updated !!! ";
        txtemailid.Enabled = txtoutboundemailserver.Enabled = txtpassword.Enabled = txttitleofemailaccount.Enabled = false;
        filldata();
        btnsetup.Visible = false;
    }
    #endregion

    #region btnedit buttonClickevent
    protected void btnedit_Click(object sender, EventArgs e)
    {
        txtemailid.Enabled = txtoutboundemailserver.Enabled = txtpassword.Enabled = txttitleofemailaccount.Enabled = true;
        btnsetup.Visible = true;
    }
    #endregion
}