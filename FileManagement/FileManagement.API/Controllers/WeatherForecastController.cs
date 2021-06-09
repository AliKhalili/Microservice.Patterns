using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagement.Infrastructure;
using FileManagement.Infrastructure.Directory;

namespace FileManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public static Guid OwnerUserId=Guid.NewGuid();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly FileManagementContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, FileManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var root = new DirectoryEntity()
            {
                CreatedDateTime = DateTime.Now,
                EntryType = FileManagementEntryType.Directory,
                Name = Path.GetFileName(@"D:\Project"),
                OwnerUserId = OwnerUserId,
                ParentDirectoryId = null,
                Id = Guid.NewGuid()
            };
            _context.Directories.Add(root);
            _context.SaveChanges(true);
            TraverseDirectory(@"D:\Project", root);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        public void TraverseDirectory(
                string currentPath,
                DirectoryEntity parentDirectory
                )
        {
            var currentDirectory = new DirectoryEntity
            {
                CreatedDateTime = DateTime.Now,
                EntryType = FileManagementEntryType.Directory,
                Name = Path.GetFileName(currentPath),
                OwnerUserId = OwnerUserId,
                ParentDirectoryId = parentDirectory?.Id,
                Id = Guid.NewGuid()
            };
            var files = Directory.GetFiles(currentPath).Select(x => new FileEntity
            {
                CreatedDateTime = DateTime.Now,
                EntryType = FileManagementEntryType.File,
                Name = Path.GetFileName(x),
                OwnerUserId = OwnerUserId,
                ParentDirectoryId = currentDirectory.Id,
                Id = Guid.NewGuid()
            });
            var directories = Directory.GetDirectories(currentPath).Select(x => new DirectoryEntity
            {
                CreatedDateTime = DateTime.Now,
                EntryType = FileManagementEntryType.Directory,
                Name = Path.GetFileName(x),
                OwnerUserId = OwnerUserId,
                ParentDirectoryId = currentDirectory.Id,
                Id = Guid.NewGuid()
            });
            try
            {
                _context.Directories.Add(currentDirectory);
                _context.Directories.AddRange(directories);
                _context.Files.AddRange(files);
                _context.SaveChanges(true);
            }
            catch (Exception e)
            {
            }

            foreach (var childPath in Directory.GetDirectories(currentPath))
            {
                TraverseDirectory(childPath, currentDirectory);
            }
        }
    }
}
