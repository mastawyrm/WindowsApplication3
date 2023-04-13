Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions

Module back_functions
    Public xlApp As New Excel.Application
    Public workBookOut As Excel.Workbook

    Public Sub listSearch(ByVal IPlist As Array, ByVal any As Boolean, ByVal subnets As Boolean, ByVal searchGroups As Boolean, ByVal includeBlanks As Boolean, ByVal allGroupCheck As Boolean, ByVal resolve_check As Boolean, Optional ByVal SVClist As Array = Nothing, Optional ByVal save_all As String = Nothing)
        Dim allObjects As New List(Of String)
        Dim objectReturn As New List(Of String)
        xlApp.SheetsInNewWorkbook = 1
        workBookOut = xlApp.Workbooks.Add
        For Each address In IPlist
            If address IsNot "" Then
                Dim IP As String = address.split("_")(0)
                Dim cidr As String = address.split("_")(1)
                Dim names As New List(Of String)
                Dim foundGroups As New List(Of String)
                Dim currentObjects As New List(Of String)
                Dim ruleIDs As New List(Of String)
                Dim nameSearched As List(Of String) = nameSearch(IP, cidr, any, subnets)
                If nameSearched IsNot Nothing Then names.AddRange(nameSearched)
                If searchGroups Then
                    Dim groupSearched As List(Of String) = groupSearch(names)
                    If groupSearched IsNot Nothing Then foundGroups.AddRange(groupSearched)
                    currentObjects = names.Union(foundGroups).ToList
                    allObjects = allObjects.Union(currentObjects).ToList
                Else
                    currentObjects = names
                End If
                ruleIDs.AddRange(IDsearch(currentObjects, "both"))

                If (ruleIDs.Count <> 0) Or includeBlanks Then
                    objectReturn = IPsheet(ruleIDs, currentObjects, IP)
                End If
                If allGroupCheck Then allObjects = allObjects.Union(objectReturn).ToList
            End If
        Next

        If searchGroups Then groupSheet(allObjects, resolve_check)

        If save_all IsNot Nothing Then
            workBookOut.SaveAs(save_all & ".xls")
        End If

        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try
    End Sub

    Public Sub ruleDump(ByVal any As Boolean, ByVal resolve_check As Boolean, Optional ByVal save_all As String = Nothing)
        Dim allObjects As New List(Of String)
        allObjects = addressNames.Union(groups).ToList
        xlApp.SheetsInNewWorkbook = 1
        workBookOut = xlApp.Workbooks.Add
        IPsheet("all", "potato", "rules")
        groupSheet(allObjects, resolve_check)
        serviceSheet(services, serviceGroup)

        If save_all IsNot Nothing Then
            workBookOut.SaveAs(save_all & ".xls")
        End If

        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try
    End Sub

    Public Sub SRC_DSTsearch(ByVal source As String, ByVal destination As String, ByVal any As Boolean, ByVal subnets As Boolean, ByVal allGroupCheck As Boolean, ByVal resolve_check As Boolean, Optional ByVal save_all As String = Nothing)
        Dim objectReturn As New List(Of String)
        Dim allObjects As New List(Of String)
        Dim srcObjects As New List(Of String)
        Dim dstObjects As New List(Of String)
        xlApp.SheetsInNewWorkbook = 1
        workBookOut = xlApp.Workbooks.Add()
        ' collect the relevant objects containing the source address then search for rule IDs
        Dim which As Integer = 0
        Dim srcIDs As New List(Of String)
        If extractValidIP(source) IsNot Nothing Then
            which += 1
            srcObjects.Clear()
            Dim srcSearched As List(Of String) = nameSearch(source, "32", any, subnets)
            If srcSearched IsNot Nothing Then srcObjects.AddRange(srcSearched)
            Dim srcGRPsearched As List(Of String) = groupSearch(srcObjects)
            If srcGRPsearched IsNot Nothing Then srcObjects = srcObjects.Union(srcGRPsearched).ToList
            srcIDs.AddRange(IDsearch(srcObjects, "source"))
        End If

        ' collect the relevant objects containing the destination address then search for rule IDs
        Dim dstIDs As New List(Of String)
        If extractValidIP(destination) IsNot Nothing Then
            which += 10
            dstObjects.Clear()
            Dim dstSearched As List(Of String) = nameSearch(destination, "32", any, subnets)
            If dstSearched IsNot Nothing Then dstObjects.AddRange(dstSearched)
            Dim dstGRPsearched As List(Of String) = groupSearch(dstObjects)
            If dstGRPsearched IsNot Nothing Then dstObjects = dstObjects.Union(dstGRPsearched).ToList
            dstIDs.AddRange(IDsearch(dstObjects, "destination"))
        End If

        Dim matchedIDs As New List(Of String)
        If which = 1 Then
            allObjects = srcObjects
            matchedIDs = srcIDs
        ElseIf which = 10 Then
            allObjects = dstObjects
            matchedIDs = dstIDs
        ElseIf which = 11 Then
            allObjects = srcObjects.Union(dstObjects).ToList
            matchedIDs = srcIDs.Intersect(dstIDs).ToList
        End If

        objectReturn = IPsheet(matchedIDs, allObjects, "Relevant Rules")
        If allGroupCheck Then allObjects = allObjects.Union(objectReturn).ToList
        groupSheet(allObjects, resolve_check)

        If save_all IsNot Nothing Then
            workBookOut.SaveAs(save_all & ".xls")
        End If

        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try

    End Sub

    Public Function loadFile(ByVal fName As String, ByVal fType As String, ByVal selectedVdom As String, ByVal vdoms As List(Of String))
        If fType Is "excel" Then
            Return excelLoad(fName)
        ElseIf fType Is "fortiConf" Then
            Return fortiLoad(fName, selectedVdom, vdoms)
        ElseIf fType Is "ssgConf" Then
            Return ssgLoad(fName)
        Else
            Return Nothing
        End If
    End Function


    ' set of main data variables to be used no matter which filetype is used
    '   from address worksheet
    Public addressNames As New List(Of String)
    Public addresses As New List(Of String)
    Public masks As New List(Of String)
    '   from address group worksheet
    Public groups As New List(Of String)
    Public groupMembers As New List(Of String)
    '   from policies worksheet
    Public policyID As New List(Of String)
    Public policySRCzone As New List(Of String)
    Public policyDSTzone As New List(Of String)
    Public policySRC As New List(Of String)
    Public policyDST As New List(Of String)
    Public policySVC As New List(Of String)
    Public policyACT As New List(Of String)
    Public policyLOG As New List(Of String)

    ' 0 = service name, 1 = protocol type, 2 = protocol number, 3 = tcp source, 4 = tcp dest,
    ' 5 = udp source, 6 = udp dest
    Public services As New List(Of List(Of String))
    Public serviceGroup As New List(Of List(Of String))


    ' open excel file in order to fill the public variables
    Private Function excelLoad(ByVal xlName As String)
        addressNames.Clear()
        addresses.Clear()
        masks.Clear()
        groups.Clear()
        groupMembers.Clear()
        policyID.Clear()
        policySRCzone.Clear()
        policyDSTzone.Clear()
        policySRC.Clear()
        policyDST.Clear()
        policySVC.Clear()
        policyACT.Clear()
        policyLOG.Clear()


        Dim xlWorkBook As Excel.Workbook
        Dim policyWS As Excel.Worksheet
        Dim addressGroupWS As Excel.Worksheet
        Dim addressesWS As Excel.Worksheet

        Try
            xlWorkBook = xlApp.Workbooks.Open(xlName)



            ' find the right worksheet by name
            Dim names As New List(Of String)
            For Each sheet In xlWorkBook.Sheets
                names.Add(sheet.name)
            Next

            ' set indexes of worksheets
            Dim policyIndex As Int16 = names.IndexOf("policies") + 1
            Dim addGroupIndex As Int16 = names.IndexOf("address groups") + 1
            Dim addressesIndex As Int16 = names.IndexOf("addresses") + 1

            ' assign variables to each sheet
            policyWS = xlWorkBook.Sheets.Item(policyIndex)
            addressGroupWS = xlWorkBook.Sheets.Item(addGroupIndex)
            addressesWS = xlWorkBook.Sheets.Item(addressesIndex)

            ' read data tables into arrays
            '   addresses
            Dim rowCount As Short = addressesWS.UsedRange.Rows.Count
            For Each cell In addressesWS.Range("D2", "D" & rowCount).Value2
                addressNames.Add(cell)
            Next
            For Each cell In addressesWS.Range("E2", "E" & rowCount).Value2
                addresses.Add(cell)
            Next
            For Each cell In addressesWS.Range("F2", "F" & rowCount).Value2
                masks.Add(cell)
            Next
            '   address groups
            rowCount = addressGroupWS.UsedRange.Rows.Count
            For Each cell In addressGroupWS.Range("D2", "D" & rowCount).Value2
                groups.Add(cell)
            Next
            For Each cell In addressGroupWS.Range("E2", "E" & rowCount).Value2
                groupMembers.Add(cell)
            Next
            '   policies
            rowCount = policyWS.UsedRange.Rows.Count
            For Each cell In policyWS.Range("D2", "D" & rowCount).Value2
                policyID.Add(cell)
            Next
            For Each cell In policyWS.Range("F2", "F" & rowCount).Value2
                policySRCzone.Add(cell)
            Next
            For Each cell In policyWS.Range("G2", "G" & rowCount).Value2
                policyDSTzone.Add(cell)
            Next
            For Each cell In policyWS.Range("I2", "I" & rowCount).Value2
                policySRC.Add(cell)
            Next
            For Each cell In policyWS.Range("K2", "K" & rowCount).Value2
                policyDST.Add(cell)
            Next
            For Each cell In policyWS.Range("L2", "L" & rowCount).Value2
                policySVC.Add(cell)
            Next
            For Each cell In policyWS.Range("M2", "M" & rowCount).Value2
                policyACT.Add(cell)
            Next
            For Each cell In policyWS.Range("N2", "N" & rowCount).Value2
                policyLOG.Add(cell)
            Next

        Catch
            MessageBox.Show("Unknown error")
            xlApp.Quit()
            Return Nothing
        End Try
        Return xlName

    End Function

    ' open conf file from Juniper
    Private Function ssgLoad(ByVal confName As String)
        addressNames.Clear()
        addresses.Clear()
        masks.Clear()
        groups.Clear()
        groupMembers.Clear()
        policyID.Clear()
        policySRCzone.Clear()
        policyDSTzone.Clear()
        policySRC.Clear()
        policyDST.Clear()
        policySVC.Clear()
        policyACT.Clear()
        policyLOG.Clear()
        services.Clear()

        Dim lines() As String = IO.File.ReadAllLines(confName)

        ' 0= none, 1 = address section, 2 = group section, 3 = policy section, 4 = service section
        Dim section As Integer = 0
        Dim lineSplit() As String

        ' counting variables for policy filling
        Dim max As New Integer
        Dim srcZNcount As New Integer
        Dim dstZNcount As New Integer
        Dim srcCount As New Integer
        Dim dstCount As New Integer
        Dim svcCount As New Integer
        Dim serviceIndex As Integer = 0
        For Each line As String In lines




            ' address section - gather address names, IPs, and masks
            If section = 1 Then

            End If

            ' group section - gather groups and members
            If section = 2 Then

            End If

            ' policy section - gather rule details
            If section = 3 Then
                If line Like vbTab & vbTab & ": (*" Then
                    policyID.Add(line.Split("(")(1))
                End If

            End If

            ' service section - gather defined services
            ' 0 = service name, 1 = protocol type, 2 = source port, 3 = destination port
            If section = 4 Then

            End If

            If line Like vbTab & ":*" Then section = 0

            ' check for section
            If section = 0 Then
                If line Like vbTab & ":address (" Then
                    section = 1
                ElseIf line Like vbTab & ":addrgroup (" Then
                    section = 2
                ElseIf line Like vbTab & ":policy (" Then
                    section = 3
                End If
            End If
        Next
        Return confName
    End Function

    ' open conf file from Fortinets
    Private Function fortiLoad(ByVal confName As String, ByVal selectedVdom As String, ByVal vdoms As List(Of String))
        addressNames.Clear()
        addresses.Clear()
        masks.Clear()
        groups.Clear()
        groupMembers.Clear()
        policyID.Clear()
        policySRCzone.Clear()
        policyDSTzone.Clear()
        policySRC.Clear()
        policyDST.Clear()
        policySVC.Clear()
        policyACT.Clear()
        policyLOG.Clear()
        services.Clear()
        serviceGroup.Clear()

        Dim lines() As String = IO.File.ReadAllLines(confName)
        Dim iprangeSection As Boolean = False
        Dim addressSection As Boolean = False
        Dim groupSection As Boolean = False
        Dim policySection As Boolean = False
        Dim serviceSection As Boolean = False
        Dim serviceGRPsection As Boolean = False
        Dim correctVdom As Boolean = False
        Dim sourceNegate As Boolean = False
        Dim destNegate As Boolean = False
        Dim lineSplit() As String
        Dim max As New Integer          ' counting variables for policy filling
        Dim iprangeStart As String
        Dim iprangeEnd As String
        Dim iprangeList() As String
        Dim srcZNcount As New Integer
        Dim dstZNcount As New Integer
        Dim srcCount As New Integer
        Dim dstCount As New Integer
        Dim svcCount As New Integer
        Dim logCount As New Integer
        Dim serviceIndex As Integer = 0
        Dim backCount As Integer = 0
        Dim serviceGRPindex As Integer = 0
        Dim currentAddressName As String


        For Each line As String In lines
            ' gather address names, IPs, and masks
            line = line.Trim

            If correctVdom Then
                If addressSection Then
                    If line Like "edit*" Then
                        currentAddressName = line.Split("""")(1)
                        addressNames.Add(currentAddressName)
                    End If
                    If line Like "set type geography" Then
                        masks.Add("geography")
                    End If
                    If line Like "set country*" Then
                        addresses.Add("Country code: " + line.Split(" ")(2))
                        Dim test = 0
                    End If
                    If line Like "set type fqdn" Then
                        masks.Add("fqdn")
                    End If
                    If line Like "set fqdn*" Then
                        addresses.Add(line.Split(" ")(2))
                        Dim test = 0
                    End If
                    If line Like "set type iprange*" Then
                        addressNames.Remove(currentAddressName)

                        iprangeSection = True
                    End If
                    If iprangeSection Then
                        If line Like "set start-ip*" Then
                            iprangeStart = line.Split(" ")(2)
                            firstStart = Convert.ToInt32(iprangeStart.Split(".")(0))
                            secondStart = Convert.ToInt32(iprangeStart.Split(".")(1))
                            thirdStart = Convert.ToInt32(iprangeStart.Split(".")(2))
                            fourthStart = Convert.ToInt32(iprangeStart.Split(".")(3))
                        End If
                        If line Like "set end-ip*" Then
                            iprangeEnd = line.Split(" ")(2)
                            firstEnd = Convert.ToInt32(iprangeEnd.Split(".")(0))
                            secondEnd = Convert.ToInt32(iprangeEnd.Split(".")(1))
                            thirdEnd = Convert.ToInt32(iprangeEnd.Split(".")(2))
                            fourthEnd = Convert.ToInt32(iprangeEnd.Split(".")(3))
                            For firstCurrent As Int32 = firstStart To firstEnd
                                If firstStart = firstEnd Then
                                    secondEndMod = secondEnd
                                Else
                                    secondEndMod = 255
                                End If
                                For secondCurrent = secondStart To secondEndMod
                                    If secondStart = secondEnd Then
                                        thirdEndMod = thirdEnd
                                    Else
                                        thirdEndMod = 255
                                    End If
                                    For thirdCurrent = thirdStart To thirdEndMod
                                        If thirdStart = thirdEnd Then
                                            fourthEndMod = fourthEnd
                                        Else
                                            fourthEndMod = 255
                                        End If
                                        For fourthCurrent = fourthStart To fourthEndMod
                                            current = firstCurrent.ToString + "." + secondCurrent.ToString + "." + thirdCurrent.ToString + "." + fourthCurrent.ToString
                                            addressNames.Add(current + "/32")
                                            addresses.Add(current)
                                            masks.Add("255.255.255.255")
                                            groupMembers.Add(current + "/32")
                                            groups.Add(currentAddressName)
                                            If groups.Count <> groupMembers.Count Then blah = true
                                        Next
                                    Next
                                Next

                            Next


                            iprangeSection = False
                        End If
                    End If
                    If line Like "set subnet*" Then
                        addresses.Add(line.Split(" ")(2))
                        masks.Add(line.Split(" ")(3))
                    End If
                    If line Like "next*" Then
                        If addresses.Count <> addressNames.Count Then
                            addresses.Add("")
                            masks.Add("")
                        End If
                    End If
                    If line Like "end" Then addressSection = False
                End If

                ' gather groups and members
                If groupSection Then
                    If line Like "edit*" Then groups.Add(line.Split("""")(1))
                    If line Like "set member*" Then
                        lineSplit = line.Split("""")
                        groupMembers.Add(lineSplit(1))
                        If lineSplit.Count > 3 Then
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                groupMembers.Add(lineSplit(index))
                                groups.Add(groups.Last)
                            Next
                        End If
                    End If
                    If line Like "end" Then groupSection = False
                End If

                ' gather rule details
                If policySection Then
                    If line Like "edit*" Then
                        policyID.Add(line.Split(" ")(1))
                        max = 1
                    End If

                    If line Like "set srcintf*" Then
                        lineSplit = line.Split("""")
                        policySRCzone.Add(lineSplit(1))
                        srcZNcount = 1
                        If lineSplit.Count > 3 Then
                            srcZNcount = (lineSplit.Count - 1) / 2
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                policySRCzone.Add(lineSplit(index))
                            Next
                            If max < srcZNcount Then max = srcZNcount
                        End If
                    End If

                    If line Like "set dstintf *" Then
                        lineSplit = line.Split("""")
                        policyDSTzone.Add(lineSplit(1))
                        dstZNcount = 1
                        If lineSplit.Count > 3 Then
                            dstZNcount = (lineSplit.Count - 1) / 2
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                policyDSTzone.Add(lineSplit(index))
                            Next
                            If max < dstZNcount Then max = dstZNcount
                        End If
                    End If

                    If line Like "set srcaddr *" Then
                        lineSplit = line.Split("""")
                        policySRC.Add(lineSplit(1))
                        srcCount = 1
                        If lineSplit.Count > 3 Then
                            srcCount = (lineSplit.Count - 1) / 2
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                policySRC.Add(lineSplit(index))
                            Next
                            If max < srcCount Then max = srcCount
                        End If
                    End If

                    If line Like "set dstaddr *" Then
                        lineSplit = line.Split("""")
                        policyDST.Add(lineSplit(1))
                        dstCount = 1
                        If lineSplit.Count > 3 Then
                            dstCount = (lineSplit.Count - 1) / 2
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                policyDST.Add(lineSplit(index))
                            Next
                            If max < dstCount Then max = dstCount
                        End If
                    End If

                    If line Like "set action *" Then policyACT.Add(line.Split(" ")(2))

                    If line Like "set service *" Then
                        lineSplit = line.Split("""")
                        policySVC.Add(lineSplit(1))
                        svcCount = 1
                        If lineSplit.Count > 3 Then
                            svcCount = (lineSplit.Count - 1) / 2
                            For index As Integer = 3 To lineSplit.Count - 1 Step 2
                                policySVC.Add(lineSplit(index))
                            Next
                            If max < svcCount Then max = svcCount
                        End If
                    End If

                    If line Like "set logtraffic *" Then
                        policyLOG.Add(line.Split(" ")(2))
                        logCount = 1
                    End If

                    If line Like "set srcaddr-negate enable*" Then
                        sourceNegate = True
                    End If

                    If line Like "set dstaddr-negate enable*" Then
                        destNegate = True
                    End If

                    If line Like "next" Then
                        If max > srcZNcount Then
                            For index As Integer = 1 To (max - srcZNcount)
                                policySRCzone.Add("")
                            Next
                        End If
                        If max > dstZNcount Then
                            For index As Integer = 1 To (max - dstZNcount)
                                policyDSTzone.Add("")
                            Next
                        End If
                        If max > srcCount Then
                            For index As Integer = 1 To (max - srcCount)
                                policySRC.Add("")
                            Next
                        End If
                        If max > dstCount Then
                            For index As Integer = 1 To (max - dstCount)
                                policyDST.Add("")
                            Next
                        End If
                        If max > svcCount Then
                            For index As Integer = 1 To (max - svcCount)
                                policySVC.Add("")
                            Next
                        End If
                        If max > logCount Then
                            For index As Integer = 1 To (max - logCount)
                                policyLOG.Add("")
                            Next
                        End If
                        If policyACT.Count < policyID.Count Then policyACT.Add("deny")

                        For index As Integer = 1 To (max - 1)
                            policyACT.Add(policyACT.Last)
                            policyID.Add(policyID.Last)
                        Next

                        If sourceNegate Then
                            For index As Integer = (policyID.Count - max) To (policyID.Count - max + dstCount - 1)
                                Dim bleh = policySRC(index)
                            Next
                        End If

                        If destNegate Then
                            For index As Integer = (policyID.Count - max) To (policyID.Count - max + dstCount - 1)
                                '  policyDST(index) = "(NEGATE)   " + policyDST(index)
                            Next
                        End If
                        srcZNcount = 0
                        dstZNcount = 0
                        srcCount = 0
                        dstCount = 0
                        svcCount = 0
                        logCount = 0
                        sourceNegate = False
                        destNegate = False
                    End If

                    If line Like "end" Then policySection = False
                End If

                ' 0 = service name, 1 = protocol type, 2 = protocol number, 3 = tcp source, 4 = tcp dest,
                ' 5 = udp source, 6 = udp dest
                If serviceSection Then
                    If line Like "edit*" Then
                        services.Add(New List(Of String))
                        services(serviceIndex).AddRange({line.Split("""")(1), "noProtType", "noProtNum", "noTCPsrc",
                                                    "noTCPdest", "noUDPsrc", "noUDPdest"})
                    End If
                    If line Like "set protocol *" Then services(serviceIndex)(1) = line.Split(" ")(2)
                    If line Like "set protocol-number *" Then services(serviceIndex)(2) = line.Split(" ")(2)
                    If line Like "set tcp-portrange *" Then
                        Dim whole = line.Remove(0, 18)
                        Dim parts = whole.split(" ")
                        Dim partCount As Int32 = 0
                        While parts.length > partCount
                            If parts(partCount) Like "*:*" Then
                                services(serviceIndex)(3) = parts(partCount).Split(":")(1)
                                services(serviceIndex)(4) = parts(partCount).Split(":")(0)
                            Else
                                services(serviceIndex)(4) = parts(partCount)
                            End If
                            If (parts.length - partCount) > 1 Then
                                services.Add(New List(Of String))
                                serviceIndex += 1
                                Dim prevName = services(serviceIndex - 1)(0)
                                services(serviceIndex).AddRange({prevName, "noProtType", "noProtNum", "noTCPsrc",
                                                    "noTCPdest", "noUDPsrc", "noUDPdest"})
                            End If
                            partCount += 1
                        End While
                    End If
                    If line Like "set udp-portrange *" Then
                        While services(serviceIndex).Item(0) Like """"
                            serviceIndex -= 1
                            backCount += 1
                        End While
                        Dim whole = line.Remove(0, 18)
                        Dim parts = whole.split(" ")
                        Dim partCount As Int32 = 0
                        While parts.length > partCount
                            If parts(partCount) Like "*:*" Then
                                services(serviceIndex)(5) = parts(partCount).Split(":")(1)
                                services(serviceIndex)(6) = parts(partCount).Split(":")(0)
                            Else
                                services(serviceIndex)(6) = parts(partCount)
                            End If
                            If (parts.length - partCount) > 1 Then
                                serviceIndex += 1
                                If backCount > 0 Then backCount -= 1
                                If services.Count <= serviceIndex Then
                                    services.Add(New List(Of String))
                                    Dim prevName = services(serviceIndex - 1)(0)
                                    services(serviceIndex).AddRange({prevName, "noProtType", "noProtNum", "noTCPsrc",
                                                    "noTCPdest", "noUDPsrc", "noUDPdest"})
                                End If
                            End If
                            partCount += 1
                        End While
                    End If
                    If line Like "next" Then
                        serviceIndex += backCount
                        serviceIndex += 1
                        backCount = 0
                    End If
                    If line Like "end" Then serviceSection = False
                End If

                ' gather service groups
                If serviceGRPsection Then
                    If line Like "edit*" Then
                        serviceGroup.Add(New List(Of String))
                        serviceGroup(serviceGRPindex).Add(line.Split("""")(1))
                    End If
                    If line Like "set member*" Then
                        lineSplit = line.Split("""")
                        For Each item In lineSplit
                            If Not item Like " " And Not item Like "set member " Then
                                serviceGroup(serviceGRPindex).Add(item)
                            End If
                        Next
                    End If
                    If line Like "next" Then serviceGRPindex += 1
                    If line Like "end" Then serviceGRPsection = False
                End If


                ' check for section
                If line Like "config firewall address" Then addressSection = True
                If line Like "config firewall addrgrp" Then groupSection = True
                If line Like "config firewall policy" Then policySection = True
                If line Like "config firewall service custom" Then
                    serviceSection = True
                    backCount = 0
                End If
                If line Like "config firewall service group" Then serviceGRPsection = True
            End If
            If Not addressSection AndAlso Not groupSection AndAlso Not policySection AndAlso Not serviceSection AndAlso Not serviceGRPsection Then
                If line Like "edit*" AndAlso vdoms.Contains(line.Split(" ")(1)) Then
                    If line.Split(" ")(1) Like selectedVdom Then
                        correctVdom = True
                    Else
                        correctVdom = False
                    End If
                ElseIf selectedVdom Like "" Then
                    correctVdom = True
                End If
            End If
        Next
        Return confName
    End Function

    ' get vdoms from Forticonfs
    Public Function vdomLoad(ByVal confName As String)
        Dim vdomSection As Boolean = False
        Dim vdoms As New List(Of String)
        Dim lines() As String = IO.File.ReadAllLines(confName)

        For Each line As String In lines
            line = line.Trim
            If vdomSection = True Then
                If line Like "edit*" Then vdoms.Add(line.Split(" ")(1))
                If line Like "end" Then
                    vdomSection = False
                    Exit For
                End If
            End If

            If line Like "config vdom" Then vdomSection = True
        Next

        Return vdoms
    End Function
    ' make a new worksheet and fill with relevant rules
    Private Function IPsheet(ByVal IDlist, ByVal actualContainers, ByVal sheetName)
        Dim mentionedObjects As New List(Of String)

        Dim worksheet As Excel.Worksheet
        worksheet = workBookOut.Worksheets.Add()
        worksheet.Name = sheetName
        worksheet.Cells.Item(1, 1) = "Policy ID"
        worksheet.Cells.Item(1, 2) = "Source Zone"
        worksheet.Cells.Item(1, 3) = "Destination Zone"
        worksheet.Cells.Item(1, 4) = "Source Addresses"
        worksheet.Cells.Item(1, 5) = "Destination Addresses"
        worksheet.Cells.Item(1, 6) = "Services"
        worksheet.Cells.Item(1, 7) = "Action"
        worksheet.Cells.Item(1, 8) = "Logging"
        worksheet.Cells.EntireRow.Item(1).Font.Bold = True
        worksheet.Cells.EntireRow.Item(1).Borders.LineStyle = 1
        worksheet.Cells.EntireRow.Item(1).Borders.Weight = 2
        Dim IDindex As Int32 = 0
        Dim rowIndex As Int32 = 2
        Dim lastID As String = Nothing
        Dim lastSRCzone As String = ""
        Dim lastDSTzone As String = ""
        Dim found As Boolean = True
        Dim nextSRCzoneIndex As Integer = 0
        Dim nextDSTzoneIndex As Integer = 0
        For Each ID In policyID
            If lastID <> ID And found Then
                worksheet.Cells.EntireRow.Item(rowIndex).Interior.ColorIndex = 44 '16
                rowIndex += 1
                lastSRCzone = ""
                lastDSTzone = ""
                found = False
            End If
            If IDlist.contains(ID) OrElse IDlist.Equals("all") Then
                found = True

                If Not policySRCzone(IDindex).Equals(lastSRCzone) Then
                    If lastID = ID Then
                        worksheet.Cells.Item(nextSRCzoneIndex, 2) = policySRCzone(IDindex)
                        If Not rowIndex.Equals(nextSRCzoneIndex) Then rowIndex = nextSRCzoneIndex - 1
                    Else worksheet.Cells.Item(rowIndex, 2) = policySRCzone(IDindex)
                    End If
                    nextSRCzoneIndex = rowIndex + 1
                End If
                lastSRCzone = policySRCzone(IDindex)
                If Not policyDSTzone(IDindex).Equals(lastDSTzone) Then
                    If lastID = ID Then
                        worksheet.Cells.Item(nextDSTzoneIndex, 3) = policyDSTzone(IDindex)
                        If Not rowIndex.Equals(nextDSTzoneIndex) Then rowIndex = nextDSTzoneIndex - 1
                    Else worksheet.Cells.Item(rowIndex, 3) = policyDSTzone(IDindex)
                    End If
                    nextDSTzoneIndex = rowIndex + 1
                End If
                lastDSTzone = policyDSTzone(IDindex)
                worksheet.Cells.Item(rowIndex, 1) = policyID.Item(IDindex)
                worksheet.Cells.Item(rowIndex, 4) = policySRC(IDindex)
                mentionedObjects.Add(policySRC(IDindex))
                If actualContainers.contains(policySRC(IDindex)) And policySRC(IDindex) IsNot "" Then worksheet.Cells.Item(rowIndex, 4).Interior.ColorIndex = 6
                worksheet.Cells.Item(rowIndex, 5) = policyDST(IDindex)
                mentionedObjects.Add(policyDST(IDindex))
                If actualContainers.contains(policyDST(IDindex)) And policyDST(IDindex) IsNot "" Then worksheet.Cells.Item(rowIndex, 5).Interior.ColorIndex = 6
                worksheet.Cells.Item(rowIndex, 6) = policySVC(IDindex)
                worksheet.Cells.Item(rowIndex, 7) = policyACT(IDindex)
                worksheet.Cells.Item(rowIndex, 8) = policyLOG(IDindex)
                rowIndex += 1
            End If
            lastID = ID
            IDindex += 1
        Next
        worksheet.Columns.EntireColumn.AutoFit()

        Return mentionedObjects.Distinct.ToList
    End Function

    ' make a worksheet and fill it with an exploded view of all mentioned groups
    Private Sub groupSheet(ByVal groupList, ByVal resolve)
        Dim worksheet As Excel.Worksheet
        worksheet = workBookOut.Worksheets.Item(xlApp.Worksheets.Count)
        If resolve Then
            worksheet.Name = "Address Groups(Names)"
        Else
            worksheet.Name = "Address Groups"
        End If

        Dim columnIndex As Int32 = 1
        Dim addrIndex As Int32 = 0
        Dim IPstring As String = ""
        For Each grp In groupList
            Dim rowIndex As Int32 = 1
            worksheet.Cells.Item(rowIndex, columnIndex) = grp
            Dim found As Boolean = False
            For index As Int32 = 0 To groups.Count - 1
                If groups.Item(index).Equals(grp) Then
                    rowIndex += 1
                    worksheet.Cells.Item(rowIndex, columnIndex) = groupMembers.Item(index)
                    found = True
                End If
            Next
            If found Then columnIndex += 1
        Next
        worksheet.Cells.EntireRow.Item(1).Font.Bold = True
        worksheet.Cells.EntireRow.Item(1).Borders.LineStyle = 1
        worksheet.Cells.EntireRow.Item(1).Borders.Weight = 2
        worksheet.Columns.EntireColumn.AutoFit()

        If resolve Then
            worksheet = workBookOut.Worksheets.Add()
            worksheet.Name = "Address Groups(IPs)"
            workBookOut.Worksheets("Address Groups(IPs)").Move(after:=workBookOut.Worksheets("Address Groups(Names)"))
            columnIndex = 1
            addrIndex = 0
            IPstring = ""
            For Each grp2 In groupList
                Dim rowIndex As Int32 = 1
                worksheet.Cells.Item(rowIndex, columnIndex) = grp2
                Dim found As Boolean = False
                For index As Int32 = 0 To groups.Count - 1
                    If groups.Item(index).Equals(grp2) Then
                        rowIndex += 1

                        If Not groups.Contains(groupMembers.Item(index)) Then
                            addrIndex = addressNames.IndexOf(groupMembers.Item(index))
                            If Not masks(addrIndex) Like "geography" And Not masks(addrIndex) Like "fqdn" And Not masks(addrIndex) Like "" Then
                                Dim bin As String = extractValidIP(masks(addrIndex), True)
                                Dim CIDRmask As String = bin.Split("1").Count - 1
                                IPstring = addresses.Item(addrIndex) + "/" + CIDRmask
                            Else
                                IPstring = addresses.Item(addrIndex)
                            End If
                        Else
                            IPstring = groupMembers.Item(index)
                        End If

                        worksheet.Cells.Item(rowIndex, columnIndex) = IPstring
                        found = True
                    End If
                Next
                If found Then columnIndex += 1
            Next
            worksheet.Cells.EntireRow.Item(1).Font.Bold = True
            worksheet.Cells.EntireRow.Item(1).Borders.LineStyle = 1
            worksheet.Cells.EntireRow.Item(1).Borders.Weight = 2
            worksheet.Columns.EntireColumn.AutoFit()
        End If
    End Sub

    Private Sub serviceSheet(ByVal serviceList, ByVal serviceGroups)
        Dim worksheet As Excel.Worksheet
        worksheet = workBookOut.Worksheets.Add()
        worksheet.Name = "Services"
        workBookOut.Worksheets("Services").Move(after:=workBookOut.Worksheets.Item(xlApp.Worksheets.Count))
        worksheet.Cells.Item(1, 1) = "Service Name"
        worksheet.Cells.Item(1, 2) = "Protocol"
        worksheet.Cells.Item(1, 3) = "Protocol number"
        worksheet.Cells.Item(1, 4) = "TCP source"
        worksheet.Cells.Item(1, 5) = "TCP destination"
        worksheet.Cells.Item(1, 6) = "UDP source"
        worksheet.Cells.Item(1, 7) = "UDP destination"
        worksheet.Cells.EntireRow.Item(1).font.bold = True
        For count As Int32 = 1 To 7
            worksheet.Cells.Item(1, count).borders.linestyle = 1
            worksheet.Cells.Item(1, count).borders.weight = 2
        Next
        Dim rowIndex As Int32 = 2
        For Each service In serviceList
            Dim name = service.item(0)
            Dim protocol = service.item(1)
            Dim protNum = service.item(2)
            Dim TCPsrc = service.item(3)
            Dim TCPdest = service.item(4)
            Dim UDPsrc = service.item(5)
            Dim UDPdest = service.item(6)
            worksheet.Cells.Item(rowIndex, 1) = name
            If Not protocol Like "noProtType" Then worksheet.Cells.Item(rowIndex, 2) = protocol
            If Not protNum Like "noProtNum" Then worksheet.Cells.Item(rowIndex, 3) = protNum
            If Not TCPsrc Like "noTCPsrc" Then worksheet.Cells.Item(rowIndex, 4) = TCPsrc
            If Not TCPdest Like "noTCPdest" Then worksheet.Cells.Item(rowIndex, 5) = TCPdest
            If Not UDPsrc Like "noUDPsrc" Then worksheet.Cells.Item(rowIndex, 6) = UDPsrc
            If Not UDPdest Like "noUDPdest" Then worksheet.Cells.Item(rowIndex, 7) = UDPdest
            rowIndex += 1
        Next
        worksheet.Columns.EntireColumn.AutoFit()

        worksheet = workBookOut.Worksheets.Add()
        worksheet.Name = "Service Groups"
        workBookOut.Worksheets("Service Groups").Move(after:=workBookOut.Worksheets("Services"))

        Dim columnIndex As New Integer
        columnIndex = 1
        For Each grp In serviceGroups
            rowIndex = 1
            For Each member In grp
                worksheet.Cells.Item(rowIndex, columnIndex) = member
                If rowIndex = 1 Then worksheet.Cells.Item(rowIndex, columnIndex).font.bold = True
                rowIndex += 1
            Next
            columnIndex += 1
        Next
        worksheet.Columns.EntireColumn.AutoFit()
    End Sub

    ' regex to return an IP from within a string and the option to return it as binary
    Public Function extractValidIP(ByVal IP As String, Optional ByVal bin As Boolean = False)
        Dim regex As Regex = New Regex("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)")
        Dim match As Match = regex.Match(IP)
        Dim out As String = Nothing
        Dim binary As String = Nothing
        If match.Success Then
            out = match.Value
        End If
        If Not out Is Nothing And bin Then
            For Each octet As Int16 In out.Split(".")
                binary += (Convert.ToString(octet, 2)).PadLeft(8, "0")
            Next
            out = binary
        End If
        Return out
    End Function

    Private Function isIPinSubnet(ByVal address, ByVal identity, ByVal CIDR)
        ' convert the IP and identity to binary and compare them using the mask
        Dim IPbin As String
        Dim IDbin As String
        Dim result As Boolean
        IPbin = extractValidIP(address, True)
        IDbin = extractValidIP(identity, True)
        result = IPbin.Substring(0, CIDR) Like IDbin.Substring(0, CIDR)
        Return result
    End Function

    ' check an IP to gather what address objects contain it
    Private Function nameSearch(ByVal searchIP, ByVal CIDRin, ByVal any, ByVal subnets)
        Dim found As Boolean
        Dim ADDindex As Int32 = 0
        Dim CIDRmask As String
        Dim relevantNames As New List(Of String)
        For Each address In addresses
            found = False
            'If masks(ADDindex) Is "255.255.255.255" Then
            'If address Is searchIP Then found = True
            If masks(ADDindex) IsNot "" And masks(ADDindex) IsNot "fqdn" And masks(ADDindex) IsNot "geography" Then
                Dim bin As String = extractValidIP(masks(ADDindex), True)
                CIDRmask = bin.Split("1").Count - 1
                If searchIP Like address AndAlso CIDRin Like CIDRmask Then found = True
                If subnets And isIPinSubnet(searchIP, address, CIDRmask) Then found = True
                If Not any Then
                    If masks(ADDindex) Like "0.0.0.0" Then found = False
                End If
            End If
            If found Then relevantNames.Add(addressNames(ADDindex))
            ADDindex += 1
        Next
        If any Then relevantNames.Add("all")
        Return relevantNames
    End Function

    ' search address groups for relevant containers
    Private Function groupSearch(ByVal searchName As List(Of String))
        Dim relevantGroups As New List(Of String)
        Dim newGroups As New List(Of String)
        For Each name In searchName
            Dim index As Integer = 0
            Dim group As New List(Of String)
            For Each member In groupMembers
                If member.Equals(name) Then
                    group.Add(groups(index))
                End If
                index += 1
            Next
            newGroups.AddRange(group)
            relevantGroups.AddRange(group)
        Next
        If newGroups.Count <> 0 Then relevantGroups.AddRange(groupSearch(newGroups))
        Return relevantGroups
    End Function

    ' search rule list and return list of ruleIDs when an address/group is used
    Private Function IDsearch(ByVal objects As List(Of String), ByVal src_dst As String)
        Dim result As New List(Of String)
        Dim foundSRCid As New List(Of String)
        Dim foundDSTid As New List(Of String)
        Dim index As Integer = 0
        For Each entry In policyID
            If objects.Contains(policySRC.Item(index)) Then foundSRCid.Add(entry)
            If objects.Contains(policyDST.Item(index)) Then foundDSTid.Add(entry)
            index += 1
        Next
        If src_dst Like "source" Then result = foundSRCid.Distinct.ToList
        If src_dst Like "destination" Then result = foundDSTid.Distinct.ToList
        If src_dst Like "both" Then result = foundSRCid.Union(foundDSTid).ToList

        Return result
    End Function

End Module
