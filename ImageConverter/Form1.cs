using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConverter
{
    public partial class ImageConverter : Form
    {
        public ImageConverter()
        {
            InitializeComponent();

            webpquality.SelectedIndex = 0; //Select first item in combo box
            progressBar.DisplayStyle = ProgressBarDisplayText.CustomText; 
            progressBar.CustomText = "Waiting"; // Set progress bar text
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "Open Image";
            dialog.Filter = "Images|*jpg;*.png;*.gif:*.tiff;*.tif;*.webp;*.bmp;*.jpeg;*.jfif|jpg files (*.jpg)|*.jpg|" +
                "png files (*.png)|*.png|gif files (*.gif)|*.gif|" +
                "jfif files (*.jfif)|*.jfif|webp files (*.webp)|*.webp|bmp files (*.bmp)|*.bmp";
            dialog.ShowDialog();

            //Adds files and ignore duplicates
            foreach (var file in dialog.FileNames)
            {

                var ex = Path.GetExtension(file);
                var n = file.Replace(ex, "");
                if (!ExistsInTable(n, ex))
                    FileDataGrid.Rows.Add(n, ex);

                filecount.Text = $"{FileDataGrid.Rows.Count} Files"; //Updates file counter

            }
        }


        private void Browse_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.ShowDialog();

            outputpath.Text = dialog.SelectedPath;
        }



        int selectedOutput = 0;
        List<string> formats = new List<string> { "JPG/JPEG", "PNG", "BMP", "GIF", "TIF/TIFF", "WEBP" };
        List<string> AllowedExtensions = new List<string> { "jpeg", "jpg", "tif", "jfif", "png", "bmp", "gif", "tiff", "webp" };
        List<int> WebpQualities = new List<int> { -1, 100, 80, 50, 20 };


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                string name = "";
                name = ((RadioButton)sender).Text.ToString();
                selectedOutput = formats.IndexOf(name);
                if (selectedOutput == 5) webpquality.Enabled = true; else webpquality.Enabled = false;
            }


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(outputpath.Text))
            {
                MessageBox.Show("Invalide output path!", "Error", MessageBoxButtons.OK);
                return;
            }
            SecondThreadConcern.Stop = false;

            cancelButton.Enabled = true ;
            convertbutton.Enabled = false;
            addbutton.Enabled = false;
            browsebutton.Enabled = false;
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            FileDataGrid.Enabled = false;
            removebutton.Enabled = false;

            progressBar.Maximum = FileDataGrid.Rows.Count;
            progressBar.CustomText = $"{0} / {progressBar.Maximum} (0%)";

            var outpath = outputpath.Text;
            if (!outpath.EndsWith("\\"))
                outpath += "\\";

            var webpq = WebpQualities[webpquality.SelectedIndex];


            //Do the conversion in another thread and report the progress to the ui
            //Code from https://stackoverflow.com/a/18033198/14190733

            var progress = new Progress<int>(s =>
            {
                progressBar.Value = s;
                progressBar.CustomText = $"{s}/{progressBar.Maximum} ({(int)((float)s / progressBar.Maximum * 100)}%)";
            });

            await Task.Factory.StartNew(() => SecondThreadConcern.LongWork(progress, selectedOutput: selectedOutput, webpq: webpq, outpath: outpath, rows: FileDataGrid.Rows),
                                TaskCreationOptions.LongRunning);

            //Show additional informaiton if conversion stopped
            string additionalInfo = "";
            if (SecondThreadConcern.Stop)
            {
                additionalInfo = "Conversion aborted!\n";
            }

            string message = additionalInfo+ $"Converted {progressBar.Value} File(s). out of {progressBar.Maximum}";
            DialogResult result = MessageBox.Show(message, "Conversion successful", MessageBoxButtons.OK);

            if (openFolder.Checked)
            {
                Process.Start("explorer.exe", outpath); // Open the output folder in the system file explorer
            }

            //reset ui elements
            cancelButton.Enabled = false;
            convertbutton.Enabled = true;
            addbutton.Enabled = true;
            browsebutton.Enabled = true;
            groupBox2.Enabled = true;
            groupBox1.Enabled = true;
            FileDataGrid.Enabled = true;
            removebutton.Enabled = true;

            progressBar.Value = 0;

            progressBar.CustomText = "Waiting";
        }


        private void removebutton_Click(object sender, EventArgs e)
        {
            if (FileDataGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in FileDataGrid.SelectedRows)
            {
                FileDataGrid.Rows.Remove(row);
            }
        }

        private void datagrid_dragEntered(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //only allow drop when image extensions are supported
                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file);
                    ext = ext.Replace(".", "");

                    if (!AllowedExtensions.Contains(ext.ToLower()))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;

            }
        }


        private void datagrid_dragDropped(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    var ex = Path.GetExtension(file);
                    var n = file.Replace(ex, "");

                    if (!ExistsInTable(n, ex))
                        FileDataGrid.Rows.Add(n, ex);

                    filecount.Text = $"{FileDataGrid.Rows.Count} Files";

                }
            }

        }

        // Checks if an image's full path is already imported
        bool ExistsInTable(object path, object ext)
        {

            for (int i = 0; i < FileDataGrid.Rows.Count; i++)
            {
                if ((FileDataGrid.Rows[i].Cells[0].Value as string) == (path as string) && (FileDataGrid.Rows[i].Cells[1].Value as string) == ext as string)
                    return true;
            }
            return false;
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            FileDataGrid.Rows.Clear();
        }

        private void ExitClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            new About().Show();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SecondThreadConcern.Stop = true;
        }
    }

    //Conversion thread
    class SecondThreadConcern
    {

        public static List<ImageFormat> ImageFormats = new List<ImageFormat> { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Bmp, ImageFormat.Gif, ImageFormat.Tiff };
        public static List<string> FormatExtensions = new List<string> { "jpeg", "png", "bmp", "gif", "tiff", "webp" };
        public static bool Stop;

        public static void LongWork(IProgress<int> progress, int selectedOutput, int webpq, string outpath, DataGridViewRowCollection rows)
        {
            int p = 0;
            foreach (DataGridViewRow row in rows)
            {
                var extension = row.Cells[1].Value as string;
                var filepath = row.Cells[0].Value as string + extension;
                var filename = Path.GetFileNameWithoutExtension(filepath);
                byte[] buffer = File.ReadAllBytes(row.Cells[0].Value as string + row.Cells[1].Value as string);

                Bitmap bitmap = null;

                //check if current image is webp and encode
                if (extension.ToLowerInvariant().Contains("webp"))
                {
                    var decoder = new Imazen.WebP.SimpleDecoder();
                    bitmap = decoder.DecodeFromBytes(buffer, buffer.LongLength);
                }
                else // If not, load file into bitmap
                {
                    bitmap = new Bitmap(new MemoryStream(buffer));
                }

                System.Drawing.Image img = bitmap;

                //If not saving as webp, use image.save with the appropriate format
                if (selectedOutput != 5)
                {
                    img.Save(outpath + filename + $".{FormatExtensions[selectedOutput]}", ImageFormats[selectedOutput]);
                }
                else
                {
                    //If saving as webp encode the file
                    var encoder = new Imazen.WebP.SimpleEncoder();
                    FileStream outStream = new FileStream(outpath + filename + $".webp", FileMode.Create);
                    encoder.Encode(bitmap, outStream, webpq);
                    outStream.Close();
                }
                p++;
                progress.Report(p); // report the progress to the ui thread

                if (Stop) break; // if user requested to stop the conversion, break out of loop

            }

        }
    }
}
