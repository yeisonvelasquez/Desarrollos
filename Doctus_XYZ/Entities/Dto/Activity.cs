namespace Entities.Dto
{
    public class Activity
    {
        #region Properties
        public int IdActivity { get; set; }
        public string Description { get; set; }
        public int IdUser { get; set; }
        #endregion Properties

        public Activity()
        {

        }

        public virtual void Dispose()
        {

        }

        ~Activity()
        {

        }
    }
}
