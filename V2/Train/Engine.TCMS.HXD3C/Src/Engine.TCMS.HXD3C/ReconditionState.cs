using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ReconditionState : baseClass, IButtonBtnEventResponser
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "����״̬����";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;
            password = string.Empty;
            ButtomButtonView.Instance.AddResponser(this);

            m_Img = this.GetImages();

            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(ButtonStr);
                password = string.Empty;
            }
        }

        #endregion#

        public override void paint(Graphics g)
        {
            g.DrawImage(m_Img[0], m_Rects[0]);
            if (password.Length != 0)
            {
                g.DrawImage(m_Img[1], m_Rects[13]);
            }
            g.DrawString("".PadLeft(password.Length, '*'), Common.Txt16FontB, Common.WhiteBrush, m_Rects[1], m_RightFormat);
        }


        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void InitData()
        {

            m_Rects = new List<Rectangle> {new Rectangle(25, 160, 750, 320), new Rectangle(135, 298, 140, 50)};
            var loc = new Point(509, 225);
            const int invateX = 7;
            const int invateY = 9;
            var size = new Size(40, 40);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects.Add(new Rectangle(loc, size));
                    loc.Offset(invateX + size.Width, 0);
                }
                loc.Offset(-(invateX + size.Width) * 3, size.Height + invateY);
            }
            m_Rects.Add(new Rectangle(loc, size));
            loc.Offset(invateX + size.Width, 0);
            m_Rects.Add(new Rectangle(loc, new Size(85, 40)));
            loc.Offset(20 - invateX - size.Width, size.Height + invateY);
            m_Rects.Add(new Rectangle(loc, new Size(80, 40)));

        }

        public bool Response(int obj)
        {
            if (GlobalParam.Instance.CurrentViewID == 215)
            {
                switch (obj)
                {
                    case 1: //��¼
                        break;
                    case 2: //�趨
                        break;
                    case 3: //ʵ��
                        append_postCmd(CmdType.ChangePage, 218, 0, 0);
                        break;
                    case 4: //״̬
                        break;
                    case 8: //����
                        this.NavigateReturn();
                        break;
                }
                return true;
            }
            return false;
        }

        private void SetNum(int num)
        {
            if (password.Length < GlobalParam.Instance.PasswordConfig.MaxLength)
            {
                password += num;
            }
        }

        private void DeleteNum()
        {
            if (password.Length != 0)
            {
                password = password.Substring(0, password.Length - 1);
            }
        }
        void MouseDownEvent(int index)
        {
            switch (index)
            {
                case 2://����7
                case 3://����8
                case 4://����9
                    SetNum(index + 5);
                    break;
                case 5://����4
                case 6://����5
                case 7://����6
                    SetNum(index - 1);
                    break;
                case 8://����1
                case 9://����2
                case 10://����3
                    SetNum(index - 7);
                    break;
                case 11://����0
                    SetNum(0);
                    break;
                case 12://���
                    DeleteNum();
                    break;
                case 13://ȷ��
                    if (password.Equals(GlobalParam.Instance.PasswordConfig.Password))
                    {
                        ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(ButtonStr2);
                    }
                    break;
            }
        }
        public override bool mouseDown(Point point)
        {
            int index = -1;
            foreach (var source in m_Rects.Skip(2).Where(w => w.Contains(point)))
            {
                index = m_Rects.IndexOf(source);
                break;
            }
            if (index != -1)
            {
                MouseDownEvent(index);
            }
            return base.mouseDown(point);
        }

        /// <summary>
        /// ͼƬ��
        /// </summary>
        private Image[] m_Img;
        public readonly string[] ButtonStr = new string[8] { "", "", "", "", "", "", "", "����" };
        public readonly string[] ButtonStr2 = new string[8] { "��¼", "�趨", "ʵ��", "״̬", "", "", "", "����" };
        private readonly StringFormat m_RightFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        /// <summary>
        /// ���ο�
        /// </summary>
        List<Rectangle> m_Rects;

        private string password;


    }
}