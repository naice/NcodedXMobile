﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcodedXMobile.Converter
{
    public interface IConverter
    {
        /// <summary>
        /// Deserialize object T from raw string.
        /// </summary>
        /// <returns>default(T) if failed.</returns>
        T DeserializeObject<T>(string raw) where T : class;
        /// <summary>
        /// Serialize object to raw string.
        /// </summary>
        /// <returns>Serialized raw string.</returns>
        string SerializeObject(object obj);
    }
}
