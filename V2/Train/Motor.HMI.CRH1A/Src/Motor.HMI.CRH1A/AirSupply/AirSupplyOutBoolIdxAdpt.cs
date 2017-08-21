using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.AirSupply
{
    public class AirSupplyOutBoolIdxAdpt
    {
        public static int GetIdx(AirSupplyBtnMgr btnMgr, int selctIndx)
        {
            switch (selctIndx)
            {
                case 0:
                    throw new Exception();
                case 1:
                    throw new Exception();

                #region 主要的, 第二排

                case 2:
                    if (btnMgr.ClickBtnType == ButtonType.CutInOrOut)
                    {
                        return 2;
                    }
                    return 2 + 6;
                case 3:
                    if (btnMgr.ClickBtnType == ButtonType.CutInOrOut)
                    {
                        return 3;
                    }
                    return 3 + 6;
                case 4:
                    if (btnMgr.ClickBtnType == ButtonType.CutInOrOut)
                    {
                        return 4;
                    }
                    return 4 + 6;

                #endregion

                case 5:
                    return 5;
                case 6:
                    return 6;
                case 7:
                    return 7;
                default:
                    throw new Exception();
            }
        }
    }
}
