
namespace MatNumUpdater
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
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Form
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "Form1";
            this.Text = "MatNum Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // 
            // labelTitle
            // 
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelTitle.Text = "MatNum Updater";
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(180, 20);
            this.Controls.Add(this.labelTitle);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(340, 70);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(200, 35);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            this.Controls.Add(this.buttonConnect);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.Location = new System.Drawing.Point(120, 75);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(200, 30);
            this.comboBoxPorts.TabIndex = 2;
            this.Controls.Add(this.comboBoxPorts);
            // 
            // labelPort
            // 
            this.labelPort.Text = "Select Port:";
            this.labelPort.Location = new System.Drawing.Point(30, 80);
            this.labelPort.AutoSize = true;
            this.Controls.Add(this.labelPort);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(30, 130);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(510, 30);
            this.textBoxInput.TabIndex = 3;
            this.Controls.Add(this.textBoxInput);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(180, 180);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(240, 40);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Send MatNum";
            this.buttonSend.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click_1);
            this.Controls.Add(this.buttonSend);

            this.ResumeLayout(false);
        }


        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelTitle;
    }
}

