using Fire.Business.Abstrack;
using Fire.Business.Concrete;
using Fire.DataAccess.Abstrack;
using Fire.DataAccess.Concrete.Ef;
using Fire.UI.Filter;
using Fire.UI.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.UI
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

            services.AddAuthentication
                (CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/User/Login";
                    x.Cookie.Name = $".apppjwt";
                    x.AccessDeniedPath = "Error/{0}";
                    x.SlidingExpiration = true;
                });
            services.AddControllersWithViews();
            services.AddCors();
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });
            #region

            services.AddScoped<AuthorizationAttribute>();
            services.AddScoped<IBankDal, EfBankDal>();
            services.AddTransient<IBankService, BankManager>();

            services.AddScoped<ICheckDal, EfCheckDal>();
            services.AddTransient<ICheckService, CheckManager>();

            services.AddScoped<IPaymentContDal, EfPaymentConDal>();
            services.AddTransient<IPaymentContService, PaymentContManager>();

            services.AddScoped<IPaymentDal, EfPaymentDal>();
            services.AddTransient<IPaymentService, PaymentManager>();

            services.AddScoped<IStockTrackingDal, EfStockTrackingDal>();
            services.AddTransient<IStockTrackingService, StockTrackingManager>();

            services.AddScoped<IFactoryDal, EfFactoryDal>();
            services.AddTransient<IFactoryService, FactoryManager>();

            services.AddScoped<IFactoryQuantityDal, EfFactoryQuantityDal>();
            services.AddTransient<IFactoryQuantityService, FactoryQuantityManager>();

            services.AddScoped<ICustomerDal, EfCustomerDal>();
            services.AddTransient<ICustomerService, CustomerManager>();

            services.AddScoped<IEarningsDal, EfEarningsDal>();
            services.AddTransient<IEarningsService, EarningsManager>();

            services.AddScoped<IEmployeesDal, EfEmployeesDal>();
            services.AddTransient<IEmployeesService, EmployeesManager>();

            services.AddScoped<IExpensesDal, EfExpensesDal>();
            services.AddTransient<IExpensesService, ExpensesManager>();

            services.AddScoped<IProductQuantityDal, EfProductQuantityDal>();
            services.AddTransient<IProductQuantityService, ProductQuantityManager>();

            services.AddScoped<IProductTypeDal, EfProductTypeDal>();
            services.AddTransient<IProductTypeService, ProductTypeManager>();

            services.AddScoped<IUserDal, EfUserDal>();
            services.AddTransient<IUserService, UserManager>();

            services.AddScoped<IProductQuantityDal, EfProductQuantityDal>();
            services.AddTransient<IProductQuantityService, ProductQuantityManager>();


            services.AddScoped<IUserRoleDal, EfUserRoleDal>();
            services.AddTransient<IUserRoleService, UserRoleManager>();

            services.AddScoped<IReceiptDal, EfReceiptDal>();
            services.AddTransient<IReceiptService, ReceiptManager>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCustomException();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
