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
        private string currentMatDate = "";
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
                buttonConnect.BackColor = Color.Red;
                comboBoxPorts.Enabled = true;
                CurrentMatNumBox.Visible = false;
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
                    buttonConnect.BackColor = Color.Green;
                    comboBoxPorts.Enabled = false;
                    CurrentMatNumBox.Visible = true;
                    AppendLog("Connected to " + serialPort.PortName);
                    // Request latest saved MatNum + Date in the beginning
                    serialPort.WriteLine("GetMatNum");
                    AppendLog("Sent: GetMatNum");
                    serialPort.WriteLine("GetMatDate");
                    AppendLog("Sent: GetMatDate");
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
                        buttonSend.Enabled = false;
                        string messageToSend = $"MatNum,{text}";
                        serialPort.WriteLine(messageToSend);
                        AppendLog("Sent: " + messageToSend);
                        string date = DateTime.Now.ToString("yyyy-MM-dd");
                        messageToSend = $"MatDate,{date}";
                        serialPort.WriteLine(messageToSend);
                        AppendLog("Sent: " + messageToSend);
                        textBoxInput.Clear();
                        SetPlaceholder();
                        serialPort.WriteLine("GetMatDate");
                        AppendLog("Sent: GetMatDate");
                        serialPort.WriteLine("MatLifeTime");
                        AppendLog("Sent: MatLifeTime");
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
                        CurrentMatNumBox.Text = $"{matNum}";
                    }
                    else if (line.StartsWith("Write Failed"))
                    {
                        //MessageBox.Show("Mat data update failed!", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLog.AppendText("❌ Update failed" + Environment.NewLine);
                    }
                    else if (line.StartsWith("MatDate,"))
                    {
                        string matDate = line.Substring("MatDate,".Length);
                        currentMatDate = matDate;
                        CurrentMatDateBox.Text = $"{matDate}";
                        buttonSend.Enabled = true;
                    }
                    else if (line.Contains("Read Failed"))
                    {
                        //MessageBox.Show("Mat data read failed!", "Read Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxLog.AppendText("❌ Read failed" + Environment.NewLine);
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
