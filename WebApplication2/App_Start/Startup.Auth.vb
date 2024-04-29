Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Owin

Partial Public Class Startup
    ' 認証の構成の詳細については、https://go.microsoft.com/fwlink/?LinkId=301864 を参照してください
    Public Sub ConfigureAuth(app As IAppBuilder)
        ' 要求ごとに 1 つのインスタンスを使用するよう db コンテキスト、ユーザー マネージャー、サインイン マネージャーを構成します
        app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
        app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
        app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)

        ' サインインしたユーザーの情報を保存するために、アプリケーションが Cookie を使用できるようにします
        ' また、サードパーティのログイン プロバイダーを使用してログインするユーザーに関する情報を、Cookie を使用して一時的に保存できるようにします
        ' サインイン Cookie を構成します
        ' OnValidateIdentity を使用すると、ユーザーがログインするときにセキュリティ スタンプを検証できるようになります。
        ' これはセキュリティ機能の 1 つであり、パスワードを変更するときやアカウントに外部ログインを追加するときに使用されます。
        app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
            .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            .Provider = New CookieAuthenticationProvider() With {
                .OnValidateIdentity = SecurityStampValidator.OnValidateIdentity(Of ApplicationUserManager, ApplicationUser)(
                    validateInterval:=TimeSpan.FromMinutes(30),
                    regenerateIdentity:=Function(manager, user) user.GenerateUserIdentityAsync(manager))},
            .LoginPath = New PathString("/Account/Login")})

        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

        ' 2 要素認証処理で第 2 の要素を確認する際に、アプリケーションがユーザー情報を一時的に保存できるようにします。
        app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(25))
        'app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromSeconds(11))

        ' アプリケーションが電話やメールなどの第 2 のログイン確認要素を記憶できるようにします。
        ' このオプションをオンにすると、ログイン処理における確認の 2 番目のステップが、ログインしたデバイスで記憶されます。
        ' これは、ログインするときの RememberMe オプションと似ています。
        app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)


        ' 次の行のコメントを解除して、サード パーティ ログイン プロバイダーを使用したログインを有効にします
        'app.UseMicrosoftAccountAuthentication(
        '    clientId:="",
        '    clientSecret:="")

        'app.UseTwitterAuthentication(
        '   consumerKey:="",
        '   consumerSecret:="")

        'app.UseFacebookAuthentication(
        '   appId:="",
        '   appSecret:="")

        'app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With {
        '   .ClientId = "",
        '   .ClientSecret = ""})
    End Sub
End Class
