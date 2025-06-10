using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace MatNumUpdater
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
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
                serialPort.Close();
                buttonConnect.Text = "Connect";
                comboBoxPorts.Enabled = true;
            }
            else
            {
                try
                {
                    serialPort.PortName = comboBoxPorts.Text;
                    serialPort.BaudRate = 9600; // adjust if needed
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

        private void buttonSend_Click_1(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                string text = textBoxInput.Text;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        string messageToSend = "MatNum," + text;
                        serialPort.Write(messageToSend);
                        textBoxInput.Clear();
                    }
                    catch (Exception ex)
                    {
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
    }
}
