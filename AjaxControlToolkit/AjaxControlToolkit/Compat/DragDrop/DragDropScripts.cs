// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Text;

[assembly: System.Web.UI.WebResource("AjaxControlToolkit.Compat.DragDrop.DragDropScripts.js", "text/javascript")]
namespace AjaxControlToolkit {

    [RequiredScript(typeof(TimerScript))]
    [RequiredScript(typeof(BlockingScripts))]
    [ClientScriptResource(null, "AjaxControlToolkit.Compat.DragDrop.DragDropScripts.js")]
    public static class DragDropScripts {
    }
}
