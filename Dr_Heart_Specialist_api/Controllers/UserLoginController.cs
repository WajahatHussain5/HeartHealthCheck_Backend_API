using System;
using System.Linq;
using System.Web.Http;
using Dr_Heart_Specialist_api.Models;
using System.IO;
using System.Reflection;
using Dr_Heart_Specialist_api.DataBaseEntity;

namespace Dr_Heart_Specialist_api.Controllers
{
    public class UserLoginController : ApiController
    {
        HeartHealthAppDatabaseEntitiesEntities db = new HeartHealthAppDatabaseEntitiesEntities();

        [HttpGet]
        public IHttpActionResult Log()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var parentOfBinPath = new DirectoryInfo(binPath).Parent.FullName;
            var filePath = System.Web.HttpContext.Current.Server.MapPath("..\\..\\Models\\MLModel\\TrainingData\\Regular_Train_Data.csv.txt");
            return Ok(filePath);
        }
        [HttpPost]
        public IHttpActionResult Login(USER_LOGIN user)
        {
            USER_LOGIN Registered_User = db.USER_LOGIN.Where(c => c.Username == user.Username).FirstOrDefault<USER_LOGIN>();
            try
            {
                if (Registered_User != null)
                {

                    Hash PasswordHash = new Hash();
                    string Salt = Registered_User.Salt;
                    string HashedPassword = PasswordHash.GenerateSha256Hash(user.Password, Salt);
                    HashedPassword = HashedPassword.Replace("-", string.Empty).Substring(0, 16);
                    if (HashedPassword == Registered_User.Password)
                    {
                        Login_Time data = new Login_Time();
                        data.Username = user.Username;
                        data.LoginTime = DateTime.Now;
                        db.Login_Time.Add(data);
                        db.SaveChanges();
                        return Ok("ok");
                    }
                    else { return BadRequest("Invalid username or password"); }
                }
                else
                {
                    return BadRequest("Invalid username or password");

                }
            }
            catch (Exception e) {
                return Ok(e);
            }
        }
    }
}
