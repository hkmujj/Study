using System;

namespace General.FlashLoader.Subsystem.Interface
{
    public interface IFlashInteractive : IDisposable
    {
        void SetValue(FlashCommandType cmdType, string value);

        event FlashDataEventHandler FlashDataRevceived;
    }
}