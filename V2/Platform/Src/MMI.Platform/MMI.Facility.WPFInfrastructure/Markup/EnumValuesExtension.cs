using System;
using System.Windows.Markup;

namespace MMI.Facility.WPFInfrastructure.Markup
{
    /// <summary>
    /// 
    /// </summary>
    [MarkupExtensionReturnType(typeof(object[]))]
    public class EnumValuesExtension : MarkupExtension
    {
        /// <summary>
        /// 
        /// </summary>
        [ConstructorArgument("enumType")]
        public Type EnumType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public EnumValuesExtension()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        public EnumValuesExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null)
            {
                throw new ArgumentException("The enum type is not setted.");
            }
            return Enum.GetValues(EnumType);
        }
    }
}
