using Microsoft.AspNetCore.Identity;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            if (context.Event.Any())
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = "admin",
                Email = "admin",
                EmailConfirmed = true,
            };

            userManager.CreateAsync(user, "Password123!").GetAwaiter().GetResult();

            var list = new Event();
            list.Name = "List 1";
            list.Schedule.Add(new Plan
            {
                Descripton = "Item 1.1"
            });

            context.Event.Add(list);

            // Veel andmeid

            context.SaveChanges();
        }
    }
}
