namespace MatNumUpdater
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelCurrentMatNum = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(30, 130);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(510, 30);
            this.textBoxInput.TabIndex = 3;

            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Location = new System.Drawing.Point(180, 180);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(240, 40);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Send MatNum";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);

            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.Location = new System.Drawing.Point(120, 75);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(200, 31);
            this.comboBoxPorts.TabIndex = 2;

            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.Red;
            this.buttonConnect.Location = new System.Drawing.Point(340, 70);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(200, 35);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);

            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(30, 80);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(95, 23);
            this.labelPort.TabIndex = 3;
            this.labelPort.Text = "Select Port:";

            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(180, 20);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(215, 32);
            this.Title.TabIndex = 0;
            this.Title.Text = "MatNum Updater";

            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(30, 270);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(510, 110);
            this.textBoxLog.TabIndex = 5;
            this.textBoxLog.ReadOnly = true;

            // 
            // labelCurrentMatNum
            // 
            this.labelCurrentMatNum.AutoSize = true;
            this.labelCurrentMatNum.Location = new System.Drawing.Point(30, 230);
            this.labelCurrentMatNum.Name = "labelCurrentMatNum";
            this.labelCurrentMatNum.Size = new System.Drawing.Size(230, 23);
            this.labelCurrentMatNum.TabIndex = 6;
            this.labelCurrentMatNum.Text = "";

            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(580, 410);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.labelCurrentMatNum);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MatNum Updater";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelCurrentMatNum;
    }
}
