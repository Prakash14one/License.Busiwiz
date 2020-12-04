<%@ Page Language="C#" MasterPageFile="CandidateMain.master" AutoEventWireup="true"
    CodeFile="VacancyProfile.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

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
    <table width="100%">
        <tr>
            <td valign="top">
                <%--<asp:Image ID="Img1" runat="server" Width="245px" Height="106px" />--%>
                <asp:ImageButton ID="Img1" runat="server" Width="245px" Height="106px" />
            </td>
            <td align="right">
                <div id="headerInfo">
                    <%-- <asp:LinkButton ID="lblcompanyname" runat="server" Font-Size="18px" ForeColor="#416273"
                        Font-Bold="true" OnClick="lblcompanyname_Click"></asp:LinkButton>--%>
                    <asp:Label runat="server" ID="lblcompanyname" Font-Size="18px"></asp:Label>
                    <div style="clear: both;">
                    </div>
                    <asp:Label runat="server" ID="lbladdress1" Font-Size="14px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lblphoneno" Font-Size="14px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lblwebsite" Font-Size="12px" Font-Bold="false" Visible="false"></asp:Label>
                    <div style="clear: both;">
                    </div>
                    <asp:Label runat="server" ID="lblcs" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="lbltollfreeno" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Label runat="server" ID="Label3" Font-Size="12px" Font-Bold="false"></asp:Label>
                    <div style="clear: both; height: 0px">
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="View other vacancies" CssClass="btnSubmit"
                        OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Business Website" CssClass="btnSubmit"
                        OnClick="Button2_Click" Visible="false" />
                </div>
            </td>
            <%-- <td>
                <div id="headerInfo">
                  
                    <div style="clear: both;">
                    </div>
                </div>
                <div id="site_title" style="vertical-align: top">
                    <h1>
                        
                    </h1>
                </div>
            </td>--%>
        </tr>
    </table>
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="Label2" runat="server" Text="Vacancy Profile"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="lblwname" runat="server" Text="Business Name">
                            </asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label class="cssLabelCompany_Information_Ans">
                            <asp:Label ID="lblbusiness" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label39" runat="server" Text="Type"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text="Term"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label class="cssLabelCompany_Information_Ans">
                            <asp:Label ID="lblvactype" runat="server" Text=""></asp:Label>
                            &nbsp;<asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                            &nbsp;<asp:Label ID="lblduration" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label40" runat="server" Text="Position Title"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label class="cssLabelCompany_Information_Ans">
                            <asp:Label ID="lblvactitle" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblcity" runat="server" Text=""></asp:Label>,
                            <asp:Label ID="lblstate" runat="server" Text=""></asp:Label>,
                            <asp:Label ID="lblcountry" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Job Function"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lbljobfun" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Qualification Requirements"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblqualifi" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label15" runat="server" Text="Salary"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblsal1" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblsal2" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label38" runat="server" Text="per"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblsal3" runat="server" ForeColor="Black"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Labedsfl14" runat="server" Text="Other Terms and Conditions"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblTandC" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                <%--    <tr>
                <td style="width: 30%">
                    <label>
                        <asp:Label ID="Label20" runat="server" Text="Number of Hours"></asp:Label>
                    </label>
                </td>
                <td style="width: 70%">
                    <label>
                        <asp:Label ID="lblhour" runat="server" Text=""></asp:Label>
                    </label>
                    <label>
                        <asp:Label ID="Label21" runat="server" Text="per"></asp:Label>
                    </label>
                    <label>
                    </label>
                </td>
            </tr>--%>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label31" runat="server" Text="Candidate should apply by"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <asp:Panel ID="Panel3" runat="server" Visible="true">
                            <label>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Apply online now" ForeColor="Black"
                                    Width="120px" OnClick="LinkButton1_Click"></asp:LinkButton>
                                <asp:Label ID="Label20" runat="server" Text="Fill Online Form" Width="120px" Visible="false"></asp:Label>
                            </label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                    <td style="width: 70%">
                        <asp:Panel ID="pansdsdssdd" runat="server" Visible="true">
                            <label>
                                <asp:Label ID="lblbyemail" runat="server" Text="Send Email to - "></asp:Label>
                                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                            </label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                    <td style="width: 70%">
                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Phone -" Width="60px"></asp:Label>
                                <asp:Label ID="lblphonno" runat="server" Text=""></asp:Label>
                                :
                                <asp:Label ID="lblpername" runat="server" Text=""></asp:Label>
                            </label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                    <td style="width: 70%">
                        <asp:Panel ID="Panel2" runat="server" Visible="true">
                            <label>
                                <asp:Label ID="Label13" runat="server" Text="Visit Address -" Width="100px"></asp:Label>
                                <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label>
                            </label>
                        </asp:Panel>
                    </td>
                </tr>
                <%-- <tr>
                <td style="width: 30%">
                    <label>
                        <asp:Label ID="Label44" runat="server" Text="Status"></asp:Label>
                    </label>
                </td>
                <td style="width: 70%">
                    <label>
                    </label>
                </td>
            </tr>--%>
            </table>
        </fieldset>
    </div>
</asp:Content>
