<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_old-Signature2.aspx.cs" Inherits="FormulaireIntervention.Views.Home.Signature2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="../../Scripts/signature_pad.umd.js"></script>
    <script type="text/javascript">
        $(function () {
            var canvas = document.querySelector('#signature');
            var pad = new SignaturePad(canvas);
            var data = "";
            $('#accept').click(function () {

                data = pad.toDataURL();
                $('#savetarget').attr('src', data);

                pad.off();

            });
        });
    </script>
    
    <title></title>
</head>
<body>
<h2>Client Sign</h2>
    <div class="container container-fluid">
        <form action="#" method="post" id="formSignature" class="form-group" runat="server">
            <canvas class="panel panel-default" width="500" height="400" id="signature" style="border:1px solid black"></canvas>
            <div class="row"><button type="button" id="accept" class="btn btn-primary">Accept signature</button></div>
            <img class="img-fluid" width="500" height="400" id="savetarget" style="border:1px solid black" runat="server"/>
            <div class="row" id="div2">
                <asp:HiddenField id="data" runat="server"/>
                <button type="submit" id="save" class="btn btn-primary">Save</button>
                <a name="dlSign" id="dlSign">Download Signature</a>
            </div>
            
    </form>
</div>
</body>
</html>
