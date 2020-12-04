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
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
//using ForAspNet.POP3;
using System.Runtime.InteropServices;
//using UtilitiesFTp.FTP;

using System.Data.SqlClient;




public partial class ShoppingCart_Admin_TransferImages : System.Web.UI.Page
{
    string location, insertimagesquery, updatequery, MASTERID, folderpath, typee;
    int viewid;
    Int32 countproductno1;
    List<string> oList1 = new List<string>();
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        Label2.Visible = false;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        
        Page.Title = pg.getPageTitle(page); 
    }
    protected void Btnimages_Click(object sender, EventArgs e)
    {
        try
        {
            Label2.Visible = false;
            grd.Visible = true;
            string productno, main1, type;


            {
                string name = "";


                //   string ftp = "192.168.6.49";

               // string ftp = txtftp.Text;


                //string ftp1 = "ftp://192.168.6.49:21/im2/";

                string ftpportno = txtportNo.Text;
                string foldernamefromftp = txtfoldername.Text;
                string fullftppath = "ftp://" + ftpportno + "/" + foldernamefromftp + "/";
                //string ftp1 = "ftp://192.168.6.49:21/im2/";
                string ftp1 = fullftppath;

                lblno.Text = ftp1;
                //  string username = "CommonAccount";

                string username = txtusername.Text;

                //string password = "User09++";

                string password = txtpassword.Text;
             //   DirectoryInfo dir = new DirectoryInfo(ftp);
                FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create(ftp1);

                oFTP.Credentials = new NetworkCredential(username, password);
                oFTP.UseBinary = false;
                oFTP.UsePassive = false;
                oFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)oFTP.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string str = sr.ReadLine();
                List<string> oList = new List<string>();
                while (str != null)
                {
                    if (str.Length > 3)
                    {
                        name = str.Trim();
                        string extension = name.Substring(name.Length - 3);
                        //  if (Convert.ToString(extension) == "jpg")
                        {
                            oList.Add(str);
                        }

                    }
                    str = sr.ReadLine();

                }
                sr.Close();
                response.Close();
                oFTP = null;

                int j = oList.Count;
                for (int i = 0; i < j; i++)
                {

                    
                    //string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + oList[i].ToString();
                    string filename = oList[i].ToString();
                    {
                        lblno.Text = "Image No:" + (i+1) + "-->";
                        Label6.Text = filename;
                    string[] separator1 = new string[] { "_" };
                    string[] strSplitArr1 = filename.Split(separator1,StringSplitOptions.RemoveEmptyEntries);
                  // int i111 = Convert.ToInt32(strSplitArr1.Length);
                    // if (i111 == 2)
                    
                        productno = strSplitArr1[0].ToString();
                        main1 = strSplitArr1[1].ToString();
                        type = strSplitArr1[2].ToString();

                        char[] separator2 = new char[] { '.' };
                        string[] strSplitArr2 = type.Split(separator2);
                        typee = strSplitArr2[0].ToString();
                      
                    }

                    if (main1 == "M" || main1 == "m")
                    {

                        if (typee == "T" || typee == "t")
                        {
                            folderpath = Session["comid"] + "/Thumbnail";
                            location = Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\");
                            // string yesno = findproduct(productno, folderpath, filename);
                            findproduct(productno, folderpath, filename);

                        }
                        else if (typee == "L" || typee == "l")
                        {
                            folderpath = Session["comid"] + "/LargeImg";
                            location = Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\");
                            findproduct1(productno, folderpath, filename);
                        }
                        //else if (type == "T" || type == "t")
                        //{
                        //    location = Server.MapPath("..\\LargeImg\\");
                        //}

                    }
                    else if (main1 == "LSS" || main1 == "lss")
                    {
                        location = Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\");
                        if (typee == "T" || typee == "t")
                        {
                            viewid = 2;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "BT" || typee == "bt")
                        {
                            viewid = 6;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "R" || typee == "r")
                        {
                            viewid = 4;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "l" || typee == "L")
                        {
                            viewid = 5;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "F" || typee == "f")
                        {
                            viewid = 1;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "b" || typee == "B")
                        {

                            viewid = 3;
                            folderpath = Session["comid"] + "/ViewLargeImg";
                            slideshow(productno, folderpath, filename, viewid);
                        }


                    }
                    else if (main1 == "SSS" || main1 == "sss")
                    {
                        folderpath = Session["comid"] + "/ViewSmallImg";
                        location = Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\");

                        if (typee == "T" || typee == "t")
                        {
                            viewid = 2;

                            slideshowsmall(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "BT" || typee == "bt")
                        {
                            viewid = 6;

                            slideshowsmall(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "R" || typee == "r")
                        {
                            viewid = 4;

                            slideshowsmall(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "l" || typee == "L")
                        {
                            viewid = 5;

                            slideshowsmall(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "F" || typee == "f")
                        {
                            viewid = 1;

                            slideshowsmall(productno, folderpath, filename, viewid);

                        }
                        else if (typee == "b" || typee == "B")
                        {

                            viewid = 3;
                            slideshowsmall(productno, folderpath, filename, viewid);
                        }


                    }


                    string destpath = location.ToString() + filename.ToString();
                    // string destpathImage = location1.ToString() + filename.ToString();
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                    // GetFile(ftp1.ToString(), oList[i].ToString(), destpathImage.ToString(), username.ToString(), password.ToString());

                    oList1.Add(productno);
                }

                countproductno1 = oList1.Count;

            }
            Label2.Visible = true;
            Label2.Text = "Images Successfully Transfered";
            printgridview();
            lblno.Text = "";
            Label6.Text = "";
            clesr1();


        }

     catch(Exception p)
    {
        Label2.Visible = true;
        Label2.Text = p.Message;
        
    }
   }
  

    public void clesr1()
    {

        // txtftp.Text="";
         txtportNo.Text="";
         txtfoldername.Text="";
         txtusername.Text = "";
         txtpassword.Text = "";
           
           
           
    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
           Create(ftp.ToString() + filename.ToString());

        oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
        oFTP.UseBinary = false;
        oFTP.UsePassive = false;
        oFTP.Method = WebRequestMethods.Ftp.DownloadFile;


        FtpWebResponse response =
           (FtpWebResponse)oFTP.GetResponse();
        Stream responseStream = response.GetResponseStream();

        //  FileStream fs = new FileStream(Destpath, FileMode.CreateNew);
        FileStream fs = new FileStream(Destpath, FileMode.Create);



        Byte[] buffer = new Byte[2047];
        int read = 1;
        while (read != 0)
        {
            read = responseStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, read);
        }

        responseStream.Close();
        fs.Flush();
        fs.Close();
        responseStream.Close();
        response.Close();

        oFTP = null;
        return true;
    }

    public void findproduct(string productno1, string folderpath, string filename)
    {
        //********find inventorymaster

        string masterid = "Select InventoryMasterId from InventoryMaster where ProductNo='" + productno1 + "'";
        SqlDataAdapter adp = new SqlDataAdapter(masterid, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MASTERID = ds.Tables[0].Rows[0][0].ToString();
        }

        else
        {
        }

        string fl = "Select * from InventoryImgMaster WHERE  InventoryMasterId='" + MASTERID + "'";
        SqlDataAdapter adp1 = new SqlDataAdapter(fl, con);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {


            string fullpath = folderpath + "/" + filename;
            string inserrtimgmaster = "update InventoryImgMaster  set  Thumbnail='" + fullpath + "'  where    InventoryMasterId ='" + MASTERID + "'";
            SqlCommand cmd = new SqlCommand(inserrtimgmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();



        }
        else
        {

            string fullpath = folderpath + "/" + filename;

            string inserrtimgmaster = "INSERT INTO InventoryImgMaster  (InventoryMasterId," +
" Thumbnail  ) " +
" VALUES(  '" + MASTERID + "','" + fullpath + "')";

            SqlCommand cmd = new SqlCommand(inserrtimgmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();



        }

    }
    public void findproduct1(string productno1, string folderpath, string filename)
    {
        //********find inventorymaster

        string masterid = "Select InventoryMasterId from InventoryMaster where ProductNo='" + productno1 + "'";
        SqlDataAdapter adp = new SqlDataAdapter(masterid, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MASTERID = ds.Tables[0].Rows[0][0].ToString();
        }

        else
        {
        }

        string fl = "Select * from InventoryImgMaster WHERE  InventoryMasterId='" + MASTERID + "'";
        SqlDataAdapter adp1 = new SqlDataAdapter(fl, con);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {


            //            string inserrtimgmaster = "INSERT INTO InventoryImgMaster  (InventoryMasterId," +
            //" Thumbnail,           LargeImg,             EntryDate) " +
            //" VALUES(  '" + DropDownList1.SelectedValue + "','Thumbnail/" + FileUpload1.FileName + "' " +
            //" ,'LargeImg/" + FileUpload2.FileName + "',   '" + Convert.ToDateTime(dt) + "' )";
            //            


            string fullpath = folderpath + "/" + filename;

            string inserrtimgmaster = "update InventoryImgMaster  set  LargeImg='" + fullpath + "'  where    InventoryMasterId ='" + MASTERID + "'";
            SqlCommand cmd = new SqlCommand(inserrtimgmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();



            //return "TRUE";

        }
        else
        {

            string fullpath = folderpath + "/" + filename;

            string inserrtimgmaster = "INSERT INTO InventoryImgMaster  (InventoryMasterId," +
" LargeImg  ) " +
" VALUES(  '" + MASTERID + "','" + fullpath + "')";

            SqlCommand cmd = new SqlCommand(inserrtimgmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();



        }

    }

    public void slideshow(string productno1, string folderpath, string filename, int viewid)
    {
        string fullpath = folderpath + "/" + filename;
        string masterid = "Select InventoryMasterId from InventoryMaster where ProductNo='" + productno1 + "'";
        SqlDataAdapter adp = new SqlDataAdapter(masterid, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MASTERID = ds.Tables[0].Rows[0][0].ToString();
        }

        else
        {
        }

        string fl = "Select * from InventoryImgMasterDetails WHERE  InventoryMaster_Id='" + MASTERID + "' and ViewID=" + viewid + "";
        SqlDataAdapter adp1 = new SqlDataAdapter(fl, con);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            string insertstrinImgDe = "update InventoryImgMasterDetails set LargeImageUrl='" + fullpath + "',ViewID=" + viewid + " where InventoryMaster_Id= '" + MASTERID + "' AND  ViewID=" + viewid + "";
            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdint.ExecuteNonQuery();
            con.Close();

        }

        //**********************
        else
        {
            string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,LargeImageUrl,ViewID) values('" + MASTERID + "','" + fullpath + "'," + viewid + ")";
            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdint.ExecuteNonQuery();
            con.Close();
        }
    }

    public void slideshowsmall(string productno1, string folderpath, string filename, int viewid)
    {
        string fullpath = folderpath + "/" + filename;
        string masterid = "Select InventoryMasterId from InventoryMaster where ProductNo='" + productno1 + "'";
        SqlDataAdapter adp = new SqlDataAdapter(masterid, con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MASTERID = ds.Tables[0].Rows[0][0].ToString();
        }

        else
        {
        }

        string fl = "Select * from InventoryImgMasterDetails WHERE  InventoryMaster_Id='" + MASTERID + "' and ViewID=" + viewid + "";
        SqlDataAdapter adp1 = new SqlDataAdapter(fl, con);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            string insertstrinImgDe = "update InventoryImgMasterDetails set SmallImageUrl='" + fullpath + "',ViewID=" + viewid + " where InventoryMaster_Id= '" + MASTERID + "' AND  ViewID=" + viewid + "";
            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdint.ExecuteNonQuery();
            con.Close();

        }

        //**********************
        else
        {
            string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,ViewID) values('" + MASTERID + "','" + fullpath + "'," + viewid + ")";
            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdint.ExecuteNonQuery();
            con.Close();
        }
    }
    public void printgridview()
    {

        for (int i = 0; i < countproductno1; i++)
        {
            string fillgrid = "select distinct ProductNo,Name from InventoryMaster where ProductNo='" + oList1[i].ToString() + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(fillgrid,con);
            adp.Fill(ds);
            grd.DataSource = ds;
            grd.DataBind();


        }
        lblcountimages.Visible = true;
        lblcountimages.Text = "Total No of Images Upload is :" + countproductno1;

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        // grd.PagerSettings.Visible = true;
        ////grd.PageIndex = e.NewPageIndex;
        //grd.PageIndex = e.NewSelectedIndex;
        //printgridview();
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PagerSettings.Visible = true;
        grd.PageIndex = e.NewPageIndex;
        //grd.PageIndex = e.NewSelectedIndex;
        printgridview();
    }
}
