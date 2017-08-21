using System.Text;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP._300S.Subsys.Resources.Converter
{
    class SelcetObjContentCreater
    {
        private SelcetObjContentCreater()
        {
            
        }

        // ReSharper disable once UnusedMember.Local
        private static void Creat()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<converter1:SelectObjectContentConverter.ObjectContentCollection>");
            //< converter1:ObjectContentPair Key = "{x:Static interface:TrainControlType.ATP}" Content = "{DynamicResource Motor.ATP.Infrasturcture.String.MaichineControl}" />
            foreach (var a in EnumUtil.GetAllEnums<ATPColor>())
            {
                sb.AppendLine(
                    string.Format(
                        "< converter1:ObjectContentPair Key = \"{{ x:Static interface: ATPColor.{0}}}\" Content = \"{{StaticResource StringMaichineControl}}\" />",
                        a));
            }
            sb.AppendLine("</converter1:SelectObjectContentConverter.ObjectContentCollection>");

            // ReSharper disable once UnusedVariable
            var s = sb.ToString();
        }
    }
}