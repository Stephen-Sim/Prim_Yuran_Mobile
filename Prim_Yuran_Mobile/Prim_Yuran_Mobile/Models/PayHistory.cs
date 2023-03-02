using System;
using System.Collections.Generic;
using System.Text;

namespace Prim_Yuran_Mobile.Models
{
    public class PayHistory
    {
        public PayHistory(int id, string name, string desc, double amount, DateTime date)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.amount = "RM " + amount.ToString("0.00");
            this.date = date.ToString("dd/MM/yyyy");
        }
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string amount { get; set; }
        public string date { get; set; }
    }
}
