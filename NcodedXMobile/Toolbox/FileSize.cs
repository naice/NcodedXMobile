using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcodedXMobile.Toolbox
{
    /// <summary>
    /// Suffix for file size, the value defines the lowest possible byte count for the suffix.
    /// </summary>
    public enum FileSizeSuffix : long {
        EB = 0x1000000000000000,
        PB = 0x4000000000000,
        TB = 0x10000000000,
        GB = 0x40000000,
        MB = 0x100000,
        KB = 0x400,
        B = 0 }
    /// <summary>
    /// Fractional file size and suffix, for easy handling.
    /// </summary>
    public sealed class FileSize
    {
        /// <summary>
        /// File size in bytes. Setting this will cause an update to <see cref="Suffix"/> and <see cref="Size"/>
        /// </summary>
        public long Bytes { get { return bytes; } set { UpdateBytes(value); } }
        /// <summary>
        /// File size in fraction corresponding to the <see cref="Suffix"/>.
        /// </summary>
        public double Size { get; private set; }
        /// <summary>
        /// Suffix for the given fractional <see cref="Size"/>.
        /// </summary>
        public FileSizeSuffix Suffix { get; private set; }

        private long bytes;

        public FileSize() : this(0)
        {

        }
        public FileSize(long fileSizeInBytes)
        {
            UpdateBytes(fileSizeInBytes);
        }

        public override string ToString()
        {
            if (Suffix == FileSizeSuffix.B)
                return $"{Size:0.00} {Suffix}";
            else
                return $"{Size:0} {Suffix}";
        }

        private void UpdateBytes(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            FileSizeSuffix suffix;
            double readable;
            if (absolute_i >= (long)FileSizeSuffix.EB) // Exabyte
            {
                suffix = FileSizeSuffix.EB;
                readable = (i >> 50);
            }
            else if (absolute_i >= (long)FileSizeSuffix.PB) // Petabyte
            {
                suffix = FileSizeSuffix.PB;
                readable = (i >> 40);
            }
            else if (absolute_i >= (long)FileSizeSuffix.TB) // Terabyte
            {
                suffix = FileSizeSuffix.TB;
                readable = (i >> 30);
            }
            else if (absolute_i >= (long)FileSizeSuffix.GB) // Gigabyte
            {
                suffix = FileSizeSuffix.GB;
                readable = (i >> 20);
            }
            else if (absolute_i >= (long)FileSizeSuffix.MB) // Megabyte
            {
                suffix = FileSizeSuffix.MB;
                readable = (i >> 10);
            }
            else if (absolute_i >= (long)FileSizeSuffix.KB) // Kilobyte
            {
                suffix = FileSizeSuffix.KB;
                readable = i;
            }
            else
            {
                readable = i;
                suffix = FileSizeSuffix.B;
            }

            // Divide by 1024 to get fractional value
            if (suffix != FileSizeSuffix.B)
                readable = (readable / 1024);

            Suffix = suffix;
            Size = readable;
            bytes = i;
        }

        public static string GetReadableString(long fileSizeInBytes)
            => new FileSize(fileSizeInBytes).ToString();
    }
    
}
