using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Entity
{
    public enum UserRoleEnum
    {
        User = 1,
        Superuser = 2,
        Admin = 3,
    }

    public class UserRole
    {
        public UserRole() 
        { 
        }

        private UserRole(UserRoleEnum roleEnum)
        {
            Id = (int)roleEnum;
            Name = roleEnum.ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public static implicit operator UserRole(UserRoleEnum roleEnum) => new UserRole(roleEnum);
        public static implicit operator UserRoleEnum(UserRole userRole) => (UserRoleEnum)userRole.Id;
    }
}
