using AssetManagement.Models;

namespace AssetManagement.Api.Models
{
    public abstract class FileReader : IDataReader
    {
        private readonly string _path;
        public string Path
        {
            get
            {
                return _path;
            }
        }

        public FileReader(string path)
        {
            _path = path;
        }

        public abstract void Read();
    }
}
