<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakePayment.aspx.cs" Inherits="WalletWebForm.Pages.MakePayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Make Payment</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <h2 class="info">Wallet Payment</h2>
        </div>
        <div style="margin-left: 2%">
            <div class="input-group col-xs-4 ">
                <span class="input-group-addon">+91</span>
                <input type="text" id="mobileInput" class="form-control" placeholder="Mobile" aria-describedby="basic-addon1" />
            </div>
            <div class="input-group col-xs-4 " style="margin-top: 1%; margin-bottom: 1%">
                <span class="input-group-addon">Upi&nbsp;</span>
                <input type="text" id="upiInput" class="form-control" placeholder="UpiId" aria-describedby="basic-addon1" />
            </div>
            <button type="button" id="saveBtn" class="btn btn-success">Save</button>
        </div>
        <div style="margin-left: 2.5%; margin-right: 5%; margin-top: 2%">
            <table class="table" id="walletTable">
                <tr class="success">
                    <th>Select</th>
                    <th>Phone Number</th>
                    <th>Upi Id</th>
                </tr>
            </table>
            <input type="hidden" id="hiddenMobile" value="" />
            <input type="hidden" id="hiddenUpi" value="" />

            <div class="input-group col-xs-4 " style="margin-top: 1%; margin-bottom: 1%">
                <span class="input-group-addon">Amount;</span>
                <input type="text" id="amountInput" class="form-control" placeholder="Amount" aria-describedby="basic-addon1" />
            </div>
            <div id="tranferDiv" style="display: inline-flex; visibility: hidden">
                <h3 class="active">Transferring To : &nbsp</h3>
                <h3 class="active">Mobile : &nbsp</h3>
                <h3 class="active" id="selectedMobile">&nbsp</h3>
                <h3 class="active">,&nbsp Upi : &nbsp</h3>
                <h3 id="selectedUpi" class="">&nbsp</h3>
            </div>
        </div>
        <button style="margin-left: 2%" type="button" id="PaymentBtn" class="btn btn-success">Transfer</button>
        <script type="text/javascript">
            $('#saveBtn').click(function () {
                var payment = {};
                payment.PhoneNumber = Number($('#mobileInput').val());
                payment.UpiId = $('#upiInput').val();
                console.log(JSON.stringify(payment))
                $.ajax({
                    type: "POST",
                    url: "http://localhost:44396/api/Payment/Post",
                    data: JSON.stringify(payment),
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    processData: true,
                    success: function (data) {
                        location.reload();
                    },
                    error: function (data) {
                        alert("fill all details");
                    }
                });
            });
            function onClickSelected(mobile, upi) {
                document.getElementById('tranferDiv').style.visibility = "visible";
                $('#selectedMobile').text(mobile);
                $('#selectedUpi').text(upi);
                $('#hiddenMobile').val(mobile);
                $('#hiddenUpi').val(upi);
            };
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $.ajax({
                    type: "GET",
                    url: "http://localhost:44396/api/Payment/Get",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function (key, value) {
                            console.log(data);
                            var phone = value.PhoneNumber;
                            var upi = value.UpiId;
                            $('<tr class="success">'
                                + '<td><div class="form-check"><input class="form-check-input" type="radio" name="Upi" id="' + upi + '" value="' + upi + '"'
                                + '<label class="form-check-label" onclick="onClickSelected(' + phone + ', \'' + upi + '\')" for ="exampleRadios2"></label> </div>' +
                                '</td><td> ' + phone + '</td > <td>' + upi + '</th> </tr > ').appendTo($('#walletTable'));
                        });
                    },
                });
            });
            $('#PaymentBtn').click(function () {
                var transfer = {};
                transfer.BeneficieryMobile = Number($('#hiddenMobile').val());
                transfer.UpiId = $('#hiddenUpi').val();
                transfer.Amount = $('#amountInput').val();
                transfer.Solution = "";
                console.log(JSON.stringify(payment))
                $.ajax({
                    type: "POST",
                    url: "http://localhost:44396/api/Payment/PaytmTransfer",
                    data: JSON.stringify(payment),
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    processData: true,
                    success: function (data) {
                        location.reload();
                    },
                    error: function (data) {
                        alert("fill all details");
                    }
                });
            });
        </script>
    </form>
</body>
</html>
