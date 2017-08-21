using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Common;
using Motor.ATP._200H.ConfigModel;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Main : ShowTypeEffectBaseClass
    {

        #region 静态字段

        /// <summary>
        /// 当前控制模式
        /// </summary>
        public static ControModelEnum CurrentModel { private set; get; }

        #endregion

        #region 私有字段

        #endregion

        private RectText m_BText; //B 区 控 制 模 式 信 息
        private RectText m_CText; //C 区 设 备 运 行 状 态
        private RectText m_EText; //E 区 人 控 机 控 模 式
        private Image[] m_Img;
        public float m_StartAngle = 135.0F;
        public float m_SweepAngle;

        private int m_RefreshCount;

        private int m_BrakeLevel; //制动等级 0代表啥都没有 1代表常用制动 2代表紧急制动 3代表允许缓解
        private DisplayType m_CurrentDisplayType;
        private string m_Year;
        private string m_Month;
        private string m_Day;
        private string m_Hour;
        private string m_Minute;
        private string m_Second;

        private float[] m_TrainInfo;
        private IFloatValueExpression m_ValueExpression;
        private int m_ViewID;

        private int[] m_TrainInfoIndex;

        
        public override string GetInfo()
        {
            return "主界面2D";
        }

        protected override void Initalize()
        {
            m_Img = new Image[UIObj.ParaList.Count];

            InitData();

            m_ValueExpression = new TrainInfoExpression();

            m_TrainInfoIndex = new[]
            {
                this.GetInFloatIndex(InFloatKeys.Inf车次号1), this.GetInFloatIndex(InFloatKeys.Inf车次号2),
                this.GetInFloatIndex(InFloatKeys.Inf司机号),
            };
        }


        public override void paint(Graphics g)
        {
            UpdateCurrentTime();

            RefreshShowType();
            SendValue();
            UpdateValue();
            ReFreshData();
            ClearSettingStatus();
            DrawOn(g);
        }

        private void UpdateCurrentTime()
        {
            DateTime val = this.CurrenTime();
            m_Year = val.Year.ToString();
            m_Month = val.Month.ToString();
            m_Day = val.Day.ToString();
            m_Hour = val.Hour.ToString();
            m_Minute = val.Minute.ToString("00");
            m_Second = val.Second.ToString();
        }

        private void SendValue()
        {
            SendDriveID();
            SendVoice();
            SendDriveInput();
        }

        private void SendDriveID()
        {
            if (!string.IsNullOrEmpty(DataViewView.m_TheDataView.DriverID))
            {
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf司机号), 0, int.Parse(DataViewView.m_TheDataView.DriverID));
            }
            if (!string.IsNullOrEmpty(DataViewView.m_TheDataView.TrainID))
            {
                var tmp = GetTrainID(DataViewView.m_TheDataView.TrainID);
                if (tmp[0] != 0)
                {
                    append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf车次号头), 0, tmp[0]);
                    append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf车次号尾), 0, tmp[1]);
                }
            }
        }

        private void SendVoice()
        {
            append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf音量), 0, VoiceSetting.Voice * 4);
        }

        private void SendDriveInput()
        {
            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub司机号输入界面), m_ViewID == 8 ? 1 : 0, 0);
        }

        private static float[] GetTrainID(string sPara)
        {
            var tmp = new float[2];
            var s = sPara.ToCharArray();
            if (char.IsUpper(s[0]))
            {
                tmp[0] = s[0];
            }
            var str = s.Skip(1)
                .Where(char.IsNumber)
                .Aggregate(string.Empty, (current, source) => current + source.ToString());
            tmp[1] = float.Parse(str);
            return tmp;
        }

        /// <summary>
        /// 获取实时数据
        /// </summary>
        public void UpdateValue()
        {
            try
            {
                m_TrainInfo = m_TrainInfoIndex.Select(s => FloatList[s]).ToArray();
            }
            catch (Exception)
            {
                // ignored
            }

            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub上行按钮标志), 0, 0);
            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub下行按钮标志), 0, 0);

        }

        /// <summary>
        /// 设置运行参数
        /// </summary>
        /// <param name="nParaA">页面跳转方式,1从当前页面,2从其他页面</param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_ViewID = nParaB;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            #region ;;;;;;;;;B 区 控 制 模 式 信 息;;;;;;;;;;

            m_BText = new RectText();
            m_BText.SetBkColor(1, 2, 3);
            m_BText.SetTextColor(255, 255, 255);
            m_BText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_BText.SetTextRect(Common2D.RectB[5].X + 3, Common2D.RectB[5].Y + 3, Common2D.RectB[5].Width - 8, 35);
            //B_text.isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;;;;;C 区 设 备 运 行 状 态;;;;;;;;;;;;;;

            m_CText = new RectText();
            m_CText.SetBkColor(1, 2, 3);
            m_CText.SetTextColor(255, 255, 255);
            m_CText.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_CText.SetTextRect(Common2D.RectC[7].X, Common2D.RectC[7].Y, Common2D.RectC[7].Width,
                Common2D.RectC[7].Height);
            //C_text.isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;E 区 人 控 机 控 模 式;;;;;;;;;;;;;;;

            m_EText = new RectText();
            m_EText.SetBkColor(1, 2, 3);
            m_EText.SetTextColor(255, 255, 255);
            m_EText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_EText.SetTextRect(Common2D.RectE[2].X + 2, Common2D.RectE[2].Y + 2, Common2D.RectE[2].Width - 4,
                Common2D.RectE[2].Height - 4);
            //E_text.isdrawrectfrm = true;

            #endregion

            #region;;;;;;;加 载 图 片;;;;;;;

            m_Img = new Image[]
            {
                ImageResource.zhidong_Level1, ImageResource.zhiDong_level2, ImageResource.zhiDong_Level3,
                ImageResource.zhiDong_Level4, ImageResource.zhiDong_Level5
            };

            if (GlobalParam.Instance.Atp200HBrakeConfig == null)
            {
                m_CurrentDisplayType = DisplayType.Normal;
            }
            else
            {
                m_CurrentDisplayType =
                    GlobalParam.Instance.Atp200HBrakeConfig.BrakeConfig.Find(
                        f => f.ProjectName == GlobalParam.Instance.Atp200HBrakeConfig.ProjectName).Display;
            }


            #endregion
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void ReFreshData()
        {
            //A_text.SetText(Valuef[1].ToString());

            #region 获取控制模式

            CurrentModel = ControModelEnumConvert.ConvertFrom(FloatList[this.GetInFloatIndex(InFloatKeys.Inf控制模式1到7)]);

            m_BText.SetText((CurrentModel != ControModelEnum.Null) ? CurrentModel.ToString() : string.Empty);

            #endregion

            #region 更 新 CTCS等级

            UpdateCTCSLevel();

            #endregion

            m_BrakeLevel = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf制动等级)]); //制动等级

            UpdateMOrPControl();

        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void UpdateMOrPControl()
        {
            if (BoolList[this.GetInboolIndex(InBoolKeys.Inb机控标志)])
            {
                m_EText.SetText("机控");
            }
            else if (BoolList[this.GetInboolIndex(InBoolKeys.Inb人控标志)])
            {
                m_EText.SetText("人控");
            }
            else
            {
                m_EText.SetText("");
            }
        }

        private void UpdateCTCSLevel()
        {
            switch (Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.InfCTCS等级)])) //CTCS等级
            {
                case 0:
                    m_CText.SetText("CTCS 0");
                    break;
                case 1:
                    m_CText.SetText("CTCS 1");
                    break;
                case 2:
                    m_CText.SetText("CTCS 2");
                    break;
                default:
                    m_CText.SetText(" ");
                    break;
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="g">绘图对象</param>
        public void DrawOn(Graphics g)
        {
            // OnDrawV(g);

            #region ;;;;;;绘 制 网 格 分 区;;;;;;;

            DrawBackgroudGrid(g);

            #endregion

            //B区模式信息显示
            m_BText.OnDraw(g);

            //C 区 设 备 运 行 状 态
            m_CText.OnDraw(g);

            DrawBrakeLevel(g);

            //E 区 司机号 车次号信息
            g.DrawString("车次号:", Common2D.Font14B, Common2D.WhiteBrush, Common2D.RectE[21].X + 3,
                Common2D.RectE[21].Y + 5);

            var traininfo = (TrainInfo) m_ValueExpression.Interprete(m_TrainInfo);
            DataViewView.m_TheDataView.DriverID = traininfo.DriverID;
            DataViewView.m_TheDataView.TrainID = traininfo.TrainID;

            g.DrawString(traininfo.TrainID, Common2D.Font14B, Common2D.WhiteBrush, Common2D.RectE[21].X + 7,
                Common2D.RectE[21].Y + 30);

            g.DrawString(traininfo.DriverID, Common2D.Font12B, Common2D.DarkGrayBrush, Common2D.RectE[21].X + 3,
                Common2D.RectE[21].Y + 55);

            //E 区 显 示 时 间 和 日 期
            g.DrawString(m_Hour + ":" + m_Minute, Common2D.Font16B, Common2D.WhiteBrush, Common2D.RectE[23].X + 3,
                Common2D.RectE[23].Y + 10);

            g.DrawString(m_Second, Common2D.Font16B, Common2D.WhiteBrush, Common2D.RectE[23].X + 20,
                Common2D.RectE[23].Y + 35);

            g.DrawString(m_Year + "/" + m_Month + "/" + m_Day, Common2D.Font10B, Common2D.DarkGrayBrush,
                Common2D.RectE[23].X,
                Common2D.RectE[23].Y + 60);

            m_EText.OnDraw(g);
        }

        private void DrawBrakeLevel(Graphics g)
        {
            switch (m_BrakeLevel)
            {
                case 1:
                    g.DrawImage(m_CurrentDisplayType == DisplayType.Normal ? m_Img[0] : m_Img[2], Common2D.RectC[8]);
                    g.DrawString("1", Common2D.Font12B, Common2D.WhiteBrush, Common2D.RectC[8].X + 28,
                        Common2D.RectC[8].Y + 10);
                    break;
                case 2:
                    g.DrawImage(m_CurrentDisplayType == DisplayType.Normal ? m_Img[1] : m_Img[2], Common2D.RectC[8]);
                    g.DrawString("4", Common2D.Font12B, Common2D.WhiteBrush, Common2D.RectC[8].X + 28,
                        Common2D.RectC[8].Y + 10);
                    break;
                case 3:
                    g.DrawImage(m_Img[2], Common2D.RectC[8]);
                    g.DrawString("7", Common2D.Font12B, Common2D.WhiteBrush, Common2D.RectC[8].X + 28,
                        Common2D.RectC[8].Y + 10);
                    break;
                case 4:
                    g.DrawImage(m_Img[3], Common2D.RectC[8]);
                    break;
                case 5:
                    g.DrawImage(m_Img[4], Common2D.RectC[8]);
                    break;
            }
        }

        private static void DrawBackgroudGrid(Graphics g)
        {

            for (var i = 0; i < 3; i++)
            {
                g.DrawRectangle(Common2D.LinePen, Common2D.RectA[i]);
            }

            for (var i = 0; i < 6; i++)
            {
                g.DrawRectangle(Common2D.LinePen, Common2D.RectB[i]);
            }

            for (var i = 0; i < 9; i++)
            {
                g.DrawRectangle(Common2D.LinePen, Common2D.RectC[i]);
            }


            for (var i = 0; i < 24; i++)
            {
                g.DrawRectangle(Common2D.LinePen, Common2D.RectE[i]);
            }

            for (var i = 0; i < 8; i++)
            {
                g.DrawRectangle(Common2D.LinePen, Common2D.RectF[i]);
            }
        }

        /// <summary>
        /// 清空屏发送数据
        /// </summary>
        private void ClearSettingStatus()
        {
            if (m_RefreshCount == 3)
            {
                foreach (var index in GetNeedClearIndexs())
                {
                    append_postCmd(CmdType.SetBoolValue, index, 0, 0);
                }

                m_RefreshCount = 0;
            }

            m_RefreshCount++;
        }

        private IEnumerable<int> GetNeedClearIndexs()
        {
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub调车模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub退出调车模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub目视模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub上行载频);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub下行载频);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS0);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS2);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub启动);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub缓解);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub警惕);
        }
    }
}