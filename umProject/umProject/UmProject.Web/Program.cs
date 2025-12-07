using UmProject.Business;
using UmProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar servicios de datos
builder.Services.AddScoped<IConexionService, ConexionService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<ICatalogoRepository, CatalogoRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUsuarioRolRepository, UsuarioRolRepository>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<IErrorSqlRepository, ErrorSqlRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IDocenteRepository, DocenteRepository>();
builder.Services.AddScoped<IPeriodoAcademicoRepository, PeriodoAcademicoRepository>();
builder.Services.AddScoped<ISeccionRepository, SeccionRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IEvaluacionAlumnoRepository, EvaluacionAlumnoRepository>();
builder.Services.AddScoped<ISolicitudBecaRepository, SolicitudBecaRepository>();
builder.Services.AddScoped<ISancionAcademicaRepository, SancionAcademicaRepository>();
builder.Services.AddScoped<IBecaProgramaRepository, BecaProgramaRepository>();
builder.Services.AddScoped<IBecaCriterioRepository, BecaCriterioRepository>();
builder.Services.AddScoped<IEvaluacionInstanciaRepository, EvaluacionInstanciaRepository>();
builder.Services.AddScoped<IEvaluacionModeloRepository, EvaluacionModeloRepository>();

// Configurar servicios de negocio
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<ICatalogoService, CatalogoService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioRolService, UsuarioRolService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();
builder.Services.AddScoped<IErrorSqlService, ErrorSqlService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IDocenteService, DocenteService>();
builder.Services.AddScoped<IPeriodoAcademicoService, PeriodoAcademicoService>();
builder.Services.AddScoped<ISeccionService, SeccionService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
builder.Services.AddScoped<IEvaluacionAlumnoService, EvaluacionAlumnoService>();
builder.Services.AddScoped<ISolicitudBecaService, SolicitudBecaService>();
builder.Services.AddScoped<ISancionAcademicaService, SancionAcademicaService>();
builder.Services.AddScoped<IBecaProgramaService, BecaProgramaService>();
builder.Services.AddScoped<IBecaCriterioService, BecaCriterioService>();
builder.Services.AddScoped<IEvaluacionInstanciaService, EvaluacionInstanciaService>();
builder.Services.AddScoped<IEvaluacionModeloService, EvaluacionModeloService>();
builder.Services.AddScoped<UmProject.Web.Services.IPdfService, UmProject.Web.Services.PdfService>();

// Configurar sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
