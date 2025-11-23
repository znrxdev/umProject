import * as XLSX from 'xlsx'
import './App.css'

function App() {
  const handleGenerateExcel = () => {
    // Plan de proyecto basado en el enunciado de la asignatura
    // Columna clave para Gantt: Tarea, Fecha Inicio, Fecha Fin (el resto es contexto útil)
    const data = [
      // ENTREGA 1: Diagramación (12/10/2025)
      {
        ID: 1,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Análisis y Alcance',
        Tarea: 'Análisis de requisitos y alcance del sistema de control de evaluaciones',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-09-29',
        'Fecha Fin': '2025-10-02',
        Tipo: 'Tarea',
      },
      {
        ID: 2,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Casos de Uso',
        Tarea: 'Identificación de actores y casos de uso principales',
        Recurso: 'Analista de requisitos',
        'Fecha Inicio': '2025-10-02',
        'Fecha Fin': '2025-10-05',
        Tipo: 'Tarea',
      },
      {
        ID: 3,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Casos de Uso',
        Tarea: 'Redacción de descripciones detalladas de casos de uso',
        Recurso: 'Analista de requisitos',
        'Fecha Inicio': '2025-10-05',
        'Fecha Fin': '2025-10-08',
        Tipo: 'Tarea',
      },
      {
        ID: 4,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Diagrama de Clases',
        Tarea: 'Diseño del diagrama de clases del sistema',
        Recurso: 'Arquitecto de software',
        'Fecha Inicio': '2025-10-08',
        'Fecha Fin': '2025-10-11',
        Tipo: 'Tarea',
      },
      {
        ID: 5,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Planificación',
        Tarea: 'Elaboración del diagrama de Gantt del proyecto',
        Recurso: 'Líder de proyecto',
        'Fecha Inicio': '2025-10-11',
        'Fecha Fin': '2025-10-12',
        Tipo: 'Tarea',
      },
      {
        ID: 6,
        Entrega: 'Entrega 1 - Diagramación',
        Fase: 'Hito',
        Tarea: '== ENTREGA 1: Diagramación ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-10-12',
        'Fecha Fin': '2025-10-12',
        Tipo: 'Hito',
      },

      // ENTREGA 2: Datos - Factibilidad (19/10/2025)
      {
        ID: 7,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Modelo de Datos',
        Tarea: 'Diseño del modelo de datos lógico (tablas y relaciones)',
        Recurso: 'Arquitecto de datos',
        'Fecha Inicio': '2025-10-13',
        'Fecha Fin': '2025-10-15',
        Tipo: 'Tarea',
      },
      {
        ID: 8,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Modelo de Datos',
        Tarea: 'Normalización y definición de claves primarias y foráneas',
        Recurso: 'Arquitecto de datos',
        'Fecha Inicio': '2025-10-15',
        'Fecha Fin': '2025-10-17',
        Tipo: 'Tarea',
      },
      {
        ID: 9,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Modelo de Datos',
        Tarea: 'Elaboración del script SQL de creación de la base de datos',
        Recurso: 'Desarrollador backend',
        'Fecha Inicio': '2025-10-17',
        'Fecha Fin': '2025-10-18',
        Tipo: 'Tarea',
      },
      {
        ID: 10,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Arquitectura y UI',
        Tarea: 'Definición de la arquitectura general del sistema',
        Recurso: 'Arquitecto de software',
        'Fecha Inicio': '2025-10-13',
        'Fecha Fin': '2025-10-18',
        Tipo: 'Tarea',
      },
      {
        ID: 11,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Arquitectura y UI',
        Tarea: 'Diseño de interfaces gráficas de usuario principales',
        Recurso: 'Diseñador UI / UX',
        'Fecha Inicio': '2025-10-18',
        'Fecha Fin': '2025-10-19',
        Tipo: 'Tarea',
      },
      {
        ID: 12,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Estudio de Factibilidad',
        Tarea: 'Elaboración del estudio de factibilidad económica',
        Recurso: 'Líder de proyecto',
        'Fecha Inicio': '2025-10-15',
        'Fecha Fin': '2025-10-17',
        Tipo: 'Tarea',
      },
      {
        ID: 13,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Estudio de Factibilidad',
        Tarea: 'Elaboración del estudio de factibilidad técnica y operativa',
        Recurso: 'Equipo técnico',
        'Fecha Inicio': '2025-10-17',
        'Fecha Fin': '2025-10-19',
        Tipo: 'Tarea',
      },
      {
        ID: 14,
        Entrega: 'Entrega 2 - Datos y Factibilidad',
        Fase: 'Hito',
        Tarea: '== ENTREGA 2: Datos - Factibilidad ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-10-19',
        'Fecha Fin': '2025-10-19',
        Tipo: 'Hito',
      },

      // ENTREGA 3: Proceso de Desarrollo (09/11/2025)
      {
        ID: 15,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Preparación',
        Tarea: 'Configuración del repositorio y del entorno de desarrollo',
        Recurso: 'DevOps / Líder técnico',
        'Fecha Inicio': '2025-10-20',
        'Fecha Fin': '2025-10-21',
        Tipo: 'Tarea',
      },
      {
        ID: 16,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Backend',
        Tarea: 'Implementación de la capa de acceso a datos',
        Recurso: 'Desarrollador backend',
        'Fecha Inicio': '2025-10-22',
        'Fecha Fin': '2025-10-27',
        Tipo: 'Tarea',
      },
      {
        ID: 17,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Backend',
        Tarea: 'Implementación de la lógica de negocio principal',
        Recurso: 'Desarrollador backend',
        'Fecha Inicio': '2025-10-27',
        'Fecha Fin': '2025-11-02',
        Tipo: 'Tarea',
      },
      {
        ID: 18,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Auditoría y Trazabilidad',
        Tarea: 'Desarrollo del módulo de auditoría y trazabilidad (logs de acciones)',
        Recurso: 'Desarrollador backend',
        'Fecha Inicio': '2025-10-27',
        'Fecha Fin': '2025-11-05',
        Tipo: 'Tarea',
      },
      {
        ID: 19,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Módulo de Becas',
        Tarea: 'Implementación del módulo de evaluación de elegibilidad para becas',
        Recurso: 'Desarrollador backend',
        'Fecha Inicio': '2025-11-02',
        'Fecha Fin': '2025-11-09',
        Tipo: 'Tarea',
      },
      {
        ID: 20,
        Entrega: 'Entrega 3 - Proceso de Desarrollo',
        Fase: 'Hito',
        Tarea: '== ENTREGA 3: Proceso de Desarrollo ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-11-09',
        'Fecha Fin': '2025-11-09',
        Tipo: 'Hito',
      },

      // ENTREGA 4: Funcionalidad (16/11/2025)
      {
        ID: 21,
        Entrega: 'Entrega 4 - Funcionalidad',
        Fase: 'Integración',
        Tarea: 'Integración de frontend con backend (módulos principales)',
        Recurso: 'Desarrollador frontend',
        'Fecha Inicio': '2025-11-10',
        'Fecha Fin': '2025-11-13',
        Tipo: 'Tarea',
      },
      {
        ID: 22,
        Entrega: 'Entrega 4 - Funcionalidad',
        Fase: 'Pruebas Funcionales',
        Tarea: 'Pruebas funcionales internas del sistema',
        Recurso: 'Responsable de pruebas',
        'Fecha Inicio': '2025-11-13',
        'Fecha Fin': '2025-11-15',
        Tipo: 'Tarea',
      },
      {
        ID: 23,
        Entrega: 'Entrega 4 - Funcionalidad',
        Fase: 'Corrección de Errores',
        Tarea: 'Corrección de errores críticos detectados',
        Recurso: 'Equipo de desarrollo',
        'Fecha Inicio': '2025-11-15',
        'Fecha Fin': '2025-11-16',
        Tipo: 'Tarea',
      },
      {
        ID: 24,
        Entrega: 'Entrega 4 - Funcionalidad',
        Fase: 'Hito',
        Tarea: '== ENTREGA 4: Funcionalidad ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-11-16',
        'Fecha Fin': '2025-11-16',
        Tipo: 'Hito',
      },

      // ENTREGA 5: Pruebas e Implementación (23/11/2025)
      {
        ID: 25,
        Entrega: 'Entrega 5 - Pruebas e Implementación',
        Fase: 'Plan de Pruebas',
        Tarea: 'Elaboración del plan de pruebas y casos de prueba',
        Recurso: 'Responsable de pruebas',
        'Fecha Inicio': '2025-11-17',
        'Fecha Fin': '2025-11-18',
        Tipo: 'Tarea',
      },
      {
        ID: 26,
        Entrega: 'Entrega 5 - Pruebas e Implementación',
        Fase: 'Ejecución de Pruebas',
        Tarea: 'Ejecución de pruebas y registro de evidencias',
        Recurso: 'Equipo de pruebas',
        'Fecha Inicio': '2025-11-18',
        'Fecha Fin': '2025-11-21',
        Tipo: 'Tarea',
      },
      {
        ID: 27,
        Entrega: 'Entrega 5 - Pruebas e Implementación',
        Fase: 'Ajustes Finales',
        Tarea: 'Ajustes finales previos a despliegue',
        Recurso: 'Equipo de desarrollo',
        'Fecha Inicio': '2025-11-21',
        'Fecha Fin': '2025-11-22',
        Tipo: 'Tarea',
      },
      {
        ID: 28,
        Entrega: 'Entrega 5 - Pruebas e Implementación',
        Fase: 'Despliegue',
        Tarea: 'Elaboración del documento estratégico de implementación',
        Recurso: 'Líder de proyecto',
        'Fecha Inicio': '2025-11-21',
        'Fecha Fin': '2025-11-23',
        Tipo: 'Tarea',
      },
      {
        ID: 29,
        Entrega: 'Entrega 5 - Pruebas e Implementación',
        Fase: 'Hito',
        Tarea: '== ENTREGA 5: Pruebas e Implementación ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-11-23',
        'Fecha Fin': '2025-11-23',
        Tipo: 'Hito',
      },

      // ENTREGA 6: Informe Final y Presentación (07/12/2025)
      {
        ID: 30,
        Entrega: 'Entrega 6 - Informe Final y Presentación',
        Fase: 'Informe Final',
        Tarea: 'Redacción del informe final y lecciones aprendidas',
        Recurso: 'Líder de proyecto',
        'Fecha Inicio': '2025-11-25',
        'Fecha Fin': '2025-12-03',
        Tipo: 'Tarea',
      },
      {
        ID: 31,
        Entrega: 'Entrega 6 - Informe Final y Presentación',
        Fase: 'Manuales',
        Tarea: 'Elaboración del manual de instalación / configuración y videotutorial',
        Recurso: 'Equipo técnico',
        'Fecha Inicio': '2025-11-27',
        'Fecha Fin': '2025-12-05',
        Tipo: 'Tarea',
      },
      {
        ID: 32,
        Entrega: 'Entrega 6 - Informe Final y Presentación',
        Fase: 'Presentación',
        Tarea: 'Preparación de la presentación y demo funcional',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-12-04',
        'Fecha Fin': '2025-12-06',
        Tipo: 'Tarea',
      },
      {
        ID: 33,
        Entrega: 'Entrega 6 - Informe Final y Presentación',
        Fase: 'Hito',
        Tarea: '== ENTREGA 6: Informe Final y Presentación ==',
        Recurso: 'Equipo completo',
        'Fecha Inicio': '2025-12-07',
        'Fecha Fin': '2025-12-07',
        Tipo: 'Hito',
      },
    ]

    // Crear hoja y libro de Excel usando la librería xlsx
    const worksheet = XLSX.utils.json_to_sheet(data)
    const workbook = XLSX.utils.book_new()
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Gantt')

    const excelBuffer = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    })

    const blob = new Blob([excelBuffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    })
    const url = URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = 'Gantt_Proyecto_Sistema_Control_Evaluaciones.xlsx'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    URL.revokeObjectURL(url)
  }

  return (
    <div className="app-container">
      <h1>Generador de Gantt (Excel)</h1>
      <p>
        Pulsa el botón para generar y descargar el archivo CSV del plan de proyecto,
        compatible con Microsoft Excel.
      </p>
      <button className="btn-primary" onClick={handleGenerateExcel}>
        Generar Excel
      </button>
    </div>
  )
}

export default App
