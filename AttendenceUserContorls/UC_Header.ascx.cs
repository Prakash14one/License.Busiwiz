﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class UserContorls_UC_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            imgMiddleImage.Visible = false;
            if (Request.Url.ToString().ToLower().Contains("productprofile.aspx"))
            {
                imgMiddleImage.Visible = true;
            }
        }
    }
}
