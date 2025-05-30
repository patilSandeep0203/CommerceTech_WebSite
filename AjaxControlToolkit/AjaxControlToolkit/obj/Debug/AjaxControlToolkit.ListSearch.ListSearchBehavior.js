// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

// ListSearchExtender originally created by Damian Mehers http://damianblog.com
Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.ListSearchBehavior = function(element) {
    /// <summary>
    /// The ListSearchBehavior allows users to search incrementally within a Select
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// Select associated with the behavior
    /// </param>

    AjaxControlToolkit.ListSearchBehavior.initializeBase(this, [element]);

    // Properties
    this._promptCssClass = null;
    this._promptText = AjaxControlToolkit.Resources.ListSearch_DefaultPrompt;
    this._offsetX = 0;
    this._offsetY = 0;
    this._promptPosition = null;


    // Variables
    this._popupBehavior = null;
    this._originalIndex = 0;    // Index of the selected option when a key is first hit (before it is changed by the browser)
    this._newIndex = -1;         // New index to which we want to move.   We need this because Firefox shifts the selected option even though we preventDefault and preventPropagation in _onKeyPress.
    this._showingPromptText = false;
    this._searchText = '';      // Actual search text (text displayed in the PromptDiv may be clipped)
    this._ellipsis = String.fromCharCode(0x2026);
    this._binarySearch = false;
    this._applicationLoadDelegate = null;
}

AjaxControlToolkit.ListSearchBehavior.prototype = {

    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>

        AjaxControlToolkit.ListSearchBehavior.callBaseMethod(this, 'initialize');
        
        var element = this.get_element();
        
        // Check for a SELECT since a ControlAdapter could have rendered the ListBox or DropDown as something else
        if(element && element.tagName === 'SELECT') {

            $addHandlers(element,
                         { 'focus'    : this._onFocus,
                           'blur'     : this._onBlur,
                           'keydown'  : this._onKeyDown,
                           'keyup'  :   this._onKeyUp,
                           'keypress' : this._onKeyPress
                         },
                         this);

            // We use the load event to determine whether the control has focus and display prompt text.  We can't do it here.
            this._applicationLoadDelegate = Function.createDelegate(this, this._onApplicationLoad);
            Sys.Application.add_load(this._applicationLoadDelegate);
        }
    },
    
    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>
        $clearHandlers(this.get_element());
        this._disposePopupBehavior();
        
        if(this._applicationLoadDelegate) {
            Sys.Application.remove_load(this._applicationLoadDelegate);
            this._applicationLoadDelegate = null;
        }

        AjaxControlToolkit.ListSearchBehavior.callBaseMethod(this, 'dispose');
    },

    _onApplicationLoad : function(sender, applicationLoadEventArgs) {
        /// <summary>
        /// Handler called automatically when a all scripts are loaded and controls are initialized
        /// Called after all scripts have been loaded and controls initialized.  If the current Select is the one that has
        /// focus then it shows the prompt text.  We cannot do this in the initialize method because the second pass initialization
        /// of the popup behavior hides it.
        /// </summary>
        /// <param name="sender" type="Object">
        /// Sender
        /// </param>
        /// <param name="applicationLoadEventArgs" type="Sys.ApplicationLoadEventArgs">
        /// Event arguments
        /// </param>

        // Determine if this SELECT is focused initially
        var hasInitialFocus = false;
        
        var clientState = AjaxControlToolkit.ListSearchBehavior.callBaseMethod(this, 'get_ClientState');
        if (clientState != null && clientState != "") {
            hasInitialFocus = (clientState === "Focused");
            AjaxControlToolkit.ListSearchBehavior.callBaseMethod(this, 'set_ClientState', null);
        }
        
        if(hasInitialFocus) {
            this._handleFocus();
        }
    },

    
   
    _isSorted : function(options) {
        /// <summary>
        /// Checks to see if the list is sorted to see if we can do the fast binary search or the slower linear search
        /// </summary>
        /// <param name="options" type="Object">
        /// Collections of options in a SELECT
        /// </param>

        var previousOptionValue = null;
        var optionsLength = options.length;
        for(var index = 0; index < optionsLength; index++) {
            var optionValue = options[index].text.toLowerCase();
            if(previousOptionValue && this._compareStrings(optionValue, previousOptionValue) < 0) {
                return false;
            }
            previousOptionValue = optionValue;
        }
        return true;
    },
    
    _onFocus : function(e) {
        /// <summary>
        /// Handler for the Select's focus event
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>
        this._handleFocus();
    },
    
    _handleFocus : function() {
        /// <summary>
        /// Utility method called when the form is loaded if the Select has the default focus, or when it is explicitly focused
        /// </summary>
        var element = this.get_element();
        this._promptDiv = document.createElement('div');
        // Need to initialize _promptDiv here even though it is set below by _updatePromptDiv because otherwise both
        // FireFox and IE bomb -- I guess there needs to be some content in the Div.  We need a value, which will be overwritten
        // below anyway.
        this._promptDiv.innerHTML = this._promptText && this._promptText.length > 0 ? this._promptText : AjaxControlToolkit.Resources.ListSearch_DefaultPrompt;
        this._showingPromptText = true;
        
        if(this._promptCssClass) {
            this._promptDiv.className = this._promptCssClass;
        }
                
        element.parentNode.insertBefore(this._promptDiv, element.nextSibling);
        this._promptDiv.style.overflow = 'hidden';
        this._promptDiv.style.height = this._promptDiv.offsetHeight + 'px';
        this._promptDiv.style.width = element.offsetWidth + 'px';

        // Hook up a PopupBehavior to the promptDiv
        this._popupBehavior = $create(AjaxControlToolkit.PopupBehavior, { parentElement : element }, {}, {}, this._promptDiv);
        if (this._promptPosition && this._promptPosition == AjaxControlToolkit.ListSearchPromptPosition.Bottom) {
            this._popupBehavior.set_positioningMode(AjaxControlToolkit.PositioningMode.BottomLeft);
        } else {
            this._popupBehavior.set_positioningMode(AjaxControlToolkit.PositioningMode.TopLeft);
        }
        this._popupBehavior.show();

        this._updatePromptDiv(this._promptText);
    },
    
    _onBlur : function() {
        /// <summary>
        /// Handle the Select's blur event
        /// </summary>
        this._disposePopupBehavior();

        // Remove the DIV showing the text typed so far
        var promptDiv = this._promptDiv;
        var element = this.get_element();

        if(promptDiv) {
           this._promptDiv = null;
           element.parentNode.removeChild(promptDiv);
        }
        
    },
    
    _disposePopupBehavior : function() {
        /// <summary>
        /// Utilty function to dispose of the popup behavior, called when the Select loses focus or when the extender is being disposed
        /// </summary>
        if (this._popupBehavior) {
            this._popupBehavior.dispose();
            this._popupBehavior = null;
        }
    },
    
    _onKeyDown : function(e) {
        /// <summary>
        /// Handler for the Select's KeyDown event
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>

        var element = this.get_element();
        var promptDiv = this._promptDiv;
        
        if(!element || !promptDiv) {
            return;
        }
        
        this._originalIndex = element.selectedIndex;

        if(this._showingPromptText) {
            promptDiv.innerHTML = '';
            this._searchText = '';
            this._showingPromptText = false;
            this._binarySearch = this._isSorted(element.options);   // Delayed until required
        }              

        // Backspace not passed to keyPressed event in IE, so handle it here
        if(e.keyCode == Sys.UI.Key.backspace) {
            e.preventDefault();
            e.stopPropagation();

            this._removeCharacterFromPromptDiv();
            this._searchForTypedText(element);
        } else if(e.keyCode == Sys.UI.Key.esc) {
            e.preventDefault();
            e.stopPropagation();

            promptDiv.innerHTML = '';
            this._searchText = '';
            this._searchForTypedText(element);
        }
    },

    
    _onKeyUp : function(e) {
        /// <summary>
        /// Handler for the Select's KeyUp event.  We need this because Firefox shifts the selected option even though
        /// we preventDefault and preventPropagation in _onKeyPress
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>

        var element = this.get_element();
        var promptDiv = this._promptDiv;
        
        if(!element || !promptDiv) {
            return;
        }

        if(this._newIndex == -1 || !element || !promptDiv || promptDiv.innerHTML == '') {
            this._newIndex = -1;
            return;
        }
        
        element.selectedIndex = this._newIndex;
        this._newIndex = -1;
    },

    _onKeyPress : function(e) {
        /// <summary>
        /// Handler for the Select's KeyPress event.
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>

        var element = this.get_element();
        var promptDiv = this._promptDiv;
        
        if(!element || !promptDiv) {
            return;
        }
        
        if(!this._isNormalChar(e)) {
            if(e.charCode == Sys.UI.Key.backspace) {
                e.preventDefault();
                e.stopPropagation();
            }
            return;
        }
        
        e.preventDefault();
        e.stopPropagation();

        // Add key pressed to the displayed DIV and search for it
        this._addCharacterToPromptDiv(e.charCode);
        this._searchForTypedText(element);
    },
    
    _isNormalChar : function(e) {
        /// <summary>
        /// Returns true if the specified charCode is a key rather than a normal (displayable) character
        /// </summary>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// Event info
        /// </param>
        /// <returns type="Boolean" />

        // Walking through Sys.UI.Keys won't work -- Ampersand is code 38 which matches 
        if (Sys.Browser.agent == Sys.Browser.Firefox && e.rawEvent.keyCode) {
            return false;
        }

        if (Sys.Browser.agent == Sys.Browser.Opera && e.rawEvent.which == 0) {
            return false;
        }

        if (e.charCode && (e.charCode < Sys.UI.Key.space || e.charCode > 6000)) {
            return false;
        }
        return true;
    },

    _updatePromptDiv : function(newText) {
        /// <summary>
        /// Updates the text in the promptDiv.
        /// </summary>
        /// <param name="newText" type="String">
        /// The new text to be displayed in the promptDiv.  Optional.  If not specified then uses the _searchText member.
        /// </param>
        ///
        var promptDiv = this._promptDiv;
        var text = typeof(newText) === 'undefined' ? this._searchText : newText;
        var textNode = promptDiv.firstChild;
        
        if(!textNode) {
            textNode = document.createTextNode(text);
            promptDiv.appendChild(textNode);
        } else {
            textNode.nodeValue = text;
        }

        if(promptDiv.scrollWidth <= promptDiv.offsetWidth && promptDiv.scrollHeight <= promptDiv.offsetHeight) {
           return;  // Already fit
        }

        // Remove characters until they fit
        for(var maxFit = text.length - 1; maxFit > 0 && (promptDiv.scrollWidth > promptDiv.offsetWidth || promptDiv.scrollHeight > promptDiv.offsetHeight); maxFit--) {
            textNode.nodeValue = this._ellipsis + text.substring(text.length - maxFit, text.length);
        }
    },

    _addCharacterToPromptDiv : function (charCode) {
        /// <summary>
        /// Adds the specified character to the promptDiv.
        /// </summary>
        /// <param name="charCode" type="Int">
        /// The charCode of the character to be added
        /// </param>
        this._searchText += String.fromCharCode(charCode);
        this._updatePromptDiv();
    },

    _removeCharacterFromPromptDiv : function () {
        /// <summary>
        /// Removes a character from the end of the promptDiv.
        /// </summary>
        if(this._searchText && this._searchText != '') {
            this._searchText = this._searchText.substring(0, this._searchText.length - 1);
            this._updatePromptDiv();
        }
    },

    _searchForTypedText  : function(element) {
        /// <summary>
        /// Searches for the text typed so far in the Select
        /// </summary>
        /// <param name="element" type="Sys.UI.DomElement" domElement="true">
        /// Select associated with the behavior
        /// </param>
        var searchText = this._searchText;
        var options = element.options;
        var text = searchText ? searchText.toLowerCase() : "";
        
        if(text.length == 0) {  // Probably hit delete -- select the first option
            if(options.length > 0) {
                element.selectedIndex = 0;
                this._newIndex = 0;
            }
            return;
        }
        
        var selectedIndex = -1;
        if(this._binarySearch) {
            selectedIndex = this._doBinarySearch(options, text, 0, options.length - 1);
        } else {
            selectedIndex = this._doLinearSearch(options, text, 0, options.length - 1);
        }
        
        // If nothing is found then stick with the current option
        if(selectedIndex == -1) {
            // Need this because firefox has aleady changed the current option based on the character typed
            this._newIndex = this._originalIndex; 
        } else {    // Otherwise move to the new option
            element.selectedIndex = selectedIndex;
            this._newIndex = selectedIndex;
        }
    },
    
    _compareStrings : function(strA, strB) {
        /// <summary>
        /// Compare two strings
        /// </summary>
        /// <param name="strA" type="String">
        /// The first string
        /// </param>
        /// <param name="strB" type="String">
        /// The second string
        /// </param>
        /// <returns type="Number" Integer="true">
        /// 0 if equal, -1 if strA < strB, 1 otherwise
        /// </returns>
        return ((strA == strB) ? 0 : ((strA < strB) ? -1 : 1))
    },
    
    _doBinarySearch : function(options, value, left, right) {
        /// <summary>
        /// Does a binary search for a value in the Select's options
        /// </summary>
        /// <param name="options" type="Object">
        /// The collection of options in the Select
        /// </param>
        /// <param name="value" type="String">
        /// The value being searched for
        /// </param>
        /// <param name="left" type="Int">
        /// The left bounds of the search
        /// </param>
        /// <param name="right" type="Right">
        /// The right bounds of the search
        /// </param>
        while (left <= right) {
            var mid = Math.floor((left+right)/2);
            var option = options[mid].text.toLowerCase().substring(0, value.length);
            var compareResult = this._compareStrings(value, option);
            if (compareResult > 0) {
                left = mid+1
            } else if(compareResult < 0) {
                right = mid-1;
            } else {
                // We've found a match, but it might not be the first -- do a linear search backwards
                while(mid > 0 && options[mid - 1].text.toLowerCase().startsWith(value)) {
                    mid--;
                }
                return mid;
            }
        }
        return -1;
    },
    
    _doLinearSearch : function(options, value, left, right) {
        /// <summary>
        /// Does a linear search for a value in the Select's options
        /// </summary>
        /// <param name="options" type="Object">
        /// The collection of options in the Select
        /// </param>
        /// <param name="value" type="String">
        /// The value being searched for
        /// </param>
        /// <param name="left" type="Int">
        /// The left bounds of the search
        /// </param>
        /// <param name="right" type="Right">
        /// The right bounds of the search
        /// </param>
        
        for(var i = left; i <= right; i++) {
            if(options[i].text.toLowerCase().startsWith(value)) {
                return i;
            }
        }
        
        return -1;
    },


    get_promptText : function() {
        /// <summary>
        /// The prompt text displayed when user clicks the list
        /// </summary>
        /// <value type="String" />
        return this._promptText;
    },              

    set_promptText : function(value) {
        if (this._promptText != value) {
            this._promptText = value;
            this.raisePropertyChanged('promptText');
        }
    },              

    get_promptCssClass : function() {
        /// <summary>
        /// CSS class applied to prompt when user clicks list.
        /// </summary>
        /// <value type="String" />
        return this._promptCssClass;
    },              

    set_promptCssClass : function(value) {
        if (this._promptCssClass != value) {
            this._promptCssClass = value;
            this.raisePropertyChanged('promptCssClass');
        }
    },

    get_promptPosition : function() {
        /// <value type="AjaxControlToolkit.ListSearchPromptPosition">
        /// Where the prompt should be positioned relative to the target control.
        /// Can be Top (default) or Bottom
        /// </value>
        return this._promptPosition;
    },

    set_promptPosition : function(value) {
        if (this._promptPosition != value) {
            this._promptPosition = value;
            this.raisePropertyChanged('promptPosition');
        }
    }
}

AjaxControlToolkit.ListSearchBehavior.registerClass('AjaxControlToolkit.ListSearchBehavior', AjaxControlToolkit.BehaviorBase);

AjaxControlToolkit.ListSearchPromptPosition = function() {
    throw Error.invalidOperation();
}

AjaxControlToolkit.ListSearchPromptPosition.prototype = {
    Top: 0,
    Bottom: 1
}
AjaxControlToolkit.ListSearchPromptPosition.registerEnum('AjaxControlToolkit.ListSearchPromptPosition');
