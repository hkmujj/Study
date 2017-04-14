using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

// ReSharper disable once CheckNamespace
namespace MMI.Facility.PublicUI.define
{
    public class ShareMemCtrl
    {
        /// <summary>
        /// ����һ���ļ�ӳ���ں˶���
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="lpAttributes"></param>
        /// <param name="flProtect"></param>
        /// <param name="dwMaxSizeHi"></param>
        /// <param name="dwMaxSizeLow"></param>
        /// <param name="lpName"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);
        
        /// <summary>
        /// ��һ���ļ�ӳ���ں˶���
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="lpName"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)]bool bInheritHandle, string lpName);

        /// <summary>
        /// ���ļ�����ӳ�䵽���̵ĵ�ַ�ռ�
        /// </summary>
        /// <param name="hFileMapping"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwFileOffsetHigh"></param>
        /// <param name="dwFileOffsetLow"></param>
        /// <param name="dwNumberOfBytesToMap"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        /// <summary>
        /// �ڵ�ǰӦ�ó�����ڴ��ַ�ռ�����һ����һ���ļ�ӳ������ӳ��
        /// </summary>
        /// <param name="pvBaseAddress"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        /// <summary>
        /// �رչ����ڴ���
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// ��չ����ķ���ֵ
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();

        const int ERROR_ALREADY_EXISTS = 183;

        const int FILE_MAP_COPY = 0x0001;
        const int FILE_MAP_WRITE = 0x0002;
        const int FILE_MAP_READ = 0x0004;
        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

        const int PAGE_READONLY = 0x02;
        const int PAGE_READWRITE = 0x04;
        const int PAGE_WRITECOPY = 0x08;
        const int PAGE_EXECUTE = 0x10;
        const int PAGE_EXECUTE_READ = 0x20;
        const int PAGE_EXECUTE_READWRITE = 0x40;

        const int SEC_COMMIT = 0x8000000;
        const int SEC_IMAGE = 0x1000000;
        const int SEC_NOCACHE = 0x1000000;
        const int SEC_RESERVE = 0x4000000;

        const int INVALID_HANDLE_VALUE = -1;

        IntPtr m_hSharedMemoryFile = IntPtr.Zero;
        IntPtr m_pwData = IntPtr.Zero;

        bool m_bAlreadyExist = false;

        public bool BInit { get; set; }

        public long MemSize { get; set; }

        public bool OtherIsWrite { get; set; }

        public ShareMemCtrl()
        {
            OtherIsWrite = false;
            MemSize = 0;
            BInit = false;
        }

        ~ShareMemCtrl()
        {
            Close();
        }

        /// <summary>
        /// ��ʼ�������ڴ�
        /// </summary>
        /// <param name="strName">�����ڴ�����</param>
        /// <param name="lngSize">�����ڴ��С</param>
        /// <returns></returns>
        public int Init(string strName, long lngSize)
        {
            if (lngSize <= 0 || lngSize > 0x00800000)
            {
                lngSize = 0x00800000;
            }
            MemSize = lngSize;

            if (strName.Length > 0)
            {
                //���������ڴ���(INVALID_HANDLE_VALUE)
                m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_EXECUTE_READWRITE, 0, (uint)lngSize, strName);
                if (m_hSharedMemoryFile == IntPtr.Zero)
                {
                    m_bAlreadyExist = false;
                    BInit = false;
                    return 2;//����������ʧ��
                }
                else
                {
                    if(GetLastError() == ERROR_ALREADY_EXISTS)//�Ѿ�����
                    {
                        m_bAlreadyExist = true;
                    }
                    else
                    {
                        m_bAlreadyExist = false;
                    }
                }
                //--------------------------------------
                //�����ڴ�ӳ��
                m_pwData = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_WRITE, 0, 0, (uint)lngSize);
                if (m_pwData == IntPtr.Zero)
                {
                    BInit = false;
                    CloseHandle(m_hSharedMemoryFile);
                    return 3;//�����ڴ�ӳ��ʧ��
                }
                else
                {
                    BInit = true;
                    if (m_bAlreadyExist == false)
                    {
                        //��ʼ��
                    }
                }
                //---------------------------------------
            }
            else {return 1;}//��������

            return 0;//�����ɹ�
        }

        /// <summary>
        /// �رչ����ڴ�
        /// </summary>
        public void Close()
        {
            if (BInit)
            {
                UnmapViewOfFile(m_pwData);
                CloseHandle(m_hSharedMemoryFile);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="bytData">����</param>
        /// <param name="lngAddr">��ʼ��ַ</param>
        /// <param name="lngSize">����</param>
        /// <returns></returns>
        public int Read(ref byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > MemSize)
            {
                return 2;    //����������
            }
            if (BInit)
            {
                Marshal.Copy(m_pwData, bytData, lngAddr, lngSize);
            }
            else
            {
                return 1;   //�����ڴ�Ϊ��ʼ��
            }
            return 0;   //���ɹ�
        }

        /// <summary>
        /// д����
        /// </summary>
        /// <param name="bytData">����</param>
        /// <param name="lngAddr">��ʼ��ַ</param>
        /// <param name="lngSize">����</param>
        /// <returns></returns>
        public int Write(byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > MemSize)
            {
                return 2;    //����������
            }
            if (BInit)
            {
                Marshal.Copy(bytData, lngAddr, m_pwData, lngSize);
            }
            else
            {
                return 1;   //�����ڴ�Ϊ��ʼ��
            }
            return 0;   //д�ɹ�
        }
    }
}
