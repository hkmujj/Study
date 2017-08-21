using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Util;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Notify;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Menu
{
    /// <summary>
    /// �˵���ͼ
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class CRH2menu : CRH2BaseClass
    {
        public Image[] Img = new Image[4];//��ʼ����͸�ģʽ���汳��ͼ
        public int flagmenu = 0;//��¼��ǰ�˵���ͼ���
        public RectangleF[] Recmenu0 = new RectangleF[3];//��ʼ��ͼ ѡ����ο�

        /// <summary>
        /// �������������
        /// </summary>
        private readonly Rectangle[] m_MenuNavigation = new Rectangle[3];

        public RectangleF[] Recmenu1 = new RectangleF[23];//˾��ģʽ ���ܾ��ο�
        public RectangleF[] Recmenu2 = new RectangleF[9];//�г�Աģʽ ���ܾ��ο�
        public RectangleF[] Recmenu3 = new RectangleF[5];//��¼ģʽ ���ܾ��ο�
        public int[] menu1 = new int[23] { 2, 3, 21, 20, 17, 22, 4, 5, 6, 10, 11, 9, 7, 23, 8, 24, 15, 16, 12, 13, 14, 18, 19 };//˾��ģʽ��������ͼ���
        public int[] menu2 = new int[9] { 57, 0, 0, 0, 0, 0, 0, 0, 0 };//�г�Աģʽ

        private List<MenuBase> m_Menus;
        private MenuBase m_SelectedMenu;

        private Image m_InitBkImage;

        public override void paint(Graphics g)
        {
            if (NotifyManager.Instance.HasNewNotify)
            {
                LogMgr.Debug("Has new Notify, menu view set m_NotifyGetter.HasResponse = true");
                m_NotifyGetter.HasResponse = true;
            }

            if (m_SelectedMenu != null)
            {
                m_SelectedMenu.OnDraw(g);
            }
            else
            {
                g.DrawImage(m_InitBkImage, 0, 0, m_InitBkImage.Width, m_InitBkImage.Height);
            }
        }

        private NotifyGetter m_NotifyGetter;

        public override bool Init()
        {

            InitDate();

            var resource = new MenuResource(this);
            resource.Init();

            LogMgr.Debug(string.Format("{0} get notify getter, app name = {1}", GetType(), ProjectName));
            m_NotifyGetter = NotifyManager.Instance.GetOrCreateNotifyGetter(ProjectName);

            m_Menus = new List<MenuBase>()
            {
                new DriverMenu(this),
                new TrainManagerMenu(this),
                new RecorderMenu(this),
            };

            foreach (var menu in m_Menus)
            {
                menu.Init();
            }

            m_InitBkImage = resource.GetBkImage(null);


            return true;
        }

        protected override bool OnMouseUp(Point point)
        {
            if (m_SelectedMenu != null)
            {
                return m_SelectedMenu.OnMouseUp(point);
            }

            return true;
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            if (m_SelectedMenu != null)
            {
                if (m_SelectedMenu.OnMouseDown(nPoint))
                {
                    return true;
                }

                for (int i = 0; i < 3; i++)
                {
                    if (m_MenuNavigation[i].Contains(nPoint))
                    {
                        m_SelectedMenu = m_Menus[i];
                        return true;
                    }
                }
            }


            else
            {
                //��ʼѡ��
                for (int i = 0; i < 3; i++)
                {
                    if (Recmenu0[i].Contains(nPoint))
                    {
                        m_SelectedMenu = m_Menus[i];
                        return true;
                    }
                }
            }
            

            return false;

        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                if ((int)nParaC == (int)ViewConfig.Black)
                {
                    m_SelectedMenu = null;
                }
                m_Menus.ForEach(e => e.SwitchFromOthers());
            }
        }


        public override string GetInfo()
        {
            return "�˵�";
        }

        public void InitDate()
        {
            

            //��ʼ�˵�
            for (int i = 0; i < 3; i++)
            {
                Recmenu0[i].X = 25 + i * 250;
                Recmenu0[i].Y = 115;
                Recmenu0[i].Width = 250;
                Recmenu0[i].Height = 370;

                m_MenuNavigation[i] = new Rectangle(12, 128 + 122*i, 100, 122);
            }


        }
    }
}