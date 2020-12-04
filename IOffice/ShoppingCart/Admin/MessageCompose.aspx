<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="MessageCompose.aspx.cs"
    Inherits="Account_MessageCompose" Title="MessageCompose" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Src="~/Account/UserControl/MessageList.ascx" TagName="MsgList" TagPrefix="MsgList" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/ExternalInternalMessage1.ascx"
    TagName="extmsg" TagPrefix="extmsg" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

function ChangeCheckBoxState(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        // For Document
        function ChangeAllCheckBoxStates(checkState)
        {
            if (CheckBoxIDs != null)
            {
               for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }        
        function ChangeHeaderAsNeeded()
        {
            if (CheckBoxIDs != null)
            {
                for (var i = 1; i < CheckBoxIDs.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked)
                    {
                       ChangeCheckBoxState(CheckBoxIDs[0], false);
                       return;
                    }
                }        
               ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
            function checktextboxmaxlength(txt, maxLen,evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  
             }
            
           
            if(evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59)
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
             if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        } 
        
    </script>

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Panel ID="pnlmsg" runat="server" Visible="False" Width="100%">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    </asp:Panel>
                </div>
                <extmsg:extmsg ID="msgwzd" runat="server" />
                <div style="clear: both;">
                </div>
                <div>
                    <%--<pnlhelp:pnlhelp ID="pnlHlp" runat="server" />--%>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Button ID="imgbtnsend" runat="server" CssClass="btnSubmit" Text="Send" OnClick="imgbtnsend_Click"
                                    ValidationGroup="1" />
                            </td>
                            <td align="center" valign="middle">
                                <asp:Button ID="imgbtnsaveasdraft" runat="server" Text="Save As Draft" OnClick="imgbtnsaveasdraft_Click"
                                    CssClass="btnSubmit" />
                            </td>
                            <td align="center" valign="middle">
                                <asp:Button ID="imgbtndiscard" runat="server" Text="Discard" OnClick="imgbtndiscard_Click"
                                    CssClass="btnSubmit" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblComposeMail" Text="Compose Mail" runat="server" Style="font-weight: 700"></asp:Label>
                        </legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 15%">
                                    <MsgList:MsgList runat="server" ID="MsgListView" />
                                </td>
                                <td style="width: 85%" colspan="2">
                                    <asp:Panel Width="100%" runat="server" ID="PnlFileAttach">
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="klblStoreName" Text="Business Name" runat="server" Width="170px"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                                            Width="180px">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblTo" Text="To" runat="server"></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="lblAddresses"
                                                            ErrorMessage="*" ValidationGroup="1" Width="13px"></asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="ImgBtnAddress" runat="server" ImageUrl="~/Account/images/addbook.png"
                                                            ToolTip="Click here to add Addresses" />
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="lblAddresses" runat="server" Width="450" ReadOnly="True" OnTextChanged="lblAddresses_TextChanged"></asp:TextBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="Button1"></asp:AsyncPostBackTrigger>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Visible="false">Select</asp:LinkButton>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblSubject" Width="170px" Text="Subject" runat="server"></asp:Label>
                                                        <br />
                                                        <%--<asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsub"
                                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([._:a-zA-Z0-9\s]*)"
                                                            ControlToValidate="txtsub" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:TextBox ID="txtsub" runat="server" ValidationGroup="1" Width="450px" MaxLength="200"
                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\_:a-zA-Z.0-9\s]+$/,'div1',200)"></asp:TextBox>
                                                    </label>
                                                    <label>
                                                        <asp:Label runat="server" ID="sadasd" Text="Max" CssClass="labelcount"></asp:Label>
                                                        <span id="div1" cssclass="labelcount">200</span>
                                                        <asp:Label ID="Label3" runat="server" Text="(A-Z 0-9 _ . :)" CssClass="labelcount"></asp:Label>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <asp:Label ID="lblAttachment" runat="server" Text="Attachment"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileuploadadattachment"
                                                            ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </label>
                                                </td>
                                                <td valign="top">
                                                    <label>
                                                        <asp:CheckBox runat="server" ID="chkattach" AutoPostBack="true" OnCheckedChanged="chkattach_CheckedChanged" />
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pblattach" runat="server" Visible="false">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <asp:FileUpload ID="fileuploadadattachment" runat="server" />
                                                                    </label>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="imgbtnattach" runat="server" Text="Attach" OnClick="imgbtnattach_Click"
                                                                        ValidationGroup="2" CssClass="btnSubmit" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                </td>
                                <td style="width: 65%" colspan="2">
                                    <asp:Panel runat="server" Width="70%" Visible="false" ID="PnlFileAttachLbl">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="lblAttachmentlbl" Text="Attachment" runat="server" Style="font-weight: 700"></asp:Label>
                                            </legend>
                                            <table width="80%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:GridView ID="gridFileAttach" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            DataKeyNames="FileNameChanged" OnRowCommand="gridFileAttach_RowCommand" PageSize="12"
                                                            OnPageIndexChanging="gridFileAttach_PageIndexChanging" Width="640px">
                                                            <Columns>
                                                                <asp:ButtonField CommandName="Remove" ImageUrl="~/Account/images/delete.gif" HeaderImageUrl="~/Account/images/delete.gif"
                                                                    ItemStyle-Width="5%" HeaderText="Remove" ButtonType="Image"></asp:ButtonField>
                                                                <asp:BoundField DataField="FileName" HeaderText="Attached File Name" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:DataList ID="DataListAttach" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" Text='<%# Eval("DocumentTitle") %>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:LinkButton ID="lnkBtnAttach" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" align="right">
                                </td>
                                <td width="85%" colspan="2">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Message" Width="175px"></asp:Label>
                                        <br />
                                        <%--<asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtMsgDetail"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)"
                                            ControlToValidate="TxtMsgDetail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                            ControlToValidate="TxtMsgDetail" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                    <label>
                                        <asp:TextBox ID="TxtMsgDetail" runat="server" TextMode="MultiLine" Width="450px"
                                            Height="60px" onkeypress="return checktextboxmaxlength(this,1000,event)" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'&*>:;={}[]|\/]/g,/^[\a-z().@+A-Z,0-9_-\s]+$/,'div2',1000)"></asp:TextBox>
                                        <br />
                                        <asp:Label runat="server" ID="Label6" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div2" cssclass="labelcount">1000</span>
                                        <asp:Label ID="Label2" runat="server" Text="(A-Z 0-9 , . @ ) ( - +)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">
                                </td>
                                <td style="width: 15%">
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Include Signature" AutoPostBack="true"
                                                        OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:CheckBox ID="chkpicture" runat="server" Text="Include Picture" AutoPostBack="true"
                                                        OnCheckedChanged="chkpicture_CheckedChanged" />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 70%" align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Image ID="image1" runat="server" Height="70px" Width="70px" />
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="" Width="185px"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label11" runat="server" Text="" Width="185px"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label12" runat="server" Text="" Width="185px"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label13" runat="server" Text="" Width="185px"></asp:Label>
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="linkAddCabinet" runat="server" OnClick="linkAddCabinet_Click"
                                                    CssClass="btnSubmit">Edit</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table>
                        <tr>
                            <td>
                                <input id="hdnFileName" runat="server" name="hdnFileName" style="width: 1px" type="hidden" />
                                <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnladdress" runat="server" BorderStyle="Outset" ScrollBars="Vertical"
                        Height="500px" Width="380px" BackColor="#CCCCCC" BorderColor="#666666">
                        <table id="innertbl1">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSelectRecipient" Text="Select Receipient" runat="server"></asp:Label>
                                            </td>
                                            <td width="10%">
                                                <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Insert" Font-Size="13px"
                                                    Font-Names="Arial" ToolTip="Insert" CssClass="btnSubmit"></asp:Button>
                                            </td>
                                            <td width="10%">
                                                <asp:ImageButton ID="ibtnCancelCabinetAdd" runat="server" ImageUrl="~/Account/images/closeicon.png"
                                                    AlternateText="Close" CausesValidation="False" ToolTip="Close"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label7" Text="User Category" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlusertype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                </label>
                                            </td>
                                            <td>
                                                <label>
                                                    <asp:Label ID="Label8" Text="Search" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged"
                                                        AutoPostBack="True" Width="150px"></asp:TextBox>
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel runat="server" ID="pnlusertypeother" Visible="false">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label4" runat="server" Text="User Type"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:DropDownList ID="ddlcompname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcompname_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel runat="server" ID="pnlusertypecandidate" Visible="false">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label9" runat="server" Text="Job Applied For"></asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:DropDownList ID="ddlcandi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcandi_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="grdPartyList" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    DataKeyNames="PartyId" GridLines="None" EmptyDataText="No Parties are available for e-mail."
                                                    OnRowDataBound="grdPartyList_RowDataBound" Width="338px" OnPageIndexChanging="grdPartyList_PageIndexChanging">
                                                    <EmptyDataRowStyle ForeColor="Peru" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="HeaderChkbox" runat="server" OnCheckedChanged="HeaderChkbox_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkParty" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Contactperson" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Compname" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="Name" HeaderText="LastName - FirstName" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="Name" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="dname" HeaderText="Department - Designation" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="PartType" HeaderText="Type" HeaderStyle-HorizontalAlign="Left"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CName" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VacancyPositionTitle" HeaderText="Job Applied For" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnladdress" TargetControlID="ImgBtnAddress" X="950" Y="-200"
                        CancelControlID="ibtnCancelCabinetAdd">
                    </cc1:ModalPopupExtender>
                    </td> </tr> </table>
                    <table id="Table5" cellspacing="0" width="40%">
                        <tr>
                            <td class="style3">
                                <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ibtnCancelCabinetAdd"
                                    X="700" Y="300">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlMainTypeAdd" runat="server" BorderStyle="Outset" BackColor="#CCCCCC"
                                    BorderColor="#666666">
                                    <table>
                                        <tr>
                                            <td class="style2">
                                                <%-- <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>--%>
                                                <table id="Table3" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="lblEmailSignature" Text="Update Email Signature" runat="server"></asp:Label>
                                                                </label>
                                                            </td>
                                                            <td colspan="1">
                                                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Close" CausesValidation="False"
                                                                    ImageUrl="~/Account/images/closeicon.png" OnClick="ibtnCancelCabinetAdd_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlMainTypeAdd1" runat="server">
                                                                    <table id="Table4" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:Label ID="lblEnterSignature" Text="Enter Your Signature" runat="server"></asp:Label>
                                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="TextBox1"
                                                                                            ErrorMessage="*" ValidationGroup="7"></asp:RequiredFieldValidator>--%>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionVasdfdsfdslidator3" runat="server"
                                                                                            ErrorMessage="*" ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox1"
                                                                                            ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*"
                                                                                            ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox2"
                                                                                            ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*"
                                                                                            ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox3"
                                                                                            ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*"
                                                                                            ValidationExpression="^([a-z().@+A-Z-,0-9_\s]*)" ControlToValidate="TextBox4"
                                                                                            ValidationGroup="7"></asp:RegularExpressionValidator>
                                                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValisdfsdfdator4" runat="server"
                                                                                            ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                                                                            ControlToValidate="TextBox1" ValidationGroup="7"></asp:RegularExpressionValidator>--%>
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <label>
                                                                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="32" Width="250px" BorderStyle="None"></asp:TextBox>
                                                                                        <asp:TextBox ID="TextBox2" runat="server" MaxLength="32" Width="250px" BorderStyle="None"></asp:TextBox>
                                                                                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="32" Width="250px" BorderStyle="None"></asp:TextBox>
                                                                                        <asp:TextBox ID="TextBox4" runat="server" MaxLength="32" Width="250px" BorderStyle="None"></asp:TextBox>
                                                                                    </label>
                                                                                    <%-- <label>
                                                                                        <asp:TextBox ID="txtmdgdtl1" runat="server" TextMode="MultiLine" Width="345px" Height="60px"
                                                                                            onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!#$%^'&*>:;={}[]|\/]/g,/^[\a-z().@+A-Z,0-9_-\s]+$/,'div3',1000)"></asp:TextBox>
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:Label runat="server" ID="Label25" Text="Max " CssClass="labelcount"></asp:Label>
                                                                                        <span id="div3">1000</span>
                                                                                        <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 , . @ ) ( - +)"></asp:Label>
                                                                                    </label>--%>
                                                                                    <label>
                                                                                        <asp:Button ID="imgbtnsubmitCabinetAdd" runat="server" Text="Update" OnClick="imgbtnsubmitCabinetAdd_Click"
                                                                                            ValidationGroup="7" CssClass="btnSubmit" />
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%-- </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnsubmitCabinetAdd"></asp:AsyncPostBackTrigger>
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnattach" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
