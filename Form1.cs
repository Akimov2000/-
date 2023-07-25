using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        private void button1_Click(object sender, EventArgs e)
        {


            var SysHeight = SystemParameters.FullPrimaryScreenHeight;
            var SysWidth = SystemParameters.FullPrimaryScreenWidth;
            int currentHeight = 0;
            int currentWith = 0;
            const int SWP_NOSIZE = 1;
            foreach (Process proc in Process.GetProcessesByName("csgo"))
            {
                RECT rc = new RECT();
                GetWindowRect(proc.MainWindowHandle, ref rc);
                int width = rc.Right - rc.Left;
                int height = rc.Bottom - rc.Top;
                if (currentWith + width >= SysWidth)
                {
                    currentHeight += height;
                    currentWith = 0;
                }
                var widthBuff = currentWith;
                Task.Factory.StartNew(() =>
                {
                    SetWindowPos(proc.MainWindowHandle, 1, 1 + widthBuff, currentHeight, 320, 240, SWP_NOSIZE);
                });
                currentWith += width;
            }

        }
        public struct RECT
        {

            public int Left;

            public int Top;

            public int Right;

            public int Bottom;

        }
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_SHOWWINDOW = 0x0040;

      

       
    }
}
