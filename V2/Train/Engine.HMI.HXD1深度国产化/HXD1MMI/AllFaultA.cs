using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 机车故障界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AllFalutA : baseClass
    {
        public static List<Fault> SortedFaultList = new List<Fault>();

        private List<AllFaultRect> _currentRectList = new List<AllFaultRect>();

        private int selectFaultIndex = 0;
       
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
            return "所有故障";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < 15; i++)
            {
                _currentRectList.Add(new AllFaultRect(new Rectangle(10, 70 + 30 * i, 760, 30), "", "", "", "", "", ""));
            }
            return true;
        }

        private void DrawSelectRect()
        {
            if (SortedFaultList.Count > 0)
            {
                _currentRectList[selectFaultIndex]._type.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._type.TextColor = Color.White;

                _currentRectList[selectFaultIndex]._code.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._code.TextColor = Color.White;

                _currentRectList[selectFaultIndex]._number.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._number.TextColor = Color.White;

                _currentRectList[selectFaultIndex]._startTime.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._startTime.TextColor = Color.White;

                _currentRectList[selectFaultIndex]._endTime.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._endTime.TextColor = Color.White;

                _currentRectList[selectFaultIndex]._describe.BackgroundColor = Color.DarkBlue;
                _currentRectList[selectFaultIndex]._describe.TextColor = Color.White;

            }
        }

        private void SetPage()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Information].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Information].State == MouseState.MouseDown)
            {
                if (SortedFaultList.Count > 0)
                {
                    FaultAssistInfo.CurrentFault = SortedFaultList[_currentPage * 15 + selectFaultIndex];

                    append_postCmd(CmdType.ChangePage, 25, 0, 0);
                }
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Down].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Down].State == MouseState.MouseDown)//下一页
            {
                int nowValue = selectFaultIndex + 1;

                int limit = SortedFaultList.Count - _currentPage * 15;

                limit = limit >= 15 ? 15 : limit;

                selectFaultIndex = nowValue >= limit ? selectFaultIndex : nowValue;
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].State == MouseState.MouseDown)//上一页
            {
                int nowValue = selectFaultIndex - 1;
                selectFaultIndex = nowValue < 0 ? 0 : nowValue;

            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].State == MouseState.MouseDown)//下一页
            {
                int nowValue = _currentPage + 1;
                _currentPage = nowValue > SortedFaultList.Count / 15 ? _currentPage : nowValue;
                selectFaultIndex = 0;
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].State == MouseState.MouseDown)//上一页
            {
                int nowValue = _currentPage - 1;
                _currentPage = nowValue < 0 ? 0 : nowValue;
                selectFaultIndex = 0;
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

            DrawSelectRect();//新增

            SortedFaultList.Sort();

            for (int i = _currentPage * 15; i < SortedFaultList.Count && i - _currentPage * 15 < 15; i++)
            {
                _currentRectList[i - _currentPage * 15]._number.Text = SortedFaultList[i].TrainNum.ToString();
                _currentRectList[i - _currentPage * 15]._type.Text = SortedFaultList[i].Level.ToString();
                _currentRectList[i - _currentPage * 15]._code.Text = SortedFaultList[i].FaultID.ToString();
                _currentRectList[i - _currentPage * 15]._describe.Text = SortedFaultList[i].FaultText.ToString();
                _currentRectList[i - _currentPage * 15]._startTime.Text = SortedFaultList[i].HappenedTime.ToString();
                _currentRectList[i - _currentPage * 15]._endTime.Text = SortedFaultList[i].EndedTime.ToString();

            }

            foreach (var v in _currentRectList)
            {
                v.OnDraw(dcGs);
            }
        }
    }
}
