using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Project;

// ReSharper disable once CheckNamespace
namespace MMI.Facility.PublicUI.Interface
{
    public interface IMainForm : IMainBaseForm
    {
        /// <summary>
        /// 视图窗口
        /// </summary>
        ReadOnlyCollection<ProjectFormBase> ViewForms { get; }

        /// <summary>
        /// 数据
        /// </summary>
        IShareForm ShareForm { get; }

        /// <summary>
        /// 工程信息
        /// </summary>
        IProjectListInfoForm ProjectListInfoForm { get; }

        /// <summary>
        /// 逻辑信息
        /// </summary>
        ILogicForm LogicForm { get; }

        /// <summary>
        /// 对象属性
        /// </summary>
        IAttributeForm AttributeForm { get; }

        void ShowAllWindows();

        IShareForm CreateShareForm();

        IProjectListInfoForm CreateProjectListInfoForm();

        ILogicForm CreateLogicForm();

        IAttributeForm CreateAttributeForm();

        IEnumerable<IViewForm> CreateViewForms();

        void Initalize();

    }
}
