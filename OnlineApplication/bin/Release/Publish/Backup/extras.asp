<%
'*************************************************************************
'*   Product:  Affiliate Wiz Version 3 
'*   Copyright (c) 2000-2002 by AdComplete.com, LLC.  All rights reserved.
'*************************************************************************

'Function to encode output
Function MakeSafeOutput(strData)
	
	If IsNull(strData)=False and Trim(strData)<>"" Then
		MakeSafeOutput=Server.HTMLEncode(strData)
	Else
		MakeSafeOutput=strData
	End If

End Function

'***************************************************************************
' Function to establish database connection
'***************************************************************************
Function gConnADODB (ConnectString)
   Dim Conn
   Set Conn = Server.Createobject("ADODB.Connection")
   Conn.ConnectionTimeout = 60
   Conn.Open ConnectString
   Set gConnADODB = Conn
End Function

'***************************************************************************
' Function to close database connection
'***************************************************************************
Sub closeConnection (Conn)
   If (IsObject(Conn) ) Then
      Conn.Close
      Set Conn= Nothing
   End If
End Sub
%>
<% ''''''''''''''''''''Change blank fields to " "   '''''''''''''''''''''''''''''''''''''''''''
Function FixBlank(strParameter)
	If Trim(strParameter)="" Or IsNull(strParameter) Then
		FixBlank=" "
	Else
		FixBlank=Replace(strParameter, "'", "''")
		FixBlank=Trim(FixBlank)
	End If
End Function  %>

<% ''''''''''''''''''''Set False Check box to 0 " "   '''''''''''''''''''''''''''''''''''''''''''
Function SetTrueFalse(strParameter)
	If Trim(strParameter)="-1" Then
		SetTrueFalse=1
	Else
		SetTrueFalse=0
	End If
End Function %>
<% '''''''''''''''''''''change blank field to 0 '''''''''''''''''''''''''''''''''''''''''''''''''
Function FixZero(strData)
	If Trim(strData)="" Or IsNull(strData) Then
		FixZero=Clng(0)
	Else
		'FixZero=Replace(strData,",",".")
		FixZero=strData
	End If
End Function
%>
<% '''''''''''''''''''''Fix Double number '''''''''''''''''''''''''''''''''''''''''''''''''
Function FixDouble(strData)
	Dim dblTemp,dblTemp2
	If Trim(strData)="" Or IsNull(strData) Then
		FixDouble=0
	Else
		'FixDouble=Replace(strData,",",".")
		dblTemp=CDbl(strData)
		FixDouble=dblTemp
		If dblTemp<1000 Then
			If Instr(CStr(dblTemp),",")>0 Then
				'number has comma as decimal
				FixDouble=Replace(dblTemp,chr(44),chr(46))
			End If
		End If
		If dblTemp>=1000 Then
			If Instr(CStr(dblTemp),",")>Instr(CStr(dblTemp),".") Then
				'decimal and period are reversed
				dblTemp2=Replace(dblTemp,chr(46),"")
				dblTemp3=Replace(dblTemp2,chr(44),chr(46))
				FixDouble=dblTemp3
			End If
		End If		
	End If
End Function
%>
<% '''''''''''''''''''''change comma to decimal'''''''''''''''''''''''''''''''''''''''''''''''''
Function FixComma(strData)
	If Trim(strData)="" Then
		FixComma=0
	Else
		FixComma=Replace(strData,",",".")
	End If
End Function
%>
<% '''''''''''''''''''''Change color to Hex'''''''''''''''''''''''''''''''''''''''''''''''''
Function FixHex(strData)
	If InStr(strData,"#")>0 Then
		FixHex=strData
	Else
		FixHex="#" & strData
	End If
End Function

'***************************************************************************
' Function to establish database connection
'***************************************************************************
Function gConnADODB (ConnectString)
   Dim Conn
   Set Conn = Server.Createobject("ADODB.Connection")
   Conn.ConnectionTimeout = 30
   Conn.Open ConnectString
   Set gConnADODB = Conn
End Function

'***************************************************************************
' Function to close database connection
'***************************************************************************
Sub closeConnection (Conn)
   If (IsObject(Conn) ) Then
      Conn.Close
      Set Conn= Nothing
   End If
End Sub

'Function to encode output
Function MakeSafeOutput(strData)
	
	If IsNull(strData)=False and Trim(strData)<>"" Then
		MakeSafeOutput=Server.HTMLEncode(strData)
	Else
		MakeSafeOutput=strData
	End If

End Function


Function PathName(strPathAndFile)

    '********************************************************************************************
    'Returns just the path name without the file name.
    '********************************************************************************************

    '********************************************************************************************
    'declarations
    '********************************************************************************************
     
    Dim intI		             'counter variable
    Dim intInputLength 		     'length of the input path and file name
    Dim intEndPath                   'left position of filename in input path & file name
    
    '********************************************************************************************
    'code
    '********************************************************************************************
    
    intInputLength = Len(strPathAndFile)
    
    For intI = intInputLength To 1 Step -1
        If Mid(strPathAndFile, intI, 1) = "\" Then
            intEndPath = intI
            Exit For
        End If
    Next
    
    PathName = Left(strPathAndFile, intEndPath)

End Function

Function getFilePath()
	Dim lsPath, arPath

	' Obtain the virtual file path. The SCRIPT_NAME
	' item in the ServerVariables collection in the
	' Request object has the complete virtual file path
	lsPath = Request.ServerVariables("SCRIPT_NAME")
                           
	' Split the path along the /s. This creates an
	' This creates an one-dimensional array 
	arPath = Split(lsPath, "/")

	' Set the last item in the array to blank string
	' (The last item actually is the file name)
	arPath(UBound(arPath,1)) = ""
	
	' Join the items in the array. This will
	' give you the virtual path of the file
	GetFilePath = Join(arPath, "/")
End Function

%>