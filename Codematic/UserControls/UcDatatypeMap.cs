using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Codematic.UserControls
{
    public partial class UcDatatypeMap : UserControl
    {
        string datatypefile = Application.StartupPath + "\\datatype.ini";
        Maticsoft.Utility.INIFile datatype;
        public UcDatatypeMap()
        {
            InitializeComponent();

            datatype = new Maticsoft.Utility.INIFile(datatypefile);
        }
    }
}
