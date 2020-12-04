<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/LicenseMaster.master"
    CodeFile="FilingDeskViewApprove.aspx.cs" Inherits="FilingDeskViewApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <script language="javascript" type="text/javascript">
        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186 || evt.keyCode == 59) {


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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="float: right;">
                                    <label>
                                        <asp:Label ID="lbldcla" runat="server" Text="Document Title: "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblddcname" runat="server" Text=""></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Page Number"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="lblnooff" runat="server" Width="50px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblfrom" runat="server" Text=" Of "></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblnototal" runat="server"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Button ID="imgimgo" runat="server" OnClick="imgimgo_Click" Text="Go" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgfirstimg" runat="server" ImageUrl="~/Account/images/firstpg.gif"
                                            AlternateText="First" OnClick="imgfirstimg_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgpriimg" runat="server" ImageUrl="~/Account/images/prevpg.gif"
                                            AlternateText="Privious" OnClick="imgpriimg_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgnextimg" runat="server" ImageUrl="~/Account/images/nextpg.gif"
                                            AlternateText="Next" OnClick="imgnextimg_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imglastimg" runat="server" ImageUrl="~/Account/images/lastpg.gif"
                                            AlternateText="Last" OnClick="imglastimg_Click" />
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" OnClick="LinkButton6_Click"></asp:LinkButton>
                                    </label>
                                    <label>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="LinkButton4_Click"></asp:LinkButton>
                                    </label>
                                </div>
                                <div style="clear: both;">
                                </div>
                                <div>
                                    <asp:Panel ID="lblim" runat="server" Height="560px" Width="100%" ScrollBars="Vertical">
                                        <div>
                                            <asp:DataList ID="DataList1" runat="server">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image2" runat="server" Height="565px" Width="100%" ImageUrl='<%#Eval("image")%>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <asp:Panel ID="Panel8" runat="server" >
                                <fieldset>
                                    <legend>Edit Document Information </legend>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="6">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Label ID="lblseld" runat="server" Text="Select Document"></asp:Label>
                                                    </legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 28%">
                                                                <label>
                                                                    <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td style="width: 12%">
                                                                <label>
                                                                    <asp:DropDownList ID="ddlbusiness" runat="server" ValidationGroup="1" Width="" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                            <td style="width: 12%">
                                                                <label>
                                                                    <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td style="width: 12%">
                                                                <label>
                                                                    <asp:DropDownList ID="ddlemp" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lbldesignation" runat="server" Text="" Visible="false"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td colspan="2" style="width: 37%">
                                                                <label>
                                                                    <asp:Label ID="Label47" runat="server" Text="Document"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:TextBox ID="txtdocno" runat="server" Width="30px"></asp:TextBox>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblof" runat="server" Text="Of"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblofno" runat="server"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Button ID="lbldocbtn" runat="server" Text="Go" OnClick="lbldocbtn_Click" />
                                                                </label>
                                                                <label>
                                                                    <asp:ImageButton ID="ibtnFirst" runat="server" ImageUrl="~/Account/images/firstpg.gif"
                                                                        AlternateText="First" OnClick="ibtnFirst_Click" />
                                                                </label>
                                                                <label>
                                                                    <asp:ImageButton ID="IbtnPrev" runat="server" ImageUrl="~/Account/images/prevpg.gif"
                                                                        AlternateText="Privious" OnClick="IbtnPrev_Click" />
                                                                </label>
                                                                <label>
                                                                    <asp:ImageButton ID="IbtnNext" runat="server" ImageUrl="~/Account/images/nextpg.gif"
                                                                        AlternateText="Next" OnClick="IbtnNext_Click" />
                                                                </label>
                                                                <label>
                                                                    <asp:ImageButton ID="IbtnLast" runat="server" ImageUrl="~/Account/images/lastpg.gif"
                                                                        AlternateText="Last" OnClick="IbtnLast_Click" />
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label1" runat="server" Text="List of documents for edit/ approval"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td colspan="5">
                                                                <label>
                                                                    <asp:DropDownList ID="ddllistofdoc" Width="500px" runat="server" OnSelectedIndexChanged="ddllistofdoc_SelectedIndexChanged"
                                                                       AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="Label11" runat="server" Text="Document ID "></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lbldocidmaster" runat="server" Text=""></asp:Label>
                                                                </label>
                                                                <asp:CheckBox ID="CheckBox1" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                <label>
                                                                    <asp:Label ID="Label6" runat="server" Text="More Filter"></asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Panel ID="pnlupdatedoc" runat="server" Visible="false" Width="100%">
                                                    <fieldset>
                                                        <legend>
                                                            <asp:Label ID="Label59" runat="server" Text="Update Document Information"></asp:Label>
                                                        </legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 14%">
                                                                    <label>
                                                                        <asp:Label ID="Label60" runat="server" Text="Business Name"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlbusnessmaster" runat="server" ValidationGroup="1" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlbusnessmaster_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <label>
                                                                        <asp:Label ID="Label43" runat="server" Text="Upload Date/Time "></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td style="width: 14%">
                                                                    <label>
                                                                        <asp:Label ID="lbldocuploaddate" runat="server" Text=""></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td style="width: 14%">
                                                                    <label>
                                                                        <asp:Label ID="Label28" runat="server" Text="Document Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtDocDate"
                                                                            ErrorMessage="*" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="rgfgfhjk" runat="server" ErrorMessage="*" ControlToValidate="TxtDocDate"
                                                                            Display="Dynamic" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                                <td style="width: 14%">
                                                                    <label>
                                                                        <asp:TextBox ID="TxtDocDate" runat="server" TabIndex="1" Width="75px"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                </td>
                                                                <td style="width: 14%; font-size: 10px;">
                                                                    <div style="float: right">
                                                                        <label>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="12px" OnClick="LinkButton1_Click">Set Default Value</asp:LinkButton>
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label44" runat="server" Text="User Type"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlPartyType" TabIndex="2" AutoPostBack="true" runat="server"
                                                                            OnSelectedIndexChanged="ddlPartyType_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label10" runat="server" Text="User Name"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlpartyname" TabIndex="3" runat="server" ValidationGroup="1">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label3" runat="server" Text="Approval Status"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlapprovalstatusforupdate" TabIndex="4" runat="server">
                                                                            <asp:ListItem Selected="True" Value="0" Text="Pending-New"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <div style="float: right">
                                                                        <asp:CheckBox ID="CheckBox4" AutoPostBack="true" Font-Size="12px" runat="server"
                                                                            OnCheckedChanged="CheckBox4_CheckedChanged" />
                                                                        <label>
                                                                            <asp:Label ID="Label57" runat="server" Font-Size="12px" Text="Switch to Default"></asp:Label>
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="lbldoyou" runat="server" Text="Cabinet"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddldocmaintype" TabIndex="5" runat="server" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddldocmaintype_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label runat="server" ID="Label45" Text="Drawer "></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddldocsubtypename" TabIndex="6" runat="server" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddldocsubtypename_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label runat="server" ID="Label46" Text="Folder "></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlDocType" TabIndex="7" runat="server">
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                 <div style="float: right">
                                                                        <asp:CheckBox ID="CheckBox5" AutoPostBack="true" Font-Size="12px" runat="server"
                                                                            OnCheckedChanged="CheckBox5_CheckedChanged" />
                                                                        <label>
                                                                            <asp:Label ID="Label64" runat="server" Font-Size="12px" Text="Show More Fields"></asp:Label>
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                              <td>
                                <label>
                                    <asp:Label ID="Label58" runat="server" Text="Document Type"></asp:Label>
                                 <asp:Label ID="Labelxc" runat="server" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RiredFiealidator2" runat="server" ControlToValidate="ddldt"
                                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td  colspan="2">
                                <label>
                                    <asp:DropDownList ID="ddldt" runat="server" ValidationGroup="1" 
                                    AutoPostBack="True" onselectedindexchanged="ddldt_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                     Width="20px" AlternateText="Add New" Height="20px"
                                        ToolTip="AddNew" onclick="ImageButton1_Click" />
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                        AlternateText="Refresh" Height="20px" Width="20px"
                                        ToolTip="Refresh" onclick="ImageButton2_Click" />
                                </label>
                            </td> <td >
                                <label>
                                    <asp:Label ID="Label61" runat="server" Text="Party Doc Ref.No."></asp:Label>
                                     <asp:RequiredFieldValidator ID="RequicmnredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtpartdocrefno"
                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegulValidator2" runat="server" ErrorMessage="Invalid Character"
                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                        ControlToValidate="txtpartdocrefno" ValidationGroup="1" ></asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td colspan="2">
                                <label>
                                    <asp:TextBox ID="txtpartdocrefno" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span2',100)"
                                        MaxLength="100" ValidationGroup="1" Width="180px" TabIndex="5"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label ID="Label62" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                    <span id="Span3" class="labelcount">100</span>
                                    <asp:Label ID="Label63" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                </label>
                            </td>
                                                            </tr>
                                                               <tr>
                                                                    <td colspan="7">
                                                                        <asp:Panel ID="Panel4" runat="server" Visible="false" Width="100%">
                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:Label ID="Label35" runat="server" Text="Ref Number "></asp:Label>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                                                ControlToValidate="txtdocrefnmbr" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <label>
                                                                                            <asp:TextBox ID="txtdocrefnmbr" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'sp11',100)"
                                                                                                MaxLength="100" ValidationGroup="1"></asp:TextBox>
                                                                                        </label>
                                                                                        <label>
                                                                                            <asp:Label ID="Label36" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                            <span id="sp11" class="labelcount">100</span>
                                                                                            <asp:Label ID="Label37" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <label>
                                                                                            <asp:Label ID="Label38" runat="server" Text="Net Amount"></asp:Label>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <label>
                                                                                            <asp:TextBox ID="txtnetamount" runat="server" MaxLength="10" onKeydown="return mak('Span1',10,this)"
                                                                                                onkeypress="return RealNumWithDecimal(this,event,2);" Width="180px" TabIndex="6"></asp:TextBox>
                                                                                        </label>
                                                                                        <label>
                                                                                            <asp:Label ID="Label39" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                                            <span id="Span2" class="labelcount">10</span>
                                                                                            <asp:Label ID="Label40" runat="server" CssClass="labelcount" Text="(0-9 .)"></asp:Label>
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:Label ID="Label8" runat="server" Text="Document Title"></asp:Label>
                                                                        <asp:Label ID="Label9" runat="server" Text="*"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdoctitle"
                                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                            ControlToValidate="txtdoctitle" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    </label>
                                                                </td>
                                                             
                                                                <td colspan="4">
                                                                    <label>
                                                                        <asp:TextBox ID="txtdoctitle" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'div1',150)"
                                                                            runat="server" ValidationGroup="1" Width="500px" MaxLength="150" TabIndex="8"></asp:TextBox>
                                                                    </label>
                                                                    <label>
                                                                        <asp:Label ID="Label17" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                                        <span id="div1" class="labelcount">150</span>
                                                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                                                    </label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <label>
                                                                        <asp:Button ID="imgbtnSubmit" Text="Save & Next" ToolTip="Save & Next" TabIndex="9"
                                                                            runat="server" OnClick="imgbtnSubmit_Click" CssClass="btnSubmit" ValidationGroup="1" />
                                                                    </label>
                                                                    <label>
                                                                        <asp:Button ID="Button6" CssClass="btnSubmit" ToolTip="Next" TabIndex="10" runat="server"
                                                                            Text="Next" OnClick="Button6_Click" />
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Panel ID="Panel1" runat="server" Visible="false" Width="100%">
                                                    <fieldset>
                                                        <legend>Select the documents to view and approve </legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 40%">
                                                                    <label>
                                                                        A) Show all documents uploaded
                                                                    </label>
                                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0" Selected="True">Period</asp:ListItem>
                                                                        <asp:ListItem Value="1">Date</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="pnlperiod" runat="server" Width="100%">
                                                                        <label>
                                                                            <asp:Label ID="Label12" runat="server" Text="Period"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlDuration" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDuration_SelectedIndexChanged">
                                                                                <asp:ListItem>Today</asp:ListItem>
                                                                                <asp:ListItem>Yesterday</asp:ListItem>
                                                                                <asp:ListItem>This Week</asp:ListItem>
                                                                                <asp:ListItem Selected="True">This Month</asp:ListItem>
                                                                                <asp:ListItem>This Quarter</asp:ListItem>
                                                                                <asp:ListItem>This Year</asp:ListItem>
                                                                                <asp:ListItem>Last Week</asp:ListItem>
                                                                                <asp:ListItem>Last Month</asp:ListItem>
                                                                                <asp:ListItem>Last Quarter</asp:ListItem>
                                                                                <asp:ListItem>Last Year</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnldate" runat="server" Visible="False" Width="100%">
                                                                        <label>
                                                                            <asp:Label ID="Label14" runat="server" Text="From Date"></asp:Label>
                                                                            <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtfrom"
                                                                                ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                        <label>
                                                                            <asp:TextBox ID="txtfrom" runat="server" Width="75px"></asp:TextBox>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="imgbtncalfrom" runat="server" Width="20px" Height="20px" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label16" runat="server" Text="To Date"></asp:Label>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="txtto" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                                                        </label>
                                                                        <label>
                                                                            <asp:TextBox ID="txtto" runat="server" Width="75px"></asp:TextBox>
                                                                        </label>
                                                                        <label>
                                                                            <asp:ImageButton ID="imgbtnto" runat="server" Width="20px" Height="20px" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                        </label>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <label>
                                                                        <asp:Label ID="Label5" runat="server" Text="B) Show all documents with approval status"></asp:Label>
                                                                    </label>
                                                                    <label>
                                                                        <asp:DropDownList ID="ddlapproval" runat="server">
                                                                            <asp:ListItem Value="5" Text="Pending-All"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Value="0" Text="Pending-New"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <label>
                                                                        C) Filter the document list based on approval status of other filing desk employee
                                                                    </label>
                                                                    <asp:CheckBox ID="chkfilteronapprovalstatus" runat="server" AutoPostBack="True" OnCheckedChanged="chkfilteronapprovalstatus_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <asp:Panel Width="100%" ID="Panel3" runat="server" Visible="False">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <asp:Label ID="Label20" runat="server" Text=" Document Approved by Office Clerk"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlfilterofficeclerk" runat="server">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label24" runat="server" Text="Office Clerk's Approval Status"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlofficestatus" runat="server">
                                                                                <asp:ListItem Value="5" Selected="True" Text="Any"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="Pending-New"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label>
                                                                            <asp:Label ID="Label21" runat="server" Text="Document Approved by Supervisor"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlsupervisorfilter" runat="server">
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                        <label>
                                                                            <asp:Label ID="Label27" runat="server" Text="Supervisor's Approval Status"></asp:Label>
                                                                        </label>
                                                                        <label>
                                                                            <asp:DropDownList ID="ddlsupervisorstatus" runat="server">
                                                                                <asp:ListItem Value="5" Selected="True" Text="Any"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="Pending-New"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel Width="100%" ID="Panel6" runat="server" Visible="False">
                                                                        <div>
                                                                            <asp:CheckBox ID="chkidcolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label29" runat="server" Text="ID"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chktitlecolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label30" runat="server" Text="Thumbnail"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkfileextsion" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label31" runat="server" Text="Upload Date"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkfoldername" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label32" runat="server" Text="Title"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkpartycolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label15" runat="server" Text="Folder"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkdocumentdate" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label33" runat="server" Text="Party Name"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkrefno" runat="server" Checked="false" />
                                                                            <label>
                                                                                <asp:Label ID="Label41" runat="server" Text="Document Ref No."></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkdocamount" runat="server" Checked="false" />
                                                                            <label>
                                                                                <asp:Label ID="Label42" runat="server" Text="Document Amount"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkuploaddate" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label34" runat="server" Text="Document Date"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkmyfoldercolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label22" runat="server" Text="Office Clerk Approval"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkaddtomyfoldercolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label23" runat="server" Text="Supervisor Approval"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chkaccountentrycolumn" runat="server" Checked="true" />
                                                                            <label>
                                                                                <asp:Label ID="Label25" runat="server" Text="Approval Status"></asp:Label>
                                                                            </label>
                                                                            <asp:CheckBox ID="chksendmessagecolumn" runat="server" Checked="false" />
                                                                            <label>
                                                                                <asp:Label ID="Label26" runat="server" Text="Approval Note"></asp:Label>
                                                                            </label>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                                        <asp:GridView ID="gridocapproval" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                            AlternatingRowStyle-CssClass="alt" AllowPaging="True" AllowSorting="True" DataKeyNames="DocumentProcessingId"
                                                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnPageIndexChanging="gridocapproval_PageIndexChanging"
                                                                            PageSize="10" OnRowCommand="gridocapproval_RowCommand" OnSorting="gridocapproval_Sorting"
                                                                            Width="100%" OnRowDataBound="gridocapproval_RowDataBound" Visible="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" SortExpression="DocumentId"
                                                                                    HeaderText="ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <a id="docviewmasterid" href='ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>'
                                                                                            target="_blank">
                                                                                            <asp:Label ID="lbldocid" runat="server" ForeColor="#426172" Text='<%#Bind("DocumentId")%>'></asp:Label>
                                                                                        </a>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Thumbnail" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Image ID="Image2" Width="60px" Height="25px" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentUploadDate" DataFormatString="{0:MM/dd/yyyy}"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="8%" SortExpression="DocumentUploadDate">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Title" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="13%" SortExpression="DocumentTitle">
                                                                                    <ItemTemplate>
                                                                                        <a id="masterhr" href='DocumentEditAndView.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Did=<%= DesignationId %>'
                                                                                            target="_blank">
                                                                                            <asp:Label ID="titillabel" runat="server" ForeColor="#426172" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>'></asp:Label>
                                                                                        </a>
                                                                                        <asp:Label ID="titlemaster" runat="server" ForeColor="#426172" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTitle")%>'
                                                                                            Visible="false"> </asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="13%" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentType" HeaderStyle-HorizontalAlign="Left" HeaderText="Folder"
                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" SortExpression="DocumentType">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PartyName" HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name"
                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="PartyName">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Ref No."
                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" SortExpression="DocumentRefNo">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldocrefno" runat="server" Text='<%# Eval("DocumentRefNo")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Amount"
                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%" SortExpression="DocumentAmount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldocamount" runat="server" Text='<%# Eval("DocumentAmount")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Document Date" SortExpression="DocumentDate" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="8%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldocumentdate" runat="server" Text='<%# Eval("DocumentDate","{0:MM/dd/yyy}")%>'></asp:Label>
                                                                                        <asp:Label ID="lbllevelofaccess" runat="server" Text='<%# Eval("Levelofaccess")%>'
                                                                                            Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblemployeeid" runat="server" Text='<%# Eval("EmployeeId")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblstatusid" runat="server" Text='<%# Eval("StatusId")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblapprovalstatus" runat="server" Text='<%# Eval("Approve")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbldocumentid" runat="server" Text='<%# Eval("DocumentId")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblmasterid" runat="server" Text='<%# Eval("ProcessingId")%>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="8%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Office Clerk <br/> Approval" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblofficeclarkapproval" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Supervisor <br/> Approval" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsupervisorapproval" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="15%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Status"
                                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="rbtnAcceptReject" runat="server" Width="130px">
                                                                                            <asp:ListItem Selected="True" Value="0" Text="Pending-New"></asp:ListItem>
                                                                                            <asp:ListItem Value="1" Text="Pending-Returned"></asp:ListItem>
                                                                                            <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                                                                            <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approval Note"
                                                                                    ItemStyle-HorizontalAlign="Left" Visible="false" SortExpression="Note">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtNote" runat="server" Height="30px" MaxLength="100" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'
                                                                                            TextMode="MultiLine" Width="150px"></asp:TextBox>
                                                                                        <asp:Label ID="lbltxtnote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'
                                                                                            Visible="false"></asp:Label>
                                                                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="txtNote"
                                                                                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="EmployeeName" HeaderStyle-HorizontalAlign="Left" HeaderText="UploadedBy"
                                                                                    ItemStyle-HorizontalAlign="Left" Visible="false" SortExpression="EmployeeName">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentId") %>'
                                                                                            CommandName="Edit1" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="ImageButton2" Visible="false" runat="server" ImageUrl="~/Account/images/AD.png">
                                                                                        </asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="3%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Edit" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                                                    <ItemTemplate>
                                                                                        <a href="javascript:void(0)" onclick='window.open(&#039;FilingDeskViewApprove.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;return=2&#039;, &#039;welcome&#039;,&#039;fullscreen=no,status=yes,top=0,left=0,menubar=yes,status=yes&#039;)'>
                                                                                            <asp:Image ID="Image1" runat="server" ToolTip="View/Edit" ImageUrl="~/Account/images/viewprofile.jpg" />
                                                                                        </a>
                                                                                        <asp:ImageButton ID="ImageButton3" Visible="false" runat="server" ImageUrl="~/Account/images/AD.png">
                                                                                        </asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                        </asp:GridView>
                                                                        <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                                        <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncal"
                                                    TargetControlID="TxtDocDate">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncalfrom"
                                                    TargetControlID="txtfrom">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtnto"
                                                    TargetControlID="txtto">
                                                </cc1:CalendarExtender>
                                                <asp:Button ID="Button4" CssClass="btnSubmit" runat="server" Text="Select Display Columns"
                                                    OnClick="Button4_Click" Visible="False" />
                                                <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Refresh" OnClick="Button5_Click"
                                                    Visible="False" />
                                                <asp:Button ID="Button2" Visible="false" ValidationGroup="1" CssClass="btnSubmit"
                                                    runat="server" Text="Update" OnClick="Button2_Click" />
                                                <asp:Button ID="Button3" Visible="false" ValidationGroup="1" CssClass="btnSubmit"
                                                    runat="server" Text="Cancel" OnClick="Button3_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                    BorderWidth="10px" Width="70%">
                                    <fieldset>
                                        <legend>Setup Default values for Document process</legend>
                                        <div align="right">
                                            <asp:ImageButton ID="ImageButton8" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                Width="15px" />
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <label>
                                                <asp:Label ID="Label56" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                                A)
                                            </label>
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Default Title"></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                    ControlToValidate="TextBox1" ValidationGroup="1"></asp:RegularExpressionValidator>
                                            </label>
                                            <label>
                                                <asp:TextBox ID="TextBox1" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span1',150)"
                                                    runat="server" ValidationGroup="1" Width="500px" MaxLength="150"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label18" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                                <span id="Span1" class="labelcount">150</span>
                                                <asp:Label ID="Label19" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                            </label>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                            <label>
                                                <asp:Label ID="Label48" runat="server" Text="Add Dynamic Document Date "></asp:Label>
                                            </label>
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                            <label>
                                                <asp:Label ID="Label49" runat="server" Text="Add Dynamic User(Party) Name "></asp:Label>
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                                B)
                                            </label>
                                            <label>
                                                <asp:Label ID="Label50" runat="server" Text="Default User(Party) Type"></asp:Label>
                                                <asp:DropDownList ID="ddlpopupusertype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpopupusertype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label51" runat="server" Text="Default User(Party) Name"></asp:Label>
                                                <asp:DropDownList ID="ddlpopuppartyname" runat="server" Width="400px">
                                                </asp:DropDownList>
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <label>
                                                C)
                                            </label>
                                            <label>
                                                <asp:Label ID="Label52" runat="server" Text="Default Cabinet"></asp:Label>
                                                <asp:DropDownList ID="ddlpopupcabinet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpopupcabinet_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label53" runat="server" Text="Default Drawer"></asp:Label>
                                                <asp:DropDownList ID="ddlpopupdrawer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpopupdrawer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label54" runat="server" Text="Default Folder"></asp:Label>
                                                <asp:DropDownList ID="ddlpopupfolder" runat="server">
                                                </asp:DropDownList>
                                            </label>
                                            <div style="clear: both;">
                                            </div>
                                            <asp:CheckBox ID="chkpopupstatus" runat="server" />
                                            <label>
                                                <asp:Label ID="Label55" runat="server" Text="Status"></asp:Label>
                                            </label>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                        <div>
                                            <asp:Button ID="Button7" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button7_Click" />
                                        </div>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="Button15" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel7" TargetControlID="Button15" CancelControlID="ImageButton8">
                                </cc1:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;">
                    </div>
                    <table width="100px">
                        <tr>
                            <td class="text">
                                <asp:Panel ID="pnloa" runat="server" Width="645px" BorderColor="Black" BorderStyle="Outset"
                                    Height="380px" BackColor="#CCCCCC">
                                    <table id="Table6" cellpadding="0" cellspacing="5">
                                        <tr>
                                            <td class="hdr" colspan="2">
                                                Accounting Entries done for following document
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Account/images/closeicon.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="column1">
                                                DocId :<asp:Label ID="lbldid" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                DocTitle :<asp:Label ID="lbldtitle" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="column2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:UpdatePanel ID="pvb" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rdradio" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                            OnSelectedIndexChanged="rdradio_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Text="Make new entry"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Add to Existing Entry" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="rdradio" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="pvlnewentry" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddloa" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="ImageButton5" runat="server" Text=" Go " OnClick="ImageButton5_Click" />
                                                    <asp:HyperLink ID="hypost" Visible="false" runat="server" Target="_blank" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="pnlexist" runat="server" Visible="true">
                                                    <asp:DropDownList ID="ddldo" runat="server" Width="200px">
                                                        <asp:ListItem Text="Cash Register"></asp:ListItem>
                                                        <asp:ListItem Text="Journal Register"></asp:ListItem>
                                                        <asp:ListItem Text="Cr/Dr Note Register"></asp:ListItem>
                                                        <asp:ListItem Text="Packing Slip Register"></asp:ListItem>
                                                        <asp:ListItem Text="Purechase Register"></asp:ListItem>
                                                        <asp:ListItem Text="Sales Register"></asp:ListItem>
                                                        <asp:ListItem Text="Sales Order Register"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="img2" runat="server" OnClick="Img2_Click" Text=" Go " />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                List of accounting entries done based on this document.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" Height="230px" Width="630px">
                                                    <asp:GridView ID="gridpopup" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                                        OnSelectedIndexChanged="gridpopup_SelectedIndexChanged" Width="612px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Datetime" HeaderText="Date" />
                                                            <asp:BoundField DataField="Entry_Type_Name" HeaderText="Entry Type" />
                                                            <asp:BoundField DataField="EntryNumber" HeaderText="Entry Number" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="mdloa" BackgroundCssClass="modalBackground" PopupControlID="pnloa"
                                    TargetControlID="Hidden1" CancelControlID="ImageButton6" runat="server">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden1" name="Hidden1" runat="Server" type="hidden" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
