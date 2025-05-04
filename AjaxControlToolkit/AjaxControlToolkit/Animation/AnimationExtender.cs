// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI;
using System.ComponentModel;

[assembly: WebResource("AjaxControlToolkit.Animation.AnimationBehavior.js", "text/javascript")]

namespace AjaxControlToolkit
{
    /// <summary>
    /// Extender used to play animations when a control's events fire
    /// </summary>
    [Designer("AjaxControlToolkit.AnimationExtenderDesigner, AjaxControlToolkit")]
    [RequiredScript(typeof(AnimationScripts))]
    [ClientScriptResource("AjaxControlToolkit.Animation.AnimationBehavior", "AjaxControlToolkit.Animation.AnimationBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class AnimationExtender : AnimationExtenderControlBase
    {
        // OnLoad Animation
        private Animation _onLoad;

        // OnClick Animation
        private Animation _onClick;

        // OnMouseOver Animation
        private Animation _onMouseOver;

        // OnMouseOut Animation
        private Animation _onMouseOut;

        // OnHoverOver Animation
        private Animation _onHoverOver;

        // OnHoverOut Animation
        private Animation _onHoverOut;

        /// <summary>
        /// OnLoad Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public new Animation OnLoad
        {
            get { return GetAnimation(ref _onLoad, "OnLoad"); }
            set { SetAnimation(ref _onLoad, "OnLoad", value); }
        }

        /// <summary>
        /// Don't serialize OnLoad in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnLoad()
        {
            return !DesignMode;
        }

        /// <summary>
        /// OnClick Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public Animation OnClick
        {
            get { return GetAnimation(ref _onClick, "OnClick"); }
            set { SetAnimation(ref _onClick, "OnClick", value); }
        }

        /// <summary>
        /// Don't serialize OnClick in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnClick()
        {
            return !DesignMode;
        }

        /// <summary>
        /// OnMouseOver Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public Animation OnMouseOver
        {
            get { return GetAnimation(ref _onMouseOver, "OnMouseOver"); }
            set { SetAnimation(ref _onMouseOver, "OnMouseOver", value); }
        }

        /// <summary>
        /// Don't serialize OnMouseOver in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnMouseOver()
        {
            return !DesignMode;
        }

        /// <summary>
        /// OnMouseOut Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public Animation OnMouseOut
        {
            get { return GetAnimation(ref _onMouseOut, "OnMouseOut"); }
            set { SetAnimation(ref _onMouseOut, "OnMouseOut", value); }
        }

        /// <summary>
        /// Don't serialize OnMouseOut in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnMouseOut()
        {
            return !DesignMode;
        }

        /// <summary>
        /// OnHoverOver Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public Animation OnHoverOver
        {
            get { return GetAnimation(ref _onHoverOver, "OnHoverOver"); }
            set { SetAnimation(ref _onHoverOver, "OnHoverOver", value); }
        }

        /// <summary>
        /// Don't serialize OnHoverOver in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnHoverOver()
        {
            return !DesignMode;
        }

        /// <summary>
        /// OnHoverOut Animation
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [ExtenderControlProperty]
        public Animation OnHoverOut
        {
            get { return GetAnimation(ref _onHoverOut, "OnHoverOut"); }
            set { SetAnimation(ref _onHoverOut, "OnHoverOut", value); }
        }

        /// <summary>
        /// Don't serialize OnHoverOut in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnHoverOut()
        {
            return !DesignMode;
        }

        /// <summary>
        /// Change any AnimationTarget references from server control IDs into the ClientIDs
        /// that the animation scripts are expecting.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ResolveControlIDs(_onLoad);
            ResolveControlIDs(_onClick);
            ResolveControlIDs(_onMouseOut);
            ResolveControlIDs(_onMouseOut);
            ResolveControlIDs(_onHoverOut);
            ResolveControlIDs(_onHoverOut);
        }
    }
}
