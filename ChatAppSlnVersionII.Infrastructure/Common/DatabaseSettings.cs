using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Infrastructure.Common
{
    namespace ChatAppSlnVersionII.Infrastructure.ConfigService
    {
        public class DatabaseSettings
        {
            public string Server { get; set; } = string.Empty;
            public string Database { get; set; } = string.Empty;
            public string UserId { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public bool Trusted_Connection { get; set; } = true;
            public bool Encrypt { get; set; } = false;
            public bool TrustServerCertificate { get; set; } = true;
            public bool Pooling { get; set; } = true;
            public int MinPoolSize { get; set; } = 0;
            public int MaxPoolSize { get; set; } = 100;
            public string BuildConnectionString()
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = Server,
                    InitialCatalog = Database,
                    IntegratedSecurity = Trusted_Connection,
                    Encrypt = Encrypt,
                    TrustServerCertificate = TrustServerCertificate,
                    Pooling = Pooling,
                    MinPoolSize = MinPoolSize,
                    MaxPoolSize = MaxPoolSize
                };

                if (!Trusted_Connection)
                {
                    builder.UserID = UserId;
                    builder.Password = Password;
                }

                return builder.ConnectionString;

            }
        }
    }

}
