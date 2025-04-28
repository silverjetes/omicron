using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace omicron
{
    internal class colors
    {
        static Random rand = new Random();

        public static uint ToHex(byte r, byte g, byte b)
        {
            return (uint)((b << 16) | (g << 8) | r);
        }

        public static uint Random(int minr = 0, int maxr = 255, int ming = 0, int maxg = 255, int minb = 0,int maxb = 255)
        {
            byte r = (byte)rand.Next(minr, maxr);
            byte g = (byte)rand.Next(ming, maxg);
            byte b = (byte)rand.Next(minb, maxb);
            return (uint)((b << 16) | (g << 8) | r);
        }
    }
}
