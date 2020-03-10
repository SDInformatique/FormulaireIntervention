using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public class Verifications
    {
        public List<string> VerifyClientData(int clientID,List<string> listOfAllInfo)
        {

            var clientFirstName = listOfAllInfo[0];
            var clientLastName = listOfAllInfo[1];
            var clientAddress = listOfAllInfo[2];
            var clientPhoneNumber = listOfAllInfo[3];

            var DB = new DBConnection();
            var clientInfoInDB = DB.SelectOneClientInDB(clientID);

            if(clientInfoInDB[1] != listOfAllInfo[0])
            {
                listOfAllInfo[0] = clientInfoInDB[1];
            }
            if (clientInfoInDB[2] != listOfAllInfo[1])
            {
                listOfAllInfo[1] = clientInfoInDB[2];
            }
            if (clientInfoInDB[3] != listOfAllInfo[2])
            {
                listOfAllInfo[2] = clientInfoInDB[3];
            }
            if (clientInfoInDB[4] != listOfAllInfo[3])
            {
                listOfAllInfo[3] = clientInfoInDB[4];
            }
            return listOfAllInfo;
        }
        public bool DuplicateClientVerification(string firstName, string lastName)
        {
            var isDuplicate = false;
            var DB = new DBConnection();

            if (DB.IsClientAlreadyInDB(firstName,lastName))
            {
                throw new MultipleUserWithSameName("Client already in Database");
            }
            return isDuplicate;
        }
    }
}