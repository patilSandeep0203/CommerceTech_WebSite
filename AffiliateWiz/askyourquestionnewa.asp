<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>New Page 1</title>
</head>

<body>
  <!--#INCLUDE FILE="simlib.asp"-->
            
<FORM METHOD=POST name="anet" ACTION="https://secure.authorize.net/gateway/transact.dll" >
  
<% 
dim filepathj, fs, f
dim first_name, last_name, email, invoice_num

first_name = Request.Form("txtFirstName") 
last_name =  Request.Form("txtLastName")
email =  Request.Form("txtEMail")

set fs=Server.CreateObject("Scripting.FileSystemObject")
Randomize timer

//loop through creating the filename while the filename already exists (to prevent overwriting previous file)
do
	invoice_num = Int(1000000 * Rnd)
	filename = invoice_num & ".txt"
	filepath = Server.MapPath("tempquestions/" & filename)
loop while (fs.FileExists (filepath) )
	
set f=fs.CreateTextFile(filepath,true)

question = "Name: " & first_name & " " & last_name   & VBCR  & "E-Mail: "  & email & VBCR & VBCR& Request.Form ("txtQuestion")
f.write(question)
f.close
set f=nothing
set fs=nothing

ret = InsertFP ("r36bk2ly", "3WWMu6MsT9b92j44", "9.99", Int(1000 * Rnd))  %>

       <INPUT TYPE=HIDDEN NAME="x_login" VALUE="r36bk2ly">
        <INPUT TYPE=HIDDEN NAME="x_description" VALUE="http://www.firstaffiliates.com/AffiliateWiz/questions/<%=filename%>">        <INPUT TYPE=HIDDEN NAME="x_show_form" VALUE="PAYMENT_FORM">

        <INPUT TYPE=HIDDEN NAME="x_amount" VALUE="9.99">
       <INPUT TYPE=HIDDEN NAME="x_first_name" VALUE="<%=first_name%>" >
       <INPUT TYPE=HIDDEN NAME="x_last_name" VALUE="<%=last_name%>">
       <INPUT TYPE=HIDDEN NAME="x_email" VALUE="<%=email%>">
        <INPUT TYPE=HIDDEN NAME="x_invoice_num" VALUE="<%=invoice_num%>">
        <INPUT TYPE=HIDDEN NAME="x_relay_response" VALUE="True">
        <INPUT TYPE=HIDDEN NAME="x_show_form" VALUE="PAYMENT_FORM">
             <INPUT TYPE=HIDDEN NAME="x_receipt_link_text" value="Click here to return.">
   

 </FORM>
     

  
   
</body>

<script type="text/javascript" language="JavaScript">
//submit form
document.anet.submit();
</script>
</body>

</html>










