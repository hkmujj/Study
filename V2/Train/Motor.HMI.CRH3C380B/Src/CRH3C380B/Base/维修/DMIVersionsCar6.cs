using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.ά��
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIVersionsCar6 : DMIVersionsCarBase
    {

        private readonly string[] m_BtnStr =
        {
            "1�ų�\n�汾",
            "2�ų�\n�汾",
            "3�ų�\n�汾",
            "4�ų�\n�汾",
            "5�ų�\n�汾",
            "",
            "7�ų�\n�汾",
            "8�ų�\n�汾",
            "�복2",
            "��ҳ��"
        };

        //2
        public override string GetInfo()
        {
            return "DMI����6�İ汾";
        }

        // ReSharper disable once ConvertToAutoProperty
        protected override string[] BtnContents
        {
            get { return m_BtnStr; }
        }

        protected override Image BackgoundImage
        {
            get { return MaintainImages.car6; }
        }

        protected override string TitleName
        {
            get { return "ά��; 6�ų��汾"; }
        }
    }
}