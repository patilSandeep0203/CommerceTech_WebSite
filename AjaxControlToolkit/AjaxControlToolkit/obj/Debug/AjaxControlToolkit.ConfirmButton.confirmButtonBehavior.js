// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.ConfirmButtonBehavior = function(element) {
    /// <summary>
    /// The ConfirmButtonBehavior extends buttons by providing a confirmation dialog when clicked
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// Button the behavior is associated with
    /// </param>
    AjaxControlToolkit.ConfirmButtonBehavior.initializeBase(this, [element]);

    // Confirm text
    this._ConfirmTextValue = null;
    
    // Click handler for the target control
    this._clickHandler = null;
}
AjaxControlToolkit.ConfirmButtonBehavior.prototype = {
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'initialize');
        
        // Attach the handler
        this._clickHandler = Function.createDelegate(this, this._onClick);
        $addHandler(this.get_element(), "click", this._clickHandler);    
    },

    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>

        // Detach event handlers
        if (this._clickHandler) {
            $removeHandler(this.get_element(), "click", this._clickHandler);
            this._clickHandler = null;
        }

        AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'dispose');
    },

    _onClick : function(e) {
        /// <summary>
        /// Button's click handler to display the confirmation dialog
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>

        if (this.get_element() && !this.get_element().disabled) {
            // Display confirm dialog and return result to allow cancellation
            if (!window.confirm(this._ConfirmTextValue)) {
                e.preventDefault();
                return false;
            }           
        }
    },

    get_ConfirmText : function() {
        /// <value type="String">
        /// The text to show when you want to confirm the click. (Note: HTML entities can be used here (ex: "&#10;" for new-line))
        /// </value>
        return this._ConfirmTextValue;
    },
    set_ConfirmText : function(value) {
        if (this._ConfirmTextValue != value) {
            this._ConfirmTextValue = value;
            this.raisePropertyChanged('ConfirmText');
        }
    }
}
AjaxControlToolkit.ConfirmButtonBehavior.registerClass('AjaxControlToolkit.ConfirmButtonBehavior', AjaxControlToolkit.BehaviorBase);
//    getDescriptor : function() {
//        var td = AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'getDescriptor');
//        // Add custom properties
//        td.addProperty('ConfirmText', String);
//        return td;
//    },