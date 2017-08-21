using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Title : baseClass
    {
        private List<RectText> m_UpperRectTextList = new List<RectText>();
        //private string[] _statusArray = new string[] { "", "主界面", "主要数据", "维护", "参数配置", "牵引数据", "隔离解锁" };

        private RectText m_Frame1;
        private RectText m_Frame2;
        private RectText m_Frame3;
        private RectText m_Frame4;
        private RectText m_Frame5;
        private RectText m_Frame6;

        private RectText m_ATC;
        private RectText m_Mode;
        private RectText m_SpeedLimit;
        public static RectText m_Temperature;

        private Color[] m_ATCColorArray;
        private Color[] m_ModeColorArray;
        private string[] m_ModeLabelArray;
        private Color[] m_LimitSpeedColorArray;
        //private string[] _LimitSpeed_LabelArray;
        private string[] m_TempertureLabelArray;

        private Image[] m_ImageArray;


        private RectText m_Speed;
        private RectText m_SpeedUnit;

        private RectText m_EmergencyHandle;
        private RectText m_EmergencyBrake;

        private RectText m_PressureUnit;
        private RectText m_Pressure;

        private RectText m_Stop;
        private RectText m_EventFaults;
        private RectText m_EventFaultCount;
        /// <summary>
        /// 当前广播模式
        /// </summary>
        public static BoardCast.Model m_Model;

        private void SendTheDirectionData()
        {
            switch (BoardCast.m_Direction)
            {
                case BoardCast.Direction.Up:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    break;
                case BoardCast.Direction.Down:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    break;
            }
        }

        public override string GetInfo()
        {
            return "目录信息";
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] str = cStr.Split(new[] { '_' });
                if (str.Length > 1)
                {
                    m_StationList.Add(str[0]);
                }
            }
        }

        private List<string> m_StationList = new List<string>();

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();

            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }



            m_Frame1 = new RectText(new Rectangle(2 + Common.m_InitialPosX, 2 + Common.m_InitialPosY, 240, 120), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
            m_Frame2 = new RectText(new Rectangle(2 + Common.m_InitialPosX + 240, 2 + Common.m_InitialPosY, 120, 120), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
            m_Frame3 = new RectText(new Rectangle(2 + Common.m_InitialPosX + 360, 2 + Common.m_InitialPosY, 100, 120), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
            m_Frame4 = new RectText(new Rectangle(2 + Common.m_InitialPosX + 460, 2 + Common.m_InitialPosY, 140, 120), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
            m_Frame5 = new RectText(new Rectangle(2 + Common.m_InitialPosX + 600, 2 + Common.m_InitialPosY, 198, 58), "", 13, 1, Color.Blue, Common.m_BackGroundColor, Color.Black, 1, true);
            m_Frame6 = new RectText(new Rectangle(2 + Common.m_InitialPosX + 600, 60 + Common.m_InitialPosY, 198, 62), "", 13, 1, Color.Blue, Common.m_BackGroundColor, Color.Black, 1, true);

            m_ATC = new RectText(new Rectangle(15 + Common.m_InitialPosX, 20 + Common.m_InitialPosY, 100, 35), "ATC", 13, 1, Color.Black, Color.FromArgb(0, 255, 64), Color.Black, 1, true, null, true);
            m_Mode = new RectText(new Rectangle(125 + Common.m_InitialPosX, 20 + Common.m_InitialPosY, 100, 35), "", 13, 1, Color.Black, Color.FromArgb(255, 139, 11), Color.Black, 1, true);
            m_SpeedLimit = new RectText(new Rectangle(15 + Common.m_InitialPosX, 70 + Common.m_InitialPosY, 100, 35), "", 13, 1, Color.Black, Color.FromArgb(255, 255, 255, 0), Color.Black, 1, true);
            m_Temperature = new RectText(new Rectangle(125 + Common.m_InitialPosX, 70 + Common.m_InitialPosY, 100, 35), "24", 13, 1, Color.Black, Color.White, Color.Black, 1, true, null, true);



            m_ATCColorArray = new Color[] { Color.FromArgb(0, 255, 64), Color.Orange, Color.Red, Color.Gray };
            m_ModeColorArray = new Color[] { Common.m_BackGroundColor, Color.Yellow, Color.Yellow, Color.Yellow, Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 0), Color.Gray };
            m_ModeLabelArray = new string[] { "模式", "洗车", "RMR", "RMF", "CM", "ATOM", "模式" };
            m_LimitSpeedColorArray = new Color[] { Color.FromArgb(0, 255, 64), Color.FromArgb(255, 255, 255, 0), Color.Gray };
            //_LimitSpeed_LabelArray = new String[] { "<65 Km/h", "<65 Km/h", "?" };
            m_TempertureLabelArray = new string[] { "23°", "24°", "25°", "26°", "27°" };

            m_Speed = new RectText(new Rectangle(2 + Common.m_InitialPosX + 242, 20 + Common.m_InitialPosY, 120 - 4, 65), "0", 40, 1, Color.Blue, Common.m_BackGroundColor, Color.Green, 1, false);
            m_SpeedUnit = new RectText(new Rectangle(2 + Common.m_InitialPosX + 242, 85 + Common.m_InitialPosY, 120 - 4, 35), "km/h", 12, 1, Color.Blue, Common.m_BackGroundColor, Color.Green, 1, false, null, true);

            m_EmergencyHandle = new RectText(new Rectangle(364 + Common.m_InitialPosX + 2, 4 + Common.m_InitialPosY, 45, 36), "", 48, 1, Color.Blue, Common.m_BackGroundColor, Color.Green, 1, false);
            m_EmergencyBrake = new RectText(new Rectangle(412 + Common.m_InitialPosX + 2, 4 + Common.m_InitialPosY, 45, 36), "", 48, 1, Color.Blue, Common.m_BackGroundColor, Color.Green, 1, false);

            m_PressureUnit = new RectText(new Rectangle(2 + Common.m_InitialPosX + 362, 42 + Common.m_InitialPosY, 96, 35), "HV", 20, 1, Color.Blue, Common.m_WhiteColor, Color.Green, 4, false, null, true);

            m_Pressure = new RectText(new Rectangle(2 + Common.m_InitialPosX + 362, 80 + Common.m_InitialPosY, 96, 35), "38A", 20, 1, Color.Black, Color.White, Color.Black, 4, false, null, true);

            m_Stop = new RectText(new Rectangle(500 + Common.m_InitialPosX, 10 + Common.m_InitialPosY, 60, 50), "", 20, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 4, false, m_ImageArray[6]);

            m_EventFaults = new RectText(new Rectangle(480 + Common.m_InitialPosX, 60 + Common.m_InitialPosY, 68, 60), "事件\n故障", 14, 1, Color.Blue, Common.m_BackGroundColor, Color.Black, 1, false, null, true);

            m_EventFaultCount = new RectText(new Rectangle(550 + Common.m_InitialPosX, 75 + Common.m_InitialPosY, 45, 38), "", 14, 1, Color.Black, Common.m_WhiteColor, Color.Black, 1, false);

            m_UpperRectTextList.Add(m_Frame1);

            m_UpperRectTextList.Add(m_Frame2);

            m_UpperRectTextList.Add(m_Frame3);

            m_UpperRectTextList.Add(m_Frame4);

            m_UpperRectTextList.Add(m_Frame5);

            m_UpperRectTextList.Add(m_Frame6);

            m_UpperRectTextList.Add(m_ATC);

            m_UpperRectTextList.Add(m_Mode);

            m_UpperRectTextList.Add(m_SpeedLimit);

            m_UpperRectTextList.Add(m_Temperature);

            m_UpperRectTextList.Add(m_Speed);

            m_UpperRectTextList.Add(m_SpeedUnit);

            m_UpperRectTextList.Add(m_EmergencyHandle);

            m_UpperRectTextList.Add(m_EmergencyBrake);

            m_UpperRectTextList.Add(m_PressureUnit);

            m_UpperRectTextList.Add(m_Pressure);


            m_UpperRectTextList.Add(m_EventFaultCount);

            m_UpperRectTextList.Add(m_EventFaults);

            return true;
        }

        private void GetValue()
        {
            #region ATC
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[i]])
                {
                    m_ATC.BackgroundColor = m_ATCColorArray[i];
                }
            }
            #endregion

            #region Mode

            for (int i = 0; i < 7; i++)
            {
                if (BoolList[UIObj.InBoolList[4 + i]])
                {
                    m_Mode.BackgroundColor = m_ModeColorArray[i];
                    m_Mode.Text = m_ModeLabelArray[i];
                }
            }
            #endregion

            #region 限速
            //update 速度限制区域颜色修改  tdw  2015/06/01 Start
            if (BoolList[UIObj.InBoolList[11]])
            {
                m_SpeedLimit.BackgroundColor = m_LimitSpeedColorArray[0];
            }
            else if (BoolList[UIObj.InBoolList[12]])
            {
                m_SpeedLimit.BackgroundColor = m_LimitSpeedColorArray[1];
            }
            else if (BoolList[UIObj.InBoolList[13]])
            {
                m_SpeedLimit.BackgroundColor = m_LimitSpeedColorArray[2];
            }
            else
            {
                m_SpeedLimit.BackgroundColor = Common.m_BackGroundColor;
            }
            //update 速度限制区域颜色修改  tdw  2015/06/01 End
            m_SpeedLimit.Text = "<" + ((int)(FloatList[UIObj.InFloatList[0]])).ToString() + " Km/h";
            #endregion

            #region 客室温度
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[14 + i]])
                {
                    m_Temperature.Text = m_TempertureLabelArray[i];
                }
            }

            #endregion

            #region 速度
            m_Speed.Text = Math.Abs(((int)(FloatList[UIObj.InFloatList[1]]))).ToString();
            #endregion


            #region 故障相关
            m_EventFaultCount.Text = EventView.m_CurrentEventList.Count.ToString();

            if (EventView.m_FaultLevel > 0)
                m_Stop.ImagePicture = m_ImageArray[9 - EventView.m_FaultLevel];

            #endregion

            #region 紧急手柄
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[19 + i]])
                {
                    m_EmergencyHandle.ImagePicture = m_ImageArray[i];
                }
            }
            #endregion

            #region 紧急刹车
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[22 + i]])
                {
                    m_EmergencyBrake.ImagePicture = m_ImageArray[i + 3];
                }
            }
            #endregion

            #region 高压
            m_PressureUnit.Text = ((int)(FloatList[UIObj.InFloatList[2]])).ToString() + "V";
            #endregion


            #region 电流
            m_Pressure.Text = FloatList[59].ToString() + "A";
            #endregion


            #region 设置当前时间
            m_Frame5.Text = DateTime.Now.ToString();
            #endregion

            #region 设置当前的站点

            try
            {
                m_Frame6.Text = m_StationList[(int)FloatList[53] - 1 < 0 ? 0 : (int)FloatList[53] - 1];
            }
            catch (Exception e)
            {
                m_Frame6.Text = "";
            }
            #endregion
        }

        private bool m_Flicker;

        public override void paint(Graphics g)
        {
            GetValue();
            SendTheDirectionData();
            foreach (var v in m_UpperRectTextList)
            {
                v.OnDraw(g);
            }
            if (m_Flicker)
            {
                m_Flicker = false;
                m_Stop.OnDraw(g);
            }
            else
            {
                m_Flicker = true;
            }

        }


    }
}
