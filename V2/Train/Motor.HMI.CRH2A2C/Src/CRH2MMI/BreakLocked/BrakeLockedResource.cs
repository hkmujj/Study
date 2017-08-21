using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;

namespace CRH2MMI.BreakLocked
{
    /// <summary>
    /// 抱死资源
    /// </summary>
    internal class BrakeLockedResource
    {

        private readonly Hold m_Hold;

        private Dictionary<BrakeLockedSendKeyModel, int> m_OutBoolIdxDic;

        public BrakeLockedResource(Hold hold)
        {
            m_Hold = hold;
            Dictionary<BrakeLockedSendKeyModel, int> dictionary = new Dictionary<BrakeLockedSendKeyModel, int>();
            foreach (BreakLockedSendModel model in GlobalInfo.CurrentCRH2Config.BrakeLockedConfig.BreakLockedSendModels)
            {
                dictionary.Add(new BrakeLockedSendKeyModel()
                               {
                                   CarNo = model.CarNo, LockedLocation = model.LockedLocation, Type = model.Type,
                               }, GlobalResource.Instance.GetOutBoolIndex(model.OutBoolColoumNames.First()));
            }
            m_OutBoolIdxDic = dictionary;
        }


        /// <summary>
        /// 获得配置文件第 idx 个bool的值 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private bool GetBoolOfIdx(int idx)
        {
            return m_Hold.BoolList[m_Hold.UIObj.InBoolList[idx]];
        }

        /// <summary>
        /// 获得抱死状态
        /// </summary>
        /// <param name="trainNo">车箱号</param>
        /// <param name="roller">轮子, 0 : 抱死1 1:抱死2</param>
        /// <returns></returns>
        public LockState GetRollerLockState(int trainNo, int roller)
        {
            var offset = trainNo * 4 + roller * 2;
            if (GetBoolOfIdx(offset + 1))
            {
                return LockState.Fault;
            }
            if (GetBoolOfIdx(offset))
            {
                return LockState.Cut;
            }
            return LockState.Normal;
        }

        public SolidBrush GetLockStateSolidBrush(LockState state)
        {
            switch (state)
            {
                case LockState.Normal:
                    return CommonResouce.WhiteBrush;
                case LockState.Cut:
                    return CRH2Resource.YellowBrush;
                case LockState.Fault:
                    return CRH2Resource.RedBrush;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public void SetBreakLockedState(BrakeLockedSendKeyModel keyModel, int value)
        {
            m_Hold.append_postCmd(CmdType.SetBoolValue, GetSetStateIndex(keyModel), value, 0);
        }

        /// <summary>
        /// 获得设置状态的索引
        /// </summary>
        /// <returns></returns>
        private int GetSetStateIndex(BrakeLockedSendKeyModel keyModel)
        {
            return m_OutBoolIdxDic[keyModel];
        }
    }
}
