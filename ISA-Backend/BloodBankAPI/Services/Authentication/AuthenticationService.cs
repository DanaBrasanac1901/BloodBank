﻿using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Materials.PasswordHasher;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BloodBankAPI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IPasswordHasher _passwordHasher;
        private IEmailSendService _emailSendService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthenticationService(IUnitOfWork unitOfWork, IEmailSendService emailSendService, IPasswordHasher passwordHasher, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSendService = emailSendService;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            if (await GetUserByEmailAsync(email) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> EmailMatchesPasswordAsync(LoginDTO dto)
        {
            Account userByEmail = await GetUserByEmailAsync(dto.Email);
            if (userByEmail != null)
            {
                return _passwordHasher.VerifyHashedPassword(userByEmail.Password, dto.Password);
            }

            return false;

        }

        private async Task<Account> GetUserByEmailAsync(string email)
        {
            IEnumerable<Account> accounts = await _unitOfWork.AccountRepository.GetByConditionAsync(u => u.Email == email);
            return accounts.FirstOrDefault();
        }

        public async Task<string> LogInUserAsync(LoginDTO dto)
        {
            Account account = await GetUserByEmailAsync(dto.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, account.UserType.ToString()),
                new Claim(ClaimTypes.PrimarySid, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.GivenName, account.Name),
                new Claim(ClaimTypes.Surname, account.Surname),
            };
            return GenerateToken(claims);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityTokenHandler().WriteToken(

                new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: credentials
                    ));

            return token;
        }


        public async Task RegisterDonor(DonorRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Donor donorData = _mapper.Map<Donor>(dto);
            await _unitOfWork.DonorRepository.InsertAsync(donorData);
            await _unitOfWork.SaveAsync();
        }

        public async Task RegisterStaff(StaffRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Staff staffData = _mapper.Map<Staff>(dto);
            await _unitOfWork.StaffRepository.InsertAsync(staffData);
            await _unitOfWork.SaveAsync();
        }

        public async Task RegisterAdmin(AdminRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Admin adminData = _mapper.Map<Admin>(dto);
            await _unitOfWork.AdminRepository.InsertAsync(adminData);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> PrepareActivationToken(string email)
        {
            Account account = await GetUserByEmailAsync(email);
            string token = Guid.NewGuid().ToString();
            if (account != null)
            {
                account.Token = token;
                await UpdateAccount(account);
            }

           token = StringToHMACString(token);

            return token;
        }

        private string StringToHMACString(string source)
        {
            using (HMACSHA256 hash = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration["VerificationTokenKey"])))
            {
                byte[] hmacToken = hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                source = Convert.ToBase64String(hmacToken);
            }
            return source;
        }

        public async Task SendActivationLink(string email, string link)
        {
            string subject = "BloodCenter Activation Link";
            string body = "Your activation link: " + link + "\nAfter clicking on the link you will be able to log in!";
            _emailSendService.SendEmail(new Message(new string[] { email, "tibbers707@gmail.com", "danabrasanac@gmail.com" }, subject, body));
        }

        public async Task<bool> ActivateAccount(string email, string token)
        {
            Account account = await GetUserByEmailAsync(email);
            if(token == null || account.UserType != UserType.DONOR) return false;
            if (TokenIsValid(account.Token, token ))
            {
                account.IsActive = true;
                await UpdateAccount(account);
            }
            return false;

        }

        private bool TokenIsValid(string tokenFromDB, string hmacToken) {
            return hmacToken == StringToHMACString(tokenFromDB);
      
        }

        public async Task UpdateAccount(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();
        }



        /*

                public string Create(User user)
                {
                    string tempPass = null;
                    if (user.Password.Equals("") || user.Password == null)
                    {
                        user.Password = GeneratePassword(7);
                        tempPass = user.Password;
                    }
                    string newPass = _passwordHasher.HashPassword(user.Password);
                    user.Password = newPass;
                    _userRepository.Create(user);
                    return tempPass;
                }

                public bool ChangePassword(User user)
                {
                    if (user == null) return false;
                    user.Password = _passwordHasher.HashPassword(user.Password);

                    Update(user);
                    return true;

                }

                public string GeneratePassword(int length)
                {
                    StringBuilder password = new StringBuilder();
                    Random random = new Random();

                    while (password.Length < length)
                    {
                        char c = (char)random.Next(32, 126);

                        if (char.IsLetterOrDigit(c)) password.Append(c);

                    }

                    return password.ToString();
                }

            
                */
    }
}
