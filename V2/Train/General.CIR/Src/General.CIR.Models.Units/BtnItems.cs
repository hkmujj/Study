using System;
using System.Linq;
using System.Reflection;
using Excel.Interface;
using General.CIR.Commands.ScreenBtnResponse;
using General.CIR.Events;
using General.CIR.Extentions;
using General.CIR.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Models.Units
{
    [ExcelLocation("界面按钮响应.xls", "Sheet1")]
    public class BtnItems : IBtnItems, ISetValueProvider
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly BtnItems.<>c <>9 = new BtnItems.<>c();

        //	public static Func<Type, bool> <>9__182_0;

        //	internal bool <Navigator>b__182_0(Type w)
        //	{
        //		return w.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault<object>() is ViewExportAttribute;
        //	}
        //}

        public string FullName = null;

        private PropertyInfo[] prop;

        [ExcelField("Key", false)]
        public string Keys
        {
            get;
            set;
        }

        [ExcelField("视图", false)]
        public string Views
        {
            get;
            set;
        }

        [ExcelField("列尾排风", false)]
        public BtnResponseBase BtnColumnEnd
        {
            get;
            set;
        }

        [ExcelField("紧急呼叫", false)]
        public BtnResponseBase BtnEmergency
        {
            get;
            set;
        }

        [ExcelField("报警", false)]
        public BtnResponseBase BtnPolice
        {
            get;
            set;
        }

        [ExcelField("主控", false)]
        public BtnResponseBase BtnMaster
        {
            get;
            set;
        }

        [ExcelField("复位", false)]
        public BtnResponseBase BtnReset
        {
            get;
            set;
        }

        [ExcelField("列尾消号", false)]
        public BtnResponseBase BtnCoumnEndNum
        {
            get;
            set;
        }

        [ExcelField("列尾确认", false)]
        public BtnResponseBase BtnColumnEndConfirm
        {
            get;
            set;
        }

        [ExcelField("风压查询", false)]
        public BtnResponseBase BtnFanSerch
        {
            get;
            set;
        }

        [ExcelField("呼叫", false)]
        public BtnResponseBase BtnCall
        {
            get;
            set;
        }

        [ExcelField("切换", false)]
        public BtnResponseBase BtnSwitch
        {
            get;
            set;
        }

        [ExcelField("挂断", false)]
        public BtnResponseBase BtnHangUp
        {
            get;
            set;
        }

        [ExcelField("设置", false)]
        public BtnResponseBase BtnSetting
        {
            get;
            set;
        }

        [ExcelField("上", false)]
        public BtnResponseBase BtnUp
        {
            get;
            set;
        }

        [ExcelField("界面", false)]
        public BtnResponseBase BtnScreen
        {
            get;
            set;
        }

        [ExcelField("左", false)]
        public BtnResponseBase BtnLeft
        {
            get;
            set;
        }

        [ExcelField("确认", false)]
        public BtnResponseBase BtnConfirm
        {
            get;
            set;
        }

        [ExcelField("右", false)]
        public BtnResponseBase BtnRight
        {
            get;
            set;
        }

        [ExcelField("查询", false)]
        public BtnResponseBase BtnSerch
        {
            get;
            set;
        }

        [ExcelField("下", false)]
        public BtnResponseBase BtnBottom
        {
            get;
            set;
        }

        [ExcelField("回格", false)]
        public BtnResponseBase BtnBack
        {
            get;
            set;
        }

        [ExcelField("打印", false)]
        public BtnResponseBase BtnPrint
        {
            get;
            set;
        }

        [ExcelField("调车请求", false)]
        public BtnResponseBase BtnRequest
        {
            get;
            set;
        }

        [ExcelField("退出", false)]
        public BtnResponseBase BtnQuit
        {
            get;
            set;
        }

        [ExcelField("数字1", false)]
        public BtnResponseBase BtnNmber1
        {
            get;
            set;
        }

        [ExcelField("数字2", false)]
        public BtnResponseBase BtnNmber2
        {
            get;
            set;
        }

        [ExcelField("数字3", false)]
        public BtnResponseBase BtnNmber3
        {
            get;
            set;
        }

        [ExcelField("数字4", false)]
        public BtnResponseBase BtnNmber4
        {
            get;
            set;
        }

        [ExcelField("数字5", false)]
        public BtnResponseBase BtnNmber5
        {
            get;
            set;
        }

        [ExcelField("数字6", false)]
        public BtnResponseBase BtnNmber6
        {
            get;
            set;
        }

        [ExcelField("数字7", false)]
        public BtnResponseBase BtnNmber7
        {
            get;
            set;
        }

        [ExcelField("数字8", false)]
        public BtnResponseBase BtnNmber8
        {
            get;
            set;
        }

        [ExcelField("数字9", false)]
        public BtnResponseBase BtnNmber9
        {
            get;
            set;
        }

        [ExcelField("数字0", false)]
        public BtnResponseBase BtnNmber0
        {
            get;
            set;
        }

        [ExcelField("星号", false)]
        public BtnResponseBase BtnAsterisk
        {
            get;
            set;
        }

        [ExcelField("井号", false)]
        public BtnResponseBase BtnWellNum
        {
            get;
            set;
        }

        [ExcelField("空白1", false)]
        public BtnResponseBase BtnF1
        {
            get;
            set;
        }

        [ExcelField("空白2", false)]
        public BtnResponseBase BtnF2
        {
            get;
            set;
        }

        [ExcelField("空白3", false)]
        public BtnResponseBase BtnF3
        {
            get;
            set;
        }

        [ExcelField("空白4", false)]
        public BtnResponseBase BtnF4
        {
            get;
            set;
        }

        [ExcelField("空白5", false)]
        public BtnResponseBase BtnF5
        {
            get;
            set;
        }

        [ExcelField("空白6", false)]
        public BtnResponseBase BtnF6
        {
            get;
            set;
        }

        [ExcelField("空白7", false)]
        public BtnResponseBase BtnF7
        {
            get;
            set;
        }

        [ExcelField("空白8", false)]
        public BtnResponseBase BtnF8
        {
            get;
            set;
        }

        protected IEventAggregator EventAggregator
        {
            get;
        }

        public BtnItems()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public void Navigator()
        {
            bool flag = string.IsNullOrEmpty(FullName);
            if (flag)
            {

                var type = GetType().Assembly.GetExportedTypes().Where(w => w.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault<object>() is ViewExportAttribute).FirstOrDefault((Type w) => w.Name.Equals(Views));
                FullName = type?.FullName;
            }
            bool flag2 = FullName == null;
            if (!flag2)
            {
                EventAggregator.GetEvent<NavigatorEvent>().Publish(FullName);
            }
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            bool flag = prop == null || prop.Length == 0;
            if (flag)
            {
                prop = typeof(BtnItems).GetProperties();
            }
            (from w in prop
             where w.Name.Equals(propertyOrFieldName)
             select w).ForEach(delegate (PropertyInfo f)
             {
                 f.SetValue(this, GetReponse(value), null);
             });
        }

        private static BtnResponseBase GetReponse(string value)
        {
            string typeName = string.Format("General.CIR.Commands.ScreenBtnResponse.{0}", value);
            Type expr_13 = Type.GetType(typeName);
            return ((expr_13 != null) ? expr_13.Assembly.CreateInstance(typeName) : null) as BtnResponseBase;
        }
    }
}
