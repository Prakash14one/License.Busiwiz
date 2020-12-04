<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" CodeFile="CompanySiteProductsandServicesInput.aspx.cs" Inherits="ShoppingCart_Admin_CompanySiteProductsandServicesInput" %>

<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
      <script type="text/javascript" language="javascript">
          function tbLimit() {

              var tbObj = event.srcElement;
              if (tbObj.value.length == tbObj.maxLength * 1) return false;

          }

          function tbCount(visCnt) {
              var tbObj = event.srcElement;

              if (tbObj.value.length > tbObj.maxLength * 1) tbObj.value = tbObj.value.substring(0, tbObj.maxLength * 1);
              if (visCnt) visCnt.innerText = tbObj.maxLength - tbObj.value.length;

          }  
  </script>
   
    <script type="text/javascript">
        $(function () {
            $(".newsticker-jcarousellite").jCarouselLite({
                vertical: true,
                hoverPause: true,
                visible: 1,
                auto: 1000,
                speed: 1000
            });

        });
    </script>
    <script src="js/jquery-latest.pack.js" type="text/javascript"></script>
    <script src="js/jcarousellite_1.0.1c4.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".newsticker-jcarousellite").jCarouselLite({
                vertical: true,
                hoverPause: true,
                visible: 1,
                auto: 500,
                speed: 1000
            });
        });
    </script>
    <!--script src="js/jquery-1.9.1.min.js" type="text/javascript"></script-->
    <script src="js/jssor.core.js" type="text/javascript"></script>
    <script src="js/jssor.utils.js" type="text/javascript"></script>
    <script src="js/jssor.slider.js" type="text/javascript"></script>
    <script src="js/jquery.simplyscroll.js" type="text/javascript"></script>
    <script type="text/javascript">
        (function ($) {
            $(function () { //on DOM ready 
                $("#scroller").simplyScroll();
            });
        })(jQuery);
    </script>
 

     <style type="text/css">
         #map_canvas { height:30px }
  . legend {
    display: block;
    width: 100%;
    padding: 0;
    margin-bottom: 20px;
    font-size: 14px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 1px solid #e5e5e5;
   }      
         
         .mGrid
{
	width: 100%;
	background-color: #fff;
	margin: 0px 0 4px -3px;
	border: solid 1px #525252;
	border-collapse: collapse;
	font-size: 9px !important;
}
.mGrid td
{
	padding: 2px;
	border: solid 1px #c1c1c1;
	
	color: #282828;
   font-size:14px;
}
.mGrid th
{
	padding: 4px 0px;
	color: #fff;
	background-color:#416271;
	border-left: solid 1px #525252;
	font-size: 14px !important;
	
}
.mGrid .alt
{
	background: #fcfcfc url(grd_alt.png) repeat-x top;
}
.mGrid .pgr
{
	background-color: #416271;
}
.mGrid .pgr table
{
	margin: 5px 0;
}
.mGrid .pgr td
{
	border-width: 0;
	padding: 0 6px;
	border-left: solid 1px #666;
	font-weight: bold;
	color: #fff;
	line-height: 12px;
}
.mGrid .pgr a
{
	color: Gray;
	text-decoration: none;
}
.mGrid .pgr a:hover
{
	color: #000;
	text-decoration: none;
}
 
             .cssButton1
{
    width: 23%;
    height: 28px;
	
	background: rgb(61, 157, 179);
	padding: -1px 5px;
	font-family: 'BebasNeueRegular','Arial Narrow',Arial,sans-serif;
	color: #fff;
	font-size: 14px;	
	border: 1px solid rgb(28, 108, 122);	
	margin-bottom: 10px;	
	text-shadow: 0 1px 1px rgba(0, 0, 0, 0.5);
	-webkit-border-radius: 3px;
	   -moz-border-radius: 3px;
	        border-radius: 3px;	
	-webkit-box-shadow: 0px 1px 6px 4px rgba(0, 0, 0, 0.07) inset,
	        0px 0px 0px 3px rgb(254, 254, 254),
	        0px 5px 3px 3px rgb(210, 210, 210);
	   -moz-box-shadow:0px 1px 6px 4px rgba(0, 0, 0, 0.07) inset,
	        0px 0px 0px 3px rgb(254, 254, 254),
	        0px 5px 3px 3px rgb(210, 210, 210);
	        box-shadow:0px 1px 6px 4px rgba(0, 0, 0, 0.07) inset,
	        0px 0px 0px 3px rgb(254, 254, 254),
	        0px 5px 3px 3px rgb(210, 210, 210);
	-webkit-transition: all 0.2s linear;
	   -moz-transition: all 0.2s linear;
	     -o-transition: all 0.2s linear;
	        transition: all 0.2s linear;
    }
           #cssmenu1
{
background: #027A91;
   margin-top: -1px;
    /*width: 384px;*/
    padding: 0;
   

    display: block;
   /* position: relative;*/
    font-family: 'PT Sans', sans-serif;
}
         #cssmenu1 ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: block;
}
#cssmenu1 ul:after,
#cssmenu1:after {
  content: " ";
  display: block;
  font-size: 0;
  height: 0;
  clear: both;
  visibility: hidden;
}
#cssmenu1 ul li {
  margin: 0;
  padding: 0;
  display: block;
  position: relative;
}
#cssmenu1 ul li a {
  text-decoration: none;
  display: block;
  margin: 0;
  -webkit-transition: color .2s ease;
  -moz-transition: color .2s ease;
  -ms-transition: color .2s ease;
  -o-transition: color .2s ease;
  transition: color .2s ease;
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}
#cssmenu1 ul li ul {
  position: absolute;
  left: -9999px;
  top: auto;
}
#cssmenu1 ul li ul li {
  max-height: 0;
  position: absolute;
  -webkit-transition: max-height 0.4s ease-out;
  -moz-transition: max-height 0.4s ease-out;
  -ms-transition: max-height 0.4s ease-out;
  -o-transition: max-height 0.4s ease-out;
  transition: max-height 0.4s ease-out;
  background: #ffffff;
}
#cssmenu1 ul li ul li.has-sub:after {
  display: block;
  position: absolute;
  content: "";
  height: 10px;
  width: 10px;
  border-radius: 5px;
  background: #000000;
  z-index: 1;
  top: 13px;
  right: 15px;
}
#cssmenu1.align-right ul li ul li.has-sub:after {
  right: auto;
  left: 15px;
}
#cssmenu1 ul li ul li.has-sub:before {
  display: block;
  position: absolute;
  content: "";
  height: 0;
  width: 0;
  border: 3px solid transparent;
  border-left-color: #ffffff;
  z-index: 2;
  top: 15px;
  right: 15px;
}
#cssmenu1.align-right ul li ul li.has-sub:before {
  right: auto;
  left: 15px;
  border-left-color: transparent;
  border-right-color: #ffffff;
}
#cssmenu1 ul li ul li a {
  font-size: 10px;
    font-weight: 100;
    text-transform: none;
    color: #000000;
    letter-spacing: 0;
    display: block;
    width: 109px;
    padding: -2px 7px 0px -4px;
}
#cssmenu1 ul li ul li:hover > a,
#cssmenu1 ul li ul li.active > a {
  color: #4cb6ea;
}
#cssmenu1 ul li ul li:hover:after,
#cssmenu1 ul li ul li.active:after {
  background: #4cb6ea;
}
#cssmenu1 ul li ul li:hover > ul {
  left: 100%;
  top: 0;
}
#cssmenu1 ul li ul li:hover > ul > li {
  max-height: 72px;
  position: relative;
}
#cssmenu1 > ul > li {
  float: left;
}
#cssmenu1.align-center > ul > li {
  float: none;
  display: inline-block;
}
#cssmenu1.align-center > ul {
  text-align: center;
}
#cssmenu1.align-center ul ul {
  text-align: left;
}
#cssmenu1.align-right > ul {
  float: right;
}
#cssmenu1.align-right > ul > li:hover > ul {
  left: auto;
  right: 0;
}
#cssmenu1.align-right ul ul li:hover > ul {
  right: 100%;
  left: auto;
}
#cssmenu1.align-right ul ul li a {
  text-align: right;
}
#cssmenu1 > ul > li:after {
  content: "";
  display: block;
  position: absolute;
  width: 100%;
  height: 0;
  top: 0;
  z-index: 0;
  background: #ffffff;
  -webkit-transition: height .2s;
  -moz-transition: height .2s;
  -ms-transition: height .2s;
  -o-transition: height .2s;
  transition: height .2s;
}
#cssmenu1 > ul > li.has-sub > a {
  padding-right: 40px;
}
#cssmenu1 > ul > li.has-sub > a:after {
  display: block;
  content: "";
  background: #ffffff;
  height: 12px;
  width: 12px;
  position: absolute;
  border-radius: 13px;
  right: 14px;
  top: 16px;
}
#cssmenu1 > ul > li.has-sub > a:before {
  display: block;
  content: "";
  border: 4px solid transparent;
  border-top-color: #4cb6ea;
  z-index: 2;
  height: 0;
  width: 0;
  position: absolute;
  right: 16px;
  top: 21px;
}
#cssmenu1 > ul > li > a {
  color: #ffffff;
    padding: 8px 20px;
    font-weight: 100;
    letter-spacing: 0px;
    text-transform: capitalize;
    font-size: 13px;
    z-index: 2;
    position: relative;

}
#cssmenu1 > ul > li:hover:after,
#cssmenu1 > ul > li.active:after {
  height: 100%;
}
#cssmenu1 > ul > li:hover > a,
#cssmenu1 > ul > li.active > a {
  color: #000000;
}
#cssmenu1 > ul > li:hover > a:after,
#cssmenu1 > ul > li.active > a:after {
  background: #000000;
}
#cssmenu1 > ul > li:hover > a:before,
#cssmenu1 > ul > li.active > a:before {
  border-top-color: #ffffff;
}
#cssmenu1 > ul > li:hover > ul {
  left: 0;
}
#cssmenu1 > ul > li:hover > ul > li {
  max-height: 72px;
  position: relative;
}
#cssmenu1 #menu-button {
  display: none;
}
#cssmenu > ul > li > a {
  display: block;
}
#cssmenu1 > ul > li {
  width: auto;
}
#cssmenu1 > ul > li > ul {
  width: 170px;
  display: block;
}
#cssmenu1 > ul > li > ul > li {
  width: 170px;
  display: block;
}
                  .footer_container1
{
	width: 100%;
	margin-right: auto;
	margin-left: auto;
	margin-top: 0px;
	padding-bottom: 0px;
	margin-bottom: 10px;
}
.footer_BG1
{
	width: 100%;
	position: relative;
	margin-top:0px;
	height: 21px;
}
#footer_contents_container1111
{
	width: 100%;
	color: #66bfce;
	
	font-size: 8px;
	letter-spacing: .1em;
	margin-right: auto;
	margin-bottom: 0;
	margin-left: auto;
	height: 30px;
	margin-top: 0px;
	background-color: #0287a1;
	background: -webkit-gradient(linear, left top, left bottom, from(#029dba), to(#027990));
	background: -moz-linear-gradient(top,  #029dba,  #027990);
	filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=     '#029dba' , endColorstr= '#027990' );
}
#copyright1 {
   /*width: 265px;*/
    color: #FFFFFF;
    text-align: center;
    font-size: 9px;
    letter-spacing: .1em;
    margin-right: auto;
    margin-bottom: 0;
    margin-left: auto;
    height: 30px;
    margin-top: 2px;
    font-family: Calibri;
}
 /* @media all and (max-width: 200px), only screen and (-webkit-min-device-pixel-ratio: 2) and (max-width: 1000px), only screen and (min--moz-device-pixel-ratio: 2) and (max-width: 1024px), only screen and (-o-min-device-pixel-ratio: 2/1) and (max-width: 1000px), only screen and (min-device-pixel-ratio: 2) and (max-width: 1024px), only screen and (min-resolution: 192dpi) and (max-width: 1024px), only screen and (min-resolution: 2dppx) and (max-width: 1024px) {*/
 

 
          .Header_container1
{
    background-color: #FFFFFF;
	width:100%;
	margin-right: auto;
	margin-left: auto;
	
}
    .Header_BG1
{
	width:100%;
	position: relative;
}  
.Header_content1111
{
	width:1024px;
	margin-right: auto;
	margin-left: auto;
	background-color: Red;
} 
.header_content_upper1
{
	width:100%;
	margin-right: auto;
	margin-left: auto;
}
    .header_upper1
{
	    width: 100%;
    margin-right: auto;
    margin-left: auto;
    height: 125px;
    background-color: #0287a1;
    background: -webkit-gradient(linear, left top, left bottom, from(#029dba), to(#027990));
}
.header_upper_logo_bg_box1
{
	width: 630px;
    height: 120px;
    float: left;
    margin-top: 3px;
    background-image: url(Images/logo_bg.png);
    background-repeat: no-repeat;
	
}
 table, th, td {
     
        border-collapse: collapse;
    font-size: 5px;
}
th, td {
    padding: 0px;
}
    

 
/*}

   
    </style>




        
   
      <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>            
                   <asp:Panel ID="Panel6" runat="server">
         <%-- <div style="/*margin: 3px 0px 0px 3px;*/padding: 12px;width: 99%;min-height: 300px;border: 2px solid #1991A9;border-radius: 18px 0px 20px 0px;text-align: -webkit-auto;">--%>
                    <div style="margin-top: 6px; margin-left: 97px;width: 1063px;    min-height:   319px;border: 2px solid #1991A9;">
                   <div style="float:left;">
                     <asp:Label ID="Label11" runat="server"  Text="Select Products and Services"></asp:Label>
                    &nbsp; &nbsp;
                           </div>   
                         
                      
                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                           onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="175px">
                           <asp:ListItem Value="0">Products and Services1</asp:ListItem>
                           <asp:ListItem Value="1">Products and Services2</asp:ListItem>
                           <asp:ListItem Value="2">Products and Services3</asp:ListItem>
                           <asp:ListItem Value="3">Products and Services4</asp:ListItem>
                       </asp:DropDownList>
                          

                        <asp:Panel ID="Panel1" runat="server"  style="width: 1024px;" >
                        <table width="100%">
                      <tr>
                      <td>
                       <asp:Label ID="lblmsg" runat="server" Visible="False" 
                                       ForeColor="#FF3300"  Font-Size="Large"  ></asp:Label>
                                     
                                       <asp:Label ID="Label5" runat="server" 
                                       Text="" Visible="False" 
                                       ForeColor="#FF3300"  Font-Size="Large" ></asp:Label>
                                    
                                           <asp:Label ID="Label6" runat="server" 
                                       Text="" Visible="False" 
                                       ForeColor="#FF3300" Font-Size="Large"  ></asp:Label>
                      </td>
                      </tr>
                        <tr>
                        <td>
                          <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <table width="100%">
                         <tr>
                      <td width="20%">
                          <asp:Label ID="lbl1" runat="server"  Font-Size="Medium" Visible="false"></asp:Label>
                           <asp:Label ID="lbimage1" runat="server"  Font-Size="Medium" ></asp:Label>
                      </td>
                       <td  width="20%">
                           <asp:FileUpload ID="FileUpload1" runat="server"  Visible="false"  />

                             </td>
                             <td  width="20%" >
                                                  <asp:Button ID="submit1" runat="server" onclick="submit1_Click" Text="Upload" 
                               Visible="false" />
                               <asp:Button ID="Button3" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button3_Click" />

                                     <asp:Button ID="edit1" runat="server" Text="Edit" onclick="edit1_Click" />
                           
                      </td>
                       
                       <td  width="20%" align="right">
                          
                           
                           <asp:Button ID="Button9" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button9_Click" />
                          
                           
                        <asp:Button ID="edit11" runat="server" Text="Edit" onclick="edit11_Click" />
                       
                        
                      </td>
                        </tr>
                        <tr>
                         <td colspan="4" align="right">
                           <asp:Label ID="Label1" runat="server" Font-Size="small" Text="Max: 40 (A-Z a-z)" ></asp:Label>
                             <asp:Label ID="Label7" runat="server" Font-Size="small" Text="Of 40 (A-Z a-z)"  Visible="false"></asp:Label>
                         </td>
                         </tr>
                       <tr >
                      <td colspan="4">
                            <asp:Image ID="Image1"   runat="server" 
                                 style="width: 642px;    height: 220px; margin-bottom: 9px;" />
                               <asp:TextBox ID="TextBox1" runat="server" Height="21px" MaxLength="40" 
                                ontextchanged="TextBox1_TextChanged" style="margin-top: -220px; margin-left: 648px; " Width="310px"></asp:TextBox>
                               </td>
                                   
                            </tr>
                            </table>
                            </asp:Panel>
                            
                            </td>
                            </tr>
                              <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <table width="100%">
                         <tr>
                      <td width="20%">
                          <asp:Label ID="lbl2" runat="server"  Font-Size="Medium" Visible="false"></asp:Label>
                          <asp:Label ID="lbimage2" runat="server"  Font-Size="Medium" ></asp:Label>
                      </td>
                       <td  width="20%">
                           <asp:FileUpload ID="FileUpload2" runat="server" Visible="false" />
                           
                             </td>
                          
                      <td  width="20%"  >
                       <asp:Button ID="submit2" runat="server" onclick="submit2_Click" Text="Upload" 
                               Visible="false" />
                      <asp:Button ID="Button5" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button5_Click" />
                           <asp:Button ID="edit2" runat="server" Text="Edit" onclick="edit2_Click" />
                      </td>
                       <td  width="20%"   >
                       
                      </td>
                       <td  width="20%" align="right" >
                           <asp:Button ID="Button10" runat="server"  Text="Cancel" 
                               Visible="false" onclick="Button10_Click" />
                        <asp:Button ID="edit21" runat="server" Text="Edit" onclick="edit21_Click" />
                        </td>
                      </tr>
                      <tr>
                      
                        <td  colspan="4" align="right">
                        <asp:Label ID="Label2" runat="server" Font-Size="small" Text= "Max: 40 (A-Z a-z)" ></asp:Label>
                          <asp:Label ID="Label8" runat="server" Font-Size="small" Text="Of 40 (A-Z a-z)" Visible="false"></asp:Label>
                      </td>
                     

                      </tr>
                       <tr>
                      <td colspan="4">
                            <asp:Image ID="Image2"  ImageUrl=""  runat="server" 
                                 style="width: 642px; height: 220px; margin-bottom: 9px;" />
                              
                            <asp:TextBox ID="TextBox2" runat="server" 
                                  style="margin-top: -220px; margin-left: 648px; height:  21px;width: 310px;" MaxLength="40" 
                                  ontextchanged="TextBox2_TextChanged" ></asp:TextBox>
                            
                            </td>
                            </tr>
                            </table>
                            </asp:Panel>
                            </td>
                            </tr>
                              <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" Visible="false">
                             <table width="100%">
                         <tr>
                      <td width="20%">
                          <asp:Label ID="lbl3" runat="server"  Font-Size="Medium" Visible="false" ></asp:Label>
                          <asp:Label ID="lbimage3" runat="server" Font-Size="Medium" ></asp:Label>
                      </td>
                       <td  width="20%">
                           <asp:FileUpload ID="FileUpload3" runat="server"  Visible="false"/> 
                           
                      </td>
                       <td  width="20%">
                       <asp:Button ID="submit3" runat="server" onclick="submit3_Click" Text="Upload" 
                               Visible="false" />
                      <asp:Button ID="Button7" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button7_Click1" />
                         
                           <asp:Button ID="edit3" runat="server" Text="Edit" onclick="edit3_Click" />
                      </td>
                       <td  width="20%">
                       
                      </td>
                       <td  width="20%" align="right">
                        
                           <asp:Button ID="Button11" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button11_Click" />
                        
                        <asp:Button ID="edit31" runat="server" Text="Edit" onclick="edit31_Click" />
                      </td>
                        </tr>
                        <tr>
                      
                       <td  colspan="4" align="right">
                        <asp:Label ID="Label3" runat="server"  Font-Size="small"  Text="Max: 40 (A-Z a-z)" ></asp:Label>
                          <asp:Label ID="Label9" runat="server" Font-Size="small" Text="Of 40 (A-Z a-z)" Visible="false" ></asp:Label>
                      </td>
                        </tr>
                       <tr>
                      <td colspan="4">
                            <asp:Image ID="Image3" ImageUrl="" runat="server" 
                                style="width: 642px; height: 220px; margin-bottom: 9px;" />
                               
                            <asp:TextBox ID="TextBox3" runat="server" 
                                        style="margin-top: -220px; margin-left: 648px; height:  21px;width: 310px;"  MaxLength="40" 
                                        ontextchanged="TextBox3_TextChanged" ></asp:TextBox>
                             
                            </td>
                           </tr>
                           </table>
                            </asp:Panel>
                            </td>
                            </tr>
                              <tr>
                        <td>
                            <asp:Panel ID="Panel5" runat="server" Visible="false">
                             <table width="100%">
                         <tr>
                      <td width="20%">
                          <asp:Label ID="lbl4" runat="server"  Font-Size="Medium" Visible="false"></asp:Label>
                          <asp:Label ID="lbimage4" runat="server"  Font-Size="Medium" ></asp:Label>
                      </td>
                       <td  width="20%">
                           <asp:FileUpload ID="FileUpload4" runat="server"  Visible="false"/> 
                           </td>
                           <td  width="20%" >
                       <asp:Button ID="submit4" runat="server" onclick="submit4_Click" Text="Upload" 
                               Visible="false" />
                      <asp:Button ID="Button8" runat="server" Text="Cancel" 
                               Visible="false" onclick="Button8_Click" />
                           <asp:Button ID="edit4" runat="server" Text="Edit" onclick="edit4_Click" />
                      </td>
                       <td  width="20%">
                       
                      </td>
                     <td  width="20%" align="right">
                        
                           <asp:Button ID="Button12" runat="server"  Text="Cancel" 
                               Visible="false" onclick="Button12_Click" />
                        
                        <asp:Button ID="edit41" runat="server" Text="Edit" onclick="edit41_Click" ToolTip="Text Editing" />
                      </td>
                        </tr>
                        <tr> 
                     
                      <td  colspan="4" align="right">
                        <asp:Label ID="Label4" runat="server"  Font-Size="small" Text="Max: 40 (A-Z a-z)"></asp:Label>
                          <asp:Label ID="Label10" runat="server" Font-Size="small" Text="Of 40 (A-Z a-z)"  Visible="false"></asp:Label>
                      </td> 
                      </tr>
                       <tr>
                      <td colspan="4">
                            <asp:Image ID="Image4" ImageUrl=""  runat="server" 
                                
                                    style="width: 642px; height: 220px;margin-bottom: 9px;" />
                                    
                             <asp:TextBox ID="TextBox4" runat="server" 
                                            style="margin-top: -220px; margin-left:685px;   height: 21px;width: 310px;"  MaxLength="40" 
                                            ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                            
                            </td>
                            </tr>
                            </table>
                            </asp:Panel>
                            </td>
                            </tr>
                             </table>
                        </asp:Panel>
                       
                   
                   </div>
      
        <div >
         
        </div>
                       <asp:Button ID="Button1" runat="server" Text="Submit" 
                           onclick="Button1_Click" style="margin-left: 605px;margin-top: 25px;"/>
   </asp:Panel>
 </ContentTemplate>
<Triggers>
 <asp:PostBackTrigger ControlID="submit1" />
 <asp:PostBackTrigger ControlID="submit2" />
  <asp:PostBackTrigger ControlID="submit3" />
   <asp:PostBackTrigger ControlID="submit4" />
    <asp:PostBackTrigger ControlID="edit11" />  
   <asp:PostBackTrigger ControlID="edit21" />  
   <asp:PostBackTrigger ControlID="edit31" />  
    <asp:PostBackTrigger ControlID="edit41" />   
 </Triggers>
 </asp:UpdatePanel>

  <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>
