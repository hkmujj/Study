using System.ComponentModel.Composition.Hosting;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
   //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetContentActionResponser<T> : BtnActionResponserBase
    {
        public T SendEventArg { set; get; }

        public override void ResponseMouseUp()
        {
            GetEvent<T>().Publish(SendEventArg);
        }
    }
}