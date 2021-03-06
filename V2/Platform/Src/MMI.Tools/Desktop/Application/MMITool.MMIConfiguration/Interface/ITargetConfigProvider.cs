﻿namespace MMITool.Addin.MMIConfiguration.Interface
{
    public interface ITargetConfigProvider
    {
        string TargetConfigFile { get; set; }

        /// <summary>
        /// 文件类型描述
        /// </summary>
        string FileTypeDescription { get; }
    }
}