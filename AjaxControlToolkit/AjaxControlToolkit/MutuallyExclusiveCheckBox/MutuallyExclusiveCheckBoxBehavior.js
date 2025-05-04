// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
Type.registerNamespace('AjaxControlToolkit');
AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior = function(element) {
    /// <summary>
    /// The MutuallyExclusiveCheckBoxBehavior allows only one checkbox in
    /// a group with the same Key to be checked at a time
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// The DOM Element the behavior is associated with
    /// </param>
    AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.initializeBase(this, [element]);
    
    this._key = "";
    this._clickHandler = Function.createDelegate(this, this._onclick);
}
AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.prototype = {
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.callBaseMethod(this, 'initialize');    
        $addHandler(this.get_element(), "click", this._clickHandler);
    },
    
    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>
        if (this._key) {
            var keys = AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.Keys;
            var ar = keys[this._key];
            Array.remove(ar, this);
            this._key = null;
        }
        $removeHandler(this.get_element(), "click", this._clickHandler);
        AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.callBaseMethod(this, 'dispose');
    },
    
    get_Key : function() {
        /// <value type="String">
        /// The unique key to use to associate checkboxes. This key does not respect INamingContainer renaming.
        /// </value>
        return this._key;
    },
    set_Key : function(value) {
        var keys = AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.Keys;
        if(value != this._key) {
            if(this._key) {
                var ar = keys[this._key];
                Array.remove(ar, this._key);
            }
            this._key = value;
            if(value) {
                var ar = keys[this._key];
                if(ar == null) {
                    ar = keys[this._key] = [];                    
                }
                Array.add(ar, this);
            }
        }
    },
    
    _onclick : function() {
        /// <summary>
        /// Click handler used to ensure only one checkbox in its key is checked
        /// </summary>
        var keys = AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.Keys;
        if(this._key && this.get_element().checked) {
            var ar = keys[this._key];
            var t = this;
            Array.forEach(ar, function(b) {
                /// <summary>
                /// Uncheck all other checkboxes in the same key group
                /// </summary>
                /// <param name="b" type="AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior">
                /// Checkbox in the same key group
                /// </param>
                if(b != t) {
                    b.get_element().checked = false;
                }
            });
        }
    }
}    
AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.Keys = {};
AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior.registerClass('AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior', AjaxControlToolkit.BehaviorBase);