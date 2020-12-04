<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="VaultDeviceDetailTbl.aspx.cs" Inherits="Admin_VaultDeviceDetailTbl" Title="VaultDeviceDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 625px">
        <tr>
            <td class="hdng">
                Add/Manage VaultDeviceDetail<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" style="width: 642px">
                  <tbody>
                        <%--<tr>
                            <td style="height: 16px; width: 166px;">
                                Client Name:
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtclientid" runat="server" Width="163px"></asp:TextBox>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtclientid" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>--%>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                VaultDevice MasterId :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddlvdm" runat="server"  Width="222px"></asp:DropDownList>
                                  <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlvdm" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                         <tr>
                            <td style="height: 16px; width: 166px;">
                                Used :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtused" runat="server" Width="163px"></asp:TextBox>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtused" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        
                       <tr>
                            <td class="column1" style="width: 166px">
                            </td>
                            <td class="column2" align="center" >
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnadd" runat="server" Text="Submit" onclick="btnadd_Click" />
                                    
                                <asp:Button ID="btntxt" runat="server" Text="TextCreate" onclick="btntxt_Click" 
                                    CausesValidation="False"/>
                                    
                                &nbsp;&nbsp;
                                    
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
                                    Font-Names="Action Is,Diagonal JL" Font-Size="Medium" NavigateUrl= "~/Admin/TextFile/0.txt" 
                                  Target="_self">Download</asp:HyperLink>
                                    <asp:Button ID="download" runat="server" Text="Download" 
                                    onclick="download_Click" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblmsg" runat="server" Width="297px" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                           
                        </tr>
                    </tbody>
                </table>      </td>
                        </tr>
                    
                </table>
         
  
    <table id="Table2" cellpadding="0" cellspacing="0">
        
        <tr>
            <td class="column2" colspan="4">
                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridBack" DataKeyNames="Id" EmptyDataText="There is no data."
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        PageSize="5">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ItemStyle-Width="50px"  CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                            <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="70px" />
                             <asp:BoundField DataField="Vaultdevicemasterid" HeaderText="Vaultdevice Masterid" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="number" HeaderText="Number" ItemStyle-Width="150px" />
                            <asp:BoundField DataField="Used" HeaderText="Used" ItemStyle-Width="200px" />
                         </Columns>
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <RowStyle CssClass="GridRowStyle" />
                        <FooterStyle CssClass="GridFooter" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
       
    </table>
    
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div style="border-right: #946702 1px solid; border-top: #946702 1px solid; left: 45%;
                visibility: visible; vertical-align: middle; border-left: #946702 1px solid;
                width: 196px; border-bottom: #946702 1px solid; position: absolute; top: 65%;
                height: 51px; background-color: #ffe29f" id="IMGDIV" align="center" runat="server"
                valign="middle">
                <table width="645px">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/images/preloader.gif"></asp:Image>
                            </td>
                            <td>
                                <asp:Label ID="lblprogress" runat="server" ForeColor="#946702" Text="Please Wait"
                                    Font-Bold="True" Font-Size="16px" Font-Names="Arial"></asp:Label><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
