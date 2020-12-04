<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSlideShow.aspx.cs" Inherits="slideshowmy" %>

<%@ Register TagPrefix="obshow" Namespace="OboutInc.Show" Assembly="obout_Show_Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../Developer/css/eshop_style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <%--<tr>
                <td align="left" valign="top">
                    <img src="test/img/tran.gif" width="679" height="5">
                </td>
            </tr>--%>
            <tr>
                <td align="center" valign="top">
                    <obshow:Show ID="Show1" runat="server" ShowType="Show" TransitionType="None" FixedScrolling="True"
                        Height="640px" Width="100%" ScrollDirection="Top">
                        <Changer ArrowType="Side1" HorizontalAlign="Center" Position="Top" Type="Arrow"
                            VerticalAlign="Middle">
                        </Changer>
                    </obshow:Show>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
