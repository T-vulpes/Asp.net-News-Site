<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="kullanicimesaj.aspx.cs" Inherits="kullanici" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <div id="kapsayici">
            <div id="yonetici">
            <div id="adminmenu">
                <ul>
                    <li><a href="sil.aspx">Haber Silme</a></li>
                    <li><a href="haberekle.aspx">Haber Ekleme</a></li>
                    <li><a href="guncelle.aspx">Haber Güncelleme</a></li>
                    <li><a href="kullanicimesaj.aspx">Kullanıcı Mesajları</a></li>
                </ul>
            </div>
            </div>
        <div id="icerik">
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT [Kimlik], [isim], [telefonno], [mail], [haber]  FROM [mesajlar]"></asp:SqlDataSource>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666"
    BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="Kimlik" 
    GridLines="Horizontal" Height="300px" Width="942px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1"
    CssClass="custom-grid">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Kimlik" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Kimlik" />
                    <asp:BoundField DataField="isim" HeaderText="Kullanıcı İsmi" SortExpression="isim" />
                    <asp:BoundField DataField="telefonno" HeaderText="Telefon Numarası" SortExpression="telefonno" />
                    <asp:BoundField DataField="mail" HeaderText="Kullanıcı Mail" SortExpression="mail" />
                    <asp:BoundField DataField="haber" HeaderText="Kullanıcı Mesajı" SortExpression="haber" />
                    <asp:TemplateField HeaderText="İşlemler">
                        <ItemTemplate>
                            <asp:Button CssClass="button" ID ="btnSil" runat="server" Text="Sil" OnClientClick="return OnDeleteConfirm();" OnClick="btnSil_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
        </div>
    </div>

</asp:Content>


