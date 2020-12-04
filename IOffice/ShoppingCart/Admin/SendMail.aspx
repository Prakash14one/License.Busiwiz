<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="SendMail.aspx.cs" Inherits="ShoppingCart_SendMail" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

     function SelectAllCheckboxes(spanChk) {

         // Added as ASPX uses SPAN for checkbox
         var oItem = spanChk.children;
         var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
         xState = theBox.checked;
         elm = theBox.form.elements;

         for (i = 0; i < elm.length; i++)
             if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
             //elm[i].click();
             if (elm[i].checked != xState)
                 elm[i].click();
             //elm[i].checked=xState;
         }
     }
    </script>
  <asp:UpdatePanel ID="uppanel" runat="server">
  <ContentTemplate>
    <div class="products_box">
        <div style="padding-left:1%">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="Label1" runat="server" Text="Send Email to Customer"></asp:Label>
            </legend>
             <table id="Table1"  cellpadding="0" style="width:100%" cellspacing="0">
                <tr>
                    <td width="20%">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text=" Business Name"></asp:Label>
                        </label>
                       
                    </td>
                    <td>
                        <label>
                             <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" Width="150px">
                            </asp:DropDownList>
                        </label>                            
                         
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Phone No"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                        ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"  
                                        ControlToValidate="TextBox1" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </label>
                        
                    </td>
                    <td>
                        <label>
                        <asp:TextBox ID="TextBox1" runat="server" Width="145px" MaxLength="15"></asp:TextBox>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label4" runat="server" Text=" Party Type"></asp:Label>
                        </label>                       
                    </td>
                    <td>       
                        <label>
                             <asp:DropDownList ID="ddlPartyType" runat="server" Width="150px">
                        </asp:DropDownList>
                        </label>                     
                       
                    </td>
                    <td>
                         <label>
                            <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
                        </label> 
                        
                    </td>
                    <td>
                        <label>
                             <asp:DropDownList ID="ddlstatus" runat="server" Width="120px">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem>InActive</asp:ListItem>
                        </asp:DropDownList>
                        </label>                       
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" >
                       
                         <asp:Button ID="ImageButton3" runat="server" Text=" Go " CssClass="btnSubmit" onclick="ImageButton3_Click" />
                        <asp:Button ID="imgbtnreset" runat="server" Text=" Reset " CssClass="btnSubmit" OnClick="imgbtnreset_Click" />
                        <asp:Button ID="btnsearchh" runat="server" Text="Go" CssClass="btnSubmit" Visible="False" />
                       
                    </td>                       
                </tr>
                 <tr>
                    <td colspan="4">
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="To"></asp:Label>
                        </label>                              
                    </td>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Both">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnRowCommand="GridView1_RowCommand" OnSorting="GridView1_Sorting"
                                    EmptyDataText="No Record Found." Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="UserID" HeaderText="userid" Visible="False" SortExpression="UserID"  />
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                    type="checkbox" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="EmailID" HeaderText="E-mail" SortExpression="EmailID" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="StateName" HeaderText="State" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="CountryName" HeaderText="Country" SortExpression="CountryName" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="PartType" HeaderText="Part Type" SortExpression="PartType" HeaderStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="4">
                        
                    </td>
                </tr>
                     <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="3">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Width="100%">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                    RepeatDirection="Horizontal" Width="50%">
                                    <asp:ListItem Selected="True" Value="0">Standard Mail Format</asp:ListItem>
                                    <asp:ListItem Value="1">Custom Mail Format</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="100%">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="20%">
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text=" Email Type"></asp:Label>
                                            </label>                                             
                                        </td>
                                        <td colspan="3">
                                            <label>
                                            <asp:DropDownList ID="ddlEmailType" runat="server" OnSelectedIndexChanged="ddlEmailType_SelectedIndexChanged"
                                                Width="145px" AutoPostBack="True">                                                
                                            </asp:DropDownList>
                                            </label>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Subject"></asp:Label>
                                <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REG1" runat="server" 
                                        ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  
                                        ControlToValidate="txtSubject" ValidationGroup="2"></asp:RegularExpressionValidator>

                            </label>                           
                        </td>
                        <td colspan="3">
                            <label>
                                 <asp:TextBox ID="txtSubject" runat="server" MaxLength="50" Width="145px"></asp:TextBox>
                            </label>                                                      
                        </td>
                    </tr>
                     <tr valign="top">
                        <td>
                            <label>
                                <asp:Label ID="Label9" runat="server" Text="Attachment"></asp:Label>
                            </label>                             
                        </td>
                        <td colspan="3" valign="top">
                            <label>
                           <asp:Label ID="lblfilename" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:Button ID="ImageButton2" runat="server" CssClass="btnSubmit" ValidationGroup="1" Text=" Add "  OnClick="btnUpload_Click" />
                            
                            </label>
                        </td>
                    </tr>
                     <tr>
                        <td valign="top">
                            <label>
                                <asp:Label ID="Label10" runat="server" Text="Message"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ErrorMessage="*" Display="Dynamic"
                                        SetFocusOnError="True" ValidationExpression="^([,._a-zA-Z0-9\s]*)"  
                                        ControlToValidate="txtBody" ValidationGroup="2"></asp:RegularExpressionValidator>
                            </label>                                 
                        </td>
                        <td colspan="3">
                            <label>
                            <asp:TextBox ID="txtBody" runat="server" MaxLength="200" Height="200px" Width="500px"></asp:TextBox>
                           </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4" align="center">
                            <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                            <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                            <asp:Button ID="ImageButton1" runat="server" OnClick="Button1_Click"  
                                ValidationGroup="2" Text=" Send "  CssClass="btnSubmit" Width="168px" />   
                            <asp:Button ID="imgbtncancel" runat="server" OnClick="imgbtncancel_Click" Text=" Cancel "  CssClass="btnSubmit" />   
                           
                        </td>                       
                    </tr>
             </table>
        </fieldset>
    </div>    
     </ContentTemplate>
     <Triggers>
     <asp:PostBackTrigger ControlID="ImageButton2" />
     </Triggers>
  </asp:UpdatePanel>
</asp:Content>
