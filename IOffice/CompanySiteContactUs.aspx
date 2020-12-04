<%@ Page Title="" Language="C#" MasterPageFile="~/CompanySite.master" AutoEventWireup="true" CodeFile="CompanySiteContactUs.aspx.cs" Inherits="CompanySiteContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<script type="text/javascript">

    var max = 40;
    function changed() {
        var textbox1 = document.getElementById('<%= TextBox1.ClientID %>');
        var labelresult = document.getElementById('<%= Label5.ClientID %>');
        labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)";
        if (textbox1.value.length >= max) {
            return false;
        }
        labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)"; return true;
    }
   
</script>
<script type="text/javascript">

    var max = 40;
    function changed() {
        var textbox1 = document.getElementById('<%= TextBox2.ClientID %>');
        var labelresult = document.getElementById('<%= Label6.ClientID %>');
        labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)";
        if (textbox1.value.length >= max) {
            return false;
        }
        labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)"; return true;
    }
    
</script>
   <script type="text/javascript">

       var max = 20;
       function changed() {
           var textbox1 = document.getElementById('<%= TextBox3.ClientID %>');
           var labelresult = document.getElementById('<%= Label7.ClientID %>');
           labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)";
           if (textbox1.value.length >= max) {
               return false;
           }
           labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)"; return true;
       }
      </script>
      <script type="text/javascript">

          var max = 3500;
          function changed() {
              var textbox1 = document.getElementById('<%= TextBox5.ClientID %>');
              var labelresult = document.getElementById('<%= Label8.ClientID %>');
              labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)";
              if (textbox1.value.length >= max) {
                  return false;
              }
              labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)"; return true;
          }
   
</script>
    <script type="text/javascript">

        var max = 40;
        function changed() {
            var textbox1 = document.getElementById('<%= TextBox4.ClientID %>');
            var labelresult = document.getElementById('<%= Label9.ClientID %>');
            labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)";
            if (textbox1.value.length >= max) {
                return false;
            }
            labelresult.innerHTML = "Max: " + (max - (textbox1.value.length)) + " (A-Z a-z)"; return true;
        }
   
</script>
<style type="text/css">
    .contus
{
       float: left;
    height: 30px;
    padding: 0px 10px;
    width: 913px;
    margin-top: -33px;
    margin-left: 281px;
       background-color: #65BBD1;
    color: #FFF;
    font-size: 19px;
    font-weight: bold;
    border-radius: 5px 5px 5px 5px;
    line-height: 30px;
    text-align: left;
    margin-left: 162px;
   
}
 .border
 {
       margin: 66px 0px 0px -2px;
    padding: 12px;
    margin-top: 41px;
    margin-left: 284px;
    width: 916px;
    min-height: 511px;
    border: 2px solid #1991A9;
    border-radius: 6px 6px 6px 6px;
    text-align: -webkit-auto;
    margin-left: 167px;
 }
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:Panel ID="Panel3" runat="server"  >
<div >
<div class="contus" >
                 Contact Us</div>
                    <div class="border">
                      <asp:Panel ID="Panel4" runat="server" Width="100%"  > 
                     
                      <div>
                     <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label></div>
                     <div style="margin-left: 145px;margin-top: -19px;width: 300px;">
                         <asp:TextBox ID="TextBox1" runat="server" onkeyup="changed();"></asp:TextBox>
                     <asp:Label ID="Label5" runat="server"  Font-Size="8pt"  ForeColor="#080808">Max: 40 (A-Z a-z)</asp:Label> 
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                          ControlToValidate="TextBox1" ErrorMessage="*" 
                                                          ValidationExpression="\b[A-Z  a-z / - ]+\b" ValidationGroup="a">*</asp:RegularExpressionValidator>
                     </div>
                     <br />
                     <div>
                         <asp:Label ID="Label2" runat="server" Text="Company Name:"></asp:Label></div>
                       <div style="margin-left: 145px;margin-top: -19px;width: 300px;" onkeyup="changed();">  <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server"  Font-Size="8pt"  ForeColor="#080808">Max: 40 (A-Z a-z)</asp:Label>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                          ControlToValidate="TextBox2" ErrorMessage="*" 
                                                          ValidationExpression="\b[A-Z  a-z / - ]+\b" ValidationGroup="a">*</asp:RegularExpressionValidator>
                        
                     </div>
                     <br />
                        <div>
                        
                            <asp:Label ID="Label3" runat="server" Text="Phone No.:"></asp:Label></div>
                           <div style="margin-left: 145px;margin-top: -19px;width: 300px;"> <asp:TextBox ID="TextBox3" runat="server" onkeyup="changed();"></asp:TextBox>
                           <asp:Label ID="Label7" runat="server"  Font-Size="8pt"  ForeColor="#080808">Max: 20 (0-9)</asp:Label>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                          ControlToValidate="TextBox3" ErrorMessage="*" 
                                                          ValidationExpression="\b[ 0-9- ]+\b" ValidationGroup="a">*</asp:RegularExpressionValidator>
                             
                            </div> 
                            <br />
                           <div>  <asp:Label ID="Label4" runat="server" Text="Email:"></asp:Label></div>
                           <div style="margin-left: 145px;margin-top: -19px;width: 300px;"> <asp:TextBox ID="TextBox4" runat="server" onkeyup="changed();"></asp:TextBox> 
                           <asp:Label ID="Label9" runat="server"  Font-Size="8pt"  ForeColor="#080808">Max: 40 (A-Z a-z)</asp:Label>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox4" ErrorMessage="Please enter valid Country Name" 
                                                             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                             ValidationGroup="a">*</asp:RegularExpressionValidator></div>
                           <br />
                           <div><asp:Label ID="Label11" runat="server" Text="Message:"></asp:Label></div>
                          <div style="width: 400px;">  <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" width=" 443px" height="218px" onkeyup="changed();"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server"  Font-Size="8pt"  ForeColor="#080808">Max: 3500 (A-Z a-z)</asp:Label>
                            </div> 
                            <%--<div>
                             <asp:Label ID="Label6" runat="server" Text="Captcha"></asp:Label>
                            </div>--%> 
                            <br />
                            <div>
                               <img src="Captcha.aspx" width="200px" height="50px" alt="Captcha.aspx" /></div>
                                <div style=" margin-left: 242px; margin-top: -34px;width: 237px;">
                                    <asp:TextBox ID="TextBox6" runat="server" Width="150px"></asp:TextBox></div>
                            <div style="margin-left: 230px;margin-top: 49px;width: 163px;">
                                <asp:Button ID="Button1" runat="server" Text="Send" onclick="Button1_Click" 
                                    ValidationGroup="a" />
                            </div>
                           </asp:Panel>
                             </div></asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

