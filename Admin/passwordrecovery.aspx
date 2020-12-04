<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="passwordrecovery.aspx.cs" Inherits="passwordrecovery" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type ="text/javascript" language="javascript" >
function ValidateText(i)
{
    if(i.value.length > 0)
    {
    i.value = i.value.replace(/[^\d]+/g, '');
    }
}


</script>

 <table cellpadding="0" cellspacing="0" id="pagetbl" border="0" align="center" style="width: 672px; background-color: #EFEFEF;">
         <tr>
            <td class="column1" style="width: 169px">
                &nbsp;</td>
            <td class="column2" style="width: 147px">
            &nbsp;</td>
            <td class="column1">
            &nbsp;</td>
            <td class="column2" style="width: 215px">
            &nbsp;</td>
        </tr>

        <tr>
            <td colspan="4" class="hdng">
                <asp:Label ID="lblHead" runat="server" Text="Username /Password recovery Form"></asp:Label></td>
        </tr>
        
        <tr>
        
        <td style="height: 20px">
        
        	</td><td>
        	</td>
        
        </tr>

        <tr>
        
        <td colspan="4">
        
        <table align="center" style="position: relative; left: 0px; top: 0px; height: 76px;">
        <tr>
        <td colspan="2">
         <asp:Label ID="lbl_msg" runat="server" Text="You will then receive an e-mail message with your forgotten User IDs" Visible="false"></asp:Label></td>
        </td>
        </tr>
        <tr>
        
        <td class="column1" style="width: 144px">
        
        Please Enter Your Email Id :
        </td>
        <td style="width: 221px">
        <asp:TextBox runat="server" id="TextBox1" Width="218px"></asp:TextBox>
        </td>
        </tr>

        
        
        
        
        <tr>
            <td class="column1" style="width: 144px">
            &nbsp;</td>
            <td class="column2" align="center" style="height: 24px; width: 221px;">
              <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" TabIndex="4" ValidationGroup="1" Text="Login" Height="19px" Width="55px" />
               &nbsp;</td>
            
            
        </tr>
         <tr>
            <td class="column1" style="width: 144px">
            &nbsp;</td>
            <td class="column2" align="center" style="height: 24px; width: 221px;">
               &nbsp;</td>
            
            
        </tr>

 <tr>
            <td class="column1" style="width: 144px">
            &nbsp;</td>
            <td class="column2" align="center" style="height: 24px; width: 221px;">
               &nbsp;</td>
            
            
        </tr>

 <tr>
            <td class="column1" style="width: 144px">
            &nbsp;</td>
            <td class="column2" align="center" style="height: 24px; width: 221px;">
               &nbsp;</td>
            
            
        </tr>


        
        
        
        
        
        <tr>
            <td class="column1" style="width: 144px">
            </td>
            <td class="column2" style="height: 24px; width: 221px;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
            <td class="column1">
            
            </td>
           
        </tr>
        </table>
        
        </td>
        </tr>
        
        
        
        
    </table>





</asp:Content>

