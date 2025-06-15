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
        private string lastSentMatNum = "";

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
                string text = textBoxInput.Text.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        lastSentMatNum = text;
                        string messageToSend = "MatNum," + text;
                        serialPort.WriteLine(messageToSend);
                        textBoxInput.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while sending: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter MatNum to send.");
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
                    this.Invoke((MethodInvoker)(() => {
                        labelCurrentMatNum.Text = $"Current MatNum: {matNum}";
                    }));
                }
                else if (line.StartsWith("MatNum Updated:"))
                {
                    string updated = line.Substring("MatNum Updated:".Length).Trim();
                    this.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show($"MatNum was successfully updated to: {updated}", "Update Successful");
                    }));
                }
                else if (line.StartsWith("MatNum Save Failed"))
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show("MatNum update failed!", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
            catch { }
        }
    }
}
