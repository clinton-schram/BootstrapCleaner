using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BootstrapCleaner
{
    public partial class Form1 : Form
    {
        iCleaner Cleaner;
        public Form1()
        {
            InitializeComponent();
            Cleaner = new Cleaner();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtOut.Text = Cleaner.Clean(txtIn.Text);
        }
    }
}
