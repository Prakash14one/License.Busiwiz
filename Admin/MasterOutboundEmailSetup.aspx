<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="MasterOutboundEmailSetup.aspx.cs" Inherits="WebMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>--%>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllmsg" runat="server" Text="MasterOutBound EmailSetup"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <%--<asp:Button ID="addnewpanel" runat="server" Text="Add Employee" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" />--%>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Outbound Email Server"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" ControlToValidate="txtoutboundemailserver"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtoutboundemailserver" runat="server" MaxLength="30" Enabled="false"
                                            Width="180px"></asp:TextBox>
                                    </label>
                              
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Email Id for Outbound email Id"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtemailid"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                   
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtemailid" runat="server" MaxLength="30" Enabled ="false"
                                           Width="180px"></asp:TextBox>
                                    </label>
                                   
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 32%">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Password for Accessing Outbound Email-Id"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpassword"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                     
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtpassword" runat="server" Enabled="false"
                                            MaxLength="20" Width="180px"></asp:TextBox>
                                    </label>
                                    <%--<label>
                                        <asp:Label ID="Label36" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span2" cssclass="labelcount">20</span>
                                   <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . @ + -)"></asp:Label>
                                    </label>--%>
                                </td>
                            </tr>
                    
                   
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label20" runat="server" Text="Title of Email Account for Display"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txttitleofemailaccount" MaxLength="50" Enabled="false"
                                            runat="server" Width="180px"></asp:TextBox>
                                    </label>
                                 
                                </td>
                            </tr>


                         
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="btnsetup" runat="server" CssClass="btnSubmit" Text="Setup" Visible="false"
                                        ValidationGroup="1" onclick="btnsetup_Click" />
                                    <asp:Button ID="btnedit" runat="server" CssClass="btnSubmit" Text="Edit"  
                                        ValidationGroup="1" onclick="btnedit_Click" />
                                  
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                </div>
      <%--  </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
