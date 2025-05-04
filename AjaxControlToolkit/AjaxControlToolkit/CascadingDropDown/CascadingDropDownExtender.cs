// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.CascadingDropDown.CascadingDropDownBehavior.js", "text/javascript")]
#endregion

namespace AjaxControlToolkit
{    
    /// <summary>
    /// CascadingDropDown extender class definition
    /// </summary>
    [Designer("AjaxControlToolkit.CascadingDropDownDesigner, AjaxControlToolkit")]
    [ClientScriptResource("AjaxControlToolkit.CascadingDropDownBehavior", "AjaxControlToolkit.CascadingDropDown.CascadingDropDownBehavior.js")]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(BlockingScripts))]
    [TargetControlType(typeof(DropDownList))]
    public class CascadingDropDown : ExtenderControlBase
    {
        public CascadingDropDown()
        {
            ClientStateValuesLoaded += new EventHandler(CascadingDropDown_ClientStateValuesLoaded);
            EnableClientState = true;
        }

        // Constant strings for each property name
        private const string stringParentControlID = "ParentControlID";
        private const string stringCategory = "Category";
        private const string stringPromptText = "PromptText";
        private const string stringLoadingText = "LoadingText";
        private const string stringServicePath = "ServicePath";
        private const string stringServiceMethod = "ServiceMethod";
       
        /// <summary>
        /// Optional ID of the parent DropDownList (upon which the contents of this control are based)
        /// </summary>
        [IDReferenceProperty(typeof(DropDownList))]
        [DefaultValue("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        [ExtenderControlProperty()]
        public string ParentControlID
        {
            get
            {
                return GetPropertyValue(stringParentControlID, "");
            }
            set
            {
                SetPropertyValue(stringParentControlID, value);
            }
        }

        /// <summary>
        /// Category of this DropDownList (used when communicating with the helper web service)
        /// </summary>
        [RequiredProperty()]
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string Category
        {
            get
            {
                return GetPropertyValue(stringCategory, "");
            }
            set
            {
                SetPropertyValue(stringCategory, value);
            }
        }

        /// <summary>
        /// Optional text displayed by a DropDownList the user has not yet touched
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string PromptText
        {
            get
            {
                return GetPropertyValue(stringPromptText, "");
            }
            set
            {
                SetPropertyValue(stringPromptText, value);
            }
        }

        /// <summary>
        /// Optional text displayed by a DropDownList when it is loading its data
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string LoadingText
        {
            get
            {
                return GetPropertyValue(stringLoadingText, "");
            }
            set
            {
                SetPropertyValue(stringLoadingText, value);
            }
        }

        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string SelectedValue
        {
            get
            {
                return ClientState ?? "";
            }
            set
            {
                ClientState = value;
            }
        }

        /// <summary>
        /// Path to the helper web service
        /// </summary>        
        [UrlProperty()]
        [ExtenderControlProperty()]
        [TypeConverter(typeof(ServicePathConverter))]
        public string ServicePath
        {
            get
            {
                return GetPropertyValue(stringServicePath, "");
            }
            set
            {
                SetPropertyValue(stringServicePath, value);
            }
        }

        private bool ShouldSerializeServicePath() {
            return !string.IsNullOrEmpty(ServiceMethod);
        }

        // Name of the helper web service method
        [RequiredProperty()]
        [DefaultValue("")]
        [ExtenderControlProperty()]
        public string ServiceMethod
        {
            get
            {
                return GetPropertyValue(stringServiceMethod, "");
            }
            set
            {
                SetPropertyValue(stringServiceMethod, value);
            }
        }

        /// <summary>
        /// Populate DropDownLists with their SelectedValues
        /// </summary>
        void CascadingDropDown_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            
            DropDownList dropDownList = (DropDownList)TargetControl;
            dropDownList.Items.Clear();
            string separator = ":::";
            string clientState = base.ClientState;
            int separatorIndex = (clientState ?? "").IndexOf(separator);
            if (-1 == separatorIndex)
            {
                // ClientState is the value to set
                dropDownList.Items.Add(clientState);
            }
            else
            {
                // Parse the value/text out of ClientState and set them
                string value = clientState.Substring(0, separatorIndex);
                string text = clientState.Substring(separatorIndex + separator.Length);
                dropDownList.Items.Add(new ListItem(text, value));
            }        
        }

        /// <summary>
        /// Helper method to parse the private storage format used to communicate known category/value pairs
        /// </summary>
        /// <param name="knownCategoryValues">private storage format string</param>
        /// <returns>dictionary of category/value pairs</returns>
        public static StringDictionary ParseKnownCategoryValuesString(string knownCategoryValues)
        {
            // Validate parameters
            if (null == knownCategoryValues)
            {
                throw new ArgumentNullException("knownCategoryValues");
            }

            StringDictionary dictionary = new StringDictionary();
            if (null != knownCategoryValues)
            {
                // Split into category/value pairs
                foreach (string knownCategoryValue in knownCategoryValues.Split(';'))
                {
                    // Split into category and value
                    string[] knownCategoryValuePair = knownCategoryValue.Split(':');
                    if (2 == knownCategoryValuePair.Length)
                    {
                        // Add the pair to the dictionary
                        dictionary.Add(knownCategoryValuePair[0].ToLowerInvariant(), knownCategoryValuePair[1]);
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Helper method to provide a simple implementation of a method to query a data set and return the relevant drop down contents
        /// </summary>
        /// <param name="document">XML document containing the data set</param>
        /// <param name="documentHierarchy">list of strings representing the hierarchy of the data set</param>
        /// <param name="knownCategoryValuesDictionary">known category/value pairs</param>
        /// <param name="category">category for which the drop down contents are desired</param>
        /// <returns>contents of the specified drop down subject to the choices already made</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", Justification = "Non-IXPathNavigable members of XmlDocument are used")]
        public static CascadingDropDownNameValue[] QuerySimpleCascadingDropDownDocument(XmlDocument document, string[] documentHierarchy, StringDictionary knownCategoryValuesDictionary, string category)
        {
            // Validate parameters
            if (null == document)
            {
                throw new ArgumentNullException("document");
            }
            if (null == documentHierarchy)
            {
                throw new ArgumentNullException("documentHierarchy");
            }
            if (null == knownCategoryValuesDictionary)
            {
                throw new ArgumentNullException("knownCategoryValuesDictionary");
            }
            if (null == category)
            {
                throw new ArgumentNullException("category");
            }

            // Root the XPath query
            string xpath = "/" + document.DocumentElement.Name;

            // Build an XPath query into the data set to select the relevant items
            foreach (string key in documentHierarchy)
            {
                if (knownCategoryValuesDictionary.ContainsKey(key))
                {
                    xpath += string.Format(CultureInfo.InvariantCulture, "/{0}[(@name and @value='{1}') or (@name='{1}' and not(@value))]", key, knownCategoryValuesDictionary[key]);
                }
            }
            xpath += ("/" + category.ToLowerInvariant());

            // Perform the XPath query and add the results to the list
            List<CascadingDropDownNameValue> result = new List<CascadingDropDownNameValue>();
            foreach (XmlNode node in document.SelectNodes(xpath))
            {
                string name = node.Attributes.GetNamedItem("name").Value;
                XmlNode valueNode = node.Attributes.GetNamedItem("value");
                string value = ((null != valueNode) ? valueNode.Value : name);
                result.Add(new CascadingDropDownNameValue(name, value));
            }

            // Return the list
            return result.ToArray();
        }
    }
}
