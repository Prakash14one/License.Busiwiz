<%@ Page Title="Product Component Master" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="Product_ComponentMaster.aspx.cs" Inherits="ProductComponentMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

 
    <ContentTemplate>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" ForeColor="#CC0000"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" GroupingText="Product Component Master">

            <table style="width: 100%">
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label67" runat="server" Text="Product Component Name"></asp:Label>
                           <asp:Label ID="Label1" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="TextBox28"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label41" runat="server" Text="Image File Name"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload25" runat="server" />
                        <asp:Button ID="imgfilename" runat="server" onclick="imgfilename_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image13" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label66" runat="server"></asp:Label>
                    </td>
                   
                    
                </tr>
                
                <tr>
                    <td width="20%">
                        <label>
                        <asp:Label ID="Label68" runat="server" Text="Product Component MasterTbl ID" 
                            Visible="False"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox27" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label23" runat="server" Text="Brand Name"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="DropDownList4" runat="server" Width="200px" >
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td  width="40%">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label19" runat="server" Text="Supplier Name"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%" valign="top">
                        <label>
                        <asp:Label ID="Label42" runat="server" Text="Specification"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        
                        <cc2:HtmlEditor ID="TextBox9" runat="server"></cc2:HtmlEditor>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label43" runat="server" Text="Capacity/Size"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td  width="40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label44" runat="server" Text="Model Number"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td  width="40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label45" runat="server" Text="Vendor Product PageURL"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td style="width: 43%" width="40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label46" runat="server" Text="Image Small Front"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload13" runat="server" />
                        <asp:Button ID="imgsmallfront" runat="server" onclick="imgsmallfront_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image1" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label54" runat="server"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label47" runat="server" Text="Image Small Back"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="imgsmallback" runat="server" onclick="imgsmallback_Click" Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image2" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label55" runat="server"></asp:Label>
                    </td>
                    
                  
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label48" runat="server" Text="Image Small TOP"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload14" runat="server" />
                        <asp:Button ID="imgsmalltop" runat="server" onclick="imgsmalltop_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image3" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label56" runat="server"></asp:Label>
                    </td>
                    
                   
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label49" runat="server" Text="Image Small Bottom"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload15" runat="server" />
                        <asp:Button ID="imgsmallbottom" runat="server" onclick="imgsmallbottom_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image4" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label57" runat="server"></asp:Label>
                    </td>
                  
                   
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label50" runat="server" Text="Image Small Right"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload16" runat="server" />
                        <asp:Button ID="imgsmallright" runat="server" onclick="imgsmallright_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image5" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label58" runat="server"></asp:Label>
                    </td>
                   
                   
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label51" runat="server" Text="Image Small Left"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload17" runat="server" />
                        <asp:Button ID="imgsmallleft" runat="server" onclick="imgsmallleft_Click" 
                            Text="Upload" />
                    </td>
                    <td width="20%">
                        <asp:Image ID="Image6" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label59" runat="server"></asp:Label>
                    </td>
                  
                  
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label33" runat="server" Text="Video URL"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label34" runat="server" Text="Image Big Front"></asp:Label>
                        </label>
                    </td>
                     <td colspan="2">
                        <asp:FileUpload ID="FileUpload18" runat="server" />
                        <asp:Button ID="imgbigfront" runat="server" onclick="imgbigfront_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image7" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label60" runat="server"></asp:Label>
                    </td>
                   
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label35" runat="server" Text="Image Big Back"></asp:Label>
                        </label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload19" runat="server" />
                        <asp:Button ID="imgbigback" runat="server" onclick="imgbigback_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image8" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label61" runat="server"></asp:Label>
                    </td>
                    
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label36" runat="server" Text="Image Big TOP"></asp:Label>
                        </label>
                    </td>
                     <td colspan="2">
                        <asp:FileUpload ID="FileUpload20" runat="server" />
                        <asp:Button ID="imgbigtop" runat="server" onclick="imgbigtop_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image9" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label62" runat="server"></asp:Label>
                    </td>
                   
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label37" runat="server" Text="Image Big Bottom"></asp:Label>
                        </label>
                    </td>
                       <td colspan="2">
                        <asp:FileUpload ID="FileUpload21" runat="server" />
                        <asp:Button ID="imgbigbottom" runat="server" onclick="imgbigbottom_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image10" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label63" runat="server"></asp:Label>
                    </td>
                 
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label38" runat="server" Text="Image Big Right"></asp:Label>
                        </label>
                    </td>
                      <td colspan="2">
                        <asp:FileUpload ID="FileUpload22" runat="server" />
                        <asp:Button ID="imgbigright" runat="server" onclick="imgbigright_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image11" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label64" runat="server"></asp:Label>
                    </td>
                  
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label39" runat="server" Text="Image Big Left"></asp:Label>
                        </label>
                    </td>
                     <td colspan="2">
                        <asp:FileUpload ID="FileUpload23" runat="server" />
                        <asp:Button ID="imgbigleft" runat="server" onclick="imgbigleft_Click" 
                            Text="Upload" />
                    </td>
                    <td width="25%">
                        <asp:Image ID="Image12" runat="server" Style="height: 75px;  width: 99px;" />
                        <asp:Label ID="Label65" runat="server"></asp:Label>
                    </td>
                   
                   
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label12" runat="server" Text="Product Component DetailTbl ID" 
                            Visible="False"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox29" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label13" runat="server" Text="Batch Number"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label14" runat="server" Text="Cost Price"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label15" runat="server" Text="Volume Discount Available Percent"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label16" runat="server" Text="Volume Discount Available Amount"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label17" runat="server" Text="Effective Start Date"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                         PopupButtonID="TextBox5" TargetControlID="TextBox5">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label18" runat="server" Text="Effective End Date"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                         PopupButtonID="TextBox6" TargetControlID="TextBox6">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
               
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label20" runat="server" Text="Sell Price"></asp:Label>
                        </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        <label>
                        <asp:Label ID="Label21" runat="server" 
                            Text="MarkUp Percentage Over EffectiveCost"></asp:Label>
                       </label>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                        <asp:Label ID="Label69" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="20%">
                        <label>
                        <asp:Label ID="Label52" runat="server" Text="Active"></asp:Label>
                        </label>
                    </td>
                    <td width="20%">
                       <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td " width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        <asp:Button ID="Btnsave" runat="server" onclick="Btnsave_Click" Text="Save" ValidationGroup="1" />
                        <asp:Button ID="Btnupdate" runat="server" Text="Update"  ValidationGroup="1"
                            onclick="Btnupdate_Click" />
                        <asp:Button ID="Btncancel" runat="server" Text="Cancel" 
                            onclick="Btncancel_Click" />
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td  width="25%">
                        &nbsp;</td>
                </tr>
            </table>

        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server"  GroupingText="List Of Product Componenet Master">
            <table style="width: 100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="ADD NEW" 
                            onclick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                   
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" 
                                                onpageindexchanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="True">
                                                <Columns>                       
                                                    <asp:BoundField DataField="ComponentName" HeaderText="Product Name" />
                                                    <asp:BoundField DataField="Active" HeaderText="Active" />
                                                    <asp:BoundField DataField="BrandID" HeaderText="Brand Name" />
                                                    <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                    <asp:BoundField DataField="Size" HeaderText="Capacity/Size" />
                                                    <asp:BoundField DataField="ModelNumber" HeaderText="Model Number" />
                                                    <asp:BoundField DataField="BatchNumber" HeaderText="Batch Number" />
                                                    <asp:BoundField DataField="CostPrice" HeaderText="Cost Price" />
                                                    <asp:BoundField DataField="VolumeDiscoutnAvailablePercent" 
                                                        HeaderText="Discount Available Percent" />
                                                    <asp:BoundField DataField="VolumeDiscoutnAvailableAmount" 
                                                        HeaderText="Discount Available Amount" />
                                                    <asp:BoundField DataField="EffectveStartDate" 
                                                        HeaderText="Start Date" />
                                                    <asp:BoundField DataField="EffectiveEndDate" HeaderText="End Date" />
                                                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier Name" />
                                                    <asp:BoundField DataField="SellPrice" HeaderText="Sell Price" />
                                                    <asp:BoundField DataField="MarkUpPercentageovereffectivecost" 
                                                        HeaderText="Percentage Overeffective Cost" />
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" 
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/edit.gif" onclick="ImageButton3_Click"  />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" 
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" 
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label15" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" 
                                                                 />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                       
                        </Columns>
                       <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                   
                       </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
 <asp:PostBackTrigger ControlID="imgfilename" />
<asp:PostBackTrigger ControlID="imgsmallfront"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgsmallback"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgsmalltop"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgsmallbottom"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgsmallright"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgsmallleft"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigfront"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigback"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigtop"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigbottom"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigright"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgbigleft"></asp:PostBackTrigger>
 </Triggers>
 
</asp:UpdatePanel>
</asp:Content>

