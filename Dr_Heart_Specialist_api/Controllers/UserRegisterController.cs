using DnsClient;
using Dr_Heart_Specialist_api.DataBaseEntity;
using Dr_Heart_Specialist_api.Models;
using Dr_Heart_Specialist_api.Models.DataModel;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Dr_Heart_Specialist_api.Controllers
{
    public class UserRegisterController : ApiController
    {
        HeartHealthAppDatabaseEntitiesEntities db = new HeartHealthAppDatabaseEntitiesEntities();
       
        [HttpPost]
        public IHttpActionResult Register(RegisterUser user2)
        {
            string Validate = IsVAlidate(user2);
            if (Validate=="USER VALIDATED")
            {

                USER_REGISTER_DATA userRegister = Convert2UserRegister(user2);
                USER_LOGIN userLogin = Convert2UserLogin(user2);
                CONSULTANT_DATA userConsultant = Convert2ConsultantData(user2);
                User_Info userInfo = Convert2UserInfo(user2);
                db.USER_REGISTER_DATA.Add(userRegister);
                db.USER_LOGIN.Add(userLogin);
                db.CONSULTANT_DATA.Add(userConsultant);
                db.User_Info.Add(userInfo);
                db.SaveChanges();
                return Ok("USER ADDED SUCCESSFULLY");
            }
            else
            {
                
                return BadRequest(Validate);
            }
        }





        //CUSTOMIZE FUNCTIONS
       
        public bool ValidateMXRecord(string[] domain)
        {
            var lookup = new LookupClient(IPAddress.Parse("8.8.4.4"), IPAddress.Parse("8.8.8.8"));

            lookup.Timeout = TimeSpan.FromSeconds(5);

            var result = lookup.Query(domain[1].ToString(), QueryType.MX);

            var records = result.Answers;

            if (records.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string IsVAlidate(RegisterUser checkuser)
        {
          try
          {
            if (checkuser.First_Name != null && checkuser.Last_Name != null && checkuser.Contact_No.ToString() != "^([0-9]{11})$" && checkuser.Address != null && checkuser.Username != null && checkuser.Password != null && checkuser.Email != null)
            {
                    var checkemail = db.USER_LOGIN.Where(c => c.Email == checkuser.Email || c.Username==checkuser.Username).FirstOrDefault<USER_LOGIN>();
                    if (checkemail==null)
                    {
                          if (Regex.IsMatch(checkuser.Contact_No.ToString(), @"^([0-9]{10})$"))
                            {
                                if (ValidateMXRecord(checkuser.Email.Split('@')) == true)
                                {
                                    return "USER VALIDATED";
                                }
                                else
                                {
                                    return "INVALID DOMAIN";
                                }
                            }
                            else
                            {
                                return "INVALID PHONE NUMBER";
                            }
                       

                    }
                    else 
                    {
                        return "EMAIL OR USERNAME ALREADY EXISTS";
                    }
            }
            else 
            {
             return "ALL FIELDS REQUIRED";
            }
                    
          }
          catch
          {
           return "BAD REQUEST";
          }
        }
        public USER_REGISTER_DATA Convert2UserRegister(RegisterUser user1)
        {
            USER_REGISTER_DATA ConvertUser = new USER_REGISTER_DATA();
            ConvertUser.First_Name = user1.First_Name;
            ConvertUser.Last_Name = user1.Last_Name;
            ConvertUser.Address = user1.Address;
            ConvertUser.Contact_No = Convert.ToInt32(user1.Contact_No);
            ConvertUser.Username = user1.Username;
            return ConvertUser;
        }
        public USER_LOGIN Convert2UserLogin(RegisterUser user1)
        {
            USER_LOGIN ConvertUser = new USER_LOGIN();
            Hash PasswordHash = new Hash();
            ConvertUser.Username = user1.Username;
            ConvertUser.Email = user1.Email;
            string Salt = PasswordHash.CreateSalt(5);
            ConvertUser.Salt = Salt;
            string HashedPassword = PasswordHash.GenerateSha256Hash(user1.Password , Salt);
            HashedPassword = HashedPassword.Replace("-", string.Empty).Substring(0, 16);
            ConvertUser.Password = HashedPassword;//to be hash
            return ConvertUser;
        }
        public CONSULTANT_DATA Convert2ConsultantData(RegisterUser user1) 
        {
            CONSULTANT_DATA ConvertUser = new CONSULTANT_DATA();
            ConvertUser.Consultant_Email = user1.Consultant_Email;
            ConvertUser.Consultant_Name = user1.Consultant_Name;
            ConvertUser.Username = user1.Username;
            return ConvertUser;
        }
        public User_Info Convert2UserInfo(RegisterUser user1) 
        {
            User_Info Convertuser = new User_Info();
            Convertuser.Age = Convert.ToDouble(user1.Age);
            Convertuser.Gender =Convert.ToDouble(user1.Gender);
            Convertuser.Fbs = user1.IsDiabetic;
            Convertuser.Username = user1.Username;
            Convertuser.Register_Date_Time = DateTime.Now;
            return Convertuser;
        }
    }
}
