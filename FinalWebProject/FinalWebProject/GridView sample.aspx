<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridView sample.aspx.cs" Inherits="FinalWebProject.GridView_sample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <asp:GridView ID="cardsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="cardId" DataSourceID="Database1">
            <Columns>
                <asp:BoundField DataField="cardName" HeaderText="Card Name"/>
                <asp:BoundField DataField="cardAbility" HeaderText="cardAbility" />
                <asp:BoundField DataField="cardManaCost" HeaderText="cardManaCost" />
                <asp:BoundField DataField="cardRarity" HeaderText="cardRarity" />
                <asp:TemplateField HeaderText="Card Image">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("cardImage") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                   </asp:GridView>
        <asp:SqlDataSource ID="Database1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [AllMagicCards]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
