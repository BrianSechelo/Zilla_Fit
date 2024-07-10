<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            display: flex;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 700px; /* Increased width */
            justify-content: space-between;
        }
        .signup-container {
            width: 60%;
            padding-right: 20px;
        }
        .preview-container {
            width: 35%;
            display: flex;
            justify-content: center;
            align-items: center;
            border-left: 1px solid #ddd;
            padding-left: 20px;
        }
        .preview-container img {
            max-width: 100%;
            max-height: 300px;
            display: none;
        }
        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }
        label {
            color: #555;
            display: block;
            margin-bottom: 5px;
        }
        input[type="text"], input[type="password"], .dropdownlist {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        input[type="text"]:focus, input[type="password"]:focus {
            border-color: #007BFF;
        }
        .btn-submit {
            background-color: #007BFF;
            color: #fff;
            border: none;
            padding: 10px;
            border-radius: 4px;
            width: 100%;
            cursor: pointer;
        }
        .btn-submit:hover {
            background-color: #0056b3;
        }
        .message {
            text-align: center;
            margin-top: 20px;
            color: red;
        }
    </style>
    <script>
        function previewImage(input) {
            var preview = document.getElementById('<%= imagePreview.ClientID %>');
            var reader = new FileReader();

            reader.onload = function (e) {
                preview.src = e.target.result;
                preview.style.display = 'block';
            }

            if (input.files && input.files[0]) {
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="signup-container">
            <h2>Sign Up</h2>
            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

            <asp:Label ID="lblTrainer" runat="server" Text="Trainer?"></asp:Label>
            <asp:DropDownList ID="ddlTrainer" runat="server" CssClass="dropdownlist">
                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                <asp:ListItem Text="No" Value="No"></asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="lblProfileImage" runat="server" Text="Profile Image:"></asp:Label>
            <asp:FileUpload ID="fuProfileImage" runat="server" OnChange="previewImage(this)" />

            <asp:Button ID="btnSubmit" runat="server" Text="Sign Up" CssClass="btn-submit" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"></asp:Label>
            <asp:Label ID="lblId" runat="server" CssClass="id" Visible="False"></asp:Label>
        </div>
        <div class="preview-container">
            <asp:Image ID="imagePreview" runat="server" src="#" alt="Image Preview" />
        </div>
    </div>
</asp:Content>