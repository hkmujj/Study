using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Model;
using Motor.ATP.Domain.Model.ControlModel;

namespace Motor.ATP.Domain._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATP300D : ATPDomainBase
    {
        public override void paint(Graphics g)
        {
            var fd = UIObj.InFloatList.Select(s => FloatList[s]).ToList();

            var nextModel = (ControlType)fd[0];
            if (m_ControlModel.NextControlModel == null || m_ControlModel.NextControlModel.Type != nextModel)
            {
                m_ControlModel.NextControlModel = ControlModelFactory.Create(this, nextModel);
            }
        }
    }
}