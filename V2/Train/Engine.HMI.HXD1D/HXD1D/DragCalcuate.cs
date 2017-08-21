
namespace HXD1D
{
    public class DragCalcuate
    {
        private float _floatValue1;
        private float _floatValue2;
        private int _type;

        /// <summary>
        /// 构造函数赋值
        /// </summary>
        /// <param name="floatValue2"></param>
        /// <param name="type">判断是牵引还是制动</param>
        /// <param name="floatValue1"></param>
        public DragCalcuate(float floatValue1,float  floatValue2, int type)
        {
            _floatValue1 = floatValue1;
            _floatValue2 = floatValue2;
            _type = type;
         
        }
        /// <summary>
        /// 0代表牵引1代表制动
        /// </summary>
        /// <returns></returns>
        public float Calucate()
        {
            return  _type == 0 ? (float) ( _floatValue1 / 0.7 ) : (float) ( _floatValue1 / 0.35 );
            //if (_type==0)
            //{
            //   float percentage;
            //    percentage = (float)(_floatValue1/0.7);
            //    return percentage;
            //}

            //if (_type==1)
            //{
            //    float percentage = (float)(_floatValue1 / 0.35);
            //    return percentage;
            //}
            //return 0;
        }

        public int Compare()
        {
            if (_type == 0)
            {
                float f1 = (float) (_floatValue1/0.7);
                float f2 = (float)(_floatValue2/0.7);
                return f1>f2 ? 33 : 34;
               
            }
            if (_type == 1)
            {
                float f1 = (float)(_floatValue1 / 0.35);
                float f2 = (float)(_floatValue2 / 0.35);
                return f1 > f2 ? 35 : 36;

            }
          
         

            return 0;
        }


    }

}
