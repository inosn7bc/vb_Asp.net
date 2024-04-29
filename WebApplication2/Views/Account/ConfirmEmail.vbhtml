@Code
    ViewBag.Title = "電子メールの確認"
End Code

<h2>@ViewBag.Title.</h2>
<div>
    <p>
        電子メールの確認を行っていただきありがとうございます。 してください @Html.ActionLink("ここをクリックしてログイン", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>
