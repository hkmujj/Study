using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface;

namespace Motor.ATP._300D.Common
{
    /// <summary>
    /// ATP 基类
    /// </summary>
    internal class ATPBase : baseClass
    {
        private Image[] m_Images;

        public Image[] Images
        {
            get { return m_Images ?? ( m_Images = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray() ); }
        }

        public static int CurrentViewIdex { private set; get; }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            try
            {
                if (Initalize())
                {
                    return true;
                }
                LogMgr.Error(string.Format("initalize {0} fail.", GetType()));
                return false;
            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format("Initalize {0} fail, occuse some exception, {1}", GetType(), e));
                return false;
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            CurrentViewIdex = nParaB;
        }

        public virtual bool Initalize()
        {
            return true;
        }
    }
}
