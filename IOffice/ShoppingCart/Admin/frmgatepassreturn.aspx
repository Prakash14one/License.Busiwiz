<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="frmgatepassreturn.aspx.cs" Inherits="Manage_frmgatepassreturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Visible="False"></asp:Label>
        </div>
        <fieldset>
            <legend>Please fill in the details about your visit </legend>
            <asp:Panel ID="panelasd" runat="server" Visible="false">
                <label class="cssLabelCompany_Information">
                    <asp:Label ID="lblEmpNa" Text="Employee Name" runat="server"></asp:Label>
                </label>
                <label class="cssLabelCompany_Information_Ans">
                    <asp:Label CssClass="lblSuggestion" ID="lblEmployeeName" runat="server"></asp:Label>
                </label>
            </asp:Panel>
            <div style="clear: both;">
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="lblOutTimelbl" Text="Approved Out Time" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblExpOut" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblInTimelbl" Text="Approved Intime" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblExpIn" CssClass="lblSuggestion" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblTotalOutsidelbl" Text="Approved Duration" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblTotalTime" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label1" Text="Actual Out Time" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblactualouttime" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label3" Text="Actual Intime" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblactualintime" CssClass="lblSuggestion" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label5" Text="Actual Duration" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblactualduration" CssClass="lblSuggestion" runat="server" Style="font-weight: 700"></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="lblDatelbl" Text="Date" runat="server"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblDateTime" CssClass="lblSuggestion" runat="server"></asp:Label>
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
            </table>
            <label class="cssLabelCompany_Information">
                <asp:Label ID="lbloutgoingdetaillbl" Text="The details of outgoing visit with Gatepass Number"
                    runat="server" Visible="false"></asp:Label>
            </label>
            <label class="cssLabelCompany_Information_Ans">
                <asp:Label runat="server" ID="lblgatepass" CssClass="lblSuggestion" Visible="false"></asp:Label>
            </label>
        </fieldset>
        <div style="clear: both;">
        </div>
        <asp:Panel ID="pnlmainentry" runat="server">
        </asp:Panel>
        <fieldset id="f123">
            <legend>
                <asp:Label ID="lblOuttimeEntry" Text="List of Task" runat="server" Style="font-weight: 700"></asp:Label>
            </legend>
            <div style="float: right;">
                <%--  <label>
                        <asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                            CssClass="btnSubmit" Text="Print Version" />
                    </label>--%>
            </div>
            <div style="clear: both;">
            </div>
            <asp:GridView ID="grdreturn" runat="server" Width="100%" DataKeyNames="ID" AutoGenerateColumns="False"
                GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No Record Found."
                AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblId" Text='<%#Bind("ID") %>' Visible="false" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TaskId" Visible="false" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskID" Text='<%#Bind("TaskID") %>' Visible="false" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyName" Text='<%#Bind("PartyName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purpose of Visit" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="lblPurpose" Text='<%#Bind("PurposeofVisit") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Reached" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTimeReached" runat="server" Width="60px"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtTimeReached" ID="RequiredFieldValidator1"
                                runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="retime" runat="server" ControlToValidate="txtTimeReached"
                                ErrorMessage="(HH:MM)" ValidationExpression="^(\d{1,2}):(\d{2})?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Left" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTimeLeft" runat="server" Width="60px"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtTimeLeft" ID="RequiredFieldValidator2"
                                runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtTimeReached"
                                ControlToValidate="txtTimeLeft" ErrorMessage="&gt;" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="time" runat="server" ControlToValidate="txtTimeLeft"
                                ErrorMessage="(HH:MM)" ValidationExpression="^(\d{1,2}):(\d{2})?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Duration Min" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDuration" Height="22px" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details of Meeting" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Width="45%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDetails" Height="60px" Width="400px" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([.a-zA-Z0-9\s]*)" ControlToValidate="txtDetails"
                                ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="AttachDocument" Visible="false" >
                            <ItemTemplate>
                                <asp:FileUpload ID="fu1control" Width="150px" runat="server" />                                
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Work Completed" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCompleted" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <div style="clear: both;">
    </div>
    <fieldset>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Want to set reminder for any future follow required if any ?"
            TextAlign="Left" />
    </fieldset>
    <div style="clear: both;">
    </div>
    <label>
        <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="btnSubmit"
            Visible="false" OnClick="btnCalculate_Click" ValidationGroup="1" />
    </label>
    <label>
        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btnSubmit" OnClick="btnSubmit_Click"
            ValidationGroup="1" />
    </label>
    <div style="clear: both;">
    </div>
</asp:Content>
