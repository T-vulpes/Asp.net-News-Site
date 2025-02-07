using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class dunya : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Integrated Security=True;";
            string query = "SELECT haber_baslik, haber_icerik, haber_resmi, haber_linki FROM haberler WHERE kategori='Dünya'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        haberRepeater.DataSource = dataTable;
                        haberRepeater.DataBind();
                    }
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection(connectionString);
        baglanti.Open();

        string sqlSorgu = "SELECT * FROM uyelik WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre";
        SqlCommand komut = new SqlCommand(sqlSorgu, baglanti);
        komut.Parameters.AddWithValue("@kullaniciadi", TextBox1.Text);
        komut.Parameters.AddWithValue("@sifre", TextBox2.Text);

        SqlDataReader okuyucu = komut.ExecuteReader();
        if (okuyucu.HasRows)
        {
            Session["UserName"] = TextBox1.Text;

            Response.Redirect("adminpanel.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Kullanıcı adı veya şifre hatalı!');", true);
        }

        okuyucu.Close();
        baglanti.Close();
    }

    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGonder_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        using (SqlConnection baglanti = new SqlConnection(connectionString))
        {
            string sqlKomut = "INSERT INTO mesajlar (isim, telefonno, mail, haber) VALUES (@isim, @telefonno, @mail, @haber)";
            using (SqlCommand komut = new SqlCommand(sqlKomut, baglanti))
            {
                komut.Parameters.AddWithValue("@isim", txtIsim.Text);
                komut.Parameters.AddWithValue("@telefonno", txtTelefonNo.Text);
                komut.Parameters.AddWithValue("@mail", txtMail.Text);
                komut.Parameters.AddWithValue("@haber", txtKonu.Text);

                try
                {
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    lblSonuc.Text = "Mesaj başarıyla gönderildi!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mesaj başarıyla gönderildi!');", true);

                }
                catch (Exception ex)
                {
                    lblSonuc.Text = "Hata oluştu: " + ex.Message;
                }
            }
        }

    }

}
