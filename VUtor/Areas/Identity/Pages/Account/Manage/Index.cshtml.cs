
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLibrary.Models;
using System.Diagnostics.CodeAnalysis;
using DataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace VUtor.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ProfileEntity> _userManager;
        private readonly SignInManager<ProfileEntity> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ProfileEntity> userManager,
            SignInManager<ProfileEntity> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }
        public List<TopicEntity> TopicList { get; set; } = new List<TopicEntity>();



        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [AllowNull]
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [AllowNull]
            [DataType(DataType.Text)]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [AllowNull]
            [Display(Name = "Course Name")]
            public CourseName CourseName { get; set; }

            [AllowNull]
            [Display(Name = "Course Year")]
            public CourseYear CourseYear { get; set; }

            [Display(Name = "Topic To Learn")]
            public int TopicToLearn { get; set; }

            [Display(Name = "Topic To Teach")]
            public int TopicToTeach { get; set; }

        }

        private async Task LoadAsync(ProfileEntity user)
        {
            TopicList = _context.Topics.ToList();
            Username = await _userManager.GetUserNameAsync(user);
            var profile = _context.Profiles.Where(p => p.Id == user.Id).Include(p => p.TopicsToLearn).Include(p => p.TopicsToTeach).First();
            Input = new InputModel
            {
                Name = user.Name,
                Surname = user.Surname,
                CourseName = (CourseName)user.CourseInfo.courseName,
                CourseYear = (CourseYear)user.CourseInfo.courseYear,
                TopicToLearn = profile.TopicsToLearn.First().Id,
                TopicToTeach = profile.TopicsToTeach.First().Id
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TopicList = _context.Topics.ToList();
            var user = await _userManager.GetUserAsync(User);
            var profile = _context.Profiles.Where(p => p.Id == user.Id).Include(p => p.TopicsToLearn).Include(p => p.TopicsToTeach).First();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.Surname != user.Surname)
            {
                user.Surname = Input.Surname;
            }

            var newCourseInfo = new CourseData((int)Input.CourseName, (int)Input.CourseYear);
            if (!user.CourseInfo.Equals(newCourseInfo))
            {
                user.CourseInfo = newCourseInfo;
            }

            var TopicLearn = _context.Topics.Where(p => p.Id == Input.TopicToLearn).First();
            if (!profile.TopicsToLearn.Contains(TopicLearn))
            {
                var currentTopic = profile.TopicsToLearn.FirstOrDefault();
                var removedTopic = _context.Topics.Where(p => p.Id == currentTopic.Id).Include(p => p.LearningProfiles).First();
                if (TopicLearn != null)
                {
                    removedTopic.LearningProfiles.Remove(user);
                    user.TopicsToLearn.Remove(removedTopic);
                    user.TopicsToLearn.Add(TopicLearn);
                    TopicLearn.LearningProfiles.Add(user);
                }
            }

            var TopicTeach = _context.Topics.Where(p => p.Id == Input.TopicToTeach).First();
            if (!user.TopicsToTeach.Contains(TopicTeach))
            {
                var currentTopic = profile.TopicsToTeach.FirstOrDefault();
                var removedTopic = _context.Topics.Where(p => p.Id == currentTopic.Id).Include(p => p.TeachingProfiles).First();
                if (TopicTeach != null)
                {
                    removedTopic.TeachingProfiles.Remove(user);
                    user.TopicsToTeach.Remove(removedTopic);
                    user.TopicsToTeach.Add(TopicTeach);
                    TopicTeach.TeachingProfiles.Add(user);
                }
            }
            await _context.SaveChangesAsync();
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}