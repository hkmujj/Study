using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;

namespace CRH2MMI.Title
{
    class TitleResource
    {

        private Title m_Title;

        public List<TrainLineConfig> TrainLineConfigs { private set; get; } 

        public TitleResource(Title title)
        {
            m_Title = title;
        }

        public void Init()
        {
            var file = Path.Combine(Path.Combine(m_Title.RecPath, "..\\"), "Config\\TrainLineConfig.xml");
            TrainLineConfigs = DataSerialization.DeserializeFromXmlFile<List<TrainLineConfig>>(file, "Root");
        }

    }
}
