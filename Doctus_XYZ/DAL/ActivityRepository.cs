namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities.Dto;
    using DAL;
    using System.Configuration;
    using Entities;
    using DAL.Util;
    using Entities.ViewModel;

    public class ActivityRepository : RepositoryMapper<ActivityViewModel>
    {
        public ActivityRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XYZConnection"].ConnectionString;
        }

        public Result<string> SaveActivity(object option, Activity dtoActivity, string user)
        {
            var idResult = ExecSPReturnMessage("SPActivities",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pDescription", dtoActivity.Description),
                new KeyValuePair<string, object>("pIdUser", dtoActivity.IdUser)
                );
            return idResult;
        }

        public Result<IList<ActivityViewModel>> GetActivities(object option, Activity dtoActivity, string user)
        {
            return ExecSPReturnList("SPActivities",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pIdActivity", dtoActivity.IdActivity),
                new KeyValuePair<string, object>("pDescription", dtoActivity.Description),
                new KeyValuePair<string, object>("pIdUser", dtoActivity.IdUser)
                );
        }
    }
}
