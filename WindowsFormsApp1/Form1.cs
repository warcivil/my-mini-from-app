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
        
        Dictionary<string, Year> csvData = new Dictionary<string, Year>();
        Dictionary<string, int> topTenCountry = new Dictionary<string, int>();
       
        public Form1()
        {
            InitializeComponent();
        }

        public void GetDataGrid() // заполняем новыми данными
        {
            int counter = 0;

            /* подчищаем наши таблицы после выбора нового года */
            dataGridView1.Rows.Clear();
            dataGriedView.Rows.Clear();
            foreach (var item in topTenCountry)
            {
                if (counter++ < 10) // выводим только 10 значений
                {
                    dataGridView1.Rows.Add(item.Key, item.Value);
                }
                else break;
            }
            foreach (var item in csvData[yearComboBox.SelectedItem.ToString()].GetCountriesDict())
            {
                dataGriedView.Rows.Add(item.Value.ToString(), item.Value.sumord);
            }
        }

        public void GetDict() // получаем dict с csv
        {
            using (TextFieldParser parser = new TextFieldParser(PATH))
            {
                /*
                 * field[0] => Year
                 * field[1] => CountryID
                 * field[2] => CountryName
                 * field[3] => CityID
                 * field[4] => CityName
                 * field[5] => OrderCount
                 */
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadFields();
                while (!parser.EndOfData) // начинаем читать файл, первую строку пропускаем
                {
                    string[] fields = parser.ReadFields();
                    Year year = null;
                    if (!csvData.ContainsKey(fields[0])) // если данных нет, то закидываем их
                    {
                        year = new Year(int.Parse(fields[0]));
                        csvData[fields[0]] = year;
                    }
                    else
                        year = csvData[fields[0]];
                    Country country = year.IfAddCountry(int.Parse(fields[1]), fields[2]); // если город добавлен, второй раз это не делаем
                    City city = country.IfAddCity(int.Parse(fields[3]), fields[4], int.Parse(fields[5])); // аналогично
                }
            } // распарсим файл, получим хорошо структуриваный словарь с данными с csv

            yearComboBox.Items.Clear(); // подчистим комбо бокс
            foreach (var id in csvData) // заполним комбо бокс
            {
                yearComboBox.Items.Add(id.Key);
            }
            yearComboBox.SelectedIndex = 0; // ставим выбор на первый элемент комбо бокса
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetDict();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* вся работа по изменению комбо бокса */
            topTenCountry = csvData[yearComboBox.SelectedItem.ToString()].TopTenCountry();
            dataLabel.Text = "данные за " + yearComboBox.SelectedItem.ToString() + " год" + "\n";
            GetDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openMyCsv.FileName = "SalesData.csv";
            openMyCsv.DefaultExt = ".csv";
            openMyCsv.Filter= "Файл формата  .csv|*.csv";

            if (openMyCsv.ShowDialog() == DialogResult.OK)
                PATH = openMyCsv.FileName;

            dataLabel.Text = "данные за " + yearComboBox.SelectedItem.ToString() + " год" + "\n";
            
            /* подчищаем наш вывод */
            dataGridView1.Rows.Clear(); 
            csvData.Clear();
            
            GetDict();
        }
    }
}
