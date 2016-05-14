using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace Codematic
{
    public partial class DataList : Form
    {
        Thread mythread;
        Maticsoft.IDBO.IDbObject dbobj;
        string dbname;
        string tabname;
        int times = 0;
        delegate void SetListCallback();

        public DataList(Maticsoft.IDBO.IDbObject idbobj, string dbName, string tabName)
        {
            InitializeComponent();
            try
            {
                dbobj = idbobj;                
                dbname = dbName;
                tabname = tabName;
                StatusLabel_dbname.Text = "库:"+dbname+"，表:"+tabname+"  ";

                mythread = new Thread(new ThreadStart(ThreadWork));
                mythread.Start();

                //ThreadWork();
            }
            catch (System.Exception er)
            {
                //MessageBox.Show(er.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
       

        void ThreadWork()
        {
            try
            {
                timer1.Enabled = true;
                StatusLabel_Tip.Text = "正在查询，请稍候...";
                BindDataList();
                timer1.Enabled = false;
                StatusLabel_Tip.Text = "完成";
            }
            catch (System.Exception er)
            {
                //MessageBox.Show(er.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
           
        }
        private void BindDataList()
        {
            if (this.dataGridView1.InvokeRequired)
            {
                SetListCallback d = new SetListCallback(BindDataList);
                this.Invoke(d, null);
            }
            else
            {
                BindData();
            }
        }
        private void BindData()
        {
            try
            {
                DataTable dt = dbobj.GetTabData(dbname, tabname);
                dataGridView1.DataSource = dt;                
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                StatusLabel_Count.Text = dt.Rows.Count + "行 ";
            }
            catch (System.Exception er)
            {
                //MessageBox.Show(er.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            times++;
            StatusLabel_time.Text = GetTimestr(times) + "  ";
        }
        private string GetTimestr(int times)
        {
            int h = 0; 
            int m = 0;            
            if (times > 60)
            {
                if (times > 3600)
                {
                    h = times / 3600;
                    m = (times - h * 3600) / 60;
                }
                else 
                {
                    m = times / 60;
                }                
            }
            int s = times - h * 3600 - m * 60;
            return h.ToString("{00}") + ":" + m.ToString("{00}") + ":" + s.ToString("{00}");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string msg = String.Format("{0},{1}", dataGridView1.CurrentCell.RowIndex,
                                            dataGridView1.CurrentCell.ColumnIndex);
                StatusLabel_rowcol.Text = msg;
            }
            catch
            { }

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.DesiredType == typeof(TimeSpan)) || (e.DesiredType == typeof(Image)))
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];                
                cell.Value = null;
               
                //if (e.Value.Equals("*"))
                //{
                //    cell.ToolTipText = "very bad";
                //}
                //else if (e.Value.Equals("**"))
                //{
                //    cell.ToolTipText = "bad";
                //}
                //else if (e.Value.Equals("***"))
                //{
                //    cell.ToolTipText = "good";
                //}
                //else if (e.Value.Equals("****"))
                //{
                //    cell.ToolTipText = "very good";
                //}
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
       
    }
}