using System;

namespace Toolbox.DataAccess
{
    public class DataAccessJsonFile
    {
        public DataAccessJsonFile() : this("dbconfig.json", "DataAccess")
        { }

        public DataAccessJsonFile(string fileName, string section)
        {
            FileName = fileName;
            Section = section;
        }

        public string FileName { get; set; }
        public string Section { get; set; }
    }
}
