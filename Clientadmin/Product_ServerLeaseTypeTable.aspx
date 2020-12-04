<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Product_ServerLeaseTypeTable.aspx.cs" Inherits="AddProduct" Title="Add Product / Version" %>

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
    </style>

    <script language="javascript" type="text/javascript">
        function OpenNewWin(url) {
            //  alert(url);
            var x = window.open(url, 'MynewWin', 'width=1000, height=1000,toolbar=2');
            x.focus();
        }
    </script>

    <div style="float: left;"><br />
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <legend>
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="addnewpanel" runat="server" Text=" Server Status Add / Manage " 
                CssClass="btnSubmit" onclick="addnewpanel_Click" />
        </div>
      
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
            <table width="100%">               
               
                <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Server Status: "></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLeaseType"  ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtLeaseType" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:TextBox ID="txtLeaseType" runat="server" Width="190px" MaxLength="50" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z/.0-9_\s]+$/,'div1',50)"></asp:TextBox>
                        </label>
                        <label>
                            <asp:Label ID="max1" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div1" cssclass="labelcount">50</span>
                            <asp:Label ID="Label25" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                    </td>
                </tr>       
                                                    
                  <tr>
                    <td style="width: 20%">
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Active: "></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <label>
                            <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                        </label>
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
            <asp:Label ID="Label30" runat="server" Text="List of Server Status"></asp:Label>
        </legend>
        <table width="100%">
           
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version" Visible="false" 
                        OnClick="Button1_Click1" />
                    <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Active: "></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="181px"
                            OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem  Text="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
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
                                
	
	
	
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
                                        OnRowCommand="GridView1_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                      OnRowDeleting="GridView1_RowDeleting"   AlternatingRowStyle-CssClass="alt" AllowSorting="True" Width="100%" OnSorting="GridView1_Sorting"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                           
                                           <asp:BoundField DataField="id" HeaderStyle-HorizontalAlign="Left" HeaderText="ID" SortExpression="id" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Server Status Name" SortExpression="Name" ItemStyle-Width="94%" />
                                            
                                                 <asp:BoundField DataField="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left" HeaderText="Active" ItemStyle-Width="4%">
                                                <ItemStyle Width="4%" />
                                            </asp:BoundField>
                                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/Account/images/edit.gif" Text="Button" HeaderImageUrl="~/Account/images/edit.gif" />
                                              
                                               <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" CommandName="delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" CommandArgument='<%# Eval("id") %>' ToolTip="Delete"></asp:ImageButton>
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
