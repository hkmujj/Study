using System;

namespace ES.Facility.PublicModule.ExportData
{
    public class StringConfig : ConfigValueBase<String, String>, IConfigValueBase<String, String>
    {
        #region :::::::::::::::::::::::::    override method    ::::::::::::::::::::::::::::
        public override void clear()
        {
            base.clear();
        }

        public override bool decodeFromString(string cString, ref int tmpErrorIndex)
        {
            return base.decodeFromString(cString, ref tmpErrorIndex);
        }

        public override bool initParaListFromString(ref int tmpErrorIndex)
        {
            return base.initParaListFromString(ref tmpErrorIndex);
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::

        #endregion
    }
}
