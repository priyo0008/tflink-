<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="TflinkTest.FamilyTree.Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <%--     <h2>Member Name,Details,Address,Photo</h2>
            <h3>Show Family Member count</h3>
            <h3>Added from Different family</h3>
            <h3>Total Administrator</h3>
            <h3>Photos Count</h3>
            <h3>Request sent,Request came,request accept</h3>
            <h3>Total Projects</h3>
            <h3>Ectra Details</h3>--%>
            <h3>Currently Online :</h3>
            <asp:Label ID="lbl_curonline" runat="server" Text=""></asp:Label>
            <h3>Total Member Visit :  </h3>
            <asp:Label ID="lbl_visit" runat="server" Text=""></asp:Label>
            <h3>Total Member Registered :  </h3>
            <asp:Label ID="lbl_reg" runat="server" Text=""></asp:Label>
            <h3>Total Administrator :  </h3>
            <asp:Label ID="lbl_admnstrt" runat="server" Text=""></asp:Label>
        </div>
        <div>
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
                            Go To Tree</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" FooterText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" FooterText="Last Name" />
                <asp:BoundField DataField="MemberId" HeaderText="Member Id" FooterText="Member Id" />
                <asp:BoundField DataField="Userid"  HeaderText="User id" FooterText="User id" />
                <asp:BoundField DataField="Password" HeaderText="Password" FooterText="Password" />
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
    </div>
</asp:Content>
