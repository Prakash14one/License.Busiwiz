<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PaymentDueReport.aspx.cs" Inherits="PaymentDueReport"
    Title="Untitled Page" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/ShoppingCart/Admin/UserControl/UControlWizardpanel.ascx" TagName="pnl"
    TagPrefix="pnl" %>
<%@ Register Src="~/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>--%>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function RealNumWithDecimal(myfield, e, dec) {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }


        function CallPrint1(strid) {
            var prtContent = document.getElementById('<%= pnlpt.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }    
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function Button3_onclick() {

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
            height: 17px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <fieldset>
                    <div style="padding-left: 0%">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        <asp:Label ID="lblentry" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnl123" runat="server">
                     
                        <asp:Panel ID="pnlMain" runat="server">
                        
                         
                        
                        
                            <table >
                              <tr>
                              <td  style="width:150px;">
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="Payment Due Range"></asp:Label>
                                                 </label>
                                   </td>
                                <td colspan="2">
                               
                                        <table>
                                            <tr>
                                                <td valign="bottom">
                                                    <label>
                                                        <asp:Label ID="Label10" runat="server" Text="From"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="fss" runat="server" ControlToValidate="txtfromdate" ValidationGroup="1"
                                                            ErrorMessage="*" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                                <td valign="bottom">
                                                    <label>
                                                        <asp:TextBox ID="txtfromdate" Width="75px" runat="server"></asp:TextBox></label><cc1:CalendarExtender
                                                            ID="CalendarExtender4" runat="server" PopupButtonID="txtfromdue" TargetControlID="txtfromdate">
                                                        </cc1:CalendarExtender>
                                                    <label>
                                                        <asp:Label ID="Label11" runat="server" Text="To"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttodate" ValidationGroup="1"
                                                            ErrorMessage="*" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <label>
                                                        <asp:TextBox ID="txttodate" Width="75px" runat="server"></asp:TextBox></label>
                                                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txttodue"
                                                        TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                   
                                </td>
                              </tr>
                                <tr>
                                    <td  style="width:150px;">
                                        <label>
                                            <asp:Label ID="Label15" runat="server" Text="Business Name "></asp:Label>
                                            <asp:RequiredFieldValidator ID="dfv" runat="server" ControlToValidate="ddlwarehouse"
                                                ValidationGroup="1" InitialValue="0" SetFocusOnError="true" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </label>
                                    </td>
                                    <td >
                                      <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlwarehouse_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </label>
                                    </td>
                                   <td>
                                        <label>
                                          <asp:Label ID="Label19" runat="server" Text="Vendor Name"></asp:Label>
                                         
                                        </label>
                                    </td>
                                    <td >
                                     <label>
                                            <asp:DropDownList ID="ddlparty" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                      
                                    </td>
                                   
                              
                                  
                                </tr>
                              
                               
                                <tr>
                                    <td align="center" colspan="4">
                                    
                                    
                                    <table>
                                    <tr>
                                    <td>
                                      <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left" Visible="False" Width="100%">
                                         <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="6" Width="100%">
                                                <asp:ListItem Selected="True">Date</asp:ListItem>
                                               <asp:ListItem Selected="True">Entry Type</asp:ListItem>
                                                <asp:ListItem Selected="True">Entry No.</asp:ListItem>
                                              
                                                <asp:ListItem Selected="True">Party Name</asp:ListItem>
                                                 <asp:ListItem Selected="True">Invoice No.</asp:ListItem>
                                                <asp:ListItem Selected="True">Gross Amount</asp:ListItem>
                                                <asp:ListItem>Tax1</asp:ListItem>
                                                <asp:ListItem>Tax2</asp:ListItem>
                                                  <asp:ListItem>Tax3</asp:ListItem>
                                                <asp:ListItem Selected="True">Shipping Charges</asp:ListItem>
                                                <asp:ListItem Selected="True">Net Amount</asp:ListItem>
                                                  <asp:ListItem Selected="True">Amount Due Date</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    </table>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                   
                                   
                                       <asp:Button ID="btnSelectColums" runat="server" CssClass="btnSubmit" Text="Select Columns"
                                            OnClick="btnSelectColums_Click" Visible="False" />
                                        &nbsp;<asp:Button ID="btnSearchGo" CssClass="btnSubmit" runat="server" OnClick="btnSearchGo_Click"
                                            Text="Go" ValidationGroup="1" />
                                    
                                    </td>
                                </tr>
                             
                            </table>
                        </asp:Panel>
                     </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Invoices" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <label>
                            <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                OnClick="Button1_Click1" />
                              </label>
                                <label>
                            <input id="Button3" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" value="Print" visible="false" />
                        </label>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel1" runat="server"  Width="100%">
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <table width="100%">
                            <tr align="center">
                                <td class="style1">
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblcompname" runat="server" Font-Italic="True"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                 <asp:Label ID="lblBusnesshea" runat="server" Text="Business : " Font-Italic="True"></asp:Label>
                                                    <asp:Label ID="lblstore" runat="server" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                              <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                  <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Party :"></asp:Label>
                                                    <asp:Label ID="lblparty" runat="server" Font-Bold="True" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label26" runat="server" Font-Italic="true" Text="List of Invoices"></asp:Label>
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblinv" runat="server" Font-Bold="True" Font-Size="13px" Text="">
                                                    </asp:Label>
                                                    
                                                    
                                                  
                                                </td>
                                            </tr>
                                             <tr>
                                                <td   style="text-align: left; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="lblddf" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <asp:GridView ID="gridpurchaseregister" runat="server" AllowSorting="True" 
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" 
                                                DataKeyNames="Purchase_Details_Id" EmptyDataText="No Record Found." 
                                                FooterStyle-CssClass="ftr" GridLines="Both" 
                                                OnRowCommand="gridpurchaseregister_RowCommand" 
                                                OnRowDataBound="gridpurchaseregister_RowDataBound" 
                                                OnRowDeleting="gridpurchaseregister_RowDeleting" 
                                                OnSorting="gridpurchaseregister_Sorting" PagerStyle-CssClass="pgr" 
                                                ShowFooter="True" onrowediting="gridpurchaseregister_RowEditing">
                                                <Columns>
                                                   
                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                           
                                                           <asp:Label ID="lbldate" runat="server"   Text='<%# Bind("Date") %>' ></asp:Label>
                                                              
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                  <asp:TemplateField HeaderText="Entry Type" SortExpression="SortName"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                           
                                                           <asp:Label ID="lbletype" runat="server"   Text='<%# Bind("SortName") %>' ></asp:Label>
                                                              
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Entry No." 
                                                        SortExpression="EntryNumer"  HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                           
                                                           
                                                            <asp:Label ID="lblentryno" runat="server" 
                                                                Text='<%# Bind("EntryNumer") %>'></asp:Label>
                                                          
                                                            <asp:Label ID="Label5" runat="server" 
                                                                Text='<%# Bind("EntryTypeId") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Party Name"  HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="PartyName">
                                                        <ItemTemplate>
                                                          
                                                          
                                                            <asp:Label ID="lblpartyname" runat="server"
                                                                Text='<%# Bind("PartyName") %>'></asp:Label>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice No."  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                         
                                                           
                                                            <asp:Label ID="lblpiid" runat="server" 
                                                                Text='<%# Bind("PurchaseInvoiceNumber") %>'></asp:Label>
                                                         
                                                            
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gross Amount"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                           
                                                            <asp:Label ID="lblgrdnetamt" runat="server"  Text=""></asp:Label>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax1"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdtax1" runat="server" 
                                                                Text='<%# Bind("TexAmount1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax2"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdtax2" runat="server" 
                                                                Text='<%# Bind("texAmount2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax3"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdtax3" runat="server" 
                                                                Text='<%# Bind("texAmount3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipping Charges"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdshippingcharg" runat="server" 
                                                                Text='<%# Bind("shippingandHandlingCharges") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net Amount" SortExpression="TotalAmt"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdgrosstotal" runat="server"
                                                                Text='<%# Bind("TotalAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Due Date" SortExpression="GrnMaster_Id"  HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblduedate" runat="server"
                                                                Text='<%# Bind("GrnMaster_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="5%" HeaderText="View Doc"  HeaderStyle-HorizontalAlign="Left" Visible="false" >
                                                        <ItemTemplate>   <asp:Label ID="lbldocno" runat="server"></asp:Label>
                                                            <asp:ImageButton ID="img1" runat="server" AlternateText="" 
                                                                CausesValidation="true" 
                                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>' 
                                                                CommandName="AddDoc" Height="22px"  Width="22px"
                                                                ImageUrl="~/ShoppingCart/images/Docimg.png" />
                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                         
                                            <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Purchase_Details_Id") %>' CommandName="Edit"
                               ImageUrl="~/Account/images/edit.gif">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" Visible="false"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" 
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                       
                                                            <asp:ImageButton ID="llinkbb" runat="server"  ToolTip="Delete"
                                                                CommandArgument='<%# Eval("Purchase_Details_Id") %>' CommandName="Delete" 
                                                                ImageUrl="~/Account/images/delete.gif" 
                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                
                                                    
                                                        <asp:TemplateField  HeaderText="Add Doc" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lbladdd" runat="server"  CommandArgument='<%# Eval("Tranction_Master_Id") %>' CommandName="Docadd"  ToolTip="Add Doc" Height="20px" ImageUrl="~/Account/images/attach.png" Width="20px">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                        <asp:Panel ID="pnlactych" runat="server" CssClass="modalPopup"
                       >
                        <fieldset>
                        <legend> <asp:Label ID="lblm" runat="server" ForeColor="Black">Note</asp:Label></legend>
                       
                        <table cellpadding="0" cellspacing="0">
                            
                            <tr>
                                <td >
                                   
                                </td>
                            </tr>
                            <tr>
                                <td >
                                <label>
                                    <asp:Label ID="lbl344" runat="server"  Text="Please note that the date selected by you is out of current active accounting year. "></asp:Label>
                                  </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                 <label>
                                    <asp:Label ID="lblm0" runat="server" Text="You will only able to view the records, you cannot edit or delete the records." > </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                 <label>
                                    <asp:Label ID="lbxs" runat="server" Text="For changing the records, You have to change active accounting year. Please contact to Account Manager or Admin " > </asp:Label>
                                    </label>
                                   
                                    </td>
                            </tr>
                            <tr>
                                <td align="center">  <asp:Button ID="Button1" runat="server"  CssClass="btnSubmit" Text="OK" OnClick="btnok_Click"
                                         />
                                    <asp:Button ID="btncanls" runat="server"  CssClass="btnSubmit" Text="Cancel" 
                                         />
                                </td>
                            </tr>
                        </table>
                         </fieldset>
                        <asp:Button ID="HiddenButtoncx2221" runat="server" Style="display: none" />
                    </asp:Panel>
                   
                    <cc1:ModalPopupExtender ID="ModalPopupExtenderAcy" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="pnlactych" TargetControlID="HiddenButtoncx2221" CancelControlID="btncanls">
                    </cc1:ModalPopupExtender>
                    <div style="clear: both;">
                    </div>
                  
                  <asp:Panel ID="Paneldoc" runat="server" Width="600px" CssClass="modalPopup">
                        <fieldset>
                                               <legend>
                                                   <asp:Label ID="lbldoclab" runat="server" Text="List of Document"></asp:Label>
                                               </legend>
                           <table width="100%">
                               <tr>
                                   <td style="width:95%;">
                                   </td>
                                   <td align="right">
                                       <asp:ImageButton ID="ImageButton10" runat="server" 
                                           ImageUrl="~/images/closeicon.jpeg"
                                           Width="16px" />
                                   </td></tr>
                                   <tr>
                                   <td colspan="2">
                                       <label>
                                       <asp:Label ID="lblheadoc" runat="server" 
                                           Text="List of documents attached to "></asp:Label>
                                   <%--    <asp:Label ID="Label22" runat="server" 
                                           Text="List of documents attched to Entry Type"></asp:Label>--%>
                                     <asp:Label ID="lbldocentrytype" runat="server" Font-Bold="True" 
                                           ForeColor="#457cec"></asp:Label>
                                       <asp:Label ID="Label2" runat="server" Text=" entry no."></asp:Label>
                                       <asp:Label ID="lbldocentryno" runat="server" Font-Bold="True" 
                                           ForeColor="#457cec"></asp:Label>
                                       </label>
                                   </td>
                               </tr>
                                   <tr>
                                   <td colspan="2">
                                    
                                                           <asp:Panel ID="pvgris" runat="server" Height="100%" ScrollBars="Vertical" 
                                                               Width="100%">
                                                               <asp:GridView ID="grd" runat="server" AlternatingRowStyle-CssClass="alt" 
                                                                   AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id"  HeaderStyle-HorizontalAlign="Left"
                                                                   OnRowCommand="grd_RowCommand" PagerStyle-CssClass="pgr" Width="100%">
                                                                   <Columns>
                                                                       <asp:BoundField DataField="Datetime" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"/>
                                                                       <asp:BoundField DataField="IfilecabinetDocId" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" />
                                                                       <asp:BoundField DataField="Titlename" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"/>
                                                                       <asp:BoundField DataField="Filename" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left" />
                                                                       <asp:TemplateField HeaderText="View Doc" ItemStyle-ForeColor="#416271">
                                                                           <ItemTemplate>
                                                                               <a href="javascript:void(0)" 
                                                                                   onclick='window.open(&#039;viewdocument.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IfilecabinetDocId")%>&#039;, &#039;welcome&#039;,&#039;width=1200,height=700,menubar=no,status=no&#039;)'>
                                                                               <asp:Label ID="lbldoc" runat="server" ForeColor="#416271" Text="View Doc"></asp:Label>
                                                                               </a>
                                                                           </ItemTemplate>
                                                                       </asp:TemplateField>
                                                                   </Columns>
                                                               </asp:GridView>
                                                           </asp:Panel>
                                                      
                                   </td>
                               </tr>
                           </table>
                        
                        
                         </fieldset></asp:Panel>
                        <asp:Button ID="Button4" runat="server" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Paneldoc" TargetControlID="Button4" CancelControlID="ImageButton10">
                        </cc1:ModalPopupExtender>
                   
                       <div style="clear: both;">
                    </div>
                            <asp:Panel ID="pnladdress" runat="server" CssClass="modalPopup" Width="70%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <input type="button" value="Print" id="Button6" runat="server" onclick="javascript:CallPrint1('pnlpt');"
                                                class="btnSubmit" visible="true" />
                                            &nbsp;
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlpt" runat="server">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Label ID="Label25" runat="server" Text="List of Accounts Entry Detail"></asp:Label>
                                                    </legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label32" runat="server" Text="Business Name:"> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblbnp" runat="server" Text=""> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    &nbsp;</label>
                                                                <label>
                                                                    <asp:Label ID="flbd" runat="server" Text="Date:"> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lbldatep" runat="server" Text=""> 
                                                                    </asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <asp:Label ID="Label31" runat="server" Text="Entry Type:"> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lbletp" runat="server" Text=""> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    &nbsp;</label>
                                                                <label>
                                                                    <asp:Label ID="Label34" runat="server" Text="Entry Number:"> 
                                                                    </asp:Label>
                                                                </label>
                                                                <label>
                                                                    <asp:Label ID="lblenp" runat="server" Text="Date:"> 
                                                                    </asp:Label>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlof" runat="server" ScrollBars="Auto">
                                                                    <asp:GridView ID="grdPartyList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                        EmptyDataText="No Record Found." PageSize="20">
                                                                        <%-- <EmptyDataRowStyle ForeColor="Peru" />--%>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Account" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblacccc" Visible="true" runat="server" Text=""></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="35%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Memo" SortExpression="Memo" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmem" Visible="true" runat="server" Text='<%#Eval("Memo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="35%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Debit" SortExpression="AmountDebit" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDebit" Visible="true" runat="server" Text='<%#Eval("AmountDebit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Credit" SortExpression="AmountCredit" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCredit" Visible="true" runat="server" Text='<%#Eval("AmountCredit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="ImgBtnAddress" runat="server" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="modalBackground"
                                PopupControlID="pnladdress" TargetControlID="ImgBtnAddress" runat="server" CancelControlID="ImageButton1">
                            </cc1:ModalPopupExtender>
                       
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
