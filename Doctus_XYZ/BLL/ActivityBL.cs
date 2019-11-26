namespace BLL
{
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Dto;
    using Entities.ViewModel;

    public class ActivityBL
    {
        private ActivityRepository repository = new ActivityRepository();

        public ActivityBL()
        {

        }

        public Result<string> RegisterActivity(object option, Activity dtoActivity, string user)
        {
            Result<string> result = repository.SaveActivity(option, dtoActivity, user);

            return result;
        }

        public Result<IList<ActivityViewModel>> GetActivities(object option, Activity dtoActivity, string user)
        {
            var result = repository.GetActivities(option, dtoActivity, user);
            if (result.Data == null || result.Data.Count == 0)
            {
                if (!string.IsNullOrEmpty(result.Message))
                {
                    return result;
                }
                else
                {
                    IList<ActivityViewModel> emptyList = null;
                    return new Result<IList<ActivityViewModel>>(emptyList, result.Message);
                }
            }
            else
                return result;

        }
    }
}
