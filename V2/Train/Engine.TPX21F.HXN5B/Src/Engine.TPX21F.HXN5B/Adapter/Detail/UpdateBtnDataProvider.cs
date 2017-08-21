using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TPX21F.HXN5B.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateBtnDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateBtnDataProvider(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            if (args.RaiseDataChangedType == RaiseCommunicationDataChangedType.ByUserManul)
            {
                return;
            }

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_1按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B1MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B1Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B1MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_2按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B2MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B2Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B2MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_3按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B3MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B3Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B3MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_4按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B4MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B4Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B4MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_5按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B5MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B5Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B5MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_6按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B6MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B6Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B6MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_7按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B7MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B7Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B7MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_8按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B8MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B8Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B8MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_9按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B9MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B9Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B9MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏下方_0按下, b =>
            {
                if (b)
                {
                    ViewModel.HardwareBtnViewModel.Model.B10MouseDownCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.B10Command.Execute(null);
                    ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_C按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_上按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.UpCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_下按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.DownCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_左按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.LeftCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_右按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.RightCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏右方_Enter按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.OKCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_温度键按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BTemperatureCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_液晶屏背光关断按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    //ViewModel.HardwareBtnViewModel.Model.UpCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_背光调暗按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BLightDownCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_背光调亮按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BLightUpCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_日夜亮度切换按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BContrastCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_音量调小按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BSoundDownCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_音量调大按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BSoundUpCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_事件信息按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BContextCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_处理信息按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BExclamationCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });

            args.ChangedBools.UpdateIfContains(InBKeys.MMI屏上方_帮助按下, b =>
            {
                if (b)
                {
                    //ViewModel.HardwareBtnViewModel.Model.BReturnCommand.Execute(null);
                }
                else
                {
                    ViewModel.HardwareBtnViewModel.Model.BQuestionMarkCommand.Execute(null);
                    //ViewModel.HardwareBtnViewModel.Model.B10MouseUpCommand.Execute(null);
                }
            });
        }
    }
}