using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public class New_Client : Client
    {
        public New_Client()
        {

        }
        public New_Client(string FirstName, string LastName, string PhoneNumber, string Address) : base(FirstName, LastName, PhoneNumber, Address)
        {
            
        }
    }
}