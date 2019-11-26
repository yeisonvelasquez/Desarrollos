namespace Entities.Dto
{
    using System;
    public class TimeXActivity
    {
        #region Properties
        public int IdTimeXActivity { get; set; }
        public int IdActivity { get; set; }
        public int TimeWorked { get; set; }
        public int IdUser { get; set; }
        public string DateActivity { get; set; }
        #endregion Properties

        public TimeXActivity()
        {

        }

        public virtual void Dispose()
        {

        }

        ~TimeXActivity()
        {

        }
    }
}
