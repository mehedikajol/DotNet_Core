﻿using System.Collections.Generic;

namespace IdentityDemo.Constants
{
    public class ClaimsList
    {
        public static Dictionary<string, string> AllClaim = new Dictionary<string, string>(){
            { "CanViewProduct", "CanViewProduct" },
            { "CanAddProduct", "CanAddProduct"},
            { "CanEditProduct", "CanEditProduct"},
            { "CanDeleteProduct", "CanDeleteProduct"}
        };
    }
}