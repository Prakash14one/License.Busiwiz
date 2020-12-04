<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MyLeaveApplication.aspx.cs" Inherits="Add_Leave_Request_Master" %>

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
             function mask(evt,max_len) {
    
   
  
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
    </style>
    <asp:UpdatePanel ID="pnnn1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="add" runat="server" Text="Add New Leave Request" OnClick="add_Click"
                            CssClass="btnSubmit" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Width="100%" Visible="False">
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Business Name"></asp:Label><asp:DropDownList
                                ID="ddlstrname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged"
                                Enabled="false">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Leave Type"></asp:Label><asp:Label ID="Label12"
                                runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLeaveType"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="1">*</asp:RequiredFieldValidator><asp:DropDownList
                                        ID="ddlLeaveType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                    </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label14" runat="server" Text="Employee Name"></asp:Label><asp:DropDownList
                                ID="ddlemp" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </label>
                        <label class="first">
                            <asp:Label ID="Label2" runat="server" Text="Leave Start Date"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartDate"
                                ValidationGroup="1">*</asp:RequiredFieldValidator><asp:TextBox ID="txtStartDate"
                                    runat="server" Width="75px" ValidationGroup="1"></asp:TextBox><cc1:CalendarExtender
                                        ID="txtdate_CalendarExtender" runat="server" TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartDate" />
                            <%--<input id="Text5" name="postcode1" class="txtInputMed" type="text" value="1/1/11 " />--%>
                        </label>
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="Leave End Date"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate" ValidationGroup="1">*</asp:RequiredFieldValidator><asp:TextBox
                                    ID="txtEndDate" runat="server" Width="75px" ValidationGroup="1"></asp:TextBox><cc1:CalendarExtender
                                        ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEndDate" />
                            <!--<input name="street1" type="text" value="Street" />-->
                            <%--<input id="Text1" name="postcode1" class="txtInputMed" type="text" value="1/1/11" />--%>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="lblpaidle" runat="server"></asp:Label></label>
                        <label>
                            <asp:Label ID="lblpaidleno" runat="server" CssClass="lblSuggestion">
                            </asp:Label></label>
                        <div style="clear: both;">
                        </div>
                        <label >
                            <asp:Label ID="Label4" runat="server" Text="Leave Request Note"></asp:Label><asp:RegularExpressionValidator
                                ID="REG1" runat="server" ControlToValidate="txtnote" Display="Dynamic" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([_.,a-zA-Z0-9\s]*)" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <br />
                        <asp:TextBox
                                    ID="txtnote" runat="server" 
                            Height="65px" TextMode="MultiLine" Width="300px"
                                           onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*>+:;={}[]|\/]/g,/^[\a-zA-Z.0-9_()\s]+$/,'div2',300)" 
                  MaxLength="300" onkeypress="return checktextboxmaxlength(this,300,event)"></asp:TextBox>
                    <asp:Label ID="Label28" runat="server" Text="Max" CssClass="labelcount" ></asp:Label> <span id="div2" class="labelcount">
                                        300</span> <asp:Label ID="Label11" runat="server" Text="(A-Z 0-9 _ () .)"  CssClass="labelcount"></asp:Label>
                                   </label>
                        <div style="clear: both;">
                        </div>
                        <label class="cssLabelCompany_Information">
                            <asp:Label ID="Label5" runat="server" Text="Supervisor Name  "></asp:Label></label>
                        <label class="cssLabelCompany_Information_Ans">
                            <asp:Label CssClass="lblSuggestion" ID="lblsupervisor" runat="server" Text="Label"></asp:Label></label>
                        <label>
                            <asp:Label ID="lblsupervisorid" runat="server" Text="Label" Visible="False"></asp:Label></label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnSubmit_Click"
                            ValidationGroup="1" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button2"
                CssClass="btnSubmit" Text="Submit" />--%>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btnCancel_Click" />
                        <%--<asp:Button runat="server" ID="Button3" CssClass="btnSubmit" Text="Cancel" />--%></asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="List of leave Applications"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                        <input id="Button7" runat="server" onclick="javascript:CallPrint('divPrint')" class="btnSubmit"
                            type="button" value="Print" visible="false" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                    CssClass="btnSubmit" Text="Print Version" />--%>
                    </div>
                    <asp:Panel ID="pnlhide" runat="server" Visible="true">
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Leave Type"></asp:Label><asp:DropDownList
                                ID="ddlfillleave" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </label>
                        <label class="first">
                            <asp:Label ID="Label8" runat="server" Text="From Date"></asp:Label>
                            <asp:RequiredFieldValidator
                                    ID="ReqieldValidator1" runat="server" ControlToValidate="txtfromdt"
                                     SetFocusOnError="true" ValidationGroup="5">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtfromdt"
                                runat="server" Width="75px" ValidationGroup="1"></asp:TextBox><cc1:CalendarExtender
                                    ID="CalendarExtender1" runat="server" TargetControlID="txtfromdt">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdt" />
                        </label>
                        <label class="first">
                            <asp:Label ID="Label9" runat="server" Text="To Date">
                            </asp:Label>
                            
                                 <asp:RequiredFieldValidator
                                    ID="Requiridator1" runat="server" ControlToValidate="txttodt"
                                     SetFocusOnError="true" ValidationGroup="5">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txttodt" runat="server" Width="75px" ValidationGroup="1"></asp:TextBox><cc1:CalendarExtender
                                ID="CalendarExtender3" runat="server" TargetControlID="txttodt">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureName="en-AU"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodt" />
                        </label>
                        <label>
                        <br />
                        <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" ValidationGroup="5" Text=" Go " OnClick="Button2_Click" />
                        </label>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label15" runat="server" Font-Size="20px" Text="Business :"></asp:Label>
                                                    <asp:Label ID="lblbusiness" runat="server" Font-Size="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label10" runat="server" Font-Size="18px" Text="List of Leave Applications"></asp:Label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                             <tr>
                                        <td align="left" style="font-size: 16px; font-weight: normal;">
                                            <asp:Label ID="Label111" runat="server" Text="Leave Type :"></asp:Label>
                                            <asp:Label ID="lblfilterleavetype" runat="server" Text=""></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                                           
                                        </td>
                                    </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Record Found." AutoGenerateColumns="False"
                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        AllowSorting="True" GridLines="Both" Width="100%" DataKeyNames="id" OnSorting="GridView2_Sorting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="11%"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbusinees" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type" SortExpression="EmployeeLeaveTypeName"
                                                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleavetype" runat="server" Text='<%# Bind("EmployeeLeaveTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName" ItemStyle-Width="12%"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemployee" runat="server" Text='<%# Bind("EmployeeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Request Note" SortExpression="leaveRequestNote"
                                                                ItemStyle-Width="22%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblreqnote" runat="server" Text='<%# Bind("leaveRequestNote")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Start Date" SortExpression="fromdate" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfromDate" runat="server" Text='<%# Bind("fromdate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave End Date" ItemStyle-Width="10%" SortExpression="Todate"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltoDate" runat="server" Text='<%# Bind("Todate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Status" ItemStyle-Width="8%" SortExpression="Status"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblapprovestatus" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Notes" ItemStyle-Width="20%" SortExpression="ApprovalNote"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblapprovenotes" runat="server" Text='<%# Bind("ApprovalNote")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                    <div style="clear: both;">
                    </div>
                    <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="ContactName" HeaderText="Leave Type" />
                <asp:BoundField DataField="PricePlan" HeaderText="Leave Request Note" />
                <asp:BoundField DataField="PricePlan" HeaderText="From Date (Leave)" />
                <asp:BoundField DataField="PricePlan" HeaderText="To Date (Leave)" />
                <asp:BoundField DataField="PricePlan" HeaderText="Approval Status" />
                <asp:BoundField DataField="PricePlan" HeaderText="Approval Notes" />
            </Columns>
        </asp:GridView>--%>
                    <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
        </asp:XmlDataSource>--%>
                </fieldset>
            </div>
            <!--end of right content-->
            <div style="clear: both;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
