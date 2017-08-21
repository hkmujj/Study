using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model;
using Urban.Domain.TrainState.Model.Common;
using Urban.NanJing.NingTian.DDU.Flash.Model.Train;
using Urban.NanJing.NingTian.DDU.Flash.Model.Train.Car;
using Urban.NanJing.NingTian.DDU.Resource;
using Urban.NanJing.NingTian.DDU.Resource.Internal;

namespace Urban.NanJing.NingTian.DDU.Flash.Model
{
    public class NingTianDDUDomain : TrainBase, IDataListener
    {
        public NingTianDDUDomain(CommunicationIndexFacade indexFacade)
        {
            m_UpdateParam.IndexFacade = indexFacade;

            Updating += DomainOnUpdating;
            Speed.Updating += SpeedOnUpdating;
            ATP.Updating += ATPOnUpdating;
            Power.ContactLinePower.Updating += ContactLinePowerOnUpdating;

            InitalizeTrainBraking();

            InitalizeCar();
        }

        private void InitalizeCar()
        {
            Cars =
                Enum.GetValues(typeof (CarType))
                    .Cast<CarType>()
                    .Select(s => new NingTianCar(s))
                    .Cast<CarBase>()
                    .ToList()
                    .AsReadOnly();
        }

        private void ContactLinePowerOnUpdating(ContactLinePower target, IUpdateParam updateParam)
        {
            target.ContactLineVoltage = updateParam.GetInFloatAt(InFloatKeys.网压);
        }

        private void InitalizeTrainBraking()
        {
            BrakingCollection = new ReadOnlyCollection<Braking>(new List<Braking>()
            {
                new Braking(BrakingState.Unkown, BrakingLevel.None, BrakingType.Emergent)
            });

            BrakingCollection[0].Updating += TrainBrakeOnUpdating;
        }

        private void TrainBrakeOnUpdating(Braking target, IUpdateParam updateParam)
        {
            target.BrakingState = BrakingState.Unkown;
            if (updateParam.GetInBoolAt(InBoolKeys.紧急制动状态未知))
            {
                target.BrakingState = BrakingState.Unkown;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.紧急制动未激活状态))
            {
                target.BrakingState = BrakingState.Relase;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.紧急制动激活状态))
            {
                target.BrakingState = BrakingState.Apply;
            }
        }

        private void ATPOnUpdating(ATP target, IUpdateParam updateParam)
        {
            target.ATPState = ATPState.Unkown;
            if (updateParam.GetInBoolAt(InBoolKeys.ATC与ATP均正常绿色))
            {
                target.ATPState = ATPState.Noraml;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.ATO不工作橙色))
            {
                target.ATPState = ATPState.Abnormal;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.ATP故障或隔离ATC红色))
            {
                target.ATPState = ATPState.Fault;
            }

            target.WorkModel = ATPModel.ModelUnconnect;
            if (updateParam.GetInBoolAt(InBoolKeys.MODE断开位))
            {
                target.WorkModel = ATPModel.ModelUnconnect;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.WASH激活洗车黄色))
            {
                target.WorkModel = ATPModel.Wash;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.RMR激活限速向后黄色))
            {
                target.WorkModel = ATPModel.RMR;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.RMF激活限速向前黄色))
            {
                target.WorkModel = ATPModel.RMF;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.CM激活手动绿色))
            {
                target.WorkModel = ATPModel.CM;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.ATOM激活自动驾驶绿色))
            {
                target.WorkModel = ATPModel.ATOM;
            }
            if (updateParam.GetInBoolAt(InBoolKeys.紧急牵引橙色))
            {
                target.WorkModel = ATPModel.EmergenceTow;
            }
        }

        private void DomainOnUpdating(TrainBase target, IUpdateParam updateParam)
        {
            target.Visible = updateParam.GetInBoolAt(InBoolKeys.黑屏);
        }

        private void SpeedOnUpdating(Speed target, IUpdateParam updateParam)
        {
            target.CurrentSpeed.Value = updateParam.GetInFloatAt(InFloatKeys.列车当前速度);

            target.LimitedSpeed.Value = updateParam.GetInBoolAt(InBoolKeys.无速度限制信息)
                ? float.NaN
                : updateParam.GetInFloatAt(InFloatKeys.限制速度);
        }

        public void OnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            
        }
    }
}