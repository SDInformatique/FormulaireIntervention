using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace FormulaireIntervention.Models
{
    public class Intervention
    {
        private Client client;
        private Intervenant intervenant;
        private InterventionType interventionType;
        public Intervention()
        {

        }
        public Intervention(Client client, Intervenant intervenant, InterventionType interventionType)
        {
            this.client = client;
            this.intervenant = intervenant;          
            this.interventionType = interventionType;
        }

        public Client Client
        {
            get { return client; }
        }
        public Intervenant Intervenant
        {
            get { return intervenant; }
        }
        public InterventionType InterventionType
        {
            get { return interventionType; }
        }


        public void CreateXML()
        {
            var DB = new DBConnection();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("   "),
                CloseOutput = true,
                OmitXmlDeclaration = true
            };
            XmlDocument xmlDocument = new XmlDocument();
            using (XmlWriter writer = XmlWriter.Create($@"Intervention{DB.GetLastInterventionID()}.xml"))
            {
                writer.WriteStartElement("Intervention");

                writer.WriteStartElement("Client");
                writer.WriteElementString("ID", this.client.ID.ToString());
                writer.WriteElementString("FirstName", this.client.FirstName);
                writer.WriteElementString("LastName", this.client.LastName);
                writer.WriteElementString("LastName", this.client.Address);
                writer.WriteElementString("LastName", this.client.PhoneNumber);
                writer.WriteEndElement();

                writer.WriteStartElement("Intervenant");

                writer.WriteElementString("FirstName", this.Intervenant.FirstName);
                writer.WriteElementString("LastName", this.Intervenant.LastName);
                writer.WriteEndElement();

                writer.WriteStartElement("Intervention_Type");
                writer.WriteElementString("Type", this.interventionType.Type);
                writer.WriteEndElement();
                writer.WriteEndElement();
                xmlDocument.Save(writer);
                writer.Flush();               
            }
            
        }
    }
}