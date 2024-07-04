<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        .hero-section {
            text-align: center;
            padding: 50px 20px;
            background-color: #007bff;
            color: white;
        }
        .hero-section h1 {
            font-size: 3em;
            margin-bottom: 20px;
        }
        .hero-section p {
            font-size: 1.2em;
            margin-bottom: 30px;
        }
        .hero-section a {
            background-color: #28a745;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
        }
        .hero-section a:hover {
            background-color: #218838;
        }
        .trainers-section {
            padding: 40px 20px;
        }
        .trainers-section h2 {
            text-align: center;
            margin-bottom: 40px;
            color: #333;
        }
        .trainer {
            display: flex;
            margin-bottom: 20px;
            background-color: #f4f4f9;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .trainer img {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            margin-right: 20px;
            object-fit: cover; /* Ensure image covers the area */
        }
        .trainer-info {
            flex-grow: 1;
        }
        .trainer-info h3 {
            margin-top: 0;
            color: #007bff;
        }
        .testimonials-section {
            padding: 40px 20px;
            background-color: #f4f4f9;
        }
        .testimonials-section h2 {
            text-align: center;
            margin-bottom: 40px;
            color: #333;
        }
        .testimonial {
            text-align: center;
            margin-bottom: 20px;
            padding: 20px;
            background-color: white;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .testimonial p {
            font-style: italic;
        }
        .testimonial h4 {
            margin-top: 10px;
            color: #007bff;
        }
    </style>
    <div class="hero-section">
        <h1>Welcome to Zilla Fit</h1>
        <p>Your journey to a healthier and fitter life starts here. Explore our trainers and their specialized fitness types.</p>
        <a href="SignUp.aspx">Get Started</a>
    </div>
    <div class="trainers-section">
        <h2>Our Top Trainers</h2>
        <asp:Repeater ID="rptTrainers" runat="server">
            <ItemTemplate>
                <div class="trainer">
                    <img src='<%# ResolveUrl(Eval("ProfileImage").ToString()) %>' alt="Trainer Image" />
                    <div class="trainer-info">
                        <h3><%# Eval("Name") %></h3>
                        <p><strong>Fitness Types:</strong> <%# Eval("FitnessTypes") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="testimonials-section">
        <h2>What Our Users Say</h2>
        <div class="testimonial">
            <p>"Zilla Fit has transformed my life! The trainers are amazing and the variety of fitness types keeps me motivated."</p>
            <h4>- John Doe</h4>
        </div>
        <div class="testimonial">
            <p>"I love the flexibility and the personalized approach. Highly recommend to anyone looking to get fit."</p>
            <h4>- Jane Smith</h4>
        </div>
    </div>
</asp:Content>
