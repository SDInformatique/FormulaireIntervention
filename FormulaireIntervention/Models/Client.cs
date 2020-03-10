using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public abstract class Client
    {
        protected int id;
        protected string firstName;
        protected string lastName;
        protected string phoneNumber;
        protected string address;

        protected Client()
        {
        }

        protected Client(string firstName, string lastName, string phoneNumber, string address)
        {
            this.firstName = firstName;
            this.lastName = lastName;   
            this.phoneNumber = phoneNumber;
            this.address = address;
        }
        protected Client(int id, string firstName, string lastName, string phoneNumber, string address)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
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
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}