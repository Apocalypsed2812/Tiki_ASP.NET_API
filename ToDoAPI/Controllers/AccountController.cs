using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories.AccountRepository;
using ToDoAPI.Repositories.ProductRepository;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string Error = "User Not Exists";
        private readonly IAccountRepository _account;
        private readonly IProductRepository _product;

        public AccountController(IAccountRepository account, IProductRepository product)
        {
            _account = account;
            _product = product;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLogin model)
        {
            var account = await _account.LoginAsync(model.Username, model.Password);
            if (account == null)
            {
                return NotFound("Not Found Account");
            }
            var hashedPassword = account!.Password;
            var passwordHasher = new PasswordHasher<Account>();
            var result = passwordHasher.VerifyHashedPassword(account, hashedPassword, model.Password);
            if (result == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Username),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VdUjXn2r5u7x!A%D*G-KaPdSgVkYp3s6"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "localhost:5238",
                    audience: "localhost:5238",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    account = account
                });

            }
            else
            {
                return Unauthorized("Password is not correct");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountModel model)
        {
            try
            {
                var passwordHasher = new PasswordHasher<AccountModel>();
                var hashedPassword = passwordHasher.HashPassword(model, model.Password);
                model.Password = hashedPassword;
                var newAccount = await _account.RegisterAsync(model);
                return newAccount == null ? NotFound() : Ok(newAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(AddToCart model)
        {
            try
            {
                await _account.AddToCart(model.accountId, model.productId);
                return Ok("Add Product To Cart Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            // Lấy thông tin tài khoản từ token đính trên header
            var username = User.Identity!.Name;

            // Trả về thông tin tài khoản
            var account = await _account.GetAccountByUsername(username);
            if(account != null)
            {
                return Ok(account);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("get-user-cart/{id}")]
        public async Task<IActionResult> GetUserCart(int id)
        {
            var userCart = await _account.GetUserCart(id);
            var productList = await _product.GetAllProductsAsync();
            if (userCart != null)
            {
                List<int> productIds = userCart.UserCartProducts.Select(ucp => ucp.ProductId).ToList();
                List<ProductModel> filteredProducts = productList.Where(p => productIds.Contains(p.Id)).ToList();

                return Ok(filteredProducts);
            }
            return NotFound();
        }

        [HttpPost("delete-product-cart")]
        public async Task<IActionResult> DeleteProductFromCart(AddToCart model)
        {
            try
            {
                var result = await _account.DeleteProductFromCart(model.accountId, model.productId);
                if (result)
                {
                    return Ok("Product removed from cart.");
                }
                return NotFound("Not Found Account Id Or Product Id");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error removing product from cart: {ex.Message}");
            }
        }

        [HttpPost("change-quantity")]
        public async Task<IActionResult> ChangeQuantity(ChangeQuantity c)
        {
            var productCart = await _account.ChangeQuantity(c.Id, c.Handle);
            if (productCart != 0)
            {
                return Ok(1);
            }
            else
            {
                return NotFound("Product Id Not Found");
            }
        }
    }
}
