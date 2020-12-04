<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AccountingSetupWizard.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="panelaccounting" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%" colspan="2">
                                    <asp:Label ID="Label27" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Accounting"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="Label28" runat="server" Font-Bold="true" Text="Would you like to go through the Accounting Setup Wizard?"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel50" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel51" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image14" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel52" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image15" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel53" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton20" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label30" runat="server" Font-Bold="true" Text="First Year Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%--<td style="width: 5%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        You are to setup the regular start date of your business as well as the first year
                                                        of accounting for your business. The system will not let you make any entry before
                                                        the start date of the first accounting year of your business. For example: your
                                                        current accounting year is from 1st April 2013 to 31 March 2014, however you started
                                                        your business on May 5th 2009. You can set 1st April 2009-31st March 2010 as your
                                                        first accounting year. The system will allow you to make all the entries for 2009-2010,
                                                        2011-2012, and the current year 2013-2014. Please note; that if you select account
                                                        year 2009-2010 as your first accounting year, and your current accounting year is
                                                        2013-2014 you will not be able to enter opening balances in the year 2013-2014.
                                                        You will have to make entry for all the accounting years right from your first accounting
                                                        year to the current year, so your account balances are carried forward to the current
                                                        accounting year.
                                                        <br />
                                                        Although your first accounting year was 2009-2010 and you already have done the
                                                        accounting of all the years up to year 2012-2013 in some other accounting software,
                                                        you can select 2013-2014 (current accounting year) as your first accounting year
                                                        and the system will allow you to import all the closing balances of year 2012-2013
                                                        as opening balances for the year 2013-2014.
                                                        <br />
                                                        <label>
                                                            Would you like to set up your first accounting year now? (Recommended)
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton1_Click" Visible="false">  Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%-- <td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel54" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel55" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image16" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel56" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel57" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton21" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label31" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label32" runat="server" Font-Bold="true" Text="Account Year Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%--<td style="width: 5%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        The current accounting year of your business is
                                                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Black"></asp:Label>
                                                        <br />
                                                        <label>
                                                            Do you want to change your current accounting year?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton2_Click" Visible="false">  Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel58" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel59" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image17" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel60" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel61" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton22" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label33" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="Initial Balance Setup"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%-- <td style="width: 5%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        If you are an existing business that currently holds account balances you can set
                                                        up the opening balances here. From this page you can bring all account balances
                                                        from other software and feed it into this page. This can only be done for the first
                                                        year of your business for your starting balance. Here you can also import all of
                                                        your opening balances in an Excel file by importing your Excel file to the system.
                                                        <br />
                                                        <label>
                                                            Would you like to set up your initial balances?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton4_Click" Visible="false">  Yes</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel62" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel63" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image18" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel64" Visible="false" runat="server" Width="100%">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel65" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton23" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="Part D"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label36" runat="server" Font-Bold="true" Text="Account Master "></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                </td>
                                                <%--<td style="width: 5%">
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        Click
                                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton3_Click">here </asp:LinkButton>
                                                        to view the accounts already set up for the business.
                                                        <br />
                                                        There are set account groups according to IFRS.
                                                        <br />
                                                        <label>
                                                            would you like to change the names of any groups or classes?<br /> (Recommended Only for
                                                            Advanced Users)
                                                          </label>
                                                           <label>
                                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="CheckBox4_CheckedChanged" />
                                                        
                                                       </label>
                                                </td>
                                                <td>
                                                </td>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
