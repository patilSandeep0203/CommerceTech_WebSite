<!--

###########################################################
#                                                         #
#  D O C U M E N T A T I O N                              #
#                                                         #
#  This code sample has been successfully tested on       #
#  third-party web servers and performed according to     #
#  documented Advanced Integration Method (AIM)           #
#  standards.                                             #
#                                                         #
#  Last updated September 2004.                           #
#                                                         #
#  For complete and freely available documentation,       #
#  please visit the Authorize.Net web site at:            #
#                                                         #
#  http://www.authorizenet.com/support/guides.php         #
#                                                         #
###########################################################

###########################################################
#                                                         #
#  D I S C L A I M E R                                    #
#                                                         #
#  WARNING: ANY USE BY YOU OF THE SAMPLE CODE PROVIDED    #
#  IS AT YOUR OWN RISK.                                   #
#                                                         #
#  Authorize.Net provides this code "as is" without       #
#  warranty of any kind, either express or implied,       #
#  including but not limited to the implied warranties    #
#  of merchantability and/or fitness for a particular     #
#  purpose.                                               #
#                                                         #
#                                                         #
###########################################################

###########################################################
#                                                         #
#  P R E R E Q U I S I T E S                              #
#                                                         #
#  Basically, all the required code to generate 1) a      #
#  unique fingerprint, using the HMAC-MD5 algorithm,      #
#  and 2) a compatible timestamp is included in the       #
#  sample code provided in this file and the additional   #
#  files that should have accompanied this file:          #
#  - md5.asp                                              #
#  - simdata.asp                                          #
#  - simlib.asp                                           #
#                                                         #
#  PLEASE UNDERSTAND that it is impossible for us to      #
#  anticipate any and all possible IIS server             #
#  configurations.                                        #
#                                                         #
#  If you cannot get the sample code to work due to an    #
#  unknown server configuration, and if you cannot        #
#  figure out how to test for the availability of those   #
#  modules, consider hiring a professional web developer  #
#  or IIS administrator.                                  #
#                                                         #
#  Authorize.Net is unable to assist you with IIS         #
#  troubleshooting and other issues relating to server    #
#  configuration.                                         #
#                                                         #
#                                                         #
###########################################################

###########################################################
#                                                         #
#  C O N T A C T    I N F O R M A T I O N                 #
#                                                         #
#  For specific questions,                                #
#  please contact Authorize.Net's Integration Services:   #
#                                                         #
#  integration at authorize dot net                       #
#                                                         #
#  Please remember that we cannot support individual      #
#  e-commerce developers with programming problems and    #
#  other issues that could be easily solved by referring  #
#  to the available reference materials.                  #
#                                                         #
###########################################################

###########################################################
#                                                         #
#  S I M   I N   A   N U T S H E L L                      #
#                                                         #
###########################################################
#                                                         #
#  1. You gather some basic transaction data on your web  #
#  site.                                                  #
#                                                         #
#  2. Using a standard HTML form, you submit the required #
#  information to Authorize.Net, by posting the form data #
#  to a specific URL on Authorize.Net’s secure server.    #
#                                                         #
#  3. On Authorize.Net’s secure server, you collect all   #
#  the financial information on the SIM Payment Form.     #
#                                                         #
#  4. When the transaction has been completed, you may    #
#  either re-direct the user to another web page on your  #
#  web site or complete the transaction on                #
#  Authorize.Net’s secure server.                         #
#                                                         #
#                                                         #
###########################################################

###########################################################
#                                                         #
#  H A R D   C O D E D   V A L U E S                      #
#                                                         #
#  The purpose of this sample code is to demonstrate how  #
#  a basic SIM transaction works.                         #
#                                                         #
#  For this purpose, we have hard-coded a number of       #
#  values, to expedite your testing and integration       #
#  efforts.                                               #
#                                                         #
#  Please, pay special attention to values, such as       #
#  your log-in ID, transaction key, amount, description,  #
#  etc. that may need to be changed throughout this       #
#  code sample and/or its associated include files.       #
#                                                         #
#                                                         #
###########################################################

-->

<!--#INCLUDE FILE="simlib.asp"-->
<!--#INCLUDE FILE="simdata.asp" -->

<html>
<head>
<title>Order Form</title>
</head>
<body>
<h3>Final Order</h3>

Description: CC AUTH ONLY<br>
Total Amount : 19.99<br>
<br>

<FORM action="https://test.authorize.net/gateway/transact.dll">
<!-- Uncomment the line ABOVE for test accounts OR the line BELOW for LIVE accounts-->
<!--<FORM action="https://secure.authorize.net/gateway/transact.dll">-->
<%
Dim sequence
Dim amount
Dim ret

' *** IF YOU WANT TO PASS CURRENCY CODE uncomment the next 2 lines **
' Dim currencycode
' Assign the transaction currency (from your shopping cart) to currencycode variable

' Trim $ dollar sign if it exists
' amount = Request("x_amount")

' Just a test so we use a hard-coded amount of 19.99
' NOTE: You must make absolutely sure that you are
' passing a number to the "amount" variable; otherwise,
' the fingerprint calculation will not work

' So, if you are gathering the amount value from a field
' or any other user input, make sure you collect it as or
' convert it to a number, using either VBScript, JScript or
' JavaScript.

amount = 19.99

If Mid(amount, 1,1) = "$" Then
	amount = Mid(amount,2)
End If

' Seed random number for more security and more randomness
Randomize
sequence = Int(1000 * Rnd)

'*** for testing only:   *********************************
'sequence = 931


' Now add the SIM related data, such as the fingerprint,
' to the HTML form.
' Response.Write ("<input type='text' name='x_description' value='" & Request("x_description") & "' style='width:300;'>" & vbCrLf)

Response.Write ("<input type='text' name='x_description' value='CC AUTH ONLY' style='width:300;'>" & vbCrLf)
Response.Write("<br>")

' Again, make sure all required values are properly declared
' in their respective places

ret = InsertFP (loginid, txnkey, amount, sequence)


' *** IF YOU ARE PASSING CURRENCY CODE uncomment and use the following instead of the InsertFP function above  ***
' ret = InsertFP (loginid, txnkey, amount, sequence, currencycode)

Response.Write ("<input type='text' name='x_login' value='" & loginid & "' style='width:300;'>" & vbCrLf)
Response.Write("<br>")
Response.Write ("<input type='text' name='x_amount' value='" & amount & "' style='width:300;'>" & vbCrLf)
Response.Write("<br>")

' *** IF YOU ARE PASSING CURRENCY CODE uncomment the line below *****
' Response.Write ("<input type=""text"" name=""x_currency_code"" value=""" & currencycode & """>" & vbCrLf)

%>

<INPUT type="text" name="x_show_form" value="PAYMENT_FORM" style="width:300;"><br>
<INPUT type="text" name="x_color_background" value="#eeeeee" style="width:300;"><br>
<br>


<INPUT type="submit" value="Authorize.Net  ::  Accept Order">
</form>
<br>
<br>
<br>
</body></html>