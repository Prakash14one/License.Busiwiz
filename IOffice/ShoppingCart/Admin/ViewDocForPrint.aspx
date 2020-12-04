<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocForPrint.aspx.cs" Inherits="Account_ViewDocForPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<meta content="60" http-equiv="refresh"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView id="grid" runat="server" Width="100%" AutoGenerateColumns="false" 
            onselectedindexchanged="grid_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%--<iframe id="SearchFrame" height="600" name="SF" scrolling="yes" src="../../Account/78787/UploadedDocuments/<%# Eval("DocumentName") %>" 
                                unselectable="on" width="100%"></iframe>--%>
                             <iframe id="Iframe1" height="600" name="SF" scrolling="yes" src="../../Account/<%=MainPath%><%# Eval("DocumentName") %>" 
                                unselectable="on" width="100%"></iframe>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
