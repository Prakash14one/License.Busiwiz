<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ClientPricePlanDetail.aspx.cs" Inherits="ClientPricePlanDetail" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
                <strong>Enter Your Price Detail</strong>
            </td>
        </tr>
        <tr>
            <td style="height: 16px;">
               Service Name :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtPlanName" runat="server" Width="215px"></asp:TextBox>
            </td>
            <td style="width: 3px; height: 16px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlanName"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
               Service Description :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtPlanDesc" runat="server" Width="214px" Height="125px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td style="width: 3px; height: 16px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPlanDesc"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td style="height: 16px">
                Servece Types :
            </td>
            <td style="width: 3px; height: 16px">
        
                            <asp:DropDownList ID="ddlservicestype" runat="server" Width="145px">
                                
                            </asp:DropDownList>
                          
               
            </td>
            </tr>
        <tr>
            <td style="height: 16px">
                Service QTY :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtqty" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
        <tr>
            <td style="height: 16px">
                Service Amount :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtPlanAmount" runat="server" Width="163px"></asp:TextBox>
                in ($)
            </td>
            <td style="width: 3px; height: 16px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPlanAmount"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
         <tr>
            <td style="height: 16px">
                Tax1name :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txt1name" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
             <tr>
            <td style="height: 16px">
                Tax1(Percentage) :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txt1perc" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
             <tr>
            <td style="height: 16px">
                Tax2name :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txt2name" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
             <tr>
            <td style="height: 16px">
                Tax2(Percentage) :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txt2perc" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
            <tr>
            <td style="height: 16px">
                Service License Period :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtlpe" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>
            <tr>
            <td style="height: 16px">
                No of busiwizcontrollers :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtbscontroller" runat="server" Width="163px"></asp:TextBox>
               
            </td>
            </tr>

        <tr>
            <td style="height: 16px">
                Active / Deactive :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
            </td>
            <td style="width: 3px; height: 16px">
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
                Start Date :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtStartdate" runat="server" Width="163px"></asp:TextBox><asp:ImageButton
                    ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" /><cc1:CalendarExtender
                        ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnEndDate" TargetControlID="txtStartdate">
                    </cc1:CalendarExtender>
            </td>
            <td style="width: 3px; height: 16px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartdate"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
                End Date :
            </td>
            <td style="width: 3px; height: 16px">
                <asp:TextBox ID="txtEndDate" runat="server" Width="163px"></asp:TextBox><asp:ImageButton
                    ID="imgbtnCalEnddate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" /><cc1:CalendarExtender
                        ID="CalendarExtender2" runat="server" PopupButtonID="imgbtnCalEnddate" TargetControlID="txtEndDate">
                    </cc1:CalendarExtender>
            </td>
            <td style="width: 3px; height: 16px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate"
                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
            </td>
            <td style="width: 3px; height: 16px">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                    ValidationGroup="1" />
            </td>
            <td style="width: 3px; height: 16px">
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="645px" Height="250px" ScrollBars="Auto">
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="ClientPricePlanId" AutoGenerateColumns="False"
            EmptyDataText="There is no data." OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                    Text="Button" />
                <asp:BoundField DataField="PricePlanName" HeaderText="Price Plan Name" />
                <asp:BoundField DataField="PricePlanDesc" HeaderText="Price Plan Desc" />
                <asp:BoundField DataField="Active" HeaderText="Active" />
                <asp:BoundField DataField="PricePlanAmount" HeaderText="Price Plan Amount(in $)" />
                <asp:BoundField DataField="Active" HeaderText="Active" />
                <asp:BoundField DataField="StartDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="Start Date" />
                <asp:BoundField DataField="EndDate" DataFormatString="{0:d-MMM-yyyy}" HeaderText="End Date" />
            </Columns>
            <PagerStyle CssClass="GridPager" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternateRow" />
            <RowStyle CssClass="GridRowStyle" />
            <FooterStyle CssClass="GridFooter" />
        </asp:GridView>
    </asp:Panel>
    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    <input id="hdnPricePlanId" runat="server" name="hdnPricePlanId" type="hidden" />
</asp:Content>
