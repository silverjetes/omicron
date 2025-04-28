using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace omicron
{
    public partial class messageBox : Form
    {
        public messageBox()
        {
            InitializeComponent();
            SystemSounds.Hand.Play();
            this.Icon = SystemIcons.Application;
            this.pictureBox1.Image = SystemIcons.Hand.ToBitmap();
            this.Font = SystemFonts.MessageBoxFont;

            Point labelLocation = new Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, pictureBox1.Location.Y + 6);
            this.label1.Location = labelLocation;

            Point buttonLocation = new Point(label1.Location.X + label1.Width - button1.Width, pictureBox1.Location.Y + pictureBox1.Size.Height + 6);
            this.button1.Location = buttonLocation;

            this.button1.Font = SystemFonts.MessageBoxFont;

            new Thread(text).Start();
            
            void text()
            {
                while (true)
                {
                    try
                    {
                        Invoke(new Action(() =>
                        {
                            this.label1.Location = labelLocation;
                            this.button1.Location = buttonLocation;

                            this.button1.Font = SystemFonts.MessageBoxFont;

                            this.label1.Text = payloads.GetRandomUnicode(20);
                            this.button1.Text = payloads.GetRandomUnicode(7);
                            this.Text = "you're doomed.";

                            this.label1.Refresh();
                            this.button1.Refresh();
                            this.pictureBox1.Refresh();

                            Thread.Sleep(100);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[ERROR] ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(">> ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(ex.ToString() + "\n");
                    }
                }
            }
        }

        private void messageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
