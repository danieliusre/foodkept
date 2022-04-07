

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodKept.Data;

namespace FoodKept.Models
{
    public class SQLTokenRepository : ITokenRepository
    {
        private readonly ShopContext _context;

        public SQLTokenRepository(ShopContext context)
        {
            _context = context;
        }

        public bool Authorize(string token)
        {
            
            var repoToken = _context.Tokens.Where((items) => items.Token == token).FirstOrDefault();
            if(repoToken != null){
                return true;
            }
            return false;
        }

        public string Login(string username, string password)
        {
            var user = _context.Users.Where((user) => user.Username == username && user.Password == password).FirstOrDefault();
            
            if(user == null){
                return null;
            }
            var newToken= Guid.NewGuid().ToString();
            var token = _context.Tokens.Where((item) => item.Username == user.Username).FirstOrDefault();
            if(token == null){
                _context.Tokens.Add(new Tokens() {Username= username, Token = newToken });
                _context.SaveChanges();
                return newToken;
            }
            token.Token = newToken;
            _context.Tokens.Update(token);
            _context.SaveChanges();

            return newToken;
        }

    }
}
