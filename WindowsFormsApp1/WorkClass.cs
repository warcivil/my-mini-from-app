using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace WindowsFormsApp1
{
    /*
     * Набор классов для структурирования csv файла
     */
    class City
    {
        int _id;
        string _name;
        int _order;


        public City(int cid, string name, int ord)
        {
            this._id = cid;
            this._name = name;
            this._order = ord;
        }

        public int GetOrd() // получение списка заказов
        {
            return this._order;
        }

    }
    class Country
    {
        string _name;
        int _id;
        public int sumord {get; private set;}
        Dictionary<int, City>  keyValuePairs = new  Dictionary<int, City>();

        public Country(int cid, string name)
        {
            this._id = cid;
            this._name = name;
        }

        public City IfAddCity(int cid, string name, int ord) 
        {
            sumord += ord; // подсчет количества заказов со страны
            City city;
            if (!keyValuePairs.ContainsKey(cid))
            {
                city = new City(cid, name, ord);
                keyValuePairs[cid] = city;
            }
            else
                city = keyValuePairs[cid];
            return city;
        }
        public  string ToString()
        {
            return _name;
        }
    }
    class Year
    {
        public int year;
        Dictionary<int,Country> countries;
        public Year(int year)
        {
            this.year = year;
            countries = new Dictionary<int, Country>();
        }
        public Country IfAddCountry(int cid,string name)
        {
            Country country;
            if (!countries.ContainsKey(cid))
            {
                country = new Country(cid,name);
                countries[cid] = country;
            }
            else
            {
                country= countries[cid];
            }
             
            return country;
        }
        public Dictionary<string, int> TopTenCountry()
        {
            string txt = "";
            Dictionary<string, int> country = new Dictionary<string, int>();
            foreach (var item in countries)
            {
                country.Add(key: item.Value.ToString(), value: item.Value.sumord);
            }
            return country.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); ;

        }
        public Dictionary<int, Country> GetCountriesDict()
        {
            return countries;
        }
    }
}
