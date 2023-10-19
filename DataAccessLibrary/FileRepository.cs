using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DataAccessLibrary
{
    public class FileRepository : GenericRepository<UserFile>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private Semaphore _pool;
        public FileRepository(ApplicationDbContext context, IConfiguration config) : base(context)
        {
            _config = config;
            _context = context;

            if (!Semaphore.TryOpenExisting("DbContextSemaphore", out _pool))
                _pool = new Semaphore(1, 1, "DbContextSemaphore");
        }

        public async Task<List<UserFile>?> GetUserFilesByTopicIdAsync(int topicId)
        {
            List<UserFile> userFiles = await LoadData();

            if (!userFiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                userFiles = userFiles.Where(p => p.TopicId.Equals(topicId)).ToList();
            }
            _pool.Release();
            return userFiles;
        }

        public async Task<List<UserFile>?> GetUserFilesByFolderNameAsync(string folderName)
        {
            List<UserFile> userFiles = await LoadData();

            if (!userFiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                userFiles = userFiles.Where(p => p.FileName.Contains(folderName)).ToList();
            }
            _pool.Release();
            return userFiles;
        }

        public async Task<List<UserFile>?> GetUserFilesByTitleAsync(string title)
        {
            List<UserFile> userFiles = await LoadData();

            if (!userFiles.IsNullOrEmpty())
            {
                while (!_pool.WaitOne(TimeSpan.FromTicks(1)))
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                userFiles = userFiles.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            _pool.Release();
            return userFiles;
        }
        
        public string CreateFolder(string storage, string folderName)
        {
            try
            {
                if (!folderName.IsNullOrEmpty() && !storage.IsNullOrEmpty())
                {
                    var fullPath = String.Concat(storage, "\\", folderName);
                    if (Directory.Exists(fullPath))
                    {
                        return fullPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(fullPath);
                        return fullPath;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<string> GetFoldersInDir(string path)
        {
            if (!path.IsNullOrEmpty())
            {
                string [] subDirs = Directory.GetDirectories(path);
                List<string> result = new List<string>();
                foreach (string subDir in subDirs)
                {
                    var dir = subDir.Substring(path.Length);
                    result.Add(dir);
                }
                return result;
            }
            return null;
        }
    }
}
