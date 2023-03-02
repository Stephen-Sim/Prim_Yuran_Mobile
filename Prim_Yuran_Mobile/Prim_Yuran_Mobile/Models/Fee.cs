using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prim_Yuran_Mobile.Models
{
    public class Fee
    {
        public Fee(int id, string name, string category, double price, string student_id)
        {
            var user_id = Convert.ToInt16(Application.Current.Properties["user_id"]);
            if (category == "Kategory A")
            {
                this.id = $"{user_id}-{id}";
            }
            else
            {
                this.id = $"{student_id}-{id}";
            }
            this.name = name;
            this.category = category;
            this.price = "RM " + price.ToString("0.00");
        }

        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string price { get; set; }
    }
}
