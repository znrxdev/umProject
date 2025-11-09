using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_PERSONAS
    {
        private int? _IdPersona;
        private string? _PrimerNombre;
        private string? _SegundoNombre;
        private string? _PrimerApellido;
        private string? _SegundoApellido;
        private int? _IdTipoDocumento;
        private string? _ValorDocumento;
        private int? _IdGeneroPersona;
        private string? _FechaNacimiento;   
        private int? _IdNacionalidad;
        private int? _IdEstadoCivil;
        private string? _FechaCreacion;
        private string? _FechaModificacion;
        private int? _IdCreador;
        private int? _IdModificador;
        private int? _IdTransaccion;
        private int? _IdEstado;

        public int? Id_Persona { get => _IdPersona; set => _IdPersona = value; }
        public string? Primer_Nombre { get => _PrimerNombre; set => _PrimerNombre = value; }
        public string? Segundo_Nombre { get => _SegundoNombre; set => _SegundoNombre = value; }
        public string? Primer_Apellido { get => _PrimerApellido; set => _PrimerApellido = value; }
        public string? Segundo_Apellido { get => _SegundoApellido; set => _SegundoApellido = value; }
        public int? Id_Tipo_Documento { get => _IdTipoDocumento; set => _IdTipoDocumento = value; }
        public string? Valor_Documento { get => _ValorDocumento; set => _ValorDocumento = value; }
        public int? Id_Genero_Persona { get => _IdGeneroPersona; set => _IdGeneroPersona = value; }
        public int? Id_Nacionalidad { get => _IdNacionalidad; set => _IdNacionalidad = value; }
        public string? Fecha_Nacimiento { get => _FechaNacimiento; set => _FechaNacimiento = value; }
        public int? Id_Estado_Civil { get => _IdEstadoCivil; set => _IdEstadoCivil = value; }
        public string? Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string? Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }
        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }
        public int? Id_Estado { get => _IdEstado; set => _IdEstado = value; }
    }
}