<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="RemunerationMaster.aspx.cs" Inherits="Add_Remuneration_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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
    <asp:UpdatePanel ID="upf" runat="server">
    <ContentTemplate>
     <div class="products_box">
      <div style="padding-left: 1%;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False" />
        </div>
        <fieldset>
           <legend> <asp:Label ID="lbledins" runat="server" Text=""></asp:Label></legend>
             <div style="float: right;">
                <asp:Button ID="btnadd" Text="Add New Remuneration" runat="server" 
                     CssClass="btnSubmit" onclick="btnadd_Click"
                   />
            </div>
              <asp:Panel ID="pnladd" Visible="false" runat="server">
            <label>
                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged">
                </asp:DropDownList>
             
            </label>
            <label>
                <asp:Label ID="Label2" runat="server" Text="Remuneration Name"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremuneration"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9 \s]*)"
                                ControlToValidate="txtremuneration" ValidationGroup="1"> </asp:RegularExpressionValidator>
                <asp:TextBox ID="txtremuneration" runat="server" MaxLength="50"
                                onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]()|\/]/g,/^[\a-zA-Z.0-9_\s]+$/,'div3',50)"></asp:TextBox>
            </label>
             <label>
                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                            <span id="div3" class="labelcount">50</span>
                            <asp:Label ID="Label8" runat="server" Text="(A-Z 0-9 _ .)" CssClass="labelcount"></asp:Label>
                        </label>


            <label>
                <asp:Label ID="Label3" runat="server" Text="Related A/C Master Name"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtremuneration"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
           
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlaccount" runat="server" Width="400px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="LinkButton13" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="imgdeptrefresh" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </label>
            
            <label>
            <br />
                <asp:ImageButton ID="LinkButton13" runat="server" Height="16px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                    OnClick="LinkButton97666667_Click" ToolTip="AddNew" ImageAlign="Bottom" Width="16px" />
            </label>
             <label>
             <br />
                <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="16px"
                   ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="LinkButton13_Click" ToolTip="Refresh"
                    Width="16px" ImageAlign="Bottom" />
            </label>
            <div style="clear: both;">
            </div>
           <label>
            <div style="clear: both;">
            <br />
            <asp:CheckBox ID="cb_active" runat="server" Text="Active"  TextAlign="Right" />
            <br />
            </div>
            </label> 
            <label>
                <asp:CheckBox ID="chkIscommi" Text="Is this a commission based remuneration? Yes"
                    runat="server" TextAlign="Left" />
            </label>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlcan" runat="server" Visible="false">
                <label><asp:CheckBox ID="chlcanadapension" Text="Is this a pensionable Income (Canada)? Yes"
                        runat="server" TextAlign="Left" />
                </label>
                <div style="clear: both;">
                </div>
                <label><asp:CheckBox ID="chkcanadainsurable" runat="server" Text="Is this a Insurable Income (Canada)? Yes"
                        TextAlign="Left" />
                </label>
            </asp:Panel>
          
            <label>
                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click"
                    CssClass="btnSubmit" ValidationGroup="1" />
                      </label>
            <label>
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" ValidationGroup="1"
                    Text="Update" CssClass="btnSubmit" Visible="False" />
            </label>
            <label>
                <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btnSubmit"
                    OnClick="Button4_Click" />
                <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%>
            </label>
            </asp:Panel>
        </fieldset>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="Label4" runat="server" Text="List of Remunerations"></asp:Label></legend>
          
         
            <div style="float: right;">
                <label>
                    <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit" CausesValidation="false"
                        OnClick="btncancel0_Click" Text="Printable Version" />
                          </label>
            <label>
                    <input id="Button2" type="button" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        class="btnSubmit" value="Print" visible="False" />
                </label>
            </div>
               <div style="clear: both;">
              <label>
                <asp:Label ID="Label5" runat="server" Text="Select by Business"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </label>
            <label>
            <asp:Label ID="Label9" runat="server" Text="Active"></asp:Label>
                <asp:DropDownList ID="ddlactive" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="2">All</asp:ListItem>
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </asp:DropDownList>
            </label> 
            </div>
             <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                <table width="100%">
                    <tr align="center">
                        <td>
                            <div id="mydiv" class="closed">
                                <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblBusiness" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label6" runat="server" Font-Size="18px" Text="List of Remuneration Details"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found." OnRowEditing="GridView1_RowEditing"
                                PageSize="25" OnRowDeleting="GridView1_RowDeleting" AllowSorting="True" OnSorting="GridView1_Sorting"
                                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                                AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging">
                                <Columns>
                                 
                                       <asp:TemplateField HeaderText="ID" SortExpression="ID"  Visible="false"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                           <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Name" SortExpression="WName" ItemStyle-Width="20%"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("WName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remuneration Name" SortExpression="RemunerationName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("RemunerationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Name" SortExpression="accountname" ItemStyle-Width="20%"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("accountname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" ValidationGroup="2" ButtonType="Image"
                                        HeaderStyle-HorizontalAlign="Left" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                        CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                        ItemStyle-Width="4%" >
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="4%" />
                                       </asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                        ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                OnClientClick="return confirm('Do you wish to delete this record?');"></asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                    <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image"  DeleteImageUrl="~/Account/images/delete.gif" HeaderImageUrl="~/Shoppingcart/images/trash.jpg"  ItemStyle-Width="4%"/>--%>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
         
        </fieldset>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
   
  
   
</asp:Content>
