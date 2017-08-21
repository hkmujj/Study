using System.Windows;

namespace CRH2TrainTypeSelector
{
    class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public StartupEventArgs StartupEventArgs { set; get; }
    }
}
