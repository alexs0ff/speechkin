using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace SpeechkinApp.Infrastructure
{
    public class RemoteGetText
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point p);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([InAttribute] System.IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        public const int WM_GETTEXT = 13;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowThreadProcessId(int handle, out int processId);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);
        [DllImport("kernel32.dll")]
        internal static extern int GetCurrentThreadId();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam);

        private const UInt32 WM_KEYDOWN = 0x0100;
        private const UInt32 WM_KEYUP = 0x0101;
        private const UInt32 VK_LCONTROL = 0x0011;
        private const UInt32 VK_C = 0x0043;

        public static string GetTextFromControlAtMousePosition()
        {
            try
            {
                Point p;
                if (GetCursorPos(out p))
                {
                    
                    IntPtr ptr = WindowFromPoint(p);
                    if (ptr != IntPtr.Zero)
                    {
                        SendCopy(ptr);

                        string clipboardTextAfter = null;

                        try
                        {
                            clipboardTextAfter = Clipboard.GetText(TextDataFormat.Text);
                        }
                        catch (Exception)
                        {
                            
                            
                        }
                        
                        return clipboardTextAfter;
                        
                    }
                }
                return "";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        private static void SendCopy(IntPtr handle)
        {
            PostMessage(handle, WM_KEYDOWN, VK_LCONTROL, IntPtr.Zero);
            PostMessage(handle, WM_KEYDOWN, VK_C, IntPtr.Zero);
            PostMessage(handle, WM_KEYUP, VK_C, IntPtr.Zero);
            PostMessage(handle, WM_KEYUP, VK_LCONTROL, IntPtr.Zero);
        }

        //Get the text of a control with its handle
        private static string GetText(IntPtr handle)
        {
            int maxLength = 100;
            IntPtr buffer = Marshal.AllocHGlobal((maxLength + 1) * 2);
            SendMessageW(handle, WM_GETTEXT, maxLength, buffer);
            string w = Marshal.PtrToStringUni(buffer);
            Marshal.FreeHGlobal(buffer);
            return w;
        }
    }
}
