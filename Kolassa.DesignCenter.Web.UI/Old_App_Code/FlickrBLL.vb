Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports FlickrNet
Imports System.ComponentModel
Imports System.Configuration

Namespace Infrastructure.BLL
    <DataObject(True)>
    <Serializable()>
    Public Class FlickrBLL

        <DataObjectMethodAttribute(DataObjectMethodType.Select, True)>
        Public Function GetPagedSet(setId As String, maximumRows As Integer, startRowIndex As Integer) As PhotosetPhotoCollection

            Dim Flickr As Flickr = New Flickr(ConfigurationManager.AppSettings("apiKey"), ConfigurationManager.AppSettings("shardSecret"))

            Dim photos As PhotosetPhotoCollection = Flickr.PhotosetsGetPhotos(setId, GetPageIndex(startRowIndex, maximumRows) + 1, maximumRows)
            ' uploadPicture("C:\\data\\c.png")
            Return photos
        End Function


        Public Function uploadPicture(FileName As String) As String
            uploadPicture = ""
            Try
                Dim lsVerifier As String = ""
                Dim Flickr As Flickr = New Flickr(ConfigurationManager.AppSettings("apiKey"), ConfigurationManager.AppSettings("shardSecret"))
                Flickr.OAuthAccessToken = ConfigurationManager.AppSettings("Flickr.OAuthAccessToken")
                Flickr.OAuthAccessTokenSecret = ConfigurationManager.AppSettings("Flickr.OAuthAccessTokenSecret")
                Dim checkt As Object = Flickr.AuthOAuthCheckToken()
                If checkt Is Nothing Then
                    Dim rt As FlickrNet.OAuthRequestToken = Flickr.OAuthGetRequestToken("oob")
                    Dim url As String = Flickr.OAuthCalculateAuthorizationUrl(rt.Token, AuthLevel.Write)
                    Dim requestToken As OAuthRequestToken = Flickr.OAuthGetRequestToken(lsVerifier)
                    lsVerifier = InputBox("Code")
                    Dim accessToken As Object = Flickr.OAuthGetAccessToken(rt, lsVerifier)
                    'ConfigurationManager.AppSettings.Remove("Flickr.OAuthAccessToken")
                    'ConfigurationManager.AppSettings.Add("Flickr.OAuthAccessToken", accessToken.)
                End If
                uploadPicture = Flickr.UploadPicture(FileName)
            Catch ex As Exception
                MsgBox(ex.InnerException.ToString)
            End Try

        End Function
        Public Function GetPagedSetCount(setId As String) As Integer

            Dim Flickr As Flickr = New Flickr(ConfigurationManager.AppSettings("apiKey"), ConfigurationManager.AppSettings("shardSecret"))
            Dim sets As Photoset = Flickr.PhotosetsGetInfo(setId)
            Return sets.NumberOfPhotos
        End Function

        <DataObjectMethodAttribute(DataObjectMethodType.Select, False)>
        Public Function GetPhotoSetsByUser(userID As String) As PhotosetCollection
            Dim apiKey As String = ConfigurationManager.AppSettings("apiKey")
            Dim Secret As String = ConfigurationManager.AppSettings("shardSecret")
            Dim flickr As Flickr = New Flickr(apiKey, Secret)


            GetPhotoSetsByUser = flickr.PhotosetsGetList(userID)

        End Function

        Protected Function GetPageIndex(startRowIndex As Integer, maximumRows As Integer) As Integer
            Dim dstart As Double
            Dim dMax As Double
            If maximumRows <= 0 Then
                Return 0
            Else
                dstart = System.Convert.ToDouble(startRowIndex)
                dMax = System.Convert.ToDouble(maximumRows)
                Return Math.Floor(dstart / dMax)
            End If
        End Function
    End Class
    Public Class FlickrManager

        Dim apiKey As String = ConfigurationManager.AppSettings("apiKey")
        Dim SharedSecret As String = ConfigurationManager.AppSettings("shardSecret")
        Dim flickr As Flickr = New Flickr(apiKey, SharedSecret)


        Public Function GetAuthInstance() As Flickr

            Dim f As Flickr = New Flickr(apiKey, SharedSecret)
            If (OAuthToken Is Nothing) Then

                f.OAuthAccessToken = OAuthToken.Token
                f.OAuthAccessTokenSecret = OAuthToken.TokenSecret
            End If
            Return f
   End Function

        Public Property OAuthToken() As OAuthAccessToken

            Get

                If (HttpContext.Current.Request.Cookies("OAuthToken") IsNot Nothing) Then
                    Return Nothing
                End If
                Dim values = HttpContext.Current.Request.Cookies("OAuthToken").Values
				'   Dim mystring As String = "  {  FullName = " & values("FullName") & " , 
				'               Token = " & values("Token") & ", 
				'                TokenSecret = " & values("TokenSecret") & ", 
				'               UserId = " & values("UserId") & ", 
				'              Username = " & values("Username") & "}"
				' Return New OAuthAccessToken(mystring)
				Dim t As New OAuthAccessToken
				t.FullName = values("FullName")
                t.Token = values("Token")
                t.TokenSecret =  values("TokenSecret")
                t.UserId = values("UserId")
                t.Username = values("Username")
                Return t
            End Get
            Set

                '// Stores the authentication token in a cookie which will expire in 1 hour
                Dim cookie As HttpCookie = New HttpCookie("OAuthToken")

                cookie.Expires = DateTime.UtcNow.AddHours(1)

                cookie.Values("FullName") = value.FullName
                cookie.Values("Token") = value.Token
                cookie.Values("TokenSecret") = value.TokenSecret
                cookie.Values("UserId") = value.UserId
                cookie.Values("Username") = value.Username
                HttpContext.Current.Response.AppendCookie(cookie)
            End Set
        End Property
    End Class
End Namespace