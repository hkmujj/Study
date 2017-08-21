using System.ComponentModel.Composition;
using MMI.Facility.Interface.Data;

namespace Subway.CBTC.BeiJiaoKong.Adapter
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    [Export]
    public class BeiJiaoKongAdapter : AdapterBase
    {

        /// <summary>
        /// 课程结束清除数据
        /// </summary>
        public override void Clear()
        {

        }

        /// <summary>
        /// 课程初始化
        /// </summary>
        public override void Initialize()
        {

        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public override void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            //dataChangedArgs.NewValue.IfContain(InBoolKeys.InBMMI屏黑屏, b => ViewModel.MMIBlack = b ? Visibility.Visible : Visibility.Hidden);
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public override void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> datatChangedArgs)
        {
            //datatChangedArgs.NewValue.IfContain("目标距离",f=>ViewModel.MainViewModel.RegionAViewModel.TagertSpeed = f);
        }
        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public override void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
/*            
            dataChangedArgs.ChangedBools.NewValue.IfContain(InBoolKeys.InBMMI屏黑屏, b => ViewModel.MMIBlack = b ? Visibility.Visible : Visibility.Hidden);

            dataChangedArgs.ChangedFloats.NewValue.IfContain("目标距离", f => ViewModel.MainViewModel.RegionAViewModel.TagertDistance = f);

            var index = GlobalParams.Instance.ProjectIndexDescription.InFloatDescriptionDictionary["目标距离"];
            var index2 = GlobalParams.Instance.ProjectIndexDescription.InFloatDescriptionDictionary["目标点速度"];
            var value1 = GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.GetFloatAt(index);
            var value2 = GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.GetFloatAt(index2);
            if (dataChangedArgs.ChangedFloats.NewValue.ContainsKey(index))
            {
                var newValue = dataChangedArgs.ChangedFloats.NewValue[index];
                var oldValue = dataChangedArgs.ChangedFloats.OldValue[index];
            }


            if (value1 > 100)
            {
                ViewModel.MainViewModel.RegionAViewModel.TargetBarType = TargetBarType.None;
            }
            else
            {
                ViewModel.MainViewModel.RegionAViewModel.TargetBarType = TargetBarType.Red;
            }
 */
        }
    }
}