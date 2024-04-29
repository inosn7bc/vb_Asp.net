@Code
    ViewBag.Title = "パスワードのリセットの確認"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title</h1>
</hgroup>
<div>
    <p>
        パスワードがリセットされました。@Html.ActionLink("ここをクリックしてログイン", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})してください
    </p>
</div>
