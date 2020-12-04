<%@ Page Title="" Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master" AutoEventWireup="true" CodeFile="shortlistingofcandidateNew.aspx.cs" Inherits="shortlistingofcandidateNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">

    function checktextboxmaxlength(txt, maxLen, evt) {
        try {
            if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                return false;
        }
        catch (e) {

        }
    }
    function mask(evt) {

        if (evt.keyCode == 13) {

        }


        if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


            alert("You have entered an invalid character");
            return false;
        }

    }
    function check(txt1, regex, reg, id, max_len) {
        if (txt1.value.length > max_len) {

            txt1.value = txt1.value.substring(0, max_len);
        }
        if (txt1.value != '' && txt1.value.match(reg) == null) {
            txt1.value = txt1.value.replace(regex, '');
            alert("You have entered an invalid character");
        }

        counter = document.getElementById(id);

        if (txt1.value.length <= max_len) {
            remaining_characters = max_len - txt1.value.length;
            counter.innerHTML = remaining_characters;
        }
    } 
        
    </script>
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
   
      
         <section class="content-header">
       

        </section>
            <!-- Main content -->
           
            <!-- left column -->
            <div class="col-md-6">
              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">&nbsp;</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                 
        
      
                  </asp:Button>
                
         <asp:Panel ID="Panel1" runat="server">
          
          
          
        &nbsp;<asp:Label ID="Label7"  runat="server" Text="Vacancy Title"  ForeColor="Black"></asp:Label><br />

               
             <asp:DropDownList ID="DropDownList1"   runat="server" AutoPostBack="True" 
                      Width="150px" >
                  </asp:DropDownList>
                 
                    <br />
                 
                    
                  <asp:Label ID="Label1" runat="server" Text=" Date Applied From" 
                ForeColor="Black"></asp:Label><br />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
                 <cc1:calendarextender ID="CalendarExtender3" runat="server" PopupButtonID="TextBox1"
                                TargetControlID="TextBox1">
                            </cc1:calendarextender>
                
                
                  <asp:Label ID="Label3" runat="server" Text=" To" 
                ForeColor="Black"></asp:Label><br />
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
                 <cc1:calendarextender ID="CalendarExtender1" runat="server" PopupButtonID="TextBox2"
                                TargetControlID="TextBox2">
                            </cc1:calendarextender>
              
               
                     <asp:Label ID="Label4" runat="server" Text="Country" 
                ForeColor="Black"></asp:Label><br />
                     <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                      Width="150px" onselectedindexchanged="DropDownList2_SelectedIndexChanged" >
                  </asp:DropDownList><br />
                
                       <asp:Label ID="Label5" runat="server" Text="State" 
                ForeColor="Black"></asp:Label><br />
                       <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                      Width="150px" onselectedindexchanged="DropDownList3_SelectedIndexChanged" >
                  </asp:DropDownList><br />
                 
                       <asp:Label ID="Label6" runat="server" Text="City" 
                ForeColor="Black"></asp:Label><br />
                       <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" 
                      Width="150px" onselectedindexchanged="DropDownList4_SelectedIndexChanged" >
                  </asp:DropDownList><br />
                
                  <asp:Label ID="Label8" runat="server" Text=" Search" 
                ForeColor="Black"></asp:Label><br />
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
               
                     <asp:Label ID="Label9" runat="server" Text="Show Active Only" 
                ForeColor="Black"></asp:Label>
                <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                     <br />
             <asp:Button ID="Button3" runat="server" Text="Go" onclick="Button3_Click" />
                   
            </asp:Panel>

           
    
                <asp:Label ID="Label2" runat="server" Text=" Shortlisting Of Candidates" Font-Bold="True"  style="margin-left: -13px;"
                                              Font-Size="18px" ></asp:Label>
                                               <asp:Button ID="Button9" runat="server" Text="Show"  ForeColor="Black" 
                                                    BackColor="#6699FF" onclick="Button9_Click"/>
                                                
                                              

                                                           
                                            <asp:Panel ID="Panel2" runat="server">
                                           
                                         
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                              
                                              
                                              
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                                              
                                              
                                              
                                               <asp:GridView ID="GridView1" runat="server"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                                AllowPaging="True"   PagerStyle-CssClass="pgr"   Width="100%"  CssClass="GridView GridView100" EmptyDataText="No Record Found."
                                                   >
                
                                              <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                  
                                                     
                                              
               
                    <asp:TemplateField HeaderText="VacancyTitle">
                        <ItemTemplate>
                     
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("VacancyPositionTitle") %>'
                            CommandName="view1" ForeColor="Black" Text='<%# Eval("VacancyPositionTitle")%>' >LinkButton</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                       <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CandidateName">
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument='<%# Eval("Candidate") %>'
                             CommandName="view4" ForeColor="Black" Text='<%# Eval("Candidate")%>' >LinkButton</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Location" DataField="location" />
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" 
                                oncheckedchanged="CheckBox2_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Note">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Candidateid" Visible="False">
                                                        <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton4" runat="server"  ForeColor="Black" Text='<%# Eval("CandidateID") %>' CommandArgument='<%# Eval("CandidateID") %>'
                                                                                CommandName="view" >LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VacancyId" Visible="False">
                                                         <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("VacancyId") %>'></asp:Label>
                                                             </ItemTemplate>
                                                        </asp:TemplateField>
                </Columns>
               
                                                    <AlternatingRowStyle CssClass="alt" />
                                                        <PagerStyle CssClass="pgr" HorizontalAlign="Right"></PagerStyle>
                                                        <HeaderStyle CssClass="header" />
                                                      
                                                    </asp:GridView>
                                                   
                                                <asp:Panel ID="Panel4" runat="server">
                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                     <asp:Button ID="Button2" runat="server" Text="Submit" BackColor="#6699FF" 
                                                    onclick="Button2_Click" />
                                                    &nbsp;&nbsp;
                                                     </asp:Panel>
                                                     
                                                    </asp:Panel>
               
                                                   
                

         
           <div>
               <asp:Panel ID="Panel3" runat="server">
            <asp:GridView ID="GridView2" runat="server"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                                AllowPaging="True"   PagerStyle-CssClass="pgr"   
                       Width="100%"  CssClass="GridView GridView100" EmptyDataText="No Record Found." 
                                                    >
                
                                              <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                    <asp:TemplateField HeaderText="Candidate Name">
                        <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("CandidateID")%>' Visible="false"></asp:Label>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Candidate") %>'
                             CommandName="view2" ForeColor="Black" Text='<%# Eval("Candidate")%>' >LinkButton</asp:LinkButton>
                        </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="15%" />
                       <ItemStyle Width="15%" />
                      
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vacancy Title">
                        <ItemTemplate>
                           <asp:Label ID="Label12" runat="server" Text='<%# Eval("ID")%>' Visible="false" ></asp:Label>
                           <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("VacancyPositionTitle") %>'
                            CommandName="view3" ForeColor="Black" Text='<%# Eval("VacancyPositionTitle")%>'>LinkButton</asp:LinkButton>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle Width="15%" />
                         
                       
                      
                    </asp:TemplateField>
                   
                   
                    <asp:TemplateField HeaderText="Candidate Code">
                      <ItemTemplate>
                     <asp:Label ID="Label11" runat="server" Text='<%# Eval("candidate_code")%>' ></asp:Label>
                      </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assessment Code">
                     <ItemTemplate>
                     <asp:Label runat="server" Text='<%# Eval("test_center_code")%>' ></asp:Label>
                     </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle Width="15%" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VacancyId" Visible="False"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CandidateId" Visible="False"></asp:TemplateField>
                </Columns>
                
                                                    <AlternatingRowStyle CssClass="alt" />
                                                        <PagerStyle CssClass="pgr" HorizontalAlign="Right"></PagerStyle>
                                                        <HeaderStyle CssClass="header" />
                </asp:GridView>
            </asp:Panel>
            </div>
            </div>
            
</asp:Content>
