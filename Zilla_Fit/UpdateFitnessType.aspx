<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateFitnessType.aspx.cs" Inherits="UpdateFitnessType" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Fitness Type</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .container {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            width: 300px;
        }

        h2 {
            text-align: center;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
        }

        .form-group input, .form-group textarea {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }

        .btn {
            display: inline-block;
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-align: center;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .message {
            text-align: center;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Update Fitness Type</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="message" ForeColor="Red"></asp:Label>
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
            <asp:Button ID="btnUpdate" runat="server" Text="Update Fitness Type" CssClass="btn" OnClick="btnUpdate_Click" />
        </div>
    </form>
</body>
</html>
