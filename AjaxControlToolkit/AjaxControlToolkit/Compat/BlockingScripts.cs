// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;


[assembly: System.Web.UI.WebResource("AjaxControlToolkit.Compat.Blocking.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// Repository of old "Atlas" code that we're waiting to have integrated into the new Microsoft Ajax Library
    /// </summary>
    /// 
    [RequiredScript(typeof(CommonToolkitScripts))]
    [ClientScriptResource(null, "AjaxControlToolkit.Compat.Blocking.js")]
    public static class BlockingScripts
    {
    }
}
