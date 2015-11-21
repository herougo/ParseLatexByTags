namespace ParsingLatexNotes
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
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnParseLatex = new System.Windows.Forms.Button();
            this.rtbTags = new System.Windows.Forms.RichTextBox();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.cbTag = new System.Windows.Forms.ComboBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCourse = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.Color.Black;
            this.rtbOutput.ForeColor = System.Drawing.Color.White;
            this.rtbOutput.Location = new System.Drawing.Point(6, 93);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(501, 380);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(545, 505);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnParseLatex);
            this.tabPage1.Controls.Add(this.rtbOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(537, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parse Latex";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cbCourse);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btnExtract);
            this.tabPage2.Controls.Add(this.cbTag);
            this.tabPage2.Controls.Add(this.btnAddTag);
            this.tabPage2.Controls.Add(this.rtbTags);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(537, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Extract";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnParseLatex
            // 
            this.btnParseLatex.Location = new System.Drawing.Point(184, 17);
            this.btnParseLatex.Name = "btnParseLatex";
            this.btnParseLatex.Size = new System.Drawing.Size(143, 64);
            this.btnParseLatex.TabIndex = 2;
            this.btnParseLatex.Text = "Parse Latex";
            this.btnParseLatex.UseVisualStyleBackColor = true;
            // 
            // rtbTags
            // 
            this.rtbTags.Location = new System.Drawing.Point(20, 134);
            this.rtbTags.Name = "rtbTags";
            this.rtbTags.ReadOnly = true;
            this.rtbTags.Size = new System.Drawing.Size(213, 209);
            this.rtbTags.TabIndex = 0;
            this.rtbTags.Text = "";
            // 
            // btnAddTag
            // 
            this.btnAddTag.Location = new System.Drawing.Point(20, 21);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(133, 52);
            this.btnAddTag.TabIndex = 1;
            this.btnAddTag.Text = "Add Tag For Extraction";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // cbTag
            // 
            this.cbTag.FormattingEnabled = true;
            this.cbTag.Location = new System.Drawing.Point(20, 95);
            this.cbTag.Name = "cbTag";
            this.cbTag.Size = new System.Drawing.Size(184, 21);
            this.cbTag.TabIndex = 2;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(329, 214);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(133, 52);
            this.btnExtract.TabIndex = 3;
            this.btnExtract.Text = "Extract Using Tags";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Course";
            // 
            // cbCourse
            // 
            this.cbCourse.FormattingEnabled = true;
            this.cbCourse.Location = new System.Drawing.Point(20, 392);
            this.cbCourse.Name = "cbCourse";
            this.cbCourse.Size = new System.Drawing.Size(213, 21);
            this.cbCourse.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 532);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnParseLatex;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbTags;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.ComboBox cbTag;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.ComboBox cbCourse;
        private System.Windows.Forms.Label label1;
    }
}

