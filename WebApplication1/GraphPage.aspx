<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraphPage.aspx.cs" Inherits="WebApplication1.GraphPage" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" Text="Return" ID="ReturnToMainPage"  OnCommand="ReturnToMainPage_Command"/>
            <br />
            <asp:Chart ID="Chart1" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~/tempImage" Width="800px" Height="500px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
                <BorderSkin SkinStyle="Sunken"></BorderSkin>
                <Series>
                    <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="Series1" ChartType="Line" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2" YValueType="Double" Legend="Product1">
                    </asp:Series>                    
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <AxisY LineColor="64, 64, 64, 64" Title="Sales(In Million)">
                         <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisY>
                        <AxisX LineColor="64, 64, 64, 64" Title="Year">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
