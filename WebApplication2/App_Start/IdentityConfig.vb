Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports System.IO

Public Class EmailService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' 電子メールを送信するには、電子メール サービスをここにプラグインします。

        Dim filePath As String = "C:\Users\inosn\Documents\backup\emailTest.txt"
        File.AppendAllText(filePath, $"{message.Subject} : {message.Body}")

        Return Task.FromResult(0)
    End Function
End Class

Public Class SmsService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' テキスト メッセージを送信するための SMS サービスをここにプラグインします。
        Dim filePath As String = "C:\Users\inosn\Documents\backup\smsTest.txt"
        File.AppendAllText(filePath, Now)
        File.AppendAllText(filePath, Environment.NewLine)
        File.AppendAllText(filePath, $"{message.Subject} : {message.Body}")
        File.AppendAllText(filePath, Environment.NewLine)


        Return Task.FromResult(0)
    End Function
End Class

' このアプリケーションで使用されるアプリケーション ユーザー マネージャーを設定します。UserManager は ASP.NET Identity の中で定義されており、このアプリケーションで使用されます。
Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser)

    Public Sub New(store As IUserStore(Of ApplicationUser))
        MyBase.New(store)
    End Sub

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
        Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

        ' ユーザー名の検証ロジックを設定します
        manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
            .AllowOnlyAlphanumericUserNames = False,
            .RequireUniqueEmail = True
        }

        ' パスワードの検証ロジックを設定します
        manager.PasswordValidator = New PasswordValidator With {
            .RequiredLength = 1
        }

        ' ユーザー ロックアウトの既定値を設定します。
        manager.UserLockoutEnabledByDefault = True
        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(25)
        manager.MaxFailedAccessAttemptsBeforeLockout = 15

        ' 2 要素認証プロバイダーを登録します。このアプリケーションでは、Phone and Emails をユーザー検証用コード受け取りのステップとして使用します。
        ' 独自のプロバイダーをプログラミングしてここにプラグインできます。
        manager.RegisterTwoFactorProvider("電話コード", New PhoneNumberTokenProvider(Of ApplicationUser) With {
                                          .MessageFormat = "あなたのセキュリティ コードは {0} です。"
                                      })
        manager.RegisterTwoFactorProvider("電子メール コード", New EmailTokenProvider(Of ApplicationUser) With {
                                          .Subject = "セキュリティ コード",
                                          .BodyFormat = "あなたのセキュリティ コードは {0} です。"
                                          })
        manager.EmailService = New EmailService()
        manager.SmsService = New SmsService()



        Dim dataProtectionProvider = options.DataProtectionProvider
        If (dataProtectionProvider IsNot Nothing) Then
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
        End If

        Return manager
    End Function

End Class

' このアプリケーションで使用されるアプリケーション サインイン マネージャーを構成します。
Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, String)
    Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
    End Function

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function
End Class
