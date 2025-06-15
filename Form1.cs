// Form1.cs
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace MatNumUpdater
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private string currentMatNum = "";
        private string placeholderText = "Enter MatNum (e.g. 1234AB)";

        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            LoadAvailablePorts();
            SetPlaceholder();
            textBoxInput.Enter += textBoxInput_Enter;
            textBoxInput.Leave += textBoxInput_Leave;
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
                labelCurrentMatNum.Visible = false;
                AppendLog("Disconnected.");
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
                    labelCurrentMatNum.Visible = true;
                    AppendLog("Connected to " + serialPort.PortName);
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
                if (!string.IsNullOrWhiteSpace(text) && text != placeholderText)
                {
                    try
                    {
                        string messageToSend = "MatNum," + text;
                        serialPort.WriteLine(messageToSend);
                        AppendLog("Sent: " + messageToSend);
                        textBoxInput.Clear();
                        SetPlaceholder();
                    }
                    catch (Exception ex)
                    {
                        AppendLog("Error while sending: " + ex.Message);
                        MessageBox.Show("Error while sending: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid MatNum.");
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

                this.Invoke((MethodInvoker)(() =>
                {

                    if (line.StartsWith("MatNum,"))
                    {
                        string matNum = line.Substring("MatNum,".Length);
                        currentMatNum = matNum;
                        labelCurrentMatNum.Text = $"Current MatNum: {matNum}";
                    }
                    else if (line.StartsWith("MatNum Updated:"))
                    {
                        string updated = line.Substring("MatNum Updated:".Length).Trim();
                        MessageBox.Show($"MatNum was successfully updated to: {updated}", "Update Successful");
                        textBoxLog.AppendText("✅ Update successful: " + updated + Environment.NewLine);
                    }
                    else if (line.StartsWith("MatNum Update Failed"))
                    {
                        MessageBox.Show("MatNum update failed!", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLog.AppendText("❌ Update failed" + Environment.NewLine);
                    }
                }));
            }
            catch { }
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

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBoxInput.Text))
            {
                textBoxInput.Text = placeholderText;
                textBoxInput.ForeColor = Color.Gray;
            }
        }

        private void RemovePlaceholder()
        {
            if (textBoxInput.Text == placeholderText)
            {
                textBoxInput.Text = "";
                textBoxInput.ForeColor = Color.Black;
            }
        }

        private void textBoxInput_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder();
        }

        private void textBoxInput_Leave(object sender, EventArgs e)
        {
            SetPlaceholder();
        }
    }
}
