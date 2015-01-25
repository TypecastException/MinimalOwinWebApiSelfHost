using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add usings:
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class MyUser
    {
        public MyUser()
        {
            Id = Guid.NewGuid().ToString();
            Claims = new List<MyUserClaim>();
        }

        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<MyUserClaim> Claims { get; set; }
    }


    public class MyUserClaim
    {
        public MyUserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }


    public class MyPasswordHasher
    {
        public string CreateHash(string password)
        {
            // FOR DEMO ONLY! Use a standard method or 
            // crypto library to do this for real:
            char[] chars = password.ToArray();
            char[] hash = chars.Reverse().ToArray();
            return new string(hash);
        }
    }


    public class MyUserStore
    {
        ApplicationDbContext _db;
        public MyUserStore(ApplicationDbContext context)
        {
            _db = context;
        }


        public async Task AddUserAsync(MyUser user, string password)
        {
            if (await UserExists(user))
            {
                throw new Exception(
                    "A user with that Email address already exists");
            }
            var hasher = new MyPasswordHasher();
            user.PasswordHash = hasher.CreateHash(password).ToString();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }


        public async Task<MyUser> FindByEmailAsync(string email)
        {
            var user = _db.Users
                .Include(c => c.Claims)
                .FirstOrDefaultAsync(u => u.Email == email);

            return await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<MyUser> FindByIdAsync(string userId)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<bool> UserExists(MyUser user)
        {
            return await _db.Users
                .AnyAsync(u => u.Id == user.Id || u.Email == user.Email);
        }


        public async Task AddClaimAsync(string UserId, MyUserClaim claim)
        {
            var user = await FindByIdAsync(UserId);
            if(user == null)
            {
                throw new Exception("User does not exist");
            }
            user.Claims.Add(claim);
            await _db.SaveChangesAsync();
        }


        public bool PasswordIsValid(MyUser user, string password)
        {
            var hasher = new MyPasswordHasher();
            var hash = hasher.CreateHash(password);
            return hash.Equals(user.PasswordHash);
        }
    }


    //public class Role
    //{
    //    public Role()
    //    {
    //        Id = Guid.NewGuid().ToString();
    //    }

    //    [Key]
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public List<UserRole> Users { get; set; }
    //}


    //public class UserRole
    //{
    //    public string UserId { get; set; }
    //    public User User { get; set; }

    //    public string RoleId { get; set; }
    //    public Role Role { get; set; }
    //}


    //public class UserManager
    //{
    //    UserStore _userStore;
    //    public UserManager(UserStore userStore)
    //    {
    //        _userStore = userStore;
    //    }


    //    public async Task CreateAsync(User user, string password)
    //    {
    //        if(await _userStore.UserExists(user))
    //        {
    //            throw new Exception("A user with that Email address already exists");
    //        }
    //        var hasher = new PasswordHasher();
    //        user.PasswordHash = hasher.CreateHash(password);
    //        await _userStore.AddUserAsync(user);
    //    }


    //    public async Task AddClaim(string UserId, Claim claim)
    //    {
    //        _userStore.AddClaim()
    //    }
    //}
}
