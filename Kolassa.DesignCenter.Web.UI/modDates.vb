Module modDates
    Function fLastMonth(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        fLastMonth = True
        ldTemp = DateAdd(DateInterval.Month, -1, Now)
        ldDate1 = CDate(ldTemp.Month & "/1/" & ldTemp.Year)
        ldDate2 = DateAdd(DateInterval.Month, 1, ldDate1)
        ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)
    End Function
    Function fThisMonth(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        fThisMonth = True
        ldTemp = Now
        ldDate1 = CDate(ldTemp.Month & "/1/" & ldTemp.Year)
        ldDate2 = DateAdd(DateInterval.Month, 1, ldDate1)
        ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)
    End Function
    Function fLastYear(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        fLastYear = True
        ldTemp = DateAdd(DateInterval.Month, -1, Now)
        ldDate1 = CDate("1/1/" & ldTemp.Year)
        ldDate2 = CDate("12/31/" & ldTemp.Year)
    End Function
    Function fThisYear(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        fThisYear = True
        ldTemp = Now
        ldDate1 = CDate("1/1/" & ldTemp.Year)
        ldDate2 = CDate("12/31/" & ldTemp.Year)
    End Function
    Function fYTD(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        fYTD = True
        ldTemp = Now
        ldDate1 = CDate("1/1/" & ldTemp.Year)
        ldDate2 = ldTemp
    End Function
    Function fLastQuarter(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        ldTemp = DateAdd(DateInterval.Quarter, -1, Now)
        fLastQuarter = fGetQuarter(ldTemp, ldDate1, ldDate2)
    End Function
    Function fThisQuarter(ByRef ldDate1, ByRef ldDate2) As Boolean
        Dim ldTemp As Date
        ldTemp = Now
        fThisQuarter = fGetQuarter(ldTemp, ldDate1, ldDate2)
    End Function
    Function fGetQuarter(ByVal ldTemp As Date, ByRef ldDate1 As Date, ByRef ldDate2 As Date) As Boolean
        Dim liMonth As Integer
        fGetQuarter = True
        liMonth = (Int((ldTemp.Month - 1) / 3) * 3) + 1
        ldDate1 = CDate(liMonth & "/1/" & ldTemp.Year)
        ldDate2 = DateAdd(DateInterval.Quarter, 1, ldDate1)
        ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)
    End Function
    Function test() As String
        Dim ld1 As Date
        Dim ld2 As Date
        Dim lbTest As Boolean
        Dim lsString As String = ""
        lbTest = (fLastMonth(ld1, ld2))
        lsString = lsString & " Last Month:   " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        lbTest = (fThisMonth(ld1, ld2))
        lsString = lsString & " This Month:   " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        lbTest = (fLastQuarter(ld1, ld2))
        lsString = lsString & " Last Quarter: " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        lbTest = (fThisQuarter(ld1, ld2))
        lsString = lsString & " This Quarter: " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        lbTest = (fLastYear(ld1, ld2))
        lsString = lsString & " Last Year:    " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        lbTest = (fThisYear(ld1, ld2))
        lsString = lsString & " This Year:    " & ld1 & " " & ld2 & Chr(13) & Chr(10)
        test = (lsString)
    End Function
End Module
