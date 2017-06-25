using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NcodedXMobile.Controls;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(BlendImageView), typeof(NcodedXMobile.Android.Renderer.BlendImageViewRenderer))]

namespace NcodedXMobile.Android.Renderer
{
    public class BlendImageViewRenderer : ViewRenderer<BlendImageView, ImageView>
    {
        private bool _isDisposed;

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
                SetNativeControl(new ImageView(Context));
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
                UpdateBitmap(null);
            }
        }

        private void UpdateBitmap(BlendImageView previous = null)
        {
            if (!_isDisposed)
            {
                var d = Resources.GetDrawable(Element.Source).Mutate();
                d.SetColorFilter(new LightingColorFilter(Element.BlendColor.ToAndroid(), Element.BlendColor.ToAndroid()));
                d.Alpha = Element.BlendColor.ToAndroid().A;
                Control.SetImageDrawable(d);
                ((IVisualElementController)Element).NativeSizeChanged();
            }
        }
    }
}