using WebApplication1;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
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
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async(User loginData, ModelDB db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.EMail == loginData.EMail && p.Password == loginData.Password);
    if (person == null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.EMail!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.EMail
    };
    return Results.Json(response);  
});

app.MapGet("api/Admission", [Authorize] async (ModelDB db) => await db.Admissions!.ToListAsync());
app.MapGet("api/Sell", [Authorize] async (ModelDB db) => await db.SellOrders!.ToListAsync());
app.MapGet("api/Admission/{Id:int}", [Authorize] async (ModelDB db, int id) => await db.Admissions!.Where(u=>u.Id == id).FirstOrDefaultAsync());
app.MapPost("api/Admission", [Authorize] async (Admission admission, ModelDB db) =>
{ 
    await db.Admissions!.AddAsync(admission);
    await db.SaveChangesAsync();
    return admission;
});
app.MapPost("api/Sell", [Authorize] async (Sell sell, ModelDB db) =>
{
    await db.SellOrders!.AddAsync(sell);
    await db.SaveChangesAsync();
    return sell;
});
app.MapDelete("api/Admission/{Id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Admission? admission = await db.Admissions.FirstOrDefaultAsync(u=>u.Id==id);
    if (admission != null) return Results.NotFound(new { message = "Пользователь не найден" });
    db.Admissions.Remove(admission);
    await db.SaveChangesAsync();
    return Results.Json(admission);
});
app.MapDelete("api/Sell/{venorcode:int}", [Authorize] async (int venorcode, ModelDB db) =>
{
    Sell? sell = await db.SellOrders.FirstOrDefaultAsync(u => u.VenorCode == venorcode);
    if (sell != null) return Results.NotFound(new { message = "Продажа не найдена" });
    db.SellOrders.Remove(sell);
    await db.SaveChangesAsync();
    return Results.Json(sell);
});
app.MapPut("api/Admission", [Authorize] async (Admission admission, ModelDB db) =>
{
    Admission? a = await db.Admissions.FirstOrDefaultAsync(u => u.VenorCode == admission.VenorCode);
    if (a != null) return Results.NotFound(new { message = "Пользователь не найден" });
    a.VenorCode = admission.VenorCode;
    a.Price = admission.Price;  
    a.Name = admission.Name;
    await db.SaveChangesAsync();
    return Results.Json(a);
});
app.MapPut("api/Sell", [Authorize] async (Sell sell, ModelDB db) =>
{
    Sell? s = await db.SellOrders.FirstOrDefaultAsync(u => u.Id == sell.Id);
    if (s != null) return Results.NotFound(new { message = "Пользователь не найден" });
    s.VenorCode = sell.VenorCode;
    s.SellingDate = sell.SellingDate;
    s.Name = sell.Name;
    s.PriceOfSold = sell.PriceOfSold;
    s.CountOfSold = sell.CountOfSold;
    await db.SaveChangesAsync();
    return Results.Json(s);
});
app.Run();