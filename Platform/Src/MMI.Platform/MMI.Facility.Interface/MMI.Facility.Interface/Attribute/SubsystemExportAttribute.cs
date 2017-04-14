using System;
using System.ComponentModel.Composition;
using MMI.Facility.Interface.Project;

namespace MMI.Facility.Interface.Attribute
{
    /// <summary>
    /// 子系统导出
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SubsystemExportAttribute : ExportAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private SubsystemExportAttribute()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractName"></param>
        public SubsystemExportAttribute(string contractName) : base(contractName, typeof (ISubsystem))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractType"></param>
        public SubsystemExportAttribute(Type contractType) : base(contractType.FullName, typeof (ISubsystem))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractName"></param>
        /// <param name="contractType"></param>
        protected SubsystemExportAttribute(string contractName, Type contractType) : base(contractName, contractType)
        {
        }
    }
}