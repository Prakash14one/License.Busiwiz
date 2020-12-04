<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="AddProduct.aspx.cs" Inherits="AddProduct" Title="Add Product / Version" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 
        
         function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
        
             function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
    </script>

    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
        
       
        TR.updated TD
        {
            background-color:yellow;
        }
        .modalBackground 
        {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
        }
    .pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    
    </style>

    <script language="javascript" type="text/javascript">
        function OpenNewWin(url) {
            //  alert(url);
            var x = window.open(url, 'MynewWin', 'width=1000, height=1000,toolbar=2');
            x.focus();
        }
    </script>

    <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" Text=""  ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text="Add Product or Version" 
                CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <label style="width:100%">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True" Width="457px" ForeColor="#0080C0">
                                <asp:ListItem Value="0" Selected="True">Add New Product</asp:ListItem>
                                <asp:ListItem Value="1">Add New Version of Existing Product</asp:ListItem>
                            </asp:RadioButtonList>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlProduct" runat="server" Visible="False" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label8" runat="server" Text="Select Product"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlProductList" DataTextField="ProductName" DataValueField="ProductId"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductList_SelectedIndexChanged" Width="190px">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Product Name"></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProductName" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                           
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtProductName" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'div1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label25" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ / .)"></asp:Label>
                        </label>
                        <label>
                             <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)" ControlToValidate="txtProductName" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label> 
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Marketing website URL"></asp:Label>
                            <asp:Label ID="Label21" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUrl" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label style="width:500px">
                            <asp:TextBox ID="txtUrl" runat="server" Width="490px" MaxLength="500" ></asp:TextBox>
                        </label>                         
                           <label>
                           <asp:Image ID="Image11235" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="Add the URL of your  Product where Users may get more info" Width="20px" />
                         <%--  <asp:LinkButton ID="lnkMsg" runat="server" ForeColor="Black"></asp:LinkButton>
                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="modal1" CancelControlID="btnCancelPopup" runat="server" TargetControlID="lnkMsg" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" Height="100px" Width="400px" runat="server" CssClass="pnlBackGround">
                                        <asp:Label ID="lblMsgPopup" runat="server" Text="Help : Add the URL of your  Product where Users may get more info">
                                     </asp:Label><br /><br />
                                        <center><asp:Button ID="btnCancelPopup" runat="server" Text="Close" /></center>
                                    </asp:Panel>--%>
                           
                        </label>                       
                        
                    </td>
                    
                </tr>
               
                <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Price plan URL"></asp:Label>
                            <asp:Label ID="Label22" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPricePlanURL" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label  style="width:500px;">
                            <asp:TextBox ID="txtPricePlanURL" runat="server" Width="490px" ></asp:TextBox>
                        </label>
                        <label>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="Add the Url of the price plan in your website where users can see price of this and other products" Width="20px" />
                       <%--  <asp:LinkButton ID="linkBtn_priceurl" runat="server" ForeColor="Black">Help ?</asp:LinkButton>
                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="ModalPopupExtender1" CancelControlID="btn_ppurlclose" runat="server" TargetControlID="linkBtn_priceurl" PopupControlID="pnl_ppurlpopp"></cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnl_ppurlpopp" Height="100px" Width="400px" runat="server" CssClass="pnlBackGround">
                                        <asp:Label ID="Label27" runat="server" Text="Help : Add the Url of the price plan in your website where users can see price of this and other products">
                                     </asp:Label><br /><br />
                                        <center><asp:Button ID="btn_ppurlclose" runat="server" Text="Close" /></center>
                                    </asp:Panel>--%>
                                    </label> 
                    </td>
                   
                </tr>
               
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Product active"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                        </label>
                    </td>                   
                </tr>
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Version No"></asp:Label>
                            <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVersionNo" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtVersionNo" runat="server" Width="190px" onkeyup="return mak('Span7',10,this)"
                                MaxLength="10"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                TargetControlID="txtVersionNo" ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="Span7" cssclass="labelcount">10</span>
                            <asp:Label ID="Label63" runat="server" Text="(0-9)"></asp:Label>
                        </label>
                    </td>
                </tr>
               
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label39" runat="server" Text="Version active"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:CheckBox ID="chbversionactive" runat="server" Text="Active" />
                        </label>
                    </td>
                  </tr>
                 <asp:Panel ID="pnldesc" runat="server">
                        <tr> 
                                    <td style="width:20%" valign="top"> 
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Add product discription"></asp:Label>
                                        </label>
                                    </td>
                                    <td colspan="2" >
                                        <asp:CheckBox ID="chk_productdesc" runat="server" Text="Yes" AutoPostBack="True" oncheckedchanged="chk_productdesc_CheckedChanged" />
                                        <br />
                                        <br />
                                        <cc2:HtmlEditor ID="txtdescription" runat="server" Visible="false"></cc2:HtmlEditor>
                                    </td>
                            </tr>
                 </asp:Panel>
                <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Start Date"></asp:Label>
                            <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartdate" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtStartdate" runat="server" Width="171px"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartdate"
                            PopupButtonID="imgbtnEndDate">
                        </cc1:CalendarExtender>
                        <label>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="Date when the product would be availble for sale." Width="20px" />
                          <%-- <asp:LinkButton ID="lbl_helpstatrtdt" runat="server" ForeColor="Black">Help ?</asp:LinkButton>
                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="ModalPopupExtender3" CancelControlID="btn_startdtcancle" runat="server" TargetControlID="lbl_helpstatrtdt" PopupControlID="pnl_startdtpopup"></cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnl_startdtpopup" Height="100px" Width="400px" runat="server" CssClass="pnlBackGround">
                                        <asp:Label ID="Label28" runat="server" Text="Help : Date when the product would be availble for sale.">
                                     </asp:Label><br /><br />
                                        <center><asp:Button ID="btn_startdtcancle" runat="server" Text="Close" /></center>
                                    </asp:Panel>--%>
                            
                        </label>                        
                    </td>                   
                </tr>
                <tr>
                    <td style="width:20%">
                        <label>
                            <asp:Label ID="Label16" runat="server" Text="End Date"></asp:Label>
                            <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="171px"></asp:TextBox>
                        </label>
                        <label style="width:40px">
                            <asp:ImageButton ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                        </label>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgbtnCalEnddate">
                        </cc1:CalendarExtender>
                         <label>
                             <asp:Image ID="Image3" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="Date when the product will no longer available for sale." Width="20px" />
                           <%--<asp:LinkButton ID="lnkbtn_enddate" runat="server" ForeColor="Black">Help ?</asp:LinkButton>
                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="ModalPopupExtender4" CancelControlID="btn_enddatecancle" runat="server" TargetControlID="lnkbtn_enddate" PopupControlID="pnl_enddatepopp"></cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnl_enddatepopp" Height="100px" Width="400px" runat="server" CssClass="pnlBackGround">
                                        <asp:Label ID="Label43" runat="server" Text="Help : Date when the product will no longer available for sale">
                                     </asp:Label><br /><br />
                                        <center>
                                        <asp:Button ID="btn_enddatecancle" runat="server" Text="Close" /></center>
                                    </asp:Panel>                            --%>
                        </label>                         
                    </td>                   
                </tr>
                <tr>
                    <td colspan="3">
                       <asp:Panel ID="pnldown" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width:680px;">
                                        <label >
                                            Is this a offline desktop or mobile application only which  the customer would able to download ?
                                        </label>                                        
                                    </td>   
                                    <td>
                                         <label>
                                            <asp:CheckBox ID="chkboxActiveDeactive0" runat="server" Text="Yes" OnCheckedChanged="chkboxActiveDeactive0_CheckedChanged" AutoPostBack="True" />                                       
                                        </label> 
                                    </td>                                 
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                         <label style="width:500px;">
                        Server details where the product code and websites are hosted
                        </label> 
                        <label>
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="Based on sever defined in  My company Profile " Width="20px" />
                        </label>                         
                    </td>
                 </tr>  
                 <tr>   
                  <td></td>
                    <td colspan="2">
                            <label>
                         <fieldset>
                                  <legend>                                            
                                  </legend>                                             
                                     <table>                                                    
                                          <tr>
                                               <td>
                                                    <label>
                                                        Server name
                                                    </label> 
                                               </td>
                                               <td>
                                                    <asp:TextBox ID="txt_servername" runat="server" Width="171px" Enabled="false"></asp:TextBox>
                                               </td>
                                               <td>
                                                    <label>
                                                       Location
                                                    </label> 
                                               </td>
                                               <td>
                                                    <asp:TextBox ID="txt_serverlocation" runat="server" Width="171px" Enabled="false"></asp:TextBox>
                                               </td>
                                          </tr>
                                           <tr>
                                               <td>
                                                    <label>
                                                       Server public IP
                                                    </label> 
                                               </td>
                                               <td>
                                                    <asp:TextBox ID="txt_publicip" runat="server" Width="171px" Enabled="false"></asp:TextBox>
                                               </td>

                                                <td>
                                                    <label>
                                                      Server private IP
                                                    </label> 
                                               </td>
                                               <td>
                                                    <asp:TextBox ID="txt_privateip" runat="server" Width="171px" Enabled="false"></asp:TextBox>
                                               </td>
                                          </tr>
                                    </table>
                            </fieldset>                                
                        </label> 
                    </td>
                </tr>
               
               <tr>
                    <td colspan="3">
                    <label>
                        Physical path where the master code and the versions will be kept
                        </label> 
                    </td>
               </tr>
                  <tr>
                        <td valign="top">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Source path folder name"></asp:Label>
                                         <asp:Label ID="Label41" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          
                                    </label>
                                </td>
                        <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                             <label style="width:300px">
                                                     <asp:TextBox ID="txt_mastercodepath" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                                                 </label> 
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:TextBox ID="txt_MaterCodeProductVersionFolder" runat="server" Width="250px"></asp:TextBox>
                                                 </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <label style="font-size:10px;">                                            
                                                Server default path for master version code\companyid\
                                            </label> 
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table> 

                                </td>
                  </tr>
                  <tr>
                        <td colspan="3">
                           <label>  
                                Physical Path where the code for all website for the product will be kept                 
                           </label>
                        </td>                       
                 </tr>
                 <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Source path folder name"></asp:Label>
                                         <asp:Label ID="Label32" runat="server" Text="*" CssClass="labelstar"></asp:Label>                                          
                                    </label>
                                </td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                             <label style="width:300px">
                                                     <asp:TextBox ID="txtMasterIISWebsite" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                                                 </label> 
                                            </td>
                                            <td>
                                                <label>
                                        <asp:TextBox ID="txt_IISProductWebsiteFolder" runat="server" Width="250px"></asp:TextBox>
                                    </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <label style="font-size:12px;">                                            
                                               Server Default Path For website\companyid\
                                            </label> 
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table> 

                                </td>
                            </tr>
               
                
                             <asp:Panel ID="Panel1" runat="server" Visible="false">
                           <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label35" runat="server" Text="Output Path Folder Name"></asp:Label>
                                         <asp:Label ID="Label36" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtoutputsourcepath" ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                               <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtoutputsourcepath" runat="server" Width="400px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="Temp Path for Compilation"></asp:Label>
                                         <asp:Label ID="Label34" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txttemppath"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txttemppath" runat="server"  Width="400px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>                           
                            
                            <tr>
                            <td colspan="3">
                            <label style="width:700px;">
                                        <asp:Label ID="Label37" runat="server" Text="List of Folder And File Not To Be included When Publishing Code :"></asp:Label>
                                    </label>
                                     <label style="width:400px;">
                                            <asp:Label ID="Label38" runat="server" Text="Ex: \\ShoppingCart\\Admin\\VersionFolder"></asp:Label>
                                        </label>
                            </td>
                            </tr>
                               <tr>
                                <td style="width:20%">
                                    
                                </td>                                
                                <td>
                                 <label style="width:500px;">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="480px"></asp:TextBox>
                                        
                                    </label>
                                    <label style="width:40px;">
                                       <asp:Button ID="Button5"  runat="server" Text="Add" OnClick="Button5_Click" />
                                    </label>
                                </td>
                                <td>
                               
                                </td>
                            </tr>
                            <tr>
                                <td style="width:20%">
                                </td>
                                <td colspan="2">
                                    <label style="width:500px;">
                                        <asp:ListBox ID="ListBox1" runat="server" Width="484px"></asp:ListBox>
                                    </label>
                                    
                                   <label style="width:50px;">
                                   <asp:Button ID="Button6" runat="server" Text="Remove" CssClass="btnSubmit" OnClick="Button6_Click" />
                                   </label>
                                    <label style="width:40px;">
                                     <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnSubmit" OnClick="btnEdit_Click"
                                        Visible="False" />
                                    </label>
                                </td>
                            </tr>
                            </asp:Panel>
                
                
                <tr>
                    <td colspan="3">
                    <asp:Panel ID="pnlhideimg" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label18" runat="server" Text="Image Upload"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="filemain" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>        
                                                               
                                    <td colspan="2">
                                        <label>
                                            <asp:CheckBox ID="chkimg" runat="server" Text=" Do you wish to uplaod images for this product ?      Yes"
                                                AutoPostBack="True" OnCheckedChanged="chkimg_CheckedChanged" 
                                            TextAlign="Left" />
                                        </label>
                                        
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <asp:Panel ID="pnlimage" runat="server" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Top"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="filetop" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Bottom"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="filebottom" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Right"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="fileright" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Left"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="fileleft" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Front"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="filefront" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="back"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:FileUpload ID="fileback" runat="server" />
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                            ValidationGroup="1" CssClass="btnSubmit" />
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button1_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <fieldset>
        <legend>
            <asp:Label ID="Label30" runat="server" Text="List of Products"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                        OnClick="Button1_Click1" />
                    <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Product Status"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="181px"
                            OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" ></asp:ListItem>
                            <asp:ListItem Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive"></asp:ListItem>
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label42" runat="server" Text="Search"></asp:Label>
                    </label> 
                    <label>
                        <asp:TextBox ID="txt_search" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px"></asp:TextBox>
                    </label> 
                    <label>
                        <asp:Button ID="Btn_Search" runat="server" Text="GO" OnClick="Btn_Search_Click" ValidationGroup="1" />
                    </label> 
                     <label style="width:140px">
                        <asp:Label ID="Label40" runat="server" Visible="false" Text="Product Name"></asp:Label>
                    </label>
                    <label>
                            <asp:DropDownList ID="ddproductfilter" DataTextField="ProductName" DataValueField="ProductId" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Width="190px" Visible="false">
                            </asp:DropDownList>
                    </label> 
                   
                </td>
            </tr>
           
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <%--<asp:UpdatePanel ID="UpdatePanelSalesOrder" runat="server">
                                            <ContentTemplate>--%>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--    <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Products"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="productDetailId" AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt" AllowSorting="True" Width="100%" OnSorting="GridView1_Sorting"
                                      OnRowDataBound="GridView1_RowDataBound"   EmptyDataText="No Record Found.">
                  
                                        <Columns>
                                            <asp:BoundField DataField="productId" HeaderStyle-HorizontalAlign="Left" HeaderText="Id" SortExpression="productId" />
                                            <asp:BoundField DataField="vid" HeaderStyle-HorizontalAlign="Left" HeaderText="vid" SortExpression="vid" />
                                                
                                            <asp:BoundField DataField="productName" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name"
                                                ItemStyle-Width="30%" SortExpression="productName">
                                                <ItemStyle Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Detail Active" ItemStyle-Width="5%">
                                                <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                         
                                             <asp:BoundField DataField="VersionNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Version No" Visible="false" 
                                                SortExpression="VersionNo" ItemStyle-Width="5%">
                                                <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VersionInfoName" HeaderStyle-HorizontalAlign="Left" HeaderText="Version Info Name"
                                                SortExpression="VersionInfoName" ItemStyle-Width="5%">
                                                <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                               <asp:BoundField DataField="VActive" SortExpression="VActive" HeaderStyle-HorizontalAlign="Left" HeaderText="Version Active" ItemStyle-Width="5%">
                                                <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Product URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%"
                                                SortExpression="productURL">
                                                <ItemTemplate>
                                                    <asp:Label ID="Labellink" runat="server" Text='<%# Bind("productURL") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("productURL") %>'
                                                        OnClick="LinkButton1_Click" ForeColor="Black"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PricePlan URL" HeaderStyle-HorizontalAlign="Left" Visible="false" 
                                                ItemStyle-Width="25%" SortExpression="PricePlanURL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVid" runat="server" Text='<%# Bind("vid") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblpid" runat="server" Text='<%# Bind("productId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblProductDetailId" runat="server" Text='<%# Bind("ProductDetailId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="Labellink1" runat="server" Text='<%# Bind("PricePlanURL") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton11" runat="server" Text='<%# Bind("PricePlanURL") %>'
                                                        OnClick="LinkButton11_Click" ForeColor="Black"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StartDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date"
                                                ItemStyle-Width="10%" DataFormatString="{0:d-MMM-yyyy}" SortExpression="StartDate">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EndDate" HeaderStyle-HorizontalAlign="Left" HeaderText="EndDate"
                                                ItemStyle-Width="10%" DataFormatString="{0:d-MMM-yyyy}" SortExpression="EndDate">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                              <asp:TemplateField HeaderText="Edit">
                                                         <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton2" Style="color: #717171;" runat="server" Text="Edit"  OnClick="link2_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delete">
                                                         <ItemTemplate>
                                                 <asp:LinkButton ID="l_btn_delete" Style="color: #717171;" runat="server" Text="Delete"  OnClick="l_btn_delete_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("vid") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                            </asp:TemplateField>

                                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Visible="false" Text="Button" HeaderImageUrl="~/Account/images/edit.gif" />
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <%--</ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSubmit"></asp:AsyncPostBackTrigger>
                                            </Triggers>
  <br/>                                      </asp:UpdatePanel>--%>
                    </asp:Panel>
                </td>
            </tr>
             <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                        BorderStyle="Solid" BorderWidth="5px">
                        <table style="width: 100%">
                            <tr>
                                <td style="height: 9px">
                                    &nbsp;
                                </td>
                                <td align="right" style="height: 9px">
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                        Width="16px" OnClick="ImageButton1_Click" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        You will need to buy our services which allows download of Busicontroller for your
                                        Product. Busicontroller will allow to regulate your license terms with your customer.</label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <asp:Button ID="Button2" runat="server" Text="OK" CssClass="btnSubmit" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button11" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel2" TargetControlID="Button11">
                    </cc1:ModalPopupExtender>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
    <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
    <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId"
        style="width: 3px" />
    <input runat="server" id="hdnProductId" type="hidden" name="hdnProductId" style="width: 3px" />
</asp:Content>
