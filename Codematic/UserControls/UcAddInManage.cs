using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Maticsoft.AddInManager;
namespace Codematic.UserControls
{
    /// <summary>
    /// 插件管理窗体
    /// </summary>
    public partial class UcAddInManage : UserControl
    {
        private UcCodeView codeview;
        AddIn addin = new AddIn();
        public UcAddInManage()
        {
            InitializeComponent();
            codeview = new UcCodeView();
            tabPage_Code.Controls.Add(codeview);
            CreatView();
            BindlistView();
            LoadAddinFile();
        }

        #region  为listView邦定 列 数据

        private void CreatView()
        {
            //创建列表
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            //this.listView1.LargeImageList = imglistView;
            //this.listView1.SmallImageList = imglistView;
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            //this.listView1.CheckBoxes = true;
            this.listView1.FullRowSelect = true;

            listView1.Columns.Add("编号", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("名称", 150, HorizontalAlignment.Left);            
            listView1.Columns.Add("版本", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("说明", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("程序集", 100, HorizontalAlignment.Left);
        }

        private void BindlistView()
        {
            DataSet ds = addin.GetAddInList();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        listView1.Items.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            string Name = row["Name"].ToString();
                            string Decription = row["Decription"].ToString();
                            string Assembly = row["Assembly"].ToString();
                            string Classname = row["Classname"].ToString();
                            string Version = row["Version"].ToString();
                            string Guid = row["Guid"].ToString();

                            ListViewItem item1 = new ListViewItem(Guid, 0);
                            //item1.ImageIndex = 4;
                            item1.SubItems.Add(Name);                            
                            item1.SubItems.Add(Version);
                            item1.SubItems.Add(Decription);
                            item1.SubItems.Add(Assembly);
                            //item1.SubItems.Add(Guid);

                            listView1.Items.AddRange(new ListViewItem[] { item1 });

                        }
                    }
                }
            }



        }
        #endregion

        #region 加载文件内容
        private void LoadAddinFile()
        {
            string xmlfile = addin.LoadFile();
            codeview.SettxtContent("XML", xmlfile);
        }
        #endregion

        #region  删除
        private void menu_ShowMain_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count < 1)
                {
                    MessageBox.Show(this, "请先选择数据项！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int count = 0;
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    string Guid = item.SubItems[0].Text;
                    addin.DeleteAddIn(Guid);
                    listView1.Items.Remove(listView1.SelectedItems[0]);
                    count++;
                }
                BindlistView();
                LoadAddinFile();
                MessageBox.Show(this, count + "项已删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(System.Exception ex)
            {
                MessageBox.Show(this, "删除失败，请重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogInfo.WriteLog(ex);
            }
        }

        #endregion

        #region 按钮
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtClassname.Text.Trim() == "") || (txtAssembly.Text.Trim() == "") || (txtName.Text.Trim() == ""))
                {
                    MessageBox.Show(this, "组件信息不完整，请认真填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string filename = txtFile.Text;
                if (File.Exists(filename))
                {
                    int n = filename.LastIndexOf("\\");
                    if (n > 1)
                    {
                        string name = filename.Substring(n + 1);
                        if (File.Exists(Application.StartupPath + "\\" + name))
                        {
                            DialogResult dr = MessageBox.Show("该组件文件已经存在，是否覆盖原组件？", "同名提示", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                File.Copy(filename, Application.StartupPath + "\\" + name, true);
                            }
                        }
                        else
                        {
                            File.Copy(filename, Application.StartupPath + "\\" + name, true);
                        }
                    }

                    addin.Guid = Guid.NewGuid().ToString().ToUpper();
                    addin.Classname = txtClassname.Text;
                    addin.Assembly = txtAssembly.Text;
                    addin.Decription = txtDecr.Text;
                    addin.Name = txtName.Text;
                    addin.Version = txtVersion.Text;
                    addin.AddAddIn();
                    BindlistView();
                    LoadAddinFile();
                    MessageBox.Show(this, "组件添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControlAddIn.SelectedIndex = 1;
                }
                else
                {
                    MessageBox.Show(this, "所选择的文件不存在！" , "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, "组件添加失败！\r\n或者请手工复制该文件至安装目录，并修改配置文件即可。\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogInfo.WriteLog(ex);
            }

        }
        /// <summary>
        /// 是否是标准接口的程序
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsValidPlugin(Type t)
        {
            bool ret = false;
            Type[] interfaces = t.GetInterfaces();
            foreach (Type theInterface in interfaces)
            {
                if ((theInterface.FullName == "LTP.IBuilder.IBuilderDAL") ||
                    (theInterface.FullName == "LTP.IBuilder.IBuilderDALTran")||
                    (theInterface.FullName == "LTP.IBuilder.IBuilderWeb") ||
                    (theInterface.FullName == "LTP.IBuilder.IBuilderBLL"))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }        
        private void btnBrow_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog sqlfiledlg = new OpenFileDialog();
                sqlfiledlg.Title = "选择组件文件";
                sqlfiledlg.Filter = "DLL Files (*.dll)|*.dll|All files (*.*)|*.*";
                DialogResult result = sqlfiledlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string filename = sqlfiledlg.FileName;
                    txtFile.Text = filename;

                    Assembly assembly = Assembly.LoadFile(filename);
                    AssemblyName assemblyName = assembly.GetName();
                    Version version = assemblyName.Version;

                    txtAssembly.Text = assemblyName.Name;
                    txtVersion.Text = version.Major + "." + version.MajorRevision;

                    bool isValid = false;
                    Type[] types = assembly.GetTypes();
                    foreach (Type t in types)
                    {
                        //PlugInAttribute pluginAttr = null;
                        //if (IsValidPlugin(t, out pluginAttr))
                        //{
                        //    mPlugs.Add(t.FullName, tmp);
                        //}
                        if (IsValidPlugin(t))
                        {
                            isValid = true;
                            txtClassname.Text = t.FullName;
                        }
                    }
                    if (!isValid)
                    {
                        MessageBox.Show(this, "非标准代码生成插件，请重新选择或改写文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //循环加载程序集到内存中。plugins.Add(pluginAssembly.CreateInstance(plugingType.FullName)); 
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, "加载组件失败！\r\n请检查该组件是否符合接口标准或文件是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogInfo.WriteLog(ex);
            }
            

        }
        #endregion

    }
}
