<%@ Page Title="" Language="C#"  MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductCodeDetailAddmanage.aspx.cs" Inherits="productcode_databaseaddmanage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
<style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
    
        TR.updated TD
        {
            background-color:yellow;
        }
        .modalBackground 
        {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
        }
    .pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div style="clear: both;">
    
                </div> 
     <fieldset>
     <div style="float: left;">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
                         <legend>
                        <asp:Label ID="Label19" runat="server" Text="Product Code Add Manage"></asp:Label>
                 </legend>         
                 <div style="float: right;">                    
                    <asp:Button ID="btn_addnew" runat="server" CssClass="btnSubmit" onclick="btnAddNewDAta_Click"  Text="Add New Codes" />                    
                </div>
     
                    <asp:Panel ID="Panel1" runat="server" Visible="false">

                        <table style="width: 100%">
                            <tr>
                                <td width="20%">
                                  <label>
                                     <asp:Label ID="lbl_serverid" runat="server" Text="" Visible="false"></asp:Label>
                                     <asp:Label ID="Label1" runat="server" Text="Select Products"></asp:Label>
                                    <asp:Label ID="Label51" runat="server" CssClass="labelstar" Text="*"></asp:Label>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFsdfsdfieldValidator34" runat="server" ControlToValidate="ddlproductversion" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>                                     
                                    </label>
                                </td>
                                <td width="50%">
                                <label>
                                    <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="200px" >
                                    </asp:DropDownList>                                   
                                    </label> 
                                    
                                     <label style="width:150px;"></label> 
                                    <label>
                                        <asp:Button ID="btn_showftp" Height="20px"   runat="server" Text="Show Ftp Details" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                             
                                                    <cc1:ModalPopupExtender   BackgroundCssClass="modalBackground"  ID="modal1" CancelControlID="btnCancelPopup" runat="server" TargetControlID="btn_showftp" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopup" Height="200px" Width="400px" runat="server" CssClass="pnlBackGround">                                                       
                                                                  <legend>
                                                                    <asp:Label ID="Label10f" runat="server" Text="FTP Detail"></asp:Label>
                                                                </legend>     
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                             <asp:Label ID="lblMsgPopup" runat="server" Text="FTP server name"></asp:Label>
                                                                             </label> 
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="ftpservename" runat="server" Text=""></asp:Label>
                                                                            </label> 
                                                                        </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td>
                                                                            <label>
                                                                             <asp:Label ID="Label10s" runat="server" Text="FTP user name"></asp:Label>
                                                                             </label> 
                                                                        </td>
                                                                        <td>
                                                                        <label>
                                                                            <asp:Label ID="ftpuser" runat="server" Text=""></asp:Label>
                                                                            </label> 
                                                                        </td>
                                                                    </tr>
                                                                </table>                   
                                                        <br /><br />
                                                        <center><asp:Button ID="btnCancelPopup" runat="server" Text="Close" /></center>
                                             </asp:Panel>
                                             
                                    </label> 
                                </td>                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                         <asp:Label ID="Label11" runat="server" Text="Products Description"></asp:Label>
                                     </label>
                                </td>
                                <td colspan="2">
                                    <label>                                                                  
                                        <asp:TextBox ID="txt_prod_desc" TextMode="MultiLine" Width="600px" 
                                        Height="45px" runat="server" Enabled="False" BackColor="White" 
                                        BorderColor="White" BorderStyle="None" ForeColor="Black"></asp:TextBox>
                                        <asp:DropDownList ID="ddlcodetypecategory" runat="server" Visible="false"  Enabled="false" onselectedindexchanged="ddlcodetypecategory_SelectedIndexChanged"  AutoPostBack="True" Width="200px">
                                         </asp:DropDownList>
                                    </label> 
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan="3">
                                     <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                     <asp:Panel ID="Pnl_addcode" runat="server" Visible="true" Width="100%">
                                                     
                                                       
                                        <fieldset>
                                        <legend>Add product code                                            
                                        </legend>                                             
                                                 <table width="100%">
                                                  <tr>
                                                        <td style="width:30%">
                                                            <label>
                                                                 <asp:Label ID="Label17111awebsite" runat="server" Text="Select the websites related to this code"></asp:Label>
                                                                 <asp:Label ID="Label1811awebstar" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLWebsiteC" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                                             </label>
                                                            </td>
                                                            <td style="width: 70%">
                                                                <label>
                                                                    <asp:DropDownList ID="DDLWebsiteC" runat="server" Width="250px" AutoPostBack="True" onselectedindexchanged="DDLWebsiteC_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Code Name"></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodetypename" ValidationGroup="1"  ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                                </label> 
                                                            </td>
                                                            <td width="70%">                              
                                                             <asp:TextBox ID="txtcodetypename" runat="server"></asp:TextBox>
                                                            </td>  
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                     <label>
                                                                            <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                                                                            </label>
                                                            </td>
                                                            <td>
                                                                    <label>
                                                                         <asp:CheckBox ID="Chk_addactive" runat="server" Checked="true" Text="Active"  />
                                                                    </label> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                    
                                                            </td>
                                                            <td>
                                                                           <asp:CheckBox ID="CheckBox1" runat="server" Text="Is it Product's Default Code / Website?"  AutoPostBack="True"/>                
                                                            </td>
                                                        </tr>
                                                        
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>                                                               
                                                            <asp:CheckBox ID="Chk_CodeOp1" runat="server" Text="This code is already attached in IIS" oncheckedchanged="Chk_CodeOp1_CheckedChanged"   AutoPostBack="True"/>
                                                            </label> 
                                                            <label>                                                               
                                                             <asp:CheckBox ID="Chk_CodeOp2" runat="server" Text="Attach this code to IIS" oncheckedchanged="Chk_CodeOp2_CheckedChanged"   AutoPostBack="True"/>
                                                            </label> 
                                                            <label>                                                                
                                                            <asp:CheckBox ID="Chk_CodeOp3" runat="server" Text="This code is offline" oncheckedchanged="Chk_CodeOp3_CheckedChanged"  AutoPostBack="True" ToolTip="Select this option if the product is finalised  and there is no development work going on and no update of the code/new version of code will be required to be created in the near future." />
                                                            </label> 
                                                        </td>                                                        
                                                    </tr>
                                                   </table>
                                                <asp:Panel ID="pnl_selectradio" runat="server" Visible="false">
                                                   <table width="100%" >                                             
                                                    <tr>
                                                        <td colspan="2" style="width:100%;background-color:#f2f2f2;">
                                                                     <label>
                                                                                        Server Name:
                                                                                        </label> 
                                                                                        <label>
                                                                                            <asp:Label ID="lbl_severname" runat="server" Text="Code Name"></asp:Label>
                                                                                        </label>                                
                                                                                        <label>
                                                                                             Public IP:
                                                                                        </label> 
                                                                                         <label>
                                                                                            <asp:Label ID="lbl_pubIP" runat="server" Text="Code Name"></asp:Label>
                                                                                        </label> 
                                                                                         <label>
                                                                                             Private IP: 
                                                                                        </label> 
                                                                                        <label>
                                                                                                <asp:Label ID="lbl_priIP" runat="server" Text="Code Name"></asp:Label>
                                                                                        </label> 
                                                                                         <label>
                                                                                                     <asp:Button ID="Button5" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                                          </label> 
                                                                                        <label>
                                                                                         <asp:Label ID="lbl_serverconnectioncheck" runat="server" Text=""></asp:Label>
                                                                                             <%--<asp:LinkButton ID="lbl_serverconnectioncheck"  runat="server"  Text="Test Connection "  OnClick="lbl_serverconnectioncheck_Click" ></asp:LinkButton>--%>
                                                                                        </label>  
                                                        </td>
                                                      
                                                            </tr>
                                                    <tr>
                                                            <td valign="top">
                                                            <label>                                                                
                                                                 <asp:Label ID="lbl_sourrcepathh" runat="server" Text="Recommended source path folder name"></asp:Label>
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <asp:TextBox ID="txtmastercodepathC"  runat="server" Width="500px" Enabled="true"></asp:TextBox>
                                                                </label> 
                                                                <label>
                                                                    <asp:Button ID="btn_testt" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                </label> 
                                                                  <label>
                                                                    <asp:Label ID="lbl_mastercodepath" runat="server" Text=""  ForeColor="Green"></asp:Label>
                                                                 </label>  
                                                            </td>
                                                        </tr>
                                                    <tr>
                                                          <asp:Panel ID="pnl_chkoption1" runat="server" Visible="false">
                                                            <td>                                                           
                                                            <label>
                                                                
                                                                <asp:Label ID="lbl_existingwebtext" runat="server" Text="Source path folder name(existing)"></asp:Label>
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                <label>
                                                               <asp:TextBox ID="txt_existingwebsite"  runat="server" Width="500px" Enabled="true"></asp:TextBox>                                                                
                                                               </label> 
                                                            </td>
                                                            </asp:Panel>
                                                        </tr> 
                                                    <tr>
                                                     <asp:Panel ID="pnl_chkoption2" runat="server" Visible="false">
                                                        <td valign="top">
                                                           <label>
                                                              Upload website code to the server                                                                 
                                                              </label>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <asp:FileUpload ID="fu_mastercode" runat="server" />
                                                            </label> 
                                                            <label>                                                            
                                                                <asp:Button ID="btn_upload_mastercode" runat="server" OnClick="btn_upload_mastercode_Click" Text="Upload" ValidationGroup="1" CssClass="btnSubmit" TabIndex="8" />
                                                            </label> 
                                                            <label>
                                                                    <asp:Label ID="lbl_fileupload" runat="server" ForeColor="Green" Font-Size="12px"></asp:Label>
                                                                    <asp:Label ID="lblfilename" runat="server" Visible="false" Font-Size="12px"></asp:Label>
                                                            </label>                                                             
                                                            <br />
                                                            <label style="font-size:10px;">
                                                                Select the folder (All sub folders and files of this folder will be zipped and uploaded to server as the code of the website)
                                                            </label>                                                                                                                   
                                                        </td>
                                                         </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                            <td>
                                                            <label>
                                                                Website URL
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                <label>
                                                                 <asp:Label ID="lbl_websiteurl" runat="server" Text=""></asp:Label>
                                                                   </label>  
                                                                 <label>
                                                                    <asp:Label ID="lbl_weburlconnection" runat="server" Text=""  ForeColor="Green"></asp:Label>
                                                                 </label>   
                                                            </td>
                                                        </tr>
                                                    <tr>
                                                        <td valign="top" style="width:30%">
                                                            <label>
                                                                <asp:Label ID="lbltemppathcodeC" runat="server" Text="Temp path For code compilation"></asp:Label> 
                                                            </label>
                                                        </td>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:TextBox ID="txttemppathC" runat="server" Enabled="true" Width="500px"></asp:TextBox>
                                                            </label>
                                                             <label>
                                                                    <asp:Button ID="Button1" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                </label> 
                                                             <label>
                                                                    <asp:Label ID="lbltemppathtest" runat="server" Text=""  ForeColor="Green"></asp:Label>
                                                                 </label>   
                                                            <label style="font-size:10px;">
                                                                <%--Server default path for master code\companyid\product name\website name\Test path--%>
                                                                Server default path for Master code\Companyid\Product name\Website name\Test path
                                                            </label> 
                                                        </td>
                                                    </tr>                            
                                                    <tr>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:Label ID="lbloutputpathcodeC" runat="server" Text="Output path folder name"></asp:Label>  
                                                            </label>
                                                        </td>
                                                        <td valign="top">
                                                            <label>
                                                                <asp:TextBox ID="txtoutputsourcepathC" runat="server" Width="500px" Enabled="true"></asp:TextBox>                                                                
                                                            </label>
                                                             <label>
                                                                    <asp:Button ID="Button2" Height="20px"   runat="server" Text="Test Connection" OnClick="btn_test_Click" CssClass="btnSubmit" />
                                                                </label> 
                                                             <label>
                                                                    <asp:Label ID="lbloupputpathtest" runat="server" Text=""  ForeColor="Green"></asp:Label>
                                                                 </label>   
                                                            <label style="font-size:10px;">
                                                                <%--Server default path for master code\companyid\product name\website name\Master code--%>
                                                                Server default path for Master code\Companyid\Product name\Website name\Master code
                                                            </label>                                   
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                           <div style="font-size: large; font-weight:500">
                                                            Synchronisation of website detail with existing system tables
                                                            </div>
                                                            <label>  
                                                            Would you like to Add/Synchronise Folder, Subfolder, MasterPage details and page details to 'PAGEMASTER' table of system(recommended)  ?
                                                           </label>
                                                           <asp:CheckBox ID="chk_insertpagemaster" runat="server"  Text="Yes"  />
                                                        </td> 
                                                     </tr>
                                                      <asp:Panel ID="Panel5" runat="server" Visible="false">       
                                                    <tr>
                                                                <td colspan="3">
                                                                <div style="font-size: large; font-weight:500">
                                                                     Rules for creating new version of the code
                                                                    </div>
                                                                <label style="width:700px;">
                                                                            <asp:Label ID="Label37C" runat="server" Text="Select the folder not  to be included when publishing  new version of the code "></asp:Label>
                                                                        </label>                                                                        
                                                                </td>
                                                                </tr>
                                                    <tr>
                                                                            <td style="width:20%">
                                    
                                                                            </td>                                
                                                                            <td>
                                                                            <div style="font-size: large; font-size:10px;">
                                                                                <asp:Label ID="Label38C" runat="server" Text="(For Eg. Developer's version pages fodlers etc)"></asp:Label>                                                                           
                                                                            </div>
                                                                             <label style="width:500px;">
                                                                                    <asp:TextBox ID="txt_removablefolderC" runat="server" Width="480px"></asp:TextBox>
                                        
                                                                                </label>
                                                                                <label style="width:40px;">
                                                                                   <asp:Button ID="btn_addremovablefolderC"  runat="server" Text="Add" OnClick="btn_addremovablefolderC_Click" />
                                                                                </label>
                                                                                <label>
                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="\\Developers" Width="20px" />
                                                                                </label> 
                                                                            </td>
                                                                            <td>
                               
                                                                            </td>
                                                                        </tr>
                                                    <tr>
                                                                            <td style="width:20%">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <label style="width:500px;">
                                                                                    <asp:ListBox ID="lBox_removablefolderC" runat="server" Width="484px" Visible="false" ></asp:ListBox>
                                                                                </label>
                                    
                                                                               <label style="width:50px;">
                                                                               <asp:Button ID="btn_addremovefolderC" runat="server" Text="Remove" CssClass="btnSubmit" OnClick="btn_addremovefolderC_Click" Visible="false"  />
                                                                               </label>
                                                                                <label style="width:40px;">
                                                                                 <asp:Button ID="btnEditremovefolderC" runat="server" Text="Edit" CssClass="btnSubmit" OnClick="btnEditremovefolderC_Click"  Visible="false" />
                                                                                </label>
                                                                            </td>
                                                                        </tr> 
                                                    <tr>
                                                                <td colspan="3">
                                                                <label style="width:700px;">
                                                                            <asp:Label ID="Label13CC" runat="server" Text="Select folder whose subfolders and files are to be deleted before publishing new version of the  code"></asp:Label>
                                                                       
                                                                         <Div style="width:1100px;font-size:10px;">
                                                                                <asp:Label ID="Label14CC" runat="server" Text="(For Eg. Documents , user images etc which are put by the developers in testing should not be passed on in the mastercode to be destributed to the custoemrs)"></asp:Label>
                                                                        </Div>
                                                                        </label>
                                                                        
                                                                </td>
                                                                </tr>
                                                    <tr>
                                            <td>
                                                <label>
                                                   <asp:Panel ID="pnl_websiter1" runat="server" Visible="true">                          
                                                    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                         <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                               <asp:Panel ID="pnlpr" runat="server" Width="400px"  ScrollBars="Horizontal" Height="200px">
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                                                                        OnRowCommand="GridView2_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                        Width="100%" OnPageIndexChanging="GridView2_PageIndexChanging" OnSorting="GridView2_Sorting"
                                                                         OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="ALL" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />
                                                                
                                                                                    <%--Checked='<%# Bind("chk") %>'--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Folder Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left"
                                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="67%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblp" runat="server" Text='<%# Bind("foldername") %>'></asp:Label>
                                                                                    <asp:Label ID="lblwebid" runat="server" Text='<%# Bind("websiteid") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>                                                                
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                 </label> 
                                            </td>
                                            </tr>   
                                                    <tr>
                                                                            <td style="width:20%">
                                    
                                                                            </td>                                
                                                                            <td>
                                                                             <label style="width:500px;">
                                                                                    <asp:TextBox ID="txtADDdeletefolderC" runat="server" Width="480px"></asp:TextBox>
                                        
                                                                                </label>
                                                                                <label style="width:40px;">
                                                                                   <asp:Button ID="btn_ADDdeletefolderC"  runat="server" Text="Add" OnClick="btn_ADDdeletefolderC_Click" />
                                                                                </label>
                                                                                <label>
                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/HelpIcon.png" ToolTip="\\Attach OR \\Shoppingcart\\Admin\\Attach" Width="20px" />
                                                                                </label> 
                                                                            </td>
                                                                            <td>
                               
                                                                            </td>
                                                                        </tr>
                                                    <tr>
                                                                            <td style="width:20%">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <label style="width:500px;">
                                                                                    <asp:ListBox ID="lst_box_deletefolderC" runat="server" Width="484px" Visible="false" ></asp:ListBox>
                                                                                </label>
                                    
                                                                               <label style="width:50px;">
                                                                               <asp:Button ID="btn_RemovedeletefolderC" runat="server" Text="Remove" CssClass="btnSubmit" OnClick="btn_RemovedeletefolderC_Click" Visible="false" />
                                                                               </label>
                                                                                <label style="width:40px;">
                                                                                 <asp:Button ID="btn_editdeletefolderC" runat="server" Text="Edit" CssClass="btnSubmit" OnClick="btn_editdeletefolderC_Click" Visible="False" />
                                                                                </label>
                                                                            </td>
                                                                        </tr>  
                                                    </asp:Panel>
                                                  </table>  
                                                </asp:Panel>  
                                                        
                                                    
                                                  
                                             </fieldset> 
                                             </asp:Panel>
                                                    </td>
                                                </tr>
                                               
                                            </table> 
                                </td>
                            </tr>
                            <tr>
                                <td width="22%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="btn_submitCode" runat="server"  CssClass="btnSubmit" onclick="btn_submitCode_Click" Text="Submit" ValidationGroup="1" />
                                    <asp:Button ID="btn_updateCode" runat="server"  CssClass="btnSubmit" onclick="btn_updateCode_Click" Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="BtnCancel_Click" />
                                </td>
                               
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                           <%-- <cc1:ModalPopupExtender ID="modal11"   BackgroundCssClass="modalBackground"  CancelControlID="btnCancelPopup1" runat="server" TargetControlID="btn_submitCode" PopupControlID="pnlPopup1"></cc1:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopup1" Height="150px" Width="600px" runat="server" CssClass="pnlBackGround" Visible="false">
                                        <asp:Label ID="lblMsgPopup1" runat="server" Text="The physical path of the website is different than the recommended path for the website. It will be moved to recommended path"></asp:Label><br /><br />
                                        <center>
                                        <label>
                                        <asp:Button ID="btnCancelPopup1" runat="server" Text="Confirm"  />    
                                        </label> 
                                        <label>
                                         <asp:Button ID="Button1" runat="server" Text="Cancel" onclick="btn_pnlclose_Click"  />                                       
                                         </label> 
                                        </center>
                                    </asp:Panel>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
              </fieldset>
              <fieldset>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
            <table>
                    
                    <tr>
                        <td>
                                <label>  
                                    <asp:Label ID="Label9" runat="server" Text="(Is this the code where the unique subscriber files will be inserted in copy of master code to make the code unique for the subscriber?)"></asp:Label>
                                </label>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox11" runat="server" Text="Is it Busicontroller Database?" AutoPostBack="True" oncheckedchanged="CheckBox11_CheckedChanged"/>       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox12" runat="server" Text="Is it Company Default Database?" AutoPostBack="True" oncheckedchanged="CheckBox12_CheckedChanged"/>           
                                                </td>
                                            </tr>
                                            
                                            
                                            
                                        </table>                      
                                    </asp:Panel> 
            </table> 
            </asp:Panel>

                    <asp:Panel ID="Panel4" runat="server" GroupingText="List Of Product Code">
                        <table style="width: 100%">   
                        <tr>
                                <td width="25%">
                                     <label>
                                        <asp:CheckBox ID="chk_Active" runat="server" Checked="true" Text="Show active filters only" AutoPostBack="True" oncheckedchanged="chk_Active_CheckedChanged"/>
                                    </label> 
                                    </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>                        
                            <tr>
                                <td colspan="4">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Product Name"></asp:Label>
                                    </label> 
                              <label>
                                    <asp:DropDownList ID="ddlProductname" runat="server" AutoPostBack="True" DataTextField="ProductName" DataValueField="ProductId" Width="200px" OnSelectedIndexChanged="ddlProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                               </label> 
                                    <label>
                                     <asp:Label ID="Label52" runat="server" Text="Category Type "></asp:Label>
                                     </label>
                                     <label>                               
                                    <asp:DropDownList ID="ddlcodetypecategory0" runat="server" Enabled="false"  onselectedindexchanged="ddlcodetypecategory0_SelectedIndexChanged"  AutoPostBack="True">
                                    </asp:DropDownList>
                                    </label> 
                                    <label>
                                     <asp:Label ID="Label12" runat="server" Text="Status "></asp:Label>
                                     </label>
                                    <label>
                                           <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" Width="110px"   OnSelectedIndexChanged="ddlcodetypecategory0_SelectedIndexChanged">
                                            <asp:ListItem Text="All" ></asp:ListItem>
                                            <asp:ListItem Text="Active" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Inactive"></asp:ListItem>
                                         </asp:DropDownList>
                                    </label>                                   
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" 
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Record Found." PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name" ItemStyle-Width="20%" SortExpression="VersionInfoName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                      <asp:Label ID="Label7" Visible="false"  runat="server" Text='<%#Bind("vv") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Website Name" ItemStyle-Width="20%" SortExpression="WebsiteName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWebsiteName" runat="server" Text='<%#Bind("WebsiteName") %>'></asp:Label>   
                                                     <asp:Label ID="lblcodetypecategory" runat="server" Text='<%#Bind("CodeTypeCategory") %>' Visible="false" ></asp:Label>                                                   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>                                            
                                           
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Name" ItemStyle-Width="20%" SortExpression="CodeTypeName" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Source Path Folder Name" ItemStyle-Width="40%" SortExpression="FileLocationPath" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaths" runat="server" Text='<%#Bind("FileLocationPath") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Default Application Code?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label53" runat="server" Text='<%#Bind("AdditionalPageInserted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code Name" ItemStyle-Width="25%" SortExpression="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodetype" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>--%>
                                            <%--
                                            <asp:TemplateField HeaderText="Busiwiz Database?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label54" runat="server" Text='<%#Bind("BusiwizSynchronization") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Default Application Database?">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label55" runat="server" Text='<%#Bind("CompanyDefaultData") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%--  <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActive" runat="server" Text='<%#Bind("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif" onclick="ImgBtn_EditGrig"/>
                                                </ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                         <asp:Label ID="Label19" runat="server"  Text='<%#Eval("ID") %>' Visible="False"></asp:Label>
                                                            <asp:ImageButton ID="imgdelete" runat="server" ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete" onclick="imgdelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4">
                                  <asp:Panel ID="pnlfolder" runat="server"  Visible="false" width="750px">
                                     <div style="position: absolute; margin:-750px 0px 0px 280px; width:750px; background-color: #CECECE;" class="Box" >
                                         <fieldset>                 
                                          <legend>
                                            <asp:Label ID="Label2" runat="server" Text="Syncronice folder and pages with database"></asp:Label>   
                                              <asp:Label ID="lbl_databaseid" Visible="false" runat="server" Text=""></asp:Label>            
                                         </legend>
                                         <table width="100%">
                                         <tr>
                                         <td align="right">
                                          <asp:Button ID="Button52" runat="server" Text=" X "   style="color: #FFFFFF; background-color: #FF0000; height: 26px;"  onclick="Btn_pnlbackupstatus_Click" />
                                         </td>
                                         </tr>
                                         <tr>
                                                <td>
                                                 <label style="width:100%;">
                                                    <asp:Label ID="Label3" runat="server" Text="Select the folder not  to be included when publishing  new version of the code"></asp:Label>
                                                    <asp:Label ID="lbl_codetypeid" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_servername" runat="server" Text="" Height="24px" style="color: #FFFFFF"></asp:Label>
                                                    </label>
                            
                                            </td>
                                         </tr>
                                         <tr>
                                              <td colspan="4">
                                                      <%--Grid--%>
                                                      <input id="Hidden1" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="Hidden2" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                            <input runat="server" id="Hidden3" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                                <asp:Panel ID="Panel6" runat="server" ScrollBars="Horizontal" Height="400px">
                                                                    <asp:GridView ID="gv_webfolder" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                                                   Width="500px" OnRowDataBound="gv_webfolder_RowDataBound"    AlternatingRowStyle-CssClass="alt" 
                                                                        OnRowCommand="gv_webfolder_RowCommand" EmptyDataText="There is no data." AllowSorting="True"                                                                        
                                                                            OnPageIndexChanging="gv_webfolder_PageIndexChanging" OnSorting="gv_webfolder_Sorting"
                                                                            OnSelectedIndexChanged="gv_webfolder_SelectedIndexChanged" AllowPaging="false" PageSize="25">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  >
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" OnCheckedChanged="ch1table_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="ALL" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Folder Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="67%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblfoldername" runat="server" Text='<%# Bind("foldername") %>'></asp:Label>
                                                                                    <asp:Label ID="lblfolderid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>                                                                                                                                               
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <asp:Button ID="btnprevious" runat="server"  CssClass="btnSubmit" onclick="btnprevious_Click" Text="Previous"  />  
                                                                <asp:Button ID="btnnext" runat="server"  CssClass="btnSubmit" onclick="btnnext_Click" Text="Next"  Visible="false"  />              
                                                                <asp:Button ID="btn_finish" runat="server"  CssClass="btnSubmit" onclick="btn_finish_Click" Text="Finish"  /> 
                                                                 <asp:Label ID="lblfinishmsg" runat="server" ForeColor="Red" Visible="false"
                                                          Text="After finish cick wait 5-10 minite we are setup website" 
                                                          style="font-size: xx-small"></asp:Label>

                                                                                                                
                                               </td>
                                           </tr>



                                  
                           
                               
                                          
                                 </table> 
            
     
                    </asp:Panel>
                                         <%-- <asp:ImageButton ID="ImageButton5" runat="server" Visible="false" ToolTip="" />                     
                                          <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnladdress" TargetControlID="ImageButton5" X="150" Y="100" CancelControlID="ibtnCancelCabinetAdd">
                                            </cc1:ModalPopupExtender>--%>
                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
               
                    <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                    <fieldset>
                     <div>
                         <asp:Label ID="Label6" runat="server" ForeColor="Black"></asp:Label>
                     </div>
                     <div>
                     </div>
                     <div align="center">
                         <asp:Button ID="Button3" runat="server" Text="Yes" onclick="Button3_Click" />
                         <asp:Button ID="Button4" runat="server" Text="Cancel" onclick="Button4_Click" />
                     </div>
                     </fieldset>
                     </asp:Panel>
                       <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Paneldoc" TargetControlID="btnreh" >
                                </cc1:ModalPopupExtender> 
                                </fieldset> 
                             
    </ContentTemplate>
     <Triggers>          
            <asp:PostBackTrigger ControlID="btn_upload_mastercode" />
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

