using System;
using System.Collections.Generic;
using MMITool.Addin.YdConfigCreater.Model.Constant;

namespace MMITool.Addin.YdConfigCreater.Controller.Result.Detail.Formats
{
    public class ConditionFormatFactory
    {
        public static readonly ConditionFormatFactory Insntace = new ConditionFormatFactory();

        private Dictionary<ModuleType, IFormatProvider> m_FormatProviderDictionary;

        public ConditionFormatFactory()
        {
            m_FormatProviderDictionary = new Dictionary<ModuleType, IFormatProvider>()
            {
                {ModuleType.IO, new IOStringFormatProvider() },
                {ModuleType.MMI, new MMIStringFormatProvider() },
                {ModuleType.MainNode, new MainNodeStringFormatProvider() },
                {ModuleType.OCC, new OCCStringFormatProvider() },
                {ModuleType.Teach, new TeachStringFormatProvider() },
            };
        }

        public IFormatProvider GetFormatProvider(ModuleType type)
        {
            return m_FormatProviderDictionary[type];
        }
    }
}