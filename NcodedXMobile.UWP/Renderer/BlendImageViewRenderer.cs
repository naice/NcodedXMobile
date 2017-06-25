using NcodedXMobile.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(BlendImageView), typeof(NcodedXMobile.UWP.Renderer.BlendImageViewRenderer))]

namespace NcodedXMobile.UWP.Renderer
{
    public class BlendImageViewRenderer : ViewRenderer<BlendImageView, BitmapIcon>
    {
        private bool _isDisposed;
        private Windows.UI.Color BlendColor {
            get {
                var c = Element.BlendColor;
                return
                    Windows.UI.Color.FromArgb(
                    (byte)(c.A * 255d), (byte)(c.R * 255d), (byte)(c.G * 255d), (byte)(c.B * 255d));

            }
        }

        public BlendImageViewRenderer()
        {
            base.AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BlendImageView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                SetNativeControl(new BitmapIcon() { Foreground=new SolidColorBrush(BlendColor) });
            }
            UpdateBitmap(e.OldElement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == BlendImageView.SourceProperty.PropertyName)
            {
                UpdateBitmap(null);
            }
            else if (e.PropertyName == BlendImageView.BlendColorProperty.PropertyName)
            {
                UpdateColor(null);
            }
        }

        private void UpdateColor(BlendImageView previous = null)
        {
            if (!_isDisposed)
            {
                Control.Foreground = new SolidColorBrush(BlendColor);
            }
        }
        private void UpdateBitmap(BlendImageView previous = null)
        {
            if (!_isDisposed)
            {
                Control.UriSource = new Uri("ms-appx:///" + Element.Source);
            }
        }
    }
}
