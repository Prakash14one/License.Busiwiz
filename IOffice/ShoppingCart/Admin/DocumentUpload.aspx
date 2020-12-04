<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="DocumentUpload.aspx.cs" Inherits="Account_DocumentUpload"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Import Namespace="System.Runtime.InteropServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">

  
 
 function RealNumWithDecimal(myfield, e, dec)
{

//myfield=document.getElementById(FindName(myfield)).value
//alert(myfield);
var key;
var keychar;
if (window.event)
key = window.event.keyCode;
else if (e)
key = e.which;
else
return true;
keychar = String.fromCharCode(key);
   if(key==13)
{
return false;
}
if ((key==null) || (key==0) || (key==8) || (key==9) || (key==27) )
{
return true;
}
else if ((("0123456789.").indexOf(keychar) > -1))
{
return true;
}
// decimal point jump
else if (dec && (keychar == "."))
{

 myfield.form.elements[dec].focus();
  myfield.value="";
 
return false;
}
else
{
  myfield.value="";
  return false;
}
}
 
    </script>

    <script language="javascript" type="text/javascript"> 
    function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if( evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
                        
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        
        
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
    
    
    </script>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" Width="206px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Cabinet - Drawer - Folder"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldoctype"
                                        Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:DropDownList ID="ddldoctype" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged"
                                        Width="426px">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton49" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        OnClick="ImageButton49_Click" Width="30px" AlternateText="Add New" Height="20px"
                                        ToolTip="AddNew" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        OnClick="ImageButton48_Click" AlternateText="Refresh" Height="20px" Width="20px"
                                        ToolTip="Refresh" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Document Title"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoctitle"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:TextBox ID="txtdoctitle" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                        onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                        ValidationGroup="1" Width="420px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="div1" class="labelcount">30</span>
                                    <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Document Date"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:TextBox ID="TxtDocDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="TxtDocDate">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Document Ref. Number"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                        ValidationGroup="1" MaxLength="100"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="sp11" class="labelcount">100</span>
                                    <asp:Label ID="Label17" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Net Amount"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:TextBox ID="txtnetamount" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                        onkeypress="return RealNumWithDecimal(this,event,2);" runat="server"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span1" class="labelcount">10</span>
                                    <asp:Label ID="Label18" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Party Name"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:DropDownList ID="ddlpartyname" runat="server" ValidationGroup="1" Width="200px">
                                    </asp:DropDownList>
                                </label>
                                <%--<asp:HyperLink ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="12px"
                            Target="_blank" NavigateUrl="~/ShoppingCart/Admin/PartyMaster.aspx">Add New </asp:HyperLink>
                        &nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" Font-Size="12px"
                            OnClick="LinkButton2_Click">Refresh</asp:LinkButton>--%>
                                <label>
                                    <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        OnClick="ImageButton50_Click" Width="30px" ToolTip="AddNew" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        OnClick="ImageButton51_Click" Width="20px" ToolTip="Refresh" />
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpartyname"
                                Display="Dynamic" ErrorMessage="*" InitialValue="0" ValidationGroup="1">*</asp:RequiredFieldValidator>&nbsp;--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Add Document"></asp:Label>
                                    <asp:Label ID="Label13" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fileuploadocurl"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <asp:FileUpload ID="fileuploadocurl" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label14" runat="server" Text="Document Description"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtdocdscrptn" ValidationGroup="1"></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td style="width: 65%">
                                <label>
                                    <asp:TextBox ID="txtdocdscrptn" runat="server" MaxLength="500" onkeypress="return checktextboxmaxlength(this,500,event)"  
                                                             
                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span2',500)"
                                        Height="62px" TextMode="MultiLine" ValidationGroup="1" Width="454px"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label15" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span2" class="labelcount">500</span>
                                    <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ , .)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <input id="hdbDId" name="hdbDId" runat="server" type="hidden" style="width: 1px" />
                                <input id="hdbPartyId" name="hdbPartyId" runat="server" type="hidden" style="width: 1px" />
                                <input id="hdbDMId" name="hdbDMId" runat="server" type="hidden" style="width: 1px" />
                                <input id="hdbDSId" name="hdbDSId" runat="server" type="hidden" style="width: 1px" />
                                <input id="hdnSIZEofFOLDER" name="hdbDSId" runat="server" type="hidden" style="width: 1px" />
                            </td>
                            <td style="width: 65%" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%" align="right">
                            </td>
                            <td style="width: 65%" align="left">
                                <asp:CheckBox ID="Chkautoprcss" runat="server" Text="Filling Desk Approval not Required"
                                    Height="27px" Checked="True" />
                                <br />
                                <label>
                                    <asp:Label ID="lbldesc" runat="server" Text="(If This is selected,The Document would be available for viewing by all users depending on their rights,otherwise it would go to document processing department for further processing)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                            </td>
                            <td style="width: 65%">
                                <asp:Button ID="imgbtnupload" runat="server" Text="Upload" CssClass="btnSubmit" EnableTheming="True"
                                    ValidationGroup="1" OnClick="imgbtnupload_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="imgbtnreset0" runat="server" Text="Reset" CssClass="btnSubmit" OnClick="imgbtnreset0_Click" />
                            </td>
                        </tr>
                    </table>
                    <%-- </td>
        </tr>
    </table>--%>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ImageButton48" EventName="Click" />
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ImageButton51" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton50" EventName="Click" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnupload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
