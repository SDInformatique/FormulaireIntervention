using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    #region Database Related Exception
    [Serializable]
    public class InvalidArgument : Exception
    {
        public InvalidArgument()
        {
        }
        public InvalidArgument(string message) : base(message)
        {
        }

        public InvalidArgument(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidArgument(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Client Related Exception
    public class UnknownClient : Exception
    {

        public UnknownClient()
        {
        }
        public UnknownClient (string message) : base(message)
        {

        }

        public UnknownClient(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public class MultipleUserWithSameName : Exception
    {
        public MultipleUserWithSameName (string message) : base(message)
        {

        }

        public MultipleUserWithSameName()
        {
        }

        public MultipleUserWithSameName(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    #endregion
}