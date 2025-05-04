<%@ Page Language="C#"  AutoEventWireup="true"
Title="QuickBooks"
    Theme="Admin" %>
    <form>


    <table width="100%" align="center" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
<% SVShowContent();%>
  
<script runat="server">
  // Do not edit below this line
  // ---------------------------------------------------------------------------
  public void SVShowContent() {
    try 
    {
      string strURLToGet = "http://intuit.sharedvue.net/sharedvue/pull/";
      string strQuerystring = Request.QueryString.ToString();
      string strReturn = "";
      string strSVHost = "";
      
      if (strQuerystring.Length > 0) { strSVHost += System.Web.HttpUtility.UrlEncode("?" + strQuerystring); }
      
      strURLToGet += "?svhost=ecenow.com" + Request.ServerVariables["script_name"] + strSVHost;

      System.Net.WebRequest reqSVRemote = System.Net.WebRequest.Create(new Uri(strURLToGet));
      System.Net.WebResponse resSVRemote = reqSVRemote.GetResponse();
      System.IO.Stream strmSVContent = resSVRemote.GetResponseStream();
      System.IO.StreamReader srSVContent = new System.IO.StreamReader(strmSVContent, Encoding.ASCII);
      strReturn = srSVContent.ReadToEnd().Trim();
      
      strmSVContent.Close();
      resSVRemote.Close();
      
      Response.Write(strReturn);
    }
    catch (Exception ex)
    {
      Response.Write("<!-- SharedVue Output: " + ex.Message + " -->");
    }
    return;
  }
  // ---------------------------------------------------------------------------
</script>
            </td>
        </tr>
    </table>


</form>
