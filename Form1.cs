using System;
using System.IO;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

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
            InitializeTooltips();
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
                    //AppendLog("Sent: GetMatNum");
                    serialPort.WriteLine("GetMatDate");
                    //AppendLog("Sent: GetMatDate");
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
                        serialPort.Write(messageToSend);
                        waitingForNewMatNum = true;
                        eeprom_communicate = true;
                        string date = DateTime.Now.ToString("yyyy-MM-dd");
                        messageToSend = $"MatDate,{date}";
                        serialPort.Write(messageToSend);
                        textBoxInput.Clear();
                        SetPlaceholder();
                        serialPort.WriteLine("GetMatDate");
                        serialPort.Write("MatLifeTime,0");
                        serialPort.Write("MatActiveTime,0");
                    }
                    catch (Exception ex)
                    {
                        AppendLog("Error while sending: " + ex.Message);
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

                        if (expectingInitialMatNum)
                        {
                            initialMatNum = matNum;
                            expectingInitialMatNum = false;
                            AppendLog("Initial MatNum read: " + matNum);
                        }
                        else if (waitingForNewMatNum)
                        {
                            if (matNum != initialMatNum)
                            {
                                waitingForNewMatNum = false;

                                if (!string.IsNullOrWhiteSpace(matNum) && matNum != "0")
                                {
                                    AppendLog("MatNum updated from " + initialMatNum + " to " + matNum);
                                    SaveMatNumToCsv(matNum, currentMatDate);
                                }
                                initialMatNum = matNum;
                            }
                            else
                            {
                                // MatNum did not change
                            }
                        }
                    }

                    else if (line.StartsWith("MatDate,"))
                    {
                        string matDate = line.Substring("MatDate,".Length);
                        currentMatDate = matDate;
                        CurrentMatDateBox.Text = $"{matDate}";
                        buttonSend.Enabled = true;
                    }
                    else if (line.StartsWith("Write Failed"))
                    {
                        if (eeprom_communicate)
                        {
                            textBoxLog.AppendText("❌ Failed to communicate with the EEPROM memory" + Environment.NewLine);
                            eeprom_communicate = false;
                        }
                    }
                    
                    else if (line.Contains("Read Failed"))
                    {
                        if (eeprom_communicate)
                        {
                            textBoxLog.AppendText("❌ Failed to communicate with the EEPROM memory" + Environment.NewLine);
                            eeprom_communicate = false;
                        }
                    }
                }));
            }
            catch { }
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

        private void textBoxInput_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder();
        }

        private void textBoxInput_Leave(object sender, EventArgs e)
        {
            SetPlaceholder();
        }

        private void buttonRefreshPorts_Click(object sender, EventArgs e)
        {
            LoadAvailablePorts();
            //AppendLog("Ports refreshed.");
        }

        private void buttonNewBoard_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                //initialMatNum = "";
                //waitingForNewMatNum = false;
                //eeprom_communicate = true;

                initialMatNum = "";
                currentMatNum = "";
                currentMatDate = "";
                waitingForNewMatNum = false;
                eeprom_communicate = true;
                expectingInitialMatNum = true;
                serialPort.WriteLine("GetMatNum");
                serialPort.WriteLine("GetMatDate");

                AppendLog("New board inserted – refreshing data.");
            }
            else
            {
                MessageBox.Show("Please connect to UART first.");
            }
        }
    }
}
