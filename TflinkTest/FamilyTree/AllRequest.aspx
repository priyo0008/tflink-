<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="AllRequest.aspx.cs" Inherits="TflinkTest.FamilyTree.AllRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .invisibletext {
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 80%; margin-left: auto; margin-right: auto;">
        <h2>Your Requests</h2>
        <p id="myrequest" style="color: red" runat="server"></p>
        <asp:GridView ID="Grd_Pofile" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundField DataField="RequestFrom" HeaderText="Request From" FooterText="Request From" />
                <asp:BoundField DataField="RequestTo" HeaderText="Request To" FooterText="Request To" />
                <asp:BoundField DataField="RequestMsg" HeaderText="Request Msg" FooterText="Request Msg" />
                <asp:BoundField DataField="Enterdt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Dob" FooterText="Dob" />
                <asp:TemplateField HeaderText="Accept Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtn_live" runat="server" CommandName="Active" OnClick="lnkbtn_live_Click" class="btn btn-default" Text='<%#Eval("Status") %>'> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtn_rej" runat="server" CommandName="Active" OnClick="lnkbtn_rej_Click" class="btn btn-default" Text='<%#Eval("Regstatus") %>'> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderText="id">
                    <HeaderStyle CssClass="invisibletext" Width="1%" />
                    <ItemStyle CssClass="invisibletext" />
                </asp:BoundField>
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
    <div style="width: 80%; margin-left: auto; margin-right: auto;">
        <h2>Requests from Other Administrators</h2>
        <p id="reqfromother" style="color: red" runat="server"></p>
        <asp:GridView ID="grd_bind" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundField DataField="RequestFrom" HeaderText="Request From" FooterText="Request From" />
                <asp:BoundField DataField="RequestTo" HeaderText="Request To" FooterText="Request To" />
                <asp:BoundField DataField="RequestMsg" HeaderText="Request Msg" FooterText="Request Msg" />
                <asp:BoundField DataField="Enterdt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Dob" FooterText="Dob" />
                <asp:BoundField DataField="Status" HeaderText="Accept Status" FooterText="Accept Status" />
                <asp:BoundField DataField="Regstatus" HeaderText="Reject Status" FooterText="Reject Status" />
                <asp:TemplateField HeaderText="Accept Status">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Estexit1" runat="server" ForeColor='<%# Eval("Status").ToString()=="Accept"?System.Drawing.Color.Blue:System.Drawing.Color.Green %>' Text='<%#Eval("Status")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject Status">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Estexit1" runat="server" ForeColor='<%# Eval("Regstatus").ToString()=="Reject"?System.Drawing.Color.Blue:System.Drawing.Color.Blue %>' Text='<%#Eval("Regstatus")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--   <asp:TemplateField HeaderText="Accept Requests">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtn_live" runat="server" CommandName="Active" OnClick="lnkbtn_live_Click" class="btn btn-default" Text='<%#Eval("Status") %>'> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Accept Requests">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtn_rej" runat="server" CommandName="Active" OnClick="lnkbtn_rej_Click" class="btn btn-default" Text='<%#Eval("Regstatus") %>'> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="id" HeaderText="id">
                    <HeaderStyle CssClass="invisibletext" Width="1%" />
                    <ItemStyle CssClass="invisibletext" />
                </asp:BoundField>
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
