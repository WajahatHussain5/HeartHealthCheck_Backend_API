//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dr_Heart_Specialist_api.DataBaseEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Login_Time
    {
        public string Username { get; set; }
        public System.DateTime LoginTime { get; set; }
        public string Email { get; set; }
    
        public virtual USER_LOGIN USER_LOGIN { get; set; }
    }
}
