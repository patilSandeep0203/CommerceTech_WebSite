
<%
strtemp = Request.ServerVariables("HTTP_REFERER")
strURL = Request.ServerVariables("QUERY_STRING")

response.redirect ("../Application/start.aspx?"&strURL)

%>



