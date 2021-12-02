using DataAccess.Data.Interfaces;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BugtrackerAPI;

public static class ApiServices
{
    public static WebApplication ConfigureDependencies(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Full setup of serilog. Read log files from appsettings.json
        builder.Host.UseSerilog((context, services, config) => config
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IMySqlDataAccess, MySqlDataAccess>();
        builder.Services.AddSingleton<IUserData, UserData>();
        builder.Services.AddSingleton<IAuthorData, AuthorData>();
        builder.Services.AddSingleton<ICommentData, CommentData>();
        builder.Services.AddSingleton<ICredentialData, CredentialData>();
        builder.Services.AddSingleton<IPostData, PostData>();
        builder.Services.AddSingleton<IProjectData, ProjectData>();
        builder.Services.AddSingleton<IProjectUserData, ProjectUserData>();
        builder.Services.AddSingleton<IUserRoleData, UserRoleData>();

        return builder.Build();
    }
}
