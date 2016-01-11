using System;
using System.Data.Entity;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess
{
    public class DataAccessJsonFile
    {
        public DataAccessJsonFile() : this(Defaults.DataAccessJsonFile.FileName, Defaults.DataAccessJsonFile.Section)
        { }

        public DataAccessJsonFile(string fileName, string section)
        {
            FileName = fileName;
            Section = section;
        }

        public string FileName { get; set; }
        public string Section { get; set; }

        public DbConfiguration DbConfiguration { get; set; }
    }
}
