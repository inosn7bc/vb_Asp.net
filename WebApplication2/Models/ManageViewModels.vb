Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin.Security

Public Class IndexViewModel
    Public Property HasPassword As Boolean
    Public Property Logins As IList(Of UserLoginInfo)
    Public Property PhoneNumber As String
    Public Property TwoFactor As Boolean
    Public Property BrowserRemembered As Boolean
End Class

Public Class ManageLoginsViewModel
    Public Property CurrentLogins As IList(Of UserLoginInfo)
    Public Property OtherLogins As IList(Of AuthenticationDescription)
End Class

Public Class FactorViewModel
    Public Property Purpose As String
End Class

Public Class SetPasswordViewModel
    <Required>
    <StringLength(100, ErrorMessage:="{0} の長さは {2} 文字以上である必要があります。", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="新しいパスワード")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name:="新しいパスワードの確認入力")>
    <Compare("NewPassword", ErrorMessage:="新しいパスワードと確認のパスワードが一致しません。")>
    Public Property ConfirmPassword As String
End Class

Public Class ChangePasswordViewModel
    <Required>
    <DataType(DataType.Password)>
    <Display(Name:="現在のパスワード")>
    Public Property OldPassword As String

    <Required>
    <StringLength(100, ErrorMessage:="{0} の長さは {2} 文字以上である必要があります。", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="新しいパスワード")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name:="新しいパスワードの確認入力")>
    <Compare("NewPassword", ErrorMessage:="新しいパスワードと確認のパスワードが一致しません。")>
    Public Property ConfirmPassword As String
End Class

Public Class AddPhoneNumberViewModel
    <Required>
    <Phone>
    <Display(Name:="電話番号")>
    Public Property Number As String
End Class

Public Class VerifyPhoneNumberViewModel
    <Required>
    <Display(Name:="コード")>
    Public Property Code As String

    <Required>
    <Phone>
    <Display(Name:="電話番号")>
    Public Property PhoneNumber As String
End Class

Public Class ConfigureTwoFactorViewModel
    Public Property SelectedProvider As String
    Public Property Providers As ICollection(Of System.Web.Mvc.SelectListItem)
End Class