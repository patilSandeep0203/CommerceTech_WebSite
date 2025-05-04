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
function validate()
{

	var ret = true;
	str = "";

	if ( document.frmQuest.txtFirstName.value == "" )
	{
		str += "Please specify First Name.\n";
		ret=false;
	}

	if ( document.frmQuest.txtLastName.value == "" )
	{
		str += "Please specify Last Name.\n";
		ret=false;
	}
	
	if ( document.frmQuest.txtEmail.value == "" )
	{
		str += "Please specify E-mail.\n";	
		ret=false;
	}
	
	if ( document.frmQuest.txtQuestion.value == "" )
	{
		str += "Please type in your question\n";	
		ret=false;
	}
	
	if ( document.frmQuest.chkBoxTerms.checked == false )
	{
		str += "Please check that you're read the Terms and Conditions\n";	
		ret=false;
	}
	
	if (str.length > 0)
	{
		alert(str);
	}
	return ret;
}
</script>

</head>
<body>
<center>
<table border="1" style="border-collapse: collapse;	border-top: thin solid #303;border-right: thin solid #303;border-left: thin solid #303;
	border-bottom-width: thin;
	border-bottom-style: none;
	border-bottom-color: #330033;" cellpadding="0" cellspacing="0"><tr valign="top"><td>
<table border="0" width="800" cellpadding="0" cellspacing="0">
<tr valign="top" bgcolor="#FFFFff" style-"height: 101px;">
<td colspan="2" align="center">
  <table width="100%" border="0" cellpadding="10" style="height: 101px;">
    <tr>
      <td><img src="images/Shim.gif" width="85" height="10" /></td>
      <td width="100%" valign="middle" align="center">
        <h2 class="style4"><img src="images/Logo02.gif" alt="Ask Grandpa Mac" width="470" height="89" /></h2>      </td>
    </tr>
  </table>
<div id="nav">
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
</div></td></tr>
<tr bgcolor="#FFFFFF" valign="top">
<td style="width: 110px"> 
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
  
<div id="database"> 
  <FORM METHOD="Post" name="frmQuest" ACTION="askyourquestiona.asp" onSubmit="return validate()" >
  
     <p class="ExtraBold style7" align="center">Ask Grandpa Mac Here </p>
   <p align="justify">
   <table border="0" height="81" width="232">
    <tr>
      <td height="23" width="68">First Name</td>
      <td height="23" width="148"><input name="txtFirstName" type="text" size="20" /></td>
    </tr>
    <tr>
      <td height="23" width="68">Last Name</td>
      <td height="23" width="148"> <input name="txtLastName" type="text" size="20" /></td>
    </tr>
    <tr>
      <td height="17" width="68">E-Mail</td>
      <td height="17" width="148"><input name="txtEmail" type="text" size="20" />
      </td>
    </tr>
   </table>
   
    <p align="justify">Thank you for taking the time to write. I can promise you that I will answer your question with the same kind of advice and concern that I give to my own children. </p>
						  I will do my best to return the answer to your question within a four day period.
                       For the advice I offer my fee is $9.99 per question.
                       
    <p class="ExtraBold"><strong>Ask Your Question Here: </strong></p>
      <p>In order to provide the best advice I can, please give me as much detailed information about yourself and the background to the question you are asking. </p>
      <p>
        <textarea name="txtQuestion" cols="63" rows="10"></textarea>
  </p>

    <p align="justify"><span class="style7"><strong>Remember... </strong></span>the advice I am giving is only for the benefit of allowing you to look at your  question through the eyes of another. Use my advice wisely. as I make no claim that it is the only way to solve your problem. <br />
      <br />
      Grandpa Mac </p>
    <p>
      <input type="checkbox" name="chkBoxTerms" value="checkbox" />
      <strong>TERMS AND CONDITIONS.&nbsp;</strong>  By checking this box, I hereby signify that I have read, understand and  agree to the <strong><a href="Terms.asp" target="_blank"><font color="red">TERMS AND CONDITIONS</font></a></strong> contained and pertaining to this website.&nbsp;  Thank you.</p>
    <div align="left">
      <table width="325" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="100%"><div align="left">
              <div class="Button" id="Button">
                <div align="center">

                </div>
              </div>
          </div>
            <p align="center"><img src="images/Shim.gif" width="15" />
            <div align="left">
              <div id="Button02">
                <div align="center">

 
      <input type="submit" name="btSubmit" value="Submit" />
                  </FORM>
                  Your Question And<br />
                  Make
                  Payment <br />
                  Via Credit Card 
                  Or<br />
                  Personal Check.</div>
              </div>
          </div></td>
        </tr>
      </table>
    </div>
    <p align="left">      </p>
  </div>
</td></tr><tr bgcolor="#FFFFFF"><td>&nbsp;</td><td align="center">
<span class="style5"><br /><p align="center">
  <a href="javascript:TellFriend('http://www.askgrandpamac.com/tell_a_friend_script.asp?page=<%
Response.write server.URLEncode("http://www.askgrandpamac.com")
%>')"><B><font color="blue" size="2">Tell A Friend About AskGrandpaMac.com<br />
  </font></B></a><span class="style5"><br />
    The Information Provided On This Website Has Been Copyrighted And Is Protected.</span><br />
  <span class="style5">Copyright &copy; MMV AskGrandpaMac.com/Edward J. McGrath</span> 



    </td>
    </tr>
    </table>
</body>

