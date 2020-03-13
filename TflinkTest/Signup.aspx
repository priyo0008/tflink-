<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="TflinkTest.Signup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                emailField.value = "";
                return false;
            }
            return true;

        }
    </script>
    <script>
           function Validate() {

            if (document.getElementById("<%=txt_Username.ClientID%>").value == "") {
                alert("Enter Username !");
                document.getElementById("<%=txt_Username.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Emailid.ClientID%>").value == "") {
                alert("Enter Email id !");
                document.getElementById("<%=txt_Emailid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Password.ClientID%>").value == "") {
                alert("Enter Password !");
                document.getElementById("<%=txt_Password.ClientID%>").focus();
                return false;
            }
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <section id="home_signup">
        <h2>Sign up here</h2>
    </section>--%>
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row justify-content-center text-center mb-5" style="margin-top: 10px;">
            <div class="col-7 text-center mb-5">
                <section id="home_signup">
                    <h2>Sign up here</h2>
                </section>
            </div>
        </div>
        <div class="row" style="margin-top: 40px; margin-bottom: 40px;">
            <div class="col-md-6">
                <img src="images/login-image.png" />
            </div>
            <div class="col-md-6">
                <asp:Panel runat="server" DefaultButton="btn_Save">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            Username :
                <asp:TextBox runat="server" placeholder="Enter Username" AutoPostBack="true" OnTextChanged="txt_Username_TextChanged" CssClass="form-control" ID="txt_Username"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionListsubcatagory" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_Username">
                            </cc1:AutoCompleteExtender>
                            Email id :<asp:TextBox runat="server" placeholder="Enter Email id" onkeyup="ValidateEmail()" onblur="validateEmail(this);" AutoPostBack="true" OnTextChanged="txt_Emailid_TextChanged" CssClass="form-control" ID="txt_Emailid"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetCompletionListEmail" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" FirstRowSelected="false" TargetControlID="txt_Emailid">
                            </cc1:AutoCompleteExtender>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    Password :<asp:TextBox runat="server" placeholder="Enter Password" type="password" CssClass="form-control" ID="txt_Password"></asp:TextBox>
                    <br />
                    <asp:Button runat="server" class="btn btn-primary" OnClientClick="return Validate();" OnClick="btn_Save_Click" Text="Create User" ID="btn_Save" />
                    <p>
                        Your email is required so we can communicate with you. Please be assured that it will never be shown to anyone.
                    </p>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
