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

        public PidTuningHelper()
        {
            InitializeComponent();

            pidTuningHelperApp = new App.App(lineChart, serialPort);

            this.InitializeComboBoxes();
            this.SetItemsToDisconnectedMode();
            pidTuningHelperApp.ClearChart();

            pidTuningHelperApp.SetCurrentKpLabel(currentKpLbl);
            pidTuningHelperApp.SetCurrentKiLabel(currentKiLbl);
            pidTuningHelperApp.SetCurrentKdLabel(currentKdLbl);
            pidTuningHelperApp.SetCurrentPidIntervalLabel(currentPidIntervalLbl);
            pidTuningHelperApp.SetCurrentPidSetpointLabel(currentPidSetpointLbl);
            pidTuningHelperApp.SetCurrentSamplingIntervalLabel(currentSamplingIntervalLbl);
            pidTuningHelperApp.SetCurrentMovAverWinLabel(currentMovAverWinLbl);
            pidTuningHelperApp.SetCurrentMinSumOfErrorsLabel(currentMinSumOfErrorsLbl);
            pidTuningHelperApp.SetCurrentMaxSumOfErrorsLabel(currentMaxSumOfErrorsLbl);
            pidTuningHelperApp.SetCurrentMinControlledVariableLabel(currentMinContrVarLbl);
            pidTuningHelperApp.SetCurrentMaxControlledVariableLabel(currentMaxContrVarLbl);
            pidTuningHelperApp.SetCurrentOffsetLabel(currentPidOffsetLbl);
            pidTuningHelperApp.SetCurrentBiasLabel(currentPidBiasLbl);
            pidTuningHelperApp.SetCurrentPidControllerStatusLabel(currentPidControllerStatusLabel);

            pidTuningHelperApp.SetKpTxtBox(kpTxtBox);
            pidTuningHelperApp.SetKdTxtBox(kdTxtBox);
            pidTuningHelperApp.SetKiTxtBox(kiTxtBox);
            pidTuningHelperApp.SetPidIntervalTxtBox(pidIntervalTxtBox);
            pidTuningHelperApp.SetPidSetpointTxtBox(pidSetpointTxtBox);
            pidTuningHelperApp.SetSamplingIntervalTxtBox(samplingIntervalTxtBox);
            pidTuningHelperApp.SetMovAverWinTxtBox(movAverageWinTxtBox);
            pidTuningHelperApp.SetMinSumOfErrorsTxtBox(minSumOfErrorsTxtBox);
            pidTuningHelperApp.SetMaxSumOfErrorsTxtBox(maxSumOfErrorsTxtBox);
            pidTuningHelperApp.SetMinControlledVariableTxtBox(minControlledVarTxtBox);
            pidTuningHelperApp.SetMaxControlledVariableTxtBox(maxControlledVarTxtBox);
            pidTuningHelperApp.SetOffsetTxtBox(pidOffsetTxtBox);
            pidTuningHelperApp.SetBiasTxtBox(pidBiasTxtBox);
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
                        this.SetItemsToConnectedModeWithoutCommunication();
                    }
                    else
                    {
                        this.SetItemsToDisconnectedMode();
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

        private void closeBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.ClearDataPacketRx();

            if (serialPort.IsOpen)
            {
                serialPort.Close();

                if (serialPort.IsOpen == false)
                {
                    this.SetItemsToDisconnectedMode();
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
            setPidSetpointBtn.Enabled = false;
            getPidSetpointBtn.Enabled = false;
            kpTxtBox.Enabled = false;
            kiTxtBox.Enabled = false;
            kdTxtBox.Enabled = false;
            pidSetpointTxtBox.Enabled = false;
            pidIntervalTxtBox.Enabled = false;
            samplingIntervalTxtBox.Enabled = false;
            movAverageWinTxtBox.Enabled = false;
            pidOffsetTxtBox.Enabled = false;
            minSumOfErrorsTxtBox.Enabled = false;
            maxSumOfErrorsTxtBox.Enabled = false;
            minControlledVarTxtBox.Enabled = false;
            maxControlledVarTxtBox.Enabled = false;
            pidBiasTxtBox.Enabled = false;
            portStatusPb.Value = 0;
            timer1.Enabled = false;

            kpTxtBox.Clear();
            kiTxtBox.Clear();
            kdTxtBox.Clear();
            pidSetpointTxtBox.Clear();
            pidIntervalTxtBox.Clear();
            samplingIntervalTxtBox.Clear();
            movAverageWinTxtBox.Clear();
            pidOffsetTxtBox.Clear();
            minSumOfErrorsTxtBox.Clear();
            maxSumOfErrorsTxtBox.Clear();
            minControlledVarTxtBox.Clear();
            maxControlledVarTxtBox.Clear();
            pidBiasTxtBox.Clear();

            currentPidControllerStatusLabel.Text = "...";
            currentKpLbl.Text = "...";
            currentKiLbl.Text = "...";
            currentKdLbl.Text = "...";
            currentPidIntervalLbl.Text = "...";
            currentSamplingIntervalLbl.Text = "...";
            currentMovAverWinLbl.Text = "...";
            currentMinSumOfErrorsLbl.Text = "...";
            currentMaxSumOfErrorsLbl.Text = "...";
            currentMinContrVarLbl.Text = "...";
            currentMaxContrVarLbl.Text = "...";
            currentPidBiasLbl.Text = "...";
            currentPidOffsetLbl.Text = "...";
            currentPidSetpointLbl.Text = "...";
        }

        private void SetItemsToConnectedModeWithoutCommunication()
        {
            openBtn.Enabled = false;
            comPortCb.Enabled = false;
            baudrateCb.Enabled = false;
            dataBitsCb.Enabled = false;
            parityCb.Enabled = false;
            stopBitsCb.Enabled = false;
            flowControlCb.Enabled = false;
            closeBtn.Enabled = true;
            startBtn.Enabled = false;
            stopBtn.Enabled = false;
            saveDataBtn.Enabled = false;
            loadDataBtn.Enabled = true;
            startPidBtn.Enabled = false;
            stopPidBtn.Enabled = false;
            setConfigDataBtn.Enabled = false;
            readConfigDataBtn.Enabled = false;
            setPidSetpointBtn.Enabled = false;
            getPidSetpointBtn.Enabled = false;
            kpTxtBox.Enabled = false;
            kiTxtBox.Enabled = false;
            kdTxtBox.Enabled = false;
            pidSetpointTxtBox.Enabled = false;
            pidIntervalTxtBox.Enabled = false;
            samplingIntervalTxtBox.Enabled = false;
            movAverageWinTxtBox.Enabled = false;
            pidOffsetTxtBox.Enabled = false;
            minSumOfErrorsTxtBox.Enabled = false;
            maxSumOfErrorsTxtBox.Enabled = false;
            minControlledVarTxtBox.Enabled = false;
            maxControlledVarTxtBox.Enabled = false;
            pidBiasTxtBox.Enabled = false;
            portStatusPb.Value = 50;
            timer1.Enabled = true;

            kpTxtBox.Clear();
            kiTxtBox.Clear();
            kdTxtBox.Clear();
            pidSetpointTxtBox.Clear();
            pidIntervalTxtBox.Clear();
            samplingIntervalTxtBox.Clear();
            movAverageWinTxtBox.Clear();
            pidOffsetTxtBox.Clear();
            minSumOfErrorsTxtBox.Clear();
            maxSumOfErrorsTxtBox.Clear();
            minControlledVarTxtBox.Clear();
            maxControlledVarTxtBox.Clear();
            pidBiasTxtBox.Clear();

            currentPidControllerStatusLabel.Text = "...";
            currentKpLbl.Text = "...";
            currentKiLbl.Text = "...";
            currentKdLbl.Text = "...";
            currentPidIntervalLbl.Text = "...";
            currentSamplingIntervalLbl.Text = "...";
            currentMovAverWinLbl.Text = "...";
            currentMinSumOfErrorsLbl.Text = "...";
            currentMaxSumOfErrorsLbl.Text = "...";
            currentMinContrVarLbl.Text = "...";
            currentMaxContrVarLbl.Text = "...";
            currentPidBiasLbl.Text = "...";
            currentPidOffsetLbl.Text = "...";
            currentPidSetpointLbl.Text = "...";
        }

        private void SetItemsToConnectedModeWithCommunication()
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
            setPidSetpointBtn.Enabled = true;
            getPidSetpointBtn.Enabled = true;
            kpTxtBox.Enabled = true;
            kiTxtBox.Enabled = true;
            kdTxtBox.Enabled = true;
            pidSetpointTxtBox.Enabled = true;
            pidIntervalTxtBox.Enabled = true;
            samplingIntervalTxtBox.Enabled = true;
            movAverageWinTxtBox.Enabled = true;
            pidOffsetTxtBox.Enabled = true;
            minSumOfErrorsTxtBox.Enabled = true;
            maxSumOfErrorsTxtBox.Enabled = true;
            minControlledVarTxtBox.Enabled = true;
            maxControlledVarTxtBox.Enabled = true;
            pidBiasTxtBox.Enabled = true;
            portStatusPb.Value = 100;
            timer1.Enabled = true;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    int receivedByte = serialPort.ReadByte();
                    // Console.WriteLine(receivedByte.ToString("X2"));
                    if (receivedByte >= 0)
                    {
                        pidTuningHelperApp.AppendNewByte(receivedByte);
                        pidTuningHelperApp.GetDataPacketRx().Decode();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            pidTuningHelperApp.IncrementSerialCommunicationCounter();

            if (pidTuningHelperApp.GetSerialCommunicationCounter() >= (int) Delay._3000ms)
            {
                this.SetItemsToConnectedModeWithoutCommunication();
            }
            else
            {
                this.SetItemsToConnectedModeWithCommunication();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pidTuningHelperApp.ExecuteStateMachine();
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
                    if (Math.Abs(xMinResult) <= 500000)
                    {
                        xMinInt = xMinResult;
                    }
                }

                if (int.TryParse(xMaxStr, out int xMaxResult))
                {
                    if (Math.Abs(xMaxResult) <= 500000)
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
                    if (Math.Abs(yMinResult) <= 500000)
                    {
                        yMinInt = yMinResult;
                    }
                }

                if (int.TryParse(yMaxStr, out int yMaxResult))
                {
                    if (Math.Abs(yMaxResult) <= 500000)
                    {
                        yMaxInt = yMaxResult;
                    }
                }

                pidTuningHelperApp.ResizeChartAxisY(yMinInt, yMaxInt);
            }
        }

        private void readConfigDataBtn_Click(object sender, EventArgs e)
        {
            this.GetPidParametersFromMicrocontroller();
        }

        private void GetPidParametersFromMicrocontroller()
        {
            currentKpLbl.Text = "...";
            currentKiLbl.Text = "...";
            currentKdLbl.Text = "...";
            currentPidIntervalLbl.Text = "...";
            currentSamplingIntervalLbl.Text = "...";
            currentMovAverWinLbl.Text = "...";
            currentMinSumOfErrorsLbl.Text = "...";
            currentMaxSumOfErrorsLbl.Text = "...";
            currentMinContrVarLbl.Text = "...";
            currentMaxContrVarLbl.Text = "...";
            currentPidBiasLbl.Text = "...";
            currentPidOffsetLbl.Text = "...";

            pidTuningHelperApp.AskForCurrentConfigDataValues();
        }

        private void setConfigDataBtn_Click(object sender, EventArgs e)
        {
            bool parseSuccess = true;

            uint samplingIntervalInMsResultParsed = 0;
            uint pidIntervalInMsResultParsed = 0;
            float kpResultParsed = 0;
            float kiResultParsed = 0;
            float kdResultParsed = 0;
            uint movingAverageWindowResultParsed = 0;
            int minSumOfErrorsResultParsed = 0;
            int maxSumOfErrorsResultParsed = 0;
            int minControlledVariableResultParsed = 0;
            int maxControlledVariableResultParsed = 0;
            float pidOffsetResultParsed = 0;
            float pidBiasResultParsed = 0;

            string samplingIntervalInMsStr = samplingIntervalTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(samplingIntervalInMsStr))
            {
                if (uint.TryParse(samplingIntervalInMsStr, out uint samplingIntervalInMsResult))
                {
                    samplingIntervalInMsResultParsed = 10 * samplingIntervalInMsResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string pidIntervalInMsStr = pidIntervalTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(pidIntervalInMsStr))
            {
                if (uint.TryParse(pidIntervalInMsStr, out uint pidIntervalInMsResult))
                {
                    pidIntervalInMsResultParsed = 10 * pidIntervalInMsResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string kpStr = kpTxtBox.Text.Trim();
            if (kpStr.Contains("."))
            {
                kpStr = kpStr.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(kpStr))
            {
                if (float.TryParse(kpStr, out float kpResult))
                {
                    kpResultParsed = kpResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string kiStr = kiTxtBox.Text.Trim();
            if (kiStr.Contains("."))
            {
                kiStr = kiStr.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(kiStr))
            {
                if (float.TryParse(kiStr, out float kiResult))
                {
                    kiResultParsed = kiResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string kdStr = kdTxtBox.Text.Trim();
            if (kdStr.Contains("."))
            {
                kdStr = kdStr.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(kdStr))
            {
                if (float.TryParse(kdStr, out float kdResult))
                {
                    kdResultParsed = kdResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string movingAverageWindow = movAverageWinTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(movingAverageWindow))
            {
                if (uint.TryParse(movingAverageWindow, out uint movingAverageWindowResult))
                {
                    movingAverageWindowResultParsed = movingAverageWindowResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string minSumOfErrors = minSumOfErrorsTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(minSumOfErrors))
            {
                if (int.TryParse(minSumOfErrors, out int minSumOfErrorsResult))
                {
                    minSumOfErrorsResultParsed = minSumOfErrorsResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string maxSumOfErrors = maxSumOfErrorsTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(maxSumOfErrors))
            {
                if (int.TryParse(maxSumOfErrors, out int maxSumOfErrorsResult))
                {
                    maxSumOfErrorsResultParsed = maxSumOfErrorsResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string minControlledVariable = minControlledVarTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(minControlledVariable))
            {
                if (int.TryParse(minControlledVariable, out int minControlledVariableResult))
                {
                    minControlledVariableResultParsed = minControlledVariableResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string maxControlledVariable = maxControlledVarTxtBox.Text.Trim();
            if (!String.IsNullOrEmpty(maxControlledVariable))
            {
                if (int.TryParse(maxControlledVariable, out int maxControlledVariableResult))
                {
                    maxControlledVariableResultParsed = maxControlledVariableResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string pidOffset = pidOffsetTxtBox.Text.Trim();
            if (pidOffset.Contains("."))
            {
                pidOffset = pidOffset.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(pidOffset))
            {
                if (float.TryParse(pidOffset, out float pidOffsetResult))
                {
                    pidOffsetResultParsed = pidOffsetResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            string pidBias = pidBiasTxtBox.Text.Trim();
            if (pidBias.Contains("."))
            {
                pidBias = pidBias.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(pidBias))
            {
                if (float.TryParse(pidBias, out float pidBiasResult))
                {
                    pidBiasResultParsed = pidBiasResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            if (parseSuccess == true)
            {
                currentKpLbl.Text = "...";
                currentKiLbl.Text = "...";
                currentKdLbl.Text = "...";
                currentPidIntervalLbl.Text = "...";
                currentSamplingIntervalLbl.Text = "...";
                currentMovAverWinLbl.Text = "...";
                currentMinSumOfErrorsLbl.Text = "...";
                currentMaxSumOfErrorsLbl.Text = "...";
                currentMinContrVarLbl.Text = "...";
                currentMaxContrVarLbl.Text = "...";
                currentPidBiasLbl.Text = "...";
                currentPidOffsetLbl.Text = "...";

                pidTuningHelperApp.SetConfigDataCommand(kpResultParsed, kiResultParsed, kdResultParsed, pidIntervalInMsResultParsed,
                    samplingIntervalInMsResultParsed, movingAverageWindowResultParsed, minSumOfErrorsResultParsed, maxSumOfErrorsResultParsed,
                    minControlledVariableResultParsed, maxControlledVariableResultParsed, pidOffsetResultParsed, pidBiasResultParsed);
            }
            else
            {
                String errorMessage = "Parse failed";
                MessageBox.Show(errorMessage, "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void startPidBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.StartPidControllerSendCommand();
        }

        private void stopPidBtn_Click(object sender, EventArgs e)
        {
            pidTuningHelperApp.StopPidControllerSendCommand();
        }

        private void setPidSetpointBtn_Click(object sender, EventArgs e)
        {
            bool parseSuccess = true;
            float pidSetpointResultParsed = 0;

            string pidSetpointStr = pidSetpointTxtBox.Text.Trim();
            if (pidSetpointStr.Contains("."))
            {
                pidSetpointStr = pidSetpointStr.Replace(".", ",");
            }
            if (!String.IsNullOrEmpty(pidSetpointStr))
            {
                if (float.TryParse(pidSetpointStr, out float pidSetpointResult))
                {
                    pidSetpointResultParsed = pidSetpointResult;
                }
                else
                {
                    parseSuccess = false;
                }
            }
            else
            {
                parseSuccess = false;
            }

            if (parseSuccess == true)
            {
                pidTuningHelperApp.SetPidSetpointSendCommand(pidSetpointResultParsed);
            }
            else
            {
                String errorMessage = "Parse failed";
                MessageBox.Show(errorMessage, "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getPidSetpointBtn_Click(object sender, EventArgs e)
        {
            currentPidSetpointLbl.Text = "...";
            pidTuningHelperApp.AskForCurrentPidSetpointValue();
        }
    }
}
