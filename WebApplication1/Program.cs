using WebApplication1;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseAuthorization();
app.UseAuthentication();
app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    return new JwtSecurityTokenHandler().WriteToken(jwt);
});
app.Map("/hello", [Authorize] () => new {message="Hello world!"});
app.Map("/", () => "Home Page");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("api/Admission", async (ModelDB db) => await db.Admissions!.ToListAsync());
app.MapGet("api/Admission/{name}", async (ModelDB db, string name) => await db.Admissions!.Where(u=>u.Name==name).ToListAsync());
app.MapPost("api/Admission", async (Admission admission, ModelDB db) =>
{
    await db.Admissions!.AddAsync(admission);
    await db.SaveChangesAsync();
    return admission;
});
app.MapPost("api/Sell", async (Sell sell, ModelDB db) =>
{
    await db.SellOrders!.AddAsync(sell);
    await db.SaveChangesAsync();
    return sell;
});
app.MapDelete("api/Admission/{name}", async (int id, ModelDB db) =>
{
    Admission? admission = await db.Admissions.FirstOrDefaultAsync(u=>u.Id==id);
    if (admission != null) return Results.NotFound(new { message = "Пользователь не найден" });
    db.Admissions.Remove(admission);
    await db.SaveChangesAsync();
    return Results.Json(admission);
});
app.MapDelete("api/Sell/{name}", async (int id, ModelDB db) =>
{
    Sell? sell = await db.SellOrders.FirstOrDefaultAsync(u => u.Id == id);
    if (sell != null) return Results.NotFound(new { message = "Продажа не найден" });
    db.SellOrders.Remove(sell);
    await db.SaveChangesAsync();
    return Results.Json(sell);
});
app.MapPut("api/Admission", async (Admission admission, ModelDB db) =>
{
    Admission? a = await db.Admissions.FirstOrDefaultAsync(u => u.Id == admission.Id);
    if (admission != null) return Results.NotFound(new { message = "Пользователь не найден" });
    a.VenorCode = admission.VenorCode;
    a.Price = admission.Price;
    a.Name = admission.Name;
    await db.SaveChangesAsync();
    return Results.Json(a);
});
app.MapPut("api/Sell", async (Sell sell, ModelDB db) =>
{
    Sell? s = await db.SellOrders.FirstOrDefaultAsync(u => u.Id == sell.Id);
    if (sell != null) return Results.NotFound(new { message = "Пользователь не найден" });
    s.VenorCode = sell.VenorCode;
    s.SellingDate = sell.SellingDate;
    s.Name = sell.Name;
    s.PriceOfSold = sell.PriceOfSold;
    s.CountOfSold = sell.CountOfSold;
    await db.SaveChangesAsync();
    return Results.Json(s);
});

app.Run();