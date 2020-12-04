<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="JobVacancyProfile.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
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
    <div class="products_box">
        <fieldset>
            <legend>
                <asp:Label ID="Label2" runat="server" Text="Temp Job Profile"></asp:Label>
            </legend>
            <table width="100%">
               
                <tr>
                    <td style="width: 30%">
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label39" runat="server" Text="Position Type"></asp:Label>                           
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label class="cssLabelCompany_Information_Ans">
                            <asp:Label ID="lblvactype" runat="server" Text=""></asp:Label>                          
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
                            <asp:Label ID="Label106" runat="server" Text="Country :"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblcountry" runat="server" Text="" Font-Bold="true"></asp:Label>,
                        </label>
                        <label>
                            <asp:Label ID="Label104" runat="server" Text="State :"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblstate" runat="server" Text="" Font-Bold="true"></asp:Label>,
                            <%-- <asp:Label ID="Label8" runat="server" Text="" ForeColor="#457cec"></asp:Label>--%>
                        </label>
                        <label>
                            <asp:Label ID="Label102" runat="server" Text="City :"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblcity" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </label>
                   
                    </td>
                </tr>               
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Skill Required"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblscriptreq" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Other Terms Conditions"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lblothertermscondition" runat="server" Text=""></asp:Label>
                        </label>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Work Timings"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                             <asp:Label ID="Label5" runat="server" Text="From"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblfrmtim" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblfrmampm" runat="server" Text=""></asp:Label>
                        </label>
                        
                        <label>
                             <asp:Label ID="Label6" runat="server" Text="To"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lbltotim" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lbltoampm" runat="server" Text=""></asp:Label>
                        </label>
                        <label>
                        <asp:Label ID="lbltizon" runat="server" Text="Time Zone:"></asp:Label>
                        <asp:Label ID="lbltimzone" runat="server" Text=""></asp:Label>
                        </label>
                        
                    </td>
                </tr>
                
                 <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Job May Last for"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                        <label>
                            <asp:Label ID="lbllastjob" runat="server" Text=""></asp:Label>
                             <asp:Label ID="lbldy" runat="server" Text="Days"></asp:Label>
                        </label>
                    </td>
                </tr>
               <%-- <tr>
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
                </tr>--%>
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
               <%-- <tr>
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
                </tr>--%>
                <tr>
                    <td style="width: 30%">
                        <label>
                            <asp:Label ID="Label31" runat="server" Text="Candidate should apply by"></asp:Label>
                        </label>
                    </td>
                    <td style="width: 70%">
                       <%-- <asp:CheckBox ID="CheckBox4" runat="server" Text="SMS" TextAlign="Right" Checked="true" />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Email" TextAlign="Right" Checked="true" />
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Phone" TextAlign="Right" Checked="true" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Apply Online" TextAlign="Right"
                            Checked="true" />--%>
                            <label>
                            <asp:Label ID="lblsms" Text="SMS" Visible="false" runat="server"></asp:Label>
                            </label>
                            <label>
                            <asp:Label ID="lblemail" Text="Email" Visible="false" runat="server"></asp:Label>
                            </label>
                             <label>
                            <asp:Label ID="lblphone" Text="Phone" Visible="false" runat="server"></asp:Label>
                            </label>
                             <label>
                            <asp:Label ID="lblapplyonline" Text="Apply Online" Visible="false" runat="server"></asp:Label>
                            </label>
                    </td>
                 </tr>
                 <%--   <td style="width: 70%">
                        <asp:Panel ID="pansdsdssdd" runat="server" Visible="false">
                            <label>
                                <asp:Label ID="lblbyemail" runat="server" Text="Email-" Width="60px"></asp:Label>
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
                <tr>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                    <td style="width: 70%">
                        <asp:Panel ID="Panel3" runat="server" Visible="true">
                            <label>
                                <asp:Label ID="Label20" runat="server" Text="Fill Online Form" Width="120px"></asp:Label>
                            </label>
                        </asp:Panel>
                    </td>
                </tr>--%>
            </table>
        </fieldset>
    </div>
</asp:Content>
