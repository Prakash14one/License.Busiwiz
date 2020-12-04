<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="InventoryProfile.aspx.cs" Inherits="ShoppingCart_Admin_InventoryProfile" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<script language="javascript" type="text/javascript">


        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  


</script>--%>

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
        <fieldset>
            <label>
                <asp:Label ID="Label5" runat="server" Text="Select Category - Sub Category - Sub Sub Category"></asp:Label>
                 <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px"
                                type="hidden" />
                <asp:DropDownList ID="ddlinventoryitems" runat="server" ValidationGroup="1" 
                                AutoPostBack="True"  Width="350px"
                                onselectedindexchanged="ddlinventoryitems_SelectedIndexChanged">
                            </asp:DropDownList>
            </label>
             <label>
                <asp:Label ID="Label6" runat="server" Text="Product Name"></asp:Label>
                <asp:DropDownList ID="ddlproduct" runat="server" ValidationGroup="1">
                            </asp:DropDownList>
            </label>
           <label>
                <br />
             <asp:Button ID="imgBtnSearchGo" runat="server" 
               OnClick="imgBtnSearchGo_Click" ValidationGroup="1" CssClass="btnSubmit"  Text=" Go " />
            </label>
        </fieldset>
        <fieldset>
            <table id="Table1" style="width: 100%">
                <tr>
                    <td>
                        <label>
                        <asp:Label ID="Label7" runat="server" Text="Product Name"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="lblinvid" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="Label8" runat="server" Text="Product Number"></asp:Label>
                        </label>                    
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="lblpnumber" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Product Barcode"></asp:Label>
                        </label>                   
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="lblpbarcode" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Category"></asp:Label>
                        </label>                    
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="lblcategory" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Sub Category"></asp:Label>
                        </label>                     
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="lblsubcategory" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text="Sub Sub Category"></asp:Label>
                        </label>                    
                    </td>
                    <td>
                        <label>
                        <asp:Label ID="lblsubsubcat" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label13" runat="server" Text="Description"></asp:Label>
                        </label> 
                         
                    </td>
                    <td colspan="3">
                        <label>
                        <asp:Label ID="lbldis" CssClass="lblSuggestion" runat="server" ></asp:Label>
                        </label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                 <asp:Label ID="Label2" runat="server" Text="Stock/Selling Price"></asp:Label>
            </legend>
            <table width="100%">               
                <tr>
                    <td align="left">
                        <asp:GridView ID="grdd" runat="server" AutoGenerateColumns="False" 
                            CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                             DataKeyNames="InventoryWarehouseMasterId" Width="100%">
                            <Columns>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Business Name" ItemStyle-Width="18%">
                                  <ItemTemplate>
                                      <asp:Label ID="lblbnae" runat="server" Text='<%# Bind("Wname") %>'></asp:Label>                                    
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Qty" ItemStyle-Width="9%">
                                  <ItemTemplate>
                                      <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>                                    
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Price" ItemStyle-Width="9%">
                                  <ItemTemplate>
                                      <asp:Label ID="lblprice" runat="server" Text='<%# Bind("Rate") %>'></asp:Label>                                    
                                  </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6" HeaderText="Weight" ItemStyle-Width="9%">
                                      <ItemTemplate>
                                          <asp:Label ID="txtgrdwight" runat="server" Text='<%# Bind("Weight") %>' Width="50px"></asp:Label>
                                      </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="6px"></HeaderStyle>
                                  </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit" ItemStyle-Width="8%">
                                      <ItemTemplate>
                                          <asp:Label ID="grdweight" runat="server" Text='<%# Bind("utype") %>'></asp:Label>
                                      </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Preferred Vendor" ItemStyle-Width="20%">
                                      <ItemTemplate>
                                          <asp:Label ID="grdvendor" Text='<%# Bind("Compname") %>' runat="server"></asp:Label>
                                      </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                  </asp:TemplateField>
                                  </Columns>
                                  
                                  <EmptyDataTemplate>
                                      No Record Found.
                                  </EmptyDataTemplate>
                              </asp:GridView>
                    </td>
                </tr>     
            </table>
        </fieldset> 
         <fieldset>
            <legend>
                <asp:Label ID="lbla" runat="server" Text="Images"></asp:Label>
            </legend>
            <table width="100%">              
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                             OnRowEditing="GridView1_RowEditing" EmptyDataText="No Record Found."
                            OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand" 
                            Width="100%" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <asp:TemplateField HeaderText="Thumbnail Image" ItemStyle-Width="47%">
                                    <ItemTemplate>
                                        <asp:Image ID="imgsmall" runat="server" Height="50px" Visible="false" Width="50px" />
                                        <br />
                                        <asp:Label ID="lblSmallImageText" runat="server" Text="No Image Available" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="FileUploadSmallImage" runat="server"  />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadSmallImage" ValidationGroup="qq1"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnsmall" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addsmall" ValidationGroup="qq1" /> 
                                        <br />
                                        <asp:Image ID="imgsmall" runat="server" Height="50px" Visible="false" Width="50px" />
                                        <br />
                                        <asp:Label ID="lblSmallImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Large Image">
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="FileUploadLargeImage" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4555" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadLargeImage" ValidationGroup="qq2"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnlarge" runat="server" Text="Add" CssClass="btnSubmit" CommandName="addLarge" ValidationGroup="qq2" /> 
                                        <br />
                                        <asp:Image ID="imglarge" runat="server" Height="50px" Visible="false" Width="80px" />
                                        <br />
                                        <asp:Label ID="lblLargeImageText" runat="server" Text="No Image Available" Visible="false"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="imglarge" runat="server" Height="50px" Visible="false" Width="80px" />
                                        <br />
                                        <asp:Label ID="lblLargeImageText" runat="server" Text="No Image Available" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField CausesValidation="false" HeaderStyle-HorizontalAlign="Left"  ButtonType="Image" HeaderText="Edit" ItemStyle-Width="4%" HeaderImageUrl="~/Account/images/edit.gif"
                                     EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                                        ShowEditButton="True" ValidationGroup="qq1" />
                                
                                <asp:CommandField ShowDeleteButton="True" Visible="false" CausesValidation="false" ValidationGroup="qq1" HeaderText="Delete" />
                                <asp:TemplateField HeaderText="HdnInvMid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvMid" runat="server" Text='<%#Eval("InventoryMasterId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblInvImgId" runat="server" Text='<%#Eval("InventoryImgMasterID") %>'
                                            Visible="false" Width="100Px" Height="50Px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblInvMid" runat="server" Text='<%#Eval("InventoryMasterId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblInvImgId" runat="server" Text='<%#Eval("InventoryImgMasterID") %>'
                                            Visible="false" Width="100Px" Height="50Px"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>                           
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </fieldset> 
        <fieldset>
            <legend>
                 <asp:Label ID="Label3" runat="server" Text="Slide Show"></asp:Label>
            </legend>
            <table width="100%">               
                <tr>
                    <td>
                        <asp:GridView ID="grdslide" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            OnRowCancelingEdit="grdslide_RowCancelingEdit" OnRowDataBound="grdslide_RowDataBound"
                            OnRowDeleting="grdslide_RowDeleting" OnRowEditing="grdslide_RowEditing" 
                            OnRowUpdating="grdslide_RowUpdating" Width="100%" 
                            HeaderStyle-HorizontalAlign="Left" onrowcommand="grdslide_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="View" HeaderStyle-Width="3%" >
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
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Small Image" ItemStyle-Width="39%">
                                    <EditItemTemplate>
                                        <asp:Image ID="imgsmalled" runat="server" Height="50px" Width="50px" Visible="false" />
                                        <br />
                                        <asp:FileUpload ID="FileUploadSmallImage" runat="server"  />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4555" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadSmallImage" ValidationGroup="q1"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnsmall" runat="server" Text="Add" CssClass="btnSubmit" CommandName="adddetailsmall" ValidationGroup="q1" /> 
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Large Image">
                                    <EditItemTemplate>                                        
                                        <asp:Image ID="imglargeed" runat="server" Height="50px" Width="80px" Visible="false"  />
                                        <br />
                                        <asp:FileUpload ID="FileUploadLargeImage" runat="server"  />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator45565" runat="server" ErrorMessage="*"
                                            ControlToValidate="FileUploadLargeImage" ValidationGroup="q2"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnlarge" runat="server" Text="Add" CssClass="btnSubmit" CommandName="adddetailLarge" ValidationGroup="q2" />                                             
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
                                </asp:TemplateField>
                                <asp:CommandField  CausesValidation="false" HeaderStyle-HorizontalAlign="Left"  ButtonType="Image" HeaderText="Edit" ItemStyle-Width="4%" HeaderImageUrl="~/Account/images/edit.gif"
                             EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                                ShowEditButton="True" />
                                 <asp:CommandField ShowDeleteButton="True" CausesValidation="false" Visible="false"  ValidationGroup="qq1" HeaderText="Delete" />
                            </Columns>                            
                        </asp:GridView>
                    </td>
                </tr> 
             </table>
        </fieldset> 
        <fieldset>
            <legend>
                <asp:Label ID="Label4" runat="server" Text="Multimidia Presentation"></asp:Label>    
            </legend>
          
            <table width="100%">
                <tr>
                    <td width="50%">
                    <asp:GridView ID="grdaudio" runat="server" AutoGenerateColumns="False" 
                    CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                        OnRowCancelingEdit="grdaudio_RowCancelingEdit" 
                        OnRowEditing="grdaudio_RowEditing" EmptyDataText="No Record Found."
                        OnRowUpdating="grdaudio_RowUpdating" 
                        Width="100%" HeaderStyle-HorizontalAlign="Left">
                        <Columns>
                        <asp:TemplateField HeaderText="Audio Presentation" ItemStyle-Width="93%">
                            <ItemTemplate>
                                <asp:Label ID="lblSmallImageText" runat="server" Text="No audio file available" Visible="true"
                                    ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:FileUpload ID="fileaudio" runat="server"  />
                                <asp:RequiredFieldValidator ID="RequiredFieldValida4" runat="server" ErrorMessage="*"
                                    ControlToValidate="fileaudio" ValidationGroup="qq1"></asp:RequiredFieldValidator>

                                <asp:Label ID="lblaudio" runat="server" Text="No audio available" Visible="false" ></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>

                            <asp:CommandField   CausesValidation="false" 
                            HeaderStyle-HorizontalAlign="Left"  ButtonType="Image" HeaderText="Edit" 
                            ItemStyle-Width="4%" HeaderImageUrl="~/Account/images/edit.gif"
                            EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                            ShowEditButton="True" ValidationGroup="qq1"
                            >
                            <HeaderStyle HorizontalAlign="Left" />
                            
                            </asp:CommandField>
                            <asp:TemplateField  HeaderStyle-Width="3%">
                            <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                <%-- <a href='<%# "~/Account/"+ DataBinder.Eval(Container.DataItem,"FileName") %>' target="_blank">
                                <center>Play</center> 
                                </a>--%>
                                <%-- <a onclick="window.open('PlayAudio.aspx?id='<%#DataBinder.Eval(Container.DataItem,"InventoryMasterId") %>', 'welcome','width=200,height=220,menubar=no,status=no')" href="javascript:void(0)">
                                Play                                                                        
                                </a>--%>
                                <a id="playaudio"  onclick="window.open('../PlayAudio.aspx?id=<%#DataBinder.Eval(Container.DataItem, "InventoryMasterId")%>', 'welcome','width=200,height=220,menubar=no,status=no')" href="javascript:void(0)" style="color:#416271">Play</a>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                               
                    </asp:GridView>
                    </td>
                    <td  width="50%">
                        <asp:GridView ID="grdvidio" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            OnRowCancelingEdit="grdvidio_RowCancelingEdit" 
                             OnRowEditing="grdvidio_RowEditing" EmptyDataText="No Record Found."
                            OnRowUpdating="grdvidio_RowUpdating" 
                            Width="100%" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                            <asp:TemplateField HeaderText="Video Presentation" ItemStyle-Width="93%">
                                <ItemTemplate>
                              
                                    <asp:Label ID="lblSmallImageText" runat="server" Text="No vidio file available" Visible="true"
                                        ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:FileUpload ID="fileaudio" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValida4" runat="server" ErrorMessage="*"
                                        ControlToValidate="fileaudio" ValidationGroup="qq1"></asp:RequiredFieldValidator>
                                
                                    <asp:Label ID="lblaudio" runat="server" Text="No vidio available" Visible="false" ></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                                           
                            <asp:CommandField   CausesValidation="false" 
                                HeaderStyle-HorizontalAlign="Left"  ButtonType="Image" HeaderText="Edit" 
                                ItemStyle-Width="4%" HeaderImageUrl="~/Account/images/edit.gif"
                                 EditImageUrl="~/Account/images/edit.gif" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                                    ShowEditButton="True" ValidationGroup="qq1">
                                <HeaderStyle HorizontalAlign="Left" />
                                
                            </asp:CommandField>
                            <asp:TemplateField  HeaderStyle-Width="3%">
                                <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                <%--<a href='<%# "~/Account/"+ DataBinder.Eval(Container.DataItem,"FileName") %>' target="_blank">
                                     <center>Play</center> 
                                    </a>--%>
                                  <a onclick="window.open('../PlayVideo.aspx?id=<%#DataBinder.Eval(Container.DataItem, "InventoryMasterId")%>', 'welcome','width=230,height=250,menubar=no,status=no')" href="javascript:void(0)" style="color:#416271">Play</a>
                                 
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                        
                        </Columns>                                                   
                        </asp:GridView>
                    </td>
                </tr>
            </table>
                   
        </fieldset>
    </div>    
      </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="GridView1" />
          <asp:PostBackTrigger ControlID="grdslide" />
                 <asp:PostBackTrigger ControlID="grdaudio" />
                 <asp:PostBackTrigger ControlID="grdvidio" />
        </Triggers>
</asp:UpdatePanel>
</asp:Content>
