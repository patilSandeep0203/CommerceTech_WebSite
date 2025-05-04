// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

// A simple version of the real behavior - just to verify script path works right.


Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.ConfirmButtonBehavior = function(element) { 
    AjaxControlToolkit.ConfirmButtonBehavior.initializeBase(this, [element]);
}

AjaxControlToolkit.ConfirmButtonBehavior.prototype = {

    //
    // Variables
    //

    // Properties
    _ConfirmTextValue : null,

    //
    // Overrides
    //

    initialize : function() {
        AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'initialize');

        this.get_element().value = this._ConfirmTextValue;
    },

    dispose : function() {
        AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'dispose');
    },

//    getDescriptor : function() {
//        var td = AjaxControlToolkit.ConfirmButtonBehavior.callBaseMethod(this, 'getDescriptor');
//        td.addProperty('ConfirmText', String);
//        return td;
//    },

    //
    // Property get/set methods
    //

    get_ConfirmText : function() {
        return this._ConfirmTextValue;
    },

    set_ConfirmText : function(value) {
        this._ConfirmTextValue = value;
    }
}

AjaxControlToolkit.ConfirmButtonBehavior.registerClass('AjaxControlToolkit.ConfirmButtonBehavior', AjaxControlToolkit.BehaviorBase);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();