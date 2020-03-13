<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdateContact.aspx.cs" Inherits="TflinkTest.Admin.UpdateContact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Update Contact Page</h3>
                    </div>
                    <!-- /.card-header -->
                    <!-- form start -->
                    <div class="form-horizontal">
                        <script type="text/javascript">
                                    function HideLabel() {
                                        var seconds = 5;
                                        setTimeout(function () {
                                            document.getElementById("<%=lbl_Success.ClientID %>").style.display = "none";
                                        }, seconds * 1000);
                                    };
                                    function HideLabel1() {
                                        var seconds = 5;
                                        setTimeout(function () {
                                            document.getElementById("<%=lbl_Update.ClientID %>").style.display = "none";
                                        }, seconds * 1000);
                                    };
                                    function HideLabel2() {
                                        var seconds = 5;
                                        setTimeout(function () {
                                            document.getElementById("<%=lbl_error.ClientID %>").style.display = "none";
                                        }, seconds * 1000);
                                    };
                                </script>
                        <div class="card-body">
                            <div class="form-group row">
                                <asp:HiddenField ID="hdn_id" runat="server" />
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Contact Page Content</label>
                                <div class="col-sm-10">
                                    <%--<input type="email" class="form-control" id="inputEmail3" placeholder="Email" />--%>
                                    <asp:TextBox ID="txt_ContactContent" runat="server" TextMode="MultiLine" class="form-control" Rows="10"></asp:TextBox>
                                    <asp:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="txt_ContactContent" EnableSanitization="false" runat="server"></asp:HtmlEditorExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="offset-sm-2 col-sm-10">
                                    <div class="form-check">
                                                <asp:Button ID="btn_save" class="btn btn-info float-center" OnClick="btn_save_Click" runat="server" Text="Save" />
                                                <asp:Label ID="lbl_Success" ForeColor="Green" Font-Bold="true" Text="Content has been submitted successfully." runat="server" Visible="false" />
                                                <asp:Label ID="lbl_Update" ForeColor="Green" Font-Bold="true" Text="Content has been updated uuccessfully." runat="server" Visible="false" />
                                                <asp:Label ID="lbl_error" ForeColor="Red" Font-Bold="true" Text="Something went wrong" runat="server" Visible="false" />
                                            </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            <%--<button type="submit" class="btn btn-default float-right">Cancel</button>--%>
                        </div>
                        <!-- /.card-footer -->
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
