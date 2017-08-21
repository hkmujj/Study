using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;

namespace Motor.ATP.Domain.Interface.UserAction
{
    [DebuggerDisplay("Id = {Id}")]
    public sealed class DriverInterfacePlaceholder : NotificationObject, IDriverInterface
    {
        public IATP ATP { get; private set; }
        public IDriverInterface Parent { get; private set; }
        public ReadOnlyCollection<IDriverInterface> Chirldren { get; private set; }
        public DriverInterfaceKey Id { get; set; }
        public IDriverPopupView PopupView { get; private set; }
        public IDriverSelectable DriverSelectable { get; private set; }
        public Stack<IDriverInterface> LastInterfaceStack { get; private set; }
        public void SetParent(IDriverInterface driverInterface)
        {
            throw new NotSupportedException();
        }

        public bool NavigateToThis(IDriverInterface lastInterface)
        {
            throw new NotSupportedException();
        }

        public bool NavigateFromThis()
        {
            throw new NotSupportedException();
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            throw new NotImplementedException();
        }
    }
}