using System.Configuration;

namespace BudgetManager.Core.Configuration
{
    public class XMLConfig : IConfig
    {
        public XMLConfig()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            ConnectionString = connectionStrings["default"].ConnectionString;
        }

        public string ConnectionString { get; private set; }
    }
}