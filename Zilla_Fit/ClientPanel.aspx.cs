using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientPanel : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-IVHRL9V\SQLEXPRESS;Initial Catalog=fitnessdb;Integrated Security=True;Pooling=False;Encrypt=False");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFitnessTypes();
            LoadClientProfile();
        }
    }

    private void LoadFitnessTypes()
    {
        try
        {
            Con.Open();
            string query = "SELECT ft.Id, ft.Name, ft.Description, ft.PricePerSession, u.Name as TrainerName, u.ProfileImage FROM FitnessTypes ft JOIN Users u ON ft.TrainerId = u.Id";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptFitnessTypes.DataSource = dt;
            rptFitnessTypes.DataBind();
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

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int fitnessTypeId = Convert.ToInt32(btn.CommandArgument);
        int clientId = GetClientId();

        try
        {
            Con.Open();
            // Get client and fitness type details
            string query = "SELECT u.Name as ClientName, ft.Name as FitnessType, ft.TrainerId, t.Name as TrainerName " +
                           "FROM Users u, FitnessTypes ft, Users t " +
                           "WHERE u.Id = @ClientId AND ft.Id = @FitnessTypeId AND ft.TrainerId = t.Id";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@FitnessTypeId", fitnessTypeId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string clientName = reader["ClientName"].ToString();
                string fitnessType = reader["FitnessType"].ToString();
                int trainerId = Convert.ToInt32(reader["TrainerId"]);
                string trainerName = reader["TrainerName"].ToString();

                reader.Close();

                // Insert enrollment record
                query = "INSERT INTO Enrollments (ClientId, ClientName, TrainerId, TrainerName, FitnessType, DateBooked) " +
                        "VALUES (@ClientId, @ClientName, @TrainerId, @TrainerName, @FitnessType, GETDATE())";
                cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@ClientId", clientId);
                cmd.Parameters.AddWithValue("@ClientName", clientName);
                cmd.Parameters.AddWithValue("@TrainerId", trainerId);
                cmd.Parameters.AddWithValue("@TrainerName", trainerName);
                cmd.Parameters.AddWithValue("@FitnessType", fitnessType);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Enrolled successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("Checkout.aspx");
            }
            else
            {
                lblMessage.Text = "Error: Could not find details for enrollment.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
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

    private void LoadClientProfile()
    {
        int clientId = GetClientId();

        try
        {
            Con.Open();
            string query = "SELECT Name, Email FROM Users WHERE Id = @ClientId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtName.Text = reader["Name"].ToString();
                txtEmail.Text = reader["Email"].ToString();
            }

            reader.Close();
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

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        int clientId = GetClientId();
        string name = txtName.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;

        try
        {
            Con.Open();
            string query = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @ClientId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.ExecuteNonQuery();
            lblMessage.Text = "Profile updated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
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

    private int GetClientId()
    {
        if (Session["ClientId"] != null)
        {
            return (int)Session["ClientId"];
        }
        else
        {
            // Handle the case where the session variable is not set
            lblMessage.Text = "Error: Client ID not found.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return -1; // Return an invalid ID or handle as needed
        }
    }

    protected void rptFitnessTypes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}
