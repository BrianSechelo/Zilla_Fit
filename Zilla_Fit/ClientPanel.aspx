<%@ Page Title="Client Panel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ClientPanel.aspx.cs" Inherits="ClientPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
            background-color: white;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .fitness-types {
            margin: 20px 0;
        }
        .fitness-type {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #ddd;
            padding: 10px 0;
        }
        .fitness-type:last-child {
            border-bottom: none;
        }
        .fitness-type-info {
            flex: 1;
        }
        .fitness-type-img {
            flex: 0 0 100px;
            text-align: center;
        }
        .fitness-type-img img {
            max-width: 100px;
            max-height: 100px;
            border-radius: 50%;
        }
        .enroll-button {
            background-color: #007bff;
            color: white;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
            margin-top: 10px;
        }
        .profile {
            margin: 20px 0;
        }
        .profile input[type="text"], .profile input[type="email"], .profile input[type="password"] {
            width: 100%;
            padding: 10px;
            margin: 5px 0;
            box-sizing: border-box;
        }
        .profile button {
            background-color: #28a745;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            margin-top: 10px;
        }
    </style>
    <div class="container">
        <h1>Client Panel</h1>
        <div class="fitness-types">
            <h2>Available Fitness Types</h2>
            <asp:Repeater ID="rptFitnessTypes" runat="server" OnItemCommand="rptFitnessTypes_ItemCommand">
                <ItemTemplate>
                    <div class="fitness-type">
                        <div class="fitness-type-info">
                            <p><strong>Name:</strong> <%# Eval("Name") %></p>
                            <p><strong>Description:</strong> <%# Eval("Description") %></p>
                            <p><strong>Trainer:</strong> <%# Eval("TrainerName") %></p>
                            <p><strong>Price per Session:</strong> <%# Eval("PricePerSession", "{0:C}") %></p>
                            <asp:Button ID="btnEnroll" runat="server" Text="Enroll" CssClass="enroll-button" CommandArgument='<%# Eval("Id") %>' OnClick="btnEnroll_Click" />
                        </div>
                        <div class="fitness-type-img">
                            <asp:Image ID="imgTrainer" runat="server" ImageUrl='<%# Eval("ProfileImage") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="profile">
            <h2>Manage Profile</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox>
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox>
            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
        </div>
    </div>
</asp:Content>
