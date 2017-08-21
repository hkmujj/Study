using System;
using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Common.View.Train
{
    internal class TrainResource
    {
        private readonly TrainView m_TrainView;

        public static TrainResource Instance { private set; get; }

        public CRH2TrainConfig TrainConfig { private set; get; }

        public TrainResource(TrainView trainView)
        {
            m_TrainView = trainView;

            Instance = this;

            Init();
        }

        private void Init()
        {
            TrainConfig = GlobalInfo.CurrentCRH2Config.TrainConfig;
        }

        /// <summary>
        /// 受电弓的状态
        /// </summary>
        /// <param name="acceptEle"></param>
        /// <returns></returns>
        public AcceptEleArcState GetAcceptEleArcState(AcceptEleArc acceptEle)
        {
            var acc = TrainConfig.AcceptEleCarConfigs.Find(f => f.CarNo == acceptEle.CarNo && f.Location == acceptEle.AngleBracketLocation && f.Direction == acceptEle.Direction);
            if (acc != null)
            {
                return m_TrainView.GetInBoolValue(acc.InBoolColoumNames.First()) ? AcceptEleArcState.Up : AcceptEleArcState.Down;
            }
            throw new ArgumentException(string.Format("There is no Accept ele arc of CarNo={0}", acceptEle));
        }

        /// <summary>
        /// 动车的状态
        /// </summary>
        /// <param name="carConfig"></param>
        /// <returns></returns>
        public CarState GetMoveCarState(CarConfig carConfig)
        {
            var rlt = CarState.Normal;
            if (m_TrainView.GetInBoolValue(carConfig.InBoolColoumNames[0]))
            {
                rlt = CarState.Normal;
            }
            if (m_TrainView.GetInBoolValue(carConfig.InBoolColoumNames[1]))
            {
                rlt = CarState.Pull;
            }
            if (m_TrainView.GetInBoolValue(carConfig.InBoolColoumNames[2]))
            {
                rlt = CarState.EleBreak;
            }
            return rlt;
        }

        public bool IsDriverRoom(CarConfig carConfig)
        {
            return m_TrainView.GetInBoolValue(carConfig.InBoolColoumNames.First());
        }

        public TrainUnitState GetUnitState(TrainUnitNo unitNo)
        {
            var unitConfig = TrainConfig.TrainUnits[(int) unitNo];
            if (m_TrainView.GetInBoolValue(unitConfig.InBoolColoumNames[2]))
            {
                return TrainUnitState.Fault;
            }

            if (m_TrainView.GetInBoolValue(unitConfig.InBoolColoumNames[0]))
            {
                return TrainUnitState.Unknown;
            }

            if (m_TrainView.GetInBoolValue(unitConfig.InBoolColoumNames[1]))
            {
                return TrainUnitState.Nomal;
            }

            return TrainUnitState.Unknown;
        }
    }
}
