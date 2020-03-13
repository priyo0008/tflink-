<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSpouses.aspx.cs" Inherits="TflinkTest.FamilyTree.AddSpouses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="col-md-12">
                <div class="row">
                    <h3>Add Spouse for Profile Surya Panda
                    </h3>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        Type :
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>Please Select</asp:ListItem>
                            <asp:ListItem>Marriage</asp:ListItem>
                            <asp:ListItem>Common Law</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        If the person already exists in the system, enter the name here and click on Search<br />
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Search" />
                          <hr />
                        OR  if the person is not yet set up,<asp:Button ID="Button2" runat="server" Text="Click To Add New Person" />
                    </div>
                    <div class="row">
                        <h3>Spouse</h3>
                        <div style="margin-bottom: 20px;">
                            <%--<asp:Button ID="btnAddRegistration" runat="server" Text="Add Registration" class="btn btn-secondary" />--%></div> 
                        <asp:GridView ID="gdvshowdata" runat="server" CssClass="table table-bordered text table-striped" AllowPaging="True" ShowFooter="True" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" PageSize="40">
                            <Columns>
                                <asp:BoundField DataField="SlNo" HeaderText="Slno" FooterText="Slno" />
                                <asp:BoundField DataField="RegdId" HeaderText="RegdId" FooterText="RegdId" />
                                <asp:BoundField DataField="Date" HeaderText="Date" FooterText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="Name" HeaderText="Name" FooterText="Name" />
                                <asp:BoundField DataField="MobNo" HeaderText="MobNo" FooterText="MobNo" />
                                <asp:BoundField DataField="Registration" HeaderText="Registration" FooterText="Registration" />
                                <asp:BoundField DataField="Category" HeaderText="Category" FooterText="Category" />
                                <asp:BoundField DataField="RegAmount" HeaderText="RegAmount" FooterText="RegAmount" DataFormatString="{0:0.00}" />
                                <asp:TemplateField HeaderText="Action" FooterText="Action">
                                    <ItemTemplate>
                                        <asp:Button Text="Edit" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" CssClass="form-edit-btn" />
                                        <asp:Button Text="Delete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return isDelete();" CssClass="form-delet-btn" />
                                        <%--<asp:Button ID="btnDelete" runat="server" Text="DELETE" OnClick="btnDelete_Click" OnClientClick="return isDelete();" CssClass="btn-danger"></asp:Button>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle />
                            <EmptyDataRowStyle BackColor="Yellow" />
                            <PagerStyle />
                            <SelectedRowStyle />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <FooterStyle />
                            <HeaderStyle />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <asp:Button ID="btn_Continue" runat="server" Text="Continue" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
