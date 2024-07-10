using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class TrainerPanel : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False");

    private void LoadFitnessTypes()
    {
        int trainerId = GetTrainerId(); // Get the logged-in trainer's ID
        if (trainerId == -1)
        {
            return; // If trainer ID is not found, return early
        }

        try
        {
            Con.Open();
            string query = "SELECT ft.Id, ft.Name, ft.Description, ft.PricePerSession, u.Name as TrainerName FROM FitnessTypes ft JOIN Users u ON ft.TrainerId = u.Id WHERE ft.TrainerId = @TrainerId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@TrainerId", trainerId);
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

    private void LoadProfileImage()
    {
        int trainerId = GetTrainerId(); // Get the logged-in trainer's ID
        if (trainerId == -1)
        {
            return; // If trainer ID is not found, return early
        }

        SqlDataReader rdr = null;
        try
        {
            Con.Open();
            string query = "SELECT ProfileImage FROM Users WHERE Id = @TrainerId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@TrainerId", trainerId);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string imagePath = rdr["ProfileImage"].ToString();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    imgProfile.ImageUrl = imagePath;
                    imgProfile.Visible = true;
                }
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
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProfileImage();
            LoadFitnessTypes();

        }
        Button loginButton = FindControlRecursive(Master, "btnLogin") as Button;
        Button rigesterButton = FindControlRecursive(Master, "btnRegister") as Button;
        if (loginButton != null)
        {
            loginButton.Text = "Log Out";

            rigesterButton.Visible = false;
        }
        else
        {
            // For debugging purposes
            lblMessage.Text = "Login button not found!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnUpdateProfileImage_Click(object sender, EventArgs e)
    {
        int trainerId = GetTrainerId();
        if (trainerId == -1)
        {
            lblMessage.Text = "Error: Trainer ID not found.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (fuProfileImage.HasFile)
        {
            string fileName = Path.GetFileName(fuProfileImage.PostedFile.FileName);
            string imagePath = "~/images/" + fileName;
            fuProfileImage.SaveAs(Server.MapPath(imagePath));

            try
            {
                Con.Open();
                string query = "UPDATE Users SET ProfileImage = @ProfileImage WHERE Id = @TrainerId";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@ProfileImage", imagePath);
                cmd.Parameters.AddWithValue("@TrainerId", trainerId);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Profile image updated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                imgProfile.ImageUrl = imagePath;
                imgProfile.Visible = true;
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
        else
        {
            lblMessage.Text = "Please select an image to upload.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnAddFitnessType_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string description = txtDescription.Text;
        decimal pricePerSession = decimal.Parse(txtPricePerSession.Text); // Parse the price
        int trainerId = GetTrainerId();

        if (trainerId == -1)
        {
            return; // If trainer ID is not found, return early
        }

        string connectionString = @"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" ;

        try
        {
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                Con.Open();
                string query = "INSERT INTO FitnessTypes (Name, Description, PricePerSession, TrainerId) VALUES (@Name, @Description, @PricePerSession, @TrainerId)";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                     cmd.Parameters.AddWithValue("@PricePerSession", pricePerSession);
                    cmd.Parameters.AddWithValue("@TrainerId", trainerId);

                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Fitness type added successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearFields();
                    LoadFitnessTypes(); // Reload fitness types to reflect changes
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message + " StackTrace: " + ex.StackTrace;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }



    private void ClearFields()
    {
        txtName.Text = "";
        txtDescription.Text = "";
    }

    private int GetTrainerId()
    {
        if (Session["TrainerId"] != null)
        {
            return (int)Session["TrainerId"];
        }
        else
        {
            // Handle the case where the session variable is not set
            lblMessage.Text = "Error: Trainer ID not found.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return -1; // Return an invalid ID or handle as needed
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int fitnessTypeId = Convert.ToInt32(btn.CommandArgument);
        Response.Redirect("UpdateFitnessType.aspx?Id=" + fitnessTypeId);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int fitnessTypeId = Convert.ToInt32(btn.CommandArgument);

        string connectionString = @"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" ;

        try
        {
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                Con.Open();
                string query = "DELETE FROM FitnessTypes WHERE Id = @FitnessTypeId";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@FitnessTypeId", fitnessTypeId);
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Fitness type deleted successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    LoadFitnessTypes(); // Reload fitness types to reflect changes
                }
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
    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id) return root;
        foreach (Control control in root.Controls)
        {
            Control foundControl = FindControlRecursive(control, id);
            if (foundControl != null) return foundControl;
        }
        return null;
    }
    protected void rptFitnessTypes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
}
