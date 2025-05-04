<%@ Application Language="C#" %>

<script runat="server">

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
    }

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    /*void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Headers.Remove("X-Powered-By");
        HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
        HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        HttpContext.Current.Response.Headers.Remove("Server");
        HttpContext.Current.Response.Headers.Remove("Server");
    }

    protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Headers.Remove("Server");
        HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
        HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
    }*/

    public class RemoveServerHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
       
</script>
