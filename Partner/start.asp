
<%
strtemp = Request.ServerVariables("HTTP_REFERER")
strURL = Request.ServerVariables("QUERY_STRING")

response.redirect ("../onlineapplication/start.aspx?"&strURL)

%>



