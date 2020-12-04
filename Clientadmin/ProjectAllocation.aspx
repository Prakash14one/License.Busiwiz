<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProjectAllocation.aspx.cs" Inherits="ProjectAllocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend id="5">Project Allocation </legend>

                    <table id="pagetbl" width="100%">
                          <tr>
                            <td>
                                <label>
                                    Project Name
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtproname" runat="server" Height="75px" 
                                        Width="500px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                <label>
                                    Project Description
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtedescription" runat="server" Height="75px" TextMode="MultiLine"
                                        Width="500px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Employee Name
                                </label>
                            </td>
                            <td>
                                <label>
                                       <asp:DropDownList ID="ddlemployee" runat="server">
                                       
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Start date
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                  <asp:TextBox ID="txtstartdate" runat="server" Width="70px" >
                                       </asp:TextBox>
                                   <cc1:CalendarExtender ID="calstartdate" runat="server" TargetControlID="txtstartdate"
                                        PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    End Date
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                 <asp:TextBox ID="txtenddate" runat="server" Width="70px" >
                                       </asp:TextBox>
                                     <cc1:CalendarExtender ID="calenddate" runat="server" TargetControlID="txtenddate"
                                        PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <label>
                                   TargetEndDate
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                 <asp:TextBox ID="txttargetenddate" runat="server" Width="70px" >
                                       </asp:TextBox>
                                     <cc1:CalendarExtender ID="targetenddate" runat="server" TargetControlID="txttargetenddate"
                                        PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                        </tr>
           <%--             <tr>
                            <td>
                                <label>
                                   Status
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:DropDownList ID="ddlmonthlygoal" runat="server">
                                    
                                     </asp:DropDownList>
                                </label>
                            </td>
                        </tr>--%>
                           
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" 
                                    onclick="BtnSubmit_Click" />
                 
                                
                                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" 
                                    onclick="Button2_Click" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <%--    <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>--%>
  </asp:UpdatePanel>
</asp:Content>

