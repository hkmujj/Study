using System.Collections.Generic;
using System.ComponentModel.Composition;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.Model;
using Engine.HMI.SS3B.View.ViewModel.KunMing;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// MasterMainPage.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = LiuZhouRegionNames.ViewContent)]
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
                {58, 0},
                {53, 100},
                {48, 200},
                {43, 300},
                {38, 400},
                {33, 500},
                {28, 600},
                {23, 700},
                {18, 800},
                {13, 900},
                {8, 1000},
                {3, 1100},
                {0, 1200}
            };
            var tmp = new LineResouceTwo()
            {
                MaxValue = 1200,
                LongLength = 1,
                ShortLength = 0.7,
                Contrast = 4,
                Enmergency = 760,
                LineCount = 60,
                Souce = souce
            };
            One.Init(new LineResouceOne()
            {
                MaxValue = 2550,
                LongLength = 1,
                ShortLength = 0.7,
                Contrast = 1,
                Enmergency = 1750,
                LineCount = 52,
                Souce = new Dictionary<double, double>()
                {
                    {50, 0},
                    {40, 500},
                    {30, 1000},
                    {20, 1500},
                    {10, 2000},
                    {0, 2500}
                }
            });
            Two.Init(tmp);
            tmp = new LineResouceTwo()
            {
                MaxValue = 1200,
                LongLength = 1,
                ShortLength = 0.7,
                Contrast = 4,
                Enmergency = 760,
                LineCount = 60,
                Souce = souce
            };
            Three.Init(tmp);
            tmp = new LineResouceTwo()
            {
                MaxValue = 1200,
                LongLength = 1,
                ShortLength = 0.7,
                Contrast = 4,
                Enmergency = 760,
                LineCount = 60,
                Souce = souce
            };
            Frou.Init(tmp);
        }
    }
}
