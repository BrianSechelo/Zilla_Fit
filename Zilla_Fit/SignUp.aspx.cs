using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : System.Web.UI.Page
{
    private static string connectionString = @"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblId.Text = "0";
            fromUsers();
        }
    }

    private void fromUsers()
    {
        string query = "select MAX(Id) AS MaxId from Users";

        using (SqlConnection Con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, Con))
            {
                SqlDataReader rdr = null;
                try
                {
                    Con.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        int maxId = rdr["MaxId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["MaxId"]);
                        lblId.Text = (maxId + 1).ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
                finally
                {
                    if (rdr != null && !rdr.IsClosed)
                    {
                        rdr.Close();
                    }
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;
        string isTrainer = ddlTrainer.SelectedValue;
        string imagePath = "";

        if (fuProfileImage.HasFile)
        {
            string fileName = Path.GetFileName(fuProfileImage.PostedFile.FileName);
            imagePath = "~/images/" + fileName;
            fuProfileImage.SaveAs(Server.MapPath(imagePath));
        }

        string query = "INSERT INTO Users (Id, Name, Email, Password, Istrainer, ProfileImage) VALUES (@Id, @Name, @Email, @Password, @Istrainer, @ProfileImage)";

        using (SqlConnection Con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, Con))
            {
                cmd.Parameters.AddWithValue("@Id", lblId.Text);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Istrainer", isTrainer);
                cmd.Parameters.AddWithValue("@ProfileImage", imagePath);

                try
                {
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Sign up successful!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearFields();
                    fromUsers();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    private void ClearFields()
    {
        txtName.Text = "";
        txtEmail.Text = "";
        txtPassword.Text = "";
        ddlTrainer.SelectedIndex = 0;
        fuProfileImage.Attributes.Clear();
        imagePreview.ImageUrl = "#";
        imagePreview.Style.Add("display", "none");
    }
}
