using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using General.DialPlate.Character;
using General.DialPlate.Element;
using General.DialPlate.Resources;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace General.DialPlate
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DialPlateView : baseClass
    {
        private List<ElementView> m_ElementViewCollection;

        private List<GDIRectDirectionText> m_Texts;

        public override bool init(ref int nErrorObjectIndex)
        {
            DialPlateConfig.Instance.Initalize(Path.Combine(AppPaths.ConfigDirectory,
                DialPlateResource.ContentConfigFileName));

            m_ElementViewCollection = DialPlateConfig.Instance.ElementModels.Select(
                  s => new ElementView(this, new ElementModelWrapper(s, RecPath))).ToList();

            UIObj.InFloatList.AddRange(
                DialPlateConfig.Instance.ElementModels.Select(s => s.LogicIndex).Where(w => w != int.MaxValue));

            CharacterConfig.Instance.LoadConfig(AppConfig.AppPaths.ConfigDirectory);

            m_Texts = CharacterConfig.Instance.CharacterModelcCollection.Select(s => new GDIRectDirectionText()
            {
                BackColorVisible = false,
                DrawFont = s.Font,
                TextColor = s.Color,
                TextFormat = s.StringFormat,
                OutLineRectangle = Rectangle.Round(s.RectangleF),
                NeedDarwOutline = s.OutlineVisible,
                RotateCenterPoint = s.RectangleF.GetCenterPoint(),
                RotateAngle = s.RotateAngle,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Text = FloatList[s.LogicIndex].ToString(s.NumberFormat);
                }
            }).ToList();
            m_Texts.ForEach(e =>
            {
                e.OutLinePen.Color = Color.Red;
                e.OutLinePen.Width = 3;
            });

            UIObj.InFloatList.AddRange(CharacterConfig.Instance.CharacterModelcCollection.Select(s => s.LogicIndex));
            return true;
        }

        public override void paint(Graphics g)
        {
            m_ElementViewCollection.ForEach(e => e.OnPaint(g));
            m_Texts.ForEach(e => e.OnPaint(g));
        }
    }
}
