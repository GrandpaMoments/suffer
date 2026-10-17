namespace PLEASEWORK
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            KeyInputBox = new TextBox();
            TextInputBox = new TextBox();
            TextOutputBox = new TextBox();
            DecodeButton = new Button();
            EncodeButton = new Button();
            KeyOutputBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Save = new Button();
            LoadFile = new Button();
            FolderName = new TextBox();
            SuspendLayout();
            // 
            // KeyInputBox
            // 
            KeyInputBox.Font = new Font("Courier New", 9F);
            KeyInputBox.Location = new Point(12, 410);
            KeyInputBox.Name = "KeyInputBox";
            KeyInputBox.PlaceholderText = "Key";
            KeyInputBox.Size = new Size(375, 28);
            KeyInputBox.TabIndex = 0;
            // 
            // TextInputBox
            // 
            TextInputBox.Font = new Font("Segoe UI Emoji", 9F);
            TextInputBox.Location = new Point(12, 49);
            TextInputBox.Multiline = true;
            TextInputBox.Name = "TextInputBox";
            TextInputBox.PlaceholderText = "Input";
            TextInputBox.ScrollBars = ScrollBars.Vertical;
            TextInputBox.Size = new Size(375, 355);
            TextInputBox.TabIndex = 2;
            // 
            // TextOutputBox
            // 
            TextOutputBox.BackColor = SystemColors.ButtonHighlight;
            TextOutputBox.Font = new Font("Segoe UI Emoji", 9F);
            TextOutputBox.Location = new Point(413, 49);
            TextOutputBox.Multiline = true;
            TextOutputBox.Name = "TextOutputBox";
            TextOutputBox.PlaceholderText = "Output";
            TextOutputBox.ReadOnly = true;
            TextOutputBox.ScrollBars = ScrollBars.Vertical;
            TextOutputBox.Size = new Size(375, 355);
            TextOutputBox.TabIndex = 3;
            // 
            // DecodeButton
            // 
            DecodeButton.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DecodeButton.Location = new Point(304, 11);
            DecodeButton.Name = "DecodeButton";
            DecodeButton.Size = new Size(83, 28);
            DecodeButton.TabIndex = 4;
            DecodeButton.Text = "Decode";
            DecodeButton.UseVisualStyleBackColor = true;
            DecodeButton.Click += DecodeButton_Click;
            // 
            // EncodeButton
            // 
            EncodeButton.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EncodeButton.Location = new Point(215, 11);
            EncodeButton.Name = "EncodeButton";
            EncodeButton.Size = new Size(83, 28);
            EncodeButton.TabIndex = 6;
            EncodeButton.Text = "Encode";
            EncodeButton.UseVisualStyleBackColor = true;
            EncodeButton.Click += EncodeButton_Click;
            // 
            // KeyOutputBox
            // 
            KeyOutputBox.BackColor = SystemColors.ButtonHighlight;
            KeyOutputBox.Font = new Font("Courier New", 9F);
            KeyOutputBox.Location = new Point(413, 410);
            KeyOutputBox.Name = "KeyOutputBox";
            KeyOutputBox.PlaceholderText = "Key";
            KeyOutputBox.ReadOnly = true;
            KeyOutputBox.Size = new Size(375, 28);
            KeyOutputBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(413, 15);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 7;
            label1.Text = "Output";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 8;
            label2.Text = "Input";
            // 
            // Save
            // 
            Save.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Save.Location = new Point(754, 11);
            Save.Name = "Save";
            Save.Size = new Size(34, 28);
            Save.TabIndex = 9;
            Save.Text = "💾";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // LoadFile
            // 
            LoadFile.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoadFile.Location = new Point(175, 12);
            LoadFile.Name = "LoadFile";
            LoadFile.Size = new Size(34, 28);
            LoadFile.TabIndex = 10;
            LoadFile.Text = "📂";
            LoadFile.UseVisualStyleBackColor = true;
            LoadFile.Click += LoadFile_Click;
            // 
            // FolderName
            // 
            FolderName.Font = new Font("Courier New", 9F);
            FolderName.ForeColor = SystemColors.WindowText;
            FolderName.Location = new Point(494, 11);
            FolderName.Name = "FolderName";
            FolderName.PlaceholderText = "Folder Name (optional)";
            FolderName.Size = new Size(254, 28);
            FolderName.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(FolderName);
            Controls.Add(LoadFile);
            Controls.Add(Save);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(EncodeButton);
            Controls.Add(DecodeButton);
            Controls.Add(TextOutputBox);
            Controls.Add(TextInputBox);
            Controls.Add(KeyOutputBox);
            Controls.Add(KeyInputBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox KeyInputBox;
        private TextBox TextInputBox;
        private TextBox TextOutputBox;
        private Button DecodeButton;
        private Button EncodeButton;
        private TextBox KeyOutputBox;
        private Label label1;
        private Label label2;
        private Button Save;
        private Button LoadFile;
        private TextBox FolderName;
    }
}
