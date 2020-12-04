<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Header.ascx.cs" Inherits="UserContorls_UC_Header" %>
<%--<style>#grad {
  background: red; /* For browsers that do not support gradients */
  background: -webkit-linear-gradient(#21485A, white, white); /* For Safari 5.1 to 6.0 */
  background: -o-linear-gradient(#21485A, white, white); /* For Opera 11.1 to 12.0 */
  background: -moz-linear-gradient(#21485A, white, white); /* For Firefox 3.6 to 15 */
  background: linear-gradient(#21485A, white, white); /* Standard syntax */
}</style>--%>
<style>
div.Label10 {
    color: white;
    text-shadow: 2px 2px 4px #000000;
}
</style>
<style>#grad {
  background: red; /* For browsers that do not support gradients */
  background: -webkit-linear-gradient(left, #0287a1 , #0287a1 , white, white); /* For Safari 5.1 to 6.0 */
  background: -o-linear-gradient(right,#0287a1 , #0287a1, white, white); /* For Opera 11.1 to 12.0 */
  background: -moz-linear-gradient(right, #0287a1 , #0287a1, white, white); /* For Firefox 3.6 to 15 */
  background: linear-gradient(to right, #0287a1 , #0287a1 , white, white); /* Standard syntax */
}</style>

<div id="headerInfo" style="height: 70px" margin-top= 0px" align="right">
    <span>
        <asp:Label runat="server" ID="Label6" Text="Capman Ltd." Visible="False"></asp:Label></span>
    <%--<div style="clear: both;">
    </div>
    <span>
        <asp:Label runat="server" ID="Label7" Text="510-24thStreet," 
        Visible="False"></asp:Label>
    </span>--%>
   <%--  <div style="clear: both;">
    </div>
    <span>
        <asp:Label runat="server" ID="Label1" Text=" PORT HURON, MI 48060, USA" 
        Visible="False"></asp:Label>
    </span>--%>
   <%-- <div style="clear: both;">
    </div>
    <span>
        <asp:Label runat="server" ID="Label8" Text="810-320-6715" Visible="False"></asp:Label>
    </span>"http://icons.iconarchive.com/icons/cornmanthe3rd/metronome/512/Communication-email-blue-icon.png"--%>
   <%-- src="https://ignitewebconceptions.com/wp-content/uploads/2014/09/webdesignblue.png" width="304" height="228">--%>
    <span>
        <asp:Image ID="Image2" runat="server" 
        ImageUrl="http://icons.iconarchive.com/icons/cornmanthe3rd/metronome/512/Communication-email-blue-icon.png" Height="26px" Width="26px" /> <asp:Label runat="server" ID="Label9" Text="support@itimekeeper.com " 
        style="margin-top: -27px" Font-Bold="True" Font-Size="Medium" 
        ForeColor="#006666" Font-Names="Arial Black" ></asp:Label>
    </span>
    <div style="clear: both;">
    </div>
    <span>
       <asp:Image ID="Image3" runat="server" 
        ImageUrl="http://image.flaticon.com/icons/svg/176/176644.svg" Height="26px" Width="26px" /> 
        
        <asp:Label runat="server" ID="Label10" Text="www.itimekeeper.com"  
        style="margin-top: -27px" Font-Bold="True" Font-Size="Medium" 
        ForeColor="#006666" Font-Names="Arial Black"></asp:Label>
    </span>
    <div style="clear: both;">
    </div>
</div>

<div id="grad" align="left" style="
    margin-top: -77px;
">
 <%--  http://license.busiwiz.com/images/Itimek.png--%>
       
  
   <asp:ImageButton ID="Image1" runat="server" style="height:120px;width:200px;margin-top: 10px;border-width:0px; margin-left: 12px"   />
   <img src="images/itimekeeper.jpg" style="
    margin-left: 597px;
    margin-right: -201px;
   
">
       
     
          </div>


