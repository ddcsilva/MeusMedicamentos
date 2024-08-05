var builder = WebApplication.CreateBuilder(args);

// Adição de Serviços ao Container de Injeção de Dependência
builder.Services.AddControllers();

var app = builder.Build();

// Configuração da Pipeline de Requisição HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();