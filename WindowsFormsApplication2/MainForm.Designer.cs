namespace Desymbolizer
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
            this.components = new System.ComponentModel.Container();
            this.textBoxMainDir = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.labelBytes = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonRevert = new System.Windows.Forms.Button();
            this.labelHappen = new System.Windows.Forms.Label();
            this.checkBoxSearchOp = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMainDir
            // 
            this.textBoxMainDir.Location = new System.Drawing.Point(13, 13);
            this.textBoxMainDir.Name = "textBoxMainDir";
            this.textBoxMainDir.Size = new System.Drawing.Size(155, 20);
            this.textBoxMainDir.TabIndex = 0;
            this.textBoxMainDir.TextChanged += new System.EventHandler(this.textBoxMainDir_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(248, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Analyze";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelBytes
            // 
            this.labelBytes.AutoSize = true;
            this.labelBytes.Location = new System.Drawing.Point(267, 50);
            this.labelBytes.Name = "labelBytes";
            this.labelBytes.Size = new System.Drawing.Size(10, 13);
            this.labelBytes.TabIndex = 3;
            this.labelBytes.Text = " ";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 196);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(572, 32);
            this.progressBar.TabIndex = 4;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(12, 78);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(249, 54);
            this.buttonReplace.TabIndex = 5;
            this.buttonReplace.Text = "Replace Links with Targets";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonRevert
            // 
            this.buttonRevert.Location = new System.Drawing.Point(335, 78);
            this.buttonRevert.Name = "buttonRevert";
            this.buttonRevert.Size = new System.Drawing.Size(249, 54);
            this.buttonRevert.TabIndex = 6;
            this.buttonRevert.Text = "Revert Changed Links";
            this.buttonRevert.UseVisualStyleBackColor = true;
            this.buttonRevert.Click += new System.EventHandler(this.buttonRevert_Click);
            // 
            // labelHappen
            // 
            this.labelHappen.AutoSize = true;
            this.labelHappen.Location = new System.Drawing.Point(21, 231);
            this.labelHappen.Name = "labelHappen";
            this.labelHappen.Size = new System.Drawing.Size(0, 13);
            this.labelHappen.TabIndex = 7;
            // 
            // checkBoxSearchOp
            // 
            this.checkBoxSearchOp.AutoSize = true;
            this.checkBoxSearchOp.Checked = true;
            this.checkBoxSearchOp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchOp.Location = new System.Drawing.Point(335, 12);
            this.checkBoxSearchOp.Name = "checkBoxSearchOp";
            this.checkBoxSearchOp.Size = new System.Drawing.Size(117, 17);
            this.checkBoxSearchOp.TabIndex = 8;
            this.checkBoxSearchOp.Text = "Search subfolders?";
            this.checkBoxSearchOp.UseVisualStyleBackColor = true;
            this.checkBoxSearchOp.CheckedChanged += new System.EventHandler(this.checkBoxSearchOp_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 264);
            this.Controls.Add(this.checkBoxSearchOp);
            this.Controls.Add(this.labelHappen);
            this.Controls.Add(this.buttonRevert);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelBytes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxMainDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Desymbolizer";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMainDir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelBytes;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonRevert;
        private System.Windows.Forms.Label labelHappen;
        private System.Windows.Forms.CheckBox checkBoxSearchOp;
    }
}

