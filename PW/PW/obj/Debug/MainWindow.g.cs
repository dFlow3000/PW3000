﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4711AB3C3FECE9F5ACEC9630D33B2C03C6954CC6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Preiswattera_3000;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Preiswattera_3000 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl MainContent;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel ActionMenue;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoToAddTeam;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoToShowTeam;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoToTnmtData;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoToEvaluation;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_GoTournamentMenue;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cnvs_PWHeader;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Preiswattera_3000;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainWindow.xaml"
            ((Preiswattera_3000.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainContent = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 3:
            this.ActionMenue = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 4:
            this.btn_GoToAddTeam = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\MainWindow.xaml"
            this.btn_GoToAddTeam.Click += new System.Windows.RoutedEventHandler(this.btn_GoToAddTeam_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_GoToShowTeam = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\MainWindow.xaml"
            this.btn_GoToShowTeam.Click += new System.Windows.RoutedEventHandler(this.btn_GoToShowTeam_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_GoToTnmtData = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\MainWindow.xaml"
            this.btn_GoToTnmtData.Click += new System.Windows.RoutedEventHandler(this.btn_GoToTnmtData_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_GoToEvaluation = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\MainWindow.xaml"
            this.btn_GoToEvaluation.Click += new System.Windows.RoutedEventHandler(this.btn_GoToEvaluation_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_GoTournamentMenue = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\MainWindow.xaml"
            this.btn_GoTournamentMenue.Click += new System.Windows.RoutedEventHandler(this.btn_GoTournamentMenue_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cnvs_PWHeader = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

