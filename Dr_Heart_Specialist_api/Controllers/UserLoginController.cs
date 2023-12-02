using Dr_Heart_Specialist_api.DataBaseEntity;
using Dr_Heart_Specialist_api.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Dr_Heart_Specialist_api.Controllers
{
    /// <summary>
    /// Controller for user login.
    /// </summary>
    public class UserLoginController : ApiController
    {
        Dr_Heart_Specialist_api_db db = new Dr_Heart_Specialist_api_db();

        /// <summary>
        /// for debugging.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Log()
        {
            string wanted_path     = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            var    binPath         = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var    parentOfBinPath = new DirectoryInfo(binPath).Parent.FullName;
            var    filePath        = System.Web.HttpContext.Current.Server.MapPath("..\\..\\Models\\MLModel\\TrainingData\\Regular_Train_Data.csv.txt");

            return Ok(filePath);
        }

        /// <summary>
        /// login user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Login(USER_LOGIN user)
        {
            USER_LOGIN Registered_User = db.USER_LOGIN.Where(c => c.Username == user.Username).FirstOrDefault<USER_LOGIN>();
            try
            {
                if (Registered_User != null)
                {

                    Hash PasswordHash = new Hash();

                    string Salt           = Registered_User.Salt;
                    string HashedPassword = PasswordHash.GenerateSha256Hash(user.Password, Salt);

                    HashedPassword        = HashedPassword.Replace("-", string.Empty).Substring(0, 16);

                    if (HashedPassword == Registered_User.Password)
                    {
                        Login_Time data = new Login_Time();
                        
                        data.Username  = user.Username;
                        data.LoginTime = DateTime.Now;
                        data.Email     = Registered_User.Email;

                        db.Login_Time.Add(data);
                        db.SaveChanges();
                        
                        return Ok("ok");
                    }
                    else
                    {
                        return BadRequest("Invalid username or password");
                    }
                }
                else
                {
                    return BadRequest("Invalid username or password");

                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    }
}
