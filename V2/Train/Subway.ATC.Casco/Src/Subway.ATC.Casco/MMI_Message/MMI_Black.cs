using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.Casco.MMI_Message
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_Black : ATCBaseClass
    {
        private StartView m_StartView;

        //2
        public override string GetInfo()
        {
            return "MMI黑屏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_StartView = new StartView(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList.First())));
            UIObj.InBoolList.Add(1);

            append_postCmd(CmdType.SetInBoolValue, 1, 1, 0);

            return true;
        }

        public override void paint(Graphics g)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                if (m_StartView.CurrentState != StartView.StartState.Started)
                {
                    m_StartView.Paint(g);
                }
                else
                {
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                }
            }
            else
            {
                m_StartView.Reset();
            }
        }
    }
}