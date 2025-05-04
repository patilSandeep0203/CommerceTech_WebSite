<!--#Include File="p3pheader.asp"-->
<!--#Include File="dbconnect.asp"-->
<%
'*************************************************************************
'*   Product:  Affiliate Wiz Version 3.0
'*   Copyright (c) 2000-2002 by AdComplete.com, LLC.  All rights reserved.
'*************************************************************************
	If Request("Task")="Get" And Request("Browser")="N" Then
		If Instr(Ucase(Request.ServerVariables("HTTP_USER_AGENT")),"MSIE")>0 Then
			Response.Buffer=True
			Response.ContentType="application/x-javascript"
			Response.Write "document.write(' '); "
			Response.End
		End If
	End If
	
	'If Request("Task")="" Then
	'	Response.Redirect Application("afw_WebSiteURL")
	'End If

	'Retrieve AffiliateID
	If Request("AffiliateID")<>"" Then
		lngAffiliateID=Clng(Request("AffiliateID"))
	Else	
		If Request("A")<>"" Then
			lngAffiliateID=Clng(Request("A"))
		Else
			lngAffiliateID=3
		End If
	End If


	'Retrieve GroupID if this is a banner group
	If Request("GroupID") <> "" Then
		lngGroupID=Clng(Request("GroupID"))
	Else
		If Request("G") <> "" Then
			lngGroupID=Clng(Request("G"))
		Else
			lngGroupID=0
		End If
	End If

	'Retrieve BannerID if this is an individual Banner
	If Request("BannerID") <> "" Then
		lngBannerID=Clng(Request("BannerID"))
	Else
		lngBannerID=Clng(Request("B"))
	End If
	
	'If no Banner specified, set Banner ID to ZERO
	If lngBannerID = "" Then lngBannerID = 0


	If Request("Task")="Get" Or Request("Task")="Click" OR Request("Task")="" Then
	
			'*************************************************************
			'check if affiliate exists
			'*************************************************************
			strTemp="ExistsAffiliateID_" & lngAffiliateID
			If Trim(Application(strTemp))="" Then
						Set rsTemp=connAffiliateWiz.Execute( "spafw_CheckIfAffiliateExists '"&Clng(lngAffiliateID)&"' " )
				If Not rsTemp.EOF Then
					If rsTemp("AffiliateExists")>0 Then
						Application.Lock
						Application(strTemp)=True
						Application.Unlock
					Else
						Application.Lock
						Application(strTemp)=False
						Application.Unlock					
					End If
				Else
					Application.Lock
					Application(strTemp)=False
					Application.Unlock								
				End If
				Set rsTemp=Nothing
				
			End If
			If Application(strTemp)<>True Then
				If Request.QueryString("Task")="Get" Then
					'Response.Redirect "blank.gif"
					Response.Redirect "https://www.firstaffiliates.com/default.aspx"
				End If
				'Response.End			
				Response.Redirect "https://www.firstaffiliates.com/default.aspx"		
			End If
			'*************************************************************
			'END check if affiliate exists
			'*************************************************************
			
		
			'*************************************************************
			'Check if daily routines should be performed
			'*************************************************************
			If Application("AffiliateWizDay")<>Day(Now()) Then
				'hourly routines
				Application.Lock
				Application("AffiliateWizDay")=Day(Now())
				Application.Unlock
				
				'*************************************************************
				'determine if notification to pay affiliates should be sent
				'*************************************************************
				strSQL="Select * From tblaff_Administrative2 Where Notification1=" & Day(Date()) & " Or Notification2=" & Day(Date())
				Set rsTemp=connAffiliateWiz.Execute(strSQL)
				If Not rsTemp.EOF Then

					If rsTemp("Notification1")=Day(Date()) Then
						If Day(rsTemp("Notification1SentDate"))<>Day(Date())  Or Month(rsTemp("Notification1SentDate"))<>Month(Date()) Then
							'Update Database
							If Application("afw_AffiliateWizSQL")=False Then
								strSQL="Update tblaff_Administrative2 Set Notification1SentDate=#" & month(date()) & "/" & day(date()) & "/" & year(date()) & "#"
							Else
								strSQL="Update tblaff_Administrative2 Set Notification1SentDate=CONVERT(DATETIME,'" & month(date()) & "/" & day(date()) & "/" & year(date()) & "',101)"
							End If	
							connAffiliateWiz.Execute strSQL	
							
							'send notification
							EmailPaymentReminder				
						End If
					ElseIF rsTemp("Notification2")=Day(Date()) Then
						If Day(rsTemp("Notification2SentDate"))<>Day(Date()) Or Month(rsTemp("Notification2SentDate"))<>Month(Date()) Then
						
							'Update Database
							If Application("afw_AffiliateWizSQL")=False Then
								strSQL="Update tblaff_Administrative2 Set Notification2SentDate=#" & month(date()) & "/" & day(date()) & "/" & year(date()) & "#"
							Else
								strSQL="Update tblaff_Administrative2 Set Notification2SentDate=CONVERT(DATETIME,'" & month(date()) & "/" & day(date()) & "/" & year(date()) & "',101)"
							End If	
							connAffiliateWiz.Execute strSQL					

							'send notification
							EmailPaymentReminder			
						End If								
					End If
				End If
				Set rsTemp=Nothing

				'*************************************************************
				
				'*************************************************************
				'Apply Recurring Commissions
				'*************************************************************
				ApplyRecurringCommissions
				
			End If
			'*************************************************************
			'Check if daily routines should be performed
			'*************************************************************			
	End If
	'end task is click or get

	Select Case Ucase(Request("Task"))

		'User is pulling a banner
		Case "GET"
			If lngGroupID <> 0 Then
				'Banner Group										
				strSQL="spafw_ObtainSumGroupBannersByGroupID " & lngGroupID
				Set rsSum=connAffiliateWiz.Execute(strSQL)

				'Pick Random Number between 1-Maximum Weightings
				If Not rsSum.EOF Then
					intMax=rsSum("SumOfWeighting")
				Else
					intMax=0
				End If
				Set rsSum=Nothing

				'Retrieve Total Number of Banners in group
				strSQL="spafw_RetrieveBannersByGroupID " &  lngGroupID
				Set rsBanners=connAffiliateWiz.Execute(strSQL) 	
				
				Randomize
				lngRandom=Int((intMax - 1 + 1) * Rnd + 1)		

				'Pick a banner in group
				intSum=0
				Do While Not rsBanners.EOF
					strBannerID=rsBanners("BannerID")
					lngBannerID=strBannerID
					strAffiliateWeighting=rsBanners("Weighting")
					intSum=intSum + Cint(strAffiliateWeighting)
					If lngRandom <= intSum Then
						'use this Campaign
						Exit Do
					Else
						rsBanners.MoveNext
					End If
				Loop
			
				'Return Banner
				//strImageURL=rsBanners("ImageURL")
				
				If Trim(strImageURL)<>"" Then
					strImageURL=Server.URLPathEncode(strImageURL)
				End If
				strBannerData="<a href=" &  Chr(34) &  Application("afw_DomainURL") & "?AffiliateID=" & lngAffiliateID & "&GroupID=" & lngGroupID & "&BannerID=" & lngBannerID & "&Task=Click" & Chr(34) & " target=" & Chr(34) & "_new" & Chr(34) & "><img border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & strImageURL & Chr(34) & " width=" & Chr(34) & rsBanners("Width") & Chr(34) & " height=" & Chr(34) & rsBanners("Height") & Chr(34) & " alt=" & chr(34) & rsBanners("AltText") & Chr(34) & "></a>" 

				Set rsBanners=Nothing
				
				
				If Request("Browser")="N" Then
					Response.Buffer=True
					Response.ContentType="application/x-javascript"
					strTemp=strBannerData
					strBannerData="document.write('" & strTemp & "'); "
				End If

				blnRedirect=False

				Response.Write strBannerData

			ElseIf lngBannerID <> 0 Then

				If LCase(Request.QueryString("Advanced"))<>"true" Then
				
					strTemp="AffiliateWizBanner_ImageURL_" & lngBannerID
					If Trim(Application(strTemp))="" Then
				
						'Individual Banner
						strSQL="spafw_GrabImageURLByBannerID " & lngBannerID
						Set rsBanners=connAffiliateWiz.Execute(strSQL)

						'return Banner
						If Not rsBanners.EOF Then
							strTargetImageURL=rsBanners("ImageURL")
							blnRedirect=True
						Else
							strTargetImageURL="blank.gif"
						End If
						Application.Lock
						Application(strTemp)=strTargetImageURL
						Application.Unlock
					
						Set rsBanners=Nothing
										
					End If
					strTargetImageURL=Application(strTemp)
					blnRedirect=True
				Else
					strTargetImageURL="blank.gif"
				End If
			Else

				'return blank image
				'Return Banner
				strTargetImageURL="blank.gif"
				blnRedirect=True

			End If
			
			'Count Impressions if Necessary
			'***************************************************************************
			If Application("afw_TrackBannerImpressions")=True Then
				If Application("afw_AffiliateWizSQL")=False Then
					CountImpressionAccess lngAffiliateID,lngBannerID,lngGroupID
				Else
					strSQL="sp_UpdateImpressions " & Clng(lngAffiliateID) & "," & Clng(lngBannerID)  
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
					
				End If				
			End If
			'***************************************************************************
			
			If blnRedirect=False Then
				Response.End
			Else
				Response.Redirect strTargetImageURL
			End If

		'user has clicked a banner or no task specified
		Case "CLICK", ""

			'record click
			'If lngGroupID <> 0 OR lngBannerID <> 0 Then

				Set rsBanners=connAffiliateWiz.Execute("spafw_GrabTargetURLByBannerID " & lngBannerID)		
				If Not rsBanners.EOF Then
					strTargetURL=rsBanners("TargetURL")
					BannerPerClick=rsBanners("PerClick")
				Else
					blnDeadRequest=True
				End If
				
				Set rsBanners=Nothing
				
				'If blnDeadRequest=True Then
				'	Response.End
				'End IF
	
				'Target URL specified in URL, no usage so far
				If Trim(Request("TargetURL"))<>"" Then
					strTargetURL=Request("TargetURL")
					If InStr(strTargetURL,"AWZQString") >0 Then
						strTargetURL=Replace(strTargetURL,"AWZQString","?")
					End If
					If InStr(strTargetURL,"AWZAMPSAND") >0 Then
						strTargetURL=Replace(strTargetURL,"AWZAMPSAND","&")
					End If
					If Instr(strTargetURL,"?")<=0 Then
						strTargetURL=strTargetURL & "?"
						blnFirst=True
					End If					
					For Each x In Request.Form
						If blnFirst=True Then 
							blnFirst=False
							strTargetURL=strTargetURL &  X & "=" & Server.URLEncode(Request.Form(x))
						Else
							strTargetURL=strTargetURL & "&" & X & "=" & Server.URLEncode(Request.Form(x))
						End If
					Next 
				End If			
	
			'End If
			
			'Count Click
			'***************************************************************************
			If Application("afw_AffiliateWizSQL")=False Then
				CountClickAccess lngAffiliateID,lngBannerID,lngGroupID,BannerPerClick
			Else

				strSQL="sp_CountClick " _
					& Clng(lngAffiliateID) _
					& "," & Clng(lngBannerID) _
					& "," & Clng(lngGroupID) _
					& "," & Clng(Application("afw_UniqueClickHour"))  _
					& ",'" & Left(FixBlank(Request.ServerVariables("REMOTE_ADDR")),255) & "'" _
					& ",'" & Left(FixBlank(Request.ServerVariables("HTTP_USER_AGENT")),255) & "'" _
					& ",'" & month(date()) & "/" & Day(Date()) & "/" & year(date()) & "'" _
					& "," & Clng(Application("afw_Tiers"))		
				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
			End If				
			'***************************************************************************

			'Version 2.0---------------------------------------------------------------
			'write cookie containing AffiliateID
			Response.Cookies("AffiliateWizAffiliateID")("AffiliateID")=lngAffiliateID
			'Response.Cookies("AffiliateWizAffiliateID")("AffiliateID")=11
			Response.Cookies("AffiliateWizAffiliateID").Domain = "www.firstaffiliates.com"
			Response.Cookies("AffiliateWizAffiliateID").Path = ""
			Response.Cookies("AffiliateWizAffiliateID").Expires = Date + GetCookieExpirationDate(lngAffiliateID)
			'Version 2.0---------------------------------------------------------------

			'Uncomment to pass AffiliateID to your web site.
			'If InStr(strTargetURL,"?") >0 Then
			'	strTargetURL =strTargetURL & "&AffiliateID=" & lngAffiliateID
			'Else
			'	strTargetURL =strTargetURL & "?AffiliateID=" & lngAffiliateID
			'End If
			
	
			'Response.Redirect ("../Affiliatewiz/")
			Response.Redirect ("https://www.firstaffiliates.com/default.aspx")
			
		End Select

Sub CountImpressionAccess(AffiliateID,BannerID,GroupID)

			Dim lngAffiliateID
			Dim lngBannerID
			Dim lngGroupID
			lngAffiliateID=AffiliateID
			lngBannerID=BannerID
			lngGroupID=GroupID
			
			'count impression
			strSQL="Select ID from tblaff_Impressions Where Year(tblaff_Impressions.ImpressionDay)=(" & Year(Date) & ") AND "
			strSQL=strSQL & " Month(tblaff_Impressions.ImpressionDay)=(" & Month(Date) & ") AND "
			strSQL=strSQL & " Day(tblaff_Impressions.ImpressionDay)=(" & Day(Date) & ") AND "
			strSQL=strSQL & " tblaff_Impressions.AffiliateID =" & lngAffiliateID & " And "
			strSQL=strSQL & " tblaff_Impressions.BannerID=" & lngBannerID & " And "
			strSQL=strSQL & " tblaff_Impressions.GroupID=" & lngGroupID
			Set rs=connAffiliateWiz.Execute(strSQL)

			If Not rs.EOF Then
				'update ImpressionCounts
				strSQL="Update tblaff_ImpressionCounts Set ImpressionCount=ImpressionCount+1 Where ImpressionID=" & rs("ID")
				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
			Else
				'create new record
				strSQL="Insert Into tblaff_Impressions (AffiliateID,BannerID,GroupID,ImpressionDay) "
				strSQL=strSQL & " Values (" & lngAffiliateID & "," & lngBannerID & "," & lngGroupID & ",#" & month(date()) & "/" & day(date()) & "/" & year(date()) & "#)"
				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

				strSQL="Select ID from tblaff_Impressions Where Year(ImpressionDay)=(" & Year(Date) & ") AND "
				strSQL=strSQL & " Month(ImpressionDay)=(" & Month(Date) & ") AND "
				strSQL=strSQL & " Day(ImpressionDay)=(" & Day(Date) & ") AND "
				strSQL=strSQL & " AffiliateID =" & lngAffiliateID & " And "
				strSQL=strSQL & " BannerID=" & lngBannerID & " And "
				strSQL=strSQL & " GroupID=" & lngGroupID

				Set rs=connAffiliateWiz.Execute(strSQL)

				'create record in ImpressionCounts
				strSQL="Insert Into tblaff_ImpressionCounts (ImpressionID,ImpressionCount) Values (" & rs("ID") & ",1)"
				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

				'create record in ClickCounts
				strSQL="Insert Into tblaff_ClickCounts (ClickID,ClickCount) Values (" & rs("ID") & ",0)"
				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
			End If
			
			Set rs=Nothing
			
			
End Sub

Sub CountClickAccess(lngAffiliateID,lngBannerID,lngGroupID,BannerPerClick)
			
			intUniqueClickHour=Clng(Application("afw_UniqueClickHour"))
			
			CountClick=True
			If intUniqueClickHour>0 Then
				strSQL="SELECT ID FROM tblaff_ClicksGeneric WHERE AffiliateID=" & lngAffiliateID 
				strSQL=strSQL & " AND BannerID =" & lngBannerID 
				strSQL=strSQL & " AND GroupID=" & lngGroupID 
				strSQL=strSQL & " AND DateDiff('h',ClickDateTime,Now)<" & intUniqueClickHour 
				strSQL=strSQL & " AND ClickIP='" & FixBlank(Request.ServerVariables("REMOTE_ADDR"))  & "'"
				strSQL=strSQL & " And ClickBrowser='" & FixBlank(Request.ServerVariables("HTTP_USER_AGENT")) & "'"	
				Set rsAffiliateWiz=connAffiliateWiz.Execute(strSQL)
				If rsAffiliateWiz.EOF= False Then
					CountClick=False
				End If
				Set rsAffiliateWiz=Nothing
			End If
			
			If CountClick=True Then

				'determin ID
				strSQL="Select ID from tblaff_Impressions Where Year(ImpressionDay)=(" & Year(Date) & ") AND "
				strSQL=strSQL & " Month(ImpressionDay)=(" & Month(Date) & ") AND "
				strSQL=strSQL & " Day(ImpressionDay)=(" & Day(Date) & ") AND "
				strSQL=strSQL & " AffiliateID =" & lngAffiliateID & " And "
				strSQL=strSQL & " BannerID=" & lngBannerID & " And "
				strSQL=strSQL & " GroupID=" & lngGroupID
				Set rs=connAffiliateWiz.Execute(strSQL)

				If Not rs.EOF Then
					'update ClickCounts
					strSQL="Update tblaff_ClickCounts Set ClickCount=ClickCount+1 Where ClickID=" & rs("ID")
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
				Else
					'create new record
					strSQL="Insert Into tblaff_Impressions (AffiliateID,BannerID,GroupID,ImpressionDay) "
					strSQL=strSQL & " Values (" & lngAffiliateID & "," & lngBannerID & "," & lngGroupID & ",#" & month(date()) & "/" & day(date()) & "/" & year(date()) & "#)"
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

					strSQL="Select ID from tblaff_Impressions Where Year(ImpressionDay)=(" & Year(Date) & ") AND "
					strSQL=strSQL & " Month(ImpressionDay)=(" & Month(Date) & ") AND "
					strSQL=strSQL & " Day(ImpressionDay)=(" & Day(Date) & ") AND "
					strSQL=strSQL & " AffiliateID =" & lngAffiliateID & " And "
					strSQL=strSQL & " BannerID=" & lngBannerID & " And "
					strSQL=strSQL & " GroupID=" & lngGroupID

					Set rs=connAffiliateWiz.Execute(strSQL)

					'create record in ImpressionCounts
					strSQL="Insert Into tblaff_ImpressionCounts (ImpressionID,ImpressionCount) Values (" & rs("ID") & ",1)"
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

					'create record in ClickCounts
					strSQL="Insert Into tblaff_ClickCounts (ClickID,ClickCount) Values (" & rs("ID") & ",1)"
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
				End If
				Set rs=Nothing
				
				If intUniqueClickHour>0 Then
				
					'Add new record with Click information in Clicks database
					strSQL="INSERT INTO tblaff_ClicksGeneric ("
					strSQL=strSQL & "AffiliateID,"
					strSQL=strSQL & "BannerID,"
					strSQL=strSQL & "GroupID,"
					strSQL=strSQL & "ClickIP,"
					strSQL=strSQL & "ClickBrowser) VALUES ("
					strSQL=strSQL & lngAffiliateID & "," 
					strSQL=strSQL & lngBannerID & "," 
					strSQL=strSQL & lngGroupID & ",'" 
					strSQL=strSQL & FixBlank(Request.ServerVariables("REMOTE_ADDR")) & "','" 
					strSQL=strSQL & FixBlank(Request.ServerVariables("HTTP_USER_AGENT")) & "')"		
				
					connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
				End If
				
				
				
				
				'****************************************************************
				'Add Pay Per Click Records
				'****************************************************************
				If BannerPerClick=True Then
					strSQL="spafw_GrabAffiliateTypeByAffiliateID " & lngAffiliateID
					Set rs=connAffiliateWiz.Execute(strSQL)
					If rs("PerClick")=True And Trim(rs("PerClickRate"))<>"" Then
						PerClickRate=FixZero(rs("PerClickRate"))
						'Count Tier 1 Click
						strSQL="Insert Into tblaff_Clicks (BannerID,GroupID,ClickIP,ClickBrowser) Values ("
						strSQL=strSQL & lngBannerID & "," 
						strSQL=strSQL & lngGroupID & ",'" 
						strSQL=strSQL & FixBlank(Request.ServerVariables("REMOTE_ADDR")) & "','" 
						strSQL=strSQL & FixBlank(Request.ServerVariables("HTTP_USER_AGENT")) & "')"	
			
						'set database connection
						'Set connAffiliateWiz=Server.CreateObject("ADODB.Connection") 
						'connAffiliateWiz.Mode = 3      '3 = adModeReadWrite
						connAffiliateWiz.Open Application("afw_ConnectionString")
						connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

						'Grab ClickID
						Set rsTempClick=Server.CreateObject("ADODB.Recordset")
						strSQL="Select @@Identity"
						rsTempClick.Open strSQL,connAffiliateWiz
						ClickID=Clng(rsTempClick.Fields.Item(0).Value)
						Set rsTempClick=Nothing
						
						strSQL="Insert Into tblaff_Clicks_Commissions (ClickID,AffiliateID,Commission,Tier) Values ("
						strSQL=strSQL & ClickID & "," & lngAffiliateID & "," 
						strSQL=strSQL & FixDouble(PerClickRate) & ",1)" 	
						connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
						
						Click
						
						'***************************************************************
						'Handle Tier2-Tier5
						'***************************************************************
			
				
					End If
					Set rs=Nothing					
					
					
				End If
				'End PerClick Affiliate
					
			End If
			'end CountClick=True
			
			If intUniqueClickHour>0 Then
				'Keep click table under control.  Delete old clicks
				strSQL="Delete From tblaff_ClicksGeneric Where DateDiff('h',ClickDateTime,Now)>" & Clng(intUniqueClickHour) + 1

				connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
			End If
			
			
End Sub

Function GetCookieExpirationDate(AffiliateID)

	Dim strGetCookieExpirationDate
	strGetCookieExpirationDate="afw_CookieExpiration_" & AffiliateID
	If Trim(Application(strGetCookieExpirationDate))<>"" Then
		GetCookieExpirationDate=Clng(Application(strGetCookieExpirationDate))
	Else
		'grab from database
		Dim rsCookieExpiration

		Set rsCookieExpiration=ConnAffiliateWiz.Execute ("spafw_GrabCookieExpirationForAffiliate '"&Clng(AffiliateID)&"' " )
		
		If Not rsCookieExpiration.EOF Then
			GetCookieExpirationDate=rsCookieExpiration("CookieExpiration")
			Application.Lock
			Application(strGetCookieExpirationDate)=rsCookieExpiration("CookieExpiration")
			Application.Unlock
		Else
			GetCookieExpirationDate=Clng(Application("afw_CookieExpiration"))
		End If
		Set rsCookieExpiration=nothing


	End If

End Function


Sub EmailPaymentReminder()

				'email administrator
				Recipient=Application("afw_AdministratorEmail")
				Subject="Reminder...it is time to pay your affiliates."
				Message="This is an automated reminder to let you know it is time to pay your affiliates.  To disable these notifications, edit the preferences in the Affiliate Wiz administration console."
				FromEmail=Application("afw_AdministratorEmail")
				MailServer=Application("afw_MailServer")
				FromName="Affiliate Wiz Reminder"
				
End Sub

'*************************************************************************
' Check for and apply recurring commissions
'*************************************************************************

Sub ApplyRecurringCommissions()

	
	strSQL="Select * From qry_ValidRecurringCommissions"
	Set rsApplyRecurComm=connAffiliateWiz.Execute(strSQL)
	If Not rsApplyRecurComm.EOF Then
		Do While Not rsApplyRecurComm.EOF
			'CUSTOMER_ID,COUNTRY,GRAND_TOTAL,AffiliateID,Commission,
			'IncludeTiers,Frequency,Repeat,NumberCompleted,LastDate
			
			strSQL=" Insert Into tblaff_Sales (" _
			& "CUSTOMER_ID," _
			& "ORDER_DATE," _
			& "COUNTRY," _ 
			& "GRAND_TOTAL," _ 
			& "AffiliateSaleAccepted," _
			& "IPAddress," _
			& "Browser," _
			& "Valid," _
			& "Reason) VALUES (" _
			& "'" & rsApplyRecurComm("CUSTOMER_ID") & "'," 
			If Application("afw_AffiliateWizSQL")=False Then
				strSQL= strSQL & "Now()," 
			Else
				strSQL= strSQL & "getDate()," 
			End If
			strSQL= strSQL & "'" & rsApplyRecurComm("COUNTRY")  & "'," _
			& FixDouble(rsApplyRecurComm("GRAND_TOTAL")) & "," _
			& "1," _
			& "' '," _
			& "' '," _
			&  "1," _
			& "' ')" 


			blnFailed=False
			'If commission is blank, enter it.
			If rsApplyRecurComm("Commission")<=0 Then
				Set rsawzCommission=connAffiliateWiz.Execute("Select PerSale,FlatRateOrPercentOfSale,PerSaleRate From tblaff_Affiliates Where AffiliateID=" & CLng(rsApplyRecurComm("AffiliateID")))
				If rsawzCommission("PerSale")=True Then
					If rsawzCommission("FlatRateOrPercentOfSale")=True Then
						curCommission=rsawzCommission("PerSaleRate")
					ElseIf rsawzCommission("FlatRateOrPercentOfSale")=False Then
						curCommission=rsApplyRecurComm("GRAND_TOTAL") * rsawzCommission("PerSaleRate")
					Else
						blnFailed=True
					End If
				Else
						blnFailed=True
				End If
			Else
				curCommission=rsApplyRecurComm("Commission")
			End If
			
			If Application("afw_Demo")=False And blnFailed=False Then
	
			
			'execute query
			connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords

			'Grab SaleID
			Set rsTemp=Server.CreateObject("ADODB.Recordset")
			strSQL="Select @@Identity"
			rsTemp.Open strSQL,connAffiliateWiz
			SalesID=rsTemp.Fields.Item(0).Value	
			SalesID=Clng(SalesID)
			Set rsTemp=Nothing

			'********************************************************
			
			'********************************************************
			'Insert Commission for first Tier
			strSQL="Insert into tblaff_Sales_Commissions (SalesID,AffiliateID,Commission,Tier) Values ("
			strSQL=strSQL & SalesID & ","
			strSQL=strSQL & rsApplyRecurComm("AffiliateID") & ","
			strSQL=strSQL & FixDouble(curCommission) & ",1)" 	
			connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
			'********************************************************
			
			'REPEAT FOR NUMBER OF TIERS....Application("afw_Tiers")
		
			'Tier X Stuff
			If Clng(Application("afw_Tiers"))>1 And rsApplyRecurComm("IncludeTiers")<>0 Then
			
				lngAffiliateID2=rsApplyRecurComm("AffiliateID")
				
				intCounterTemp=2
				Do While intCounterTemp<=Application("afw_Tiers")
				Tier2AffiliateID=0
				Tier2Commission=0		
				
		
				'Obtain Tier2 AffiliateID if Exists
				strSQL="Select AffiliateID From tblaff_Tier2Members Where NewMemberID=" & lngAffiliateID2
				Set rsTemp=connAffiliateWiz.Execute(strSQL)
				If rsTemp.EOF=False Then
					Tier2AffiliateID=rsTemp("AffiliateID")
					strSQL="Select PerSaleRate" & intCounterTemp & ",FlatRateOrPercentOfSale,PerSale From tblaff_Affiliates Where AffiliateID=" & Tier2AffiliateID
					Set rsTemp2=connAffiliateWiz.Execute(strSQL)

					If Not rsTemp2.EOF Then
						If rsTemp2("PerSale")<>0 Then
							strTemp="PerSaleRate" & intCounterTemp
							If rsTemp2("FlatRateOrPercentOfSale")=True  Then
								Tier2Commission=rsTemp2(strTemp)
							Else
								Tier2Commission=rsApplyRecurComm("GRAND_TOTAL") * rsTemp2(strTemp)
							End If
					

							Tier2AffiliateID=FixZero(Tier2AffiliateID)
							Tier2Commission=FixDouble(Tier2Commission)
			
							'Insert Commission for second  Tier
							strSQL="Insert into tblaff_Sales_Commissions (SalesID,AffiliateID,Commission,Tier) Values ("
							strSQL=strSQL & SalesID & ","
							strSQL=strSQL & Tier2AffiliateID & ","
							strSQL=strSQL & Tier2Commission & "," & Clng(intCounterTemp) & ")" 	
							connAffiliateWiz.Execute strSQL,,AdExecuteNoRecords
								
						End If
						'end PerSale<>0
				
					End If
					'End Not rsTemp2.EOF
					Set rsTemp2=Nothing
				
				End If
				'End not rsTemp.EOF
				intCounterTemp=intCounterTemp+1
				lngAffiliateID2=Tier2AffiliateID
				Set rsTemp=Nothing
				Loop
				
			End If
			'End Tiers >1

			End If
			'end demo=false

			'update LastDate
			strSQLTemp="Update tblaff_RecurringCommissions Set NumberCompleted=NumberCompleted+1, LastDate="
			If Application("afw_AffiliateWizSQL")=False Then
				strSQLTemp= strSQLTemp & "Now()" 
			Else
				strSQLTemp= strSQLTemp & "getDate()" 
			End If
			strSQLTemp= strSQLTemp & " Where ID=" & rsApplyRecurComm("ID")
			connAffiliateWiz.Execute strSQLTemp
			
			rsApplyRecurComm.MoveNext
		Loop
	End If
	Set rsApplyRecurComm=Nothing
	
	'delete all that are completed
	connAffiliateWiz.Execute ("Delete From tblaff_RecurringCommissions Where NumberCompleted >= Repeat" )
	
End Sub

'*************************************************************************
'*   Product:  Affiliate Wiz Version 3 
'*   Copyright (c) 2000-2002 by AdComplete.com, LLC.  All rights reserved.
'*************************************************************************
%>	
