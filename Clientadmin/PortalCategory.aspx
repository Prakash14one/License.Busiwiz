<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="PortalCategory.aspx.cs" Inherits="PortalCategory" Title="Portal Category" %>

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

        function mask(evt) {

            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
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
        .style4
        {
            width: 355px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            <div style="padding-left: 2%">
                <div style="clear: both;">
                    <asp:Panel ID="Panel2" runat="server">
                    </asp:Panel>
                </div>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend1" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button  ID="btnadddd" runat="server"  Text="Add Price Plan Category" Width="180px"  CssClass="btnSubmit" onclick="btnadddd_Click"  />
                        <asp:Button ID="btndosyncro" runat="server"  CssClass="btnSubmit" OnClick="btndosyncro_Click" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>                    
                    <asp:Panel ID="pnladdnew" runat="server" Visible="false" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Products:"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductversion" Width="300px"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlproductversion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Portal Name: "></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlportalname"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlportalname" Width="300px" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                        <td>
                            <label style="width:550px;">

                                <asp:Label ID="Label37" runat="server" Text="Priceplan Category Type:"></asp:Label>     
                                 <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>                          
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcategoryType"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </label>
                        </td>
                        <td>
                            <label>
                                <asp:DropDownList ID="ddlcategoryType" Width="300px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategoryType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>

                              <tr>
                                <td>
                                    <label style="width:550px;">
                                        <asp:Label ID="Label11" runat="server" Text="Priceplan Category Sub Type:"></asp:Label>                                           
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcategorysubType" Width="300px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategorysubType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                             <asp:Panel ID="pnlnoofcompany" runat="server" Visible="false" Width="100%">
                            <td>
                                     <label style="width:550px;">
                                         Shared with Maximm number of  Companies of any Portal: 	
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" TargetControlID="txtmaxshare" ValidChars="012.3456789">
                                        </cc1:FilteredTextBoxExtender>
                                      </label> 
                               
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox ID="txtmaxshare" runat="server"></asp:TextBox>
                                    </label> 
                            </td>
                            </asp:Panel>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Category Name:"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodetypecategory"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtcodetypecategory" runat="server"></asp:TextBox>
                                           <asp:CheckBox ID="chkupload" runat="server" AutoPostBack="True" Visible="false">
                                        </asp:CheckBox>
                                    </label>
                                </td>
                            </tr>
                            
                             <%-- <tr>
                                  <td colspan="2">
                                  <label style="width:450px">
                                      Is this PricePlan category relates to ?
                                      </label> 
                                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="MasterAdmin">Master Admin</asp:ListItem>
                                                <asp:ListItem Value="Franchisee">Franchisee</asp:ListItem>
                                                <asp:ListItem Value="Customer" Selected="True">Customer</asp:ListItem>
                                                <asp:ListItem Value="other1" Selected="True">other1</asp:ListItem>
                                                <asp:ListItem Value="other2" Selected="True">other2</asp:ListItem>
                                                
                                        </asp:RadioButtonList>
                                      
                                      </td>

                            </tr>--%>
                            

                            <asp:Panel ID="pnlradio" runat="server" Visible="false" Width="100%">
                            <tr>
                            <td colspan="2">
                                     <label style="width:550px;">
                                          Do you wish to give the subscriber the option to accept online payments?			
                                      </label> 
                                <label>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                </label> 
                            </td>
                            </tr>
                             <tr>
                            <td colspan="2">
                            <label style="width:750px;">
                                               Do you wish to allow subscribers to have an outbound email functionality with their own email server?					
	                            </label> 
                                <label>
                                  <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                </label> 
                            </td>                            
                            </tr>
                             <tr>
                            <td colspan="2">
                            <label style="width:650px;">
                         Do you wish to offer a free trial of any your other products to subscribers?					
                               
                            </label> 
                            <label>
                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" Width="100px" >
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True" >No</asp:ListItem>
                                </asp:RadioButtonList>
                            </label> 
                           </td> 
                            
                            </tr>
                          </asp:Panel>



                              <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Status:"></asp:Label>                                        
                                    </label>
                                </td>
                                <td>
                                <label>
                                    <asp:DropDownList ID="ddlstatus" runat="server">
                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>                                   
                                 </td>                                                       
                            </tr>                             
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" ValidationGroup="1" Text="Submit" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" Visible="false" runat="server" CssClass="btnSubmit" ValidationGroup="1" Text="Update" onclick="Button2_Click" />
                                   <asp:Button ID="btncancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="btnSubmit" onclick="btncancel_Click"  />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%;">
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label>
                                                <asp:Label ID="Label20" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdsync" runat="server">
                                                            <asp:ListItem Value="1" Text="Yes, this is the last record in the series of records I am inserting/editing to this table right now"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="No, I am still going to add/edit records to this table right now"
                                                                Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnok" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btndosyncro_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                                     <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"  PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                        </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Portal Category"></asp:Label>
                    </legend>
                    <table style="width: 100%"> 
                    <tr>
                        <td align="right">
                     <div style="float: right">                         
                            
                       <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version" OnClick="Button1_Click1" Visible="false"  />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"  style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                        </td>
                     </tr>
                   <%-- <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button1_Click1" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>--%>
                                                    
                        <tr>
                            <td >  
                                <label style="width:140px;">                            
                              <asp:Label ID="lblProductVersion"  text=" Filter by Products:" runat="server"></asp:Label>
                              </label>                              
                              <label style="width:180px;">
                                    <asp:DropDownList ID="ddlstsearchproduct" runat="server" Width="180px"   AutoPostBack="True"  onselectedindexchanged="ddlstsearchproduct_SelectedIndexChanged" >
                                         </asp:DropDownList>
                               </label>                          
                            <label style="width:100px;">                                                  
                              <asp:Label ID="Label6" text="Portal Name:" runat="server"></asp:Label>
                            </label>
                             <label style="width:180px;">
                                    <asp:DropDownList ID="ddlstsearchportal" runat="server" Width="180px">
                                         </asp:DropDownList>
                               </label>                                          
                             <label style="width:110px;">
                                <asp:Label ID="Label8" runat="server" Text="Category Type:"></asp:Label>
                            </label>                       
                            <label style="width:120px;">
                                <asp:DropDownList ID="ddlCategoryTypeFilter" runat="server" Width="120px">                                
                                </asp:DropDownList>
                            </label>
                            <label style="width:60px;">
                               <asp:Label ID="Label2st" Text="Status:" runat="server"></asp:Label>                            
                            </label> 
                            <label style="width:140px;">
                                   <asp:DropDownList ID="ddlstsearchstatus"  runat="server" Width="140px">
                                    <asp:ListItem Value="2" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>                                   
                                </asp:DropDownList>                                            
                             </label>                                                                  
                            <label style="width:40px;">
                              <asp:Button ID="btngo" runat="server" Text="Go" CausesValidation="false" onclick="btngo_Click"  />
                            </label>
                            </td>
                        </tr>                      
                    </table>                 
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">                                   
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Text="List of Portal Category" ForeColor="Black"
                                                                    Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                          <table width="100%">
                                         <tr>
                                             <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" onrowediting="GridView1_RowEditing" 
                                                      onrowdatabound="GridView1_RowDataBound" onrowdeleting="GridView1_RowDeleting"
                                                      AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True" OnSorting="GridView1_Sorting"  >
                                        
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="18%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Portal Name " SortExpression="PortalName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="18%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblportalcategory" runat="server" Text='<%#Bind("PortalName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcategoryname" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                        
                                                         <asp:TemplateField HeaderText="Category Type" SortExpression="CategoryTypeName" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>                                                                                                                        
                                                               <asp:Label ID="lblCategoryType"  runat="server" Text='<%# Eval("CategoryTypeName") %>'></asp:Label>                                                                
                                                            </ItemTemplate>
                                                             <HeaderStyle HorizontalAlign="Left" />                                                           
                                                        </asp:TemplateField>   
                                                          <asp:TemplateField HeaderText="Category Sub Type" SortExpression="SubTypeName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>                                                                                                                        
                                                               <asp:Label ID="lblCategorysubType"  runat="server" Text='<%# Eval("SubTypeName") %>'></asp:Label>                                                                
                                                            </ItemTemplate>
                                                             <HeaderStyle HorizontalAlign="Left" />                                                           
                                                        </asp:TemplateField>                                                 
                                                         <asp:TemplateField HeaderText="Status" HeaderStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>                                                                                                                        
                                                               <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>   
                                                                <asp:Label ID="lblproductid2" Visible="false" runat="server" Text="Active"></asp:Label>   
                                                                 <asp:Label ID="lblproductid3" Visible="false" runat="server" Text="Inactive"></asp:Label>                                                     
                                                            </ItemTemplate>
                                                             <HeaderStyle HorizontalAlign="Left" />                                                           
                                                        </asp:TemplateField>                                                 
                                                        <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderText="Edit" HeaderStyle-Width="1%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnedit" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                                    ToolTip="Edit" CommandArgument='<%#Eval("ID")%>' CommandName="edit" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    ToolTip="Delete" CommandName="delete" ImageUrl="~/Account/images/delete.gif"
                                                                    OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </td>
                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
         <Triggers>  
          <asp:PostBackTrigger ControlID="btnadddd" /> 
           <asp:PostBackTrigger ControlID="Button1" />      
           <asp:PostBackTrigger ControlID="Button2" />
           <asp:PostBackTrigger ControlID="btncancel" /> 
           <asp:PostBackTrigger ControlID="btngo" />
           <asp:PostBackTrigger ControlID="Button3" />                                 
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
