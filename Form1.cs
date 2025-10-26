using System;
using System.IO;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MatNumUpdater
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private ToolTip toolTip1 = new ToolTip();
        private string currentMatNum = "";
        private string currentMatDate = "";
        private string placeholderText = "Enter MatNum (e.g. 1234AB)";
        private string initialMatNum = "";
        private bool waitingForNewMatNum = false;
        private bool eeprom_communicate = false;
        private bool expectingInitialMatNum = false;

        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            LoadAvailablePorts();
            SetPlaceholder();
            textBoxInput.Enter += textBoxInput_Enter;
            textBoxInput.Leave += textBoxInput_Leave;

            // Set default ActiveRows / ActiveColumns
            textBoxRowStart.Text = "01";
            textBoxRowEnd.Text = "60";
            textBoxColStart.Text = "01";
            textBoxColEnd.Text = "30";
        }

        private void LoadAvailablePorts()
        {
            comboBoxPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
                comboBoxPorts.SelectedIndex = 0;
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
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
                    expectingInitialMatNum = true;
                    serialPort.WriteLine("GetMatNum");
                    initialMatNum = "";
                    waitingForNewMatNum = false;
                    eeprom_communicate = true;
                    serialPort.WriteLine("GetMatDate");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("Please connect to UART first.");
                return;
            }

            string text = textBoxInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(text) || text == placeholderText)
            {
                MessageBox.Show("Please enter a valid MatNum.");
                return;
            }

            try
            {
                buttonSend.Enabled = false;

                // Send MatNum
                string messageToSend = $"MatNum,{text}";
                //serialPort.Write(messageToSend);
                await SendCommandAsync(messageToSend);
                waitingForNewMatNum = true;
                eeprom_communicate = true;

                // Send MatDate
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                messageToSend = $"MatDate,{date}";
                //serialPort.Write(messageToSend);
                await SendCommandAsync(messageToSend);

                textBoxInput.Clear();
                SetPlaceholder();

                // Send ActiveRows / ActiveColumns / MatLifeTime / MatActiveTime
                await SendCommandAsync($"ActiveRows,{textBoxRowStart.Text},{textBoxRowEnd.Text}");
                await SendCommandAsync($"ActiveColumns,{textBoxColStart.Text},{textBoxColEnd.Text}");
                await SendCommandAsync("MatLifeTime,0");
                await SendCommandAsync("MatActiveTime,0");
            }
            catch (Exception ex)
            {
                AppendLog("Error while sending: " + ex.Message);
            }
        }

        private async Task SendCommandAsync(string command)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write(command);
                    await Task.Delay(40);
                }
            }
            catch (Exception ex)
            {
                AppendLog("Error sending command: " + ex.Message);
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string line = serialPort.ReadLine().Trim();
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    if (line.StartsWith("MatNum,"))
                        HandleMatNum(line.Substring("MatNum,".Length));
                    else if (line.StartsWith("MatDate,"))
                        HandleMatDate(line.Substring("MatDate,".Length));
                    else if (line.Contains("Write Failed") || line.Contains("Read Failed"))
                    {
                        if (eeprom_communicate)
                        {
                            AppendLog("❌ Failed to communicate with the EEPROM memory");
                            eeprom_communicate = false;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                AppendLog("UART Error: " + ex.Message);
            }
        }

        private void HandleMatNum(string matNum)
        {
            currentMatNum = matNum;
            CurrentMatNumBox.Text = matNum;

            if (expectingInitialMatNum)
            {
                initialMatNum = matNum;
                expectingInitialMatNum = false;
                AppendLog("Initial MatNum read: " + matNum);
            }
            else if (waitingForNewMatNum)
            {
                if (currentMatNum != initialMatNum)
                {
                    waitingForNewMatNum = false;
                    buttonSend.Enabled = true;
                    if (!string.IsNullOrWhiteSpace(matNum) && matNum != "0")
                    {
                        AppendLog($"MatNum updated from {initialMatNum} to {matNum}");
                        SaveMatNumToCsv(matNum, currentMatDate);
                    }
                    initialMatNum = matNum;
                }
            }
        }

        private void HandleMatDate(string matDate)
        {
            currentMatDate = matDate;
            CurrentMatDateBox.Text = matDate;
        }

        private void SaveMatNumToCsv(string matNum, string matDate)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MatNumbersLog.csv");
            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Mat Number,Date Saved");
                }
                writer.WriteLine($"{matNum},{matDate}");
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

        private void textBoxInput_Enter(object sender, EventArgs e) => RemovePlaceholder();
        private void textBoxInput_Leave(object sender, EventArgs e) => SetPlaceholder();

        private void buttonRefreshPorts_Click(object sender, EventArgs e) => LoadAvailablePorts();

        private void buttonNewBoard_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("Please connect to UART first.");
                return;
            }

            initialMatNum = "";
            currentMatNum = "";
            currentMatDate = "";
            waitingForNewMatNum = false;
            eeprom_communicate = true;
            expectingInitialMatNum = true;
            buttonSend.Enabled = true;
            serialPort.WriteLine("GetMatNum");
            serialPort.WriteLine("GetMatDate");

            AppendLog("New board inserted – refreshing data.");
        }
    }
}
