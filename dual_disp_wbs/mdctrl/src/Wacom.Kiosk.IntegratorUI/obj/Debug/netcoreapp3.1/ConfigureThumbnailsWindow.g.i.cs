﻿#pragma checksum "..\..\..\ConfigureThumbnailsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A92F0510E9FA05D181BB3008C73A564AFA7B261E"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Wacom.Kiosk.IntegratorUI;


namespace Wacom.Kiosk.IntegratorUI {
    
    
    /// <summary>
    /// ConfigureThumbnailsWindow
    /// </summary>
    public partial class ConfigureThumbnailsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_filepicker_thumbnail;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_from;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combobox_thumbs_from;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_to;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combobox_thumbs_to;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\ConfigureThumbnailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_update_thumbnails;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Wacom.Kiosk.IntegratorUI;V1.2.0.0;component/configurethumbnailswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ConfigureThumbnailsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.button_filepicker_thumbnail = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\ConfigureThumbnailsWindow.xaml"
            this.button_filepicker_thumbnail.Click += new System.Windows.RoutedEventHandler(this.button_filepicker_thumbnail_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.label_from = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.combobox_thumbs_from = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.label_to = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.combobox_thumbs_to = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.button_update_thumbnails = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\ConfigureThumbnailsWindow.xaml"
            this.button_update_thumbnails.Click += new System.Windows.RoutedEventHandler(this.button_update_thumbnails_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

