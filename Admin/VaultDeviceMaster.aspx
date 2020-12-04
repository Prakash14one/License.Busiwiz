<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="VaultDeviceMaster.aspx.cs" Inherits="Admin_VaultDeviceMaster" Title="VaultdeviceMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charcode = (evt.which) ? evt.which : event.keyCode
            if (charcode > 31 && (charcode < 48 || charcode > 57))
                return false

            return true
        }
</script>
    <table cellpadding="0" cellspacing="0" style="width: 625px">
        <tr>
            <td class="hdng">
                Add/Manage Vaultdevice Master<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" style="width: 642px">
                  <tbody>
                       
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                VaultDeviceMaster Name :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="Txtaddname" runat="server" Width="163px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txtaddname" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                         <tr>
                            <td style="height: 16px; width: 166px;">
                                VaultDeviceType Name :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="ddldivicetype" runat="server"  Width="222px">
                                                                  
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldivicetype" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                         
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                Sr Number :
                            </td>
                            <td class="column2" colspan="3" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtsrnu" runat="server" Width="163px" 
                                    onkeypress="return isNumberKey(event);" ontextchanged="txtsrnu_TextChanged"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsrnu" ErrorMessage="*"></asp:RequiredFieldValidator>
<asp:Button id="btnCheckCompany" onclick="btnCheckCompany_Click" runat="server" Width="111px" 
                            Text="Check Availability" Height="18px"></asp:Button>
                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblCompanyIDAVl" runat="server" ForeColor="#C00000" 
                            Font-Bold="False"></asp:Label>
                            </td>
                          
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                Active/Deactive :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:CheckBox ID="Chkactive" Checked="true" runat="server" />
                            </td>
                          
                        </tr>
                        <tr>
            <td style="height: 16px; width: 166px;">
                Start Date :
            </td>
            <td style="width: 248px; height: 16px">
                <asp:TextBox ID="txtStartdate" runat="server" Width="163px"></asp:TextBox> <asp:ImageButton
                    ID="imgbtnEndDate" runat="server" ImageUrl="~/images/cal_actbtn.jpg" /><cc1:CalendarExtender
                        ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnEndDate" TargetControlID="txtStartdate">
                    </cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartdate"
                    ErrorMessage="*" ></asp:RequiredFieldValidator></td>
            <td style="width: 3px; height: 16px">
                
            </td></tr>
            <tr>
                            <td style="height: 16px; width: 166px;">
                                SeedNumber :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtseedno" runat="server" onkeypress="return isNumberKey(event);" Width="163px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtseedno" ErrorMessage="*"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtseedno" 
                                    ErrorMessage="Max 5 Digit allowed" 
                                    ValidationExpression="^[0-9.\s]{1,5}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                Arithmaticoperator :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:DropDownList ID="txtaritho" runat="server"  Width="222px">
                                 <asp:ListItem Text="-Select-"  Selected="True"></asp:ListItem>  
                                           <asp:ListItem Text="+"></asp:ListItem>   
                                                      <asp:ListItem Text="-"></asp:ListItem>   
                                                       <asp:ListItem Text="*"></asp:ListItem>   
                                                        <asp:ListItem Text="/"></asp:ListItem>             
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaritho" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 166px;">
                                HopNumber :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txthopno" runat="server" onkeypress="return isNumberKey(event);" Width="163px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator8" runat="server" ControlToValidate="txthopno" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txthopno" 
                                    ErrorMessage="Max 3 Digit allowed" 
                                    ValidationExpression="^[0-9.\s]{1,3}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </td>
                          
                        </tr>
        <tr>
                            <td style="height: 16px; width: 166px;">
                                Active time of number mnts :
                            </td>
                            <td class="column2" colspan="2" style="padding-top: 10px;">
                                
                                <asp:TextBox ID="txtatm" runat="server" Width="163px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtatm" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                          
                        </tr>
                       <tr>
                            <td class="column1" style="width: 166px">
                            </td>
                            <td class="column2" align="center" style="width: 248px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnadd" runat="server" Text="Submit" onclick="btnadd_Click" />
                                    
                            </td>
                            <td class="column2">
                            </td>
                            <td class="column2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblmsg" runat="server" Width="297px" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                           
                        </tr>
                    </tbody>
                </table>      </td>
                        </tr>
                    
                </table>
         
  
    <table id="Table2" cellpadding="0" cellspacing="0">
        
        <tr>
            <td class="column2" colspan="4">
                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" Width="645px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridBack" DataKeyNames="Id" EmptyDataText="There is no data."
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                        PageSize="5">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="edit1" HeaderText="Edit" ImageUrl="~/images/edit.gif"
                                Text="Button" />
                            <asp:BoundField DataField="Name" HeaderText="Vaultdevice Mastername" />
                            <asp:BoundField DataField="SrNumber" HeaderText="SrNumber" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="name1" HeaderText="Vaultdevice Typename" />
                               <asp:BoundField DataField="Active" HeaderText="Active" />
                            <asp:BoundField DataField="StartDate" HeaderText="StartDate" />
                            <asp:BoundField DataField="SeedNumber" HeaderText="SeedNumber" />
                            <asp:BoundField DataField="Arithmaticoperator" HeaderText="Arithmatic Operator" />
                            <asp:BoundField DataField="HopNumber" HeaderText="HopNumber" />
                            <asp:BoundField DataField="activetimeofnumbermnts" HeaderText="Activetime mnts" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" />
                          
                        </Columns>
                        <PagerStyle CssClass="GridPager" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        <RowStyle CssClass="GridRowStyle" />
                        <FooterStyle CssClass="GridFooter" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
       
    </table>
    
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div style="border-right: #946702 1px solid; border-top: #946702 1px solid; left: 45%;
                visibility: visible; vertical-align: middle; border-left: #946702 1px solid;
                width: 196px; border-bottom: #946702 1px solid; position: absolute; top: 65%;
                height: 51px; background-color: #ffe29f" id="IMGDIV" align="center" runat="server"
                valign="middle">
                <table width="645px">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/images/preloader.gif"></asp:Image>
                            </td>
                            <td>
                                <asp:Label ID="lblprogress" runat="server" ForeColor="#946702" Text="Please Wait"
                                    Font-Bold="True" Font-Size="16px" Font-Names="Arial"></asp:Label><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

