using System;

namespace WindowsFormsApp1
{
	partial class Form1
	{
		/// <summary>
		///Gerekli tasarımcı değişkeni.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Kullanılan tüm kaynakları temizleyin.
		/// </summary>
		///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer üretilen kod

		/// <summary>
		/// Tasarımcı desteği için gerekli metot - bu metodun 
		///içeriğini kod düzenleyici ile değiştirmeyin.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.label1 = new System.Windows.Forms.Label();
			this.dosyaSecBtn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.populayonTxt = new System.Windows.Forms.TextBox();
			this.iterasyonTxt = new System.Windows.Forms.TextBox();
			this.mutasyonTxt = new System.Windows.Forms.TextBox();
			this.caprazlamaTxt = new System.Windows.Forms.TextBox();
			this.elitizmTxt = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.nwoxRadio = new System.Windows.Forms.RadioButton();
			this.ciftRadio = new System.Windows.Forms.RadioButton();
			this.tekRadio = new System.Windows.Forms.RadioButton();
			this.hesaplaBtn = new System.Windows.Forms.Button();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Dosya Adı";
			// 
			// dosyaSecBtn
			// 
			this.dosyaSecBtn.Location = new System.Drawing.Point(101, 10);
			this.dosyaSecBtn.Name = "dosyaSecBtn";
			this.dosyaSecBtn.Size = new System.Drawing.Size(75, 23);
			this.dosyaSecBtn.TabIndex = 1;
			this.dosyaSecBtn.Text = "Dosya Sec";
			this.dosyaSecBtn.UseVisualStyleBackColor = true;
			this.dosyaSecBtn.Click += new System.EventHandler(this.dosyaSecBtn_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(219, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Popülasyon Boyutu";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(223, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "İterasyon Sayısı";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(228, 73);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Mutasyon Oranı";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(228, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(93, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Çarprazlama Oranı";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(228, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Elitizm Oranı";
			// 
			// populayonTxt
			// 
			this.populayonTxt.Location = new System.Drawing.Point(339, 10);
			this.populayonTxt.Name = "populayonTxt";
			this.populayonTxt.Size = new System.Drawing.Size(100, 20);
			this.populayonTxt.TabIndex = 7;
			// 
			// iterasyonTxt
			// 
			this.iterasyonTxt.Location = new System.Drawing.Point(339, 42);
			this.iterasyonTxt.Name = "iterasyonTxt";
			this.iterasyonTxt.Size = new System.Drawing.Size(100, 20);
			this.iterasyonTxt.TabIndex = 8;
			// 
			// mutasyonTxt
			// 
			this.mutasyonTxt.Location = new System.Drawing.Point(339, 73);
			this.mutasyonTxt.Name = "mutasyonTxt";
			this.mutasyonTxt.Size = new System.Drawing.Size(100, 20);
			this.mutasyonTxt.TabIndex = 9;
			// 
			// caprazlamaTxt
			// 
			this.caprazlamaTxt.Location = new System.Drawing.Point(339, 100);
			this.caprazlamaTxt.Name = "caprazlamaTxt";
			this.caprazlamaTxt.Size = new System.Drawing.Size(100, 20);
			this.caprazlamaTxt.TabIndex = 10;
			// 
			// elitizmTxt
			// 
			this.elitizmTxt.Location = new System.Drawing.Point(339, 126);
			this.elitizmTxt.Name = "elitizmTxt";
			this.elitizmTxt.Size = new System.Drawing.Size(100, 20);
			this.elitizmTxt.TabIndex = 11;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.nwoxRadio);
			this.groupBox1.Controls.Add(this.ciftRadio);
			this.groupBox1.Controls.Add(this.tekRadio);
			this.groupBox1.Location = new System.Drawing.Point(12, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(164, 106);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Çarprazlama Seçeneği";
			// 
			// nwoxRadio
			// 
			this.nwoxRadio.AutoSize = true;
			this.nwoxRadio.Location = new System.Drawing.Point(12, 81);
			this.nwoxRadio.Name = "nwoxRadio";
			this.nwoxRadio.Size = new System.Drawing.Size(59, 17);
			this.nwoxRadio.TabIndex = 2;
			this.nwoxRadio.TabStop = true;
			this.nwoxRadio.Text = "NWOX";
			this.nwoxRadio.UseVisualStyleBackColor = true;
			// 
			// ciftRadio
			// 
			this.ciftRadio.AutoSize = true;
			this.ciftRadio.Location = new System.Drawing.Point(12, 53);
			this.ciftRadio.Name = "ciftRadio";
			this.ciftRadio.Size = new System.Drawing.Size(40, 17);
			this.ciftRadio.TabIndex = 1;
			this.ciftRadio.TabStop = true;
			this.ciftRadio.Text = "Çift";
			this.ciftRadio.UseVisualStyleBackColor = true;
			// 
			// tekRadio
			// 
			this.tekRadio.AutoSize = true;
			this.tekRadio.Location = new System.Drawing.Point(12, 26);
			this.tekRadio.Name = "tekRadio";
			this.tekRadio.Size = new System.Drawing.Size(44, 17);
			this.tekRadio.TabIndex = 0;
			this.tekRadio.TabStop = true;
			this.tekRadio.Text = "Tek";
			this.tekRadio.UseVisualStyleBackColor = true;
			// 
			// hesaplaBtn
			// 
			this.hesaplaBtn.Location = new System.Drawing.Point(518, 67);
			this.hesaplaBtn.Name = "hesaplaBtn";
			this.hesaplaBtn.Size = new System.Drawing.Size(125, 48);
			this.hesaplaBtn.TabIndex = 13;
			this.hesaplaBtn.Text = "HESAPLA";
			this.hesaplaBtn.UseVisualStyleBackColor = true;
			this.hesaplaBtn.Click += new System.EventHandler(this.hesaplaBtn_Click);
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(16, 191);
			this.chart1.Name = "chart1";
			this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series1.Legend = "Legend1";
			series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
			series1.Name = "Average";
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series2.Legend = "Legend1";
			series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
			series2.Name = "Best";
			this.chart1.Series.Add(series1);
			this.chart1.Series.Add(series2);
			this.chart1.Size = new System.Drawing.Size(627, 252);
			this.chart1.TabIndex = 14;
			this.chart1.Text = "chart1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 477);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.hesaplaBtn);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.elitizmTxt);
			this.Controls.Add(this.caprazlamaTxt);
			this.Controls.Add(this.mutasyonTxt);
			this.Controls.Add(this.iterasyonTxt);
			this.Controls.Add(this.populayonTxt);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dosyaSecBtn);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}



		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button dosyaSecBtn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox populayonTxt;
		private System.Windows.Forms.TextBox iterasyonTxt;
		private System.Windows.Forms.TextBox mutasyonTxt;
		private System.Windows.Forms.TextBox caprazlamaTxt;
		private System.Windows.Forms.TextBox elitizmTxt;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton nwoxRadio;
		private System.Windows.Forms.RadioButton ciftRadio;
		private System.Windows.Forms.RadioButton tekRadio;
		private System.Windows.Forms.Button hesaplaBtn;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
	}
}

