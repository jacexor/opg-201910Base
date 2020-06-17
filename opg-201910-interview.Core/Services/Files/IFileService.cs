using opg_201910_interview.Core.Enums;
using opg_201910_interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opg_201910_interview.Core.Services.Files
{
    public interface IFileService
    {
        Task<FileDateFormat> GetFilesDateFormatBaseByMajorityAsync(string path);

        FileDateFormat CheckFileFormat(string filePath);

        Task<List<File>> ReadFilesAsync(string path, FileDateFormat acceptedFormat);
    }
}
