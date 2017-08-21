using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.过程数据.NetControl.Child.TransInfo;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child
{
    public class NetTrainsInfoView : NetControlChildView
    {
        private TransInfoContent m_Content;

        private HXD3DBlueButton m_ReturnBtn;

        private Dictionary<TransType, Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>> m_TypeDataDictionary;

        private List<HXD3DBlueButton> m_ChangContentBtns;

        public NetTrainsInfoView(ProcessNetControl parent) : base(parent, NetControlChildType.TrainsInfo)
        {
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            m_Content = new TransInfoContent();
            m_Content.Init();

            InitalizeTypeData();

            InitalizeChangeContentBtns();

            m_ReturnBtn = new HXD3DBlueButton
            {
                Text = "返回",
                OutLineRectangle = new Rectangle(700, 510, 80, 40),
            };
            m_ReturnBtn.ButtonClickEvent +=
                (sender, args) => Parent.RequestNavigateTo(NetControlChildType.NetControlRoot);
        }

        private void InitalizeChangeContentBtns()
        {
            var x = 10;
            var y = 130;
            var w = 70;
            var h = 40;

            m_ChangContentBtns =
                Enum.GetValues(typeof(TransType)).Cast<TransType>().Select((s, i) => new HXD3DBlueButton()
                {
                    Text = EnumUtil.GetDescription(s).First(),
                    OutLineRectangle = new Rectangle(x + i%10*w, y + i/10*h, w, h),
                    Tag =  s,
                    IsSelected = false,
                    ButtonClickEvent = (sender, args) =>
                    {
                        var btn = (HXD3DBlueButton) sender;
                        
                        ChangeContentTo((TransType) btn.Tag);
                    },
                }).ToList();
        }

        public override void NavigateToThis()
        {
            ChangeContentTo(TransType.CI1);
        }

        private void ChangeContentTo(TransType type)
        {
            m_Content.UpdateSendData(m_TypeDataDictionary[type].Item1);
            m_Content.UpdateRecvData(m_TypeDataDictionary[type].Item2);
            m_ChangContentBtns.ForEach(e => e.IsSelected = false);
            m_ChangContentBtns[(int) type].IsSelected = true;
        }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_ChangContentBtns.ForEach(e => e.OnDraw(g));

            m_Content.OnDraw(g);

            m_ReturnBtn.OnDraw(g);
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            m_ReturnBtn.OnMouseUp(point);

            m_ChangContentBtns.ForEach(e => e.OnMouseUp(point));

            return true;
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            m_ChangContentBtns.ForEach(e => e.OnMouseDown(point));

            return m_ReturnBtn.OnMouseDown(point);
        }

        private void InitalizeTypeData()
        {
            var rand = new Random();
            m_TypeDataDictionary = new Dictionary<TransType, Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>>();

            for (int i = (int) TransType.CI1; i <= (int) TransType.CI6; ++i)
            {
                m_TypeDataDictionary.Add((TransType) i,
                    new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 25), CreateRandStrings(rand, 31)));
            }

            for (int i = (int) TransType.APU11; i <= (int) TransType.APU22; ++i)
            {
                m_TypeDataDictionary.Add((TransType) i,
                    new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 10), CreateRandStrings(rand, 15)));
            }

            m_TypeDataDictionary.Add(TransType.BCU,
                new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 32), CreateRandStrings(rand, 32)));

            for (int i = (int) TransType.LG1; i <= (int) TransType.LG2; ++i)
            {
                m_TypeDataDictionary.Add((TransType) i,
                    new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 8), CreateRandStrings(rand, 28)));
            }

            for (int i = (int) TransType.PSU1; i <= (int) TransType.PSU2; ++i)
            {
                m_TypeDataDictionary.Add((TransType) i,
                    new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 7), CreateRandStrings(rand, 18)));
            }

            m_TypeDataDictionary.Add(TransType.A6A,
                new Tuple<ReadOnlyCollection<string>, ReadOnlyCollection<string>>(CreateRandStrings(rand, 80), CreateRandStrings(rand, 8)));

        }

        private static ReadOnlyCollection<string> CreateRandStrings(Random rand, int count)
        {
            return Enumerable.Repeat(0, count).Select(s => rand.Next(0, 255).ToString("X2")).ToList().AsReadOnly();
        }
    }
}