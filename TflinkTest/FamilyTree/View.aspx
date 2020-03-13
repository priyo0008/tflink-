<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="TflinkTest.FamilyTree.View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .edit-parent .modal-dialog {
            width: 90%;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 80%;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
                height: 80%;
                background-color: floralwhite;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td {
                text-align: left;
            }
    </style>
    <style>
        a {
            text-decoration: none;
            display: inline-block;
            padding: 8px 16px;
        }

            a:hover {
                background-color: #ddd;
                color: black;
            }

        .previous {
            background-color: #f1f1f1;
            color: black;
        }

        .next {
            background-color: #4CAF50;
            color: white;
        }

        .round {
            border-radius: 50%;
        }
    </style>
    <%--<script>
        function Validate() {

            if (document.getElementById("<%=ddl_gender.ClientID%>").value == "--Select Gender--") {
                alert("Select Gender !");
                document.getElementById("<%=ddl_gender.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Phonenmb.ClientID%>").value == "") {
                alert("Enter Phone Number !");
                document.getElementById("<%=txt_Phonenmb.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Fname.ClientID%>").value == "") {
                alert("Enter First Name !");
                document.getElementById("<%=txt_Fname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Lname.ClientID%>").value == "") {
                alert("Enter Last Name !");
                document.getElementById("<%=txt_Lname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddl_Country.ClientID%>").value == "") {
                alert("Select Country Name !");
                document.getElementById("<%=ddl_Country.ClientID%>").focus();
                return false;
            }  
        }
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false; 
            return true;
        }
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                emailField.value = "";
                return false;
            }
            return true;

        }
    </script>--%>
    <%--     <script> 
        function Validate() {

            if (document.getElementById("<%=ddl_gender.ClientID%>").value == "--Select Gender--") {
                alert("Select Gender !");
                document.getElementById("<%=ddl_gender.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Phonenmb.ClientID%>").value == "") {
                alert("Enter Phone Number !");
                document.getElementById("<%=txt_Phonenmb.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Fname.ClientID%>").value == "") {
                alert("Enter First Name !");
                document.getElementById("<%=txt_Fname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_Lname.ClientID%>").value == "") {
                alert("Enter Last Name !");
                document.getElementById("<%=txt_Lname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddl_Country.ClientID%>").value == "") {
                alert("Select Country Name !");
                document.getElementById("<%=ddl_Country.ClientID%>").focus();
                return false;
            } 
        }
    </script>--%>
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script language="javascript">
        function singleRbtnSelect(chb) {
            $(chb).closest("table").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);
        }
        function CloseWindow() {
            window.close();
        }
    </script>
    <%--    <script>
        function previewFile() {
            var preview = document.querySelector('#<%=img_showimg.ClientID %>');
            var file = document.querySelector('#<%=flud_img.ClientID %>').files[0];
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
        function Validateblog() {

            if (document.getElementById("<%=img_showimg.ClientID%>").value == "") {
                alert("Select a image first !");
                document.getElementById("<%=flud_img.ClientID%>").focus();
                return false;
            }
        }
    </script>--%>
    <%--<script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=txt_msg.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:HiddenField ID="hdn_pawrd" runat="server" />
            <asp:HiddenField ID="hdn_fmlyid" runat="server" />
            <asp:HiddenField ID="hdn_id" runat="server" />
            <asp:HiddenField ID="hdn_Userid" runat="server" />
            <div class="container">
                <!-- Trigger the modal with a button -->
                <!-- Modal -->
                <div id="myModal" class="edit-parent fade in modal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Edit Profile</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div runat="server" id="Div1">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        Gender<span style="color: Red; font-weight: bold; font-size: small">*</span> :
                            <asp:DropDownList ID="ddl_gender" CssClass="form-control" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                Phone : 
                            <asp:TextBox ID="txt_Phonenmb" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        First Namepp :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:TextBox ID="txt_Fname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        Email Id : 
                            <asp:TextBox ID="txt_Email" onkeyup="ValidateEmail()" onblur="validateEmail(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        Last Name :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:TextBox ID="txt_Lname" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        Address : 
                            <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        City/Town : 
                            <asp:TextBox ID="txt_City" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        Region/State : 
                            <asp:TextBox ID="txt_State" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        Date of Birth(DOB) : 
                            <asp:TextBox ID="txt_Dob" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txt_Dob" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                        <asp:CheckBox ID="chb_dobCirca" runat="server" />Circa
                                    </div>
                                    <div class="col-md-6 form-group">
                                        Country :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:DropDownList ID="ddl_Country" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        Birth Location : 
                            <asp:TextBox ID="txt_Birthloc" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                Date of Death(DOD) : 
                           <p id="curalive" runat="server">Currently Alive </p>
                                                <asp:Button ID="btn_Edit" class="btn btn-primary" OnClick="btn_Edit_Click" runat="server" Text="Edit" />
                                                <asp:TextBox ID="txt_Dod" Visible="false" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>

                                                <cc1:CalendarExtender ID="txt_frmDT_CalendarExtender" runat="server" Enabled="True" TargetControlID="txt_Dod" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                                <div runat="server" id="divcreca" visible="false">
                                                    <asp:CheckBox ID="chb_dodCirca" Visible="false" runat="server" />Circa
                                                </div>
                                                <div runat="server" id="Deathlocdiv" visible="false">
                                                    Death Location : 
                                                            <asp:TextBox ID="txt_deathloc" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        Notes : 
                            <asp:TextBox ID="txt_notes" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                        fields with <span style="color: Red; font-weight: bold; font-size: small">*</span> must be filled in, all the rest are optional.
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                <asp:Button ID="btn_Update" class="btn btn-primary" OnClientClick="return Validate();" OnClick="Button1_Click" runat="server" Text="Update" />
                            </div>

                            <div class="modal-footer" style="text-align: center;">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
                <div id="myModal2" class="edit-parent fade in modal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Add Names</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div runat="server" id="Div2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 form-group">
                                        First Name-Know as : 
                            <asp:TextBox ID="txt_Fnameknas" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 form-group">
                                        First Name-Legal :<%--<span style="color: Red; font-weight: bold; font-size: small">*</span>--%>
                                        <asp:TextBox ID="txt_FnameLegal" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        EMiddle Name : 
                            <asp:TextBox ID="txt_Emiddlename" onkeyup="ValidateEmail()" onblur="validateEmail(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        Last Name :<%--<span style="color: Red; font-weight: bold; font-size: small">*</span>--%>
                                        <asp:TextBox ID="txt_LastName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-4 form-group">
                                        Type : 
                            <asp:DropDownList ID="ddl_Type" CssClass="form-control" runat="server">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>Birth</asp:ListItem>
                                <asp:ListItem>Changed</asp:ListItem>
                            </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 form-group">
                                        Changed Desc. : 
                              <asp:DropDownList ID="ddl_Changedec" CssClass="form-control" runat="server">
                                  <asp:ListItem>Legal Change</asp:ListItem>
                                  <asp:ListItem>By Marriage</asp:ListItem>
                                  <asp:ListItem>By Adoption</asp:ListItem>
                                  <asp:ListItem>Other</asp:ListItem>
                                  <asp:ListItem>Spelling</asp:ListItem>
                              </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 form-group">
                                        Changed Date : 
                            <asp:TextBox ID="txt_date" CssClass="form-control" autocomplete="false" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txt_date" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                        <asp:CheckBox ID="chck_cicra" runat="server" />:Circa
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                <asp:Button ID="Button2" class="btn btn-primary" OnClientClick="return Validate();" OnClick="Button2_Click" runat="server" Text="Save" />
                            </div>

                            <div class="modal-footer" style="text-align: center;">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
                <asp:HiddenField ID="hdn_FullName" runat="server" />
                <script>
                    function previewFile() {
                        var preview = document.querySelector('#<%=img_showimg.ClientID %>');
                        var file = document.querySelector('#<%=flud_img.ClientID %>').files[0];
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
                    function Validateblog() {

                        if (document.getElementById("<%=img_showimg.ClientID%>").value == "") {
                        alert("Select a image first !");
                        document.getElementById("<%=flud_img.ClientID%>").focus();
                        return false;
                    }
                }
                </script>
                <div id="myModal3" class="edit-parent fade in modal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Add Pictures</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div runat="server" id="Div5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 form-group">
                                        Description :
                            <asp:TextBox ID="txt_Desc" TextMode="MultiLine" Rows="3" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 form-group">
                                        Date :
                            <asp:TextBox ID="txt_Date1" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txt_Date1" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        Choose File: 
                            <asp:FileUpload ID="flud_img" class="file-path validate" onchange="previewFile()" runat="server" />
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Image ID="img_showimg" ImageUrl="~/FamilyTree/images/defaultimage.jpg" runat="server" Height="100" Width="100" />
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                <asp:Button ID="Button1" class="btn btn-primary" OnClientClick="return Validateblog();" OnClick="Button1_Click1" runat="server" Text="Save" />
                            </div>

                            <div class="modal-footer" style="text-align: center;">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
                <div id="myModal4" class="edit-parent fade in modal" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" style="margin-top: 30px;">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Change Password</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div runat="server" id="Div6">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4 form-group">
                                                Password :
                            <asp:TextBox ID="txt_pswrd" type="password" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="col-md-4 form-group">
                                                New Password :
                            <asp:TextBox ID="txt_Newpswrd" type="password" CssClass="form-control" runat="server"></asp:TextBox>

                                            </div>

                                            <div class="col-md-4 form-group">
                                                Confirm New Password: 
                            <asp:TextBox ID="txt_Confrmpswrd" type="password" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                        <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                        <asp:Label ID="lbl_pswrdchngmsg" runat="server"></asp:Label>
                                        <asp:Button ID="btn_Paswordchng" class="btn btn-primary" OnClientClick="return Validateblog();" OnClick="btn_Paswordchng_Click" runat="server" Text="Save" />
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <div>

                <div class="container">
                    <h2>
                        <asp:Label ID="lblMessage" ForeColor="Green" Font-Bold="true" Text="Message has been sent successfully." runat="server" Visible="false" />
                    </h2>
                    <h3>Profile of
                    <p runat="server" style="background-color: azure" id="PProfileof"></p>
                    </h3>
                    <asp:Button ID="btnClose" CssClass="settings" runat="server" Text="Close" Visible="false" CausesValidation="false" />
                </div>
                <div id="exTab1" class="container">
                    <ul class="nav nav-tabs">
                        <li>
                            <a href="MyTree1.aspx" class="previous">&laquo; Previous</a>
                        </li>
                        <li runat="server" id="profile">
                            <a href="#Profilea" data-toggle="tab">Profile</a>
                        </li>
                        <li><a href="#2a" data-toggle="tab">Names</a>
                        </li>
                        <li runat="server" id="photo"><a href="#photoa" data-toggle="tab">Pictures</a>
                        </li>
                        <li><a href="#4a" data-toggle="tab">Administrator</a>
                        </li>
                        <li>
                            <p runat="server" id="P1"></p>
                        </li>
                    </ul>

                    <div class="tab-content clearfix">
                        <%--Add parents from here by choose--%>
                        <div runat="server" id="Profilea">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="modal-body">

                                            <%--<asp:Button ID="btn_editprofl" class="btn btn-info" runat="server" Text="Edit Profile" />--%>
                                            <div runat="server" id="editbtn" visible="false">
                                                Profile : 
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Edit Profile </button>
                                            </div>
                                            <br>
                                            <table class="table table-bordered" style="margin-top: 30px;">
                                                <tr>
                                                    <td>
                                                        <p runat="server" id="PGender"></p>
                                                    </td>
                                                    <td>
                                                        <p runat="server" id="PCity"></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p runat="server" id="Pfname"></p>
                                                    </td>
                                                    <td>
                                                        <p runat="server" id="PRegion"></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p runat="server" id="Plastname"></p>
                                                    </td>
                                                    <td>
                                                        <p runat="server" id="Pcountry"></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p runat="server" id="Pdob"></p>
                                                    </td>
                                                    <td>
                                                        <p runat="server" id="Pdod"></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p runat="server" id="Pbirthloc"></p>
                                                    </td>
                                                    <td>
                                                        <p runat="server" id="Pdeathloc"></p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--Add New Member--%>
                        <div class="tab-pane" id="2a">

                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="modal-body">
                                            <%--   Add Name<h3 runat="server" id="Addname"></h3>
                                        :--%>
                                            <div runat="server" id="Div4" visible="false">
                                                Name List : 
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal2">+Add</button>
                                            </div>

                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" style="margin-top: 30px;">
                                                <ContentTemplate>

                                                    <%--<asp:Button ID="btn_Addname" class="btn btn-info" data-toggle="modal" data-target="#myModal2" runat="server" Text="+Add" />--%>
                                                    <asp:GridView ID="Grd_Names" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" OnRowEditing="Grd_Names_RowEditing" OnRowDeleting="Grd_Names_RowDeleting" OnRowCancelingEdit="Grd_Names_RowCancelingEdit" OnRowUpdating="Grd_Names_RowUpdating" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                                                                    <asp:Label CssClass="invisibletext" ID="lbl_ID" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--   <asp:TemplateField>
                                                    <ItemTemplate>
                                         <asp:LinkButton ID="lnkbtn_NameEdit" runat="server" CommandName="Active" OnClick="lnkbtn_NameEdit_Click"  class="btn btn-default" Text='Edit'> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>

                                                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" class="btn btn-success" CommandName="Edit" />
                                                                    <%--<asp:Image ID="Image3" ImageUrl="~/FamilyTree/images/deleteimg.jpg" Height="20" CommandName="Delete" runat="server" />--%>
                                                                    <asp:Button ID="btn_Delete" runat="server" class="btn btn-danger" Text="Delete" CommandName="Delete" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" class="btn btn-success" CommandName="Update" />
                                                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-warning" Text="Cancel" CommandName="Cancel" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="First name known as">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Fnamekwas" runat="server" Text='<%#Eval("FnameKwas") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Fnameknas" runat="server" Text='<%#Eval("FnameKwas") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="First name legal">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_FnameLegal" runat="server" Text='<%#Eval("FnameLegal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_FnameLegal" runat="server" Text='<%#Eval("FnameLegal") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Middle name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_MiddleName" runat="server" Text='<%#Eval("MiddleName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_MiddleName" runat="server" Text='<%#Eval("MiddleName") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Lastname" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Lastname" runat="server" Text='<%#Eval("LastName") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--                                                <asp:BoundField DataField="FnameKwas" HeaderText="First name known as" FooterText="First name known as" />
                                                <asp:BoundField DataField="FnameLegal" HeaderText="First name legal" FooterText="First name legal" />
                                                <asp:BoundField DataField="MiddleName" HeaderText="Middle name" FooterText="Middle name" />
                                                <asp:BoundField DataField="LastName" HeaderText="Last name" FooterText="Last name" />--%>
                                                            <%--<asp:BoundField DataField="Type" HeaderText="Type" FooterText="Type" /> --%>
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

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="modal-footer">
                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                            <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                            <%-- <asp:Button ID="Button1" class="btn btn-primary" OnClientClick="return Validate();" runat="server" Text="Save" />--%>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <section id="home_signup"></section>
                        <div runat="server" id="photoa">

                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="modal-body">
                                            <div runat="server" id="Div3" visible="false">
                                                Tagged  Pictures : 
                                       
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal3">+Add</button>
                                                <p>
                                                    <span runat="server" id="fullname2"></span>is tagged in the picture(s) below.<%-- Click on any picture for a larger image and all its details.</p>--%>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" style="margin-top: 30px;">
                                                        <ContentTemplate>

                                                            <%--<asp:Button ID="btn_Addname" class="btn btn-info" data-toggle="modal" data-target="#myModal2" runat="server" Text="+Add" />--%>
                                                            <asp:GridView ID="grd_Picture" runat="server" CssClass="table table-bordered text table-striped" ShowFooter="True" AutoGenerateColumns="False" OnRowEditing="Grd_Names_RowEditing" OnRowDeleting="Grd_Names_RowDeleting" OnRowCancelingEdit="Grd_Names_RowCancelingEdit" OnRowUpdating="Grd_Names_RowUpdating" HeaderStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label CssClass="invisibletext" ID="lbl_ID" Visible="false" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Image">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="img_images" runat="server" Height="100" ImageUrl='<%#Eval("Photo") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Fnamekwas" runat="server" Text='<%#Eval("Descr") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <%--    <asp:BoundField DataField="FnameKwas" HeaderText="First name known as" FooterText="First name known as" />
                                                <asp:BoundField DataField="FnameLegal" HeaderText="First name legal" FooterText="First name legal" />
                                                <asp:BoundField DataField="MiddleName" HeaderText="Middle name" FooterText="Middle name" />
                                                <asp:BoundField DataField="LastName" HeaderText="Last name" FooterText="Last name" />--%>
                                                                    <%--<asp:BoundField DataField="Type" HeaderText="Type" FooterText="Type" /> --%>
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

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                            <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                            <%-- <asp:Button ID="Button1" class="btn btn-primary" OnClientClick="return Validate();" runat="server" Text="Save" />--%>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="tab-pane" id="4a">

                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="modal-body" runat="server" id="passwordchng" visible="false">
                                            <p runat="server" id="Changemsg"></p>
                                            User Type:	Administrator 
                                        <br />
                                            Username:<asp:Label ID="lbl_Username" runat="server"></asp:Label>
                                            <br />
                                            Password:	**********  
                                       <%-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal3">+Add</button>--%>
                                            <a href="#" data-toggle="modal" data-target="#myModal4">[Click here to change] </a>

                                        </div>
                                        <div class="modal-footer">
                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                            <%--<button type="button" class="btn btn-primary">Save</button>--%>
                                            <%-- <asp:Button ID="Button1" class="btn btn-primary" OnClientClick="return Validate();" runat="server" Text="Save" />--%>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Bootstrap core JavaScript
    ================================================== -->
                <!-- Placed at the end of the document so the pages load faster -->
                <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
                <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
                <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
            </div>
        </div>
    </form>
</body>
</html>
