// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

//  _SliderDragDropManagerInternal
//  This is a DragDropManager that returns always a GenericDragDropManager instance.
//  It is used in the Slider control to prevent IE from displaying the drag/drop icons 
//  when dragging the slider's handle.
//

AjaxControlToolkit._SliderDragDropManagerInternal = function() {
    AjaxControlToolkit._SliderDragDropManagerInternal.initializeBase(this);
    
    this._instance = null;
}
AjaxControlToolkit._SliderDragDropManagerInternal.prototype = {
    _getInstance : function() {
        this._instance = new AjaxControlToolkit.GenericDragDropManager();

        this._instance.initialize();
        this._instance.add_dragStart(Function.createDelegate(this, this._raiseDragStart));
        this._instance.add_dragStop(Function.createDelegate(this, this._raiseDragStop));
        
        return this._instance;
    }   
}
AjaxControlToolkit._SliderDragDropManagerInternal.registerClass('AjaxControlToolkit._SliderDragDropManagerInternal', AjaxControlToolkit._DragDropManager);
AjaxControlToolkit.SliderDragDropManagerInternal = new AjaxControlToolkit._SliderDragDropManagerInternal();

// SliderOrientation
// Specifies the orientation of the slider.
//
AjaxControlToolkit.SliderOrientation = function() {

}
AjaxControlToolkit.SliderOrientation.prototype = {
    Horizontal : 0,
    Vertical : 1
}
AjaxControlToolkit.SliderOrientation.registerEnum('AjaxControlToolkit.SliderOrientation', false);

// The SliderBehavior upgrades a textbox to a graphical slider.
//
AjaxControlToolkit.SliderBehavior = function(element) {
    AjaxControlToolkit.SliderBehavior.initializeBase(this, [element]);
    
    // Members
    //
    this._minimum =  0;
    this._maximum =  100;
    this._value = null;
    this._steps =  0;
    this._decimals =  0;
    this._orientation =  AjaxControlToolkit.SliderOrientation.Horizontal;
    this._railElement = null;
    this._railCssClass = null;
    this._isHorizontal =  true;
    this._isUpdatingInternal =  false;
    this._isInitializedInternal =  false;
    this._enableHandleAnimation =  false;
    this._handle = null;
    this._handleImage =  null;
    this._handleAnimation =  null;
    this._handleAnimationDuration =  0.1;
    this._handleImageUrl = null;
    this._handleCssClass = null;
    this._dragHandle = null;
    this._mouseupHandler = null;
    this._selectstartHandler = null;
    this._boundControlChangeHandler = null;
    this._boundControlKeyPressHandler = null;
    this._boundControlID = null;
    this._length =  '150px';
    this._raiseChangeOnlyOnMouseUp =  true;
    this._animationPending = false;
    this._selectstartPending = false;
    
    // Events
    //
    // sliderInitialized
    // valueChanged
    // slideStart
    // slideEnd
}
AjaxControlToolkit.SliderBehavior.prototype = {
    
    // initialize / dispose
    //
    initialize : function() {
        AjaxControlToolkit.SliderBehavior.callBaseMethod(this, 'initialize');
                
        this._initializeLayout();
    },
    
    dispose : function() {
        this._disposeHandlers();
        
        this._disposeBoundControl();
        
        if(this._enableHandleAnimation && this._handleAnimation) {
            this._handleAnimation.dispose();
        }
                
        AjaxControlToolkit.SliderBehavior.callBaseMethod(this, 'dispose');
    },
        
    // _initializeLayout
    // The slider's layout can be setup in 3 ways:
    // 1) Setting the RailCssClass and HandleCssClass properties.
    // 2) Setting the RailCssClass and HandleImageUrl properties.
    // 3) Using the default layout.
    //
    _initializeLayout : function() {
        // Element for the slider's rail.
        this._railElement = document.createElement('DIV');
        
        // Layout for the slider's handle.
        this._railElement.innerHTML = '<div></div>';
        this._handle = this._railElement.childNodes[0];
        this._handle.style.overflow = 'hidden';
        
        // Initialize left and top to 0px for Opera
        if(Sys.Browser.agent == Sys.Browser.Opera) {
            this._handle.style.left = '0px';
            this._handle.style.top = '0px';
        }
        
        // Replace the textbox with the graphical slider.
        var textBoxElement = this.get_element();
        var textBoxElementBounds = CommonToolkitScripts.getBounds(textBoxElement);
        
        textBoxElement.parentNode.insertBefore(this._railElement, textBoxElement);
        textBoxElement.style.display = 'none';
        
        // This is a dummy flag for saving keystrokes. 
        this._isHorizontal = (this._orientation == AjaxControlToolkit.SliderOrientation.Horizontal); 
        
        // Determine the kind of layout.
        var isUsingCSS = (this._railCssClass && this._handleCssClass);
        var isUsingHandleImage = (!isUsingCSS && this._railCssClass && this._handleImageUrl);
        
        if(isUsingCSS) {
            this._railElement.className = this._railCssClass;
            this._handle.className = this._handleCssClass;
            
            this._initializeSlider();
        }
        else if(isUsingHandleImage) {
            this._railElement.className = this._railCssClass;
            this._loadHandleImage();
        }
        else { 
            if (this._isHorizontal) {
                this._railElement.className = "ajax__slider_h_rail";
                this._handle.className = "ajax__slider_h_handle";
                this._railElement.style.width = this._length;
            } else {
                this._railElement.className = "ajax__slider_v_rail";
                this._handle.className = "ajax__slider_v_handle";
                this._railElement.style.height = this._length;
            }
            this._initializeSlider();
        }
        
        // Enforce positioning set via the associated textbox's style.
        this._enforceTextBoxElementPositioning();
    },
    
    // _enforceTextBoxElementPositioning
    // Enforce positioning set via the associated textbox's style.
    //
    _enforceTextBoxElementPositioning : function() {
        var tbPosition = 
            {
                position: this.get_element().style.position,
                top: this.get_element().style.top,
                right: this.get_element().style.right,
                bottom: this.get_element().style.bottom,
                left: this.get_element().style.left
            };
        
        if(tbPosition.position != '') {
           this._railElement.style.position = tbPosition.position;
        }
        if(tbPosition.top != '') {
            this._railElement.style.top = tbPosition.top;
        }
        if(tbPosition.right != '') {
           this._railElement.style.right = tbPosition.right;
        }
        if(tbPosition.bottom != '') {
           this._railElement.style.bottom = tbPosition.bottom;
        }
        if(tbPosition.left != '') {
            this._railElement.style.left = tbPosition.left;
        }
    },
    
    // _loadHandleImage
    // This method loads the image used for the handle.
    //
    _loadHandleImage : function() {
        this._handleImage = document.createElement('IMG');
        this._handle.appendChild(this._handleImage);
        $addHandler(this._handleImage, 'load', Function.createDelegate(this, this._onHandleImageLoaded));
        this._handleImage.src = this._handleImageUrl;
        this._handle.style.position = 'absolute'; 
    },
    
    // _onHandleImageLoaded
    // This method is called when the handle image is loaded.
    //
    _onHandleImageLoaded : function() {
        var handleImageBounds = CommonToolkitScripts.getBounds(this._handleImage);
        
        this._handle.style.height = handleImageBounds.height + 'px';
        this._handle.style.width = handleImageBounds.width + 'px';
                
        this._initializeSlider();
    },
    
    // _initializeSlider
    // This method initializes the slider control and is called after the 
    // layout has been setup.
    //
    _initializeSlider : function() {
        this._initializeBoundControl();
                
        // Check if a value is already set in the textbox.
        var _elementValue;
        try {
            _elementValue = parseFloat(this.get_element().value);
        } catch(ex) {
            _elementValue = Number.NaN;
        }
        
        this.set_Value(_elementValue);
        
        // Position the slider's handle to the current value.
        this._setHandleOffset(this._value);
                
        // Setup the invisible drag handle.          
        this._initializeDragHandle();
                                        
        // Register ourselves as a drop target.
        AjaxControlToolkit.SliderDragDropManagerInternal.registerDropTarget(this);
        
        this._initializeHandlers();
                
        this._initializeHandleAnimation();
                        
        this._isInitializedInternal = true;
        
        this._raiseEvent('sliderInitialized');
        
    },
    
    // _initializeBoundControl
    // Creates and initializes the control that is bound to the slider.
    //
    _initializeBoundControl : function() {
        if(this._boundControlID) {
            var boundElement = $get(this._boundControlID);
            var isInputElement = boundElement && boundElement.nodeName == 'INPUT';
            
            if(isInputElement) {
                this._boundControlChangeHandler = Function.createDelegate(this, this._onBoundControlChange);
                this._boundControlKeyPressHandler = Function.createDelegate(this, this._onBoundControlKeyPress);
                
                $addHandler(boundElement, 'change', this._boundControlChangeHandler);
                $addHandler(boundElement, 'keypress', this._boundControlKeyPressHandler);
            }
        }
    },
    
    _disposeBoundControl : function() {
        if(this._boundControlID) {
            var boundElement = $get(this._boundControlID);
            var isInputElement = boundElement && boundElement.nodeName == 'INPUT';
            
            if(isInputElement) {
                $removeHandler(boundElement, 'change', this._boundControlChangeHandler);
                $removeHandler(boundElement, 'keypress', this._boundControlKeyPressHandler);
            }
        }
    },
    
    _onBoundControlChange : function(evt) {
        this._animationPending = true;
        this._setValueFromBoundControl();
    },
    
    _onBoundControlKeyPress : function(evt) {
        if(evt.charCode == 13) {
            this._animationPending = true;
            this._setValueFromBoundControl();
            evt.preventDefault();
        }
    },
    
    _setValueFromBoundControl : function() {
        this._isUpdatingInternal = true;
        
        if(this._boundControlID) {
            this._calcValue($get(this._boundControlID).value);
        }
        
        this._isUpdatingInternal = false;
    },
   
    // _initializeHandleAnimation
    // Initializes the animation for the handle element.
    //
    _initializeHandleAnimation : function() {
        if(this._steps > 0) {
            this._enableHandleAnimation = false;
            return;
        }
        
        if(this._enableHandleAnimation) {
            this._handleAnimation = new AjaxControlToolkit.Animation.LengthAnimation(
                    this._handle, this._handleAnimationDuration, 100, 'style');
        }
    },
    
    // _ensureBinding
    //
    _ensureBinding : function() {
        if(this._boundControlID) {
            var value = this._value;
            
            if(value >= this._minimum || value <= this._maximum) {
                var boundElement = $get(this._boundControlID);
                var isInputElement = boundElement && boundElement.nodeName == 'INPUT';
                
                if(isInputElement) {
                    boundElement.value = value;
                }
                else if(boundElement) {
                    boundElement.innerHTML = value;
                }
            }
        }
    },
        
    // _getBoundsInternal
    // This function swaps the x and y coordinates to use the same logic 
    // for both the horizontal and vertical slider.
    //
    _getBoundsInternal : function(element) {
        var bounds = CommonToolkitScripts.getBounds(element);
        
        if(this._orientation == AjaxControlToolkit.SliderOrientation.Vertical) {
             bounds = { x : bounds.y, 
                        y : bounds.x, 
                        height : bounds.width, 
                        width : bounds.height, 
                        right : bounds.right,
                        bottom : bounds.bottom,
                        location : {x:bounds.y, y:bounds.x},
                        size : {width:bounds.height, height:bounds.width}
                      };
        }
        
        return bounds;
    },
    
    // _getRailBounds
    // This method returns the slider's rail bounds.
    //
    _getRailBounds : function() {
        var bounds = this._getBoundsInternal(this._railElement);
        
        return bounds;
    },
    
    // _getHandleBounds
    // This method returns the slider's handle bounds.
    //
    _getHandleBounds : function() {
        return this._getBoundsInternal(this._handle);
    },
    
    // _initializeDragHandle
    // Initializes the invisible drag handle used to obtain
    // an horizontal or vertical drag effect with the ASP.NET AJAX framework.
    //
    _initializeDragHandle : function() {
        var dh = this._dragHandle = document.createElement('DIV');
        
        dh.style.position = 'absolute';
        dh.style.width = '1px';
        dh.style.height = '1px';
        dh.style.overflow = 'hidden';
        dh.style.zIndex = '999';
        dh.style.background = 'none';
        
        document.body.appendChild(this._dragHandle);
    },
    
    // _resetDragHandle
    // This method is called to reset the invisible drag handle to its
    // default position.
    //
    _resetDragHandle : function() {
        var handleBounds = CommonToolkitScripts.getBounds(this._handle);
        
        CommonToolkitScripts.setLocation(this._dragHandle, {x:handleBounds.x, y:handleBounds.y});
    },
    
    // _initializeHandlers
    // This method creates the event handlers and attach them to the 
    // corresponding events.
    //
    _initializeHandlers : function() {
        this._selectstartHandler = Function.createDelegate(this, this._onSelectStart);
        this._mouseupHandler = Function.createDelegate(this, this._onMouseUp);
        
        $addHandler(document, 'mouseup', this._mouseupHandler);
                   
        $addHandlers(this._handle, 
            {
                'mousedown': this._onMouseDown,
                'dragstart': this._IEDragDropHandler,
                'drag': this._IEDragDropHandler,
                'dragend': this._IEDragDropHandler
            },
            this);
            
        $addHandlers(this._railElement,
            {
                'click': this._onRailClick
            },
            this);
    },
    
    _disposeHandlers : function() {
        $clearHandlers(this._handle);
        $clearHandlers(this._railElement);
        
        $removeHandler(document, 'mouseup', this._mouseupHandler);
        this._mouseupHandler = null;
        this._selectstartHandler = null;
    },
    
    // startDragDrop
    // Tells the DragDropManager to start a drag and drop operation.          
    //  
    startDragDrop : function(dragVisual) {
        this._resetDragHandle();
        
        AjaxControlToolkit.SliderDragDropManagerInternal.startDragDrop(this, dragVisual, null);
    },
    
    // _onMouseDown
    // Handles the slider's handle mousedown event.
    //
    _onMouseDown : function(evt) {
        window._event = evt;        
        evt.preventDefault();
        
        if(!AjaxControlToolkit.SliderBehavior.DropPending) {
            AjaxControlToolkit.SliderBehavior.DropPending = this;
            
            $addHandler(document, 'selectstart', this._selectstartHandler);
            this._selectstartPending = true;
            
            this.startDragDrop(this._dragHandle);
        }
    },
    
    // _onMouseUp
    // Handles the document's mouseup event.
    //
    _onMouseUp : function(evt) {
        var srcElement = evt.target;
        
        if(AjaxControlToolkit.SliderBehavior.DropPending == this) {
            AjaxControlToolkit.SliderBehavior.DropPending = null;
            
            if(this._selectstartPending) {
                $removeHandler(document, 'selectstart', this._selectstartHandler);
            }
        }
    },
    
    // _onRailClick
    // Handles the rail element's click event.
    _onRailClick : function(evt) {
        if(evt.target == this._railElement) {
            this._animationPending = true;
            this._onRailClicked(evt);    
        }
    },
    
    // _IEDragDropHandler
    // This method handles the drag events raised by Internet Explorer, 
    // preventing the default behaviors since we are using the 
    // GenericDragDropManager
    //
    _IEDragDropHandler : function(evt) {
        evt.preventDefault();
    },
    
    // _onSelectStart
    // Handles the document's selectstart event.
    //
    _onSelectStart : function(evt) {
        evt.preventDefault();
    },
    
    // _calcValue
    // This function computes the slider's value.
    //
    _calcValue : function(value, mouseOffset) {
        var val;
        
        if(value != null) {
            if(!Number.isInstanceOfType(value)) {
                try {
                    value = parseFloat(value);
                } catch(ex) {
                    value = Number.NaN;
                }
            }

            if(isNaN(value)) {
                value = this._minimum;
            }
            
            val = (value < this._minimum) ? this._minimum
                : (value > this._maximum) ? this._maximum
                : value;
        }
        else {                
            var _minimum = this._minimum;
            var _maximum = this._maximum;
            var handleBounds = this._getHandleBounds();
            var sliderBounds = this._getRailBounds();        
            var handleX = (mouseOffset) ? mouseOffset - handleBounds.width / 2 
                                        : handleBounds.x - sliderBounds.x;
            var extent = sliderBounds.width - handleBounds.width;
            var percent = handleX / extent;
            
            val = (handleX == 0) ? _minimum
                : (handleX == (sliderBounds.width - handleBounds.width)) ? _maximum
                : _minimum + percent * (_maximum - _minimum);
        }
        
        if(this._steps > 0) {
            val = this._getNearestStepValue(val);
        }
        
        val = (val < this._minimum) ? this._minimum
            : (val > this._maximum) ? this._maximum
            : val;
                
        this._isUpdatingInternal = true;
        this.set_Value(val);
        this._isUpdatingInternal = false;
        
        return val;
    },
       
    // _setHandleOffset
    // This function computes the handle's offset corresponding 
    // to a given value.   
    //
    _setHandleOffset : function(value, playHandleAnimation) {
        var _minimum = this._minimum;
        var _maximum = this._maximum;
        var handleBounds = this._getHandleBounds();
        var sliderBounds = this._getRailBounds();
        var Commons = AjaxControlToolkit.CommonToolkitScripts;
        
        // If the slider has a containing element with display:none, then the bounds returned will always
        // be zero. To work around that, we retrieve the width value (the only one we need) from the 
        // element's style. This implies that both the height and width CSS attributes of the rail 
        // and the handle must be set in order for the slider to work properly. The default layout
        // already sets them in the CSS resource file. 
        if(handleBounds.width <= 0 && sliderBounds.width <= 0) {
           handleBounds.width = parseInt(Commons.getCurrentStyle(this._handle, 'width'));
           sliderBounds.width = parseInt(Commons.getCurrentStyle(this._railElement, 'width'));
           
           if(handleBounds.width <= 0 || sliderBounds.width <= 0) {
               throw Error.argument('width', AjaxControlToolkit.Resources.Slider_NoSizeProvided); 
           }
        }

        var extent = _maximum - _minimum;
        var fraction = (value - _minimum) / extent;
        var hypOffset = Math.round(fraction * (sliderBounds.width - handleBounds.width));
        
        var offset = (value == _minimum) ? 0
                   : (value == _maximum) ? (sliderBounds.width - handleBounds.width)
                   : hypOffset;
        
        if(playHandleAnimation) {
            this._handleAnimation.set_startValue(handleBounds.x - sliderBounds.x);
            this._handleAnimation.set_endValue(offset);
            this._handleAnimation.set_propertyKey((this._isHorizontal) ? 'left' : 'top');
            this._handleAnimation.play();
            
            this._animationPending = false;
        }
        else {
            if(this._isHorizontal) {
                this._handle.style.left = offset + 'px';
            }
            else {
                this._handle.style.top = offset + 'px';
            }
        }
    },
 
    // _getNearestStepValue
    // This function computes the current value for a "discrete" slider.
    //
    _getNearestStepValue : function(value) {
        if(this._steps == 0) return value;
        
        var extent = this._maximum - this._minimum;
        if (extent == 0) return value;
        
        var delta = extent / (this._steps - 1);

        return Math.round(value / delta) * delta;
    },
    
    // _onHandleReleased
    // This function is invoked when the slider's handle is released.
    //
    _onHandleReleased : function() {
        if(this._raiseChangeOnlyOnMouseUp) {
            this._fireTextBoxChangeEvent();
        }
        
        this._raiseEvent('slideEnd');
    },
    
    // _onRailClicked
    // This function is invoked when the slider's rail is clicked.
    //
    _onRailClicked : function(evt) {
        // Compute the pointer's offset.
        var handleBounds = this._getHandleBounds();
        var sliderBounds = this._getRailBounds();
        var offset = (this._isHorizontal) ? evt.offsetX : evt.offsetY;
        var minOffset = handleBounds.width / 2;
        var maxOffset = sliderBounds.width - minOffset;
        
        offset = (offset < minOffset) ? minOffset 
               : (offset > maxOffset) ? maxOffset
               : offset;
         
        this._calcValue(null, offset, true);
        
        this._fireTextBoxChangeEvent();
    },
    
    // _fireTextBoxChangeEvent
    // Raise the change event on the underlying textbox when 
    // its value is updated programmatically.
    //
    _fireTextBoxChangeEvent : function() {
        if (document.createEvent) {
            var onchangeEvent = document.createEvent('HTMLEvents');
            onchangeEvent.initEvent('change', true, false);
            
            this.get_element().dispatchEvent(onchangeEvent);
        } 
        else if(document.createEventObject) {
            this.get_element().fireEvent('onchange');
        }
    },
        
    // IDragSource Members.
    //
    get_dragDataType : function() {        
        return 'HTML';
    },
    
    getDragData : function() {
        return this._handle;
    },
    
    get_dragMode : function() {       
        return AjaxControlToolkit.DragMode.Move;
    },
    
    onDragStart : function() {
        this._resetDragHandle();
        this._raiseEvent('slideStart');
    },
    
    onDrag : function() {
        var dragHandleBounds = this._getBoundsInternal(this._dragHandle);
        var handleBounds = this._getHandleBounds();
        var sliderBounds = this._getRailBounds();
        
        var handlePosition; 
        if(this._isHorizontal) {
            handlePosition = { x:dragHandleBounds.x - sliderBounds.x, y:0 };
        }
        else {
            handlePosition = { y:dragHandleBounds.x - sliderBounds.x, x:0 };
        }
        
        CommonToolkitScripts.setLocation(this._handle, handlePosition);
        
        this._calcValue(null, null);
        
        // If we have a discrete slider, correct the handle's position
        // based on the computed value.
        if(this._steps > 1) {
            this._setHandleOffset(this.get_Value(), false);
        }
    },
        
    onDragEnd : function() {
        this._onHandleReleased();
    },
    
    // IDropTarget members.
    //
    get_dropTargetElement : function() {
        return document.body;
    },
        
    canDrop : function(dragMode, dataType) {
        return dataType == 'HTML';
    },
    
    drop : Function.emptyMethod,
    
    onDragEnterTarget : Function.emptyMethod,
    
    onDragLeaveTarget : Function.emptyMethod,
    
    onDragInTarget : Function.emptyMethod,
    
    // Events
    //
    add_sliderInitialized : function(handler) {
        this.get_events().addHandler('sliderInitialized', handler);
    },
    
    remove_sliderInitialized : function(handler) {
        this.get_events().removeHandler('sliderInitialized', handler);
    },
    
    add_valueChanged : function(handler) {
        this.get_events().addHandler('valueChanged', handler);
    },
    
    remove_valueChanged : function(handler) {
        this.get_events().removeHandler('valueChanged', handler);
    },
    
    add_slideStart : function(handler) {
        this.get_events().addHandler('slideStart', handler);
    },
    
    remove_slideStart : function(handler) {
        this.get_events().removeHandler('slideStart', handler);
    },
    
    add_slideEnd : function(handler) {
        this.get_events().addHandler('slideEnd', handler);
    },
    
    remove_slideEnd : function(handler) {
        this.get_events().removeHandler('slideEnd', handler);
    },
    
    _raiseEvent : function(eventName, eventArgs) {
        var handler = this.get_events().getHandler(eventName);
        if (handler) {
         if (!eventArgs) {
            eventArgs = Sys.EventArgs.Empty;
         }
         handler(this, eventArgs);
        }
    },
    
    // Properties.
    //
    get_Value : function() {
        return this._value;
    },
    
    set_Value : function(value) {
        var oldValue = this._value;
        var newValue = value;
        
        if(!this._isUpdatingInternal) {
            newValue = this._calcValue(value);
        }
        
        this.get_element().value = this._value = newValue.toFixed(this._decimals);

        this._ensureBinding();
                
        if(!Number.isInstanceOfType(this._value)) {
            try {
                this._value = parseFloat(this._value);
            } catch(ex) {
                this._value = Number.NaN;
            }
        }

        if(this._isInitializedInternal) {
            this._setHandleOffset(newValue, this._enableHandleAnimation && this._animationPending);
                
            if(this._isUpdatingInternal) {
                if(!this._raiseChangeOnlyOnMouseUp) {
                    this._fireTextBoxChangeEvent();
                }
            }
            
            if(this._value != oldValue) {
                this._raiseEvent('valueChanged');
            }
        }
    },

    get_RailCssClass : function() {
        return this._railCssClass;
    },
    
    set_RailCssClass : function(value) {
        this._railCssClass = value;
    },  
    
    get_HandleImageUrl : function() {
        return this._handleImageUrl;
    },
    
    set_HandleImageUrl : function(value) {
        this._handleImageUrl = value;
    },
    
    get_HandleCssClass : function() {
        return this._handleCssClass;
    },
    
    set_HandleCssClass : function(value) {
        this._handleCssClass = value;
    },
    
    get_Minimum : function() {
        return this._minimum;
    },
    
    set_Minimum : function(value) {
        this._minimum = value;
    }, 
    
    get_Maximum : function() {
        return this._maximum;
    },
    
    set_Maximum : function(value) {
        this._maximum = value;
    },
    
    get_Orientation : function() {
        return this._orientation;
    },
    
    set_Orientation : function(value) {
        this._orientation = value;
    },
    
    get_Steps : function() {
        return this._steps;
    },
    
    set_Steps : function(value) {
        this._steps = Math.abs(value);
        this._steps = (this._steps == 1) ? 2 : this._steps;
    },
    
    get_Decimals : function() {
        return this._decimals;
    },
    
    set_Decimals : function(value) {
        this._decimals = Math.abs(value);
    },
    
    get_EnableHandleAnimation : function() {
        return this._enableHandleAnimation;
    },
    
    set_EnableHandleAnimation : function(value) {
        this._enableHandleAnimation = value;
    },
    
    get_HandleAnimationDuration : function() {
        return this._handleAnimationDuration;
    },
    
    set_HandleAnimationDuration : function(value) {
        this._handleAnimationDuration = value;
    }, 
      
    get_BoundControlID : function() {
        return this._boundControlID;
    },
    
    set_BoundControlID : function(value) {
        this._boundControlID = value;
    },
    
    get_Length : function() {
        return this._length;
    },
    
    set_Length : function(value) {
        this._length = value + 'px';
    },
    
    get_SliderInitialized : function() {
        return this._isInitializedInternal;
    },
    
    get_RaiseChangeOnlyOnMouseUp : function() {
        return this._raiseChangeOnlyOnMouseUp;
    },
    
    set_RaiseChangeOnlyOnMouseUp : function(value) {
        this._raiseChangeOnlyOnMouseUp = value;
    },
        
    // These are helper functions for communicating state back to the extender on the
    // server side.  They take or return a custom string that is available in your initialize method
    // and later.
    //
    getClientState : function() {
        var value = AjaxControlToolkit.SliderBehavior.callBaseMethod(this, 'get_ClientState');                
        if (value == '') value = null;
        return value;
    },
     
    setClientState : function(value) {
        return AjaxControlToolkit.SliderBehavior.callBaseMethod(this, 'set_ClientState',[value]);                
    }
}

AjaxControlToolkit.SliderBehavior.DropPending = null; // Global, used to work around an issue when using the GenericDragDropManager in IE.

AjaxControlToolkit.SliderBehavior.registerClass('AjaxControlToolkit.SliderBehavior', AjaxControlToolkit.BehaviorBase, AjaxControlToolkit.IDragSource, AjaxControlToolkit.IDropTarget);
