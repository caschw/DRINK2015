using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRINK2015.Domain
{
    public class PartyDetail
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Address { get; set; }

        public bool RequiresInvite { get; set; }

        public string Sponsors { get; set; }
    }
}
