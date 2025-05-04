// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.FilteredTextBoxBehavior = function(element) {
    /// <summary>
    /// The FilteredTextBoxBehavior is used to prevent invalid characters from being entered into a textbox
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement">
    /// The textbox element this behavior is associated with
    /// </param>
    AjaxControlToolkit.FilteredTextBoxBehavior.initializeBase(this, [element]);
    
    this._keypressHandler = null;
    this._changeHandler = null;
    
    this._filterType =  AjaxControlToolkit.FilterTypes.Custom;
    this._validChars = null;
    
    this.charTypes = new Object(); 
    
    this.charTypes["LowercaseLetters"] = "abcdefghijklmnopqrstuvwxyz";
    this.charTypes["UppercaseLetters"] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    this.charTypes["Numbers"]          = "0123456789";    
}
AjaxControlToolkit.FilteredTextBoxBehavior.prototype = {
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        AjaxControlToolkit.FilteredTextBoxBehavior.callBaseMethod(this, 'initialize');
        
        var element = this.get_element();
        
        this._keypressHandler = Function.createDelegate(this, this._onkeypress);
        $addHandler(element, 'keypress', this._keypressHandler);
        
        this._changeHandler = Function.createDelegate(this, this._onchange);
        $addHandler(element, 'change', this._changeHandler);
    },
    
    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>
        var element = this.get_element();
        
        $removeHandler(element, 'keypress', this._keypressHandler);
        this._keypressHandler = null;
        
        $removeHandler(element, 'change', this._changeHandler);
        this._changeHandler = null;
        
        AjaxControlToolkit.FilteredTextBoxBehavior.callBaseMethod(this, 'dispose');
    },
    
    _getValidChars : function() {
        /// <summary>
        /// Get all the valid characters
        /// </summary>
        /// <returns type="String">
        /// All valid characters
        /// </returns>

        if (this._validChars) return this._validChars;
        this._validChars = "";
        
        for (type in this.charTypes) {
            var filterType = AjaxControlToolkit.FilterTypes.toString(this._filterType);
            if (filterType.indexOf(type) != -1) {   
                this._validChars += this.charTypes[type];
            }
        }

        return this._validChars;    
    },
    
    get_ValidChars : function() {
        /// <value type="String">
        /// A string consisting of all characters considered valid for the textbox, if "Custom" is specified as the field type. Otherwise this parameter is ignored.
        /// </value>
        return this.charTypes["Custom"];
    },
    set_ValidChars : function(value) {
        if (this._validChars != null || this.charTypes["Custom"] != value) {
            this.charTypes["Custom"] = value;
            this._validChars = null;
            this.raisePropertyChanged('ValidChars');
        }
    },
    
    get_FilterType : function() {
        /// <value type="AjaxControlToolkit.FilterTypes">
        /// FilterType - A the type of filter to apply, as a comma-separated combination of Numbers, LowercaseLetters, UppercaseLetters, and Custom. If Custom is specified, the ValidChars field will be used in addition to other settings such as Numbers.
        /// </value>
        return this._filterType;
    },        
    set_FilterType : function(value) {
        if (this._validChars != null || this._filterType != value) {
            this._filterType = value;
            this._validChars = null;
            this.raisePropertyChanged('FilterType');
        }
    },

    _onkeypress : function(evt) {
        /// <summary>
        /// Handler for the target textbox's key press event
        /// </summary>
        /// <param name="evt" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>

        // This handler will only get called for valid characters in IE, we use keyCode
        //
        // In FireFox, this will be called for all key presses, with charCode/which
        // being set for keys we should filter (e.g. the chars) and keyCode being
        // set for all other keys.
        //
        // scanCode = event.charCode
        //
        // In Safari, charCode, which, and keyCode will all be filled with the same value,
        // as well as keyIdentifier, which has the string representation either as "end" or "U+00000008"
        //
        // 1) Check for ctrl/alt/meta -> bail if true
        // 2) Check for keyIdentifier.startsWith("U+") -> bail if false
        // 3) Check keyCode < 0x20 -> bail
        // 4) Special case Delete (63272) -> bail
        
        var scanCode;

        if (evt.rawEvent.keyIdentifier) {
            // Safari
            // Note (Garbin): used the underlying rawEvent insted of the DomEvent instance.
            if (evt.rawEvent.ctrlKey || evt.rawEvent.altKey || evt.rawEvent.metaKey) {
                return;
            }
            
            if (evt.rawEvent.keyIdentifier.substring(0,2) != "U+") {
                return;
            }
            
            scanCode = evt.rawEvent.charCode; 
            if (scanCode == 63272 /* Delete */) {
                return;
            }
        } else {
            scanCode = evt.charCode;
        }  
            
        if (scanCode && scanCode >= 0x20 /* space */) {
            var c = String.fromCharCode(scanCode);                        
            if(!this._processKey(c)) {
                evt.preventDefault();
            }
        }
    },
    
    _processKey : function(key) {
        /// <summary>
        /// Determine whether the key is valid or whether it should be filtered out
        /// </summary>
        /// <param name="key" type="String">
        /// Character to be validated
        /// </param>
        /// <returns type="Boolean">
        /// True if the character should be accepted, false if it should be filtered
        /// </returns>

        // Allow anything that's not a printable character,
        // e.g. backspace, arrows, etc.  Everything above 32
        // should be considered allowed, as it may be Unicode, etc.
        var filter = this._getValidChars();
        
        // return true if we should accept the character.
        return (!filter || filter.length == 0 || filter.indexOf(key) != -1);
    },
    
    _onchange : function() {
        /// <summary>
        /// Handler for the target textbox's key change event which will filter
        /// the text again (to make sure no text was inserted without keypresses, etc.)
        /// </summary>
        
        var text = this.get_element().value;
        var i = 0;
        var chars = this._getValidChars()
        while (i < text.length) {
            if (chars.indexOf(text.substring(i, i+1)) == -1) {
                text = text.substring(0, i) + text.substring(i+1, text.length);
            } else {
                i++;
            }
        }

        this.get_element().value = text;
    }
}
AjaxControlToolkit.FilteredTextBoxBehavior.registerClass('AjaxControlToolkit.FilteredTextBoxBehavior', AjaxControlToolkit.BehaviorBase);
//    getDescriptor : function() {
//        var td = AjaxControlToolkit.FilteredTextBoxBehavior.callBaseMethod(this, 'getDescriptor');
//        
//        td.addProperty('ValidChars', String);        
//        td.addProperty('FilterType', String);        
//        return td;
//    }


AjaxControlToolkit.FilterTypes = function() {
    /// <summary>
    /// Character filter to be applied to a textbox
    /// </summary>
    /// <field name="Custom" type="Number" integer="true">
    /// Custom Characters
    /// </field>
    /// <field name="Numbers" type="Number" integer="true">
    /// Numbers (0123456789)
    /// </field>
    /// <field name="UppercaseLetters" type="Number" integer="true">
    /// Uppercase Letters (ABCDEFGHIJKLMNOPQRSTUVWXYZ)
    /// </field>
    /// <field name="LowercaseLetters" type="Number" integer="true">
    /// Lowercase Letters (abcdefghijklmnopqrstuvwxyz)
    /// </field>
    throw Error.invalidOperation();
}
AjaxControlToolkit.FilterTypes.prototype = {
    Custom           :  0x1,
    Numbers          :  0x2,
    UppercaseLetters :  0x4,
    LowercaseLetters :  0x8
}
AjaxControlToolkit.FilterTypes.registerEnum('AjaxControlToolkit.FilterTypes', true);