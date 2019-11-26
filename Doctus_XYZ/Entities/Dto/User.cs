namespace Entities.Dto
{
    public class User
    {
        #region Properties
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        #endregion Properties

        public User()
        {

        }

        public virtual void Dispose()
        {

        }

        ~User()
        {

        }
    }
}
