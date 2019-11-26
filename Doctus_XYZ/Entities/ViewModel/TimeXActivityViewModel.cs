namespace Entities.ViewModel
{
    public class TimeXActivityViewModel
    {
        #region Properties
        public int IdTimeXActivity { get; set; }
        public int IdActivity { get; set; }
        public string Description { get; set; }
        public int TimeWorked { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string DateActivity { get; set; }
        #endregion Properties

        public TimeXActivityViewModel()
        {

        }

        public virtual void Dispose()
        {

        }

        ~TimeXActivityViewModel()
        {

        }
    }
}
