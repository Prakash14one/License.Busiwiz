using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for ClearInputs
/// </summary>
public class ClearInputs
{
	
		public void ClearControl( Control control )
            {
                var textbox = control as TextBox;
                if (textbox != null)
                textbox.Text = string.Empty;

                var dropDownList = control as DropDownList;
                if (dropDownList != null)
                dropDownList.SelectedIndex = 0;
    

                foreach( Control childControl in control.Controls )
                    {
                        ClearControl( childControl );
                    }
            }

	
}