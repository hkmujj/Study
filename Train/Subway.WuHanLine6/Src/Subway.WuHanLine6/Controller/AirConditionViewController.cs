using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Models.Model;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 空调状态界面控制
    /// </summary>
    [Export]
    public class AirConditionViewController : ControllerBase<AirConditionModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AirConditionViewController()
        {
            ModelSetting = new DelegateCommand<string>(ModelSettingAction);
            SettingValue = new DelegateCommand<string>(SettingValueAction);
            TrainSelct = new DelegateCommand<string>(TrainSelctAction);
        }

        private void TrainSelctAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                if (ViewModel.F001Select && ViewModel.F002Select && ViewModel.F003Select && ViewModel.F004Select && ViewModel.F005Select && ViewModel.F006Select)
                {
                    ViewModel.AllTrainSelect = true;
                    ViewModel.F001Select = false;
                    ViewModel.F002Select = false;
                    ViewModel.F003Select = false;
                    ViewModel.F004Select = false;
                    ViewModel.F005Select = false;
                    ViewModel.F006Select = false;
                }
                else
                {
                    ViewModel.AllTrainSelect = false;
                }
            }
            else
            {
                if (ViewModel.AllTrainSelect)
                {
                    ViewModel.F001Select = false;
                    ViewModel.F002Select = false;
                    ViewModel.F003Select = false;
                    ViewModel.F004Select = false;
                    ViewModel.F005Select = false;
                    ViewModel.F006Select = false;
                }
            }
        }

        private void SettingValueAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            var tmpValue = obj.Replace("+", "").Replace("-", "");
            if (obj.Contains("+"))
            {
                Addtion(tmpValue);
            }
            else if (obj.Contains("-"))
            {
                Subtraction(tmpValue);
            }
        }

        private void Addtion(string value)
        {
            var tmpValue = int.Parse(value);
            if (ViewModel.AllTrainSelect)
            {
                SetOne(tmpValue, true);
                SetTwo(tmpValue, true);
                SetThree(tmpValue, true);
                SetFour(tmpValue, true);
                SetFive(tmpValue, true);
                SetSix(tmpValue, true);
            }
            else
            {
                if (ViewModel.F001Select)
                {
                    SetOne(tmpValue, true);
                }
                if (ViewModel.F002Select)
                {
                    SetTwo(tmpValue, true);
                }
                if (ViewModel.F003Select)
                {
                    SetThree(tmpValue, true);
                }
                if (ViewModel.F004Select)
                {
                    SetFour(tmpValue, true);
                }
                if (ViewModel.F005Select)
                {
                    SetFive(tmpValue, true);
                }
                if (ViewModel.F006Select)
                {
                    SetSix(tmpValue, true);
                }
            }
        }

        private void Subtraction(string value)
        {
            var tmpValue = int.Parse(value);
            if (ViewModel.AllTrainSelect)
            {
                SetOne(tmpValue, false);
                SetTwo(tmpValue, false);
                SetThree(tmpValue, false);
                SetFour(tmpValue, false);
                SetFive(tmpValue, false);
                SetSix(tmpValue, false);
            }
            else
            {
                if (ViewModel.F001Select)
                {
                    SetOne(tmpValue, false);
                }
                if (ViewModel.F002Select)
                {
                    SetTwo(tmpValue, false);
                }
                if (ViewModel.F003Select)
                {
                    SetThree(tmpValue, false);
                }
                if (ViewModel.F004Select)
                {
                    SetFour(tmpValue, false);
                }
                if (ViewModel.F005Select)
                {
                    SetFive(tmpValue, false);
                }
                if (ViewModel.F006Select)
                {
                    SetSix(tmpValue, false);
                }
            }
        }

        /// <summary>
        /// 设置1车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetOne(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF001 += value;
            }
            else
            {
                ViewModel.SettingValueF001 -= value;
            }
        }

        /// <summary>
        /// 设置2车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetTwo(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF002 += value;
            }
            else
            {
                ViewModel.SettingValueF003 -= value;
            }
        }

        /// <summary>
        /// 设置3车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetThree(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF003 += value;
            }
            else
            {
                ViewModel.SettingValueF003 -= value;
            }
        }

        /// <summary>
        /// 设置4车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetFour(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF004 += value;
            }
            else
            {
                ViewModel.SettingValueF004 -= value;
            }
        }

        /// <summary>
        /// 设置5车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetFive(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF005 += value;
            }
            else
            {
                ViewModel.SettingValueF005 -= value;
            }
        }

        /// <summary>
        /// 设置6车值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag">加减法Flag true 加法 false 减法</param>
        private void SetSix(int value, bool flag)
        {
            if (flag)
            {
                ViewModel.SettingValueF006 += value;
            }
            else
            {
                ViewModel.SettingValueF006 -= value;
            }
        }

        private void ModelSettingAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            //TODO 模式转换
            switch (obj)
            {
                case "":
                    break;
            }
        }

        private AirRunModel m_CurrentModel;

        /// <summary>
        /// 值设置
        /// </summary>
        public ICommand SettingValue { get; private set; }

        /// <summary>
        /// 模式设置
        /// </summary>
        public ICommand ModelSetting { get; private set; }

        /// <summary>
        /// 选择
        /// </summary>
        public ICommand TrainSelct { get; private set; }
    }
}