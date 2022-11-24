namespace Server;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy  => { policy.WithOrigins("https://localhost:7251").AllowAnyHeader().AllowAnyMethod();});
            });

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddRazorPages();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<CommitTreeContext>(opt =>
            opt.UseSqlServer(Database.GetConnectionString()));
        builder.Services.AddScoped<ICommitDataRepo, CommitDataRepo>();
        // builder.Services.AddCors(options => {
        //     options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        // });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();

            endpoints.MapControllers();                
            endpoints.MapFallbackToFile("index.html");
        });

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CommitTreeContext>();
            Database.SetupDatabase(context);
        }

        app.Run();
    }
}
