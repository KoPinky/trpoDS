namespace WpfApp2
{
    partial class graphWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AngleList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Rebuilding = new System.Windows.Forms.Button();
            this.VectorsBox = new System.Windows.Forms.ComboBox();
            this.AngleBox = new System.Windows.Forms.TextBox();
            this.AngleL = new System.Windows.Forms.Label();
            this.VectorL = new System.Windows.Forms.Label();
            this.PointRot = new System.Windows.Forms.Label();
            this.XPoint = new System.Windows.Forms.TextBox();
            this.YPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(12, 21);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chart.Size = new System.Drawing.Size(435, 360);
            this.chart.TabIndex = 0;
            this.chart.Text = "Charts";
            this.chart.UseWaitCursor = true;
            // 
            // AngleList
            // 
            this.AngleList.Location = new System.Drawing.Point(487, 213);
            this.AngleList.Multiline = true;
            this.AngleList.Name = "AngleList";
            this.AngleList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AngleList.Size = new System.Drawing.Size(279, 197);
            this.AngleList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Углы : ";
            // 
            // Rebuilding
            // 
            this.Rebuilding.Location = new System.Drawing.Point(544, 128);
            this.Rebuilding.Name = "Rebuilding";
            this.Rebuilding.Size = new System.Drawing.Size(75, 23);
            this.Rebuilding.TabIndex = 3;
            this.Rebuilding.Text = "Поворот";
            this.Rebuilding.UseVisualStyleBackColor = true;
            this.Rebuilding.Click += new System.EventHandler(this.Rebuilding_Click);
            // 
            // VectorsBox
            // 
            this.VectorsBox.FormattingEnabled = true;
            this.VectorsBox.Location = new System.Drawing.Point(645, 90);
            this.VectorsBox.Name = "VectorsBox";
            this.VectorsBox.Size = new System.Drawing.Size(121, 21);
            this.VectorsBox.TabIndex = 4;
            // 
            // AngleBox
            // 
            this.AngleBox.Location = new System.Drawing.Point(645, 66);
            this.AngleBox.Name = "AngleBox";
            this.AngleBox.Size = new System.Drawing.Size(121, 20);
            this.AngleBox.TabIndex = 6;
            // 
            // AngleL
            // 
            this.AngleL.AutoSize = true;
            this.AngleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AngleL.Location = new System.Drawing.Point(484, 67);
            this.AngleL.Name = "AngleL";
            this.AngleL.Size = new System.Drawing.Size(38, 17);
            this.AngleL.TabIndex = 7;
            this.AngleL.Text = "Угол";
            // 
            // VectorL
            // 
            this.VectorL.AutoSize = true;
            this.VectorL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VectorL.Location = new System.Drawing.Point(484, 96);
            this.VectorL.Name = "VectorL";
            this.VectorL.Size = new System.Drawing.Size(55, 17);
            this.VectorL.TabIndex = 7;
            this.VectorL.Text = "Вектор";
            // 
            // PointRot
            // 
            this.PointRot.AutoSize = true;
            this.PointRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PointRot.Location = new System.Drawing.Point(484, 39);
            this.PointRot.Name = "PointRot";
            this.PointRot.Size = new System.Drawing.Size(114, 17);
            this.PointRot.TabIndex = 7;
            this.PointRot.Text = "Точка поворота";
            // 
            // XPoint
            // 
            this.XPoint.Location = new System.Drawing.Point(671, 38);
            this.XPoint.Name = "XPoint";
            this.XPoint.Size = new System.Drawing.Size(32, 20);
            this.XPoint.TabIndex = 8;
            // 
            // YPoint
            // 
            this.YPoint.Location = new System.Drawing.Point(734, 38);
            this.YPoint.Name = "YPoint";
            this.YPoint.Size = new System.Drawing.Size(32, 20);
            this.YPoint.TabIndex = 8;
            this.YPoint.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(642, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "x : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(709, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "y :";
            // 
            // graphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.YPoint);
            this.Controls.Add(this.XPoint);
            this.Controls.Add(this.VectorL);
            this.Controls.Add(this.PointRot);
            this.Controls.Add(this.AngleL);
            this.Controls.Add(this.AngleBox);
            this.Controls.Add(this.VectorsBox);
            this.Controls.Add(this.Rebuilding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AngleList);
            this.Controls.Add(this.chart);
            this.DoubleBuffered = true;
            this.Name = "graphWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Start);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox AngleList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Rebuilding;
        private System.Windows.Forms.ComboBox VectorsBox;
        private System.Windows.Forms.TextBox AngleBox;
        private System.Windows.Forms.Label AngleL;
        private System.Windows.Forms.Label VectorL;
        private System.Windows.Forms.Label PointRot;
        private System.Windows.Forms.TextBox XPoint;
        private System.Windows.Forms.TextBox YPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}