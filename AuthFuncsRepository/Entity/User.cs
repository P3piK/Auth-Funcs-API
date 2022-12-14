using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Entity
{
    public class User : EntityBase
    {
        private readonly DbContext context;

        #region Constructor

        public User()
        {
        }

        public User(AFContext context)
        {
            this.context = context;
        }

        #endregion

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int StatusId { get; set; }
        public virtual UserStatus Status { get; set; }

        public int RoleId { get; set; }
        public virtual UserRole Role { get; set; }

        public int Persist()
        {
            SetupSystemFields();

            context.Update(this);
            context.SaveChanges();

            return Id;
        }
    }
}
