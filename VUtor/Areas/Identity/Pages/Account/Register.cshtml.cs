#nullable disable

using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<TopicEntity> TopicList { get; set; } = new List<TopicEntity>();


        public class InputModel
        {

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
            public CourseName CourseName { get; set; }

            [Required]
            [Display(Name = "Course Year")]
            public CourseYear CourseYear { get; set; }

            [Display(Name = "Topic To Learn")]
            public int TopicToLearn { get; set; }

            [Display(Name = "Topic To Teach")]
            public int TopicToTeach { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            TopicList = await _context.Topics.ToListAsync();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            TopicList = await _context.Topics.ToListAsync();
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid && Input.CourseName != 0 && Input.CourseYear != 0)
            {
                var user = CreateUser();

                user.Name = Input.Name;
                user.Surname = Input.Surname;
                user.CourseInfo = new CourseData((int)Input.CourseName, (int)Input.CourseYear);
                user.CreationDate = new profileCreationDate();
                _context.Attach(user);

                foreach (var topic in TopicList)
                {
                    if (topic.Id == Input.TopicToLearn)
                    {
                        _context.Attach(topic);
                        topic.LearningProfiles.Add(user);
                        user.TopicsToLearn.Add(topic);
                    }
                    if (topic.Id == Input.TopicToTeach)
                    {
                        _context.Attach(topic);
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