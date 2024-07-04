<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainerPanel.aspx.cs" Inherits="TrainerPanel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trainer Panel</title>
    <style>
        .container {
            width: 300px;
            margin: 100px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            background-color: #fff;
        }
        .container h2 {
            text-align: center;
        }
        .profile-container {
            width: 100%;
            text-align: center;
        }
        .profile-container img {
            max-width: 200px; /* Set the maximum width */
            max-height: 200px; /* Set the maximum height */
            border-radius: 50%;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
        }
        .form-group input, .form-group textarea, .form-group select {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }
        .btn {
            width: 100%;
            padding: 10px;
            border: none;
            background-color: #007BFF;
            color: #fff;
            cursor: pointer;
            border-radius: 5px;
            margin-top: 10px;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .btn-update, .btn-delete {
            margin-top: 10px;
            background-color: #28a745;
            color: white;
            border: none;
            padding: 10px;
            text-align: center;
            display: inline-block;
            font-size: 16px;
            cursor: pointer;
        }
        .btn-delete {
            background-color: #dc3545;
        }
        .fitness-type {
            margin-top: 20px;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin-bottom: 10px;
        }
    </style>
    <script>
        function previewImage(input) {
            var preview = document.getElementById('<%= imgProfile.ClientID %>');
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="profile-container">
                <asp:Image ID="imgProfile" runat="server" />
                <div class="form-group">
                    <asp:FileUpload ID="fuProfileImage" runat="server" />
                </div>
                <asp:Button ID="btnUpdateProfileImage" runat="server" Text="Update Profile Image" CssClass="btn-submit" OnClick="btnUpdateProfileImage_Click" />
            </div>

            <h2>Add Fitness Type</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div class="form-group">
                <label for="txtName">Fitness Type Name:</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPricePerSession">Price Per Session:</label>
                <asp:TextBox ID="txtPricePerSession" runat="server"></asp:TextBox>
            </div>
            <asp:Button ID="btnAddFitnessType" runat="server" Text="Add Fitness Type" CssClass="btn" OnClick="btnAddFitnessType_Click" />
            <h2>Fitness Types</h2>
            <asp:Repeater ID="rptFitnessTypes" runat="server" OnItemCommand="rptFitnessTypes_ItemCommand">
                <ItemTemplate>
                    <div class="fitness-type">
                        <h4><%# Eval("Name") %> - <%# Eval("Description") %> - $<%# Eval("PricePerSession") %></h4>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandArgument='<%# Eval("Id") %>' OnClick="btnUpdate_Click" CssClass="btn-update" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="btnDelete_Click" CssClass="btn-delete" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
