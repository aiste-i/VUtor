/*using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DataAccessLibrary.Models;
using DataAccessLibrary.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace VUtor.Areas.Identity.Pages.Account.Manage
{
    public class EditProfileModel : PageModel
    {
        private readonly SignInManager<ProfileEntity> _signInManager;
        private readonly UserManager<ProfileEntity> _userManager;
        private readonly ApplicationDbContext _context;

        public EditProfileModel(
            UserManager<ProfileEntity> userManager,
            SignInManager<ProfileEntity> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public ProfileEntity Profile { get; set; }
        public List<TopicEntity> Topics { get; set; } = new List<TopicEntity>();


        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Display(Name = "Course Name")]
            public int CourseName { get; set; }

            [Display(Name = "Course Year")]
            public int CourseYear { get; set; }

            [Display(Name = "Topic To Learn")]
            public TopicEntity TopicToLearn { get; set; }

            [Display(Name = "Topic To Teach")]
            public TopicEntity TopicToTeach { get; set; }

            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Profile = await _userManager.GetUserAsync(User);
            ReturnUrl = returnUrl;
        }

        public async Task OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid && Profile != null)
            {
                if (!Input.Name.IsNullOrEmpty() && Input.Name.CompareTo(Profile.Name) != 0)
                {
                    Profile.Name = Input.Name;
                }
                if (!Input.Surname.IsNullOrEmpty() && Input.Surname.CompareTo(Profile.Surname) != 0)
                {
                    Profile.Surname = Input.Surname;
                }
                if (Input.CourseName.CompareTo(Profile.CourseName) != 0)
                {
                    Profile.CourseName = Input.CourseName;
                }
                if (Input.CourseYear.CompareTo(Profile.CourseYear) != 0)
                {
                    Profile.CourseYear = Input.CourseYear;
                }
                if (Input.TopicToLearn != null && !Input.TopicToLearn.Equals(Profile.TopicsToLearn.First()))
                {
                    var removedTopic = Profile.TopicsToLearn.First();
                    var addedTopic = FindTopic(Input.TopicToLearn.Id);
                    if (addedTopic != null)
                    {
                        Profile.TopicsToLearn.Remove(removedTopic);
                        Profile.TopicsToLearn.Add(addedTopic);
                        removedTopic.LearningProfiles.Remove(Profile);
                        addedTopic.LearningProfiles.Add(Profile);
                    }
                }
                if (Input.TopicToTeach != null && !Input.TopicToTeach.Equals(Profile.TopicsToTeach))
                {
                    var removedTopic = Profile.TopicsToTeach.First();
                    var addedTopic = FindTopic(Input.TopicToTeach.Id);
                    if (addedTopic != null)
                    {
                        Profile.TopicsToTeach.Remove(removedTopic);
                        Profile.TopicsToTeach.Add(addedTopic);
                        removedTopic.TeachingProfiles.Remove(Profile);
                        addedTopic.TeachingProfiles.Add(Profile);
                    }
                }
                await _context.SaveChangesAsync();
                return LocalRedirect(returnUrl);
            }
            return Page();
        }

        private TopicEntity FindTopic(int Id)
        {
            Topics = _context.Topics.ToList();
            foreach (TopicEntity topic in Topics)
            {
                if (topic.Id == Id)
                {
                    return topic;
                }
            }
            return null;
        }
    }
}*/