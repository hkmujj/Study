namespace CRH2MMI.PowerClassify
{
    class PowerClassify380A : PowerClassify2C
    {
        public PowerClassify380A(PowerTpe powerTpe) : base(powerTpe)
        {
        }

        public override void Init()
        {
            base.Init();
            
            InitalizeSetButon();
        }
    }
}
