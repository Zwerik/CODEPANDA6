<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Code Panda - TVS</title>
		<link href="style.css" rel = stylesheet />
		<link href="http://vvn.nl/sites/all/themes/vvn/favicon.ico" rel="shortcut icon">  
		<meta name="description" content="Code Panda - TVS">
		<meta name="author" content="Code Panda - www.codepanda.nl">
		<meta charset="UTF-8">
		<script language="javascript" src="javascript/jquery.js"></script>
	</head>
	<body>
		<form id="form1" runat="server">
		<div class="header">
			<div class="wrapper">
				<a href="index.aspx"><img src="img\header.png"></img></a>
				<ul class="menu">
					<li><a href="login.aspx">Login</a></li>
					<li><a href="reparatie.aspx">Reparatie</a></li>
					<li><a href="schoonmaak.aspx">Schoonmaak</a></li>
					<li><a href="Beheer.aspx">Home</a></li>
				</ul>
			</div>
		</div>
<body>
        
    <div class="wrapper">
			<div class="container">
    <div>
            <h1>Login:</h1>
            Username:
            <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
            <br />
            Password:
            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
            <asp:Label ID="lblCredInfo" runat="server" Text=" "></asp:Label>
        </div>
        </div>
		</div>
    </form>
</body>
</html>
