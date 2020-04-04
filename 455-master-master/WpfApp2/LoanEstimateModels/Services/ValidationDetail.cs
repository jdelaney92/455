using System;
using System.Collections.Generic;

namespace TVA_CCU.Models
{
    public class ValidationDetail
    {
       public bool IsValid { get; set; }
       
       public List<string> ErrorMessages { get; set; }
    }
}


