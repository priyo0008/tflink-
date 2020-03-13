<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="ExistingAllData.aspx.cs" Inherits="TflinkTest.FamilyTree.ExistingAllData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="images/loader2.gif" Height="150" Width="150" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 80%; margin-left: auto; margin-right: auto;">
                <asp:DropDownList ID="ddl_Getdata" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddl_Getdata_TextChanged" runat="server">
                    <asp:ListItem>FL_Names</asp:ListItem>
                    <asp:ListItem>FL_Parents</asp:ListItem>
                    <asp:ListItem>FL_Persons</asp:ListItem>
                    <asp:ListItem>FL_Projects</asp:ListItem>
                    <asp:ListItem>FL_Spouses</asp:ListItem>
                    <asp:ListItem>Userid-Password</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width: 80%; margin-left: auto; margin-right: auto;">
                <h2>Existing Data </h2>
                <asp:GridView ID="Grd_Pofile" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="true" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <Columns>
                            <asp:TemplateField HeaderText="SlNo.">
                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
