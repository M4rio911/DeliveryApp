using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.API.Configuration;

public static class InitRoles
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Client", "Delivery", "DeliveryManager", "Support", "Admin" };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
    public static async Task SeedAdminUserAsync(UserManager<User> userManager)
    {
        string adminEmail = "admin@gmail.com";
        string login = "admin";
        string adminPassword = "zaq1@WSX";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin != null)
        {
            if (!await userManager.IsInRoleAsync(existingAdmin, "Admin"))
            {
                await userManager.AddToRoleAsync(existingAdmin, "Admin");
            }
            return;
        }

        var adminUser = new User
        {
            UserName = login,
            FirstName = "admin",
            LastName = "admin",
            UserTypeId = 4,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            throw new Exception("Failed to create default admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public static async Task SeedDictionaries(DeliveryDbContext dbContext)
    {
        var now = DateTime.UtcNow;

        var lsitOfDictionaryTypes = new List<DictionaryType>()
        {
            new(){Id = 1, Name = "UserType", Created = now, CreatedBy = "init"},
            new(){Id = 2, Name = "PackageStatus", Created = now, CreatedBy = "init"},
            new(){Id = 5, Name = "PackageType", Created = now, CreatedBy = "init"},
            new(){Id = 6, Name = "TransportationType", Created = now, CreatedBy = "init"},
            new(){Id = 7, Name = "PaymentStatus", Created = now, CreatedBy = "init"},
            new(){Id = 8, Name = "PaymentType", Created = now, CreatedBy = "init"},
            new(){Id = 9, Name = "AddressType", Created = now, CreatedBy = "init"},
            new(){Id = 10, Name = "TransportationStatus", Created = now, CreatedBy = "init"}
        };

        await dbContext.AddRangeAsync(lsitOfDictionaryTypes);

        var dictionaries = new List<Dictionary>
        {
            new() { Id = 1, DictionaryTypeId = 1, Name = "Client", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 2, DictionaryTypeId = 8, Name = "BLIK", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 4, DictionaryTypeId = 1, Name = "Admin", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 5, DictionaryTypeId = 7, Name = "Unpaid", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 6, DictionaryTypeId = 7, Name = "Paid", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 7, DictionaryTypeId = 8, Name = "PayPal", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 8, DictionaryTypeId = 8, Name = "Card", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 9, DictionaryTypeId = 8, Name = "Transfer", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 12, DictionaryTypeId = 1, Name = "Delivery", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 13, DictionaryTypeId = 1, Name = "DeliveryManager", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 14, DictionaryTypeId = 1, Name = "Support", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 15, DictionaryTypeId = 5, Name = "XL", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 16, DictionaryTypeId = 5, Name = "L", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 17, DictionaryTypeId = 5, Name = "M", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 18, DictionaryTypeId = 5, Name = "S", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 19, DictionaryTypeId = 5, Name = "Letter", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 20, DictionaryTypeId = 6, Name = "Collection", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 21, DictionaryTypeId = 6, Name = "Delivery", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 22, DictionaryTypeId = 9, Name = "Delivery address", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 23, DictionaryTypeId = 9, Name = "PickupPoint", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 24, DictionaryTypeId = 2, Name = "New", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 25, DictionaryTypeId = 2, Name = "Paid", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 26, DictionaryTypeId = 2, Name = "Posted", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 27, DictionaryTypeId = 2, Name = "AssignedToCollect", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 28, DictionaryTypeId = 2, Name = "Collected", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 29, DictionaryTypeId = 2, Name = "Storage", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 30, DictionaryTypeId = 2, Name = "AssignedToDelivery", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 31, DictionaryTypeId = 2, Name = "InDelivery", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 32, DictionaryTypeId = 2, Name = "IssuedToDelivery", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 33, DictionaryTypeId = 2, Name = "Delivered", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 34, DictionaryTypeId = 2, Name = "Completed", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 35, DictionaryTypeId = 10, Name = "Scheduled", IsDefault = true, Created = now, CreatedBy = "init" },
            new() { Id = 36, DictionaryTypeId = 10, Name = "Started", IsDefault = false, Created = now, CreatedBy = "init" },
            new() { Id = 37, DictionaryTypeId = 10, Name = "Finished", IsDefault = false, Created = now, CreatedBy = "init" }
        };

        await dbContext.AddRangeAsync(dictionaries);

        await dbContext.SaveChangesAsync();
    }
}
