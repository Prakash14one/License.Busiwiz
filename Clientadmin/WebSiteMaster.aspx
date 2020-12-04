<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="WebSiteMaster.aspx.cs" Inherits="ShoppingCart_Admin_CompanyWebsitMaster"
    Title="WebSite Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
            <div style="float: left;">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <div style="float: right;">
                    <asp:Button ID="addnewpanel" runat="server" Text="Add Website" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                    <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Click" Text="Do Synchronise" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label6" runat="server" Text="Website Master - Manage"></asp:Label>
                        </legend>
                        <table cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select Product/Version"></asp:Label>
                                        <asp:Label ID="Label85" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFsdfsdfieldValidator34" runat="server" ControlToValidate="ddlProductname"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlProductname" runat="server" Width="300px"  AutoPostBack="True"  onselectedindexchanged="ddlproductname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Website Title"></asp:Label>
                                        <asp:Label ID="Label57" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtSiteName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                       
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txtSiteName" runat="server" Width="216px" 
                                        ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" 
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'div1',20)" 
                                        AutoPostBack="True" ontextchanged="txtSiteName_TextChanged"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label38" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>

                               <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label148" runat="server" Text="Url of production code of website "></asp:Label>
                                     
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="TextBox9" runat="server" Width="216px" 
                                       
                                        ></asp:TextBox>
                                    </label>
                                   
                                </td>
                            </tr>



                              <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label149" runat="server" Text="Domain name to be used when creating website in client IIS "></asp:Label>
                                     
                                    </label>
                                </td>
                                    <td align="left">
                                      <label>
                                        <asp:Label ID="Label152" runat="server" Text=" 35 "></asp:Label>
                                        .
                                     </label>
                                     <label>
                                    <asp:TextBox ID="TextBox10" runat="server" AutoPostBack="True"  
                                      
                                       
                                        ValidationGroup="1" Width="216px" ontextchanged="TextBox10_TextChanged"></asp:TextBox>
                               </label>
                                    </td>
                            </tr>


                             

                             <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label153" runat="server" Text="Select the primary  DNS server  for the domain"></asp:Label>
                                      <asp:Label ID="Label156" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                   
                                </td>
                            </tr>






                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Website URL"></asp:Label>
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="http://www.ijobcenter.com/images/questionmark.jpg"
                                                    ToolTip="This is the website where the Developers will work to add and update pages through version folder." Width="25px" />
                                        <asp:Label ID="Label58" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWebUrl"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtWebUrl" runat="server" Width="216px" ValidationGroup="1" MaxLength="100"
                                          AutoPostBack="True" OnTextChanged="txtWebUrl_TextChanged"   onkeyup="return mak('Span20',100,this)"></asp:TextBox>
                                    </label>
                                    
                                    <label>
                                    <asp:Label ID="lbl_weburlconnection" runat="server" Text="" Visible="false" ForeColor="Green"></asp:Label>
                                    </label>
                                     
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Port"></asp:Label>
                                        <asp:Label ID="Label59" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWebsitePort"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtWebsitePort" runat="server" ValidationGroup="1" MaxLength="20"
                                            Width="216px" onkeyup="return mak('Span8',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtWebsitePort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label87" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span8" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label46" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <%-- <tr>
                              <td align="right" valign="top">
                                    Website Port :
                                </td>
                                <td class="col4">
                                    <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="1" 
                                        MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtWebsitePort"
                                        InitialValue="--Select--" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </td>
                                 <td align="right" valign="top">
                                   
                                </td>
                                <td class="col4">
                                   
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label7" runat="server" Text="IIS Server Information"></asp:Label>
                        </legend>
                        <table width="100%">
                             <asp:Panel ID="Panel1" runat="server" Visible="false"> 
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Server IP-Url"></asp:Label>
                                        <asp:Label ID="Label60" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtIISServerIP"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator22" ControlToValidate="txtIISServerIP"
                                            runat="server" ErrorMessage="Enter Valid IIS Server IP-Url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtIISServerIP" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span21',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label88" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span21" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label61" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtServerUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtServerUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txtServerUserId" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:.;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span9',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label89" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span9" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label47" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                    <%--                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                        ControlToValidate="txtServerUserId" runat="server" 
                                        ErrorMessage="RegularExpressionValidator" 
                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label144" runat="server" Text="Public IP"></asp:Label>
                                       
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txt_publicip" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"></asp:TextBox>
                                    </label>
                                    <label>
                                       
                                    </label>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label145" runat="server" Text="Private IP"></asp:Label>
                                       
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txt_private" runat="server" Width="200px" ValidationGroup="1" MaxLength="20" ></asp:TextBox>
                                    </label>
                                    <label>
                                       
                                    </label>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label62" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtServerPw"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="invalid Character"
                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([@+-a-zA-Z0-9_.\s]*)"
                                    ControlToValidate="txtServerPw" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtServerPw" runat="server" ValidationGroup="1" MaxLength="20" TextMode="Password"
                                            Width="200px" onkeyup="return mak('Span15',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label90" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span15" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label52" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label63" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtISSPort"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtISSPort" runat="server" ValidationGroup="1" Width="200px" MaxLength="20"
                                            onkeyup="return mak('Span7',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            TargetControlID="txtISSPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label91" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span7" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label45" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="2">
                                     <label>
                                        Server Name:
                                    </label> 
                                    <label>
                                        <asp:Label ID="Label131" runat="server" Text=""></asp:Label>
                                    </label> 
                                    <label>
                                    Public IP:
                                    </label>
                                    <label>
                                      <asp:Label ID="Label132" runat="server" Text=""></asp:Label>
                                    </label> 
                                    <label>
                                    Private IP:
                                    </label>   
                                     <label>
                                      <asp:Label ID="Label147" runat="server" Text=""></asp:Label>
                                         <asp:Label ID="Label128" runat="server" Text="" Visible="false"></asp:Label>
                                    </label>                              
                                      <label>
                                        <asp:DropDownList ID="ddlserverMas" runat="server" Width="300px"  AutoPostBack="True"  onselectedindexchanged="ddlserverMas_SelectedIndexChanged " Visible="false" >
                                        </asp:DropDownList>
                                    </label> 
                                     <label>                                    
                                        <asp:Label ID="lbl_servertest" runat="server" Text=" " ForeColor="Green"></asp:Label>                                    
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <label>
                                Physical path where the code for the website will be kept
                                </label>
                                </td>
                                </tr>
                            <tr>
                                 <td style="width: 35%" valign="top">
                                    <label>
                                        SourcePath FolderName  &nbsp
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="http://www.ijobcenter.com/images/questionmark.jpg"
                                                    ToolTip="Physical Path where the code for all website for the product will be kept." Width="25px" />                                   
                                    </label> 
                                   
                                    
                              </td>
                               <td>

                                       <label>
                                        <asp:TextBox ID="txt_codepath" runat="server" Enabled="false"  Width="400px" MaxLength="200"></asp:TextBox>
                           </label>
                               
                                     <label>
                                     \</label>
                                     <label>
                                     <asp:TextBox ID="txt_IISWebsiteFolderName" runat="server" AutoPostBack="True" 
                                         MaxLength="200" OnTextChanged="txt_IISWebsiteFolderName_TextChanged" 
                                         ValidationGroup="1" Width="200px"></asp:TextBox>
                                     </label>
                                     <label>
                                     <asp:Label ID="Label130" runat="server" CssClass="labelcount" 
                                         Text="eg. \wesbite1"></asp:Label>
                                     </label>
                                     <label>
                                     <asp:Label ID="lbl_webfoldertest" runat="server" ForeColor="Green" Text=" "></asp:Label>
                                     </label>
                                     <label>
                                     <asp:Label ID="Label129" runat="server" CssClass="labelstar" Text=""></asp:Label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" 
                                         ControlToValidate="txtISSPort" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                     </label>
                                    
                                     </td>
                                
                            </tr>
                          
                            <tr>
                                <td colspan="2" align="right">
                                    <label style="font-size:10px;" >
                                       <asp:Label ID="Label140" runat="server" Text="( For eg. If the website physical path is I:\mainfolder\wesbite1 and product mastercode path is already defined as I:\Mainfolder then mention here just  \website1 )" ></asp:Label>
                                    </label> 
                                </td>
                            </tr>
                            <tr >
                                <td style="width: 35%" valign="top">
                                    <label >
                                        <asp:Label ID="Label137" runat="server" Text="Version folder path on server computer"></asp:Label>
                                        </label>
                                        </td>
                                            <td>
                                        <label>
                                        <asp:TextBox ID="TextBox5" runat="server" Width="400px"></asp:TextBox>
                                        </label>
                                        <label>\</label>
                                        <label>
                                    <%--<asp:TextBox ID="txt_versionfolderrootpath" runat="server" Width="400px" MaxLength="500"></asp:TextBox>--%>
                                    <asp:TextBox ID="txt_IISVersionFolderPath" runat="server" AutoPostBack="True" 
                                        MaxLength="500" OnTextChanged="txt_IISVersionFolderPath_TextChanged" 
                                        ValidationGroup="1" Width="200px"></asp:TextBox>
                                        </label>
                                        <label>
                                    <asp:Label ID="Label146" runat="server" CssClass="labelcount" 
                                        Text="eg. \VersionFolder"></asp:Label>
                                    <asp:Label ID="lbl_versionfoldertest" runat="server" ForeColor="Green" Text=" "></asp:Label>
                                        </label>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2" align="right" >
                                     <label style="font-size:10px;">
                                        <asp:Label ID="Label139" runat="server" Text="( For eg. If the website physical path is I:\mainfolder\wesbite1\Versionfolder and product mastercode path is already defined as I:\Mainfolder then mention here just \wesbite1\Versionfolder )" CssClass="labelcount"></asp:Label>
                                        
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                    <asp:Label ID="Label134" runat="server" Text="Website URL version folder path"></asp:Label>
                                    <asp:Label ID="Label135" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_IISVersionFolderPath" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                    
                               </td>
                               <td>
                                <label>
                                        <asp:TextBox ID="txtVersionfolderpath" runat="server" ValidationGroup="1" Width="400px" MaxLength="500" AutoPostBack="True"  ></asp:TextBox>                                         
                                    </label>
                                   
                                    <label>                                       
                                        <span id="Span33">eg. License.busiwiz.com/VersionFolder</span>
                                         <asp:Label ID="Label136" runat="server" Text="" CssClass="labelcount"></asp:Label>
                                        <span id="Span32" cssclass="labelcount"></span>
                                        <asp:Label ID="Label138" runat="server" Text="" CssClass="labelcount" Visible="False"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                             
                           

                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                    <asp:Label ID="Label133" runat="server" Text="Password protected folder of this site"></asp:Label>
                                    </label>
                               </td>
                               <td>
                                <label>
                                 <asp:TextBox ID="TextBox6" runat="server" ValidationGroup="1" Width="400px" MaxLength="200" AutoPostBack="True"  ></asp:TextBox>
                                
                                </label>
                                <label>\</label>
                                <label>
                                 <asp:TextBox ID="TextBox7" runat="server" ValidationGroup="1" Width="200px" MaxLength="200" ></asp:TextBox>
                                
                                </label>
                                <asp:ImageButton ID="ImageButton2" runat="server" style="  margin-top: 12px;" ImageUrl="http://www.ijobcenter.com/images/questionmark.jpg"
                                                    ToolTip=" The folder name whose access will be restricted and userid and password will be required to access the folder.(The entire code should be put under this subfolder)" Width="25px" />


                                <asp:LinkButton ID="lnkMsg" runat="server" ForeColor="Black" Visible="false">Help ?</asp:LinkButton>
                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="modal1" CancelControlID="btnCancelPopup" runat="server" TargetControlID="lnkMsg" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" Height="100px" Width="400px" runat="server" CssClass="pnlBackGround" Visible="false">
                                        <asp:Label ID="lblMsgPopup" runat="server" Text="Help : If your website is published in the folder I:/wesbites/myproject and 
if you have kept a subfolder namely 'Admin' in that which is password protected so that no user 
from the internetcan see the pages in the folder admin, then mention the path 'I:/websites/Myproduct/Admin' in this field"></asp:Label><br /><br />
                                        <center><asp:Button ID="btnCancelPopup" runat="server" Text="Close"  /></center>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                  


                    <fieldset >
                        <legend>
                            <asp:Label ID="Label12" runat="server" Text="Database Information"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Database Name"></asp:Label>
                                        <asp:Label ID="Label64" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDatabaseName" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'div1',20)"></asp:TextBox>
                                            <%--onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span1',20)"--%>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label92" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label39" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="IP-Url"></asp:Label>
                                        <asp:Label ID="Label65" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDatabaseServerurl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDatabaseServerurl" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span22',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label93" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span22" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label67" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDBUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtDBUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDBUserId" runat="server" Width="200px" ValidationGroup="1" MaxLength="20"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label94" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span11" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label48" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label68" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtDBPassword" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span16',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label95" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span16" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label53" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label69" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtDatabaseAccessPort"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtDatabaseAccessPort" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span6',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                            TargetControlID="txtDatabaseAccessPort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label96" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span6" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label44" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                      </asp:Panel>
                       <asp:Panel ID="Panel3" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label18" runat="server" Text="BusiControlller Information"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="BusiController Name"></asp:Label>
                                        <asp:Label ID="Label70" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBusicontrollerName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtBusicontrollerName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusicontrollerName" MaxLength="50" runat="server" ValidationGroup="1"
                                            Width="200px" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span2',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label97" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label40" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Database Name"></asp:Label>
                                        <asp:Label ID="Label71" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtBusiDatabaseName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtBusiDatabaseName" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiDatabaseName" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span3',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label98" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span3" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label41" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Server Ip-Url"></asp:Label>
                                        <asp:Label ID="Label72" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtBusiServerUrl"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiServerUrl" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="50" onkeyup="return mak('Span23',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label99" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span23" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label22" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label73" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBusiUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtBusiUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiUserId" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span12',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label100" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span12" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label49" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label23" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label74" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDBPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusipassword" runat="server" ValidationGroup="1" MaxLength="20"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span17',20,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label101" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span17" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label54" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label24" runat="server" Text="Access Port"></asp:Label>
                                        <asp:Label ID="Label75" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtBusiController"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiController" runat="server" Width="200px" ValidationGroup="1"
                                            MaxLength="20" onkeyup="return mak('Span5',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            TargetControlID="txtBusiController" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label102" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span5" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label43" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                        <asp:Label ID="Label25" runat="server" Text="ConnectionString"></asp:Label>
                                        <asp:Label ID="Label76" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtBusiconnectionString"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtBusiconnectionString" runat="server" MaxLength="100" ValidationGroup="1"
                                            Width="200px" onkeyup="return mak('Span24',100,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label103" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span24" cssclass="labelcount">100</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </asp:Panel>
                     <asp:Panel ID="Panel4" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label26" runat="server" Text="FTP for Source Code"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblFTPURLMsg1" runat="server" Text="NoteNote : This FTP account must map to the parent folder where your website code is saved. For example if your website code is placed in the folder 'Mywebsitecode' and it is placed in the folder I:/ websites  then the this ftp account should be mapped to I:/websites"></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    &nbsp;</td>
                                <td class="col2" align="left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                    <asp:Label ID="Label27" runat="server" Text="Url"></asp:Label>
                                    <asp:Label ID="Label77" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                        ControlToValidate="txtFtpUrl" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                                        ControlToValidate="txtFtpUrl" ErrorMessage="*" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td align="left" class="col2">
                                    <label>
                                    <asp:TextBox ID="txtFtpUrl" runat="server" MaxLength="50" 
                                        onkeyup="return mak('Span25',50,this)" ValidationGroup="1" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                    <asp:Label ID="Label104" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                    <span ID="Span25" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label28" runat="server" Text="Port"></asp:Label>
                                        <asp:Label ID="Label78" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtFtpPort"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <label>
                                        <asp:TextBox ID="txtFtpPort" runat="server" ValidationGroup="1" MaxLength="20" Width="200px"
                                            onkeyup="return mak('Span4',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            TargetControlID="txtFtpPort" ValidChars="0147852369/\." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label105" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span4" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label42" CssClass="labelcount" runat="server" Text="(0-9 / \ .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" class="col1">
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label79" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtFtpUserId"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtFtpUserId" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtFtpUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtFtpUserId" runat="server" Width="200px" ValidationGroup="1" MaxLength="50"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span13',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label106" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span13" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label50" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label30" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label80" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtFtpPassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtFtpPassword" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td class="col2" align="left">
                                    <label>
                                        <asp:TextBox ID="txtFtpPassword" runat="server" ValidationGroup="1" MaxLength="50"
                                            TextMode="Password" Width="200px" onkeyup="return mak('Span18',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label107" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span18" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label55" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                </td>
                                <td align="left">
                                    <asp:Button ID="Button1" ValidationGroup="2" runat="server" Text="Test Ftp Server Connection"
                                        OnClick="Button1_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </asp:Panel>
                     <asp:Panel ID="Panel5" runat="server" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label35" runat="server" Text="FTP for Work Guide"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                   
                                    <asp:Label ID="lblFTPWorkGuideURLMsg1" runat="server" Text="NoteNote : This FTP account must map to the parent folder where your website code is saved. For example if your website code is placed in the folder 'Mywebsitecode' and it is placed in the folder I:/ websites  then the this ftp account should be mapped to I:/websites"></asp:Label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                    <asp:Label ID="Label34" runat="server" Text="URL"></asp:Label>
                                    <asp:Label ID="Label81" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtFTPWorkGuideUrl" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtFTPWorkGuideUrl" ValidationGroup="3">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                    <asp:TextBox ID="txtFTPWorkGuideUrl" runat="server" MaxLength="50" onkeyup="return mak('Span26',50,this)" ValidationGroup="1" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                    <asp:Label ID="Label108" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                    <span id="Span26" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label33" runat="server" Text="Port"></asp:Label>
                                        <asp:Label ID="Label82" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtFtpWorkGuidePort"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtFtpWorkGuidePort" runat="server" ValidationGroup="1" Width="200px"
                                            MaxLength="20" onkeyup="return mak('Span10',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                            TargetControlID="txtFtpWorkGuidePort" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label109" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span10" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label66" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label83" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtFtpWorkGuideUserId"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtFtpWorkGuideUserId"
                                            ValidationGroup="3">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtFtpWorkGuideUserId" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtFtpWorkGuideUserId" runat="server" MaxLength="50" ValidationGroup="1"
                                            Width="200px" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span14',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label110" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span14" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label51" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label84" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtFtpWorkGuidePassword"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtFtpWorkGuidePassword"
                                            ValidationGroup="3">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtFtpWorkGuidePassword" runat="server" ValidationGroup="1" TextMode="Password"
                                            MaxLength="50" Width="200px" onkeyup="return mak('Span19',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label111" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span19" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label56" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                </td>
                                <td align="left">
                                    <asp:Button ID="Button2" ValidationGroup="3" runat="server" Text="Test Ftp Server Connection"
                                        OnClick="Button2_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                     </asp:Panel>
                     
                   
                        <table width="100%">
                       <asp:Panel ID="Panel6" runat="server" Visible="false">
                            <tr>
                                <td colspan="2" width="100%">
                                    <asp:Label ID="lblFTPUploadURLMsg1" runat="server" Text="Note that the FTP account for file uploads by developers for various versions of the software during development of pages,should be mapped to the same folder as "></asp:Label>
                                     <asp:Label ID="Label141" runat="server" Text=" above mentioned 'Version Folder path on server computer' That path is reproduced here below for your easy reference. "></asp:Label>
                                    
                                    <%--NoteNote : This FTP account must map to the parent folder where your website code is saved. For example if your website code is placed in the folder 'Mywebsitecode' and it is placed in the folder I:/ websites  then the this ftp account should be mapped to I:/websites--%>
                                    
                                    
                                </td>
                            </tr>
                               <tr>
                                <td style="width: 35%" valign="top">
                                    &nbsp;</td>
                                <td><label>Ex:I:/capmanversion/license.busiwiz.com/VersionFolder/</label> 
                                    &nbsp;</td>
                            </tr>
                             

                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                    <asp:Label ID="Label113" runat="server" Text="URL"></asp:Label>
                                    <asp:Label ID="Label114" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="TextBox1" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="TextBox1" ValidationGroup="33">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" onkeyup="return mak('Span27',50,this)" ValidationGroup="1" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                    <asp:Label ID="Label115" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                    <span id="Span27" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                         
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label116" runat="server" Text="Port"></asp:Label>
                                        <asp:Label ID="Label117" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="TextBox2"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox2" runat="server" ValidationGroup="1" Width="200px" MaxLength="20"
                                            onkeyup="return mak('Span28',20,this)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            TargetControlID="TextBox2" ValidChars="0147852369." />
                                    </label>
                                    <label>
                                        <asp:Label ID="Label118" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span28" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label119" CssClass="labelcount" runat="server" Text="(0-9 .)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label120" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label121" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtFtpWorkGuideUserId"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="TextBox3"
                                            ValidationGroup="33">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="TextBox3" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="50" ValidationGroup="1" Width="200px"
                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span29',50)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label122" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span29" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label123" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%" valign="top">
                                    <label>
                                        <asp:Label ID="Label124" runat="server" Text="Password"></asp:Label>
                                        <asp:Label ID="Label125" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="TextBox4"
                                            ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="TextBox4"
                                            ValidationGroup="33">*</asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TextBox4" runat="server" ValidationGroup="1" TextMode="Password"
                                            MaxLength="50" Width="200px" onkeyup="return mak('Span30',50,this)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label126" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span30" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label127" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 35%">
                                </td>
                                <td align="left">
                                    <asp:Button ID="Button5" ValidationGroup="33" runat="server" Text="Test Ftp Server Connection"
                                        CssClass="btnSubmit" OnClick="Button5_Click" />
                                </td>
                            </tr>
                            <tr>
                            </asp:Panel>
                           
                            </tr>
                              <tr>
                                <td style="width: 35%">
                                <label>
                                 <asp:Label ID="Label112" runat="server" Text="Status"></asp:Label>
                                </label>
                                </td>
                                <td>
                                <label>
                                
                                     <asp:CheckBox ID="CheckBox1" runat="server" Text="Active"/>
                                </label>
                                </td>
                                </tr>
                       

                            <tr>
                                <td style="width: 35%">
                                </td>
                                <td>
                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btnSubmit" 
                                        OnClick="BtnSubmit_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="btnSubmit" 
                                        OnClick="BtnUpdate_Click" Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btnSubmit" 
                                        OnClick="BtnCancel_Click" Text="Cancel" />
                                    
                                </td>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                    <td style="width: 178px">
                                        <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                    </td>
                                </tr>
                            </tr>
                        </table>
                  
                    </asp:Panel>
               
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="Label36" runat="server" Text="List of Company Websites"></asp:Label>
                </legend>
                <div style="float: right">
                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                        OnClick="Button1_Click1" />
                    <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
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
                                        <%--<tr align="center">
                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="Label37" runat="server" Font-Italic="true" Text="List of Company Websites"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <label>                                   
                                   Filter by Server
                                   
                                  
                                   </label>
                                   <label>
                                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                     onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                       </asp:DropDownList>
                                   
                                   
                                   <asp:DropDownList ID="Ddlproduct_search" runat="server" Visible="false" DataTextField="ProductName" DataValueField="ProductId" AutoPostBack="True" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                   </label>
                                   <label>
                                   
                                   Search
                                   </label>
                                   <label>
                                       <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                   </label>
                                   <label>
                                       <asp:Button ID="Button6" runat="server" Text="Go" 
                                     onclick="Button6_Click" />
                                   </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                                    EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" AllowPaging="True"
                                    AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%"
                                    OnRowDeleting="GridView1_RowDeleting" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" PageSize="15" OnSorting="GridView1_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" ItemStyle-Width="20%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("ProductName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version" SortExpression="VersionInfoName" ItemStyle-Width="12%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVersionInfoName" runat="server" Text='<%# Bind("VersionInfoName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Website Name" SortExpression="WebsiteName" ItemStyle-Width="22%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWebsiteName" runat="server" Text='<%# Bind("WebsiteName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Website Url" SortExpression="WebsiteUrl" ItemStyle-Width="40%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblurl" runat="server" Text='<%# Bind("WebsiteUrl")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Server Name" SortExpression="WebsiteUrl" ItemStyle-Width="40%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserver" runat="server" Text='<%# Bind("ServerName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Server Public IP " SortExpression="WebsiteUrl" ItemStyle-Width="40%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpublicip" runat="server" Text='<%# Bind("PublicIp")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Server Private IP" SortExpression="WebsiteUrl" ItemStyle-Width="40%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprivateip" runat="server" Text='<%# Bind("Ipaddress")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>


                                        <asp:ButtonField CommandName="editview" Text="Edit" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                            ButtonType="Image" ItemStyle-Width="3%">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="3%" />
                                        </asp:ButtonField>
                                        <%--<asp:ButtonField CommandName="Delete" Text="Delete" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                        HeaderImageUrl="~/ShoppingCart/images/trash.jpg" ImageUrl="~/images/delete.gif"
                                        ButtonType="Image" ItemStyle-Width="3%">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="3%" />
                                    </asp:ButtonField>--%>


                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete"
                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                    CommandArgument='<%# Eval("ID") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                        <td>
                          <asp:Panel ID="Paneldoc" runat="server" Width="65%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                        </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
