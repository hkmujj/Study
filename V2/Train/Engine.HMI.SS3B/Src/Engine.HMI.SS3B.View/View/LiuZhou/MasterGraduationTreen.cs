using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Engine.HMI.SS3B.Interface;
using Engine.HMI.SS3B.View.Annotations;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// MasterGraduationTreen.xaml 的交互逻辑
    /// </summary>
    public partial class MasterGraduationTreen : UserControl, INotifyPropertyChanged
    {
        public MasterGraduationTreen()
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
        public static readonly DependencyProperty BarValueOneProperty = DependencyProperty.Register(
          "BarValueOne", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        public double BarValueOne
        {
            get { return (double)GetValue(BarValueOneProperty); }
            set { SetValue(BarValueOneProperty, value); }
        }

        public static readonly DependencyProperty BarValueTwoProperty = DependencyProperty.Register(
            "BarValueTwo", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        public double BarValueTwo
        {
            get { return (double)GetValue(BarValueTwoProperty); }
            set { SetValue(BarValueTwoProperty, value); }
        }

        public static readonly DependencyProperty BarValueThreeProperty = DependencyProperty.Register(
            "BarValueThree", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        public double BarValueThree
        {
            get { return (double)GetValue(BarValueThreeProperty); }
            set { SetValue(BarValueThreeProperty, value); }
        }

        public static readonly DependencyProperty BarValueFourProperty = DependencyProperty.Register(
            "BarValueFour", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        public double BarValueFour
        {
            get { return (double)GetValue(BarValueFourProperty); }
            set { SetValue(BarValueFourProperty, value); }
        }

        public static readonly DependencyProperty BarValueFiveProperty = DependencyProperty.Register(
            "BarValueFive", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        public double BarValueFive
        {
            get { return (double)GetValue(BarValueFiveProperty); }
            set { SetValue(BarValueFiveProperty, value); }
        }

        public static readonly DependencyProperty BarValueSixProperty = DependencyProperty.Register(
            "BarValueSix", typeof(double), typeof(MasterGraduationTreen), new PropertyMetadata(default(double)));

        private IGraduationResouce m_Souce;

        public double BarValueSix
        {
            get { return (double)GetValue(BarValueSixProperty); }
            set { SetValue(BarValueSixProperty, value); }
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
