var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IMaterialRepository, MaterialRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();    
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarFront", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});        

var app = builder.Build();

app.UseCors("LiberarFront");

if (app.Environment.IsDevelopment())       
{                                            
    app.UseSwagger();                        
    app.UseSwaggerUI();                       
}                                             

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();