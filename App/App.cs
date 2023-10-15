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
        private Label currentPidDelayLabel;
        private Label currentPidSetpointLabel;
        private Label currentSamplingDelayLabel;
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
                    this.currentKpLabel.Text = this.payloadRxDataBytes[0].ToString();
                    this.currentKiLabel.Text = this.payloadRxDataBytes[1].ToString();
                    this.currentKdLabel.Text = this.payloadRxDataBytes[2].ToString();
                    break;

                case ((byte) CommandsFromMicrocontroller.PidControllerParameterValues):
                    int samplingDelayAux = ((this.payloadRxDataBytes[0] << 8) + this.payloadRxDataBytes[1]);
                    int pidDelayAux = ((this.payloadRxDataBytes[2] << 8) + this.payloadRxDataBytes[3]);
                    int pidSetpoint = ((this.payloadRxDataBytes[4] << 24) + (this.payloadRxDataBytes[5] << 16) + (this.payloadRxDataBytes[6] << 8) + this.payloadRxDataBytes[7]);
                    int movingAverageWindow = this.payloadRxDataBytes[8];

                    int samplingDelayInMiliSeconds = samplingDelayAux / 10;
                    int pidDelayInMiliSeconds = pidDelayAux / 10;

                    this.currentSamplingDelayLabel.Text = samplingDelayInMiliSeconds.ToString() + " ms";
                    this.currentPidDelayLabel.Text = pidDelayInMiliSeconds.ToString() + " ms";
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

        public void SetKpSendCommand(uint kp)
        {
            if (kp >= 0 && kp <= 0xFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = ((byte) kp);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKpValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= kp <= 255", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetKiSendCommand(uint ki)
        {
            if (ki >= 0 && ki <= 0xFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = ((byte) ki);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKiValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= ki <= 255", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetKdSendCommand(uint kd)
        {
            if (kd >= 0 && kd <= 0xFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                this.payloadTxDataBytes[0] = ((byte) kd);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetKdValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= kd <= 255", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetSamplingDelaySendCommand(uint samplingDelayInMsx10)
        {
            if (samplingDelayInMsx10 >= 0 && samplingDelayInMsx10 <= 0xFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 2;
                this.payloadTxDataBytes[0] = (byte) ((samplingDelayInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) (samplingDelayInMsx10 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetSamplingDelayValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= samplingDelayInMs <= 6553", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetPidDelaySendCommand(uint pidDelayInMsx10)
        {
            if (pidDelayInMsx10 >= 0 && pidDelayInMsx10 <= 0xFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 2;
                this.payloadTxDataBytes[0] = (byte) ((pidDelayInMsx10 >> 8) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) (pidDelayInMsx10 & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetPidDelayValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= pidDelayInMs <= 6553", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SetPidSetpointSendCommand(uint pidSetpoint)
        {
            if (pidSetpoint >= 0 && pidSetpoint <= 0xFFFFFFFF)
            {
                Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
                int qtyOfBytes = 4;
                this.payloadTxDataBytes[0] = (byte) ((pidSetpoint >> 24) & 0x00FF);
                this.payloadTxDataBytes[1] = (byte) ((pidSetpoint >> 16) & 0x00FF);
                this.payloadTxDataBytes[2] = (byte) ((pidSetpoint >> 8) & 0x00FF);
                this.payloadTxDataBytes[3] = (byte) (pidSetpoint & 0x00FF);
                this.dataPacketTx.SetCommand((byte) CommandsToMicrocontroller.SetPidSetpointValue);
                this.dataPacketTx.SetPayloadData(payloadTxDataBytes, (byte) qtyOfBytes);
                this.dataPacketTx.Mount();
                this.dataPacketTx.SerialSend(this.serialPort);
            }
            else
            {
                MessageBox.Show("0 <= pidSetpoint <= 4294967295", "Invalid value...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void SetCurrentPidDelayLabel(Label currentPidDelayLabel)
        {
            this.currentPidDelayLabel = currentPidDelayLabel;
        }

        public void SetCurrentSamplingDelayLabel(Label currentSamplingDelayLabel)
        {
            this.currentSamplingDelayLabel = currentSamplingDelayLabel;
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
