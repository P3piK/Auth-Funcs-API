using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Entity
{
    public abstract class EntityBase
    {
        public DateTime Modified { get; set; }
        public int? ModifierId { get; set; }
        public virtual User? Modifier { get; set; }

        public void SetupSystemFields()
        {
            Modified = DateTime.Now;
            //ModifierId = null;
        }
    }
}
