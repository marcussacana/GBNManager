using GBNManager;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GMGUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        GBN Script;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Script = new GBN(File.ReadAllBytes(openFileDialog1.FileName));
            var Text = Script.Import();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(Text);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var Strs = listBox1.Items.Cast<string>().ToArray();

            var NewScript = Script.Export(Strs);
            File.WriteAllBytes(saveFileDialog1.FileName, NewScript);

            MessageBox.Show("Saved");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            }
            catch { }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                listBox1.Items[listBox1.SelectedIndex] = textBox1.Text.Replace("\\n", "\n");
            }
        }
    }
}
