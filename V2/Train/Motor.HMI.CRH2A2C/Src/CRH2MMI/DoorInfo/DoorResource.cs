using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Resource.Images;

namespace CRH2MMI.DoorInfo
{
    internal class DoorResource
    {
        private readonly Door m_Door;

        public static DoorResource Instance { private set; get; }

        private Dictionary<DoorInBoolKeyModel, int> m_InBoolIndexDic;

        public DoorResource(Door door)
        {
            m_Door = door;
            Instance = this;
            m_InBoolIndexDic =
                GlobalInfo.CurrentCRH2Config.DoorConfig.DoorInBoolModels.ToDictionary(
                    s => new DoorInBoolKeyModel() {CarNo = s.CarNo, Location = s.DoorLocation, OperType = s.Type},
                    s => GlobalResource.Instance.GetInBoolIndex(s.InBoolColoumNames.First()));
        }

        public List<int> GetNotUseIndexList()
        {
            switch (GlobalInfo.CurrentCRH2Config.Type)
            {
                case CRH2Type.CRH2A:
                    return new List<int> { 7, 8, 8 + 4, 16 + 7, 24 + 0, 24 + 4 };
                case CRH2Type.CRH2B:
                    return new List<int>();
                case CRH2Type.CRH2C:
                    return new List<int>(){8 + 6, 24 + 6};
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Image GetDoorImage(DoorState state)
        {
            Image img = null;
            switch (state)
            {
                case DoorState.Open:
                    img = ImageResource.door2;
                    break;
                case DoorState.Close:
                    img = ImageResource.door3;
                    break;
                case DoorState.Press:
                    break;
                case DoorState.Relase:
                    break;
                case DoorState.Fault:
                    break;
                case DoorState.Cut:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            //todo
            return img;
        }

        /// <summary>
        /// 4个元素
        ///  1：关    0：开
        ///  1：压紧    0：松
        ///  故障
        ///  切除
        /// </summary>
        /// <param name="trainNo"></param>
        /// <param name="doorLocation"></param>
        /// <returns></returns>
        public List<DoorState> GetDoorStates(int trainNo, DoorLocation doorLocation)
        {
            int offset;
            switch (doorLocation)
            {
                case DoorLocation.Door1:
                    offset = 8;
                    break;
                case DoorLocation.Door2:
                    offset = 0;
                    break;
                case DoorLocation.Door3:
                    offset = 12;
                    break;
                case DoorLocation.Door4:
                    offset = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("doorLocation");
            }

            offset += trainNo * 16;

            var rlt = new List<DoorState> { DoorState.Open, DoorState.Relase, DoorState.Null, DoorState.Null };

            if (GetBoolValue(offset))
            {
                rlt[0] = DoorState.Close;
            }
            if (GetBoolValue(offset + 1))
            {
                rlt[1] = DoorState.Press;
            }
            if (GetBoolValue(offset + 2))
            {
                rlt[2] = DoorState.Fault;
            }
            if (GetBoolValue(offset + 3))
            {
                rlt[3] = DoorState.Cut;
            }

            return rlt;
        }

        public DoorState GetDoorStates(DoorInBoolKeyModel model)
        {
            DoorState rlt;
            var bValue = m_Door.BoolList[m_InBoolIndexDic[model]];
            
            {
                switch (model.OperType)
                {
                    case DoorOperType.PressRelase:
                        rlt = bValue ? DoorState.Press : DoorState.Relase;
                        break;
                    case DoorOperType.OpenClose:
                        rlt = bValue ? DoorState.Close : DoorState.Open;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return rlt;
        }

        private bool GetBoolValue(int offset)
        {
            return m_Door.BoolList[m_Door.UIObj.InBoolList[offset]];
        }
    }
}
