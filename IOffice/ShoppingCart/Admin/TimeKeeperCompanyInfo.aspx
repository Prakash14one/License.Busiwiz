<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="TimeKeeperCompanyInfo.aspx.cs" Inherits="ShoppingCart_Admin_TimeKeeperCompanyInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlpvt" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:UpdatePanel ID="update" runat="server">
                    <ContentTemplate>
                        <div style="clear: both;">
                        </div>
                        <fieldset>
                            <%-- <legend>
                    <asp:Label ID="Label13" runat="server" Text="1. Company Information"></asp:Label></legend>--%>
                            <div style="float: right;">
                                <asp:Button ID="btnmanage" runat="server" Text="Manage" CssClass="btnSubmit" OnClick="btnmanage_Click" />
                            </div>
                            <asp:Panel ID="Panel2" runat="server" Visible="False" Width="100%">
                                <label>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>Are you Setup New Company?</asp:ListItem>
                                        <asp:ListItem>Existing Company</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                                <div class="cleaner">
                                </div>
                                <label>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" Height="25px" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                        Visible="False">
                                        <asp:ListItem Value="0">Only Product</asp:ListItem>
                                        <asp:ListItem Value="1">Only Service</asp:ListItem>
                                        <asp:ListItem Value="2">Both</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                                <div class="cleaner">
                                </div>
                                <label>
                                    <asp:Label ID="lblboth" runat="server" Text="What you wish to show your home page for shoppingcart "
                                        Visible="False"></asp:Label>
                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                                        Visible="False">
                                        <asp:ListItem Value="1">Products</asp:ListItem>
                                        <asp:ListItem Value="2">Service</asp:ListItem>
                                    </asp:RadioButtonList>
                                </label>
                            </asp:Panel>
                            <div class="cleaner">
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label1" runat="server" Text="Company Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="txtcompanyname" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label2" Text="PATEL LTD"></asp:Label>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label4" runat="server" Text="Address 1"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label ID="txtaddress1" CssClass="lblSuggestion" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label9" Text="INDIA"></asp:Label>--%>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label2" runat="server" Text="Contact Person Name"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="txtcontactpersonname" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label3" Text="MEHUL"></asp:Label>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label5" runat="server" Text="Address 2"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="txtAddress2" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label10" Text="GUJARAT"></asp:Label>--%>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <%--<asp:Label ID="Label3" runat="server" Text="Contact Person Designation"></asp:Label>--%>
                                            <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="lblemail" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" ID="txtcontactpersondesi" runat="server"></asp:Label>--%>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label4" Text="IT"></asp:Label>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label7" runat="server" Text="Country"></asp:Label>, <asp:Label ID="Label8"
                                                runat="server" Text="State"></asp:Label>, <asp:Label ID="Label9" runat="server" Text="City"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label ID="lblcountry" runat="server" ForeColor="#457cec"></asp:Label>, <asp:Label
                                                ID="lblstate" runat="server" ForeColor="#457cec"></asp:Label>, <asp:Label ID="lblcity"
                                                    runat="server" ForeColor="#457cec"></asp:Label>
                                        </label>
                                        <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label13" Text="DWARKA"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label12" runat="server" Text="Phone Number"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="lblphone" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label6" Text="Ahmedabad"></asp:Label>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label11" runat="server" Text="ZIP/Postal Code"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="lblpincode" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label14" Text="5193307344"></asp:Label>--%>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                            <asp:Label ID="Label10" runat="server" Text="Fax Number"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <asp:Label CssClass="lblSuggestion" ID="lblfax" runat="server"></asp:Label>
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label7" Text="Ahmedabad"></asp:Label>--%>
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information">
                                        </label>
                                    </td>
                                    <td>
                                        <label class="cssLabelCompany_Information_Ans">
                                            <%--<asp:Label CssClass="lblSuggestion" runat="server" ID="Label15" Text="38000"></asp:Label>--%>
                                        </label>
                                    </td>
                                </tr>
                               
                            </table>
                            <div class="cleaner">
                            </div>
                        </fieldset>
                        <div style="clear: both;">
                        </div>
                        <%-- <fieldset>
                <legend>
                    <asp:Label ID="Label19" runat="server" Text="2. Please change your company logo if required"></asp:Label></legend>
                <label>
                    <asp:Label ID="lblLogo" runat="server" Text="Company Logo "></asp:Label>
                </label>
                <asp:FileUpload ID="FileUpload1"  runat="server" Visible="False" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="FileUpload1"
                                ErrorMessage="*" ValidationGroup="7412" Visible="False">
                                </asp:RequiredFieldValidator>
                       <asp:Label ID="Label23" runat="server" Text="Logo Size : 176 X 106" Visible="False"></asp:Label>
                       
                        <asp:Button ID="imgBtnImageUpdate" Text="Update Image" runat="server"  CssClass="btnSubmit"
                                    OnClick="imgBtnImageUpdate_Click" Visible="False" /> 
                                    
                     <asp:Button ID="ImgBtncancel" Text="Cancel" runat="server" OnClick="ImgBtncancel_Click"  CssClass="btnSubmit"
                                            Visible="False" />    
                    
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnllogo" runat="server" Width="180px">
                        <asp:Button ID="btnChange" Text="Change" runat="server" CssClass="btnSubmit" OnClick="btnChange_Click" />
                        <br />
                        <asp:Image ID="imgLogo" runat="server" Height="106px" Width="176px" />
                    </asp:Panel>
                
            </fieldset>--%>
                        <asp:Panel ID="pnconfor" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA"
                            BorderStyle="Outset" Width="300px">
                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center">
                                        Confirmation....
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblddffg" runat="server" Text="Notice : Please note that you will not be able to change this information in future.You will be able to feed Opening balances of all accounts for this accounting year only."
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnapd" runat="server" CssClass="btnSubmit" Text="Confirm" OnClick="btnapd_Click" />
                                        <asp:Button ID="btns" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btns_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                            ID="ModalPopupExtender122" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="btns" PopupControlID="pnconfor" TargetControlID="HiddenButton222">
                        </cc1:ModalPopupExtender>
                        <div style="clear: both;">
                        </div>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:PostBackTrigger ControlID="imgBtnImageUpdate" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnChange" />
            <asp:PostBackTrigger ControlID="imgBtnImageUpdate" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
