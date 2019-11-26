namespace BLL
{
    using DAL;
    using Entities;
    using Entities.Dto;
    using System.Collections.Generic;
    using Entities.ViewModel;

    public class TimeXActivityBL
    {
        private TimeXActivityRepository repository = new TimeXActivityRepository();

        public TimeXActivityBL()
        {

        }

        public Result<string> RegisterTimeXActivity(object option, TimeXActivity dtoTimeXActivity, string user)
        {
            Result<string> result = repository.SaveTimeXActivity(option, dtoTimeXActivity, user);

            return result;
        }

        public Result<IList<TimeXActivityViewModel>> GetTimeXActivity(object option, TimeXActivity dtoTimeXActivity, string user)
        {
            var result = repository.GetTimeXActivity(option, dtoTimeXActivity, user);
            if (result.Data == null || result.Data.Count == 0)
            {
                if (!string.IsNullOrEmpty(result.Message))
                {
                    return result;
                }
                else
                {
                    IList<TimeXActivityViewModel> emptyList = null;
                    return new Result<IList<TimeXActivityViewModel>>(emptyList, result.Message);
                }
            }
            else
                return result;

        }
    }
}
