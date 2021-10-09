using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;


namespace nugrab
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
                
                openFileDialog.Filter = "NuGet Packages (*.nupkg)|*.nupkg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = openFileDialog.FileName;
                }
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            saveFileDialog.Filter = "DLL file (*.dll)|*.dll";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox2.Text = saveFileDialog.FileName;
              
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string outfolder = Application.StartupPath + @"\temp";
                Directory.CreateDirectory(outfolder);
                ZipFile.ExtractToDirectory(richTextBox1.Text, outfolder);
                File.Copy(outfolder + @"\lib\net" + richTextBox3.Text + @"\" + richTextBox4.Text + ".dll", richTextBox2.Text);

                DirectoryInfo di = new DirectoryInfo(outfolder);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(outfolder);
                MessageBox.Show("Finished successfully!", "Done.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception err)
            {
                MessageBox.Show("There was an Error Extracting the NUGET dll", "Uh Oh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
