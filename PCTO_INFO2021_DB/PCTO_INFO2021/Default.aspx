<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PCTO_INFO2021.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="imgImage" runat="server" />
        <br />
        <asp:TextBox ID="txtOut" runat="server" Height="268px" Width="620px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
        <p style="margin-left: 60px">
            <asp:Button ID="btnN" runat="server" Height="30" Width="50" Text="N" OnClick="btnN_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblObiettivo" runat="server" Text="Target: "></asp:Label>
&nbsp;&nbsp;<asp:DropDownList ID="ddlTarget" runat="server" Height="18px" Width="180px" Enabled="False">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Ricomincia" OnClick="btnReset_Click" />
        </p>
        <p>
            <asp:Button ID="btnO" runat="server" Height="30" Width="50" Text="O" OnClick="btnO_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnE" runat="server" Height="30" Width="50" Text="E" OnClick="btnE_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblAzione" runat="server" Text="Azione: "></asp:Label>
&nbsp;<asp:DropDownList ID="ddlAction" runat="server" Height="16px" Width="179px" Enabled="False">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSalva" runat="server" Text="Salva" OnClick="btnSalva_Click" Width="74px" />
            </p>
        <p style="margin-left: 60px">
            <asp:Button ID="btnS" runat="server" Height="30" Width="50" Text="S" OnClick="btnS_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnUsa" runat="server" Height="30" Width="50" Text="Usa" Enabled="False" OnClick="btnUsa_Click" />
        &nbsp;
            <asp:Label ID="lblVita" runat="server" Text="Vita: "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblDBError" runat="server" Text="........................"></asp:Label>
        </p>
    </form>
</body>
</html>
