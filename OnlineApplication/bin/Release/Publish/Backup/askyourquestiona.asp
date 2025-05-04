<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>New Page 1</title>
</head>

<body>
  <!--#INCLUDE FILE="simlib.asp"-->
            
<FORM METHOD=POST name="anet" ACTION="https://secure.authorize.net/gateway/transact.dll"  >
  
<% 
dim filepath, fs, f
dim first_name, last_name, email, invoicenum

first_name = Request.Form("txtFirstName") 
last_name =  Request.Form("txtLastName")
email =  Request.Form("txtEMail")

set fs=Server.CreateObject("Scripting.FileSystemObject")
Randomize timer

//loop through creating the filename while the filename already exists (to prevent overwriting previous file)
do
	invoicenum = Int(1000000 * Rnd)
	filename = invoicenum & ".txt"
	filepath = Server.MapPath("tempquestions/" & filename)
loop while (fs.FileExists (filepath) )
	
set f=fs.CreateTextFile(filepath,true)

//write the question to a text file in the question folder
question = "Name: " & first_name & " " & last_name   & VBCR  & "E-Mail: "  & email & VBCR & VBCR& Request.Form ("txtQuestion")
f.write(question)
f.close
set f=nothing
set fs=nothing

ret = InsertFP ("CTC5060", "2fuh0xtZUrgC1KWW", "9.99", int(rnd*998)+1)  %>

       <INPUT TYPE=HIDDEN NAME="x_login" VALUE="CTC5060">
        <INPUT TYPE=HIDDEN NAME="x_description" VALUE="http://firstaffiliates.com/affiliatewiz/tempquestions/<%=filename%>">       
         <INPUT TYPE=HIDDEN NAME="x_show_form" VALUE="PAYMENT_FORM">

        <INPUT TYPE=HIDDEN NAME="x_amount" VALUE="9.99">
        <INPUT TYPE=HIDDEN NAME="x_receipt_link_text" value="Click here to return to the Ask Grandpa Mac site.">
        <INPUT TYPE=HIDDEN NAME="x_receipt_link_url" value="http://www.askgrandpamac.com/thankyouzip01/thankyoueng.asp?a=1">
       <INPUT TYPE=HIDDEN NAME="x_first_name" VALUE="<%=first_name%>" >
       <INPUT TYPE=HIDDEN NAME="x_last_name" VALUE="<%=last_name%>">
       <INPUT TYPE=HIDDEN NAME="x_email" VALUE="<%=email%>">
       <INPUT TYPE=HIDDEN NAME="x_relay_response" VALUE="True">


        <INPUT TYPE=HIDDEN NAME="x_invoice_num" VALUE="<%=invoicenum%>">
        <INPUT TYPE=HIDDEN NAME="x_show_form" VALUE="PAYMENT_FORM">

 </FORM>
     

  
   
</body>

<script type="text/javascript" language="JavaScript">
//submit form
document.anet.submit();
</script>
</body>

</html>











