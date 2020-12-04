<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="MyProjectMasterFinal.aspx.cs" Inherits="AllWork" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script language="javascript" type="text/javascript">
         function CallPrint(strid) {
             var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
             var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
             WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="" media="screen" />');
             WinPrint.document.close();
             WinPrint.focus();
             WinPrint.print();
             WinPrint.close();

         } 
    </script>
    <script language="javascript" type="text/javascript">


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

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                   
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Create new project" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" Visible="False" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlmonthgoal" runat="server" Width="100%" Visible="false">
                        <table id="pagetbl" width="100%">
                          <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                            <tr>
                                <td>
                                    <label>
                                        Department Name
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlDeptName" runat="server" Width="369px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Employee Name
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlemployee" runat="server" Width="369px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Project Name
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtproname" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    
                                        <asp:TextBox ID="txtproname" runat="server" Height="15px" Width="450px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text=" Project Description"></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="ChkProDesc" AutoPostBack="true" runat="server" 
                                        OnCheckedChanged="ChkProDesc_CheckedChanged" Visible="False" />
                                </td>
                                <td colspan="4">
                                    <label>
                                        <asp:TextBox ID="txtedescription" runat="server" Height="75px" TextMode="MultiLine"
                                            Visible="True" Width="369px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        Start Date
                                    </label>
                                </td>
                                <td colspan="4">
                                    <label>
                                        <asp:TextBox ID="txtstartdate" runat="server" Width="100px" 
                                        AutoPostBack="True" ontextchanged="txtstartdate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calstartdate" runat="server" TargetControlID="txtstartdate"
                                            PopupButtonID="txtstartdate" FirstDayOfWeek="Saturday" 
                                        Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </label>
                                    
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        Target End Date
                                    </label>
                                </td>
                                <td colspan="4">
                                        <asp:RadioButtonList ID="txttargetenddate" runat="server" 
                                        onselectedindexchanged="txttargetenddate_SelectedIndexChanged" CellPadding="0" CellSpacing="0" 
                                            AutoPostBack="True">
                                            <asp:ListItem Value="1">Due Today</asp:ListItem>
                                            <asp:ListItem Value="2">Due Tomorrow</asp:ListItem>
                                            <asp:ListItem Value="3">Due in a Week</asp:ListItem>
                                            <asp:ListItem Value="4">Due in a Month</asp:ListItem>
                                    </asp:RadioButtonList>
                                      <asp:RadioButton ID="RadioButton2" runat="server" 
                                        Text="Due on a Specific Date" AutoPostBack="True" 
                                            oncheckedchanged="RadioButton2_CheckedChanged" />
                                       <label>  <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox></label>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox1"
                                            PopupButtonID="TextBox1"  
                                        Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    
                                </td>
                            </tr>
                            <tr>
                            <td>
                           <label>
                                     <asp:Label ID="Label18" runat="server" Text="End Date" Visible="false"
                                        ></asp:Label>
                                        
                                    </label>
                                </td>
                            <td colspan="4">
                                    <label>
                                        <asp:TextBox ID="txtenddate" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calenddate" runat="server" TargetControlID="txtenddate"
                                            PopupButtonID="txtenddate">
                                        </cc1:CalendarExtender>
                                    </label>
                            </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        Incentive Value
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtinsentive" runat="server" Height="15px" Width="369px"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtinsentive" ErrorMessage="Numerical Values Only" 
                                        ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                    </label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Attachments" Font-Bold="True" 
                                        Font-Size="Medium"></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" 
                                        Visible="False" >
                                    </asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                
                                          
                                                    <td width="30%">
                                                        <label>
                                                            <asp:Label ID="Label19" runat="server" Text=" Title"></asp:Label>
                                                            <%--<asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txttitlename"
                                                                ErrorMessage="*" ValidationGroup="2">

                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttitlename"
                                                                Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                                                ValidationGroup="1">

                                                            </asp:RegularExpressionValidator>
                                                        </label>
                                                        </td>
                                                         <td width="30%">
                                                        <label>
                                                            <asp:TextBox ID="txttitlename" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)"
                                                                runat="server"></asp:TextBox>
                                                        </label>
                                                        <label>
                                                            Max <span id="Span10">30</span>
                                                            <asp:Label ID="Label54" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                    </td>
                                                    </tr>
                            <tr>
                                                    <td width="30%">
                                                        <asp:RadioButtonList ID="Upradio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Enabled ="true">Audio File</asp:ListItem>
                                                            <asp:ListItem Value="2">Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    </tr>
                            <tr>
                                                    <td width="25%">
                                                        <asp:Panel ID="pnlpdfup" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="Label21" runat="server" Text="Other File"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:FileUpload ID="fileuploadadattachment" runat="server" />
                                                            </label>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnladio" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="Label22" runat="server" Text=" Audio File"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:FileUpload ID="fileuploadaudio" runat="server" />
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                    </tr>
                            <tr>
                                                    <td width="10%">
                                                        <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_Click"
                                                            ValidationGroup="1" />
                                                    </td>
                                                </tr>
                            <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document File" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldoc" runat="server" Text='<%#Bind("Doc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" Text="Delete" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                              <tr>
                                
                                <td colspan="5" align="center">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button1_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" 
                                        OnClick="btnupdate_Click" Text="Update" ValidationGroup="1" Visible="false" />
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button2_Click1" Text="Cancel" />
                                </td>
                                
                            </tr>
                              </asp:Panel>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label30" runat="server" Text="List of Projects"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
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
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Visible="false"  Text="Printable Version" onclick="Button4_Click"/>
                                                <input id="Button5" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                    style="width: 51px;" type="button" value="Print" visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <label style="width:180px;">
                                                    <asp:Label ID="Label5" runat="server" Text="Filter By Employee: "></asp:Label>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="180px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                             
                                                 <label style="width:180px;">
                                                    <asp:Label ID="Label8" runat="server" Text="Filter By Status: "></asp:Label>
                                                    <asp:DropDownList ID="ddlsortmonth" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlsortmonth_SelectedIndexChanged">
                                                        <%--<asp:ListItem>---Select All---</asp:ListItem>--%>
                                                        <asp:ListItem>Pending</asp:ListItem>
                                                        <asp:ListItem>Completed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                                 <label style="width:150px;">
                                                Priority:  

                                                    <asp:DropDownList ID="ddl_priortyadd" runat="server" Width="150px" >                                                   
                                                            <asp:ListItem Value="0">-select-</asp:ListItem>
                                                            <asp:ListItem Value="1">Low</asp:ListItem>
                                                            <asp:ListItem Value="2">Medium</asp:ListItem>
                                                            <asp:ListItem Value="3">High</asp:ListItem>
                                                        </asp:DropDownList>
                                                </label> 
                                                <label style="width:150px;">
                                                    <asp:Label ID="Label13" runat="server" Text="Search: "></asp:Label>
                                                         <asp:TextBox ID="txtsearch" runat="server" Width="150px" ></asp:TextBox>
                                                </label>
                                                <label>
                                                  <asp:Label ID="Label6" runat="server" Text="Filter By Department: " Visible="false" ></asp:Label>
                                                    <asp:DropDownList ID="ddlsortdept" runat="server" AutoPostBack="True" Width="180px" Visible="false"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </label>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               
                                             
                                                <label style="width:180px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Select Project Date: "></asp:Label>
                                                    <%-- <asp:Label ID="Label2" runat="server" Text="Filter By ProjectStatus:"></asp:Label>--%>
                                                        <asp:TextBox ID="txtsortdate" runat="server"  Width="180px" ontextchanged="txtsortdate_TextChanged">
                                                        </asp:TextBox>
                                                        <cc1:CalendarExtender ID="calsortdate" runat="server" PopupButtonID="txtsortdate" TargetControlID="txtsortdate" Format="MM/dd/yyyy">
                                                        </cc1:CalendarExtender>
                                                </label>
                                                <label style="width:180px;">
                                                Filter By Project Date:
                                                     <asp:DropDownList ID="ddlsortdate" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlsortdate_SelectedIndexChanged" Width="180px">
                                                      <asp:ListItem>---Select All---</asp:ListItem>
                                                    <asp:ListItem>All Project Started After this Date</asp:ListItem>
                                                    <asp:ListItem>All Project having Target After this Date</asp:ListItem>
                                                    <asp:ListItem>All Project Ended Before this Date</asp:ListItem>
                                                    <asp:ListItem>All Project having Target Before this Date</asp:ListItem>
                                                    <asp:ListItem>All Project Ended After this Date</asp:ListItem>
                                                    <asp:ListItem>All Project Started Before this Date</asp:ListItem>
                                                    <asp:ListItem>All Project Started Selected Date</asp:ListItem>
                                                    <asp:ListItem>All Project Ended Selected Date</asp:ListItem>
                                                    <asp:ListItem>All Project having Target Selected Date</asp:ListItem>
                                                </asp:DropDownList>
                                                </label>                                               
                                              
                                                 <label style="width:150px;">
                                                            Reminder Date:
                                                          <asp:TextBox ID="txt_remindersearch"  runat="server" Width="150px">
                                                            </asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txt_remindersearch" TargetControlID="txt_remindersearch" Format="MM/dd/yyyy">
                                                                </cc1:CalendarExtender>
                                                    </label> 
                                                   <label style="width:150px;">
                                                
                                                    <asp:Label ID="Label17" runat="server" Text="Deadline Status: "></asp:Label>
                                                     <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                                                     <asp:ListItem Value="0">-select-</asp:ListItem>
                                                            <asp:ListItem Value="1">Overdue</asp:ListItem>
                                                            <asp:ListItem Value="2">Due Today</asp:ListItem>
                                                            <asp:ListItem Value="3">Due Tomorrow</asp:ListItem>
                                                            <asp:ListItem Value="4">Due This Week</asp:ListItem>
                                                            <asp:ListItem Value="5">Due This Month</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                       
                                            </td>
                                        </tr>
                                       
                                       
                                      
                                      
                                        <tr>
                                            <td>
                                                <label>
                                                <asp:Button ID="btngo" runat="server" CausesValidation="false" 
                                                    onclick="btngo_Click" Text="Go" />
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                
                                                <asp:GridView ID="grdmonthlygoal" runat="server" CssClass="mGrid" 
                                                    onrowcommand="grdmonthlygoal_RowCommand1" PageSize="15" 
                                                    AutoGenerateColumns="False" Width="100%" 
                                                    onrowdeleting="grdmonthlygoal_RowDeleting" 
                                                    onrowediting="grdmonthlygoal_RowEditing" 
                                                    onrowupdating="grdmonthlygoal_RowUpdating"
                                                    PagerStyle-CssClass="prg"
                                                    AllowPaging="True" 
                                                    onselectedindexchanged="grdmonthlygoal_SelectedIndexChanged"  
                                                    onpageindexchanging="grdmonthlygoal_PageIndexChanging1" 
                                                    onrowdatabound="grdmonthlygoal_RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproId" runat="server" Text='<%#Bind("ProjectMaster_Id")%>'></asp:Label>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("ProjectMaster_Employee_Id")%>'></asp:Label>
                                                                <asp:Label ID="lbldep" runat="server" Text='<%#Bind("ProjectMaster_DeptID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dept Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label120" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121" runat="server" Text='<%#Bind("Name1")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Title">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Gray" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                                            CommandName="view111"  Text='<%# Eval("ProjectMaster_ProjectTitle") %>'
                                                                    ToolTip="View Project Profile">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Priority">
                                                            <ItemTemplate >
                                                                <asp:Label ID="Label_Priority" runat="server" Text='<%# Eval("Priority") %>' ></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date"  >
                                                            <ItemTemplate >
                                                                <asp:Label ID="Label122" runat="server" Text='<%# Eval("ProjectMaster_StartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label123" runat="server" Text='<%# Eval("ProjectMaster_EndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target End Date">
                                                            <ItemTemplate>
                                                               <asp:Label ID="Label20" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:dd/MM/yyyy}") %>' ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
                                                                <asp:Label ID="Label124" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Upcoming Deadlines">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label125" runat="server" Text='<%# Eval("deadline") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label126" runat="server" Text='<%# Eval("ProjectMaster_ProjectStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/adda.png"  HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                
                                                                <asp:ImageButton ID="Image" runat="server" ImageUrl="~/Account/images/adda.png" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    CommandName="View" ToolTip="Add Progress Report" />
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/edit.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/delete.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                              <%-- <asp:GridView ID="grdmonthlygoal" runat="server" AutoGenerateColumns="False" CssClass="mGrid" 
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                   Width="100%" EmptyDataText="No Record Found." OnRowDeleting="grdmonthlygoal_RowDeleting"
                                                    OnRowEditing="grdmonthlygoal_RowEditing" OnRowUpdating="grdmonthlygoal_RowUpdating"
                                                    OnRowCommand="grdmonthlygoal_RowCommand" AllowPaging="true" PageSize="10" 
                                                    onpageindexchanging="grdmonthlygoal_PageIndexChanging" 
                                                    >
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblproId" runat="server" Text='<%#Bind("ProjectMaster_Id")%>'></asp:Label>
                                               <asp:Label ID="lblemp" runat="server" Text='<%#Bind("ProjectMaster_Employee_Id")%>'></asp:Label>
                                                  <asp:Label ID="lbldep" runat="server" Text='<%#Bind("ProjectMaster_DeptID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                        <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept Name"
                                                            ItemStyle-Width="12%" SortExpression="Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="12%"
                                                            HeaderText="Employee Name" SortExpression="Name1">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                      <asp:BoundField DataField="ProjectMaster_ProjectTitle" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Project Title" ItemStyle-Width="20%" SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">

                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" HeaderText="Project Title"
                                                                                    SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                                            CommandName="view111" ForeColor="Gray" Text='<%# Eval("ProjectMaster_ProjectTitle") %>' ToolTip="View Project Profile"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                                </asp:TemplateField>

                                                        <asp:BoundField DataField="ProjectMaster_StartDate" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%" HeaderText="Start Date" SortExpression="ProjectMaster_StartDate"
                                                            DataFormatString="{0:dd.MM.yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectMaster_EndDate" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="1%" HeaderText="End Date" SortExpression="ProjectMaster_EndDate"
                                                            DataFormatString="{0:dd.MM.yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectMaster_TargetEndDate" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%" HeaderText="Target End Date" SortExpression="ProjectMaster_TargetEndDate"
                                                            DataFormatString="{0:dd.MM.yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                       <asp:TemplateField HeaderText="Upcoming Deadlines">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label125" runat="server" Text='<%# Eval("deadline") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectMaster_ProjectStatus" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%" HeaderText="Status" SortExpression="ProjectMaster_ProjectStatus"
                                                            DataFormatString="{0:dd.MM.yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>

                                                         <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="View"
                                                                    Text="View"></asp:LinkButton>
                                                                <asp:Button ID="Button12" runat="server" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    CommandName="View" Text="Add Progress Report"  />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    CommandName="Edit" CommandArgument='<%# Eval("ProjectMaster_Id") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="Delete" CommandArgument='<%# Eval("ProjectMaster_Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="View" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="View" CommandArgument='<%# Eval("ProjectMaster_Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                      
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>--%>
                                                
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                        <tr>
            <td>
              <asp:Label ID="lblvername" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                <asp:CheckBox ID="ChkActive" runat="server" Checked="true" Text="Active" Visible=false />
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>

 <asp:Panel ID="pnl_paydetail" runat="server" Visible="false"   BackColor="Gray"  >
            <div style="position:absolute;  margin: -800px 0px 0px 6px; width:1300px; background-color: #dddddd;" >
           
              <fieldset>
        <legend>
            <asp:Label ID="Label119" runat="server" Text="Add Progress Report"></asp:Label>
        </legend>
         <asp:Panel ID="pnl_licence" runat="server">
        <%--<div style="position: absolute; margin: -00px 0px 0px 350px; height:600px; width:840px; background-color: #A4A4A4;" class="Box" >
           <div>--%>
              <fieldset>
               <div style="margin-left: 1%">
                <asp:Label ID="lblstsmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
              <table width="80%">

        <tr>
                                <td>
                                    <label>
                                        Reporting Date: 
                                    </label>
                                </td>
                                <td colspan="1">
                                    <label>
                                        <asp:TextBox ID="txtreportingdate" runat="server" Width="100px">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreportingdate"
                                            PopupButtonID="txtreportingdate"   Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                   
                                   
                                       
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Status Report: ">
                                        </asp:Label><asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Inavalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z/.0-9_\s]*)"
                                ControlToValidate="txtprogress" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                    <%--<asp:CheckBox ID="ChkProDesc" AutoPostBack="true" runat="server" OnCheckedChanged="ChkProDesc_CheckedChanged" />--%>
                                </td>
                                <td width="70%">
                                    <label>
                                        <asp:TextBox ID="txtprogress" runat="server" Height="40px" TextMode="MultiLine" Visible="True" Width="250px"></asp:TextBox>
                                    </label>
                                     <asp:Label ID="Label25" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="30%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text=" Project Complete: "></asp:Label>
                                    </label>
                                    
                                </td>
                                <td width="70%">
                                    <label>
                                    <asp:CheckBox ID="CheckBox1" AutoPostBack="true" runat="server" Text="Yes" oncheckedchanged="CheckBox1_CheckedChanged1" 
                                         />
                                        <%--<asp:TextBox ID="txtcompltdate" runat="server" Width="100px" Visible="false">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtcompltdate"
                                            PopupButtonID="ImageButtoncompl">
                                        </cc1:CalendarExtender>
                                    
                                        <asp:ImageButton ID="ImageButtoncompl" runat="server" ImageUrl="~/images/cal_actbtn.jpg" Visible="false"/>--%>
                                    </label>
                                </td>
                            </tr>

                             <tr>
                                <td>
                                    <label>
                                    Reminder Date:
                                    </label>
                                </td>
                                <td colspan="1">
                                    <label>
                                        <asp:TextBox ID="txt_reminder" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_reminder"
                                            PopupButtonID="txt_reminder"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    </label>
                                     <label>
                             <asp:Label ID="lblenddateerror" runat="server" Text="" 
                            style="color: #FF3300" ></asp:Label>
                        </label>
                                </td>
                            </tr>

                              <tr>
                                <td colspan="5">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Upload Documents:  "></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="Chkupld" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="Chkupld_CheckedChanged" Visible="False">
                                    </asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Pnlstsup" runat="server" >
                                      <fieldset>
                                            <table width="80%" >
                                                <tr>
                                                    <td width="30%">
                                                        <label>
                                                            <asp:Label ID="Label10" runat="server" Text="Title: "></asp:Label>
                                                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtststitle" ErrorMessage="*" ValidationGroup="2">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtststitle"  Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  ValidationGroup="1">
                                                            </asp:RegularExpressionValidator>
                                                            <label>
                                                            <asp:TextBox ID="txtststitle" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)"  runat="server"></asp:TextBox>
                                                        </label>
                                                         <label>
                                                        Max <span ID="Span1">30</span>
                                                        <asp:Label ID="Label12" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td width="30%">
                                                        <asp:RadioButtonList ID="rdoselct" runat="server" AutoPostBack="True"
                                                            RepeatDirection="Horizontal" 
                                                            onselectedindexchanged="rdoselct_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Enabled ="true">Audio File</asp:ListItem>
                                                            <asp:ListItem Value="2">Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Panel ID="Pnlof" runat="server" Visible="false">
                                                            <label>
                                                                <%--<asp:Label ID="Label13" runat="server" Text="Other File"></asp:Label>--%>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            </label>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Pnlad" runat="server" Visible="false">
                                                            <label>
                                                                <asp:Label ID="Label16" runat="server" Text="Audio File"></asp:Label>
                                                            </label>
                                                            <label>
                                                            
                                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                                            
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="10%">
                                                        <%--<asp:Button ID="Button6" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button6_Click"
                                                            ValidationGroup="2"  />--%>
                                                            <asp:Button ID="Button7" CssClass="btnSubmit" runat="server" Text="Add" 
                                                            ValidationGroup="2" onclick="Button7_Click"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:GridView ID="gridstsFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" onrowcommand="gridstsFileAttach_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PDFURL" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstspdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblststitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstsaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document File" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldoc" runat="server" Text='<%#Bind("Doc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="Delete1" ItemStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderText="Delete" ImageUrl="~/Account/images/delete.gif" Text="Delete" />
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
                                <td align="center" colspan="5">
                                    <asp:Button ID="Button8" runat="server" CssClass="btnSubmit" onclick="Button8_Click" Text="Submit" />
                                    <asp:Button ID="Button9" runat="server" CssClass="btnSubmit" 
                                        onclick="Button9_Click" Text="Cancel" />
                                </td>
                            
                                
                                
                            </tr>

                            <tr>
                                <td>
                                <div style="margin-left: 1%">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </div>
                            </tr>
           
            </table>
        <table style="width:100%">
        <tr>
        <td style="width:92%">
        </td>            
        <td  style="width:8%">
       <%-- <asp:Button ID="Button6" runat="server" Text=" X " 
                style="color: #FFFFFF; background-color: #FF0000; height: 26px;" 
                onclick="Button4_Click" />--%>
        </td>
        </tr>
        </table> 
       
            </fieldset>

            
              </div>
      </asp:Panel> 
      </fieldset>
              
            </div>
      </asp:Panel>
            </div>



        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="Button7" />
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="Button7"></asp:PostBackTrigger>
        </Triggers>

        


</asp:UpdatePanel>
<div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>
