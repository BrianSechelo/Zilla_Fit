using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(@"Server=sql.bsite.net\MSSQL2016;Database=briansechelo_;User Id=briansechelo_;password=Topverbalist7;Integrated Security=False" );

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTrainers();
        }
    }

    private void LoadTrainers()
    {
        try
        {
            Con.Open();
            string query = @"
                SELECT TOP 5 u.Name, u.ProfileImage, 
                    STUFF((SELECT ', ' + ft.Name 
                           FROM FitnessTypes ft 
                           WHERE ft.TrainerId = u.Id 
                           FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS FitnessTypes
                FROM Users u
                WHERE u.Istrainer = 'Yes'
            ";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptTrainers.DataSource = dt;
            rptTrainers.DataBind();
        }
        catch (Exception ex)
        {
            // Handle exception
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
