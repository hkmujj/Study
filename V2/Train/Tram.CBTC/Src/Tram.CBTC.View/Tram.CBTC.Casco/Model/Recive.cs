using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tram.CBTC.Casco.Model.TempModel;
using Tram.CBTC.Casco.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Recive;

namespace Tram.CBTC.Casco.Model
{
    [Export]
    public class Recive : IRecive
    {
        [Import]
        public Lazy<DomainViewModel> DoMainViewModel;
        /// <summary>
        /// 初始化线路号和站点列表
        /// </summary>
        /// <returns></returns>
        public bool InitEndStationAndRoadID()
        {
            DoMainViewModel.Value.Model.PopModel.EndStationModel.ComboxSelectItemModels = new List<ComboxSelectItemModel>();
            var model1 = new ComboxSelectItemModel();
            model1.Content = "终点站";
            model1.Items = new List<ComboxSelectItemModel>();
            model1.Items.Add(new ComboxSelectItemModel() { Content = "郫县西站" });
            model1.Items.Add(new ComboxSelectItemModel() { Content = "郫县东站" });
            DoMainViewModel.Value.Model.PopModel.EndStationModel.ComboxSelectItemModels.Add(model1);
            var model2 = new ComboxSelectItemModel();
            model2.Content = "路径号";
            model2.Items = new List<ComboxSelectItemModel>();
            model2.Items.Add(new ComboxSelectItemModel() { Content = "1" });
            model2.Items.Add(new ComboxSelectItemModel() { Content = "2" });
            DoMainViewModel.Value.Model.PopModel.EndStationModel.ComboxSelectItemModels.Add(model2);
            DoMainViewModel.Value.Model.PopModel.EndStationModel.SelectItem = DoMainViewModel.Value.Model.PopModel.EndStationModel.ComboxSelectItemModels.FirstOrDefault();
            return true;
        }

        /// <summary>
        /// 初始化可选执行计划，车次号，单程号
        /// </summary>
        /// <returns></returns>
        public bool InitPlan()
        {
            //TODO 添加 初始化计划号相关信息
            DoMainViewModel.Value.Model.PopModel.SelectPlanTrainAndOneWay.SelectItemModels = new List<ComboxSelectItemModel2>();
            var model1 = new ComboxSelectItemModel2();
            model1.Content = "周一计划";
            model1.Items1 = new List<ComboxSelectItemModel2>();
            model1.Items2 = new List<ComboxSelectItemModel2>();
            model1.Items1.Add(new ComboxSelectItemModel2() { Content = "1" });
            model1.Items1.Add(new ComboxSelectItemModel2() { Content = "2" });
            model1.Items2.Add(new ComboxSelectItemModel2() { Content = "3" });
            model1.Items2.Add(new ComboxSelectItemModel2() { Content = "4" });
            var model2 = new ComboxSelectItemModel2();
            model2.Content = "周二计划";
            model2.Items1 = new List<ComboxSelectItemModel2>();
            model2.Items2 = new List<ComboxSelectItemModel2>();
            model2.Items1.Add(new ComboxSelectItemModel2() { Content = "1" });
            model2.Items1.Add(new ComboxSelectItemModel2() { Content = "2" });
            model2.Items2.Add(new ComboxSelectItemModel2() { Content = "3" });
            model2.Items2.Add(new ComboxSelectItemModel2() { Content = "4" });
            DoMainViewModel.Value.Model.PopModel.SelectPlanTrainAndOneWay.SelectItemModels.Add(model1);
            DoMainViewModel.Value.Model.PopModel.SelectPlanTrainAndOneWay.SelectItemModels.Add(model2);
            DoMainViewModel.Value.Model.PopModel.SelectPlanTrainAndOneWay.SelectItem = DoMainViewModel.Value.Model.PopModel.SelectPlanTrainAndOneWay.SelectItemModels.FirstOrDefault();
            return true;
        }
    }
}
