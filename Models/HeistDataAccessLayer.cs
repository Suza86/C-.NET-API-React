using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agency04.Models
{
    public class HeistDataAccessLayer

    {
        HeistDBContext db = new HeistDBContext();

        public IEnumerable<HeistMember> GetAllHeistMembers()
        {
            try
            {
                return db.HeistMember.ToList();
            }
            catch
            {
                throw;
            }
        }

        // Method AddHeistMember add new member record
        public int AddHeistMember(HeistMember member)
        {
            try {
                db.HeistMember.Add(member);
                db.SaveChanges();
                return 1;
            }
            catch {
                throw;
            }
        }

        //Method UpdateHeistMember update record of praticular member
        public int UpdateHeistMember(HeistMember member)
        {
            try
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Method DeleteHeistMember delete record of a praticular member
        public int DeleteHeistMember(int id) {
            try {
                HeistMember hm = db.HeistMember.Find(id);
                db.HeistMember.Remove(hm);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        // Method HeistMember get the details of a praticular member
        public HeistMember GetHeistMemberData(int id)
        {
            try
            {
                HeistMember member = db.HeistMember.Find(id);
                return member;
            }
            catch
            {
                throw;
            }
        }

        // Get List of HeistSkills
        public List<HeistSkills> GetHeistSkills()
        {
            List<HeistSkills> lhs = new List<HeistSkills>();
            lhs = (from HeadSkillsList in db.HeistSkills select HeadSkillsList).ToList();
            return lhs;
        }
    }
}
