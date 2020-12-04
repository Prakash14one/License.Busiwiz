<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AccountingYearMasterInfo.aspx.cs" Inherits="ShoppingCart_Admin_Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblyearcacc" Text="Select your first accounting year for your business"
                            runat="server"></asp:Label>
                    </legend>
                    <label>
                        <asp:HyperLink ID="lblopenaccy" Target="_blank" runat="server"></asp:HyperLink>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlaccy" runat="server">
                        <table width="100%" cellspacing="3">
                            <tr>
                                <td width="42%">
                                    <label>
                                        <asp:Label ID="lblf1" Text="Business Name" runat="server"></asp:Label>
                                    </label>
                                </td>
                                <td width="58%">
                                    <label>
                                        <asp:DropDownList ID="ddlbus" runat="server" OnSelectedIndexChanged="ddlbus_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="Panel1" Visible="true" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td width="42%">
                                                    <label>
                                                        <br />
                                                        <asp:Label ID="Label5" Text="First accounting year" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td width="58%">
                                                    <label>
                                                        <asp:Label ID="Label8" Text="Year" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlacyear" runat="server" Width="100px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlacyear_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <br />
                                                        <asp:Label ID="lble2" Text="Normal start date and month of your accounting year"
                                                            runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label4" Text="Month" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlacmonth" Width="50px" runat="server" OnSelectedIndexChanged="ddlacmonth_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label7" Text="Date" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlacday" Width="50px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlacday_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label2" Text="Please note that once this accounting year is selected you cannot go back into an earlier accounting year and make any accounting entries."
                                                            runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label6" Text="Your first account year is: " runat="server"></asp:Label>
                                        <asp:Label ID="lblfysdate" Text="" runat="server"></asp:Label>
                                        <asp:Label ID="Label3" Text="-" runat="server"></asp:Label>
                                        <asp:Label ID="lblfyedate" Text="" runat="server"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label9" Visible="false" Text="You are unable to change your first accounting year."
                                            runat="server"></asp:Label>
                                    </label>
                                    <div style="clear: both;">
                                    </div>
                                    <label>
                                        <asp:Label ID="Label10" Visible="false" Text="However, you can change your current accounting year from "
                                            runat="server"></asp:Label>
                                    </label>
                                 <label>  <a Id="AHREF" runat="server" target="_blank">here.</a></label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnconf" runat="server" CssClass="btnSubmit" Text="Confirm" OnClick="btnconf_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnconfor" runat="server" CssClass="modalPopup" BorderStyle="Outset"
                                        Width="450px">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label1" runat="server" Text="Confirmation:"></asp:Label>
                                            </legend>
                                            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblddffg" runat="server" Text="Please note that when you create a new company the accounting year is already set starting from April 1st and ending on March 31st. You can change those dates and press the 'Confirm' button in order to make them effective. Once confirmed, you cannot make changes to this start date and end date. Further, you can make changes (and confirm these changes) up until you change the current accounting year from the page Change Accounting Year."></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="lblddffg12" runat="server" Text="Please note, that once this accounting year is selected, you cannot go into an earlier accounting year and make any accounting entries."></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnapd" runat="server" CssClass="btnSubmit" Text="Confirm" OnClick="btnapd_Click" />
                                                        <asp:Button ID="btns" runat="server" CssClass="btnSubmit" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset></asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                                        ID="ModalPopupExtender122" runat="server" BackgroundCssClass="modalBackground"
                                        CancelControlID="btns" PopupControlID="pnconfor" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
