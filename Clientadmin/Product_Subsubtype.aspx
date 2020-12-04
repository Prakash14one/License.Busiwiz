<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="Product_Subsubtype.aspx.cs" Inherits="Productsubsub_type" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <fieldset>
             <legend>
            <asp:Label ID="Label5" runat="server" Text="Product Sub Sub Type Add / Manage"></asp:Label>
        </legend>
        <div style="float: right;">
            <asp:Button ID="Button3" runat="server"  Text="Add New Sub Sub Type"   onclick="Button3_Click" />
        </div>
        <div style="clear: both;">
        </div>
    
    <asp:Panel ID="Panel1" runat="server" >
        <table style="width: 100%">
            <tr>
                            <td>
                               <label>
                                <asp:Label ID="Label20" runat="server" Text="Product Type: "></asp:Label>
                                 <asp:Label ID="Label69" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvname0" runat="server" ControlToValidate="ddlprodtype"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </label> 
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlprodtype" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"  Width="180px">
                                </asp:DropDownList>
                            </td>
                           
                        </tr>
            <tr>
            <td width="20%">
                    <label>
                    <asp:Label ID="Label16" runat="server" Text="Product Sub Type: "></asp:Label>
                     <asp:Label ID="Label6" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsubtype"
                                                ErrorMessage="*" ValidationGroup="1" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </label>
                </td>
                <td style="width: 34%">
                    <asp:DropDownList ID="ddlsubtype" runat="server" >
                     
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                 </tr>   
            <tr>
                <td width="20%">
                    <label>
                    <asp:Label ID="Label12" runat="server" Text="Product Sub Sub Type: "></asp:Label>
                      <asp:Label ID="Label63" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" ControlToValidate="txtsubsub"
                                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </label>
                </td>
                <td style="width: 34%">
                    <asp:TextBox ID="txtsubsub" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    &nbsp;</td>
                </caption>
            </tr>
            <tr>
            
                <td width="20%">
                    <label>
                    <asp:Label ID="Label1" runat="server" Text="Active: "></asp:Label>
                    </label>
                </td>
                <td style="width: 34%">
                    <asp:CheckBox ID="chkboxActiveDeactive" runat="server" Text="Active" />
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                    </tr>
            <tr>
                <td width="20%">
                    <label>
                    <asp:Label ID="Label13" runat="server" Text="Inactive Date: "></asp:Label>
                    </label>
                </td>
                <td style="width: 34%">
                    <asp:TextBox ID="txtEndDate" runat="server" ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                        PopupButtonID="txtEndDate" TargetControlID="txtEndDate">
                    </cc1:CalendarExtender>
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td width="20%">
                    <label>
                    <asp:Label ID="Label14" runat="server" Text="Image: "></asp:Label>
                    </label>
                </td>
                <td style="width: 34%">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="190px" />
                    &nbsp;
                    <asp:Button ID="Btupload" runat="server" onclick="Btupload_Click" 
                        Text="Upload" />
                    <br />
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td style="width: 34%">
                    <asp:Image ID="Image1" runat="server" Style="height: 75px;  width: 99px;" />
                    <asp:Label ID="Label19" runat="server" Visible="False"></asp:Label>
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td style="width: 34%">
                    <asp:Button ID="Btnsave"   runat="server" ForeColor="Black" onclick="Btnsave_Click"   Text="Save"   ValidationGroup="1"    />
                    <asp:Button ID="Btnupdate" runat="server" ForeColor="Black" onclick="Btnupdate_Click" Text="Update" ValidationGroup="1"   />
                    <asp:Button ID="Btncancel" runat="server" ForeColor="Black" onclick="Btncancel_Click" Text="Cancel" />
                </td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
            </tr>
        </table>        
    </asp:Panel>
    </fieldset>
    <asp:Panel ID="Panel2" runat="server" GroupingText="List of product sub sub type">
        <table style="width: 100%">
            
                <tr>
                                    <td colspan="2">
                                         <label style="width:120px;">
                                                <asp:Label ID="Label2" runat="server" Text="Product Type: "></asp:Label>
                                        </label>
                                        <label style="width:180px;">
                                        <asp:DropDownList ID="ddlproducttypeFiltr" runat="server" Width="180px" AutoPostBack="True"   OnSelectedIndexChanged="ddlproductypefilter_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                        </label> 
                                        <label style="width:150px;">
                                            <asp:Label ID="Label4" runat="server" Text="Product Sub Type: "></asp:Label>
                                        </label>
                                         <label style="width:180px;">
                                         <asp:DropDownList ID="ddlsubtypefilter" runat="server" Width="180px" >                     
                                            </asp:DropDownList>
                                            </label>    
                                     
                                         <label style="width:100px;">
                                                          <asp:Label ID="Label3" runat="server" Text="Active: "></asp:Label>
                                     </label>


                                     <label style="width:180px;">
                                                <asp:DropDownList ID="ddlstatus" runat="server"  Width="181px" >
                                                  <asp:ListItem  Text="All" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                                 <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                 </label>
                                  <label style="width:60px;">
                                            <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                                </label> 
                                    </td>
                                    </tr>

            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                           EmptyDataText="No Record Found."       CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" 
                                                onpageindexchanging="GridView1_PageIndexChanging" PageSize="20" AllowPaging="True">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="Product Sub Sub Type Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="subname" HeaderText="Product Sub Type Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="InactiveDate" HeaderText="Inactive Date"  HeaderStyle-HorizontalAlign="Left"
                                                        DataFormatString="{0:MM/dd/yyy}" />
                                                    <asp:BoundField DataField="ImageFileName" HeaderText="Image File Name" Visible="false"  />
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Edit" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                              
                                                                ImageUrl="~/Account/images/edit.gif" onclick="ImageButton3_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label19" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" ToolTip="Delete" onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                     </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </ContentTemplate>
     <Triggers>
 <asp:PostBackTrigger ControlID="Btupload" />
 </Triggers>
    </asp:UpdatePanel>
    <div style="position: fixed;bottom: 0; right:20px;" >
              <asp:Label ID="lblVersion" runat="server" ForeColor="#416271" Font-Size="10px"></asp:Label>
              </div>

</asp:Content>


