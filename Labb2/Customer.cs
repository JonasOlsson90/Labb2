﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Customer
    {
        public string Name { get; private protected set; }
        public string Password { get; private protected set; }
        public List<Item> Chart { get; private protected set; }
        private protected bool IsLoggedIn { get; set; }

        private protected Customer()
        {

        }

        public Customer(string name, string password)
        {
            // Set name and password
            Name = name;
            Password = password;
            Chart = new List<Item>();
        }

        public void LoggIn(string name, string password)
        {
            if (name == Name && password == Password)
                IsLoggedIn = true;
        }

        public void LoggOut()
        {
            if (IsLoggedIn)
                IsLoggedIn = false;
            else
                Console.WriteLine("You are logged out");
        }

        public void AddToChart(Item item)
        {
            // Add item to chart
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}