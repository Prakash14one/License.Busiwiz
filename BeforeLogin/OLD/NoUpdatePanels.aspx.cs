using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UpdatePanelExamples
{
    public partial class NoUpdatePanels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var dateString = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
            lblContentOneDate.Text = dateString;
            //Update content two and content three dates only when page is served for first time
            
            lblContentTwoDate.Text = dateString;
            lblContentThreeDate.Text = dateString;
          
        }

        protected void btnRefresh1_OnClick(object sender, EventArgs e)
        {
            lblContentTwoDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
        }

        protected void btnRefresh2_OnClick(object sender, EventArgs e)
        {
            lblContentThreeDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff");
        }

      
    }
}