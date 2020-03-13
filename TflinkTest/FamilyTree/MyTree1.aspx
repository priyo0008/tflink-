<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="MyTree1.aspx.cs" Inherits="TflinkTest.FamilyTree.MyTree1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        html, body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
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
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowModalPopupchld() {
            $find("mpechld").show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowModalPopupParents() {
            $find("mpeParents").show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowModalPopupparentedit() {
            $find("parentedit").show();
            return false;
        }
    </script>
    <script type="text/javascript">
        function ShowModalPopupparenteditphoto() {
            $find("parenteditphoto").show();
            return false;
        }
    </script>

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
    </script>
    <style>
        /*Now the CSS*/
        * {
            margin: 0;
            padding: 0;
        }

        .tree ul {
            padding-top: 20px;
            position: relative;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }

        .tree li {
            float: left;
            text-align: center;
            list-style-type: none;
            position: relative;
            padding: 20px 5px 0 5px;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }

            /*We will use ::before and ::after to draw the connectors*/

            .tree li::before, .tree li::after {
                content: '';
                position: absolute;
                top: 0;
                right: 50%;
                border-top: 3px solid darkgray;
                width: 50%;
                height: 20px;
            }

            .tree li::after {
                right: auto;
                left: 50%;
                border-left: 1px solid darkgray;
            }

            /*We need to remove left-right connectors from elements without 
any siblings*/
            .tree li:only-child::after, .tree li:only-child::before {
                display: none;
            }

            /*Remove space from the top of single children*/
            .tree li:only-child {
                padding-top: 0;
            }

            /*Remove left connector from first child and 
right connector from last child*/
            .tree li:first-child::before, .tree li:last-child::after {
                border: 0 none;
            }
            /*Adding back the vertical connector to the last nodes*/
            .tree li:last-child::before {
                border-right: 1px solid darkgray;
                border-radius: 0 5px 0 0;
                -webkit-border-radius: 0 5px 0 0;
                -moz-border-radius: 0 5px 0 0;
            }

            .tree li:first-child::after {
                border-radius: 5px 0 0 0;
                -webkit-border-radius: 5px 0 0 0;
                -moz-border-radius: 5px 0 0 0;
            }

        /*Time to add downward connectors from parents*/
        .tree ul ul::before {
            content: '';
            position: absolute;
            top: 0;
            left: 50%;
            border-left: 1px solid darkgray;
            width: 0;
            height: 20px;
        }

        .tree li div {
            border: 2px solid #b2a4a4;
            padding: 1px 1px;
            text-decoration: none;
            color: black;
            font-family: arial, verdana, tahoma;
            font-size: 15px;
            font: bold;
            display: inline-block;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }

            /*Time for some hover effects*/
            /*We will apply the hover effect the the lineage of the element also*/
            .tree li div:hover, .tree li div:hover + ul li div {
                background: cornsilk;
                color: #000;
                border: 8px solid white;
            }
                /*Connector styles on hover*/
                .tree li div:hover + ul li::after,
                .tree li div:hover + ul li::before,
                .tree li div:hover + ul::before,
                .tree li div:hover + ul ul::before {
                    border-color: #94a0b4;
                }

        span.box-tree-left {
            width: 79%;
            float: left;
        }

        span.box-tree-right {
            float: right;
            width: 19%;
        }
        /*.box-main-tree {
    width: 100%;
    display: inline-block;
}*/

        /*Thats all. I hope you enjoyed it.
Thanks :)*/
    </style>
    <style>
        .settings h1 {
            margin: 0px 0 20px 0;
            border-bottom: 1px solid silver;
            padding: 0 0 10px 0;
            font-size: 29px;
            letter-spacing: 0;
        }

        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Add Your Spouse Profile 
        </div>
        <div class="body">
            <iframe id="iFramePersonal" src="Add_Spouses.aspx" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btnClose" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlPopUpchld" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Add Your Child Profile 
        </div>
        <div class="body">
            <iframe id="iFramePersonalchld" src="Add_Child.aspx" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btn_closechld" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlPopUpparents" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Add Your Parents Profile 
        </div>
        <div class="body">
            <iframe id="iFramePersonalParent" src="Add_Parents.aspx" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btnCloseparents" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>
    <asp:Panel ID="paneleditparentsphoto" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            View Profile 
        </div>
        <div class="body">
            <iframe id="iFrameEditParenteditphoto" src="Edit_Parents.aspx?target=photo" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btncloseeditprntphoto" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>
    <asp:Panel ID="paneleditparents" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            View Profile 
        </div>
        <div class="body">
            <iframe id="iFrameEditParentedit" src="Edit_Parents.aspx" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btncloseeditprnt" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>


    <div runat="server" id="scriptid">
    </div>
    <%--  <script type="text/javascript">
        function ShowModalPopup() {
            $find("myModal").show();
            return false;
        }
    </script>--%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-toggle="modal" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%-- <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                    <h4 class="modal-title" id="myModalLabel">Enter Your Profile</h4>
                    <asp:Label ID="lbl_error" ForeColor="Red" runat="server"></asp:Label>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            Gender<span style="color: Red; font-weight: bold; font-size: small">*</span> :
                            <asp:DropDownList ID="ddl_gender" CssClass="form-control" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                <ContentTemplate>
                                    Phone : 
                            <asp:TextBox ID="txt_Phonenmb" AutoPostBack="true" OnTextChanged="txt_Phonenmb_TextChanged" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            Birth Location : 
                            <asp:TextBox ID="txt_Birthloc" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                    <asp:Button ID="btn_save" class="btn btn-primary" OnClientClick="return Validate();" OnClick="btn_save_Click" runat="server" Text="Save" />
                </div>

            </div>
        </div>
    </div>

    <div>
        <h2>Tree For<p runat="server" id="Treefor"></p>
        </h2>
    </div>
    <hr />
    <h4>Click on any
        <img src="images/tree2.jpg" width="30" height="30" />
        to re-draw the family tree for that person.  Click on any Person's name or on the
        <img src="images/image4.png" width="30" height="30" />
        to see their profile detail.
        <a href="MyTree1.aspx">
            <button type="button" style="margin-left: auto; margin-right: 30px; display: block;" class="btn btn-info">Refresh Tree</button></a>
    </h4>
    <div style="background-color: #d8e9f7; height: 50px; width: 100%;">
        <%-- <button type="button" style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-primary">+Add Parents</button>--%>
        <%-- <a href="#" onclick="window.open('Edit_Parents.aspx?Memidview=vvv','popup_window','height=500px,width=500px,scrollbars=1');"><img src='images/image4.png' width='30' height='30' /></a>--%>
        <asp:Button ID="btn_Addparents" runat="server" Style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-info" OnClientClick="return ShowModalPopupParents()"
            Text="+Add Parents" />
        <asp:LinkButton ID="lnkbtn_btn" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpeParents" runat="server" PopupControlID="pnlPopUpparents"
            TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btnCloseparents">
        </cc1:ModalPopupExtender>
        <a class="thickbox"
            title="Add Child" runat="server" id="lnkAddChild"
            href="popuptest.aspx?From=FamilyTree&<%--TB_iframe--%>=true&height=560&width=950&modal=true">
            <%--  <asp:Button ID="Button1" runat="server" Text="Add" />--%>
        </a>
    </div>

    <div class="tree" style="width: auto; margin-left: auto; margin-right: auto; display: table;">
        <ul>
            <li runat="server" id="bindmother">
                <%--     <h3>Mother</h3>
              <%--  <div runat="server"  id="bindmother"> 
                </div>--%>
                <%-- <div class="box-main-tree">
                    <span class="box-tree-left">
                        <a href="#">Mother Name</a>
                        <p>Montreal CA</p>
                        <p>M 45-65 years</p>
                    </span>
                    <span class="box-tree-right">
                        <a href="#">
                            <img src="images/tree2.jpg" width="30" height="30" /></a>
                        <a href="#">
                            <img src="images/greenimg2.jpg" width="30" height="30" /></a>
                        <a href="#">
                            <img src="images/image4.png" width="30" height="30" /></a>
                    </span>
                </div>--%>
            </li>
            <li>
                <ul runat="server">
                    <li runat="server">
                        <div class="box-main-tree">
                            <span class="box-tree-left">
                                <p runat="server" id="MailName"></p>
                                <p runat="server" id="PLocation"></p>
                                <p runat="server" id="Years"></p>
                            </span>
                            <span class="box-tree-right">
                                <a href="#">
                                    <img src="images/tree2.jpg" width="30" height="30" /></a>
                                <a href="#">
                                    <%--<img src="images/greenimg2.jpg" width="30" height="30" />--%>
                                    <asp:ImageButton ID="ImageButton2" ImageUrl="images/greenimg2.jpg" Width="30" Height="30" AlternateText="no" OnClientClick="return ShowModalPopupparentedit()" runat="server" />
                                    <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" BehaviorID="parentedit" runat="server" PopupControlID="paneleditparents"
                                        TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="paneleditparents">
                                    </cc1:ModalPopupExtender>
                                </a>
                                <a href="#">
                                    <%--<img src="#" width="30" height="30" />--%>
                                    <%-- <asp:Button ID="btneditparent" runat="server" class="btn btn-info" OnClientClick="return ShowModalPopupparentedit()"
                                    Text="Edit" />--%>
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/image4.png" Width="30" Height="30" AlternateText="no" OnClientClick="return ShowModalPopupparenteditphoto()" runat="server" />
                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="parenteditphoto" runat="server" PopupControlID="paneleditparentsphoto"
                                        TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="paneleditparentsphoto">
                                    </cc1:ModalPopupExtender>
                                </a>
                            </span>
                        </div>
                        <ul runat="server">
                            <li runat="server">
                                <h3>Spouse</h3>
                                <asp:Button ID="btnShow" runat="server" class="btn btn-info" OnClientClick="return ShowModalPopup()"
                                    Text="+Add" />
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="mpShow" BehaviorID="mpe" runat="server" PopupControlID="pnlPopUp"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <ul runat="server" id="Bind_Spouse">
                                </ul>
                            </li>
                            <li>
                                <h3>Children</h3>
                                <%--<asp:Button ID="btn_Addchld" runat="server" class="btn btn-info" Text="+Add" />--%>
                                <asp:Button ID="btn_Addchld" runat="server" class="btn btn-info" OnClientClick="return ShowModalPopupchld()"
                                    Text="+Add" />
                                <asp:LinkButton ID="lnkchld" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpechld" runat="server" PopupControlID="pnlPopUpchld"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btn_closechld">
                                </cc1:ModalPopupExtender>
                                <ul runat="server" id="Bind_Child">
                                </ul>
                            </li>
                            <li runat="server" id="siblinglitag" visible="false">
                                <h3>Sibling</h3>
                                <%-- <asp:Button ID="btn_Addsib" class="btn btn-info" runat="server" Text="+Add" />--%>
                                <ul runat="server" id="Bind_Siblings" visible="false">
                                    <%--<li>
                                        <div class="box-main-tree">
                                            <span class="box-tree-left">
                                                <a href="#">Sibling 1</a>
                                                <p>Montreal CA</p>
                                                <p>M 45-65 years</p>
                                            </span>
                                            <span class="box-tree-right">
                                                <a href="#">
                                                    <img src="images/tree2.jpg" width="30" height="30" /></a>
                                                <a href="#">
                                                    <img src="images/greenimg2.jpg" width="30" height="30" /></a>
                                                <a href="#">
                                                    <img src="images/image4.png" width="30" height="30" /></a>
                                            </span>
                                        </div>
                                    </li>--%>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li runat="server" id="bindfather">
                <%--      <h3>Father</h3>
      <div class="box-main-tree">
                    <span class="box-tree-left">
                        <a href="#">Father Name</a>
                        <p>Montreal CA</p>
                        <p>M 45-65 years</p>
                    </span>
                    <span class="box-tree-right">
                        <a href="#">
                            <img src="images/tree2.jpg" width="30" height="30" /></a>
                        <a href="#">
                            <img src="images/greenimg2.jpg" width="30" height="30" /></a>
                        <a href="#">
                            <img src="images/image4.png" width="30" height="30" /></a>
                    </span>
                </div>--%>
            </li>
        </ul>
    </div>
    <div style="margin-bottom: 700px;">
    </div>
</asp:Content>
