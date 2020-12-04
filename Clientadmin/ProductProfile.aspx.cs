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
using OboutInc.Show;

public partial class ProductProfile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ProductId"] != null)
            {
             //   ViewState["Productid"] = Request.QueryString["ProductId"].ToString();
                ViewState["Productid"] = 41;
               
                string strcln = " SELECT * from  ProductMaster where ProductId= '"+ViewState["Productid"]+"' ";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);

                if(dtcln.Rows.Count>0)
                {
                    ImageButton1.ImageUrl="~/Productimage/" + dtcln.Rows[0]["Pimage"].ToString()+ " ";

                                         //ImageButton1.ImageUrl="~/images/" + dt.Rows[0]["CompanyLogo"].ToString()+" ";
                    lblproduct.Text = dtcln.Rows[0]["ProductName"].ToString();
                    lblproductdescription123.Text = dtcln.Rows[0]["Description"].ToString();

                    string strcln123 = " select Productfeaturetbl.* from Productfeaturetbl inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=Productfeaturetbl.productversionid inner join  ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ProductMaster.ProductId= '" + ViewState["Productid"] + "' ";
                    SqlCommand cmdcln123 = new SqlCommand(strcln123, con);
                    DataTable dtcln123 = new DataTable();
                    SqlDataAdapter adpcln123 = new SqlDataAdapter(cmdcln123);
                    adpcln123.Fill(dtcln123);

                    GridView1.DataSource = dtcln123;
                    GridView1.DataBind();

                    Image1.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Topimg"].ToString();
                    Image2.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Bottomimage"].ToString();
                    Image3.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Rightimage"].ToString();
                    Image4.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Leftimage"].ToString();
                    Image5.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Frontimage"].ToString();
                    Image6.ImageUrl = "~/Productimage/" + dtcln.Rows[0]["Backimage"].ToString();



                    string str = "SELECT Topimg,Bottomimage,Rightimage,Leftimage,Frontimage,Backimage,Description from  ProductMaster where ProductId= '" + ViewState["Productid"] + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    //768*576
                    //string sTemplate = "<table><tr><td><img src='{pImage}' width='912' height='684'  /></td></tr><tr><td><div>{pView}</div></td></tr></table>";
                    string sTemplate = "<table><tr><td><img src='{pImage}'  width='200' height='100'  /></td></tr><tr><td><div>{pView}</div></td></tr></table>";
                    string sTemplate2 = "<table><tr><td><img src= '{pImage1}' width='200' height='100'  /></td></tr><tr><td><div>{pView1}</div></td></tr></table>";
                    string sTemplate3 = "<table><tr><td><img src= '{pImage2}' width='200' height='100'  /></td></tr><tr><td><div>{pView2}</div></td></tr></table>";
                    string sTemplate4 = "<table><tr><td><img src= '{pImage3}' width='200' height='100'  /></td></tr><tr><td><div>{pView3}</div></td></tr></table>";
                    string sTemplate5 = "<table><tr><td><img src= '{pImage4}' width='200' height='100'  /></td></tr><tr><td><div>{pView4}</div></td></tr></table>";
                    string sTemplate6 = "<table><tr><td><img src= '{pImage5}' width='200' height='100'  /></td></tr><tr><td><div>{pView5}</div></td></tr></table>";

                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {



                            string pImage = dr.GetString(0);
                            string pView = dr.GetString(0);

                            string pImage1 = dr.GetString(1);
                            string pView1 = dr.GetString(1);

                            string pImage2 = dr.GetString(2);
                            string pView2 = dr.GetString(2);

                            string pImage3 = dr.GetString(3);
                            string pView3 = dr.GetString(3);

                            string pImage4 = dr.GetString(4);
                            string pView4 = dr.GetString(4);

                            string pImage5 = dr.GetString(5);
                            string pView5 = dr.GetString(5);


                            StringBuilder tpl = new StringBuilder();
                            tpl.Append(sTemplate);
                            tpl.Replace("{pImage}", pImage);
                            tpl.Replace("{pView}", pView);

                           

                            StringBuilder tp2 = new StringBuilder();
                            tp2.Append(sTemplate2);
                            tp2.Replace("{pImage1}", pImage1);
                            tp2.Replace("{pView1}", pView1);

                            StringBuilder tp3 = new StringBuilder();
                            tp3.Append(sTemplate3);
                            tp3.Replace("{pImage2}", pImage2);
                            tp3.Replace("{pView2}", pView2);

                            StringBuilder tp4 = new StringBuilder();
                            tp4.Append(sTemplate4);
                            tp4.Replace("{pImage3}", pImage3);
                            tp4.Replace("{pView3}", pView3);

                            StringBuilder tp5 = new StringBuilder();
                            tp5.Append(sTemplate5);
                            tp5.Replace("{pImage4}", pImage4);
                            tp5.Replace("{pView4}", pView4);

                            StringBuilder tp6 = new StringBuilder();
                            tp6.Append(sTemplate6);
                            tp6.Replace("{pImage5}", pImage5);
                            tp6.Replace("{pView5}", pView5);


                            Show1.AddHtmlPanel(tpl.ToString());
                            Show1.AddHtmlPanel(tp2.ToString());
                            Show1.AddHtmlPanel(tp3.ToString());

                            Show1.AddHtmlPanel(tp4.ToString());

                            Show1.AddHtmlPanel(tp5.ToString());

                            Show1.AddHtmlPanel(tp6.ToString());


                            //lblMsg.Visible = false;
                        }
                    }
                    else
                    {
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "No Images to Display";
                        return;
                    }
                    Show1.StyleFolder = "style5";
                    //Show1.Width = "450px";
                    //Show1.Height = "450px";
                    Show1.ShowType = ShowType.Show;
                    Show1.ManualChanger = false;
                    //Show1.Changer.Width = 625;
                    //Show1.Changer.Height = 40;
                    Show1.Changer.Type = ChangerType.Both;
                    Show1.Changer.HorizontalAlign = ChangerHorizontalAlign.Center;
                    //Show1.Changer.Height = 40;
                    //Show1.Changer.Width = 625;
                    dr.Close();
                    con.Close();






                   
                    
                }


               

            }
        }

    }
}
