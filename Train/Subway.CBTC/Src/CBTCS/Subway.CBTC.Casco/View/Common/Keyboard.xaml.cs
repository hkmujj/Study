using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CBTC.Infrasturcture.Events;

namespace Subway.CBTC.Casco.View.Common
{
    /// <summary>
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class Keyboard 
    {
        public Keyboard()
        {
            InitializeComponent();

        }

        public static readonly DependencyProperty InputtedCommandProperty = DependencyProperty.Register(
            "InputtedCommand", typeof(ICommand), typeof(Keyboard), new PropertyMetadata(default(ICommand)));

        public ICommand InputtedCommand
        {
            get { return (ICommand) GetValue(InputtedCommandProperty); }
            set { SetValue(InputtedCommandProperty, value); }
        }




        private void OnInputWord(string word)
        {
            InputWordEvent.EventArgs args = null;
            switch (word)
            {
                case "0":
                    args = new InputWordEvent.EventArgs("0");
                    break;
                case "1":
                    args = new InputWordEvent.EventArgs("1");
                    break;
                case "2":
                    args = new InputWordEvent.EventArgs("2");
                    break;
                case "3":
                    args = new InputWordEvent.EventArgs("3");
                    break;
                case "4":
                    args = new InputWordEvent.EventArgs("4");
                    break;
                case "5":
                    args = new InputWordEvent.EventArgs("5");
                    break;
                case "6":
                    args = new InputWordEvent.EventArgs("6");
                    break;
                case "7":
                    args = new InputWordEvent.EventArgs("7");
                    break;
                case "8":
                    args = new InputWordEvent.EventArgs("8");
                    break;
                case "9":
                    args = new InputWordEvent.EventArgs("9");
                    break;
                case "<-":
                    args = new InputWordEvent.EventArgs("", InputWordEvent.Type.Delete);
                    break;
                case "OK":
                    args = new InputWordEvent.EventArgs("", InputWordEvent.Type.OK);
                    break;
            }

            RaiseInputted(args);
        }

        private void RaiseInputted(InputWordEvent.EventArgs args)
        {
            if (InputtedCommand != null && InputtedCommand.CanExecute(args))
            {
                InputtedCommand.Execute(args);
            }   
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                OnInputWord(btn.Content as string);
            }
        }
    }
}
