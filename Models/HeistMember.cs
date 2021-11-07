using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agency04.Models
{
    public class HeistMember
    {
        public int HeistMemberId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Skills { get; set; }
        public string MainSkill { get; set; }
        public string Status { get; set; }
    }
}
