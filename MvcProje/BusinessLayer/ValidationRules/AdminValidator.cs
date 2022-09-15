using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AdminValidator: AbstractValidator<Admin>
    {
        public AdminValidator()
        {
            //----------------------Kural oluşturma sayfası-------------------
            RuleFor(x => x.AdminUserName).NotEmpty().WithMessage("Kullanıcı Adını Boş Geçemezsiniz");
            RuleFor(x => x.AdminPassword).NotEmpty().WithMessage("Şifreyi Boş Geçemezsiniz");
            RuleFor(x => x.AdminRole).NotEmpty().WithMessage("Kullanıcı Rolünü Boş Geçemezsiniz");
            RuleFor(x => x.AdminPassword).MinimumLength(3).WithMessage("Lütfen en 3 karakterde bir şifre giriniz");
            RuleFor(x => x.AdminPassword).MaximumLength(5).WithMessage("Lütfen 5 karakterden fazla değer girişi yapmayın");
            RuleFor(x => x.AdminRole).MaximumLength(1).WithMessage("Lütfen 1 karakterden fazla değer girişi yapmayın");
        }
    }
}
