using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using PidTuningHelper.DataPacket;
using System.IO.Ports;
using PidTuningHelper.Misc;
using System.Windows.Forms;

namespace PidTuningHelper.App
{
    public class App
    {
        private byte receivedCommand;
        private bool decodeReceivedCommand;

        private int processVariableValue;
        private int pointsCounter;

        private int serialCommunicationCounter;
        private bool currentPidControllerStatus;

        private SerialPort serialPort;
        private Chart lineChart;
        private string lineChartSerie = "processVariableSerie";

        private Label currentKpLabel;
        private Label currentKiLabel;
        private Label currentKdLabel;
        private Label currentPidIntervalLabel;
        private Label currentPidSetpointLabel;
        private Label currentSamplingIntervalLabel;
        private Label currentMovAverWinLabel;
        private Label currentMinSumOfErrorsLabel;
        private Label currentMaxSumOfErrorsLabel;
        private Label currentMinControlledVariableLabel;
        private Label currentMaxControlledVariableLabel;
        private Label currentOffsetLabel;
        private Label currentBiasLabel;
        private Label currentPidControllerStatusLabel;

        private TextBox kpTxtBox;
        private TextBox kiTxtBox;
        private TextBox kdTxtBox;
        private TextBox pidIntervalTxtBox;
        private TextBox pidSetpointTxtBox;
        private TextBox samplingIntervalTxtBox;
        private TextBox movAverWinTxtBox;
        private TextBox minSumOfErrorsTxtBox;
        private TextBox maxSumOfErrorsTxtBox;
        private TextBox minControlledVariableTxtBox;
        private TextBox maxControlledVariableTxtBox;
        private TextBox offsetTxtBox;
        private TextBox biasTxtBox;

        private int stateMachine;

        private DataPacketTx dataPacketTx;
        private DataPacketRx dataPacketRx;
        private byte[] payloadTxDataBytes;
        private byte[] payloadRxDataBytes;

        private int lineChartMinX;
        private int lineChartMaxX;
        private int lineChartMinY;
        private int lineChartMaxY;

        public App(Chart lineChart, SerialPort serialPort)
        {
            this.receivedCommand = 0;
            this.decodeReceivedCommand = false;

            this.stateMachine = 0;

            this.processVariableValue = 0;
            this.currentPidControllerStatus = false;

            this.dataPacketTx = new DataPacketTx(0xAA, 0x55);
            this.dataPacketRx = new DataPacketRx(0xAA, 0x55);
            this.payloadTxDataBytes = new byte[this.dataPacketTx.GetQtyPayloadTxDataBytes()];
            this.payloadRxDataBytes = new byte[this.dataPacketRx.GetQtyPayloadRxDataBytes()];

            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            Array.Clear(this.payloadRxDataBytes, 0, this.dataPacketRx.GetQtyPayloadRxDataBytes());

            this.SetLineChart(lineChart);
            this.SetSerialPort(serialPort);

            this.lineChartMinX = (int) this.lineChart.ChartAreas[0].AxisX.Minimum;
            this.lineChartMaxX = (int) this.lineChart.ChartAreas[0].AxisX.Maximum;
            this.lineChartMinY = (int) this.lineChart.ChartAreas[0].AxisY.Minimum;
            this.lineChartMaxY = (int) this.lineChart.ChartAreas[0].AxisY.Maximum;

            this.pointsCounter = this.lineChartMinX;
        }

        public void AppendNewByte(int newByte)
        {
            this.dataPacketRx.Append((byte)newByte);
        }

        public void DecodeAndExecuteCommand()
        {
            switch (this.receivedCommand)
            {
                case ((byte) CommandsFromMicrocontroller.CurrentConfigDataValues):
                    int pidKpTimes1000 = ((this.payloadRxDataBytes[0] << 24) + (this.payloadRxDataBytes[1] << 16) + (this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    float pidKp = ((float) pidKpTimes1000) / 1000;
                    this.currentKpLabel.Text = pidKp.ToString();
                    this.kpTxtBox.Text = pidKp.ToString();

                    int pidKiTimes1000 = ((this.payloadRxDataBytes[4] << 24) + (this.payloadRxDataBytes[5] << 16) + (this.payloadRxDataBytes[6] << 8) + this.payloadRxDataBytes[7]);
                    float pidKi = ((float) pidKiTimes1000) / 1000;
                    this.currentKiLabel.Text = pidKi.ToString();
                    this.kiTxtBox.Text = pidKi.ToString();

                    int pidKdTimes1000 = ((this.payloadRxDataBytes[8] << 24) + (this.payloadRxDataBytes[9] << 16) + (this.payloadRxDataBytes[10] << 8) + this.payloadRxDataBytes[11]);
                    float pidKd = ((float) pidKdTimes1000) / 1000;
                    this.currentKdLabel.Text = pidKd.ToString();
                    this.kdTxtBox.Text = pidKd.ToString();

                    int pidIntervalAux = ((this.payloadRxDataBytes[12] << 8) + this.payloadRxDataBytes[13]);
                    int pidIntervalInMiliSeconds = pidIntervalAux / 10;
                    this.currentPidIntervalLabel.Text = pidIntervalInMiliSeconds.ToString();
                    this.pidIntervalTxtBox.Text = pidIntervalInMiliSeconds.ToString();

                    int samplingIntervalAux = ((this.payloadRxDataBytes[14] << 8) + this.payloadRxDataBytes[15]);
                    int samplingIntervalInMiliSeconds = samplingIntervalAux / 10;
                    this.currentSamplingIntervalLabel.Text = samplingIntervalInMiliSeconds.ToString();
                    this.samplingIntervalTxtBox.Text = samplingIntervalInMiliSeconds.ToString();

                    int movingAverageWindow = ((this.payloadRxDataBytes[16] << 8) + this.payloadRxDataBytes[17]);
                    this.currentMovAverWinLabel.Text = movingAverageWindow.ToString();
                    this.movAverWinTxtBox.Text = movingAverageWindow.ToString();

                    int minSumOfErrorsUnsigned = ((this.payloadRxDataBytes[18] << 24) + (this.payloadRxDataBytes[19] << 16) + (this.payloadRxDataBytes[20] << 8) + this.payloadRxDataBytes[21]);
                    int minSumOfErrors = minSumOfErrorsUnsigned - 1000000000;
                    this.currentMinSumOfErrorsLabel.Text = minSumOfErrors.ToString();
                    this.minSumOfErrorsTxtBox.Text = minSumOfErrors.ToString();

                    int maxSumOfErrorsUnsigned = ((this.payloadRxDataBytes[22] << 24) + (this.payloadRxDataBytes[23] << 16) + (this.payloadRxDataBytes[24] << 8) + this.payloadRxDataBytes[25]);
                    int maxSumOfErrors = maxSumOfErrorsUnsigned - 1000000000;
                    this.currentMaxSumOfErrorsLabel.Text = maxSumOfErrors.ToString();
                    this.maxSumOfErrorsTxtBox.Text = maxSumOfErrors.ToString();

                    int minControlledVariableUnsigned = ((this.payloadRxDataBytes[26] << 24) + (this.payloadRxDataBytes[27] << 16) + (this.payloadRxDataBytes[28] << 8) + this.payloadRxDataBytes[29]);
                    int minControlledVariable = minControlledVariableUnsigned - 1000000000;
                    this.currentMinControlledVariableLabel.Text = minControlledVariable.ToString();
                    this.minControlledVariableTxtBox.Text = minControlledVariable.ToString();

                    int maxControlledVariableUnsigned = ((this.payloadRxDataBytes[30] << 24) + (this.payloadRxDataBytes[31] << 16) + (this.payloadRxDataBytes[32] << 8) + this.payloadRxDataBytes[33]);
                    int maxControlledVariable = maxControlledVariableUnsigned - 1000000000;
                    this.currentMaxControlledVariableLabel.Text = maxControlledVariable.ToString();
                    this.maxControlledVariableTxtBox.Text = maxControlledVariable.ToString();

                    int offsetUnsigned = ((this.payloadRxDataBytes[34] << 24) + (this.payloadRxDataBytes[35] << 16) + (this.payloadRxDataBytes[36] << 8) + this.payloadRxDataBytes[37]);
                    float offset = ((((float) offsetUnsigned) - 1000000) / 1000);
                    this.currentOffsetLabel.Text = offset.ToString();
                    this.offsetTxtBox.Text = offset.ToString();

                    int biasUnsigned = ((this.payloadRxDataBytes[38] << 24) + (this.payloadRxDataBytes[39] << 16) + (this.payloadRxDataBytes[40] << 8) + this.payloadRxDataBytes[41]);
                    float bias = ((((float) biasUnsigned) - 1000000) / 1000);
                    this.currentBiasLabel.Text = bias.ToString();
                    this.biasTxtBox.Text = bias.ToString();
                    break;

                case ((byte) CommandsFromMicrocontroller.CurrentPidSetpointValue):
                    int pidSetpointTimes1000 = ((this.payloadRxDataBytes[0] << 24) + (this.payloadRxDataBytes[1] << 16) + (this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    float pidSetpoint = ((float) pidSetpointTimes1000) / 1000;
                    this.currentPidSetpointLabel.Text = pidSetpoint.ToString();
                    this.pidSetpointTxtBox.Text = pidSetpoint.ToString();
                    break;

                case ((byte) CommandsFromMicrocontroller.CurrentProcessVariableValue):
                    this.processVariableValue = ((this.payloadRxDataBytes[0] << 24) + (this.payloadRxDataBytes[1] << 16) + (this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    this.lineChart.Series[this.lineChartSerie].Points.AddXY(this.pointsCounter, this.processVariableValue);

                    this.pointsCounter++;
                    if (this.pointsCounter > this.lineChartMaxX)
                    {
                        this.pointsCounter = this.lineChartMinX;
                        this.ClearChart();
                    }
                    break;

                case ((byte) CommandsFromMicrocontroller.KeepAliveMessageWithPidControllerStatus):
                    this.ClearSerialCommunicationCounter();

                    if (this.payloadRxDataBytes[0] == 0x01)
                    {
                        this.currentPidControllerStatus = true;
                    }
                    else
                    {
                        this.currentPidControllerStatus = false;
                    }

                    if (this.currentPidControllerStatus == true)
                    {
                        this.currentPidControllerStatusLabel.Text = "RUNNING";
                    }
                    else
                    {
                        this.currentPidControllerStatusLabel.Text = "HALT";
                    }
                    break;

                default:
                    break;
            }
        }

        public void ClearDataPacketRx()
        {
            this.dataPacketRx.Clear();
        }

        public void SaveData()
        {
            int qty_of_data_points = this.lineChart.Series[this.lineChartSerie].Points.Count();

            if (qty_of_data_points > 1)
            {
                int[] x_values = new int[qty_of_data_points - 1];
                int[] y_values = new int[qty_of_data_points - 1];

                for (int i = 1; i < qty_of_data_points; i++)
                {
                    x_values[i - 1] = (int)this.lineChart.Series[this.lineChartSerie].Points[i].XValue;
                    y_values[i - 1] = (int)this.lineChart.Series[this.lineChartSerie].Points[i].YValues[0];
                }

                Csv csv = new Csv();
                csv.SetColumnData(x_values, "x_values");
                csv.SetColumnData(y_values, "y_values");
                csv.PrepareData();
                csv.SaveDataToFile();
                csv.Clear();
            }
        }

        public void LoadData()
        {
            Csv csv = new Csv();
            List<int[]> data = new List<int[]>();
            int qtyOfRows = 0;
            int qtyOfColumns = 0;

            data = csv.GetDataColumns();

            if (csv.GetDataReady() == true)
            {
                qtyOfColumns = csv.GetQtyOfColumns();
                qtyOfRows = csv.GetQtyOfRows();

                int[] x_values = new int[qtyOfRows];
                int[] y_values = new int[qtyOfColumns];

                x_values = data[0];
                y_values = data[1];

                this.ClearChart();

                for (int i = 0; i < x_values.Length; i++)
                {
                    this.lineChart.Series[this.lineChartSerie].Points.AddXY(x_values[i], y_values[i]);
                }
            }
            else
            {
                MessageBox.Show("Load error");
            }

        }

        public void SetConfigDataCommand(float kp, float ki, float kd, uint pidIntervalInMsx10, uint samplingIntervalInMsx10, uint movingAverageWindow,
            int minSumOfErrors, int maxSumOfErrors, int minControlledVariable, int maxControlledVariable, float pidOffset, float pidBias)
        {
            bool error = false;

            int pidKpTimes1000 = 0;
            int pidKiTimes1000 = 0;
            int pidKdTimes1000 = 0;
            int offset = 0;
            int bias = 0;

            if (kp < 0 || kp > 4095)
            {
                error = true;
            }
            else
            {
                float pidKpTimes1000Aux = 1000 * kp;
                pidKpTimes1000 = (int)pidKpTimes1000Aux;
            }

            if (ki < 0 || ki > 4095)
            {
                error = true;
            }
            else
            {
                float pidKiTimes1000Aux = 1000 * ki;
                pidKiTimes1000 = (int)pidKiTimes1000Aux;
            }

            if (kd < 0 || ki > 4095)
            {
                error = true;
            }
            else
            {
                float pidKdTimes1000Aux = 1000 * kd;
                pidKdTimes1000 = (int)pidKdTimes1000Aux;
            }

            if (pidIntervalInMsx10 < 0 || pidIntervalInMsx10 > 65535)
            {
                error = true;
            }

            if (samplingIntervalInMsx10 < 0 || samplingIntervalInMsx10 > 65535)
            {
                error = true;
            }

            if (movingAverageWindow < 0 || movingAverageWindow > 65535)
            {
                error = true;
            }

            if (minSumOfErrors < -1000000000 || minSumOfErrors > 1000000000)
            {
                error = true;
            }
            else
            {
                minSumOfErrors += 1000000000;
            }

            if (maxSumOfErrors < -1000000000 || maxSumOfErrors > 1000000000)
            {
                error = true;
            }
            else
            {
                maxSumOfErrors += 1000000000;
            }

            if (minControlledVariable < -1000000000 || minControlledVariable > 1000000000)
            {
                error = true;
            }
            else
            {
                minControlledVariable += 1000000000;
            }

            if (maxControlledVariable < -1000000000 || maxControlledVariable > 1000000000)
            {
                error = true;
            }
            else
            {
                maxControlledVariable += 1000000000;
            }

            if (pidOffset < -1000000 || pidOffset > 1000000)
            {
                error = true;
            }
            else
            {
                float offsetAux = (1000 * pidOffset) + 1000000;
                offset = (int)offsetAux;
            }

            if (pidBias < -1000000 || pidBias > 1000000)
            {
                error = true;
            }
            else
            {
                float biasAux = (1000 * pidBias) + 1000000;
                bias = (int)biasAux;
            }

            if (error == false)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());

                this.payloadTxDataBytes[0] = (byte) ((pidKpTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidKpTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidKpTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidKpTimes1000 & 0x00FF);

                this.payloadTxDataBytes[4] = (byte) ((pidKiTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[5] = (byte) ((pidKiTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[6] = (byte) ((pidKiTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[7] = (byte) (pidKiTimes1000 & 0x00FF);

                this.payloadTxDataBytes[8] = (byte) ((pidKdTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[9] = (byte) ((pidKdTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[10] = (byte) ((pidKdTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[11] = (byte) (pidKdTimes1000 & 0x00FF);

                this.payloadTxDataBytes[12] = (byte) ((pidIntervalInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[13] = (byte) (pidIntervalInMsx10 & 0x00FF);

                this.payloadTxDataBytes[14] = (byte) ((samplingIntervalInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[15] = (byte) (samplingIntervalInMsx10 & 0x00FF);

                this.payloadTxDataBytes[16] = (byte) ((movingAverageWindow >> 8) & 0x00FF);
                this.payloadTxDataBytes[17] = (byte) (movingAverageWindow & 0x00FF);

                this.payloadTxDataBytes[18] = (byte) ((minSumOfErrors >> 24) & 0x000000FF);
                this.payloadTxDataBytes[19] = (byte) ((minSumOfErrors >> 16) & 0x000000FF);
                this.payloadTxDataBytes[20] = (byte) ((minSumOfErrors >> 8) & 0x000000FF);
                this.payloadTxDataBytes[21] = (byte) (minSumOfErrors & 0x000000FF);

                this.payloadTxDataBytes[22] = (byte) ((maxSumOfErrors >> 24) & 0x000000FF);
                this.payloadTxDataBytes[23] = (byte) ((maxSumOfErrors >> 16) & 0x000000FF);
                this.payloadTxDataBytes[24] = (byte) ((maxSumOfErrors >> 8) & 0x000000FF);
                this.payloadTxDataBytes[25] = (byte) (maxSumOfErrors & 0x000000FF);

                this.payloadTxDataBytes[26] = (byte) ((minControlledVariable >> 24) & 0x000000FF);
                this.payloadTxDataBytes[27] = (byte) ((minControlledVariable >> 16) & 0x000000FF);
                this.payloadTxDataBytes[28] = (byte) ((minControlledVariable >> 8) & 0x000000FF);
                this.payloadTxDataBytes[29] = (byte) (minControlledVariable & 0x000000FF);

                this.payloadTxDataBytes[30] = (byte) ((maxControlledVariable >> 24) & 0x000000FF);
                this.payloadTxDataBytes[31] = (byte) ((maxControlledVariable >> 16) & 0x000000FF);
                this.payloadTxDataBytes[32] = (byte) ((maxControlledVariable >> 8) & 0x000000FF);
                this.payloadTxDataBytes[33] = (byte) (maxControlledVariable & 0x000000FF);

                this.payloadTxDataBytes[34] = (byte) ((offset >> 24) & 0x00FF);
                this.payloadTxDataBytes[35] = (byte) ((offset >> 16) & 0x00FF);
                this.payloadTxDataBytes[36] = (byte) ((offset >> 8) & 0x00FF);
                this.payloadTxDataBytes[37] = (byte) (offset & 0x00FF);

                this.payloadTxDataBytes[38] = (byte) ((bias >> 24) & 0x00FF);
                this.payloadTxDataBytes[39] = (byte) ((bias >> 16) & 0x00FF);
                this.payloadTxDataBytes[40] = (byte) ((bias >> 8) & 0x00FF);
                this.payloadTxDataBytes[41] = (byte) (bias & 0x00FF);

                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetConfigDataValues);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 42);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                String errorMessage = 
                    "0 <= kp <= 4095\n" +
                    "0 <= ki <= 4095\n" +
                    "0 <= kd <= 4095\n" +
                    "0 <= pidIntervalInMs <= 65535\n" +
                    "0 <= samplingIntervalInMs <= 65535\n" +
                    "0 <= movingAverageWindow <= 65535\n" +
                    "-1000000000 <= minSumOfErrors <= 1000000000\n" +
                    "-1000000000 <= maxSumOfErrors <= 1000000000\n" +
                    "-1000000000 <= minControlledVariable <= 1000000000\n" +
                    "-1000000000 <= maxControlledVariable <= 1000000000\n" +
                    "-1000000 <= pidOffset <= 1000000\n" +
                    "-1000000 <= pidBias <= 1000000";

                MessageBox.Show(errorMessage, "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AskForCurrentConfigDataValues()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForCurrentConfigDataValues);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 0);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void AskForCurrentPidSetpointValue()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());

            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForSendPidSetpointValue);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 0);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void SetPidSetpointSendCommand(float pidSetpoint)
        {
            if (pidSetpoint >= 0 && pidSetpoint <= 16777215)
            {
                float pidSetpointTimes1000Aux = 1000 * pidSetpoint;
                int pidSetpointTimes1000 = (int) pidSetpointTimes1000Aux;

                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 4;
                this.payloadTxDataBytes[0] = (byte) ((pidSetpointTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidSetpointTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidSetpointTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidSetpointTimes1000 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetPidSetpointValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= pidSetpoint <= 16777215", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void StopPidControllerSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) RunPidController.False);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForRunPidController);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void StartPidControllerSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) RunPidController.True);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForRunPidController);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void StopDataAquisitionSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Disable);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForSendProcessVariable);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void StartDataAquisitionSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Enable);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForSendProcessVariable);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void ClearChart()
        {
            this.lineChart.Series[this.lineChartSerie].Points.Clear();
            this.lineChart.Series[this.lineChartSerie].Points.AddXY(this.lineChartMinX - 1, this.lineChartMinY);
            this.ResetPointsCounter();
        }

        public void ResizeChartAxisX(int xMin, int xMax)
        {
            if (xMax > xMin)
            {
                double interval = (xMax - xMin) / 20;
                if (interval >= 1)
                {
                    this.lineChart.ChartAreas[0].AxisX.Minimum = xMin;
                    this.lineChart.ChartAreas[0].AxisX.Maximum = xMax;

                    this.lineChart.ChartAreas[0].AxisX.MinorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MinorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MajorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.Interval = interval;

                    this.lineChartMinX = (int)lineChart.ChartAreas[0].AxisX.Minimum;
                    this.lineChartMaxX = (int)lineChart.ChartAreas[0].AxisX.Maximum;

                    this.ClearChart();
                }
                else
                {
                    MessageBox.Show("Max-X - Min-X >= 20", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Max-X shall be greater than Min-X", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ResizeChartAxisY(int yMin, int yMax)
        {
            if (yMax > yMin)
            {
                double interval = (yMax - yMin) / 10;
                if (interval >= 1)
                {
                    this.lineChart.ChartAreas[0].AxisY.Minimum = yMin;
                    this.lineChart.ChartAreas[0].AxisY.Maximum = yMax;

                    this.lineChart.ChartAreas[0].AxisY.MinorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MinorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MajorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MajorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.Interval = interval;

                    this.lineChartMinY = (int)lineChart.ChartAreas[0].AxisY.Minimum;
                    this.lineChartMaxY = (int)lineChart.ChartAreas[0].AxisY.Maximum;

                    this.ClearChart();
                }
                else
                {
                    MessageBox.Show("Max-Y - Min-Y >= 10", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Max-Y shall be greater than Min-Y", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ExecuteStateMachine()
        {
            switch (this.stateMachine)
            {
                case 0:
                    if (this.dataPacketRx.isValid())
                    {
                        this.receivedCommand = this.dataPacketRx.GetCommand();
                        byte receivedPayloadDataLength = this.dataPacketRx.GetPayloadDataLength();

                        if (receivedPayloadDataLength > 0)
                        {
                            this.SetPayloadRxDataBytes(this.dataPacketRx.GetPayloadData(), receivedPayloadDataLength);
                        }

                        this.decodeReceivedCommand = true;
                        this.dataPacketRx.Clear();
                    }
                    this.stateMachine = 1;
                    break;

                case 1:
                    if (this.decodeReceivedCommand == true)
                    {
                        this.DecodeAndExecuteCommand();
                        this.decodeReceivedCommand = false;
                    }
                    this.stateMachine = 0;
                    break;

                default:
                    this.stateMachine = 0;
                    break;
            }
        }

        public bool LineChartContainsPoints()
        {
            if (this.lineChart.Series[this.lineChartSerie].Points.Count() > 1)
            {
                return true;
            }

            return false;
        }

        public void SetPayloadRxDataBytes(byte[] data, byte dataLength)
        {
            if (dataLength <= this.dataPacketRx.GetQtyPayloadRxDataBytes())
            {
                Array.Copy(data, this.payloadRxDataBytes, dataLength);
            }
        }

        public void ResetPointsCounter()
        {
            this.pointsCounter = this.lineChartMinX;
        }

        public void IncrementSerialCommunicationCounter()
        {
            this.serialCommunicationCounter++;
        }

        public void ClearSerialCommunicationCounter()
        {
            this.serialCommunicationCounter = 0;
        }

        public int GetSerialCommunicationCounter()
        {
            return this.serialCommunicationCounter;
        }

        public void SetLineChart(Chart lineChart)
        {
            this.lineChart = lineChart;
        }

        public void SetSerialPort(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }

        public void SetCurrentKpLabel(Label currentKpLabel)
        {
            this.currentKpLabel = currentKpLabel;
        }

        public void SetCurrentKiLabel(Label currentKiLabel)
        {
            this.currentKiLabel = currentKiLabel;
        }

        public void SetCurrentKdLabel(Label currentKdLabel)
        {
            this.currentKdLabel = currentKdLabel;
        }

        public DataPacketRx GetDataPacketRx()
        {
            return this.dataPacketRx;
        }

        public void SetCurrentPidIntervalLabel(Label currentPidIntervalLabel)
        {
            this.currentPidIntervalLabel = currentPidIntervalLabel;
        }

        public void SetCurrentSamplingIntervalLabel(Label currentSamplingIntervalLabel)
        {
            this.currentSamplingIntervalLabel = currentSamplingIntervalLabel;
        }

        public void SetCurrentPidSetpointLabel(Label currentPidSetpointLabel)
        {
            this.currentPidSetpointLabel = currentPidSetpointLabel;
        }
        
        public void SetCurrentMovAverWinLabel(Label currentMovAverWinLabel)
        {
            this.currentMovAverWinLabel = currentMovAverWinLabel;
        }

        public void SetCurrentMinSumOfErrorsLabel(Label currentMinSumOfErrorsLabel)
        {
            this.currentMinSumOfErrorsLabel = currentMinSumOfErrorsLabel;
        }

        public void SetCurrentMaxSumOfErrorsLabel(Label currentMaxSumOfErrorsLabel)
        {
            this.currentMaxSumOfErrorsLabel = currentMaxSumOfErrorsLabel;
        }

        public void SetCurrentMinControlledVariableLabel(Label currentMinControlledVariableLabel)
        {
            this.currentMinControlledVariableLabel = currentMinControlledVariableLabel;
        }

        public void SetCurrentMaxControlledVariableLabel(Label currentMaxControlledVariableLabel)
        {
            this.currentMaxControlledVariableLabel = currentMaxControlledVariableLabel;
        }

        public void SetCurrentOffsetLabel(Label currentOffsetLabel)
        {
            this.currentOffsetLabel = currentOffsetLabel;
        }

        public void SetCurrentBiasLabel(Label currentBiasLabel)
        {
            this.currentBiasLabel = currentBiasLabel;
        }

        public void SetCurrentPidControllerStatusLabel(Label currentPidControllerStatusLabel)
        {
            this.currentPidControllerStatusLabel = currentPidControllerStatusLabel;
        }

        public void SetKpTxtBox(TextBox kpTxtBox)
        {
            this.kpTxtBox = kpTxtBox;
        }

        public void SetKiTxtBox(TextBox kiTxtBox)
        {
            this.kiTxtBox = kiTxtBox;
        }

        public void SetKdTxtBox(TextBox kdTxtBox)
        {
            this.kdTxtBox = kdTxtBox;
        }

        public void SetPidIntervalTxtBox(TextBox pidIntervalTxtBox)
        {
            this.pidIntervalTxtBox = pidIntervalTxtBox;
        }

        public void SetPidSetpointTxtBox(TextBox pidSetpointTxtBox)
        {
            this.pidSetpointTxtBox = pidSetpointTxtBox;
        }

        public void SetSamplingIntervalTxtBox(TextBox samplingIntervalTxtBox)
        {
            this.samplingIntervalTxtBox = samplingIntervalTxtBox;
        }

        public void SetMovAverWinTxtBox(TextBox movAverWinTxtBox)
        {
            this.movAverWinTxtBox = movAverWinTxtBox;
        }

        public void SetMinSumOfErrorsTxtBox(TextBox minSumOfErrorsTxtBox)
        {
            this.minSumOfErrorsTxtBox = minSumOfErrorsTxtBox;
        }

        public void SetMaxSumOfErrorsTxtBox(TextBox maxSumOfErrorsTxtBox)
        {
            this.maxSumOfErrorsTxtBox = maxSumOfErrorsTxtBox;
        }

        public void SetMinControlledVariableTxtBox(TextBox minControlledVariableTxtBox)
        {
            this.minControlledVariableTxtBox = minControlledVariableTxtBox;
        }

        public void SetMaxControlledVariableTxtBox(TextBox maxControlledVariableTxtBox)
        {
            this.maxControlledVariableTxtBox = maxControlledVariableTxtBox;
        }

        public void SetOffsetTxtBox(TextBox offsetTxtBox)
        {
            this.offsetTxtBox = offsetTxtBox;
        }

        public void SetBiasTxtBox(TextBox biasTxtBox)
        {
            this.biasTxtBox = biasTxtBox;
        }
    }
}
