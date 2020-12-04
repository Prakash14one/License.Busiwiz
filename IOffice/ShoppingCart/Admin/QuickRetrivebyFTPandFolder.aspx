<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" 
    AutoEventWireup="true" CodeFile="QuickRetrivebyFTPandFolder.aspx.cs" Inherits="QuickRetrivebyFTPandFolder"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
 
        function ChangeCheckBoxState(id, checkState)
        {
            var cb = document.getElementById(id);
            if (cb != null)
               cb.checked = checkState;
        }
        // For Document
        function ChangeAllCheckBoxStates(checkState)
        {
            if (CheckBoxIDs != null)
            {
               for (var i = 0; i < CheckBoxIDs.length; i++)
               ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }
        }        
        function ChangeHeaderAsNeeded()
        {
            if (CheckBoxIDs != null)
            {
                for (var i = 1; i < CheckBoxIDs.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked)
                    {
                       ChangeCheckBoxState(CheckBoxIDs[0], false);
                       return;
                    }
                }        
               ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
        // For designation
        function ChangeAllCheckBoxStatesDes(checkState)
        {          
            if (CheckBoxIDsDes != null)
            {
                   for (var i = 0; i < CheckBoxIDsDes.length; i++)
                   ChangeCheckBoxState(CheckBoxIDsDes[i], checkState);
            }
        }        
        function ChangeHeaderAsNeededDes()
        {
            if (CheckBoxIDsDes != null)
            {
                for (var i = 1; i < CheckBoxIDsDes.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDsDes[i]);
                    if (!cb.checked)
                    {
                     ChangeCheckBoxState(CheckBoxIDsDes[0], false);
                        return;
                    }
                }                
                ChangeCheckBoxState(CheckBoxIDsDes[0], true);
            }
        }         
        // For employee
        function ChangeAllCheckBoxStatesEmp(checkState)
        {
            if (CheckBoxIDsEmp != null)
            {
               for (var i = 0; i < CheckBoxIDsEmp.length; i++)
               ChangeCheckBoxState(CheckBoxIDsEmp[i], checkState);
            }
        }                
        function ChangeHeaderAsNeededEmp()
        {
            if (CheckBoxIDsEmp != null)
            {
                for (var i = 1; i < CheckBoxIDsEmp.length; i++)
                {
                    var cb = document.getElementById(CheckBoxIDsEmp[i]);
                    if (!cb.checked)
                    {
                       ChangeCheckBoxState(CheckBoxIDsEmp[0], false);
                        return;
                    }
                }
                ChangeCheckBoxState(CheckBoxIDsEmp[0], true);
            }
        }
        
         function SelectAllCheckboxes(spanChk){

   // Added as ASPX uses SPAN for checkbox
   var oItem = spanChk.children;
   var theBox= (spanChk.type=="checkbox") ? 
        spanChk : spanChk.children.item[0];
   xState=theBox.checked;
   elm=theBox.form.elements;

   for(i=0;i<elm.length;i++)
     if(elm[i].type=="checkbox" && 
              elm[i].id!=theBox.id)
     {
       //elm[i].click();
       if(elm[i].checked!=xState)
         elm[i].click();
       //elm[i].checked=xState;
     }
 } 
  function mask(evt)
        { 

           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
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

    <style type="text/css">
        #innertbl1
        {
            width: 706px;
        }
        #subinnertbl1
        {
            width: 691px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="lbldoyou" runat="server" Text="   Business Name"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:DropDownList ID="ddlbusiness" runat="server" Width="190px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text=" Retrieval From"></asp:Label>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">FTP</asp:ListItem>
                                                <asp:ListItem Value="2">FOLDER</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <label>
                                    <asp:Label ID="lblhea" runat="server" Text="Select Folder Path to Download"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table id="GridTbl" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnldesignation" runat="server">
                                                            <asp:GridView ID="grdDesignation" runat="server" DataKeyNames="FolderId" AllowPaging="True"
                                                                CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                AllowSorting="True"  AutoGenerateColumns="False" EmptyDataText="No Folder Path available in Auto Document Download"
                                                                OnRowDataBound="grdDesignation_RowDataBound" Width="100%" OnPageIndexChanging="grdDesignation_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                                                        <HeaderTemplate>
                                                                      
                                                                            <input id="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                                                type="checkbox"  />
                                                                                
                                                                                  <asp:Label ID="lblselect" runat="server" Text="Select"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkDesignation" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Ftp Server" SortExpression="FolderName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfoldername" runat="server" Text='<%# Eval("FolderName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" SortExpression="Username">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Username")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Auto Process Rule"
                                                                        SortExpression="DocumentAutoApprove">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ablautoupp" runat="server" Text='<%# Eval("RuleType")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Default Folder" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocTypeAll" runat="server" Text='<%# Eval("DocTypeAll")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="Default Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDocumentDescription" runat="server" Text='<%# Eval("DocumentDescription")%>'></asp:Label>
                            </ItemTemplate>                           
                        </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Last Retrieval Time" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllastrettime" runat="server" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Last Retrieval Message" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllastretmsg" runat="server" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="imgbShowEmp" runat="server" Text="Retrieve Now" OnClick="imgbShowEmp_Click"
                                                            CssClass="btnSubmit" />
                                                        <asp:Button ID="imgbtnreset0" runat="server" AlternateText="Reset" CssClass="btnSubmit"
                                                            Text="Reset" OnClick="imgbtnreplay_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px" colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1">
                                <table id="innertbl1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                <asp:Literal ID="CheckBoxIDsArrayDes" runat="server"></asp:Literal>
                <asp:Literal ID="CheckBoxIDsArrayEmp" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
