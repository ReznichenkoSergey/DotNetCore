using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class FileKeyCreator : IFileKeyCreator
    {
        Dictionary<string, int> _fileKeys;

        public FileKeyCreator()
        {
            _fileKeys = new Dictionary<string, int>();
        }

        public string GetKey(string fileName)
        {
            if (!_fileKeys.ContainsKey(fileName))
            {
                _fileKeys.Add(fileName, fileName.GetHashCode());
            }
            return _fileKeys[fileName].ToString();
        }
    }
}
