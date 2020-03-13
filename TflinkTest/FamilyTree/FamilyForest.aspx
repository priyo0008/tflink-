<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="FamilyForest.aspx.cs" Inherits="TflinkTest.FamilyTree.FamilyForest" %>
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
                border-top: 2px solid darkgray;
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
            border: 1px solid darkgray;
            padding: 5px 10px;
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
                background: #c8e4f8;
                color: #000;
                border: 1px solid #94a0b4;
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
    <div class="container">
        <div class="col-md-12">
            <div class="row">
                <h2>This page is Under Construction.</h2> 
                <hr />
                <p>On this page you will be able to see the links between different Family Trees.</p>
            </div>
        </div>
        <div>
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
                                       <asp:ImageButton ID="ImageButton2" ImageUrl="images/greenimg2.jpg"  width="30" height="30"  AlternateText="no" OnClientClick="return ShowModalPopupparentedit()" runat="server" />
                                <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" BehaviorID="parentedit" runat="server" PopupControlID="paneleditparents"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="paneleditparents">
                                </cc1:ModalPopupExtender>
                                </a>
                                <a href="#">
                                    <%--<img src="#" width="30" height="30" />--%>
                                         <%-- <asp:Button ID="btneditparent" runat="server" class="btn btn-info" OnClientClick="return ShowModalPopupparentedit()"
                                    Text="Edit" />--%>
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/image4.png"  width="30" height="30"  AlternateText="no" OnClientClick="return ShowModalPopupparenteditphoto()" runat="server" />
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
        </div>
    </div>
</asp:Content>
