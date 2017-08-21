using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.View.Control;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.View.Views.Common;

namespace MMI.Facility.View.Views.DebugViews
{
    public partial class ObjectAttribForm : System.Windows.Forms.Form, IAttributeForm
    {
        public event Action<int> BoolObjectAttribeFormDoubelClick;

        public event Action<int> FloatObjectattributeFormDoubleClick;

        public ObjectAttribForm(IDataPackage dataPackage)
            : this()
        {
            DataPackage = dataPackage;
        }

        public ObjectAttribForm()
        {
            ChildAttributeControlDic = new Dictionary<string, IChildAttributeControl>();

            InitializeComponent();

            ShowInTaskbar = false;

            tabControlChildAttribute.AutoSize = true;
        }

        private void ObjectAttribForm_Load(object sender, EventArgs e)
        {
            var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(ObjectAttribForm));
            Top = rect.Top;
            Left = rect.Left;

            foreach (var appConfig in DataPackage.Config.AppConfigs.Where(w => w.SubsystemType == SubsystemType.Addin))
            {
                var child = new ChildAttributeControl(DataPackage, appConfig.AppName) {AutoSize = true};
                ChildAttributeControlDic.Add(appConfig.AppName, child);
                tabControlChildAttribute.TabPages.Add(appConfig.AppName, appConfig.AppName);
                var page = tabControlChildAttribute.TabPages[appConfig.AppName];
                page.Controls.Add(child);
                page.SizeChanged += (o, args) =>
                {
                    child.Size = page.Size;
                };
                child.TextChanged += (o, args) =>
                {
                    page.Text = child.Text;
                };
                child.BoolObjectAttribeFormDoubelClick += (o, arg) =>
                {
                    if (BoolObjectAttribeFormDoubelClick != null)
                    {
                        BoolObjectAttribeFormDoubelClick(arg.Index);
                    }
                };

                child.FloatObjectattributeFormDoubleClick += (o, arg) =>
                {
                    if (FloatObjectattributeFormDoubleClick != null)
                    {
                        FloatObjectattributeFormDoubleClick(arg.Index);
                    }
                };
            }
        }

        public void Select(string appName, IUIObject obj)
        {
            Select();
            ChildAttributeControlDic[appName].Select(obj);
            tabControlChildAttribute.SelectedTab = tabControlChildAttribute.TabPages[appName];
        }

        public void Select(string appName, string objKey)
        {
            Select(appName, ProjetAddinInstanceDic[appName][objKey].UIObj);
        }

        private IDataPackage m_DataPackage;

        public IDataPackage DataPackage
        {
            get { return m_DataPackage; }
            private set
            {
                m_DataPackage = value;
                // TODO extension
                ProjetAddinInstanceDic = m_DataPackage.AddInLoader.ProjetAddinInstanceDic;
                Config = m_DataPackage.Config;
            }
        }

        protected Dictionary<string, Dictionary<string, IObjectBase>> ProjetAddinInstanceDic { get; private set; }


        public IConfig Config { private set; get; }

        public Dictionary<string, IChildAttributeControl> ChildAttributeControlDic { get; private set; }

        public bool ObjectIsInputFromFile { get; set; }

        public bool CanShow { get; set; }

        public void ReastData()
        {

        }
    }
}
