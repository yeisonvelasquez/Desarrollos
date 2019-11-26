namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL;
    using Entities;
    using Entities.Dto;

    public class UserBL
    {
        private UserRepository repository = new UserRepository();

        public UserBL()
        {

        }

        public Result<string> SaveUser(object option, User dtoUser, string user)
        {
            Result<string> result = repository.SaveUser(option, dtoUser, user);

            return result;
        }

        public Result<IList<User>> GetUser(object option, User dtoUser, string user)
        {
            var result = repository.GetUser(option, dtoUser, user);
            if (result.Data == null || result.Data.Count == 0)
            {
                if (!string.IsNullOrEmpty(result.Message))
                {
                    return result;
                }
                else
                {
                    IList<User> emptyList = null;
                    return new Result<IList<User>>(emptyList, result.Message);
                }
            }
            else
                return result;

        }

        public Result<IList<User>> Login(object option, User dtoUser, string user)
        {
            var result = repository.GetUser(option, dtoUser, user);
            if (result.Data == null || result.Data.Count == 0)
            {
                if (!string.IsNullOrEmpty(result.Message))
                {
                    return result;
                }
                else
                {
                    IList<User> emptyList = null;
                    return new Result<IList<User>>(emptyList, "3¬Usuario o contraseña Inválidos");
                }
            }
            else
                return result;

        }
    }
}
