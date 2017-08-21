using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.HMI.SS3B.CommonView;
using Engine.HMI.SS3B.View.Model;
using Engine.HMI.SS3B.View.ViewModel.KunMing;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// TestFaultxaml.xaml 的交互逻辑
    /// </summary>
    public partial class TestFaultxaml : UserControl
    {
        private readonly Dictionary<int, FaultContent> m_ControlDictionary;
        private SS3BViewModel m_SS3BViewModel;
        private List<MessageInfo> m_MessageInfos;
        public TestFaultxaml(SS3BViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            m_ControlDictionary = new Dictionary<int, FaultContent>();
            InitControl();

        }
        public TestFaultxaml()
        {
            InitializeComponent();
            m_ControlDictionary = new Dictionary<int, FaultContent>();
            InitControl();

        }
         public static readonly DependencyProperty InfosProperty = DependencyProperty.Register(
            "Infos", typeof (List<MessageInfo>), typeof (TestFaultxaml), new FrameworkPropertyMetadata(default(List<MessageInfo>),FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,InfoChnged));

        public List<MessageInfo> Infos
        {
            get { return (List<MessageInfo>) GetValue(InfosProperty); }
            set { SetValue(InfosProperty, value); }
        }
        private static void InfoChnged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TestFaultxaml)d).OnInfoChnged(e);
        }

        private void OnInfoChnged(DependencyPropertyChangedEventArgs e)
        {
            MessageInfos = Infos;
        }
        public List<MessageInfo> MessageInfos
        {
            get { return m_MessageInfos; }
            set
            {
                if (m_MessageInfos == value)
                {
                    return;
                }
                m_MessageInfos = value;
                InitContent();
            }
        }

        void InitContent()
        {
            for (int i = 0; i < 15; i++)
            {
                m_ControlDictionary[i].ContentOne = string.Empty;
                m_ControlDictionary[i].ContenTwo = string.Empty;
                m_ControlDictionary[i].ContentThree = string.Empty;
            }
            for (int i = 0; i < MessageInfos.Count; i++)
            {
                m_ControlDictionary[i].ContentOne = MessageInfos[i].Level;
                m_ControlDictionary[i].ContenTwo = MessageInfos[i].Content;
                m_ControlDictionary[i].ContentThree = MessageInfos[i].Time.ToString("yy-MM-dd hh:mm:ss");
            }

        }
        private void InitControl()
        {

            var brush1 = new SolidColorBrush(Color.FromArgb(255, 140, 156, 181));
            var brush2 = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            for (int i = 0; i < 15; i++)
            {
                var tmp = new FaultContent { BackBrush = i % 2 == 0 ? brush2 : brush1 };
                Grid.SetRow(tmp, i + 2);
                Grid.SetColumn(tmp, 0);
                Grid.SetColumnSpan(tmp, 12);
                m_ControlDictionary.Add(i, tmp);
                Grid.Children.Add(tmp);
            }
            var rec = new Rectangle()
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 1,
            };
            Grid.SetRow(rec, 1);
            Grid.SetColumn(rec, 0);
            Grid.SetRowSpan(rec, 17);
            Grid.Children.Add(rec);
            var rec1 = new Rectangle()
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 1,
            };
            Grid.SetRow(rec1, 1);
            Grid.SetColumn(rec1, 1);
            Grid.SetRowSpan(rec1, 17);
            Grid.SetColumnSpan(rec1, 6);
            Grid.Children.Add(rec1);
            var rec2 = new Rectangle()
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 1,
            };
            Grid.SetRow(rec2, 1);
            Grid.SetColumn(rec2, 7);
            Grid.SetRowSpan(rec2, 17);
            Grid.SetColumnSpan(rec2, 5);
            Grid.Children.Add(rec2);
        }

        private void TestFaultxaml_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) && !((bool)e.OldValue))
            {
                Infos = ((SS3BViewModel)DataContext).MessageMgr.GetDeafauList().ToList();
            }
        }

       
    }
}
