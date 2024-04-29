@ModelType ExternalLoginConfirmationViewModel
@Code
    ViewBag.Title = "登録"
End Code

<h2>@ViewBag.Title.</h2>
<h3>@ViewBag.LoginProvider アカウントを関連付けます。</h3>

@Using Html.BeginForm("ExternalLoginConfirmation", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
    <h4>関連付けフォーム</h4>
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <p class="text-info">
        この <strong>@ViewBag.LoginProvider</strong> での認証に成功しました。
        ログインを完了するには、このサイトでのユーザー名を下の欄に入力して [登録] ボタンを
        クリックしてください。
    </p>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" class="btn btn-default" value="登録" />
        </div>
    </div>
    </text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
