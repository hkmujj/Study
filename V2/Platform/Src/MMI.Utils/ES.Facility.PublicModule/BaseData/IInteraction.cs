using System;
namespace ES.Facility.PublicModule.BaseData
{
    [CLSCompliant(false)]
    public interface IInteraction
    {
        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::

        /// <summary>
        /// 数据编号 用于某些 需要ID识别的场合
        /// </summary>
        int Id { get; set;}

        /// <summary>
        /// 区分是托管还是非托管数据
        /// </summary>
        DataUseType DataMode { get; set; }

        /// <summary>
        /// 非托管数据存储区
        /// </summary>
        unsafe SInteraction SInteractionData { get; set; }

        /// <summary>
        /// 托管数据类型识别编号
        /// </summary>
        DataFormType ObjectDataType { get; set;}

        /// <summary>
        /// 托管数据类型识别编号
        /// </summary>
        int ObjectDataExType { get; set;}

        /// <summary>
        /// 托管数据存储区
        /// </summary>
        object ObjectData { get; set; }

        #endregion

        #region ::::::::::::::::::::::::::::::::  method    ::::::::::::::::::::::::::::::::

        #endregion
    }
}
