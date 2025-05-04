<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>New Page 1</title>
</head>

<body>

           
  
<% 
dim  fs, response
dim invoicenum, oldpath, newfilepath


response = Request.Form("x_response") 
invoicenum = Request.Form ("x_invoice_num")

//if the response is valid (transaction was accepted)
//if (response = 1) then
	//move the question file to the 'Paid' folder
	set fs=Server.CreateObject("Scripting.FileSystemObject")
	filename = invoicenum & ".txt"
   oldpath =  Server.MapPath("tempquestions/" & filename)

	newfilepath = Server.MapPath("questions/" & filename)
	fs.MoveFile oldpath, newfilepath
//end if

set fs=nothing

%>

     

  
   
</body>

</body>

</html>
















