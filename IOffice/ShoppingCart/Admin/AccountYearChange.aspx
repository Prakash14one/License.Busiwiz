<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AccountYearChange.aspx.cs" Inherits="Add_Account_Year" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
    </script>

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <label>
                        <asp:Label ID="lblmsg" Text="" ForeColor="Red" runat="server"></asp:Label>
                    </label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <label>
                        <asp:Label ID="lblacchead" Text="Select your current accounting year (Year for which you want to make accounting entries)"
                            runat="server"></asp:Label>
                    </label>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblAccountingYear" Text="List of Accounting Years" runat="server"
                            Font-Bold="true"></asp:Label></legend>
                    <label>
                        <asp:Label ID="lblBusinessName" runat="server" Text="Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </label>
                    <div style="float: right;">
                        <asp:Button ID="btnaddnewaccount0" runat="server" OnClick="btnaddnewaccount_Click"
                            CssClass="btnSubmit" Text="Add New Accounting Year" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Printable Version"
                            CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" class="btnSubmit" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" Visible="false"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label8" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusinessN" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label11" runat="server" Font-Italic="True" Text="List of Accounting Years"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr style="vertical-align: top; height: 100%">
                                <td>
                                    <asp:GridView ID="grd" runat="server" DataKeyNames="Report_Period_Id" Width="100%"
                                        OnSorting="grid_Sorting" AllowSorting="True" AutoGenerateColumns="False" GridLines="Both"
                                        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Wname" Visible="true"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="22%">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblreportmasterid" Visible="false" runat="server" Text='<%# Eval("Report_Period_Id")%>'></asp:Label>
                                                 
                                                    <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Wname")%>'></asp:Label>
                                                    <asp:Label ID="lblWhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date of Accounting Year" SortExpression="StartDate"
                                                Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="22%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsdate" runat="server" Text='<%# Eval("StartDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date of Accounting Year" SortExpression="EndDate"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="22%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbledate" runat="server" Text='<%# Eval("EndDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select Current Accounting Year<br/>You can only select the previous, or the next record" 
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="34%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkactive" runat="server" Checked='<%# Eval("Active")%>' AutoPostBack="true"
                                                        OnCheckedChanged="chkactive_chachedChanged" ToolTip="You can only select the previous or the next record of the existing current accounting year.">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnconfor" runat="server" CssClass="modalPopup" Width="60%">
                                    <div>
                                        <fieldset style="border: 1px solid white;">
                                            <legend style="color: Black">Confirmation </legend>
                                            <div>
                                                <div style="float: right;">
                                                    <label>
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Label ID="lblddffg" runat="server" Text="Are you sure you wish to change the current accounting year? "></asp:Label>
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text=" This will recalculate all balances of all accounts of the previous year and they will carry forward as an opening balance of accounts for the newly selected current accounting year. "></asp:Label>
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text="This transfer may take a while, depending on the amount of data. "></asp:Label>
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnapd" runat="server" CssClass="btnSubmit" Text="Confirm" OnClick="btnapd_Click" />
                                                    <asp:Button ID="btns" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btns_Click" />
                                                </div>
                                            </div>
                                </asp:Panel>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender122" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnconfor" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="400px">
                                    <div>
                                        <fieldset style="border: 1px solid white;">
                                            <legend style="color: Black"> </legend>
                                            <div>
                                                <div style="float: right;">
                                                    <label>
                                                    </label>
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="You should only be able to select the  previous, or the next accounting year. You should not be able to jump over current accounting year. "></asp:Label>
                                                    </label>
                                                </div>
                                               
                                                <div style="clear: both;">
                                                </div>
                                                <div>
                                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Cancel" 
                                                        onclick="Button3_Click" />
                                                </div>
                                            </div>
                                </asp:Panel>
                                
                                   <asp:Button ID="Button2" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="Button2">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlbusiness" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnapd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnaddnewaccount0" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grd" EventName="Sorting" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
