namespace DAL
{
    using DAL.Util;
    using Entities.Dto;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities;
    using Entities.ViewModel;

    public class TimeXActivityRepository : RepositoryMapper<TimeXActivityViewModel>
    {
        public TimeXActivityRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XYZConnection"].ConnectionString;
        }

        public Result<string> SaveTimeXActivity(object option, TimeXActivity dtoTimeXActivity, string user)
        {
            var idResult = ExecSPReturnMessage("SPTimeXActivity",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pIdActivity", dtoTimeXActivity.IdActivity),
                new KeyValuePair<string, object>("pTimeWorked", dtoTimeXActivity.TimeWorked),
                new KeyValuePair<string, object>("pIdUser", dtoTimeXActivity.IdUser),
                new KeyValuePair<string, object>("pDateActivity", dtoTimeXActivity.DateActivity)
                );
            return idResult;
        }

        public Result<IList<TimeXActivityViewModel>> GetTimeXActivity(object option, TimeXActivity dtoTimeXActivity, string user)
        {
            return ExecSPReturnList("SPTimeXActivity",
                new KeyValuePair<string, object>("pOption", option),
                new KeyValuePair<string, object>("pIdTimeXActivity", dtoTimeXActivity.IdTimeXActivity),
                new KeyValuePair<string, object>("pIdActivity", dtoTimeXActivity.IdActivity),
                new KeyValuePair<string, object>("pTimeWorked", dtoTimeXActivity.TimeWorked),
                new KeyValuePair<string, object>("pIdUser", dtoTimeXActivity.IdUser),
                new KeyValuePair<string, object>("pDateActivity", dtoTimeXActivity.DateActivity)
                );
        }
    }
}
