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


public partial class Admin_BusiwizMasterInfoTbl : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataTable dt;
    SqlCommand cmd1;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string str22 = " SELECT * from BusiwizMasterInfoTbl ";
        SqlCommand cmd2 = new SqlCommand(str22, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
        DataSet dt1 = new DataSet();
        adp1.Fill(dt1);
        if (dt1.Tables[0].Rows.Count == 0)
        {
            string buz = "insert into BusiwizMasterInfoTbl(ID,Name,LogoURL,PaypalID,PaypalNotifyURL,PaypalCancelURL,PaypalReturnURL,PaymentNotifyURL)values('1','" + txtname.Text.Trim() + "','" + ViewState["upfile"] + "','" + txtpaypalid.Text.Trim() + "','" + txtpaypalnotifyurl.Text.Trim() + "','" + txtpaypalcancelurl.Text.Trim() + "','" + txtpaypalreturnurl.Text.Trim() + "','" + txtpaypalpaymenturl.Text.Trim() + "')";
            SqlCommand cmd1 = new SqlCommand(buz, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            string buz = "update BusiwizMasterInfoTbl set Name='" + txtname.Text.Trim() + "',LogoURL='" + ViewState["upfile"] + "',PaypalID='" + txtpaypalid.Text.Trim() + "',PaypalNotifyURL='" + txtpaypalnotifyurl.Text.Trim() + "',PaypalCancelURL='" + txtpaypalnotifyurl.Text.Trim() + "',PaypalReturnURL='" + txtpaypalreturnurl.Text.Trim() + "',PaymentNotifyURL='" + txtpaypalpaymenturl.Text.Trim() + "' where ID= '" + dt1.Tables[0].Rows[0]["ID"] + "'";
            SqlCommand cmd1 = new SqlCommand(buz, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void Butsubmit_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload1.FileName);
            ViewState["upfile"] = FileUpload1.FileName;
        }
    }
}
