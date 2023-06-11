using Moq;
using System.Configuration;
using UtilsClasses;

namespace PlanerAppTests.Tests
{
    /// <summary>
    /// Provides unit tests for utility classes.
    /// </summary>
    public class UtilsTests
    {
        /// <summary>
        /// Tests the retrieval of a connection string value.
        /// </summary>
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

        /// <summary>
        /// Represents a wrapper interface for accessing configuration manager settings.
        /// </summary>
        public interface IConfigurationManagerWrapper
        {
            string GetConnectionString(string name);
        }

        /// <summary>
        /// Represents a wrapper implementation for accessing configuration manager settings.
        /// </summary>
        public class ConfigurationManagerWrapper : IConfigurationManagerWrapper
        {
            public string GetConnectionString(string name)
            {
                return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
            }
        }

        /// <summary>
        /// Represents a helper class for utility methods.
        /// </summary>
        public class Helper
        {
            private readonly IConfigurationManagerWrapper configurationManagerWrapper;

            /// <summary>
            /// Initializes a new instance of the Helper class.
            /// </summary>
            public Helper()
            {
            }

            /// <summary>
            /// Initializes a new instance of the Helper class with a configuration manager wrapper.
            /// </summary>
            /// <param name="configurationManagerWrapper">The configuration manager wrapper to use.</param>
            public Helper(IConfigurationManagerWrapper configurationManagerWrapper)
            {
                this.configurationManagerWrapper = configurationManagerWrapper;
            }

            /// <summary>
            /// Retrieves the connection string value for the specified name.
            /// </summary>
            /// <param name="name">The name of the connection string.</param>
            /// <returns>The connection string value.</returns>
            public string CnnValue(string name)
            {
                return configurationManagerWrapper.GetConnectionString(name);
            }
        }
    }
}
