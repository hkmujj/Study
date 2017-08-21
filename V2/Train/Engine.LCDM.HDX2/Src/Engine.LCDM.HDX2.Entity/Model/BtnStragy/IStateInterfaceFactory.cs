namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    public interface IStateInterfaceFactory
    {
        /// <summary>
        /// 获得一个用户接口
        /// </summary>
        /// <param name="interfaceKey"></param>
        /// <returns></returns>
        IStateInterface GetOrCreate(StateInterfaceKey interfaceKey); 
    }
}