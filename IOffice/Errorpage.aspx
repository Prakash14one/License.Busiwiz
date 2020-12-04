<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master/Login.master" CodeFile="Errorpage.aspx.cs" Inherits="Errorpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <!-- Codrops top bar -->
        <div class="codrops-top">
        </div>
        <!--/ Codrops top bar -->
        <div id="container_demo">
            <a class="hiddenanchor" id="toregister"></a><a class="hiddenanchor" id="tologin">
            </a>
            <div id="wrapper">
                <div id="login" class="animate form">
                    <form runat="server" id="fmLogin" autocomplete="on">
                    <h3>
                    <asp:Label ID="lblerrms" runat="Server" Text="Sorry this page has some error.Kindly report to webmaster.You need to login again to the application"></asp:Label>
                    </h3>
                        
                      <p class="login button">
                     
                   </p>

                    <p class="login button">
                 <%--   <%=strplan1%> --%>
                        <asp:Button ID="btnsignin" runat="server" Text="Login Now" 
                                                            TabIndex="1" onclick="btnsignin_Click" 
                                                            />
                                                              &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel"  
                                                           TabIndex="2" Visible="false"
                                                            />
                    </p>
                     <p>
                        <label for="username" class="uname">
             <%--         <asp:Label ID="lblms" runat="server" Text=""></asp:Label>--%>
                        </label>
                      
                    </p>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
