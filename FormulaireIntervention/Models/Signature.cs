using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormulaireIntervention.Models
{
    public class Signature
    {
        public string SignatureData { get; set; }
        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(SignatureData))
            {
                return;
            }
            var binarySignature = Convert.FromBase64String(SignatureData);

            System.IO.File.WriteAllBytes("Signature.png", binarySignature); 
        }
    }
}