<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parent.aspx.cs" Inherits="TflinkTest.FamilyTree.Parent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
<title></title>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 80%;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
            height: 80%;
        }
        .modalPopup .footer
        {
            padding: 3px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        .modalPopup td
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
    </script>
        </head>
<body>
     <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <div>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" OnClientClick="return ShowModalPopup()"
            Text="Show New Page" />
        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="mpShow" BehaviorID="mpe" runat="server" PopupControlID="pnlPopUp"
            TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                New page in iFrame
            </div>
            <div class="body">
                <iframe id="iFramePersonal" src="NewPage.aspx" style="height:400px;" width="100%">
                </iframe>
                <br />
                <br />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
