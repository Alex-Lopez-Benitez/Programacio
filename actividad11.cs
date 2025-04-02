namespace GestionProyectos
{
    class Persona
    {
        public string Nombre { get; set; }
        public string DNI { get; set; }

        public Persona(string nombre, string dni)
        {
            Nombre = nombre;
            DNI = dni;
        }
    }

    class Cliente : Persona
    {
        public decimal PagoRealizado { get; set; }
        public decimal Adelanto { get; set; }

        public Cliente(string nombre, string dni, decimal pagoRealizado, decimal adelanto) 
            : base(nombre, dni)
        {
            PagoRealizado = pagoRealizado;
            Adelanto = adelanto;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, DNI: {DNI}, Pago realizado: {PagoRealizado:C}, Adelanto: {Adelanto:C}";
        }
    }

    class Proveedor : Persona
    {
        public string Recurso { get; set; }

        public Proveedor(string nombre, string dni, string recurso) 
            : base(nombre, dni)
        {
            Recurso = recurso;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, DNI: {DNI}, Recurso: {Recurso}";
        }
    }

    class Empleado
    {
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }

        public Empleado(string nombre, string cargo, decimal salario)
        {
            Nombre = nombre;
            Cargo = cargo;
            Salario = salario;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Cargo: {Cargo}, Salario: {Salario:C}";
        }
    }

    enum EstadoTarea
    {
        Pendiente,
        EnProgreso,
        Completada
    }

    class Tarea
    {
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string Descripcion { get; set; }

        public Tarea(string nombre, EstadoTarea estado, string descripcion)
        {
            Nombre = nombre;
            Estado = estado;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Estado: {Estado}, Descripción: {Descripcion}";
        }
    }

    enum EstadoProyecto
    {
        EnProgreso,
        Completado
    }

    class Proyecto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DiasRestantes { get; set; }
        public List<Empleado> Empleados { get; set; }
        public List<Tarea> Tareas { get; set; }
        public decimal CostoPorDia { get; set; }
        public EstadoProyecto Estado { get; set; }
        public Cliente Cliente { get; set; }
        public List<Proveedor> Proveedores { get; set; }

        public Proyecto(string nombre, string descripcion, int diasRestantes, decimal costoPorDia, EstadoProyecto estado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            DiasRestantes = diasRestantes;
            CostoPorDia = costoPorDia;
            Estado = estado;
            Empleados = new List<Empleado>();
            Tareas = new List<Tarea>();
            Proveedores = new List<Proveedor>();
        }

        public void AsignarCliente(Cliente cliente)
        {
            Cliente = cliente;
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            Proveedores.Add(proveedor);
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            Empleados.Add(empleado);
        }

        public void AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
        }

        public decimal CalcularCostoEstimado()
        {
            return CostoPorDia * DiasRestantes;
        }

        public decimal CalcularBalanceCliente()
        {
            if (Cliente == null)
                return 0;
            
            return CalcularCostoEstimado() - Cliente.PagoRealizado - Cliente.Adelanto;
        }

        public int ObtenerTareasPendientes()
        {
            return Tareas.Count(t => t.Estado == EstadoTarea.Pendiente);
        }

        public int ObtenerNumeroEmpleados()
        {
            return Empleados.Count;
        }

        public void MostrarInformacion()
        {
            System.Console.WriteLine($"informacion del proyecto: {Nombre}");
            System.Console.WriteLine($"Descripción: {Descripcion}");
            System.Console.WriteLine($"Días restantes: {DiasRestantes}");
            System.Console.WriteLine($"Estado actual: {Estado}");
            System.Console.WriteLine($"Costo estimado: {CalcularCostoEstimado():C}");
            System.Console.WriteLine($"Tareas pendientes: {ObtenerTareasPendientes()}");
            System.Console.WriteLine($"Número de empleados: {ObtenerNumeroEmpleados()}");
            
            // Mostrar información del cliente
            System.Console.WriteLine("Información del cliente:");
            if (Cliente != null)
            {
                System.Console.WriteLine($"- {Cliente}");
                System.Console.WriteLine($"  Balance pendiente: {CalcularBalanceCliente():C}");
            }
            else
            {
                System.Console.WriteLine("  No hay cliente asignado");
            }
            
            System.Console.WriteLine("Proveedores:");
            if (Proveedores.Count > 0)
            {
                foreach (var proveedor in Proveedores)
                {
                    System.Console.WriteLine($"- {proveedor}");
                }
            }
            else
            {
                System.Console.WriteLine("  No hay proveedores asignados");
            }
            
            System.Console.WriteLine("Empleados asignados:");
            foreach (var empleado in Empleados)
            {
                System.Console.WriteLine($"- {empleado}");
            }
            
            System.Console.WriteLine("Tareas del proyecto:");
            foreach (var tarea in Tareas)
            {
                System.Console.WriteLine($"- {tarea}");
            }
        }
    }

    class GestorProyectos
    {
        private List<Proyecto> proyectos;
        private List<Cliente> clientes;
        private List<Proveedor> proveedores;

        public GestorProyectos()
        {
            proyectos = new List<Proyecto>();
            clientes = new List<Cliente>();
            proveedores = new List<Proveedor>();
        }

        public void AgregarProyecto(Proyecto proyecto)
        {
            proyectos.Add(proyecto);
        }

        public void AgregarCliente(Cliente cliente)
        {
            clientes.Add(cliente);
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            proveedores.Add(proveedor);
        }

        public void MostrarProyectos()
        {
            System.Console.WriteLine("lista de los proyectos");
            foreach (var proyecto in proyectos)
            {
                System.Console.WriteLine($"- {proyecto.Nombre} ({proyecto.Estado})");
            }
        }

        public void MostrarClientes()
        {
            System.Console.WriteLine("lista de los clientes");
            foreach (var cliente in clientes)
            {
                System.Console.WriteLine($"- {cliente}");
            }
        }

        public void MostrarProveedores()
        {
            System.Console.WriteLine("lista de los provedores");
            foreach (var proveedor in proveedores)
            {
                System.Console.WriteLine($"- {proveedor}");
            }
        }

        public Proyecto BuscarProyecto(string nombre)
        {
            return proyectos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public Cliente BuscarCliente(string dni)
        {
            return clientes.FirstOrDefault(c => c.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase));
        }

        public Proveedor BuscarProveedor(string dni)
        {
            return proveedores.FirstOrDefault(p => p.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GestorProyectos gestor = new GestorProyectos();
            
            bool continuarPrograma = true;
            
            while (continuarPrograma)
            {
                System.Console.WriteLine("1. Crear nuevo proyecto");
                System.Console.WriteLine("2. Ver lista de proyectos");
                System.Console.WriteLine("3. Ver detalles de un proyecto");
                System.Console.WriteLine("4. Registrar nuevo cliente");
                System.Console.WriteLine("5. Ver lista de clientes");
                System.Console.WriteLine("6. Registrar nuevo proveedor");
                System.Console.WriteLine("7. Ver lista de proveedores");
                System.Console.WriteLine("8. Asignar cliente a proyecto");
                System.Console.WriteLine("9. Asignar proveedor a proyecto");
                System.Console.WriteLine("10. Salir");
                System.Console.Write("Seleccione una opción: ");
                
                string opcion = System.Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        CrearProyecto(gestor);
                        break;
                    case "2":
                        gestor.MostrarProyectos();
                        System.Console.WriteLine("Presione Enter para continuar");
                        System.Console.ReadLine();
                        break;
                    case "3":
                        VerDetallesProyecto(gestor);
                        break;
                    case "4":
                        RegistrarCliente(gestor);
                        break;
                    case "5":
                        gestor.MostrarClientes();
                        System.Console.WriteLine("Presione Enter para continuar");
                        System.Console.ReadLine();
                        break;
                    case "6":
                        RegistrarProveedor(gestor);
                        break;
                    case "7":
                        gestor.MostrarProveedores();
                        System.Console.WriteLine("Presione Enter para continuar");
                        System.Console.ReadLine();
                        break;
                    case "8":
                        AsignarClienteAProyecto(gestor);
                        break;
                    case "9":
                        AsignarProveedorAProyecto(gestor);
                        break;
                    case "10":
                        continuarPrograma = false;
                        break;
                    default:
                        System.Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
                
                System.Console.Clear();
            }
        }
        
        static void CrearProyecto(GestorProyectos gestor)
        {
            System.Console.WriteLine("crear un nuevo proyecto ");
            
            System.Console.Write("Nombre del proyecto: ");
            string nombre = System.Console.ReadLine();
            
            System.Console.Write("Descripción del proyecto: ");
            string descripcion = System.Console.ReadLine();
            
            System.Console.Write("Días restantes: ");
            int diasRestantes = int.Parse(System.Console.ReadLine());
            
            System.Console.Write("Costo por día: ");
            decimal costoPorDia = decimal.Parse(System.Console.ReadLine());
            
            System.Console.WriteLine("Estado del proyecto:");
            System.Console.WriteLine("1. En Progreso");
            System.Console.WriteLine("2. Completado");
            int estadoOpcion = int.Parse(System.Console.ReadLine());
            EstadoProyecto estado = (estadoOpcion == 2) ? EstadoProyecto.Completado : EstadoProyecto.EnProgreso;
            
            Proyecto nuevoProyecto = new Proyecto(nombre, descripcion, diasRestantes, costoPorDia, estado);
            
            // Agregar empleados
            bool agregarMasEmpleados = true;
            System.Console.WriteLine("Agregar empleados al proyecto:");
            
            while (agregarMasEmpleados)
            {
                System.Console.Write("Nombre del empleado: ");
                string nombreEmpleado = System.Console.ReadLine();
                
                System.Console.Write("Cargo del empleado: ");
                string cargoEmpleado = System.Console.ReadLine();
                
                System.Console.Write("Salario del empleado: ");
                decimal salarioEmpleado = decimal.Parse(System.Console.ReadLine());
                
                Empleado nuevoEmpleado = new Empleado(nombreEmpleado, cargoEmpleado, salarioEmpleado);
                nuevoProyecto.AgregarEmpleado(nuevoEmpleado);
                
                System.Console.Write("¿Desea agregar otro empleado? (S/N): ");
                agregarMasEmpleados = System.Console.ReadLine().ToUpper() == "S";
            }
            
            // Agregar tareas
            bool agregarMasTareas = true;
            System.Console.WriteLine("Agregar tareas al proyecto:");
            
            while (agregarMasTareas)
            {
                System.Console.Write("Nombre de la tarea: ");
                string nombreTarea = System.Console.ReadLine();
                
                System.Console.Write("Descripción de la tarea: ");
                string descripcionTarea = System.Console.ReadLine();
                
                System.Console.WriteLine("Estado de la tarea:");
                System.Console.WriteLine("1. Pendiente");
                System.Console.WriteLine("2. En Progreso");
                System.Console.WriteLine("3. Completada");
                int estadoTareaOpcion = int.Parse(System.Console.ReadLine());
                
                EstadoTarea estadoTarea;
                switch (estadoTareaOpcion)
                {
                    case 2:
                        estadoTarea = EstadoTarea.EnProgreso;
                        break;
                    case 3:
                        estadoTarea = EstadoTarea.Completada;
                        break;
                    default:
                        estadoTarea = EstadoTarea.Pendiente;
                        break;
                }
                
                Tarea nuevaTarea = new Tarea(nombreTarea, estadoTarea, descripcionTarea);
                nuevoProyecto.AgregarTarea(nuevaTarea);
                
                System.Console.Write("¿Desea agregar otra tarea s: si n:no ");
                agregarMasTareas = System.Console.ReadLine().ToUpper() == "S";
            }
            
            gestor.AgregarProyecto(nuevoProyecto);
            System.Console.WriteLine("Proyecto creado exitosamente.");
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
        
        static void RegistrarCliente(GestorProyectos gestor)
        {
            System.Console.WriteLine("Registrar cliente");
            
            System.Console.Write("Nombre del cliente: ");
            string nombre = System.Console.ReadLine();
            
            System.Console.Write("DNI del cliente: ");
            string dni = System.Console.ReadLine();
            
            System.Console.Write("Pagos realizados hasta el momento: ");
            decimal pagosRealizados = decimal.Parse(System.Console.ReadLine());
            
            System.Console.Write("Adelanto entregado: ");
            decimal adelanto = decimal.Parse(System.Console.ReadLine());
            
            Cliente nuevoCliente = new Cliente(nombre, dni, pagosRealizados, adelanto);
            gestor.AgregarCliente(nuevoCliente);
            
            System.Console.WriteLine("Cliente registrado exitosamente.");
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
        
        static void RegistrarProveedor(GestorProyectos gestor)
        {
            
            System.Console.Write("Nombre del proveedor: ");
            string nombre = System.Console.ReadLine();
            
            System.Console.Write("DNI del proveedor: ");
            string dni = System.Console.ReadLine();
            
            System.Console.Write("Recurso que suministra: ");
            string recurso = System.Console.ReadLine();
            
            Proveedor nuevoProveedor = new Proveedor(nombre, dni, recurso);
            gestor.AgregarProveedor(nuevoProveedor);
            
            System.Console.WriteLine("Proveedor registrado exitosamente.");
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
        
        static void AsignarClienteAProyecto(GestorProyectos gestor)
        {
            
            System.Console.Write("Ingrese el nombre del proyecto: ");
            string nombreProyecto = System.Console.ReadLine();
            
            Proyecto proyecto = gestor.BuscarProyecto(nombreProyecto);
            
            if (proyecto == null)
            {
                System.Console.WriteLine("Proyecto no encontrado.");
                System.Console.WriteLine("Presione Enter para continuar");
                System.Console.ReadLine();
                return;
            }
            
            System.Console.Write("Ingrese el DNI del cliente: ");
            string dniCliente = System.Console.ReadLine();
            
            Cliente cliente = gestor.BuscarCliente(dniCliente);
            
            if (cliente == null)
            {
                System.Console.WriteLine("Cliente no encontrado.");
                System.Console.WriteLine("Presione Enter para continuar");
                System.Console.ReadLine();
                return;
            }
            
            proyecto.AsignarCliente(cliente);
            System.Console.WriteLine("Cliente asignado al proyecto exitosamente.");
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
        
        static void AsignarProveedorAProyecto(GestorProyectos gestor)
        {
            
            System.Console.Write("Ingrese el nombre del proyecto: ");
            string nombreProyecto = System.Console.ReadLine();
            
            Proyecto proyecto = gestor.BuscarProyecto(nombreProyecto);
            
            if (proyecto == null)
            {
                System.Console.WriteLine("Proyecto no encontrado.");
                System.Console.WriteLine("Presione Enter para continuar");
                System.Console.ReadLine();
                return;
            }
            
            System.Console.Write("Ingrese el DNI del proveedor: ");
            string dniProveedor = System.Console.ReadLine();
            
            Proveedor proveedor = gestor.BuscarProveedor(dniProveedor);
            
            if (proveedor == null)
            {
                System.Console.WriteLine("Proveedor no encontrado.");
                System.Console.WriteLine("Presione Enter para continuar");
                System.Console.ReadLine();
                return;
            }
            
            proyecto.AgregarProveedor(proveedor);
            System.Console.WriteLine("Proveedor asignado al proyecto exitosamente.");
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
        
        static void VerDetallesProyecto(GestorProyectos gestor)
        {
            System.Console.Write("Ingrese el nombre del proyecto a buscar: ");
            string nombreProyecto = System.Console.ReadLine();
            
            Proyecto proyectoEncontrado = gestor.BuscarProyecto(nombreProyecto);
            
            if (proyectoEncontrado != null)
            {
                proyectoEncontrado.MostrarInformacion();
            }
            else
            {
                System.Console.WriteLine("Proyecto no encontrado.");
            }
            
            System.Console.WriteLine("Presione Enter para continuar");
            System.Console.ReadLine();
        }
    }
}

