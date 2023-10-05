﻿namespace PidTuningHelper
{
    partial class PidTuningHelper
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(60D, 3500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(120D, 1000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(180D, 250D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(240D, 4000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(300D, 2000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(360D, 4500D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSerialPort = new System.Windows.Forms.TabPage();
            this.settingsGb = new System.Windows.Forms.GroupBox();
            this.portStatusPb = new System.Windows.Forms.ProgressBar();
            this.closeBtn = new System.Windows.Forms.Button();
            this.openBtn = new System.Windows.Forms.Button();
            this.flowControlLbl = new System.Windows.Forms.Label();
            this.flowControlCb = new System.Windows.Forms.ComboBox();
            this.parityLbl = new System.Windows.Forms.Label();
            this.parityCb = new System.Windows.Forms.ComboBox();
            this.stopBitsLbl = new System.Windows.Forms.Label();
            this.stopBitsCb = new System.Windows.Forms.ComboBox();
            this.dataBitsLbl = new System.Windows.Forms.Label();
            this.dataBitsCb = new System.Windows.Forms.ComboBox();
            this.baudrateLbl = new System.Windows.Forms.Label();
            this.comPortLbl = new System.Windows.Forms.Label();
            this.baudrateCb = new System.Windows.Forms.ComboBox();
            this.comPortCb = new System.Windows.Forms.ComboBox();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.currentSamplingDelayLbl = new System.Windows.Forms.Label();
            this.currentPidDelayLbl = new System.Windows.Forms.Label();
            this.currentPidSetpointLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.currentKdLbl = new System.Windows.Forms.Label();
            this.currentKiLbl = new System.Windows.Forms.Label();
            this.currentKpLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.readConfigDataBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.startPidBtn = new System.Windows.Forms.Button();
            this.stopPidBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.setConfigDataBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.pidSetpointTxtBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pidDelayTxtBox = new System.Windows.Forms.TextBox();
            this.setKsBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.kiTxtBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.kdTxtBox = new System.Windows.Forms.TextBox();
            this.kpTxtBox = new System.Windows.Forms.TextBox();
            this.samplingDelayTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resizeBtn = new System.Windows.Forms.Button();
            this.maxXLbl = new System.Windows.Forms.Label();
            this.maxXTxtBox = new System.Windows.Forms.TextBox();
            this.maxYLbl = new System.Windows.Forms.Label();
            this.minYTxtBox = new System.Windows.Forms.TextBox();
            this.minXTxtBox = new System.Windows.Forms.TextBox();
            this.maxYTxtBox = new System.Windows.Forms.TextBox();
            this.minYLbl = new System.Windows.Forms.Label();
            this.minXLbl = new System.Windows.Forms.Label();
            this.controlGb = new System.Windows.Forms.GroupBox();
            this.loadDataBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.saveDataBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.graphGb = new System.Windows.Forms.GroupBox();
            this.lineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageSerialPort.SuspendLayout();
            this.settingsGb.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.controlGb.SuspendLayout();
            this.graphGb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSerialPort);
            this.tabControl1.Controls.Add(this.tabPageGraph);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1254, 613);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSerialPort
            // 
            this.tabPageSerialPort.Controls.Add(this.settingsGb);
            this.tabPageSerialPort.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerialPort.Name = "tabPageSerialPort";
            this.tabPageSerialPort.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerialPort.Size = new System.Drawing.Size(1246, 587);
            this.tabPageSerialPort.TabIndex = 0;
            this.tabPageSerialPort.Text = "Serial Port";
            this.tabPageSerialPort.UseVisualStyleBackColor = true;
            // 
            // settingsGb
            // 
            this.settingsGb.Controls.Add(this.portStatusPb);
            this.settingsGb.Controls.Add(this.closeBtn);
            this.settingsGb.Controls.Add(this.openBtn);
            this.settingsGb.Controls.Add(this.flowControlLbl);
            this.settingsGb.Controls.Add(this.flowControlCb);
            this.settingsGb.Controls.Add(this.parityLbl);
            this.settingsGb.Controls.Add(this.parityCb);
            this.settingsGb.Controls.Add(this.stopBitsLbl);
            this.settingsGb.Controls.Add(this.stopBitsCb);
            this.settingsGb.Controls.Add(this.dataBitsLbl);
            this.settingsGb.Controls.Add(this.dataBitsCb);
            this.settingsGb.Controls.Add(this.baudrateLbl);
            this.settingsGb.Controls.Add(this.comPortLbl);
            this.settingsGb.Controls.Add(this.baudrateCb);
            this.settingsGb.Controls.Add(this.comPortCb);
            this.settingsGb.Location = new System.Drawing.Point(420, 93);
            this.settingsGb.Name = "settingsGb";
            this.settingsGb.Size = new System.Drawing.Size(396, 379);
            this.settingsGb.TabIndex = 1;
            this.settingsGb.TabStop = false;
            this.settingsGb.Text = "Settings";
            // 
            // portStatusPb
            // 
            this.portStatusPb.Location = new System.Drawing.Point(31, 309);
            this.portStatusPb.Name = "portStatusPb";
            this.portStatusPb.Size = new System.Drawing.Size(335, 50);
            this.portStatusPb.TabIndex = 14;
            // 
            // closeBtn
            // 
            this.closeBtn.Enabled = false;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(210, 228);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(156, 59);
            this.closeBtn.TabIndex = 13;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // openBtn
            // 
            this.openBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openBtn.Location = new System.Drawing.Point(31, 228);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(162, 59);
            this.openBtn.TabIndex = 12;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // flowControlLbl
            // 
            this.flowControlLbl.AutoSize = true;
            this.flowControlLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowControlLbl.Location = new System.Drawing.Point(27, 180);
            this.flowControlLbl.Name = "flowControlLbl";
            this.flowControlLbl.Size = new System.Drawing.Size(98, 20);
            this.flowControlLbl.TabIndex = 11;
            this.flowControlLbl.Text = "Flow control:";
            // 
            // flowControlCb
            // 
            this.flowControlCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flowControlCb.FormattingEnabled = true;
            this.flowControlCb.Items.AddRange(new object[] {
            "None",
            "Xon/Xoff"});
            this.flowControlCb.Location = new System.Drawing.Point(186, 179);
            this.flowControlCb.Name = "flowControlCb";
            this.flowControlCb.Size = new System.Drawing.Size(180, 21);
            this.flowControlCb.TabIndex = 10;
            // 
            // parityLbl
            // 
            this.parityLbl.AutoSize = true;
            this.parityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parityLbl.Location = new System.Drawing.Point(27, 153);
            this.parityLbl.Name = "parityLbl";
            this.parityLbl.Size = new System.Drawing.Size(52, 20);
            this.parityLbl.TabIndex = 9;
            this.parityLbl.Text = "Parity:";
            // 
            // parityCb
            // 
            this.parityCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityCb.FormattingEnabled = true;
            this.parityCb.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.parityCb.Location = new System.Drawing.Point(186, 152);
            this.parityCb.Name = "parityCb";
            this.parityCb.Size = new System.Drawing.Size(180, 21);
            this.parityCb.TabIndex = 8;
            // 
            // stopBitsLbl
            // 
            this.stopBitsLbl.AutoSize = true;
            this.stopBitsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopBitsLbl.Location = new System.Drawing.Point(27, 126);
            this.stopBitsLbl.Name = "stopBitsLbl";
            this.stopBitsLbl.Size = new System.Drawing.Size(76, 20);
            this.stopBitsLbl.TabIndex = 7;
            this.stopBitsLbl.Text = "Stop bits:";
            // 
            // stopBitsCb
            // 
            this.stopBitsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsCb.FormattingEnabled = true;
            this.stopBitsCb.Items.AddRange(new object[] {
            "1 bit",
            "1.5 bit",
            "2 bit"});
            this.stopBitsCb.Location = new System.Drawing.Point(186, 125);
            this.stopBitsCb.Name = "stopBitsCb";
            this.stopBitsCb.Size = new System.Drawing.Size(180, 21);
            this.stopBitsCb.TabIndex = 6;
            // 
            // dataBitsLbl
            // 
            this.dataBitsLbl.AutoSize = true;
            this.dataBitsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataBitsLbl.Location = new System.Drawing.Point(27, 99);
            this.dataBitsLbl.Name = "dataBitsLbl";
            this.dataBitsLbl.Size = new System.Drawing.Size(77, 20);
            this.dataBitsLbl.TabIndex = 5;
            this.dataBitsLbl.Text = "Data bits:";
            // 
            // dataBitsCb
            // 
            this.dataBitsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsCb.FormattingEnabled = true;
            this.dataBitsCb.Items.AddRange(new object[] {
            "7 bits",
            "8 bits"});
            this.dataBitsCb.Location = new System.Drawing.Point(186, 98);
            this.dataBitsCb.Name = "dataBitsCb";
            this.dataBitsCb.Size = new System.Drawing.Size(180, 21);
            this.dataBitsCb.TabIndex = 4;
            // 
            // baudrateLbl
            // 
            this.baudrateLbl.AutoSize = true;
            this.baudrateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baudrateLbl.Location = new System.Drawing.Point(27, 72);
            this.baudrateLbl.Name = "baudrateLbl";
            this.baudrateLbl.Size = new System.Drawing.Size(79, 20);
            this.baudrateLbl.TabIndex = 3;
            this.baudrateLbl.Text = "Baudrate:";
            // 
            // comPortLbl
            // 
            this.comPortLbl.AutoSize = true;
            this.comPortLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comPortLbl.Location = new System.Drawing.Point(27, 45);
            this.comPortLbl.Name = "comPortLbl";
            this.comPortLbl.Size = new System.Drawing.Size(82, 20);
            this.comPortLbl.TabIndex = 1;
            this.comPortLbl.Text = "COM Port:";
            // 
            // baudrateCb
            // 
            this.baudrateCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudrateCb.FormattingEnabled = true;
            this.baudrateCb.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.baudrateCb.Location = new System.Drawing.Point(186, 71);
            this.baudrateCb.Name = "baudrateCb";
            this.baudrateCb.Size = new System.Drawing.Size(180, 21);
            this.baudrateCb.TabIndex = 2;
            // 
            // comPortCb
            // 
            this.comPortCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortCb.FormattingEnabled = true;
            this.comPortCb.Location = new System.Drawing.Point(186, 44);
            this.comPortCb.Name = "comPortCb";
            this.comPortCb.Size = new System.Drawing.Size(180, 21);
            this.comPortCb.TabIndex = 0;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.Controls.Add(this.groupBox4);
            this.tabPageGraph.Controls.Add(this.groupBox3);
            this.tabPageGraph.Controls.Add(this.groupBox2);
            this.tabPageGraph.Controls.Add(this.groupBox1);
            this.tabPageGraph.Controls.Add(this.controlGb);
            this.tabPageGraph.Controls.Add(this.graphGb);
            this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraph.Size = new System.Drawing.Size(1246, 587);
            this.tabPageGraph.TabIndex = 1;
            this.tabPageGraph.Text = "PID";
            this.tabPageGraph.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.currentSamplingDelayLbl);
            this.groupBox4.Controls.Add(this.currentPidDelayLbl);
            this.groupBox4.Controls.Add(this.currentPidSetpointLbl);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.currentKdLbl);
            this.groupBox4.Controls.Add(this.currentKiLbl);
            this.groupBox4.Controls.Add(this.currentKpLbl);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.readConfigDataBtn);
            this.groupBox4.Location = new System.Drawing.Point(789, 386);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(451, 195);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PID Monitor";
            // 
            // currentSamplingDelayLbl
            // 
            this.currentSamplingDelayLbl.AutoSize = true;
            this.currentSamplingDelayLbl.Location = new System.Drawing.Point(273, 141);
            this.currentSamplingDelayLbl.Name = "currentSamplingDelayLbl";
            this.currentSamplingDelayLbl.Size = new System.Drawing.Size(16, 13);
            this.currentSamplingDelayLbl.TabIndex = 29;
            this.currentSamplingDelayLbl.Text = "...";
            // 
            // currentPidDelayLbl
            // 
            this.currentPidDelayLbl.AutoSize = true;
            this.currentPidDelayLbl.Location = new System.Drawing.Point(273, 98);
            this.currentPidDelayLbl.Name = "currentPidDelayLbl";
            this.currentPidDelayLbl.Size = new System.Drawing.Size(16, 13);
            this.currentPidDelayLbl.TabIndex = 31;
            this.currentPidDelayLbl.Text = "...";
            // 
            // currentPidSetpointLbl
            // 
            this.currentPidSetpointLbl.AutoSize = true;
            this.currentPidSetpointLbl.Location = new System.Drawing.Point(273, 54);
            this.currentPidSetpointLbl.Name = "currentPidSetpointLbl";
            this.currentPidSetpointLbl.Size = new System.Drawing.Size(16, 13);
            this.currentPidSetpointLbl.TabIndex = 30;
            this.currentPidSetpointLbl.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "PID Setpoint:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "PID delay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Sampling delay:";
            // 
            // currentKdLbl
            // 
            this.currentKdLbl.AutoSize = true;
            this.currentKdLbl.Location = new System.Drawing.Point(376, 141);
            this.currentKdLbl.Name = "currentKdLbl";
            this.currentKdLbl.Size = new System.Drawing.Size(16, 13);
            this.currentKdLbl.TabIndex = 23;
            this.currentKdLbl.Text = "...";
            // 
            // currentKiLbl
            // 
            this.currentKiLbl.AutoSize = true;
            this.currentKiLbl.Location = new System.Drawing.Point(376, 98);
            this.currentKiLbl.Name = "currentKiLbl";
            this.currentKiLbl.Size = new System.Drawing.Size(16, 13);
            this.currentKiLbl.TabIndex = 25;
            this.currentKiLbl.Text = "...";
            // 
            // currentKpLbl
            // 
            this.currentKpLbl.AutoSize = true;
            this.currentKpLbl.Location = new System.Drawing.Point(376, 54);
            this.currentKpLbl.Name = "currentKpLbl";
            this.currentKpLbl.Size = new System.Drawing.Size(16, 13);
            this.currentKpLbl.TabIndex = 24;
            this.currentKpLbl.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Kd:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Ki:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Kp:";
            // 
            // readConfigDataBtn
            // 
            this.readConfigDataBtn.Location = new System.Drawing.Point(6, 41);
            this.readConfigDataBtn.Name = "readConfigDataBtn";
            this.readConfigDataBtn.Size = new System.Drawing.Size(156, 123);
            this.readConfigDataBtn.TabIndex = 20;
            this.readConfigDataBtn.Text = "Read Config Data";
            this.readConfigDataBtn.UseVisualStyleBackColor = true;
            this.readConfigDataBtn.Click += new System.EventHandler(this.readConfigDataBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.startPidBtn);
            this.groupBox3.Controls.Add(this.stopPidBtn);
            this.groupBox3.Location = new System.Drawing.Point(8, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 195);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PID Control";
            // 
            // startPidBtn
            // 
            this.startPidBtn.Location = new System.Drawing.Point(19, 19);
            this.startPidBtn.Name = "startPidBtn";
            this.startPidBtn.Size = new System.Drawing.Size(156, 82);
            this.startPidBtn.TabIndex = 21;
            this.startPidBtn.Text = "Start PID";
            this.startPidBtn.UseVisualStyleBackColor = true;
            // 
            // stopPidBtn
            // 
            this.stopPidBtn.Location = new System.Drawing.Point(19, 107);
            this.stopPidBtn.Name = "stopPidBtn";
            this.stopPidBtn.Size = new System.Drawing.Size(156, 82);
            this.stopPidBtn.TabIndex = 22;
            this.stopPidBtn.Text = "Stop PID";
            this.stopPidBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.setConfigDataBtn);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.pidSetpointTxtBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.pidDelayTxtBox);
            this.groupBox2.Controls.Add(this.setKsBtn);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.kiTxtBox);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.kdTxtBox);
            this.groupBox2.Controls.Add(this.kpTxtBox);
            this.groupBox2.Controls.Add(this.samplingDelayTxtBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(211, 386);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 195);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PID Settings";
            // 
            // setConfigDataBtn
            // 
            this.setConfigDataBtn.Location = new System.Drawing.Point(130, 41);
            this.setConfigDataBtn.Name = "setConfigDataBtn";
            this.setConfigDataBtn.Size = new System.Drawing.Size(156, 123);
            this.setConfigDataBtn.TabIndex = 19;
            this.setConfigDataBtn.Text = "Set Config Data";
            this.setConfigDataBtn.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "PID Setpoint";
            // 
            // pidSetpointTxtBox
            // 
            this.pidSetpointTxtBox.Location = new System.Drawing.Point(18, 57);
            this.pidSetpointTxtBox.Name = "pidSetpointTxtBox";
            this.pidSetpointTxtBox.Size = new System.Drawing.Size(106, 20);
            this.pidSetpointTxtBox.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "PID delay";
            // 
            // pidDelayTxtBox
            // 
            this.pidDelayTxtBox.Location = new System.Drawing.Point(18, 101);
            this.pidDelayTxtBox.Name = "pidDelayTxtBox";
            this.pidDelayTxtBox.Size = new System.Drawing.Size(106, 20);
            this.pidDelayTxtBox.TabIndex = 15;
            // 
            // setKsBtn
            // 
            this.setKsBtn.Location = new System.Drawing.Point(393, 41);
            this.setKsBtn.Name = "setKsBtn";
            this.setKsBtn.Size = new System.Drawing.Size(156, 123);
            this.setKsBtn.TabIndex = 6;
            this.setKsBtn.Text = "Set Ks";
            this.setKsBtn.UseVisualStyleBackColor = true;
            this.setKsBtn.Click += new System.EventHandler(this.setKsBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(322, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Ki";
            // 
            // kiTxtBox
            // 
            this.kiTxtBox.Location = new System.Drawing.Point(325, 101);
            this.kiTxtBox.Name = "kiTxtBox";
            this.kiTxtBox.Size = new System.Drawing.Size(62, 20);
            this.kiTxtBox.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Sampling delay";
            // 
            // kdTxtBox
            // 
            this.kdTxtBox.Location = new System.Drawing.Point(325, 144);
            this.kdTxtBox.Name = "kdTxtBox";
            this.kdTxtBox.Size = new System.Drawing.Size(62, 20);
            this.kdTxtBox.TabIndex = 10;
            // 
            // kpTxtBox
            // 
            this.kpTxtBox.Location = new System.Drawing.Point(325, 57);
            this.kpTxtBox.Name = "kpTxtBox";
            this.kpTxtBox.Size = new System.Drawing.Size(62, 20);
            this.kpTxtBox.TabIndex = 6;
            // 
            // samplingDelayTxtBox
            // 
            this.samplingDelayTxtBox.Location = new System.Drawing.Point(18, 144);
            this.samplingDelayTxtBox.Name = "samplingDelayTxtBox";
            this.samplingDelayTxtBox.Size = new System.Drawing.Size(106, 20);
            this.samplingDelayTxtBox.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(322, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Kd";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Kp";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resizeBtn);
            this.groupBox1.Controls.Add(this.maxXLbl);
            this.groupBox1.Controls.Add(this.maxXTxtBox);
            this.groupBox1.Controls.Add(this.maxYLbl);
            this.groupBox1.Controls.Add(this.minYTxtBox);
            this.groupBox1.Controls.Add(this.minXTxtBox);
            this.groupBox1.Controls.Add(this.maxYTxtBox);
            this.groupBox1.Controls.Add(this.minYLbl);
            this.groupBox1.Controls.Add(this.minXLbl);
            this.groupBox1.Location = new System.Drawing.Point(977, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 150);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Axes config";
            // 
            // resizeBtn
            // 
            this.resizeBtn.Location = new System.Drawing.Point(28, 111);
            this.resizeBtn.Name = "resizeBtn";
            this.resizeBtn.Size = new System.Drawing.Size(206, 31);
            this.resizeBtn.TabIndex = 6;
            this.resizeBtn.Text = "Resize";
            this.resizeBtn.UseVisualStyleBackColor = true;
            this.resizeBtn.Click += new System.EventHandler(this.resizeBtn_Click);
            // 
            // maxXLbl
            // 
            this.maxXLbl.AutoSize = true;
            this.maxXLbl.Location = new System.Drawing.Point(131, 26);
            this.maxXLbl.Name = "maxXLbl";
            this.maxXLbl.Size = new System.Drawing.Size(37, 13);
            this.maxXLbl.TabIndex = 9;
            this.maxXLbl.Text = "Max-X";
            // 
            // maxXTxtBox
            // 
            this.maxXTxtBox.Location = new System.Drawing.Point(134, 42);
            this.maxXTxtBox.Name = "maxXTxtBox";
            this.maxXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.maxXTxtBox.TabIndex = 8;
            // 
            // maxYLbl
            // 
            this.maxYLbl.AutoSize = true;
            this.maxYLbl.Location = new System.Drawing.Point(131, 69);
            this.maxYLbl.Name = "maxYLbl";
            this.maxYLbl.Size = new System.Drawing.Size(37, 13);
            this.maxYLbl.TabIndex = 13;
            this.maxYLbl.Text = "Max-Y";
            // 
            // minYTxtBox
            // 
            this.minYTxtBox.Location = new System.Drawing.Point(28, 85);
            this.minYTxtBox.Name = "minYTxtBox";
            this.minYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.minYTxtBox.TabIndex = 10;
            // 
            // minXTxtBox
            // 
            this.minXTxtBox.Location = new System.Drawing.Point(28, 42);
            this.minXTxtBox.Name = "minXTxtBox";
            this.minXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.minXTxtBox.TabIndex = 6;
            // 
            // maxYTxtBox
            // 
            this.maxYTxtBox.Location = new System.Drawing.Point(134, 85);
            this.maxYTxtBox.Name = "maxYTxtBox";
            this.maxYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.maxYTxtBox.TabIndex = 12;
            // 
            // minYLbl
            // 
            this.minYLbl.AutoSize = true;
            this.minYLbl.Location = new System.Drawing.Point(25, 69);
            this.minYLbl.Name = "minYLbl";
            this.minYLbl.Size = new System.Drawing.Size(34, 13);
            this.minYLbl.TabIndex = 11;
            this.minYLbl.Text = "Min-Y";
            // 
            // minXLbl
            // 
            this.minXLbl.AutoSize = true;
            this.minXLbl.Location = new System.Drawing.Point(25, 26);
            this.minXLbl.Name = "minXLbl";
            this.minXLbl.Size = new System.Drawing.Size(34, 13);
            this.minXLbl.TabIndex = 7;
            this.minXLbl.Text = "Min-X";
            // 
            // controlGb
            // 
            this.controlGb.Controls.Add(this.loadDataBtn);
            this.controlGb.Controls.Add(this.startBtn);
            this.controlGb.Controls.Add(this.saveDataBtn);
            this.controlGb.Controls.Add(this.stopBtn);
            this.controlGb.Controls.Add(this.clearBtn);
            this.controlGb.Location = new System.Drawing.Point(977, 162);
            this.controlGb.Name = "controlGb";
            this.controlGb.Size = new System.Drawing.Size(263, 218);
            this.controlGb.TabIndex = 16;
            this.controlGb.TabStop = false;
            this.controlGb.Text = "Plotting control";
            // 
            // loadDataBtn
            // 
            this.loadDataBtn.Location = new System.Drawing.Point(134, 159);
            this.loadDataBtn.Name = "loadDataBtn";
            this.loadDataBtn.Size = new System.Drawing.Size(122, 53);
            this.loadDataBtn.TabIndex = 5;
            this.loadDataBtn.Text = "Load Data";
            this.loadDataBtn.UseVisualStyleBackColor = true;
            this.loadDataBtn.Click += new System.EventHandler(this.loadDataBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(6, 23);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(122, 53);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start Plot";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // saveDataBtn
            // 
            this.saveDataBtn.Location = new System.Drawing.Point(6, 159);
            this.saveDataBtn.Name = "saveDataBtn";
            this.saveDataBtn.Size = new System.Drawing.Size(122, 53);
            this.saveDataBtn.TabIndex = 4;
            this.saveDataBtn.Text = "Save Data";
            this.saveDataBtn.UseVisualStyleBackColor = true;
            this.saveDataBtn.Click += new System.EventHandler(this.saveDataBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(134, 23);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(123, 53);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop Plot";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(6, 82);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(251, 53);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // graphGb
            // 
            this.graphGb.Controls.Add(this.lineChart);
            this.graphGb.Location = new System.Drawing.Point(8, 6);
            this.graphGb.Name = "graphGb";
            this.graphGb.Size = new System.Drawing.Size(963, 374);
            this.graphGb.TabIndex = 15;
            this.graphGb.TabStop = false;
            this.graphGb.Text = "Chart";
            // 
            // lineChart
            // 
            this.lineChart.BorderlineWidth = 2;
            chartArea2.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea2.AxisX.InterlacedColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.Interval = 25D;
            chartArea2.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.MajorGrid.Interval = 25D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.Maximum = 500D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.MinorGrid.Enabled = true;
            chartArea2.AxisX.MinorGrid.Interval = 25D;
            chartArea2.AxisX.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.MinorTickMark.Enabled = true;
            chartArea2.AxisX.MinorTickMark.Interval = 25D;
            chartArea2.AxisX.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea2.AxisY.Crossing = -1.7976931348623157E+308D;
            chartArea2.AxisY.InterlacedColor = System.Drawing.Color.LightGoldenrodYellow;
            chartArea2.AxisY.Interval = 500D;
            chartArea2.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MajorGrid.Interval = 500D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.Maximum = 5000D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.MinorGrid.Enabled = true;
            chartArea2.AxisY.MinorGrid.Interval = 500D;
            chartArea2.AxisY.MinorGrid.IntervalOffset = double.NaN;
            chartArea2.AxisY.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.MinorTickMark.Enabled = true;
            chartArea2.AxisY.MinorTickMark.Interval = 500D;
            chartArea2.AxisY.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.DimGray;
            chartArea2.Name = "ChartArea1";
            this.lineChart.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.BorderColor = System.Drawing.Color.Blue;
            legend2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            legend2.InterlacedRowsColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.lineChart.Legends.Add(legend2);
            this.lineChart.Location = new System.Drawing.Point(6, 19);
            this.lineChart.Name = "lineChart";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Blue;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.LegendText = "Process Variable";
            series2.Name = "processVariableSerie";
            dataPoint8.BorderWidth = 2;
            dataPoint8.IsVisibleInLegend = false;
            dataPoint8.LegendText = "Process Variable";
            dataPoint9.BorderWidth = 2;
            dataPoint9.IsVisibleInLegend = false;
            dataPoint9.LegendText = "Process Variable";
            dataPoint10.BorderWidth = 2;
            dataPoint10.IsVisibleInLegend = false;
            dataPoint10.LegendText = "Process Variable";
            dataPoint11.BorderWidth = 2;
            dataPoint11.IsVisibleInLegend = false;
            dataPoint11.LegendText = "Process Variable";
            dataPoint12.BorderWidth = 2;
            dataPoint12.IsVisibleInLegend = false;
            dataPoint12.LegendText = "Process Variable";
            dataPoint13.BorderWidth = 2;
            dataPoint13.IsVisibleInLegend = false;
            dataPoint13.LegendText = "Process Variable";
            dataPoint14.BorderWidth = 2;
            dataPoint14.IsVisibleInLegend = false;
            dataPoint14.LegendText = "Process Variable";
            series2.Points.Add(dataPoint8);
            series2.Points.Add(dataPoint9);
            series2.Points.Add(dataPoint10);
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.lineChart.Series.Add(series2);
            this.lineChart.Size = new System.Drawing.Size(951, 349);
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "Line Chart";
            title2.Visible = false;
            this.lineChart.Titles.Add(title2);
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 25;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // PidTuningHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 615);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PidTuningHelper";
            this.Text = "PID Tuning Helper";
            this.tabControl1.ResumeLayout(false);
            this.tabPageSerialPort.ResumeLayout(false);
            this.settingsGb.ResumeLayout(false);
            this.settingsGb.PerformLayout();
            this.tabPageGraph.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.controlGb.ResumeLayout(false);
            this.graphGb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSerialPort;
        private System.Windows.Forms.GroupBox settingsGb;
        private System.Windows.Forms.ProgressBar portStatusPb;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label flowControlLbl;
        private System.Windows.Forms.ComboBox flowControlCb;
        private System.Windows.Forms.Label parityLbl;
        private System.Windows.Forms.ComboBox parityCb;
        private System.Windows.Forms.Label stopBitsLbl;
        private System.Windows.Forms.ComboBox stopBitsCb;
        private System.Windows.Forms.Label dataBitsLbl;
        private System.Windows.Forms.ComboBox dataBitsCb;
        private System.Windows.Forms.Label baudrateLbl;
        private System.Windows.Forms.Label comPortLbl;
        private System.Windows.Forms.ComboBox baudrateCb;
        private System.Windows.Forms.ComboBox comPortCb;
        private System.Windows.Forms.TabPage tabPageGraph;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button resizeBtn;
        private System.Windows.Forms.Label maxXLbl;
        private System.Windows.Forms.TextBox maxXTxtBox;
        private System.Windows.Forms.Label maxYLbl;
        private System.Windows.Forms.TextBox minYTxtBox;
        private System.Windows.Forms.TextBox minXTxtBox;
        private System.Windows.Forms.TextBox maxYTxtBox;
        private System.Windows.Forms.Label minYLbl;
        private System.Windows.Forms.Label minXLbl;
        private System.Windows.Forms.GroupBox controlGb;
        private System.Windows.Forms.Button loadDataBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button saveDataBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.GroupBox graphGb;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart lineChart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button setKsBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox kiTxtBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox kdTxtBox;
        private System.Windows.Forms.TextBox kpTxtBox;
        private System.Windows.Forms.TextBox samplingDelayTxtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox pidDelayTxtBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox pidSetpointTxtBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button startPidBtn;
        private System.Windows.Forms.Button stopPidBtn;
        private System.Windows.Forms.Button readConfigDataBtn;
        private System.Windows.Forms.Button setConfigDataBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label currentKdLbl;
        private System.Windows.Forms.Label currentKiLbl;
        private System.Windows.Forms.Label currentKpLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentSamplingDelayLbl;
        private System.Windows.Forms.Label currentPidDelayLbl;
        private System.Windows.Forms.Label currentPidSetpointLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer3;
    }
}

