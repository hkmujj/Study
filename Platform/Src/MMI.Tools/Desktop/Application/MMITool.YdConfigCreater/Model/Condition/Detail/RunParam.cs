namespace MMITool.Addin.YdConfigCreater.Model.Condition.Detail
{
    public class RunParam
    {
        public RunParam()
        {
            AsyncTcp = false;
            AsyncUdp = true;
            AsyncMult = true;
        }

        public bool AsyncTcp { get; private set; }

        public bool AsyncUdp { get; private set; }

        public bool AsyncMult { get; private set; }
    }
}