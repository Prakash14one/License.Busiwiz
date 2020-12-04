<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="EoqCalculation.aspx.cs" Inherits="EoqCalculation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>

<script runat="server">    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

       
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }


        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }




            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="padding-left: 1%">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div>
                        <table width="100%">
                            <tr>
                                <td colspan="3">
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Business Name "></asp:Label>
                                    </label>
                                     <label>
                                        <asp:DropDownList ID="ddlWarehouse" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                     <label>
                                        <asp:Label ID="Label1" runat="server" Text="Current Accounting Year : "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblcurrentaccountstart" runat="server"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                                        <asp:Label ID="lblcurrentaccountend" runat="server"></asp:Label>
                                    </label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label3" runat="server" ForeColor="#426172" Text="Last EOQ made on "></asp:Label>
                                    <asp:Label ID="lbllasteogdate" Font-Bold="true" Font-Italic="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="Do you wish to recalculate Economic Order Quantity?"
                                        OnCheckedChanged="CheckBox1_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Inventory EOQ" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right; margin-left: 80px;">
                        <label>
                            <asp:Button ID="Button1" Visible="false" runat="server" CssClass="btnSubmit" Text="Save"
                                OnClick="Button1_Click" />
                            <asp:Button ID="Button2" Visible="false" runat="server" CssClass="btnSubmit" Text="Recalculate & Save"
                                OnClick="Button2_Click" />
                            <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button3_Click" />
                            <input id="btnPrint" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label16" runat="server" Font-Italic="True" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblstore" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label4" runat="server" Font-Italic="True" Text="List of Inventory EOQ"></asp:Label>
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlstock" runat="server" ScrollBars="Both" Width="100%">
                                        <asp:GridView EmptyDataText="No Record Found." AllowSorting="true" ID="GridView1"
                                            DataKeyNames="InventoryWarehouseMasterId" FooterStyle-BackColor="#416271" 
                                            runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            CssClass="mGrid" GridLines="Both" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        <asp:Label ID="lblinvwarehouseid" runat="server" Text='<%# Bind("InventoryWarehouseMasterId") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblprefferedvendorid" runat="server" Text='<%# Bind("PreferredVendorId") %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product No." SortExpression="ProductNo" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproductno" runat="server" Text='<%# Bind("ProductNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Weight/Unit" DataField="weightunitname" SortExpression="weightunitname"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="Product Volume (In Cubic Feet)" SortExpression="Volume"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproductvoulme" runat="server" Text='<%# Bind("Volume") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Yearly Avg Stock" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblyearlyavgstock" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avg Stock Volume" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblavgstockvolume" Visible="false" runat="server" ></asp:Label>
                                                        <asp:Label ID="lblavgstockvolumefordisplay" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stored at Location" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlsitename" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlsitename_SelectedIndexChanged"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Site Volume Capacity" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsitevolumecapacity" Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblsitevolumecapacityfordisplay" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Usage of Site Volume %" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproductusageofsitevolumepercent" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Site Storage Cost per year" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsitestoragecostperyear" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carring Cost of Product per year" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcarringcostproductperyear" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carring Cost Per Unit" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcarringcostperunit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ordering cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblorderingcost" runat="server"></asp:Label>
                                                        <asp:Label ID="lblorderingcostlabel" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EOQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbleoq" ForeColor="#426172" runat="server"></asp:Label>
                                                        <asp:Label ID="lbleoqtext" ForeColor="Red" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
