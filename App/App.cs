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

        private int stateMachine;
        private int counterTimer1;

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

            this.counterTimer1 = 0;
            this.stateMachine = 0;

            this.processVariableValue = 0;

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
                case ((byte) CommandsFromMicrocontroller.ProcessVariableValue):
                    this.processVariableValue = ((this.payloadRxDataBytes[0] << 24) + (this.payloadRxDataBytes[1] << 16) + (this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    this.lineChart.Series[this.lineChartSerie].Points.AddXY(this.pointsCounter, this.processVariableValue);

                    this.pointsCounter++;
                    if (this.pointsCounter > this.lineChartMaxX)
                    {
                        this.pointsCounter = this.lineChartMinX;
                        this.ClearChart();
                    }
                    break;

                case ((byte) CommandsFromMicrocontroller.PidKsParameterValues):
                    int pidKpTimes1000 = ((this.payloadRxDataBytes[0] << 24) + (this.payloadRxDataBytes[1] << 16) + (this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    int pidKiTimes1000 = ((this.payloadRxDataBytes[4] << 24) + (this.payloadRxDataBytes[5] << 16) + (this.payloadRxDataBytes[6] << 8) + this.payloadRxDataBytes[7]);
                    int pidKdTimes1000 = ((this.payloadRxDataBytes[8] << 24) + (this.payloadRxDataBytes[9] << 16) + (this.payloadRxDataBytes[10] << 8) + this.payloadRxDataBytes[11]);

                    float pidKp = ((float) pidKpTimes1000) / 1000;
                    float pidKi = ((float) pidKiTimes1000) / 1000;
                    float pidKd = ((float) pidKdTimes1000) / 1000;

                    this.currentKpLabel.Text = pidKp.ToString();
                    this.currentKiLabel.Text = pidKi.ToString();
                    this.currentKdLabel.Text = pidKd.ToString();
                    break;

                case ((byte) CommandsFromMicrocontroller.PidControllerParameterValues):
                    int samplingIntervalAux = ((this.payloadRxDataBytes[0] << 8) + this.payloadRxDataBytes[1]);
                    int pidIntervalAux = ((this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);

                    int pidSetpointTimes1000 = ((this.payloadRxDataBytes[4] << 24) + (this.payloadRxDataBytes[5] << 16) + (this.payloadRxDataBytes[6] << 8) + this.payloadRxDataBytes[7]);
                    float pidSetpoint = ((float) pidSetpointTimes1000) / 1000;

                    int movingAverageWindow = ((this.payloadRxDataBytes[8] << 8) + this.payloadRxDataBytes[9]);

                    int samplingIntervalInMiliSeconds = samplingIntervalAux / 10;
                    int pidIntervalInMiliSeconds = pidIntervalAux / 10;

                    this.currentSamplingIntervalLabel.Text = samplingIntervalInMiliSeconds.ToString();
                    this.currentPidIntervalLabel.Text = pidIntervalInMiliSeconds.ToString();
                    this.currentPidSetpointLabel.Text = pidSetpoint.ToString();
                    this.currentMovAverWinLabel.Text = movingAverageWindow.ToString();
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

        public void StartDataAquisitionSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Enable);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForSendProcessVariable);
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

        public void StartPidControllerSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) RunPidController.True);
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForRunPidController);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
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

        public void SetKpSendCommand(float kp)
        {
            if (kp >= 0 && kp <= 0xFFF)
            {
                float pidKpTimes1000Aux = 1000 * kp;
                int pidKpTimes1000 = (int) pidKpTimes1000Aux;

                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = (byte) ((pidKpTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidKpTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidKpTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidKpTimes1000 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKpValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 4);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= kp <= 4095", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetKiSendCommand(float ki)
        {
            if (ki >= 0 && ki <= 0xFFF)
            {
                float pidKiTimes1000Aux = 1000 * ki;
                int pidKiTimes1000 = (int) pidKiTimes1000Aux;

                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = (byte) ((pidKiTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidKiTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidKiTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidKiTimes1000 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKiValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 4);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= ki <= 4095", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetKdSendCommand(float kd)
        {
            if (kd >= 0 && kd <= 0xFFF)
            {
                float pidKdTimes1000Aux = 1000 * kd;
                int pidKdTimes1000 = (int) pidKdTimes1000Aux;

                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = (byte) ((pidKdTimes1000 >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidKdTimes1000 >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidKdTimes1000 >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidKdTimes1000 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKdValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 4);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= kd <= 4095", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetSamplingIntervalSendCommand(uint samplingIntervalInMsx10)
        {
            if (samplingIntervalInMsx10 >= 0 && samplingIntervalInMsx10 <= 0xFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 2;
                this.payloadTxDataBytes[0] = (byte) ((samplingIntervalInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) (samplingIntervalInMsx10 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetSamplingIntervalValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= samplingIntervalInMs <= 6553", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetMovingAverageWindowSendCommand(uint movingAverageWindow)
        {
            if (movingAverageWindow >= 0 && movingAverageWindow <= 0xFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 2;
                this.payloadTxDataBytes[0] = (byte) ((movingAverageWindow >> 8) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) (movingAverageWindow & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetMovingAverageWindow);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= movingAverageWindow <= 65535", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetPidIntervalSendCommand(uint pidIntervalInMsx10)
        {
            if (pidIntervalInMsx10 >= 0 && pidIntervalInMsx10 <= 0xFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 2;
                this.payloadTxDataBytes[0] = (byte) ((pidIntervalInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) (pidIntervalInMsx10 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetPidIntervalValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= pidIntervalInMs <= 6553", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetPidSetpointSendCommand(float pidSetpoint)
        {
            if (pidSetpoint >= 0 && pidSetpoint <= 0x00FFFFFF)
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

        public void AskForPidKsParameters()
        {
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForPidKsParameters);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void AskForPidControllerParameters()
        {
            this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.AskForPidControllerParameters);
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

        public void IncrementCounterTimer1()
        {
            this.counterTimer1++;
        }

        public void ExecuteStateMachine()
        {
            switch (this.stateMachine)
            {
                case 0:
                    //if (this.counterTimer1 >= (int) Delay._10ms)
                    //{
                    //    this.dataPacketRx.Decode();
                    //    this.counterTimer1 = 0;
                    //}
                    this.stateMachine = 1;
                    break;

                case 1:
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
                    this.stateMachine = 2;
                    break;

                case 2:
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
    }
}
