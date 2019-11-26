namespace WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Entities.Dto;
    using Entities;
    using BLL;
    using System.Web;
    using Entities.ViewModel;

    public class ActivitiesController : ApiController
    {
        // GET: api/Activities
        [HttpGet]
        public Result<IList<ActivityViewModel>> GetActivities()
        {
            Result<string> sessionResult = validarSesion();
            Result<IList<ActivityViewModel>> result;
            if (sessionResult.StatusCode == 200)
            {
                var BL = new ActivityBL();

                Activity dtoActivity = new Activity();
                dtoActivity.IdUser = int.Parse(sessionResult.Data.Split('¬')[1].ToString());
                result = BL.GetActivities(1, dtoActivity, string.Empty);
                if (string.IsNullOrEmpty(result.Message))
                    result.StatusCode = (int)HttpStatusCode.OK;
                else
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
                result = new Result<IList<ActivityViewModel>>(null, sessionResult.Message, sessionResult.StatusCode);

            if (!string.IsNullOrEmpty(result.Message))
                result.Message = result.Message.Split('¬')[1];

            return result;
        }

        [HttpPost]
        public Result<IList<ActivityViewModel>> RegisterActivity(Activity dtoActivity)
        {
            Result<IList<ActivityViewModel>> result;
            Result<string> resultInsert;
            Result<string> sessionResult = validarSesion();
            if (sessionResult.StatusCode == 200)
            {
                ActivityBL BL = new ActivityBL();
                dtoActivity.IdUser = int.Parse(sessionResult.Data.Split('¬')[1].ToString());
                resultInsert = BL.RegisterActivity(2, dtoActivity, string.Empty);
                if (string.IsNullOrEmpty(resultInsert.Message) || resultInsert.Message.StartsWith("1"))
                {
                    result = BL.GetActivities(1, dtoActivity, string.Empty);
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    result = new Result<IList<ActivityViewModel>>(null, resultInsert.Message, sessionResult.StatusCode);
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
                result = new Result<IList<ActivityViewModel>>(null, sessionResult.Message, sessionResult.StatusCode);
            if (!string.IsNullOrEmpty(result.Message))
                result.Message = result.Message.Split('¬')[1];
            return result;
        }

        private Result<string> validarSesion()
        {
            Result<string> result;
            var httpRequest = HttpContext.Current.Request;
            var tokens = httpRequest.RequestContext.HttpContext.Items;
            string _token = string.Empty;
            string user = string.Empty;
            if (((System.Net.Http.HttpRequestMessage)(tokens["MS_HttpRequestMessage"])).Headers.Authorization != null)
            {
                _token = ((System.Net.Http.HttpRequestMessage)(tokens["MS_HttpRequestMessage"])).Headers.Authorization.Parameter.ToString();
                user = TokenGenerator.ValidateToken(_token);
                if (!string.IsNullOrEmpty(user))
                    result = new Result<string>(user, string.Empty, (int)HttpStatusCode.OK);
                else
                    result = new Result<string>(user, "3¬Su sesión caducó", (int)HttpStatusCode.Unauthorized);
            }
            else
                result = new Result<string>(user, "3¬Token no recibido", (int)HttpStatusCode.BadRequest);

            return result;
        }
    }
}
