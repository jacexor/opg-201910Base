using opg_201910_interview.Core.Enums;
using opg_201910_interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace opg_201910_interview.Core.Services.Files
{
    public class FileService : IFileService
    {
        public readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public FileDateFormat CheckFileFormat(string filePath)
        {
            return Path.GetFileName(filePath).Split("-").Length == 4 ? FileDateFormat.DashedFormat : FileDateFormat.NoSpaceFormat;
        }

        public async Task<FileDateFormat> GetFilesDateFormatBaseByMajorityAsync(string path)
        {
            return await Task.Run(() =>
            {
                var dic = new List<FileDateFormat>();
                var p = _hostingEnvironment.ContentRootPath + "\\" + path.Replace("/", "\\");

                foreach (var dir in Directory.GetFiles(p))
                {
                    dic.Add(CheckFileFormat(dir));
                }

                var counts = dic.GroupBy(p => p)
                    .Select(x => new
                    {
                        Format = x.Key,
                        Count = x.Count()
                    }).OrderByDescending(p => p.Count);
             
                return counts.FirstOrDefault().Format;
            });
        }

        public async Task<List<Models.File>> ReadFilesAsync(string path, FileDateFormat acceptedFormat)
        {
            return await Task.Run(() =>
            {
                var files = new List<Models.File>();

                var p = _hostingEnvironment.ContentRootPath + "\\" + path.Replace("/", "\\");

                foreach (var dir in Directory.GetFiles(p))
                {
                    var name = Path.GetFileName(dir).Replace(".xml", "");

                    if (CheckFileFormat(dir) == acceptedFormat)
                    {
                        switch (acceptedFormat)
                        {
                            case FileDateFormat.DashedFormat:
                                files.Add(new Models.File()
                                {
                                    Name = name.Split('-')[0],
                                    Date = new DateTime(int.Parse(name.Split('-')[1]), int.Parse(name.Split('-')[2]), int.Parse(name.Split('-')[3]))
                                });
                                break;
                            case FileDateFormat.NoSpaceFormat:
                                files.Add(new Models.File()
                                {
                                    Name = name.Split('_')[0],
                                    Date = new DateTime(int.Parse(name.Split('_')[1].Substring(0, 4)), int.Parse(name.Split('_')[1].Substring(4, 2)), int.Parse(name.Split('_')[1].Substring(6, 2)))
                                });
                                break;
                        }
                    }
                }

                return files;
            });
        }
    }
}
