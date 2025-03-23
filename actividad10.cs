// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         Libros.IniciarLibreria();
//     }
// }

// class Libro
// {
//     public string Titulo { get; set; }
//     public string Autor { get; set; }
//     public int Año { get; set; }

//     public override string ToString()
//     {
//         return $"{Titulo};{Autor};{Año}";
//     }
// }

// class Libros
// {
//     static string archivoLibros = "libros.txt";
//     static List<Libro> librosTemporales = new List<Libro>();

//     public static void IniciarLibreria()
//     {
//         bool salir = false;

//         while (!salir)
//         {
//             System.Console.Clear();
//             System.Console.WriteLine(" SISTEMA DE LIBRERÍA ====");
//             System.Console.WriteLine("1. Añadir libros");
//             System.Console.WriteLine("2. Mostrar libros (ordenados por autor)");
//             System.Console.WriteLine("3. Salir");
//             System.Console.Write("Seleccione una opción: ");

//             string opcion = System.Console.ReadLine();

//             switch (opcion)
//             {
//                 case "1":
//                     AñadirLibros();
//                     break;
//                 case "2":
//                     MostrarLibros();
//                     break;
//                 case "3":
//                     salir = true;
//                     break;
//                 default:
//                     System.Console.WriteLine("Opción no válida. Pulse cualquier tecla para continuar..");
//                     System.Console.ReadKey();
//                     break;
//             }
//         }
//     }

//     static void AñadirLibros()
//     {
//         System.Console.Clear();
//         System.Console.WriteLine(" AÑADIR LIBROS ====");
//         System.Console.WriteLine("Ingrese los datos de los libros (deje el título en blanco para terminar):");

//         bool continuarAgregando = true;
//         while (continuarAgregando)
//         {
//             System.Console.Write("Título del libro (o Enter para terminar): ");
//             string titulo = System.Console.ReadLine();

//             if (string.IsNullOrWhiteSpace(titulo))
//             {
//                 continuarAgregando = false;
//             }
//             else
//             {
//                 System.Console.Write("Autor: ");
//                 string autor = System.Console.ReadLine();

//                 System.Console.Write("Año de publicación: ");
//                 if (int.TryParse(System.Console.ReadLine(), out int año))
//                 {
//                     Libro libro = new Libro
//                     {
//                         Titulo = titulo,
//                         Autor = autor,
//                         Año = año
//                     };

//                     librosTemporales.Add(libro);
//                     System.Console.WriteLine("Libro agregado temporalmente. Puede agregar otro o presionar Enter en el título para finalizar");
//                 }
//                 else
//                 {
//                     System.Console.WriteLine("Año inválido. Este libro no será agregado");
//                 }
//             }
//         }

//         if (librosTemporales.Count > 0)
//         {
//             GuardarLibrosEnArchivo();
//             System.Console.WriteLine($"Se han guardado {librosTemporales.Count} libros en el archivo");
//             librosTemporales.Clear();
//         }
//         else
//         {
//             System.Console.WriteLine("No se agregaron libros");
//         }

//         System.Console.WriteLine("Pulse cualquier tecla para continuar..");
//         System.Console.ReadKey();
//     }

//     static void GuardarLibrosEnArchivo()
//     {
//         try
//         {
//             using (StreamWriter writer = File.AppendText(archivoLibros))
//             {
//                 foreach (Libro libro in librosTemporales)
//                 {
//                     writer.WriteLine(libro.ToString());
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             System.Console.WriteLine($"Error al guardar en el archivo: {ex.Message}");
//         }
//     }

//     static void MostrarLibros()
//     {
//         System.Console.Clear();
//         System.Console.WriteLine(" LIBROS ORDENADOS POR AUTOR ====");

//         if (!File.Exists(archivoLibros))
//         {
//             System.Console.WriteLine("El archivo de libros no existe");
//             System.Console.WriteLine("Pulse cualquier tecla para continuar..");
//             System.Console.ReadKey();
//             return;
//         }

//         try
//         {
//             List<Libro> libros = new List<Libro>();
            
//             string[] lineas = File.ReadAllLines(archivoLibros);
            
//             foreach (string linea in lineas)
//             {
//                 string[] partes = linea.Split(';');
//                 if (partes.Length >= 3 && int.TryParse(partes[2], out int año))
//                 {
//                     Libro libro = new Libro
//                     {
//                         Titulo = partes[0],
//                         Autor = partes[1],
//                         Año = año
//                     };
//                     libros.Add(libro);
//                 }
//             }

//             libros = libros.OrderBy(l => l.Autor).ToList();

//             if (libros.Count == 0)
//             {
//                 System.Console.WriteLine("No hay libros en el archivo");
//             }
//             else
//             {
//                 System.Console.WriteLine("Libros ordenados por autor:");
//                 foreach (Libro libro in libros)
//                 {
//                     System.Console.WriteLine($"Título: {libro.Titulo}");
//                     System.Console.WriteLine($"Autor: {libro.Autor}");
//                     System.Console.WriteLine($"Año: {libro.Año}");
//                     System.Console.WriteLine("--------------------");
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             System.Console.WriteLine($"Error al leer el archivo: {ex.Message}");
//         }

//         System.Console.WriteLine("Pulse cualquier tecla para continuar..");
//         System.Console.ReadKey();
//     }
// }

internal class Program
{
    static List<string> localMessages = new List<string>();
    static string messagesFilePath = "mensajes.txt";

    static void Main(string[] args)
    {
        if (!File.Exists(messagesFilePath))
        {
            CreateInitialMessagesFile();
        }

        bool exit = false;
        while (!exit)
        {
            System.Console.Clear();
            System.Console.WriteLine(" SISTEMA BBS ====");
            System.Console.WriteLine("1. Añadir mensaje local");
            System.Console.WriteLine("2. Listar todos los usuarios");
            System.Console.WriteLine("3. Leer un mensaje local (por usuario)");
            System.Console.WriteLine("4. Leer todos los mensajes locales");
            System.Console.WriteLine("5. Pasar mensajes locales a archivo");
            System.Console.WriteLine("6. Leer todos los mensajes de archivo");
            System.Console.WriteLine("7. Salir");
            System.Console.Write("Seleccione una opción: ");

            string option = System.Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddLocalMessage();
                    break;
                case "2":
                    ListAllUsers();
                    break;
                case "3":
                    ReadLocalMessageByUser();
                    break;
                case "4":
                    ReadAllLocalMessages();
                    break;
                case "5":
                    SaveLocalMessagesToFile();
                    break;
                case "6":
                    ReadAllFileMessages();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    System.Console.WriteLine("Opción no válida. Pulse cualquier tecla para continuar..");
                    System.Console.ReadKey();
                    break;
            }
        }
    }

    static void CreateInitialMessagesFile()
    {
        List<string> initialMessages = new List<string>
        {
            "c3po;para r2d2;A veces simplemente no entiendo el comportamiento humano",
            "luke;para leia;Necesitamos reunirnos urgentemente para discutir la situación",
            "han;para chewbacca;Prepara el Halcón Milenario para partir mañana",
            "leia;para han;Tenemos que hablar sobre los planes de la resistencia",
            "r2d2;para c3po;Bip boop beep (Tengo información importante)"
        };

        File.WriteAllLines(messagesFilePath, initialMessages);
    }

    static void AddLocalMessage()
    {
        System.Console.Clear();
        System.Console.WriteLine(" AÑADIR MENSAJE LOCAL ====");
        
        System.Console.Write("Usuario remitente: ");
        string user = System.Console.ReadLine();
        
        System.Console.Write("Asunto: ");
        string subject = System.Console.ReadLine();
        
        System.Console.Write("Mensaje: ");
        string message = System.Console.ReadLine();

        string fullMessage = $"{user};{subject};{message}";
        localMessages.Add(fullMessage);

        System.Console.WriteLine("Mensaje añadido con éxito. Pulse cualquier tecla para continuar..");
        System.Console.ReadKey();
    }

    static void ListAllUsers()
    {
        System.Console.Clear();
        System.Console.WriteLine(" LISTA DE TODOS LOS USUARIOS ====");
        
        HashSet<string> localUsers = new HashSet<string>();
        foreach (string msg in localMessages)
        {
            string[] parte = msg.Split(';');
            if (parte.Length >= 1)
            {
                localUsers.Add(parte[0]);
            }
        }

        HashSet<string> fileUsers = new HashSet<string>();
        if (File.Exists(messagesFilePath))
        {
            string[] fileLines = File.ReadAllLines(messagesFilePath);
            foreach (string line in fileLines)
            {
                string[] parte = line.Split(';');
                if (parte.Length >= 1)
                {
                    fileUsers.Add(parte[0]);
                }
            }
        }

        HashSet<string> allUsers = new HashSet<string>(localUsers.Concat(fileUsers));
        
        if (allUsers.Count == 0)
        {
            System.Console.WriteLine("No hay usuarios registrados");
        }
        else
        {
            System.Console.WriteLine("Usuarios en el sistema:");
            foreach (string user in allUsers)
            {
                System.Console.WriteLine($"- {user}");
            }
        }

        System.Console.WriteLine("Pulse cualquier tecla para continuar..");
        System.Console.ReadKey();
    }

    static void ReadLocalMessageByUser()
    {
        System.Console.Clear();
        System.Console.WriteLine(" LEER MENSAJES LOCALES POR USUARIO ====");
        
        System.Console.Write("Ingrese el nombre del usuario: ");
        string searchUser = System.Console.ReadLine();

        bool foundMessages = false;
        foreach (string msg in localMessages)
        {
            string[] parte = msg.Split(';');
            if (parte.Length >= 3 && parte[0].Equals(searchUser, StringComparison.OrdinalIgnoreCase))
            {
                System.Console.WriteLine($"De: {parte[0]}");
                System.Console.WriteLine($"Asunto: {parte[1]}");
                System.Console.WriteLine($"Mensaje: {parte[2]}");
                System.Console.WriteLine("--------------------");
                foundMessages = true;
            }
        }

        if (!foundMessages)
        {
            System.Console.WriteLine($"No se encontraron mensajes locales del usuario '{searchUser}'");
        }

        System.Console.WriteLine("Pulse cualquier tecla para continuar..");
        System.Console.ReadKey();
    }

    static void ReadAllLocalMessages()
    {
        System.Console.Clear();
        System.Console.WriteLine("TODOS LOS MENSAJES LOCALES");
        
        if (localMessages.Count == 0)
        {
            System.Console.WriteLine("No hay mensajes locales");
        }
        else
        {
            foreach (string msg in localMessages)
            {
                string[] parte = msg.Split(';');
                if (parte.Length >= 3)
                {
                    System.Console.WriteLine($"De: {parte[0]}");
                    System.Console.WriteLine($"Asunto: {parte[1]}");
                    System.Console.WriteLine($"Mensaje: {parte[2]}");
                }
            }
        }

        System.Console.WriteLine("Pulse cualquier tecla para continuar..");
        System.Console.ReadKey();
    }

    static void SaveLocalMessagesToFile()
    {
        System.Console.Clear();
        System.Console.WriteLine(" GUARDAR MENSAJES LOCALES A ARCHIVO ====");
        
        if (localMessages.Count == 0)
        {
            System.Console.WriteLine("No hay mensajes locales para guardar");
        }
        else
        {
            using (StreamWriter writer = File.AppendText(messagesFilePath))
            {
                foreach (string msg in localMessages)
                {
                    writer.WriteLine(msg);
                }
            }

            System.Console.WriteLine($"Se han guardado {localMessages.Count} mensajes en el archivo");
            
            localMessages.Clear();
        }

        System.Console.WriteLine("Pulse cualquier tecla para continuar..");
        System.Console.ReadKey();
    }

    static void ReadAllFileMessages()
    {
        System.Console.Clear();
        System.Console.WriteLine("TODOS LOS MENSAJES DEL ARCHIVO");
        
        if (!File.Exists(messagesFilePath))
        {
            System.Console.WriteLine("El archivo de mensajes no existe");
        }
        else
        {
            string[] fileLines = File.ReadAllLines(messagesFilePath);
            
            if (fileLines.Length == 0)
            {
                System.Console.WriteLine("No hay mensajes en el archivo");
            }
            else
            {
                foreach (string line in fileLines)
                {
                    string[] parte = line.Split(';');
                    if (parte.Length >= 3)
                    {
                        System.Console.WriteLine($"De: {parte[0]}");
                        System.Console.WriteLine($"Asunto: {parte[1]}");
                        System.Console.WriteLine($"Mensaje: {parte[2]}");
                    }
                }
            }
        }

        System.Console.WriteLine("Pulsa cualquier tecla para continuar");
        System.Console.ReadKey();
    }
}