using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NcodedXMobile.Toolbox
{
    /// <summary>
    /// A toolbox for various functions.
    /// </summary>
    public static class Toolbox
    {
        /// <summary>
        /// Gets readable size from byte count
        /// </summary>
        /// <param name="byteCount">Bytes count.</param>
        /// <returns>Formatted string.</returns>
        public static string ByteCountToReadableString(long byteCount)
        {
            return FileSize.GetReadableString(byteCount);
        }
        /// <summary>
        /// Shift Long from high and low int
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns>Shifted long</returns>
        public static long DoubleInt2Long(int low, int high)
        {
            long b = high;
            b = b << 32;
            b = b | (uint)low;
            return b;
        }
        /// <summary>
        /// Interpolate between array of colors based on a fraction form 0.0 to 1.0.
        /// </summary>
        /// <param name="colors">Array of Colors.</param>
        /// <param name="fraction">Fraction from 0.0 to 1.0 higher and lower values will be truncated.</param>
        /// <returns></returns>
        public static Color InterpolateColor(Color[] colors, double fraction)
        {
            double r = 0.0, g = 0.0, b = 0.0;
            double total = 0.0;
            double step = 1.0 / (double)(colors.Length - 1);
            double mu = 0.0;
            double sigma_2 = 0.035;
            fraction = fraction > 1 ? 1 : fraction < 0 ? 0 : fraction;

            foreach (Color color in colors)
            {
                total += Math.Exp(-(fraction - mu) * (fraction - mu) / (2.0 * sigma_2)) / Math.Sqrt(2.0 * Math.PI * sigma_2);
                mu += step;
            }

            mu = 0.0;
            foreach (Color color in colors)
            {
                double percent = Math.Exp(-(fraction - mu) * (fraction - mu) / (2.0 * sigma_2)) / Math.Sqrt(2.0 * Math.PI * sigma_2);
                mu += step;

                r += color.R * percent / total;
                g += color.G * percent / total;
                b += color.B * percent / total;
            }

            return new Color(r, g, b);
        }
    }
}
