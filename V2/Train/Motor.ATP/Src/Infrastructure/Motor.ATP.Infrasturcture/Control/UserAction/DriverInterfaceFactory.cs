using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class DriverInterfaceFactory : IDriverInterfaceFactory
    {
        private Dictionary<DriverInterfaceKey, IDriverInterface> m_DriverInterfaceDictionary;

        protected string m_ConfigFile;

        protected Type[] m_SerachTypes;

        private readonly IATP m_ATP;

        private readonly string m_OrdinaryActionResponserNamespacePrefix = typeof (EmptyActionResponser).Namespace;

        protected string ActionResponserNamespacePrefix { get; set; }
        protected string StateProviderNamespacePrefix { get; set; }

        protected DriverInterfaceFactory(IATP atp)
        {
            Contract.Requires(atp != null);
            m_ATP = atp;
        }

        protected virtual Dictionary<DriverInterfaceKey, IDriverInterface> BuildDriverInterfaceDictionary()
        {
            if (string.IsNullOrWhiteSpace(m_ConfigFile))
            {
                AppLog.Error("m_ConfigFile IsNullOrWhiteSpace, can not BuildDriverInterfaceDictionary");
            }
            else if (!File.Exists(m_ConfigFile))
            {
                AppLog.Error(string.Format("{0} is not exsist, can not BuildDriverInterfaceDictionary!", m_ConfigFile));
            }
            else
            {
                return BuildDriverInterfaceDictionaryByConfigFile();
            }

            return new Dictionary<DriverInterfaceKey, IDriverInterface>();
        }

        private Dictionary<DriverInterfaceKey, IDriverInterface> BuildDriverInterfaceDictionaryByConfigFile()
        {
            Contract.Requires(m_ConfigFile != null);

            if (Path.GetExtension(m_ConfigFile) != ".xls")
            {
                AppLog.Error(string.Format("{0} is not an .xls file, can not BuildDriverInterfaceDictionary!",
                    m_ConfigFile));
                return new Dictionary<DriverInterfaceKey, IDriverInterface>();
            }
            var config = new ExcelReaderConfig
            {
                Coloumns = new List<ColoumnConfig> {new ColoumnConfig {Name = "*"}},
                File = m_ConfigFile,
                SheetNames = new List<string> {RegionFStructConfigNames.SheetName}
            };
            var dt = config.Adapter();

            var dic = BuildDriverInterfaceDictionaryByDataTable(dt.Tables[0]);

            CorrectParents(dic);

            Initalize(dic);

            return dic;
        }

        private void Initalize(Dictionary<DriverInterfaceKey, IDriverInterface> dic)
        {
            foreach (var drvierSelectableItem in dic.SelectMany(kvp => kvp.Value.DriverSelectable.SelectableItems))
            {
                drvierSelectableItem.Initalize();
            }
        }

        private void CorrectParents(Dictionary<DriverInterfaceKey, IDriverInterface> dic)
        {
            foreach (var driverInterface in dic)
            {
                if (dic.ContainsKey(driverInterface.Value.Parent.Id))
                {
                    driverInterface.Value.SetParent(dic[driverInterface.Value.Parent.Id]);
                }
            }
        }

        protected virtual Dictionary<DriverInterfaceKey, IDriverInterface> BuildDriverInterfaceDictionaryByDataTable(
            DataTable dt)
        {
            var dic = new Dictionary<DriverInterfaceKey, IDriverInterface>(new DriverInterfaceKeyEqualityComparer());
            foreach (DataRow row in dt.Rows)
            {
                var key = DriverInterfaceKey.Parser(row[RegionFStructConfigNames.ColoumnKey].ToString());

                var parent = new DriverInterfacePlaceholder
                {
                    Id = DriverInterfaceKey.Parser(row[RegionFStructConfigNames.ColoumnParentKey].ToString())
                };

                var popupView = BuildDriverPopupViewIfNeed(dt, row, key);

                var driverSelectable = BuildDriverSelectable(dt, row);

                var driverInterface = new DriverInterface(key, driverSelectable, m_ATP, popupView);
                driverInterface.SetParent(parent);

                driverSelectable.Parent = driverInterface;
                dic.Add(key, driverInterface);
            }

            return dic;
        }

        private DriverSelectable BuildDriverSelectable(DataTable dt, DataRow row)
        {
            Contract.Requires(m_SerachTypes != null);

            var items = new List<IDriverSelectableItem>();

            for (int i = 3; i < dt.Columns.Count; i += 4)
            {
                var content = row[i + 1].ToString();
                if (string.IsNullOrWhiteSpace(row[i].ToString()))
                {
                    AppLog.Error(
                        string.Format(
                            "Can not parser user action type 【{0}】 in cell [{1},{2}], ignore the coloumns behind this coloumn.",
                            row[i], row[0], dt.Columns[i].ColumnName));
                    break;
                }
                var type = (UserActionType) Enum.Parse(typeof (UserActionType), row[i].ToString());
                var image = Convert.IsDBNull(row[i + 2]) || string.IsNullOrEmpty(row[i + 2].ToString())
                    ? null
                    : Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, row[i + 2].ToString()));

                var key = row[3 + i];

                var stateProviderTypeName = Convert.IsDBNull(key)
                    ? ((string.IsNullOrWhiteSpace(content)
                        ? typeof (EmptyStateProvider).FullName
                        : typeof (NormalStateProvider).FullName))
                    : string.Format("{0}.{1}StateProvider", StateProviderNamespacePrefix, key);
                var actionResponseTypeName = Convert.IsDBNull(key)
                    ? (string.IsNullOrWhiteSpace(content)
                        ? typeof (EmptyActionResponser).FullName
                        : string.Format("{0}.{1}OrdinaryActionResponser", m_OrdinaryActionResponserNamespacePrefix, type))
                    : string.Format("{0}.{1}ActionResponser", ActionResponserNamespacePrefix, key);

                var keyColoumnName = dt.Columns[i + 3].ColumnName;

                var stateProvider = BuildStateProvider(dt, row, stateProviderTypeName, keyColoumnName);
                var driverSelectableItem = new DriverSelectableItem(content, type, image, stateProvider, row);
                driverSelectableItem.ActionResponser = BuildActionResponser(driverSelectableItem, dt, row,
                    actionResponseTypeName, keyColoumnName);

                items.Add(driverSelectableItem);
            }
            var driverSelectable = new DriverSelectable(items);
            return driverSelectable;
        }

        private IDriverActionResponser BuildActionResponser(DriverSelectableItem driverSelectableItem, DataTable dt,
            DataRow row, string actionResponseTypeName, string colName)
        {
            IDriverActionResponser actionResponser = null;

            var actionResponseType = m_SerachTypes.FirstOrDefault(f => f.Name == actionResponseTypeName || f.FullName == actionResponseTypeName);
            if (actionResponseType != null)
            {
                AppLog.Debug(string.Format("Sucess found class {0} of name {1} in assembly {2}", actionResponseType,
                    actionResponseTypeName, actionResponseType.Assembly));
                actionResponser =
                    Activator.CreateInstance(actionResponseType, driverSelectableItem) as IDriverActionResponser;
                AppLog.Debug(string.Format("Sucess create instance of type {0}", actionResponseTypeName));
                if (actionResponser == null)
                {
                    AppLog.Fatal(
                        string.Format(
                            "Class {0} is not a type inherit from {1} in row : {2} coloumn: {2}.\r\n{3}",
                            actionResponseType, typeof(IDriverActionResponser), row[0], colName));
                }
            }

            if (actionResponser == null)
            {
                AppLog.Fatal(
                    string.Format(
                        "Can not create state provider where the class name is {0} in row : {1} coloumn: {2}. so create {3} to replace it.",
                        actionResponseTypeName, row[0], colName, typeof(EmptyActionResponser)));
                actionResponser = new EmptyActionResponser(driverSelectableItem);
            }
            return actionResponser;
        }

        private IDriverSelectableItemStateProvider BuildStateProvider(DataTable dt, DataRow row,
            string stateProviderTypeName,
            string colName)
        {
            IDriverSelectableItemStateProvider stateProvider = null;
            var stateProviderType =
                m_SerachTypes.FirstOrDefault(f => f.Name == stateProviderTypeName || f.FullName == stateProviderTypeName);
            if (stateProviderType != null)
            {
                AppLog.Debug(string.Format("Sucess found class {0} of name {1} in assembly {2}", stateProviderType,
                    stateProviderTypeName, stateProviderType.Assembly));
                stateProvider = Activator.CreateInstance(stateProviderType) as IDriverSelectableItemStateProvider;
                AppLog.Debug(string.Format("Sucess create instance of type {0}", stateProviderTypeName));
                if (stateProvider == null)
                {
                    AppLog.Fatal(
                        string.Format(
                            "Class {0} is not a type inherit from {1} in row : {2} coloumn: {2}.\r\n{3}",
                            stateProviderType, typeof(IDriverSelectableItemStateProvider), row[0], colName));
                    
                }
            }

            if (stateProvider == null)
            {
                AppLog.Fatal(
                    string.Format(
                        "Can not create state provider where the class name is {0} in row : {1} coloumn: {2}. so create {3} to replace it.",
                        stateProviderTypeName, row[0], colName, typeof(NormalStateProvider)));
                stateProvider = new NormalStateProvider();
            }
            return stateProvider;
        }

        private IDriverPopupView BuildDriverPopupViewIfNeed(DataTable dt, DataRow row, DriverInterfaceKey key)
        {
            Contract.Requires(m_SerachTypes != null);

            var popupViewTypeName = row[RegionFStructConfigNames.ColoumnPopupView];
            IDriverPopupView popupView = null;
            if (Convert.IsDBNull(popupViewTypeName))
            {
                return null;
            }
            var popViewType = FindType(popupViewTypeName.ToString());
            if (popViewType != null)
            {
                popupView = GetOrCreatePopupView(popViewType);
                AppLog.Info(string.Format("Sucess create instance of type {0}", popViewType));
                if (popupView == null)
                {
                    AppLog.Fatal(
                        string.Format(
                            "Class {0} is not a type inherit from {1} in row : {2} coloumn: {3}.\r\n{4}",
                            popViewType, typeof(IDriverPopupView), row[0], dt.Rows.IndexOf(row),
                            RegionFStructConfigNames.ColoumnPopupView));
                    return null;
                }
            }

            if (popupView == null)
            {
                AppLog.Fatal(
                    string.Format(
                        "Can not found type of {0} when BuildDriverInterfaceDictionaryByDataTable where the row : key is {1} row no.: {2}",
                        popupViewTypeName, key, row[0]));
            }

            return popupView;
        }

        protected virtual IDriverPopupView GetOrCreatePopupView(Type popViewType)
        {
            return Activator.CreateInstance(popViewType) as IDriverPopupView;
        }

        [DebuggerStepThrough]
        public IDriverInterface GetOrCreateDriverInterface(DriverInterfaceKey interfaceKey)
        {
            if (m_DriverInterfaceDictionary == null)
            {
                m_DriverInterfaceDictionary = BuildDriverInterfaceDictionary();
            }

            Contract.Assert(m_DriverInterfaceDictionary != null, "m_DriverInterfaceDictionary != null");

            return m_DriverInterfaceDictionary.ContainsKey(interfaceKey)
                ? m_DriverInterfaceDictionary[interfaceKey]
                : null;
        }

        private Type FindType(string typeName)
        {
            return m_SerachTypes.FirstOrDefault(f => f.Name == typeName || f.FullName == typeName);
        }
    }
}