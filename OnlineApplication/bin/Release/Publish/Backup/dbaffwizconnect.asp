<!--#Include File="db.asp"-->
<%
'*************************************************************************
'*   Product:  Affiliate Wiz Version 3 
'*   Copyright (c) 2000-2002 by AdComplete.com, LLC.  All rights reserved.
'*************************************************************************

	If (Trim(Application("afw_AdministratorName"))="" Or Trim(Application("afw_AdministratorPassword"))="" Or Trim(Application("afw_DomainURL"))="") OR (AffiliateWizConnectString<>Application("afw_ConnectionString")) Then

		afwUpdatePreferences

	End If

	Sub afwUpdatePreferences()

		'set database connection
		Set connAffWiz=Server.CreateObject("ADODB.Connection") 
		connAffWiz.Mode = 3      '3 = adModeReadWrite
		connAffWiz.Open ConnAffiliateWiz

		'grab administrative data
		strSQL="SELECT UserID, AdministratorName, AdministratorPassword, AdministratorEmail, "
		strSQL=strSQL & "MailProgram, MailServer, WebSiteURL, WebSiteName, NewMembersRequireValidation, "
		strSQL=strSQL & "AllowUsersEditing, DateFormat, ApplicationPath, DomainURL, StoreDSN, "
		strSQL=strSQL & "UniqueClickHour, CookieExpiration,AffiliateSignupPage, "
		strSQL=strSQL & "EmailAdministratorSaleNotification, HTTP_REFERER, TrackBannerImpressions,LocaleIdentifier,AffiliateTypeID "
		strSQL=strSQL & "FROM tblaff_Administrative"

		Set rs=connAffWiz.Execute(strSQL)

		Application.Lock

		'Connection String
		Application("afw_ConnectionString")=AffiliateWizConnectString

		'username
		Application("afw_AdministratorName")=rs("AdministratorName")

		'password
		Application("afw_AdministratorPassword")=rs("AdministratorPassword")

		'email Address
		Application("afw_AdministratorEmail")=rs("AdministratorEmail")

		'Email Program
		Application("afw_MailProgram")=rs("MailProgram")
	
		'MailServer
		Application("afw_MailServer")=rs("MailServer")

		'Web Site URL
		Application("afw_WebSiteURL")=rs("WebSiteURL")

		'Web Site Name
		Application("afw_WebSiteName")=rs("WebSiteName")

		'New Members Require Validation
		Application("afw_NewMembersRequireValidation")=rs("NewMembersRequireValidation")

		'Allow Users Editing
		Application("afw_AllowUsersEditing")=rs("AllowUsersEditing")
	
		'Date Format
		Application("afw_DateFormat")=rs("DateFormat")

		'domain url
		Application("afw_DomainURL")=rs("DomainURL")

		'Store DSN
		Application("afw_StoreDSN")=rs("StoreDSN")

		'Unique Click Hour
		Application("afw_UniqueClickHour")=rs("UniqueClickHour")

		'Days until cookies expire
		Application("afw_CookieExpiration")=rs("CookieExpiration")


		'Email Sales Notification To Administrator
		Application("afw_EmailAdministratorSaleNotification")=rs("EmailAdministratorSaleNotification")

		'User REFERER Method to validate sales
		Application("afw_HTTP_REFERER")=rs("HTTP_REFERER")

		'Track Banner Impressions
		Application("afw_TrackBannerImpressions")=rs("TrackBannerImpressions")


		'Signup page for referrals
		Application("afw_AffiliateSignupPage")=rs("AffiliateSignupPage")

		'LocaleIdentifier 
		Application("afw_LocaleIdentifier")=rs("LocaleIdentifier")

		'AffiliateTypeID
		Application("afw_AffiliateTypeID")=rs("AffiliateTypeID")

		'Recordset paging site
		Application("afw_PageSize")=10

		Application.Unlock

		'destroy recordset
		Set rs=Nothing

		'*************************************************************
		'Commission Rates
		'*************************************************************

		strSQL="Select PerSale, PerLead, PerClick,Tiers, "
		strSQL=strSQL & "FlatRateOrPercentOfSale, PerSaleRate, PerLeadRate, PerClickRate, "
		strSQL=strSQL & "PerSaleRate2, PerLeadRate2, PerClickRate2, "
		strSQL=strSQL & "PerSaleRate3, PerLeadRate3, PerClickRate3, "
		strSQL=strSQL & "PerSaleRate4, PerLeadRate4, PerClickRate4, "
		strSQL=strSQL & "PerSaleRate5, PerLeadRate5, PerClickRate5 "
		strSQL=strSQL & " From tblaff_CommissionRates "

		Set rs=connAffWiz.Execute(strSQL)

		Application.Lock

		'Number of Tiers
		Application("afw_Tiers")=rs("Tiers")

		'Per Sale Affiliate Types Support T/F
		Application("afw_PerSale")=rs("PerSale")

		'Per Lead Affiliate Types Support T/F
		Application("afw_PerLead")=rs("PerLead")

		'Per Click Affiliate Types Support T/F
		Application("afw_PerClick")=rs("PerClick")

		'Per Sales Are Flat Rate or Percentage of Sale (T=Flat Rate)
		Application("afw_FlatRateOrPercentOfSale")=rs("FlatRateOrPercentOfSale")

		'Per Sale Affiliate Rate
		Application("afw_PerSaleRate")=rs("PerSaleRate")
		Application("afw_PerSaleRate2")=rs("PerSaleRate2")
		Application("afw_PerSaleRate3")=rs("PerSaleRate3")
		Application("afw_PerSaleRate4")=rs("PerSaleRate4")
		Application("afw_PerSaleRate5")=rs("PerSaleRate5")

		'Per Lead Affiliate Rate
		Application("afw_PerLeadRate")=rs("PerLeadRate")
		Application("afw_PerLeadRate2")=rs("PerLeadRate2")	
		Application("afw_PerLeadRate3")=rs("PerLeadRate3")
		Application("afw_PerLeadRate4")=rs("PerLeadRate4")
		Application("afw_PerLeadRate5")=rs("PerLeadRate5")

		'Per Click Affiliate Rate
		Application("afw_PerClickRate")=rs("PerClickRate")
		Application("afw_PerClickRate2")=rs("PerClickRate2")
		Application("afw_PerClickRate3")=rs("PerClickRate3")
		Application("afw_PerClickRate4")=rs("PerClickRate4")
		Application("afw_PerClickRate5")=rs("PerClickRate5")

		Application.UnLock

		'destroy recordset
		Set rs=Nothing

		'*************************************************************
		'End Commission Rates
		'*************************************************************

		'*************************************************************
		'LARGE TEXT FIELDS, SELECT SEPARATELY
		'*************************************************************
		strSQL="SELECT NewMemberMessage,PendingMemberMessage,RejectedMemberMessage,NewSaleMessage FROM tblaff_Administrative"
		Set rs=connAffWiz.Execute(strSQL)

		'New Member Message
		Application("afw_NewMemberMessage")=rs("NewMemberMessage")
		Application("afw_PendingMemberMessage")=rs("PendingMemberMessage")
		Application("afw_RejectedMemberMessage")=rs("RejectedMemberMessage")
		Application("afw_NewSaleMessage")=rs("NewSaleMessage")
		
		'destroy recordset
		Set rs=Nothing

		%>
		<!--#Include File="adminparams.asp"-->
		<%

		'**********************************************************************************
		'VERSION #
		Application.Lock
		IF Application("afw_AffiliateWizSQL")=True Then
			Application("AffiliateWiz_ProgramVersion")="Version 3.0 for SQL"
		Else
			Application("AffiliateWiz_ProgramVersion")="Version 3.0 for Access"
		End If
		Application.Unlock			
		'**********************************************************************************

	End Sub


%>
<!--#Include File="extras.asp"-->

