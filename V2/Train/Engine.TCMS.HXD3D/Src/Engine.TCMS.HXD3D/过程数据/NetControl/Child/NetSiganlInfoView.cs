using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.Config;
using Engine.TCMS.HXD3D.过程数据.NetControl.Child.SignalInfo;
using Excel.Interface;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child
{
    public class NetSiganlInfoView : NetControlChildView
    {

        private HXD3DBlueButton m_ReturnBtn;

        private HXD3DBlueButton[] m_SelectPageBtn;

        private SingalInfoChild[] m_ContentPages;

        private SingalInfoChild m_CurrentContent;

        public SingalInfoContentConfig[] ContentConfigs { get; private set; }

        public NetSiganlInfoView(ProcessNetControl parent) : base(parent, NetControlChildType.SignalInfo)
        {
        }


        /// <summary>初始化</summary>
        public override void Init()
        {
            ContentConfigs =
                ExcelParser.Parser<SingalInfoContentConfig>(Parent.AppConfig.AppPaths.ConfigDirectory).ToArray();

            InitalizeBtns();

            InitalizePages();

            SelectPage();
        }

        private void InitalizeReturnBtn()
        {
            m_ReturnBtn = new HXD3DBlueButton
            {
                Text = "返回",
                OutLineRectangle = new Rectangle(700, 510, 80, 40)
            };
            m_ReturnBtn.ButtonClickEvent +=
                (sender, args) => Parent.RequestNavigateTo(NetControlChildType.NetControlRoot);
        }

        private void InitalizeBtns()
        {
            InitalizeReturnBtn();

            InitalizePageBtns();
        }

        private void InitalizePageBtns()
        {
            m_SelectPageBtn = new[]
            {
                new HXD3DBlueButton {Text = "AUX1"},
                new HXD3DBlueButton {Text = "AUX2"},
                new HXD3DBlueButton {Text = "DI1"},
                new HXD3DBlueButton {Text = "DI2"}
            };

            var x = 10;
            var y = 125;
            var w = 65;
            var h = 45;
            for (int i = 0; i < m_SelectPageBtn.Length; i++)
            {
                var t = m_SelectPageBtn[i];
                t.OutLineRectangle = new Rectangle(x, y, w, h);
                t.ContentTextControl.BackColorVisible = false;
                var i1 = i;
                t.ButtonClickEvent += (sender, args) => SelectPage(i1);
                x = x + w + 2;
            }
        }

        public override void NavigateToThis()
        {
            SelectPage();
        }

        private void SelectPage(int index = 0)
        {
            if (m_SelectPageBtn.Length > index && index >= 0)
            {
                foreach (var btn in m_SelectPageBtn)
                {
                    btn.ContentTextControl.BackColorVisible = false;
                }

                m_SelectPageBtn[index].ContentTextControl.BackColorVisible = true;
            }

            m_CurrentContent = m_ContentPages[index];
        }

        private void InitalizePages()
        {
            m_ContentPages = new SingalInfoChild[] {new AUX1View(this), new AUX2View(this), new DI1View(this), new DI2View(this),};

            foreach (var info in m_ContentPages)
            {
                info.Init();
            }
        }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_CurrentContent.OnPaint(g);

            foreach (var btn in m_SelectPageBtn)
            {
                btn.OnDraw(g);
            }

            m_ReturnBtn.OnDraw(g);
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            m_ReturnBtn.OnMouseUp(point);

            foreach (var btn in m_SelectPageBtn)
            {
                btn.OnMouseUp(point);
            }

            return true;
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            m_ReturnBtn.OnMouseDown(point);

            foreach (var btn in m_SelectPageBtn)
            {
                btn.OnMouseDown(point);
            }

            return true;
        }
    }
}