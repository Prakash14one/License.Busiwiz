<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="DoorAccess.aspx.cs" Inherits="ShoppingCart_Admin_DoorAccess" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

        function checktextboxmaxlength1(txt, maxLen, evt) {
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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {

                if (evt.srcElement.value > 255) {
                    txt.value = txt.value.substring(0, maxLen - 1);
                    return false;
                }

            }
            catch (e) {

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
             <div style="float: left;padding:1%;">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                <fieldset>
                <legend><asp:Label ID="lblms" runat="server" Text="Set Door/IP Based Device Access Rights"></asp:Label></legend>
                 <div style="clear: both;">
                  <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                      
                    </div>
                     <label style="width:20%;">
                            <asp:Label ID="Label5" runat="server" Text="Select Business">
                            </asp:Label> <asp:Label ID="Label40" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlwarehouse"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </label>
                             <label>
                     <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlwarehouse_SelectedIndexChanged" >
                            
                                </asp:DropDownList>
                        
                    </label>
                     <div style="clear: both;">
                     <label  style="width:20%;"></label>
                     <asp:RadioButtonList ID="rdlist" runat="server" RepeatDirection="Horizontal" 
                             AutoPostBack="True" onselectedindexchanged="rdlist_SelectedIndexChanged">
                     <asp:ListItem Text="Select Designation" Value="1"></asp:ListItem>
                       <asp:ListItem Text="Select Employee" Value="2"></asp:ListItem>
                  
                     </asp:RadioButtonList>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pndesi" runat="server" Visible="false">
                    <label  style="width:20%;">
                       <asp:Label ID="Label2" runat="server" Text="Designation">
                            </asp:Label> <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldesig"
                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
               
                    </label>
                    <label>  <asp:DropDownList ID="ddldesig" runat="server" 
                            onselectedindexchanged="ddldesig_SelectedIndexChanged" 
                            AutoPostBack="True" >
                            
                                </asp:DropDownList>
                        </label>
                    </asp:Panel>
                    
                     <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlemp" runat="server" Visible="false">
                    <label  style="width:20%;">
                       <asp:Label ID="Label4" runat="server" Text="Employees">
                            </asp:Label> <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlemp"
                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
               
                    </label>
                    <label>  <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlemp_SelectedIndexChanged" >
                            
                                </asp:DropDownList>
                        </label>
                    </asp:Panel>
                       <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgd" runat="server" Visible="false">
                     <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="The selected Designation has the following Door/IP Based Device Access Rights"  runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                       
                            <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                            <input id="Button3" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" class="btnSubmit" visible="False" />
                        </label>
                     
                    </div>
               
                    <div style="clear: both;">
                  
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <tr align="center">
                            <td>
                                <div id="mydiv" class="closed">
                                    <table width="100%">
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                <asp:Label ID="lblCompany" runat="server" Font-Italic="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="Label19" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" ForeColor="Black">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                         <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="lbltype" runat="server" Text="" Font-Italic="true"></asp:Label>
                                             
                                               
                                            </td>
                                        </tr>
                                         <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="lblemptext" runat="server" Font-Italic="True"></asp:Label>
                                                <asp:Label ID="lblemp" runat="server" Font-Italic="True" ForeColor="Black">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                <asp:Label ID="lblghead" runat="server" Font-Italic="True" Text="The selected Designation has the following Door/IP Based Device Access Rights"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                  EmptyDataText="No Record Found. " CssClass="mGrid"
                                    GridLines="Both" PagerStyle-CssClass="pgr" 
                                    AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                    HorizontalAlign="Left" Width="100%" OnSorting="GridView1_Sorting" 
                                    >
                                    <Columns>
                                       
                                       
                                          <asp:TemplateField HeaderText="Door/IP Based Device Name" SortExpression="DoorName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldoorname" runat="server" Text='<%#Bind("DoorName") %>'></asp:Label>
                                                <asp:Label ID="lbldoorid" runat="server" Text='<%#Bind("DoorId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Door/IP Based<br> Device Number" SortExpression="DoorNo" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcous" runat="server" Text='<%#Bind("DoorNo") %>'  ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                                            Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblloca" runat="server" Text='<%#Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Left">
                  
                  <HeaderTemplate><asp:CheckBox ID="cbHeader"  runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                  <br /> <asp:Label ID="check" runat="server"  Text="Right To Open"/> </HeaderTemplate>
                  <ItemTemplate><asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>' runat="server" />
                  <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server"   Visible="false"/>
                  </ItemTemplate>
                  </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Left">
                  
                  <HeaderTemplate><asp:CheckBox ID="cbHeader1"  runat="server" OnCheckedChanged="ch2_chachedChanged" AutoPostBack="true" /> <br />  <asp:Label ID="check1" runat="server"  Text="Right To Start"/> </HeaderTemplate>
                  <ItemTemplate><asp:CheckBox ID="cbItem1" Checked='<%# Bind("chk1") %>' runat="server" />
                  <asp:CheckBox ID="chkdef1" Checked='<%# Bind("chk1") %>' runat="server"   Visible="false"/>
                  </ItemTemplate>
                  </asp:TemplateField>
                                    
                                             <asp:TemplateField  HeaderStyle-HorizontalAlign="Left">
                  
                  <HeaderTemplate><asp:CheckBox ID="cbHeader2"  runat="server" OnCheckedChanged="ch3_chachedChanged" AutoPostBack="true" />  <br /> <asp:Label ID="check2" runat="server" Text="Right To Stop"/> </HeaderTemplate>
                  <ItemTemplate><asp:CheckBox ID="cbItem2" Checked='<%# Bind("chk2") %>' runat="server" />
                  <asp:CheckBox ID="chkdef2" Checked='<%# Bind("chk2") %>' runat="server"   Visible="false"/>
                  </ItemTemplate>
                  </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </asp:Panel>
                    <div style="clear: both;">
                    <br />
                    </div>
               <div style="clear: both;">
                 <table width="100%";>
                 <tr>
                 <td align="center">
                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btnSubmit" 
                         onclick="btnsave_Click" />
                 </td>
                 </tr>
                 </table>
                
                    </div>
                </fieldset></asp:Panel>
                   
</fieldset>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

