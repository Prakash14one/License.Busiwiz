<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="AdminProjectMasterLB.aspx.cs" Inherits="AllWork" %>
        <%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor" TagPrefix="cc2" %>
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
                    <legend id="5"> Add New Project </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Create new project" CssClass="btnSubmit"
                            OnClick="addnewpanel_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlmonthgoal" runat="server" Width="100%" Visible="false">
                        <table id="pagetbl" width="100%">
                                <tr>
                                                <td style="width: 30%" align="left">
                                                    <label>
                                                        <asp:Label ID="lblwname" runat="server" Text="Business Name: "></asp:Label>
                                                    </label>
                                                </td>
                                                <td style="width: 70%">
                                                    <label>                                                        
                                                        <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                                            Width="250px">
                                                        </asp:DropDownList>
                                                    </label>
                                                   
                                                                                               
                                                </td>
                                            </tr>
                                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label32" runat="server" Text="Department Name: "></asp:Label>
                                       
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddldesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged"  Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                  
                                    
                                </td>
                            </tr>
                                             
                            
                            <tr>
                                <td>
                                    <label>
                                     <asp:Label ID="Label34" runat="server" Text="Department Name: " Visible="false" ></asp:Label>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlDeptName" runat="server"  Width="250px" AutoPostBack="true"
                                          Visible="false"  OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                     <asp:Label ID="Label35" runat="server" Text="Employee Name: "  ></asp:Label>
                                        
                                         <asp:Label ID="Label33" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatr1" runat="server" ControlToValidate="ddlemployee"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlemployee" runat="server" Width="250px" AutoPostBack="true"   OnSelectedIndexChanged="ddlddlemployee_SelectedIndexChangedEmp" >
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                    <asp:Label ID="Label36" runat="server" Text="Project Name: " ></asp:Label>
                                        
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtproname" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>                                    
                                        <asp:TextBox ID="txtproname" runat="server"  Width="250px"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Project Description: "></asp:Label>
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
                                     <asp:Label ID="Label37" runat="server" Text="Target Start Date: "></asp:Label>
                                        
                                    </label>
                                </td>
                                <td colspan="4">
                                    <label>
                                        <asp:TextBox ID="txtstartdate" runat="server" Width="100px" 
                                        AutoPostBack="True" ontextchanged="txtstartdate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calstartdate" runat="server" TargetControlID="txtstartdate"
                                            PopupButtonID="txtstartdate" FirstDayOfWeek="Saturday" >
                                        </cc1:CalendarExtender>
                                    </label>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td valign="top">
                                    <label>
                                    <asp:Label ID="Label38" runat="server" Text="Target End Date: "></asp:Label>
                                        
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
                                      <asp:RadioButton ID="RadioButton2" runat="server" Text="Due on a Specific Date" AutoPostBack="True" oncheckedchanged="RadioButton2_CheckedChanged" />
                                       <label style="width:150px">
                                         <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="100px"    AutoPostBack="True" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                                         </label>
                                         <label>
                                          <asp:Label ID="lblenddateerror" runat="server" Text="" style="color: #FF3300" ></asp:Label>
                                         </label> 
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox1"  PopupButtonID="TextBox1"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    
                                </td>
                            </tr>
                            <tr>
                            <td>
                             <label>
                                  <asp:Label ID="Label25" runat="server" Text="Priority Status: "></asp:Label>
                              </label>
                            </td>
                            <td colspan="4">
                            <label>
                                      <asp:DropDownList ID="ddl_priortyadd"  runat="server"  Width="100px" >                                                   
                                                           
                                                            <asp:ListItem Value="1">Low</asp:ListItem>
                                                             <asp:ListItem Value="2">Medium</asp:ListItem>
                                                            <asp:ListItem Value="3">High</asp:ListItem>
                                                        </asp:DropDownList>
                            </label> 
                            </td>
                            </tr>
                            <tr>
                            <td>
                            <label>
                            
                            <asp:Label ID="Label39" runat="server" Text="Project Type: "></asp:Label>
                             <asp:Label ID="Label42" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_projectype"
                                            ErrorMessage="*" InitialValue="0" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                            </td>
                            <td> 
                            <label style="width:250px">
                                       <asp:DropDownList ID="ddl_projectype" runat="server"  Width="250px" AutoPostBack="true">
                                        </asp:DropDownList>
                            </label>
                            <label>
                             <asp:ImageButton ID="imgadddepart" runat="server"  ImageUrl="~/images/AddNewRecord.jpg"  ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadddepart_Click" />
                             <asp:ImageButton ID="imgrefreshdepart" runat="server" AlternateText="Refresh"  ImageUrl="~/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px" Visible="false"  ImageAlign="Bottom" OnClick="imgrefreshdepart_Click" /> 
                             </label>
                            </td>
                            </tr>
                            <tr>
                            <td>
                           <label>
                                     <asp:Label ID="Label18" runat="server" Text="End Date: " ></asp:Label>
                                        
                                    </label>
                                </td>
                            <td colspan="4">
                                    <label>
                                        <asp:TextBox ID="txtenddate" runat="server" Width="100px" Visible="false" 
                                        ontextchanged="txtenddate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calenddate" runat="server" TargetControlID="txtenddate"
                                            PopupButtonID="txtenddate">
                                        </cc1:CalendarExtender>
                                    </label>
                            </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>
                                    <asp:Label ID="Label40" runat="server" Text="Incentive Value: " ></asp:Label>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtinsentive" runat="server"  Width="250px"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtinsentive" ErrorMessage="Numerical Values Only" 
                                        ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                    </label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Attachments: "></asp:Label>
                                    </label>
                                   
                                </td>
                                <td>
                                 <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" Visible="False" >
                                    </asp:CheckBox>
                                      <asp:RadioButtonList ID="Upradio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"  RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Enabled ="true">Audio File</asp:ListItem>
                                                            <asp:ListItem Value="2">Other Files</asp:ListItem>
                                                        </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                 <asp:Panel ID="pnluplod" runat="server" Visible="false">
                                          
                                                    <td width="30%">
                                                        <label>
                                                            <asp:Label ID="Label19" runat="server" Text="File Name: "></asp:Label>                                                          
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txttitlename" ErrorMessage="*" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttitlename" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                        </label>
                                                        </td>
                                                         <td >
                                                            <label style="width:250px">
                                                                 <asp:TextBox ID="txttitlename" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)" Width="250px"   runat="server"></asp:TextBox>
                                                            </label> 
                                                        <label style="width:50px">
                                                            Max <span id="Span10">30</span>
                                                          
                                                        </label>
                                                         <label style="width:80px">
                                                                <asp:Label ID="Label43" runat="server" Text="Select File"></asp:Label>
                                                            </label>
                                                           
                                                            <label style="width:200px">
                                                                <asp:FileUpload ID="fileuploadadattachment" runat="server" />
                                                                  <asp:FileUpload ID="fileuploadaudio" runat="server" />
                                                            </label>
                                                            <label style="width:80px">
                                                                    <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" Text="Upload" OnClick="Button2_Click" ValidationGroup="1" />
                                                            </label> 
                                                    </td>
                                                   
                                                   </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                    <td></td>
                                                    <td>
                                                      
                                                    </td>
                                                    </tr>
                                                   <tr>
                                                    <td width="10%">

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                            <Columns>
                                                               <%-- <asp:TemplateField HeaderText="PDFURL" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="File Name " HeaderStyle-HorizontalAlign="Left">
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
                                
                                <td colspan="4" align="center">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button1_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" 
                                        OnClick="btnupdate_Click" Text="Update" ValidationGroup="1" Visible="false" />
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" 
                                        OnClick="Button2_Click1" Text="Cancel" />
                                </td>
                               
                            </tr>
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
                                                <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" Text="Printable Version" onclick="Button4_Click" Visible="false" />
                                                <input id="Button5" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                                    style="width: 51px;" type="button" value="Print" visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label style="width:180px;" >
                                                    <asp:Label ID="Label6" runat="server" Text="Filter By Business Name: " ></asp:Label>                                              
                                                    <asp:DropDownList ID="ddlsortdept" runat="server" AutoPostBack="True" Width="180px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="false">
                                                    </asp:DropDownList>
                                                     <asp:DropDownList ID="ddlbusinessfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusinessfilter_SelectedIndexChanged" Width="180px">
                                                        </asp:DropDownList>
                                                </label>
                                                <label style="width:180px;">
                                                    <asp:Label ID="Label41" runat="server" Text="Department Name: "></asp:Label>
                                                    <asp:DropDownList ID="ddldepartmentfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepartmentfilter_SelectedIndexChanged"  Width="180px">
                                                    </asp:DropDownList>
                                             </label>
                                                <label style="width:180px;">
                                                    <asp:Label ID="Label5" runat="server" Text="Filter By Employee :"></asp:Label>
                                             
                                                    <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="180px"
                                                        OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
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
                                                 <label style="width:180px;">
                                                    <asp:Label ID="Label9" runat="server" Text="Filter By Project Date:"></asp:Label>
                                                    
                                                    <%-- <asp:Label ID="Label2" runat="server" Text="Filter By ProjectStatus:"></asp:Label>--%>
                                                <asp:DropDownList ID="ddlsortdate" runat="server" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="ddlsortdate_SelectedIndexChanged" Width="180px">
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
                                                 <label style="width:80px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Select Date:"></asp:Label>                                                    
                                                       <asp:TextBox ID="txtsortdate"  runat="server" Width="80px"> </asp:TextBox>
                                                <cc1:CalendarExtender ID="calsortdate" runat="server" PopupButtonID="txtsortdate"
                                                    TargetControlID="txtsortdate" Format="MM/dd/yyyy">
                                                </cc1:CalendarExtender>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              
                                                <label style="width:180px;">
                                                    <asp:Label ID="Label13" runat="server" Text="Search: "></asp:Label>
                                                    <asp:TextBox ID="txtsearch" runat="server" Width="180px" ></asp:TextBox>
                                                 </label>
                                                  <label style="width:180px;">
                                                
                                                    <asp:Label ID="Label17" runat="server" Text="Deadline Status: "></asp:Label>
                                                  
                                                     <asp:DropDownList ID="DropDownList1" runat="server" Width="180px" >
                                                     <asp:ListItem Value="0">-select-</asp:ListItem>
                                                            <asp:ListItem Value="1">Overdue</asp:ListItem>
                                                            <asp:ListItem Value="2">Due Today</asp:ListItem>
                                                            <asp:ListItem Value="3">Due Tomorrow</asp:ListItem>
                                                            <asp:ListItem Value="4">Due This Week</asp:ListItem>
                                                            <asp:ListItem Value="5">Due This Month</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                 <label style="width:180px;">
                                                
                                                    <asp:Label ID="Label23" runat="server" Text="Priority Status: "></asp:Label>
                                                  
                                                     <asp:DropDownList ID="ddl_priority" runat="server" Width="180px">
                                                     <asp:ListItem Value="0">-All-</asp:ListItem>
                                                            <asp:ListItem Value="1">Low</asp:ListItem>
                                                            <asp:ListItem Value="2">Medium</asp:ListItem>
                                                            <asp:ListItem Value="3">High</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                      <label style="width:180px;">
                                                            Reminder Date:
                                                    
                                                  <asp:TextBox ID="txt_remindersearch"  runat="server" Width="80px"> </asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txt_remindersearch"
                                                    TargetControlID="txt_remindersearch" Format="MM/dd/yyyy">
                                                </cc1:CalendarExtender>
                                                </label> 

                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                <asp:Button ID="btngo" runat="server" CausesValidation="false"  onclick="btngo_Click" Text="Go" />
                                                </label>

                                                  
                                                    
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                
                                                <asp:GridView ID="grdmonthlygoal" runat="server" CssClass="mGrid"   onrowcommand="grdmonthlygoal_RowCommand1" PageSize="15"  AutoGenerateColumns="False" Width="100%"  onrowdeleting="grdmonthlygoal_RowDeleting" 
                                                    onrowediting="grdmonthlygoal_RowEditing" onrowupdating="grdmonthlygoal_RowUpdating" PagerStyle-CssClass="prg" AllowPaging="True"  onselectedindexchanged="grdmonthlygoal_SelectedIndexChanged"   onpageindexchanging="grdmonthlygoal_PageIndexChanging1"
                                                      onrowdatabound="grdmonthlygoal_RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproId" runat="server" Text='<%#Bind("ProjectMaster_Id")%>'></asp:Label>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("ProjectMaster_Employee_Id")%>'></asp:Label>
                                                                <asp:Label ID="lbldep" runat="server" Text='<%#Bind("ProjectMaster_DeptID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dept Name" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label120" runat="server" Text='<%#Bind("Name")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121" runat="server" Text='<%#Bind("Name1")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Title">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Gray" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                                            CommandName="view111"  Text='<%# Eval("ProjectMaster_ProjectTitle") %>'
                                                                    ToolTip="View Project Profile" >LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                           <asp:TemplateField HeaderText="Priority">
                                                            <ItemTemplate >
                                                                <asp:Label ID="Label_Priority" runat="server" Text='<%# Eval("Priority") %>' ></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Date"  >
                                                            <ItemTemplate >
                                                                <asp:Label ID="Label122" runat="server" Text='<%# Eval("ProjectMaster_StartDate","{0:dd.MM.yyyy}") %>' ></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label123" runat="server" Text='<%# Eval("ProjectMaster_EndDate","{0:dd.MM.yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target End Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:dd.MM.yyyy}") %>' ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
                                                                 <%--<asp:Label ID="Label23" runat="server" Text=" / Overdue" ForeColor="Red" Font-Bold="True" ></asp:Label>--%>
                                                                
                                                                <asp:Label ID="Label124" runat="server" Text='<%# Eval("ProjectMaster_TargetEndDate","{0:dd.MM.yyyy}") %>' ></asp:Label>
                                                                <%--<asp:Label ID="Label25" runat="server" Text=" / Due Today"  Font-Bold="True" ></asp:Label>--%>
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
                                                                 <%--<asp:Label ID="lblcompleted" runat="server" Text='<%# Eval("ProjectMaster_ProjectStatus") %>' ForeColor="Green" Font-Bold="True"></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/adda.png" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                
                                                                <asp:ImageButton ID="Image" runat="server" ImageUrl="~/Account/images/adda.png" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    CommandName="View" ToolTip="Add Progress Report" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/edit.gif"  ToolTip="Edit"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ProjectMaster_Id") %>'
                                                                    ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                              
                                                
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
                                                <asp:CheckBox ID="ChkActive" runat="server" Checked="true" Text="Active" Visible="false" />
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
<asp:Panel ID="pnl_addtype" runat="server" Visible="false"  BackColor="Gray"  >
            <div style="position:absolute;  margin: -1820px -30px -3px 306px; width:700px; background-color: #dddddd;" >
           
              <fieldset>
        <legend>
            <asp:Label ID="Label26" runat="server" Text="Manage Project Type"></asp:Label>
        </legend>
         <asp:Panel ID="Panel2" runat="server">
        <table>
           
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label27" runat="server" Text="Select Employee"></asp:Label>
                                        <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="DropDownList2emp1252523" runat="server" ControlToValidate="ddl_empfortype"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                      <asp:DropDownList ID="ddl_empfortype" runat="server" Width="250px" AutoPostBack="false" OnSelectedIndexChanged="ddl_empfortype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Project Type Name"></asp:Label>
                                        <asp:Label ID="Label31" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
                                       
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtaddtype" runat="server" Width="300px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            
                            </td>
                            <td>
                             <asp:Button ID="Button6" runat="server" CausesValidation="false"  onclick="btngo_Clicktype" Text="Add" />
                              <asp:Button ID="Button10" runat="server" CausesValidation="false"  onclick="btngo_Clicktype1" Text="Cancel" />
                            </td>
                            </tr>
        </table> 
        <table width="100%">
        <tr>
        <td>
        
        </td>
        </tr>
        <tr >
        <td style="width:100%">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%"
                                                   PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                                                   DataKeyNames="ProjectTypeID" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                    OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" AllowSorting="True"
                                                    OnSorting="GridView1_Sorting">
                                                    <Columns>                                                      
                                                       
                                                        <asp:TemplateField HeaderText="Type_Name" HeaderStyle-HorizontalAlign="Left" SortExpression="Project Type" HeaderStyle-Width="60%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesignationname" runat="server" Text='<%#Bind("Type_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:ButtonField CommandName="vi" HeaderImageUrl="~/Account/images/edit.gif" ButtonType="Image"
                                                            ImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Text="Edit"
                                                            HeaderText="Edit" ValidationGroup="2">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" CommandName="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("ProjectTypeID") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:CommandField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ShowDeleteButton="True" />--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
        
        </td>
        
        </tr>
        </table> 
        </asp:Panel>
       
      </fieldset>
              
            </div>
      </asp:Panel>
      



 <asp:Panel ID="pnl_paydetail" runat="server" Visible="false"  BackColor="Gray"  Width="100%">
            <div style="position:relative;  margin: -8px 0px 0px 0px;  width:100%; background-color: #dddddd;" >
           
              <fieldset>
        <legend>
            <asp:Label ID="Label119" runat="server" Text="Add Progress Report"></asp:Label>
        </legend>
         <asp:Panel ID="pnl_licence" runat="server">
        
              <fieldset>
               <div style="margin-left: 1%">
                <asp:Label ID="lblstsmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
              <table width="70%" align="center"  >

       
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Status Report: "> </asp:Label>
                                    </label>
                                    <%--<asp:CheckBox ID="ChkProDesc" AutoPostBack="true" runat="server" OnCheckedChanged="ChkProDesc_CheckedChanged" />--%>
                                </td>
                                <td width="70%">
                                    <label style="width:100%">
                                       
                                            <cc2:HtmlEditor ID="txtprogressjkjk"   runat="server" Visible="false"></cc2:HtmlEditor>
                                             <asp:TextBox ID="txtprogress" runat="server" Width="750px" Height="100px" TextMode="MultiLine"> </asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                             <tr>
                                <td>
                                    <label>
                                        Reporting Date: 
                                    </label>
                                </td>
                                <td colspan="1">
                                    <label>
                                        <asp:TextBox ID="txtreportingdate" runat="server" Width="100px"> </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreportingdate"
                                            PopupButtonID="txtreportingdate"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                   
                                   
                                       
                                    </label>
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
                                        <asp:TextBox ID="txt_reminder" runat="server" Width="100px"> </asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_reminder" PopupButtonID="txt_reminder"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                   
                                   
                                       
                                    </label>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="5">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Upload Documents: "></asp:Label>
                                    </label>
                                    <asp:CheckBox ID="Chkupld" runat="server" AutoPostBack="True"  oncheckedchanged="Chkupld_CheckedChanged" Visible="False">
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
                                                            <asp:Label ID="Label10" runat="server" Text=" File Name"></asp:Label>
                                                            <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtststitle"
                                                                ErrorMessage="*" ValidationGroup="2"> </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtststitle" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"  ValidationGroup="1"> </asp:RegularExpressionValidator>
                                                            <label>
                                                            <asp:TextBox ID="txtststitle" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)" runat="server"></asp:TextBox>
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
                                                                <asp:Label ID="Label16" runat="server" Text=" Audio File"></asp:Label>
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
                                                    <td colspan="5">
                                                        <asp:GridView ID="gridstsFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            Width="70%" onrowcommand="gridstsFileAttach_RowCommand">
                                                            <Columns>
                                                              <%--  <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblstspdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="URL" HeaderStyle-HorizontalAlign="Left">
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
                                <td align="center" colspan="2" style="width: 100%" width="50%">
                                    <asp:Button ID="Button8" runat="server" CssClass="btnSubmit" 
                                        onclick="Button8_Click" Text="Submit" />
                                    <asp:Button ID="Button9" runat="server" CssClass="btnSubmit" 
                                        onclick="Button9_Click" Text="Cancel" />
                                </td>
                            
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <div style="margin-left: 1%">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </div>
                            </tr>
           
            </table>       
            </fieldset>           
             
      </asp:Panel> 
       </div>
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
<asp:PostBackTrigger ControlID="rdoselct"></asp:PostBackTrigger>

<asp:PostBackTrigger ControlID="txtprogress"></asp:PostBackTrigger>

        </Triggers>

        


</asp:UpdatePanel>
<div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>
</asp:Content>
