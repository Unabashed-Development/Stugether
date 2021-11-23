using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway
{
    public class Account
    {


        public string passwordHash => BCrypt.Net.BCrypt.HashPassword("Spidb1@!#");

        public bool verified => BCrypt.Net.BCrypt.Verify("Spidb1@!#", passwordHash);
    }
}
