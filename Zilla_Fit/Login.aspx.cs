using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

public partial class Login : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-IVHRL9V\SQLEXPRESS;Initial Catalog=fitnessdb;Integrated Security=True;Pooling=False;Encrypt=False");

    protected void Page_Load(object sender, EventArgs e)
    {
    }

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        string password = txtPassword.Text;

        try
        {
            Con.Open();
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                bool isTrainer = rdr["Istrainer"].ToString() == "Yes";
                // Store the trainer ID in session
               
                if (isTrainer)
                {
                    // Store the trainer ID in session
                    Session["TrainerId"] = userId;
                    Response.Redirect("TrainerPanel.aspx");
                }
                else
                {
                    // Store the client ID in session
                    Session["ClientId"] = userId;
                    Response.Redirect("ClientPanel.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Invalid email or password!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            Con.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }
    }
}