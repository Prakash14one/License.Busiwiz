<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="JobManage.aspx.cs" Inherits="ShoppingCart_Admin_JobManage"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div>
                    <label>
                        <asp:Label ID="Label21" runat="server" Text="Filter by Business Name"></asp:Label>
                        <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label22" runat="server" Text="From Date"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtexpiry"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtstartdate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                            TargetControlID="txtstartdate">
                        </cc1:CalendarExtender>
                    </label>
                    <label>
                    <br />
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                    </label>
                    <label>
                        <asp:Label ID="Label23" runat="server" Text="To Date"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtexpiry"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtenddate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" 
                            PopupButtonID="ImageButton3" TargetControlID="txtenddate">
                        </cc1:CalendarExtender>
                    </label>
                    <label>
                    <br />
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                    </label>
                    <label>
                        <asp:Label ID="Label24" runat="server" Text="Status"></asp:Label>
                        <asp:DropDownList ID="ddlfilterstatus" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label25" runat="server" Text="Party Name"></asp:Label>
                        <asp:DropDownList ID="ddlfilterparty" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label>
                    <br />
                     <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Go" CssClass="btnSubmit" />
                    </label>
                </div>
                <div style="clear: both;">
                </div>
                <table width="100%">
                   
                   
                    <tr>
                        <td colspan="2" style="width: 100%">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                DataKeyNames="Id" OnRowCommand="GridView1_RowCommand" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Job no" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobno" runat="server" Text='<%#Bind("JobNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtJobno" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblJobno" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferenceNo" runat="server" Text='<%#Bind("JobReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtReferenceNo" runat="server"></asp:TextBox>
                                            <asp:Label ID="lbl" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobName" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtJobName" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblJobName" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnote" runat="server" Text='<%#Bind("Note") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPartyName" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" runat="server" Text='<%#Bind("Compname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPartyName" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("StatusName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%#Bind("JobStartDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%#Bind("JobEndDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:ButtonField CommandName="View" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="View" Text="View/Manage" ItemStyle-ForeColor="#416371" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%">
                            <asp:Panel ID="Panel2" runat="server" Width="100%" Visible="False">
                                <table id="innertbl" style="width: 100%">
                                    
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlStoreName" runat="server" Width="150px" Enabled="False">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Job Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddljobname" runat="server" Width="150px" Enabled="False">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text="Job Number"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Reference No."></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblReferenceNo" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Party Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblPartyName" runat="server" Text="Label"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label15" runat="server" Text="Status"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label16" runat="server" Text="Job Start Date"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblJobStart" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label17" runat="server" Text="Job End Date"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label18" runat="server" Text="Total Material Cost"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lblTotMaterialCost" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label19" runat="server" Text="Total Labour Cost"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbltotallabour" runat="server" Font-Bold="True"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="Total Overhead Cost"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbltotaloverhead" runat="server" Font-Bold="True"></asp:Label>
                                            </label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <table id="tbl" style="width: 100%">
                                    <tr>
                                        <td colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label3" runat="server" Text="Inventotory Item"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlmainitem"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:DropDownList ID="ddlmainitem" runat="server" >
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label4" runat="server" Text="Qty Produced"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox17"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Batch No."></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtbatchno"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtbatchno" runat="server"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtissuedate"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtissuedate" runat="server" Width="100px"></asp:TextBox>
                                            </label>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                PopupButtonID="imgbtncal" TargetControlID="txtissuedate">
                                            </cc1:CalendarExtender>
                                            <label>
                                                <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label7" runat="server" Text="Ref No."></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox18"
                                                    EnableViewState="False" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Note"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox19"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Expiry Date"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtexpiry"
                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="txtexpiry" runat="server" Width="100px"></asp:TextBox>
                                            </label>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                PopupButtonID="ImageButton1" TargetControlID="txtexpiry">
                                            </cc1:CalendarExtender>
                                            <label>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                            </label>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center">
                                            <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Add" ValidationGroup="1"
                                                CssClass="btnSubmit" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%" align="center">
                            <label>
                                <asp:Label ID="lblpercentmasg" runat="server" ForeColor="Red"></asp:Label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdjob" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemmasterid" runat="server" Text='<%#Bind("InvWMasterId") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblitemname123" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            <asp:Label ID="lblmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="JobMasterTblId123" runat="server" Text='<%#Bind("JobMasterTblId") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstartdate123" runat="server" Text='<%#Bind("Date","{0:dd/MM/yyy}") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty Produced">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtactual123" runat="server" Text='<%#Bind("ActualQty") %>' Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtactual123" runat="server"
                                                ControlToValidate="txtactual123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch No." Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtbatchno123" runat="server" Text='<%#Bind("BatchNo") %>' Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtbatchno123" runat="server"
                                                ControlToValidate="txtbatchno123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtexpirydate123" runat="server" Text='<%#Bind("ExpiryDate","{0:MM/dd/yyyy}") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtexpirydate123" runat="server"
                                                ControlToValidate="txtexpirydate123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref No." Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtreferenceno123" runat="server" Text='<%#Bind("ReferenceNumber") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtreferenceno123" runat="server"
                                                ControlToValidate="txtreferenceno123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Note" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtnote123" runat="server" Text='<%#Bind("Note") %>' Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtnote123" runat="server"
                                                ControlToValidate="txtnote123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Cost Allocation %">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmaterialcostallocation" runat="server" Text='<%#Bind("MaterialCostAllocationPercent") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtmaterialcostallocation"
                                                runat="server" ControlToValidate="txtmaterialcostallocation" ErrorMessage="*"
                                                ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Cost Allocation Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmcamount" runat="server" Text='<%#Bind("MaterialCostAmount") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxttxtmcamount465" runat="server"
                                                ControlToValidate="txtmcamount" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Labour Cost Allocation %">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtlabourcostallocation" runat="server" Text='<%#Bind("LabourCostAllocationPercent") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxttxtmcamount" runat="server"
                                                ControlToValidate="txtlabourcostallocation" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Labour Cost Allocation Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtlabourcostamount" runat="server" Text='<%#Bind("LabourCostAmount") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtlabourcostamount" runat="server"
                                                ControlToValidate="txtlabourcostamount" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Over Head Allocation %">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtoaallocationpercent" runat="server" Text='<%#Bind("OverHeadAllocationPercent") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtoaallocationpercent" runat="server"
                                                ControlToValidate="txtoaallocationpercent" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Over Head Allocation Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtoaamount" runat="server" Text='<%#Bind("OverHeadAllocationAmount") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtoaamount" runat="server"
                                                ControlToValidate="txtoaamount" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Production Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttotalcost123" runat="server" Text='<%#Bind("TotalProductionCost") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxttotalcost123" runat="server"
                                                ControlToValidate="txttotalcost123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Production Cost Per Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttotalcostperunit123" runat="server" Text='<%#Bind("TotalProductionCost") %>'
                                                Width="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxttotalcostperunit123" runat="server"
                                                ControlToValidate="txttotalcostperunit123" ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Detail" ItemStyle-ForeColor="#416371">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsalesitemid" runat="server" Text='<%#Bind("InvWMasterId") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbljomasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Sales" ForeColor="Black" OnClick="link1_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button1" runat="server" Text="Calculate" OnClick="Button1_Click"
                                Visible="False" ValidationGroup="2" CssClass="btnSubmit" />
                            <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" Visible="False"
                                CssClass="btnSubmit" />
                            <asp:Button ID="btnedit" runat="server" Text="Edit" Visible="False" CssClass="btnSubmit" />
                            <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="False" CssClass="btnSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel3" runat="server" Visible="False">
                                <tr>
                                    <td colspan="4">
                                        <table id="tbl1" style="width: 100%">
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="4">
                                                    <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                        Width="16px" OnClick="ImageButton2_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <label>
                                                        <asp:Label ID="lblmsg1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    <label>
                                                        <asp:Label ID="lblitemnametemp" runat="server"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label1" runat="server" Text="Show All invoices with the same item sold after the job start date"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
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
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text="Track Items produced by Job Number <> to Sales Invoices"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
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
                                                <td colspan="4">
                                                    <asp:GridView ID="grdsalespopup" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Invoice Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltranmid123" runat="server" Text='<%# Eval("Tranction_Master_Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsalesdate" runat="server" Text='<%# Eval("SalesOrderDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpartymasterid" runat="server" Text='<%# Eval("PartyID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblpartymastername" runat="server" Text='<%# Eval("Compname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Sold">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqty123" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrate123" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sales Qty (Mapeed to Job)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtsalesqty" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty Sold By This Invoice">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtqtysold" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="Button4" runat="server" Text="Submit" OnClick="Button4_Click" CssClass="btnSubmit" />
                                                    <asp:Button ID="Button5" runat="server" Text="Cancel" CssClass="btnSubmit" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        Width="100%" DataKeyNames="Id" OnRowDeleting="GridView2_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Invoice Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltrnMid456" runat="server" Text='<%# Eval("Transaction_Master_Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Party">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpartymaster123" runat="server" Text='<%# Eval("PartyID") %>'></asp:Label>
                                                                    <asp:Label ID="lblparty456" runat="server" Text='<%# Eval("Compname") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqty456" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" Visible="false" />
                                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="false" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
