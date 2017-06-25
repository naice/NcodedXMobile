using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using NcodedXMobile.Controls;

[assembly: ExportRenderer(typeof(BlendImageView), typeof(NcodedXMobile.iOS.Renderer.BlendImageViewRenderer))]

namespace NcodedXMobile.iOS.Renderer
{
    public class BlendImageViewRenderer : ViewRenderer<BlendImageView, UIImageView>
    {
        private bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing && base.Control != null)
            {
                UIImage image = base.Control.Image;
                UIImage uIImage = image;
                if (image != null)
                {
                    uIImage.Dispose();
                    uIImage = null;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BlendImageView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                UIImageView uIImageView = new UIImageView(CGRect.Empty)
                {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    ClipsToBounds = true
                };
                SetNativeControl(uIImageView);
            }
            if (e.NewElement != null)
            {
                SetImage(e.OldElement);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == BlendImageView.SourceProperty.PropertyName)
            {
                SetImage(null);
            }
            else if (e.PropertyName == BlendImageView.BlendColorProperty.PropertyName)
            {
                SetImage(null);
            }
        }

        private void SetImage(BlendImageView previous = null)
        {
            if (previous == null)
            {
                var uiImage = new UIImage(Element.Source);
                uiImage = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                Control.TintColor = Element.BlendColor.ToUIColor();
                Control.Image = uiImage;
                if (!_isDisposed)
                {
                    ((IVisualElementController)Element).NativeSizeChanged();
                }
            }
        }
    }
}