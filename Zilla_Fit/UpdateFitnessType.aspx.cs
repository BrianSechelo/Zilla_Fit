using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateFitnessType : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" );
    int fitnessTypeId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                fitnessTypeId = Convert.ToInt32(Request.QueryString["Id"]);
                LoadFitnessTypeDetails(fitnessTypeId);
            }
            else
            {
                lblMessage.Text = "No fitness type ID provided.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private void LoadFitnessTypeDetails(int id)
    {
        try
        {
            Con.Open();
            string query = "SELECT Name, Description, PricePerSession FROM FitnessTypes WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                txtName.Text = rdr["Name"].ToString();
                txtDescription.Text = rdr["Description"].ToString();
                txtPricePerSession.Text = rdr["PricePerSession"].ToString();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string description = txtDescription.Text;
        decimal pricePerSession = decimal.Parse(txtPricePerSession.Text); // Parse the price
        fitnessTypeId = Convert.ToInt32(Request.QueryString["Id"]);

        try
        {
            Con.Open();
            string query = "UPDATE FitnessTypes SET Name = @Name, Description = @Description, PricePerSession = @PricePerSession WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@PricePerSession", pricePerSession);
            cmd.Parameters.AddWithValue("@Id", fitnessTypeId);

            cmd.ExecuteNonQuery();
            lblMessage.Text = "Fitness type updated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            Response.Redirect("TrainerPanel.aspx");
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
