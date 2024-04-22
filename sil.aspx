<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sil.aspx.cs" Inherits="sil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1 {
            width: 80%;
        }

        .style2 {
            text-align: center;
            color: #1956A7;
        }
    </style>
    <script type="text/javascript">
        function OnDeleteConfirm() {
            return confirm('Bu kaydı silmek istediğinizden emin misiniz?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
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
                SelectCommand="SELECT [haber_baslik], [NO], [haber_icerik], [haber_tarihi] FROM [haberler]"></asp:SqlDataSource>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666"
    BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="NO" 
    GridLines="Horizontal" Height="300px" Width="942px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1"
    CssClass="custom-grid">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="NO" HeaderText="NO" InsertVisible="False" ReadOnly="True" SortExpression="NO" />
                    <asp:BoundField DataField="haber_baslik" HeaderText="haber_baslik" SortExpression="haber_baslik" />
                    <asp:BoundField DataField="haber_icerik" HeaderText="haber_icerik" SortExpression="haber_icerik" />
                    <asp:BoundField DataField="haber_tarihi" HeaderText="haber_tarihi" SortExpression="haber_tarihi" />
                    <asp:TemplateField HeaderText="İşlemler">
                        <ItemTemplate>
                            <asp:Button CssClass="button" ID="btnSil" runat="server" Text="Sil" OnClientClick="return OnDeleteConfirm();" OnClick="btnSil_Click" />
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
            <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Visible="False"></asp:TextBox>
        </div>
    </div>
</asp:Content>
