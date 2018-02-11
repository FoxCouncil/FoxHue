using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FoxHue
{
    public static class Program
    {
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];

                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }

        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FoxHueContext());
        }
    }
}
