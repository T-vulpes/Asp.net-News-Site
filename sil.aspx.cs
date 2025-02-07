using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sil : System.Web.UI.Page
{
    SqlConnection baglanti;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VeriTabaniniBagla();
            VerileriGetir();
        }
    }

    private void VerileriGetir()
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection(connectionString);
        string sqlSorgu = "SELECT * FROM haberler";
        SqlCommand komut = new SqlCommand(sqlSorgu, baglanti);

        try
        {
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();

            if (okuyucu.HasRows)
            {
                GridView1.DataSource = okuyucu;
                GridView1.DataBind();
            }

            okuyucu.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            baglanti.Close();
        }
    }

    private void VeriTabaniniBagla()
    {
        string connectionString = "Server=DESKTOP-OF8K7QI\\MSSQL;Database=habersitesi;Trusted_Connection=True;";
        baglanti = new SqlConnection(connectionString);
        baglanti.Open();
    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int selectedIndex = GridView1.SelectedIndex;
        string secilenID = GridView1.DataKeys[selectedIndex]["NO"].ToString();

        TextBox1.Text = secilenID;
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "confirm('Bu kaydı silmek istediğinizden emin misiniz?')", true);
    }

    protected void btnSil_Click(object sender, EventArgs e)
    {
        if (GridView1.SelectedRow != null)
        {
            int selectedIndex = GridView1.SelectedIndex;
            string secilenID = GridView1.DataKeys[selectedIndex]["NO"].ToString();
            Sil(secilenID);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lütfen bir satır seçin.');", true);
        }
    }

    private void Sil(string id)
    {
        VeriTabaniniBagla();
        string deleteQuery = "DELETE FROM haberler WHERE NO = @ID";
        SqlCommand deleteCommand = new SqlCommand(deleteQuery, baglanti);
        deleteCommand.Parameters.AddWithValue("@ID", id);

        int affectedRows = deleteCommand.ExecuteNonQuery();
        baglanti.Close();

        if (affectedRows == 0)
        {
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Silme işlemi başarısız oldu.');", true);
        }
        else
        {
            VerileriGetir();
        }
    }

}

