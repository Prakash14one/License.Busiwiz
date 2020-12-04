<%@ Page Language="C#" MasterPageFile="CandidateMain.master" AutoEventWireup="true"
    CodeFile="AddDocResume.aspx.cs" Inherits="Admin_AddDocMaster" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
        }
        function SelectAllCheckboxes1(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
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
         function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        } 
    </script>

    <asp:UpdatePanel ID="updiihh" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="">                            
                    </asp:Label>
                    <asp:Label ID="lablcname" runat="server" Visible="false"></asp:Label>
                </div>
                <fieldset>
                    <asp:Panel ID="Paneldoc" runat="server" Width="100%">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel3" runat="server" Visible="true">
                                            <table id="Table3" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="20%">
                                                    </td>
                                                    <td align="left">
                                                        <label>
                                                            <asp:Label ID="Label1" runat="server" Visible="true" Font-Bold="true" Font-Size="16px"
                                                                Text="Add document for the following : ">
                                                            </asp:Label>
                                                            <asp:Label ID="lblhead" runat="server" Visible="true" Font-Bold="true" Font-Size="16px"
                                                                Text="">
                                                            </asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                                            <table id="Table1" width="100%">
                                                <tr>
                                                    <td width="20%">
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButtonList Style="text-align: center" ID="rdpop" runat="server" AutoPostBack="True"
                                                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rdpop_SelectedIndexChanged"
                                                            Visible="False">
                                                            <asp:ListItem Value="1" Text="Attach document from filing cabinet"></asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True" Text="Upload and Attach a new document"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlfileup" runat="server" Visible="false">
                                            <table id="tb1" cellpadding="0" cellspacing="3" width="100%">
                                                <tr>
                                                    <td width="20%" align="right">
                                                        <label>
                                                            Cabinet-Drawer-Folder
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:DropDownList ID="ddltypeofdoc" runat="server" Width="550px" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" align="right">
                                                        <label>
                                                            Select Period
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True">Document Date</asp:ListItem>
                                                                <asp:ListItem>Document Upload Date</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                    </td>
                                                    <td>
                                                        <label>
                                                            From
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtfrom" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$">
                                                            </asp:RegularExpressionValidator>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txtfrom" runat="server" Width="70px"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                        </label>
                                                        <label>
                                                            To
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtto" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                        </label>
                                                        <label>
                                                            <asp:TextBox ID="txtto" runat="server" Width="70px"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                            <cc1:CalendarExtender ID="CalendarExtenderfrom" runat="server" PopupButtonID="imgbtncalfrom"
                                                                TargetControlID="txtfrom">
                                                            </cc1:CalendarExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtenderto" runat="server" PopupButtonID="imgbtnto"
                                                                TargetControlID="txtto">
                                                            </cc1:CalendarExtender>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnsearchgo" runat="server" Text=" Go " OnClick="btnsearchgo_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                            <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="True" AllowSorting="True"
                                                                AutoGenerateColumns="False" DataKeyNames="DocumentId" EmptyDataText="No Record Found."
                                                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                GridLines="Both" OnRowCommand="Gridreqinfo_RowCommand" Width="100%" PageSize="25"
                                                                OnSorting="Gridreqinfo_Sorting" OnPageIndexChanging="Gridreqinfo_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                                CommandArgument='<%#Eval("DocumentId") %>' ForeColor="Black"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Doc Date" HeaderStyle-Width="70px" DataField="DocumentDate"
                                                                        SortExpression="DocumentDate" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    <asp:BoundField DataField="PartyName" HeaderText="Party" SortExpression="PartyName"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" SortExpression="DocumentTitle"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <input id="chkAll1" onclick="javascript:SelectAllCheckboxes1(this);" type="checkbox" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkaccentry" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnatt" runat="server" Text="Attach" OnClick="btnatt_Click" Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlinserdoc" runat="server" Visible="false">
                                            <table id="Table2" cellpadding="0" cellspacing="0" width="100%">
                                                <asp:Panel ID="panelhide" runat="server">
                                                    <tr>
                                                        <td width="20%" align="right">
                                                            <label>
                                                                Document Title
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoctitle"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z/0-9\s]*)" ControlToValidate="txtdoctitle"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtdoctitle" runat="server" ValidationGroup="1" TabIndex="2" MaxLength="30"
                                                                    onKeydown="return mak('div1',10,this)">
                                                                </asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:Label ID="Label12" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">30</span>
                                                                <asp:Label ID="lbljshg" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right">
                                                            <label>
                                                                Cabinet-Drawer-Folder
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlindocfil"
                                                                    ErrorMessage="*" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton49" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton48" EventName="Click" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlindocfil" Width="550px" runat="server" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton49" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                            OnClick="ImageButton49_Click" Width="20px" AlternateText="Add New" ImageAlign="Bottom"
                                                                            Height="20px" ToolTip="AddNew" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton48" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                            OnClick="ImageButton48_Click" AlternateText="Refresh" Height="20px" ImageAlign="Bottom"
                                                                            Width="20px" ToolTip="Refresh" />
                                                                    </label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right">
                                                            <label>
                                                                User Name
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlpartyname"
                                                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton50" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="ImageButton51" EventName="Click" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlpartyname" runat="server" ValidationGroup="1" TabIndex="3">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton50" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                            OnClick="ImageButton50_Click" ImageAlign="Bottom" ToolTip="AddNew" Width="20px" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="ImageButton51" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                            OnClick="ImageButton51_Click" ImageAlign="Bottom" ToolTip="Refresh" Width="20px" />
                                                                    </label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right">
                                                            <label>
                                                                Document Date
                                                                <cc1:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="TxtDocDate">
                                                                </cc1:CalendarExtender>
                                                                <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="TxtDocDate"
                                                                    ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="TxtDocDate" runat="server" Width="70px" TabIndex="4"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right">
                                                            <label>
                                                                Document Ref. Number
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtdocrefnmbr" runat="server" MaxLength="15" ValidationGroup="1"
                                                                    Width="90px" TabIndex="5" Text="0"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td width="20%" align="right">
                                                        <label>
                                                            Document to Upload
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileUpload1"
                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </label>
                                                        <label>
                                                            <asp:Button ID="imgbtnAdd" runat="server" Text=" Add " ValidationGroup="1" OnClick="imgbtnAdd_Click" />
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" align="right">
                                                        <label>
                                                            <asp:Label ID="Label2" runat="server" Visible="false" Text="Net Amount">
                                                            </asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txtnetamount" Visible="false" runat="server" MaxLength="15" Width="90px"
                                                                TabIndex="6" Text="0"></asp:TextBox>
                                                            <asp:RegularExpressionValidator Visible="false" ID="RegularExpressionValidator4"
                                                                ControlToValidate="txtnetamount" ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server"
                                                                ErrorMessage="Invalid" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnldoclist" runat="server">
                                                            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Record Found." CssClass="mGrid"
                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                                                AutoGenerateColumns="False" DataKeyNames="documenttype" Width="100%" PageSize="20"
                                                                OnRowCommand="GridView1_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Business Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        DataField="Businessname"></asp:BoundField>
                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpid" runat="server" Text='<%#Eval("PartyId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" DataField="DocType"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Document Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        DataField="DocumentTitle"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        DataField="documentname"></asp:BoundField>
                                                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundField>
                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocdate" runat="server" Text='<%#Eval("docdate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocrefno" runat="server" Text='<%#Eval("docrefno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocamt" runat="server" Text='<%#Eval("docamt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Account/images/delete.gif"
                                                                        ImageUrl="~/Account/images/delete.gif" HeaderText="Delete" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del"></asp:ButtonField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="tempupload" runat="server" OnClick="Button3_Click" Text="Temp Upload"
                                                            Visible="False" />
                                                        <asp:Button ID="btnuplo" runat="server" Visible="false" OnClick="btnuplo_Click" Text="Upload" />
                                                        <asp:Button ID="imgbtnreset" runat="server" OnClick="imgbtnreset_Click" Text="Reset"
                                                            Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btn1pop" runat="server" Text="Go" OnClick="btn1pop_Click" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="background-color: #CCCCCC; font-weight: bold;">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label3" runat="server" Text="List of Documents to be Attached"> </asp:Label>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                            DataKeyNames="Id" Width="100%" OnRowCommand="grd_RowCommand" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" SortExpression="Doc Id" HeaderStyle-Width="3%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DocumentId") %>'
                                                            CommandName="Send" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DocumentId") %>'
                                                            ForeColor="Black"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DocumentTitle" HeaderText="Title" ItemStyle-Width="200px"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DocumentName" HeaderText="File Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id")%>'
                                                            CommandName="delete1" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Are you sure you want to delete this Record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnAdd" />
            <asp:AsyncPostBackTrigger ControlID="rdpop" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnsearchgo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnatt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnuplo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
