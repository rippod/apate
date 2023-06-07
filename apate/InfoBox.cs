using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apate
{
    public partial class InfoBox : Form
    {
        public InfoBox(string title, string info)
        {
            InitializeComponent();
            this.Text = title;
            label1.Text = info;

        }
    }
}
