using System;

namespace Engine._6A.Adapter.Adapter.Common
{
    public class TitleAdapter : ModelAdapterBase
    {
        public override void Heart()
        {
            base.Heart();
            Adapter.Model.Title.DateTime = DateTime.Now;
        }

        public TitleAdapter(IEngineAdapter adapter) : base(adapter)
        {
        }
    }
}