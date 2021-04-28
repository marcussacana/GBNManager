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
            TDataMode = false;
            openFileDialog1.Filter = "All GBIN/GSTR Files|*.gbin;*.gstr";
            saveFileDialog1.Filter = "All GBIN/GSTR Files|*.gbin;*.gstr";
            openFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        bool TDataMode = false;
        TextData TData;
        GBN Script;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (TDataMode)
            {
                TData = new TextData(File.ReadAllBytes(openFileDialog1.FileName)); ;
                var Text = TData.Import();

                listBox1.Items.Clear();
                listBox1.Items.AddRange(Text);
            }
            else
            {
                Script = new GBN(File.ReadAllBytes(openFileDialog1.FileName));
                var Text = Script.Import();

                listBox1.Items.Clear();
                listBox1.Items.AddRange(Text);
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TDataMode = true;
            openFileDialog1.Filter = "All BIN Files|*.bin";
            saveFileDialog1.Filter = "All BIN Files|*.bin";
            openFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var Strs = listBox1.Items.Cast<string>().ToArray();
            if (TDataMode)
            {
                var NewScript = TData.Export(Strs);
                File.WriteAllBytes(saveFileDialog1.FileName, NewScript);
            }
            else
            {

                var NewScript = Script.Export(Strs);
                File.WriteAllBytes(saveFileDialog1.FileName, NewScript);
            }

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
