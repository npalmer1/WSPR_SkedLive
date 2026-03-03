namespace WSPR_Live
{
    partial class LiveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            testDBbutton = new Button();
            updatebutton = new Button();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            bandlistBox = new ListBox();
            label5 = new Label();
            filterbutton = new Button();
            datecheckBox = new CheckBox();
            DFromtextBox = new TextBox();
            callFiltertextBox = new TextBox();
            DTotextBox = new TextBox();
            label3 = new Label();
            label6 = new Label();
            kmcheckBox = new CheckBox();
            Ulabel = new Label();
            Dlabel = new Label();
            label7 = new Label();
            Nowbutton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            label4 = new Label();
            PlistBox = new ListBox();
            label8 = new Label();
            Plabel = new Label();
            timer2 = new System.Windows.Forms.Timer(components);
            updatecheckBox = new CheckBox();
            calltextBox = new TextBox();
            othercheckBox = new CheckBox();
            label9 = new Label();
            Clearbutton = new Button();
            disabledlabel = new Label();
            CWSSBlistBox = new ListBox();
            label10 = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // testDBbutton
            // 
            testDBbutton.Location = new Point(853, 1);
            testDBbutton.Name = "testDBbutton";
            testDBbutton.Size = new Size(164, 21);
            testDBbutton.TabIndex = 0;
            testDBbutton.Text = "test connection to wspr.live";
            testDBbutton.UseVisualStyleBackColor = true;
            testDBbutton.Click += testDBbutton_Click;
            // 
            // updatebutton
            // 
            updatebutton.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            updatebutton.Location = new Point(709, 1);
            updatebutton.Margin = new Padding(0);
            updatebutton.Name = "updatebutton";
            updatebutton.Size = new Size(126, 21);
            updatebutton.TabIndex = 2;
            updatebutton.Text = "Update from local db";
            updatebutton.UseVisualStyleBackColor = true;
            updatebutton.Click += updatebutton_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(608, 510);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(137, 23);
            dateTimePicker1.TabIndex = 5;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(795, 509);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(137, 23);
            dateTimePicker2.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(768, 513);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 7;
            label1.Text = "to:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(564, 514);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 8;
            label2.Text = "From:";
            // 
            // bandlistBox
            // 
            bandlistBox.FormattingEnabled = true;
            bandlistBox.Items.AddRange(new object[] { "All", "LF", "MF", "160m", "80m", "60m", "40m", "30m", "22m", "20m", "17m", "15m", "12m", "10m", "8m", "6m", "4m", "2m", "70cm", "23cm" });
            bandlistBox.Location = new Point(309, 514);
            bandlistBox.Name = "bandlistBox";
            bandlistBox.Size = new Size(70, 19);
            bandlistBox.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(266, 516);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 12;
            label5.Text = "Band:";
            // 
            // filterbutton
            // 
            filterbutton.Location = new Point(951, 514);
            filterbutton.Name = "filterbutton";
            filterbutton.Size = new Size(66, 23);
            filterbutton.TabIndex = 13;
            filterbutton.Text = "Apply";
            filterbutton.UseVisualStyleBackColor = true;
            filterbutton.Click += filterbutton_Click;
            // 
            // datecheckBox
            // 
            datecheckBox.AutoSize = true;
            datecheckBox.Location = new Point(397, 514);
            datecheckBox.Name = "datecheckBox";
            datecheckBox.Size = new Size(143, 19);
            datecheckBox.TabIndex = 14;
            datecheckBox.Text = "Enable date/time filter";
            datecheckBox.UseVisualStyleBackColor = true;
            // 
            // DFromtextBox
            // 
            DFromtextBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DFromtextBox.Location = new Point(642, 541);
            DFromtextBox.Name = "DFromtextBox";
            DFromtextBox.Size = new Size(61, 22);
            DFromtextBox.TabIndex = 15;
            DFromtextBox.TextChanged += DFromtextBox_TextChanged;
            DFromtextBox.KeyPress += DFromtextBox_KeyPress;
            // 
            // callFiltertextBox
            // 
            callFiltertextBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            callFiltertextBox.Location = new Point(457, 543);
            callFiltertextBox.Margin = new Padding(0);
            callFiltertextBox.Name = "callFiltertextBox";
            callFiltertextBox.Size = new Size(91, 22);
            callFiltertextBox.TabIndex = 16;
            callFiltertextBox.KeyPress += callFiltertextBox_KeyPress;
            // 
            // DTotextBox
            // 
            DTotextBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DTotextBox.Location = new Point(768, 540);
            DTotextBox.Name = "DTotextBox";
            DTotextBox.Size = new Size(59, 22);
            DTotextBox.TabIndex = 17;
            DTotextBox.KeyPress += DTotextBox_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(397, 547);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 18;
            label3.Text = "Call filter:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(581, 542);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 19;
            label6.Text = "Distance:";
            // 
            // kmcheckBox
            // 
            kmcheckBox.AutoSize = true;
            kmcheckBox.Location = new Point(865, 541);
            kmcheckBox.Name = "kmcheckBox";
            kmcheckBox.Size = new Size(65, 19);
            kmcheckBox.TabIndex = 21;
            kmcheckBox.Text = "Use km";
            kmcheckBox.UseVisualStyleBackColor = true;
            kmcheckBox.CheckedChanged += kmcheckBox_CheckedChanged;
            // 
            // Ulabel
            // 
            Ulabel.AutoSize = true;
            Ulabel.Location = new Point(833, 541);
            Ulabel.Name = "Ulabel";
            Ulabel.Size = new Size(26, 15);
            Ulabel.TabIndex = 22;
            Ulabel.Text = "mls";
            // 
            // Dlabel
            // 
            Dlabel.AutoSize = true;
            Dlabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Dlabel.Location = new Point(709, 543);
            Dlabel.Name = "Dlabel";
            Dlabel.Size = new Size(56, 13);
            Dlabel.TabIndex = 23;
            Dlabel.Text = "0 - 12,453";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(642, 583);
            label7.Name = "label7";
            label7.Size = new Size(390, 13);
            label7.TabIndex = 24;
            label7.Text = "To reduce traffic at wspr.live received reports may not appear for 6 minutes";
            // 
            // Nowbutton
            // 
            Nowbutton.Location = new Point(27, 539);
            Nowbutton.Name = "Nowbutton";
            Nowbutton.Size = new Size(88, 23);
            Nowbutton.TabIndex = 26;
            Nowbutton.Text = "Update now";
            Nowbutton.UseVisualStyleBackColor = true;
            Nowbutton.Click += Nowbutton_Click;
            // 
            // timer1
            // 
            timer1.Interval = 120000;
            timer1.Tick += timer1_Tick;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11, Column12, Column14, Column15, Column13 });
            dataGridView1.Location = new Point(12, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1142, 476);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column1.HeaderText = "Date";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 110;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column2.HeaderText = "Call";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Frequency";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 87;
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column4.HeaderText = "SNR";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 44;
            // 
            // Column5
            // 
            Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column5.HeaderText = "Drift";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 45;
            // 
            // Column6
            // 
            Column6.HeaderText = "TX loc";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 64;
            // 
            // Column7
            // 
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column7.HeaderText = "dBm";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 44;
            // 
            // Column8
            // 
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column8.HeaderText = "Reporter";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 110;
            // 
            // Column9
            // 
            Column9.HeaderText = "RX Loc";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.Width = 68;
            // 
            // Column10
            // 
            Column10.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column10.HeaderText = "km";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            Column10.Width = 52;
            // 
            // Column11
            // 
            Column11.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column11.HeaderText = "miles";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.Width = 52;
            // 
            // Column12
            // 
            Column12.HeaderText = "Az";
            Column12.Name = "Column12";
            Column12.ReadOnly = true;
            Column12.Width = 45;
            // 
            // Column14
            // 
            Column14.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column14.HeaderText = "CW @100W";
            Column14.Name = "Column14";
            Column14.ReadOnly = true;
            Column14.Width = 87;
            // 
            // Column15
            // 
            Column15.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column15.HeaderText = "SSB @100W";
            Column15.Name = "Column15";
            Column15.ReadOnly = true;
            Column15.Width = 87;
            // 
            // Column13
            // 
            Column13.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column13.HeaderText = "Version";
            Column13.Name = "Column13";
            Column13.ReadOnly = true;
            Column13.Width = 78;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(100, 515);
            label4.Name = "label4";
            label4.Size = new Size(160, 15);
            label4.TabIndex = 10;
            label4.Text = "Filters (interrogates local db):";
            // 
            // PlistBox
            // 
            PlistBox.FormattingEnabled = true;
            PlistBox.Items.AddRange(new object[] { "10 min", "20 min", "30 min", "1 hour", "3 hours", "6 hours", "12 hours", "24 hours" });
            PlistBox.Location = new Point(137, 573);
            PlistBox.Name = "PlistBox";
            PlistBox.Size = new Size(91, 19);
            PlistBox.TabIndex = 27;
            PlistBox.SelectedIndexChanged += PlistBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(46, 573);
            label8.Name = "label8";
            label8.Size = new Size(85, 15);
            label8.TabIndex = 28;
            label8.Text = "Update period:";
            // 
            // Plabel
            // 
            Plabel.AutoSize = true;
            Plabel.Location = new Point(238, 577);
            Plabel.Name = "Plabel";
            Plabel.Size = new Size(22, 15);
            Plabel.TabIndex = 29;
            Plabel.Text = "---";
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 60000;
            timer2.Tick += timer2_Tick;
            // 
            // updatecheckBox
            // 
            updatecheckBox.AutoSize = true;
            updatecheckBox.Location = new Point(296, 577);
            updatecheckBox.Name = "updatecheckBox";
            updatecheckBox.Size = new Size(257, 19);
            updatecheckBox.TabIndex = 30;
            updatecheckBox.Text = "Disable updates (avoid interupting a search)";
            updatecheckBox.UseVisualStyleBackColor = true;
            updatecheckBox.CheckedChanged += updatecheckBox_CheckedChanged;
            // 
            // calltextBox
            // 
            calltextBox.Enabled = false;
            calltextBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            calltextBox.Location = new Point(126, 3);
            calltextBox.Margin = new Padding(0);
            calltextBox.Name = "calltextBox";
            calltextBox.Size = new Size(113, 22);
            calltextBox.TabIndex = 32;
            calltextBox.TextChanged += calltextBox_TextChanged;
            // 
            // othercheckBox
            // 
            othercheckBox.AutoSize = true;
            othercheckBox.Location = new Point(12, 3);
            othercheckBox.Name = "othercheckBox";
            othercheckBox.Size = new Size(111, 19);
            othercheckBox.TabIndex = 33;
            othercheckBox.Text = "Allow other call:";
            othercheckBox.UseVisualStyleBackColor = true;
            othercheckBox.CheckedChanged += othercheckBox_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(242, 7);
            label9.Name = "label9";
            label9.Size = new Size(159, 15);
            label9.TabIndex = 34;
            label9.Text = "(note: not saved in database)";
            // 
            // Clearbutton
            // 
            Clearbutton.Location = new Point(951, 547);
            Clearbutton.Name = "Clearbutton";
            Clearbutton.Size = new Size(66, 23);
            Clearbutton.TabIndex = 35;
            Clearbutton.Text = "Clear";
            Clearbutton.UseVisualStyleBackColor = true;
            Clearbutton.Click += Clearbutton_Click;
            // 
            // disabledlabel
            // 
            disabledlabel.AutoSize = true;
            disabledlabel.BackColor = Color.Tomato;
            disabledlabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            disabledlabel.ForeColor = Color.Yellow;
            disabledlabel.Location = new Point(457, 7);
            disabledlabel.Name = "disabledlabel";
            disabledlabel.Size = new Size(101, 15);
            disabledlabel.TabIndex = 36;
            disabledlabel.Text = "Updates disabled";
            disabledlabel.Visible = false;
            // 
            // CWSSBlistBox
            // 
            CWSSBlistBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CWSSBlistBox.FormattingEnabled = true;
            CWSSBlistBox.Items.AddRange(new object[] { "5", "10", "15", "20", "30", "50", "70", "100", "200", "300", "500", "700", "1000", "1500", "2000" });
            CWSSBlistBox.Location = new Point(1102, 513);
            CWSSBlistBox.Name = "CWSSBlistBox";
            CWSSBlistBox.Size = new Size(52, 17);
            CWSSBlistBox.TabIndex = 37;
            CWSSBlistBox.SelectedValueChanged += CWSSBlistBox_SelectedValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(1037, 513);
            label10.Name = "label10";
            label10.Size = new Size(60, 26);
            label10.TabIndex = 38;
            label10.Text = "CW/SSB \r\npower (W)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1037, 541);
            label11.Name = "label11";
            label11.Size = new Size(88, 15);
            label11.TabIndex = 39;
            label11.Text = "for comparison";
            // 
            // LiveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1176, 604);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(CWSSBlistBox);
            Controls.Add(disabledlabel);
            Controls.Add(Clearbutton);
            Controls.Add(label9);
            Controls.Add(othercheckBox);
            Controls.Add(calltextBox);
            Controls.Add(updatecheckBox);
            Controls.Add(Plabel);
            Controls.Add(label8);
            Controls.Add(PlistBox);
            Controls.Add(Nowbutton);
            Controls.Add(label7);
            Controls.Add(Dlabel);
            Controls.Add(Ulabel);
            Controls.Add(kmcheckBox);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(DTotextBox);
            Controls.Add(callFiltertextBox);
            Controls.Add(DFromtextBox);
            Controls.Add(datecheckBox);
            Controls.Add(filterbutton);
            Controls.Add(label5);
            Controls.Add(bandlistBox);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(dataGridView1);
            Controls.Add(updatebutton);
            Controls.Add(testDBbutton);
            Name = "LiveForm";
            Text = "Received transmissions for this call";
            Load += LiveForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button testDBbutton;
        private System.Windows.Forms.Button updatebutton;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox bandlistBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button filterbutton;
        private System.Windows.Forms.CheckBox datecheckBox;
        private System.Windows.Forms.TextBox DFromtextBox;
        private System.Windows.Forms.TextBox callFiltertextBox;
        private System.Windows.Forms.TextBox DTotextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox kmcheckBox;
        private System.Windows.Forms.Label Ulabel;
        private System.Windows.Forms.Label Dlabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Nowbutton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox PlistBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Plabel;
        private System.Windows.Forms.Timer timer2;
        private CheckBox updatecheckBox;
        private TextBox calltextBox;
        private CheckBox othercheckBox;
        private Label label9;
        private Button Clearbutton;
        private Label disabledlabel;
        private ListBox CWSSBlistBox;
        private Label label10;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column13;
        private Label label11;
    }
}