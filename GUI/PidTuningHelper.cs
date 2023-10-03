﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using PidTuningHelper.DataPacket;
using PidTuningHelper.App;

namespace PidTuningHelper
{
    public partial class PidTuningHelper : Form
    {

        App.App pidTuningHelperApp;

        private int stateMachineUpdateKs;
        private int stateMachineUpdateKsCounter;
        private bool updateKs;

        public PidTuningHelper()
        {
            InitializeComponent();

            pidTuningHelperApp = new App.App(lineChart, serialPort);

            stateMachineUpdateKs = 0;
            stateMachineUpdateKsCounter = 0;
            updateKs = false;

            this.InitializeComboBoxes();
            this.SetItemsToDisconnectedMode();
            pidTuningHelperApp.ClearChart();
        }

        private void InitializeComboBoxes()
        {
            this.SetAvailablePorts();
            this.SetPredefinedItems();
        }

        private void SetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comPortCb.Items.AddRange(ports);
        }

        private void SetPredefinedItems()
        {
            baudrateCb.SelectedItem = "115200";
            dataBitsCb.SelectedItem = "8 bits";
            parityCb.SelectedItem = "None";
            stopBitsCb.SelectedItem = "1 bit";
            flowControlCb.SelectedItem = "None";
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.ClearDataPacketRx();

            try
            {
                if (comPortCb.Text != "")
                {
                    this.ConfigSerialPort();
                    serialPort.Open();

                    if (serialPort.IsOpen == true)
                    {
                        this.SetItemsToConnectedMode();
                        this.EnableTimer();
                    }
                    else
                    {
                        this.SetItemsToDisconnectedMode();
                        this.DisableTimer();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid port...", "Connection failure");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EnableTimer()
        {
            timer1.Enabled = true;
        }

        private void DisableTimer()
        {
            timer1.Enabled = false;
        }

        private void ConfigSerialPort()
        {
            serialPort.PortName = comPortCb.Text;
            serialPort.BaudRate = Convert.ToInt32(baudrateCb.Text);

            switch (parityCb.SelectedItem)
            {
                case "None":
                    serialPort.Parity = Parity.None;
                    break;
                case "Even":
                    serialPort.Parity = Parity.Even;
                    break;
                case "Odd":
                    serialPort.Parity = Parity.Odd;
                    break;
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }

            switch (dataBitsCb.SelectedItem)
            {
                case "7 bits":
                    serialPort.DataBits = 7;
                    break;
                case "8 bits":
                    serialPort.DataBits = 8;
                    break;
                default:
                    serialPort.DataBits = 8;
                    break;
            }

            switch (stopBitsCb.SelectedItem)
            {
                case "1 bit":
                    serialPort.StopBits = StopBits.One;
                    break;
                case "1.5 bit":
                    serialPort.StopBits = StopBits.OnePointFive;
                    break;
                case "2 bit":
                    serialPort.StopBits = StopBits.Two;
                    break;
                default:
                    serialPort.StopBits = StopBits.One;
                    break;
            }

            switch (flowControlCb.SelectedItem)
            {
                case "None":
                    serialPort.Handshake = Handshake.None;
                    break;
                case "Xon/Xoff":
                    serialPort.Handshake = Handshake.XOnXOff;
                    break;
                default:
                    serialPort.Handshake = Handshake.None;
                    break;
            }
        }

        private void SetItemsToConnectedMode()
        {
            openBtn.Enabled = false;
            comPortCb.Enabled = false;
            baudrateCb.Enabled = false;
            dataBitsCb.Enabled = false;
            parityCb.Enabled = false;
            stopBitsCb.Enabled = false;
            flowControlCb.Enabled = false;
            closeBtn.Enabled = true;
            startBtn.Enabled = true;
            stopBtn.Enabled = true;
            loadDataBtn.Enabled = false;
            startPidBtn.Enabled = true;
            stopPidBtn.Enabled = true;
            setConfigDataBtn.Enabled = true;
            readConfigDataBtn.Enabled = true;
            setKsBtn.Enabled = true;
            kpTxtBox.Enabled = true;
            kiTxtBox.Enabled = true;
            kdTxtBox.Enabled = true;
            pidSetpointTxtBox.Enabled = true;
            pidDelayTxtBox.Enabled = true;
            samplingDelayTxtBox.Enabled = true;
            portStatusPb.Value = 100;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.ClearDataPacketRx();

            if (serialPort.IsOpen)
            {
                serialPort.Close();

                if (serialPort.IsOpen == false)
                {
                    this.SetItemsToDisconnectedMode();
                    this.DisableTimer();
                }
            }
        }

        private void SetItemsToDisconnectedMode()
        {
            openBtn.Enabled = true;
            comPortCb.Enabled = true;
            baudrateCb.Enabled = true;
            dataBitsCb.Enabled = true;
            parityCb.Enabled = true;
            stopBitsCb.Enabled = true;
            flowControlCb.Enabled = true;
            closeBtn.Enabled = false;
            startBtn.Enabled = false;
            stopBtn.Enabled = false;
            saveDataBtn.Enabled = false;
            loadDataBtn.Enabled = true;
            startPidBtn.Enabled = false;
            stopPidBtn.Enabled = false;
            setConfigDataBtn.Enabled = false;
            readConfigDataBtn.Enabled = false;
            setKsBtn.Enabled = false;
            kpTxtBox.Enabled = false;
            kiTxtBox.Enabled = false;
            kdTxtBox.Enabled = false;
            pidSetpointTxtBox.Enabled = false;
            pidDelayTxtBox.Enabled = false;
            samplingDelayTxtBox.Enabled = false;
            portStatusPb.Value = 0;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    int receivedByte = serialPort.ReadByte();
                    if (receivedByte >= 0)
                    {
                        pidTuningHelperApp.AppendNewByte(receivedByte);
                    }
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("TimeoutException", "There was an exception.");
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            saveDataBtn.Enabled = false;
            loadDataBtn.Enabled = false;
            pidTuningHelperApp.StartDataAquisitionSendCommand();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.StopDataAquisitionSendCommand();
            loadDataBtn.Enabled = true;
            if (pidTuningHelperApp.LineChartContainsPoints() == true)
            {
                saveDataBtn.Enabled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pidTuningHelperApp.IncrementCounterTimer1();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pidTuningHelperApp.ExecuteStateMachine();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if ((updateKs == true) && (stateMachineUpdateKsCounter >= (int)Delay._100ms))
            {
                stateMachineUpdateKsCounter = 0;
                timer3.Enabled = false;
                updatePidKsParameters();
            }
            stateMachineUpdateKsCounter++;
        }

        private void updatePidKsParameters()
        {
            switch (stateMachineUpdateKs)
            {
                case 0:
                    string kpStr = kpTxtBox.Text.Trim();
                    if (!String.IsNullOrEmpty(kpStr))
                    {
                        if (int.TryParse(kpStr, out int kpResult))
                        {
                            pidTuningHelperApp.SetKpSendCommand(kpResult);
                        }
                    }
                    stateMachineUpdateKs = 1;
                    timer3.Enabled = true;
                    break;

                case 1:
                    string kiStr = kiTxtBox.Text.Trim();
                    if (!String.IsNullOrEmpty(kiStr))
                    {
                        if (int.TryParse(kiStr, out int kiResult))
                        {
                            pidTuningHelperApp.SetKiSendCommand(kiResult);
                        }
                    }
                    stateMachineUpdateKs = 2;
                    timer3.Enabled = true;
                    break;

                case 2:
                    string kdStr = kdTxtBox.Text.Trim();
                    if (!String.IsNullOrEmpty(kdStr))
                    {
                        if (int.TryParse(kdStr, out int kdResult))
                        {
                            pidTuningHelperApp.SetKdSendCommand(kdResult);
                        }
                    }
                    stateMachineUpdateKs = 0;
                    updateKs = false;
                    stateMachineUpdateKsCounter = 0;
                    timer3.Enabled = false;
                    startPidBtn.Enabled = true;
                    stopPidBtn.Enabled = true;
                    setConfigDataBtn.Enabled = true;
                    readConfigDataBtn.Enabled = true;
                    setKsBtn.Enabled = true;
                    break;

                default:
                    stateMachineUpdateKs = 0;
                    updateKs = false;
                    stateMachineUpdateKsCounter = 0;
                    timer3.Enabled = false;
                    startPidBtn.Enabled = true;
                    stopPidBtn.Enabled = true;
                    setConfigDataBtn.Enabled = true;
                    readConfigDataBtn.Enabled = true;
                    setKsBtn.Enabled = true;
                    break;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.ClearChart();
            saveDataBtn.Enabled = false;

            if (serialPort.IsOpen == true)
            {
                startBtn.Enabled = true;
            }
        }

        private void saveDataBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.SaveData();
        }

        private void loadDataBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            pidTuningHelperApp.LoadData();
        }

        private void resizeBtn_Click(object sender, EventArgs e)
        {
            string xMinStr = minXTxtBox.Text.Trim();
            string xMaxStr = maxXTxtBox.Text.Trim();
            string yMinStr = minYTxtBox.Text.Trim();
            string yMaxStr = maxYTxtBox.Text.Trim();

            int xMinInt = 0;
            int xMaxInt = 0;
            int yMinInt = 0;
            int yMaxInt = 0;

            if (!String.IsNullOrEmpty(xMinStr) && !String.IsNullOrEmpty(xMaxStr))
            {
                if (int.TryParse(xMinStr, out int xMinResult))
                {
                    if (Math.Abs(xMinResult) <= 5000)
                    {
                        xMinInt = xMinResult;
                    }
                }

                if (int.TryParse(xMaxStr, out int xMaxResult))
                {
                    if (Math.Abs(xMaxResult) <= 5000)
                    {
                        xMaxInt = xMaxResult;
                    }
                }

                pidTuningHelperApp.ResizeChartAxisX(xMinInt, xMaxInt);
            }

            if (!String.IsNullOrEmpty(yMinStr) && !String.IsNullOrEmpty(yMaxStr))
            {
                if (int.TryParse(yMinStr, out int yMinResult))
                {
                    if (Math.Abs(yMinResult) <= 5000)
                    {
                        yMinInt = yMinResult;
                    }
                }

                if (int.TryParse(yMaxStr, out int yMaxResult))
                {
                    if (Math.Abs(yMaxResult) <= 5000)
                    {
                        yMaxInt = yMaxResult;
                    }
                }

                pidTuningHelperApp.ResizeChartAxisY(yMinInt, yMaxInt);
            }
        }

        private void setKsBtn_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
            updateKs = true;

            startPidBtn.Enabled = false;
            stopPidBtn.Enabled = false;
            setConfigDataBtn.Enabled = false;
            readConfigDataBtn.Enabled = false;
            setKsBtn.Enabled = false;
        }

    }
}
