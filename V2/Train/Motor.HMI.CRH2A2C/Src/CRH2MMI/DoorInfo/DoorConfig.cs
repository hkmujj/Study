using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.DoorInfo
{
    [XmlRoot]
    public class DoorConfig
    {
        [XmlArray]
        public List<DoorInBoolModel> DoorInBoolModels { set; get; }

        //private static void Test()
        //{
        //    var d = DataSerialization.DeserializeFromXmlFile<DoorConfig>("D:\\a.xml");
        //    var op = d.DoorInBoolModels.FindAll(f => f.Type == DoorOperType.OpenClose);
        //    op.ForEach(e =>
        //    {
        //        var OpenColseDoorSize = new Size(30, 40);
        //        var i = e.CarNo;
        //        if (e.DoorLocation == DoorLocation.Door1)
        //        {
        //            e.Rectangle = new Rectangle(220 + i*44, 238, OpenColseDoorSize.Width, OpenColseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door2)
        //        {
        //            e.Rectangle = new Rectangle(220 + i*44, 395, OpenColseDoorSize.Width, OpenColseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door3)
        //        {
        //            e.Rectangle = new Rectangle(220 + i*44, 293, OpenColseDoorSize.Width, OpenColseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door4)
        //        {
        //            e.Rectangle = new Rectangle(220 + i*44, 450, OpenColseDoorSize.Width, OpenColseDoorSize.Height);
        //        }
        //    });
        //    var pr = d.DoorInBoolModels.FindAll(f => f.Type == DoorOperType.PressRelase);
        //    pr.ForEach(e =>
        //    {
        //        var PressReleaseDoorSize = new Size(42, 24);
        //        var i = e.CarNo;
        //        if (e.DoorLocation == DoorLocation.Door1)
        //        {
        //            e.Rectangle = new Rectangle(212 + i * 43, 152, PressReleaseDoorSize.Width, PressReleaseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door2)
        //        {
        //            e.Rectangle = new Rectangle(212 + i * 43, 517, PressReleaseDoorSize.Width, PressReleaseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door3)
        //        {
        //            e.Rectangle = new Rectangle(212 + i * 43, 178, PressReleaseDoorSize.Width, PressReleaseDoorSize.Height);
        //        }
        //        if (e.DoorLocation == DoorLocation.Door4)
        //        {
        //            e.Rectangle = new Rectangle(212 + i * 43, 543, PressReleaseDoorSize.Width, PressReleaseDoorSize.Height);
        //        }
        //    });
        //    DataSerialization.SerializeToXmlFile(d, "D:\\a1.xml");
        //}

    }

    [DebuggerDisplay("CarNo={CarNo}, Type={Type}, DoorLocation={DoorLocation}, InBools={InBoolXmlNames}")]
    [XmlRoot]
    public class DoorInBoolModel : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public DoorLocation DoorLocation { set; get; }

        [XmlAttribute]
        public DoorOperType Type { set; get; }
    }
}
