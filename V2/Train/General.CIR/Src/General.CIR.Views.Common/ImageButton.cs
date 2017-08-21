using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using General.CIR.Commands.ScreenBtnResponse;

namespace General.CIR.Views.Common
{
	public class ImageButton : Button
	{
		public static readonly DependencyProperty DelaySecondProperty;

		public static readonly DependencyProperty DownImageProperty;

		public static readonly DependencyProperty UpImageProperty;

		public static readonly DependencyProperty DownCommandProperty;

		public static readonly DependencyProperty UpCommandProperty;

		public int DelaySecond
		{
			get
			{
				return (int)GetValue(DelaySecondProperty);
			}
			set
			{
				SetValue(DelaySecondProperty, value);
			}
		}

		public ImageSource DownImage
		{
			get
			{
				return (ImageSource)GetValue(DownImageProperty);
			}
			set
			{
				SetValue(DownImageProperty, value);
			}
		}

		public ImageSource UpImage
		{
			get
			{
				return (ImageSource)GetValue(UpImageProperty);
			}
			set
			{
				SetValue(UpImageProperty, value);
			}
		}

		public ICommand DownCommand
		{
			get
			{
				return (ICommand)GetValue(DownCommandProperty);
			}
			set
			{
				SetValue(DownCommandProperty, value);
			}
		}

		public ICommand UpCommand
		{
			get
			{
				return (ICommand)GetValue(UpCommandProperty);
			}
			set
			{
				SetValue(UpCommandProperty, value);
			}
		}

		static ImageButton()
		{
			DelaySecondProperty = DependencyProperty.Register("DelaySecond", typeof(int), typeof(ImageButton), new PropertyMetadata(0));
			DownImageProperty = DependencyProperty.Register("DownImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
			UpImageProperty = DependencyProperty.Register("UpImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
			DownCommandProperty = DependencyProperty.Register("DownCommand", typeof(ICommand), typeof(ImageButton), new PropertyMetadata(null));
			UpCommandProperty = DependencyProperty.Register("UpCommand", typeof(ICommand), typeof(ImageButton), new PropertyMetadata(null));
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
		}

		protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
			base.OnPreviewMouseDown(e);
			Task.Factory.StartNew(delegate(object s)
			{
				object[] array = s as object[];
				Thread.Sleep(new TimeSpan(0, 0, (int)array[0]));
				bool flag = (int)array[0] == 0;
				if (flag)
				{
					ICommand expr_35 = (ICommand)array[1];
					if (expr_35 != null)
					{
						expr_35.Execute(null);
					}
				}
				else
				{
					bool flag2 = e.ButtonState == MouseButtonState.Pressed;
					if (flag2)
					{
						ICommand expr_61 = (ICommand)array[1];
						if (expr_61 != null)
						{
							expr_61.Execute(null);
						}
					}
				}
			}, new object[]
			{
				DelaySecond,
				DownCommand
			});
			BtnResponseBase.OperateTime = DateTime.Now;
		}

		protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
		{
			ICommand expr_07 = UpCommand;
			if (expr_07 != null)
			{
				expr_07.Execute(null);
			}
			base.OnPreviewMouseUp(e);
		}
	}
}
