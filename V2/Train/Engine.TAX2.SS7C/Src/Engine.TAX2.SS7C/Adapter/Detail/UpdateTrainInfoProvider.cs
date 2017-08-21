using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TAX2.SS7C.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTrainInfoProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateTrainInfoProvider(IEventAggregator eventAggregator, SS7CViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }


        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ti = ViewModel.TrainInfoViewModel.Model;

            args.ChangedFloats.UpdateIfContains(Inf.主界面_机车速度, f => ti.CurrentSpeed = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_运行级位, f => ti.RunningLevel = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_轴位, f => ti.AxisLocation = (int) f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_轴温, f => ti.AxisTemperature = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电压U1, f => ti.EleV1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电压U2, f => ti.EleV2 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_励磁电流i1, f => ti.MagneticCuttingI1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_励磁电流i2, f => ti.MagneticCuttingI2 = f);

            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I1, f => ti.EleI1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I2, f => ti.EleI2 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I3, f => ti.EleI3 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I4, f => ti.EleI4 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I5, f => ti.EleI5 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I6, f => ti.EleI6 = f);

            //TODO  如果有不同需要增加1个 EmergEleV
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电压U1限制, f => ti.EmergEleV = f);
            //TODO 同上
            args.ChangedFloats.UpdateIfContains(Inf.主界面_电机电流I1限制, f => ti.EmergEleI = f);
            //TODO 同上
            args.ChangedFloats.UpdateIfContains(Inf.主界面_励磁电流i1限制, f => ti.EmergMagneticCuttingI = f);

            args.ChangedFloats.UpdateIfContains(Inf.主界面_列供1电流, f => ti.PowerSupplyUnit1.Current = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_列供1电压, f => ti.PowerSupplyUnit1.Voltage = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_列供2电流, f => ti.PowerSupplyUnit2.Current = f);
            args.ChangedFloats.UpdateIfContains(Inf.主界面_列供2电压, f => ti.PowerSupplyUnit2.Voltage = f);

            args.ChangedFloats.UpdateIfContains(Inf.主界面_磁削系数, f => ti.MagneticCuttingRatio = f);

            args.ChangedBools.UpdateIfContains(InB.主界面_削磁系数_显示, b => ti.IsMagneticCuttingRatioVisible = b);

            args.ChangedBools.UpdateIfContains(InB.辅助系统_有信息,
                b => ti.AssistSystemInfoState = b ? AssistSystemInfoState.HasInfo : AssistSystemInfoState.None);

            ti.FeedbackFlag = GetFeedbackFlag();

            ti.WorkState = GetWorkState();

            ti.CutoffState1 = GetCutoffState(InB.主界面_一架正常, InB.主界面_一架隔离);
            ti.CutoffState2 = GetCutoffState(InB.主界面_二架正常, InB.主界面_二架隔离);
            ti.CutoffStateLCU = GetCutoffState(InB.主界面_LCU正常, InB.主界面_LCU隔离);

            UpdateTrainInfo2(args);
        }

        private CutoffState GetCutoffState(string noraml, string cutoff)
        {
            var cs = CutoffState.Unkown;

            if (DataService.GetInBoolOf(cutoff))
            {
                cs = CutoffState.Cutoff;
            }
            else if (DataService.GetInBoolOf(noraml))
            {
                cs = CutoffState.Normal;
            }

            return cs;
        }

        private WorkState GetWorkState()
        {
            var ws = WorkState.None;

            if (DataService.GetInBoolOf(InB.主界面_机车工况_制动))
            {
                ws = WorkState.Brake;
            }
            else if (DataService.GetInBoolOf(InB.主界面_机车工况_牵引))
            {
                ws = WorkState.Tow;
            }
            else if (DataService.GetInBoolOf(InB.主界面_机车工况_主断合))
            {
                ws = WorkState.MainSwitchOn;
            }
            else if (DataService.GetInBoolOf(InB.主界面_机车工况_蓄电池合))
            {
                ws = WorkState.PowerOn;
            }

            return ws;
        }

        private void UpdateTrainInfo2(CommunicationDataChangedArgs args)
        {
            var ts2 = ViewModel.TrainInfoViewModel.Model.ItemCollection;
            if (ts2.IsValueCreated)
            {
                foreach (var it in ts2.Value.Where(w => !string.IsNullOrWhiteSpace(w.Item.IndexName)))
                {
                    args.ChangedBools.UpdateIfContains(it.Item.IndexName,
                        b => it.AbnormalState = b ? AbnormalState.Abnormal : AbnormalState.Normal);
                }
            }
        }

        private FeedbackFlag GetFeedbackFlag()
        {
            var ff = FeedbackFlag.None;
            if (DataService.GetInBoolOf(InB.加馈标志_加馈))
            {
                ff = FeedbackFlag.Feedback;
            }
            else if (DataService.GetInBoolOf(InB.加馈标志_未加馈))
            {
                ff = FeedbackFlag.NoFeedback;
            }
            return ff;
        }
    }
}