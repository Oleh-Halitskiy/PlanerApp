using Moq;
using System.Configuration;
using UtilsClasses;

namespace PlanerAppTests.Tests
{
    public class UtilsTests
    {
        [Fact]
        public void CnnValue_ValidName_ReturnsConnectionString()
        {
            // Arrange
            string expectedConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            string connectionName = "MyConnection";

            var configurationManagerMock = new Mock<IConfigurationManagerWrapper>();
            configurationManagerMock.Setup(c => c.GetConnectionString(connectionName))
                .Returns(expectedConnectionString);

            var helper = new Helper(configurationManagerMock.Object);

            // Act
            string result = helper.CnnValue(connectionName);

            // Assert
            Assert.Equal(expectedConnectionString, result);
        }

        public interface IConfigurationManagerWrapper
        {
            string GetConnectionString(string name);
        }

        public class ConfigurationManagerWrapper : IConfigurationManagerWrapper
        {
            public string GetConnectionString(string name)
            {
                return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
            }
        }

        public class Helper
        {
            private readonly IConfigurationManagerWrapper configurationManagerWrapper;

            public Helper()
            {
            }

            public Helper(IConfigurationManagerWrapper configurationManagerWrapper)
            {
                this.configurationManagerWrapper = configurationManagerWrapper;
            }

            public string CnnValue(string name)
            {
                return configurationManagerWrapper.GetConnectionString(name);
            }
        }

    }
}
