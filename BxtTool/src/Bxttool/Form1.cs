using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BaXterX;
using System.IO;
using System.Diagnostics;

namespace Bxttool
{
    public partial class Form1 : Form
    {
        string path = string.Empty;
        string csv = string.Empty;

        public Form1()
        {
            
            InitializeComponent();
        }

        public void ReadBaxter()
        {
            try
            {
                Reader reader = new Reader();   // BaXter
//                StreamReader stream = new StreamReader(path, Encoding.GetEncoding("euc-jp"));
                StreamReader stream = new StreamReader(path, Encoding.GetEncoding("UTF-8"));

                reader.readFromStream(stream.BaseStream);

                //Now the document has been parsed, it can be read or modified via the reader class.
                //Check the metadata we're interested in exists in the document
                if (reader.document.exists(ElementNames.AUTHORING_TOOL))
                {
                    // Read the Authoring Tool properties
                    richTextBoxResult.AppendText("Before " + reader.document.authoringToolName + " " + reader.document.authoringToolVersion + Environment.NewLine);

                    // Edit the Authoring Tool property
                    reader.document.authoringToolVersion = "v3";
                    richTextBoxResult.AppendText("Edited " + reader.document.authoringToolName + " " + reader.document.authoringToolVersion + Environment.NewLine);
                }

                // We can easily erase whole elements
                if (reader.document.exists(ElementNames.SMARTPAD))
                {
                    reader.document.eraseElement(ElementNames.SMARTPAD);
                }
                // Then re-add them
                reader.document.smartPadID = "12345";
                reader.document.smartPadDeviceName = "Wacom Clipboard";
                richTextBoxResult.AppendText(reader.document.smartPadDeviceName + Environment.NewLine);

                // As the document metadata has been edited, we should regenerate the XMP
                // for the client to insert back into the PDF.
                var new_xmp = reader.document.toXMP();

                //All Document Level Metadata is accessible via the document object
                var page_ids = reader.document.pageIDList;
                //Pages with Metadata will be listed in the PageIDList
                richTextBoxResult.AppendText("Active Pages: \t" + Environment.NewLine);
                foreach (var page_id in page_ids)
                {
                    richTextBoxResult.AppendText("PDF #" + page_id.Item1 + " UUID " + page_id.Item2 + Environment.NewLine);
                }

                //Page objects are accessed in the order they were discovered.
                var page = reader.document.pages[0];
                richTextBoxResult.AppendText("Got Page by vector with UUID " + page.uuid + Environment.NewLine);

                //Using our page object reference, we can access page level metadata
                if (page.exists(ElementNames.PAGE_ID))
                {
                    richTextBoxResult.AppendText("Page with UUID " + page.uuid + " belongs to PDF page " + page.pdfPage + Environment.NewLine);
                }

                //Accessing Fields within a Page is much the same as accessing Pages within the Document
                var field_ids = page.fieldIDList;
                richTextBoxResult.AppendText("Found Fields \t" + Environment.NewLine);
                foreach (var field_id in field_ids)
                {
                    richTextBoxResult.AppendText(field_id + "\t" + Environment.NewLine);
                }

                //We can iterate through a Page's fields vector to find any signatures / handwriting etc.
                foreach (var field in page.fields)
                {
                    if (field.type == "Signature")
                    {
                        richTextBoxResult.AppendText("Found a signature Field " + field.pdfID + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Encrypted " + (field.encrypted ? "YES" : "NO") + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Required " + (field.required ? "YES" : "NO") + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Signatory Time  " + field.completionTime + Environment.NewLine);
                        richTextBoxResult.AppendText("\t FSS Data " + field.data + Environment.NewLine);
                    }
                    else if (field.type == "Text")
                    {
                        richTextBoxResult.AppendText("Found a text Field: " + field.pdfID + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Tag: " + field.tag + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Handwriting Recognition Data: " + field.data + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Location XYHW: "
                            + field.locationX + ", "
                            + field.locationY + ", "
                            + field.locationH + ", "
                            + field.locationW + ", "
                            + Environment.NewLine);
                        richTextBoxResult.AppendText("\t Completion Time: "
                            + field.completionTime + Environment.NewLine);

                        csv += (field.pdfID
                            + "," + field.tag
                            + "," + field.data
                            + ", " + field.locationX
                            + ", " + field.locationY
                            + ", " + field.locationH
                            + ", " + field.locationW
                            + Environment.NewLine);
                    }
                }

                pbtnExport.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void WriteBaxter()
        {
            try
            {
                string fullpath = Directory.GetCurrentDirectory() + "\\" + "fields.csv";
                textBoxWriteFile.Text = fullpath;

                FileStream hStream = File.Create(fullpath);
                {
                    // 作成時に返される FileStream を利用して閉じる
                    if (hStream != null)
                    {
                        hStream.Close();
                    }
                }
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                StreamWriter writer =
                  new StreamWriter(fullpath, true, sjisEnc);
                writer.WriteLine(csv);
                writer.Close();

                MessageBox.Show("Done", "Export", MessageBoxButtons.OK);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void pbtnRead_Click(object sender, EventArgs e)
        {
            ReadBaxter();
        }

        private void pbtnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pbtnReadFileDlg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = string.Empty;
 //           ofd.InitialDirectory = @"C:\";
            ofd.InitialDirectory =string.Empty;  // current directory
            ofd.Filter = "PDF File (*.pdf)|*.pdf|All Files(*.*)|*.*";
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "Please set the File";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;    // full path + filename + extension

                textBoxReadFile.Text = path;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "BxtTool";
            textBoxWriteFile.Enabled = false;
            pbtnWriteFileDlg.Enabled = false;
            pbtnExport.Enabled = false;
        }

        private void pbtnExport_Click(object sender, EventArgs e)
        {
            WriteBaxter();
        }
    }
}
