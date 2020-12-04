<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Blank.master" AutoEventWireup="true" CodeFile="DownloadPopup.aspx.cs" Inherits="DownloadPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlPopup" runat="server"  BackColor="White" BorderColor="Gray" Width="600" Height="500px"
                    BorderStyle="Solid" BorderWidth="5px">
                <div style="text-align:center; margin-bottom:10px">
                <asp:Label ID="lblTitle" runat="server" Text="List of documentation" Font-Bold="true" ></asp:Label></div>
                <asp:GridView runat="server" ID="grvDocuments" EmptyDataText="No documentation found." AutoGenerateColumns="false" Width="100%" DataKeyNames="DocumentTitle,DocumentID,PageVersionID" OnRowCommand="grvDocuments_RowCommand" >
                    <Columns>
                        <asp:BoundField DataField="DocumentTitle" SortExpression="DocumentTitle" HeaderText="Document Title" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" CommandName="Download" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ForeColor="black" runat="server" Text="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView><br />

                <asp:GridView runat="server" Visible="false" ID="grvInstructions" EmptyDataText="No instruction files found." AutoGenerateColumns="false" Width="100%" DataKeyNames="ID,VersionID,FileTitle,WorkRequirementPdfFileName" OnRowCommand="grvInstructions_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="FileTitle" SortExpression="FileTitle" HeaderText="FileTitle" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" CommandName="Download" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ForeColor="black" runat="server" Text="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView><br />
                <center>
                <asp:Button ID="btnCancel" onclientclick="window.close();" runat="server" Text="Cancel"  /></center>
        
            </asp:Panel>
</asp:Content>

