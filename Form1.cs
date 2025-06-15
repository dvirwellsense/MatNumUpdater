// Form1.cs
using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace MatNumUpdater
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private string currentMatNum = "";

        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            LoadAvailablePorts();
        }

        private void LoadAvailablePorts()
        {
            comboBoxPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
                comboBoxPorts.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.DataReceived -= serialPort_DataReceived;
                serialPort.Close();
                buttonConnect.Text = "Connect";
                comboBoxPorts.Enabled = true;
                //labelCurrentMatNum.Visible = true;
            }
            else
            {
                try
                {
                    serialPort.PortName = comboBoxPorts.Text;
                    serialPort.BaudRate = 9600;
                    serialPort.DataReceived += serialPort_DataReceived;
                    serialPort.Open();
                    buttonConnect.Text = "Disconnect";
                    comboBoxPorts.Enabled = false;
                    //labelCurrentMatNum.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                string text = textBoxInput.Text;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        string messageToSend = "MatNum," + text;
                        serialPort.WriteLine(messageToSend);
                        AppendLog("Sent: " + messageToSend);
                        textBoxInput.Clear();
                    }
                    catch (Exception ex)
                    {
                        AppendLog("Error while sending: " + ex.Message);
                        MessageBox.Show("Error while sending: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter text to send.");
                }
            }
            else
            {
                MessageBox.Show("Please connect to UART first.");
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string line = serialPort.ReadLine().Trim();

                if (line.StartsWith("MatNum,"))
                {
                    string matNum = line.Substring("MatNum,".Length);
                    currentMatNum = matNum;

                    this.Invoke((MethodInvoker)(() =>
                    {
                        labelCurrentMatNum.Text = $"Current MatNum: {matNum}";
                    }));
                }
                else if (line.StartsWith("MatNum Updated:"))
                {
                    string updated = line.Substring("MatNum Updated:".Length).Trim();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show($"MatNum was successfully updated to: {updated}", "Update Successful");
                        textBoxLog.AppendText("✅ Update successful: " + updated + Environment.NewLine);
                    }));
                }
                else if (line.StartsWith("MatNum Update Failed"))
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show("MatNum update failed!", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLog.AppendText("❌ Update failed" + Environment.NewLine);
                    }));
                }
            }
            catch (Exception ex)
            {
                //this.Invoke((MethodInvoker)(() =>
                //{
                //    textBoxLog.AppendText("⚠ Error reading data: " + ex.Message + Environment.NewLine);
                //}));
            }
        }

        private void AppendLog(string msg)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke(new Action(() => AppendLog(msg)));
                return;
            }

            textBoxLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
        }
    }
}
