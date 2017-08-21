using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;

namespace Motor.ATP._300T.共用
{
    public interface IBtnEventListener
    {
        bool ResponseBtnEvent(BtnInfo btnInfo);
    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPButtonScreen : ATPBaseClass, IDataListener
    {
        private List<RectangleF> m_RectsList;
        private Region[] m_BtnRegionArr;
        private bool[] m_BtnIsDown;
        private string[] m_BtnStr;

        public BtnInfo BtnDownState { set; get; }

        private int m_BtnDownId = -1;
        public BtnInfo BtnUpState { set; get; }

        public List<IBtnEventListener> BtnEventListeners { get; private set; }

        //存放所有按着的按钮编号
        private readonly List<int> m_PressedBtnList = new List<int>();

        private static readonly string[] BtnIndexNames =
        {
            InBoolKeys.InbF1,
            InBoolKeys.InbF2,
            InBoolKeys.InbF3,
            InBoolKeys.InbF4,
            InBoolKeys.InbF5,
            InBoolKeys.InbF6,
            InBoolKeys.InbF7,
            InBoolKeys.InbF8,
            InBoolKeys.Inb1,
            InBoolKeys.Inb2,
            InBoolKeys.Inb3,
            InBoolKeys.Inb4,
            InBoolKeys.Inb5,
            InBoolKeys.Inb6,
            InBoolKeys.Inb7,
            InBoolKeys.Inb8,
            InBoolKeys.Inb9,
            InBoolKeys.Inb0,
            InBoolKeys.Inb警惕按键
        };

        public ATPButtonScreen()
        {
            BtnEventListeners = new List<IBtnEventListener>();
        }

        public void RegistListener(IBtnEventListener btnEventListener)
        {

        }

        //2
        public override string GetInfo()
        {
            return "边框按钮";
        }

        //6
        /// <summary>
        /// 按钮状态获取
        /// </summary>
        private void CheckBtnState()
        {
            for (var i = 0; i < BtnIndexNames.Length; i++)
            {
                var target = BoolList;
                var index = GetInBoolIndex(BtnIndexNames[i]);

                if (target[index] && !m_PressedBtnList.Contains(i)) //判断按钮按下
                {
                    m_PressedBtnList.Add(i);
                    m_BtnIsDown[i] = true;
                    BtnDownState = new BtnInfo(i);
                    break;
                }

                if (m_PressedBtnList.Contains(i) && !target[index]) //判断按钮弹起
                {
                    m_PressedBtnList.RemoveAll(r => r == i);
                    m_BtnIsDown[i] = false;
                    BtnUpState = new BtnInfo(i);
                    BtnDownState = null;
                    break;
                }
            }

            if (!m_PressedBtnList.Any())
            {
                BtnDownState = null;
            }

            if (BtnDownState != null)
            {
                BtnUpState = null;
            }
        }

        public override bool Initalize()
        {
            this.RegistDataListener(this);

            m_RectsList = Coordinate.RectangleFLists(View_ID_Name.ButtonScreen);

            m_BtnRegionArr = new Region[19];
            m_BtnIsDown = new bool[19];

            for (var i = 0; i < m_BtnRegionArr.Length; i++)
            {
                m_BtnRegionArr[i] = new Region(m_RectsList[i]);
            }

            m_BtnStr = new[]
            {
                "F1",
                "F2",
                "F3",
                "F4",
                "F5",
                "F6",
                "F7",
                "F8",
                "调车\n1\n ",
                "目视\n2\nABC",
                "机信\n3\nDEF",
                "启动\n4\nGHI",
                "缓解\n5\nJKL",
                "\n6\nMNO",
                "\n7\nPQRS",
                "\n8\nTUV",
                "\n9\nWXYZ",
                "\n0\n ",
                "警惕\n字母"
            };
            return true;
        }

        public override void paint(Graphics g)
        {
            for (var i = 0; i < m_BtnIsDown.Length; i++)
            {
                g.DrawImage(m_BtnIsDown[i] ? BtnImages.Btn_Down : BtnImages.Btn_Up, m_RectsList[i]);
                g.DrawString(m_BtnStr[i], i < 8 ? FontsItems.Font18YouB : FontsItems.Font13YouB,
                    m_BtnIsDown[i] ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush,
                    m_RectsList[i], FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        public override bool mouseDown(Point point)
        {

            for (var i = 0; i < m_BtnIsDown.Length; i++)
            {
                if (m_BtnRegionArr[i].IsVisible(point))
                {
                    m_BtnIsDown[i] = true;
                    BtnDownState = new BtnInfo(i);
                    m_BtnDownId = i;
                    break;
                }
            }

            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (m_BtnDownId != -1)
            {
                m_BtnIsDown[m_BtnDownId] = false;
                BtnUpState = new BtnInfo(m_BtnDownId);
                BtnDownState = null;
                m_BtnDownId = -1;
            }
            return true;
        }

        protected virtual bool OnBtnEvent(BtnInfo btnInfo)
        {
            foreach (var btnEventListener in BtnEventListeners)
            {
                btnEventListener.ResponseBtnEvent(btnInfo);
            }

            return true;
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {

        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {

        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            CheckBtnState();
        }
    }
}
