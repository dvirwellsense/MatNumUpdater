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
            this.CurrentMatNumBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentMatDateBox = new System.Windows.Forms.TextBox();
            this.buttonRefreshPorts = new System.Windows.Forms.Button();
            this.buttonNewBoard = new System.Windows.Forms.Button();
            this.textBoxRowStart = new System.Windows.Forms.TextBox();
            this.textBoxRowEnd = new System.Windows.Forms.TextBox();
            this.textBoxColStart = new System.Windows.Forms.TextBox();
            this.textBoxColEnd = new System.Windows.Forms.TextBox();
            this.labelRowStart = new System.Windows.Forms.Label();
            this.labelRowEnd = new System.Windows.Forms.Label();
            this.labelColStart = new System.Windows.Forms.Label();
            this.labelColEnd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(30, 173);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(564, 30);
            this.textBoxInput.TabIndex = 3;
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Location = new System.Drawing.Point(196, 224);
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
            this.buttonConnect.Location = new System.Drawing.Point(394, 22);
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
            this.Title.Location = new System.Drawing.Point(200, 21);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(215, 32);
            this.Title.TabIndex = 0;
            this.Title.Text = "MatNum Updater";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(30, 400);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(564, 80);
            this.textBoxLog.TabIndex = 5;
            // 
            // CurrentMatNumBox
            // 
            this.CurrentMatNumBox.Location = new System.Drawing.Point(169, 123);
            this.CurrentMatNumBox.Name = "CurrentMatNumBox";
            this.CurrentMatNumBox.Size = new System.Drawing.Size(130, 30);
            this.CurrentMatNumBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current MatNum:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Burning date:";
            // 
            // CurrentMatDateBox
            // 
            this.CurrentMatDateBox.Location = new System.Drawing.Point(464, 123);
            this.CurrentMatDateBox.Name = "CurrentMatDateBox";
            this.CurrentMatDateBox.Size = new System.Drawing.Size(130, 30);
            this.CurrentMatDateBox.TabIndex = 9;
            // 
            // buttonRefreshPorts
            // 
            this.buttonRefreshPorts.Location = new System.Drawing.Point(326, 71);
            this.buttonRefreshPorts.Name = "buttonRefreshPorts";
            this.buttonRefreshPorts.Size = new System.Drawing.Size(36, 33);
            this.buttonRefreshPorts.TabIndex = 0;
            this.buttonRefreshPorts.Text = "🔄";
            this.buttonRefreshPorts.UseVisualStyleBackColor = true;
            this.buttonRefreshPorts.Click += new System.EventHandler(this.buttonRefreshPorts_Click);
            // 
            // buttonNewBoard
            // 
            this.buttonNewBoard.Location = new System.Drawing.Point(394, 75);
            this.buttonNewBoard.Name = "buttonNewBoard";
            this.buttonNewBoard.Size = new System.Drawing.Size(199, 31);
            this.buttonNewBoard.TabIndex = 10;
            this.buttonNewBoard.Text = "New Board";
            this.buttonNewBoard.UseVisualStyleBackColor = true;
            this.buttonNewBoard.Click += new System.EventHandler(this.buttonNewBoard_Click);
            // 
            // textBoxRowStart
            // 
            this.textBoxRowStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxRowStart.Location = new System.Drawing.Point(139, 320);
            this.textBoxRowStart.Name = "textBoxRowStart";
            this.textBoxRowStart.Size = new System.Drawing.Size(84, 30);
            this.textBoxRowStart.TabIndex = 11;
            this.textBoxRowStart.Text = "01";
            // 
            // textBoxRowEnd
            // 
            this.textBoxRowEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxRowEnd.Location = new System.Drawing.Point(234, 320);
            this.textBoxRowEnd.Name = "textBoxRowEnd";
            this.textBoxRowEnd.Size = new System.Drawing.Size(79, 30);
            this.textBoxRowEnd.TabIndex = 12;
            this.textBoxRowEnd.Text = "60";
            // 
            // textBoxColStart
            // 
            this.textBoxColStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxColStart.Location = new System.Drawing.Point(334, 320);
            this.textBoxColStart.Name = "textBoxColStart";
            this.textBoxColStart.Size = new System.Drawing.Size(74, 30);
            this.textBoxColStart.TabIndex = 13;
            this.textBoxColStart.Text = "01";
            // 
            // textBoxColEnd
            // 
            this.textBoxColEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxColEnd.Location = new System.Drawing.Point(417, 320);
            this.textBoxColEnd.Name = "textBoxColEnd";
            this.textBoxColEnd.Size = new System.Drawing.Size(71, 30);
            this.textBoxColEnd.TabIndex = 14;
            this.textBoxColEnd.Text = "30";
            // 
            // labelRowStart
            // 
            this.labelRowStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRowStart.Location = new System.Drawing.Point(139, 295);
            this.labelRowStart.Name = "labelRowStart";
            this.labelRowStart.Size = new System.Drawing.Size(84, 20);
            this.labelRowStart.TabIndex = 15;
            this.labelRowStart.Text = "Row Start";
            // 
            // labelRowEnd
            // 
            this.labelRowEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRowEnd.Location = new System.Drawing.Point(234, 295);
            this.labelRowEnd.Name = "labelRowEnd";
            this.labelRowEnd.Size = new System.Drawing.Size(79, 22);
            this.labelRowEnd.TabIndex = 16;
            this.labelRowEnd.Text = "Row End";
            // 
            // labelColStart
            // 
            this.labelColStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelColStart.Location = new System.Drawing.Point(334, 295);
            this.labelColStart.Name = "labelColStart";
            this.labelColStart.Size = new System.Drawing.Size(84, 22);
            this.labelColStart.TabIndex = 17;
            this.labelColStart.Text = "Col Start";
            // 
            // labelColEnd
            // 
            this.labelColEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelColEnd.Location = new System.Drawing.Point(417, 295);
            this.labelColEnd.Name = "labelColEnd";
            this.labelColEnd.Size = new System.Drawing.Size(71, 22);
            this.labelColEnd.TabIndex = 18;
            this.labelColEnd.Text = "Col End";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(624, 500);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.CurrentMatNumBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurrentMatDateBox);
            this.Controls.Add(this.buttonRefreshPorts);
            this.Controls.Add(this.buttonNewBoard);
            this.Controls.Add(this.textBoxRowStart);
            this.Controls.Add(this.textBoxRowEnd);
            this.Controls.Add(this.textBoxColStart);
            this.Controls.Add(this.textBoxColEnd);
            this.Controls.Add(this.labelRowStart);
            this.Controls.Add(this.labelRowEnd);
            this.Controls.Add(this.labelColStart);
            this.Controls.Add(this.labelColEnd);
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
        private System.Windows.Forms.TextBox CurrentMatNumBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CurrentMatDateBox;
        private System.Windows.Forms.Button buttonRefreshPorts;
        private System.Windows.Forms.Button buttonNewBoard;

        private System.Windows.Forms.TextBox textBoxRowStart;
        private System.Windows.Forms.TextBox textBoxRowEnd;
        private System.Windows.Forms.TextBox textBoxColStart;
        private System.Windows.Forms.TextBox textBoxColEnd;

        private System.Windows.Forms.Label labelRowStart;
        private System.Windows.Forms.Label labelRowEnd;
        private System.Windows.Forms.Label labelColStart;
        private System.Windows.Forms.Label labelColEnd;
    }
}
