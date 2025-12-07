using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using UmProject.Entities;

namespace UmProject.Web.Services
{
    public class PdfService : IPdfService
    {
        private const string ColorPrimario = "#0A1C33";
        private const string ColorSecundario = "#1E3A5F";
        private const string ColorAcento = "#D4AF37";
        private const string ColorFondo = "#F5F7FA";
        private const string ColorBlancoClaro = "#C8C8C8";

        static PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerarPdfUsuariosAsync(List<ReporteUsuario> usuarios, string titulo, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await Task.Run(() =>
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10f));

                        page.Header()
                            .Background(ColorPrimario)
                            .Padding(1f, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Item()
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text("SISTEMA DE GESTIÓN ACADÉMICA")
                                                    .FontSize(12f)
                                                    .FontColor(Colors.White)
                                                    .Bold();
                                                col.Item().Text("Gestión Integral de Evaluaciones y Becas")
                                                    .FontSize(9f)
                                                    .FontColor(ColorBlancoClaro);
                                            });
                                        row.AutoItem()
                                            .AlignRight()
                                            .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                            .FontSize(9f)
                                            .FontColor(ColorBlancoClaro);
                                    });
                                column.Item().PaddingTop(0.5f, Unit.Centimetre)
                                    .BorderBottom(2f)
                                    .BorderColor(ColorAcento)
                                    .Text(titulo)
                                    .FontSize(18f)
                                    .FontColor(Colors.White)
                                    .Bold();
                            });

                        page.Content()
                            .PaddingVertical(0.5f, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Spacing(8f);

                                column.Item()
                                    .Background(ColorFondo)
                                    .Padding(0.5f, Unit.Centimetre)
                                    .Column(infoCol =>
                                    {
                                        infoCol.Item().Text("Período de consulta:")
                                            .FontSize(9f)
                                            .FontColor("#323232");
                                        if (fechaInicio.HasValue && fechaFin.HasValue)
                                        {
                                            infoCol.Item().Text($"Desde: {fechaInicio.Value:dd/MM/yyyy} hasta: {fechaFin.Value:dd/MM/yyyy}")
                                                .FontSize(9f)
                                                .FontColor("#505050");
                                        }
                                        else
                                        {
                                            infoCol.Item().Text("Sin filtro de fechas")
                                                .FontSize(9f)
                                                .FontColor("#505050");
                                        }
                                    });

                                if (usuarios == null || usuarios.Count == 0)
                                {
                                    column.Item()
                                        .Background("#F0F0F0")
                                        .Padding(1f, Unit.Centimetre)
                                        .Text("No hay datos para mostrar en el período seleccionado.")
                                        .FontSize(12f)
                                        .FontColor("#646464")
                                        .AlignCenter();
                                }
                                else
                                {
                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(2.5f);
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(1.2f);
                                            columns.RelativeColumn(1.2f);
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Element(HeaderCellStyle).Text("Usuario").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Nombre Completo").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Documento").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Tipo Doc.").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Fecha Creación").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Estado").SemiBold();
                                        });

                                        int index = 0;
                                        foreach (var usuario in usuarios)
                                        {
                                            Func<IContainer, IContainer> cellStyle = index % 2 == 0 ? DataCellStyleEven : DataCellStyleOdd;
                                            table.Cell().Element(cellStyle).Text(usuario.Usuario ?? "");
                                            table.Cell().Element(cellStyle).Text(usuario.NombreCompleto ?? "");
                                            table.Cell().Element(cellStyle).Text(usuario.ValorDocumento ?? "");
                                            table.Cell().Element(cellStyle).Text(usuario.TipoDocumento ?? "");
                                            table.Cell().Element(cellStyle).Text(usuario.FechaCreacionUsuario ?? "");
                                            table.Cell().Element(cellStyle).Text(usuario.EstadoUsuario ?? "");
                                            index++;
                                        }
                                    });

                                    column.Item()
                                        .PaddingTop(0.5f, Unit.Centimetre)
                                        .Background(ColorSecundario)
                                        .Padding(0.5f, Unit.Centimetre)
                                        .Row(row =>
                                        {
                                            row.RelativeItem()
                                                .Text($"Total de registros: {usuarios.Count}")
                                                .FontSize(11f)
                                                .FontColor(Colors.White)
                                                .Bold();
                                            row.AutoItem()
                                                .Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                                .FontSize(9f)
                                                .FontColor(ColorBlancoClaro);
                                        });
                                }
                            });

                        page.Footer()
                            .Background(ColorPrimario)
                            .Padding(0.3f, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Item()
                                    .AlignCenter()
                                    .DefaultTextStyle(x => x.FontSize(8f).FontColor(ColorBlancoClaro))
                                    .Text(x =>
                                    {
                                        x.Span("Página ");
                                        x.CurrentPageNumber();
                                        x.Span(" de ");
                                        x.TotalPages();
                                    });
                                column.Item()
                                    .AlignCenter()
                                    .Text("Sistema de Gestión Académica - Documento confidencial para uso institucional")
                                    .FontSize(7f)
                                    .FontColor("#B4B4B4")
                                    .Italic();
                            });
                    });
                });

                return document.GeneratePdf();
            });
        }

        public async Task<byte[]> GenerarPdfPersonasAsync(List<ReportePersona> personas, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfUsuariosAsync(
                personas.Select(p => new ReporteUsuario
                {
                    Usuario = null,
                    NombreCompleto = p.NombreCompleto,
                    ValorDocumento = p.ValorDocumento,
                    TipoDocumento = p.TipoDocumento,
                    FechaCreacionUsuario = p.FechaCreacion,
                    EstadoUsuario = p.Estado
                }).ToList(),
                "REPORTE DE PERSONAS REGISTRADAS",
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfMateriasAsync(List<ReporteMateria> materias, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await Task.Run(() =>
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10f));

                        ConfigurarEncabezado(page, "REPORTE DE MATERIAS", fechaInicio, fechaFin);

                        page.Content()
                            .PaddingVertical(0.5f, Unit.Centimetre)
                            .Column(column =>
                            {
                                if (materias == null || materias.Count == 0)
                                {
                                    column.Item()
                                        .Background("#F0F0F0")
                                        .Padding(1f, Unit.Centimetre)
                                        .Text("No hay datos para mostrar.")
                                        .FontSize(12f)
                                        .FontColor("#646464")
                                        .AlignCenter();
                                }
                                else
                                {
                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(3f);
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(1.5f);
                                            columns.RelativeColumn(1.5f);
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Element(HeaderCellStyle).Text("Código").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Nombre").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Fecha Creación").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Fecha Modificación").SemiBold();
                                            header.Cell().Element(HeaderCellStyle).Text("Estado").SemiBold();
                                        });

                                        int index = 0;
                                        foreach (var materia in materias)
                                        {
                                            Func<IContainer, IContainer> cellStyle = index % 2 == 0 ? DataCellStyleEven : DataCellStyleOdd;
                                            table.Cell().Element(cellStyle).Text(materia.CodigoMateria ?? "");
                                            table.Cell().Element(cellStyle).Text(materia.NombreMateria ?? "");
                                            table.Cell().Element(cellStyle).Text(materia.FechaCreacion ?? "");
                                            table.Cell().Element(cellStyle).Text(materia.FechaModificacion ?? "");
                                            table.Cell().Element(cellStyle).Text(materia.Estado ?? "");
                                            index++;
                                        }
                                    });

                                    column.Item()
                                        .PaddingTop(0.5f, Unit.Centimetre)
                                        .Background(ColorSecundario)
                                        .Padding(0.5f, Unit.Centimetre)
                                        .Row(row =>
                                        {
                                            row.RelativeItem()
                                                .Text($"Total de registros: {materias.Count}")
                                                .FontSize(11f)
                                                .FontColor(Colors.White)
                                                .Bold();
                                            row.AutoItem()
                                                .Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                                .FontSize(9f)
                                                .FontColor(ColorBlancoClaro);
                                        });
                                }
                            });

                        ConfigurarPieDePagina(page);
                    });
                });

                return document.GeneratePdf();
            });
        }

        public async Task<byte[]> GenerarPdfPeriodosAsync(List<ReportePeriodo> periodos, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                periodos.Select(p => new ReporteMateria
                {
                    CodigoMateria = p.CodigoPeriodo,
                    NombreMateria = p.NombrePeriodo,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion,
                    Estado = p.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfSeccionesAsync(List<ReporteSeccion> secciones, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                secciones.Select(s => new ReporteMateria
                {
                    CodigoMateria = s.CodigoSeccion,
                    NombreMateria = $"{s.NombreMateria} - {s.Docente}",
                    FechaCreacion = s.FechaCreacion,
                    FechaModificacion = s.FechaModificacion,
                    Estado = s.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfGruposAsync(List<ReporteGrupo> grupos, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                grupos.Select(g => new ReporteMateria
                {
                    CodigoMateria = g.CodigoGrupo,
                    NombreMateria = g.NombreGrupo,
                    FechaCreacion = g.FechaCreacion,
                    FechaModificacion = g.FechaModificacion,
                    Estado = g.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfInscripcionesAsync(List<ReporteInscripcion> inscripciones, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                inscripciones.Select(i => new ReporteMateria
                {
                    CodigoMateria = i.CodigoInscripcion,
                    NombreMateria = $"{i.NombreEstudiante} - {i.TipoInscripcion}",
                    FechaCreacion = i.FechaCreacion,
                    FechaModificacion = null,
                    Estado = i.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfEvaluacionesAsync(List<ReporteEvaluacion> evaluaciones, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                evaluaciones.Select(e => new ReporteMateria
                {
                    CodigoMateria = e.CodigoRegistro,
                    NombreMateria = $"{e.NombreEvaluacion} - {e.NombreEstudiante}",
                    FechaCreacion = e.FechaCreacion,
                    FechaModificacion = null,
                    Estado = e.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfBecasProgramasAsync(List<ReporteBecaPrograma> programas, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                programas.Select(p => new ReporteMateria
                {
                    CodigoMateria = p.CodigoPrograma,
                    NombreMateria = p.NombrePrograma,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion,
                    Estado = p.EstadoPrograma
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfBecasConvocatoriasAsync(List<ReporteBecaConvocatoria> convocatorias, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                convocatorias.Select(c => new ReporteMateria
                {
                    CodigoMateria = c.CodigoConvocatoria,
                    NombreMateria = c.NombreConvocatoria,
                    FechaCreacion = c.FechaCreacion,
                    FechaModificacion = c.FechaModificacion,
                    Estado = c.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfBecasSolicitudesAsync(List<ReporteBecaSolicitud> solicitudes, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                solicitudes.Select(s => new ReporteMateria
                {
                    CodigoMateria = s.CodigoSeguimiento,
                    NombreMateria = $"{s.NombrePrograma} - {s.NombreEstudiante}",
                    FechaCreacion = s.FechaSolicitud,
                    FechaModificacion = null,
                    Estado = s.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfSancionesAsync(List<ReporteSancion> sanciones, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                sanciones.Select(s => new ReporteMateria
                {
                    CodigoMateria = s.CodigoSancion,
                    NombreMateria = $"{s.NombreEstudiante} - {s.TipoSancion}",
                    FechaCreacion = s.FechaCreacion,
                    FechaModificacion = s.FechaModificacion,
                    Estado = s.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        public async Task<byte[]> GenerarPdfTransaccionesAsync(List<ReporteTransaccion> transacciones, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await GenerarPdfMateriasAsync(
                transacciones.Select(t => new ReporteMateria
                {
                    CodigoMateria = t.IdTransaccion?.ToString() ?? "",
                    NombreMateria = t.Concepto ?? "",
                    FechaCreacion = t.FechaCreacion,
                    FechaModificacion = null,
                    Estado = t.Estado
                }).ToList(),
                fechaInicio,
                fechaFin);
        }

        private void ConfigurarEncabezado(PageDescriptor page, string titulo, DateTime? fechaInicio, DateTime? fechaFin)
        {
            page.Header()
                .Background(ColorPrimario)
                .Padding(1f, Unit.Centimetre)
                .Column(column =>
                {
                    column.Item()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Column(col =>
                                {
                                    col.Item().Text("SISTEMA DE GESTIÓN ACADÉMICA")
                                        .FontSize(12f)
                                        .FontColor(Colors.White)
                                        .Bold();
                                    col.Item().Text("Gestión Integral de Evaluaciones y Becas")
                                        .FontSize(9f)
                                        .FontColor(ColorBlancoClaro);
                                });
                            row.AutoItem()
                                .AlignRight()
                                .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                .FontSize(9f)
                                .FontColor(ColorBlancoClaro);
                        });
                    column.Item().PaddingTop(0.5f, Unit.Centimetre)
                        .BorderBottom(2f)
                        .BorderColor(ColorAcento)
                        .Text(titulo)
                        .FontSize(18f)
                        .FontColor(Colors.White)
                        .Bold();
                });
        }

        private void ConfigurarPieDePagina(PageDescriptor page)
        {
            page.Footer()
                .Background(ColorPrimario)
                .Padding(0.3f, Unit.Centimetre)
                .Column(column =>
                {
                    column.Item()
                        .AlignCenter()
                        .DefaultTextStyle(x => x.FontSize(8f).FontColor(ColorBlancoClaro))
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                    column.Item()
                        .AlignCenter()
                        .Text("Sistema de Gestión Académica - Documento confidencial para uso institucional")
                        .FontSize(7f)
                        .FontColor("#B4B4B4")
                        .Italic();
                });
        }

        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background(ColorSecundario)
                .Padding(0.3f, Unit.Centimetre)
                .BorderBottom(1f)
                .BorderColor(ColorAcento)
                .DefaultTextStyle(x => x.FontSize(9f).FontColor(Colors.White).Bold());
        }

        private static IContainer DataCellStyleEven(IContainer container)
        {
            return container
                .Background(Colors.White)
                .Padding(0.25f, Unit.Centimetre)
                .BorderBottom(0.5f)
                .BorderColor("#E0E0E0")
                .DefaultTextStyle(x => x.FontSize(9f).FontColor("#323232"));
        }

        private static IContainer DataCellStyleOdd(IContainer container)
        {
            return container
                .Background(ColorFondo)
                .Padding(0.25f, Unit.Centimetre)
                .BorderBottom(0.5f)
                .BorderColor("#E0E0E0")
                .DefaultTextStyle(x => x.FontSize(9f).FontColor("#323232"));
        }
    }
}

