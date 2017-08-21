using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtil.Rtf
{
    internal enum RtfEnumConversion { UseName, UseValue, UseAttribute }

    /// <summary>
    /// Specifies a way to convert an enum to a control word.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    internal class RtfEnumAsControlWordAttribute : Attribute
    {
        private RtfEnumConversion _conversion = RtfEnumConversion.UseName;

        /// <summary>
        /// Gets conversion type.
        /// </summary>
        internal RtfEnumConversion Conversion
        {
            get { return _conversion; }
        }

        /// <summary>
        /// Gets or sets a String value used as a prefix when converting an enum to control word.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Initializes a new instance of ESCommon.Rtf.RtfEnumAsControlWordAttribute class.
        /// </summary>
        /// <param name="conversion">Conversion type.</param>
        internal RtfEnumAsControlWordAttribute(RtfEnumConversion conversion)
        {
            Prefix = String.Empty;
            _conversion = conversion;
        }
    }
}