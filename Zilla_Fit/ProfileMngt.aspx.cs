using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfileMngt : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" );

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProfile();
        }
    }

    private void LoadProfile()
    {
        try
        {
            Con.Open();
            int userId = (int)Session["UserId"];
            string query = "SELECT Name, Email FROM Users WHERE Id = @UserId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtName.Text = reader["Name"].ToString();
                txtEmail.Text = reader["Email"].ToString();
            }
            Con.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Con.Open();
            int userId = (int)Session["UserId"];
            string query = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @UserId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text); // Add hashing for better security
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();

            lblMessage.Text = "Profile updated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            Con.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
        finally
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }
    }

    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {

    }
}