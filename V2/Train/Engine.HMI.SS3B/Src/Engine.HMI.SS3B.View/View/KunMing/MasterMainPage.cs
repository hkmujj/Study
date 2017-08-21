using System.Collections.Generic;
using System.ComponentModel.Composition;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.Model;
using Engine.HMI.SS3B.View.ViewModel.KunMing;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.HMI.SS3B.View.View.KunMing
{
    /// <summary>
    /// MasterMainPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = KunMingRegionNames.ViewContent, IsDefaultView = true)]
    public partial class MasterMainPage
    {
        [ImportingConstructor]
        public MasterMainPage(SS3BViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            InitSouce();
        }

        private void InitSouce()
        {
            var souce = new Dictionary<double, double>()
            {
                {46, 200},
                {36, 400},
                {26, 600},
                {16, 800},
                {6, 1000},
            };
            var tmp = new LineResouceTwo()
            {
                MaxValue = 1100,
                LongLength = 0.2,
                ShortLength = 0.1,
                Contrast = 4,
                Enmergency = 800,
                LineCount = 55,
                Souce = souce
            };
            One.Init(new LineResouceOne()
            {
                MaxValue = 2250,
                LongLength = 0.2,
                ShortLength = 0.1,
                Contrast = 4,
                Enmergency = 1750,
                LineCount = 45,
                Souce = new Dictionary<double, double>()
                {
                    {36, 500},
                    {26, 1000},
                    {16, 1500},
                    {6, 2000},
                }
            });
            Two.Init(tmp);
            //tmp = new LineResouceTwo()
            //{
            //    MaxValue = 1200,
            //    LongLength = 1,
            //    ShortLength = 0.7,
            //    Contrast = 4,
            //    Enmergency = 760,
            //    LineCount = 60,
            //    Souce = souce
            //};
            Three.Init(tmp);
            //tmp = new LineResouceTwo()
            //{
            //    MaxValue = 1200,
            //    LongLength = 1,
            //    ShortLength = 0.7,
            //    Contrast = 4,
            //    Enmergency = 760,
            //    LineCount = 60,
            //    Souce = souce
            //};
            Frou.Init(tmp);
        }
    }
}
