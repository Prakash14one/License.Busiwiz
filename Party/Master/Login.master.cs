using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Master_Login : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Url.Host.ToString() == "onlineaccounts.net")
            {
                mainloginlogo.ImageUrl = "images/onlineaccounts.jpg";
                //copyright.Text = "OnlineAccouns.net";
                lblemail.Text = "support@onlineaccounts.net";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else if (Request.Url.Host.ToString() == "ifilecabinet.com")
            {
                mainloginlogo.ImageUrl = "images/ifilecabinet.jpg";
               // copyright.Text = "ifilecabinet.com";
                lblemail.Text = "support@ifilecabinet.com";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else if (Request.Url.Host.ToString() == "eplaza.us")
            {
                mainloginlogo.ImageUrl = "images/eplaza.jpg";
               // copyright.Text = "eplaza.us";
                lblemail.Text = "support@eplaza.us";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else if (Request.Url.Host.ToString() == "iofficemanager.com")
            {
                mainloginlogo.ImageUrl = "images/iofficemanager.jpg";
                //copyright.Text = "iofficemanager.com";
                lblemail.Text = "support@iofficemanager.com";
                lblcontactno.Text = "703-7452-504";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else if (Request.Url.Host.ToString() == "itimekeeper.com")
            {
                mainloginlogo.ImageUrl = "~/images/itimekeeper.jpg";
                //copyright.Text = "itimekeeper.com";
                lblemail.Text = "support@itimekeeper.com";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else if (Request.Url.Host.ToString() == "ipayrollmanager.com")
            {
                mainloginlogo.ImageUrl = "images/ipayrollmanager.jpg";
               // copyright.Text = "ipayrollmanager.com";
                lblemail.Text = "support@ipayrollmanager.com";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
            else
            {
                mainloginlogo.ImageUrl = "~/images/itimekeeper.jpg";
               // copyright.Text = "OnlineAccouns.net";
                lblemail.Text = "support@itimekeeper.com";
                lblcontactno.Text = "703-8255-903";
                lbladdress.Text = "510-24thStreet, PORT HURON, MI 48060, USA";
            }
        }

    }
}
