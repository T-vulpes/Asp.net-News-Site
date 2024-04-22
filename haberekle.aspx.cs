using System;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class haberekle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO haberler (haber_baslik, haber_icerik, haber_tarihi, haber_linki, kategori, haber_resmi) VALUES(@haber_baslik, @haber_icerik, @haber_tarihi, @haber_linki, @kategori, @haber_resmi)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@haber_baslik", TextBox1.Text);
                cmd.Parameters.AddWithValue("@haber_icerik", TextBox2.Text);
                cmd.Parameters.AddWithValue("@haber_tarihi", TextBox3.Text);
                cmd.Parameters.AddWithValue("@haber_linki", TextBox4.Text);
                cmd.Parameters.AddWithValue("@kategori", kategori.SelectedValue);

                string fileName = "";
                if (FileUpload1.HasFile)
                {
                    fileName = FileUpload1.FileName;
                    FileUpload1.SaveAs(Server.MapPath("~/resimler/") + fileName);
                    cmd.Parameters.AddWithValue("@haber_resmi", fileName);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Veri Eklendi.!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Veri eklenirken bir hata oluştu.');", true);
                    }
                }
                else
                {
                    // Dosya yüklenmemişse, uyarı göster
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                }
            }
        }
    }


    protected void ClearContentButton_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
    }
}
