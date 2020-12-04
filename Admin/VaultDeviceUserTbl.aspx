<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="VaultDeviceUserTbl.aspx.cs" Inherits="Admin_VaultDeviceUserTbl" Title="VaultDeviceUserTbl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 625px">
        <tr>
            <td class="hdng">
                Add/Manage VaultDevice User<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" style="width: 642px">
                  <tbody>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                ClientId:
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddlclientid" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlclientid" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                               ProductNameVersionId :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddlpv" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpv" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                         <tr>
                            <td style="height: 16px; width: 166px;">
                                companyId :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddlcompanyid" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcompanyid" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                          <tr>
                            <td style="height: 16px; width: 166px;">
                                VaultDevce Sr Number :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddlsrno" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlsrno" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                UserId :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtuserid" runat="server" Width="163px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtuserid" ErrorMessage="*"></asp:RequiredFieldValidator>
<asp:Button id="btnCheckCompany" onclick="btnCheckCompany_Click" runat="server" Width="111px" 
                            Text="Conform Change" Height="18px"></asp:Button>
                                <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblCompanyIDAVl" runat="server" ForeColor="#C00000" 
                            Font-Bold="False"></asp:Label>
                            </td>
                          
                        </tr>
                          <tr>
                            <td style="height: 16px; width: 166px;">
                                Vault Devicemaster Name :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddldvm" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldvm" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                       <tr>
                            <td class="column1" style="width: 166px">
                            </td>
                            <td class="column2" align="center" style="width: 248px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnadd" runat="server" Text="Submit" onclick="btnadd_Click" />
                                    
                            </td>
                            <td class="column2">
                            </td>
                            <td class="column2">
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
                            <asp:BoundField DataField="clientid" HeaderText="ClientId" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="ProductVersionID" HeaderText="Product VersionID" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="companyid" HeaderText="CompanyId" ItemStyle-Width="60px" />
                              <asp:BoundField DataField="SrNumber" HeaderText="SrNumber" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="userid" HeaderText="Userid" ItemStyle-Width="130px" />
                               <asp:BoundField DataField="Name" HeaderText="Vaultdevice Manstername" />
                          
                          
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

