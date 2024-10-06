# WebAppBlog

Descripción del Proyecto: 
● Esta aplicación permite gestionar publicaciones
de blog (BlogPost) y los comentarios asociados (PostComment). Los usuarios
pueden realizar búsquedas de publicaciones a través del campo Title y
obtener una lista paginada de publicaciones. Además, pueden visualizar los
comentarios de una publicación específica y agregar nuevos comentarios o
nuevos post.

Objetivo: 
● Proporcionar una API REST que permita a los clientes listar y
buscar publicaciones de blog, así como gestionar los comentarios
relacionados con cada publicación.

Tecnologías Utilizadas:
● Backend: ASP.NET Core (C#)
● Base de datos: En memoria
● Documentación API: Swagger
● Frameworks: Entity Framework Core

Modelos de Datos
BlogPost:
● Campos: Id, Title, PublishDate, List<PostComment>
● Relaciones: Un BlogPost puede tener muchos PostComments
PostComment:
● Campos: Id, BlogPostId, UserFullName, Comment
● Relaciones: Cada PostComment pertenece a un BlogPost (Clave
foránea).
