using System.Diagnostics.CodeAnalysis;

namespace Motor.ATP._300T.共用
{
    /// <summary>
    /// 视图名
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum View_ID_Name
    {
        ClassOverScreen,
        BlackScreen,
        FaultReceive,
        MainScreen,
        ButtonScreen,
        OpenFunBtn,
    }

    public enum FontName
    {
        Arial,
        宋体,
        黑体,
        幼圆,
    }

    public enum FontRelated
    {
        居中,
        靠左,
        靠右,
        靠左上,
        靠左下,
    }

    public enum RectRiseDirection
    {
        上,
        下,
        左,
        右,
    }
}
