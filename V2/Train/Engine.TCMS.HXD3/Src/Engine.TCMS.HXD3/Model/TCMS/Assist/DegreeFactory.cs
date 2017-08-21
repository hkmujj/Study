using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.TCMS.HXD3.Model.TCMS.Assist
{
    public class DegreeFactory
    {
        public static readonly DegreeFactory Instance = new DegreeFactory();

        private DegreeFactory()
        {
            RawSideEle = CreateDegrees(50);

            RawSideVol = CreateDegrees(40);

            ControlVol = CreateDegrees(20);

            Tow = CreateDegrees(20);
        }

        private ObservableCollection<DegreeItem> CreateDegrees(int count)
        {
            var des = new ObservableCollection<DegreeItem>(
                Enumerable.Range(0, count + 1)
                    .Select(
                        (s, i) => new DegreeItem {Scal = 98d*s/count, Width = i%10 == 0 ? 15 : 8, Stoke = 1})
                    .ToList());
            des[0].Width = 20;
            des.Last().Width = 20;
            des.Last().Scal = 98;
            return des;
        }

        /// <summary>
        /// 原边电流
        /// </summary>
        public ObservableCollection<DegreeItem> RawSideEle { private set; get; }

        /// <summary>
        /// 原边电压
        /// </summary>
        public ObservableCollection<DegreeItem> RawSideVol { private set; get; }

        /// <summary>
        /// 控制电压
        /// </summary>
        public ObservableCollection<DegreeItem> ControlVol { private set; get; }

        /// <summary>
        /// 牵引
        /// </summary>
        public ObservableCollection<DegreeItem> Tow { private set; get; }
    }
}