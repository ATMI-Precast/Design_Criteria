
namespace Design_Criteria
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_TeddsFile = new System.Windows.Forms.TextBox();
            this.btn_OpenExplorer = new System.Windows.Forms.Button();
            this.btn_PreviewDC = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_UpdateDC = new System.Windows.Forms.Button();
            this.btn_OpenExistingTedds = new System.Windows.Forms.Button();
            this.btn_GetVars = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Get Variables";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "OPEN EXISTING TEDDS FILE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "File Path";
            // 
            // txt_TeddsFile
            // 
            this.txt_TeddsFile.Location = new System.Drawing.Point(66, 41);
            this.txt_TeddsFile.Name = "txt_TeddsFile";
            this.txt_TeddsFile.Size = new System.Drawing.Size(171, 20);
            this.txt_TeddsFile.TabIndex = 3;
            this.txt_TeddsFile.TextChanged += new System.EventHandler(this.txt_TeddsFile_TextChanged);
            // 
            // btn_OpenExplorer
            // 
            this.btn_OpenExplorer.Location = new System.Drawing.Point(243, 39);
            this.btn_OpenExplorer.Name = "btn_OpenExplorer";
            this.btn_OpenExplorer.Size = new System.Drawing.Size(28, 23);
            this.btn_OpenExplorer.TabIndex = 4;
            this.btn_OpenExplorer.Text = "...";
            this.btn_OpenExplorer.UseVisualStyleBackColor = true;
            this.btn_OpenExplorer.Click += new System.EventHandler(this.btn_OpenExplorer_Click);
            // 
            // btn_PreviewDC
            // 
            this.btn_PreviewDC.Location = new System.Drawing.Point(102, 76);
            this.btn_PreviewDC.Name = "btn_PreviewDC";
            this.btn_PreviewDC.Size = new System.Drawing.Size(135, 23);
            this.btn_PreviewDC.TabIndex = 7;
            this.btn_PreviewDC.Text = "Preview Design Criteria";
            this.btn_PreviewDC.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 120);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(902, 368);
            this.listBox1.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.InitialDirectory = "@\"C:\\\"";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Browse Text Files";
            // 
            // btn_UpdateDC
            // 
            this.btn_UpdateDC.Location = new System.Drawing.Point(243, 76);
            this.btn_UpdateDC.Name = "btn_UpdateDC";
            this.btn_UpdateDC.Size = new System.Drawing.Size(137, 23);
            this.btn_UpdateDC.TabIndex = 9;
            this.btn_UpdateDC.Text = "Update Design Criteria";
            this.btn_UpdateDC.UseVisualStyleBackColor = true;
            this.btn_UpdateDC.Click += new System.EventHandler(this.btn_UpdateDC_Click);
            // 
            // btn_OpenExistingTedds
            // 
            this.btn_OpenExistingTedds.Location = new System.Drawing.Point(279, 39);
            this.btn_OpenExistingTedds.Name = "btn_OpenExistingTedds";
            this.btn_OpenExistingTedds.Size = new System.Drawing.Size(101, 23);
            this.btn_OpenExistingTedds.TabIndex = 6;
            this.btn_OpenExistingTedds.Text = "Open Tedds File";
            this.btn_OpenExistingTedds.UseVisualStyleBackColor = true;
            this.btn_OpenExistingTedds.Click += new System.EventHandler(this.btn_RunTedds_Click);
            // 
            // btn_GetVars
            // 
            this.btn_GetVars.Location = new System.Drawing.Point(15, 76);
            this.btn_GetVars.Name = "btn_GetVars";
            this.btn_GetVars.Size = new System.Drawing.Size(81, 23);
            this.btn_GetVars.TabIndex = 10;
            this.btn_GetVars.Text = "Get Variables";
            this.btn_GetVars.UseVisualStyleBackColor = true;
            this.btn_GetVars.Click += new System.EventHandler(this.btn_GetVars_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 513);
            this.Controls.Add(this.btn_GetVars);
            this.Controls.Add(this.btn_UpdateDC);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_PreviewDC);
            this.Controls.Add(this.btn_OpenExistingTedds);
            this.Controls.Add(this.btn_OpenExplorer);
            this.Controls.Add(this.txt_TeddsFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_TeddsFile;
        private System.Windows.Forms.Button btn_OpenExplorer;
        private System.Windows.Forms.Button btn_PreviewDC;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_UpdateDC;
        private System.Windows.Forms.Button btn_OpenExistingTedds;
        private System.Windows.Forms.Button btn_GetVars;
    }
}

