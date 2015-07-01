<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Beheer.aspx.cs" Inherits="Beheer" %>


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
<!--<body style= " ">-->

    <form id="form1" runat="server">
        <div class="header">
			<div class="wrapper">
				<a href="index.aspx"><img src="img\header.png"></img></a>
				<ul class="menu">
					<li><a href="login.aspx">Login</a></li>
					<li><a href="Reparatie.aspx">Reparatie</a></li>
					<li><a href="Schoonmaak.aspx">Schoonmaak</a></li>
					<li><a href="Beheer.aspx">Home</a></li>
				</ul>
			</div>
		</div>
        <!--<p><asp:Button ID="btnLogOut" runat="server" Text="Uitloggen" OnClick="btnLogOut_Click" /> <br />
            <asp:Label ID="lblCurrentlyLoggedIn" runat="server" Text="nootjes"></asp:Label>
        </p>
        <p><asp:Button ID="btnToRepa" runat="server" Text="Reparatiesysteem" OnClick="btnToRepa_Click" /></p>
        <p><asp:Button ID="btnToService" runat="server" Text="Schoonmaaksysteem" OnClick="btnToService_Click" /></p>-->
    <div class="wrapper">
			<div class="container">
        
        <!--<h1>Tram Beheer Systeem</h1>-->
    <div>
        <!--<h2>(Ver)Plaatsen/ Verwijderen:</h2>-->
        <p>
            <asp:DropDownList ID="ddlPlaats" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem>Plaats</asp:ListItem>
                <asp:ListItem>Verwijder</asp:ListItem>
                <asp:ListItem>Blokkeer</asp:ListItem>
            </asp:DropDownList> Tram: <asp:DropDownList ID="ddlTrams" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList> Op spoor: <asp:DropDownList ID="ddlTracks" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTracks_SelectedIndexChanged"></asp:DropDownList> Op Sector: <asp:TextBox ID="tbSector" runat="server" Width="50px"></asp:TextBox> <asp:Button ID="btnPlaatsTram" runat="server" Text="(Ver) Plaats" OnClick="btnPlaatsTram_Click" /></p>
    </div>
    <!--<div style="width: 1040px;">-->
    <h2> Sporen overzicht:</h2>
        <!-- Top Row: -->
    <div style="width: 900px;  ">
    <div style="float: left; width: 150px; ">
        <asp:Panel ID="p38" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p37" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p36" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p35" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p34" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p33" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p32" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p31" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p30" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p40" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p41" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p42" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p43" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p44" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p45" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p46" runat="server"></asp:Panel>
    </div>
        <!-- Bottom Row: -->
    <br />
    <!--<div style="width: 1040px;">-->
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p58" runat="server"></asp:Panel>
    </div>
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p57" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p56" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p55" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p54" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p53" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p52" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p51" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p64" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p63" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p62" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p61" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p60" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p74" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p75" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p76" runat="server"></asp:Panel>
    </div>
            <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p77" runat="server"></asp:Panel>
    </div>
    <br />
        <!-- Bottom Row right individual: -->
    <!--<div style="width: 300px;">-->
    <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p12" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p13" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p14" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p15" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p16" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p17" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p18" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p19" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p20" runat="server"></asp:Panel>
    </div>
        <div style="float: left; width: 150px;  ">
        <asp:Panel ID="p21" runat="server"></asp:Panel>
    </div>
    </div>
                </div>
    </div>
    </form>
</body>
</html>
