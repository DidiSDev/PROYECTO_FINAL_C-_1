﻿#pragma checksum "..\..\..\RegistrarWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F6BD34A121CE82AD31C17BA9181D09F0DCB3BD67"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using PROYECTO_FINAL_PRIMER_TRIMESTRE;
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


namespace PROYECTO_FINAL_PRIMER_TRIMESTRE {
    
    
    /// <summary>
    /// RegistrarWindow
    /// </summary>
    public partial class RegistrarWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 132 "..\..\..\RegistrarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CajaUsuario;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\RegistrarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox CajaContraseña;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\RegistrarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox CajaRepetirContraseña;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\RegistrarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CajaCorreoElectronico;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\RegistrarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CajaRepetirCorreoElectronico;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PROYECTO_FINAL_PRIMER_TRIMESTRE;component/registrarwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RegistrarWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CajaUsuario = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.CajaContraseña = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.CajaRepetirContraseña = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.CajaCorreoElectronico = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CajaRepetirCorreoElectronico = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 156 "..\..\..\RegistrarWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BotonRegistrar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

