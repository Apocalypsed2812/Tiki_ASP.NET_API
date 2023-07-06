using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata;
using System.Security.Principal;
using ToDoAPI.Data;
using ToDoAPI.DTO;
using ToDoAPI.Models;

namespace ToDoAPI.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HobbyContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<AccountModel> _passwordHasher;

        public AccountRepository(HobbyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<AccountModel>();
        }

        public async Task AddToCart(int accountId, int productId)
        {
            var userCart = await _context.UserCarts!
                                     .Include(uc => uc.UserCartProducts)
                                     .FirstOrDefaultAsync(uc => uc.AccountId == accountId);

            var product = await _context.Products!.FindAsync(productId);

            if (userCart == null)
            {
                userCart = new UserCart
                {
                    AccountId = accountId,
                    UserCartProducts = new List<UserCartProduct>()
                };
                _context.UserCarts!.Add(userCart);
            }

            userCart.UserCartProducts.Add(new UserCartProduct
            {
                UserCartId = userCart.Id,
                ProductId = product.Id,
                Product = product,
            });

            await _context.SaveChangesAsync();
            //return true;


        }

        public async Task<int> ChangeQuantity(int productId, string handle)
        {
            return 1;
        }

        public async Task<bool> DeleteProductFromCart(int accountId, int productId)
        {

            var cart = await _context.UserCarts!
                            .Include(uc => uc.UserCartProducts)
                            .FirstOrDefaultAsync(uc => uc.Id == accountId);

            if (cart != null)
            {
                var productToRemove = cart.UserCartProducts.FirstOrDefault(ucp => ucp.ProductId == productId);
                if (productToRemove != null)
                {
                    cart.UserCartProducts.Remove(productToRemove);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<AccountModel> GetAccountByUsername(string username)
        {
            var account = await _context.Accounts!.SingleOrDefaultAsync(a => a.Username == username);
            if (account != null)
            {
                var accountLogin = _mapper.Map<AccountModel>(account);
                return accountLogin;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserCartDTO>GetUserCart(int accountId)
        {
            var userCart = await _context.UserCarts!
                                     .Include(uc => uc.UserCartProducts)
                                         //.ThenInclude(ucp => ucp.Product)
                                     .FirstOrDefaultAsync(uc => uc.AccountId == accountId);
            if(userCart != null)
            {
                var userCartDTO = _mapper.Map<UserCartDTO>(userCart);
                return userCartDTO;
            }
            return null;
        }

        public async Task<Account> LoginAsync(string username, string password)
        {
            var account = await _context.Accounts!.FirstOrDefaultAsync(a => a.Username == username);
            //var accountLogin = _mapper.Map<AccountModel>(account);
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }

        public async Task<AccountDTO> RegisterAsync(AccountModel model)
        {
            var account = await _context.Accounts!.SingleOrDefaultAsync(a => a.Username == model.Username);
            if (account != null)
            {
                return null;
            }
            else
            {
                var newAccount = _mapper.Map<Account>(model);

                newAccount.Role = "user";
                //var userCart = new UserCart { Products = new List<Product>() };
                //newAccount.UserCart = userCart;

                _context.Accounts!.Add(newAccount);
                await _context.SaveChangesAsync();

                var newAccountDto = _mapper.Map<AccountDTO>(newAccount);
                var userCartDto = _mapper.Map<UserCartDTO>(newAccount.UserCart);
                newAccountDto.UserCart = userCartDto;

                return newAccountDto;
            }
        }
    }
}
