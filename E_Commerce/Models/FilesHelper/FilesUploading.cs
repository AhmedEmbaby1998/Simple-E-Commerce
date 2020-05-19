using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using E_Commerce.Models.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commerce.Models.FilesHelper
{
    public class FilesUploading
    {
        private IWebHostEnvironment _environment;
        private string _subDirectory;
        private List<IFormFile> _formFiles;
        private List<string> _keys { get; }
        public FilesUploading(IWebHostEnvironment env, string subDirectory)
        {
            _environment = env;
            _subDirectory = subDirectory;
            _keys=new List<string>();
        }

        public void Stage(IFormFile formFile)
        {
            _formFiles.Add(formFile);
        }

        public void StageRange(List<IFormFile> formFiles)
        {
            _formFiles = formFiles;
        }

        public IList<string> Save()
        {
            foreach (var formFile in _formFiles)
            {
                var key = Guid.NewGuid()+"_"+formFile.FileName;
                _keys.Add(key);
                var path = Path.Combine(_environment.WebRootPath, _subDirectory, key);
                formFile.CopyTo(new FileStream(path,FileMode.Create));
            }

            return _keys;
        }

        public List<T> GetFilesObjects<T>() where T:IInstance<T>, new()
        {
            var list=new List<T>();
            foreach (var key in _keys)
            {
                list.Add(new T().GetInstance(key));
            }

            return list;
        }
        
        
        
    }
}