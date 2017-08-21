namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// ÎÄ¼þ¶ÁÐ´Àà
    /// </summary>
    public class IoHelper : IIoHelper
    {

        #region ::::::::::::::::::::::::::::::::  attribute  ::::::::::::::::::::::::::::::::

        public bool IsOpen { get; set; }

        public string LinkPath { get; set; }

        #endregion

        #region ::::::::::::::::::::::::::::::::  structure  ::::::::::::::::::::::::::::::::
        public IoHelper()
        {
            IsOpen = false;
            LinkPath = string.Empty;
        }

        #endregion


        #region ::::::::::::::::::::::::::::::::   method    ::::::::::::::::::::::::::::::::

        public virtual void Open() { }

        public virtual void Close() { }

        public virtual T Select<T>(string keyStr, string keyName, string defaultValue = "NULL") where T : class { return default(T); }

        public virtual int Updata(string keyStr, string keyName, string value) { return 0; }

        public virtual int Inset(string keyStr, string keyName, string value) { return 0; }

        public virtual int Delete(string keyStr, string keyName) { return 0; }

        #endregion

    }
}
