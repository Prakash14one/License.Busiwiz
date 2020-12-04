<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="iofficesetupwizard.aspx.cs" Inherits="Allsetupwizard"
    Title="Setup Wizard" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="panelioffice" runat="server" Width="100%">
                    <fieldset>
                        <table width="100%" cellspacing="0" cellpadding="2">
                            <tr>
                                <td style="width: 85%">
                                    <asp:Label ID="Label75" runat="server" Font-Bold="true" Font-Size="X-Large" Text="iOffice"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="mypanel1" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label76" runat="server" Font-Bold="true" Text="Would you like to go through the iOffice Setup Wizard?"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel132" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel133" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image44" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel134" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image45" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel135" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton40" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label77" runat="server" Font-Bold="true" Text="Part A"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label78" runat="server" Font-Bold="true" Text="Goal Center Setup"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label110" runat="server" Font-Bold="true" Text="Step 1"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label104" runat="server" Font-Bold="true" Text="Missions" Font-Size="16px"
                                                            ForeColor="Black"></asp:Label>
                                                        are the largest goal you can create for your business. Use Missions to establish
                                                        the overall vision of the business.
                                                        <br />
                                                        For example: To be the market leader in your market sector within the city of New
                                                        York.
                                                        <br />
                                                        <label>
                                                            Would you like to add Missions to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton44" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton44_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label111" runat="server" Font-Bold="true" Text="Step 2"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label105" runat="server" Font-Bold="true" Text="Long Term Goals" Font-Size="16px"
                                                            ForeColor="Black"></asp:Label>
                                                        are encorporated into Missions and are used to break them down the large Mission
                                                        set out in to goals that<br />
                                                        can be accomplished in the long term. You must have a Mission already created in
                                                        order to add a Long Term Goal.
                                                        <br />
                                                        For Example: Have a market share of 25% within 8 years.
                                                        <br />
                                                        <label>
                                                            Would you like to add a Long Term Goal for your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton1_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label114" runat="server" Font-Bold="true" Text="Step 3"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label106" runat="server" Font-Bold="true" Text="Short Term Goals"
                                                            Font-Size="16px" ForeColor="Black"></asp:Label>
                                                        are created in order to accomplish the larger Long Term Goals. You must have created
                                                        a Long Term Goal<br />
                                                        in order to add any Short Term Goals.
                                                        <br />
                                                        For example: Marketing department must grow our customer base by 7-10% within the
                                                        first 3 years of business.
                                                        <br />
                                                        <label>
                                                            Would you like to add any Short Term Goals?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton2_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label115" runat="server" Font-Bold="true" Text="Step 4"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label107" runat="server" Font-Bold="true" Text="Yealy Goals" Font-Size="16px"
                                                            ForeColor="Black"></asp:Label>
                                                        are created to help complete your Short Term Goals and are to be completed within
                                                        a year. You must have a<br />
                                                        Short Term Goal created in order to add any Yearly Goals.
                                                        <br />
                                                        For example: Marketing department is to create a marketing campaign to promote sales
                                                        by 20% in 2013.
                                                        <br />
                                                        <label>
                                                            Would you like to add a Yearly Goal to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton3_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label116" runat="server" Font-Bold="true" Text="Step 5"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label108" runat="server" Font-Bold="true" Text="Monthly Goals" Font-Size="16px"
                                                            ForeColor="Black"></asp:Label>
                                                        are created to help business stay on track in order to finish their yearly goals.
                                                        You must have created  a <br /> yearly goal  in order to add Monthly Goals.
                                                        <br />
                                                        For Example: Complete a marketing campaign complete with research, cost anaysis
                                                        and presentation.
                                                        <br />
                                                        <label>
                                                            Would you like to add a Monthly Goal to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton4_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            
                                         
                                            
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label117" runat="server" Font-Bold="true" Text="Step 6"></asp:Label>
                                                    </label>
                                                    <label>
                                                        <asp:Label ID="Label109" runat="server" Font-Bold="true" Text="Weekly Goals" Font-Size="16px"
                                                            ForeColor="Black"></asp:Label>
                                                        <b></b>break your Monthly Goals into managable targets. You must have a Monthly
                                                        Goal created in order to add <br /> a   Weekly Goal.
                                                        <br />
                                                        For example: Marketing department is to complete market research and analysis.
                                                        <br />
                                                        <label>
                                                            Would you like to add a Weekly Goal to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox6_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton5_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel136" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel137" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image46" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel138" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image47" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel139" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton41" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label79" runat="server" Font-Bold="true" Text="Part B"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label80" runat="server" Font-Bold="true" Text="Strategy and Tactic Setup"></asp:Label>
                                                </td>
                                            </tr>
                                      
                                                 <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        Businesses can add different Strategies and Tactics to help them accomplish the
                                                        goals that have been laid out in the Goal Center.<br />
                                                        Strategies and Tactics can be linked to specific goals so as to keep organizations
                                                        organizaed in accomplishing the goals required to be successful.
                                                    </label><br />
                                                    <label>
                                                        <label>
                                                            Would you like to add a Strategy to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox7" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox7_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton6_Click" Visible="false"> Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Would you like to add a Tactic to your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox8" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox8_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton8_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel140" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel141" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image48" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel142" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image49" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel143" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton42" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label81" runat="server" Font-Bold="true" Text="Part C"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label82" runat="server" Font-Bold="true" Text="Project and Task Setup"></asp:Label>
                                                </td>
                                            </tr>
                                      
                                         
                                                   <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        Weekly Goals can be broken into smaller Projects and/or Tasks. Projects and Tasks
                                                        can be assigned to individual employee&#39;s<br />
                                                        and budgeted for. Budgeted and actual costs are then calculated for the goals that
                                                        they are linked to.
                                                    </label><br />
                                                    <label>
                                                        <label>
                                                            Would you like to add a Project for your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox9" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox9_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton9_Click" Visible="false"> Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Would you like to add a Task for your business?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox10" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox10_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton10_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                         
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel144" runat="server" Width="100%">
                                        <table>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:Panel ID="Panel145" runat="server" Width="100%" Visible="false">
                                                        <asp:Image ID="Image50" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel146" Visible="false" runat="server" Width="100%">
                                                        <asp:Image ID="Image51" runat="server" Width="40px" Height="25px" ImageUrl="~/images/GreenMark.jpg" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel147" Visible="false" runat="server" Width="100%">
                                                        <asp:LinkButton ID="LinkButton43" runat="server">Try Again</asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="Label83" runat="server" Font-Bold="true" Text="Part D"></asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label ID="Label84" runat="server" Font-Bold="true" Text="Policy, Procedure and Rule Setup"></asp:Label>
                                                </td>
                                            </tr>
                                                                                
                                              <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        In iOffice you can create an online policy handbook for your employees. First create
                                                        a policy category, then start to add different<br />
                                                        policy, procedures and rules to your business.
                                                    </label><br />
                                                    <label>
                                                        <label>
                                                            Would you like to setup a Policy?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox11" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox11_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton11_Click" Visible="false"> Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Would you like to setup a Procedure?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox12" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox12_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton12_Click" Visible="false"> Yes</asp:LinkButton>
                                                        <br />
                                                        <label>
                                                            Would you like to setup a Rule?
                                                        </label>
                                                        <asp:CheckBox ID="CheckBox13" runat="server" Text="Yes" AutoPostBack="True" OnCheckedChanged="CheckBox13_CheckedChanged" />
                                                        <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="Black" Font-Bold="true"
                                                            OnClick="LinkButton13_Click" Visible="false"> Yes</asp:LinkButton>
                                                    </label>
                                                </td>
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
