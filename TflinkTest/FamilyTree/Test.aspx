<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TflinkTest.FamilyTree.Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script language="javascript">
        function singleRbtnSelect(chb) {
            $(chb).closest("txt_text").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);
        }
        function CloseWindow() {
            window.close();
        }
        function singleRbtnSelect(chb) {
            $(chb).closest("lbl_mytext").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);
        }
        function CloseWindow() {
            window.close();
        }
        function singleRbtnSelect(chb) {
            $(chb).closest("myset").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);
        }
        function CloseWindow() {
            window.close();
        }
    </script>
    <script>
        function Validate() {

            if (document.getElementById("<%=txt_username.ClientID%>").value == "") {
                alert("Enter Your Name !");
                document.getElementById("<%=txt_username.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txt_xhatmsg.ClientID%>").value == "") {
                alert("Write Some Message !");
                document.getElementById("<%=txt_xhatmsg.ClientID%>").focus();
                return false;
            }
        }
    </script>

</head>
<body style="background-image: url(images/backimg.jpeg)">
    <form id="form1" runat="server" style="background-image: url(images/backimg.jpeg)">
        <%-- <script type="text/javascript">
        function ShowModalPopupchld() {
            $find("mpechld").show();
            return false;
        }
    </script>--%>
        <%--    <asp:Panel ID="pnlPopUpchld" runat="server" CssClass="modalPopup" Style="display: none">
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
    </asp:Panel>--%>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="container" style="height: auto; margin-left: auto; margin-right: auto; margin-bottom: auto; margin-top: auto; text-align-last: center; background-image: url(images/backimg.jpeg)">
                <%--    <asp:TextBox ID="txt_text" Width="100%" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_text_TextChanged" runat="server"></asp:TextBox>--%>

                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server"><img src="images/chanepswimg.png" />
                    <ContentTemplate>
                        <asp:Timer ID="myset" runat="server" OnTick="Timer1_Tick" Interval="1000" />
                        <asp:Label ID="lbl_mytext" Visible="false" runat="server"></asp:Label>
                            <script type="text/javascript">
                    function HideLabel() {
                        var seconds = 5;
                        setTimeout(function () {
                            document.getElementById("<%=lbl_typingsts.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
                </script>
                <asp:Label ID="lbl_typingsts" Visible="false" Text="Typing..." runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                <asp:TextBox ID="txt_username" Width="100%" BackColor="cadetblue" ForeColor="aliceblue" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row" style="height: 400px; text-align-last: center; overflow: scroll; margin-left: auto; margin-right: auto;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div runat="server" id="mychat" style="align-content: center;">
                            </div>
                            <asp:Timer ID="myset" runat="server" OnTick="myset_Tick" Interval="1000" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:TextBox ID="txt_xhatmsg" Width="100%" BackColor="cadetblue" ForeColor="aliceblue" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row" style="text-align-last: center; margin-top: 35px;">
                    <asp:Button ID="btn_group" Height="60px" BackColor="darkseagreen" ForeColor="aliceblue" class="btn btn-secondary" Width="100%" OnClick="btn_group_Click" OnClientClick="return Validate();" runat="server" Text="send" />
                </div>

                <%--   <asp:Button ID="btn_Addchld" runat="server" class="btn btn-info" OnClientClick="return ShowModalPopupchld()"
                                    Text="+Add" />
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpechld" runat="server" PopupControlID="pnlPopUpchld"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btn_closechld">
                                </cc1:ModalPopupExtender>--%>
                <h2>Hello mr</h2>
                <div id="getvalues" runat="server">
                </div>
                <div runat="server" id="btnbind">
                </div>
                <%--<input value='button' name='btn_Addchld' type="button" class='btn btn-info'  onclick='return ShowModalPopupchld()' />--%>
                <%--<asp:Button ID='btn_Addchld' class='btn btn-info' OnClientClick='return ShowModalPopupchld()' Text='+Add' />--%>
                <cc1:ModalPopupExtender ID='ModalPopupExtender1' BehaviorID='mpechld' runat='server' PopupControlID='pnlPopUpchld' TargetControlID='lnkFake' BackgroundCssClass='modalBackground' CancelControlID='btn_closechld'></cc1:ModalPopupExtender>
                <asp:LinkButton ID='lnkFake' runat='server'></asp:LinkButton>

                <h5>where is button</h5>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="new windows" />
                <a href="Test.aspx" target="_blank">
                    <input type="button" class="button" value="Open" />
                </a>
                <asp:Button ID="Button2" runat="server" Text="Button2" OnClientClick="window.open('Test.aspx');width=500;returnfalse;" />
                <a href="#" onclick="window.open('Edit_Parents.aspx?Memidview= <%#Eval("UserCourseId") %> ','PrintMe','height=500px,width=500px,scrollbars=1');">SomeText</a>
            </div>
        </div>
    </form>
</body>
</html>
