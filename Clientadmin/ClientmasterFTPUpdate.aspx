<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ClientmasterFTPUpdate.aspx.cs" Inherits="MainMenuMaster" Title="Product Main Menu-Add,Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
       

     
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
            <div class="products_box">
                <div style="margin-left:1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                      
                        <asp:Button ID="addnewpanel" Visible="false" runat="server" Text="Add Main Menu" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                     
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="true">
                        <table width="100%">
                          

                          
                               
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="FTP Server Name"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_serverftp"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                     
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        
                                            <asp:TextBox ID="txt_serverftp" runat="server" Width="270px" ></asp:TextBox>
                                            
                                    </label>
                                    <label>
                                        

                                    </label>
                                </td>
                            </tr>
  <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="FTP Port"></asp:Label>                                    
                                     
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        
                                            <asp:TextBox ID="txtFtpPort" runat="server" Width="70px" ></asp:TextBox>
                                    </label>
                                    <label>
                                        

                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="User Id"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtftpuserid"
                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtftpuserid" runat="server" Width="270px" ></asp:TextBox>
                                       
                                    </label>
                                    <label>
                                       
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                      <asp:Label ID="Label1" runat="server" Text="Password"></asp:Label>                                        
                                      
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txt_password" runat="server" Width="270px" ></asp:TextBox>                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                <asp:Button ID="Button2" ValidationGroup="1" runat="server" Text="Test Ftp Server Connection"
                                        OnClick="Button2_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Buttonupdate" runat="server" Text="Update" ValidationGroup="1" Visible="true"
                                        CssClass="btnSubmit" OnClick="Buttonupdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
