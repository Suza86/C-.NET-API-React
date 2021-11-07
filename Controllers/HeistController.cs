using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agency04.Models;

namespace Agency04.Controllers
{
    public class HeistController : Controller
    {
        HeistDataAccessLayer heistMember = new HeistDataAccessLayer();

        [HttpGet]
        [Route("api/member/Index")]
        public IEnumerable<HeistMember> Index()
        {
            return heistMember.GetAllHeistMembers();
        }

        [HttpPost]
        [Route("api/member/CreateHeistMember")]
        public int CreateHeistMember(HeistMember member)
        {
            return heistMember.AddHeistMember(member);
        }

        [HttpGet]
        [Route("api/member/DetailsHeistMember/{id}")]
        public HeistMember DetailsHeistMember(int id)
        {
            return heistMember.GetHeistMemberData(id);
        }

        [HttpPut]
        [Route("api/member/EditHeistMember")]
        public int EditHeistMember(HeistMember member)
        {
            return heistMember.UpdateHeistMember(member);
        }

        [HttpDelete]
        [Route("api/member/deleteHeistMember/{id}")]
        public int deleteHeistMember(int id) {
            return heistMember.DeleteHeistMember(id);
        }

        [HttpGet]
        [Route("api/member/GetHeistMemberList")]
        public IEnumerable<HeistSkills> DetailsHeistMember() {
            return heistMember.GetHeistSkills();
        }
    }
}
