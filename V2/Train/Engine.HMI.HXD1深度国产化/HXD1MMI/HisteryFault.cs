using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Service;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 历史错误
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class HisteryFault : baseClass
    {
        public static ReadOnlyCollection<Fault> EventListA { private set; get; }
        public static ReadOnlyCollection<Fault> EventListB { private set; get; }

        public static ReadOnlyCollection<Fault> AllEvents { private set; get; }
        
        public static List<Fault> CurrentFault = new List<Fault>();

        private List<AllFaultRect> _currentRectList = new List<AllFaultRect>();
        private int _currentPage = 0;

        private AllBackGround _backGround = new AllBackGround(
            new Rectangle(10, 70, 50, 460),
             new Rectangle(60, 70, 50, 460),
              new Rectangle(110, 70, 70, 460),
               new Rectangle(180, 70, 290, 460),
                new Rectangle(470, 70, 150, 460),
                 new Rectangle(620, 70, 150, 460)
            );

        private AllFaultTitleRect _currentFaultTitleRect = new AllFaultTitleRect(new Rectangle(10, 40, 760, 30), "类别", "车号", "代码", "故障内容", "故障发生时间", "故障结束时间");

        public override string GetInfo()
        {
            return "历史故障";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Started)
                {
                    FaultFacode.Instance.Clear();
                }
            };

            for (int i = 0; i < 15; i++)
            {
                _currentRectList.Add(new AllFaultRect(new Rectangle(10, 70 + 30 * i, 760, 30), "", "", "", "", "", ""));
            }
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");

            try
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);

                ParseLines(allLines);

                AllEvents =  new ReadOnlyCollection<Fault>(EventListA.Union(EventListB).ToList());
            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format("Can not read file : {0} \r\n {1}", file, e));
            }

            return true;
        }

        private void ParseLines(string[] allLines)
        {
            var eva = new List<Fault>();
            var evb = new List<Fault>();
            foreach (var cStr in allLines)
            {
                if (cStr.Trim() != "")
                {
                    string[] str = cStr.Split(';');
                    if (str.Length == 10)
                    {
                        var data = new Fault
                        {
                            LogicalBit = Convert.ToInt32(str[1]),
                            Level = (FaultLevel) Enum.Parse(typeof (FaultLevel), str[2]),
                            TrainNum = int.Parse(str[3]),
                            FaultID = Convert.ToInt32(str[4]),
                            FaultText = str[5],
                            Helpinfo_V_Big_0 = str[6],
                            HelpinfoV_Equal_0 = str[7],
                            HelpInfo = str[8],
                            XXXXXX = str[9]
                        };

                        if (str[0].Equals("A"))
                        {
                            eva.Add(data);
                        }
                        else
                        {
                            evb.Add(data);
                        }
                    }
                }
            }

            EventListA = new ReadOnlyCollection<Fault>(eva);
            EventListB = new ReadOnlyCollection<Fault>(evb);
        }

        private void SetPage()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].State == MouseState.MouseDown)//下一页
            {
                int nowValue = _currentPage + 1;
                _currentPage = nowValue > CurrentFault.Count / 15 ? _currentPage : nowValue;
              
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].State == MouseState.MouseDown)//上一页
            {
                int nowValue = _currentPage - 1;
                _currentPage = nowValue < 0 ? 0 : nowValue;
               
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _currentRectList.Count; i++)
            {
                _currentRectList[i].Clear();
            }
        }

        public override void paint(Graphics dcGs)
        {
            _backGround.OnDraw(dcGs);
            _currentFaultTitleRect.OnDraw(dcGs);

            Clear();
            SetPage();//新增



            _backGround.OnDraw(dcGs);
            _currentFaultTitleRect.OnDraw(dcGs);

            CurrentFault.Sort();

            for (int i = _currentPage * 15; i < CurrentFault.Count && i - _currentPage * 15 < 15; i++)
            {
                _currentRectList[i - _currentPage * 15]._number.Text = CurrentFault[i].TrainNum.ToString();
                _currentRectList[i - _currentPage * 15]._type.Text = CurrentFault[i].Level.ToString();
                _currentRectList[i - _currentPage * 15]._code.Text = CurrentFault[i].FaultID.ToString();
                _currentRectList[i - _currentPage * 15]._describe.Text = CurrentFault[i].FaultText.ToString();
                _currentRectList[i - _currentPage * 15]._startTime.Text = CurrentFault[i].HappenedTime.ToString();
                _currentRectList[i - _currentPage * 15]._endTime.Text = CurrentFault[i].EndedTime.ToString();

            }

            foreach (var v in _currentRectList)
            {
                v.OnDraw(dcGs);
            }
        }
    }
}