using System;
using System.Threading;
using System.Windows.Forms;
namespace Codematic
{
	internal class ThreadExceptionHandler
	{
		public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				DialogResult dialogResult = this.ShowThreadExceptionDialog(e.Exception);
				if (dialogResult == DialogResult.Abort)
				{
					Application.Exit();
				}
			}
			catch
			{
				try
				{
					MessageBox.Show("严重错误", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				finally
				{
					Application.Exit();
				}
			}
		}
		private DialogResult ShowThreadExceptionDialog(Exception ex)
		{
			string errorMessage = string.Concat(new object[]
			{
				"错误信息：\n\n",
				ex.Message,
				"\n\n",
				ex.GetType(),
				"\n\nStack Trace:\n",
				ex.StackTrace
			});
			SendErrInfo sendErrInfo = new SendErrInfo(errorMessage);
			return sendErrInfo.ShowDialog();
		}
	}
}
