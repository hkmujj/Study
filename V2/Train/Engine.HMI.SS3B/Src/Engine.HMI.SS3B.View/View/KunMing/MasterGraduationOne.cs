using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Engine.HMI.SS3B.Interface;

namespace Engine.HMI.SS3B.View.View.KunMing
{
    /// <summary>
    /// MasterGraduationOne.xaml 的交互逻辑
    /// </summary>
    public partial class MasterGraduationOne : UserControl, INotifyPropertyChanged
    {
        public MasterGraduationOne()
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

        public static readonly DependencyProperty BarValueOneOneProperty = DependencyProperty.Register(
            "BarValueOneOne", typeof(double), typeof(MasterGraduationOne), new PropertyMetadata(default(double)));

        public double BarValueOneOne
        {
            get { return (double) GetValue(BarValueOneOneProperty); }
            set { SetValue(BarValueOneOneProperty, value); }
        }

        public static readonly DependencyProperty BarValueOneTwoProperty = DependencyProperty.Register(
            "BarValueOneTwo", typeof(double), typeof(MasterGraduationOne), new PropertyMetadata(default(double)));

        public double BarValueOneTwo
        {
            get { return (double) GetValue(BarValueOneTwoProperty); }
            set { SetValue(BarValueOneTwoProperty, value); }
        }

        public static readonly DependencyProperty BarValueOneThreeProperty = DependencyProperty.Register(
            "BarValueOneThree", typeof(double), typeof(MasterGraduationOne), new PropertyMetadata(default(double)));

        public double BarValueOneThree
        {
            get { return (double) GetValue(BarValueOneThreeProperty); }
            set { SetValue(BarValueOneThreeProperty, value); }
        }

        public static readonly DependencyProperty BarValueOneFourProperty = DependencyProperty.Register(
            "BarValueOneFour", typeof(double), typeof(MasterGraduationOne), new PropertyMetadata(default(double)));

        private IGraduationResouce m_Souce;

        public double BarValueOneFour
        {
            get { return (double) GetValue(BarValueOneFourProperty); }
            set { SetValue(BarValueOneFourProperty, value); }
        }

  

        public event PropertyChangedEventHandler PropertyChanged;

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
