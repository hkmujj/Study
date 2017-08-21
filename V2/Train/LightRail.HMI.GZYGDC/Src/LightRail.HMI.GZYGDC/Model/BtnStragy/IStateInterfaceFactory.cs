using System.Collections.Generic;
using System.Collections.ObjectModel;
using LightRail.HMI.GZYGDC.Model.ConfigModel;

namespace LightRail.HMI.GZYGDC.Model.BtnStragy
{
    public interface IStateInterfaceFactory
    {
        /// <summary>
        /// 获得一个用户接口
        /// </summary>
        /// <param name="interfaceKey"></param>
        /// <returns></returns>
        IStateInterface GetOrCreate(StateInterfaceKey interfaceKey, List<StateInterfaceItem> StateInterfaceCollection); 
    }
}