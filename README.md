# Fintrac
### Introduccion
FinTrac es una startup enfocada en simplificar la gestión de finanzas personales. Nuestra tarea fue crear un Producto Mínimo Viable (MVP) que reflejara la visión de FinTrac de hacer que la administración financiera sea tan fácil como enviar un mensaje de texto.
A lo largo de este proyecto, nos hemos enfocado en el desarrollo de software con C# y Blazor Server, adoptando el Desarrollo Guiado por Pruebas (TDD) y adhiriendo a los principios de Clean Code. Estos enfoques no solo han sido esenciales para construir una plataforma funcional y confiable, sino que también han enriquecido nuestras habilidades como desarrolladores.
Nos enfrentamos a diversos desafíos técnicos y de diseño, superándolos mediante soluciones innovadoras y trabajo en equipo efectivo. Este proceso nos ha permitido aplicar teorías y técnicas aprendidas en un contexto práctico, ofreciendo valiosas lecciones y experiencias.


### Pasos para la Instalación
Descarga del Código Fuente:

- Descargue el código fuente correspondiente al último release etiquetado para la
entrega actual.

Configuración de la Base de Datos:
- Localizar el archivo ZIP proporcionado (en la entrega por Gestión) que contiene los
backups de la base de datos.
- Restaure la base de datos utilizando el archivo .bak.

Configuración de Strings de Conexión:
- En la base del directorio Release_Fintrac , edite el archivo appsettings.json de la siguiente manera:

```{
"ConnectionStrings": {
"DefaultConnection": "Data
Source=*<DESKTOP-XXXXX>\\SQLEXPRESS;User
ID=<usuario>;Password=<password>;Database=<database-name>; Connect
Timeout=10;TrustServerCertificate=true"
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
},
"AllowedHosts": "*"
}
```

Asegúrese de reemplazar los valores ```<DESKTOP-XXXXX>``` por el nombre de su
equipo, ```<usuario>``` por el nombre de usuario creado en SQL Server, ```<password>```
por la contraseña del usuario de SQL Server, y ```<database-name>``` por el nombre
de la base de datos que haya creado en SQL Management Studio.
### Ejecución del Proyecto:
Localice el ejecutable WebUI.exe que se encuentra en Release_Fintrac y ejecutelo
- Una vez iniciada la aplicación, abra su navegador y navegue a
http://localhost:[puerto]/ para acceder a FinTrac.
- Verifique que la aplicación se conecte correctamente a la base de datos y que todas
las funcionalidades estén operativas.

***

#### Logros del Proyecto
Al concluir esta fase del desarrollo de FinTrac, nos complace destacar varios logros significativos. Hemos creado una buena solución para la gestión de finanzas personales, que no solo cumple con los requisitos establecidos, sino que también ofrece una experiencia de usuario intuitiva y eficiente. La integración de tecnologías avanzadas como Entity Framework y el enfoque en un diseño extensible han sido fundamentales en estos logros. 
#### Desafíos y Aprendizaje
El proyecto presentó desafíos únicos, especialmente en la transición a una persistencia de datos y en la adaptación a cambios de requisitos. A través de estos desafíos, hemos aprendido la importancia de una planificación flexible y la adaptabilidad en el desarrollo de software. La aplicación del TDD y los principios de Clean Code no solo mejoraron la calidad del código, sino que también facilitaron la gestión de estos desafíos.
#### Reflexiones sobre el Trabajo en Equipo
Cada miembro aportó sus fortalezas individuales, lo que permitió abordar una amplia gama de tareas técnicas y asegurar un progreso constante. Esta experiencia ha reforzado nuestra comprensión de la importancia de la comunicación y la cooperación en proyectos de desarrollo de software.
