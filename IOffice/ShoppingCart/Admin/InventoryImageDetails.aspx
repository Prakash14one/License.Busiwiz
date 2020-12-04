<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="InventoryImageDetails.aspx.cs" Inherits="InventoryImageDetails" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:UpdatePanel ID="uppae" runat="server">
        <ContentTemplate>
            <div class="products_box">
                 <div style="padding-left:1%">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                 </div>
            </div>
            <fieldset>            
                <legend>
                    <asp:Label ID="Label10" runat="server" Text="Search for inventory that you wish to create a slideshow for" ></asp:Label>
                </legend>
                <table width="100%">
                     <tr>
                        <td width="25%">
                            <label>
                            <asp:Label ID="Label2" runat="server" Text="Select by Business Name" ></asp:Label>
                            </label>
                            
                        </td>
                        <td width="25%">
                            <label>
                            <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True"   
                                Width="200px" onselectedindexchanged="ddlWarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                            </label>
                        </td>
                        <td width="25%">
                           <label>
                            <asp:Label ID="Label7" runat="server" Text="Search by Name" ></asp:Label>
                            </label>
                        </td>
                        <td width="25%">
                           <label>
                                <asp:TextBox ID="txtSearchInvName" runat="server" Width="194px"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                            <asp:Label ID="Label3" runat="server" Text="Category" ></asp:Label>
                            </label>
                             
                        </td>
                        <td>
                            <label>
                            <asp:DropDownList ID="ddlInvCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvCat_SelectedIndexChanged"
                                Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                            <label>
                            <asp:Label ID="Label4" runat="server" Text="Sub Category" ></asp:Label>
                            </label>
                            
                        </td>
                        <td>
                            <label>
                            <asp:DropDownList ID="ddlInvSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubCat_SelectedIndexChanged"
                                Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <label>
                            <asp:Label ID="Label5" runat="server" Text="Sub Sub Category" ></asp:Label>
                            </label>
                             
                        </td>
                        <td>
                            <label>
                            <asp:DropDownList ID="ddlInvSubSubCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvSubSubCat_SelectedIndexChanged"
                                Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                        <td>
                             <label>
                            <asp:Label ID="Label6" runat="server" Text="Select Product" ></asp:Label>
                            </label>
                             
                        </td>
                        <td align="left">
                            <label>
                                <asp:DropDownList ID="ddlInvName" runat="server" Width="200px" 
                                onselectedindexchanged="ddlInvName_SelectedIndexChanged1">
                            </asp:DropDownList>
                            </label>
                            
                        </td>
                    </tr>
                     
                    <tr>
                        <td colspan="4" align="center">
                                           
                            <asp:Button ID="btnSearchGo" runat="server" onclick="imgBtnSearchGo_Click" Text="  Go  " CssClass="btnSubmit"  />
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                        
                        </td>
                    </tr>
                    </table>
            </fieldset>
                   
                <asp:Panel ID="Panel1" runat="server" Height="170px" ScrollBars="Both" Visible="False" Width="100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label11" runat="server" Text="Search for inventory that you wish to create a slideshow for" ></asp:Label>
                    </legend>
                                <asp:GridView ID="grdInvMasters" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                                                    AlternatingRowStyle-CssClass="alt"
                                    DataKeyNames="InventoryMasterId" EmptyDataText="No Record Found." OnRowCommand="grdInvMasters_RowCommand"
                                    Width="100%">
                                    
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Category : Sub Category : Sub Sub Category" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CatScSsc") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="38%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <a href='Inventoryprofile.aspx?Invmid=<%# Eval("InventoryMasterId")%>' target="_blank">
                                                <asp:Label ID="lblInvName" runat="server" Text='<%#Bind("Name") %>' ForeColor="#416271" ></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="25%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductNo" runat="server" Text='<%#Bind("ProductNo") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvweight" runat="server" Text='<%#Bind("Unit") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvunit" runat="server" Text='<%#Bind("unittype") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Barcode" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBarcode" runat="server" Text='<%#Bind("Barcode") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:ButtonField CommandName="Select1" ItemStyle-ForeColor="#416271" HeaderStyle-HorizontalAlign="Left" Text="View" HeaderText="View" ItemStyle-Width="4%" />
                                        <asp:TemplateField HeaderText="InvMasterId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvMasterId" runat="server" Text='<%#Bind("InventoryMasterId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                </asp:GridView>
                </fieldset>
                </asp:Panel>
                        
                
             <asp:Panel ID="pnlViewFill" runat="server" Visible="False" Width="100%">
                <fieldset>
                <legend>
                    <asp:Label ID="Label8" runat="server" Text="Image Entry for " ></asp:Label>
                    <asp:Label ID="lblInvCScSScName" runat="server"></asp:Label>
                </legend>
                <table id="Table1" cellpadding="0" cellspacing="0" style="width: 100%">                                                       
                    <tr>
                        <td align="right">
                            <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit"
                            CausesValidation="false" OnClick="btncancel0_Click" Text="Printable Version" />
                         <input id="Button2" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                   class="btnSubmit"  type="button" value="Print" visible="false" />
                        </td>
                    </tr>                           
                    <tr>
                        <td>      
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">   
                            <table  width="100%">
                                 <tr align="center">
                                            <td colspan="4">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%" style="color:Black; font-weight:bold; font-style:italic; text-align:center">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblcomname" runat="server"  Font-Size="20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="Label9" runat="server"  Font-Size="20px" Text="Business : "></asp:Label>
                                                                <asp:Label ID="lblbusiness" runat="server"  Font-Size="20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblhead" runat="server"  Font-Size="18px" Text="List of Images Detail"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="font-size:16px; font-weight:normal;">
                                                                <asp:Label ID="lblitemdd" runat="server"  Text="Item Name :"></asp:Label>
                                                                <asp:Label ID="lblitemname" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                       
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                <tr>
                                <td colspan="4">
                                
                                                
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" GridLines="Both" ShowFooter="true" 
                                                                    AlternatingRowStyle-CssClass="alt"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
                                OnRowUpdating="GridView1_RowUpdating" Width="100%" 
                                        onrowcommand="GridView1_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                        <asp:Label ID="lblinvImgMdid" runat="server"  Visible="false"></asp:Label>
                                            
                                            <asp:Label ID="lblviewid" runat="server" Text='<%# Eval("ViewID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblview" runat="server" Text='<%# Eval("ViewName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                         <asp:Label ID="lblinvImgMdid" runat="server"  Visible="false"></asp:Label>
                                            
                                            <asp:Label ID="lblviewid" runat="server" Text='<%# Eval("ViewID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblview" runat="server" Text='<%# Eval("ViewName")%>'></asp:Label>
                                       
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Images for small slideshow" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="29%">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblheadsmall" runat="server" Text="Images for small slideshow"></asp:Label>
                                            <asp:Button ID="Buttonsmallslideshow" runat="server" CssClass="btnSubmit"  
                                                        OnClick="LinkButton4_Click" Text="View Slideshow"  />
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:Image ID="imgsmall" runat="server" Height="50px" Width="50px"  Visible="false" />
                                            <br />
                                            <asp:FileUpload ID="FileUploadSmallImage" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4555" runat="server" ErrorMessage="*"
                                                ControlToValidate="FileUploadSmallImage" ValidationGroup="q1"></asp:RequiredFieldValidator>
                                                
                                            <asp:Button ID="btnsmall" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addsmall" ValidationGroup="q1" /> 
                                            <br />
                                            Recommended Image Size : 200 X 400
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imgsmall" runat="server" Height="50px" Visible="false" Width="50px" />
                                            <br />
                                            <asp:Label ID="lblSmallImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                            <asp:ImageButton ID="delesmallimg" runat="server"  
                                                 CommandName="DeleteSmall" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                 </asp:ImageButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:Label ID="Label14" runat="server" Font-Bold="true" ForeColor="#416271" Text="Total Size of Slideshow"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Size of the Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsize" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text="KB"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                          <asp:Label ID="lbltotalsmall" ForeColor="#0099ff"  runat="server" Text="Label"></asp:Label>
                                    </FooterTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Images for large slideshow" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="29%">
                                         <HeaderTemplate>
                                            <asp:Label ID="lblheadlarge" runat="server" Text="Images for large slideshow"></asp:Label>
                                            <asp:Button ID="Buttonlargeslideshow" runat="server" CssClass="btnSubmit" 
                                                        OnClick="LinkButton5_Click" Text="View Slideshow"  />
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:Image ID="imglarge" runat="server" Height="50px" Width="80px"  Visible="false" />
                                            <br />
                                            <asp:FileUpload ID="FileUploadLargeImage" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45565" runat="server" ErrorMessage="*"
                                                ControlToValidate="FileUploadLargeImage" ValidationGroup="q2"></asp:RequiredFieldValidator>
                                            <asp:Button ID="btnlarge" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addLarge" ValidationGroup="q2" /> 
                                            <br />
                                            Recommended Image Size : 400 X 800
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%--<asp:Image ID="Imglarge" runat="server" ImageUrl='<%#Bind("LargeImageUrl")%>' />--%>
                                            <%--<img height="50" hspace="0" src='../<%#DataBinder.Eval(Container.DataItem,"LargeImageUrl") %>' width="50" border="0" />--%>
                                            <asp:Image ID="imglarge" runat="server" Height="50px" Visible="false" Width="80px" />
                                            <br />
                                            <asp:Label ID="lblLargeImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                            <asp:ImageButton ID="delelargeimg" runat="server"  
                                                 CommandName="DeleteLarge" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                 </asp:ImageButton>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                            <asp:Label ID="Label15" runat="server" Font-Bold="true" ForeColor="#416271" Text="Total Size of Slideshow"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size of the Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllargesize" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="Label13" runat="server" Text="KB"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbltotallarge"  ForeColor="#0099ff" runat="server" Text="Label"></asp:Label>
                                    </FooterTemplate>
                                   </asp:TemplateField>
                                    <asp:CommandField  CausesValidation="false" ShowEditButton="True"  HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Edit"    ButtonType="Image" EditImageUrl="~/Account/images/edit.gif" HeaderImageUrl="~/Account/images/edit.gif" 
                                          UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                     <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"   HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                 <asp:ImageButton ID="Btn" runat="server"  
                                                 CommandName="Delete" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                 </asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="3%" />
                                     </asp:TemplateField>
                                    
                                </Columns>
                                
                            </asp:GridView>
                            </td>
                                </tr>
                                
                            </table>  
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ImageButton1" runat="server" Text="Submit" OnClick="Button1_Click"  Visible="False" />
                              <asp:Button ID="ImageButton2" runat="server" Text="Cancel" OnClick="Button2_Click"   Visible="False" />  
                           
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>          
     </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="GridView1" />
        </Triggers>
</asp:UpdatePanel>
</asp:Content>

