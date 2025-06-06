// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.CascadingDropDownBehavior = function(e) {
    /// <summary>
    /// The CascadingDropDownBehavior is used to populate drop downs with values from a web service
    /// </summary>
    /// <param name="e" type="Sys.UI.DomElement" domElement="true">
    /// The DOM element the behavior is associated with
    /// </param>
    AjaxControlToolkit.CascadingDropDownBehavior.initializeBase(this, [e]);

    // Properties
    this._parentControlID = null;
    this._category = null;
    this._promptText = null;
    this._loadingText = null;
    this._servicePath = null;
    this._serviceMethod = null;

    // Variables
    this._parentElement = null;
    this._changeHandler = null;
    this._parentChangeHandler = null;
    this._lastParentValues = null;
    this._selectedValue = null;
}
AjaxControlToolkit.CascadingDropDownBehavior.prototype = {
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        /// <returns />
        AjaxControlToolkit.CascadingDropDownBehavior.callBaseMethod(this, 'initialize');

        var e = this.get_element();

        // Clear any items that may have been put there for server side convenience
        this._clearItems();

        e.CascadingDropDownCategory = this._category;

        // Attach change handler to self
        this._changeHandler = Function.createDelegate(this, this._onChange);
        $addHandler(e, "change",this._changeHandler);
        
        if (this._parentControlID) {
            // Set properties on element so that children controls (if any) can have easy access
            this._parentElement = $get(this._parentControlID);
                    
            // Attach change handler to parent
            Sys.Debug.assert(this._parentElement != null, String.format(AjaxControlToolkit.Resources.CascadingDropDown_NoParentElement, this._parentControlID));
            if (this._parentElement) {
                e.CascadingDropDownParentControlID = this._parentControlID;
                this._parentChangeHandler = Function.createDelegate(this, this._onParentChange);
                
                $addHandler(this._parentElement, "change", this._parentChangeHandler);
                if (!this._parentElement.childDropDown) {
                    this._parentElement.childDropDown = new Array();
                }
                this._parentElement.childDropDown.push(this);
            }
        }

        // Simulate parent change to populate self, even if no parent exists.
        this._onParentChange(null, true);
    },

    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>
        /// <returns />

        var e = this.get_element();

        // Detach change handler for self
        if (this._changeHandler) {            
            $removeHandler(e, "change", this._changeHandler);
            this._changeHandler = null;
        }

        // Detach change handler for parent
        if (this._parentChangeHandler) {
            if (this._parentElement) {                
                $removeHandler(this._parentElement, "change", this._parentChangeHandler);
            }
            this._parentChangeHandler = null;
        }

        AjaxControlToolkit.CascadingDropDownBehavior.callBaseMethod(this, 'dispose');
    },

    _clearItems : function() {
        /// <summary>
        /// Clear the items from the drop down
        /// </summary>
        /// <returns />

        var e = this.get_element();
        while (0 < e.options.length) {
            e.remove(0);
        }
    },

    _isPopulated : function() {
        /// <summary>
        /// Determine whether the drop down has any items
        /// </summary>
        /// <returns type="Boolean">
        /// Whether the drop down has any items
        /// </returns>
        
        var items = this.get_element().options.length;
      
        if (this._promptText) {
            return items > 1;
        } else {
            return items > 0;
        }           
    },

    _setOptions : function(list, inInit, gettingList) {
        /// <summary>
        /// Set the contents of the DropDownList to the specified list
        /// </summary>
        /// <param name="list" mayBeNull="true" elementType="Object">
        /// Array of options (where each option has name and value properties)
        /// </param>
        /// <param name="inInit" type="Boolean" optional="true">
        /// Whether this is being called from the initialize method
        /// </param>
        /// <param name="gettingList" type="Boolean" optional="true">
        /// Whether we are fetching the list of options from the web service
        /// </param>
        /// <returns />

        if (!this.get_isInitialized()) {
            return;
        }

        var e = this.get_element();
        // Remove existing contents
        this._clearItems();

        // Populate prompt text (if available)
        var headerText;
        if (gettingList && this._loadingText) {
            headerText = this._loadingText;
        } else if (this._promptText) {
            headerText = this._promptText;
        }
        if (headerText) {
            var optionElement = new Option(headerText, "");
            e.options[e.options.length] = optionElement;
        }

        // Add each item to the DropDownList, selecting the previously selected item
        var selectedValueOption = null;
        var defaultIndex = -1;

        if (list) {
            for (i = 0 ; i < list.length ; i++) {
                var listItemName = list[i].name;
                var listItemValue = list[i].value;
                
                if (list[i].isDefaultValue) {
                    defaultIndex = i;
                    if (this._promptText) {
                        // bump the index if there's a prompt item in the list.
                        //
                        defaultIndex++;
                    }
                }

                var optionElement = new Option(listItemName, listItemValue);
                if (listItemValue == this._selectedValue) {
                    selectedValueOption = optionElement;
                }

                e.options[e.options.length] = optionElement;
            }
            if (selectedValueOption) {
                selectedValueOption.selected = true;
            }
        }
        
        // if we didn't match the selected value, and we found a default
        // item, select that one.
        //
        if (selectedValueOption) {
            // Call set_SelectedValue to store the text as well
            this.set_SelectedValue(e.options[e.selectedIndex].value, e.options[e.selectedIndex].text);
        } else if (!selectedValueOption && defaultIndex != -1) {
            e.options[defaultIndex].selected = true;
            this.set_SelectedValue(e.options[defaultIndex].value, e.options[defaultIndex].text);
        } else if (!inInit && !selectedValueOption && !gettingList) {
            this.set_SelectedValue('', '');
        }

        if (e.childDropDown && !gettingList) {
            for(i = 0; i < e.childDropDown.length; i++) {
                e.childDropDown[i]._onParentChange();
            }
        }
        else {
            if (list && (Sys.Browser.agent !== Sys.Browser.Safari) && (Sys.Browser.agent !== Sys.Browser.Opera)) {
                // Fire the onchange event for the control to notify any listeners of the change
                if (document.createEvent) {
                    var onchangeEvent = document.createEvent('HTMLEvents');
                    onchangeEvent.initEvent('change', true, false);
                    this.get_element().dispatchEvent(onchangeEvent);
                } else if( document.createEventObject ) {
                    this.get_element().fireEvent('onchange');
                }
            }
        }

        // Disable the control if prompt text is present and an empty list was populated
        if (headerText) {
            e.disabled = !list || (0 == list.length);
        }
    },

    _onChange : function() {
        /// <summary>
        /// Handler for the drop down's change event
        /// </summary>
        /// <returns />

        if (!this._isPopulated()) {
            return;
        }

        var e = this.get_element();

        // Record the selected value in the client state
        //
        if ((-1 != e.selectedIndex) && !(this._promptText && (0 == e.selectedIndex))) {
            this.set_SelectedValue(e.options[e.selectedIndex].value, e.options[e.selectedIndex].text);
        } else {
            this.set_SelectedValue('', '');
        }
    },

    _onParentChange : function(evt, inInit) {
        /// <summary>
        /// Handler for the parent drop down's change event
        /// </summary>
        /// <param name="evt" type="Object">
        /// Set by the browser when called as an event handler (unused here)
        /// </param>
        /// <param name="inInit" type="Boolean">
        /// Whether this is being called from the initialize method
        /// </param>
        /// <returns />

        var e = this.get_element();

        // Create the known category/value pairs string for sending to the helper web service
        // Follow parent pointers so that the complete state can be sent
        // Format: 'name1:value1;name2:value2;...'
        var knownCategoryValues = '';
        var parentControlID = this._parentControlID;
        while (parentControlID) {
            var parentElement = $get(parentControlID);
            if (parentElement && (-1 != parentElement.selectedIndex)){
                var selectedValue = parentElement.options[parentElement.selectedIndex].value;
                
                if (selectedValue && selectedValue != "") {
                    knownCategoryValues = parentElement.CascadingDropDownCategory + ':' + selectedValue + ';' + knownCategoryValues;
                    parentControlID = parentElement.CascadingDropDownParentControlID;
                    continue;
                }
            } 
            break;
        }
        
        if (knownCategoryValues != '' && this._lastParentValues == knownCategoryValues) {
            return;
        }
        
        this._lastParentValues = knownCategoryValues;
        
        // we have a parent but it doesn't have a valid value
        //
        if (knownCategoryValues == '' && this._parentControlID) {
            this._setOptions(null, inInit);
            return;
        }

        // Show the loading text (if any)
        this._setOptions(null, inInit, true);

        if (this._servicePath && this._serviceMethod) {
            // Call the helper web service
            Sys.Net.WebServiceProxy.invoke(this._servicePath, this._serviceMethod, false,
                { knownCategoryValues:knownCategoryValues, category:this._category },
                Function.createDelegate(this, this._onMethodComplete), Function.createDelegate(this, this._onMethodError));
        }
    },

    _onMethodComplete : function(result, userContext, methodName) {
        // Success, update the DropDownList
        this._setOptions(result);
    },

    _onMethodError : function(webServiceError, userContext, methodName) {
        // Indicate failure
        if (webServiceError.get_timedOut()) {
            this._setOptions( [ this._makeNameValueObject(AjaxControlToolkit.Resources.CascadingDropDown_MethodTimeout) ] );
        } else {
            this._setOptions( [ this._makeNameValueObject(String.format(AjaxControlToolkit.Resources.CascadingDropDown_MethodError, webServiceError.get_statusCode())) ] );
        }
    },

    _makeNameValueObject : function(message) {
        /// <summary>
        /// Create an object with name and value properties set to the provided message
        /// </summary>
        /// <param name="message" type="String">
        /// Message
        /// </param>
        /// <returns type="Object">
        /// Object with name and value properties set to the message
        /// </returns>

        return { 'name': message, 'value': message };
    },

    get_ParentControlID : function() {
        /// <value type="String">
        /// ID of the parent drop down in a hierarchy of drop downs
        /// </value>
       return this._parentControlID;
    },
    set_ParentControlID : function(value) {
        if (this._parentControlID != value) {
            this._parentControlID = value;
            this.raisePropertyChanged('ParentControlID');
        }
    },

    get_Category : function() {
        /// <value type="String">
        /// Category of this drop down
        /// </value>
        return this._category;
    },
    set_Category : function(value) {
        if (this._category != value) {
            this._category = value;
            this.raisePropertyChanged('Category');
        }
    },

    get_PromptText : function() {
        /// <value type="String">
        /// Prompt text displayed as the first entry in the drop down
        /// </value>
        return this._promptText;
    },
    set_PromptText : function(value) {
        if (this._promptText != value) {
            this._promptText = value;
            this.raisePropertyChanged('PromptText');
        }
    },

    get_LoadingText : function() {
        /// <value type="String">
        /// Loading text to to be displayed when getting the drop down's values from the web service
        /// </value>
        return this._loadingText;
    },
    set_LoadingText : function(value) {
        if (this._loadingText != value) {
            this._loadingText = value;
            this.raisePropertyChanged('LoadingText');
        }
    },

    get_SelectedValue : function() {
         /// <value type="String">
         /// Selected value of the drop down
         /// </value>
        return this._selectedValue;
    },
    set_SelectedValue : function(value, text) {
        if (this._selectedValue != value) {
            if (!text) {
                // Initial population by server; look for "value:::text" pair
                var i = value.indexOf(':::');
                if (-1 != i) {
                    text = value.slice(i + 3);
                    value = value.slice(0, i);
                }
            }
            this._selectedValue = value;
            AjaxControlToolkit.CascadingDropDownBehavior.callBaseMethod(this, 'set_ClientState', [ this._selectedValue+':::'+text ]);
            this.raisePropertyChanged('SelectedValue');
        }
    },

    get_ServicePath : function() {
        /// <value type="String" mayBeNull="true">
        /// Path to the web service
        /// </value>
        return this._servicePath;
    },
    set_ServicePath : function(value) {
        if (this._servicePath != value) {
            this._servicePath = value;
            this.raisePropertyChanged('ServicePath');
        }
    },

    get_ServiceMethod : function() {
        /// <value type="String">
        /// Name of the method to invoke on the web service
        /// </value>
        return this._serviceMethod;
    },
    set_ServiceMethod : function(value) {
        if (this._serviceMethod != value) {
            this._serviceMethod = value;
            this.raisePropertyChanged('ServiceMethod');
        }
    }
}
AjaxControlToolkit.CascadingDropDownBehavior.registerClass('AjaxControlToolkit.CascadingDropDownBehavior', AjaxControlToolkit.BehaviorBase);
//    getDescriptor : function() {
//        var td = AjaxControlToolkit.CascadingDropDownBehavior.callBaseMethod(this, 'getDescriptor');
//        // Add custom properties
//        td.addProperty('ParentControlID', String);
//        td.addProperty('Category', String);
//        td.addProperty('PromptText', String);
//        td.addProperty('LoadingText', String);
//        td.addProperty('ServicePath', String);
//        td.addProperty('ServiceMethod', String);
//        td.addProperty('SelectedValue', String);
//        return td;
//    },
