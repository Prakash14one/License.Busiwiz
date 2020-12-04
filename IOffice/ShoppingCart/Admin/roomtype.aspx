<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    CodeFile="roomtype.aspx.cs" Inherits="add_roomtype" Title="Add RoomType" %>

<script runat="server">

   
  
</script>
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
        </script>

 <script language="javascript" type="text/javascript">
     function mask(evt) {

         if (evt.keyCode == 13) {

             return false;
         }


         if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


             //  alert("You have entered an invalid character");
             return false;
         }

     }
     function check(txt1, regex, reg, id, max_len) {
         if (txt1.value != '' && txt1.value.match(reg) == null) {
             txt1.value = txt1.value.replace(regex, '');
             //   alert("You have entered an invalid character");
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
<div class="products_box">
        <div style="padding-left: 1%">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
            </legend>
            <div style="float: right;">
                <asp:Button ID="btnadddd" runat="server" Text="Add New Room Type"
                    Width="180px" CssClass="btnSubmit" onclick="btnadddd_Click" />
            </div>
            <div style="clear: both;">
            </div>
            <asp:Panel runat="server" ID="pnladdd" Visible="false">
                <table width="100%">
                    <tr align="left">
                        <td width="25%">
                            <label>
                                School Name
                                <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlschool"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td valign="bottom">
                            <asp:DropDownList ID="ddlschool" runat="server" 
                                OnSelectedIndexChanged="ddlschool_SelectedIndexChanged" AutoPostBack="True" 
                               >
                            </asp:DropDownList>
                        </td>
                    </tr>
                   <tr>
                        <%-- <td style="width: 25%">
                                    <label>
                                        Grade Name
                                        <asp:Label ID="asdsadsad" runat="server" Text="*" CssClass="labelstar"></asp:Label>
         <asp:RangeValidator id="RangeValidator1" runat="server"
ErrorMessage="Please Select"  ControlToValidate="ddlGradeName" ValidationGroup="save"
></asp:RangeValidator>
                                    </label>
                                </td>--%>
                        <td style="width: 25%">
                            <label>
                                Building Name
                                <asp:Label ID="asdsadsad" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlbuildingName"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td valign="bottom">
                            <label>
                                <asp:DropDownList ID="ddlbuildingName" runat="server" 
                                onselectedindexchanged="ddlbuildingName_SelectedIndexChanged" AutoPostBack="True" 
                         >
                                </asp:DropDownList>
                            </label>
                         
                        </td>
                    </tr>
                    <tr>
                      <td style="width: 25%">
                            <label>
                                Floor Name
                                <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlfloor"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td valign="bottom">
                            <label>
                                <asp:DropDownList ID="ddlfloor" runat="server" onselectedindexchanged="ddlfloor_SelectedIndexChanged" 
                         >
                                </asp:DropDownList>
                            </label>
                         
                        </td>
                    </tr>
                    <tr>
                       
                        <td style="width: 25%">
                            <label>
                              Room Type
                                <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttype"
                                    SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txttype"
                                    ValidationGroup="save"></asp:RegularExpressionValidator>
                            </label>
                        </td>

                     <td valign="middle">
                            <label>
                                <asp:TextBox ID="txttype" runat="server" MaxLength="25" onKeydown="return mask(event)"
                                    Width="200px" onkeyup="return check(this,/[\\/!._@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'Span2',25)"
                                    TabIndex="5" OnTextChanged="txtfloor_TextChanged"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                <span id="Span2" cssclass="labelcount">25</span>
                                <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>
                            </label>
         
                        </td>
                  </tr>
                     <tr>
                        <td width="25%">
                            <label style="width: 350px;">
                                Take me to add Room after adding this Room Type</label>
                        </td>
                        <td>

                            <asp:CheckBox ID="chkNavigation" runat="server" Text="" ForeColor="#006699" 
                                oncheckedchanged="chkNavigation_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                        </td>
                        <td>
                            <asp:Button ID="btnsave" runat="server" Text="Submit" ValidationGroup="save" CssClass="btnSubmit"
                                OnClick="btnsave_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Update" CssClass="btnSubmit" ValidationGroup="save"
                                Visible="false" onclick="Button3_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>
                <asp:Label ID="Label4" runat="server" Text="List of Room Types"></asp:Label>
            </legend>
            <div id="divgrid" runat="server" visible="true" style="margin: 10px;">
                <div style="float: right; text-align: right; width: 100%;">
                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                        OnClick="Button1_Click1" />
                    <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        style="width: 51px;" type="button" value="Print" visible="false" />
                </div>
                <%-- <table width="100%">
                        <tr>
                        <td>--%>
                         <table>
                    <tr>
                  <td style="padding-left:5px;">
                <label>
                    Filter by School Name
                </label>
                <label>
                    <asp:DropDownList ID="filterschool" runat="server" AutoPostBack="True" 
                          onselectedindexchanged="filterschool_SelectedIndexChanged" >
                    </asp:DropDownList>
                </label>
                 </td>
                <td style="padding-left:5px;">
                <label>
                    Filter by Building Name
                </label>
                <label>
                    <asp:DropDownList ID="filterbuilding" runat="server" AutoPostBack="true" onselectedindexchanged="filterbuilding_SelectedIndexChanged" 
                         >
                    </asp:DropDownList>
                </label>
                 </td>
                                 <td style="padding-left:5px;">
                <label>
                    Filter by Floor Name
                </label>
                <label>
                    <asp:DropDownList ID="filterfloor" runat="server" AutoPostBack="true" onselectedindexchanged="filterfloor_SelectedIndexChanged1" 
                        >
                    </asp:DropDownList>
                </label>
                 </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="850Px">
                                                    <tr align="center">
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label24" runat="server" Font-Italic="True" Text="List of Room Types"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  <%--  <tr>
                                                        <td align="left" style="text-align: left; font-size: 14px;">
                                                            <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                            <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="GVC" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                Width="100%" DataKeyNames="ID" EmptyDataText="No Record Found." CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EnableModelValidation="True"
                                                OnPageIndexChanging="GVC_PageIndexChanging" OnRowCommand="GVC_RowCommand" OnRowDeleting="GVC_RowDeleting"
                                                OnRowEditing="GVC_RowEditing">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="School Name" SortExpression="SectionName" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrSchool" runat="server" Text='<%# Bind("School") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Building Name" SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labesdsdl6" runat="server" Text='<%# Eval("BuildingName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Floor Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfloornname" runat="server" Text='<%# Eval("floorname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Room Type">
                                                     <ItemTemplate>
                                                            <asp:Label ID="lblroomtypenname" runat="server" Text='<%# Eval("RoomType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/UserControl/images/edit.gif"
                                                        HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/UserControl/images/edit.gif"
                                                                runat="server" ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" Width="2%"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                        HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/UserControl/images/delete.gif"
                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" Width="2%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
           
        </fieldset>
    </div>
</asp:Content>

