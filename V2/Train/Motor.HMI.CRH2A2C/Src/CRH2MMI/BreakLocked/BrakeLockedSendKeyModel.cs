namespace CRH2MMI.BreakLocked
{
    class BrakeLockedSendKeyModel
    {
        public CtrolType Type { set; get; }

        public int LockedLocation { set; get; }

        public int CarNo { set; get; }

        public override string ToString()
        {
            return string.Format("{0}车厢,抱死{1},{2}", CarNo, LockedLocation, Type);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ToString().GetHashCode().Equals(obj.ToString().GetHashCode());
        }
    }
}
