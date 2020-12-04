<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="MyDailyITWorkReport.aspx.cs" Inherits="MyDailyWorkReport" Title="My Daily Work Report -Manage" %>

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

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }   
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
             <asp:Panel ID="pnl_add" runat="server" Visible="false">
                                    
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" Text="Employee Daily Work  Report" runat="server"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label3" Text="Employee Name" runat="server"></asp:Label>
                                </label>
                            </td>
                            <td width="70%">
                                <label>
                                    <asp:DropDownList ID="ddlemployee" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label4" Text="Date " runat="server"></asp:Label>
                                    <asp:Label ID="Label5" Text="*" runat="server" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttargetdatedeve"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="70%">
                                <label>
                                    <asp:TextBox ID="txttargetdatedeve" runat="server" Width="70px" OnTextChanged="txttargetdatedeve_TextChanged" Enabled="false" 
                                        AutoPostBack="True"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttargetdatedeve"
                                        PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                                <label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" Enabled="false" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Type Of Work
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Enabled="false">
                                        <asp:ListItem Value="1">Developer</asp:ListItem>
                                        <asp:ListItem Value="2">Tester</asp:ListItem>
                                        <asp:ListItem Value="3">Supervisor</asp:ListItem>
                                        <asp:ListItem Value="5">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label36" Text="Main Work" runat="server"></asp:Label>
                                    
                                </label>
                            </td>
                            <td width="70%">
                            <table>
                            <tr>
                            <td>
                             <label style="width:400px;">
                                    <asp:TextBox ID="txt_mainwork" runat="server" Width="400px" Enabled="false" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </label>
                               
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Bud Hrs Main Task :"  style="font-weight: 700"></asp:Label>
                                            <asp:Label ID="lblmaintaskbudhour" runat="server" Text="00:00"></asp:Label>
                                        </td>
                                        <td>
                                            
                                            &nbsp;&nbsp; &nbsp;</td>
                                        <td>
                                          
                                            <asp:Label ID="Label23" runat="server" Text="Cost :" style="font-weight: 700"></asp:Label>
                                            <asp:Label ID="lblmaintaskcost" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                <tr>
                                <td>    
                                    <asp:Label ID="Label15" runat="server" Text="Act Hrs Main Task :"   style="font-weight: 700"></asp:Label>
                                    <asp:Label ID="lbltotalhourspentonmaintask" runat="server" Text="00:00"></asp:Label>
                                </td>
                                <td>
                                    
                                    
                                    &nbsp; &nbsp;</td>
                                   
                                <td>
                                 <asp:Label ID="Label22" runat="server" Text="Cost :" style="font-weight: 700"></asp:Label>
                                   <asp:Label ID="lblspenthourcost" runat="server" Text="0"></asp:Label>   
                                </td>                                    
                                <td>
                                  
                                </td>
                                </tr>                        
                                <tr>
                                <td>
                                    <strong>Incentive Value :</strong>
                                      <asp:Label ID="lbl_incentiveAct" runat="server" Text="00.00"></asp:Label>
                                </td>
                                <td>
                                        
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                </tr>                        
                                
                                        </table> 
                            
                            </td>
                            </tr>
                            </table> 
                                 
                              
                              
                            
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label6" Text="Today's Work" runat="server"></asp:Label>
                                    <asp:Label ID="Label13" Text="*" runat="server" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldVdsalidator4" runat="server" ControlToValidate="ddlworktitle"
                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td width="70%">
                                <label style="width:600px;">
                                    <asp:DropDownList ID="ddlworktitle" runat="server" Width="600px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlworktitle_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txt_title" runat="server" Width="600px" Enabled="false" Height="25px" ></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label7" Text="Work Report " runat="server"></asp:Label>
                                    <asp:Label ID="Label8" Text="*" runat="server" CssClass="labelstar"></asp:Label>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtworkdonereport"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                                   <%-- 
                                    <asp:RegularExpressionValidator ID="RegularExprdfdsdfsdffdessionValidator5" runat="server"
                                        ControlToValidate="txtworkdonereport" SetFocusOnError="True" ErrorMessage="*"
                                        ValidationExpression="^([\S\s]{0,100})$" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                </label>
                            </td>
                            <td width="70%">
                            <table>
                            <tr>
                            <td>
                                    <label style="width:400px;">
                                    <asp:TextBox ID="txtworkdonereport" 
                                        runat="server" Height="135px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                     <%--   onkeypress="return checktextboxmaxlength(this,200,event)"
                                        onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_. ]+$/,'div1',200)"--%>
                                </label>
                            
                            </td>
                            <td>
                                    <table>
                                    <tr>
                                    <td>
                                            
                                    <asp:Label ID="Label11" runat="server" Text="Bud. Hrs Today's Task :"  style="font-weight: 700"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="00:00"></asp:Label>
                                    </td>
                                    <td>
                                        
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>
                                               
                                               <asp:Label ID="Label24" runat="server" Text="Cost :" style="font-weight: 700"></asp:Label>
                                               <asp:Label ID="lbltodaytaskcost" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td>
                                             
                                    </td>
                                    </tr>
                                    
                                        <tr>
                                    <td>
                                             <asp:Label ID="Label17" runat="server" Text="Act. Hrs Today's Task :"  style="font-weight: 700"></asp:Label>
                                             <asp:Label ID="lblemprequestedhour" runat="server" Text="00:00"></asp:Label>
                                    </td>
                                    <td>
                                    
                                    </td>
                                    <td>
                                         <asp:Label ID="Label20" runat="server" Text="Cost :" style="font-weight: 700"></asp:Label>
                                         <asp:Label ID="lblrequestedempcost" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td>
                                             
                                    </td>
                                    </tr>
                                    
                                    <tr>
                                    <td>
                                             <strong>Incentive Value :</strong>
                                             <asp:Label ID="txt_incentive" runat="server" Text="00.00"></asp:Label>
                                    </td>
                                    <td>
                                                 <asp:TextBox ID="txt_incentive1" runat="server" Width="60px" MaxLength="5" Enabled="false" Visible="false"></asp:TextBox>
                                          
                                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          
                                    </td>
                                    <td>
                                    
                                    </td>
                                    <td>
                                    
                                    </td>
                                    </tr>
                                    
                                    </table> 
                            </td>
                            </tr>
                            </table> 
                                <label>
                                    <asp:Label ID="Label21" Text="Max" runat="server" CssClass="labelcount" Visible="false" ></asp:Label>
                                    <span id="div1" cssclass="labelcount" runat="server" visible="false">200</span>
                                    <asp:Label ID="lblinvstiename" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)" Visible="false"></asp:Label>
                                </label>
                                
                            </td>
                        </tr>


                        
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label9" Text="Hour Spent on Today's Task" runat="server"></asp:Label>
                                    <asp:Label ID="Label10" Text="*" runat="server" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txthourspent"
                                        ErrorMessage="*" ValidationGroup="1">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExprfdsdfessionValidator3" runat="server"
                                        ControlToValidate="txthourspent" ErrorMessage="Invalid" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$"
                                        ValidationGroup="1">
                                    </asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td width="70%">
                                <label>
                                    <asp:TextBox ID="txthourspent" runat="server" Width="60px" MaxLength="5">00:00</asp:TextBox>
                                  
                                </label>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="HH:MM"></asp:Label>
                                </label>
                            </td>
                        </tr>
                         <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label19" runat="server" Text="Employee Effective Rate per Hour"></asp:Label>
                                </label>
                            </td>
                            <td width="70%">
                                <label>
                                    <asp:Label ID="lblempeffectiverate" runat="server" Text=""></asp:Label>
                                </label>
                            </td>
                        </tr>
                        
                       
                        
                         
                        <tr>
                            <td width="30%">
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Today's Task allocation mentioned above is completely finished as asked ?"></asp:Label>
                                </label>
                            </td>
                            <td width="70%">
                              
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Value="1" >Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnltest" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td width="30%">
                                                <label>
                                                    <asp:Label ID="Label18" runat="server" Text="Actual hour required to finish today's task">
                                                    </asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtactualhourrequired"
                                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$">
                                                    </asp:RegularExpressionValidator>
                                                </label>
                                            </td>
                                            <td width="70%">
                                                <label>
                                                    <asp:TextBox ID="txtactualhourrequired" runat="server" Width="60px">00:00</asp:TextBox>
                                                </label>
                                            </td>

                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                      
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlmaintaskstatus" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td width="30%">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text=" Main Task is completely finished and tested by you ? Yes "></asp:Label>
                                                </label>
                                                <td width="70%">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                            </td>
                            <td width="70%">
                                <asp:Button ID="Button1" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click"
                                    ValidationGroup="1" />
                                <span lang="en-us">&nbsp;</span><asp:Button ID="Button2" CssClass="btnSubmit" runat="server"
                                    Text="Cancel" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnl_grid" runat="server">
                <fieldset>
                <legend>
                        <asp:Label ID="Label35" Text="List Of Employee Daily Work  Report" runat="server"></asp:Label>
                    </legend>
                <table>
                <tr>
                <td>
                <table>
                <tr>
                <td>
                 <label>
                 Employee Name :
                                <asp:DropDownList ID="ddl_empsearch" runat="server" Enabled="false" Width="180px" >
                                </asp:DropDownList>
                            </label>
               
               
                 <label>Product Name :
                                <asp:DropDownList ID="ddlwebsite" runat="server"  AutoPostBack="true" Width="180px" OnSelectedIndexChanged="ddlwebsite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
               
               
                <label>
                Page Name :
                                <asp:DropDownList ID="ddlpagename" runat="server" Width="180px" >
                                </asp:DropDownList>
                            </label>
                
               
                
                 <label>
               Main Status :                <asp:DropDownList ID="ddlmaintaskstatus" runat="server"  Width="120px" >
                                    <asp:ListItem Value="2">All</asp:ListItem>
                                    <asp:ListItem Value="1">Complete</asp:ListItem>
                                    <asp:ListItem Value="0">Pending</asp:ListItem>
                                </asp:DropDownList>
                            </label>
               
                <label style="width:50px">
                <asp:Button ID="Button8" runat="server" CssClass="btnSubmit" Text="Send Mail" onclick="SendMailtoadmin"/>
                </label> 
                </td>                
                </tr>

                <tr>
                  <td >
                            <label style="width:80px">
                            <asp:Label ID="Label26" runat="server" Text="Date From :"></asp:Label>
                            </label>
                             <label style="width:105px">
                              
                                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                            </label> 
                            <label style="width:50px">
                           
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                                PopupButtonID="ImageButton1">
                            </cc1:CalendarExtender>
                       
                            <label style="width:30px">
                                <asp:Label ID="Label25" runat="server" Text="To :"></asp:Label>
                                  </label>
                                    <label style="width:115px">
                             
                                <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox1"
                                    ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                                 <label style="width:40px">
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                            </label>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox2"
                                PopupButtonID="ImageButton3">
                            </cc1:CalendarExtender>
                       
                 
                        <label style="width:50px">
                        Search 
                        </label>
                        <label> 
                         <asp:TextBox ID="TextBox5" runat="server"   placeholder="Search"  Font-Bold="true"  Width="250px" ></asp:TextBox>
                        </label> 
                        <label style="width:50px;">
                        <asp:Button ID="Button3" runat="server" Text="Go" OnClick="Button1_ClickSearch" CssClass="btnSubmit" />
                        </label> 
                        </td>
                        
                </tr>
                <tr>
                <td >
                <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False" PageSize="30" AllowPaging="true"
                  OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                                    EmptyDataText="No Records Found." DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                                    ShowFooter="true" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="GridView2_Sorting"
                                                    OnRowDataBound="GridView2_RowDataBound">
                                                     <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Employee" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" Visible="false"       ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                                                <asp:Label ID="lblempid" runat="server" Text='<%#Bind("EmployeeId") %>' Visible="false" ></asp:Label>
                                                                <asp:Label ID="lbl_mainid" runat="server" Text='<%#Bind("Id") %>' Visible="false" ></asp:Label>                                                                
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="workallocationdate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblreportdate" runat="server" Text='<%#Bind("workallocationdate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Type of work" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworktype" runat="server" Text='<%#Bind("Statuslabel") %>'></asp:Label>
                                                                <asp:Label ID="lblworktypeid" runat="server" Text='<%#Bind("Typeofwork") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            SortExpression="ProductName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwebsitename12345" runat="server"  Text='<%#Bind("ProductName")%>' ></asp:Label>
                                                                <asp:Label ID="lblprodect" runat="server"  Text='<%#Bind("ProductName")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Page Name" SortExpression="PageTitle" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                  <asp:Label ID="lblpagename123" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>                                                             
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Main Task" SortExpression="WorkRequirementTitle" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                                <asp:Label ID="lblworktitle12345" runat="server" Text='<%#Bind("WorkRequirementTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        
                                                        <asp:TemplateField HeaderText="Today's Task" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="WorkDoneReport">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworktobedone" runat="server" Text='<%#Bind("worktobedone") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Work Report" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="WorkDoneReport">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworktobedonsse" runat="server" Text='<%#Bind("WorkDoneReport") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Attach." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Text="Download" OnClick="link1_Click" Visible="false"></asp:LinkButton>
                                                                <asp:ImageButton ID="imageBTN_IWork" ToolTip="Update" runat="server" OnClick="link1_Click" Visible="true" ImageUrl="~/Account/images/attach.jpg" ></asp:ImageButton>
                                                                <asp:Label ID="lbl_noattach" runat="server" Text="None" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblfillreport" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_workid" runat="server" Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Today's Task<br> Status" HeaderStyle-HorizontalAlign="Left"   ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                             <asp:Label ID="lblwebsitenameWorkDone" runat="server"  Text='<%#Bind("WorkDone")%>' ></asp:Label>
                                                                
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lbl_textTotal" runat="server" ForeColor="Black" Text="Total :" ></asp:Label>
                                                                </FooterTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Main Task <br>Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            SortExpression="ProductName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmaintaskstatus" runat="server"></asp:Label>
                                                                 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Incent. Off" HeaderStyle-HorizontalAlign="Left"                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_inceoffer" runat="server" Text='<%#Bind("Offer_Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_totaloffer" runat="server" ForeColor="Black" Text="Text" ></asp:Label>                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Incent. Earn" HeaderStyle-HorizontalAlign="Left"                                                             ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_earn" runat="server" Text='<%#Bind("Earn_Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_TotalEarnoffer" runat="server" ForeColor="Black" Text="Text" ></asp:Label>                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         
                                                         <asp:TemplateField HeaderText="Bud.Hr Today Task" SortExpression="budgetedhour" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudgetd132" runat="server" Text='<%#Bind("budgetedhour") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblftrtodaybudhrtotal" runat="server" ForeColor="Black"></asp:Label>
                                                                <asp:Label ID="lblhronly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblminuteonly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Act. Hrs Today" SortExpression="HourSpent" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactualhour" runat="server" Text='<%#Bind("HourSpent") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>                                                            
                                                                <asp:Label ID="lblftractualtodaytask" runat="server" ForeColor="Black" Text="" ></asp:Label>
                                                                 <asp:Label ID="lblacthronly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblactminuteonly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField> 
                                                       
                                                        <asp:TemplateField HeaderText="Budg. Hrs Main" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudhrmaintask" Text='<%#Bind("budgetedhour") %>' runat="server"></asp:Label>
                                                                 </ItemTemplate>
                                                             <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_budghurfootr" runat="server" ForeColor="Black" Text="" ></asp:Label>
                                                                 
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Act.Hr Main Task" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactualhrmaintask" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      


                                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText=""  ItemStyle-HorizontalAlign="Left"  HeaderStyle-Height="20px" >
                                                             <ItemTemplate>
                                                                  <%-- <asp:Button ID="Button15" runat="server" Text="Edit"  CommandName="nnn"  CommandArgument='<%# Eval("ID") %>' /> --%>
                                                           <%-- <asp:Label ID="lbl_allredySend" runat="server" Text="View Report" ForeColor="Green" Font-Size="16px"></asp:Label>--%>
                                                            <asp:LinkButton ID="LinkButton2Show" runat="server" Text='Edit Report' ForeColor="Green"  CommandArgument='<%# Eval("Id") %>'  CommandName="Select" Visible="false">Edit Report</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5Edit" runat="server" Text='Fill Report' ForeColor="Red"  CommandArgument='<%# Eval("Id") %>'  CommandName="Select" >Fill Report</asp:LinkButton>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>

                                                       
                                                       
                                                       

                                                      
                                                     
                                                <%--       Hide All Extra                                                        --%>
                                                       
                                                        <asp:TemplateField HeaderText="Today's Work Report" SortExpression="WorkDoneReport" Visible="false" 
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworkreport" runat="server" Text='<%#Bind("WorkDoneReport") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                       
                                                        <asp:TemplateField HeaderText="Today's Task Status" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltodaytaskstatus" runat="server" Text='<%#Bind("TodayStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="File Uploaded" HeaderStyle-HorizontalAlign="Left" Visible="false"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Text="FileUpload"
                                                                    OnClick="link312_Click">
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblfileupload" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="PageWorkTblId">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmasterId" runat="server" Text='<%#Bind("PageWorkTblId") %>'></asp:Label>
                                                                <asp:Label ID="lblmyworktblid" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Ver. No" SortExpression="VersionNo" HeaderStyle-HorizontalAlign="Left" Visible="false"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblversionno123" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate Per Hour" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                            SortExpression="EffectiveRate" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrateperhour" runat="server" Text='<%#Bind("EffectiveRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    

                                                       <%--added by me--%>

                                                        <asp:TemplateField HeaderText="Act. Hr Main Task" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActHrMainTask" runat="server" Text='<%#Bind("HourSpent") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_ActHourMainTaskFTRss" runat="server" ForeColor="Black" Text="0.00" ></asp:Label>                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                     <AlternatingRowStyle BackColor="White" />    
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                   <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                   <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                </td>
                </tr>
                </table> 
                </td>
                </tr>
                
                <tr>
                <td align="right">
                 <asp:Label ID="lbl_totalinctiv" runat="server" ForeColor="Black" Text="" ></asp:Label>
                 <asp:Label ID="lbl_earn" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                </td>
                </tr>
                
                 <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Gray" Width="60%" 
                                BorderStyle="Solid" BorderWidth="5px" Height="570px" ScrollBars="Both">

                          
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>Work Intruction File </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel7" runat="server"  HorizontalAlign="Center">
                                                                <asp:GridView ID="GridView3" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                                    DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltitledailywork" runat="server" Text='<%#Bind("FileTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpdfurldailywork" runat="server" Text='<%#Bind("WorkRequirementPdfFilename") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaudiourldailywork" runat="server" Text='<%#Bind("WorkRequirementAudioFileName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldate123dailywork" runat="server" Text='<%#Bind("Date" ,"{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                               <asp:LinkButton ID="linkdow1dailywork" runat="server" Text="Download" OnClick="linkdow1dailywork_Click"
                                                                                    ForeColor="Black"></asp:LinkButton>
                                                                                    
                                                                                <asp:UpdatePanel ID="UpdatePanel1grd" runat="server">
                                                                                    <ContentTemplate>
                                                                                     
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                       
                                                                                        <asp:PostBackTrigger ControlID="linkdow1dailywork" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                       <asp:Panel ID="Panel5" runat="server" Visible="false">
                                    <tr>
                                        <td>
                                            <label>
                                                Page Work Detail
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblpaged" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                 
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label27" runat="server" Text="Website Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblwebsitenamedetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label28" runat="server" Text="Page Title"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblpagenamedetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label29" runat="server" Text="Version no"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblnewpageversion" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label30" runat="server" Text="File Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblnewpagedetaildetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label31" runat="server" Text="Work Title"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblworktitledetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000; height: 22px;">
                                            <label>
                                                <asp:Label ID="Label32" runat="server" Text="Work Description"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000; height: 22px;">
                                            <label>
                                                <asp:Label ID="lblworkdescriptiondetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label33" runat="server" Text="Targate Date"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lbltargatedatedetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="Label34" runat="server" Text="Budgedet Hour"></asp:Label>
                                            </label>
                                        </td>
                                        <td style="width: 70%; font-size: 12px; color: #000000;">
                                            <label>
                                                <asp:Label ID="lblbudgetedhourdetail" runat="server"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                     
                                        <td colspan="2">
                                            <fieldset>
                                                <legend>Page Version Intruction File </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" HorizontalAlign="Center"
                                                                Height="115px">
                                                                <asp:GridView ID="GridView1" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                                    DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("FileTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("WorkRequirementPdfFilename") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("WorkRequirementAudioFileName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldate123" runat="server" Text='<%#Bind("Date" ,"{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkdow1" runat="server" Text="Download" OnClick="linkdow1_Click"
                                                                                    ForeColor="Black"></asp:LinkButton>
                                                                                    
                                                                                     <asp:UpdatePanel ID="UpdatePanel1grd" runat="server">
                                                                                    <ContentTemplate>
                                                                                     
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                       
                                                                                        <asp:PostBackTrigger ControlID="linkdow1" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                      
                                    </tr>
                                     
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>Source Code Files </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" HorizontalAlign="Center"
                                                                Height="120px">
                                                                <asp:GridView ID="grdsourcefile" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"
                                                                    DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblspagetitle" runat="server" Text='<%#Bind("PageTitle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="VersionNumber" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblviesionno" runat="server" Text='<%#Bind("VersionNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FileName" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                            ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FileName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfilenam" runat="server" Text='<%#Bind("upfile") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldates" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkdow" runat="server" Text="Download" OnClick="linkdow_Click"
                                                                                    ForeColor="Black"></asp:LinkButton>
                                                                                     <asp:UpdatePanel ID="UpdatePanel1grd" runat="server">
                                                                                    <ContentTemplate>
                                                                                     
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                       
                                                                                        <asp:PostBackTrigger ControlID="linkdow" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                     </asp:Panel>
                                    
                                </table>
                            </asp:Panel>
                            <asp:Button ID="Button4" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="Panel1" TargetControlID="Button4" CancelControlID="ImageButton5">
                            </cc1:ModalPopupExtender>
                            <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        </td>
                    </tr>
                
                </table> 
                </fieldset> 
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V4 11Dec Develop By Pk" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
