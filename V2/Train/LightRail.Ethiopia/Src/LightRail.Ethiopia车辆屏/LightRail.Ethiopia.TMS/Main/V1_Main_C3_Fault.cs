using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LightRail.Ethiopia.TMS.Control.Common;
using LightRail.Ethiopia.TMS.Pub;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface;

namespace LightRail.Ethiopia.TMS.Main
{
    /// <summary>
    /// 功能描述：公共组件-No.3-获取故障信息
    /// 创建人：唐林
    /// 创建时间：2014-7-9
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_Main_C3_Fault : baseClass
    {
        private float[] _lengths = new float[6] { 53, 49, 52, 73, 82, 345 };
        private Timer _timer = new Timer();
        private Int32 _index = 0;
        private List<FaultInfo> _faultInfos=new List<FaultInfo>();

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-获取故障";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _timer.Interval = 2000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
            VC_C0_Title.EventHandle_ConfirmFault+=VC_C0_Title_EventHandle_ConfirmFault;

            return true;
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VC_C0_Title_EventHandle_ConfirmFault(object sender, ClickEventArgs<Boolean> e)
        {
            if (_faultInfos != null && _faultInfos.Count != 0)
            {
                if (_index >= _faultInfos.Count) _index = 0;
                _faultInfos[_index].IsConfirm = true;
                _faultInfos = VC_C4_GetFault.CurrentFaults.FindAll(a => a.IsConfirm == false);
            }
        }

        /// <summary>
        /// 计时器事件响应函数：倒计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            _index = _index > 10000 ? 0 : _index + 1;
        }

        public override void paint(Graphics dcGs)
        {
            _faultInfos = VC_C4_GetFault.CurrentFaults.FindAll(a => a.IsConfirm == false);
            if (_faultInfos != null && _faultInfos.Count != 0)
            {
                if (_index >= _faultInfos.Count) _index = 0;

                String[] strs = new[]
                {
                    _faultInfos[_index].Vehicle,
                    _faultInfos[_index].Grade.ToString(),
                    _faultInfos[_index].Code,
                    _faultInfos[_index].Device,
                    _faultInfos[_index].Position,
                    _faultInfos[_index].Name
                };

                float xpos = 10;
                for (int i = 0; i < strs.Length; i++)
                {
                    dcGs.DrawString(
                        strs[i],
                        new Font("Verdana", 10),
                        new SolidBrush(Color.FromArgb(255, 255, 0)),
                        new RectangleF(xpos, 523, _lengths[i], 28),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                    xpos += _lengths[i];
                }
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
