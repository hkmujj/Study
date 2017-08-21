using System;
using System.Drawing;
using System.IO;
using CommonUtil.Util;

namespace General.DialPlate.Element
{
    public class ElementModelWrapper
    {

        public IElementModel ElementModel { private set; get; }

        public Image Image { private set; get; }

        public ElementModelWrapper(IElementModel elementModel, string resPath)
        {
            ElementModel = elementModel;
            if (!Directory.Exists(resPath))
            {
                throw new ArgumentException(string.Format("{0} is not exisets!", resPath));
            }

            if (elementModel != null)
            {
                if (string.IsNullOrWhiteSpace(elementModel.ImageName))
                {
                    LogMgr.Error("Inputted image name is null!");
                    return;
                }
                var file = Path.Combine(resPath, elementModel.ImageName);
                if (!File.Exists(file))
                {
                    LogMgr.Error(string.Format("Can not found image {0}", file));
                    return;
                }

                Image = Image.FromFile(file);
            }
        }
    }
}