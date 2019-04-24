<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="safe_web_app.WebForm1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text2 {
            height: 60px;
            margin-top: 0px;
        }
        #CommentBox {
            height: 43px;
            width: 186px;
            margin-right: 1px;
        }
        #TextArea1 {
            height: 27px;
            width: 165px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:TextBox ID="appname" runat="server" ReadOnly="True"></asp:TextBox>
        
        <table style="width:100%;">
        <tr>
            <td>
                
                     <hr />
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>

                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("name") %>  '></asp:Label>
                        <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text='<%#Eval("time") %>'></asp:Label>
                        <br />
                        <br />
                        <div runat="server" innerText='<%#Eval("com") %>'></div>
                        <hr />
                    </ItemTemplate>
                    </asp:Repeater>
            </td>
        </tr>
    </table>




    <table style="width:100%;">
        <tr>
            <td>Comment:<br />
                <asp:TextBox ID="commentBox" runat="server" Height="68px" TextMode="MultiLine" Width="172px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Comment" /></td>
            <td></td>
        </tr>
    </table>
    </form>
</body>
</html>
