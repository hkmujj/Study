using System.ComponentModel;

namespace Engine.TCMS.Turkmenistan.Model.State
{
    public enum WorkModel
    {
        [Description("惰　转")]
        Coasting,
        [Description("牵　引")]
        Traction,
        [Description("制　动")]
        Brake,
        [Description("自负荷")]
        SlefLoading,
    }
}
