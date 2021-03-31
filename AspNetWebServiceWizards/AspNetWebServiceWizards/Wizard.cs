using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebServiceWizards
{
    public class Wizard
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string School { get; set; }
        public string BloodStatus { get; set; }
        public string Occupation { get; set; }
        public Birthday Birthday { get; set; }
    }
}
