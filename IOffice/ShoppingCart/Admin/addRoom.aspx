<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/LicenseMaster.master"
    CodeFile="addRoom.aspx.cs" Inherits="addRoom" Title="Add Room" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
&nbsp;
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

        function mask(evt) {

            if (evt.keyCode == 13) {


            }
            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode ==186) {


               
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
               
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
        </script>
<script language="javascript" type="text/javascript">

    function mak(id, max_len, myele) {
        counter = document.getElementById(id);

        if (myele.value.length <= max_len) {
            remaining_characters = max_len - myele.value.length;
            counter.innerHTML = remaining_characters;
        }
    }

    function checktextboxmaxlength(txt, maxLen, evt) {
        try {
            if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                return false;
        }
        catch (e) {

        }
    }
    function mask(evt) {

        if (evt.keyCode == 13) {


        }
        if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


          
            return false;
        }

    }
    function check(txt1, regex, reg, id, max_len) {
        if (txt1.value.length > max_len) {

            txt1.value = txt1.value.substring(0, max_len);
        }
        if (txt1.value != '' && txt1.value.match(reg) == null) {
            txt1.value = txt1.value.replace(regex, '');
       
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
                        <asp:Button ID="btnadddd" runat="server" Text="Add Room" OnClick="btnadddd_Click"
                            Width="180px" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Visible="false">
                        <table width="100%">
                     
                               <tr>
                          <td style="width: 25%">
                            <label>
School Name
                                <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlschool"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>

                             <td>
                             <label>
                <asp:DropDownList ID="ddlschool" runat="server" AutoPostBack="true" 
                                     onselectedindexchanged="ddlschool_SelectedIndexChanged">

                                  </asp:DropDownList>
                                            </label>
                                             
                                </td>
                            </tr>
                            <tr>
                            
                                <td style="width: 25%">
                            <label>
                               Building
                                <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlbuilding"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                                  <td>
                                  <label>
                                       <asp:DropDownList ID="ddlbuilding" runat="server" AutoPostBack="true" 
                                          onselectedindexchanged="ddlbuilding_SelectedIndexChanged">

                                  </asp:DropDownList>
                                       </label>
                                
                                          

                                </td>
                              

                            </tr>
                            <tr>
                          <td style="width: 25%">
                            <label>
Floor
                                <asp:Label ID="asdsadsad" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlfloor"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>

                             <td>
                             <label>
                <asp:DropDownList ID="ddlfloor" runat="server" AutoPostBack="true" 
                                     onselectedindexchanged="ddlfloor_SelectedIndexChanged">

                                  </asp:DropDownList>
                                            </label>
                                             
                                </td>
                            </tr>
                              <tr>
                         <td style="width: 25%">
                            <label>
Room Type
                                <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRoomtype"
                                    ValidationGroup="save" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                            </label>
                        </td>

                             <td>
                             <label>
                <asp:DropDownList ID="ddlRoomtype" runat="server" AutoPostBack="true" 
                                     onselectedindexchanged="ddlRoomtype_SelectedIndexChanged">

                                  </asp:DropDownList>
                                            </label>
                                               

                                    
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
                                   Room No
                                          <asp:Label ID="Label13" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRoomno"
                                            SetFocusOnError="true" ValidationGroup="save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtRoomno"
                                            ValidationGroup="save"></asp:RegularExpressionValidator>

                                    </label>
                                </td>
                                <td>
                           <%--  <asp:TextBox ID="txtRoomno" runat="server" MaxLength="50"    onkeyup="return check(this,^\d+)" 
                                            Width="200px"  ></asp:TextBox>--%>

                                <label>
                                                                            
                                             <asp:TextBox ID="txtRoomno" runat="server" MaxLength="10"  onKeydown="return mask(event)"
                                            Width="200px" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'Span11',10)"></asp:TextBox>
                                 </label>

                                <label>
                                       <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="Max"></asp:Label>
                                            <span id="Span11" cssclass="labelcount">10</span>
                                              <asp:Label ID="Label6" runat="server" CssClass="labelcount" Text="(A-Z 0-9)"></asp:Label>                        
                                </label>
                                </td>

                            </tr>
                             <tr align="left">
                <td>
                <label>Available for Educational Purpose</label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActiveInactive" runat="server" AutoPostBack="true" 

                                             >
      
                                  <asp:ListItem Value="1">Available</asp:ListItem>
                                  <asp:ListItem Value="0">Unavailable</asp:ListItem>
      
                                  </asp:DropDownList>
                </td>
            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" Text="Submit" ValidationGroup="save"
                                        CssClass="btnSubmit" onclick="btnsave_Click" TabIndex="5" />
                                    <asp:Button ID="btnupdate" runat="server" Text="Update"  CssClass="btnSubmit" ValidationGroup="save"
                                        Visible="false" onclick="btnupdate_Click"  />
                                    <asp:Button ID="btncancel" runat="server"  Text="Cancel"
                                        CssClass="btnSubmit" onclick="btncancel_Click"  />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>


                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Rooms"></asp:Label>
                    </legend>
                      <div id="divgrid" runat="server" visible="true" style="margin: 10px;">
            <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" 
                            Text="Printable Version" onclick="Button1_Click"
                          />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
              
                    <table width="100%">

                        <tr>
                       <td style="padding-left:5px;width:35%;">
                <label style="width: 200px;">
                    Filter by School Name
                </label>
                <label>
                    <asp:DropDownList ID="filterschool" runat="server" AutoPostBack="True" onselectedindexchanged="filterschool_SelectedIndexChanged" 
                         >
                    </asp:DropDownList>
                </label>
                 </td>
    <td style="width:35%;">
                <label style="width: 200px;">
                    Filter by Building Name 
                </label>
                <label>
                    <asp:DropDownList ID="filterbuilding" runat="server" AutoPostBack="true" onselectedindexchanged="filterbuilding_SelectedIndexChanged"
                         >
                    </asp:DropDownList>
                </label>
                 </td> 
                 </tr>
                 <tr>
                  <td style="padding-left:5px;width:40%;">
                <label style="width: 200px;">
                    Filter by Floor Name
                </label>
                <label>
                    <asp:DropDownList ID="filterfloor" runat="server" AutoPostBack="true" onselectedindexchanged="filterfloor_SelectedIndexChanged1" 
                        >
                    </asp:DropDownList>
                </label>
                 </td>
                    <td style="width:40%;">
                <label style="width: 200px;">
                    Filter by Room Type
                </label>
                <label>
                    <asp:DropDownList ID="filterroomtype" runat="server" AutoPostBack="true" onselectedindexchanged="filterroomtype_SelectedIndexChanged" 
                        >
                    </asp:DropDownList>
                </label>
                 </td>
                 </tr>
                 <tr>
                 <%--<td style="padding-left:5px;width:35%;">
                <label>
                    Filter by Room Type
                </label>
                <label>
                    <asp:DropDownList ID="filterroomtype" runat="server" AutoPostBack="True" 
                         >
                    </asp:DropDownList>
                </label>
                 </td>--%>
                           <td style="padding-left:5px;width:40%;">
                                <label style="width: 200px;">
                                    Status
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstatus_search" runat="server" 
                                    AutoPostBack="True" onselectedindexchanged="ddlstatus_search_SelectedIndexChanged"
                                      >
                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                        <asp:ListItem Value="0">Unavailable</asp:ListItem>
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
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="True" 
                                                                    Text="List of Rooms" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="Labdfk2" runat="server" Font-Italic="true" Text="Status : "></asp:Label>
                                                                <asp:Label ID="lblstat" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="GVC" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="RoomID" EmptyDataText="No Record Found." 
                                                  
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    EnableModelValidation="True" onpageindexchanging="GVC_PageIndexChanging" 
                                                    onrowcommand="GVC_RowCommand" onrowdeleting="GVC_RowDeleting" onrowediting="GVC_RowEditing" 
                                                    >
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                      
                                                          <asp:TemplateField HeaderText="School Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblschhool" runat="server" Text='<%# Bind("School") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Building Name" HeaderStyle-HorizontalAlign="left" 
                                                            HeaderStyle-Width="20%" >
                                                           <ItemTemplate>
                                                                <asp:Label ID="lblbuilding" runat="server" Text='<%# Bind("BuildingName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="20%" 
                                                            HeaderText="Floor Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfloor" runat="server" Text='<%# Eval("floorname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Room No" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblroomno" runat="server" Text='<%# Bind("RoomNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="RoomType" HeaderStyle-HorizontalAlign="left"
                                                          HeaderStyle-Width="20%"  >
                                                           <ItemTemplate>
                                                                <asp:Label ID="lblRoomType" runat="server" Text='<%# Bind("RoomType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                          </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Available"  HeaderStyle-HorizontalAlign="left"
                                                          HeaderStyle-Width="20%"  >
                                                        <ItemTemplate>
                                                                <asp:Label ID="lblAvailable" runat="server" Text='<%# Bind("Available") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        
                                                         <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/UserControl/images/edit.gif"
                                                    HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/UserControl/images/edit.gif"
                                                            runat="server" ToolTip="Edit" CommandArgument='<%# Eval("RoomID") %>' CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="left" Width="2%"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("RoomID") %>'
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


