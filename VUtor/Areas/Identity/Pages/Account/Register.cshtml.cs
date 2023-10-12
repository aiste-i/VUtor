// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccessLibrary.Data;
using DataAccessLibrary.Models;

namespace VUtor.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ProfileEntity> _signInManager;
        private readonly UserManager<ProfileEntity> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserStore<ProfileEntity> _userStore;
        private readonly IUserEmailStore<ProfileEntity> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ProfileEntity> userManager,
            IUserStore<ProfileEntity> userStore,
            SignInManager<ProfileEntity> signInManager,
            ApplicationDbContext context,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Required]
            [Display(Name = "Course Name")]
            public int CourseName { get; set; }

            [Required]
            [Display(Name = "Course Year")]
            public int CourseYear { get; set; }

            [Display(Name = "Topic To Learn")]
            public TopicEntity TopicToLearn { get; set; }

            [Display(Name = "Topic To Teach")]
            public TopicEntity TopicToTeach { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public List<TopicEntity> TopicList { get; set; } = new List<TopicEntity>();

        public async Task OnGetAsync(string returnUrl = null)
        {
            TopicList = await _context.Topics.ToListAsync();
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            TopicList = await _context.Topics.ToListAsync();
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                var top = TopicList.ElementAtOrDefault(5);

                user.Name = Input.Name;
                user.Surname = Input.Surname;
                user.CourseName = Input.CourseName;
                Console.WriteLine(user.CourseName);
                user.CourseYear = Input.CourseYear;
                Console.WriteLine(user.CourseYear);

                foreach(var topic in TopicList)
                {
                    if(topic == Input.TopicToLearn)
                    {
                        topic.LearningProfiles.Add(user);
                        user.TopicsToLearn.Add(topic);
                    }
                    if(topic == Input.TopicToTeach)
                    { 
                        topic.TeachingProfiles.Add(user);
                        user.TopicsToTeach.Add(topic);
                    }
                }

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    //confirms email on registration, should be changed
                    await _userManager.ConfirmEmailAsync(user, code);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _context.SaveChangesAsync();
                    return LocalRedirect(returnUrl);
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ProfileEntity CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ProfileEntity>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ProfileEntity)}'. " +
                    $"Ensure that '{nameof(ProfileEntity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ProfileEntity> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ProfileEntity>)_userStore;
        }
    }
}
