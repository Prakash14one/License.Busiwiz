<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="ProductCodeVersionUpdate.aspx.cs" Inherits="Publish_Uncompiled_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }




        }

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }

        function check(txt1, regex, reg, id, max_len) {

            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }

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
    <asp:UpdatePanel ID="pnlid" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Font-Size="16px"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="" ForeColor="Red" Font-Size="16px"></asp:Label><br />
                <asp:Button ID="btn_syncser" Visible="false" runat="server" Text="Syncronice last record" CssClass="btnSubmit" OnClick="btn_syncser_Click" />
                <%--<asp:Label ID="lblstep1" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep2" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep3" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep4" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep5" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep6" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep7" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep8" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep9" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep10" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep11" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep12" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label><br />
                <asp:Label ID="lblstep13" runat="server" Text="" ForeColor="Red" Font-Size="12px"></asp:Label>     --%>           
            </div>
            <div style="clear: both;">            
            </div>
            
            <div class="products_box">
                <fieldset>
                  <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Create product code new version"></asp:Label>
                    </legend>
                      <div style="float: right;">
                             <asp:Button ID="addnewpanel" runat="server" Text="Create New Version" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                                 
                         
                         </div>
                          <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Product"></asp:Label>
                                         <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductname" runat="server" Width="300px"  AutoPostBack="True"  onselectedindexchanged="ddlproductname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <label>
                                         <asp:Label ID="Label7lde" runat="server" Text="Products description"></asp:Label>
                                     </label>
                                </td>
                                <td>
                                    <label>                                                                  
                                        <asp:TextBox ID="txt_prod_desc" BackColor="White" 
                                        BorderColor="White" BorderStyle="None" ForeColor="Black" TextMode="MultiLine" Width="600px" Height="45px"  runat="server" Enabled="False"></asp:TextBox>
                                    </label> 
                                </td>
                            </tr>
                             <tr>
                                   <td>
                                  <label>
                                    Create code in compiled version 
                                    </label> 
                                   </td>
                                    <td>
                                    <label>
                                        <asp:CheckBox ID="chk_compiler" runat="server" Enabled="false"  Checked="true" Text="Yes" />
                                        </label> 
                                                <asp:DropDownList ID="ddlcodetypecatefory" Width="200px"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlcodetypecatefory_SelectedIndexChanged" Visible="false">
                                                </asp:DropDownList>
                                    </td>                                    
                                    </tr>    
                            
                           
                          
                            <tr>
                                <td valign="top">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select website name"></asp:Label>
                                         <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcodetype" ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcodetype" runat="server" AutoPostBack="True" Width="200px" onselectedindexchanged="ddlcodetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                     <asp:Panel ID="Panel1" runat="server" Visible="false">  
                                            <asp:Panel ID="pnl_websiter1" runat="server" Visible="true">                          
                                                    <input id="hdnsortExp" type="hidden" name="hdnsortExp" style="width: 3px" runat="Server" />
                                                        <input id="hdnsortDir" type="hidden" name="hdnsortDir" style="width: 3px" runat="Server" />
                                                         <input runat="server" id="hdnProductDetailId" type="hidden" name="hdnProductDetailId" style="width: 3px" />
                                                               <asp:Panel ID="pnlpr" runat="server" Width="600px"  ScrollBars="Horizontal" Height="200px">
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="WebsiteID" AutoGenerateColumns="False"
                                                                        OnRowCommand="GridView2_RowCommand" EmptyDataText="There is no data." AllowSorting="True"
                                                                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                     OnRowDataBound="GridView2_RowDataBound"   Width="100%" OnPageIndexChanging="GridView2_PageIndexChanging" OnSorting="GridView2_Sorting"
                                                                         OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField ControlStyle-Width="5%" FooterStyle-Width="5%" HeaderStyle-Width="5%"  Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="cbHeader" runat="server" Visible="false"  OnCheckedChanged="ch1_chachedChanged" AutoPostBack="true" />
                                                                                    <asp:Label ID="check" runat="server" ForeColor="White" Text="" HeaderStyle-Width="30px" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbItem" Checked='<%# Bind("chk") %>'  runat="server" Enabled="false"  />
                                                                                    <asp:CheckBox ID="chkdef" Checked='<%# Bind("chk") %>' runat="server" Visible="false" />                                                                                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Website Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblp" runat="server" Text='<%# Bind("WebsiteName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblwebid" runat="server" Text='<%# Bind("WebsiteID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_codetypeid" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Code Name" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Need To Create Version?" SortExpression="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcheck" runat="server" ></asp:Label>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                       
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                            </asp:Panel>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
                                </td>
                                <td align="right"> 
                                        <asp:Button ID="btn_showdetail" Height="20px"   runat="server" Text="Show More Details" OnClick="btn_showdetail_Click" CssClass="btnSubmit" />
                                </td>
                            </tr>
                              <asp:Panel ID="pnl_showdetail" runat="server" Width="100%" Visible="false">
                            <tr>
                                <td>
                                    <label>
                                        Client FTP account name
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
                                       Client FTP user ID
                                    </label> 
                                </td>
                                <td>
                                      <label>
                                         <asp:Label ID="ftpuser" runat="server" Text="New code version number"></asp:Label>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="New code version number"></asp:Label>
                                         
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:Label ID="lblnewcodetypeNo" runat="server" Text="New code version number"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="New code folder Size "></asp:Label>
                                         
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:Label ID="lblsize" runat="server" Text=""></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Source path folder name of website"></asp:Label>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsourcepath"  runat="server"  Width="800px" Enabled="False" ></asp:TextBox>
                                    </label>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Temp path for compilation of website"></asp:Label>
                                        
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txttemppath" runat="server" Enabled="False" Width="800px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Output path folder name of website"></asp:Label>                                         
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtoutputsourcepath" runat="server" Enabled="False" Width="800px"></asp:TextBox>
                                    </label>
                                     
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="2">
                                      <asp:CheckBox ID="chkday" runat="server" AutoPostBack="true"  oncheckedchanged="chkday_CheckedChanged" Visible="false"  Text="Automatically create  new version for this code that interval of following days (if any pages have changes in the code)" />
                                    <label>
                                        <asp:TextBox ID="txt_day" Visible="false" runat="server"  Width="80px"></asp:TextBox>
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btn_VersionCreate" runat="server" Text="Create Version" CssClass="btnSubmit" ValidationGroup="1" OnClick="btn_VersionCreate_Click" />
                                    <asp:Button ID="Btncancle" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Btncancle_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegendd" runat="server" Text="List product code version"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Labeczcxl19" runat="server" Text="List of compiled product" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                      <label style="width:180px;">
                                        <asp:Label ID="Label15" runat="server" Text="Filter by product:"></asp:Label>                                         
                                    </label>
                                     <label>
                                        <asp:DropDownList ID="DDLProductSearch" runat="server" AutoPostBack="True" onselectedindexchanged="DDLProductSearch_SelectedIndexChanged" Width="200px" >
                                        </asp:DropDownList>
                                    </label>
                                     <label style="width:200px;">
                                        <asp:Label ID="Label10" runat="server" Text="Filter by website:"></asp:Label>                                         
                                    </label>
                                     <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"  
                                          Width="200px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </label>
                                      <label>
                                        <asp:Label ID="Label11" runat="server" Text="Period:"></asp:Label>                                         
                                    </label>
                                     <label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                          Width="200px" onselectedindexchanged="DropDownList2_SelectedIndexChanged" >
                                          
                                           <asp:ListItem Value="-1" >All</asp:ListItem>
                                           <asp:ListItem Value="1">This Month </asp:ListItem>
                                            <asp:ListItem Value="2">This Week </asp:ListItem>
                                        </asp:DropDownList>
                                    </label>                                   
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <label style="width:180px;">
                                        <asp:Label ID="Label16" runat="server" Text="Compile status:"></asp:Label>                                         
                                    </label>
                                    <label>
                                    <asp:DropDownList ID="ddlCategorySearch" Width="200px"  runat="server" AutoPostBack="True">
                                      <asp:ListItem Value="2" >All</asp:ListItem>
                                       <asp:ListItem Value="1" >Yes</asp:ListItem>
                                           <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </label> 
                                     <label style="width:200px;">
                                        <asp:Label ID="Label17" runat="server" Text="Successfully created status:"></asp:Label>                                         
                                    </label>
                                    <label>                                         
                                        <asp:DropDownList ID="DDLCodeTypeSearch" runat="server" AutoPostBack="True" Width="200px">
                                        <asp:ListItem Value="2" Selected="True">All</asp:ListItem>
                                           <asp:ListItem Value="1" >Yes</asp:ListItem>
                                           <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>                                    
                                    </label> 
                                     <label>
                                        <asp:Label ID="Label12" runat="server" Text="Search:"></asp:Label>                                         
                                    </label>
                                    <label>                                         
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>                              
                                    </label> 
                                    <label>
                                    <asp:Button ID="Button1" runat="server" Text="Go" onclick="Button1_Click" />
                                    </label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdcompiledproduct" runat="server" CssClass="mGrid" GridLines="Both"
                                      OnRowDataBound="grdcompiledproduct_RowDataBound"   PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        Width="100%">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproductidgrd" runat="server" Visible="false" Text='<%#Bind("ProductVerID") %>'></asp:Label>
                                                    <asp:Label ID="lblproductnamegrd" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodetypename" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Code Version No" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodeversionno" runat="server" Text='<%#Bind("codeversionnumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Created Using Compiler?" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("CreatedUsingCompliler") %>'  ></asp:Label>
                                                      <asp:Label ID="lblproductcodeid" runat="server" Visible="false" Text='<%#Bind("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatetime" runat="server" Text='<%#Bind("VersionDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Successfully created?" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server"  Text="" ></asp:Label>
                                                    <%--Text='<%#Bind("Successfullycreated") %>'--%>
                                                    <asp:Label ID="lbl_latestpath" runat="server" Text='<%#Bind("TemporaryPath") %>' Visible="false" ></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Profile" HeaderStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Profile" 
                                                        ForeColor="Black" onclick="LinkButton1_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<%--asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>

