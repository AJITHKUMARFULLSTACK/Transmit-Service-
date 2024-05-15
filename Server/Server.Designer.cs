namespace Server
{
    partial class Server
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
            folderBrowserDialog1 = new FolderBrowserDialog();
            Server_Start = new Button();
            Save = new Button();
            saveFileDialog1 = new SaveFileDialog();
            SuspendLayout();
            // 
            // Server_Start
            // 
            Server_Start.Location = new Point(103, 184);
            Server_Start.Name = "Server_Start";
            Server_Start.Size = new Size(112, 34);
            Server_Start.TabIndex = 0;
            Server_Start.Text = "Start";
            Server_Start.UseVisualStyleBackColor = true;
            Server_Start.Click += Server_Start_Click;
            // 
            // Save
            // 
            Save.Location = new Point(268, 184);
            Save.Name = "Save";
            Save.Size = new Size(112, 34);
            Save.TabIndex = 1;
            Save.Text = "Save File ";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 450);
            Controls.Add(Save);
            Controls.Add(Server_Start);
            Name = "Form1";
            Text = "7";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog1;
        private Button Server_Start;
        private Button Save;
        private SaveFileDialog saveFileDialog1;
    }
}
