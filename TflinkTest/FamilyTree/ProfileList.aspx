<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="ProfileList.aspx.cs" Inherits="TflinkTest.FamilyTree.ProfileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 80%; margin-left: auto; margin-right: auto;">
        <h2>Profiles I Control </h2>
        <asp:GridView ID="Grd_Pofile" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <%--<asp:TemplateField HeaderText="SlNo.">
                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:ImageField DataImageUrlField="FirstName" ControlStyle-Width="50" ControlStyle-Height="50" HeaderText="Preview Image"></asp:ImageField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href='MyTree1.aspx?Memid=<%#Eval("MemberId") %>'>
                            <asp:Image ID="Image1" Height="30" Width="30" ImageUrl="images/tree2.jpg" runat="server" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" FooterText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" FooterText="Last Name" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" FooterText="Gender" />
                <asp:BoundField DataField="Dob" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Age" FooterText="Age" />
                <asp:BoundField DataField="City" HeaderText="City" FooterText="City" />
                <asp:BoundField DataField="Dod"  DataFormatString="{0:MM/dd/yyyy}" HeaderText="" FooterText="" />
                <asp:BoundField DataField="Creradod"  HeaderText="" FooterText="" />
            </Columns>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
</asp:Content>
