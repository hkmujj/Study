namespace Engine.TPX21F.HXN5B.Model.Interface
{
    public interface IValueDialPlateDegree
    {
        /// <summary>
        /// 值 
        /// </summary>
        float Value { get; }

        /// <summary>
        /// 角度, 与绘图角度相同
        /// </summary>
        float Angle { get; }

        /// <summary>
        /// 刻度长度
        /// </summary>
        float Lenght { get; }

        /// <summary>
        /// 显示内容
        /// </summary>
        string Text { get; }
    }
}