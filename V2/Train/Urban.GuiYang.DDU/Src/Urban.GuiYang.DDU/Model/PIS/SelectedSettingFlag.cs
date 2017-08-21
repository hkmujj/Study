using System.Diagnostics;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.PIS
{
    [DebuggerDisplay("PISType={PISType},SelecttedSettingType={SelecttedSettingType},SelectedStationType={SelectedStationType}")]
    public class SelectedSettingFlag
    {
        public PISType PISType { get; set; }

        public PISSelecttedSettingType SelecttedSettingType { get; set; }

        /// <summary>
        /// 车站类型
        /// </summary>
        public PISSelectedStationType SelectedStationType { get; set; }
    }
}