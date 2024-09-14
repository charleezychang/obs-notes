using Microsoft.EntityFrameworkCore;

using Olympia.Persistence;

namespace Olympia.RequestPipeline
{
    public static class WebApplicationExtensions
    {
        public static WebApplication InitializeDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OlympiaContext>();
                db.Database.Migrate();
            }
            return app;
        }
    }
}
