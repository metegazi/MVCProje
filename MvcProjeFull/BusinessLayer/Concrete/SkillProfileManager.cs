﻿using BusinessLayer.Abstact;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SkillProfileManager : ISkillProfileService
    {
        ISkillProfileDal _skillProfile;

        public SkillProfileManager(ISkillProfileDal skillProfile)
        {
            _skillProfile = skillProfile;
        }

        public SkillProfile GetByID(int id)
        {
            return _skillProfile.Get(x => x.SkillID == id);
        }

        public List<SkillProfile> GetList()
        {
            return _skillProfile.List();
        }

        public void SkillProfileAdd(SkillProfile skill)
        {
            _skillProfile.Insert(skill);
        }

        public void SkillProfileDelete(SkillProfile skill)
        {
            throw new NotImplementedException();
        }

        public void SkillProfileUpdate(SkillProfile skill)
        {
            _skillProfile.Update(skill);
        }
    }
}