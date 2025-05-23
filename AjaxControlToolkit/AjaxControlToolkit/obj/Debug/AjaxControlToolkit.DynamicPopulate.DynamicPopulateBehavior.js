// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

Type.registerNamespace('AjaxControlToolkit');

AjaxControlToolkit.DynamicPopulateBehavior = function(element) {
    /// <summary>
    /// The DynamicPopulateBehavior replaces the contents of an element with the result of a web service or page method call.  The method call returns a string of HTML that is inserted as the children of the target element.
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// DOM Element the behavior is associated with
    /// </param>
    AjaxControlToolkit.DynamicPopulateBehavior.initializeBase(this, [element]);
    
    this._servicePath = null;
    this._serviceMethod = null;
    this._contextKey = null;
    this._populateTriggerID = null;
    this._setUpdatingCssClass = null;
    this._clearDuringUpdate = true;
    this._customScript = null;
    
    this._clickHandler = null;
    
    this._callID = 0;
    this._currentCallID = -1;
}
AjaxControlToolkit.DynamicPopulateBehavior.prototype = {
    initialize : function() {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        AjaxControlToolkit.DynamicPopulateBehavior.callBaseMethod(this, 'initialize');
    
        // hook up the trigger if we have one.
        if (this._populateTriggerID) {
            var populateTrigger = $get(this._populateTriggerID);
            if (populateTrigger) {
                this._clickHandler = Function.createDelegate(this, this._onPopulateTriggerClick);
                $addHandler(populateTrigger, "click", this._clickHandler);
            }
        }
    },
    
    dispose : function() {
        /// <summary>
        /// Dispose the behavior
        /// </summary>

        // clean up the trigger event.
        if (this._populateTriggerID && this._clickHandler) {
            var populateTrigger = $get(this._populateTriggerID);
            if (populateTrigger) {
                $removeHandler(populateTrigger, "click", this._clickHandler);
            }
            this._populateTriggerID = null;
            this._clickHandler = null;
        }
       
        AjaxControlToolkit.DynamicPopulateBehavior.callBaseMethod(this, 'dispose');
    },
    
    add_populated : function(handler) {
        /// <summary>
        /// Add a handler on the populated event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
        this.get_events().addHandler("populated", handler);
    },
    remove_populated : function(handler) {
        /// <summary>
        /// Remove a handler from the populated event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
        this.get_events().removeHandler("populated", handler);
    },
    raisePopulated : function(arg) {
        /// <summary>
        /// Raise the populated event
        /// </summary>
        /// <param name="arg" type="Sys.EventArgs">
        /// Event arguments
        /// </param>
        var handler = this.get_events().getHandler("populated");  
        if (handler) handler(this, arg);
    },
    
    add_populating : function(handler) {
        /// <summary>
        /// Add a handler on the populating event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
        this.get_events().addHandler("populating", handler);
    },
    remove_populating : function(handler) {
        /// <summary>
        /// Remove a handler from the populating event
        /// </summary>
        /// <param name="handler" type="Function">
        /// Handler
        /// </param>
        this.get_events().removeHandler("populating", handler);
    },
    raisePopulating : function(arg) {
        /// <summary>
        /// Raise the populating event
        /// </summary>
        /// <param name="arg" type="Sys.EventArgs">
        /// Event arguments
        /// </param>
        var handler = this.get_events().getHandler("populating");
        if (handler) handler(this, arg);
    },
    
    populate : function(contextKey) {
        /// <summary>
        /// Get the dymanic content and use it to populate the target element
        /// </summary>
        /// <param name="contextKey" type="String" mayBeNull="true" optional="true">
        /// An arbitrary string value to be passed to the web method. For example, if the element to be
        /// populated is within a data-bound repeater, this could be the ID of the current row.
        /// </param>

        if (this._currentCallID == -1) {
            this._setUpdating(true);   
        }        
        if (this._customScript) {
            // Call custom javascript call to populate control
            var scriptResult = eval(this._customScript);
            this.get_element().innerHTML = scriptResult; 
            this._setUpdating(false);
         } else {
             this._currentCallID = ++this._callID;
             if (this._servicePath && this._serviceMethod) {

                Sys.Net.WebServiceProxy.invoke(this._servicePath, this._serviceMethod, false,
                    { contextKey:(contextKey ? contextKey : this._contextKey) },
                    Function.createDelegate(this, this._onMethodComplete), Function.createDelegate(this, this._onMethodError),
                    this._currentCallID);
             }
        }
    },

    _onMethodComplete : function (result, userContext, methodName) {

        // ignore if it's not the current call.
        if (userContext != this._currentCallID) return;

        // Time has passed; make sure the element is still accessible
        var e = this.get_element();
        if (e) {
            e.innerHTML = result;
        }

        this._setUpdating(false);
    },

    _onMethodError : function(webServiceError, userContext, methodName) {

        // ignore if it's not the current call.
        if (userContext != this._currentCallID) return;

        var e = this.get_element();
        if (e) {
            if (webServiceError.get_timedOut()) {
                e.innerHTML = AjaxControlToolkit.Resources.DynamicPopulate_WebServiceTimeout;
            } else {
                e.innerHTML = String.format(AjaxControlToolkit.Resources.DynamicPopulate_WebServiceError, webServiceError.get_statusCode());
            }
        }

        this._setUpdating(false);
    },

    _onPopulateTriggerClick : function() {
        /// <summary>
        /// Handler for the element described by PopulateTriggerID's click event
        /// </summary>

        // just call through to the trigger.
        this.populate(this._contextKey);
    },
    
    _setUpdating : function(updating) {
        /// <summary>
        /// Toggle the display elements to indicate if they are being updated or not
        /// </summary>
        /// <param name="updating" type="Boolean">
        /// Whether or not the display should indicated it is being updated
        /// </param>

        var e = this.get_element();
        if (this._setUpdatingCssClass) {               
            if (!updating) {
                e.className = this._oldCss;              
                this._oldCss = null;
            } else {
                this._oldCss = e.className;                
                e.className = this._setUpdatingCssClass;      
            }
        }
        
        if (updating && this._clearDuringUpdate) {
            e.innerHTML = "";
        }
        
        if (updating) {
            this.raisePopulating(this, Sys.EventArgs.Empty);
        } else {
            this._currentCallID = -1;
            this.raisePopulated(this, Sys.EventArgs.Empty);
        }
    },
    
    get_ClearContentsDuringUpdate : function() {
        /// <value type="Boolean">
        /// Whether the contents of the target should be cleared when an update begins
        /// </value>
        return this._clearDuringUpdate;
    },
    set_ClearContentsDuringUpdate : function(value) {
        if (this._clearDuringUpdate != value) {
            this._clearDuringUpdate = value;
            this.raisePropertyChanged('ClearContentsDuringUpdate');
        }
    },
    
    get_ContextKey : function() {
        /// <value type="String">
        /// An arbitrary string value to be passed to the web method.
        /// For example, if the element to be populated is within a
        /// data-bound repeater, this could be the ID of the current row.
        /// </value>
        return this._contextKey;
    },
    set_ContextKey : function(value) {
        if (this._contextKey != value) {
            this._contextKey = value;
            this.raisePropertyChanged('ContextKey');
        }
    },
    
    get_PopulateTriggerID : function() {
        /// <value type="String" mayBeNull="true" optional="true">
        /// Name of an element that triggers the population of the target when clicked
        /// </value>
        return this._populateTriggerID;
    },
    set_PopulateTriggerID : function(value) {
        if (this._populateTriggerID != value) {
            this._populateTriggerID = value;
            this.raisePropertyChanged('PopulateTriggerID');
        }
    },
    
    get_ServicePath : function() {
        /// <value type="String" mayBeNull="true" optional="true">
        /// The URL of the web service to call.  If the ServicePath is not defined, then we will invoke a PageMethod instead of a web service.
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
        /// The name of the method to call on the page or web service
        /// </value>
        /// <remarks>
        /// The signature of the method must exactly match the following:
        ///    [WebMethod]
        ///    string DynamicPopulateMethod(string contextKey)
        ///    {
        ///        ...
        ///    }
        /// </remarks>
        return this._serviceMethod;
    },
    set_ServiceMethod : function(value) {
        if (this._serviceMethod != value) {
            this._serviceMethod = value;
            this.raisePropertyChanged('ServiceMethod');
        }
    },
    
    get_UpdatingCssClass : function() {
        /// <value type="String">
        /// The CSS class to apply to the target during asynchronous calls
        /// </value>
        return this._setUpdatingCssClass;
    },
    set_UpdatingCssClass : function(value) {
        if (this._setUpdatingCssClass != value) {
            this._setUpdatingCssClass = value;
            this.raisePropertyChanged('UpdatingCssClass');
        }
    },
    
    get_CustomScript : function() {
        /// <value type="String">
        /// The script to invoke instead of calling a Web or Page method. This script must evaluate to a string value.
        /// </value>
        return this._customScript;
    },   
    set_CustomScript : function(value) {
        if (this._customScript != value) {
            this._customScript = value;
            this.raisePropertyChanged('CustomScript');
        }
    }
}
AjaxControlToolkit.DynamicPopulateBehavior.registerClass('AjaxControlToolkit.DynamicPopulateBehavior', AjaxControlToolkit.BehaviorBase);
//    getDescriptor : function() {
//        var td = AjaxControlToolkit.DynamicPopulateBehavior.callBaseMethod(this, 'getDescriptor');
//        td.addProperty('ServicePath', String);        
//        td.addProperty('ServiceMethod', String);        
//        td.addProperty('ContextKey', String);        
//        td.addProperty('PopulateTriggerID', String);        
//        td.addProperty('UpdatingCssClass', String);        
//        td.addProperty('ClearContentsDuringUpdate', Boolean);   
//        td.addProperty('CustomScript', String);       
//        return td;
//    },
