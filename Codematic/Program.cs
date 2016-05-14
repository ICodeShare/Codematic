using System;
using System.Threading;
using System.Windows.Forms;
namespace Codematic
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //ThreadExceptionHandler @object = new ThreadExceptionHandler();
            //Application.ThreadException += new ThreadExceptionEventHandler(@object.Application_ThreadException);
            MainForm mainForm = new MainForm();
            //if (mainForm.mutex != null)
            //{
                Application.Run(mainForm);
            //    return;
            //}
            //MessageBox.Show(mainForm, "动软代码生成器已经启动！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
           
        }
    }
}
