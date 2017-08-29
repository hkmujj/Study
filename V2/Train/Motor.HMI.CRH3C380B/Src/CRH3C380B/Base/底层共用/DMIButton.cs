using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    /// <summary>
    /// 按钮类
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIButton : CRH3C380BBase, IDataListener
    {
        public const int BtnMaxCount = 25;

        private bool[] m_BtnBuffer;

        private int[] m_BtnIndexs;

        private List<RectangleF> m_RectsList = new List<RectangleF>();

        //按钮区域
        private readonly List<Region> m_BtnArea = new List<Region>();

        //25个按钮状态
        private readonly bool[] m_BtnState = new bool[25];

        // ReSharper disable once InconsistentNaming
        private readonly List<int> _btnDownList = new List<int>();

        /// <summary>
        /// 按钮按下列表
        /// </summary>
        public List<int> BtnDownList
        {
            get { return GetBtnDownOrUpList(_btnDownList); }
        }

        // ReSharper disable once InconsistentNaming
        private readonly List<int> _btnUpList = new List<int>();
        private int m_BtnDownID = -1;

        /// <summary>
        /// 按钮弹起列表
        /// </summary>
        public List<int> BtnUpList
        {
            get { return GetBtnDownOrUpList(_btnUpList); }
        }

        public override string GetInfo()
        {
            return "DMI按钮";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            DataPackage.ServiceManager.GetService<IDataChangeListenService>().RegistListener(this, AppConfig);

            return true;
        }

        private void InitData()
        {
            m_BtnBuffer = new bool[BtnMaxCount];

            m_BtnIndexs =
                (IsLeftScreen ? DMIButtonResoure.BtnInDMI1 : DMIButtonResoure.BtnInDMI3).Select(
                    s => IndexDescriptionConfig.InBoolDescriptionDictionary[s]).ToArray();

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (!Coordinate.RectangleFLists(ViewIDName.DMIButton, ref m_RectsList))
            {
                return;
            }

            for (int i = 0; i < 25; i++)
            {
                m_BtnArea.Add(new Region(m_RectsList[3 + i]));
            }
        }

        public override bool OnMouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < m_BtnArea.Count; index++)
            {
                if (!m_BtnArea[index].IsVisible(nPoint))
                {
                    continue;
                }

                m_BtnState[index] = true;
                m_BtnDownID = index;
                m_BtnBuffer[index] = true;
                return true;
            }

            return false;
        }

        public override bool OnMouseUp(Point nPoint)
        {
            if (m_BtnDownID == -1)
            {
                return false;
            }

            m_BtnState[m_BtnDownID] = false;
            m_BtnBuffer[m_BtnDownID] = false;
            m_BtnDownID = -1;
            return true;
        }

        public override void Paint(Graphics g)
        {
            UpdateBtnState();

            Draw(g);
        }

        //按钮按下后切换操作
        private void ButtonStateChange(int btnID)
        {
            switch (btnID%25)
            {
                case 17:
                    //DMITitle.TrainInSide = !DMITitle.TrainInSide;
                    break;
                case 18:
                    //DMITitle.NightMode = !DMITitle.NightMode;
                    break;
                case 19:
                    //DMITitle.MarshallMode = !DMITitle.MarshallMode;
                    break;
                case 20:
                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                    break;
                case 24:
                    switch (DMITitle.CurrentMMIName)
                    {
                        case MMIType.左侧MMI屏:
                            DMITitle.CurrentMMIName = MMIType.右侧MMI屏;
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case MMIType.右侧MMI屏:
                            DMITitle.CurrentMMIName = MMIType.左侧MMI屏;
                            append_postCmd(CmdType.ChangePage, 21, 0, 0);
                            break;
                    }

                    break;
            }
        }

        //按钮状态获取
        private void UpdateBtnState()
        {
            for (int i = 0; i < BtnMaxCount; i++)
            {

                if (UpdateBtnUpOrDown(m_BtnBuffer[i], i))
                {
                }

                //if (UpdateBtnUpOrDown(BoolList[m_BtnIndexs[i]], i))
                {
                }
            }
        }

        private bool UpdateBtnUpOrDown(bool isDown, int i)
        {
            if (isDown) //判断按钮按下
            {
                m_BtnState[i%25] = true;
                if (_btnDownList.Count == 0) //按键列表为空
                {
                    _btnDownList.Add(i);
                }
                else if (_btnDownList[0] != i) //按键列表已经有值并且前后不同
                {
                    _btnUpList.Clear(); //上一个按钮弹起状态存到弹起按钮列表
                    _btnUpList.Add(_btnDownList[0]);
                    ButtonStateChange(_btnUpList[0]);

                    _btnDownList.Clear(); //清空一遍再把后收到的加入
                    _btnDownList.Add(i);
                }
            }
            else //判断按钮弹起
            {
                m_BtnState[i%25] = false;
                //弹起后下一个周期就把弹起列表清空
                if (_btnUpList.Count != 0 && _btnUpList[0] == i)
                {
                    _btnUpList.Clear();
                    return true;
                }
                //按钮按下和弹起转换瞬间
                if (_btnDownList.Count == 0 || _btnDownList[0] != i)
                {
                    return true;
                }

                _btnUpList.Clear();
                _btnUpList.Add(_btnDownList[0]);
                ButtonStateChange(_btnUpList[0]);

                _btnDownList.Clear(); //清空按下列表

                return true;
            }

            return false;
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintButtonsState(g);
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawImage(BtnImages.TitleUp, m_RectsList[0]);
            g.DrawImage(BtnImages.TitleDown, m_RectsList[1]);
            g.DrawImage(BtnImages.Title_Right, m_RectsList[2]);

        }

        private void PaintButtonsState(Graphics g)
        {
            for (int i = 0; i < m_BtnArea.Count; i++)
            {
                if (m_BtnState[i])
                {
                    g.DrawImage(i == 21 ? BtnImages.baia : BtnImages.bai, m_RectsList[i + 3]);
                }
            }
        }

        private static readonly List<int> Tmp = new List<int>();

        private static List<int> GetBtnDownOrUpList(List<int> btnList)
        {
            Tmp.Clear();
            foreach (int temp in btnList)
            {
                Tmp.Add(temp%25);
            }

            return Tmp;
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            for (var i = 0; i < m_BtnIndexs.Length; ++i)
            {
                dataChangedArgs.UpdateIfContains(m_BtnIndexs[i], (idx, b) => { UpdateBtnUpOrDown(b, i); });
            }
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
        }
    }
}