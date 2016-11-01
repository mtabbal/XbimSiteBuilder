﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Xbim.SiteBuilder.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class Layout : LayoutBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name=""description"" content=""xBIM Toolkit is an Open Source toolkit for IFC and BIM. You can use it to do anything with IFC. It implements IFC2x3 and IFC4 and allows to develop SW agnostic to IFC version and file format."">
    <meta name=""author"" content=""xBIM Team"">
    <link rel=""icon"" href=""/img/favicon.ico"">

    <title>");
            
            #line 17 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.Title ?? ContentNode.Title));
            
            #line default
            #line hidden
            this.Write(@"</title>

    <!-- Bootstrap core CSS -->
    <link href=""/css/bootstrap.min.css"" rel=""stylesheet"">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src=""https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js""></script>
      <script src=""https://oss.maxcdn.com/respond/1.4.2/respond.min.js""></script>
    <![endif]-->

    <!-- Custom styles for this template -->
    <link href=""/css/codestyles.css"" rel=""stylesheet"">
    <link href=""/css/site.css"" rel=""stylesheet"">

    <script src=""/js/jquery.min.js""></script>

  </head>
<!-- NAVBAR
================================================== -->
  <body>

    <nav class=""navbar navbar-inverse"">
      <div class=""container"">
        <div class=""navbar-header"">
          <button type=""button"" class=""navbar-toggle collapsed"" data-toggle=""collapse"" data-target=""#navbar"" aria-expanded=""false"" aria-controls=""navbar"">
            <span class=""sr-only"">Toggle navigation</span>
            <span class=""icon-bar""></span>
            <span class=""icon-bar""></span>
            <span class=""icon-bar""></span>
          </button>
          <a class=""navbar-brand"" href=""/index.html"">xBIM</a>
        </div>
        <div id=""navbar"" class=""collapse navbar-collapse"">
          ");
            
            #line 51 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 
		  var navTempl = new Navigation(NavigationRoot, true, ContentNode);
		   
            
            #line default
            #line hidden
            this.Write("\t\t   ");
            
            #line 54 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(navTempl.TransformText()));
            
            #line default
            #line hidden
            this.Write("\r\n        </div><!--/.nav-collapse -->\r\n      </div>\r\n    </nav>\r\n");
            
            #line 58 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 if (Settings.ShowBanner) { 
            
            #line default
            #line hidden
            this.Write("\t<div class=\"banner architecture\">\r\n\t\t");
            
            #line 60 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.BannerContent));
            
            #line default
            #line hidden
            this.Write("\r\n\t</div>\r\n");
            
            #line 62 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 64 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 if(Settings.UseContainer) {
            
            #line default
            #line hidden
            this.Write("\t<div class=\"container\">\r\n\t");
            
            #line 66 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\t<div>\r\n\t");
            
            #line 68 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
 } 
            
            #line default
            #line hidden
            
            #line 69 "C:\Users\Martin\Source\Repos\XbimSiteBuilder\Xbim.SiteBuilder\Templates\Layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Content));
            
            #line default
            #line hidden
            this.Write(@"

    </div>
    

	<div class=""brands-container"">
	<p><small>Some of the companies using xBIM Toolkit:</small></p>
		<img src=""/img/logos/viewpoint_logo_gray.png"" class=""brand""/>
		<img src=""/img/logos/welplan_logo_gray.png"" class=""brand""/>
		<img src=""/img/logos/nbs_logo_gray.png"" class=""brand""/>
	</div>
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src=""/js/bootstrap.min.js""></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src=""/js/ie10-viewport-bug-workaround.js""></script>
  </body>
</html>
");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class LayoutBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
