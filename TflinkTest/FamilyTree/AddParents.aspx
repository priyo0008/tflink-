<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddParents.aspx.cs" Inherits="TflinkTest.FamilyTree.AddParents" %>

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
        <script> 
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="modal-body">
                        <div class="row">
                            <div runat="server" id="Bindnotice">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                Type  :
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_Parentstype">
                                    <asp:ListItem>Select Type</asp:ListItem>
                                    <asp:ListItem>Mother</asp:ListItem>
                                    <asp:ListItem>Father</asp:ListItem>
                                    <asp:ListItem>Guardian</asp:ListItem>
                                    <asp:ListItem>Stepmother</asp:ListItem>
                                    <asp:ListItem>Stepfather</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Gender<span style="color: Red; font-weight: bold; font-size: small">*</span> :
                            <asp:DropDownList ID="ddl_gender" CssClass="form-control" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                Phone : 
                            <asp:TextBox ID="txt_Phonenmb" AutoPostBack="true" OnTextChanged="txt_Phonenmb_TextChanged" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                First Name :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:TextBox ID="txt_Fname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                Email Id : 
                            <asp:TextBox ID="txt_Email" onkeyup="ValidateEmail()" onblur="validateEmail(this);" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Last Name :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:TextBox ID="txt_Lname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                Address : 
                            <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                City/Town : 
                            <asp:TextBox ID="txt_City" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                Region/State : 
                            <asp:TextBox ID="txt_State" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Date of Birth(DOB) : 
                            <asp:TextBox ID="txt_Dob" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txt_Dob" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                <asp:CheckBox ID="chb_dobCirca" runat="server" />Circa
                            </div>
                            <div class="col-md-6">
                                Country :<span style="color: Red; font-weight: bold; font-size: small">*</span> : 
                            <asp:DropDownList ID="ddl_Country" CssClass="form-control" runat="server">
                                <asp:ListItem>India</asp:ListItem>
                                <asp:ListItem>USA</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Birth Location : 
                            <asp:TextBox ID="txt_Birthloc" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                Date of Death(DOD) : 
                            Currently Alive 
                            <asp:Button ID="btn_Edit" class="btn btn-primary" runat="server" Text="Edit" />
                                <asp:TextBox ID="txt_Dod" Visible="false" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txt_frmDT_CalendarExtender" runat="server" Enabled="True" TargetControlID="txt_Dod" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                <div runat="server" id="divcreca" visible="false">
                                    <asp:CheckBox ID="chb_dodCirca" Visible="false" runat="server" />Circa
                                </div>
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
                        <asp:Button ID="btn_save" class="btn btn-primary" OnClick="btn_save_Click" OnClientClick="return Validate();" runat="server" Text="Save" />
                    </div>
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
