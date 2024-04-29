@ModelType IndexViewModel
@Code
    ViewBag.Title = "管理する"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
<div>
    <h4>アカウント設定を変更してください</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>パスワード:</dt>
        <dd>
            [
            @If Model.HasPassword Then
                @Html.ActionLink("パスワードの変更", "ChangePassword")
            Else
                @Html.ActionLink("作成", "SetPassword")
            End If
            ]
        </dd>
        <dt>外部ログイン:</dt>
        <dd>
            @Model.Logins.Count [
            @Html.ActionLink("管理", "ManageLogins") ]
        </dd>
            2 要素認証システムでの検証の 2 番目の要素として、電話番号を使用できます。
             
             SMS を使用する 2 要素認証をサポートするように、この ASP.NET アプリケーションを
                設定する方法の詳細については、<a href="https://go.microsoft.com/fwlink/?LinkId=403804">この資料</a>をご覧ください。
             
             2 要素認証を設定した後、次のブロックをコメント解除します

            <dt>電話番号:</dt>
            <dd>
                @(If(Model.PhoneNumber, "None"))
                @If (Model.PhoneNumber <> Nothing) Then
                    @<br />
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Change", "AddPhoneNumber")&nbsp;&nbsp;]</text>
                    @Using Html.BeginForm("RemovePhoneNumber", "Manage", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken
                        @<text>[<input type="submit" value="削除" class="btn-link" />]</text>
                    End Using
                Else
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Add", "AddPhoneNumber") &nbsp;&nbsp;]</text>
                End If
            </dd>

        <dt>2 要素認証:</dt>
        <dd>
            <p>
                2 要素認証プロバイダーが構成されていません。2 要素認証をサポートするように、この ASP.NET アプリケーションを
                            設定する方法の詳細については、<a href="https://go.microsoft.com/fwlink/?LinkId=403804">この資料</a>をご覧ください。
            </p>
                @If Model.TwoFactor Then
                    @Using Html.BeginForm("DisableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      有効
                      <input type="submit" value="無効化" class="btn btn-link" />
                      </text>
                    End Using
                Else
                    @Using Html.BeginForm("EnableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      無効
                      <input type="submit" value="有効化" class="btn btn-link" />
                      </text>
                    End Using
                End If
        </dd>
    </dl>
</div>
