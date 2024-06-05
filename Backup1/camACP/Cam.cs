using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace camACP
{
    //参考AVICap技术
    public class Cam
    {
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;

        private IntPtr hWndC;
        private IntPtr mControlPtr;
        private bool bWorkStart = false;
        private int mWidth;
        private int mHeight;
        private int mLeft;
        private int mTop;

        /// <summary>
        /// 初始化显示图像
        /// </summary>
        /// <param name="handle">控件的句柄 </param>
        /// <param name="left">开始显示的左边距 </param>
        /// <param name="top">开始显示的上边距 </param>
        /// <param name="width">要显示的宽度 </param>
        /// <param name="height">要显示的长度 </param>
        public Cam(IntPtr handle, int left, int top, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
            mLeft = left;
            mTop = top;
        }
        // 创建捕捉窗口
        //HWND VFWAPI capCreateCaptureWindow(
        // LPCSTR lpszWindowName，// 捕捉窗口名字
        // DWORD dwStyle，// 捕捉窗口的风格
        // int x，// 窗口左上角x轴坐标
        // int y，// 窗口左上角y轴坐标
        // int nWidth，// 窗口的宽度
        // int nHeight，// 窗口的高度
        // HWND HWnd，// 父窗口句柄
        // Int nID// 捕捉窗口的ID号
        //);
        //如果该函数调用成功 则函数返回窗口的句柄 否则函数返回NULL。
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        // 视频格式设置对话框
        // BOOL capDlgVideoFormat( hwnd ); // hwnd：捕捉窗口句柄
        //视频格式设置对话框对于每一个捕捉驱动程序来说，是唯一的。而且，有些驱动程序不一定支持这一功能。应用程序可以通过检测CAPDRIVERCAPS结构的成员变量fHasDlgVideoFormat来判断驱动程序是否支持这一功能。

        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 开始显示图像
        /// </summary>
        public void Start()
        {
            if (bWorkStart)
            {
                return;
            }
            bWorkStart = true;
            byte[] lpszName = new byte[100];
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);
            if (hWndC.ToInt32() != 0)
            {
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, IntPtr.Zero, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_SCALE, (IntPtr)1, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, (IntPtr)66, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, (IntPtr)1, IntPtr.Zero);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, (IntPtr)1, IntPtr.Zero);
            }
            return;
        }

        /// <summary>
        /// 停止显示图像
        /// </summary>
        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, IntPtr.Zero, IntPtr.Zero);
            bWorkStart = false;
        }

        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name="path">要保存bmp文件的路径 </param>
        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path); //将System.String内容复制到非托管内存当中
            SendMessage(hWndC, WM_CAP_SAVEDIB, IntPtr.Zero, hBmp);
        }

        ///<summary>
        ///录像
        ///</summary>
        ///<param name="path">要保存avi文件的路径。(格式: c:\\windows\\c.bmp)</param>
        public void Kinescope(string path)
        {
            IntPtr hCap = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, IntPtr.Zero, hCap);
            SendMessage(hWndC, WM_CAP_SEQUENCE, IntPtr.Zero, hCap);
        }

        ///<summary>
        ///停止录像
        ///</summary>
        public void StopKinescope()
        {
            SendMessage(hWndC, WM_CAP_STOP, IntPtr.Zero, IntPtr.Zero);
        }

    }
}
