Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Net
Imports System.Web

Namespace DMS.DAL.StaticHelper
    Public Module SessionHelper
        Public Sub SetSession(ByVal Data As DMS.DAL.StaticHelper.SystemInfoForSession)
            System.Web.HttpContext.Current.Session("SystemInfoForSession") = Data
        End Sub

        Public Function GetSession() As SystemInfoForSession
            Return CType(System.Web.HttpContext.Current.Session("SystemInfoForSession"), DMS.DAL.StaticHelper.SystemInfoForSession)
        End Function

        Public Function GetIPAddress() As String
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim ipAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

            If Not String.IsNullOrEmpty(ipAddress) Then
                Dim addresses As String() = ipAddress.Split(","c)

                If addresses.Length <> 0 Then
                    Return addresses(0)
                End If
            End If

            Return context.Request.ServerVariables("REMOTE_ADDR")
        End Function

        Public Function GetUserIpInfo(ByVal ip As String) As IpInfo
            Dim ipInfo As DMS.DAL.StaticHelper.IpInfo = New DMS.DAL.StaticHelper.IpInfo() With {
                .Ip = ip,
                .City = "Chabahil",
                .Country = "Nepal"
            }

            If Not DMS.DAL.Properties.Settings.[Default].GetExternalIPInfo Then
                Return ipInfo
            End If

            Try
                Dim info As String = New System.Net.WebClient().DownloadString("http://ipinfo.io/" & ip)
                ipInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DMS.DAL.StaticHelper.IpInfo)(info)
                Dim myRI1 As System.Globalization.RegionInfo = New System.Globalization.RegionInfo(ipInfo.Country)
                ipInfo.Country = myRI1.EnglishName
            Catch __unusedException1__ As System.Exception
                ipInfo.Country = DirectCast(Nothing, System.String)
            End Try

            ipInfo.Ip = ip

            If Equals(ipInfo.City, DirectCast(Nothing, System.String)) Then
                ipInfo.City = "Chabahil"
            End If

            Return ipInfo
        End Function

        Public Function GetUserIpInfo() As IpInfo
            Return DMS.DAL.StaticHelper.SessionHelper.GetUserIpInfo(DMS.DAL.StaticHelper.SessionHelper.GetIPAddress())
        End Function
    End Module

    Public Class IpInfo
        <Newtonsoft.Json.JsonPropertyAttribute("ip")>
        Public Property Ip As String
        <Newtonsoft.Json.JsonPropertyAttribute("hostname")>
        Public Property Hostname As String
        <Newtonsoft.Json.JsonPropertyAttribute("city")>
        Public Property City As String
        <Newtonsoft.Json.JsonPropertyAttribute("region")>
        Public Property Region As String
        <Newtonsoft.Json.JsonPropertyAttribute("country")>
        Public Property Country As String
        <Newtonsoft.Json.JsonPropertyAttribute("loc")>
        Public Property Loc As String
        <Newtonsoft.Json.JsonPropertyAttribute("org")>
        Public Property Org As String
        <Newtonsoft.Json.JsonPropertyAttribute("postal")>
        Public Property Postal As String
    End Class
End Namespace
