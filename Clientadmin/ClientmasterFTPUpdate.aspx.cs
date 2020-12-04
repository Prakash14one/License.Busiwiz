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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

public partial class MainMenuMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    bool gg;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ClientId"] = 35;
            ViewState["sortOrder"] = ""; 
            Fillgrid();
            lbllegend.Text = "Update Client FTP Detail";
            pnladdnew.Visible = true;
        }

    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {
       

    }

    protected void Fillgrid()
    {
       string finalstr = " Select * From ClientMaster Where ClientMasterId='" + Session["ClientId"] + "'";

        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        if (ds.Rows.Count > 0)
        {
            txt_serverftp.Text = ds.Rows[0]["FTP"].ToString();
            txtftpuserid.Text = ds.Rows[0]["FTPUserName"].ToString();
            txt_password.Text = ds.Rows[0]["FTPPassword"].ToString();
            txtFtpPort.Text = ds.Rows[0]["FTPPort"].ToString();
        }
        else
        {
          

        }

    }
      
  
    protected void Buttonupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "update   ClientMaster  set " +
               " FTP='" + txt_serverftp.Text + "',FTPUserName='" + txtftpuserid.Text + "',FTPPassword='" + txt_password.Text + "',FTPPort='"+txtFtpPort.Text +"' where ClientMasterId =" + Session["ClientId"];
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = " Updated Successfully.";
           // Response.Redirect("ClientList.aspx");
        }
        catch (Exception err)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error ;" + err.Message;
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lbllegend.Text = "Update Client FTP Detail";
        lblmsg.Text = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        gg = isValidConnection(txt_serverftp.Text, txtftpuserid.Text, txt_password.Text);
        if (gg)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Read and Write successful";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Read and Write not successful";
        }

    }
    private bool isValidConnection(string url, string user, string password)
    {
        try
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = url.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(txtFtpPort.Text);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(txtFtpPort.Text);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(txtFtpPort.Text);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(txtFtpPort.Text);

                }

            }
            string ftphost = ftpurl;



            // FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftphost);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(user, password);
            request.GetResponse();
        }
        catch (WebException ex)
        {
            return false;
        }
        return true;
    }
   
}
