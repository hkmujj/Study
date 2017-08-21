using System.Drawing;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0_C0_BlackView : baseClass
    {
        public bool IsBlackView
        {
            set
            {
                if (_isBlackView == value)
                {
                    return;
                }

                _isBlackView = value;
                append_postCmd(CmdType.ChangePage, _isBlackView ? 104 : 103, 0, 0);
            }
        }

        private bool _isBlackView = false;

        public override string GetInfo()
        {
            return "黑屏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            SetValueWhenDebug();

            return true;
        }

        private void SetValueWhenDebug()
        {
            if (DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
            {
                return;
            }

            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 1);
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
                if (nParaB == 103)
                {
                    _isBlackView = false;
                }
            }
        }

        public override void paint(Graphics dcGs)
        {
            IsBlackView = BoolList[UIObj.InBoolList[0]];
        }
    }
}
