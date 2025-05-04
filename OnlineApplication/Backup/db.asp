<%
'*************************************************************************
'*   Product:  Affiliate Wiz Version 3 
'*   Copyright (c) 2000-2002 by AdComplete.com, LLC.  All rights reserved.
'*************************************************************************

	'ACCESS USERS**********ACCESS USERS**********ACCESS USERS**********ACCESS USERS
	'******************************************************************************
	'Modify this section if using FrontPage 2000
		AffiliateWizConnectString=Application("AffiliateWiz_ConnectionString")
	'******************************************************************************
	'Modify This Section if using the Physical Server Path to Connect to Database
		'strYourDatabasePath="C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\affiliatewiz.mdb"
		'AffiliateWizConnectString="driver={Microsoft Access Driver (*.mdb)};dbq=" & strYourDatabasePath
	'******************************************************************************
	'Modify his section if using an OLEDB connection
		'AffiliateWizConnectString="Provider=Microsoft.Jet.OLEDB.4.0;" & _
		'	"Data Source=C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\affiliatewiz.mdb;" & _
		'	"Jet OLEDB:Database"
	'******************************************************************************

	'SQL USERS**********SQL USERS**********SQL USERS**********SQL USERS
	'******************************************************************************
		'Modify this section for connecting directly to the database using OLE DB
		set connAffiliateWiz = CreateObject("ADODB.Connection")
		ConnAffiliateWiz.OPEN ="PROVIDER=SQLOLEDB.1;Driver=sqlsrv32.dll;Persist Security Info=True;Data Source=SERVER-R320;UID=sqladmin;PWD=lacmrce567;DATABASE=AffiliateWiz"

		set connToken = CreateObject("ADODB.Connection")
		connToken.OPEN ="PROVIDER=SQLOLEDB;Driver=sqlsrv32.dll;Data Source=SERVER-R320;UID=partner;PWD=comm753sql;DATABASE=Token"

	'******************************************************************************
%>