using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace md_formatter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[])(e.Data?.GetData(DataFormats.FileDrop) ?? Array.Empty<string>());
            foreach (var fileName in fileNames)
            {
                var _ = new Formatter(fileName, fileName);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Move
                : DragDropEffects.None;
        }
    }
}
