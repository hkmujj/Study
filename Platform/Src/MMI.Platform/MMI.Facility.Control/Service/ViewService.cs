using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.Running;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Facility.View.Views;
using MMI.Facility.View.Views.ActualContentHost;

namespace MMI.Facility.Control.Service
{
    public class ViewService : IRunningViewService
    {
        private readonly ObservableCollection<ProjectFormBase> m_AllViewFormCollection;
        private readonly ObservableCollection<ProjectFormBase> m_ActivedFormCollection;

        public ReadOnlyCollection<ProjectFormBase> AllViewFormCollection { private set; get; }

        public ReadOnlyCollection<ProjectFormBase> ActivedFormCollection { private set; get; }

        public event Action<IRunningViewService, NotifyCollectionChangedAction> AcitvedFormChanged;
        public event Action<IRunningViewService, NotifyCollectionChangedEventArgs> AllViewFormChanged;


        public event Action<IViewService, ReadOnlyCollection<IScreenItem>> CurrentScreenItemsChanged;

        private Form m_ViewMdiParent;

        private ReadOnlyCollection<IScreenItem> m_CurrentReadolyScreenItems;

        private List<IScreenItem> m_CurrentScreenItems;
        /// <summary>
        /// 当前屏元素表。
        /// </summary>
        public List<IScreenItem> CurrentScreenItems
        {
            get { return m_CurrentScreenItems; }
            private set
            {
                m_CurrentScreenItems = value;
                m_CurrentReadolyScreenItems = new ReadOnlyCollection<IScreenItem>(value);
            }
        }

        /// <summary>
        /// 当前屏元素表。
        /// </summary>
        ReadOnlyCollection<IScreenItem> IViewService.CurrentScreenItems
        {
            get { return m_CurrentReadolyScreenItems; }
        }


        public IDataPackage DataPackage { private set; get; }

        public ViewService()
        {
            m_AllViewFormCollection = new ObservableCollection<ProjectFormBase>();
            AllViewFormCollection = new ReadOnlyCollection<ProjectFormBase>(m_AllViewFormCollection);
            m_ActivedFormCollection = new ObservableCollection<ProjectFormBase>();
            ActivedFormCollection = new ReadOnlyCollection<ProjectFormBase>(m_ActivedFormCollection);

            m_ActivedFormCollection.CollectionChanged += (sender, args) => OnAcitvedFormChanged(args);

            m_AllViewFormCollection.CollectionChanged += (sender, args) => OnAllViewFormChanged(args);

            CurrentScreenItems = new List<IScreenItem>();
        }


        public void Initalize(ViewServiceInitalizeParam initalizeParam)
        {
            DataPackage = initalizeParam.DataPackage;
            m_ViewMdiParent = initalizeParam.ViewParent;
            foreach (var item in m_AllViewFormCollection)
            {
                item.MdiParent = m_ViewMdiParent;
            }

            foreach (
                var item in
                    initalizeParam.DataPackage.Config.AppConfigs.Where(w => w.SubsystemType == SubsystemType.Addin)
                        .Select(appConfig => new ViewFormActual(appConfig.AppName, initalizeParam.DataPackage)
                        {
                            MdiParent = initalizeParam.ViewParent,
                            ShowInTaskbar = true,
                        }))
            {
                m_AllViewFormCollection.Add(item);
            }

            var actives = initalizeParam.DataPackage.Config.SystemConfig.SubsystemConfigCollection.Where(w => w.NeedLoad).Select(s => s.Name).ToList();

            var allViewProjectNames = m_AllViewFormCollection.Select(s => s.ProjectName).ToList();
            if (allViewProjectNames.Except(actives).Count() != allViewProjectNames.Count - actives.Count)
            {
                LogMgr.Warn("All registted view is not contains all needed active views.");
            }
            LogMgr.Info("All registted view is {0}",
                string.Join("\t",
                    AllViewFormCollection.Select(s => string.Format("Name={0},Type={1}", s.ProjectName, s.GetType()))));
            LogMgr.Info("All need active view is {0}", string.Join("\t", actives));

            foreach (var active in m_AllViewFormCollection.Where(w => actives.Contains(w.ProjectName)))
            {
                SetFormPropertyByConfig(active);

                AddActiveView(active);
            }
        }



        public void Active(string projectName)
        {
            var view = m_AllViewFormCollection.FirstOrDefault(f => f.ProjectName == projectName);
            if (view == null)
            {
                LogMgr.Error(string.Format("There is no view which project name = {0}, can not active it.", projectName));
                return;
            }

            AddActiveView(view);
        }

        private void AddActiveView(ProjectFormBase view)
        {
            if (m_ActivedFormCollection.Contains(view))
            {
                LogMgr.Debug(string.Format("has active view which project name is {0}, ignore this active operate.",
                    view.ProjectName));
                return;
            }

            m_ActivedFormCollection.Add(view);
        }

        public void Active(string projectName, Point location)
        {
            var view = m_AllViewFormCollection.FirstOrDefault(f => f.ProjectName == projectName);
            if (view == null)
            {
                LogMgr.Error(string.Format("There is no view which project name = {0}, can not active it.", projectName));
                return;
            }

            if (view.InvokeRequired)
            {
                view.Invoke(new Action(() =>
                {
                    view.Left = location.X;
                    view.Top = location.Y;
                }));
            }
            else
            {
                view.Left = location.X;
                view.Top = location.Y;
            }
            

            AddActiveView(view);
        }

        public void Deactive(string projectName)
        {
            var view = m_ActivedFormCollection.FirstOrDefault(f => f.ProjectName == projectName);
            if (view == null)
            {
                LogMgr.Debug(string.Format("there is no active view which project name is {0} to deactive.", projectName));
                return;
            }

            m_ActivedFormCollection.Remove(view);
        }

        public void Regist(string projectName, ProjectFormBase form)
        {
            var view = m_AllViewFormCollection.FirstOrDefault(f => f.ProjectName == projectName);
            if (view != null)
            {
                LogMgr.Warn(string.Format("Can not regist projcet({0}) more than once,Ignore this regist action. ", projectName));
            }
            else
            {
                LogMgr.Debug(string.Format("Regist project({0}) success, recorded the view of the projct.", projectName));
                form.MdiParent = m_ViewMdiParent;
                
                m_AllViewFormCollection.Add(form);
            }
        }

        /// <summary>
        /// 更新当前显示的界面元素
        /// </summary>
        /// <param name="screenItems"></param>
        public void UpdateCurrentScreenItems(IEnumerable<IScreenItem> screenItems)
        {
            var old = new List<IScreenItem>(CurrentScreenItems);

            CurrentScreenItems.Clear();

            CurrentScreenItems.AddRange(screenItems);

            var all = CurrentScreenItems.SelectMany(s => s.ProjectCollection).ToList();

            var todeletes = m_ActivedFormCollection.Select(s => s.AppName).Except(all).ToList();
            var toadds = all.Except(m_ActivedFormCollection.Select(s => s.AppName)).ToList();

            foreach (var todelet in todeletes)
            {
                Deactive(todelet);
            }

            foreach (var toadd in toadds)
            {
                Active(toadd);
            }

            OnCurrentScreenItemsChanged(old.AsReadOnly());
        }

        private void UpdateViewFormProperties(NotifyCollectionChangedEventArgs args)
        {
            if (DataPackage == null)
            {
                return;
            }

            foreach (ProjectFormBase form in args.NewItems)
            {
                SetFormPropertyByConfig(form);
            }
        }

        private void SetFormPropertyByConfig(ProjectFormBase form)
        {
            var appConfig = DataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == form.ProjectName);
            if (appConfig == null)
            {
                LogMgr.Warn("Can not found app config of name={0}, will not set registed view location be app config.",
                    form.ProjectName);
            }
            else
            {
                if (appConfig.ActureFormConfig != null)
                {
                    SysLog.Info("Set registed view location by app config , the region={0}",
                        appConfig.ActureFormConfig.Rectangle);
                    form.Left = appConfig.ActureFormConfig.Rectangle.Left;
                    form.Top = appConfig.ActureFormConfig.Rectangle.Top;
                    form.Size = appConfig.ActureFormConfig.Rectangle.Size;
                    form.MdiParent = m_ViewMdiParent;
                    form.TopMost = appConfig.ActureFormConfig.TopMost;
                    if (!appConfig.ActureFormConfig.IsCourseVisible)
                    {
                        form.MouseEnter += ActiveFormOnMouseEnter;
                        form.MouseLeave += ActiveFormOnMouseLeave;
                    }
                }
                else
                {
                    SysLog.Warn(
                        "The ActureFormConfig is null where app={0}, can not set registed view's location by app config",
                        appConfig.AppName
                        );
                }
                form.Text = appConfig.AppName;
                form.FormBorderStyle = DataPackage.Config.SystemConfig.IsDebugModel ? FormBorderStyle.Sizable : FormBorderStyle.None;
            }

            form.Load += FormOnLoad;
        }


        private void FormOnLoad(object sender, EventArgs eventArgs)
        {

        }

        private void ActiveFormOnMouseEnter(object sender, EventArgs eventArgs)
        {
            Cursor.Hide();
        }

        private void ActiveFormOnMouseLeave(object sender, EventArgs eventArgs)
        {
            Cursor.Show();
        }

        protected virtual void OnCurrentScreenItemsChanged(ReadOnlyCollection<IScreenItem> oldItems)
        {
            if (CurrentScreenItemsChanged != null)
            {
                CurrentScreenItemsChanged(this, oldItems);
            }
        }
        private void OnAcitvedFormChanged(NotifyCollectionChangedEventArgs args)
        {
            var handler = AcitvedFormChanged;
            if (handler != null)
            {
                handler(this, args.Action);
            }
        }            

        private void OnAllViewFormChanged(NotifyCollectionChangedEventArgs args)
        {
            UpdateViewFormProperties(args);

            var handler = AllViewFormChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

    }
}