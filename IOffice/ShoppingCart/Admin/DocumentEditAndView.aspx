<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/LicenseMaster.master"
    CodeFile="DocumentEditAndView.aspx.cs" Inherits="Account_DocumentEditAndView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function RealNumWithDecimal(myfield, e, dec) {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="lblim" runat="server" Height="520px" Width="100%" ScrollBars="Both">
                                    <asp:DataList ID="DataList1" runat="server">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("image")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="5" width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ibtnFirst" runat="server" ImageUrl="~/Account/images/firstpg.gif"
                                                OnClick="ibtnFirst_Click" Visible="False" />
                                            <asp:ImageButton ID="IbtnPrev" runat="server" ImageUrl="~/Account/images/prevpg.gif"
                                                OnClick="IbtnPrev_Click" Visible="False" />
                                            <asp:ImageButton ID="IbtnNext" runat="server" ImageUrl="~/Account/images/nextpg.gif"
                                                OnClick="IbtnNext_Click" Visible="False" />
                                            <asp:ImageButton ID="IbtnLast" runat="server" ImageUrl="~/Account/images/lastpg.gif"
                                                OnClick="IbtnLast_Click" Visible="False" /><br />
                                            <asp:Panel ID="pnlmsg" runat="server" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblmsg" runat="server" ForeColor="#FF3300"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        
                                            <fieldset>
                                                <legend></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Business Name
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlbusiness" runat="server" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label10" runat="server" Text="Document Type"></asp:Label>
                                                                <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RiredFiealidator2" runat="server" ControlToValidate="ddldt"
                                                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddldt_SelectedIndexChanged" Width="120px">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                    Width="20px" AlternateText="Add New" Height="20px" ToolTip="AddNew" OnClick="ImageButton1_Click" />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                    AlternateText="Refresh" Height="20px" Width="20px" ToolTip="Refresh" OnClick="ImageButton2_Click" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Doc Upload Date
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="lblUploadDate" runat="server"></asp:Label>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Party Name
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlpartyname"
                                                                    Display="Dynamic" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlpartyname" runat="server" ValidationGroup="1">
                                                                </asp:DropDownList>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton50" runat="server" Height="18px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                                                    OnClick="ImageButton50_Click" ImageAlign="Bottom" ToolTip="AddNew" Width="20px" />
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="ImageButton51" runat="server" Height="18px" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                                                    OnClick="ImageButton51_Click" ImageAlign="Bottom" ToolTip="Refresh" Width="20px" />
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label20" runat="server" Text="Party Doc Ref.No."></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequicmnredFieldValidator2" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtpartdocrefno" ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegulValidator2" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtpartdocrefno" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                              <%--  <asp:TextBox ID="txtpartdocrefno" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span2',100)"
                                                                    MaxLength="100" ValidationGroup="1" Width="80px" TabIndex="5"></asp:TextBox>
                                                    --%>        <asp:TextBox ID="txtpartdocrefno" runat="server" 
                                                                    MaxLength="100" ValidationGroup="1" Width="80px" TabIndex="5"></asp:TextBox>
                                                      </label>
                                                         <%--   <label>
                                                                <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span2" class="labelcount">100</span>
                                                                <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                            </label>--%>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label12" runat="server" Text="Doc Ref.No."></asp:Label>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                    ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                               <%-- <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                                                    MaxLength="100" ValidationGroup="1" Width="80px" TabIndex="5"></asp:TextBox>
                                          --%>                  <asp:TextBox ID="txtdocrefnmbr" runat="server"
                                                                    MaxLength="100" ValidationGroup="1" Width="80px" TabIndex="5"></asp:TextBox>
                                           </label>
                                                           <%-- <label>
                                                                <asp:Label ID="Label13" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="sp11" class="labelcount">100</span>
                                                                <asp:Label ID="Label18" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                            </label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Cabinet-Drawer-Folder
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldoctype"
                                                                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="*" ValidationGroup="1"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </label>
                                                        </td>
                                                        <td colspan="3">
                                                            <label>
                                                                <asp:DropDownList ID="ddldoctype" runat="server" ValidationGroup="1" Width="407px"
                                                                    DataTextField="doctype" DataValueField="DocumentTypeId">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Net Amount
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtnetamount" runat="server" onkeypress="return RealNumWithDecimal(this,event,2);"
                                                                    Width="80px" MaxLength="20"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtnetamount" ValidChars="0123456789.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:Label ID="Label7" runat="server" Text="Document Title"></asp:Label>
                                                                <asp:Label ID="Label8" runat="server" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdoctitle"
                                                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9/.$\s]*)"
                                                                    ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td colspan="3">
                                                            <label>
                                                                <%--<asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_$ \s]+/,'div1',100)"
                                                                    runat="server" ValidationGroup="1" Width="400px" MaxLength="100" TabIndex="2"></asp:TextBox>
                                              --%>             
                                              <asp:TextBox ID="txtdoctitle" 
                                                                    runat="server" ValidationGroup="1" Width="400px" MaxLength="100" TabIndex="2"></asp:TextBox>
                                              
                                               </label>
                                                            <%--<label>
                                                                <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="div1" class="labelcount">100</span>
                                                                <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                            </label>--%>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Document Date
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                    ControlToValidate="txtDate" ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="rghjk" Display="Dynamic" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtDate" ValidationGroup="1" SetFocusOnError="true" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtDate" runat="server" ValidationGroup="1" Width="80px"></asp:TextBox>
                                                            </label>
                                                            <label>
                                                                <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                            </label>
                                                            <label>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtncal"
                                                                    TargetControlID="txtDate">
                                                                </cc1:CalendarExtender>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Doc Description
                                                            </label>
                                                        </td>
                                                        <td colspan="3">
                                                            <label>
                                                               <%-- <asp:TextBox ID="txtdocdscrptn" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_$ \s]+/,'Span1x',500)"
                                                                    runat="server" ValidationGroup="1" Width="400px" MaxLength="500" ></asp:TextBox>
                           --%>                                  <asp:TextBox ID="txtdocdscrptn" 
                                                                    runat="server" ValidationGroup="1" Width="400px" MaxLength="500" ></asp:TextBox>
                           </label>
                                                           <%-- <label>
                                                                <asp:Label ID="Label2" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                <span id="Span1x" class="labelcount">500</span>
                                                                <asp:Label ID="Label3" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                            </label>--%>
                                                        </td>
                                                        <td colspan="2" valign="bottom">
                                                         <asp:Button ID="btncon" runat="server"  Text="Confirm"
                                                                CssClass="btnSubmit" ValidationGroup="1" onclick="btncon_Click" />
                                                            <asp:Button ID="imgbtnupdate" runat="server" OnClick="imgbtnupdate_Click" Text="Update"
                                                                CssClass="btnSubmit" ValidationGroup="1" Visible="false" />
                                                        </td>
                                                        <td valign="bottom">
                                                            <input id="hdnDocId" runat="server" name="HdnDocId" type="hidden" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
