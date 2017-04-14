using System;


namespace ES.Facility.PublicModule.BaseData
{
    /// <summary>
    /// ���л�����ʱʹ��
    /// ͨ�ö�������Ը����Խ��б��
    /// </summary>
    public class ESDataTypeAttribute : Attribute
    {
        private AttributeType _dataAttribe;

        /// <summary>
        /// ����������
        /// </summary>
        public AttributeType DataAttribe
        {
            get { return _dataAttribe; }
            set { _dataAttribe = value; }
        }

        /// <summary>
        /// �����������ͣ�����GKS�е����ͷ���
        /// </summary>
        /// <param name="NType"></param>
        public ESDataTypeAttribute(AttributeType nType) { _dataAttribe = nType; }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public static string GetName() { return "ESAttribute"; }
    }

    /// <summary>
    /// �����л������ͣ�����GKS���͵��ڲ�ʶ��
    /// </summary>
    [Flags]
    public enum AttributeType
    {
        /// <summary>Ĭ�Ϸ�ʽ</summary>
        None = 10,
        
        /// <summary>
        /// �Զ����ƶ���
        /// ��ģʽ��Ҫ������չ�����ʹ�ã��������������������в���
        /// ���౻ʵ�� ��ܽ���˳����� ��ʼ���ӿ� �����ӿ� ֱ�����յ�������˳���Ϣ �����ͷ�
        /// �ӿ����Ͳμ� 
        /// ����ģʽ�μ� 
        /// </summary>
        AutoControlClass = 15,
        
        /// <summary>
        /// ʱ����ƶ���
        /// ��ģʽ�ɿ�ܶ�ʱ�����е���
        /// </summary>
        TimingClass = 17,

        /// <summary>
        /// ��Ϣ��������
        /// ��ģʽ����Ϣ���е��ã�������ʵ�������ӿ�
        /// </summary>
        MessaggingClass = 18,

        /// <summary>MMI�Ĳ����</summary>
        isMMIObjectClass = 0,

        /// <summary>������չ��</summary>
        isFunc = 1,

        /// <summary>MMI�����Ա�����</summary>
        isMMIAttributes = 2,
    }

    /// <summary>
    /// ������ܵ���ʱ��������ȷ�ĵ��÷�ʽ
    /// </summary>
    [Flags]
    public enum CallFunType
    {
        //ͼ��
        Draw = 20,
    }

    public class EsModeType
    {

        protected AttributeType theModeType = AttributeType.None;

        /// <summary>
        /// ��������
        /// </summary>
        public AttributeType ModeType { get { return theModeType; } set { theModeType = value; } }


    }


}
