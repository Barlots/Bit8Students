using System.Globalization;
using System.Linq;
using Bit8.Students.Common;
using Bit8.Students.Persistence;
using Bit8.Students.Query;
using Bit8.Students.Query.Students;
using Bit8.Students.Services;
using Bit8.Students.Services.Disciplines;
using Bit8.Students.Services.Semesters;
using Bit8.Students.Services.Students;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bit8.Students.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage)
                            .ToArray();

                        return new BadRequestObjectResult(errors);
                    };
                });
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            
            services.AddTransient<IBConfiguration>(x => new BConfiguration(Configuration));

            services.AddTransient<IDisciplineService, DisciplineService>();
            services.AddTransient<ISemesterService, SemesterService>();
            services.AddTransient<IStudentService, StudentService>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentQuery, StudentQuery>();

            services.AddTransient<IValidator<CreateDisciplineRequest>, CreateDisciplineRequestValidator>();
            services.AddTransient<IValidator<CreateSemesterRequest>, CreateSemesterRequestValidator>();
            services.AddTransient<IValidator<AssignToSemesterRequest>, AssignToSemesterRequestValidator>();
            services.AddTransient<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}