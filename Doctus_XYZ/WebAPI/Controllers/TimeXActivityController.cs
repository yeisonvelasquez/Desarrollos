namespace WebAPI.Controllers
{
    using BLL;
    using Entities;
    using Entities.Dto;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Http;
    using System;
    using System.Globalization;
    using Entities.ViewModel;

    public class TimeXActivityController : ApiController
    {
        // GET: api/Activities
        [HttpGet]
        public Result<IList<TimeXActivityViewModel>> GetTimeXActivity(int idActivity)
        {
            Result<string> sessionResult = validarSesion();
            Result<IList<TimeXActivityViewModel>> result;
            if (sessionResult.StatusCode == 200)
            {
                var BL = new TimeXActivityBL();

                TimeXActivity dtoTimeXActivity = new TimeXActivity()
                {
                    IdActivity = idActivity,
                    IdUser = int.Parse(sessionResult.Data.Split('¬')[1].ToString())
                };
                result = BL.GetTimeXActivity(1, dtoTimeXActivity, string.Empty);
                if (string.IsNullOrEmpty(result.Message))
                {
                    result.StatusCode = (int)HttpStatusCode.OK;
                    if (result.Data == null)
                        result.Message = "1¬No se encontraron registros de tiempo para la Actividad";
                }
                else
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
                result = new Result<IList<TimeXActivityViewModel>>(null, sessionResult.Message, sessionResult.StatusCode);

            if (!string.IsNullOrEmpty(result.Message))
                result.Message = result.Message.Split('¬')[1];

            return result;
        }

        [HttpPost]
        public Result<IList<TimeXActivityViewModel>> RegisterTimeXActivity(TimeXActivity dtoTimeXActivity)
        {
            Result<IList<TimeXActivityViewModel>> result;
            Result<string> resultInsert;
            Result<string> sessionResult = validarSesion();
            if (sessionResult.StatusCode == 200)
            {
                //DateTime dt = DateTime.ParseExact(dtoTimeXActivity.DateActivity.ToString() + " 12:00:00", "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                //dtoTimeXActivity.DateActivity = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                TimeXActivityBL BL = new TimeXActivityBL();
                dtoTimeXActivity.IdUser = int.Parse(sessionResult.Data.Split('¬')[1].ToString());
                resultInsert = BL.RegisterTimeXActivity(2, dtoTimeXActivity, string.Empty);
                if (string.IsNullOrEmpty(resultInsert.Message) || resultInsert.Message.StartsWith("1"))
                {
                    result = BL.GetTimeXActivity(1, dtoTimeXActivity, string.Empty);
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    result = new Result<IList<TimeXActivityViewModel>>(null, resultInsert.Message, sessionResult.StatusCode);
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
                result = new Result<IList<TimeXActivityViewModel>>(null, sessionResult.Message, sessionResult.StatusCode);
            if (!string.IsNullOrEmpty(result.Message) && result.Message.Contains("¬"))
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
