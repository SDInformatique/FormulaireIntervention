using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FormulaireIntervention.Models;

namespace FormulaireIntervention.Models
{
    public class Intervenant
    {
        private string firstName;
        private string lastName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public IEnumerable<Intervenant> GetIntervenants()
        {
            var DB = new DBConnection();
            List<Intervenant> listIntervenant = DB.GetListOfIntervenant();

            return listIntervenant;
        }


    }
    

}