using System;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.DMI.Model;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultInfoView : baseClass, IDisposable
    {


        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <param name="nErrorObjectIndex"/>
        /// <returns/>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g"/>
        public override void paint(Graphics g)
        {
            foreach (var kvp in Meter.ErrorMap)
            {
                var key = kvp.Key;
                if (BoolList[key] && !Meter.CurEvent.ContainsKey(key))
                {
                    var data = new EventData
                    {
                        key = key,
                        m_Data = kvp.Value,
                        m_Dtime = DateTime.Now
                    };
                    Meter.CurEvent.Add(key, data);
                    Meter.m_KeyArr.Add(key);
                }
                else if (!BoolList[key] && Meter.CurEvent.ContainsKey(key))
                {

                    Meter.CurEvent.Remove(key);
                    Meter.m_KeyArr.Remove(key);
                }
            }
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {

        }
    }
}