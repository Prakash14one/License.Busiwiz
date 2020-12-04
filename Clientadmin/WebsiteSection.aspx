<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="WebsiteSection.aspx.cs" Inherits="SubMenuMaster" Title="Untitled Page" %>

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
    </style>

    <script language="javascript" type="text/javascript">
    function CallPrint(strid) 
    {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  
                
     function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
             
               
            
        }  
        function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }
        
        
      
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
        }    
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }     
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add Website Section" OnClick="addnewpanel_Click" CssClass="btnSubmit" />
                        <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text=" Select Website Name"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" ControlToValidate="ddlWebsite"
                                            ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlWebsite" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWebsite_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Section Name"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" ControlToValidate="txtSectionName"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtSectionName"
                                            ErrorMessage="Invalid Character" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ValidationGroup="1"> </asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtSectionName" onKeydown="return mask(event)" MaxLength="30" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'Span10',30)"
                                            Width="180px" runat="server"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label27" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                        <span id="Span10" class="labelcount">30</span>
                                        <asp:Label ID="Label13" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="After Login Page"></asp:Label>
                                       <%-- <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlLoginPage" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="Normal Login Url"></asp:Label>
                                        <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNormalUrl"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNormalUrl"
                                            ErrorMessage="Invalid Url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtNormalUrl" runat="server" onKeyup="return mak('Span1',50,this)"
                                            Width="300px" MaxLength="50"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label15" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="E.g. After Selecting the Website , Enter here Default Url like. http://www.xyz.com/  "
                                            Font-Size="10px" ForeColor="#FF3300"></asp:Label></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Login Url"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLoginUrl"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLoginUrl"
                                            ErrorMessage="Invalid Url" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtLoginUrl" runat="server" onKeyup="return mak('Span2',50,this)"
                                            Width="300px" MaxLength="50"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" Text="Max" runat="server" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" cssclass="labelcount">50</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="E.g. After Selecting the Website , Enter here Login Url like. http://www.xyz.com/Admin  "
                                            Font-Size="10px" ForeColor="#FF3300"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <label>
                            <asp:Label ID="Label14" runat="server" Text="Default Page Location for Section:"></asp:Label>
                            </label> 
                            </td>
                            <td>
                      
                      <label style="width:120px">
                        <asp:DropDownList ID="ddl_MainFolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedMainFolder">
                        </asp:DropDownList>
                        </label>
                      <label style="width:120px">  
                        <asp:DropDownList ID="ddl_subfolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedsubFolder">
                        </asp:DropDownList>
                        </label>
                        <label style="width:120px">  
                        <asp:DropDownList ID="ddl_SubSubfolder" runat="server" Width="120px" AutoPostBack="true"   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChangedSubsubFolder">
                        </asp:DropDownList>

                        </label>   
                      <label>
                        <asp:TextBox ID="txtFolderName" runat="server" visible="False" Width="220px" MaxLength="100" onkeyup="return mak('Span4',100,this)" ></asp:TextBox>
                      </label> 
                            </td>
                            </tr>

                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btnSubmit" Text="Submit" OnClick="Button1_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="btnSubmit" OnClick="BtnUpdate_Click"
                                        Text="Update" ValidationGroup="1" Visible="False" />
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllgnd" runat="server" Text="List of Website Sections"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="btnprint_Click" />
                        <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label12" runat="server" Text="List of Website Sections" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="asdadasd" runat="server" Width="100%">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            OnRowDeleting="GridView1_RowDeleting" DataKeyNames="WebsiteSectionId" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                                            AllowSorting="True" OnSorting="GridView1_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Website Name" SortExpression="WebsiteName" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwebsite" runat="server" Text='<%#Bind("WebsiteName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section Name" SortExpression="SectionName" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSectionName" runat="server" Text='<%#Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Normal Url" SortExpression="NormalUrl" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNormalUrl" runat="server" Text='<%#Bind("NormalUrl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login Url" SortExpression="LoginUrl" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSectidsadonName" runat="server" Text='<%#Bind("LoginUrl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="After Login Default Page" SortExpression="AfterLoginDefaultPageId"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSectasdsadsaasdionName" runat="server" Text='<%#Bind("PageName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/Account/images/edit.gif"
                                                    UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif"
                                                    HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                    ValidationGroup="2">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:CommandField>
                                                <%-- <asp:CommandField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ShowDeleteButton="True"  />--%>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" CommandName="Delete"
                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                            CommandArgument='<%# Eval("WebsiteSectionId") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <%--  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="GridHeader" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle CssClass="GridAlternateRow" BackColor="#DCDCDC" />
                            <FooterStyle CssClass="GridFooter" BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle CssClass="GridRowStyle" BackColor="#EEEEEE" ForeColor="Black" />--%><PagerStyle
                                CssClass="pgr" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                </td>
                            </tr>

                            <tr>
                        <td>
                          <asp:Panel ID="Paneldoc" runat="server" Width="65%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
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
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
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
                                    </fieldset></asp:Panel>
                                <asp:Button ID="btnreh" runat="server" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModernpopSync" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Paneldoc" TargetControlID="btnreh" CancelControlID="ImageButton10">
                                </cc1:ModalPopupExtender>
                        </td>
                        </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
