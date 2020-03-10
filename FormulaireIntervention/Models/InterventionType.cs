using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public class InterventionType
    {
        private string interventionType;
        public string Type
        {
            get { return interventionType; }
            set { interventionType = value; }
        }

        public IEnumerable<InterventionType> GetInterventionTypes()
        {
            var DB = new DBConnection();
            List<InterventionType> listInterventionTypes = DB.GetListOfInterventionType();
            return listInterventionTypes;
        }
    }
}