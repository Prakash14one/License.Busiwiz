<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="GiftCertMasterTbl.aspx.cs" Inherits="ShoppingCart_Admin_Default4" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">
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
             <asp:Label ID="lblmessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </div> 
        <div style="clear: both;">
            </div>
         <div class="products_box">
            <fieldset>
             <table style="width: 100%;">
                <tr>
                    <td colspan="3" align="right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" 
                            Text="View List of Certificates Purchased" onclick="Button1_Click" />
                    </td>
                </tr>
                 <tr>
                     <td>
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Card No."></asp:Label>
                        </label>
                     </td>
                     <td>
                         <label>
                            <asp:Label ID="lblnum" runat="server" Text=""></asp:Label>
                        </label>
                     </td>
                     
                 </tr>
                 <tr>
                     <td>
                         <label>
                            <asp:Label ID="Label2" runat="server" Text="Passcode"></asp:Label>
                        </label>
                     </td>
                     <td>
                          <label>
                            <asp:Label ID="lblalphanum" runat="server" Text=""></asp:Label>
                        </label>
                     </td>
                   
                 </tr>
                 <tr>
                     <td>
                         <label>
                            <asp:Label ID="Label3" runat="server" Text="Amount"></asp:Label>                              
                             <asp:RequiredFieldValidator ID="iop" runat="server" ErrorMessage="*" ControlToValidate="TextBox1">
                             </asp:RequiredFieldValidator>
                             <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                             <asp:RegularExpressionValidator ID="Rew" ControlToValidate="TextBox1"
                                  ValidationExpression="^(-)?\d+(\.\d\d)?$" runat="server" ErrorMessage="Invalid">
                              </asp:RegularExpressionValidator>
                        </label>
                         <label>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </label>
                     </td>
                     <td>
                        <label>
                             <asp:TextBox ID="TextBox1" runat="server" MaxLength="15" onkeyup="return mak('Span2',15,this)"></asp:TextBox>
                               <cc1:FilteredTextBoxExtender ID="dfg" runat="server"
                                   Enabled="True" TargetControlID="TextBox1" ValidChars="0147852369.">
                               </cc1:FilteredTextBoxExtender>                               
                        </label>
                        <label>
                            Characters Remaining                       
                            <span id="Span2">15</span>
                            <asp:Label ID="Label23" runat="server" Text="(0-9,.)"></asp:Label>
                        </label>
                     </td>
                    
                 </tr>
                 <tr>
                    <td>
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Recipient"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <label>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </label>
                    </td>
               
                 </tr>
                 <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </td>
                    <td>
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="On Successful Completion of this transaction,would you like to send an Email to the Recipient ?"></asp:Label>
                    </label>
                    </td>
                
                 </tr>
                 <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Buy Now" CssClass="btnSubmit" 
                            onclick="Button2_Click"/>
                    </td>
                 </tr>
             </table>
            </fieldset>
         </div>
        </ContentTemplate>
       </asp:UpdatePanel>     
</asp:Content>


