<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdateSearch.aspx.cs" Inherits="TflinkTest.Admin.UpdateSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewFile() {
            var preview = document.querySelector('#<%=img_imagesearch.ClientID %>');
            var file = document.querySelector('#<%=flud_image.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }
            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Update Contact Page</h3>
                    </div>
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
                                <div class="offset-sm-1 col-sm-11">
                                    <asp:FileUpload ID="flud_image" onchange="previewFile()" runat="server" />
                                    <asp:Image ID="img_imagesearch" Height="100" Width="100" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="offset-sm-1 col-sm-11">
                                    <div class="form-check">
                                        <asp:Button ID="btn_Upload" class="btn btn-info float-center" OnClick="btn_Upload_Click" runat="server" Text="Save" />
                                        <asp:Label ID="lbl_Success" ForeColor="Green" Font-Bold="true" Text="Image has been submitted successfully." runat="server" Visible="false" />
                                        <asp:Label ID="lbl_Update" ForeColor="Green" Font-Bold="true" Text="Image has been updated uuccessfully." runat="server" Visible="false" />
                                        <asp:Label ID="lbl_error" ForeColor="Red" Font-Bold="true" Text="Something went wrong" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
