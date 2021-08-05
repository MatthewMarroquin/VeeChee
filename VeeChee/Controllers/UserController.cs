using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VeeChee.Controllers
{
    public class UserController : ControllerBase
    {

        IConfiguration _iconfiguration;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IConfiguration iconfiguration)
        {
            _logger = logger;
            _iconfiguration = iconfiguration;
        }

        [HttpPost]
        [Route("api/User/Signup")]
        public async Task<IActionResult> Signup([FromBody] UserRequest data)
        {
            UserResponse response = new UserResponse();
            try
            {
                if (data != null && !string.IsNullOrEmpty(data?.email) && !string.IsNullOrEmpty(data?.password) && !string.IsNullOrEmpty(data?.username))
                {
                    if (!ValidEmail(data.email))
                    {
                        response.Message = "Invalid Email";
                        return Ok(JsonConvert.SerializeObject(response));
                    }
                    //TODO: call signup service
                    string requestUri = _iconfiguration["USER_GATEWAY"] ?? string.Empty;
                    response.Message = "Success";
                    return Ok(JsonConvert.SerializeObject(response));
                }
                else
                {
                    response.Message = "InvalidRequest";
                    return Ok(JsonConvert.SerializeObject(response));
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An Unexpected Error Occured on Login. Exception: {e?.Message}");
                response.Message = "InternalServerError";
                return BadRequest(JsonConvert.SerializeObject(response));
            }
        }

        [HttpPost]
        [Route("api/User/Login")]
        public async Task<IActionResult> Login([FromBody] UserRequest data)
        {
            UserResponse response = new UserResponse();
            try
            {
                if (data != null && !string.IsNullOrEmpty(data?.email) && !string.IsNullOrEmpty(data?.password))
                {
                    if (!ValidEmail(data.email))
                    {
                        response.Message = "Invalid Email";
                        return Ok(JsonConvert.SerializeObject(response));
                    }
                    //TODO : call login service
                    string requestUri = _iconfiguration["USER_GATEWAY"] ?? string.Empty;
                    response.Message = "Success";
                    response.UserId = "new user 123";
                    return Ok(JsonConvert.SerializeObject(response));
                }
                else
                {
                    response.Message = "Invalid Request";
                    return Ok(JsonConvert.SerializeObject(response));
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"An Unexpected Error Occured on Login. Exception: {e?.Message}");
                response.Message = "InternalServerError";
                return BadRequest(JsonConvert.SerializeObject(response));
            }
        }

        private bool ValidEmail(string email)
        {
            Regex r = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
            try
            {
                if (r.IsMatch(email?.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An Unexpected Error Occured Validating Email. Exception: {e?.Message}");
                return false;
            }
        }
    }

    public class UserRequest
    {
        public string requestType { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string userId { get; set; }
    }

    public class UserResponse
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        public string RoleId {get; set;}
    }
}
