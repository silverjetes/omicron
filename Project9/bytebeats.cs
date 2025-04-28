using System;
using System.IO;
using System.Media;

public class bytebeats
{
    public static void player()
    {
        Func<double, double>[] formulas = new Func<double, double>[] //set the formulas here
        {
            // wow so pro code
            t => t/(t%55)*t,
            t => (((int)t^6+(int)t>>8)*(int)t>>12)+t,
            t => t/(t%((int)t>>2|(int)t>>7))*t,
            t => ((int) t^((int)t*(int)t>>14))+((600000/(t%2500))*2),
            t => ((int)t>>3>>(int)t*(int)t|(int)t>>3)%500,
            t => (int)t^((int)t*(int)t|((int)t>>4)*((int)t>>6)>>6),
            t => (int)t*((int)t>>3|(int)t>>4)>>5,
            t => ((int)t*(int)t|(int)t>>1)+(((int)t>>2)*((int)t>>6)>>6),
            t => t/(t%55)*t,
        };

        int[] drs = new int[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };  //set the durations for bytebeats, 1st will play 5 seconds, 2nd will play 3...
        int[] sra = new int[] { 8000, 8000, 8000, 8000, 8000, 8000, 8000, 8000, 8000 };  //frequency (sample rate)
        for (int i = 0; i < formulas.Length + 1; i++)
        {
            var formula = formulas[i];
            int dr = drs[i];
            int sr = sra[i];
            int bs = sr * dr;

            byte[] buffer = new byte[bs];
            for (int t = 0; t < bs; t++)
            {
                buffer[t] = (byte)((int)formula(t) & 0xFF);
            }

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(new[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' });
                bw.Write(36 + buffer.Length);
                bw.Write(new[] { (byte)'W', (byte)'A', (byte)'V', (byte)'E' });
                bw.Write(new[] { (byte)'f', (byte)'m', (byte)'t', (byte)' ' });
                bw.Write(16);
                bw.Write((short)1); // PCM
                bw.Write((short)1); // mono
                bw.Write(sr);  //sample rate
                bw.Write(sr * 1 * 8 / 8);
                bw.Write((short)(1 * 8 / 8));
                bw.Write((short)8);  //bits per sample
                bw.Write(new[] { (byte)'d', (byte)'a', (byte)'t', (byte)'a' });
                bw.Write(buffer.Length);
                bw.Write(buffer);

                ms.Position = 0; //set the position in zero, important!

                using (SoundPlayer player = new SoundPlayer(ms))
                {
                    player.PlaySync();
                }
            }
        }
    }
}