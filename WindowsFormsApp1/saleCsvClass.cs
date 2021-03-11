using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace WindowsFormsApp1
{
    class Sale
    {
        private int _year;
        private int _countryId;
        private String _countryName;
        private int _cityID;
        private String _cityName;
        private int _orderCount;
        public Sale(int _year, int _countryId, String _countryName, int _cityID, String _cityName, int _orderCount)
        {
            this._year = _year;
            this._countryId = _countryId;
            this._countryName = _countryName;
            this._cityID = _cityID;
            this._cityName = _cityName;
            this._orderCount = _orderCount;
        }
        public void Print()
        {
            Console.WriteLine(this._year + " " + this._countryName + " " + this._countryId + " " + this._cityID + " " + this._cityName + this._orderCount);
        }

        public int Year()
        {
            return this._year;
        }

        public int CountryId()
        {
            return this._countryId;
        }

        public string CountryName()
        {
            return this._countryName;
        }

        public int CityID()
        {
            return this._cityID;
        }

        public string CityName()
        {
            return this._cityName;
        }

        public int OrderCount()
        {
            return this._orderCount;
        }
    }

}
