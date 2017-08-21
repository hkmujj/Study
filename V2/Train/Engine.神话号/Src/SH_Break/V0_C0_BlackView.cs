using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V0_C0_BlackView : baseClass
    {
        public Boolean IsBlackView
        {
            set
            {
                if (_isBlackView == value)
                    return;

                _isBlackView = value;
                if (_isBlackView)
                {
                    append_postCmd(CmdType.ChangePage, 104, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.ChangePage, 103, 0, 0);
                }
            }
        }
        private Boolean _isBlackView = false;

        public override string GetInfo()
        {
            return "黑屏";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 103) _isBlackView = false;
            }
        }

        public override void paint(Graphics dcGs)
        {
            IsBlackView = BoolList[UIObj.InBoolList[0]];

            base.paint(dcGs);
        }
    }
}
