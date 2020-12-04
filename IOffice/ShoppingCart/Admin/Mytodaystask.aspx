<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="MyTodaysTask.aspx.cs"
    Inherits="Admin_Admin_files_FrmEmployeeloginTask" Title="Untitled Page" %>

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
        .style1
        {
            width: 24%;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
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
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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
            <div style="float: left;">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="My Today's Task"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="btnprintableversion" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="btnprintableversion_Click" />
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" value="Print" visible="False" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    <%--    <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="lblfrom" runat="server" Text="From"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txtestartdate" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="cal1" TargetControlID="txtestartdate" PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtestartdate">
                                    </cc1:MaskedEditExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                <label>
                                    <asp:Label ID="lblto" runat="server" Text="To"></asp:Label>
                                </label>
                                <label>
                                    <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="cal2" TargetControlID="txteenddate" PopupButtonID="ImageButton1">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txteenddate">
                                    </cc1:MaskedEditExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                </label>
                                <label>
                                    <asp:Button ID="btngo" Text="Go" runat="server" CssClass="btnSubmit" OnClick="btngo_Click" />
                                </label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Employee Hourly Rate"></asp:Label>
                                    </label>
                                    <label>
                                        <asp:Label ID="lblhourrate" runat="server" Text=""></asp:Label>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr>
                                                            <td align="center" style="font-size: 20px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lblcompanyname" Font-Italic="true" runat="server" ForeColor="Black"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="Label22" runat="server" Font-Italic="true" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="font-size: 18px; font-weight: bold; color: #000000">
                                                                <asp:Label ID="lbldateprint" runat="server" Font-Italic="true" Text="List of Employee Task Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="text-align: left; font-size: 14px;">
                                                                <asp:Label ID="lblemployeenameprint" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId"
                                                    OnRowCancelingEdit="grid_RowCancelingEdit" OnRowEditing="grid_RowEditing" AllowSorting="True"
                                                    OnRowUpdating="grid_RowUpdating" Width="100%" EditRowStyle-VerticalAlign="Top"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    RowStyle-VerticalAlign="Top" EmptyDataText="No Record Found." OnSorting="grid_Sorting"
                                                    OnRowCommand="grid_RowCommand">
                                                    <RowStyle VerticalAlign="Top" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="taskallocationdate" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltaskallocationdate" runat="server" Text='<%# Eval("taskallocationdate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Task Name" SortExpression="taskallocationdate" ItemStyle-Width="30%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltaskname" runat="server" Text='<%# Eval("TaskMasterName")%>'></asp:Label>
                                                                <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("taskid")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budgeted Minute" SortExpression="EUnitsAlloted" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudgetedminute123" runat="server" Text='<%# Eval("EUnitsAlloted")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budgeted Empl.Cost" SortExpression="Rate" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbeemplcostute123" runat="server" Text='<%# Eval("Rate","{0:###,###.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Minute" SortExpression="unitsused" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitsused" runat="server" Text='<%# Eval("unitsused")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtactualunit123" runat="server" Width="50px" Text='<%# Eval("unitsused")%>'>
                                                                </asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtactualunit123"
                                                                    ValidChars="">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Empl.Cost" ItemStyle-Width="5%" SortExpression="ActualRate"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactempcost" runat="server" Text='<%# Eval("ActualRate","{0:###,###.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Task Report" SortExpression="taskreport" ItemStyle-Width="35%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltaskreport" runat="server" Text='<%# Eval("taskreport")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txttaskreport" runat="server" TextMode="MultiLine" Text='<%# Eval("taskreport")%>'
                                                                    Height="60px" Width="300px">
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supervisor Note" SortExpression="supervisornote" Visible="false"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsupervisornote" runat="server" TextMode="MultiLine" Text='<%# Eval("supervisornote")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtsupervisornote" runat="server" Text='<%# Eval("supervisornote")%>'
                                                                    Width="200px">
                                                                </asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" SortExpression="StatusName" ItemStyle-Width="5%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatusname" runat="server" Text='<%# Eval("StatusName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlstatusfill" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblstatusid123" runat="server" Text='<%# Eval("Status")%>' Visible="false"></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Attachment" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/Account/images/attach.png">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblattachment" runat="server" Text="Add"></asp:Label>--%>
                                                                <asp:ImageButton ID="lblattachment" runat="server" ToolTip="Attachment" ImageUrl="~/Account/images/attach.png" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("TaskId") %>'
                                                                    ForeColor="Black" CommandName="Add">Add</asp:LinkButton>
                                                                <%--<asp:Label ID="lblstatusid123" runat="server" Text='<%# Eval("Status")%>' Visible="false"></asp:Label>--%>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-Width="4%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                    CommandArgument='<%#Eval("TaskId") %>' ForeColor="Black"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-HorizontalAlign="Left"
                                                            ButtonType="Image" EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/updategrid.jpg"
                                                            CancelImageUrl="~/images/delete.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                                            HeaderStyle-Width="2%" />
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <EditRowStyle VerticalAlign="Top" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td style="width: 70%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" Width="620px"
                                    Height="300px" BorderStyle="Solid" BorderWidth="10px">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                                    <tr>
                                                        <td>
                                                            Office Documents
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                                OnClick="ImageButton3_Click" Width="15px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlof" ScrollBars="Vertical" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                                    Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    OnRowCommand="GridView1_RowCommand" EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                            HeaderText="Doc ID" SortExpression="DocumentId" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="LinkButton1" ForeColor="Black" runat="server" Text='<%#Eval("DocumentId") %>'
                                                                                    CommandName="View" HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-Width="2%"
                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                                                        <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:BoundField DataField="DocType" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                    Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grid" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="grid" EventName="RowUpdating" />
            <asp:AsyncPostBackTrigger ControlID="grid" EventName="RowCancelingEdit" />
            <asp:AsyncPostBackTrigger ControlID="grid" EventName="RowEditing" />
            <asp:AsyncPostBackTrigger ControlID="btnprintableversion" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
