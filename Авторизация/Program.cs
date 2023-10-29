using Microsoft.AspNetCore.Mvc;

namespace Авторизация
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(builder => builder.WithHeaders("password").AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.MapGet("/1", () =>
            //{
            //    using (var db = new applicationContext())
            //    {

            //    }

            //});

            app.MapPost("/voity", (string email, string password) =>
            {
                using (var db = new applicationContext())
                {
                    var row = db.users_persons.FirstOrDefault(x => x.Email == email && x.Password == password);
                    if (row == null)
                    {
                        return Results.BadRequest("bad!");
                    }
                    return Results.Ok("great!");
                }

            });

            app.MapPost("/regiz", (string email, string password, string repeat_password) =>
            {
                using (var db = new applicationContext())
                {

                    var row = db.users_persons.FirstOrDefault(x => x.Email == email);
                    if (row != null)
                    {
                        return Results.BadRequest("уже есть такой email!");
                    }

                    // если нету такого поль
                    if (repeat_password == password)
                    {
                        db.users_persons.Add(new Models.Users
                        {
                            Email = email,
                            Password = password,
                        });
                        db.SaveChanges();

                        return Results.Ok("great!");
                    }
                    return Results.BadRequest("bad!");
                }

            });

            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("password"))
                {
                    context.Response.StatusCode = 400; // Bad Request
                    return;
                }

                // Получаем значение заголовка
                var headerValue = context.Request.Headers["password"];

                if (headerValue == "12345")
                    await next();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}