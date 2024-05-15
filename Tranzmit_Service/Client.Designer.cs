namespace Tranzmit___Client
{
    partial class Client
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
            openFileDialog1 = new OpenFileDialog();
            Connet = new Button();
            Browse = new Button();
            Send = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Connet
            // 
            Connet.Location = new Point(24, 55);
            Connet.Name = "Connet";
            Connet.Size = new Size(112, 34);
            Connet.TabIndex = 0;
            Connet.Text = "Connect";
            Connet.UseVisualStyleBackColor = true;
            Connet.Click += Connet_Click_1;
            // 
            // Browse
            // 
            Browse.Location = new Point(336, 159);
            Browse.Name = "Browse";
            Browse.Size = new Size(112, 34);
            Browse.TabIndex = 1;
            Browse.Text = "Browse";
            Browse.UseVisualStyleBackColor = true;
            Browse.Click += Browse_Click;
            // 
            // Send
            // 
            Send.Location = new Point(24, 252);
            Send.Name = "Send";
            Send.Size = new Size(112, 34);
            Send.TabIndex = 2;
            Send.Text = "Send";
            Send.UseVisualStyleBackColor = true;
            Send.Click += Send_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(24, 162);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(286, 31);
            textBox1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 450);
            Controls.Add(textBox1);
            Controls.Add(Send);
            Controls.Add(Browse);
            Controls.Add(Connet);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private Button Connet;
        private Button Browse;
        private Button Send;
        private TextBox textBox1;
    }
}
