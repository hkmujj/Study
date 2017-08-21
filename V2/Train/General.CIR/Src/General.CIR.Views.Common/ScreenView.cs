using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace General.CIR.Views.Common
{
	public partial class ScreenView : UserControl
	{
		public static readonly DependencyProperty BackBrushProperty = DependencyProperty.Register("BackBrush", typeof(SolidColorBrush), typeof(ScreenView), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

		public static readonly DependencyProperty ForBrushProperty = DependencyProperty.Register("ForBrush", typeof(SolidColorBrush), typeof(ScreenView), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

		public static readonly DependencyProperty BorderThickProperty = DependencyProperty.Register("BorderThick", typeof(Thickness), typeof(ScreenView), new PropertyMetadata(new Thickness(1.0)));

		public static readonly DependencyProperty BorBrushProperty = DependencyProperty.Register("BorBrush", typeof(SolidColorBrush), typeof(ScreenView), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

	

		public SolidColorBrush BackBrush
		{
			get
			{
				return (SolidColorBrush)GetValue(BackBrushProperty);
			}
			set
			{
				SetValue(BackBrushProperty, value);
			}
		}

		public SolidColorBrush ForBrush
		{
			get
			{
				return (SolidColorBrush)GetValue(ForBrushProperty);
			}
			set
			{
				SetValue(ForBrushProperty, value);
			}
		}

		public Thickness BorderThick
		{
			get
			{
				return (Thickness)GetValue(BorderThickProperty);
			}
			set
			{
				SetValue(BorderThickProperty, value);
			}
		}

		public SolidColorBrush BorBrush
		{
			get
			{
				return (SolidColorBrush)GetValue(BorBrushProperty);
			}
			set
			{
				SetValue(BorBrushProperty, value);
			}
		}

		public ScreenView()
		{
			InitializeComponent();
		}

	
	}
}
