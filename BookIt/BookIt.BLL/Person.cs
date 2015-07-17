using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookIt.BLL
{
    public class Person
    {
        public int Id { get; set; }
        public Role PersonRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}