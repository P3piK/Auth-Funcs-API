using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsRepository.Entity
{
    public enum UserStatusEnum
    {
        Active = 1,
        Inactive = 2,
        PasswordReset = 3,
        NotConfirmed = 4,
    }

    public class UserStatus
    {
        public UserStatus()
        {
        }
        
        private UserStatus(UserStatusEnum statusEnum)
        {
            Id = (int)statusEnum;
            Name = statusEnum.ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public static implicit operator UserStatus(UserStatusEnum statusEnum) => new UserStatus(statusEnum);
        public static implicit operator UserStatusEnum(UserStatus userStatus) => (UserStatusEnum)userStatus.Id;
    }
}
