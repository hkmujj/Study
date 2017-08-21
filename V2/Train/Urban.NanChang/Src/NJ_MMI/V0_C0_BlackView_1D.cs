using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V0_C0_BlackView_1D : baseClass
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

        private int _currentViewState = 0;
        private Boolean _isOpen = false;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "黑屏";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //append_postCmd(CmdType.SetInBoolValue, 1, 1, 0);
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
            //if (nParaA == 2)
            //{
            //    this._currentViewState = nParaB;
            //    switch (this._currentViewState)
            //    {
            //        case 0:
            //            this.IsBlackView = false;
            //            break;
            //        default:
            //            this.IsBlackView = true;
            //            _isOpen = true;
            //            break;
            //    }
            //}
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            IsBlackView = BoolList[UIObj.InBoolList[0]];
            //if (BoolList[UIObj.InBoolList[0]] && !BoolOldList[UIObj.InBoolList[0]])
            //    append_postCmd(CmdType.ChangePage, 104, 0, 0);
            //else if (!BoolList[UIObj.InBoolList[0]] && BoolOldList[UIObj.InBoolList[0]])
            //{
            //    append_postCmd(CmdType.ChangePage, 103, 0, 0);
            //}

            base.paint(dcGs);
        }
        #endregion
    }
}
