using System;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// 定义文件的打开、关闭、读取接口
    /// </summary>
    /// <remarks>文件操作接口</remarks>
    public interface IFileOperate
    {
        /// <summary>
        /// 文件系统为路径
        /// 数据库系统则为连接字
        /// </summary>
        String LinkPath { get; set;}

        /// <summary>
        /// 打开文件或连接
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭文件或连接
        /// </summary>
        void Close();
    }


    /// <summary>
    /// 定义数据操作方式
    /// </summary>
    public interface IDataOperate
    {
        /// <summary>
        /// 查询
        /// </summary>
        T Select<T>(string keyStr, string keyName, string defaultValue="NULL") where T : class;

        /// <summary>
        /// 更新
        /// </summary>
        int Updata(string keyStr, string keyName, string value);

        /// <summary>
        /// 插入
        /// </summary>
        int Inset(string keyStr, string keyName, string value);

        /// <summary>
        /// 删除
        /// </summary>
        int Delete(string keyStr, string keyName);
    }

    /// <summary>
    /// 外部数据操作模式
    /// </summary>
    public interface IIoHelper : IFileOperate, IDataOperate
    {

        #region 属性

        /// <summary>
        /// 文件状态是否为开启
        /// </summary>
        /// <value>
        /// true 开启
        /// false 关闭
        /// </value>
        bool IsOpen { get;  set;  }
        
        #endregion
    }

}//end ns
