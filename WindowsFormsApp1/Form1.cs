using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        String PATH = "csv/SalesData.csv";
        Dictionary<string, Year> keyValuePairs = new Dictionary<string, Year>();
        Dictionary<string, int> topTenCountry = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
        }

        public void GetDict() // получаем dict с csv
        {
            using (TextFieldParser parser = new TextFieldParser(PATH))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadFields();
                while (!parser.EndOfData) // начинаем читать файл, первую строку пропускаем
                {
                    string[] fields = parser.ReadFields();
                    Year year = null;
                    if (!keyValuePairs.ContainsKey(fields[0])) // если данных нет, то закидываем их
                    {
                        year = new Year(int.Parse(fields[0]));
                        keyValuePairs[fields[0]] = year;
                    }
                    else
                        year = keyValuePairs[fields[0]];
                    Country country = year.IfAddCountry(int.Parse(fields[1]), fields[2]); // если город добавлен, второй раз это не делаем
                    City city = country.IfAddCity(int.Parse(fields[3]), fields[4], int.Parse(fields[5])); // аналогично
                }
            } // распарсим файл 
            comboBox1.Items.Clear();
            foreach (var id in keyValuePairs) // заполним комбо бокс
            {
                comboBox1.Items.Add(id.Key);

            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetDict();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* вся работа по изменению комбо бокса */
            int counter = 0;
            
            topTenCountry = keyValuePairs[comboBox1.SelectedItem.ToString()].TopTenCountry();
            label1.Text = "данные за " + comboBox1.SelectedItem.ToString() + " год" + "\n";
            label1.Text += keyValuePairs[comboBox1.SelectedItem.ToString()].ToString() + "\n";
            label4.Text = "Топ 10 стран за " + comboBox1.SelectedItem.ToString() + " год";
           
            dataGridView1.Rows.Clear();
            foreach(var item in topTenCountry)
            {
                if (counter++ < 10) // выводим только 10 значений
                {
                    dataGridView1.Rows.Add(item.Key, item.Value);
                }
                else break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            PATH = openFileDialog1.FileName;
            //if(comboBox1.SelectedItem != null)
            label1.Text = "данные за " + comboBox1.SelectedItem.ToString() + " год" + "\n";
            dataGridView1.Rows.Clear();
            keyValuePairs.Clear();
            GetDict();
        }
    }
}
