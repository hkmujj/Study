using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CommonUtil.Annotations;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class StrDrivData2D : ATPBase, IButtonResponser
    {
        private int FC_X = FrameButton2D.FrameChange_X;
        private int FC_Y = FrameButton2D.FrameChange_Y;

        private ViewType m_CurrentViewIndex = 0;
        private readonly StringFormat m_DrawFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        /// <summary>
        /// ���Խ��
        /// </summary>
        public static String TestResult { private set; get; }

        public static bool breal = false;

        private int flag = 0;

        private int count = 0;

        /// <summary>
        /// �ڽ����γ̺���Ҫ���������
        /// </summary>
        private List<int> m_BoolNeedResetAfterCourse;

        /// <summary>
        /// ��Ϣ֪ͨ��Ԫ
        /// </summary>
        public class NotifyInfoItem
        {
            /// <summary>
            /// �߼�����
            /// </summary>
            public int LogicIndex { private set; get; }

            /// <summary>
            /// ��Ϣ����
            /// </summary>
            public string Content { private set; get; }

            /// <summary>
            /// ȷ�Ϻ���λ
            /// </summary>
            public int LogicIndexAfterConfirm { private set; get; }

            public NotifyInfoItem(int logicIndex, string content, int afterConfirm = 0)
            {
                LogicIndex = logicIndex;
                Content = content;
                LogicIndexAfterConfirm = afterConfirm;
            }
        }

        static StrDrivData2D()
        {
            NotifyInfoCollection = new SortedList<int, NotifyInfoItem>();
        }

        public static NotifyInfoItem CurrentNotifyInfoItem { set; get; }

        public static SortedList<int, NotifyInfoItem> NotifyInfoCollection { private set; get; }

        public StrDrivData2D()
        {
            m_BoolNeedResetAfterCourse = new List<int>();
        }
        public override bool Initalize()
        {
            LoadMessageInfo();
            ButtonVeiw.Instance.AddResponser(this);
            if (UIObj.ParaList.Count >= 0)
            {
                InitDate();
                m_Timer = new Timer((state) =>
                {
                    Thread.Sleep(500);
                    append_postCmd(CmdType.SetBoolValue, 4968, 0, 0);
                    m_Timer.Change(int.MaxValue, int.MaxValue);
                }, null, int.MaxValue, int.MaxValue);
            }
            else
            {
                return false;
            }

            return true;
        }

        private Timer m_Timer;
        public override void paint(Graphics g)
        {
            if (OutBoolList[4968])
            {
                m_Timer.Change(0, 1000);
            }
            ResponseDown(Background2D.PressedButtonIndex);
            ResponseUp(Background2D.PoppedButtonIndex);

            OnDraw(g);

        }

        public void OnMouseDown(ButtonType pressedBtnIndex)
        {
            switch (m_CurrentViewIndex)
            {
                case ViewType.Data:
                    OnButtonDownOfDataView(pressedBtnIndex);
                    break;
                case ViewType.ControlLevel:
                    OnButtonDownOfCtcsLevelView(pressedBtnIndex);
                    break;
                case ViewType.Other:
                    OnButtonDownOfOtherViews(pressedBtnIndex);
                    break;
                case ViewType.ShowData:
                    OnButtonDownOfShowDataView(pressedBtnIndex);
                    break;
                case ViewType.ETCSTest:
                    OnButtonDownOfEtcsTest(pressedBtnIndex);
                    break;
                case ViewType.Model:
                    OnButtonDownOfControlModelView(pressedBtnIndex);
                    break;
                case ViewType.ConfirmStart:
                    OnButtonDownOfConfirmViewDown(pressedBtnIndex);
                    break;
                case (ViewType)110:
                    switch (pressedBtnIndex)
                    {
                        case ButtonType.F3:
                            if (!BoolList[411])
                            {
                                append_postCmd(CmdType.ChangePage, 111, 0, 0);
                            }
                            break;
                        case ButtonType.F4:
                            append_postCmd(CmdType.ChangePage, 106, 0, 0);
                            break;
                        case ButtonType.F8:
                            append_postCmd(CmdType.ChangePage, 100, 0, 0);
                            break;
                    }
                    break;
                case (ViewType)111:
                    switch (pressedBtnIndex)
                    {
                        case ButtonType.F3:
                            if (NotifyInfoCollection.ContainsKey(381))
                            {
                                CurrentNotifyInfoItem = NotifyInfoCollection[381];
                                DMIMainMenu2D.Popuptxt = CurrentNotifyInfoItem.Content;
                                append_postCmd(CmdType.ChangePage, 109, 0, 0);
                            }
                            break;
                        case ButtonType.F8:
                            append_postCmd(CmdType.ChangePage, 100, 0, 0);
                            break;
                    }
                    break;
            }
        }

        private void OnMouseUp(ButtonType popBtnIndex)
        {
            switch (m_CurrentViewIndex)
            {
                case ViewType.ConfirmStart:
                    OnButtonDownOfConfirmViewUp(popBtnIndex);
                    break;
            }
        }


        private void OnButtonDownOfConfirmViewUp(ButtonType popBtnIndex)
        {
            if (popBtnIndex == ButtonType.ATPConfirm)
            {
                LogMgr.Debug("set 4840 = 0");

                append_postCmd(CmdType.SetBoolValue, 4800 + CurrentNotifyInfoItem.LogicIndexAfterConfirm, 0, 0);

                append_postCmd(CmdType.ChangePage, 100, 0, 0);
            }
        }

        private void OnButtonDownOfConfirmViewDown(ButtonType pressedBtnIndex)
        {
            if (pressedBtnIndex == ButtonType.ATPConfirm)
            {
                DMIMainMenu2D.Popuptxt = " ";
                //append_postCmd(CmdType.SetBoolValue, 4460 + CurrentNotifyInfoItem.LogicIndex, 1, 0);
                LogMgr.Debug("set 4840 = 1");
                append_postCmd(CmdType.SetBoolValue, 4800 + CurrentNotifyInfoItem.LogicIndexAfterConfirm, 1, 0);
            }
        }

        private void OnButtonDownOfControlModelView(ButtonType pressedBtnIndex)
        {
            switch (pressedBtnIndex)
            {
                case ButtonType.F2:
                    if ((int)FloatList[50] != 6)
                    {
                        break;
                    }
                    if (NotifyInfoCollection.ContainsKey(380) && Background2D.bfirstshuju && Background2D.bfirstsijihao && Background2D.bfirstcheci)
                    {
                        CurrentNotifyInfoItem = NotifyInfoCollection[380];
                        DMIMainMenu2D.Popuptxt = CurrentNotifyInfoItem.Content;
                        append_postCmd(CmdType.ChangePage, 109, 0, 0);
                    }
                    break;
                case ButtonType.F3:
                    if ((int)FloatList[50] != 5)
                    {
                        append_postCmd(CmdType.SetBoolValue, 4829, 1, 0);
                        append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    }
                    break;
                case ButtonType.F4:
                    if ((int)FloatList[50] == 5)
                    {
                        append_postCmd(CmdType.SetBoolValue, 4834, 1, 0);
                        append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    }
                    break;
                case ButtonType.F5:
                    append_postCmd(CmdType.SetBoolValue, 4830, 1, 0);
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
                case ButtonType.F7:
                    if (((int)FloatList[50] == 1 || (int)FloatList[50] == 10 || (int)FloatList[50] == 11) && FloatList[62] == 0)
                    {
                        if (NotifyInfoCollection.ContainsKey(380))
                        {
                            CurrentNotifyInfoItem = NotifyInfoCollection[380];
                            DMIMainMenu2D.Popuptxt = CurrentNotifyInfoItem.Content;
                            append_postCmd(CmdType.ChangePage, 109, 0, 0);
                        }
                    }
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        private void OnButtonDownOfEtcsTest(ButtonType btnIndex)
        {
            switch (flag)
            {
                case 0:
                    switch (btnIndex)
                    {
                        case ButtonType.F2:
                            DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[7] = "ȡ��";
                            flag = 1;
                            DMIMainMenu2D.Popuptxt = "���Թ�������ȴ�";
                            append_postCmd(CmdType.SetBoolValue, 4968, 1, 0);
                            break;
                        case ButtonType.F8:
                            append_postCmd(CmdType.ChangePage, 100, 0, 0);
                            break;
                    }
                    break;
                case 1:
                    switch (btnIndex)
                    {
                        case ButtonType.F8:
                            DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[1] = "���Կ�\nʼ";
                            DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                            DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                            flag = 0;
                            break;
                    }
                    //if (count == 0 && FloatList[33] < 300)
                    //{
                    //    count = 1;
                    //}
                    //else if (count == 1 && FloatList[33] > 400)
                    //{
                    //    count = 2;
                    //    append_postCmd(CmdType.SetBoolValue, 4968, 1, 0);
                    //}
                    //else if (count == 2 && FloatList[33] < 300)
                    //{
                    //    count = 3;
                    //}
                    //else if (count == 3 && FloatList[33] > 400)
                    //{
                    //    count = 4;
                    //    append_postCmd(CmdType.SetBoolValue, 4968, 1, 0);
                    //}
                    //else if (count == 4 && FloatList[33] < 300)
                    //{
                    //    count = 5;
                    //}
                    //else if (count == 5 && FloatList[33] > 400)
                    //{
                    //    count = 6;
                    //}
                    if (count == 0)
                    {
                        count = 1;
                    }
                    else if (count == 1 && BoolList[409])
                    {
                        count = 6;
                    }
                    if (count >= 6)
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[1] = "���Կ�\nʼ";
                        DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                        count = 0;
                        flag = 0;
                        breal = true;
                        DMIMainMenu2D.Popuptxt = "�˹����Խ������";
                        TestResult = "���Խ������";
                    }
                    break;
            }
        }

        private void OnButtonDownOfShowDataView(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F1:
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;
                case ButtonType.F2:
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case ButtonType.F3:
                    append_postCmd(CmdType.ChangePage, 120, 0, 0);
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        private void OnButtonDownOfOtherViews(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F2:
                    if ((int)FloatList[50] == 6)
                    {
                        append_postCmd(CmdType.ChangePage, 107, 0, 0);
                    }
                    break;
                case ButtonType.F3:
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case ButtonType.F4:
                    append_postCmd(CmdType.ChangePage, 120, 0, 0);
                    break;
                case ButtonType.F6:
                    append_postCmd(CmdType.ChangePage, 106, 0, 0);
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        private void OnButtonDownOfCtcsLevelView(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F1:
                    append_postCmd(CmdType.SetBoolValue, 4832, 1, 0);
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
                case ButtonType.F2:
                    append_postCmd(CmdType.SetBoolValue, 4833, 1, 0);
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        private void OnButtonDownOfDataView(ButtonType btnIndex)
        {
            switch (btnIndex)
            {
                case ButtonType.F1:
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;
                case ButtonType.F2:
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case ButtonType.F3:
                    append_postCmd(CmdType.ChangePage, 120, 0, 0);
                    //append_postCmd(CmdType.SetFloatValue, 215, 0, 1);
                    break;
                case ButtonType.F6:
                    append_postCmd(CmdType.ChangePage, 106, 0, 0);
                    break;
                case ButtonType.F8:
                    append_postCmd(CmdType.ChangePage, 100, 0, 0);
                    break;
            }
        }

        public override string GetInfo()
        {
            return "�г����ݵȲ˵�";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                m_CurrentViewIndex = (ViewType)nParaB;
                InitDate();
            }
        }

        public void InitDate()
        {
            count = 0;

            DMIMainMenu2D.FlashState = -1;


            DMIMainMenu2D.ClearInputButtonContents();

            switch (m_CurrentViewIndex)
            {
                case (ViewType)103:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "˾����";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "���κ�";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "�г���\n��";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "�г���\n�����";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    break;
                case (ViewType)104:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "0��";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "1��";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    break;
                case ViewType.Other:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    if ((int)FloatList[50] == 6)
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[1] = "ETCS����";
                    }
                    else
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    }
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "����";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "����";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "��ҹ/\n����";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    break;
                case ViewType.ShowData:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "˾����";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "���κ�";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "�г�\n����";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    break;
                case ViewType.ETCSTest:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "���Կ�\nʼ";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    flag = 0;
                    break;
                case ViewType.Model:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    if ((int)FloatList[50] == 6 && Background2D.bfirstshuju && Background2D.bfirstsijihao && Background2D.bfirstcheci)
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[1] = "����";
                    }
                    else
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    }
                    if ((int)FloatList[50] != 5)
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[2] = "����";
                        DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    }
                    else
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                        DMIMainMenu2D.ControlButtonContentCollection[3] = "�˳�\n����";
                    }
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "��ͷ��";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    if (((int)FloatList[50] == 1 || (int)FloatList[50] == 10 || (int)FloatList[50] == 11) && FloatList[62] == 0)
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[6] = "Ŀ��";
                    }
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    flag = 0;
                    break;
                case ViewType.ConfirmStart:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "";
                    flag = 0;
                    break;
                case (ViewType)110:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    if (!BoolList[411])
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[2] = "����";
                    }
                    else
                    {
                        DMIMainMenu2D.ControlButtonContentCollection[2] = "";
                    }
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "�г�\n����";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    flag = 0;
                    break;
                case (ViewType)111:
                    DMIMainMenu2D.ControlButtonContentCollection[0] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[1] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[2] = "Խ��";
                    DMIMainMenu2D.ControlButtonContentCollection[3] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[4] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[5] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[6] = "";
                    DMIMainMenu2D.ControlButtonContentCollection[7] = "����ʾ\n��";
                    flag = 0;
                    break;
            }
        }

        public void OnDraw(Graphics g)
        {
            switch (m_CurrentViewIndex)
            {
                case ViewType.Data:
                    g.DrawString("����", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString("F1 ����˾����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + FC_Y);
                    g.DrawString("F2 ���복�κ�", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 40 + FC_Y);
                    g.DrawString("F3 �����г�����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 80 + FC_Y);
                    g.DrawString("F6 ��ʾ�г�����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 200 + FC_Y);
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
                case ViewType.ControlLevel:
                    g.DrawString("�����п�ϵͳ�ȼ�", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString("F1 ETCS 0��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + FC_Y);
                    g.DrawString("F2 ETCS 1��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 40 + FC_Y);
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
                case ViewType.Other:
                    g.DrawString("����", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    if ((int)FloatList[50] == 6)
                    {
                        g.DrawString("F2 ETCS����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 80 + FC_Y);
                    }
                    g.DrawString("F3 ����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 120 + FC_Y);
                    g.DrawString("F4 ����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 160 + FC_Y);
                    g.DrawString("F6 ��ҹ/����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 240 + FC_Y);
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
                case ViewType.ShowData:
                    g.DrawString("��ʾ�г�����", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    var drawFormat1 = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
                    g.DrawString("�ƶ��ٷֱȣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 40 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("�г����ȣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 80 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("����ٶȣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 120 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("�г����ͣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 160 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("�ƶ����ͣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 200 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("���أ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 240 + FC_Y, 182, 28), drawFormat1);
                    g.DrawString("�޽磺", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 280 + FC_X, 182, 28), drawFormat1);
                    g.DrawString("�����ԣ�", FormatStyle.Font16, FormatStyle.WhiteBrush, new RectangleF(400 + FC_X, 320 + FC_X, 182, 28), drawFormat1);

                    g.DrawString(Inputdata2D.BrakePercent.ToString(), FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 30 + FC_Y);
                    g.DrawString(Inputdata2D.TrainLength.ToString(), FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 70 + FC_Y);
                    g.DrawString(Inputdata2D.MaxSpeed.ToString(), FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 110 + FC_Y);
                    g.DrawString("0", FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 150 + FC_Y);
                    g.DrawString("9R/P", FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 190 + FC_Y);
                    g.DrawString("17", FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 230 + FC_Y);
                    g.DrawString("0", FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 270 + FC_Y);
                    g.DrawString("0", FormatStyle.Font30, FormatStyle.YellowBrush, 588 + FC_X, 310 + FC_Y);

                    break;
                case ViewType.ETCSTest:
                    g.DrawString("ETCS����", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    if (flag == 0)
                    {
                        g.DrawString("ATPϵͳ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 440 + FC_X, 60 + FC_Y);
                        g.FillRectangle(FormatStyle.WhiteBrush, new RectangleF(440 + FC_X, 90 + FC_Y, 250, 28));
                        g.DrawString("ETCS", FormatStyle.Font16, FormatStyle.BlackBrush, 440 + FC_X, 90 + FC_Y);

                        g.DrawString("�ϴβ���ʱ�䣺", FormatStyle.Font16, FormatStyle.WhiteBrush, 440 + FC_X, 150 + FC_Y);

                        g.FillRectangle(FormatStyle.WhiteBrush, new RectangleF(440 + FC_X, 180 + FC_Y, 250, 28));

                        g.DrawString(Background2D.StrtimeY + "    " + Background2D.StrtimeH1, FormatStyle.Font16, FormatStyle.BlackBrush, 440 + FC_X, 180 + FC_Y);

                        g.DrawString("�����", FormatStyle.Font16, FormatStyle.WhiteBrush, 440 + FC_X, 240 + FC_Y);

                        g.FillRectangle(FormatStyle.WhiteBrush, new RectangleF(440 + FC_X, 270 + FC_Y, 250, 28));
                        g.DrawString(TestResult, FormatStyle.Font16, FormatStyle.BlackBrush, 440 + FC_X, 270 + FC_Y);
                    }
                    else if (flag == 1)
                    {
                        g.DrawString("ETCS����", FormatStyle.Font16, FormatStyle.WhiteBrush, 499 + FC_X, 70 + FC_Y);
                        g.DrawString("���Թ�������ȴ�", FormatStyle.Font16, FormatStyle.WhiteBrush, 453 + FC_X, 120 + FC_Y);
                        g.DrawString("��ʼ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 530 + FC_X, 170 + FC_Y);

                        g.DrawString(Background2D.StrtimeY, FormatStyle.Font16, FormatStyle.WhiteBrush, 466 + FC_X, 230 + FC_Y);
                        g.DrawString(Background2D.StrtimeH1, FormatStyle.Font16, FormatStyle.WhiteBrush, 594 + FC_X, 230 + FC_Y);
                    }
                    break;
                case ViewType.Model:
                    g.DrawString("ģʽ", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    if ((int)FloatList[50] == 6 && Background2D.bfirstshuju && Background2D.bfirstsijihao && Background2D.bfirstcheci)
                    {
                        g.DrawString("F2 ����ʼ", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 80 + FC_Y);
                    }
                    if ((int)FloatList[50] != 5)
                    {
                        g.DrawString("F3 ����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 120 + FC_Y);
                    }
                    else
                    {
                        g.DrawString("F4 �˳�����ģʽ", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 160 + FC_Y);
                    }
                    g.DrawString("F5 ��ͷ��ģʽ", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 200 + FC_Y);
                    if (((int)FloatList[50] == 1 || (int)FloatList[50] == 10 || (int)FloatList[50] == 11) && FloatList[62] == 0)
                    {
                        g.DrawString("F7 Ŀ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 280 + FC_Y);
                    }
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
                case ViewType.ConfirmStart:
                    g.DrawString("ȷ��", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString(CurrentNotifyInfoItem == null ? string.Empty : CurrentNotifyInfoItem.Content, FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 180 + FC_Y);
                    break;
                case (ViewType)110:
                    g.DrawString("ר��", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    if (!BoolList[411])
                    {
                        g.DrawString("F3 ����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 120 + FC_Y);
                    }
                    g.DrawString("F4 ��ʾ�п�����", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 160 + FC_Y);
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
                case (ViewType)111:
                    g.DrawString("����", FormatStyle.Font12, FormatStyle.WhiteBrush, new RectangleF(419 + FC_X, 4 + FC_Y, 302, 28), m_DrawFormat);
                    g.DrawString("F3 Խ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 120 + FC_Y);
                    g.DrawString("F8 ����ʾ��", FormatStyle.Font16, FormatStyle.WhiteBrush, 425 + FC_X, 40 + 280 + FC_Y);
                    break;
            }
        }

        private void LoadMessageInfo()
        {
            var file = Path.Combine(RecPath, "..\\Config\\��Ϣ֪ͨ.txt");

            if (File.Exists(file))
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);
                foreach (var cStr in allLines)
                {
                    var split = cStr.Split(new Char[] { ' ', ',', '\t' }).ToList();
                    split.RemoveAll(string.IsNullOrWhiteSpace);

                    if (split.Count < 2)
                    {
                        return;
                    }

                    try
                    {
                        var index = int.Parse(split[0]);
                        var afterConfirm = 0;
                        if (split.Count == 3)
                        {
                            afterConfirm = int.Parse(split[2]);
                        }
                        NotifyInfoCollection.Add(index, new NotifyInfoItem(index, split[1], afterConfirm));
                    }
                    catch (Exception e)
                    {
                        LogMgr.Warn(string.Format("Can not parser driver data info of \"{0}\" {1}", cStr, e));
                    }
                }
            }
            else
            {
                LogMgr.Error(string.Format("Can not found needed file {0}", file));
            }
        }

        public bool ResponseDown(ButtonType btnType)
        {
            if (!UIObj.ViewList.Contains(CurrentViewIdex))
            {
                return false;
            }
            OnMouseDown(btnType);
            return true;
        }

        public bool ResponseUp(ButtonType btnType)
        {
            if (!UIObj.ViewList.Contains(CurrentViewIdex))
            {
                return false;
            }
            OnMouseUp(btnType);
            return true;
        }
    }
}