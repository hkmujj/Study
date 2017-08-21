using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CommonUtil.Util;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;

namespace SubSysytemConfigEditer
{
    public interface IChangeSubSystem
    {
        string TagertFileName { get; }
        ISystemConfig SystemConfigOne { get; set; }
        ISystemConfig SystemConfigTwo { get; set; }
        string[] Args { get; }
        void Changed();
    }

    internal class CopySubSystem : IChangeSubSystem
    {
        public CopySubSystem(string[] args)
        {
            Args = args;
            TagertFileName = args[0];
            FileName = args[1];
        }

        public string TagertFileName { get; protected set; }
        public string FileName { get; protected set; }
        public ISystemConfig SystemConfigOne { get; set; }
        public ISystemConfig SystemConfigTwo { get; set; }

        private List<SubsystemConfig> m_List;
        public string[] Args { get; private set; }

        public void Changed()
        {
            m_List = new List<SubsystemConfig>();
            SystemConfigOne = DataSerialization.DeserializeFromXmlFile<SystemConfig>(TagertFileName);
            SystemConfigTwo = DataSerialization.DeserializeFromXmlFile<SystemConfig>(FileName);
            foreach (var config in from config in SystemConfigOne.SubsystemConfigCollection let tmp = m_List.FirstOrDefault(f => f.Name == config.Name) where tmp == null select config)
            {
                m_List.Add((SubsystemConfig)config);
            }
            foreach (var config in from config in SystemConfigTwo.SubsystemConfigCollection let tmp = m_List.FirstOrDefault(f => f.Name == config.Name) where tmp == null select config)
            {
                m_List.Add((SubsystemConfig)config);
            }
            ((SystemConfig)SystemConfigOne).SubsystemConfigs = new ObservableCollection<SubsystemConfig>(m_List);
            DataSerialization.SerializeToXmlFile((SystemConfig)SystemConfigOne, TagertFileName);
        }
    }

    internal class AddSubSystem : IChangeSubSystem, ISubsystemConfig
    {
        private SubsystemType m_SubsystemType;
        private ProjectType m_ProjectType;
        private bool m_NeedLoad;
        private string m_Name;
        private string m_Dll;
        private string m_EntryClass;
        public string TagertFileName { get; protected set; }
        public ISystemConfig SystemConfigOne { get; set; }
        public ISystemConfig SystemConfigTwo { get; set; }
        public ReadOnlyCollection<ISubsystemConfig> Subsystem { get; set; }

        public string[] Args { get; set; }

        public AddSubSystem(string[] args)
        {
            this.Args = args;
        }

        public void Changed()
        {
            TagertFileName = Args[0];
            Enum.TryParse(Args[1], true, out m_SubsystemType);
            Enum.TryParse(Args[2], true, out m_ProjectType);
            bool.TryParse(Args[3], out m_NeedLoad);
            Name = Args[4];
            Dll = Args[5];
            EntryClass = Args[6];
            SystemConfigOne = DataSerialization.DeserializeFromXmlFile<SystemConfig>(TagertFileName);
            var tmp = SystemConfigOne.SubsystemConfigCollection.Cast<SubsystemConfig>().ToList();
            var tmp1 = new SubsystemConfig
            {
                Name = Name,
                Dll = Dll,
                EntryClass = EntryClass,
                NeedLoad = NeedLoad,
                ProjectType = ProjectType,
                SubsystemType = SubsystemType
            };
            tmp.Add(tmp1);
            ((SystemConfig)SystemConfigOne).SubsystemConfigs = new ObservableCollection<SubsystemConfig>(tmp);
            DataSerialization.SerializeToXmlFile((SystemConfig)SystemConfigOne, TagertFileName);
        }

        public SubsystemType SubsystemType
        {
            get { return m_SubsystemType; }
            set { m_SubsystemType = value; }
        }

        public ProjectType ProjectType
        {
            get { return m_ProjectType; }
            set { m_ProjectType = value; }
        }

        public bool NeedLoad
        {
            get { return m_NeedLoad; }
            set { m_NeedLoad = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Dll
        {
            get { return m_Dll; }
            set { m_Dll = value; }
        }

        public string EntryClass
        {
            get { return m_EntryClass; }
            set { m_EntryClass = value; }
        }

        /// <summary>
        /// PTT共享内存名称
        /// </summary>
        public string ShareName { get; }

        /// <summary>
        /// PTT共享内存Index
        /// </summary>
        public int ShareIndex { get; }
    }
}