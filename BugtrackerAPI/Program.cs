using BugtrackerAPI;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IMySqlDataAccess, MySqlDataAccess>();
//builder.Services.AddSingleton<IUserData, UserData>();
//builder.Services.AddSingleton<IAuthorData, AuthorData>();
//builder.Services.AddSingleton<ICommentData, CommentData>();
//builder.Services.AddSingleton<ICredentialData, CredentialData>();
//builder.Services.AddSingleton<IPostData, PostData>();
//builder.Services.AddSingleton<IProjectData, ProjectData>();
//builder.Services.AddSingleton<IProjectUserData, ProjectUserData>();
//builder.Services.AddSingleton<IUserRoleData, UserRoleData>();

var app = ApiServices.ConfigureDependencies(args);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
