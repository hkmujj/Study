using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Engine.HMI.SS3B.Interface;
using Engine.HMI.SS3B.View.Annotations;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// MasterGraduationTwo.xaml 的交互逻辑
    /// </summary>
    public partial class MasterGraduationTwo : UserControl, INotifyPropertyChanged
    {
        public MasterGraduationTwo()
        {
            InitializeComponent();
        }
        public void Init(IGraduationResouce souce)
        {
            this.GraduationLineOne.GraduationResouce = souce;
            Souce = souce;
        }
        public IGraduationResouce Souce
        {
            get { return m_Souce; }
            set { m_Souce = value; RaisePropertyChanged("Souce"); }
        }
        public static readonly DependencyProperty BarValueTwoOneProperty = DependencyProperty.Register(
          "BarValueTwoOne", typeof(double), typeof(MasterGraduationTwo), new PropertyMetadata(default(double)));

        public double BarValueTwoOne
        {
            get { return (double)GetValue(BarValueTwoOneProperty); }
            set { SetValue(BarValueTwoOneProperty, value); }
        }

        public static readonly DependencyProperty BarValueTwoTwoProperty = DependencyProperty.Register(
            "BarValueTwoTwo", typeof(double), typeof(MasterGraduationTwo), new PropertyMetadata(default(double)));

        private IGraduationResouce m_Souce;

        public double BarValueTwoTwo
        {
            get { return (double)GetValue(BarValueTwoTwoProperty); }
            set { SetValue(BarValueTwoTwoProperty, value); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
