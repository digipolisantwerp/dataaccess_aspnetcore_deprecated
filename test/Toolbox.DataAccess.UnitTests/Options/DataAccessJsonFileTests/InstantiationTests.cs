using System;
using Toolbox.DataAccess.Options;
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
        private void DefaultSectionIsSet()
        {
            var options = new DataAccessJsonFile();
            Assert.Equal(Defaults.DataAccessJsonFile.Section, options.Section);
        }

        [Fact]
        private void DefaultFileNameIsSet()
        {
            var options = new DataAccessJsonFile();
            Assert.Equal(Defaults.DataAccessJsonFile.FileName, options.FileName);
        }

        [Fact]
        private void DefaultDbConfigurationIsNull()
        {
            var options = new DataAccessJsonFile();
            Assert.Null(options.DbConfiguration);
        }
    }
} 
