using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Deschedule.ExternalDll
{
    public static class Win32Api
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName); // (Win32Api) 查找窗体 lpClassName为窗体Class名称, lpWindowName为窗体标题

        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMsgTimeout(IntPtr hWnd,
        uint Msg,
        UIntPtr wParam,
        IntPtr lParam,
        uint fuFlags,
        uint uTimeout,
        IntPtr lpdwResult);


        /*
         * 向窗体发送信息
         * 见 https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-sendmessagetimeouta
         * hWnd为Handled的窗体
         * Msg为传入信息
         * wParam和lParam均为参数
         * fuFlags为接收线程的操作
         * uTimeout为超时期限
         * lpdwResult为消息发送的结果
         */

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string winName);
        /*
         * 根据窗体关系以及有关信息查找窗体
         */

        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc proc, IntPtr lParam); // 根据窗体信息枚举窗体

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow); // 设置窗体状况

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hwnd, IntPtr parentHwnd); // 移动窗体层级, 前者为需要被更改层级的窗体，后者为需要更改的层级的下面的窗体

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int i);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

    }
}
