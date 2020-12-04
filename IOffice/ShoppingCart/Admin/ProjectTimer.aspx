<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ProjectTimer.aspx.cs" Inherits="ProjectTimer"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%-- <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  
function Button1_onclick() {

}

function Button2_onclick() {

}

   function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
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

   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <script language="javascript" type="text/javascript">
         </script>
         <div class="products_box">
         
       
               <div style="padding-left:1%">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="true"></asp:Label>
               </div>
               <div style="clear: both;">
               
               </div>      
          <fieldset>
                <legend>
          
                    <asp:Label ID="lbllegend" runat="server" Text="Project Time Tracking" 
                       ></asp:Label> 
          
                </legend>
          
                <div  style="float:right">
                        <asp:Button ID="btnaddpj" runat="server" Text="View Project List" 
                            CssClass="btnSubmit" onclick="btnaddpj_Click" 
                   />
                </div>
          
     <label>
                             <asp:Label ID="Label1" runat="server"  Text="Business Name"></asp:Label>
                             <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="1" runat="server" ErrorMessage="*" ControlToValidate="ddlstore"></asp:RequiredFieldValidator>
                             <asp:DropDownList ID="ddlstore" runat="server"  OnSelectedIndexChanged="ddlStoreName_SelectedIndexChanged"
                                            AutoPostBack="True">
                             </asp:DropDownList>
                        </label>
             
              <div style="clear: both;">
              </div>
              <label>
              <asp:RadioButtonList ID="Rdprojecttype" runat="server" 
                      RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="Rdprojecttype_SelectedIndexChanged" >
              <asp:ListItem Value="0" Selected="True" Text="Start Time Tracking"></asp:ListItem>
               <asp:ListItem Value="1" Text="End Time Tracking"></asp:ListItem>
              </asp:RadioButtonList>
              </label>
                
                 
              <div style="clear: both;">
              </div>
                
            
                                                     <label>
                                                        <asp:Label ID="Label4" runat="server"  Text="Select Customer"></asp:Label>
                                                        <asp:Label ID="Label5" runat="server"  Text="*" CssClass="labelstar"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="ReqdFieldValidator12" runat="server" 
                                    ControlToValidate="ddlparty" Display="Dynamic" ErrorMessage="*" 
                                    SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                 
                                                   </label>
                                                    <label>
                                                        <asp:DropDownList ID="ddlparty" 
                    runat="server" AppendDataBoundItems="true" 
                    onselectedindexchanged="ddlparty_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        </label>
                                            <label>
                           
                            <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" ImageAlign="Bottom" />
                        </label>
                        <label>
                         
                            <asp:ImageButton ID="LinkButton97666667" runat="server" Height="20px" ToolTip="Refresh "
                                Width="20px" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                OnClick="LinkButton97666667_Click" ImageAlign="Bottom"></asp:ImageButton>
                        </label>
             
               <label>
                                            <asp:Label ID="Label10" runat="server" Text="Select Project"></asp:Label>  
                                             <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label> 
                                             <asp:RequiredFieldValidator ID="reqfi" ValidationGroup="1" ControlToValidate="ddljob" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>               
                                         </label>
                                         <label>
                                         <asp:Panel ID="pnljst" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddljob" runat="server">
                                            </asp:DropDownList>
                                            </asp:Panel>
                                        </label>
                                             <label>
                                         <asp:Panel ID="pnljobend" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddljobend" runat="server" 
                                                 onselectedindexchanged="ddljobend_SelectedIndexChanged" AutoPostBack="True" 
                    >
                                            </asp:DropDownList>
                                            </asp:Panel>
                                        </label>
                                           <label>
                          
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                    AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                Height="20px" ToolTip="Add New " Width="20px"  
                    ImageAlign="Bottom" onclick="ImageButton1_Click" />
                        </label>
                        <label>
                       
                            <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ToolTip="Refresh "
                                Width="20px" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                 ImageAlign="Bottom" onclick="ImageButton2_Click"></asp:ImageButton>
                        </label>  
                       <div style="clear: both;">
                        </div>
                 <div style="clear: both;">
                <asp:Panel id="pnlendtime" runat="server">
               <label> <asp:Label ID="lblpstarted" runat="server" Text="Project started at "></asp:Label>
                <asp:Label ID="lblstime" runat="server" Text=""></asp:Label>
               </label> 
               
                <label> <asp:Label ID="Label2" runat="server" Text="Time Spent So far "></asp:Label>
                <asp:Label ID="lbltimespend" runat="server" Text=""></asp:Label>
               </label> 
                 </asp:Panel>
                 </div>
                 <div style="clear: both;">
                 <br />
                        </div>
                   <div style="clear: both;">
                   <table width="80%">
                   <tr>
                   <td align="center">
                     <asp:Button ID="btnstime" runat="server" 
                                            Text="Start Time Tracking Now" Width="70px" CssClass="btnSubmit" 
                           ValidationGroup="1" onclick="btnstime_Click" />
                                             <asp:Button ID="btnetime" runat="server" 
                                            Text="End Time Tracking Now" Width="70px" CssClass="btnSubmit" 
                           ValidationGroup="1" onclick="btnetime_Click" />
                   </td>
                   </tr>
                   </table>
                 
              </div>
          
          
            <div style="clear: both;">
                 
                        </div>
                        <asp:Panel ID="pnlstm" runat="server" >
                        <label>
                        <asp:Label ID="bbdf" runat="server" Text="List of Ongoing Projects"></asp:Label>
                        </label>
                         <div style="clear: both;">
           <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                                 AlternatingRowStyle-CssClass="alt" Width="100%" 
                            DataKeyNames="Id" onpageindexchanging="GridView1_PageIndexChanging" 
                                 EmptyDataText="No Record Found." onrowcommand="GridView1_RowCommand" 
                                 onrowcancelingedit="GridView1_RowCancelingEdit" 
                                 onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                                 onrowupdating="GridView1_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartyname" runat="server" Text='<%#Bind("Compname") %>'></asp:Label>
                                       
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                     <asp:Label ID="lblPartyname1" runat="server" Text='<%#Bind("Compname") %>'></asp:Label>
                                  
                                   </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Title" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectname" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                         <asp:Label ID="lbljobid" runat="server" Text='<%#Bind("JobId") %>' Visible="false"></asp:Label>
                                                       
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    <asp:Label ID="lblProjectname1" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                     <asp:Label ID="lbljobid1" runat="server" Text='<%#Bind("JobId") %>' Visible="false"></asp:Label>
                                                       
                                   </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date/Time Started" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                         <asp:Label ID="lblfromtime" runat="server" Text='<%#Bind("FromTime") %>'></asp:Label>
                                    </ItemTemplate>
                                     <EditItemTemplate>
                                    <label>
                                        <asp:TextBox ID="txtGridInED" runat="server" Width="65px" Text='<%#Bind("Date") %>'></asp:TextBox><asp:RequiredFieldValidator
                                            ID="requiredfididato1" runat="server" ControlToValidate="txtGridInED" ErrorMessage="*"
                                            ValidationGroup="qq"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rghjk2" runat="server" ErrorMessage="*" ControlToValidate="txtGridInED"
                                            ValidationGroup="qq" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                      
                                        <cc1:CalendarExtender ID="CalendarExtender3339991" TargetControlID="txtGridInED"
                                            PopupButtonID="txtGridInED" runat="server">
                                        </cc1:CalendarExtender>
                                        </label>
                                        <label>
                                          <asp:TextBox ID="lblfromtime11" runat="server"  Width="40px"  Text='<%#Bind("FromTime") %>'></asp:TextBox>
                                          <asp:RequiredFieldValidator
                                            ID="RequirieldValidator1" runat="server" ControlToValidate="lblfromtime11" ErrorMessage="*" SetFocusOnError="true"
                                            ValidationGroup="qq"></asp:RequiredFieldValidator>
                                        </label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="Date/Time Ended" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                         <asp:Label ID="lblEnddate" runat="server" Text='<%#Bind("Enddate") %>'></asp:Label>
                                         <asp:Label ID="lblfromtotime" runat="server" Text='<%#Bind("FromToTime") %>'></asp:Label>
                                    </ItemTemplate>
    <EditItemTemplate>
                                    <label>
                                        <asp:TextBox ID="txtGridInED1" runat="server" Width="65px" Text='<%#Bind("Enddate") %>'></asp:TextBox><asp:RequiredFieldValidator
                                            ID="requiididato1" runat="server" ControlToValidate="txtGridInED1" ErrorMessage="*"
                                            ValidationGroup="qq"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rghjk21" runat="server" ErrorMessage="*" ControlToValidate="txtGridInED1"
                                            ValidationGroup="qq" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                      
                                        <cc1:CalendarExtender ID="CalExtender3339991" TargetControlID="txtGridInED1"
                                            PopupButtonID="txtGridInED1" runat="server">
                                        </cc1:CalendarExtender>
                                        </label>
                                        <label>
                                          <asp:TextBox ID="lblfromtime1" runat="server"  Width="40px" Text='<%#Bind("FromToTime") %>'></asp:TextBox>
                                          <asp:RequiredFieldValidator
                                            ID="RequiredFdator1" runat="server" ControlToValidate="lblfromtime1" ErrorMessage="*" SetFocusOnError="true"
                                            ValidationGroup="qq"></asp:RequiredFieldValidator>
                                        </label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Spent Hours" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Width="8%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                       
                                         <asp:Label ID="lblhor" runat="server" Text='<%#Bind("Hrs") %>'></asp:Label>
                                           <asp:Label ID="lblrate" runat="server" Text='<%#Bind("Rate") %>' Visible="false"></asp:Label>
                                           
                                    </ItemTemplate>
                                 <EditItemTemplate>
                                   <label>
                                          <asp:Label ID="txthr" runat="server" Text='<%#Bind("Hrs") %>'></asp:Label>
                                            <asp:Label ID="lblrate1" runat="server" Text='<%#Bind("Rate") %>' Visible="false"></asp:Label>
                                        </label>
                                 </EditItemTemplate>
                                </asp:TemplateField>
                                  <asp:CommandField ShowEditButton="True" ValidationGroup="qq" CancelText="CANCEL"
                                    CausesValidation="true" HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                    ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif"
                                    UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif" />
                              
                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Btn" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                             </Columns>
                          
                            <PagerStyle CssClass="pgr" />
                            <AlternatingRowStyle CssClass="alt" />
                          
                        </asp:GridView>
             </div>
             </asp:Panel>
          </fieldset>
         
            
            </div>
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

