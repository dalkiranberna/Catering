using Entity.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Gender
    {
        Female,
        Male
    }

    public enum PersonType
    {
        Member,
        Chef
    }


    public class Member : IdentityUser
    {
        public int Age { get; set; }
        public string ImageURL { get; set; }
        public bool HasPhoto { get; set; } //fotoğrafı yoksa default bi şey getirsin diye
        public int Experience { get; set; }
        public string WhoAmI { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public string Password { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual List<Certificate> Certificates { get; set; }
        public virtual List<Product> Products { get; set; } //kişinin dükkanındaki ürünler
        public int SenderId { get; set; }
        public List<Notification> Notifications { get; set; }
        public Gender Gender { get; set; }
        public PersonType PersonType { get; set; }
        public Member()
        {
            Like = 0;
            Dislike = 0;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Member> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Certificate : IEntity<int>
    {
        public int Id { get; set; }
        public string CertName { get; set; }
        public DateTime CertDate { get; set; }
        public string ByWho { get; set; }
        //public string MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
