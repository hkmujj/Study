using System.Runtime.InteropServices;
using HD_HXD2_TMS.V2控制;
using HD_HXD2_TMS.V6数据输入;
using HD_HXD2_TMS.V7维护测试;
using HD_HXD2_TMS.VC公共组件;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Drawing;
using HD_HXD2_TMS.Extension;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace HD_HXD2_TMS.V0黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V0_BlackView : baseClass, IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetSystemCursor(IntPtr hCursor, uint id);

        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        public const uint SPI_SETCURSORS = 87;
        public const uint SPIF_SENDWININICHANCE = 2;

        public const uint OCR_NORMAL = 32512;
        public const uint OCR_IBEAM = 32513;

        public Boolean IsBlack
        {
            set
            {
                if (_isBlack == value) return;
                _isBlack = value;

                if (value) //开屏
                {
                    this.SetOutBoolValue( UIObj.OutBoolList[0], HD_V6_OtherSetting.CurrentModeIndex, 0);
                    SetSystemCursor(System.Windows.Forms.Cursors.Hand.CopyHandle(), OCR_NORMAL);
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                }
                else
                {
                    //复位
                    HD_V3_FunctionBtns.Reset();
                    HD_V5_FunctionBtns.Reset();
                    HD_V6_FunctionBtns.Reset();
                    HD_V8_FunctionBtns.Reset();
                    HD_V7_Test_Phase.Reset();
                    HD_V6_OtherSetting.Reset();
                    HD_VC_Password.IsShowPassword = false;

                    SystemParametersInfo(SPI_SETCURSORS, 0, IntPtr.Zero, SPIF_SENDWININICHANCE);
                    append_postCmd(CmdType.ChangePage, 0, 0, 0);
                }
            }
        }
        private Boolean _isBlack = false; 
        public override string GetInfo()
        {
            return "公共试图-标题信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            IsBlack = BoolList[UIObj.InBoolList[0]];

            base.paint(dcGs);
        }

        public void Dispose()
        {
            SystemParametersInfo(SPI_SETCURSORS, 0, IntPtr.Zero, SPIF_SENDWININICHANCE);
        }
    }
}
