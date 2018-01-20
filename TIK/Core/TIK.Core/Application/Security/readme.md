### using ServiceCollection extension as below

// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    // using this command.
    services.JwtBearerAuthentication(Configuration);

    services.AddMvc();
}