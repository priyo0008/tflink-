<%@ Page Title="" Language="C#" MasterPageFile="~/FamilyTree/Familymaster.Master" AutoEventWireup="true" CodeBehind="MyTree2.aspx.cs" Inherits="TflinkTest.FamilyTree.MyTree2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p {
            padding-top: 20px;
            padding-left: 300px;
            border-style: solid;
            border-width: 2px;
        }

        a {
            margin-top: 50px;
        }

        * {
            margin: auto;
            padding: auto;
        }

        .tree ul {
            padding-top: 20px;
            position: relative;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            transition: all 0.5s;
        }

        .tree li {
            float: left;
            text-align: center;
            list-style-type: none;
            position: relative;
            padding: 20px 5px 0 5px;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            transition: all 0.5s;
        }
            /*We will use ::before and ::after to draw the connectors*/

            .tree li::before,
            .tree li::after {
                content: '';
                position: absolute;
                top: 0;
                right: 50%;
                border-top: 1px solid #ccc;
                width: 50%;
                height: 45px;
                z-index: -1;
            }

            .tree li::after {
                right: auto;
                left: 50%;
                border-left: 1px solid #ccc;
            }


            /*We need to remove left-right connectors from elements without 
			any siblings*/

            .tree li:only-child::after,
            .tree li:only-child::before {
                display: none;
            }


            /*Remove space from the top of single children*/

            .tree li:only-child {
                padding-top: 0;
            }


            /*Remove left connector from first child and 
			right connector from last child*/

            .tree li:first-child::before,
            .tree li:last-child::after {
                border: 0 none;
            }


            /*Adding back the vertical connector to the last nodes*/

            .tree li:last-child::before {
                border-right: 1px solid #ccc;
                border-radius: 0 5px 0 0;
                -webkit-transform: translateX(1px);
                -moz-transform: translateX(1px);
                transform: translateX(1px);
                -webkit-border-radius: 0 5px 0 0;
                -moz-border-radius: 0 5px 0 0;
                border-radius: 0 5px 0 0;
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
            top: -12px;
            left: 50%;
            border-left: 1px solid #ccc;
            width: 0;
            height: 32px;
            z-index: -1;
        }

        .tree li a {
            border: 1px solid #ccc;
            padding: 5px 10px;
            text-decoration: none;
            color: #666;
            font-family: arial, verdana, tahoma;
            font-size: 11px;
            display: inline-block;
            background: white;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            transition: all 0.5s;
        }

            .tree li a + a {
                margin-left: 20px;
                position: relative;
            }

                .tree li a + a::before {
                    content: '';
                    position: absolute;
                    border-top: 1px solid #ccc;
                    top: 50%;
                    left: -21px;
                    width: 20px;
                }


            /*Time for some hover effects*/


            /*We will apply the hover effect the the lineage of the element also*/

            .tree li a:hover,
            .tree li a:hover ~ ul li a {
                background: #c8e4f8;
                color: #000;
                border: 1px solid #94a0b4;
            }


                /*Connector styles on hover*/

                .tree li a:hover ~ ul li::after,
                .tree li a:hover ~ ul li::before,
                .tree li a:hover ~ ul::before,
                .tree li a:hover ~ ul ul::before {
                    border-color: #94a0b4;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left: 200px;">


        <div class="tree">
            <ul>
                <li>

                    <ul>
                        <li>
                            <a href="#">Thomas(Brother)</a>
                            <a href="#">Me</a>
                            <a href="#">Brandon(Brother)</a>
                            <ul>
                                <li><a href="#">Grand Child</a></li>
                                <li>
                                    <a href="#">Grand Child</a>
                                    <ul>
                                        <li>
                                            <a href="#">Great Grand Child</a>
                                            <ul>
                                                <li>
                                                    <a href="#">Great Grand Child</a>
                                                    <ul>
                                                        <li>
                                                            <a href="#">Great Grand Child</a>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>

                                    </ul>
                                </li>
                                <li>
                                    <a href="#">Grand Child</a>
                                    <ul>
                                        <li>
                                            <a href="#">Great Grand Child</a>
                                        </li>

                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <div class="tree">
        <ul>
            <li id="ContentPlaceHolder1_bindmother">
                <h3>Mother</h3>
                <div class='box-main-tree'>
                    <span class='box-tree-left'><a href='#'>Meera Rout</a><p>Montreal CA</p>
                        <p>F  41 years</p>
                    </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid61'>
                        <img src='images/tree2.jpg' width='30' height='30' /></a><a href='#'><img src='images/greenimg2.jpg' width='30' height='30' /></a> <a href='#'>
                            <img src='images/image4.png' width='30' height='30' /></a> </span>
                </div>
            </li>
            <li>
                <ul>
                    <li>
                        <div class="box-main-tree">
                            <span class="box-tree-left">
                                <a href="#" id="ContentPlaceHolder1_MailName">Priyansh Rout</a>
                                <p id="ContentPlaceHolder1_PLocation">Bhubaneswar</p>
                                <p id="ContentPlaceHolder1_Years">M - 26 Years</p>
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

                        <ul>

                            <li>
                                <h3>Spouse</h3>

                                &nbsp;&nbsp;&nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btnShow" value="+Add" onclick="return ShowModalPopup();" id="ContentPlaceHolder1_btnShow" class="btn btn-info" />
                                <a id="ContentPlaceHolder1_lnkFake" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$lnkFake&#39;,&#39;&#39;)"></a>

                                <ul id="ContentPlaceHolder1_Bind_Spouse">
                                    <div class='box-main-tree'>
                                        <span class='box-tree-left'><a href='#'>Swati   Meher</a><p>Montreal CA</p>
                                            <p>F - 24 years</p>
                                        </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid11'>
                                            <img src='images/tree2.jpg' width='30' height='30' /></a> <a href='#'>
                                                <img src='images/greenimg2.jpg' width='30' height='30' /></a><a href='#'><img src='images/image4.png' width='30' height='30' /></a> </span>
                                    </div>
                                    <ul>
                                        <li>
                                            <div class='box-main-tree'>
                                                <span class='box-tree-left'><a href='#'>Niva   dandsena</a><p>Montreal CA</p>
                                                    <p>F - 23 years</p>
                                                </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid21'>
                                                    <img src='images/tree2.jpg' width='30' height='30' /></a> <a href='#'>
                                                        <img src='images/greenimg2.jpg' width='30' height='30' /></a><a href='#'><img src='images/image4.png' width='30' height='30' /></a> </span>
                                            </div>
                                        </li>
                                    </ul>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <h3>Children</h3>

                        &nbsp;&nbsp;&nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btn_Addchld" value="+Add" onclick="return ShowModalPopupchld();" id="ContentPlaceHolder1_btn_Addchld" class="btn btn-info" />
                        <a id="ContentPlaceHolder1_lnkchld" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$lnkchld&#39;,&#39;&#39;)"></a>

                        <ul id="ContentPlaceHolder1_Bind_Child">
                            <div class='box-main-tree'>
                                <span class='box-tree-left'><a href='#'>Shyam   Meher</a><p>Montreal CA</p>
                                    <p>M - 11 years</p>
                                </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid31'>
                                    <img src='images/tree2.jpg' width='30' height='30' /></a> <a href='#'>
                                        <img src='images/greenimg2.jpg' width='30' height='30' /></a><a href='#'><img src='images/image4.png' width='30' height='30' /></a> </span>
                            </div>
                            <ul>
                                <li>
                                    <div class='box-main-tree'>
                                        <span class='box-tree-left'><a href='#'>Anjali   Meher</a><p>Montreal CA</p>
                                            <p>F - 15 years</p>
                                        </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid71'>
                                            <img src='images/tree2.jpg' width='30' height='30' /></a> <a href='#'>
                                                <img src='images/greenimg2.jpg' width='30' height='30' /></a><a href='#'><img src='images/image4.png' width='30' height='30' /></a> </span>
                                    </div>
                                    <ul>
                                        <li>
                                            <div class='box-main-tree'>
                                                <span class='box-tree-left'><a href='#'>Nirmal   Sahu</a><p>Montreal CA</p>
                                                    <p>M - 12 years</p>
                                                </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid111'>
                                                    <img src='images/tree2.jpg' width='30' height='30' /></a> <a href='#'>
                                                        <img src='images/greenimg2.jpg' width='30' height='30' /></a><a href='#'><img src='images/image4.png' width='30' height='30' /></a> </span>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                    </li>
                    <li>
                        <h3>Sibling</h3>

                        <ul id="ContentPlaceHolder1_Bind_Siblings">
                        </ul>
                    </li>
                </ul>
            </li>
            <li id="ContentPlaceHolder1_bindfather">
                <h3>Father</h3>
                <div class='box-main-tree'>
                    <span class='box-tree-left'><a href='#'>Ashok Rout</a><p>Montreal CA</p>
                        <p>M  50 years</p>
                    </span><span class='box-tree-right'><a href='MyTree1.aspx?Memid=Memberid51'>
                        <img src='images/tree2.jpg' width='30' height='30' /></a><a href='#'><img src='images/greenimg2.jpg' width='30' height='30' /></a> <a href='#'>
                            <img src='images/image4.png' width='30' height='30' /></a> </span>
                </div>
            </li>
        </ul>
    </div>
</asp:Content>
