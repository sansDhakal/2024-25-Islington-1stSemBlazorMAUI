using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Models
{
    public class User
    {
        public int UserId { get; set; }
       
        public string UserName { get; set; }

        public string Password { get; set; }

        public string CurrencyType { get; set; }

        /*public double CurrentBalance { get; set; }*/

    }

}