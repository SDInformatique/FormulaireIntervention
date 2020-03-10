using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FormulaireIntervention.Models
{
    /// <summary>
    /// Class that contains methodes for interaction with the database
    /// </summary>
    public class DBConnection
    {
        private MySqlConnection connection;
        private MySqlCommand command;

        /// <summary>
        /// Constructor for DBConnection, initalize the connection with the Database 
        /// </summary>
        public DBConnection()
        {
            Init();
        }

        #region Private Methodes
        /// <summary>
        /// Method that initialize the connection with the databas by opening it.
        /// </summary>
        private void Init()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";           
            stringBuilder.Database = "formulaireintervention";
            stringBuilder.UserID = "DBconnector";
            stringBuilder.Password = "Pa$$w0rd";
            connection = new MySqlConnection(stringBuilder.ToString());
            connection.Open();
        }
        /// <summary>
        /// Private Method for closing the connection with the database
        /// </summary>
        /// <param name="connection"></param>
        private void Close(MySqlConnection connection)
        {
            connection.Dispose();
        }
        #endregion
        #region Public Methodes
        #region InsertMethods
        /// <summary>
        /// Insert a New client in the database with all required fields
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <param name="firstName">The first name of the client</param>
        /// <param name="lastName">The last name of the client</param>
        /// <param name="address">The address of the client</param>
        /// <param name="phoneNumber">The phone number of the client</param>
        public void InsertNewClientInDatabase(string firstName, string lastName, string address, string phoneNumber)
        {
            try{
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
            catch (MySqlException e)
            {
                throw;
            }
            string insertCommand = $@"INSERT INTO clients(ClientFirstName, ClientLastName, ClientAddress, ClientPhoneNumber) VALUES ('{firstName}','{lastName}','{address}','{phoneNumber}')";
            command = new MySqlCommand(insertCommand, connection);
            int result = command.ExecuteNonQuery();
            connection.Close();
        }
        public void InsertNewIntervention(int intervenantID,int interventionClientID, int interventionTypeID, string interventionDuration)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string insertCommand = $@"INSERT INTO intervention(InterventionIntervenantID ,InterventionClientID, InterventionTypeID, InterventionDuration) VALUES ('{intervenantID}','{interventionClientID}','{interventionTypeID}','{interventionDuration}')";
            command = new MySqlCommand(insertCommand, connection);
            int result = command.ExecuteNonQuery();
            connection.Close();
        }
        public void InsertClientSignature(int clientID, int interventionID, string signatureData)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string insertCommand = $@"INSERT INTO clientsignature(clientID ,interventionID, signatureData) VALUES ('{clientID}','{interventionID}','{signatureData}')";
            command = new MySqlCommand(insertCommand, connection);
            int result = command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
        #region Select Methods
        public List<string> SelectOneClientInDB(int ID)
        {
            string selectCommand = $@"SELECT * FROM clients WHERE ClientID = '{ID}' ;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            var resultList = new List<string>();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    resultList.Add(reader.GetString(0));
                    resultList.Add(reader.GetString(1));
                    resultList.Add(reader.GetString(2));
                    resultList.Add(reader.GetString(3));
                    resultList.Add(reader.GetString(4));
                }
            }
            reader.Close();
            connection.Close();
            return resultList;
        }
        public int SelectClientIDInDB(string firstName, string lastName)
        {
            string selectCommand = $@"SELECT ClientID FROM clients WHERE ClientFirstName = '{firstName}' AND ClientLastName = '{lastName}'  ;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            int resultID = 0;
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    resultID = reader.GetInt16(0);
                    counter++;
                }
            }
            reader.Close();
            connection.Close();
            return resultID;
        }
        
        public bool IsClientAlreadyInDB(string firstName, string lastName)
        {
            bool isClientInDB = false;
            string selectCommand = $@"SELECT ClientID FROM clients WHERE ClientFirstName = '{firstName}' AND ClientLastName = '{lastName}'  ;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                isClientInDB = true;
            }
            reader.Close();
            connection.Close();

            return isClientInDB;
;
        }
        #endregion
        #region Delete Methods
        public void DeleteUserInDB(int ID)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string selectCommand = $@"DELETE FROM clients WHERE ClientID = '{ID}' ;";
            command = new MySqlCommand(selectCommand, connection);
            int result = command.ExecuteNonQuery();
            if (result != 1)
            {
                throw new UnknownClient("The client isn't existing in database");
            }
            connection.Close();
        }
        public void DeleteUserInDB(string firstName, string lastName)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string selectCommand = $@"SELECT ClientID FROM clients WHERE ClientFirstName = '{firstName}'  AND ClientLastName = '{lastName}' ;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            List<int> IDList = new List<int>();
            int counter = 0;
            if (reader.HasRows)
            {
                
                while (reader.Read())
                {
                    IDList.Add( reader.GetInt16(0));
                    counter++;
                }
            }

            reader.Close();
            
            if (counter == 0)
            {
                throw new UnknownClient("The client isn't existing in database");
            }
            else
            {
                foreach (var id in IDList)
                {
                    DeleteUserInDB(id);
                }             
            }
            connection.Close();
        }
        #endregion
        #region Get Methods
        public List<Intervenant> GetListOfIntervenant()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            List<Intervenant> listIntervernant = new List<Intervenant>();
            Intervenant intervenant;
            string selectCommand = $@"SELECT IntervenantFirstName, IntervenantLastName FROM intervenant;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    intervenant = new Intervenant();
                    intervenant.FirstName = reader.GetString(0);
                    intervenant.LastName = reader.GetString(1);
                    listIntervernant.Add(intervenant);
                    counter++;
                }

            }
            reader.Close();
            connection.Close();
            return listIntervernant;
        }
        public List<InterventionType> GetListOfInterventionType()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            List<InterventionType> listInterventionTypes = new List<InterventionType>();
            InterventionType interventionType;
            string selectCommand = $@"SELECT InterventionTypeName FROM interventiontype;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    interventionType = new InterventionType();
                    interventionType.Type = reader.GetString(0);
                    listInterventionTypes.Add(interventionType);
                    counter++;
                }

            }
            reader.Close();
            connection.Close();
            return listInterventionTypes;
        }
        public List<ExistingClient> GetListOfClients()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            List<ExistingClient> existingClients = new List<ExistingClient>();
            string selectCommand = $@"SELECT ClientID, ClientFirstName, ClientLastName, ClientAddress, ClientPhoneNumber  FROM clients;";
            ExistingClient existingClient;
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    existingClient = new ExistingClient();
                    existingClient.ID = reader.GetInt16(0);
                    existingClient.FirstName = reader.GetString(1);
                    existingClient.LastName = reader.GetString(2);
                    existingClient.Address = reader.GetString(3);
                    existingClient.PhoneNumber = reader.GetString(4);
                    existingClients.Add(existingClient);
                }
            }
            reader.Close();
            connection.Close();
            return existingClients;
        }
        public int GetClientIDFromName(string firstname, string lastname)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            int clientID = 0;
            string selectCommand = $@"SELECT ClientID FROM clients WHERE ClientFirstName = {firstname} AND ClientLastName = {lastname}";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if(clientID == 0)
                    {
                        clientID = reader.GetInt16(0);
                    }                 
                }
            }
            reader.Close();
            connection.Close();
            return clientID;
        }
        public int GetLastClientID()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            int lastuserID = 0;
            string selectCommand = $@"SELECT ClientID FROM clients ORDER BY ClientID DESC LIMIT 1;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lastuserID = reader.GetInt16(0);
                }
            }
            reader.Close();
            connection.Close();
            return lastuserID;
        }
        public int GetLastInterventionID()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            int lastInterventionID = 0;
            string selectCommand = $@"SELECT InterventionID FROM intervention ORDER BY InterventionID DESC LIMIT 1;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lastInterventionID = reader.GetInt16(0);
                }
            }
            reader.Close();
            connection.Close();
            return lastInterventionID;
        }
        public int GetIntervenantID(string firstName, string lastName)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string selectCommand = $@"SELECT IntervenantID FROM intervenant WHERE IntervenantFirstName = '{firstName}' AND IntervenantLastName = '{lastName}'  ;";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            int resultID = 0;
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    resultID = reader.GetInt16(0);
                    counter++;
                }
            }
            reader.Close();
            connection.Close();
            return resultID;
        }
        public int GetInterventionTypeID(string interventionTypeName)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            string selectCommand = $@"SELECT InterventionTypeID FROM interventionType WHERE InterventionTypeName = '{interventionTypeName}';";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            int resultID = 0;
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    resultID = reader.GetInt16(0);
                    counter++;
                }
            }
            reader.Close();
            connection.Close();
            return resultID;
        }
        public int GetClientIDFromIntervention(int interventionID)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            int clientID = 0;
            string selectCommand = $@"SELECT InterventionClientID FROM intervention WHERE InterventionID = {interventionID}";
            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clientID = reader.GetInt16(0);
                }
            }
            reader.Close();
            connection.Close();
            return clientID;
        }

        #endregion
        public List<string> FetchAllInfo(int interventionID)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            var listOfAllInfo = new List<string>();

            //omg j'ai tellement ragé sur cet putain de reuket
            string selectCommand = $@"SELECT intervention.InterventionID,`clients`.`ClientFirstName` AS `ClientFirstName`,`clients`.`ClientLastName` AS `ClientLastName`,`clients`.`ClientAddress` AS `ClientAddress`,`clients`.`ClientPhoneNumber` AS `ClientPhoneNumber`,`intervenant`.`IntervenantFirstName` AS `IntervenantFirstName`,`intervenant`.`IntervenantLastName` AS `IntervenantLastName`,interventiontype.InterventionTypeName, intervention.InterventionDuration 
                                    FROM (((`intervention` 
                                    left join `clients` on(`intervention`.`InterventionClientID` = `clients`.`ClientID`)) 
                                    left join `intervenant` on(`intervention`.`InterventionIntervenantID` = `intervenant`.`IntervenantID`))
                                    LEFT JOIN interventiontype ON(intervention.InterventionTypeID = interventiontype.InterventionTypeID))
                                    WHERE intervention.InterventionID = '{interventionID}';";

            command = new MySqlCommand(selectCommand, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int counter = 0;
                while (reader.Read())
                {
                    listOfAllInfo.Add(reader.GetString(1));
                    listOfAllInfo.Add(reader.GetString(2));
                    listOfAllInfo.Add(reader.GetString(3));
                    listOfAllInfo.Add(reader.GetString(4));
                    listOfAllInfo.Add(reader.GetString(5));
                    listOfAllInfo.Add(reader.GetString(6));
                    listOfAllInfo.Add(reader.GetString(7));
                    listOfAllInfo.Add(reader.GetString(8));
                    counter++;
                }
            }

            return listOfAllInfo;
        }
        #endregion
    }
}