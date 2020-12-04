<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="WorkAllocationForEmployee.aspx.cs" Inherits="WorkAllocationForEmployee"
    Title="WorkAllocation For Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

     <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
        
        .button {
   border-top: 1px solid #96d1f8;
   background: #65a9d7;
   background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
   background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
   background: -moz-linear-gradient(top, #3e779d, #65a9d7);
   background: -ms-linear-gradient(top, #3e779d, #65a9d7);
   background: -o-linear-gradient(top, #3e779d, #65a9d7);
   padding: 10px 80px;
   -webkit-border-radius: 0px;
   -moz-border-radius: 0px;
   border-radius: 0px;
   -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
   -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
   box-shadow: rgba(0,0,0,1) 0 1px 0;
   text-shadow: rgba(0,0,0,.4) 0 1px 0;
   color: white;
   font-size: 18px;
   font-family: Georgia, serif;
   text-decoration: none;
   vertical-align: middle;
   }
    </style>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
              <table width="100%">
                <tr>
                <td width="50%" align="center">
                            <asp:Button ID="addnewpanel" runat="server" Text="New IT Work Allocation" CssClass="button"  OnClick="addnewpanel_Click" />
                </td>
                <td width="50%" align="center">
                                <asp:Button ID="Button4" runat="server" Text="Reallocate Unfinished IT Work" CssClass="button"  OnClick="addnewpanel_Click1" />
                </td>
                </tr>
              </table> 
                
        <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">                    
            <div class="products_box">
                <fieldset>
                    <legend>Work Allocation for Employee </legend>
                    <table id="pagetbl" width="100%">
                        <tr>
                            <td style="width: 27%">
                                <label>
                                    Date:
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttargetdatedeve"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttargetdatedeve"
                                        ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td>
                                <label style="width:120px;">
                                    <asp:TextBox ID="txttargetdatedeve" runat="server" Width="120px" OnTextChanged="ddlemployee_SelectedIndexChanged" AutoPostBack="True"></asp:TextBox>
                                </label>
                                <label  style="width:50px;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttargetdatedeve"  PopupButtonID="ImageButton2">
                                    </cc1:CalendarExtender>
                                </label>
                            </td>
                            <td >
                                <label>
                                    Total Work Hour Allocated
                                </label>
                            </td>
                            <td >
                                <label>
                                    <asp:Label ID="lbltotalworkallocatedtoday" runat="server" Font-Bold="True"></asp:Label>
                                </label>
                            </td>
                            <td >
                            </td>
                            <td>
                            </td>
                        </tr>
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
                            <td>
                            </td>
                              <td>
                            </td>
                            <td>
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
                                  <td>
                            </td>
                              <td>
                            </td>
                            <td>
                            </td>
                            </tr>
                                             
                        <tr>
                            <td>
                                <label>
                                    Employee Name: 
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
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
                                <label>
                                    Type Of Work: 
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                         <asp:ListItem Value="4">Designer</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Developer</asp:ListItem>
                                        <asp:ListItem Value="5">Other</asp:ListItem>
                                        <asp:ListItem Value="3">Supervisor</asp:ListItem>
                                        <asp:ListItem Value="2">Tester</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Product Name: 
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:DropDownList ID="ddlproductname" runat="server" AutoPostBack="True" Width="600px"
                                        OnSelectedIndexChanged="ddlproductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Page Name: 
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" Width="600px" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Pending Page Work Title: 
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:DropDownList ID="ddlworktitle" runat="server" Width="600px" AutoPostBack="True" OnSelectedIndexChanged="ddlworktitle_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Main Task Bud. Hour: 
                                </label>
                            </td>
                             <td>
                             <table>
                             <tr>
                                    <td>
                                <label style="width:40px">
                                    <asp:Label ID="lblmasterbudgetedhour" runat="server" Font-Bold="True">00:00</asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:120px">
                                    Rate Per Hour
                                </label>
                            </td>
                            
                           

                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblmasterbudhrrate" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    Cost
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblmasterbudcost" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td> 

                             </tr>
                             </table> 
                             </td>                                 
                            
                        </tr>
                        <tr>
                            <td>
                                <label style="width:300px;">
                                    Act. Hrs. spent on main task till yesterday:
                                </label>
                            </td>
                            <td>
                                <table>
                                <tr>
                                 <td>
                                <label style="width:40px">
                                    <asp:Label ID="lblactualhourtillyesterday" runat="server" Font-Bold="True">00:00</asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:120px">
                                    Rate Per Hour
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblactualrate" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    Cost
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblactualcost" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td>
                                </tr>
                                </table> 
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="width: 27%">
                                <label>
                                    Employee requested Hour: 
                                </label>
                            </td>
                            <td>
                            <table>
                                <tr>
                                    
                                         <td>
                                <label style="width:40px">
                                    <asp:Label ID="lblemprequestedhour" runat="server" Font-Bold="True" Text="00:00"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:120px">
                                    Rate Per Hour
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblemprequestedrate" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    Cost
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lblrequestedcost" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                </label>
                            </td>
                                  
                                </tr>
                            </table> 
                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                                <label style="width:300px">
                                    Today's Task bud. Hour: 
                                    <asp:Label ID="Label1" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttodaybudgetdhour"
                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttodaybudgetdhour"
                                        ErrorMessage="Only HH:MM" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([0-1][0-9]|[2][0-3]):(([0-5][0-9]))$">
                                    </asp:RegularExpressionValidator>
                                </label>
                            </td>
                            <td>
                            <table>
                                <tr>
                                  
                                               <td>
                                <label style="width:40px">
                                    <asp:TextBox ID="txttodaybudgetdhour" runat="server" Width="40px" AutoPostBack="True"
                                        OnTextChanged="txttodaybudgetdhour_TextChanged">00:00</asp:TextBox>
                                </label>
                            </td>
                            <td>
                                <label style="width:120px">
                                    Rate Per Hour: 
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lbltodayhrrate" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    Cost
                                </label>
                            </td>
                            <td>
                                <label style="width:50px">
                                    <asp:Label ID="lbltodayscost" runat="server" Font-Bold="True">0</asp:Label>
                                </label>
                            </td>
                                   
                                </tr>
                            </table> 
                            </td>
                         
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Work to be done: 
                                </label>
                            </td>
                            <td colspan="5">
                                <label>
                                    <asp:TextBox ID="txtworktobedone" runat="server" Height="75px" TextMode="MultiLine"
                                        Width="500px"></asp:TextBox>
                                </label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <lable>
                        Priority: 
                        </lable>
                        </td>
                        
                        <td colspan="5">
                         <asp:DropDownList ID="ddl_priority" runat="server">
                                        <asp:ListItem Value="1">High</asp:ListItem>
                                        <asp:ListItem Value="2">Medium</asp:ListItem>
                                        <asp:ListItem Value="3">Low</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        </tr>

                        <tr>
                        <td>
                        <label>
                        Incentive Value: 
                        </label> 
                        </td>
                        <td colspan="4">
                        <asp:TextBox ID="txtincentive" runat="server" Width="220px" MaxLength="4" onkeyup="return mak('Span1',4,this)"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        TargetControlID="txtincentive" ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <label style="width:400px">
                                    <asp:Label ID="Label7" runat="server" Text="Do you wish to upload any  file for the instructions? Yes"></asp:Label>
                                </label>
                                <label>
                                <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" OnCheckedChanged="chkupload_CheckedChanged" Checked="true">
                                </asp:CheckBox>
                                </label> 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="pnlup" runat="server" Visible="true">
                                    <fieldset>
                                        <table width="100%" id="Table1">
                                            <tr>
                                              
                                                <td colspan="4">
                                                    <label style="width:200px;">
                                                        <asp:Label ID="Label19" runat="server" Text=" Title"></asp:Label>
                                                        <asp:Label ID="Label20" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txttitlename" ErrorMessage="*" ValidationGroup="2">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttitlename" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ValidationGroup="1">
                                                        </asp:RegularExpressionValidator>
                                                          <asp:TextBox ID="txttitlename" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)" Text="Planning"  runat="server" Width="150px"></asp:TextBox>
                                                        Max <span id="Span10">30</span>
                                                        <asp:Label ID="Label54" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                    </label>
                                                    
                                              <label  style="width:450px;">
                                                    <asp:RadioButtonList ID="Upradio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Upload Audio Instruction File</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Upload Other Files</asp:ListItem>
                                                    </asp:RadioButtonList>
                                               </label>
                                                    <asp:Panel ID="pnlpdfup" runat="server" Visible="false">
                                                        <label style="width:80px;">
                                                            <asp:Label ID="Label21" runat="server" Text="Other File"></asp:Label>
                                                        </label>
                                                        <label style="width:150px;">
                                                            <asp:FileUpload ID="fileuploadadattachment" runat="server" Width="150px"  />
                                                        </label>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnladio" runat="server" Visible="false">
                                                        <label style="width:80px;">
                                                            <asp:Label ID="Label22" runat="server" Text="Audio File"></asp:Label>
                                                        </label>
                                                        <label style="width:150px;">
                                                            <asp:FileUpload ID="fileuploadaudio" runat="server" Width="150px" />
                                                        </label>
                                                    </asp:Panel>
                                              <label style="width:50px;">
                                                    <asp:Button ID="Button3" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_Click" ValidationGroup="2" />
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:GridView ID="gridFileAttach" runat="server" CssClass="mGrid" GridLines="Both"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                        Width="100%" OnRowCommand="gridFileAttach_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                  <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    
                                                                      <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
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
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btnSubmit" />
                                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            </asp:Panel>

             <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="true"> 
              <fieldset>
                    <legend>Reallocate Unfinished Work For Employee </legend>
             <table width="100%">
             <tr>
                 <td>
                     <table>
                       
                         <tr>
                             <td colspan="2">
                                 <label style="width:170px">
                                 <asp:Label ID="Label26" runat="server" Text="Date of Pending Work :"></asp:Label>
                                 </label>
                                 <label style="width:105px">
                                 <asp:TextBox ID="TextBox1" runat="server" Width="100px" OnTextChanged="ddlemployee_SelectedIndexChanged1" AutoPostBack="True"></asp:TextBox>
                                 </label>
                                 <label style="width:50px">
                                 <asp:ImageButton ID="ImageButton1" runat="server" 
                                     ImageUrl="~/images/cal_actbtn.jpg" />
                                 </label>
                                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                     PopupButtonID="ImageButton1" TargetControlID="TextBox1">
                                 </cc1:CalendarExtender>
                             </td>
                         
                              <td style="width: 17%">
                                <label>
                                    
                                </label>
                            </td>
                            <td style="width: 17%">
                                <label>
                                    <asp:Label ID="lbl_totalhours" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                </label>
                            </td>
                         </tr>
                         <tr>
                          <td colspan="4">
                          <label style="width:180px;" >
                                                    <asp:Label ID="Label8" runat="server" Text="Filter By Business Name: " ></asp:Label>                                              
                                                  <asp:DropDownList ID="ddlbusinessfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusinessfilter_SelectedIndexChanged" Width="180px">
                                                        </asp:DropDownList>
                                                    
                                                </label>
                                                <label style="width:180px;">
                                                    <asp:Label ID="Label41" runat="server" Text="Department Name: "></asp:Label>
                                                    <asp:DropDownList ID="ddldepartmentfilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepartmentfilter_SelectedIndexChanged"  Width="180px">
                                                    </asp:DropDownList>
                                             </label>
                                 <label>
                                 Employee Name :
                                 <asp:DropDownList ID="ddl_empsearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged1">
                                 </asp:DropDownList>
                                 </label>
                                  <label>
                                    Type Of Work
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" >
                                         <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        <asp:ListItem Value="1">Developer</asp:ListItem>
                                        <asp:ListItem Value="3">Supervisor</asp:ListItem>
                                        <asp:ListItem Value="2">Tester</asp:ListItem>
                                         <asp:ListItem Value="5">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                             </td>
                          
                        </tr>
                        <tr>
                         
                            <td colspan="4">

                                <label>
                                <asp:Label ID="lbl_gverror" runat="server" Text="*" CssClass="labelstar" Visible="false"></asp:Label>
                                     <asp:Button ID="Button5" runat="server" CssClass="btnSubmit"  OnClick="Button1_ClickSearch" Text="Go" />
                                </label>
                            </td>
                        </tr>
                     </table>
                 </td>
             </tr>
          <tr>
             <td>
                    <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False" PageSize="30" AllowPaging="true"  OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"  EmptyDataText="No Records Found." DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr"  ShowFooter="true" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="GridView2_Sorting" OnRowDataBound="GridView2_RowDataBound">
                                                     <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworktype" runat="server" Text='<%#Bind("Statuslabel") %>'></asp:Label>
                                                                <asp:Label ID="lblworktypeid" runat="server" Text='<%#Bind("Typeofwork") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Main Task" SortExpression="WorkRequirementTitle" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                                <asp:Label ID="lblworktitle12345" runat="server" Text='<%#Bind("WorkRequirementTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Main Task<br> Bud. Hrs" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudhrmaintask" runat="server"></asp:Label>
                                                                 </ItemTemplate>
                                                             <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_budghurfootr" runat="server" ForeColor="Black" Text="" Visible="false"></asp:Label>                            
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Task" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="WorkDoneReport">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblworktobedone" runat="server" Text='<%#Bind("worktobedone") %>'></asp:Label>
                                                                <asp:Label ID="lbl_pageid" runat="server" Text='<%#Bind("Pageid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Task<br>Bud. Hrs" SortExpression="budgetedhour" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbudgetd132_BugHourToday" runat="server" Text='<%#Bind("budgetedhour") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attach" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                            <ItemTemplate>
                                                                  <asp:ImageButton ID="LinkButton1" runat="server" ToolTip="Attachments" CommandName="Attach" ImageUrl="~/Account/images/attach.jpg" CommandArgument='<%# Eval("Pageid") %>'></asp:ImageButton>                                                                                                               
                                                                    <asp:Label ID="lblfillreport" runat="server"></asp:Label>
                                                                     <asp:Label ID="lbl_noAttach" runat="server" Text="Attachment Unavailable" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_mainid" runat="server"  Text='<%#Bind("id") %>' Visible="false"></asp:Label>
                                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reallocation Date" SortExpression="budgetedhour" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                            <table>
                                                            <tr>
                                                                <td>
                                                                     <asp:TextBox ID="txtNewtargetdatedeve" runat="server" Width="70px" OnTextChanged="ddlemployee_SelectedIndexChangedGrid" AutoPostBack="True" CommandName="Select"></asp:TextBox>   
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton3New" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                                <cc1:CalendarExtender ID="CalendarExtender3New" runat="server" TargetControlID="txtNewtargetdatedeve"  PopupButtonID="ImageButton3New">
                                                                </cc1:CalendarExtender>    
                                                                </td>
                                                            </tr>
                                                            </table> 
                                                            </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="New Hrs" SortExpression="budgetedhour" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                              <asp:TextBox ID="txt_newHrs" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txttodaybudgetdhour_TextChanged">00:00</asp:TextBox>
                                                              
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText=""  ItemStyle-HorizontalAlign="Left"  HeaderStyle-Height="20px" >
                                                                 <ItemTemplate>
                                                                  <%-- <asp:Button ID="Button15" runat="server" Text="Edit"  CommandName="nnn"  CommandArgument='<%# Eval("ID") %>' /> --%>
                                                           <%-- <asp:Label ID="lbl_allredySend" runat="server" Text="View Report" ForeColor="Green" Font-Size="16px"></asp:Label>--%>
                                                            <asp:LinkButton ID="LinkButton2Show" runat="server" Text='' ForeColor="Green"  CommandArgument='<%# Eval("Id") %>'  CommandName="Select" Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5Edit" runat="server" Text='Fill Report' ForeColor="Red"  CommandArgument='<%# Eval("Id") %>'  CommandName="Select" >Reallocate</asp:LinkButton>
                                                                   </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Total Hrs.<br> for Day" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_totalhrsforday" runat="server"></asp:Label>
                                                                 </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                      
                                                     
                                                <%--       Hide All Extra    --%>
                                                                            

                                                        <asp:TemplateField HeaderText="Act. Hrs" SortExpression="HourSpent" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactualhour" runat="server" Text='<%#Bind("HourSpent") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>                                                            
                                                                <asp:Label ID="lblftractualtodaytask" runat="server" ForeColor="Black" Text="" ></asp:Label>
                                                                 <asp:Label ID="lblacthronly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblactminuteonly" runat="server" ForeColor="Black" Visible="false"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="Incent. Earn" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_earn" runat="server" Text='<%#Bind("Earn_Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_TotalEarnoffer" runat="server" ForeColor="Black" Text="Text" ></asp:Label>                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Today's Task<br> Status" HeaderStyle-HorizontalAlign="Left"   ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                             <asp:Label ID="lblwebsitenameWorkDone" runat="server"  Text='<%#Bind("WorkDone")%>' ></asp:Label>                                                                
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                              <asp:Label ID="lbl_textTotal" runat="server" ForeColor="Black" Text="Total :" ></asp:Label>
                                                                </FooterTemplate>
                                                        </asp:TemplateField>                                                         
                                                        <asp:TemplateField HeaderText="Incent. Off" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_inceoffer" runat="server" Text='<%#Bind("Offer_Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>                                                            
                                                                <asp:Label ID="lbl_totaloffer" runat="server" ForeColor="Black" Text="Text" ></asp:Label>                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Main Task <br>Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                          Visible="false"  SortExpression="ProductName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmaintaskstatus" runat="server"></asp:Label>                                                                 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" 
                                                            SortExpression="ProductName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwebsitename12345" runat="server"  Text='<%#Bind("ProductName")%>' ></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="workallocationdate" HeaderStyle-HorizontalAlign="Left"
                                                          Visible="false"   ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblreportdate" runat="server" Text='<%#Bind("workallocationdate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
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
                                                        <asp:TemplateField HeaderText="Act.Hr Main Task" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblactualhrmaintask" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bud.Hr Today Task" SortExpression="budgetedhour" HeaderStyle-HorizontalAlign="Left" Visible="false"
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
                                                       


                                                    </Columns>
                                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                     <AlternatingRowStyle BackColor="White" />    
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>

                                                 <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                             <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
             </td>
             </tr>            
             </table> 
                </fieldset>
             </asp:Panel>

           
                         <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="80%" BorderStyle="Solid" BorderWidth="5px"  ScrollBars="Both">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/closeicon.jpeg" runat="server" Width="16px" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>Work Intruction File </legend>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel7" runat="server" ScrollBars="Both" HorizontalAlign="Center" Height="150px">
                                                                <asp:GridView ID="GridView3" runat="server" EmptyDataText="No file found." AutoGenerateColumns="False"  DataKeyNames="Id" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%">
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
                                                                               <asp:LinkButton ID="linkdow1dailywork" runat="server" Text="Download" OnClick="linkdow1dailywork_Click" ForeColor="Black"></asp:LinkButton>                                                                                    
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
                                                             <fieldset>
                                                            <legend>Upload Other Work Intruction File </legend>
                                                            <asp:Panel ID="Panel3" runat="server" Visible="true">
                                    <fieldset>
                                        <table width="100%" id="Table2">
                                            <tr>
                                              
                                                <td width="30%">
                                                    <label>
                                                        <asp:Label ID="Label2" runat="server" Text=" Title"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="*" ValidationGroup="11">
                                                        </asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ValidationGroup="11">
                                                        </asp:RegularExpressionValidator>--%>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="TextBox2"  MaxLength="30" Text="Planning" runat="server"></asp:TextBox>
                                                         <%--onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)  onKeydown="return mask(event)""--%>
                                                    </label>
                                                    <label>
                                                        Max <span id="Span1">30</span>
                                                        <asp:Label ID="Label4" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                                                    </label>
                                                </td>
                                                <td width="30%">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Upradio_SelectedIndexChanged"  RepeatDirection="Vertical">
                                                        <asp:ListItem Value="1">Upload Audio Instruction File</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">Upload Other Files</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td width="25%">
                                                    <asp:Panel ID="Panel4" runat="server" Visible="true">
                                                        <label>
                                                            <asp:Label ID="Label5" runat="server" Text="Other File"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:FileUpload ID="fileupload1other" runat="server" />
                                                        </label>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel5" runat="server" Visible="false">
                                                        <label>
                                                            <asp:Label ID="Label6" runat="server" Text=" Audio File"></asp:Label>
                                                        </label>
                                                        <label>
                                                            <asp:FileUpload ID="fileupload2audio" runat="server" />
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%">
                                                    <asp:Button ID="Button7" CssClass="btnSubmit" runat="server" Text="Add" OnClick="Button2_ClickAdd" ValidationGroup="11" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:GridView ID="GridView1Attach" runat="server" CssClass="mGrid" GridLines="Both"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                        Width="100%" OnRowCommand="gridFileAttach_RowCommandn">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                  <asp:Label ID="lbltitle" runat="server" Text='<%#Bind("Title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PDF URL" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    
                                                                      <asp:Label ID="lblpdfurl" runat="server" Text='<%#Bind("PDFURL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audio URL" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaudiourl" runat="server" Text='<%#Bind("AudioURL") %>'></asp:Label>
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
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="Button6" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"   PopupControlID="Panel2" TargetControlID="Button6" CancelControlID="ImageButton5">
                            </cc1:ModalPopupExtender>
                            <input id="Hidden3" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="Hidden4" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
            <asp:PostBackTrigger ControlID="Button7" />
        </Triggers>
    </asp:UpdatePanel>
      <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" Text="V5" ForeColor="#416271" Font-Size="14px"></asp:Label>
              </div>
</asp:Content>
