namespace Bxttool
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbtnReadFileDlg = new System.Windows.Forms.Button();
            this.pbtnWriteFileDlg = new System.Windows.Forms.Button();
            this.textBoxReadFile = new System.Windows.Forms.TextBox();
            this.textBoxWriteFile = new System.Windows.Forms.TextBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.pbtnRead = new System.Windows.Forms.Button();
            this.pbtnExport = new System.Windows.Forms.Button();
            this.pbtnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbtnReadFileDlg
            // 
            this.pbtnReadFileDlg.Location = new System.Drawing.Point(405, 32);
            this.pbtnReadFileDlg.Name = "pbtnReadFileDlg";
            this.pbtnReadFileDlg.Size = new System.Drawing.Size(60, 45);
            this.pbtnReadFileDlg.TabIndex = 0;
            this.pbtnReadFileDlg.Text = "File...";
            this.pbtnReadFileDlg.UseVisualStyleBackColor = true;
            this.pbtnReadFileDlg.Click += new System.EventHandler(this.pbtnReadFileDlg_Click);
            // 
            // pbtnWriteFileDlg
            // 
            this.pbtnWriteFileDlg.Location = new System.Drawing.Point(406, 114);
            this.pbtnWriteFileDlg.Name = "pbtnWriteFileDlg";
            this.pbtnWriteFileDlg.Size = new System.Drawing.Size(59, 52);
            this.pbtnWriteFileDlg.TabIndex = 1;
            this.pbtnWriteFileDlg.Text = "Dir...";
            this.pbtnWriteFileDlg.UseVisualStyleBackColor = true;
            // 
            // textBoxReadFile
            // 
            this.textBoxReadFile.Location = new System.Drawing.Point(43, 43);
            this.textBoxReadFile.Name = "textBoxReadFile";
            this.textBoxReadFile.Size = new System.Drawing.Size(345, 22);
            this.textBoxReadFile.TabIndex = 2;
            // 
            // textBoxWriteFile
            // 
            this.textBoxWriteFile.Location = new System.Drawing.Point(43, 129);
            this.textBoxWriteFile.Name = "textBoxWriteFile";
            this.textBoxWriteFile.Size = new System.Drawing.Size(345, 22);
            this.textBoxWriteFile.TabIndex = 3;
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(43, 203);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(576, 258);
            this.richTextBoxResult.TabIndex = 4;
            this.richTextBoxResult.Text = "";
            // 
            // pbtnRead
            // 
            this.pbtnRead.Location = new System.Drawing.Point(503, 32);
            this.pbtnRead.Name = "pbtnRead";
            this.pbtnRead.Size = new System.Drawing.Size(116, 45);
            this.pbtnRead.TabIndex = 5;
            this.pbtnRead.Text = "Read";
            this.pbtnRead.UseVisualStyleBackColor = true;
            this.pbtnRead.Click += new System.EventHandler(this.pbtnRead_Click);
            // 
            // pbtnExport
            // 
            this.pbtnExport.Location = new System.Drawing.Point(503, 114);
            this.pbtnExport.Name = "pbtnExport";
            this.pbtnExport.Size = new System.Drawing.Size(116, 51);
            this.pbtnExport.TabIndex = 6;
            this.pbtnExport.Text = "Export";
            this.pbtnExport.UseVisualStyleBackColor = true;
            this.pbtnExport.Click += new System.EventHandler(this.pbtnExport_Click);
            // 
            // pbtnClose
            // 
            this.pbtnClose.Location = new System.Drawing.Point(455, 472);
            this.pbtnClose.Name = "pbtnClose";
            this.pbtnClose.Size = new System.Drawing.Size(164, 42);
            this.pbtnClose.TabIndex = 7;
            this.pbtnClose.Text = "Close";
            this.pbtnClose.UseVisualStyleBackColor = true;
            this.pbtnClose.Click += new System.EventHandler(this.pbtnClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 526);
            this.Controls.Add(this.pbtnClose);
            this.Controls.Add(this.pbtnExport);
            this.Controls.Add(this.pbtnRead);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.textBoxWriteFile);
            this.Controls.Add(this.textBoxReadFile);
            this.Controls.Add(this.pbtnWriteFileDlg);
            this.Controls.Add(this.pbtnReadFileDlg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pbtnReadFileDlg;
        private System.Windows.Forms.Button pbtnWriteFileDlg;
        private System.Windows.Forms.TextBox textBoxReadFile;
        private System.Windows.Forms.TextBox textBoxWriteFile;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Button pbtnRead;
        private System.Windows.Forms.Button pbtnExport;
        private System.Windows.Forms.Button pbtnClose;
    }
}

