<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popuptest.aspx.cs" Inherits="TflinkTest.FamilyTree.Popuptest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script type="text/javascript">
        function ShowModalPopupParents() {
            $find("mpeParents").show();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div>
     <asp:Panel ID="pnlPopUpparents" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Add Your Parents Profile 
        </div>
        <div class="body">
            <iframe id="iFramePersonalParent" src="Addparentchoose.aspx" height="400px" width="100%"></iframe>
            <br />
            <br />
            <a href="MyTree1.aspx">
                <asp:Button ID="btnCloseparents" runat="server" class="btn btn-danger" Text="Close" /></a>
        </div>
    </asp:Panel>
         <div style="background-color: #d8e9f7; height: 50px; width: 100%;">
        <%-- <button type="button" style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-primary">+Add Parents</button>--%>
        <asp:Button ID="Button1" runat="server" Style="margin-left: auto; margin-right: auto; display: block;" class="btn btn-info" OnClientClick="return ShowModalPopupParents()"
            Text="+Add Parents" />
        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpeParents" runat="server" PopupControlID="pnlPopUpparents"
            TargetControlID="lnkFake" BackgroundCssClass="modalBackground" CancelControlID="btnCloseparents">
        </cc1:ModalPopupExtender>

        <a class="thickbox"
            title="Add Child" runat="server" id="lnkAddChild"
            href="popuptest.aspx?From=FamilyTree&<%--TB_iframe--%>=true&height=560&width=950&modal=true">
            <%--  <asp:Button ID="Button1" runat="server" Text="Add" />--%>
        </a>
    </div>
    </div>
    </form>
</body>
</html>
