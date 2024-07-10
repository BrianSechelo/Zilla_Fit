using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Checkout : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["FitnessTypeId"] != null && Session["FitnessTypeName"] != null && Session["FitnessTypePrice"] != null)
            {
                hfFitnessTypeId.Value = Session["FitnessTypeId"].ToString();
                lblFitnessType.Text = Session["FitnessTypeName"].ToString();
                lblTotalPrice.Text = ((decimal)Session["FitnessTypePrice"]).ToString("C");
            }
            else
            {
                Response.Redirect("ClientPanel.aspx");
            }
        }

        Button loginButton = FindControlRecursive(Master, "btnLogin") as Button;
        Button rigesterButton = FindControlRecursive(Master, "btnRegister") as Button;
        if (loginButton != null)
        {
            loginButton.Text = "Log Out";
            loginButton.Click += new EventHandler(LoginButton_Click);
            rigesterButton.Visible = false;
        }
        else
        {
            // For debugging purposes
            lblMessage.Text = "Login button not found!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout.aspx");
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        string paymentMethod = Request.Form["paymentMethod"];

        if (string.IsNullOrEmpty(paymentMethod))
        {
            lblMessage.Text = "Please select a payment method.";
            return;
        }

        // Process payment (this is a placeholder, replace with actual payment processing logic)
        lblMessage.Text = "Payment successful using " + paymentMethod + "!";
        lblMessage.ForeColor = System.Drawing.Color.Green;

        // After payment, you might want to redirect the user or perform further actions
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
}
