<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<!--#INCLUDE FILE="simlib.asp"-->

<title>Ask Grandpa Mac Personal Internet Advice Service -  Ask Your Question English</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link href="0505_layout.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.style4 {
	font-size: 4em;
	font-family: "Times New Roman", Times, serif;
	height: 108px;
	padding: 0px;

	margin: 0px;
}
.style5 {font-size: x-small}
.style7 {color: #FF0000}
-->
</style>

<script language="javascript">
function TellFriend(ref)
{ 
var str="toolbar=no,status=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=420,width=500"
	tellaFriend = window.open(ref,"TellObj",str);
    tellaFriend.opener = top;
} 
//function validates fields and returns false if error is encountered
function validate()
{

	var ret = true;
	str = "";

	if ( document.frmCheckout.x_first_name.value == "" )
	{
		str += "Please specify First Name.\n";
		ret=false;
	}

	if ( document.frmCheckout.x_last_name.value == "" )
	{
		str += "Please specify Last Name.\n";
		ret=false;
	}	
	
	if ( document.frmCheckout.x_company.value == "" )
	{
		str += "Please specify Company.\n";
		ret=false;
	}
	
	if ( document.frmCheckout.x_address.value == "" )
	{
		str += "Please specify Address.\n";
		ret=false;
	}
	if ( document.frmCheckout.x_city.value == "" )
	{
		str += "Please specify City.\n";
		ret=false;
	}
	if ( document.frmCheckout.x_zip.value == "" )
	{
		str += "Please specify Zip Code.\n";
		ret=false;
	}
	if ( document.frmCheckout.x_state.value == "" )
	{
		str += "Please select State.\n";
		ret=false;
	}
	if ( document.frmCheckout.x_country.value == "" )
	{
		str += "Please select Country.\n";	
		ret=false;
	}
		if ( document.frmCheckout.x_email.value == "" )
	{
		str += "Please select E-mail.\n";	
		ret=false;
	}


	//if Check Box for Book Purchase is enabled
	if ( document.frmCheckout.chkBoxBook.checked)
	{
		//check all the Ship to Fields
		if ( document.frmCheckout.x_ship_to_first_name.value == "" )
		{
			str += "Please specify Shipping First Name.\n";
			ret=false;
		}

		if ( document.frmCheckout.x_ship_to_last_name.value == "" )
		{
			str += "Please specify Shipping Last Name.\n";
			ret=false;
		}	
	
		if ( document.frmCheckout.x_ship_to_address.value == "" )
		{
			str += "Please specify Shipping Address.\n";
			ret=false;
		}
		if ( document.frmCheckout.x_ship_to_city.value == "" )
		{
			str += "Please specify Shipping City.\n";
			ret=false;
		}
		if ( document.frmCheckout.x_ship_to_zip.value == "" )
		{
			str += "Please specify Shipping Zip Code.\n";
			ret=false;
		}
		if ( document.frmCheckout.x_ship_to_state.value == "" )
		{
			str += "Please select Shipping State.\n";
			ret=false;
		}
		if ( document.frmCheckout.x_ship_to_country.value == "" )
		{
			str += "Please select Shipping Country.\n";
			ret=false;
		}
		

	} //end if chkBoxBook enabled
		

	if (str.length > 0)
	{
		alert(str);
	}
	return ret;
}

//function enables or disables the Shipping and Book Purchase
function checkLayer()
{
	if ( document.frmCheckout.chkBoxBook.checked )
	{
		document.frmCheckout.x_ship_to_first_name.disabled = false;
		document.frmCheckout.x_ship_to_last_name.disabled = false;	
		document.frmCheckout.x_ship_to_address.disabled = false;
		document.frmCheckout.x_ship_to_city.disabled = false;
		document.frmCheckout.x_ship_to_company.disabled = false;
		document.frmCheckout.x_ship_to_state.disabled = false;
		document.frmCheckout.x_ship_to_zip.disabled = false;
		document.frmCheckout.x_ship_to_country.disabled = false;
		document.frmCheckout.chkBoxGrand.disabled = false;
		document.frmCheckout.chkBoxParent.disabled = false;
		document.frmCheckout.chkBoxFamily.disabled  = false;

		document.frmCheckout.chkBoxShipSame.disabled = false;
				
	}
	else
	{
		//disable all Book and Shipping information
		document.frmCheckout.x_ship_to_first_name.disabled = true;
		document.frmCheckout.x_ship_to_last_name.disabled = true;
		document.frmCheckout.x_ship_to_address.disabled = true;
		document.frmCheckout.x_ship_to_city.disabled = true;
		document.frmCheckout.x_ship_to_company.disabled = true;
		document.frmCheckout.x_ship_to_state.disabled = true;
		document.frmCheckout.x_ship_to_zip.disabled = true;
		document.frmCheckout.x_ship_to_country.disabled = true;
		document.frmCheckout.chkBoxGrand.disabled = true;
		document.frmCheckout.chkBoxParent.disabled =true;
		document.frmCheckout.chkBoxFamily.disabled  = true;
		document.frmCheckout.lstQtyGrandParent.disabled  = true;
		document.frmCheckout.lstQtyParent.disabled  = true;
		document.frmCheckout.lstQtyFamily.disabled  = true;
		document.frmCheckout.chkBoxShipSame.disabled = true;
		
		//reset Quantity for all books
		document.frmCheckout.lstQtyParent.value = "Qty";
		document.frmCheckout.lstQtyGrandParent.value = "Qty";
	   document.frmCheckout.lstQtyFamily.value = "Qty";
		
		//uncheck all books
		document.frmCheckout.chkBoxGrand.checked = false;
		document.frmCheckout.chkBoxParent.checked = false;
	   document.frmCheckout.chkBoxFamily.checked = false;
	   
		document.frmCheckout.x_ship_to_first_name.value="";
		document.frmCheckout.x_ship_to_last_name.value="";
		document.frmCheckout.x_ship_to_address.value="";
		document.frmCheckout.x_ship_to_city.value="";
		document.frmCheckout.x_ship_to_zip.value="";
		document.frmCheckout.x_ship_to_country.value="United States";
	}
}

function fillShipping()
{
	if ( document.frmCheckout.chkBoxShipSame.checked )
	{
		//populate shipping address with billing address
		document.frmCheckout.x_ship_to_first_name.value= document.frmCheckout.x_first_name.value;
		document.frmCheckout.x_ship_to_last_name.value=document.frmCheckout.x_last_name.value;
		document.frmCheckout.x_ship_to_address.value=document.frmCheckout.x_address.value;
		document.frmCheckout.x_ship_to_company.value=document.frmCheckout.x_company.value;
		document.frmCheckout.x_ship_to_city.value=document.frmCheckout.x_city.value;
		document.frmCheckout.x_ship_to_zip.value=document.frmCheckout.x_zip.value;
		document.frmCheckout.x_ship_to_country.value="United States";
	}
}
 
//function enables the purchase information for Grandparent book
function checkBookGrand()
{
	if ( document.frmCheckout.chkBoxGrand.checked )
	{
		document.frmCheckout.lstQtyGrandParent.disabled  = false;
	}
	else
	{
		document.frmCheckout.lstQtyGrandParent.disabled  = true;
		document.frmCheckout.lstQtyGrandParent.value = "Qty";
	}
}

//function enables the purchase information for Grandparent book
function checkBookParent()
{
	if ( document.frmCheckout.chkBoxParent.checked )
	{
		document.frmCheckout.lstQtyParent.disabled  = false;
	}
	else
	{
		document.frmCheckout.lstQtyParent.disabled  = true;
		document.frmCheckout.lstQtyParent.value = "Qty";
	}
}

//function enables the purchase information for Grandparent book
function checkBookFamily()
{
	if ( document.frmCheckout.chkBoxFamily.checked )
	{
		document.frmCheckout.lstQtyFamily.disabled  = false;
	}
	else
	{
		document.frmCheckout.lstQtyFamily.disabled  = true;
		document.frmCheckout.lstQtyFamily.value = "Qty";
	}
}
</script>

</head>
<BODY onLoad="checkLayer()">
<center>
<table border="1" style="border-collapse: collapse; border-left: thin solid #303; border-right: thin solid #303; border-top: thin solid #303; border-bottom: thin none #330033" cellpadding="0" cellspacing="0" height="1225"><tr valign="top"><td height="19">
<tr valign="top" bgcolor="#FFFFff" style-"height: 101px;">
<td colspan="2" align="center" height="168">
  <table width="100%" border="0" cellpadding="10" style="height: 101px;">
    <tr>
      <td><img src="images/Shim.gif" width="85" height="10" /></td>
      <td width="100%" valign="middle" align="center">
        <h2 class="style4"><img src="images/Logo02.gif" alt="Ask Grandpa Mac" width="470" height="89" /></h2>      </td>
    </tr>
  </table>

  <table width="790" height="34" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <th width="4" scope="col">&nbsp;</th>
      <th scope="col"><a href="askgrandpamac_index.asp">Home&nbsp;</a></th>
      <th scope="col"><a href="aboutUs.asp">About This  Page</a></th>
      <th scope="col"><a href="adviceOffered.asp">Advice Offered</a></th>
      <th scope="col"><a href="askyourquestionnew.asp">Ask Your Question</a></th>
      <th nowrap="nowrap" scope="col"><a href="grandparentingBook2.asp">Books By Grandpa Mac</a></th>
      <th scope="col"><a href="S_Disclaimer.htm">En Espa&ntilde;ol</a></th>
    </tr>
  </table>

</td></tr>
<tr bgcolor="#FFFFFF" valign="top">
<td style="width: 110px" height="921"> 
    <table width="110" style="border-right: 2px solid #999;padding-left: 4px; padding-right: 4px;padding-top: 2px;margin-top: 0px;" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <th height="90" align="center" valign="bottom" scope="col"><a href="whoIam.asp"><br />
          <img src="images/ReSized/Buttons/MickNDogs.jpg" alt="Advice On Growing Up With Your Children" width="90" height="90" border="1" /></a>        </th>
      </tr>
      <tr style="height: 13px;padding-top: 0px;padding-bottom: 0px;">
        <td  style="height: 13px;padding-top: 0px;padding-bottom: 0px;" align="center" valign="top"><a href="whoIam.asp">Who I Am...</a></td>
      </tr>
      <tr style="padding-top: 0px;">
        <td style="padding-top: 0px;" height="90" align="center" valign="bottom"><br />
          <strong><a href="myHero1903.asp"><img src="images/ReSized/Buttons/NewCarl.jpg" alt="Advice On Becoming Your Child&rsquo;s Hero" width="90" height="90" border="1" /></a></strong>        </td>
      </tr>
      <tr style="height: 13px;padding-top: 0px;padding-bottom: 0px;">
        <td  style="height: 13px;padding-top: 0px;padding-bottom: 0px;" align="center" valign="top"><a href="myHero1903.asp">My Hero... </a></td>
      </tr>
      <tr style="padding-top: 0px;">
        <td  style="padding-top: 0px;" height="90" align="center" valign="bottom"><strong><a href="me_myHero.asp"><br />
          <img src="images/ReSized/Buttons/GreatGF.jpg" alt="Advice On Living With Death" width="90" height="90" border="1" /></a></strong>        </td>
      </tr>
      <tr style="height: 13px;padding-top: 0px;padding-bottom: 0px;">
        <td  style="height: 13px;padding-top: 0px;padding-bottom: 0px;" align="center" valign="top"><a href="me_myHero.asp">Me &amp; My
            
        Hero...</a> </td>
      </tr>
      <tr style="padding-top: 0px;">
        <td  style="padding-top: 0px;" height="90" align="center" valign="bottom"><br />
          <strong><a href="growingUp.asp"><img src="images/ReSized/Buttons/NewDad.jpg" alt="Advice On Illness And Aging" width="90" height="90" border="1" /></a></strong>        </td>
      </tr>
      <tr style="height: 13px;padding-top: 0px;padding-bottom: 0px;">
        <td  style="height: 13px;padding-top: 0px;padding-bottom: 0px;" align="center" valign="top"><a href="growingUp.asp">Growing Up...</a> </td>
      </tr>
      <tr style="padding-top: 0px;">
        <td style="padding-top: 0px;" height="90" align="center" valign="bottom"><br />
          <strong><a href="strongWoman.asp"><img src="images/ReSized/Buttons/NewLadies.jpg" alt="Advice On Romancing The One You Love" width="90" height="90" border="1" /></a></strong>        </td>
      </tr>
      <tr>
        <td height="15" align="center" valign="top" nowrap="nowrap"><a href="strongWoman.asp">Strong 
        Women.</a>..</td>
      </tr>
      <tr>
        <td height="90" align="center" valign="bottom"><br />
          <a href="myTurn.asp"><img src="images/ReSized/Buttons/NewMinta.jpg" alt="Advice On Talking To Daughters" width="90" height="90" border="1" /></a>        </td>
      </tr>
      <tr>
        <td height="15" align="center" valign="top"><a href="myTurn.asp">My Turn:<br />
        Minta &amp; Me...</a></td>
      </tr>
      <tr>
        <td height="15" align="center"><br />
          <a href="myTurnEmmett02.asp"><img src="images/ReSized/Buttons/NewEm.jpg" alt="Advice On Talking To Sons" width="90" height="90" border="1" /></a>        </td>
      </tr>
      <tr>
        <td height="15" align="center" valign="top"><a href="myTurnEmmett02.asp">My Turn:<br />
Emmett &amp; Me...</a></td>
      </tr>
      <tr>
        <td height="15" align="center"><br />
          <a href="myTurn_GC02.asp"><img src="images/ReSized/Buttons/NewCal.jpg" alt="Advice On How To Be A Good Parent" width="90" height="90" border="1" /></a>        </td>
      </tr>
      <tr>
        <td height="15" align="center" valign="top"><a href="myTurn_GC02.asp">My Turn:<br />
Caleb &amp; Me...</a></td>
      </tr>
      <tr>
        <td height="15" align="center"><br />
          <a href="MyTurnMimi.htm"><img src="images/ReSized/Buttons/NewMimi.jpg" alt="Advice On How To Be A Good Grandparent" width="90" height="90" border="1" /></a>        </td>
      </tr>
      <tr>
        <td height="15" align="center" valign="top"><a href="MyTurnMimi.htm">My Turn:<br />
Mimi &amp; Me...</a></td>
      </tr>
    </table>  </td>
   <td>
    <form name="frmCheckout" id="form1" method="post" action="https://secure.authorize.net/gateway/transact.dll" onsubmit="return validate()">

     <p align="center" class="ExtraBold style7"><br>
     Provide your Billing Information </p>

      <center>
     <table border="1">
      <tr>
        <td width="88">First Name</td>
        <td width="234" align="left"><input type="text" name="x_first_name" size="16"></td>
      </tr>
      <tr>
        <td width="88">Last Name</td>
        <td width="234" align="left"><input type="text" name="x_last_name" size="16"></td>
      </tr>
      <tr>
        <td width="88">Company</td>
        <td width="234" align="left"><input type="text" name="x_company" size="32"></td>
      </tr>
      <tr>
        <td width="88">Address</td>
        <td width="234" align="left"><input type="text" name="x_address" size="32"></td>
      </tr>
      <tr>
        <td width="88">City</td>
        <td width="234" align="left"><input type="text" name="x_city" size="32"></td>
      </tr>
      <tr>
        <td>State/Province</td>
        <td width="234" align="left"><select size="1" name="x_state">
            <option value="AL">Alabama</option>
            <option selected>State</option>
          </select></td>
      </tr>
      <tr>
        <td width="88">Zip Code</td>
        <td width="234" align="left"><input type="text" name="x_zip" size="16"></td>
      </tr>
      <tr>
        <td width="88">Country</td>
        <td width="234" align="left"><select size="1" name="x_country">
            <option selected value="United States">United States</option>
          </select></td>
      </tr>
      <tr>
        <td width="88">E-Mail</td>
        <td width="234" align="left"><input type="text" name="x_email" size="16"></td>
      </tr>
      <tr>
        <td width="88">Phone</td>
        <td width="234" align="left"><input type="text" name="x_phone" size="16"></td>
      </tr>
     </table>

	  
     <br>

	  
     <input type="checkbox" name="chkBoxBook" value="ON" onClick="checkLayer()"> 
     I would
     like to purchase a Book By Grandpa Mac
     
     <table border="1">
      <tr>
        <td>&nbsp;</td>
        <td>Quantity</td>
        <td>Title</td>
        <td>Price</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chkBoxGrand" value="ON" onClick="checkBookGrand()"></td>
        <td><select size="1" name="lstQtyGrandParent">
            <option>Qty</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
            <option value="10">10</option>
          </select></td>
        <td><i>Secrets On How To Become A Better Grandparent</i></td>
        <td>$14.99</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chkBoxParent" value="ON"  onClick="checkBookParent()"></td>
        <td><select size="1" name="lstQtyParent">
            <option>Qty</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
            <option value="10">10</option>
          </select></td>
        <td><i>Secrets On How To Become A Better Parent</i></td>
        <td>$14.99</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chkBoxFamily" value="ON"  onClick="checkBookFamily()"></td>
        <td><select size="1" name="lstQtyFamily">
            <option>Qty</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
            <option value="10">10</option>
          </select></td>
        <td><i>Secrets On How To Raise A Family</i></td>
        <td>$14.99</td>
      </tr>
    </table>
    
     <br>
     <font color="#FF0000">
     Provide your Shipping Information<br>
     </font>
    
    <br>
     <input type="checkbox" name="chkBoxShipSame" value="ON" onClick="fillShipping()"><font size="2">Populate with Billing
     Information</font>
     <table border="1">
     </center>
      <tr>
        <td width="88">First Name</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_first_name" size="16"></td>
      </tr>
      <tr>
        <td width="88">Last Name</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_last_name" size="16"></td>
      </tr>
      <tr>
        <td width="88">Company</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_company" size="32"></td>
      </tr>
      <tr>
        <td width="88">Address</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_address" size="32"></td>
      </tr>
      <tr>
        <td width="88">City</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_city" size="32"></td>
      </tr>
      <tr>
        <td>State/Province</td>
        <td width="234" align="left"><select size="1" name="x_ship_to_state">
            <option value="AL">Alabama</option>
            <option selected>State</option>
          </select></td>
      </tr>
      <tr>
        <td width="88">Zip Code</td>
        <td width="234" align="left"><input type="text" name="x_ship_to_zip" size="16"></td>
      </tr>
      <tr>
        <td width="88">Country</td>
        <td width="234" align="left"><select size="1" name="x_ship_to_country">
            <option selected value="United States">United States</option>
          </select></td>
      </tr>
     </table>
    <p align="center">

 
     	<INPUT TYPE=SUBMIT VALUE="Submit" name="btSubmit" >
    &nbsp;<br>
    <font size="2">'Submit' will direct you to a Secure Payment Page to enter
    your credit card information<input type=hidden name="x_price" value="<%=9.99%>">  
                  
        <% ret = InsertFP ("r36bk2ly", "3WWMu6MsT9b92j44", "9.99", Int(1000 * Rnd))  %>
    </font>&nbsp; 
       <INPUT TYPE=HIDDEN NAME="x_login" VALUE="r36bk2ly">
        <INPUT TYPE=HIDDEN NAME="x_description" VALUE="">

        <INPUT TYPE=HIDDEN NAME="x_show_form" VALUE="PAYMENT_FORM">

        <INPUT TYPE=HIDDEN NAME="x_amount" VALUE="9.99">
        <INPUT TYPE=HIDDEN NAME="x_receipt_link_text" value="Click here to return to the Ask Grandpa Mac site.">
        <INPUT TYPE=HIDDEN NAME="x_receipt_link_url" value="http://www.askgrandpamac.com/thankyouzip01/thankyoueng.asp?a=1">
        

      </center>   

    </form>
    
<table>
    
  <tr bgcolor="#FFFFFF"><td height="109">&nbsp;</td>
  <td align="center" height="109">
                 <% //ret = InsertFP ("CTC5060", "2fuh0xtZUrgC1KWW", "9.99", int(rnd*998)+1)  %> 
<span class="style5"><br /><p align="center">
  <a href="javascript:TellFriend('http://www.askgrandpamac.com/tell_a_friend_script.asp?page=<%
Response.write server.URLEncode("http://www.askgrandpamac.com")
%>')"><B><font color="blue" size="2">Tell A Friend About AskGrandpaMac.com<br />
  </font></B><span class="style5"><br />
    The Information Provided On This Website Has Been Copyrighted And Is Protected.</span><br />
  <span class="style5">Copyright &copy; MMV AskGrandpaMac.com/Edward J. McGrath</span> 



    </td>
    </tr>
</body>




