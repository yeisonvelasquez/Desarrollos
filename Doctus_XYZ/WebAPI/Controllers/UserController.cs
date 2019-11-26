namespace WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Entities;
    using Entities.Dto;
    using BLL;
    using System.Web;

    public class UserController : ApiController
    {
        // GET: api/User
        [HttpGet]
        public Result<IList<User>> GetUsers()
        {
            //Result<string> sessionResult = validarSesion();
            Result<IList<User>> result;
            //if (sessionResult.StatusCode == 200)
            //{
                var BL = new UserBL();

                User dtoUser = new User();

                result = BL.GetUser(0, dtoUser, string.Empty);
                if (string.IsNullOrEmpty(result.Message))
                    result.StatusCode = (int)HttpStatusCode.OK;
                else
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
            //}
            //else
            //    result = new Result<IList<Reception>>(null, sessionResult.Message, sessionResult.StatusCode);

            return result;
        }

        //[HttpPost]
        //public Result<string> SaveUser(User dtoUser)
        //{
        //    Result<string> result;
        //    //Result<string> sessionResult = validarSesion();
        //    //if (sessionResult.StatusCode == 200)
        //    //{
        //        var BL = new UserBL();

        //    result = BL.SaveUser(2, dtoUser, string.Empty);
        //        if (string.IsNullOrEmpty(result.Message) || result.Message.StartsWith("1"))
        //            result.StatusCode = (int)HttpStatusCode.OK;
        //        else
        //            result.StatusCode = (int)HttpStatusCode.BadRequest;
        //    //}
        //    //else
        //    //    result = new Result<IList<Box>>(null, sessionResult.Message, sessionResult.StatusCode);
        //    if (!string.IsNullOrEmpty(result.Message))
        //        result.Message = result.Message.Split('¬')[1];
        //    return result;
        //}

        [HttpPost]
        //[Route("authenticate")]
        public Result<string> Login(User dtoUser)
        {
            Result<string> result = new Result<string>(string.Empty, string.Empty);
            if (dtoUser == null || string.IsNullOrEmpty(dtoUser.UserName) || string.IsNullOrEmpty(dtoUser.Password))
                result = new Result<string>(null, "Campos obligatorios sin diligenciar", (int)HttpStatusCode.BadRequest);
            else
            {
                var BL = new UserBL();

                var tempResult = BL.Login(1, dtoUser, string.Empty);
                result = new Result<string>(tempResult != null && tempResult.Data != null ? TokenGenerator.GenerateToken(tempResult.Data.FirstOrDefault().UserName.ToString(), tempResult.Data.FirstOrDefault().IdUser) : string.Empty, tempResult.Message);
                if (string.IsNullOrEmpty(result.Message) || result.Message.StartsWith("1"))
                    result.StatusCode = (int)HttpStatusCode.OK;
                else
                    result.StatusCode = (int)HttpStatusCode.BadRequest;

                if (!string.IsNullOrEmpty(result.Message))
                    result.Message = result.Message.Split('¬')[1];
            }
            return result;
        }

        [HttpGet]
        [Route("validateuser")]
        public Result<string> ValidateUser()
        {
            Result<string> result;
            //TokenGenerator.ValidateToken(token);


            var httpRequest = HttpContext.Current.Request;
            var token = httpRequest.RequestContext.HttpContext.Items;
            string _token = string.Empty;
            string user = string.Empty;
            if (((System.Net.Http.HttpRequestMessage)(token["MS_HttpRequestMessage"])).Headers.Authorization != null)
            {
                _token = ((System.Net.Http.HttpRequestMessage)(token["MS_HttpRequestMessage"])).Headers.Authorization.Parameter.ToString();
                user = TokenGenerator.ValidateToken(_token);
                if (!string.IsNullOrEmpty(user))
                    result = new Result<string>(user, string.Empty, (int)HttpStatusCode.OK);
                else
                    result = new Result<string>(user, "Su sesión caducó", (int)HttpStatusCode.Unauthorized);
            }
            else
                result = new Result<string>(user, "Token no recibido", (int)HttpStatusCode.BadRequest);

            //return _token;

            return result;

            //Result<string> result = new Result<string>();
            //TokenGenerator.ValidateToken(token);
            //return result;
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
                    result = new Result<string>(user, "Su sesión caducó", (int)HttpStatusCode.Unauthorized);
            }
            else
                result = new Result<string>(user, "Token no recibido", (int)HttpStatusCode.BadRequest);

            return result;
        }

        //[HttpGet]
        //[Route("echoping")]
        //public IHttpActionResult EchoPing()
        //{
        //    return Ok(true);
        //}

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/User/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/User
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/User/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/User/5
        //public void Delete(int id)
        //{
        //}
    }
}
