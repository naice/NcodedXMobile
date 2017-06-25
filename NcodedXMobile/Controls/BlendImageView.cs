using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NcodedXMobile.Controls
{
    public class BlendImageView : View
    {
        #region BlendColorProperty
        public static readonly BindableProperty BlendColorProperty = 
            BindableProperty.Create(
                nameof(BlendColor), 
                typeof(Color), 
                typeof(BlendImageView), 
                default(Color));

        /// <summary>
        /// Color that will blend the image.
        /// </summary>
        public Color BlendColor
        {
            get
            {
                return (Color)GetValue(BlendColorProperty);
            }
            set
            {
                SetValue(BlendColorProperty, value);
            }
        }
        #endregion

        #region SourceProperty
        public static readonly BindableProperty SourceProperty = 
            BindableProperty.Create(
                nameof(Source), 
                typeof(string), 
                typeof(BlendImageView), 
                default(string));

        /// <summary>
        /// The Image source.
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        #endregion
    }
}
