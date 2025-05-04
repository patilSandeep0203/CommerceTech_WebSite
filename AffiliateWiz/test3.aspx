<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test3.aspx.cs" Inherits="test3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Clickable Slideshow</title>

<SCRIPT LANGUAGE="Javascript">
<!-- Hide script from old browsers

// *********************************************************************
//   This script can be used to click forwards, as well as backwards,
//                     through an "array" of images
// *********************************************************************
// Adapted from: "picturebooks/project/siteslides/project_slides.asp"
//   at http://landscapes.ce.vt.edu/ - ("From Landscapes To Waterscapes"
//   - Virginia Tech Civil and Environmental Engineering Dept.)
// Adaptation: Robin W. Moore (B.S., Computer Sciences, Virginia Tech,
//   1972)
// *********************************************************************
// **        THERE ARE SEVEN STEPS TO SETTING UP THIS SCRIPT          **
// *********************************************************************

// STEP 1 - Place your image addresses in double-quotes below and
//   SEPARATE them with commas. Leave out the last comma. Be forewarned
//   that if your images are not the same width and height, their
//   display will be distorted/contorted in some browsers to the
//   dimensions of your first listed image.

var myImages = new Array(

"images/0.image.jpg",
"images/1.image.jpg",
"images/2.image.jpg",
"images/3.image.jpg",
"images/4.image.jpg",
"images/5.image.jpg"

);

// END STEP 1

// STEP 2 - Place your image titles in double-quotes below and SEPARATE
//   them with commas. Leave out the last comma. The number of image
//   titles should match the number of your images.

var myCaptions = new Array(

"1st Caption",
"2nd Caption",
"3rd Caption",
"4th Caption",
"5th Caption",
"6th Caption"

);

// END STEP 2

var thisPic = 0;  //the counter for the image array
var lastPic = (myImages.length-1);  //the last cell in our array = (number of pictures - 1)

function processPrevious()
  {
    if (document.images)
    {
      if (thisPic==0) //if at the very beginning of array
      {
        thisPic=lastPic; //goto the last cell in the array
      }
      else
      {
        thisPic-- ; //else simply decrement the counter
      }
      document.Image.src=myImages[thisPic];
      document.myForm.Caption.value=myCaptions[thisPic];
    }
  }

function processNext()
  {
    if (document.images)
    {
      if (thisPic==lastPic) //if at the very end of the array
      {
        thisPic=0; //goto the first cell of the array
      }
      else
      {
        thisPic++; //else simply increment the counter
      }
      document.Image.src=myImages[thisPic];
      document.myForm.Caption.value=myCaptions[thisPic];
    }
  }

//End hiding script from old browsers -->
</SCRIPT>
</head>

<body>
<center>

<FORM name="myForm">

<!-- STEP 3 - Enter the max. width of your images in 'width=' below -->

<table width="400">

<!-- END STEP 3 -->

<tr><td align="center">
<INPUT type="BUTTON"  value=" « " name="right" onClick="processPrevious()">
<INPUT type="BUTTON"  value=" » " name="left" onClick="processNext()">
</td></tr>

<tr><td align="center">

<!-- STEP 4 - Enter the path to your first image in 'src=' below -->
<!--          DO NOT CHANGE THE 'name="Image"' SETTING -->

<img src="images/0.image.jpg" name="Image" width="400">

<!-- STEP 5 - Enter the max. width of your images in 'width=' above -->
<!--          DO NOT CHANGE THE 'name="Image"' SETTING -->

<!-- END STEPS 4 and 5 -->

</td></tr>

<tr><td align="center">

<!-- STEP 6 - Enter the title of your first image in 'value=' below -->
<!--          DO NOT CHANGE THE 'name="Caption"' SETTING -->

<INPUT type="text" value="Caption 1" name="Caption" size=50 maxlength=300>

<!-- END STEP 6 -->

<hr width="100">
<font size="2">

<!-- STEP 7 - Enter any text you want to remain visible on the page -->

The Woodbridge Senior High School Mighty Viking Marching Band (a Virginia Honor Band) participates in the 2001 Old Bridge Christmas Parade

<!-- END STEP 7 -->

</font>
</td></tr>
</table>
</FORM>

</center>
</body>

</html>