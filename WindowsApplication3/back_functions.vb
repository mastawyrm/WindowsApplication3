Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions

Module back_functions
    Public xlApp As New Excel.Application
    Public workBookOut As Excel.Workbook

    Public Sub listSearch(ByVal IPlist As Array, ByVal any As Boolean)
        Dim allObjects As New List(Of String)
        xlApp.SheetsInNewWorkbook = 1
        workBookOut = xlApp.Workbooks.Add()
        For Each IP In IPlist
            Dim names As New List(Of String)
            Dim foundGroups As New List(Of String)
            Dim currentObjects As New List(Of String)
            Dim ruleIDs As New List(Of String)
            Dim nameSearched As List(Of String) = nameSearch(IP, any)
            If nameSearched IsNot Nothing Then names.AddRange(nameSearched)
            Dim groupSearched As List(Of String) = groupSearch(names)
            If groupSearched IsNot Nothing Then foundGroups.AddRange(groupSearched)
            currentObjects = names.Union(foundGroups).ToList
            allObjects = allObjects.Union(currentObjects).ToList
            ruleIDs.AddRange(IDsearch(currentObjects, "both"))

            IPsheet(ruleIDs, IP)
        Next
        groupSheet(allObjects)
        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try
    End Sub

    Public Sub ruleDump(ByVal any As Boolean)
        Dim allObjects As New List(Of String)
        allObjects = addressNames.Union(groups).ToList
        xlApp.SheetsInNewWorkbook = 1
        workBookOut = xlApp.Workbooks.Add
        IPsheet("all", "rules")
        groupSheet(allObjects)
        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try
    End Sub

    Public Sub SRC_DSTsearch(ByVal source As String, ByVal destination As String, ByVal any As Boolean)
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
            Dim srcSearched As List(Of String) = nameSearch(source, any)
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
            Dim dstSearched As List(Of String) = nameSearch(destination, any)
            If dstSearched IsNot Nothing Then dstObjects.AddRange(dstSearched)
            Dim dstGRPsearched As List(Of String) = groupSearch(dstObjects)
            If dstGRPsearched IsNot Nothing Then dstObjects = dstObjects.Union(dstSearched).ToList
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

        IPsheet(matchedIDs, "Relevant Rules")
        groupSheet(allObjects)
        Try
            xlApp.Visible = True
        Catch
            MessageBox.Show("Excel broke something")
            xlApp.Quit()
        End Try

    End Sub

    Public Function loadFile(ByVal fName As String, ByVal fType As String)
        If fType Is "excel" Then
            Return excelLoad(fName)
        ElseIf fType Is "fortiConf" Then
            Return fortiLoad(fName)
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

    ' open conf file from Fortinets
    Private Function fortiLoad(ByVal confName As String)
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
        Dim addressSection As Boolean = False
        Dim groupSection As Boolean = False
        Dim policySection As Boolean = False
        Dim serviceSection As Boolean = False
        Dim lineSplit() As String
        ' counting variables for policy filling
        Dim max As New Integer
        Dim srcZNcount As New Integer
        Dim dstZNcount As New Integer
        Dim srcCount As New Integer
        Dim dstCount As New Integer
        Dim svcCount As New Integer
        Dim serviceIndex As New Integer
        For Each line As String In lines
            ' gather address names, IPs, and masks
            line = line.Trim
            If addressSection Then
                If line Like "edit*" Then addressNames.Add(line.Split("""")(1))
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

                If line Like "set logtraffic *" Then policyLOG.Add(line.Split(" ")(2))

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
                    If policyACT.Count < policyID.Count Then policyACT.Add("deny")
                    For index As Integer = 1 To (max - 1)
                        policyACT.Add(policyACT.Last)
                        policyID.Add(policyID.Last)
                        policyLOG.Add(policyLOG.Last)
                    Next
                End If

                If line Like "end" Then policySection = False
            End If

            ' 0 = service name, 1 = protocol type, 2 = protocol number, 3 = tcp source, 4 = tcp dest,
            ' 5 = udp source, 6 = udp dest
            If serviceSection Then
                If line Like "edit *" Then
                    services.Add(New List(Of String))
                    services(serviceIndex).AddRange({line.Split("""")(1), "noProtType", "noProtNum", "noTCPsrc",
                                                    "noTCPdest", "noUDPsrc", "noUDPdest"})
                End If
                If line Like "set protocol *" Then services(serviceIndex)(1) = line.Split(" ")(2)
                If line Like "set protocol-number *" Then services(serviceIndex)(2) = line.Split(" ")(2)
                If line Like "set tcp-portrange *" Then
                    Dim whole = line.Remove(0, 18)
                    If whole Like "*:*" Then
                        services(serviceIndex)(3) = whole.Split(":")(1)
                        services(serviceIndex)(4) = whole.Split(":")(0)
                    Else
                        services(serviceIndex)(4) = whole
                    End If
                End If
                If line Like "set udp-portrange *" Then
                    Dim whole = line.Remove(0, 18)
                    If whole Like "*:*" Then
                        services(serviceIndex)(5) = whole.Split(":")(1)
                        services(serviceIndex)(6) = whole.Split(":")(0)
                    Else
                        services(serviceIndex)(6) = whole
                    End If
                End If
                Dim bleh = "bleh"
                If line Like "next" Then serviceIndex += 1
                If line Like "end" Then serviceSection = False
                End If


                ' check for section
                If line Like "config firewall address" Then addressSection = True
            If line Like "config firewall addrgrp" Then groupSection = True
            If line Like "config firewall policy" Then policySection = True
            If line Like "config firewall service custom" Then
                serviceIndex = 0
                serviceSection = True
            End If

        Next
        Return confName
    End Function
    ' make a new worksheet and fill with relevant rules
    Private Sub IPsheet(ByVal IDlist, ByVal sheetName)
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
                worksheet.Cells.EntireRow.Item(rowIndex).Interior.ColorIndex = 16
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
                worksheet.Cells.Item(rowIndex, 5) = policyDST(IDindex)
                worksheet.Cells.Item(rowIndex, 6) = policySVC(IDindex)
                worksheet.Cells.Item(rowIndex, 7) = policyACT(IDindex)
                worksheet.Cells.Item(rowIndex, 8) = policyLOG(IDindex)
                rowIndex += 1
            End If
            lastID = ID
            IDindex += 1
        Next
        worksheet.Columns.EntireColumn.AutoFit()
    End Sub

    ' make a worksheet and fill it with an exploded view of all mentioned groups
    Private Sub groupSheet(ByVal groupList)
        Dim worksheet As Excel.Worksheet
        worksheet = workBookOut.Worksheets.Item(xlApp.Worksheets.Count)
        worksheet.Name = "Address Groups"
        Dim columnIndex As Int32 = 1
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
    Private Function nameSearch(ByVal searchIP, ByVal any)
        Dim found As Boolean
        Dim ADDindex As Int16 = 0
        Dim CIDRmask As String
        Dim relevantNames As New List(Of String)
        For Each address In addresses
            found = False
            'If masks(ADDindex) Is "255.255.255.255" Then
            'If address Is searchIP Then found = True
            If masks(ADDindex) IsNot "" Then
                Dim bin As String = extractValidIP(masks(ADDindex), True)
                CIDRmask = bin.Split("1").Count - 1
                If isIPinSubnet(searchIP, address, CIDRmask) Then found = True
                If Not any Then
                    If masks(ADDindex) Like "0.0.0.0" Then found = False
                End If
            End If
            If found Then relevantNames.Add(addressNames(ADDindex))
            ADDindex += 1
        Next
        If any Then relevantNames.add("all")
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
