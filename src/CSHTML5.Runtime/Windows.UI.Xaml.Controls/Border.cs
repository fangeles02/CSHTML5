﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ● This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ● You are NOT allowed to:
//       – Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       – Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       – Remove or modify this notice
//
//  ● Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using CSHTML5.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
#if MIGRATION
using System.Windows.Media;
#else
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

#if MIGRATION
namespace System.Windows.Controls
#else
namespace Windows.UI.Xaml.Controls
#endif
{
    /// <summary>
    /// Draws a border, background, or both, around another object.
    /// </summary>
    /// <example>
    /// You can add a Border to the XAML as follows:
    /// <code lang="XAML" xml:space="preserve">
    /// <Border Width="60"
    ///         Height="30"
    ///         CornerRadius="15"
    ///         Padding="20"
    ///         Background="Blue"
    ///         HorizontalAlignment="Left">
    ///     <!--Child here.-->
    /// </Border>
    /// </code>
    /// Or in C# (assuming we have a StackPanel Named MyStackPanel):
    /// <code lang="C#">
    /// Border myBorder = new Border();
    /// myBorder.Width = 60;
    /// myBorder.Height = 30;
    /// myBorder.CornerRadius = new CornerRadius(15);
    /// myBorder.Padding = new Thickness(20);
    /// myBorder.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
    /// myBorder.HorizontalAlignment=HorizontalAlignment.Left;
    /// MyStackPanel.Children.Add(myBorder);
    /// </code>
    /// </example>
    [ContentProperty("Child")]
    public class Border : FrameworkElement
    {
        private UIElement _child;

        /// <summary>
        /// Gets or sets the child element to draw the border around.
        /// </summary>
        public UIElement Child
        {
            get
            {
                return _child;
            }
            set
            {
                if (this._isLoaded)
                {
                    INTERNAL_VisualTreeManager.DetachVisualChildIfNotNull(_child, this);
                    INTERNAL_VisualTreeManager.AttachVisualChildIfNotAlreadyAttached(value, this);
                }
                _child = value;
            }
        }

        protected internal override void INTERNAL_OnAttachedToVisualTree()
        {
            INTERNAL_VisualTreeManager.AttachVisualChildIfNotAlreadyAttached(_child, this);
        }


        /// <summary>
        /// Gets or sets the Brush that fills the background of the border.
        /// </summary>
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        /// <summary>
        /// Identifies the Background dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(Border), new PropertyMetadata(null)
            {
                GetCSSEquivalent = (instance) =>
                {
                    return new CSSEquivalent()
                    {
                        Name = new List<string> { "background", "backgroundColor", "backgroundColorAlpha" },
                    };
                }
            }
            );


        /// <summary>
        /// Gets or sets a brush that describes the border background of a control.
        /// </summary>
        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        /// <summary>
        /// Identifies the BorderBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Border), new PropertyMetadata(null)
            {
                GetCSSEquivalent = (instance) =>
                {
                    return new CSSEquivalent()
                    {
                        Name = new List<string> { "borderColor" },
                    };
                }
            }
            );

        /// <summary>
        /// Gets or sets the thickness of the border.
        /// </summary>
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        /// <summary>
        /// Identifies the BorderThickness dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Border), new PropertyMetadata(new Thickness()) { MethodToUpdateDom = BorderThickness_MethodToUpdateDom });

        static void BorderThickness_MethodToUpdateDom(DependencyObject d, object newValue)
        {
            if (newValue != null)
            {
                var border = (Border)d;
                dynamic domElement = INTERNAL_HtmlDomManager.GetFrameworkElementOuterStyleForModification(border);
                var thickness = (Thickness)newValue;
                domElement.borderStyle = "solid"; //todo: see if we should put this somewhere else
                domElement.borderWidth = thickness.Top + "px " + thickness.Right + "px " + thickness.Bottom + "px " + thickness.Left + "px ";
                domElement.boxSizing = "border-box";
                //domElement.borderWidth = 
                //      (newValue.Top > 0 ? newValue.Top + 1 : 0).ToString() + "px "
                //      + (newValue.Right > 0 ? newValue.Right + 1 : 0).ToString() + "px "
                //      + (newValue.Bottom > 0 ? newValue.Bottom + 1 : 0).ToString() + "px "
                //      + (newValue.Left > 0 ? newValue.Left + 1 : 0).ToString() + "px ";
            }
        }

        /// <summary>
        /// Gets or sets the radius for the corners of the border.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>
        /// Identifies the CornerRadius dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Border), new PropertyMetadata(new CornerRadius()) { MethodToUpdateDom = CornerRadius_MethodToUpdateDom });

        static void CornerRadius_MethodToUpdateDom(DependencyObject d, object newValue)
        {
            var border = (Border)d;
            var cornerRadius = (CornerRadius)newValue;
            var domStyle = INTERNAL_HtmlDomManager.GetFrameworkElementOuterStyleForModification(border);
            domStyle.borderTopLeftRadius = cornerRadius.TopLeft + "px";
            domStyle.borderTopRightRadius = cornerRadius.TopRight + "px";
            domStyle.borderBottomRightRadius = cornerRadius.BottomRight + "px";
            domStyle.borderBottomLeftRadius = cornerRadius.BottomLeft + "px";
        }


        // Returns:
        //     The dimensions of the space between the border and its child as a Thickness
        //     value. Thickness is a structure that stores dimension values using pixel
        //     measures.
        /// <summary>
        /// Gets or sets the distance between the border and its child object.
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        /// <summary>
        /// Identifies the Padding dependency property.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(Border), new PropertyMetadata(new Thickness()) { MethodToUpdateDom = Padding_MethodToUpdateDom });

        private static void Padding_MethodToUpdateDom(DependencyObject d, object newValue)
        {
            var border = (Border)d;
            var newPadding = (Thickness)newValue;
            var innerDomElement = border.INTERNAL_InnerDomElement;
            var styleOfInnerDomElement = INTERNAL_HtmlDomManager.GetDomElementStyleForModification(innerDomElement);
            if (newPadding == null) //if it is null, we want 0 everywhere
            {
                newPadding = new Thickness();
            }
            //todo: if the container has a padding, add it to the margin
            styleOfInnerDomElement.boxSizing = "border-box";
            styleOfInnerDomElement.paddingLeft = newPadding.Left + "px";
            styleOfInnerDomElement.paddingTop = newPadding.Top + "px";
            styleOfInnerDomElement.paddingRight = newPadding.Right + "px";
            styleOfInnerDomElement.paddingBottom = newPadding.Bottom + "px";
        }
    }
}
