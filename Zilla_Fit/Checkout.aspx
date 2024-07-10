<%@ Page Title="Check Out" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
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
        .payment-method {
            margin: 20px 0;
        }
        .payment-method label {
            display: block;
            margin-bottom: 10px;
        }
        .payment-method input[type="radio"] {
            margin-right: 10px;
        }
        .btn-pay {
            background-color: #28a745;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            display: block;
            margin: 20px auto;
        }
    </style>
    <div class="container">
        <h1>Checkout</h1>
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:HiddenField ID="hfFitnessTypeId" runat="server" />
        <p><strong>Fitness Type Enrolled:</strong> <asp:Label ID="lblFitnessType" runat="server"></asp:Label></p>
        <p><strong>Total Price:</strong> <asp:Label ID="lblTotalPrice" runat="server"></asp:Label></p>
        <div class="payment-method">
            <h2>Select Payment Method</h2>
            <label>
                <input type="radio" name="paymentMethod" value="Card" /> Pay with Card
            </label>
            <label>
                <input type="radio" name="paymentMethod" value="Mpesa" /> Pay with Mpesa
            </label>
        </div>
        <asp:Button ID="btnPay" runat="server" Text="Pay" CssClass="btn-pay" OnClick="btnPay_Click" />
    </div>
</asp:Content>