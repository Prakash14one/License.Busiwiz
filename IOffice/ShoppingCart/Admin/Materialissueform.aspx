<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="Materialissueform.aspx.cs" Inherits="ShoppingCart_Admin_Materialissueform"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function Button1_onclick() {

        }

        function Button2_onclick() {

        }

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
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
                <div style="padding-left:1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
             
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="New Issue of Material for Work Order" 
                            Visible="False"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add Material Issue" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnluper" Visible="false">
                    <div>
                        <label>
                            <asp:Label ID="Label27" runat="server" Text="Business Name"></asp:Label>
                            <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStoreName"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlStoreName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                           <label>
                            <asp:Label ID="Label31" runat="server" Text="Select Work Order Name"></asp:Label>
                            <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSelectJob"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlSelectJob" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlSelectJob_SelectedIndexChanged">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label28" runat="server" Text="Reference No."></asp:Label>
                            <asp:Label ID="Label52" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRefernceNo"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtRefernceNo" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>.a-zA-Z_+:;={}[]|\/]/g,/^[\0-9\s]+$/,'div1',15)"></asp:TextBox>
   <asp:Label ID="Label56" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div1" class="labelcount">15</span>
                            <asp:Label ID="Label25" runat="server" Text="(0-9)" CssClass="labelcount"></asp:Label>
                             <cc1:FilteredTextBoxExtender ID="FilteredTextBokghxExtender11" runat="server" Enabled="True"
                                            TargetControlID="txtRefernceNo" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                        </label>
                        <label>
                            <asp:Label ID="Label30" runat="server" Text="Date"></asp:Label>
                            <asp:Label ID="Label54" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtissuedate"
                                ErrorMessage="*" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            
                          
                                        <asp:TextBox ID="txtissuedate" runat="server" Width="80px" 
        ></asp:TextBox>
                                    </label>    
                                      
                                    <label>  
                                    <br />  
                                        <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                          <cc1:CalendarExtender  ID="CalendarExtender1" runat="server"
                                            PopupButtonID="imgbtncal" TargetControlID="txtissuedate">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ControlToValidate="txtissuedate" Display="Dynamic"
                                ErrorMessage="Inavalid Character" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label> 
                        <div style="clear: both;">
                        </div>
                     
                        <label>
                          <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                 
                            <asp:Label ID="Label29" runat="server" Text="Note"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s_.]*)"
                                ControlToValidate="txtNote" ValidationGroup="1" >
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtNote" runat="server" MaxLength="100" Width="654px" onKeydown="return mask(event)"
                                
                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9._\s]+$/,'div2',100)"></asp:TextBox>
                            <asp:Label ID="Label56j" runat="server" Text="Max " CssClass="labelcount" ></asp:Label><span id="div2" class="labelcount">100</span>
                            <asp:Label ID="Label26" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                        </label>
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
             
                <asp:Panel ID="pnldown" runat="server" Visible="false">
                    <fieldset>
                     <legend>
                        <asp:Label ID="Label68" runat="server"  Text="List of Material Issued"
                           ></asp:Label>
                    </legend>
                        <table id="tbldown" width="100%">
                            <tr>
                                <td >
                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="Id" Width="100%" OnRowDeleting="GridView1_RowDeleting" 
                                        OnRowCancelingEdit="GridView1_RowCancelingEdit" ShowFooter="True">
                                        <Columns>
                                        <asp:TemplateField HeaderText="Sr No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlitem123" runat="server" OnSelectedIndexChanged="ddlitem123_SelectedIndexChanged" AutoPostBack="true" Width="95%">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="InvWMasterId123" runat="server" 
                                                        Text='<%# Eval("InvWMasterId") %>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="lblmasterid" runat="server" Text='<%# Eval("Id") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Qty On Hand" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblqtyonhand" runat="server"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                 <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issued Qty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="130px">
                                                <ItemTemplate>
                                                <label>
                                                    <asp:TextBox ID="txtqty123" runat="server" Text='<%# Eval("Qty") %>' Width="80px">
                                                                                                      </asp:TextBox>
                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBonder10" runat="server" Enabled="True"
                                            TargetControlID="txtqty123" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </label>
                                        <label>
                                           <asp:Label ID="lblredmask" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                                                       </label>
                                                    <asp:Label ID="lblmaterialmasterid" runat="server" Text='<%# Eval("MaterialMasterId") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"  />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Avg. Rate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcost123" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal123" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                         <asp:Label ID="lbltotalfooter" runat="server"  Font-Bold="true"></asp:Label>
                                         </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                 <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Calculate"
                                        CssClass="btnSubmit" ValidationGroup="1"/>
                                    <asp:Button ID="Button3" runat="server" Text="Submit" OnClick="Button3_Click" CssClass="btnSubmit"
                                        ValidationGroup="1" Visible="False" />
                                
                                   
                                    <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" 
                                        Text="Update" CssClass="btnSubmit" Visible="False" />
                                        
                                         <asp:Button ID="Button9" runat="server"  Text="Cancel"
                                        CssClass="btnSubmit" onclick="Button9_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label49" runat="server"  Text="List of Material Issued for Various Work Orders"></asp:Label>
                    </legend>
                    <div style="float: right">
                       
                            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="false" />
                       
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label50" runat="server" Text="Select by Business Name"></asp:Label>
                      
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStoreName"
                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Material Issued for Various Work Orders"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="Id" Width="100%" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnSorting="GridView1_Sorting" EmptyDataText="No Record Found." 
                                        onpageindexchanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business" SortExpression="WName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstrname123" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Order Name" SortExpression="JobName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobname123" runat="server" Text='<%# Eval("JobName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldate123" runat="server" Text='<%# Eval("Date","{0:MM/dd/yyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference No." SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno123" runat="server" Text='<%# Eval("ReferenceNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnote123" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" ItemStyle-Width="3%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton4" runat="server" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit"
                                                        CommandArgument='<%# Eval("Id") %>' OnClick="LinkButton4_Click"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#AAAAAA" Width="399px">
                                        <fieldset>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <asp:Label ID="Label48" runat="server" Text="Confirm Delete"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #CCCCCC">
                                                        <label>
                                                            <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You Want to 
                                                    Delete !</asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button5" runat="server" Text="Yes" BackColor="#CCCCCC" Width="55px"
                                                            Height="25px" CssClass="btnSubmit" />
                                                        <asp:Button ID="Button6" runat="server" Text="No" BackColor="#CCCCCC" Width="55px"
                                                            Height="25px" CssClass="btnSubmit" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset></asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
