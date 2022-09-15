using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface ISkillProfileService
    {
        List<SkillProfile> GetList();
        void SkillProfileAdd(SkillProfile skill);
        SkillProfile GetByID(int id);
        void SkillProfileDelete(SkillProfile skill);

        void SkillProfileUpdate(SkillProfile skill);
    }
}
