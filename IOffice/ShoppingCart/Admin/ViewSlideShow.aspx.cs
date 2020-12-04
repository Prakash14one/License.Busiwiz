using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using System.Text;
using OboutInc.Show;

public partial class slideshowmy : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Request.QueryString["Id"] != null)
        {
            string str = "select image,image as ID from BusinessDetails2 where detailid='" + Request.QueryString["Id"].ToString() + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            //string sTemplate = "<table><tr><td><img src=\"{pImage}\" border=\"0\" Width=\"300px\" Height=\"500px\" / ></td></tr><tr><td><div>{pView}</div></td></tr></table>";

            string sTemplate = "<table width=\"100%\"><tr><td align=\"center\"><div>Slideshow</div></td></tr><tr><td align=\"center\"><img src='{pImage}' /></td></tr></table>";



            if (dr != null)
            {
                while (dr.Read())
                {
                    //string me = dr.GetString(0);

                    string pImage = "http://" + Request.Url.Host.ToString() + dr.GetString(0).Replace('~', ' ').Trim();

                    //me = me.Replace('~', ' ');

                    //string pImage = "http://" + Request.Url.Host.ToString() + me.TrimStart();

                    //string pView = dr.GetString(1).Replace("~/ShoppingCart/images/", "");                    

                    StringBuilder tpl = new StringBuilder();
                    tpl.Append(sTemplate);
                    tpl.Replace("{pImage}", pImage);
                    //tpl.Replace("{pView}", "Slideshow");

                    Show1.AddHtmlPanel(tpl.ToString());
                }
            }
            else
            {
                return;
            }

            Show1.StyleFolder = "style51";
            //Show1.Width = "450px";
            //Show1.Height = "450px";
            Show1.ShowType = ShowType.Show;
            Show1.ManualChanger = true;
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    public DataSet getData()
    {

        string str = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryDetails.Description, InventoryWarehouseMasterTbl.Rate, InventoryDetails.Weight, " +
                   "    InventoryImgMaster.LargeImg, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                  " FROM         InventoryMaster LEFT OUTER JOIN " +
                   "   InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId LEFT OUTER JOIN " +
                   "   InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                    "  InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId " +
                    " WHERE   (InventoryMaster.InventoryMasterId = '5' )";


        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
}