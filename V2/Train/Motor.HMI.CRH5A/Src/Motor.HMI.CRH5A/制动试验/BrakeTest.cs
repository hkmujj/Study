using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5A.底层共用;
using Motor.HMI.CRH5A.底层共用.RectView;

namespace Motor.HMI.CRH5A.制动试验
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class BrakeOneTest : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "制动试验1视图";
        }

        //6
        public override bool Initalize()
        {
            Instance = this;
            m_LlRectStatesList = new List<RectangleF[]>();

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.BrakeOneTest);
            m_TheRectStateList = new List<RectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));

            return Init();
        }

        protected override void ParseLine(int line, string content)
        {
            string[] strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == "BrakeOneTest")
            {
                m_TheRectStateList.Add(new RectState(
                       (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                       (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                       new[]
                        {
                            m_RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0]
                        }
                       ));
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2) return;
            foreach (var index in m_TheRectStateList)
            {
                index.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }


        private int m_ValueIndex;
        public static BrakeOneTest Instance { get; private set; }
        public void RefreshBrakeTestStatus()
        {
            foreach (var rectState in m_TheRectStateList)
            {
                rectState.RefreshStateColor(this);
            }
        }
        private void GetValue()
        {
            //if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            if (ButtonsScreen.BtnState != null && !ButtonsScreen.BtnState.IsPress)
            {
                switch (ButtonsScreen.BtnState.BtnId)
                {
                    case 15://1
                        append_postCmd(CmdType.ChangePage, 34, 0, 0);
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 16://2
                        append_postCmd(CmdType.ChangePage, 35, 0, 0);
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 17://3
                        append_postCmd(CmdType.ChangePage, 36, 0, 0);
                        ButtonsScreen.BtnState.Press();
                        break;
                }
            }


        }

        private void DrawOn(Graphics e)
        {
            for (m_ValueIndex = 0; m_ValueIndex < 9; m_ValueIndex++)
            {
                e.DrawString(m_NameArr[m_ValueIndex], FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush, m_RectsList[m_ValueIndex],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            foreach (var index in m_TheRectStateList)
            {
                index.DrawRectState(e);
            }
        }

        #endregion

        private bool Init()
        {
           
            return true;
        }

       
        private List<RectangleF> m_RectsList;
        private static List<RectState> m_TheRectStateList;
        private List<RectangleF[]> m_LlRectStatesList;

        //private int[] _shortRectArr =
        //{
        //    14,
        //    22,
        //    30,
        //    38,
        //    46,
        //    54,
        //    62,
        //    70,
        //    78

        //};

        private string[] m_NameArr =
        {
            "自动制动试验激活", "列车管气密性试验",
            "直通制动试验", "自动试验和警惕阀试验", "BCU安全环路闭合试验",
            "防滑器试验", "启动菜单导向制动试验", "菜单导向制动试验",
            "试验结束"
        };

    }

    [GksDataType(DataType.isMMIObjectClass)]
    internal class BrakeTwoTest : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "制动试验2视图";
        }

        //6
        public override bool Initalize()
        {
            Instance = this;
            m_LlRectStatesList = new List<RectangleF[]>();

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.BrakeTwoTest);
            m_TheRectStateList = new List<RectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));

            return Init();
        }
        public static BrakeTwoTest Instance { get; private set; }
        protected override void ParseLine(int line, string content)
        {
            string[] strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == "BrakeTwoTest")
            {
                m_TheRectStateList.Add(new RectState(
                       (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                       (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                       new[]
                        {
                            m_RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0]
                        }
                       ));
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2) return;
            foreach (var index in m_TheRectStateList)
            {
                index.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
            }
        }
        public void RefreshBrakeTestStatus()
        {
            foreach (var rectState in m_TheRectStateList)
            {
                rectState.RefreshStateColor(this);
            }
        }
        public override void Paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        private int m_ValueIndex;


        private void GetValue()
        {


        }

        private void DrawOn(Graphics e)
        {

            for (m_ValueIndex = 0; m_ValueIndex < 6; m_ValueIndex++)
            {
                e.DrawString(m_NameArr[m_ValueIndex], FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush, m_RectsList[m_ValueIndex],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            foreach (var index in m_TheRectStateList)
            {
                index.DrawRectState(e);
            }
        }

        #endregion

        private bool Init()
        {
           
            return true;
        }

       
        private List<RectangleF> m_RectsList;
        private static List<RectState> m_TheRectStateList;
        private List<RectangleF[]> m_LlRectStatesList;

        //private int[] _shortRectArr =
        //{
        //    14,
        //    22,
        //    30,
        //    38,
        //    46,
        //    54


        //};

        private string[] m_NameArr =
        {
            "菜单导向制动试验激活", "第一次检测制动是否缓解",
            "检测制动是否施加", "紧急制动试验", "第二次检测制动是否缓解",
            "试验结束"
        };

    }

    [GksDataType(DataType.isMMIObjectClass)]
    internal class BrakeThreeTest : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "制动试验3视图";
        }
        public static BrakeThreeTest Instance { get; private set; }
        //6
        public override bool Initalize()
        {
            Instance = this;
            m_LlRectStatesList = new List<RectangleF[]>();

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.BrakeThreeTest);
            m_TheRectStateList = new List<RectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));

            return Init();
        }

        protected override void ParseLine(int line, string content)
        {
            string[] strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == "BrakeThreeTest")
            {
                m_TheRectStateList.Add(new RectState(
                       (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                       (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                       new[]
                        {
                            m_RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                            m_RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0]
                        }
                       ));
            }
        }


        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2) return;
            foreach (var index in m_TheRectStateList)
            {
                index.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
            }
        }
        public void RefreshBrakeTestStatus()
        {
            foreach (var rectState in m_TheRectStateList)
            {
                rectState.RefreshStateColor(this);
            }
        }
        public override void Paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        private int m_ValueIndex;



        private void GetValue()
        {


        }

        private void DrawOn(Graphics e)
        {

            for (m_ValueIndex = 0; m_ValueIndex < 8; m_ValueIndex++)
            {
                e.DrawString(m_NameArr[m_ValueIndex], FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush, m_RectsList[m_ValueIndex],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            foreach (var index in m_TheRectStateList)
            {
                index.DrawRectState(e);
            }
        }

        #endregion

        private bool Init()
        {
            
            return true;
        }

      
        private List<RectangleF> m_RectsList;

        private List<RectangleF[]> m_LlRectStatesList;
        private static List<RectState> m_TheRectStateList;

        //private int[] _shortRectArr =
        //{
        //    14,
        //    22,
        //    30,
        //    38,
        //    46,
        //    54,
        //    62,
        //    70

        //};

        private string[] m_NameArr =
        {
            "备用制动试验激活", "第一次检测制动是否缓解",
            "启动备用制动", "检测制动是否施加", "紧急制动试验", "第二次检测制动是否缓解",
            "停用备用制动", "试验结束"
        };
    }
}


