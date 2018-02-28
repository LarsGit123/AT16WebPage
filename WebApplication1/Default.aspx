<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" Font-Size="XX-Large" Height="100px" Width="307px" Font-Bold="True">Kviknehyttas nettside. Velg et bilde å se:</asp:Label>
        <br />
       
      
        <asp:DropDownList ID="GreetList" autopostback="true" runat="server" OnSelectedIndexChanged="GreetList_SelectedIndexChanged">        
        </asp:DropDownList>
        <br />
        
        <asp:hyperlink id="link" runat="server">
   <asp:image id="img" runat="server"/>
</asp:hyperlink>
        <br />
        <br />
        <asp:Label runat="server" text="Temperatur i AT16" Font-Size="X-Large" Width="300px"/>
        <asp:Label runat="server" id="At16tempLabel" Font-Size="X-Large" />
        <br />
        <asp:Label runat="server" text="Fuktighet %" Font-Size="X-Large" Width="300px"/>
        <asp:Label runat="server" id="At16humLabel" Font-Size="X-Large"/>        
        <br />
        
        <asp:Label runat="server" id="At16timeLabel" />
    </div>
    </form>
    
</body>
</html>
