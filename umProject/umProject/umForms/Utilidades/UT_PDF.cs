using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CAPA_ENTIDADES;

namespace umForms.Utilidades
{
    public static class UtPdf
    {
        // Colores institucionales académicos (usando valores hexadecimales)
        // #0A1C33 - Azul oscuro institucional
        private static string ColorPrimario => "#0A1C33";
        // #1E3A5F - Azul medio
        private static string ColorSecundario => "#1E3A5F";
        // #D4AF37 - Dorado académico
        private static string ColorAcento => "#D4AF37";
        // #F5F7FA - Gris claro
        private static string ColorFondo => "#F5F7FA";
        // #C8C8C8 - Gris claro para textos secundarios
        private static string ColorBlancoClaro => "#C8C8C8";

        static UtPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static void GenerarReporteUsuarios(List<CeReporteUsuario> usuarios, string titulo, string rutaArchivo, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1.5f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10f));

                    // Encabezado institucional
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

                            // Información del reporte
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
                                        infoCol.Item().Text($"Desde: {fechaInicio.Value.ToString("dd/MM/yyyy")} hasta: {fechaFin.Value.ToString("dd/MM/yyyy")}")
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

                                    // Encabezado de tabla
                                    table.Header(header =>
                                    {
                                        header.Cell().Element(HeaderCellStyle).Text("Usuario").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Nombre Completo").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Documento").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Tipo Doc.").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Fecha Creación").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Estado").SemiBold();
                                    });

                                    // Filas de datos con colores alternados
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

                                // Resumen
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
                                            .Text($"Generado: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}")
                                            .FontSize(9f)
                                            .FontColor(ColorBlancoClaro);
                                    });
                            }
                        });

                    // Pie de página institucional
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

            document.GeneratePdf(rutaArchivo);
        }

        public static void GenerarReporteAuditoria(List<CeReporteAuditoria> auditoria, string titulo, string rutaArchivo, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(1.2f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8f));

                    // Encabezado institucional
                    page.Header()
                        .Background(ColorPrimario)
                        .Padding(0.8f, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item()
                                .Row(row =>
                                {
                                    row.RelativeItem()
                                        .Column(col =>
                                        {
                                            col.Item().Text("SISTEMA DE GESTIÓN ACADÉMICA")
                                                .FontSize(11f)
                                                .FontColor(Colors.White)
                                                .Bold();
                                            col.Item().Text("Registro de Auditoría y Trazabilidad")
                                                .FontSize(8f)
                                                .FontColor(ColorBlancoClaro);
                                        });
                                    row.AutoItem()
                                        .AlignRight()
                                        .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                        .FontSize(8f)
                                        .FontColor(ColorBlancoClaro);
                                });
                            column.Item().PaddingTop(0.4f, Unit.Centimetre)
                                .BorderBottom(2f)
                                .BorderColor(ColorAcento)
                                .Text(titulo)
                                .FontSize(16f)
                                .FontColor(Colors.White)
                                .Bold();
                        });

                    page.Content()
                        .PaddingVertical(0.4f, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(6f);

                            // Información del reporte
                            column.Item()
                                .Background(ColorFondo)
                                .Padding(0.4f, Unit.Centimetre)
                                .Column(infoCol =>
                                {
                                    infoCol.Item().Text("Período de consulta:")
                                        .FontSize(8f)
                                        .FontColor("#323232");
                                    if (fechaInicio.HasValue && fechaFin.HasValue)
                                    {
                                        infoCol.Item().Text($"Desde: {fechaInicio.Value.ToString("dd/MM/yyyy")} hasta: {fechaFin.Value.ToString("dd/MM/yyyy")}")
                                            .FontSize(8f)
                                            .FontColor("#505050");
                                    }
                                    else
                                    {
                                        infoCol.Item().Text("Sin filtro de fechas")
                                            .FontSize(8f)
                                            .FontColor("#505050");
                                    }
                                });

                            if (auditoria == null || auditoria.Count == 0)
                            {
                                column.Item()
                                    .Background("#F0F0F0")
                                    .Padding(0.8f, Unit.Centimetre)
                                    .Text("No hay registros de auditoría para el período seleccionado.")
                                    .FontSize(11f)
                                    .FontColor("#646464")
                                    .AlignCenter();
                            }
                            else
                            {
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(0.8f);
                                        columns.RelativeColumn(2.2f);
                                        columns.RelativeColumn(2.5f);
                                        columns.RelativeColumn(1.5f);
                                        columns.RelativeColumn(1.5f);
                                        columns.RelativeColumn(1.5f);
                                        columns.RelativeColumn(1.2f);
                                        columns.RelativeColumn(1.2f);
                                        columns.RelativeColumn(0.8f);
                                    });

                                    // Encabezado de tabla
                                    table.Header(header =>
                                    {
                                        header.Cell().Element(HeaderCellStyle).Text("ID").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Tipo Transacción").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Concepto").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Tipo Entidad").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Persona").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Usuario").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Autor").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Fecha").SemiBold();
                                        header.Cell().Element(HeaderCellStyle).Text("Compl.").SemiBold();
                                    });

                                    // Filas de datos con colores alternados
                                    int index = 0;
                                    foreach (var item in auditoria)
                                    {
                                        Func<IContainer, IContainer> cellStyle = index % 2 == 0 ? DataCellStyleEven : DataCellStyleOdd;
                                        table.Cell().Element(cellStyle).Text(item.IdTransaccion?.ToString() ?? "");
                                        table.Cell().Element(cellStyle).Text(item.NombreTipoTransaccion ?? "");
                                        table.Cell().Element(cellStyle).Text(item.Concepto ?? "");
                                        table.Cell().Element(cellStyle).Text(item.TipoEntidad ?? "");
                                        table.Cell().Element(cellStyle).Text(item.NombrePersona ?? "");
                                        table.Cell().Element(cellStyle).Text(item.NombreUsuario ?? "");
                                        table.Cell().Element(cellStyle).Text(item.NombreAutor ?? "");
                                        table.Cell().Element(cellStyle).Text(item.FechaCreacion ?? "");
                                        table.Cell().Element(cellStyle).Text((item.Completado == true) ? "SI" : "NO");
                                        index++;
                                    }
                                });

                                // Resumen
                                column.Item()
                                    .PaddingTop(0.4f, Unit.Centimetre)
                                    .Background(ColorSecundario)
                                    .Padding(0.4f, Unit.Centimetre)
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Text($"Total de registros: {auditoria.Count}")
                                            .FontSize(10f)
                                            .FontColor(Colors.White)
                                            .Bold();
                                        row.AutoItem()
                                            .Text($"Generado: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}")
                                            .FontSize(8f)
                                            .FontColor(ColorBlancoClaro);
                                    });
                            }
                        });

                    // Pie de página institucional
                    page.Footer()
                        .Background(ColorPrimario)
                        .Padding(0.25f, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item()
                                .AlignCenter()
                                .DefaultTextStyle(x => x.FontSize(7f).FontColor(ColorBlancoClaro))
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
                                .FontSize(6f)
                                .FontColor("#B4B4B4")
                                .Italic();
                        });
                });
            });

            document.GeneratePdf(rutaArchivo);
        }

        // Estilos para celdas de encabezado
        static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background(ColorSecundario)
                .BorderBottom(1f)
                .BorderColor(ColorAcento)
                .PaddingVertical(6f)
                .PaddingHorizontal(4f)
                .AlignMiddle()
                .AlignCenter();
        }

        // Estilos para celdas de datos (filas pares)
        static IContainer DataCellStyleEven(IContainer container)
        {
            return container
                .Background(Colors.White)
                .BorderBottom(0.5f)
                .BorderColor("#DCDCDC")
                .PaddingVertical(4f)
                .PaddingHorizontal(4f)
                .AlignMiddle();
        }

        // Estilos para celdas de datos (filas impares)
        static IContainer DataCellStyleOdd(IContainer container)
        {
            return container
                .Background(ColorFondo)
                .BorderBottom(0.5f)
                .BorderColor("#DCDCDC")
                .PaddingVertical(4f)
                .PaddingHorizontal(4f)
                .AlignMiddle();
        }
    }
}

