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

public partial class IOffice_ShoppingCart_Admin_SatelliteServerSetupUtilityAddmanage : System.Web.UI.Page
{
    bool gg;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldropdown();
            fillgrid();
            

        }
    }
    public void filldropdown()
    {

        string selct = "select * from OperatingSystemsMaster ";
        SqlCommand cmd = new SqlCommand(selct, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            DropDownList2.DataSource = dt;
            DropDownList2.DataValueField = "Operatingid";
            DropDownList2.DataTextField = "Operatingsystemname";
            DropDownList2.DataBind();


        }
    
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        if (FileUpload1.HasFile == true)
        {

            string str = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (str.ToLower())
            {
                case ".zip":
                    break;

            }
            if (str == ".zip")
            {

                if (Directory.Exists(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc"));
                }
                string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
                Session["filename"] = filename;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\") + filename);
                Label41.Text = "Uploaded:" + filename + "";
                lblmsg.Text = "uploaded successfully";
            }
            else
            {
                lblmsg.Text = "Invalid file extension.";


            }
        }
        else
        {
            lblmsg.Text = "select the file.";
        }
    }
    public void ftp()
    {
        string strFTPPath = "ftp://" + TextBox1.Text + ":" + TextBox3.Text;
        string locationt = Server.MapPath("~//Account//" + Session["Comid"] + "//TempDoc//");
        
        string strFilInfo = locationt.ToString() + Session["filename"].ToString();
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create(strFTPPath.ToString() +"//"+ Session["filename"].ToString());

        oFTP.Credentials = new NetworkCredential(TextBox4.Text, TextBox2.Text);
        oFTP.UseBinary = false;
        oFTP.UsePassive = true;
        oFTP.Method = WebRequestMethods.Ftp.UploadFile;

        FileInfo fileInfo = new FileInfo(strFilInfo);
        FileStream fileStream = fileInfo.OpenRead();

        int bufferLength = 2048;
        byte[] buffer = new byte[bufferLength];

        Stream uploadStream = oFTP.GetRequestStream();
        int contentLength = fileStream.Read(buffer, 0, bufferLength);

        while (contentLength != 0)
        {
            uploadStream.Write(buffer, 0, contentLength);
            contentLength = fileStream.Read(buffer, 0, bufferLength);
        }

        uploadStream.Close();
        fileStream.Close();

        oFTP = null;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

      
        ftp();

        DataTable sele = select("select SatelliteServerSetupMaster.ID from OperatingSystemsMaster inner join SatelliteServerSetupMaster on SatelliteServerSetupMaster.OpertatingSystemID=OperatingSystemsMaster.ID where OpertatingSystemID='" + DropDownList1.SelectedValue + "' ");
        if (sele.Rows.Count > 0)
        {
        //    Session["id"]=sele.Rows[0]["ID"].ToString();
        //    Button2_Click(sender,e);
            lblmsg.Text = "Already exist";
        }
        else
        {

            con.Open();
            string str = "insert into SatelliteServerSetupMaster values('" + DropDownList2.SelectedValue + "','" + Session["filename"].ToString() + "','" + TextBox1.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox2.Text + "','" + TextBox5.Text + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            fillgrid();
            Panel3.Visible = false;
            lblmsg.Text = "Record inserted successfully";
            con.Close();
            Button4.Visible = true;
        }

    }
    public void fillgrid()
    {
        string selct = "select * from OperatingSystemsMaster inner join SatelliteServerSetupMaster on OperatingSystemsMaster.Operatingid= SatelliteServerSetupMaster.OpertatingSystemID ";
        SqlCommand cmd = new SqlCommand(selct, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            GridView1.DataBind();


        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        con.Open();
        string str = "update  SatelliteServerSetupMaster set OpertatingSystemID='" + DropDownList2.SelectedValue + "',FileName='" + Session["filename"].ToString() + "',FTPURL='" + TextBox1.Text + "',Port='" + TextBox3.Text + "',Userid='" + TextBox4.Text + "',Password='" + TextBox2.Text + "',FolderName='" + TextBox5.Text + "' where Id='" + Session["id"].ToString() +"' ";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();
        fillgrid();
        Panel3.Visible = false;
        lblmsg.Text = "Record update successfully";
        con.Close();
        Button4.Visible = true;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button4.Visible = true;
        Panel3.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {

            con.Open();
            string str = "delete from SatelliteServerSetupMaster where  Id='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            fillgrid();

            lblmsg.Text = "Record delete successfully";
            fillgrid();
            con.Close();
        }


    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selct = "select * from OperatingSystemsMaster where Operatingsystemname='"+DropDownList2.SelectedItem.Text+"'";
        SqlCommand cmd = new SqlCommand(selct, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
          
          DropDownList1.SelectedItem.Text=dt.Rows[0]["Type"].ToString();

        }

    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        gg = isValidConnection(TextBox1.Text, TextBox4.Text, TextBox2.Text);
        if (gg)
        {
           // Label1.Visible = true;
            lblmsg0.Text = "Read successful and Write successful";
        }
        else
        {
           // Label1.Visible = true;
            lblmsg0.Text = "Read not successful and Write not successful";
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
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(TextBox3.Text);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(TextBox3.Text);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(TextBox3.Text);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(TextBox3.Text);

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



    protected void imgbtnedit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label ld = (Label)GridView1.Rows[j].FindControl("Label8");
        Panel3.Visible = true;
        Button9.Visible = false;
        Button2.Visible = true;



        Session["id"] = ld.Text;
        SqlCommand cmd = new SqlCommand("select * from OperatingSystemsMaster inner join SatelliteServerSetupMaster on OperatingSystemsMaster.Id= SatelliteServerSetupMaster.OpertatingSystemID where SatelliteServerSetupMaster.Id='" + ld.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtbn = new DataTable();
        da.Fill(dtbn);
        if (dtbn.Rows.Count > 0)
        {

            TextBox5.Text = dtbn.Rows[0]["FolderName"].ToString();
            TextBox1.Text = dtbn.Rows[0]["FTPURL"].ToString();
            TextBox3.Text = dtbn.Rows[0]["Port"].ToString();
            TextBox4.Text = dtbn.Rows[0]["Userid"].ToString();
            TextBox2.Text = dtbn.Rows[0]["Password"].ToString();
            DropDownList2.SelectedItem.Text = dtbn.Rows[0]["Operatingsystemname"].ToString();
            DropDownList1.SelectedItem.Text = dtbn.Rows[0]["Type"].ToString();


        }


    }









    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
    }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        Panel3.Visible = true;
    }
    protected void Button4_Click2(object sender, EventArgs e)
    {
        Panel3.Visible = true;
        Button4.Visible = false;

    }
}