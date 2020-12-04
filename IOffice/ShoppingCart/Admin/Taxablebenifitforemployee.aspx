<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Taxablebenifitforemployee.aspx.cs" Inherits="Taxablebenifitforemployee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style type="text/css">
        .open
        {
	        display:block;
        }
        .closed
        {
	        display:none;
        }
    </style>
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
    </script>
    <div class="products_box">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  
            <div style="padding-left:1%">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"  Text="" Visible="False" />
            </div>
        <fieldset>
            
            <legend><asp:Label ID="Label5" runat="server" Text="Taxable Benefits for Employee"></asp:Label></legend>
            <label>
                <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstrname"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlstrname" runat="server" AutoPostBack="true"
                   OnSelectedIndexChanged="ddlstrname_SelectedIndexChanged1">
                          
                </asp:DropDownList>
                                                  
              <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px"
                                        type="hidden" />
                                   <input
                                        id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
          
            </label>
            
            
              <label>
                <asp:Label ID="Label8" runat="server" Text="Employee Name"></asp:Label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlemp"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlemp" runat="server" >
                  
                        
                </asp:DropDownList>
                                                  
             
            </label>
            
             <label>
                <asp:Label ID="Label6" runat="server" Text="Taxable benefits name"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddltaxbenifitname"
                                                        ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddltaxbenifitname" runat="server" >
                                                                      
                 </asp:DropDownList>
             
            </label>
           
          <div style="clear: both;">
            </div>
            
           <label>
                <asp:Label ID="Label3" runat="server" Text="Recurring Benefit?"></asp:Label>
               </label>
                <asp:RadioButtonList ID="ddlrecringbenifit" runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True" 
                onselectedindexchanged="txttaxablename_SelectedIndexChanged" >
                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
            </asp:RadioButtonList>
               
                  
                  <div style="clear: both;">
            </div>  
             <asp:Panel ID="pnldate" runat="server" Visible="true">    
             <label>
            
                <asp:Label ID="Label17" runat="server" Text="Date"></asp:Label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtreqdate" SetFocusOnError="true"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
               <asp:TextBox ID="txtreqdate" runat="server" Width="87px" ></asp:TextBox>
                      
             
            </label>
            <label><br />
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            PopupButtonID="ImageButton2" TargetControlID="txtreqdate">
                        </cc1:CalendarExtender>
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                            ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                          
                            </label>
           </asp:Panel>  
             <div style="clear: both;">
            </div>
            
              
          
             <label>
              <asp:Label ID="lblamt" runat="server" Text="Amount"></asp:Label>
            
                  <asp:RequiredFieldValidator ID="Requirelalidator9" runat="server" ControlToValidate="txtamt"
                                                        ErrorMessage="*" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
               <asp:TextBox ID="txtamt" runat="server" ></asp:TextBox>
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtamt" ValidChars="-.">
                                                    </cc1:FilteredTextBoxExtender>                          
                                          
            </label>
            <label>
              <asp:Label ID="Label11" runat="server" Text="Status" ></asp:Label>
              <asp:DropDownList ID="ddlstatus" runat="server" Width="100px">
              <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                <asp:ListItem  Value="0" Text="Inactive"></asp:ListItem>
              </asp:DropDownList>
            </label>
              <div style="clear: both;">
            </div>
            <label>
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit"
                                                        onclick="btnsubmit_Click" 
                  ValidationGroup="1" />
                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit"
                                                        onclick="btnupdate_Click" 
                  Visible="False" ValidationGroup="1" />
          
            </label>
            <label>
                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" 
                                                        onclick="btncancel_Click" />
              
            </label>
        </fieldset>

       
            
        <div style="clear: both;">
        </div>
        <fieldset>
            <legend>
                <asp:Label ID="Label4" runat="server" 
                    Text="List of Taxable Benefits for Employee"></asp:Label></legend>
  <div style="float: right;">
                 <label>
                    <asp:Button ID="Button1" CssClass="btnSubmit"  runat="server" Text="Printable Version" 
                        onclick="Button1_Click" />
                    <input id="Button7" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" 
                        type="button" value="Print" visible="False" />
                      
                </label>
            
            </div>
            <div style="clear: both;">
            </div>
 <label>
                <asp:Label ID="Label9" runat="server" Text="Business Name"></asp:Label>
               <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlstrname"
                                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlfilterbus" runat="server" AutoPostBack="true"
                   OnSelectedIndexChanged="ddlfilterbus_SelectedIndexChanged">
                          
                </asp:DropDownList>
                                                  
             
            </label>
           <label>
                <asp:Label ID="Label1" runat="server" Text="Employee Name"></asp:Label>
                         <asp:DropDownList ID="ddlfilteremp" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlfilteremp_SelectedIndexChanged" >
                  
                        
                </asp:DropDownList>
                                                  
             
            </label>
           
             <label>
                <asp:Label ID="Label10" runat="server" Text="Taxable benefits name"></asp:Label>
                       <asp:DropDownList ID="ddlfiltertaxpay" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlfiltertaxpay_SelectedIndexChanged" >
                                                                      
                 </asp:DropDownList>
             
            </label>
            <label>
              <asp:Label ID="Label12" runat="server" Text="Status" ></asp:Label>
              <asp:DropDownList ID="ddlfilterstatus" runat="server" Width="100px" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlfilterstatus_SelectedIndexChanged" >
              <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
                <asp:ListItem  Value="0" Text="Inactive"></asp:ListItem>
              </asp:DropDownList>
            </label>
           
          
                             <div style="clear: both;">
            </div>
          
           <asp:Panel ID="pnlgrid" runat="server"  Width="100%">
                <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="center">
                        <td>
                             <div id="mydiv" class="closed">
                                 <table width="100%" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCompany" runat="server" Font-Size="20px"></asp:Label>                                                                                        
                                        </td>
                                    </tr>  
                                   <tr>
                                        <td align="center">
                                            <asp:Label ID="lblemp" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbltax" runat="server" Font-Size="20px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Font-Size="18px" Text="List of Taxable Benefits for Employee" ></asp:Label>
                                        </td>
                                    </tr>                            
                                   
                                    
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>                    
                            </div>
                        </td>
                    </tr>         
                    <tr>
                        <td>
                        
                        
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                        CellPadding="4" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                            AlternatingRowStyle-CssClass="alt" Width="100%" 
                                                        onrowediting="GridView1_RowEditing" 
                                AllowSorting="True" onsorting="GridView1_Sorting" 
                                onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting">
                        <Columns>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="4%" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                        <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="15%" SortExpression="Wname" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblbname" runat="server" Text='<%# Eval("Wname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Taxable benefits Name" ItemStyle-Width="15%" SortExpression="Taxablebenifitname" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbltaxbname" runat="server" Text='<%# Eval("Taxablebenifitname") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Recurring benefits" ItemStyle-Width="4%" SortExpression="RecrringType" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                <asp:Label ID="lblrectype" runat="server" Text='<%# Eval("RecrringType") %>' ></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Amount" ItemStyle-Width="5%" SortExpression="Amount" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                <asp:Label ID="lbluni" runat="server" Text='<%# Eval("Amount") %>' ></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                          <asp:TemplateField HeaderText="Status" ItemStyle-Width="4%" SortExpression="Status" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                <asp:Label ID="lblst" runat="server" Text='<%# Eval("Status") %>' ></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:CommandField ShowSelectButton="true"  HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit" ButtonType="Image" SelectImageUrl="~/Account/images/edit.gif"
                                            ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                    
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"  HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinkbb" runat="server" 
                                                     CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                    </Columns>
                       
                    </asp:GridView>
                 </td>
                    </tr>     
                    </table>
                </asp:Panel>
            <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="ContactName" HeaderText="Department" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Designation" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee Id" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee Name" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Employee No" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Status" />
                    <asp:BoundField DataField="PricePlan" HeaderText="Batch Name" />
                    <asp:HyperLinkField Text="Edit" HeaderText="Edit" />
                </Columns>
            </asp:GridView>--%>
            <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data1.xml">
            </asp:XmlDataSource>--%>
        </fieldset>
            <asp:HiddenField ID="hfidwhid" runat="server" />
                <asp:HiddenField ID="hfempid" runat="server" />
                <asp:HiddenField ID="hfbmid" runat="server" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
    
</asp:Content>
