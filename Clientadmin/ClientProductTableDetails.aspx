<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ClientProductTableDetails.aspx.cs" Inherits="Productportalmaster2"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

  
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        .style1
        {
            width: 100%;
        }
        </style>
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </legend>
                        <div style="float: right;">
                            <asp:Button ID="addnewpanel" runat="server" Text="Add New Client Product Table Details"
                                CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                                <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit" 
                            OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                        </div>
                    </fieldset></asp:Panel>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbladd" runat="server" Text=" Client Product Table Details"></asp:Label>
                        </legend>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                           <asp:Label ID="Label9" runat="server" Text="Product/Version"></asp:Label>
                                        <asp:Label ID="Label69" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvname0" runat="server" ControlToValidate="ddlProductVersion"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlProductVersion" runat="server"  Width="200px" onselectedindexchanged="ddlProductVersion_SelectedIndexChanged"  AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                              <tr>
                                <td style="width: 35%">
                                    <label>
                                         <asp:Label ID="Label10" runat="server" Text="Product Description :"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    
                                    <asp:Label ID="Label11" runat="server" ></asp:Label>
                                    <%--<asp:TextBox ID="Label11" TextMode="MultiLine" Width="600px" Height="45px"  runat="server" Enabled="False"></asp:TextBox>--%>
                                         
                                    
                                    <asp:Label ID="Label23" runat="server"></asp:Label>
                                         
                                    
                                </td>
                            </tr>
                                <tr>
                                <td style="width: 35%">
                                <label>
                                    <asp:Label ID="Label12" runat="server" Text="Database :"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="DDLCodetype" runat="server" Width="200px" onselectedindexchanged="DDLCodetype_SelectedIndexChanged"  AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:Image ID="Image1" runat="server" Visible="False" />
                                         <asp:Label ID="Label35" runat="server"></asp:Label>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Visible="false" onclick="LinkButton2_Click">Click Here to View</asp:LinkButton>
                                    </label>
                                <label>
                                   
                                </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <label>
                                    <asp:Label ID="Label13" runat="server" Text="Table Category :"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                    <asp:DropDownList ID="DDLTableCategory" runat="server" AutoPostBack="True" Width="200px">
                                    </asp:DropDownList>                              
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                  <label>
                                        <asp:Label ID="lbltb" runat="server" Text="Table Name"></asp:Label>
                                        <asp:Label ID="Label26" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttable" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txttable" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([\._a-zA-Z0-9\s]*)" ValidationGroup="1"></asp:RegularExpressionValidator>
                                     
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txttable" nKeydown="return mask(event)" 
                                        onkeypress="return checktextboxmaxlength(this,50,event)" Width="200px"
                                            onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+;={}[]|\/]/g,/^[\ _a-zA-Z.0-9\s]+$/,'Span1',50)"  runat="server"   MaxLength="50"
                                            ValidationGroup="1" AutoPostBack="True"  ontextchanged="txttable_TextChanged"></asp:TextBox>
                                    </label>
                                    <label>
                                     <asp:Label ID="Label36" runat="server" CssClass="labelstar"></asp:Label>
                                    </label>
                                    <label id="l1" runat="server">
                                        <asp:Label ID="Label4" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                        <span id="Span1" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label5" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                      <label>
                                        <asp:Label ID="Label2" runat="server" Text="Table Title"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttitle"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttitle"
                                            Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([\._a-zA-Z0-9\s]*)"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                     
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txttitle" nKeydown="return mask(event)" onkeypress="return checktextboxmaxlength(this,50,event)" Width="200px"
                                            onkeyup="return check(this,/[\\/!@#$%^'&amp;*()&gt;+;={}[]|\/]/g,/^[\ _a-zA-Z.0-9\s]+$/,'Span2',50)"
                                            runat="server"   MaxLength="50"
                                            ValidationGroup="1"></asp:TextBox>
                                    </label>
                                    <label id="Label6" runat="server">
                                        <asp:Label ID="Label7" runat="server" CssClass="labelcount" Text="Max "></asp:Label>
                                        <span id="Span2" cssclass="labelcount">50</span>
                                        <asp:Label ID="Label8" runat="server" CssClass="labelcount" Text="(A-Z 0-9 . _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel11" runat="server" Visible="false">
                                            <table class="style1">
                                                <tr>
                                                    <td width="35%">
                                                        <label>
                                                        <asp:Label ID="Label369" runat="server" Text="Store Procedure"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        <asp:Label ID="Label371" runat="server" ></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="35%">
                                                        <label>
                                                        <asp:Label ID="Label370" runat="server" Text="Page name"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        <asp:Label ID="Label372" runat="server"></asp:Label>
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                            </tr>
                               
                               
                                <tr>
                                <td style="width: 35%">
                                   <label>
                                       <asp:Label ID="Label24" runat="server" Text="Do you wish to add fields to this table?"></asp:Label>
                                   </label></td>
                                <td>
                                  <label>
                                  
                                      <asp:CheckBox ID="CheckBox3" runat="server" Text="Yes" AutoPostBack="True"  oncheckedchanged="CheckBox3_CheckedChanged" /></label></td>
                            </tr>
                                <tr>                                    
                                    <td colspan="2">
                                        <asp:Panel ID="Panel9" runat="server" Visible="false" GroupingText="Table Field List">
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel10" runat="server" Visible="false" GroupingText="Add Table Field Name">

                                                            

                                                            <table class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvaddnew" runat="server" AutoGenerateColumns="False" 
                                                                CssClass="mGrid">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Field Name" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_FieldName" runat="server" Width="150px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlfiledtype" runat="server" Width="150px">
                                                                                <asp:ListItem Selected="True" Text="int" Value="int"></asp:ListItem>
                                                                                <asp:ListItem Text="bigint" Value="bigint"></asp:ListItem>
                                                                                <asp:ListItem Text="nvarchar" Value="nvarchar"></asp:ListItem>
                                                                                <asp:ListItem Text="binary" Value="binary"></asp:ListItem>
                                                                                <asp:ListItem Text="bit" Value="bit"></asp:ListItem>
                                                                                <asp:ListItem Text="char" Value="char"></asp:ListItem>
                                                                                <asp:ListItem Text="date" Value="date"></asp:ListItem>
                                                                                <asp:ListItem Text="datetime" Value="datetime"></asp:ListItem>
                                                                                <asp:ListItem Text="datetime2(7)" Value="datetime2(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="datetimeoffset(7)" Value="datetimeoffset(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="decimal(18, 0)" Value="decimal(18, 0)"></asp:ListItem>
                                                                                <asp:ListItem Text="float" Value="float"></asp:ListItem>
                                                                                <asp:ListItem Text="geography" Value="geography"></asp:ListItem>
                                                                                <asp:ListItem Text="geometry" Value="geometry"></asp:ListItem>
                                                                                <asp:ListItem Text="float" Value="float"></asp:ListItem>
                                                                                <asp:ListItem Text="image" Value="image"></asp:ListItem>
                                                                                <asp:ListItem Text="numeric(18, 0)" Value="numeric(18, 0)"></asp:ListItem>
                                                                                <asp:ListItem Text="real" Value="real"></asp:ListItem>
                                                                                <asp:ListItem Text="time(7)" Value="time(7)"></asp:ListItem>
                                                                                <asp:ListItem Text="real" Value="real"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Size" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsize" runat="server" Width="60px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Primary Key" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkPK" runat="server" oncheckedchanged="ChkPK_CheckedChanged" AutoPostBack="True" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:TemplateField HeaderText="Foreign Key" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkFK" runat="server" oncheckedchanged="ChkFK_CheckedChanged" AutoPostBack="True" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Foreign Key Table Name" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="DDLgvFKTableName" runat="server" Width="100px" AutoPostBack="True" onselectedindexchanged="DDLgvFKTableName_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Foreign key Field Name" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="DDLgvFKFieldName" runat="server" Width="100px">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Null Value Allowed" ItemStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkAllowNull" runat="server" Checked="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Is Identity (Auto Number)" ItemStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_identity" runat="server"  />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btn_storefield" runat="server" Text="Add" onclick="btn_storefield_Click" /></td>
                                                                </tr>
                                                            </table>

                                                            

                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Button ID="btnaddnewfield" runat="server" Text="Add New" onclick="btnaddnewfield_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="Gv_TableField" runat="server" AutoGenerateColumns="False" 
                                                            CssClass="mGrid" onrowcommand="Gv_TableField_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Field Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label25" runat="server" Text='<%#Eval("feildname")%>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label27" runat="server" Text='<%#Eval("type")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label28" runat="server" Text='<%#Eval("size")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Primary Key">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label29" runat="server" Text='<%#Eval("primarykey")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Foreign Key">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label31" runat="server" Text='<%#Eval("foreignkey")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText=" Foreign Key Table Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label32" runat="server" Text='<%#Eval("keytable")%>'></asp:Label>
                                                                       <asp:Label ID="Label351" runat="server" Text='<%#Eval("keytableid")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Foreign key Field Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label33" runat="server" Text='<%#Eval("keyfeild")%>'></asp:Label>
                                                                         <asp:Label ID="Label354" runat="server" Text='<%#Eval("keyfeildid")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Null Value Allowed">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label34" runat="server" Text='<%#Eval("nullvalue")%>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Is Identity (Auto Number)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvAutoNumber" runat="server" Text='<%#Eval("autunumber")%>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                
                                                                <asp:ButtonField ButtonType="Image" CommandName="del" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                            </tr>
                                <tr>
                                    <td style="width: 35%">
                                        <label>
                                        <asp:Label ID="Label15" runat="server" Text="Active :"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                        </label>
                                    </td>
                            </tr>
                                <tr>
                                <td style="width: 35%">
                               
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                </td>
                                <td>
                                    <label>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="BtnUpdate" runat="server" CssClass="btnSubmit" Text="Update" ValidationGroup="1" Visible="False" OnClick="BtnUpdate_Click1" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="btnCancel_Click" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </fieldset></asp:Panel>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label18" runat="server" Text="List of Client  Product Table Details"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version" Visible="false"   OnClick="Button1_Click1" />
                        <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"  style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                          <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Product Name :"></asp:Label>
                                        <asp:DropDownList ID="DDL_ProductversionSearch" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList4_SelectedIndexChanged" Width="180px">
                                        </asp:DropDownList>
                                </label>
              
                             <label>
                                    <asp:Label ID="Label17" runat="server" Text="Database :"></asp:Label>
                                    <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True"  onselectedindexchanged="DropDownList6_SelectedIndexChanged" Width="180px">
                                    </asp:DropDownList>
                                </label>            
                              <label>
                                    <asp:Label ID="Label19" runat="server" Text="Category  :" ></asp:Label>
                                    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList5_SelectedIndexChanged" Width="180px">
                                    </asp:DropDownList>
                                </label>              
                              <label>
                                    <asp:Label ID="Label20" runat="server" Text=" Active :"></asp:Label>
                                    <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList7_SelectedIndexChanged" Width="180px">                   
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="0">Deactive</asp:ListItem>                   
                                    </asp:DropDownList>
                             </label>
                              </td>
                        </tr>
                        <tr>
                          <td>
                              <label>
                                  <asp:Label ID="Label21" runat="server" Text="Search :"></asp:Label>                                 
                                          <asp:TextBox ID="TextBox1" runat="server" Width="180px"></asp:TextBox></label>
                              </label>                            
                              <label>
                                    <br />
                                   <asp:Button ID="Button15" runat="server" Text="Go" onclick="Button15_Click" />
                              </label>               
                         </td>
                        </tr>
                        <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="850Px">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                          
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label30" runat="server" Font-Italic="true" Text="List of Client Product Table Details"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                        EmptyDataText="No Record Found." AllowPaging="True" Width="100%" CssClass="mGrid"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" EnableSortingAndPagingCallbacks="True"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                        OnRowEditing="GridView1_RowEditing1" OnRowUpdating="GridView1_RowUpdating" 
                                        OnRowDeleting="GridView1_RowDeleting1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Name" SortExpression="VersionInfoName" ItemStyle-Width="12%" Visible="false"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproductname" runat="server" Text='<%# Bind("productversion")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Database "  Visible="false" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("databasename") %>' Enabled="false"
                                                ForeColor="Black" ></asp:LinkButton>
                                         <asp:Label ID="Lael32" runat="server" Text='<%# Eval("id") %>' Visible="false" ></asp:Label>
                                           <%--  <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Category " SortExpression="Name" Visible="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael33" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                             <%--<asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Table Name" SortExpression="Table Name" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltname" runat="server" Text='<%# Bind("TableName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Table Title" SortExpression="TableTitle" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemaildisplaynameName" runat="server" Text='<%# Bind("TableTitle")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                           
                                      <asp:TemplateField HeaderText="Parent Table  " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael34" runat="server" Text='<%# Eval("parenttable") %>'></asp:Label>
                                             <%--<asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                          <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stored  procedure">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label373" runat="server" Text='<%#Eval("sp")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                            </asp:TemplateField>

                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="15px"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lbladdd" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                                CommandName="view" ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg"
                                                                Width="20px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="2%" />
                                                    </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View Records" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" CommandArgument='<%# Eval("Id") %>' ToolTip="View Records" CommandName="ViewRecords"
                                                       >View</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" runat="server" CommandArgument='<%# Eval("Id") %>' ToolTip="Edit" CommandName="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/trash.jpg" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"  OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>

                            <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="65%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <label>
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdsync" runat="server">
                                                                    <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                        Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                            </td>
                            </tr>
                             <tr>
                            <td>
                              <asp:Panel ID="Panel3" runat="server" Width="65%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                           
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                        Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" >
                                                    <asp:Label ID="Label355" runat="server" Text="Database Name" ForeColor="Black"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        :<asp:Label ID="Label360" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td width="50%" >
                                                    <asp:Label ID="Label356" runat="server" Text="Sql Server" ForeColor="Black"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        :<asp:Label ID="Label361" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" >
                                                    <asp:Label ID="Label358" runat="server" Text="Instance" ForeColor="Black"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        :<asp:Label ID="Label365" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td width="50%" >
                                                    <asp:Label ID="Label359" runat="server" Text="User name" ForeColor="Black"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        :<asp:Label ID="Label366" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td width="50%" >
                                                    <asp:Label ID="Label364" runat="server" Text="Password" ForeColor="Black"></asp:Label>
                                                </td>
                                                    <td width="50%" >
                                                        :<asp:Label ID="Label367" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <asp:Label ID="Label363" runat="server" Text="Port" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td width="50%">
                                                    :<asp:Label ID="Label368" runat="server" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" >
                                                    &nbsp;</td>
                                                    <td width="50%" >
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel3" TargetControlID="Button5" CancelControlID="ImageButton1">
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
