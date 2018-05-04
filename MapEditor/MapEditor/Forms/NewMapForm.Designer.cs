namespace MapEditor.Content
{
    partial class NewMapForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mapNameText = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mapHeightBox = new System.Windows.Forms.NumericUpDown();
            this.mapWidthBox = new System.Windows.Forms.NumericUpDown();
            this.tileHeightBox = new System.Windows.Forms.NumericUpDown();
            this.tileWidthBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.mapHeightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileHeightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileWidthBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Map Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tile Height:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Map Width:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tile Width:";
            // 
            // mapNameText
            // 
            this.mapNameText.Location = new System.Drawing.Point(112, 29);
            this.mapNameText.Name = "mapNameText";
            this.mapNameText.Size = new System.Drawing.Size(213, 20);
            this.mapNameText.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(112, 193);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(226, 193);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // mapHeightBox
            // 
            this.mapHeightBox.Location = new System.Drawing.Point(112, 86);
            this.mapHeightBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.mapHeightBox.Name = "mapHeightBox";
            this.mapHeightBox.Size = new System.Drawing.Size(66, 20);
            this.mapHeightBox.TabIndex = 8;
            this.mapHeightBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // mapWidthBox
            // 
            this.mapWidthBox.Location = new System.Drawing.Point(263, 86);
            this.mapWidthBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.mapWidthBox.Name = "mapWidthBox";
            this.mapWidthBox.Size = new System.Drawing.Size(62, 20);
            this.mapWidthBox.TabIndex = 9;
            this.mapWidthBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // tileHeightBox
            // 
            this.tileHeightBox.Location = new System.Drawing.Point(112, 154);
            this.tileHeightBox.Name = "tileHeightBox";
            this.tileHeightBox.Size = new System.Drawing.Size(66, 20);
            this.tileHeightBox.TabIndex = 10;
            this.tileHeightBox.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // tileWidthBox
            // 
            this.tileWidthBox.Location = new System.Drawing.Point(263, 154);
            this.tileWidthBox.Name = "tileWidthBox";
            this.tileWidthBox.Size = new System.Drawing.Size(62, 20);
            this.tileWidthBox.TabIndex = 11;
            this.tileWidthBox.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // NewMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 235);
            this.Controls.Add(this.tileWidthBox);
            this.Controls.Add(this.tileHeightBox);
            this.Controls.Add(this.mapWidthBox);
            this.Controls.Add(this.mapHeightBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.mapNameText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewMapForm";
            this.Text = "NewMap";
            ((System.ComponentModel.ISupportInitialize)(this.mapHeightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileHeightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileWidthBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mapNameText;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown mapHeightBox;
        private System.Windows.Forms.NumericUpDown mapWidthBox;
        private System.Windows.Forms.NumericUpDown tileHeightBox;
        private System.Windows.Forms.NumericUpDown tileWidthBox;
    }
}