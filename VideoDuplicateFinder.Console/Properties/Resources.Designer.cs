﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoDuplicateFinderConsole.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VideoDuplicateFinderConsole.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more of your provided arguments are invalid: {0}.
        /// </summary>
        internal static string Cmd_InvalidArgs {
            get {
                return ResourceManager.GetString("Cmd_InvalidArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is an unknown command.
        /// </summary>
        internal static string Cmd_UnknownCommand {
            get {
                return ResourceManager.GetString("Cmd_UnknownCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Boolean.
        /// </summary>
        internal static string CmdBoolean {
            get {
                return ResourceManager.GetString("CmdBoolean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cleanup database.
        /// </summary>
        internal static string CmdDescription_Clean {
            get {
                return ResourceManager.GetString("CmdDescription_Clean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to path to the folder to exclude in scan.
        /// </summary>
        internal static string CmdDescription_ExcludeFolder {
            get {
                return ResourceManager.GetString("CmdDescription_ExcludeFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to path to the folder to include for scan.
        /// </summary>
        internal static string CmdDescription_IncludeFolder {
            get {
                return ResourceManager.GetString("CmdDescription_IncludeFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to include images.
        /// </summary>
        internal static string CmdDescription_IncludeImages {
            get {
                return ResourceManager.GetString("CmdDescription_IncludeImages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If given, output will be printed as JSON to stdout.
        /// </summary>
        internal static string CmdDescription_json {
            get {
                return ResourceManager.GetString("CmdDescription_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path where the output html file will be saved.
        /// </summary>
        internal static string CmdDescription_Output {
            get {
                return ResourceManager.GetString("CmdDescription_Output", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Percent of similarity when a video is considered as duplicate, range 0-100.
        /// </summary>
        internal static string CmdDescription_Percent {
            get {
                return ResourceManager.GetString("CmdDescription_Percent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If quiet the scan progress will not be shown.
        /// </summary>
        internal static string CmdDescription_Quiet {
            get {
                return ResourceManager.GetString("CmdDescription_Quiet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Include all sub folders.
        /// </summary>
        internal static string CmdDescription_Recursive {
            get {
                return ResourceManager.GetString("CmdDescription_Recursive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find {0}. Please verify its installed and PATH variable is set..
        /// </summary>
        internal static string CmdException_FFprobeMissingLinux {
            get {
                return ResourceManager.GetString("CmdException_FFprobeMissingLinux", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find {0}.exe. Please download latest version and place it in the same directory of this application..
        /// </summary>
        internal static string CmdException_FFprobeMissingWindows {
            get {
                return ResourceManager.GetString("CmdException_FFprobeMissingWindows", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot parse {0}.
        /// </summary>
        internal static string CmdException_InvalidArg {
            get {
                return ResourceManager.GetString("CmdException_InvalidArg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing arguments.
        /// </summary>
        internal static string CmdException_MissingArgs {
            get {
                return ResourceManager.GetString("CmdException_MissingArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing exclude folder.
        /// </summary>
        internal static string CmdException_MissingExcludePath {
            get {
                return ResourceManager.GetString("CmdException_MissingExcludePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing include folder.
        /// </summary>
        internal static string CmdException_MissingIncludePath {
            get {
                return ResourceManager.GetString("CmdException_MissingIncludePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing output folder.
        /// </summary>
        internal static string CmdException_MissingOutputPath {
            get {
                return ResourceManager.GetString("CmdException_MissingOutputPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing percent value.
        /// </summary>
        internal static string CmdException_MissingPercent {
            get {
                return ResourceManager.GetString("CmdException_MissingPercent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target path does not exist: {0}.
        /// </summary>
        internal static string CmdException_PathNotExist {
            get {
                return ResourceManager.GetString("CmdException_PathNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown exception.
        /// </summary>
        internal static string CmdException_Unknown {
            get {
                return ResourceManager.GetString("CmdException_Unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Float.
        /// </summary>
        internal static string CmdFloat {
            get {
                return ResourceManager.GetString("CmdFloat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path.
        /// </summary>
        internal static string CmdPath {
            get {
                return ResourceManager.GetString("CmdPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available commands:.
        /// </summary>
        internal static string CmdUsageHeader {
            get {
                return ResourceManager.GetString("CmdUsageHeader", resourceCulture);
            }
        }
    }
}
