// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System.Web.UI.WebControls;
using System.Web.UI;

using AjaxControlToolkit.Design;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System;
using System.Security;


namespace AjaxControlToolkit
{
    class NumericUpDownDesigner : ExtenderControlBaseDesigner<NumericUpDownExtender>
    {
        private DesignerActionListCollection _actionLists;
        private List<string> _textBoxList;
        public List<string> TextBoxList
        {
            get { return _textBoxList; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                _textBoxList = new List<string>();
                if (this.Component.Site.Container is IDesignerHost)
                {
                    IDesignerHost dhost = (IDesignerHost)(this.Component.Site.Container);
                    foreach (Control control in ((Page)dhost.RootComponent).Controls)
                    {
                        if (control is TextBox)
                            _textBoxList.Add(control.ID);
                    }
                }
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.AddRange(base.ActionLists);

                    // Add a custom DesignerActionList
                    _actionLists.Add(new ActionList(this));
                }

                return _actionLists;
            }
        }

        public class ActionList : DesignerActionList
        {
//            private NumericUpDownDesigner _parent;
            private DesignerActionItemCollection _items;

            // Constructor
            public ActionList(NumericUpDownDesigner parent)
                : base(parent.Component)
            {
//                _parent = parent;

            }
/*
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Use in GetSortedActionItems")]
            [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
            public Microsoft.Web.UI.TargetControlPropertiesCollection<NumericUpDownProperties> Targets
            {
                get
                {
                    return ((NumericUpDownExtender)_parent.Component).TargetProperties;
                }
            }
  
 * */

            // Create the ActionItem collection
            [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (_items == null)
                {
                    _items = new DesignerActionItemCollection();
                    //Add Properties
                    _items.Add(new DesignerActionPropertyItem("Targets", "Change Items Properties"));
                }
                return _items;
            }
        }
    }
}
