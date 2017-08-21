using System;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Service;
using Motor.ATP._300T.主屏;

namespace Motor.ATP._300T
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TopAdorner : baseClass, IDisposable
    {
        private ATPMainScreen m_ATPMainScreen;

        public const int MaxLightValue = 100;

        public const int MinLightValue = 0;

        private const int MaxGammy = 120;

        private readonly SolidBrush m_BackBrush = new SolidBrush(Color.Transparent);

        public int Gammy
        {
            get
            {
                return
                    (int)
                        (MaxGammy -
                         ((double) MaxGammy/(MaxLightValue - MinLightValue)*
                          m_ATPMainScreen.OpenFunBtnCtcs300T.LightValue));
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_ATPMainScreen =
                DataPackage.ServiceManager.GetService<IObjectService>()
                    .GetObject<ATPMainScreen>(AppConfig.AppName)
                    .FirstOrDefault();

            DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);

            return true;

        }

        public override void paint(Graphics g)
        {
            m_BackBrush.Color = Color.FromArgb(Gammy, 0, 0, 0);

            g.FillRectangle(m_BackBrush, 0, 0, 800, 600);

        }

        public void Dispose()
        {
            m_BackBrush.Dispose();
        }
    }
}