<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ProjectReport.aspx.cs" Inherits="ProjectReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  
function Button1_onclick() {

}

function Button2_onclick() {

}

   function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
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
        .style1
        {
            height: 28px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Time Spent of project"></asp:Label>
                    </legend>
                    <div style="float: right">
                    <asp:Button ID="Button3" runat="server" Text="Employee Worksheet"  
                            CssClass="btnSubmit" onclick="Button3_Click"
                            />
                    </div>
                    <label>
                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                        <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="1" runat="server"
                            ErrorMessage="*" ControlToValidate="ddlstore"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlstore" runat="server" OnSelectedIndexChanged="ddlStoreName_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Customer"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlparty" runat="server" AppendDataBoundItems="true" 
                        AutoPostBack="True" onselectedindexchanged="ddlparty_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Employee"></asp:Label>
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlemployee" runat="server" Width="180px">
                        </asp:DropDownList>
                    </label>
                    <label>
                                            <asp:Label ID="Label10" runat="server" Text="Project"></asp:Label>  
                                            
                                       </label>
                                         <label>
                                  
                                            <asp:DropDownList ID="ddljob" runat="server">
                                            </asp:DropDownList>
                                         
                                        </label>
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Date"></asp:Label>
                    </label>
                    <label>
                        <asp:TextBox ID="txtissuedate" runat="server" Width="80px"></asp:TextBox>
                    </label>
                    <label>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtncal"
                            TargetControlID="txtissuedate">
                        </cc1:CalendarExtender>
                        <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                    </label>
                    <div style="clear: both;">
                    </div>
                    <div style="clear: both;">
                        <br />
                    </div>
                    <div style="clear: both;">
                        <asp:Button ID="btngo" runat="server" Text=" Go " Width="70px" CssClass="btnSubmit"
                            ValidationGroup="1" OnClick="btnstime_Click" />  <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />

                    </div>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label19" runat="server" Text="List of Time Tracked Projects"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <%--  <div style="clear: both;">
            </div>--%>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="clear: both;">
                                </div>
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                                    <table width="100%">
                                        <tr>
                                            <td class="style1">
                                                <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; color: Navy; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; color: Black; font-size: 18px; font-weight: bold;">
                                                               <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="Business :"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; ">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Font-Bold="true" Text="Customer :"></asp:Label>
                                                                <asp:Label ID="lblgcust" runat="server" Font-Italic="true" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label20" runat="server" Text="Employee :" Font-Size="16px" Font-Italic="true" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="lblemp" runat="server" Font-Size="16px" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label7" runat="server" Text="Job :" Font-Size="16px" Font-Italic="true" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="lbljj" runat="server" Font-Size="16px" Font-Italic="true" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label6" runat="server" Text="Date :" Font-Size="16px" Font-Bold="true" Font-Italic="true"></asp:Label>
                                                                <asp:Label ID="lblgdate" runat="server" Font-Size="16px" Font-Italic="true" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label5" runat="server" Text="List of Time Tracked Projects" Font-Size="16px" ></asp:Label>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="false" 
                                                    AutoGenerateColumns="False" AllowSorting="true"
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    Width="100%" DataKeyNames="Id" 
                                                    EmptyDataText="No Record Found." ShowFooter="True" 
                                                    onsorting="GridView1_Sorting" onrowcommand="GridView1_RowCommand" 
                                                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Project Title" SortExpression="JobName" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectname" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                                                
                                                           
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee" SortExpression="Employee" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Bind("Employee") %>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start Time" SortExpression="FromTime" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                <asp:Label ID="lblfromtime" runat="server" Text='<%#Bind("FromTime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End Time" SortExpression="FromToTime" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                               <asp:Label ID="lblEnddate" runat="server" Text='<%#Bind("Enddate") %>'></asp:Label>
                                                                <asp:Label ID="lblfromtotime" runat="server" Text='<%#Bind("FromToTime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Hours & Minutes" SortExpression="Hrs" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhor" runat="server" Text='<%#Bind("Hrs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            
                                                            <asp:Label ID="lblfoothrs" runat="server" Font-Bold="true"></asp:Label>
                                                            </FooterTemplate>
                                                        
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%"  SortExpression="Rate" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrate" runat="server" Text='<%#Bind("Rate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                         
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" SortExpression="Cost"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCost" runat="server" Text='<%#Bind("Cost") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            
                                                            <asp:Label ID="lblfootcost" Font-Bold="true" runat="server" ></asp:Label>
                                                            </FooterTemplate>
                                                          
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' CommandName="Edit"
                               ImageUrl="~/Account/images/edit.gif">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="llinkbb" runat="server"  ToolTip="Delete"
                                CommandArgument='<%# Eval("Id") %>' CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?')">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField  HeaderImageUrl="~/Account/images/viewprofile.jpg" HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lbladdd" runat="server"  CommandArgument='<%# Eval("Id") %>' CommandName="view"  ToolTip="View Profile" Height="20px" ImageUrl="~/Account/images/viewprofile.jpg" Width="20px">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
