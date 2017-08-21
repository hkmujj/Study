using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch.Detail;
using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    public class ChangeSoftwareActionResponserBase : BtnActionResponserBase
    {
        protected SoftSwitchModel SoftSwitchModel
        {
            get { return ViewModel.Value.Domain.SoftSwitchViewModel.Model; }
        }

        protected void ChangeItem(int page, int index, bool value = true)
        {
            ReadOnlyCollection<SoftSwitchItem> pagItems = null;

            switch (page)
            {
                case 0:
                    pagItems = SoftSwitchModel.Page1.Value;
                    break;
                case 1:
                    pagItems = SoftSwitchModel.Page2.Value;
                    break;
                case 2:
                    pagItems = SoftSwitchModel.Page3.Value;
                    break;
            }

            if (pagItems != null && pagItems.Count > index)
            {
                var it = pagItems[index];

                if (it != null && it.ItemConfig.OutLogicIndex != null)
                {
                    EventAggregator.GetEvent<SendDataRequestEvent>()
                        .Publish(new SendDataRequestEvent.Args(it.ItemConfig.OutLogicIndex, value));
                }
            }
        }
    }


    [Export]
    public class ChangeSoftwarePage2It00ActionResponser : ChangeSoftwareActionResponserBase
    {

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 0);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 0, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It01ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 1);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 1, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It02ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 2);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 2, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It03ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 3);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 3, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It04ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 4);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 4, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It05ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 5);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 5, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It06ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 6);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 6, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It07ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 7);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 7, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It08ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 8);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 8, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It09ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 9);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 9, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage2It10ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(2, 10);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(2, 10, false);
        }
    }


    [Export]
    public class ChangeSoftwarePage1It00ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 0);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 0, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It01ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 1);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 1, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It02ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 2);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 2, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It03ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 3);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 3, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It04ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 4);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 4, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It05ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 5);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 5, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It06ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 6);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 6, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It07ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 7);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 7, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It08ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 8);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 8, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It09ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 9);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 9, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage1It10ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(1, 10);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(1, 10, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It00ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 0);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 0, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It01ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 1);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 1, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It02ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 2);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 2, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It03ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 3);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 3, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It04ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 4);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 4, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It05ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 5);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 5, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It06ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 6);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 6, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It07ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 7);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 7, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It08ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 8);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 8, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It09ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 9);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 9, false);
        }
    }

    [Export]
    public class ChangeSoftwarePage0It10ActionResponser : ChangeSoftwareActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            ChangeItem(0, 10);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            ChangeItem(0, 10, false);
        }
    }
}
