using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using General.DialPlate.Resources;

namespace General.DialPlate.Character
{
    public class CharacterConfig : IDisposable
    {
        public static readonly string FileName = DialPlateCharacterResource.FileName;

        public static CharacterConfig Instance { private set; get; }

        private readonly List<CharacterModel> m_CharacterModelcCollection;

        public ReadOnlyCollection<CharacterModel> CharacterModelcCollection { private set; get; }

        static CharacterConfig()
        {
            Instance = new CharacterConfig();
        }

        private CharacterConfig()
        {
            CharacterModelcCollection =
                new ReadOnlyCollection<CharacterModel>(m_CharacterModelcCollection = new List<CharacterModel>());
        }

        public void LoadConfig(string path)
        {
            var file = Path.Combine(path, FileName);

            if (!File.Exists(file))
            {
                LogMgr.Info("Can not found file ({0}), so there is not any Character model . ", file);
                return;
            }

            LogMgr.Info("Parsering file {0}.", file);

            var readConfig = new ExcelReaderConfig()
            {
                File = file,
                SheetNames = new List<string>() {DialPlateCharacterResource.SheetName},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig()
                    {
                        IsPrimaryKey = false,
                        Name = "*"
                    }
                }
            };

            var dt = readConfig.Adapter().Tables[0];

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    var x = Convert.ToSingle(row[DialPlateCharacterResource.顶点坐标X]);
                    var y = Convert.ToSingle(row[DialPlateCharacterResource.顶点坐标Y]);
                    var w = Convert.ToSingle(row[DialPlateCharacterResource.显示大小Width]);
                    var h = Convert.ToSingle(row[DialPlateCharacterResource.显示大小Height]);
                    var logic = Convert.ToInt32(row[DialPlateCharacterResource.值的数据索引]);
                    var r = Convert.ToInt16(row[DialPlateCharacterResource.R]);
                    var g = Convert.ToInt16(row[DialPlateCharacterResource.G]);
                    var b = Convert.ToInt16(row[DialPlateCharacterResource.B]);
                    var va = Enum.Parse(typeof (CharLineAlignment), row[DialPlateCharacterResource.字符垂直对称].ToString());
                    var ha = Enum.Parse(typeof (CharAlignment), row[DialPlateCharacterResource.字符水平对称].ToString());
                    var numberFormat = row[DialPlateCharacterResource.字符精度].ToString();

                    var bold = Convert.ToBoolean(row[DialPlateCharacterResource.Bold]);
                    var isVertical = row[DialPlateCharacterResource.Direction].ToString() ==
                              DialPlateCharacterResource.Vertical;

                    var isShowOutline = Convert.ToBoolean(row[DialPlateCharacterResource.是否显示调试边框]);

                    var rotateAngle = Convert.ToSingle(row[DialPlateCharacterResource.RotateAngle]);

                    var color = Color.FromArgb(r, g, b);
                    var sf = new StringFormat()
                    {
                        LineAlignment = (StringAlignment) (int) va,
                        Alignment = (StringAlignment) (int) ha,
                        
                    };
                    if (isVertical)
                    {
                        sf.FormatFlags = StringFormatFlags.DirectionVertical;
                    }

                    var fontName = row[DialPlateCharacterResource.字体名称].ToString();
                    var fontSize = Convert.ToSingle(row[DialPlateCharacterResource.字体大小]);

                    var font = new Font(fontName, fontSize,
                        bold ? FontStyle.Bold : FontStyle.Regular);

                    var model = new CharacterModel(font, color, sf, new RectangleF(x, y, w, h), logic, numberFormat,
                        isShowOutline, rotateAngle);

                    m_CharacterModelcCollection.Add(model);
                }
            }
            catch (Exception e)
            {
                LogMgr.Error("Occuse some error when parser file {0}, \r\n{1}", file, e);
            }

            LogMgr.Info("Parser file {0} sucess . There is {1} records in it.", file, m_CharacterModelcCollection.Count);
        }

        public void Dispose()
        {
            m_CharacterModelcCollection.ForEach(e =>
            {
                e.Font.Dispose();
                e.StringFormat.Dispose();
            });
        }
    }
}