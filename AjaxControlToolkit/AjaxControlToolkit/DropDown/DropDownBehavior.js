// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.DropDownBehavior = function AjaxControlToolkit$DropDownBehavior(element) {
    AjaxControlToolkit.DropDownBehavior.initializeBase(this, [element]);
    this._dropDownControl = null;
    this._dropDownControlID = null;
    this._highlightBorderColor = "#2353B2";
    this._highlightBackgroundColor = "#FFF3DB";
    this._dropArrowBackgroundColor = "#C6E1FF";
    this._dropArrowImageUrl = null;
    this._dropArrowWidth = "16px";
    this._oldBackgroundColor = null;
    this._dropFrame = null;
    this._dropArrow = null;
    this._dropArrowImage = null;
    this._dropWrapper = null;
    this._isOpen = false;
    this._isOver = false;
    this._wasClicked = null;
    this._dropWrapperHoverBehavior = null;
    this._dropFramePopupBehavior = null;
    this._dropPopupPopupBehavior = null;
    this._cacheDynamicResults = true;
    this._dynamicContextKey = null;
    this._dynamicServicePath = null;
    this._dynamicServiceMethod = null;
    this._dynamicPopulateBehavior = null;
    this._dynamicPopulated = false;
    
    this._dropDownControl$delegates = {
        click : Function.createDelegate(this, this._dropDownControl_onclick),
        contextmenu : Function.createDelegate(this, this._dropDownControl_oncontextmenu)
    }
    this._dropFrame$delegates = {
        click : Function.createDelegate(this, this._dropFrame_onclick),
        contextmenu : Function.createDelegate(this, this._dropFrame_oncontextmenu)
    }
    this._dropWrapper$delegates = {
        click : Function.createDelegate(this, this._dropWrapper_onclick),
        contextmenu : Function.createDelegate(this, this._dropWrapper_oncontextmenu)
    }
    this._document$delegates = {
        click : Function.createDelegate(this, this._document_onclick),
        contextmenu : Function.createDelegate(this, this._document_oncontextmenu)
    }
    this._dropWrapperHoverBehavior$delegates = {
        hover : Function.createDelegate(this, this._dropWrapperHoverBehavior_onhover),
        unhover : Function.createDelegate(this, this._dropWrapperHoverBehavior_onunhover)
    }
    this._dynamicPopulateBehavior$delegates = {
        populated : Function.createDelegate(this, this._dynamicPopulateBehavior_onpopulated)
    }
}
AjaxControlToolkit.DropDownBehavior.prototype = {

    add_popup : function(handler) {
        this.get_events().addHandler("popup", handler);
    },
    remove_popup : function(handler) {
        this.get_events().removeHandler("popup", handler);
    },
    raisePopup : function(args) {
        var eh = this.get_events().getHandler("popup");
        if (eh) {
            eh(this, args || Sys.EventArgs.Empty);
        }
    },

    add_populating : function(handler) {
        this.get_events().addHandler("populating", handler);
    },
    remove_populating : function(handler) {
        this.get_events().removeHandler("populating", handler);
    },
    raisePopulating : function() {
        var eh = this.get_events().getHandler("populating");
        if (eh) {
            eh(this, Sys.EventArgs.Empty);
        }
    },

    add_populated : function(handler) {
        this.get_events().addHandler("populated", handler);
    },
    remove_populated : function(handler) {
        this.get_events().removeHandler("populated", handler);
    },
    raisePopulated : function() {
        var eh = this.get_events().getHandler("populated");
        if (eh) {
            eh(this, Sys.EventArgs.Empty);
        }
    },

    get_dropDownControl : function() {
        return this._dropDownControl;
    },
    set_dropDownControl : function(value) {        
        if (this._dropDownControl != value) {
            this._dropDownControl = value;
            this.raisePropertyChanged("dropDownControl");
        }
    },
    
    get_highlightBorderColor : function() {
        return this._highlightBorderColor;
    },
    set_highlightBorderColor : function(value) {

        if (this._highlightBorderColor != value) {
            this._highlightBorderColor = value;
            if(this.get_isInitialized()) {
                // this._dropFrame.style.borderColor = value;
            }
            this.raisePropertyChanged("highlightBorderColor");
        }
    },
    
    get_highlightBackgroundColor : function() {
        return this._highlightBackgroundColor; 
    },
    set_highlightBackgroundColor : function(value) {

        if (this._highlightBackgroundColor != value) {
            this._highlightBackgroundColor = value;
            if(this.get_isInitialized() && this._isOpen) {
                this._dropWrapper.style.backgroundColor = value;
            }
            this.raisePropertyChanged("highlightBackgroundColor");
        }
    },
    
    get_dropArrowBackgroundColor : function() {
        return this._dropArrowBackgroundColor;
    },
    set_dropArrowBackgroundColor : function(value) {

        if (this._dropArrowBackgroundColor != value) {
            this._dropArrowBackgroundColor = value;
            if(this.get_isInitialized()) {
                this._dropArrow.style.backgroundColor = value;
            }
            this.raisePropertyChanged("dropArrowBackgroundColor");
        }
    },
    
    get_dropArrowImageUrl : function() {
        return this._dropArrowImageUrl;
    },
    set_dropArrowImageUrl : function(value) {
        if (this._dropArrowImageUrl != value) {
            this._dropArrowImageUrl = value;
            if(this.get_isInitialized()) {
                if (this._dropArrow.className) {
                    this._dropArrow.className = "";
                    this._dropArrowWrapper.style.display = 'block';
                }
                this._dropArrowImage.src = value;
            }
            this.raisePropertyChanged("dropArrowImageUrl");
        }
    },
    
    get_dropArrowWidth : function() {
        return this._dropArrowWidth;
    },
    set_dropArrowWidth : function(value) {

        if (this._dropArrowWidth != value) {
            this._dropArrowWidth = value;
            if(this.get_isInitialized()) {
                this._dropArrow.style.width = value;
            }
            this.raisePropertyChanged("dropArrowWidth");
        }
    },
    
    get_cacheDynamicResults : function() {
        return this._cacheDynamicResults;
    },
    set_cacheDynamicResults : function(value) {
        if (this._cacheDynamicResults != value) {
            this._cacheDynamicResults = value;
            this.raisePropertyChanged("cacheDynamicResults");
        }
    },

    get_dynamicContextKey : function() {
        return this._dynamicContextKey;
    },
    set_dynamicContextKey : function(value) {
        if (this._dynamicContextKey != value) {
            this._dynamicContextKey = value;
            this.raisePropertyChanged('dynamicContextKey');
        }
    },

    get_dynamicServicePath : function() {
        return this._dynamicServicePath;
    },
    set_dynamicServicePath : function(value) {
        if (this._dynamicServicePath != value) {
            this._dynamicServicePath = value;
            this.raisePropertyChanged('dynamicServicePath');
        }
    },

    get_dynamicServiceMethod : function() {
        return this._dynamicServiceMethod;
    },
    set_dynamicServiceMethod : function(value) {
        if (this._dynamicServiceMethod != value) {
            this._dynamicServiceMethod = value;
            this.raisePropertyChanged('dynamicServiceMethod');
        }
    },
    
    get_isOver : function() {
        return this._isOver;
    },
    
    get_isOpen : function() {
        return this._isOpen;
    },
    
    initialize : function() {
        AjaxControlToolkit.DropDownBehavior.callBaseMethod(this, 'initialize');        
        var elt = this.get_element();        
        var parent = elt.parentNode;
        
        //
        // _dropDownControl
        //
        if (this._dropDownControl == null) {
            $common.createElementFromTemplate({
                parent : parent,
                nameTable : this,
                name : "_dropDownControl",
                nodeName : "div",
                visible : false,
                cssClasses : this._dropDownControl ? null : ["ajax__dropdown_panel"],
                properties : { 
                    __GENERATED : true 
                }
            });
        }
        $addHandlers(this._dropDownControl, this._dropDownControl$delegates);

        //
        // _dropFrame
        // _dropArrow
        // _dropArrowWrapper
        // _dropArrowImage
        //        
        $common.createElementFromTemplate({
            parent : parent,
            nameTable : this,
            name : "_dropFrame",
            nodeName : "span",
            visible : false,
            children : [{
                name : "_dropFrameTop",
                nodeName : "div",
                cssClasses : ["ajax__dropdown_frame_line"]
            }, {
                name : "_dropFrameRight",
                nodeName : "div",
                cssClasses : ["ajax__dropdown_frame_line"]
            }, {
                name : "_dropFrameBottom",
                nodeName : "div",
                cssClasses : ["ajax__dropdown_frame_line"]
            }, {
                name : "_dropFrameLeft",
                nodeName : "div",
                cssClasses : ["ajax__dropdown_frame_line"]
            }, {
                name : "_dropArrow",
                nodeName : "div",
                cssClasses : (!this._dropArrowImageUrl) ? ["ajax__dropdown_arrow", "ajax__dropdown_arrow_image"] : ["ajax__dropdown_arrow"],
                properties : {
                    style : {
                        width : this._dropArrowWidth,
                        backgroundColor : this._dropArrowBackgroundColor
                    }
                },
                events : this._dropFrame$delegates,
                children : [{
                    name : "_dropArrowWrapper",
                    nodeName : "div",
                    visible : !!this._dropArrowImageUrl,
                    cssClasses : ["ajax__dropdown_arrow_wrapper"],
                    children : [{
                        name : "_dropArrowImage",
                        nodeName : "img",
                        properties : { 
                            src : this._dropArrowImageUrl
                        }
                    }]
                }]
            }]
        });

        //
        // _dropWrapper
        //
        $common.createElementFromTemplate({
            parent : null,
            nameTable : this,
            name : "_dropWrapper",
            nodeName : "span",
            properties : { 
                style : { 
                    cursor : "default" 
                }
            },
            events : this._dropWrapper$delegates,
            content : elt
        });
        
        //
        // _dropFramePopupBehavior
        //
        this._dropPopupPopupBehavior = $create(AjaxControlToolkit.PopupBehavior, { 
            positioningMode : AjaxControlToolkit.PositioningMode.BottomRight, 
            parentElement : elt, 
            y : -1 
        }, null, null, this._dropDownControl);
        
        //
        // _dropWrapperHoverBehavior
        //
        this._dropWrapperHoverBehavior = $create(AjaxControlToolkit.HoverBehavior, { 
            hoverElement : this._dropFrame 
        }, this._dropWrapperHoverBehavior$delegates, null, this._dropWrapper);

        //
        // wire events
        //
        $addHandlers(document, this._document$delegates);
    },    
    
    dispose : function() {
        var elt = this.get_element();
        if (this._isOpen) {
            this.hide();
            this.unhover();
            this._isOpen = false;
        }
        $common.removeHandlers(document, this._document$delegates);
        if (this._dynamicPopulateBehavior) {
            this._dynamicPopulateBehavior.dispose();
            this._dynamicPopulateBehavior = null;
        }
        if (this._dropPopupPopupBehavior) {
            this._dropPopupPopupBehavior.dispose();
            this._dropPopupPopupBehavior = null;
        }
        if (this._dropWrapperHoverBehavior) {
            this._dropWrapperHoverBehavior.dispose();
            this._dropWrapperHoverBehavior = null;
        }
        if (this._dropFrame) {
            $common.removeElement(this._dropFrame);
            this._dropFrame = null;
            this._dropFrameTop = null;
            this._dropFrameRight = null;
            this._dropFrameBottom = null;
            this._dropFrameLeft = null;
            this._dropArrow = null;
            this._dropArrowWrapper = null;
            this._dropArrowImage = null;
        }
        if (this._dropWrapper) {
            $common.removeHandlers(this._dropWrapper, this._dropWrapper$delegates);
            $common.unwrapElement(elt, this._dropWrapper);
            this._dropWrapper = null;
        }
        if (this._dropDownControl) {
            $common.removeHandlers(this._dropDownControl, this._dropDownControl$delegates);
            if (this._dropDownControl.__GENERATED) {
                $common.removeElement(this._dropDownControl);
            }
            this._dropDownControl = null;
        }
        AjaxControlToolkit.DropDownBehavior.callBaseMethod(this, 'dispose');
    },    

    hover : function() {
        var elt = this.get_element();
        if (!this._isOver) {
            this._isOver = true;
            var bounds = $common.getBounds(elt);
            $common.setBounds(this._dropFrameTop, { 
                x : bounds.x, 
                y : bounds.y,
                width : bounds.width,
                height : 1
            });
            $common.setBounds(this._dropFrameRight, {
                x : bounds.x + bounds.width - 1,
                y : bounds.y,
                width : 1,
                height : bounds.height
            });
            $common.setBounds(this._dropFrameBottom, {
                x : bounds.x,
                y : bounds.y + bounds.height - 1,
                width : bounds.width,
                height : 1
            });
            $common.setBounds(this._dropFrameLeft, {
                x : bounds.x,
                y : bounds.y,
                width : 1,
                height : bounds.height
            });
            $common.setBounds(this._dropArrow, {
                x : bounds.x + bounds.width - 17,
                y : bounds.y + 1,
                width : 16,
                height : bounds.height - 2
            });
            this._dropFrameTop.style.backgroundColor = this._highlightBorderColor;
            this._dropFrameRight.style.backgroundColor = this._highlightBorderColor;
            this._dropFrameBottom.style.backgroundColor = this._highlightBorderColor;
            this._dropFrameLeft.style.backgroundColor = this._highlightBorderColor;
            Sys.UI.DomElement.setVisible(this._dropFrame, true);
            if (!this._oldBackgroundColor) {            
                this._oldBackgroundColor = $common.getCurrentStyle(elt, 'backgroundColor');
            }
            elt.style.backgroundColor = this._highlightBackgroundColor;
        }
    },
    
    unhover : function() {
        var elt = this.get_element();
        if (this._isOver || !this._isOpen) {
            this._isOver = false;
            if (!this._isOpen) {        
                Sys.UI.DomElement.setVisible(this._dropFrame, false);
                if (this._oldBackgroundColor) {
                    elt.style.backgroundColor = this._oldBackgroundColor;
                    this._oldBackgroundColor = null;
                } else {
                    elt.style.backgroundColor = "transparent";
                }
            }
        }
    },
    
    show : function() {
        if (!this._isOpen) {
            this.hover();
            var args = new Sys.CancelEventArgs();
            this.raisePopup(args);
            if (!args.get_cancel()) {
                this._isOpen = true;
                this.populate();
                if (!this._dynamicPopulateBehavior || (this._dynamicPopulated && this._cacheDynamicResults)) {
                    this._dropPopupPopupBehavior.show();
                }
            }
        }
    },
    
    hide : function() {
        if (this._isOpen) {
            this._isOpen = false;
            this._dropPopupPopupBehavior.hide();
        }
    },

    populate : function(contextKeyOverride) {
        if (this._dynamicPopulated && this._cacheDynamicResults) {
            return;
        }
        if (this._dynamicPopulateBehavior && (this._dynamicPopulateBehavior.get_element() != this.get_element())) {
            this._dynamicPopulateBehavior.dispose();
            this._dynamicPopulateBehavior = null;
        }
        if (!this._dynamicPopulateBehavior && this._dynamicServiceMethod) {
            this._dynamicPopulateBehavior = $create(AjaxControlToolkit.DynamicPopulateBehavior, {
                ContextKey : this._dynamicContextKey,
                ServicePath : this._dynamicServicePath,
                ServiceMethod : this._dynamicServiceMethod
            }, this._dynamicPopulateBehavior$delegates, null, this._dropDownControl);
        }
        if(this._dynamicPopulateBehavior) {
            this.raisePopulating();
            this._dynamicPopulateBehavior.populate(contextKeyOverride ? contextKeyOverride : this._dynamicContextKey);
        }
    },
    
    _dropWrapperHoverBehavior_onhover : function(sender, e) {
        this.hover();
    },
    _dropWrapperHoverBehavior_onunhover : function(sender, e) {
        this.unhover();
    },
    _dropWrapper_onclick : function(e) {
        if(e.target.tagName != "A") {
            if(!this._isOpen) {
                this.show();
            } else {
                this.hide();
            }
            this._wasClicked = true;
        }
    },
    _dropWrapper_oncontextmenu : function(e) {
        if(e.target.tagName != "A") {
            this._wasClicked = true;
            e.preventDefault();
            this.show();
        }
    },
    _dropFrame_onclick : function(e) {
        if(!this._isOpen) {
            this.show();
        } else {
            this.hide();
        }
        this._wasClicked = true;
    },
    _dropFrame_oncontextmenu : function(e) {
        this._wasClicked = true;
        e.preventDefault();
        this.show();
    },
    _dropDownControl_onclick : function(e) {
        //this._wasClicked = true;
    },
    _dropDownControl_oncontextmenu : function(e) {
        this._wasClicked = true;
        e.preventDefault();
    },
    _document_onclick : function(e) {        
        if(this._wasClicked) {
            this._wasClicked = false;
        } else if(this._isOpen) {
            this.hide();
            this.unhover();
        }
    },
    _document_oncontextmenu : function(e) {
        if(this._wasClicked) {
            this._wasClicked = false;
        } else if(this._isOpen) {
            this.hide();
            this.unhover();
        }
    },
    _dynamicPopulateBehavior_onpopulated : function(sender, e) {
        this.raisePopulated();
        if (this._isOpen) {
            this._dynamicPopulated = true;
            this._dropPopupPopupBehavior.show();
        }
    }
}
AjaxControlToolkit.DropDownBehavior.registerClass('AjaxControlToolkit.DropDownBehavior', AjaxControlToolkit.BehaviorBase);