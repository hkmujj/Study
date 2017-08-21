namespace Motor.ATP._200H.Common
{
    interface IFloatValueExpression
    {
        /// <summary>
        /// 解释 数据的意义
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        object Interprete(float[] values);
    }
}
