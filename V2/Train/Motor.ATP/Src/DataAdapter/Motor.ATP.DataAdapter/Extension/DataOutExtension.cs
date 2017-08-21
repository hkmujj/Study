using System;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T;
using Motor.ATP.DataAdapter.Model;
using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Interface.UserAction;
namespace Motor.ATP.DataAdapter.Extension
{
    public static class DataOutExtension
    {
        public static void UpdateDriverInterface(this SignalDataOut dataOut, string interfaceKey)
        {
            dataOut.ATP.DriverInterfaceController.UpdateDriverInterfaceAfterButtonResponsed(DriverInterfaceKey.Parser(interfaceKey));
        }

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var d = new SignalDataOut300T() {ATPVolume = Single.Epsilon, TimeSpan = 1000};

            d.RBCDataBuffer[0] = 'c';

            var d1 = d.Clone();

            d.ATPVolume = float.MaxValue;
            d.RBCDataBuffer[0] = 'd';
        }
    }
    
}