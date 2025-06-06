// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.CollapsiblePanelExpandDirection = function() {
    /// <summary>
    /// The CollapsiblePanelExpandDirection enumeration describes whether the panel is opened vertically or horizontally
    /// </summary>
    /// <field name="Horizontal" type="Number" integer="true" />
    /// <field name="Vertical" type="Number" integer="true" />
    throw Error.invalidOperation();
}
AjaxControlToolkit.CollapsiblePanelExpandDirection.prototype = {
    Horizontal : 0,
    Vertical: 1
}
AjaxControlToolkit.CollapsiblePanelExpandDirection.registerEnum("AjaxControlToolkit.CollapsiblePanelExpandDirection", false);


AjaxControlToolkit.CollapsiblePanelBehavior = function(element) {
    /// <summary>
    /// The CollapsiblePanelBehavior allows you to add collapsible sections to your page
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// Element to associate the behavior with
    /// </param>
    AjaxControlToolkit.CollapsiblePanelBehavior.initializeBase(this, [element]);
    
    // property values
    this._collapsedSize = 0;
    this._expandedSize = 0;
    this._scrollContents = null;    
    this._collapsed = false;    
    this._expandControlID = null;
    this._collapseControlID = null;
    this._textLabelID = null;    
    this._collapsedText = null;
    this._expandedText = null;
    this._imageControlID = null;
    this._expandedImage = null;
    this._collapsedImage = null;
    this._suppressPostBack = null;
    this._autoExpand = null;
    this._autoCollapse = null;
    this._expandDirection = AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical;
    
    // handler delegates
    this._collapseClickHandler = null;
    this._expandClickHandler = null;    
    this._mouseEnterHandler = null;    
    this._mouseLeaveHandler = null;
    
    // the div we wrap around the panel contents
    this._childDiv = null;
    
    // Animation used to open/close the panel
    this._animation = null;
}
AjaxControlToolkit.CollapsiblePanelBehavior.prototype = {    
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        AjaxControlToolkit.CollapsiblePanelBehavior.callBaseMethod(this, 'initialize');
        
        var element = this.get_element();
        this._animation = new AjaxControlToolkit.Animation.LengthAnimation(element, .25, 10, 'style', null, 0, 0, 'px');
        if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
            this._animation.set_propertyKey('height');
        } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
           this._animation.set_propertyKey('width');
        }
        this._animation.add_ended(Function.createDelegate(this, this._onAnimateComplete));
        
        // for checkboxes, we don't want to suppress the posback (e.g. return false)
        // because that stops them from toggling their state.
        //           
        if (this._suppressPostBack == null) {
            if (element.tagName == "INPUT" && element.type == "checkbox") {
                this._suppressPostBack = false;
                this.raisePropertyChanged('SuppressPostBack');
            }                    
            else if (element.tagName == "A") {
                this._suppressPostBack = true;
                this.raisePropertyChanged('SuppressPostBack');
            }
        }
        
        
        // Check our client state.  If it's present,
        // that means this is a postback, so we restore the state.
        //
        var lastState = AjaxControlToolkit.CollapsiblePanelBehavior.callBaseMethod(this, 'get_ClientState');                
        if (lastState && lastState != "") {
            var wasCollapsed = Boolean.parse(lastState);  
            if (this._collapsed != wasCollapsed) {
                this._collapsed = wasCollapsed;
                this.raisePropertyChanged('Collapsed');       
            }
        }
        
        this._setupChildDiv();        
        
        // setup the initial size and state
        //
        if (this._collapsed) {
            this._setTargetSize(this._getCollapsedSize());            
        } else {            
            this._setTargetSize(this._getExpandedSize());
        } 
        
        this._setupState(this._collapsed);
        
        // setup all of our handlers.
        if (this._collapseControlID == this._expandControlID) {
            this._collapseClickHandler = Function.createDelegate(this, this._toggle);
            this._expandClickHandler = null; // we don't need both if we're toggling.
        } else {
            this._collapseClickHandler = Function.createDelegate(this, this._doClose);
            this._expandClickHandler = Function.createDelegate(this, this._doOpen);       
        }
        
        if (this._autoExpand) {
            this._mouseEnterHandler = Function.createDelegate(this, this._onMouseEnter);
            $addHandler(element, 'mouseover', this._mouseEnterHandler);
        }       
        if (this._autoCollapse) {
            this._mouseLeaveHandler = Function.createDelegate(this, this._onMouseLeave);
            $addHandler(element, 'mouseout', this._mouseLeaveHandler);
        }
        
        // attach the click handlers
        //
        if (this._collapseControlID) {
            var collapseElement = $get(this._collapseControlID);
            if (!collapseElement) {
                throw Error.argument('CollapseControlID', String.format(AjaxControlToolkit.Resources.CollapsiblePanel_NoControlID, this._collapseControlID));
            } else {
                $addHandler(collapseElement, 'click', this._collapseClickHandler);
            }
        }
        
        if (this._expandControlID) {
            if (this._expandClickHandler) { // if it's a toggle don't set up again
                var expandElement = $get(this._expandControlID);
                if (!expandElement) {
                    throw Error.argument('ExpandControlID', String.format(AjaxControlToolkit.Resources.CollapsiblePanel_NoControlID, this._expandControlID));
                } else {
                    $addHandler(expandElement, 'click', this._expandClickHandler);
                }
            }
        }
    },
    
    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>
        
        var element = this.get_element();
        
        if (this._collapseClickHandler) {
            var collapseElement = (this._collapseControlID ? $get(this._collapseControlID) : null);
            if (collapseElement) {
                $removeHandler(collapseElement, 'click', this._collapseClickHandler);
            }
            this._collapseClickHandler = null;
        }
        
        if (this._expandClickHandler) {
            var expandElement = (this._expandControlID ? $get(this._expandControlID) : null);
            if (expandElement) {
                $removeHandler(expandElement, 'click', this._expandClickHandler);
            }
            this._expandClickHandler = null;
        }
        
        if (this._mouseEnterHandler) {
            $removeHandler(element, 'mouseover', this._mouseEnterHandler);
        }
        
        if (this._mouseLeaveHandler) {
            $removeHandler(element, 'mouseout', this._mouseLeaveHandler);
        }
        
        if (this._animation) {
            this._animation.dispose();
            this._animation = null;
        }
        
        AjaxControlToolkit.CollapsiblePanelBehavior.callBaseMethod(this, 'dispose');
    },
    
    _checkCollapseHide : function() {
        /// <summary>
        /// Check if a control is collapsed and hidden
        /// (and set its display to none if it is supposed to be hidden)
        /// </summary>
        /// <returns type="Boolean">
        /// Whether the control is collapsed and hidden
        /// </returns>

        if (this._collapsed && this._getTargetSize() == 0) {
            var e = this.get_element();
            var display = CommonToolkitScripts.getCurrentStyle(e, 'display');
            if (!e.oldDisplay && display != "none") {
                e.oldDisplay = display;
                e.style.display = "none";
            }
            return true;
        }
        return false;
    },
    
    _doClose : function(eventObj) {
        /// <summary>
        /// Collapse the panel
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>
        
        // TODO: Update to use the new event model
    
        if (this._animation) {
            // stop any running animation.
            //
            this._animation.stop();        
            
            // setup the animation state
            //        
            this._animation.set_startValue(this._getTargetSize());
            this._animation.set_endValue(this._getCollapsedSize());
            
            // do it!
            //
            this._animation.play();
        }
        
        this._setupState(true);
                
        // stop postback if necessary.
        //
        if (this._suppressPostBack) {
            if (eventObj && eventObj.preventDefault) {
                eventObj.preventDefault();
            } else {
                event.returnValue = false;
                return false;
            }
        }
    },
    
    _doOpen : function(eventObj) {
        /// <summary>
        /// Expand the Panel
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>
    
        // TODO: Update to use the new event model
    
        // stop any existing animation
        //
        if (this._animation) {
            this._animation.stop();
            var e = this.get_element();
            
            if (this._checkCollapseHide() && CommonToolkitScripts.getCurrentStyle(e, 'display', e.style.display)) {
                if (e.oldDisplay) {
                    e.style.display = e.oldDisplay;
                } else {
                    // IE isn't compliant on this one...
                    if (e.style.removeAttribute) {
                        e.style.removeAttribute("display");
                    } else {
                       e.style.removeProperty("display");
                    }
                }
                e.oldDisplay = null;
            }
            
            // setup the animation state
            //        
            this._animation.set_startValue(this._getTargetSize());
            this._animation.set_endValue(this._getExpandedSize());
            
            // do it!
            //
            this._animation.play();
        }
        
        // relect our state changes
        //
        this._setupState(false);
        
        // stop the postback if necessary.
        //
        if (this._suppressPostBack) {
            if (eventObj && eventObj.preventDefault) {
                eventObj.preventDefault();
            } else {
                event.returnValue = false;
                return false;
            }
        }
    },
    
    _onAnimateComplete : function() {
        /// <summary>
        /// Handler to listen for the end of the expand/collapse animation
        /// </summary>
    
        var e = this.get_element();
    
        // if we're fully expanded and the inner pannel is still smaller
        // than the size we've expanded to, fall back to auto
        //    
        if (!this._collapsed && !this._expandedSize)
        {
            if(this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
                if(this._childDiv.offsetHeight <= e.offsetHeight) {
                   e.style.height = "auto";
                   this.raisePropertyChanged('TargetHeight');
                } 
                else {
                    this._checkCollapseHide();
                }
            }
            else // horizontal
            {
                if( this._childDiv.offsetWidth <= e.offsetWidth) {
                   e.style.width = "auto";
                   this.raisePropertyChanged('TargetWidth');
                }
                else {
                    this._checkCollapseHide();
                }
            }
        }
        else {
            this._checkCollapseHide();
        }
        
        if (this._collapsed) {
            this.raiseCollapseComplete();
        } else {
            this.raiseExpandComplete()
        }
    },
    
    _onMouseEnter : function() {
        /// <summary>
        /// OnMouseOver Handler
        /// </summary>
        if (this._autoExpand) {
            this._doOpen();
        }
    },
    
    _onMouseLeave : function() {
        /// <summary>
        /// OnMouseOut Handler
        /// </summary>
        if (this._autoCollapse) {
           this._doClose();
        }
    },
    
    _getExpandedSize : function() {
        /// <summary>
        /// Get the size of the panel when fully expanded
        /// </summary>
        /// <returns type="Number" integer="true">
        /// Size of the panel when fully expanded
        /// </returns>
    
        if (this._expandedSize) {
            return this._expandedSize;
        }                
        
        if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
            return this._childDiv.offsetHeight;
        } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
            return this._childDiv.offsetWidth;
        }
    },
    
    _getCollapsedSize : function() {
        /// <summary>
        /// Get the size of the panel when fully collapsed
        /// </summary>
        /// <returns type="Number" integer="true">
        /// Size of the panel when fully collapsed
        /// </returns>

        if (this._collapsedSize) {
            return this._collapsedSize;
        }
        
        return 0;
    },
    
     _getTargetSize : function() {
         /// <summary>
         /// Get the target size of the Panel
         /// </summary>
         /// <returns type="Number" integer="true">
         /// Target size of the Panel
         /// </returns>

        var value;
        if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
           value = this.get_TargetHeight();
        } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
           value = this.get_TargetWidth();
        }       
        
        // Safari returns undefined if display is 'none'
        //
        if (value === undefined) {
            value = 0;
        }
        return value;
    },
    
    _setTargetSize : function(value) {
        /// <summary>
        /// Set the target size of the panel
        /// </summary>
        /// <param name="value" type="Number" integer="true">
        /// Target size of the panel
        /// </param>

        // we don't always want to set the target size here.
        // if we don't have an expanded size, and we're not collapsed,
        // and we're at (or past) our "maximum" size
        //
        var useSize = this._collapsed || this._expandedSize;
        var e = this.get_element();
        if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
            if (useSize || value < e.offsetHeight) {
                this.set_TargetHeight(value);
            } else {
                // if we're at our maximum size, flip to auto 
                // so that nested collapsible panels will
                // work properly.
                //
                e.style.height = "auto";
                this.raisePropertyChanged('TargetHeight');
            }
        } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
            if (useSize || value < e.offsetWidth) {
                this.set_TargetWidth(value);
            }
            else {
                e.style.width = "auto";
                this.raisePropertyChanged('TargetWidth');
            }            
        }
        this._checkCollapseHide();
    },
    
    _setupChildDiv : function() {
        /// <summary>
        /// Set up a child div to host our panel contents
        /// and then push the panel's children into it.
        /// </summary>

        
        var startSize = this._getTargetSize();
        
        var e = this.get_element();
                
        this._childDiv = e.cloneNode(false);
        this._childDiv.id = '';
        
        // move all children into the div.
        //
        while (e.hasChildNodes()) {            
            var child = e.childNodes[0];
            child = e.removeChild(child);
            this._childDiv.appendChild(child);                
        }

        //Delete some style after cloneNode
        //On Clone
        this._childDiv.style.position = ""; 
        this._childDiv.style.margin   = ""; 
        
        //On Parent
        e.style.padding = "";
        e.style.border = "";
                
        if (this._scrollContents) {        
            e.style.overflow = "scroll";
         }
         else {
            e.style.overflow = "hidden";
         }
                
        // we optimize for the case where the original size is the collapsed size.
        // if that's the case, we go ahead and set the inner panel to auto so you 
        // can still expand it but not see it expanded by default when the page loads.
        //
        if (startSize == this._collapsedSize) {
            if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
                this._childDiv.style.height = "auto"; 
            } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
                this._childDiv.style.width = "auto"; 
            }
        }
        
        e.appendChild(this._childDiv);
        
        if (this._collapsed) {
            startSize = this._getCollapsedSize();
        }
        else {
            startSize = this._getExpandedSize();
        }
            
        // set up our initial size
        //    
        if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical) {
            e.style.height = startSize + "px";
            e.style.width = CommonToolkitScripts.getCurrentStyle(this._childDiv, 'width');
            
            if (!this._expandedSize) {                                         
                e.style.height = "auto";
            }
            else {
               e.style.height = this._expandedSize + "px";
            }
            this._childDiv.style.height = "auto";                

        } else if (this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Horizontal) {
            e.style.height = CommonToolkitScripts.getCurrentStyle(this._childDiv, 'height');
            e.style.width = startSize + "px";
            
            //e.style.width = this._getExpandedSize() + "px";
  
            if (!this._expandedSize) {                                         
                e.style.width = "auto";
            }
            else {
               e.style.width = this._expandedSize + "px";
            }
            this._childDiv.style.width = "auto";
        }    
    },
    
    _setupState : function(isCollapsed) {
        /// <summary>
        /// Get all the state set consistently when we change modes
        /// </summary>
        /// <param name="isCollapsed" type="Boolean">
        /// True to setup the state as if we're collapsed, false to setup the state as if we're expanded
        /// </param>

    
        if (isCollapsed) {           
        
            // change the text label ID if we have one.
            //
            if (this._textLabelID && this._collapsedText) {
                var e = $get(this._textLabelID);
                
                if (e) {
                    e.innerHTML = this._collapsedText;
                }
            }
            
            // Change the image if we have one
            if (this._imageControlID && this._collapsedImage) {
                var i = $get(this._imageControlID);
                if (i && i.src) {
                    i.src = this._collapsedImage;
                    if (this._expandedText || this._collapsedText) {
                        i.title = this._collapsedText;
                    }
                }
            }            
        }
        else {  
            if (this._textLabelID && this._expandedText) {
                var e = $get(this._textLabelID);
                
                if (e) {
                    e.innerHTML = this._expandedText;
                }
            }
            
            // Change the image if we have one
            if (this._imageControlID && this._expandedImage) {
                var i = $get(this._imageControlID);
                if (i && i.src) {
                    i.src = this._expandedImage;
                    if (this._expandedText || this._collapsedText) {
                            i.title = this._expandedText;
                    }
                }
            }        
        } 
        
        // set our member variable and set the client state to reflect it
        // 
        if (this._collapsed != isCollapsed) {
            this._collapsed = isCollapsed;        
            this.raisePropertyChanged('Collapsed');
        }
        AjaxControlToolkit.CollapsiblePanelBehavior.callBaseMethod(this, 'set_ClientState', [this._collapsed.toString()]);                        
    },
    
    _toggle : function(eventObj) {
        /// <summary>
        /// Event handler to epxand or collapse the panel (based on its current state)
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>

        if (this.get_Collapsed()) {
            return this._doOpen(eventObj);
        } else {
            return this._doClose(eventObj);
        }
    },

    add_collapseComplete : function(handler) {
        /// <summary>
        /// Add a handler to the collapseComplete event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
    	this.get_events().addHandler('collapseComplete', handler);
    },
    remove_collapseComplete : function(handler) {
        /// <summary>
        /// Remove a handler from the collapseComplete event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
    	this.get_events().removeHandler('collapseComplete', handler);
    },
    raiseCollapseComplete : function() {
        /// <summary>
        /// Raise the collapseComplete event
        /// </summary>
    	var handlers = this.get_events().getHandler('collapseComplete');
    	if (handlers) {
    		handlers(this, Sys.EventArgs.Empty);
    	}
    },
    
    add_expandComplete : function(handler) {
        /// <summary>
        /// Add a handler to the expandComplete event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
    	this.get_events().addHandler('expandComplete', handler);
    },
    remove_expandComplete : function(handler) {
        /// <summary>
        /// Remove a handler from the expandComplete event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
    	this.get_events().removeHandler('expandComplete', handler);
    },
    raiseExpandComplete : function() {
        /// <summary>
        /// Raise the expandComplete event
        /// </summary>
    	var handlers = this.get_events().getHandler('expandComplete');
    	if (handlers) {
    		handlers(this, Sys.EventArgs.Empty);
    	}
    },

    get_TargetHeight : function() {
        /// <value type="Number" integer="true">
        /// Wrap the height of the panel
        /// </value>
        return this.get_element().offsetHeight;        
    },
    set_TargetHeight : function(value) {        
        this.get_element().style.height = value + "px";        
        this.raisePropertyChanged('TargetHeight');
    },
    
    get_TargetWidth : function() {
        /// <value type="Number" integer="true">
        /// Wrap the width of the panel
        /// </value>
        return this.get_element().offsetWidth;        
    },
    set_TargetWidth : function(value) {
        this.get_element().style.width = value + "px"        
        this.raisePropertyChanged('TargetWidth');
    },
        
    get_Collapsed : function() {
        /// <value type="Boolean">
        /// Whether or not the panel is collapsed
        /// </value>
        return this._collapsed;        
    },    
    set_Collapsed : function(value) {
        // if we're changing values, and we're live, toggle.
        if (this.get_isInitialized() && this.get_element() && value != this.get_Collapsed()) {
            this._toggle();
        }
        else {
            this._collapsed = value;
            this.raisePropertyChanged('Collapsed');
        }
    },
    
    get_CollapsedSize : function() {
        /// <value type="Number" integer="true">
        /// The size of the target, in pixels, when it is in the collapsed state
        /// </value>
        return this._collapsedSize;
    },
    set_CollapsedSize : function(value) {
        if (this._collapsedSize != value) {
            this._collapsedSize = value;
            this.raisePropertyChanged('CollapsedSize');
        }
    },
    
    get_ExpandedSize : function() {
        /// <value type="Number" integer="true">
        /// The size of the target, in pixels, when it is in the expanded state
        /// </value>
        return this._expandedSize;
    },
    set_ExpandedSize : function(value) {
        if (this._expandedSize != value) {
            this._expandedSize = value;
            this.raisePropertyChanged('ExpandedSize');
        }
    },
    
    get_CollapseControlID : function() {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._collapseControlID;
    },
    set_CollapseControlID : function(value) {
        if (this._collapseControlID != value) {
            this._collapseControlID = value;
            this.raisePropertyChanged('CollapseControlID');
        }
    },
    
    get_ExpandControlID : function() {
        /// <value type="String">
        /// ID of the control used to expand the target when clicked
        /// </value>
        return this._expandControlID;
    },    
    set_ExpandControlID : function(value) {
        if (this._expandControlID != value) {
            this._expandControlID = value;
            this.raisePropertyChanged('ExpandControlID');
        }
    },
    
    get_ScrollContents : function() {
        /// <value type="Boolean">
        /// Whether to add a scrollbar when the contents are larger than the target (the contents will be clipped if false)
        /// </value>
        return this._scrollContents;
    },
    set_ScrollContents : function(value) {
        if (this._scrollContents != value) {
            this._scrollContents = value;
            this.raisePropertyChanged('ScrollContents');
        }
    },
    
    get_SuppressPostBack : function() {
        /// <value type="Boolean">
        /// Whether or not to suppress postbacks generated when the CollapseControlID or ExpandControlID elements are clicked
        /// </value>
        return this._suppressPostBack;
    },
    set_SuppressPostBack : function(value) {
        if (this._suppressPostBack != value) {
            this._suppressPostBack = value;
            this.raisePropertyChanged('SuppressPostBack');
        }
    },
    
    get_TextLabelID : function() {
        /// <value type="String">
        /// ID of the element where the "status text" for the target will be placed
        /// </value>
        return this._textLabelID;
    },
    set_TextLabelID : function(value) {
        if (this._textLabelID != value) {
            this._textLabelID = value;
            this.raisePropertyChanged('TextLabelID');
        }
    },
    
    get_ExpandedText : function() {
        /// <value type="String">
        /// Text to show in the element specified by TextLabelID when the target is expanded.  This text is also used as the alternate text of the image if ImageControlID has been provided.
        /// </value>
        return this._expandedText;
    },
    set_ExpandedText : function(value) {
        if (this._expandedText != value) {
            this._expandedText = value;
            this.raisePropertyChanged('ExpandedText');
        }
    },
    
    get_CollapsedText : function() {
        /// <value type="String">
        /// Text to show in the element specified by TextLabelID when the target is collapsed.  This text is also used as the alternate text of the image if ImageControlID has been provided.
        /// </value>
        return this._collapsedText;
    },
    set_CollapsedText : function(value) {
        if (this._collapsedText != value) {
            this._collapsedText = value;
            this.raisePropertyChanged('CollapsedText');
        }
    },
    
    get_ImageControlID : function() {
        /// <value type="String">
        /// ID of the <img> element where an icon indicating the collapsed status of the target will be placed
        /// </value>
        return this._imageControlID;
    },
    set_ImageControlID : function(value) {
        if (this._imageControlID != value) {
            this._imageControlID = value;
            this.raisePropertyChanged('ImageControlID');
        }
    },
    
    get_ExpandedImage : function() {
        /// <value type="String">
        /// Path to an image to show in the element specified by ImageControlID when the target is expanded
        /// </value>
        return this._expandedImage;
    },
    set_ExpandedImage : function(value) {
        if (this._expandedImage != value) {
            this._expandedImage = value;
            this.raisePropertyChanged('ExpandedImage');
        }
    },
    
    get_CollapsedImage : function() {
        /// <value type="String">
        /// Path to an image to show in the element specified by ImageControlID when the target is collapsed
        /// </value>
        return this._collapsedImage;
    },
    set_CollapsedImage : function(value) {
        if (this._collapsedImage != value) {
            this._collapsedImage = value;
            this.raisePropertyChanged('CollapsedImage');
        }
    },
    
    get_AutoExpand : function() {
        /// <value type="Boolean">
        /// Whether to automatically expand the target when the mouse is moved over it
        /// </value>
        return this._autoExpand;
    },
    set_AutoExpand : function(value) {
        if (this._autoExpand != value) {
            this._autoExpand = value;
            this.raisePropertyChanged('AutoExpand');
        }
    },
    
    get_AutoCollapse : function() {
        /// <value type="Boolean">
        /// Whether to automatically collapse the target when the mouse is moved off of it
        /// </value>
        return this._autoCollapse;
    },
    set_AutoCollapse : function(value) {
        if (this._autoCollapse != value) {
            this._autoCollapse = value;
            this.raisePropertyChanged('AutoCollapse');
        }
    },    
    
    get_ExpandDirection : function() {
        /// <value type="AjaxControlToolkit.CollapsiblePanelExpandDirection">
        /// Direction the panel will expand and collapse (can be either "Vertical" or "Horizontal")
        /// </value>
        return this._expandDirection == AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical;
    },      
    set_ExpandDirection : function(value) {
        if (this._expandDirection != value) {
            this._expandDirection = value;
            this.raisePropertyChanged('ExpandDirection');
        }
    }
}
AjaxControlToolkit.CollapsiblePanelBehavior.registerClass('AjaxControlToolkit.CollapsiblePanelBehavior', AjaxControlToolkit.BehaviorBase);
//    getDescriptor : function() {
//        var td = AjaxControlToolkit.CollapsiblePanelBehavior.callBaseMethod(this, 'getDescriptor');
//        
//        // lots of properties to add here
//        //
//        td.addProperty('CollapsedSize', Number);        
//        td.addProperty('ExpandedSize', Number);        
//        td.addProperty('CollapseControlID', String);        
//        td.addProperty('ExpandControlID', String);        
//        td.addProperty('Collapsed', Boolean);
//        td.addProperty('ScrollContents', Boolean);
//        td.addProperty('SuppressPostBack', Boolean);
//        td.addProperty('AutoExpand', Boolean);        
//        td.addProperty('AutoCollapse', Boolean);        
//        td.addProperty('CollapsedText', String);
//        td.addProperty('ExpandedText', String);
//        td.addProperty('TextLabelID', String);
//        td.addProperty('CollapsedImage', String);
//        td.addProperty('ExpandedImage', String);
//        td.addProperty('ImageControlID', String);
//        td.addProperty('ExpandDirection', AjaxControlToolkit.CollapsiblePanelExpandDirection);
//        
//        // these are for internal use
//        //
//        td.addProperty('TargetHeight', Number);
//        td.addProperty('TargetWidth', Number);
//        
//        // events
//        //
//        td.addEvent('collapseComplete', true);
//        td.addEvent('expandComplete', true);
//        
//        return td;
//    },