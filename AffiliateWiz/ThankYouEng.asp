<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Ask Grandpa Mac  Personal Internet Advice Service - Thanks For Writing English</title>
<style type="text/css">
body {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #000000;
	text-decoration: none;
	text-align: center;
	background-color: #ffffff;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	cursor: crosshair;
}
<!--
.style1 {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.style2 {color: #FF0000}
a:link {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	color: #FF0000;
	text-decoration: none;
}
.style3 {font-size: x-small}
-->
</style>

<script language="javascript">
function TellFriend(ref)
{ 
var str="toolbar=no,status=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=420,width=500"
	tellaFriend = window.open(ref,"TellObj",str);
    tellaFriend.opener = top;
} 
 
</script>

</head>

<body>
<h1 align="justify">&nbsp;</h1>
<table width="650" border="0" align="center" cellpadding="10" cellspacing="0">
  <tr>
    <td bgcolor="#FFFFFF"><h2 align="center">Thanks For Writing&hellip;</h2>
      <p align="justify"> I have just received your question as I am  at my computer replying to others before yours.&nbsp;  Please know that I will respond back to you and answer your question just  as quickly as possible.</p>
      <p align="justify">With this E-Mail, you can be  assured that your order has been received and is being processed.&nbsp; You may also wish to print this as a receipt  for your records.</p>
      <% if request("a") <> "1" then %>
      <p align="justify">Your transaction ID Number is:</p>
      <p align="justify">&nbsp; <% = request("txn_id") %>.</p>
      <% end if %>
      <p align="justify">Should you be interested in knowing  more about me and what as Grandpa Mac I hold most dear, I invite you to click  on to the below link and view a short film I wrote and produced with my wife  Dyanne titled <strong>A Day In</strong> <strong>The Life Of My Uncle Jay</strong>.&nbsp; </p>
      <p align="justify">Through this film, my Hawaiian  Uncle shares many of the secrets that he passed on to me on how to lead a happy  life in a very confused world.&nbsp; If you  have the time, I believe that you will enjoy it.&nbsp; </p>
      <p align="left">        Happy Trails.
      Grandpa Mac</p>
      <p align="left"><strong><span class="style2"><a href="http://myhawaiianuncle.com" target="_blank">Click  Here</a> </span></strong>To View Film.</p>      
      <p align="justify" class="style1">&nbsp;</p>    </td>
    <td><div align="center"><img src="images/EdnDi.jpg" alt="Advice On Dating " width="284" height="415" border="1" /><br />
        <br />
    </div></td>
  </tr>
</table>
<h1 align="justify">&nbsp;</h1>
<p align="center">
  <a href="javascript:TellFriend('http://www.askgrandpamac.com/tell_a_friend_script.asp?page=
<%
dim invoicenum, oldpath, newfilepath, response

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
%>')"><B><font color="blue" size="2">Tell A Friend About AskGrandpaMac.com<br />
  </font></B></a><span class="style5"><br />
    <span class="style3">The Information Provided On This Website Has Been Copyrighted And Is Protected.</span></span><span class="style3"><br />
    Copyright &copy; MMV AskGrandpaMac.com/Edward J. McGrath</span> 
</body>
</html>
