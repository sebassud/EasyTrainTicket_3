using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyTrainTickets3.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Tickets { get; set; }  // Our property

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //"Server=tcp:easytraintickets.database.windows.net,1433;Initial Catalog=easytrainticketsdb;Persist Security Info=False;User ID=sebassud;Password=qwER12#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        //Data Source=.\\SQLEXPRESS;Initial Catalog=EasyTrainTickets.Domain.Data.EasyTrainTicketsDbEntities;Integrated Security=True
        public ApplicationDbContext()
            : base("Server=tcp:easytraintickets.database.windows.net,1433;Initial Catalog=easytrainticketsdb;Persist Security Info=False;User ID=sebassud;Password=qwER12#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}