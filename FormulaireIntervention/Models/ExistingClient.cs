using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public class ExistingClient : Client
    {
        public IEnumerable<ExistingClient> GetClientsList()
        {
            DBConnection DB = new DBConnection();
            List<ExistingClient> listExistingClient = DB.GetListOfClients();
            return listExistingClient;
        }
    }
}