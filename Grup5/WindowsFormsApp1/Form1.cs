using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
	
        private String Namee;
        private String Comment;
        private String Type;
        private String Dimension;
        private String Edge;
        private String[,] sehirler;
        private String[,] populasyon;
        private String[,] yavru_populasyon;
        private String[,] parents;
        private int yavru_index = 0;
        private int populasyon_boyutu = 0;
        private int iterasyon_sayisi = 0;
        private double mutasyon_orani = 0.0;
        private double caprazlama_orani = 0.0;
        private double elitizm_orani = 0.0;
        private int elitizm_sayisi;
		public int sehir_sayisi;
		public double[] best;
		public double[] ortalama;
		public string TourSection;
		public double bestOfBest;

		Random random = new Random();

		private double Distance(int a, int b, int islem)
        {
            double sonuc = 0.0;
            int ax = Convert.ToInt32(sehirler[a, 0]);
            int ay = Convert.ToInt32(sehirler[a, 1]);
            int bx = Convert.ToInt32(sehirler[b, 0]);
            int by = Convert.ToInt32(sehirler[b, 1]);
            if (islem == 1) //Euc
            {
                sonuc = Math.Sqrt(Math.Pow((ax - bx), 2) + Math.Pow((ay - by), 2));

            }
            else if (islem == 2)   //Max
            {
                sonuc = Math.Max(Math.Abs(ax - bx), Math.Abs(ay - by));

            }
            else if (islem == 3)   //Man
            {
                sonuc = Math.Abs(ax - bx) + Math.Abs(ay - by);
            }
            return sonuc;
        }

		private void hesaplaBtn_Click(object sender, EventArgs e)
		{
			

			populasyon_boyutu = Convert.ToInt32(populayonTxt.Text);
			iterasyon_sayisi = Convert.ToInt32(iterasyonTxt.Text);
			mutasyon_orani = Convert.ToDouble(mutasyonTxt.Text);
			caprazlama_orani = Convert.ToDouble(caprazlamaTxt.Text);
			elitizm_orani = Convert.ToDouble(elitizmTxt.Text);
			elitizm_sayisi = Convert.ToInt32(populasyon_boyutu * elitizm_orani);
			populasyon = new String[populasyon_boyutu, 2];
			yavru_populasyon = new String[populasyon_boyutu, 2];
			parents = new String[populasyon_boyutu, 2];
			best = new double[iterasyon_sayisi+1];
			ortalama = new double[iterasyon_sayisi+1];

			String d = Application.StartupPath + "\\son.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);


			populasyonOlustur();
			populasyonSirala(populasyon);
			best[0] = Convert.ToDouble(populasyon[0, 1]);
			ortalama[0] = popOrtalama();
			double min = best[0];
			for (int i = 0; i < iterasyon_sayisi; i++)
			{
				populasyonSirala(populasyon);
				elitizm();
				turnuva();
				mutasyon();
				populasyonSirala(populasyon);


				if (Convert.ToDouble(populasyon[0, 1]) <= min)
				{
					TourSection = populasyon[0, 0];
				}

				populasyon = yavru_populasyon;

				best[i+1] = Convert.ToDouble(populasyon[0, 1]);
				
				ortalama[i+1] = popOrtalama();

				yavru_index = 0;
			}

			grafik();

			String da = Application.StartupPath + "\\PROJE_TOUR.tour";
			FileStream fs2 = new FileStream(da, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw2 = new StreamWriter(fs2);

			sw2.WriteLine("NAME: " + Namee);
			sw2.WriteLine("COMMENT: " + best.Min());
			sw2.WriteLine("TYPE: " + Type);
			sw2.WriteLine("DIMENSION: " + Dimension);
			sw2.WriteLine("TOUR SECTION: " + TourSection);
			for (int i = 0; i < iterasyon_sayisi; i++)
			{
				sw2.WriteLine((i + 1) + "  " + best[i]);

			}
			sw2.Flush();
			sw2.Close();
			fs2.Close();


			for (int i = 0; i <= iterasyon_sayisi; i++)
			{
				sw.WriteLine(best[i]);

			}
			sw.Flush();
			sw.Close();
			fs.Close();

		}
		private void turnuva()
		{
			
				String d = Application.StartupPath + "\\turnuva.txt";
				FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
				StreamWriter sw = new StreamWriter(fs);
			
				int[] kazananlar = new int[populasyon_boyutu - elitizm_sayisi];


				int p = 0;
				int k = 0;
				int child_count = populasyon_boyutu - elitizm_sayisi;
				while (child_count != 0)
				{
					sw.WriteLine("child count : " + child_count);
					if (child_count >= 4)
					{
						while (p < 2)
						{
							sw.WriteLine("PARENT -" + (p + 1));
							int[] dizi = Enumerable.Range(elitizm_sayisi, (populasyon_boyutu - elitizm_sayisi)).OrderBy(c => Guid.NewGuid()).Except(kazananlar).Take(4).ToArray();


							kazananlar[k] = dizi.Min();
							sw.WriteLine("kazananlar[" + k + "] =" + kazananlar[k]);
							parents[p, 0] = populasyon[kazananlar[k], 0];
							parents[p, 1] = populasyon[kazananlar[k], 1];
							sw.WriteLine(parents[p, 0] + "*" + parents[p, 1]);
							p++;
							k++;


						}
						caprazlama(parents);
						child_count -= 2;
						p = 0;
						k = 0;
						
					}
					else if (child_count == 3)
					{
						int[] dizi = Enumerable.Range(elitizm_sayisi, populasyon_boyutu - elitizm_sayisi).Except(kazananlar).ToArray();

						Array.Sort(dizi);
						parents[0, 0] = populasyon[dizi[0], 0];
						parents[0, 1] = populasyon[dizi[0], 1];
						parents[1, 0] = populasyon[dizi[1], 0];
						parents[1, 1] = populasyon[dizi[1], 1];

						sw.WriteLine(parents[0, 0] + "*" + parents[0, 1]);
						sw.WriteLine(parents[1, 0] + "*" + parents[1, 1]);

						caprazlama(parents);

						yavru_populasyon[populasyon_boyutu - 1, 0] = populasyon[dizi[2], 0];
						yavru_populasyon[populasyon_boyutu - 1, 1] = populasyon[dizi[2], 1];



						child_count = 0;
					}
					else if (child_count == 2)
					{
						int[] dizi = Enumerable.Range(elitizm_sayisi, populasyon_boyutu - elitizm_sayisi).Except(kazananlar).ToArray();

						parents[0, 0] = populasyon[dizi[0], 0];
						parents[0, 1] = populasyon[dizi[0], 1];
						parents[1, 0] = populasyon[dizi[1], 0];
						parents[1, 1] = populasyon[dizi[1], 1];

						sw.WriteLine(parents[0, 0] + "*" + parents[0, 1]);
						sw.WriteLine(parents[1, 0] + "*" + parents[1, 1]);

						caprazlama(parents);

						child_count = 0;
						k = 0;
				}
					else if (child_count == 1)
					{
						int[] dizi = Enumerable.Range(elitizm_sayisi, populasyon_boyutu - elitizm_sayisi).Except(kazananlar).ToArray();
						yavru_populasyon[populasyon_boyutu - 1, 0] = populasyon[dizi[0], 0];
						yavru_populasyon[populasyon_boyutu - 1, 1] = populasyon[dizi[0], 1];

						sw.WriteLine(yavru_populasyon[populasyon_boyutu - 1, 0]);


						child_count = 0;
						k = 0;
				}
			

			}
				sw.Flush();
				sw.Close();
				fs.Close();

			}

		private void elitizm()
		{
			String d = Application.StartupPath + "\\elitizim.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);

			for (int i = 0; i < elitizm_sayisi; i++)
			{
				yavru_populasyon[i, 0] = populasyon[i, 0];
				yavru_populasyon[i, 1] = populasyon[i, 1];

				sw.WriteLine(yavru_populasyon[i, 1]);
				sw.WriteLine(yavru_populasyon[i, 0]);


			}
			sw.Flush();
			sw.Close();
			fs.Close();


		}


		private void populasyonOlustur()
        {	
			String d = Application.StartupPath + "\\popOlustur.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);

			int[] dizi = new int[sehir_sayisi];

			for (int i = 0; i < populasyon_boyutu; i++)
            {
				
				
				 dizi = Enumerable.Range(0, sehir_sayisi).OrderBy(c => Guid.NewGuid()).ToArray();
               
                int x = 0;
                int x0 =dizi[0];
                int baslangic = x0;
                populasyon[i, 0] = dizi[0].ToString();
                double sonuc = 0;

				int j;
                for (j = 1; j < dizi.Length; j++)
                {
                    populasyon[i, 0] += "-" + dizi[j];
                    x = dizi[j];


					switch (Edge)
					{
						case "EUC_2D":

							sonuc += Distance(x0, x, 1);
							x0 = x;

							break;
						case "MAX_2D":

							sonuc += Distance(x0, x, 2);
							x0 = x;
							break;
						case "MAN_2D":
							sonuc += Distance(x0, x, 3);
							x0 = x;
							break;
						case "CEIL_2D":
							sonuc += Distance(x0, x, 1);
							sonuc = Math.Ceiling(sonuc);
							x0 = x;
							break;
						default: MessageBox.Show("Geçersiz uygunluk"); break;
					}


				}
				
				if (Edge == "EUC_2D") sonuc += Distance(baslangic, x, 1);
				if (Edge == "MAX_2D") sonuc += Distance(baslangic, x, 2);
				if (Edge == "MAN_2D") sonuc += Distance(baslangic, x, 3);
				if (Edge == "CEIL_2D")
				{
					sonuc += Distance(baslangic, x, 1);
					sonuc = Math.Ceiling(sonuc);
				}

				populasyon[i, 1] = sonuc.ToString();
				sw.WriteLine(populasyon[i, 0]);
				sw.WriteLine(populasyon[i, 1]);

			}
			sw.Flush();
			sw.Close();
			fs.Close();

		}

        private void populasyonSirala(String[,] dizi) //selectsort
        {
			String d = Application.StartupPath + "\\siralipop.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine("sonuclar");
			int min=0;
			String temp1="";
			String temp0="";
			for (int i = 0; i < populasyon_boyutu; i++)
			{
				min = i;
				for (int j = i + 1; j < populasyon_boyutu; j++)
				{
					if (Convert.ToDouble(dizi[j, 1]) < Convert.ToDouble(dizi[min, 1]))
					{
						min = j;
					}
					temp0 = dizi[i, 0];
					dizi[i, 0] = dizi[min, 0];
					dizi[min, 0] = temp0;

					temp1 = dizi[i, 1];
					dizi[i, 1] = dizi[min, 1];
					dizi[min, 1] = temp1;
				}

			}
			for (int k = 0; k < populasyon_boyutu; k++)
			{
				sw.WriteLine(populasyon[k, 1]);
				sw.WriteLine(populasyon[k, 0]);
			}

			sw.Flush();
			sw.Close();
			fs.Close();

		}

		private void caprazlama(String[,] parentdizi)
        {
			String d =Application.StartupPath + "\\çaprazlama.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);

			if (random.NextDouble() <= caprazlama_orani)
			{

				if (tekRadio.Checked == true)
				{
					//Tek noktalı

					double c1_sonuc = 0, c2_sonuc = 0.0;
					String child1 = "";
					String child2 = "";
					int i, j = 0, k = 0;
					int nokta = random.Next(1, sehir_sayisi - 1);
					string[] p1 = new string[sehir_sayisi];
					 p1 = parentdizi[0, 0].Split('-');
					string[] p2 = new string[sehir_sayisi];
					 p2 = parentdizi[1, 0].Split('-');
					string[] c1 = new string[p1.Length];
					string[] c2 = new string[p2.Length];


					for (i = 0; i < nokta; i++)
					{
						c1[i] = p1[i];
						c2[i] = p2[i];
					}
					for (i = nokta; i < sehir_sayisi; i++)
					{
						while (Array.Exists(c1, element => element == p2[j])) { j++; }
						c1[i] = p2[j];

						while (Array.Exists(c2, element => element == p1[k])) { k++; }
						c2[i] = p1[k];
					}

					foreach (string x in c1)
					{
						child1 += x + "-";
					}
					foreach (string x in c2)
					{
						child2 += x + "-";
					}

					child1 = child1.Substring(0, child1.Length - 1);
					child2 = child2.Substring(0, child2.Length - 1);

					for (i = 0; i < c1.Length - 1; i++)
					{
						switch (Edge)
						{
							case "EUC_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								break;
							case "MAX_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 2);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 2);
								break;
							case "MAN_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 3);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 3);
								break;
							case "CEIL_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								c1_sonuc = Math.Ceiling(c1_sonuc);
								c2_sonuc = Math.Ceiling(c2_sonuc);
								break;
							default: MessageBox.Show("Geçersiz uygunluk"); break;
						}

					}
					if (Edge == "EUC_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length-1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);

					}

					if (Edge == "MAX_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 2);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 2);

					}
					if (Edge == "MAN_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 3);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 3);

					}
					if (Edge == "CEIL_2D")
					{
						
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);
						
						c1_sonuc = Math.Ceiling(c1_sonuc);
						c2_sonuc = Math.Ceiling(c2_sonuc);
					}

					yavru_populasyon[yavru_index, 0] = child1;
					yavru_populasyon[yavru_index, 1] = c1_sonuc.ToString();
					yavru_index++;
					yavru_populasyon[yavru_index, 0] = child2;
					yavru_populasyon[yavru_index, 1] = c2_sonuc.ToString();
					yavru_index++;

					for (int x = 0; x < 2; x++)
					{
						//sw.WriteLine(parents[x, 0] + "----" + parents[k, 1]);
						sw.WriteLine(yavru_populasyon[x, 0] + "----" + yavru_populasyon[x, 1]);
					}
				}

				else if (ciftRadio.Checked == true)
				{
					//Çift Noktalı

					String child1 = "";
					String child2 = "";
					double c1_sonuc = 0, c2_sonuc = 0.0;
					int nokta1 = random.Next(1, sehir_sayisi - 2);
					int nokta2 = random.Next(nokta1, sehir_sayisi - 1);
					int i, j = nokta1, k = nokta1;
					string[] p1 = new string[sehir_sayisi];
					p1 = parentdizi[0, 0].Split('-');
					string[] p2 = new string[sehir_sayisi];
				    p2 = parentdizi[1, 0].Split('-');
					string[] c1 = new string[p1.Length];
					string[] c2 = new string[p2.Length];


					for (i = nokta1; i < nokta2; i++)
					{
						c1[i] = p1[i];
						c2[i] = p2[i];
					}


					for (i = 0; i < nokta1; i++)
					{

						if (!Array.Exists(c1, element => element == p2[i]))
						{
							c1[i] = p2[i];

						}
						else
						{

							while (Array.Exists(c1, element => element == p2[j]))
							{
								j++;
							}

							c1[i] = p2[j];
						}


						if (!Array.Exists(c2, element => element == p1[i]))
						{
							c2[i] = p1[i];
						}
						else
						{
							while (Array.Exists(c2, element => element == p1[k]))
							{ k++; }
							c2[i] = p1[k];
						}

					}

					for (i = nokta2; i < sehir_sayisi; i++)
					{

						if (!Array.Exists(c1, element => element == p2[i]))
						{
							c1[i] = p2[i];
						}
						else
						{
							while (Array.Exists(c1, element => element == p2[j])) { j++; }
							c1[i] = p2[j];
						}

						if (!Array.Exists(c2, element => element == p1[i]))
						{
							c2[i] = p1[i];
						}
						else
						{
							while (Array.Exists(c2, element => element == p1[k])) { k++; }
							c2[i] = p1[k];
						}

					}

					foreach (string x in c1)
					{
						child1 += x + "-";
					}
					foreach (string x in c2)
					{
						child2 += x + "-";
					}

					child1 = child1.Substring(0, child1.Length - 1);
					child2 = child2.Substring(0, child2.Length - 1);

					for (i = 0; i < c1.Length - 1; i++)
					{
						switch (Edge)
						{
							case "EUC_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								break;
							case "MAX_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 2);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 2);
								break;
							case "MAN_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 3);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 3);
								break;
							case "CEIL_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								c1_sonuc = Math.Ceiling(c1_sonuc);
								c2_sonuc = Math.Ceiling(c2_sonuc);
								break;
							default: MessageBox.Show("Geçersiz uygunluk"); break;
						}
					}
					if (Edge == "EUC_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);

					}

					if (Edge == "MAX_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 2);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 2);

					}
					if (Edge == "MAN_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 3);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 3);

					}
					if (Edge == "CEIL_2D")
					{

						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);

						c1_sonuc = Math.Ceiling(c1_sonuc);
						c2_sonuc = Math.Ceiling(c2_sonuc);
					}
					yavru_populasyon[yavru_index, 0] = child1;
					yavru_populasyon[yavru_index, 1] = Convert.ToString(c1_sonuc);
					yavru_index++;
					yavru_populasyon[yavru_index, 0] = child2;
					yavru_populasyon[yavru_index, 1] = Convert.ToString(c2_sonuc);
					yavru_index++;

					for (int m = 0; m < 2; m++)
					{
						sw.WriteLine(yavru_populasyon[m, 0] + "---" + yavru_populasyon[m, 1]);
					}

				}

				else if (nwoxRadio.Checked == true)
				{
					//NWOX

					double c1_sonuc = 0, c2_sonuc = 0.0;
					String child1 = "";
					String child2 = "";
					int i, j;
					int nokta1 = random.Next(1, sehir_sayisi - 4);
					int nokta2 = random.Next(nokta1, sehir_sayisi - 2);

					string[] p1 = new string[sehir_sayisi];
					p1 = parentdizi[0, 0].Split('-');
					string[] p2 = new string[sehir_sayisi];
					p2 = parentdizi[1, 0].Split('-');
					string[] c1 = new string[p1.Length];
					string[] c2 = new string[p2.Length];




					for (i = nokta1; i < nokta2; i++)
					{
						c1[i] = p2[i];
						c2[i] = p1[i];
					}

					//cocuk1
					j = 0;
					for (i = 0; i < nokta1; i++)
					{
						if (!Array.Exists(c1, element => element == p1[i]))
						{
							c1[j] = p1[i];
							j++;
						}
					}
					i = 0;
					while (j < nokta1)
					{
						if (!Array.Exists(c1, element => element == p1[nokta1 + i]))
						{
							c1[j] = p1[nokta1 + i];
							j++;
						}
						i++;
					}
					j = 0;
					while (nokta1 + i < nokta2)
					{
						if (!Array.Exists(c1, element => element == p1[nokta1 + i]))
						{
							c1[nokta2 + j] = p1[nokta1 + i];
							j++;
						}
						i++;
					}
					i = nokta2;
					while (nokta2 + j != p1.Length)
					{
						if (!Array.Exists(c1, element => element == p1[i]))
						{
							c1[nokta2 + j] = p1[i];
							j++;
						}
						i++;
					}
					foreach (string x in c1)
					{
						child1 += x + "-";
					}
					child1 = child1.Substring(0, child1.Length - 1);



					//cocuk2
					j = 0;
					for (i = 0; i < nokta1; i++)
					{
						if (!Array.Exists(c2, element => element == p2[i]))
						{
							c2[j] = p2[i];
							j++;
						}
					}
					i = 0;
					while (j < nokta1)
					{
						if (!Array.Exists(c2, element => element == p2[nokta1 + i]))
						{
							c2[j] = p2[nokta1 + i];
							j++;
						}
						i++;
					}
					j = 0;
					while (nokta1 + i < nokta2)
					{
						if (!Array.Exists(c2, element => element == p2[nokta1 + i]))
						{
							c2[nokta2 + j] = p2[nokta1 + i];
							j++;
						}
						i++;
					}
					i = nokta2;
					while (nokta2 + j != p1.Length)
					{
						if (!Array.Exists(c2, element => element == p2[i]))
						{
							c2[nokta2 + j] = p2[i];
							j++;
						}
						i++;
					}
					foreach (string x in c2)
					{
						child2 += x + "-";
					}
					child2 = child2.Substring(0, child2.Length - 1);

					for (i = 0; i < sehir_sayisi - 1; i++)
					{
						switch (Edge)
						{
							case "EUC_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								break;
							case "MAX_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 2);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 2);
								break;
							case "MAN_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 3);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 3);
								break;
							case "CEIL_2D":
								c1_sonuc += Distance(Convert.ToInt32(c1[i]), Convert.ToInt32(c1[i + 1]), 1);
								c2_sonuc += Distance(Convert.ToInt32(c2[i]), Convert.ToInt32(c2[i + 1]), 1);
								c1_sonuc = Math.Ceiling(c1_sonuc);
								c2_sonuc = Math.Ceiling(c2_sonuc);
								break;
							default: MessageBox.Show("Geçersiz uygunluk"); break;
						}
					}
					if (Edge == "EUC_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);

					}

					if (Edge == "MAX_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 2);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 2);

					}
					if (Edge == "MAN_2D")
					{
						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 3);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 3);

					}
					if (Edge == "CEIL_2D")
					{

						c1_sonuc += Distance(Convert.ToInt32(c1[0]), Convert.ToInt32(c1[c1.Length - 1]), 1);
						c2_sonuc += Distance(Convert.ToInt32(c2[0]), Convert.ToInt32(c2[c2.Length - 1]), 1);

						c1_sonuc = Math.Ceiling(c1_sonuc);
						c2_sonuc = Math.Ceiling(c2_sonuc);
					}

					yavru_populasyon[yavru_index, 0] = child1;
					yavru_populasyon[yavru_index, 1] = Convert.ToString(c1_sonuc);
					yavru_index++;
					yavru_populasyon[yavru_index, 0] = child2;
					yavru_populasyon[yavru_index, 1] = Convert.ToString(c2_sonuc);
					yavru_index++;
					for (int x = 0; x < 2; x++)
					{
						//sw.WriteLine(parents[x, 0] + "----" + parents[k, 1]);
						sw.WriteLine(yavru_populasyon[x, 0] + "----" + yavru_populasyon[x, 1]);
					}

				}
			}
			else {

				yavru_populasyon[yavru_index, 0] = parentdizi[0, 0];
				yavru_populasyon[yavru_index, 1] = parentdizi[0, 1];
				yavru_index++;
				yavru_populasyon[yavru_index, 0] = parentdizi[1, 0];
				yavru_populasyon[yavru_index, 1] = parentdizi[1, 1];
				yavru_index++;
			}
			sw.Flush();
			sw.Close();
			fs.Close();
		}

        private void mutasyon()
        {
			String d = Application.StartupPath + "\\mutasyon.txt";
			FileStream fs = new FileStream(d, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);


			for (int i = 0; i <populasyon_boyutu; i++)
            {
                if (random.NextDouble() <= mutasyon_orani)
                {
                    int[] m_point = Enumerable.Range(0, sehir_sayisi).OrderBy(c => Guid.NewGuid()).ToArray();
                    String[] yavru = yavru_populasyon[i, 0].Split('-');
                    String temp = yavru[m_point[0]];
                    yavru[m_point[0]] = yavru[m_point[1]];
                    yavru[m_point[1]] = temp;
                   
                    int x = 0;
                    int x0 = Convert.ToInt32(yavru[0]);
                    int baslangic = x0;
                    yavru_populasyon[i, 0] = yavru[0];
                    double sonuc = 0;
                    for (int a=1; a<sehirler.Length/2; a++)
                    {
                        yavru_populasyon[i, 0] +="-"+yavru[a];
                        x = Convert.ToInt32(yavru[a]);
                        switch (Edge)
                        {
                            case "EUC_2D":

                                sonuc += Distance(x0, x, 1);
                                x0 = x;

                                break;
                            case "MAX_2D":

                                sonuc += Distance(x0, x, 2);
                                x0 = x;
                                break;
                            case "MAN_2D":
                                sonuc += Distance(x0, x, 3);
                                x0 = x;
                                break;
                            case "CEIL_2D":
                                sonuc += Distance(x0, x, 1);
                                sonuc = Math.Ceiling(sonuc);
                                x0 = x;
                                break;
                            default: MessageBox.Show("Geçersiz uygunluk"); break;
                        }


                    }
                    if (Edge == "EUC_2D") sonuc += Distance(baslangic, x, 1);
                    if (Edge == "MAX_2D") sonuc += Distance(baslangic, x, 2);
                    if (Edge == "MAN_2D") sonuc += Distance(baslangic, x, 3);
                    if (Edge == "CEIL_2D")
                    {
                        sonuc += Distance(baslangic, x, 1);
                        sonuc = Math.Ceiling(sonuc);
                    }
                    yavru_populasyon[i, 1] = sonuc.ToString();
					
                }
				sw.WriteLine(yavru_populasyon[i, 0]);
			}

			sw.Flush();
			sw.Close();
			fs.Close();


		}
            
        private void grafik()
        {
			Double min_deger = best[0];
			chart1.Series[1].Points.AddXY(1, best[0]);
			for (int i = 1; i < iterasyon_sayisi; i++)
			{
				chart1.Series[0].Points.AddXY(i , ortalama[i]);

				if (best[i] < min_deger)
				{
					chart1.Series[1].Points.AddXY(i,best[i]);
					min_deger = best[i];
				}
				else
				{
					chart1.Series[1].Points.AddXY(i, min_deger);
				}
			
			}

        }

		private double popOrtalama()
		{
			double ort = 0;
			for (int i = 0; i < populasyon_boyutu; i++)
			{
				ort += Convert.ToDouble(populasyon[i, 1]);
				
			}
			return (ort / populasyon_boyutu);
		}

        private void dosyaSecBtn_Click(object sender, EventArgs e)
        {
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "TSP Dosyası |*.tsp";
			file.ShowDialog();
			string dosya_yolu = file.FileName;
			label1.Text = Path.GetFileName(dosya_yolu);
			FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
			StreamReader sw = new StreamReader(fs);

			int startIndex = 0;
			int endIndex = 0;

			string[] yazi = File.ReadAllLines(dosya_yolu);
			string[] line;

			for (int i = 0; i < 5; i++)
			{
				line = yazi[i].Replace(" ", "").Split(':');

				switch (line[0])
				{
					case "NAME":
						Namee = line[1].ToString();
						break;

					case "COMMENT":
						Comment = line[1].ToString();
						break;

					case "TYPE":
						if (String.Equals(line[1], "TSP").ToString() == "True") { Type = line[1].ToString(); }
						else MessageBox.Show("TYPE 'TSP' DEĞİL!");
						break;

					case "DIMENSION":
						Dimension = line[1].ToString();
						break;

					case "EDGE_WEIGHT_TYPE":
						if (String.Equals(line[1], "EUC_2D").ToString() == "True") { Edge = line[1].ToString(); }
						else if (String.Equals(line[1], "MAX_2D").ToString() == "True") { Edge = line[1].ToString(); }
						else if (String.Equals(line[1], "MAN_2D").ToString() == "True") { Edge = line[1].ToString(); }
						else if (String.Equals(line[1], "CEIL_2D").ToString() == "True") { Edge = line[1].ToString(); }
						else MessageBox.Show("EDGE_WEIGHT_TYPE FARKLI");
						break;

					default:
						MessageBox.Show("HATA!!");
						break;
				}
			}


			for (int i = 5; i < yazi.Length; i++)
			{
				if (yazi[i].Trim() == "NODE_COORD_SECTION")
				{

					startIndex = i + 1;

				}
				if (yazi[i].Trim() == "EOF")
				{

					endIndex = i - 1;
				}

			}

			sehirler = new String[endIndex - startIndex+1 , 2];    
			for (int i = startIndex; i <= endIndex; i++)
			{

				string[] lines = yazi[i].Split(' ').Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

				sehirler[i - startIndex, 0] = lines[1];
				sehirler[i - startIndex, 1] = lines[2];

			}

			sehir_sayisi = endIndex - startIndex + 1;

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}

       

	
}

                    

 