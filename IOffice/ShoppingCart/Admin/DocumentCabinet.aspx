<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentCabinet.aspx.cs" Inherits="Account_DocumentCabinet"
    Title="Untitled Page" %>
<%--
<%@ Register Src="~/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }  
        function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
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
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }    

    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="lbldoyou" runat="server" Text="Business Name"></asp:Label>
                                </label>
                            </td>
                            <td width="70%">
                                <label>
                                    <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged"
                                        Width="350px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Cabinet"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <asp:DropDownList ID="ddlcabinet" runat="server" Width="350px" AutoPostBack="True"
                                                DataTextField="DocumentMainType" DataValueField="DocumentMainTypeId" OnSelectedIndexChanged="ddlcabinet_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlbusiness" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Drawer"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <asp:DropDownList ID="ddldrower" runat="server" Width="350px" AutoPostBack="True"
                                                DataTextField="DocumentSubType" DataValueField="DocumentSubTypeId" OnSelectedIndexChanged="ddldrower_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlcabinet" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Folder"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <asp:DropDownList ID="ddltypeofdoc" runat="server" Width="350px" OnSelectedIndexChanged="ddltypeofdoc_SelectedIndexChanged"
                                                DataTextField="DocumentType" DataValueField="DocumentTypeId">
                                            </asp:DropDownList>
                                        </label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddldrower" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Status "></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server" RepeatDirection="Horizontal" Width="100px">
                                        <asp:ListItem Selected="True" Text="All"></asp:ListItem>
                                        <asp:ListItem Text="Pending"></asp:ListItem>
                                        <asp:ListItem Value="True" Text="Accept">Accept</asp:ListItem>
                                        <asp:ListItem Value="False" Text="Reject">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label17" runat="server" Text="Filter by Upload Date"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lbldatefrom" runat="server" Text="From "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="TextBox1_MaskedEditExtender2" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdate" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton2"
                                        TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                <label>
                                    <asp:Label ID="lbldateto" runat="server" Text="To "></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtncal2"
                                        TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="imgbtncal2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" CssClass="btnSubmit" runat="server" Text="Go" ValidationGroup="1"
                                    OnClick="btnShow_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Documents for Approval (Without Document Approval Rule Flow)"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button1_Click" />
                                    <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblcom" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="true" Text="Business : "
                                                                    Font-Size="18px"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="18px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblhead" runat="server" Font-Bold="True" Font-Italic="true" Font-Size="18px"
                                                                    Text="List of Documents for Approval (Without Document Approval Rule Flow)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label1" runat="server" Text="Cabinet :"></asp:Label>
                                                                <asp:Label ID="lblcabi" runat="server"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label2" runat="server" Text="Drawer :"></asp:Label>
                                                                <asp:Label ID="lbldrower" runat="server"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label3" runat="server" Text="Folder :"></asp:Label>
                                                                <asp:Label ID="lblfolder" runat="server"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label9" runat="server" Text="Status"></asp:Label>
                                                                <asp:Label ID="lblstatusprint" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblFootermsg" runat="server" ForeColor="White"></asp:Label>
                                                <asp:GridView ID="Gridreqinfo" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" DataKeyNames="DocumentTypeId" EmptyDataText="No Record Found."
                                                    CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    OnPageIndexChanging="Gridreqinfo_PageIndexChanging" OnRowCommand="Gridreqinfo_RowCommand"
                                                    OnSorting="Gridreqinfo_Sorting" PageSize="15" Width="100%" OnRowEditing="Gridreqinfo_RowEditing"
                                                    OnRowCancelingEdit="Gridreqinfo_RowCancelingEdit" OnRowUpdating="Gridreqinfo_RowUpdating"
                                                    OnRowDataBound="Gridreqinfo_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Doc ID" ShowHeader="true"
                                                            SortExpression="DocumentId" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <a href="javascript:void(0)" onclick='window.open(&#039;ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Did=<%= DesignationId %>&#039;, &#039;welcome&#039;,&#039;fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no&#039;)'>
                                                                    <asp:Label ID="lnbvvd" ForeColor="#426172" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentId") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="DocumentUploadDate" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocupdate" runat="server" Text='<%# Eval("DocumentUploadDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc. Type" SortExpression="DocumentType" HeaderStyle-HorizontalAlign="Left"
                                                            Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldoctype123" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Title" SortExpression="DocumentTitle" ItemStyle-Width="15%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocumenttitle123" runat="server" Text='<%# Eval("DocumentTitle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name" SortExpression="PartyName" ItemStyle-Width="10%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpartyname123" runat="server" Text='<%# Eval("PartyName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View" ItemStyle-Width="5%"
                                                            ShowHeader="False">
                                                            <ItemTemplate>
                                                                <a href="javascript:void(0)" onclick='window.open(&#039;ViewDocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>&amp;Did=<%= DesignationId %>&#039;, &#039;welcome&#039;,&#039;fullscreen=no,status=yes,top=0,left=0,menubar=yes,status=yes&#039;)'>
                                                                    <asp:Label ID="lblview" ForeColor="#426172" runat="server" Text="View"></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblInw" runat="server" Text="Accept/Reject"></asp:Label>
                                                                <asp:RadioButtonList ID="rlheader" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rlheader_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="2">None</asp:ListItem>
                                                                    <asp:ListItem Value="1">Accept</asp:ListItem>
                                                                    <asp:ListItem Value="0">Reject</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <EditItemTemplate>
                                                                <asp:RadioButtonList ID="rbtnAcceptRejectedit" runat="server" RepeatDirection="Horizontal">
                                                                    <%-- <asp:ListItem Selected="True" Value="None">None</asp:ListItem>--%>
                                                                    <asp:ListItem Value="True">Accept</asp:ListItem>
                                                                    <asp:ListItem Value="False">Reject</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rbtnAcceptReject" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                                                                    <asp:ListItem Value="True">Accept</asp:ListItem>
                                                                    <asp:ListItem Value="False">Reject</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approve Type" ItemStyle-Width="15%">
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbldocmasterid" runat="server" Text='<%# Eval("DocumentId")%>' Visible="false"></asp:Label>
                                                                <asp:DropDownList ID="ddlApprovetypeedit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovetypeedit_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:LinkButton ID="lnkappedit" runat="server" ForeColor="#426172" CausesValidation="false"
                                                                    OnClick="LinkButton152edit_Click" Text="More Info"> </asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlApprovetype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovetype_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:LinkButton ID="lnkapp" runat="server" ForeColor="#426172" CausesValidation="false"
                                                                    OnClick="LinkButton152_Click" Text="More Info"> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Note" ItemStyle-Width="15%">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lbltxtnoteapprovalnote" runat="server" Text="Notes"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" ForeColor="White" runat="server" CausesValidation="false"
                                                                    AutoPostBack="True" OnClick="LinkButton3_Click" Text="(More Info)"> </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG2" runat="server" ControlToValidate="TextBox2"
                                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltxtnote" runat="server" Text=""></asp:Label>
                                                                <asp:TextBox ID="TextBox3" Visible="false" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="REG3" runat="server" ControlToValidate="TextBox3"
                                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)"
                                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-HorizontalAlign="Left"
                                                            ButtonType="Image" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                                            CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                            HeaderStyle-Width="2%" />
                                                        <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Delete" Visible="false" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ToolTip="Delete"
                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentId")%>' CommandName="delete1"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" ImageUrl="~/Account/images/delete.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                            </td>
                            <td width="50%">
                                <asp:Button ID="imgbtnSubmit" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="imgbtnSubmit_Click"
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="50%">
                                    <fieldset>
                                        <legend>Approval Type Description </legend>
                                        <div>
                                            <table width="100%">
                                                <tr>
                                                    <td width="30%">
                                                        <label>
                                                            Approval Type
                                                        </label>
                                                    </td>
                                                    <td width="70%">
                                                        <label>
                                                            <asp:DropDownList ID="ddlappd" runat="server" DataTextField="DocumentType"  Enabled="false" DataValueField="DocumentTypeId"
                                                                OnSelectedIndexChanged="ddlappd_SelectedIndexChanged" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Approval Type Description
                                                        </label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <asp:TextBox ID="txtappdes" runat="server" Height="70px" TextMode="MultiLine" Enabled="false" Width="400px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="REG4" runat="server" ErrorMessage="*" Display="Dynamic"
                                                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtappdes"
                                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnclose" runat="server" CssClass="btnSubmit" Text="Close" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset></asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel1" TargetControlID="hdnconfirm" CancelControlID="btnclose">
                                </cc1:ModalPopupExtender>
                                <input id="hdnconfirm" runat="Server" name="hdnconfirm" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
