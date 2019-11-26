namespace DAL
{
    using Entities.Dto;
    using DAL.Util;
    using Entities;
    using System.Configuration;
    using System.Collections.Generic;

    public class UserRepository : RepositoryMapper<User>
    {
        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XYZConnection"].ConnectionString;
        }

        public Result<string> SaveUser(object option, User dtoUser, string user)
        {
            var idResult = ExecSPReturnMessage("SPUser",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pUserName", dtoUser.UserName),
                new KeyValuePair<string, object>("pFullName", dtoUser.FullName),
                new KeyValuePair<string, object>("pPassword", dtoUser.Password)
                );
            return idResult;
        }

        public Result<IList<User>> GetUser(object option, User dtoUser, string user)
        {
            return ExecSPReturnList("SPUser",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pIdUser", dtoUser.IdUser),
                new KeyValuePair<string, object>("pUserName", dtoUser.UserName),
                new KeyValuePair<string, object>("pFullName", dtoUser.FullName),
                new KeyValuePair<string, object>("pPassword", dtoUser.Password)
                );
        }
    }
}
