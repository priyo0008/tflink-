<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UnlockCust.aspx.cs" Inherits="TflinkTest.Admin.UnlockCust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%--  <style>
        .invisibletext {
            visibility: hidden;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Update Locker</h3>
                    </div>
                    <div class="form-horizontal">
                        <div class="card-body">
                            <div class="form-group row" style="width:auto;">
                                <asp:HiddenField ID="hdn_id" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
                                            <p id="myrequest" style="color: red" runat="server"></p>
                                            <asp:GridView ID="Grd_Custlock"  runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="FamilyId" HeaderText="Request From" FooterText="Request From" />
                                                    <asp:BoundField DataField="MemberId" HeaderText="Member Id" FooterText="Member Id" />
                                                    <asp:BoundField DataField="FirstName" HeaderText="Name" FooterText="Name" />
                                                    <asp:BoundField DataField="Contact" HeaderText="Contact" FooterText="Contact" />
                                                    <asp:BoundField DataField="Dob" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Dob" FooterText="Dob" />
                                                    <asp:TemplateField HeaderText="Accept Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn_live" runat="server" CommandName="Active" ForeColor='<%# Eval("Wrngpswatmpt").ToString()=="Lock"?System.Drawing.Color.Red:System.Drawing.Color.Green %>' OnClick="lnkbtn_live_Click" class="btn btn-default" Text='<%# Eval("Wrngpswatmpt").ToString()=="Lock"? "Locked":"Lock" %>'> </asp:LinkButton>
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                    <div class="card-footer">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
