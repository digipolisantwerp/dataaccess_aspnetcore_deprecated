using System;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.DataAccessJsonFileTests
{
    public class InstantiationTests
    {
        [Fact]
        private void FileNameIsSet()
        {
            var options = new DataAccessJsonFile("filename", "section");
            Assert.Equal("filename", options.FileName);
        }

        [Fact]
        private void SectionIsSet()
        {
            var options = new DataAccessJsonFile("filename", "section");
            Assert.Equal("section", options.Section);
        }

        [Fact]
        private void DefaultSectionIsDataAccess()
        {
            var options = new DataAccessJsonFile();
            Assert.Equal("DataAccess", options.Section);
        }

        [Fact]
        private void DefaultFileNameIsDbConfig()
        {
            var options = new DataAccessJsonFile();
            Assert.Equal("dbconfig.json", options.FileName);
        }
    }
} 
