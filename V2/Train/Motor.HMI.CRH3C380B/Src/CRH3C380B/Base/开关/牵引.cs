using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMITraction : CRH3C380BBase
    {
        private bool m_IsBl;
        private List<CommonInnerControlBase> m_ComtrolCollection;

        private Dictionary<int, int> m_PowerUpDictionary;

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_OutBNameIndexs =
        {
            OutBoolKeys.Oub受电弓1隔离,
            OutBoolKeys.Oub受电弓2隔离,
            OutBoolKeys.Oub受电弓3隔离,
            OutBoolKeys.Oub受电弓4隔离,
            OutBoolKeys.Oub真空断路器1隔离,
            OutBoolKeys.Oub真空断路器2隔离,
            OutBoolKeys.Oub真空断路器3隔离,
            OutBoolKeys.Oub真空断路器4隔离,
            OutBoolKeys.Oub车顶隔离开关1隔离,
            OutBoolKeys.Oub车顶隔离开关2隔离,
            OutBoolKeys.Oub车顶隔离开关3隔离,
            OutBoolKeys.Oub车顶隔离开关4隔离,
            OutBoolKeys.Oub牵引变流器1隔离,
            OutBoolKeys.Oub牵引变流器2隔离,
            OutBoolKeys.Oub牵引变流器3隔离,
            OutBoolKeys.Oub牵引变流器4隔离,
            OutBoolKeys.Oub牵引变流器5隔离,
            OutBoolKeys.Oub牵引变流器6隔离,
            OutBoolKeys.Oub牵引变流器7隔离,
            OutBoolKeys.Oub牵引变流器8隔离,
            OutBoolKeys.Oub受电弓1恢复,
            OutBoolKeys.Oub受电弓2恢复,
            OutBoolKeys.Oub受电弓3恢复,
            OutBoolKeys.Oub受电弓4恢复,
            OutBoolKeys.Oub真空断路器1恢复,
            OutBoolKeys.Oub真空断路器2恢复,
            OutBoolKeys.Oub真空断路器3恢复,
            OutBoolKeys.Oub真空断路器4恢复,
            OutBoolKeys.Oub车顶隔离开关1恢复,
            OutBoolKeys.Oub车顶隔离开关2恢复,
            OutBoolKeys.Oub车顶隔离开关3恢复,
            OutBoolKeys.Oub车顶隔离开关4恢复,
            OutBoolKeys.Oub牵引变流器1恢复,
            OutBoolKeys.Oub牵引变流器2恢复,
            OutBoolKeys.Oub牵引变流器3恢复,
            OutBoolKeys.Oub牵引变流器4恢复,
            OutBoolKeys.Oub牵引变流器5恢复,
            OutBoolKeys.Oub牵引变流器6恢复,
            OutBoolKeys.Oub牵引变流器7恢复,
            OutBoolKeys.Oub牵引变流器8恢复,

        };

        private readonly string[] m_ContentStrs = {"25kV", "受电弓", "主断路器", "车顶隔离开关", "牵引变流器", "辅助供电单元"};


        private readonly int[] m_Row58 = {101, 103, 119, 104, 120, 106};

        private readonly int[] m_Row516 = {101, 103, 119, 104, 120, 106, 109, 111, 127, 112, 128, 114};



        private bool m_TrainInside;
        private bool m_MarshallMode;

        private readonly SwitchsState[] m_ShouDianGong =
        {
            SwitchsState.弓未知,
            SwitchsState.弓未知,
            SwitchsState.弓未知,
            SwitchsState.弓未知
        };

        private readonly SwitchsState[] m_ZhuDuan =
        {
            SwitchsState.主断未知,
            SwitchsState.主断未知,
            SwitchsState.主断未知,
            SwitchsState.主断未知
        };

        private readonly SwitchsState[] m_CheDingGeLi =
        {
            SwitchsState.车顶隔离开关未知,
            SwitchsState.车顶隔离开关未知,
            SwitchsState.车顶隔离开关未知,
            SwitchsState.车顶隔离开关未知
        };

        private readonly SwitchsState[] m_QianYinBianLiu =
        {
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知,
            SwitchsState.牵引变流器未知
        };

        private readonly SwitchsState[] m_FuZhuGongDian =
        {
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑,
            SwitchsState.辅助供电黑
        };


        private Dictionary<SwitchsState, Image[]> m_SwitchStateToImage;

        private int m_RowId; //行坐标
        private int m_RankId; //列坐标
        private readonly Dictionary<int, int[]> m_RowRankNumbC16 = new Dictionary<int, int[]>();
        private readonly Dictionary<int, int[]> m_RowRankNumbC8 = new Dictionary<int, int[]>();

        private readonly List<string> m_AssPowerNameIndex = new List<string>
        {
            InBoolKeys.Inb牵引_辅助供电单元_1_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_1_黄,
            InBoolKeys.Inb牵引_辅助供电单元_2_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_2_黄,
            InBoolKeys.Inb牵引_辅助供电单元_3_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_3_黄,
            InBoolKeys.Inb牵引_辅助供电单元_4_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_4_黄,
            InBoolKeys.Inb牵引_辅助供电单元_5_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_5_黄,
            InBoolKeys.Inb牵引_辅助供电单元_6_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_6_黄,
            InBoolKeys.Inb牵引_辅助供电单元_7_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_7_黄,
            InBoolKeys.Inb牵引_辅助供电单元_8_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_8_黄,
            InBoolKeys.Inb牵引_辅助供电单元_9_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_9_黄,
            InBoolKeys.Inb牵引_辅助供电单元_10_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_10_黄,
            InBoolKeys.Inb牵引_辅助供电单元_11_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_11_黄,
            InBoolKeys.Inb牵引_辅助供电单元_12_蓝,
            InBoolKeys.Inb牵引_辅助供电单元_12_黄,

        };

        private readonly List<string> m_TowIndexNames = new List<string>
        {
            InBoolKeys.Inb牵引_牵引变流器_11_闭合,
            InBoolKeys.Inb牵引_牵引变流器_11_断开,
            InBoolKeys.Inb牵引_牵引变流器_11_切除,
            InBoolKeys.Inb牵引_牵引变流器_12_闭合,
            InBoolKeys.Inb牵引_牵引变流器_12_断开,
            InBoolKeys.Inb牵引_牵引变流器_12_切除,
            InBoolKeys.Inb牵引_牵引变流器_21_闭合,
            InBoolKeys.Inb牵引_牵引变流器_21_断开,
            InBoolKeys.Inb牵引_牵引变流器_21_切除,
            InBoolKeys.Inb牵引_牵引变流器_22_闭合,
            InBoolKeys.Inb牵引_牵引变流器_22_断开,
            InBoolKeys.Inb牵引_牵引变流器_22_切除,
            InBoolKeys.Inb牵引_牵引变流器_31_闭合,
            InBoolKeys.Inb牵引_牵引变流器_31_断开,
            InBoolKeys.Inb牵引_牵引变流器_31_切除,
            InBoolKeys.Inb牵引_牵引变流器_32_闭合,
            InBoolKeys.Inb牵引_牵引变流器_32_断开,
            InBoolKeys.Inb牵引_牵引变流器_32_切除,
            InBoolKeys.Inb牵引_牵引变流器_41_闭合,
            InBoolKeys.Inb牵引_牵引变流器_41_断开,
            InBoolKeys.Inb牵引_牵引变流器_41_切除,
            InBoolKeys.Inb牵引_牵引变流器_42_闭合,
            InBoolKeys.Inb牵引_牵引变流器_42_断开,
            InBoolKeys.Inb牵引_牵引变流器_42_切除,

        };

        private readonly List<string> m_CarTopIndexNames = new List<string>
        {
            InBoolKeys.Inb牵引_车顶隔离开关_1_闭合,
            InBoolKeys.Inb牵引_车顶隔离开关_1_断开,
            InBoolKeys.Inb牵引_车顶隔离开关_1_切除,
            InBoolKeys.Inb牵引_车顶隔离开关_2_闭合,
            InBoolKeys.Inb牵引_车顶隔离开关_2_断开,
            InBoolKeys.Inb牵引_车顶隔离开关_2_切除,
            InBoolKeys.Inb牵引_车顶隔离开关_3_闭合,
            InBoolKeys.Inb牵引_车顶隔离开关_3_断开,
            InBoolKeys.Inb牵引_车顶隔离开关_3_切除,
            InBoolKeys.Inb牵引_车顶隔离开关_4_闭合,
            InBoolKeys.Inb牵引_车顶隔离开关_4_断开,
            InBoolKeys.Inb牵引_车顶隔离开关_4_切除,

        };

        //2
        private readonly List<string> m_MainSwIndexNames = new List<string>
        {
            InBoolKeys.Inb牵引_主断_1_闭合,
            InBoolKeys.Inb牵引_主断_1_断开,
            InBoolKeys.Inb牵引_主断_1_切除,
            InBoolKeys.Inb牵引_主断_2_闭合,
            InBoolKeys.Inb牵引_主断_2_断开,
            InBoolKeys.Inb牵引_主断_2_切除,
            InBoolKeys.Inb牵引_主断_3_闭合,
            InBoolKeys.Inb牵引_主断_3_断开,
            InBoolKeys.Inb牵引_主断_3_切除,
            InBoolKeys.Inb牵引_主断_4_闭合,
            InBoolKeys.Inb牵引_主断_4_断开,
            InBoolKeys.Inb牵引_主断_4_切除,

        };

        private readonly List<string> m_PantographStateIndexNames = new List<string>
        {
            InBoolKeys.Inb牵引_受电弓_1_升弓,
            InBoolKeys.Inb牵引_受电弓_1_降弓,
            InBoolKeys.Inb牵引_受电弓_1_切除,
            InBoolKeys.Inb牵引_受电弓_2_升弓,
            InBoolKeys.Inb牵引_受电弓_2_降弓,
            InBoolKeys.Inb牵引_受电弓_2_切除,
            InBoolKeys.Inb牵引_受电弓_3_升弓,
            InBoolKeys.Inb牵引_受电弓_3_降弓,
            InBoolKeys.Inb牵引_受电弓_3_切除,
            InBoolKeys.Inb牵引_受电弓_4_升弓,
            InBoolKeys.Inb牵引_受电弓_4_降弓,
            InBoolKeys.Inb牵引_受电弓_4_切除,
        };

        private Timer m_SendTimer;
        private Timer m_ClearTimer;

        private void ChangeValueTo(object index = null)
        {
            if (index != null)
            {
                append_postCmd(CmdType.SetBoolValue, (int) index, 1, 0);
                m_ClearTimer.Change(5000, -1);
            }
        }

        private void ChangeValue(object index = null)
        {
            for (int indexs = 5040; indexs < 5060; indexs++)
            {
                append_postCmd(CmdType.SetBoolValue, indexs, 0, 0);
            }
        }

        public override string GetInfo()
        {
            return "DMI牵引";
        }

        //6
        public override bool Initalize()
        {
            m_SendTimer = new Timer(ChangeValueTo);
            m_ClearTimer = new Timer(ChangeValue);
            m_SendTimer.Change(int.MaxValue, -1);
            m_ClearTimer.Change(int.MaxValue, -1);
            InitData();
            return true;
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                BtnStrUpdate();
                m_RowId = 0;
                m_RankId = 0;

            }
            m_TrainInside = DMITitle.TrainInSide;
            m_MarshallMode = DMITitle.MarshallMode;
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintState(g);

            PaintRectangles(g);
            if (GlobalParam.Instance.ProjectType != ProjectType.CRH380BL)
            {
                m_ComtrolCollection.ForEach(f => f.OnPaint(g));
            }
        }

        private void GetValue()
        {
            ResponseBtnEvent();

            for (var i = 0; i < 4; i++)
            {
                UpdatePantograph(i);

                UpdateMainSwitch(i);

                UpdateTopSwitch(i);
            }

            UpdateTow();

            UpdateAssPower();

            if (m_RowId > 0)
            {
                BtnUpdate();
            }
            else
            {
                BtnStrUpdate();
            }
        }

        private void UpdatePantograph(int i)
        {
            //受电弓
            if (GetInBoolValue(m_PantographStateIndexNames[2 + i*3]))
            {
                m_ShouDianGong[i] = SwitchsState.切除弓;
            }
            else if (GetInBoolValue(m_PantographStateIndexNames[1 + i*3]))
            {
                m_ShouDianGong[i] = SwitchsState.降弓;
            }
            else if (GetInBoolValue(m_PantographStateIndexNames[i*3]))
            {
                m_ShouDianGong[i] = SwitchsState.升弓;
            }
            else
            {
                m_ShouDianGong[i] = SwitchsState.弓未知;
            }
        }

        private void UpdateMainSwitch(int i)
        {
            //主断
            if (GetInBoolValue(m_MainSwIndexNames[2 + i*3]))
            {
                m_ZhuDuan[i] = SwitchsState.主断切除;
            }
            else if (GetInBoolValue(m_MainSwIndexNames[1 + i*3]))
            {
                m_ZhuDuan[i] = SwitchsState.主断断开;
            }
            else if (GetInBoolValue(m_MainSwIndexNames[0 + i*3]))
            {
                m_ZhuDuan[i] = SwitchsState.主断闭合;
            }
            else
            {
                m_ZhuDuan[i] = SwitchsState.主断未知;
            }
        }

        private void UpdateTopSwitch(int i)
        {
            //车顶隔离开关
            if (GetInBoolValue(m_CarTopIndexNames[2 + i*3]))
            {
                m_CheDingGeLi[i] = SwitchsState.车顶隔离开关切除;
            }
            else if (GetInBoolValue(m_CarTopIndexNames[1 + i*3]))
            {
                m_CheDingGeLi[i] = SwitchsState.车顶隔离开关断开;
            }
            else if (GetInBoolValue(m_CarTopIndexNames[0 + i*3]))
            {
                m_CheDingGeLi[i] = SwitchsState.车顶隔离开关闭合;
            }
            else
            {
                m_CheDingGeLi[i] = SwitchsState.车顶隔离开关未知;
            }
        }

        private void UpdateTow()
        {
            for (var i = 0; i < 8; i++)
            {
                //牵引变流器
                if (GetInBoolValue(m_TowIndexNames[2 + i*3]))
                {
                    m_QianYinBianLiu[i] = SwitchsState.牵引变流器切除;
                }
                else if (GetInBoolValue(m_TowIndexNames[1 + i*3]))
                {
                    m_QianYinBianLiu[i] = SwitchsState.牵引变流器断开;
                }
                else if (GetInBoolValue(m_TowIndexNames[0 + i*3]))
                {
                    m_QianYinBianLiu[i] = SwitchsState.牵引变流器闭合;
                }
                else
                {
                    m_QianYinBianLiu[i] = SwitchsState.牵引变流器未知;
                }
            }
        }

        private void UpdateAssPower()
        {
            for (var i = 0; i < 12; i++)
            {
                //辅助供电单元
                if (GetInBoolValue(m_AssPowerNameIndex[0 + i*2]))
                {
                    m_FuZhuGongDian[i] = SwitchsState.辅助供电蓝;
                }
                else if (GetInBoolValue(m_AssPowerNameIndex[1 + i*2]))
                {
                    m_FuZhuGongDian[i] = SwitchsState.辅助供电黄;
                }
                else
                {
                    m_FuZhuGongDian[i] = SwitchsState.辅助供电黑;
                }
            }
        }

        private void ResponseBtnEvent()
        {
            #region ::::::::::::::::::: 按钮

            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //返回
                        append_postCmd(CmdType.ChangePage, 190, 0, 0);
                        break;
                    case 1: //左
                        if (m_RankId > 0)
                        {
                            m_RankId--;
                        }
                        break;
                    case 2: //右
                        if (m_RowId > 0 &&
                            m_RankId <
                            ((m_MarshallMode || m_IsBl)
                                ? m_RowRankNumbC16[m_RowId].Length
                                : m_RowRankNumbC8[m_RowId].Length) - 1)
                        {
                            m_RankId++;
                        }
                        break;
                    case 3: //上
                        if (m_RowId > 0)
                        {
                            m_RowId--;
                            m_RankId = 0;
                        }
                        break;
                    case 4: //下
                        if (m_RowId < 4)
                        {
                            m_RowId++;
                            m_RankId = 0;
                        }
                        break;
                    case 6: //恢复
                        if (!string.IsNullOrEmpty(m_BtnStr[0]))
                        {
                            if (!m_TrainInside)
                            {
                                append_postCmd(CmdType.SetBoolValue,
                                    GetOutBoolIndex(m_OutBNameIndexs[(m_RowId - 1)*4 + m_RankId]), 0, 0);
                                append_postCmd(CmdType.SetBoolValue,
                                    GetOutBoolIndex(m_OutBNameIndexs[20 + (m_RowId - 1)*4 + m_RankId]),
                                    1, 0);
                                ChangeValueTo(GetOutBoolIndex(m_OutBNameIndexs[20 + (m_RowId - 1)*4 + m_RankId]));
                            }
                            else
                            {
                                if (m_RowId < 4)
                                {
                                    append_postCmd(CmdType.SetBoolValue,
                                        GetOutBoolIndex(
                                            m_OutBNameIndexs[
                                                m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 2 : 0) - m_RankId]),
                                        0, 0);
                                    append_postCmd(CmdType.SetBoolValue,
                                        GetOutBoolIndex(m_OutBNameIndexs[
                                            20 + m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 2 : 0) - m_RankId]), 1, 0);
                                    ChangeValueTo(
                                        GetOutBoolIndex(m_OutBNameIndexs[
                                            20 + m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 2 : 0) - m_RankId]));
                                }
                                else if (m_RowId == 4)
                                {
                                    if (GlobalParam.Instance.ProjectType == ProjectType.CRH3C)
                                    {
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[19 - ((m_MarshallMode || m_IsBl) ? 0 : 4) - m_RankId]),
                                            0, 0);
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[
                                                    20 + 19 - ((m_MarshallMode || m_IsBl) ? 0 : 4) - m_RankId]),
                                            1, 0);
                                        ChangeValueTo(
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[
                                                    20 + 19 - ((m_MarshallMode || m_IsBl) ? 0 : 4) - m_RankId]));
                                    }
                                    else
                                    {
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[19 - ((m_MarshallMode || m_IsBl) ? 4 : 0) - m_RankId]),
                                            0, 0);
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[
                                                    20 + 19 - ((m_MarshallMode || m_IsBl) ? 4 : 0) - m_RankId]),
                                            1, 0);
                                        ChangeValueTo(
                                            GetOutBoolIndex(
                                                m_OutBNameIndexs[
                                                    20 + 19 - ((m_MarshallMode || m_IsBl) ? 4 : 0) - m_RankId]));
                                    }
                                }
                            }
                        }
                        break;
                    case 8: //隔离
                        if (!string.IsNullOrEmpty(m_BtnStr[2]))
                        {
                            if (!m_TrainInside)
                            {
                                append_postCmd(CmdType.SetBoolValue,
                                    GetOutBoolIndex(m_OutBNameIndexs[(m_RowId - 1)*4 + m_RankId]), 1, 0);
                                append_postCmd(CmdType.SetBoolValue,
                                    GetOutBoolIndex(m_OutBNameIndexs[20 + (m_RowId - 1)*4 + m_RankId]),
                                    0, 0);
                            }
                            else
                            {
                                if (m_RowId < 4)
                                {
                                    if (GlobalParam.Instance.ProjectType == ProjectType.CRH3C)
                                    {
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(m_OutBNameIndexs[
                                                m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 2 : 0) - m_RankId]), 1, 0);
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(m_OutBNameIndexs[
                                                20 + m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 2 : 0) - m_RankId]),
                                            0,
                                            0);
                                    }
                                    else
                                    {
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(m_OutBNameIndexs[
                                                m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 0 : 2) - m_RankId]), 1, 0);
                                        append_postCmd(CmdType.SetBoolValue,
                                            GetOutBoolIndex(m_OutBNameIndexs[
                                                20 + m_RowId*4 - 1 - ((m_MarshallMode || m_IsBl) ? 0 : 2) - m_RankId]),
                                            0,
                                            0);
                                    }
                                }
                                else if (m_RowId == 4)
                                {
                                    append_postCmd(CmdType.SetBoolValue,
                                        GetOutBoolIndex(
                                            m_OutBNameIndexs[19 - ((m_MarshallMode || m_IsBl) ? 0 : 4) - m_RankId]), 1,
                                        0);
                                    append_postCmd(CmdType.SetBoolValue,
                                        GetOutBoolIndex(
                                            m_OutBNameIndexs[20 + 19 - ((m_MarshallMode || m_IsBl) ? 0 : 4) - m_RankId]),
                                        0, 0);
                                }
                            }
                        }
                        break;
                    case 10: //
                        break;
                    case 15: //主页面
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }

            #endregion
        }

        private void BtnUpdate()
        {
            if (CheckStateIsCutted())
            {
                m_BtnStr[0] = "恢复";
                m_BtnStr[2] = string.Empty;
            }
            else
            {
                m_BtnStr[0] = string.Empty;
                m_BtnStr[2] = "切除";
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        private bool CheckStateIsCutted()
        {
            switch (m_RowId)
            {
                case 1:
                    return m_ShouDianGong[m_RankId] == SwitchsState.切除弓;
                case 2:
                    return m_ZhuDuan[m_RankId] == SwitchsState.主断切除;
                case 3:
                    return m_CheDingGeLi[m_RankId] == SwitchsState.车顶隔离开关切除;
                case 4:
                    return m_QianYinBianLiu[m_RankId] == SwitchsState.牵引变流器切除;
            }

            return true;
        }

        private void BtnStrUpdate()
        {
            m_BtnStr[0] = string.Empty;
            m_BtnStr[2] = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("开关;牵引", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

        }

        private void PaintState(Graphics g)
        {
            if (!m_TrainInside)
            {
                for (var i = 0; i < ((m_MarshallMode || m_IsBl) ? 4 : 2); i++)
                {
                    g.DrawImage(m_SwitchStateToImage[m_ShouDianGong[i]][DMITitle.NightMode ? 1 : 0],
                        m_RectsList[m_RowRankNumbC16[1][i]]); //受电弓
                    g.DrawImage(m_SwitchStateToImage[m_ZhuDuan[i]][DMITitle.NightMode ? 1 : 0],
                        m_RectsList[m_RowRankNumbC16[2][i]]); //主断
                    g.DrawImage(m_SwitchStateToImage[m_CheDingGeLi[i]][DMITitle.NightMode ? 1 : 0],
                        m_RectsList[m_RowRankNumbC16[3][i]]); //车顶隔离开关
                }
                for (var i = 0; i < ((m_MarshallMode || m_IsBl) ? 8 : 4); i++)
                {
                    g.DrawImage(m_SwitchStateToImage[m_QianYinBianLiu[i]][DMITitle.NightMode ? 1 : 0],
                        m_RectsList[m_RowRankNumbC16[4][i]]); //牵引变流器
                }
                for (var i = 0; i < ((m_MarshallMode || m_IsBl) ? 12 : 6); i++)
                {
                    g.DrawImage(
                        BoolList[m_PowerUpDictionary[i + 1]]
                            ? CommonImages.StateUnkown
                            : m_SwitchStateToImage[m_FuZhuGongDian[i]][DMITitle.NightMode ? 1 : 0],
                        m_RectsList[m_Row516[i]]);
                    if (!BoolList[m_PowerUpDictionary[i + 1]])
                    {
                        g.DrawString(i.ToString(CultureInfo.InvariantCulture), FontsItems.FontC11,
                            SolidBrushsItems.YellowBrush1, m_RectsList[m_Row516[i]]);
                    }
                }
            }
            else
            {
                for (var i = ((m_MarshallMode || m_IsBl) ? 4 : 2) - 1; i >= 0; i--)
                {
                    g.DrawImage(
                        m_SwitchStateToImage[m_ShouDianGong[(m_MarshallMode || m_IsBl) ? 3 - i : 1 - i]][
                            DMITitle.NightMode ? 1 : 0],
                        m_RectsList[(m_MarshallMode || m_IsBl) ? m_RowRankNumbC16[1][i] : m_RowRankNumbC8[1][i]]); //受电弓
                    g.DrawImage(
                        m_SwitchStateToImage[m_ZhuDuan[(m_MarshallMode || m_IsBl) ? 3 - i : 1 - i]][
                            DMITitle.NightMode ? 1 : 0],
                        m_RectsList[(m_MarshallMode || m_IsBl) ? m_RowRankNumbC16[2][i] : m_RowRankNumbC8[2][i]]); //主断
                    g.DrawImage(
                        m_SwitchStateToImage[m_CheDingGeLi[(m_MarshallMode || m_IsBl) ? 3 - i : 1 - i]][
                            DMITitle.NightMode ? 1 : 0],
                        m_RectsList[(m_MarshallMode || m_IsBl) ? m_RowRankNumbC16[3][i] : m_RowRankNumbC8[3][i]]);
                    //车顶隔离开关
                }
                for (var i = ((m_MarshallMode || m_IsBl) ? 8 : 4) - 1; i >= 0; i--)
                {
                    g.DrawImage(
                        m_SwitchStateToImage[m_QianYinBianLiu[(m_MarshallMode || m_IsBl) ? 7 - i : 3 - i]][
                            DMITitle.NightMode ? 1 : 0],
                        m_RectsList[(m_MarshallMode || m_IsBl) ? m_RowRankNumbC16[4][i] : m_RowRankNumbC8[4][i]]);
                    //牵引变流器
                }
                for (var i = ((m_MarshallMode || m_IsBl) ? 12 : 6) - 1; i >= 0; i--)
                {
                    g.DrawImage(
                        BoolList[m_PowerUpDictionary[i + 1]]
                            ? CommonImages.StateUnkown
                            : m_SwitchStateToImage[m_FuZhuGongDian[(m_MarshallMode || m_IsBl) ? 11 - i : 5 - i]][
                                DMITitle.NightMode ? 1 : 0],
                        m_RectsList[(m_MarshallMode || m_IsBl) ? m_Row516[i] : m_Row58[i]]);
                    if (!BoolList[m_PowerUpDictionary[i + 1]])
                    {
                        g.DrawString(i.ToString(CultureInfo.InvariantCulture),
                            FontsItems.FontC11,
                            SolidBrushsItems.YellowBrush1,
                            m_RectsList[(m_MarshallMode || m_IsBl) ? m_Row516[i] : m_Row58[i]]);
                    }
                }
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            //框线
            g.DrawImage((m_MarshallMode || m_IsBl)
                ? DMITitle.NightMode ? CommonImages.开关牵引16_1 : CommonImages.开关牵引16_0
                : DMITitle.NightMode ? CommonImages.开关牵引8_1 : CommonImages.开关牵引8_0, m_RectsList[12]);

            for (var i = 0; i < 6; i++)
            {
                //标题
                g.DrawString(m_ContentStrs[i], FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[14 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }

            for (var i = 0; i < ((m_MarshallMode || m_IsBl) ? 16 : 8); i++)
            {
                //车厢号
                g.DrawString(m_IsBl ? GetCarNumOfBl(i) : GetCarNum.GetNum(i, m_MarshallMode, m_TrainInside),
                    FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[20 + i], FontsItems.TheAlignment(FontRelated.居中));
            }


            if (m_RowId == 0)
            {
                g.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[13]);
            }
            else
            {
                g.FillRectangle(SolidBrushsItems.BlueBrush1,
                    m_RectsList[(m_MarshallMode || m_IsBl)
                        ? m_RowRankNumbC16[m_RowId][m_RankId]
                        : m_RowRankNumbC8[m_RowId][m_RankId]]);
            }
        }

        private string GetCarNumOfBl(int i)
        {
            return DMITitle.TrainInSide ? (16 - i).ToString("00") : (i + 1).ToString("00");
        }

        private void InitData()
        {
            m_IsBl = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL;
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }
            m_ComtrolCollection = new List<CommonInnerControlBase>();
            m_ComtrolCollection.Add(new GDIRectText
            {
                Text = "动车组1",
                TextColor = DMITitle.NightMode ? Color.Black : Color.White,
                OutLineRectangle = new Rectangle(180, 75, 130, 30),
                NeedDarwOutline = false,
                Tag = 1,
                DrawFont = FontsItems.FontC11,
                RefreshAction = o =>
                {
                    ((GDIRectText) o).Text = DMITitle.MarshallMode && DMITitle.TrainInSide ? "动车组2" : "动车组1";
                }

            });
            m_ComtrolCollection.Add(new GDIRectText
            {
                Text = "动车组2",
                TextColor = DMITitle.NightMode ? Color.Black : Color.White,
                DrawFont = FontsItems.FontC11,
                OutLineRectangle = new Rectangle(480, 75, 130, 30),
                NeedDarwOutline = false,
                Tag = 2,
                Visible = false,
                RefreshAction = o =>
                {
                    ((GDIRectText) o).Text = DMITitle.MarshallMode && DMITitle.TrainInSide ? "动车组1" : "动车组2";
                }
            });
            m_ComtrolCollection.Add(new Line(new Point(478, 75), new Point(478, 464))
            {
                LinePen = new Pen(DMITitle.NightMode ? Color.Black : Color.White, 1),
                Visible = false,
                Tag = 1
            });
            DMITitle.MarshallModeChanged += m =>
            {
                m_ComtrolCollection[m_ComtrolCollection.FindIndex(f => f is Line && (int) f.Tag == 1)].Visible =
                    DMITitle.MarshallMode;
                m_ComtrolCollection[m_ComtrolCollection.FindIndex(f => f is GDIRectText && (int) f.Tag == 2)].Visible =
                    DMITitle.MarshallMode;
            };
            int[] tmp = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            m_PowerUpDictionary = tmp.ToDictionary(num => num,
                num => GetInBoolIndex(string.Format("牵引-辅助供电单元--{0}--状态未知", num)));
            if (Coordinate.RectangleFLists(ViewIDName.DMITraction, ref m_RectsList))
            {

            }

            m_RowRankNumbC8.Add(1, new[] {36 + 1, 36 + 6});
            m_RowRankNumbC8.Add(2, new[] {36 + 17, 36 + 22});
            m_RowRankNumbC8.Add(3, new[] {36 + 33, 36 + 38});
            m_RowRankNumbC8.Add(4, new[] {36 + 48, 36 + 50, 36 + 53, 36 + 55});

            m_RowRankNumbC16.Add(1, new[] {36 + 1, 36 + 6, 36 + 9, 36 + 14});
            m_RowRankNumbC16.Add(2, new[] {36 + 17, 36 + 22, 36 + 25, 36 + 30});
            m_RowRankNumbC16.Add(3, new[] {36 + 33, 36 + 38, 36 + 41, 36 + 46});
            m_RowRankNumbC16.Add(4, new[] {36 + 48, 36 + 50, 36 + 53, 36 + 55, 36 + 56, 36 + 58, 36 + 61, 36 + 63});

            m_SwitchStateToImage = new Dictionary<SwitchsState, Image[]>
            {
                {SwitchsState.弓未知, new Image[] {MSImages.未知, MSImages.未知}},
                {SwitchsState.切除弓, new Image[] {MSImages.受电弓切除_0, MSImages.受电弓切除_1}},
                {SwitchsState.升弓, new Image[] {MSImages.升弓_0, MSImages.升弓_1}},
                {SwitchsState.降弓, new Image[] {MSImages.降弓_0, MSImages.降弓_1}},
                {SwitchsState.主断未知, new Image[] {MSImages.未知, MSImages.未知}},
                {SwitchsState.主断切除, new Image[] {MSImages.主断切除_0, MSImages.主断切除_1}},
                {SwitchsState.主断闭合, new Image[] {MSImages.主断合_0, MSImages.主断合_1}},
                {SwitchsState.主断断开, new Image[] {MSImages.主断断_0, MSImages.主断断_1}},
                {SwitchsState.车顶隔离开关未知, new Image[] {MSImages.未知, MSImages.未知}},
                {SwitchsState.车顶隔离开关切除, new Image[] {MSImages.车顶隔离切除_0, MSImages.车顶隔离切除_1}},
                {SwitchsState.车顶隔离开关闭合, new Image[] {MSImages.车顶隔离合_0, MSImages.车顶隔离合_1}},
                {SwitchsState.车顶隔离开关断开, new Image[] {MSImages.车顶隔离断_0, MSImages.车顶隔离断_1}},
                {SwitchsState.牵引变流器未知, new Image[] {MSImages.未知, MSImages.未知}},
                {SwitchsState.牵引变流器切除, new Image[] {MSImages.主断切除_0, MSImages.主断切除_1}},
                {SwitchsState.牵引变流器闭合, new Image[] {MSImages.主断合_0, MSImages.主断合_1}},
                {SwitchsState.牵引变流器断开, new Image[] {MSImages.主断断_0, MSImages.主断断_1}},
                {SwitchsState.辅助供电蓝, new Image[] {MSImages.xfk_0_0, MSImages.xfk_1_0}},
                {SwitchsState.辅助供电黄, new Image[] {MSImages.xfk_0_1, MSImages.xfk_1_1}},
                {SwitchsState.辅助供电黑, new Image[] {MSImages.xfk_0_2, MSImages.xfk_1_2}}
            };
        }
    }
}