namespace Entities.ViewModel
{
    public class ActivityViewModel
    {
        #region Properties
        public int IdActivity { get; set; }
        public string Description { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
        #endregion Properties

        public ActivityViewModel()
        {

        }

        public virtual void Dispose()
        {

        }

        ~ActivityViewModel()
        {

        }
    }
}
