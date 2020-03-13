<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPage.aspx.cs" Inherits="TflinkTest.FamilyTree.NewPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal fade" id="myModalspouse" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-toggle="modal" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%-- <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <h4 class="modal-title" id="myModalLabelspouse">Add Spouse for Profile Kapriel Armutlu</h4>
                        <asp:Label ID="Label1" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <%--       <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                Gender<span style="color: Red; font-weight: bold; font-size: small">*</span> :
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            If the person already exists in the system, enter the name here and click on Search
                        </div>
                        <div class="row">
                            <asp:TextBox ID="txt_Search" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" Text="Search" />
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                OR if the person is not yet set up<asp:Button ID="btn_Addnew" runat="server" Text="Click to add new person" />
                            </div>
                        </div>
                        <div class="row">
                            <h4>Spouse</h4>
                        </div>
                        <div class="row">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        <%--<button type="button" class="btn btn-primary">Save</button>--%>
                        <asp:Button ID="Button2" class="btn btn-primary" OnClientClick="return Validate();" runat="server" Text="Continue" />
                        <asp:Button ID="btn_add" runat="server" Text="Add" />
                    </div>
                    <%--     </ContentTemplate>
                </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
