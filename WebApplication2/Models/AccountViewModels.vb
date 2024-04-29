Imports System.ComponentModel.DataAnnotations

Public Class ExternalLoginConfirmationViewModel
    <Required>
    <Display(Name:="電子メール")>
    Public Property Email As String
End Class

Public Class ExternalLoginListViewModel
    Public Property ReturnUrl As String
End Class

Public Class SendCodeViewModel
    Public Property SelectedProvider As String
    Public Property Providers As ICollection(Of System.Web.Mvc.SelectListItem)
    Public Property ReturnUrl As String
    Public Property RememberMe As Boolean
End Class

Public Class VerifyCodeViewModel
    <Required>
    Public Property Provider As String
    
    <Required>
    <Display(Name:="コード")>
    Public Property Code As String
    
    Public Property ReturnUrl As String
    
    <Display(Name:="認証情報をこのブラウザーに保存しますか?")>
    Public Property RememberBrowser As Boolean

    Public Property RememberMe As Boolean
End Class

Public Class ForgotViewModel
    <Required>
    <Display(Name:="電子メール")>
    Public Property Email As String
End Class

Public Class LoginViewModel
    <Required>
    <Display(Name:="電子メール")>
    <EmailAddress>
    Public Property Email As String

    <Required>
    <DataType(DataType.Password)>
    <Display(Name:="パスワード")>
    Public Property Password As String

    <Display(Name:="このアカウントを記憶する")>
    Public Property RememberMe As Boolean
End Class

Public Class RegisterViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="電子メール")>
    Public Property Email As String

    <Required>
    <StringLength(100, ErrorMessage:="{0} の長さは {2} 文字以上である必要があります。", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="パスワード")>
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="パスワードの確認入力")>
    <Compare("Password", ErrorMessage:="パスワードと確認のパスワードが一致しません。")>
    Public Property ConfirmPassword As String
End Class

Public Class ResetPasswordViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="電子メール")>
    Public Property Email() As String

    <Required>
    <StringLength(100, ErrorMessage:="{0} の長さは {2} 文字以上である必要があります。", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="パスワード")>
    Public Property Password() As String

    <DataType(DataType.Password)>
    <Display(Name:="パスワードの確認入力")>
    <Compare("Password", ErrorMessage:="パスワードと確認のパスワードが一致しません。")>
    Public Property ConfirmPassword() As String

    Public Property Code() As String
End Class

Public Class ForgotPasswordViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="電子メール")>
    Public Property Email() As String
End Class
