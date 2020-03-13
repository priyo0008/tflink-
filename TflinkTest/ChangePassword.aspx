<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TflinkTest.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row justify-content-center text-center mb-5" style="margin-top: 30px;">
            <div class="col-7 text-center mb-5">
                <h2>Change Password</h2>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row" style="margin-top: 40px; margin-bottom: 40px;">
                    <div class="col-md-6">
                        <asp:Image ID="Image1" Width="100%" ImageUrl="FamilyTree/images/chanepswimg.png" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <div runat="server" id="searchpart">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    Password :
                <asp:TextBox runat="server" autocomplete="off" placeholder="Enter Password" CssClass="form-control" ID="txt_Password"></asp:TextBox>

                                    <asp:Label ID="lbl_oldpaswrd" runat="server" ForeColor="Red"></asp:Label><br />
                                    New Password :<asp:TextBox runat="server" autocomplete="off" placeholder="Enter New Password" CssClass="form-control" ID="txt_Newpassword"></asp:TextBox>

                                    <asp:Label ID="lbl_Newpswrd" runat="server" ForeColor="Red"></asp:Label><br />
                                    Confirm   New Password :<asp:TextBox runat="server" autocomplete="off" placeholder="Enter New Password" CssClass="form-control" ID="txt_connewpswrd"></asp:TextBox>

                                    <asp:Label ID="lbl_connewpswrd" runat="server" ForeColor="Red"></asp:Label>
                                    <br />
                                    <asp:Button runat="server" class="btn btn-primary" OnClick="btn_Save_Click" Text="Change Password" ID="btn_Save" />
                                    <asp:Button ID="btn_cancel" OnClick="btn_cancel_Click" class="btn btn-danger" runat="server" Text="Cancel" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top:50px;">
                    <div class="col-md-12">
                        <div id="showpart" runat="server" visible="false">
                            <asp:Button ID="btn_search" class="btn btn-primary" runat="server" Text="Start a new search" />
                            <p>This list does not include any persons under 18 years of age.</p>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 50px;">
                    <div class="col-md-6">
                        <p id="perdonfound" runat="server"></p>
                    </div>
                    <div class="col-md-6">
                        <p runat="server" id="Administrator"></p>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
