﻿#pragma checksum "..\..\..\..\login_registration\RegisterWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DE368C9227182F4F230A286AE66E63A66F59CBC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace Kursach.login_registration {
    
    
    /// <summary>
    /// RegisterWindow
    /// </summary>
    public partial class RegisterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsernameTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfirmPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoleComboBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CodeLabel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\login_registration\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AccessCodeTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Kursach;component/login_registration/registerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\login_registration\RegisterWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UsernameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.ConfirmPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.RoleComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\..\login_registration\RegisterWindow.xaml"
            this.RoleComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RoleComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CodeLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.AccessCodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 45 "..\..\..\..\login_registration\RegisterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Register_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 49 "..\..\..\..\login_registration\RegisterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GoToLogin_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 52 "..\..\..\..\login_registration\RegisterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close_app);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

