<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schoonmaak.aspx.cs" Inherits="Schoonmaak" %>

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
					<li><a href="index.aspx">Home</a></li>
				</ul>
			</div>
		</div>
		<div class="wrapper">
			<div class="container">
				
			    Tram Status aanpassen:<br />
                <br />
                TramNr:<asp:TextBox ID="tbTramNrClean" runat="server"></asp:TextBox>
                <asp:Button ID="btAdjustCln" runat="server" Text="Aanpassen" OnClick="btAdjustCln_Click"/>
                <br />
                Username: <asp:DropDownList ID="ddlUserClean" runat="server">
                </asp:DropDownList>
                Status: <asp:DropDownList ID="ddlStatusClean" runat="server">
                </asp:DropDownList>
                Datum: <asp:DropDownList ID="ddlDateClean" runat="server">
                </asp:DropDownList>
				
			    <br />
				
			    <br />
                <asp:Panel ID="pnlClean" class="panelCln" runat="server" BackColor="White">
                    <br />
                    <asp:Repeater ID="rptClean" runat="server">
                        <HeaderTemplate>
                            <table border="1" width="100%">
                                <tr>
                                    <th>Select</th>
                                    <th>Datum</th>
                                    <th>Type</th>
                                    <th>Tramnummer </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td> <a href="<%# "schoonmaak.aspx?query=" + Eval("id") %>">Select</a></td>
                                <td><%#Eval("Date") %></td>
                                <td><%#Eval("Type")%></td>
                                <td><%#Eval("Tram.TramNr")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                </asp:Panel>
				
			    <br />
				
			</div>
		</div>
	    </form>
	</body>
</html>

