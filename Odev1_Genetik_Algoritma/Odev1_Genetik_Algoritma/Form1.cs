using Odev1_Genetik_Algoritma.CLASS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev1_Genetik_Algoritma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OleDbConnection conn;
        OleDbDataAdapter da;
        DataTable dt;
        
        Genetic gen = new Genetic();
        DataTable matrix = new DataTable();


        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\EdgeList.xlsx;Extended Properties='Excel 12.0 Xml; HDR = YES;IMEX=1;'");

            da = new OleDbDataAdapter("Select * from [Sayfa1$]", conn);

            dt = new DataTable();

            da.Fill(dt);

            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            gen.Population_Count = 6;
            gen.Modul_Count = 2;
            gen.edgeList_Count = dt.Rows.Count; 

            gen.NodeCount = getNodeCount(dt);  // Nod Sayısı Bul

            //Matris Oluştur

            for (int i = 0; i < gen.NodeCount; i++)
            {
                DataRow row = matrix.NewRow();
                for (int j = 0; j < gen.NodeCount; j++)
                {
                    matrix.Columns.Add();
                    row[j] = 0;
                }

                matrix.Rows.Add(row);
            }


            //Matrisi Doldur
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                matrix.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1][Convert.ToInt32(dt.Rows[i][1]) - 1] = 1;
                matrix.Rows[Convert.ToInt32(dt.Rows[i][1]) - 1][Convert.ToInt32(dt.Rows[i][0]) - 1] = 1;
            }


            // ilk Kromozomları rastgele oluştur

            List<ChromosomeInfo> population = new List<ChromosomeInfo>();
            Random rnd = new Random();

            for (int i = 0; i < gen.Population_Count; i++)
            {
                ChromosomeInfo s = new ChromosomeInfo();
                for (int j = 0; j < gen.NodeCount; j++)
                {
                    s.Chromosome.Add(rnd.Next(2));
                }

                population.Add(s);
            }
            
            gen.Population = population;


            //Fintness Fonk. Uygula
            for (int i = 0; i < gen.Population_Count; i++)
            {
                Apply_Fitness_Function(i);

                string lines = string.Join(Environment.NewLine, gen.Population[i].Chromosome);
                lb_Description.Items.Add(lines + "...............Q = " + gen.Population[i].Q.ToString());
            }

            int a = 0;
        }

        private void Apply_Fitness_Function(int chromosomeID)
        {
            double _q = 0;
            double top = 0;

            for (int i = 0; i < gen.NodeCount; i++)
            {
                for (int j = 0; j < gen.NodeCount; j++)
                {
                    _q = Convert.ToDouble(Convert.ToDouble(matrix.Rows[i][j]) - (Convert.ToDouble(get_Row_Degree(i) * get_Column_Degree(j)) / Convert.ToDouble((2 * gen.edgeList_Count)))) * get_Are_Same_Group(chromosomeID, i, j);
                    //lb_Description.Items.Add(_q + " +++ " + get_Are_Same_Group(chromosomeID, i, j));
                    top = top + _q;
                }
            }

            //lb_Description.Items.Add(Convert.ToDouble(1 / Convert.ToDouble(2 * gen.edgeList_Count)) + "........." + top);
            gen.Population[chromosomeID].Q = Convert.ToDouble(1 / Convert.ToDouble(2 * gen.edgeList_Count)) * top;
        }


        private int get_Row_Degree(int rowid)
        {
            int degreee = 0;

            for (int i = 0; i < gen.NodeCount; i++)
            {
                degreee = degreee + Convert.ToInt32(matrix.Rows[rowid][i]);
            }

            return degreee;
        }

        private int get_Column_Degree(int columnid)
        {
            int degreee = 0;

            for (int i = 0; i < gen.NodeCount; i++)
            {
                degreee = degreee + Convert.ToInt32(matrix.Rows[i][columnid]);
            }

            return degreee;
        }

        private int get_Are_Same_Group(int chromosomeID, int i, int j)
        {
            if (gen.Population[chromosomeID].Chromosome[i] == gen.Population[chromosomeID].Chromosome[j])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        } 

        private int getNodeCount(DataTable _dt)
        {
            int maxValue = 0;

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                for (int j = 0; j < _dt.Columns.Count; j++)
                {
                    if (Convert.ToInt32(_dt.Rows[i][j]) > maxValue)
                    {
                        maxValue = Convert.ToInt32(_dt.Rows[i][j]);
                    }
                }
            }

            return maxValue;
        }
        
    }
}
