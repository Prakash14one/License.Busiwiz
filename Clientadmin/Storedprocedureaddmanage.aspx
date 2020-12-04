<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="Storedprocedureaddmanage.aspx.cs" Inherits="procedureadd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

 <ContentTemplate>
         <div style="margin-left:1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>


            <fieldset><legend>Store Procedure Add</legend>
                <div style="float: right;">
             <asp:Button ID="btn_addnewreco" runat="server" Text="Procedure Add / Manage"  CssClass="btnSubmit" onclick="addnewpanel_Click" />
          </div>
            <asp:Panel ID="pnl_addPROCEDURE" runat="server">
                   <table style="width: 100%">
                    <tr>
                        <td width="25%">
                            <label>
                              <asp:Label ID="lbl_serverid" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                            </label>
                            </td>  

                            <td width="75%">
                           <label>
                                <asp:DropDownList ID="ddlProductname"  runat="server" AutoPostBack="True" Width="300px" onselectedindexchanged="ddlProductname_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            </td>
                    </tr>
                    
                     <tr>
                                <td>
                                    <label>
                                     <asp:Label ID="Label15" runat="server" Text="Select Database"></asp:Label> 
                                     </label> 
                                </td>
                                <td>
                                      <asp:DropDownList ID="ddlcodetype" runat="server" AutoPostBack="True" Width="300px" onselectedindexchanged="ddlcodetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                </td>
                            </tr>

                      <tr>
                          <td >
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="Procedure Name"></asp:Label>
                            </label></td>
                         <td >
                            <label>
                                    <asp:TextBox ID="txtname" runat="server" Width="500px" AutoPostBack="True" ontextchanged="txtname_TextChanged"></asp:TextBox>
                            </label>
                            <label>
                                <asp:Label ID="lbl_errortxtname" runat="server" Text="" CssClass="labelstar"></asp:Label>
                            </label> 
                             
                            </td>
                    </tr>
                    <tr>
                        <td>
                        
                        </td>
                        <td>
                        <asp:Panel ID="pnl_showgidproc" runat="server" Height="200px" ScrollBars="Vertical" Visible="false">
                            <asp:GridView ID="gv_selectprocedure" runat="server" Width="80%" AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" 
                                            onselectedindexchanged="gv_selectprocedure_SelectedIndexChanged" >
                                <Columns> 
                                    <asp:TemplateField HeaderText="Procedures" SortExpression="name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("name") %>' ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Select " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnk21" runat="server" ForeColor="Black" Text="Select" onclick="lnk_selectstoreproce_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>                                   
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                            </asp:Panel>
                            <br />
                            <asp:Label ID="lbl_ie" runat="server"  Text="Instructions to add store procedure"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                         <td >
                           <label>
                                <asp:Label ID="Label3" runat="server" Text="Procedure Type"></asp:Label>
                            </label>
                            </td>
                             <td>
                            <label>
                             <asp:DropDownList ID="ddltype" Width="200px" runat="server" AutoPostBack="True" >
                                        <asp:ListItem Text="Insert-Select-Delete-Update" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Insert" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Select" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Delete" Value="4"></asp:ListItem>
                                         <asp:ListItem Text="Update" Value="5"></asp:ListItem>                                       
                                </asp:DropDownList>
                                </label> 
                                </td>
                    </tr>
                     <tr>
                          <td valign="top">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Procedure Code"></asp:Label>
                                </label>
                               
                                      <asp:Panel ID="pnl_ieproced" runat="server" Width="60%" CssClass="modalPopup">                                                                    
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closeicon.jpeg" Width="16px" />
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <td>
                                                    <label>
                                                          <asp:Label ID="lbl_syntax" runat="server" Text="Please use following syntax :"></asp:Label>
                                                       <%-- //txtpricedure--%>
                                                    </label> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"> 
                                                            <asp:Image ID="Image11235" ImageUrl="~/images/Pages/EXofstoreprocedure.png"  runat="server" />    
                                                          <asp:TextBox ID="txtpricedure" runat="server" TextMode="MultiLine" Width="100%"  Height="200px"></asp:TextBox>                                                        
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                <asp:Button ID="Button4" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnl_ieproced" TargetControlID="lbl_ie" CancelControlID="ImageButton1">
                                </cc1:ModalPopupExtender>

                                </label> 
                            </td>
                            <td valign="top">  
                                  <label style="Width:100%;">
                                    <asp:TextBox ID="txtprocedure" runat="server" TextMode="MultiLine" Width="100%"  Height="200px"></asp:TextBox>
                                    </label> 
                            </td>
                    </tr>    
                    <tr>
                          <td valign="top">
                                <label>
                                    
                                    <asp:CheckBox ID="chk_desc" runat="server" Text=" Do you wish to add description ?" AutoPostBack="True" OnCheckedChanged="chkimg_CheckedChanged" TextAlign="Left" />
                                </label>
                            </td>
                            <td valign="top">  
                                   <label>
                                        <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Visible="false" Width="350px" Height="70px"></asp:TextBox>                          
                                    </label> 
                            </td>                          
                    </tr>
                    
                           <tr>
                               <td>
                                   <label>
                                        <asp:Label ID="Label6" runat="server" Text="Which table are used for storeprocedure"></asp:Label>
                                   </label>
                               </td>
                               <td>
                                <label>
                                    Search Table
                                     <asp:TextBox ID="txtsearch" runat="server" ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                                 </label>
                                
                                 <label><br />
                                        <asp:Button ID="Button6" runat="server" Text="Go" onclick="Button6_Click" />
                                 </label>  
                                  <label><br />
                                            <asp:Button ID="Button7" runat="server" Text="Show All" onclick="Button7_Click" />
                                            </label> 
                               </td>
                           </tr>
                            <tr>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                            <asp:Button ID="Button1" runat="server" Text="Add Selected" onclick="Button1_Click1" Visible="false" />
                                                    </td>
                                               </tr>
                           <tr>
                               <td width="25%">
                               </td>
                               <td  width="100%">
                                   <asp:Panel ID="Panel51" runat="server">
                                       <asp:Panel ID="Panel101" runat="server" Height="147px" ScrollBars="Vertical">
                                            <table>
                                           
                                                <tr>
                                                    <td style="width:50%;"  valign="top">
                                                            <label style="width:100%;">
                                                    <asp:GridView ID="GridView5" runat="server" Width="100%"  AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" onselectedindexchanged="GridView5_SelectedIndexChanged" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText=" Table Name " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:LinkButton ID="lnk20" runat="server" ForeColor="Black" onclick="lnk20_Click" Text='<%#Eval("TableName") %>'></asp:LinkButton>
                                                                         <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Select " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                   
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                               </label>
                                                    </td>
                                                    <td style="width:50%;" valign="top">
                                                        <label style="width:100%;">
                                                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" onrowcommand="GridView1_RowCommand" DataKeyNames="id" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selected Table " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                     <asp:Label ID="lbid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>                           
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Account/images/delete.gif" HeaderText="Remove" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del">
                                                               <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:ButtonField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </asp:GridView>
                                            </label>
                                                    </td>
                                                </tr>
                                            </table> 
                                              
                                             
                                    </asp:Panel>
                                   </asp:Panel>
                                   
                               </td>
                           </tr>
                       </caption>




</table>
<br />

                               
                 <table style="width: 100%">
                    <tr>
                         <td width="25%" >
                            </td>
                         <td width="75%">
                            </td>
                    </tr>
                     <tr>
                         <td width="25%">
                          <label>
                                <asp:Label ID="Label7" runat="server" Text="Pages in which procedures to be used"></asp:Label>
                            </label>
                            </td>
                             <td width="75%">
                                <label>
                                Select Website for search
                                        <asp:DropDownList ID="ddlwebsite" runat="server" AutoPostBack="false" Width="200px" >
                                        </asp:DropDownList>
                                </label> 
                                
                                <label>
                                           Search Page   
                                            <asp:TextBox ID="txtpagesearch" runat="server" ontextchanged="txtpagesearch_TextChanged" AutoPostBack="True"></asp:TextBox>
                                 </label>
                                 <label><br />
                                        <asp:Button ID="Button10" runat="server" Text="Go" onclick="Button10_Click"  />
                                 </label> 
                                 <label><br />
                                        <asp:Button ID="Button11" runat="server" Text="Show All" onclick="Button11_Click" />     
                                 </label> 
                                 
                            </td>
                            </tr>                            
                            <tr>
                                <td>
                                
                                </td>
                                <td>
                                     <label>
                                            <asp:Button ID="Button2" runat="server" Text="Add Selected" onclick="Button2_Click1" Visible="false"  />
                                    </label> 
                                </td>
                            </tr>
                             <tr>
                             <td  width="25%"></td>
                          <td   width="75%" >
                           
                              <asp:Panel ID="Panel4" runat="server"  >
                                  <asp:Panel ID="Panel9" runat="server" Height="147px" ScrollBars="Vertical">
                                  
                            <label style="width:80%;">                                        
                           <asp:GridView ID="GridView6" runat="server" Width="80%" AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" onselectedindexchanged="GridView6_SelectedIndexChanged" >
                                <Columns>                                
                                <asp:TemplateField HeaderText="Website Name" SortExpression="WebsiteName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:Label ID="Label4" runat="server" Text='<%#Eval("WebsiteName") %>'></asp:Label>                                             
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Page Name " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lnk21" runat="server" ForeColor="Black" Text='<%#Eval("PageName") %>' onclick="lnk21_Click"></asp:LinkButton>
                                             <asp:Label ID="lbl_id" runat="server" Text='<%#Eval("PageId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Select " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>                                   
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                            </label>                            
                            </asp:Panel>
                            </asp:Panel>                           
                          </td>
                    </tr>                            
                            </table>                           
                          <table width="100%">                    
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridView2" runat="server" Width="50%" AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  AllowSorting="True" 
                                onselectedindexchanged="GridView2_SelectedIndexChanged" onrowcommand="GridView2_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Selected Page " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael11" runat="server" Text='<%# Eval("PageName") %>' Enabled="False"></asp:Label>
                                             <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                    </asp:TemplateField>                                   
                                      <asp:ButtonField ButtonType="Image"  ImageUrl="~/Account/images/delete.gif" HeaderText="Remove" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" CommandName="del">
                                               <HeaderStyle HorizontalAlign="Left" />
                                               <ItemStyle HorizontalAlign="Left" Width="2%" />
                                      </asp:ButtonField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </td>                    
                    </tr>


                    <tr>
                        <td>
                            
                            
                            
                        </td>
                    </tr>
                </table>
                <br />
                <div align="center">
                         <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="1" CssClass="btnSubmit" />                    
                         <asp:Button ID="Button12" runat="server" Text="Update" Visible="false" onclick="Button12_Click" CssClass="btnSubmit" />                    
                        <asp:Button ID="Button13" runat="server" Text="Cancel" onclick="Button13_Click" CssClass="btnSubmit" />                    
                    </div>
                      </asp:Panel>
         </fieldset>

                    <br />
                    <fieldset>
                    <legend>List of Stored Procedure </legend>                  
                     <asp:Panel ID="Panel1" runat="server">
                       <table style="width: 100%">
                      
                    <tr>
                         <td>
                            <label>
                                    <asp:Label ID="Label22" runat="server" Text="Product"></asp:Label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="300px" AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                            </label> 
                                <label>
                                        <asp:Label ID="Label23" runat="server" Text="Database Name"></asp:Label>
                                        <asp:DropDownList ID="ddlcodetypesearch" runat="server"  Width="200px" AutoPostBack="True" onselectedindexchanged="ddlcodetypesearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label> 
                                     <label style="vertical-align:bottom;">
                                                     <asp:Image ID="img_dbconn" runat="server" Width="30px" Visible="false" ImageUrl="" />
                                                     </label>                             
                                      <label>
                                         <asp:Label ID="Label8" runat="server" Text="Search"></asp:Label>
                                        <asp:TextBox ID="txt_searchname" runat="server" AutoPostBack="True" ontextchanged="txt_searchname_TextChanged"></asp:TextBox>
                                    </label>   
                                     <label>
                                     <br />
                                            <asp:Button ID="BtnGo" CssClass="btnSubmit" runat="server" Text="Go" OnClick="BtnGo_Click"  />
                                </label>                         
                          </td>                        
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Label ID="Lbl_text1" runat="server" style="color:#d5a26d;"></asp:Label>
                                <asp:Label ID="Lbl_text2" runat="server" style="color:#d5a26d;"></asp:Label>
                                <asp:Label ID="Lbl_text3" runat="server" style="color:#d5a26d;"></asp:Label>
                                <asp:LinkButton ID="lnk_syncdb"  OnClick="btnsynTables" style="background-color:#d5a26d;color:#FFF;" runat="server" Text=" Yes" ></asp:LinkButton>
                                 <asp:Label ID="Label9" runat="server" style="color:#d5a26d;"></asp:Label>
                                    <asp:Panel ID="Panel2" runat="server" Width="50%" CssClass="modalPopup">                                                                    
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                 <label>
                                                       Store Procedures that exist at the SQL Server related to that table
                                                    </label> 
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/closeicon.jpeg" Width="16px" />
                                                </td>
                                            </tr>                                          
                                           
                                            <tr>
                                                <td colspan="2"> 
                                                  <asp:Panel ID="Panel3" runat="server" ScrollBars="Horizontal" Height="250px" >            
                                                        <asp:GridView ID="GridView4" runat="server" Width="80%" AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" 
                                                              onselectedindexchanged="GridView4_SelectedIndexChanged" >
                                                    <Columns> 
                                                        <asp:TemplateField HeaderText="Procedures" SortExpression="name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("name") %>' ></asp:Label>
                                                                    <asp:Label ID="lbl_code" runat="server" Text='<%#Eval("procedurecode") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="View " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                              <asp:LinkButton ID="lnkView" runat="server" ForeColor="Black" Text="View" onclick="lnk_viewselectstoreproce_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                             <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>                                   
                                                         <asp:TemplateField HeaderText="Import " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                              <asp:LinkButton ID="lnk21" runat="server" ForeColor="Black" Text="Import" onclick="lnk_selectstoreproce_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                             <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>                                   
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>   
                                                </asp:Panel>                                                
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                <asp:Button ID="Button3" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" 
                                PopupControlID="Panel2" TargetControlID="Button3" CancelControlID="ImageButton2">
                                </cc1:ModalPopupExtender>



                            </label> 
                        </td>
                    </tr>
                    </table>

                     <asp:GridView ID="GridView3" runat="server" Width="100%" OnRowCommand="GridView3_RowCommand"  
                             AutoGenerateColumns="False" EmptyDataText="No Record Found." CssClass="mGrid" PagerStyle-CssClass="pgr" 
                             AlternatingRowStyle-CssClass="alt"  AllowPaging="True"  AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"  
                             PageSize="10" onselectedindexchanged="GridView3_SelectedIndexChanged" DataKeyNames="id" >
                                <Columns>
                                      <asp:TemplateField HeaderText="Database " SortExpression="Database Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_database" runat="server" Text='<%# Eval("CodeTypeName") %>'></asp:Label>                                    
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael31" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                            <%-- <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Stored Procedure Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Name") %>' EnableTheming="False" ForeColor="Black" onclick="LinkButton1_Click" ></asp:LinkButton>
                                            <asp:Label ID="Lael32" runat="server" Text='<%# Eval("id") %>' Visible="false" ></asp:Label>                                           
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date created " SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael33" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy}") %>'  ></asp:Label>                                             
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="User created" SortExpression="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Lael34" runat="server" Text='<%# Eval("employeename") %>'></asp:Label>                                             
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                          <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit1" CommandArgument='<%# Eval("id") %>' ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonDel" runat="server" CommandName="Delete" CommandArgument='<%# Eval("id") %>' ToolTip="Delete" ImageUrl="~/Account/images/delete.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
 </asp:Panel>
</fieldset>

    
    
  




     <asp:Panel ID="Paneldoc" runat="server" Width="60%" CssClass="modalPopup">
     <%--CssClass="modalPopup"--%>
                                    <fieldset>                                      
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 95%;">
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/closeicon.jpeg" Width="16px" />
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rb_option" runat="server" style="font-size:14px">
                                                                    <asp:ListItem Value="0" Text="Add new Store procedure to the grid and database<br>(Select this option only if there does not exist any store procedure by this name in database)"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Update store procedure to the grid for an existing store procedure at database" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btn_selecoption" runat="server" CssClass="btnSubmit" Text="OK" OnClick="btn_selecoption_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground" PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>

</ContentTemplate>

</asp:UpdatePanel>
</asp:Content>

