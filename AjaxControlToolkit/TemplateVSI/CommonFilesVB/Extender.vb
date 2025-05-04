Imports System
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports AjaxControlToolkit

#Region "Assembly Resource Attribute"
<Assembly: System.Web.UI.WebResource("$rootnamespace$.$baseitemname$Behavior.js", "text/javascript")> 
#End Region

Namespace $rootnamespace$

    <Designer(GetType($baseitemname$Designer))> _
    <ClientScriptResource("$rootnamespace$.$baseitemname$Behavior", "$rootnamespace$.$baseitemname$Behavior.js")> _
    <TargetControlType(GetType(Control))> _
    Public Class $baseitemname$Extender 
    Inherits ExtenderControlBase

    ' TODO: Add your property accessors here.
    <ExtenderControlProperty()> _
    <DefaultValue("")> _
    Public Property MyProperty() As String
        Get
            Return GetPropertyValue("MyProperty", "")
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MyProperty", value)
        End Set
    End Property

    End Class

End Namespace
