using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SecurePdf
{
    public partial class SecurePdfForm : Form
    {
        private string message;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        public static extern IntPtr CreateRoundRectRgn
            (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse
            );
        public SecurePdfForm()
        {
            InitializeComponent();
            convertFile.Enabled = false;
            //convertFolder.Enabled = false;
        }


        private void SecurePdfForm_Load(object sender, EventArgs e)
        {
            selectFileBtn.Region = Region.FromHrgn(CreateRoundRectRgn(30, 0, selectFileBtn.Width, selectFileBtn.Height, 30, 0));

        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            fbd.Filter = "Pdf Files|*.pdf";
            fbd.FilterIndex = 1;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string file = fbd.FileName;
                filePath.Text = file;
                //docFile.Text = filep;
            }
            if (filePath.Text != "")
            {
                convertFile.Enabled = true;
            }
        }

        private void convertFile_Click(object sender, EventArgs e)
        {
            loading.Visible = true;
           // convertFolder.Enabled = false;
            convertFile.Enabled = false;
            loading.Dock = DockStyle.Fill;
            backgroundWorker2.RunWorkerAsync();
        }






        private void convertFile_RunWorker(object sender, RunWorkerCompletedEventArgs e)
        {
            if (message != "Success")
            {
                convertFile.Enabled = true;
                loading.Visible = false;
                MessageBox.Show("Some error occured \n" + message);

            }
            else
            {
                filePath.Text = "";
                MessageBox.Show("Your Pdf converted successfully to secure pdf");
                loading.Visible = false;
            }
        }

        private void convertFile_DoWorker(object sender, DoWorkEventArgs e)
        {
            ConvertSecurePdf securePdf = new ConvertSecurePdf();
            message = securePdf.ConvertWordFileToPdf(filePath.Text);
        }
    }
}
