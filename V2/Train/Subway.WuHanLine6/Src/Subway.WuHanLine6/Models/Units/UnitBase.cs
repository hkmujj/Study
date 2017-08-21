using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    ///
    /// </summary>
    public abstract class UnitBase : NotificationObject, ISetValueProvider
    {
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public virtual void SetValue(string propertyOrFieldName, string value)
        {
        }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public abstract void Changed(IDictionary<int, bool> args);
    }
}