using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.HMI.SS3B.CommonView
{
    /// <summary>
    /// FaultContent.xaml 的交互逻辑
    /// </summary>
    public partial class FaultContent : UserControl
    {
        public FaultContent()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty BackBrushProperty = DependencyProperty.Register(
          "BackBrush", typeof(SolidColorBrush), typeof(FaultContent), new PropertyMetadata(default(SolidColorBrush), ChangeBack));

        private static void ChangeBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).OnChangeBack(e);
        }

        void OnChangeBack(DependencyPropertyChangedEventArgs e)
        {
            this.LayoutRoot.Background = BackBrush;
        }
        public SolidColorBrush BackBrush
        {
            get { return (SolidColorBrush)GetValue(BackBrushProperty); }
            set { SetValue(BackBrushProperty, value); }
        }

        public static readonly DependencyProperty ForeGroundOneProperty = DependencyProperty.Register(
            "ForeGroundOne", typeof(SolidColorBrush), typeof(FaultContent), new PropertyMetadata(default(SolidColorBrush), ForeGroundOneChange));

        private static void ForeGroundOneChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).ChangForeGroundOne(e);
        }

        void ChangForeGroundOne(DependencyPropertyChangedEventArgs e)
        {
            this.TextBlockOne.Foreground = ForeGroundOne;
        }
        public SolidColorBrush ForeGroundOne
        {
            get { return (SolidColorBrush)GetValue(ForeGroundOneProperty); }
            set { SetValue(ForeGroundOneProperty, value); }
        }

        public static readonly DependencyProperty ForeGroundTwoProperty = DependencyProperty.Register(
            "ForeGroundTwo", typeof(SolidColorBrush), typeof(FaultContent), new PropertyMetadata(default(SolidColorBrush), ForeGroundTwoChange));

        private static void ForeGroundTwoChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).ChangForeGroundTwo(e);
        }
        void ChangForeGroundTwo(DependencyPropertyChangedEventArgs e)
        {
            this.TextBlockTwo.Foreground = ForeGroundTwo;
        }
        public SolidColorBrush ForeGroundTwo
        {
            get { return (SolidColorBrush)GetValue(ForeGroundTwoProperty); }
            set { SetValue(ForeGroundTwoProperty, value); }
        }

        public static readonly DependencyProperty ForedGroundThreeProperty = DependencyProperty.Register(
            "ForedGroundThree", typeof(SolidColorBrush), typeof(FaultContent), new PropertyMetadata(default(SolidColorBrush), ForeGroundThreeChange));

        private static void ForeGroundThreeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).ChangForeGroundThree(e);
        }
        void ChangForeGroundThree(DependencyPropertyChangedEventArgs e)
        {
            this.TextBlockThree.Foreground = ForedGroundThree;
        }
        public SolidColorBrush ForedGroundThree
        {
            get { return (SolidColorBrush)GetValue(ForedGroundThreeProperty); }
            set { SetValue(ForedGroundThreeProperty, value); }
        }
        public static readonly DependencyProperty ContentOneProperty = DependencyProperty.Register(
          "ContentOne", typeof(string), typeof(FaultContent), new PropertyMetadata(default(string), ContentOneChanged));

        private static void ContentOneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).CntentOne(e);
        }

        void CntentOne(DependencyPropertyChangedEventArgs e)
        {
            TextBlockOne.Text = ContentOne;
        }
        public string ContentOne
        {
            get { return (string)GetValue(ContentOneProperty); }
            set { SetValue(ContentOneProperty, value); }
        }

        public static readonly DependencyProperty ContenTwoProperty = DependencyProperty.Register(
            "ContenTwo", typeof(string), typeof(FaultContent), new PropertyMetadata(default(string), ContentTwoChanged));
        private static void ContentTwoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).CntentTwo(e);
        }
        void CntentTwo(DependencyPropertyChangedEventArgs e)
        {
            TextBlockTwo.Text = ContenTwo;
        }
        public string ContenTwo
        {
            get { return (string)GetValue(ContenTwoProperty); }
            set { SetValue(ContenTwoProperty, value); }
        }

        public static readonly DependencyProperty ContentThreeProperty = DependencyProperty.Register(
            "ContentThree", typeof(string), typeof(FaultContent), new PropertyMetadata(default(string), ContentThreeChanged));
        private static void ContentThreeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultContent)d).CntentThree(e);
        }
        void CntentThree(DependencyPropertyChangedEventArgs e)
        {
            TextBlockThree.Text = ContentThree;
        }
        public string ContentThree
        {
            get { return (string)GetValue(ContentThreeProperty); }
            set { SetValue(ContentThreeProperty, value); }
        }
    }
}
