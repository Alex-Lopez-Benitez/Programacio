using System;
using System.Collections.Generic;

class Program
{
    static List<Nave> listaNaves = new List<Nave>();
    static Queue<Nave> queueNaves = new Queue<Nave>();
    static Stack<Nave> stackNaves = new Stack<Nave>();

    static void Main(string[] args)
    {   
        while (true)
        {
            System.Console.WriteLine("Menu escriba 1 para generar una nave, 2 para generar 20 naves, 3 para contar naves, 4 para mostrar las naves, 5 borrar naves y 6 para salir ");
            int menu;
            menu = Convert.ToInt16(Console.ReadLine());
            switch (menu)
            {
                case 1: 
                        GenerarNave(); 
                break;
                case 2: GenerarNaves(20); 
                break;
                case 3: 
                        ContarNaves(); 
                break;
                case 4: 
                        MostrarNave(); 
                break;
                case 5:
                        BorrarNave(); 
                break;
                case 6:
                return;
                default:
                 break;
            }
        }
    }

    static void GenerarNave()
    {
        GenerarNaves(1);
    }

    static void GenerarNaves(int cantidad)
    {
        Random random = new Random(); 
        string[] numeros = { "1", "2", "3", "4", "5", "6", "7" };
        string[] modelos = { "halconmilenario", "cazaestelar", "super destructor", "ywing", "xwing" };

        for (int i = 0; i < cantidad; i++)
        {
            string nombreNave = numeros[random.Next(numeros.Length)] + numeros[random.Next(numeros.Length)] + random.Next(10, 99);
            string modeloNave = modelos[random.Next(modelos.Length)];

            Nave nuevaNave = new Nave(nombreNave, modeloNave);
            System.Console.WriteLine($"Nave generada: NOMBRE: {nombreNave} | MODELO: {modeloNave}");

            if (modeloNave == "halconmilenario")
                listaNaves.Add(nuevaNave);
            else if (modeloNave == "cazaestelar" || modeloNave == "super destructor" || modeloNave == "ywing")
                queueNaves.Enqueue(nuevaNave);
            else
                stackNaves.Push(nuevaNave);
        }
    }

    static void ContarNaves()
    {
        System.Console.WriteLine($"Total Halcón Milenario: {listaNaves.Count}");
        System.Console.WriteLine($"Total en Queue: {queueNaves.Count}");
        System.Console.WriteLine($"Total en Stack: {stackNaves.Count}");

        System.Console.WriteLine("Lista de naves en cada almacén:");

        System.Console.WriteLine("Lista Naves ");
        foreach (var nave in listaNaves)
            System.Console.WriteLine($"{nave.nombre} - {nave.modelo}");

        System.Console.WriteLine("Queue Naves ");
        foreach (var nave in queueNaves)
            System.Console.WriteLine($"{nave.nombre} - {nave.modelo}");

        System.Console.WriteLine("Stack Naves ");
        foreach (var nave in stackNaves)
            System.Console.WriteLine($"{nave.nombre} - {nave.modelo}");
    }

    static void MostrarNave()
    { 
        System.Console.WriteLine("Últimas naves en cada almacén:");
        
        if (listaNaves.Count > 0)
            System.Console.WriteLine($"Lista: {listaNaves[listaNaves.Count - 1].nombre} - {listaNaves[listaNaves.Count - 1].modelo}");
        else
            System.Console.WriteLine("Lista está vacía.");

        if (queueNaves.Count > 0)
            System.Console.WriteLine($"Queue: {queueNaves.Peek().nombre} - {queueNaves.Peek().modelo}");
        else
            System.Console.WriteLine("Queue está vacía.");

        if (stackNaves.Count > 0)
            System.Console.WriteLine($"Stack: {stackNaves.Peek().nombre} - {stackNaves.Peek().modelo}");
        else
            System.Console.WriteLine("Stack está vacía.");
    }

    static void BorrarNave()
    {
        System.Console.WriteLine("Elige almacén para borrar:1: Lista2: Queue3: Stack");
        int opcion = Convert.ToInt32(Console.ReadLine());

        switch (opcion)
        {
            case 1:
                if (listaNaves.Count > 0)
                {
                    listaNaves.RemoveAt(listaNaves.Count - 1);
                    System.Console.WriteLine("Última nave de la lista eliminada.");
                }
                else
                    System.Console.WriteLine("No hay naves en la lista.");
                break;
            case 2:
                if (queueNaves.Count > 0)
                {
                    queueNaves.Dequeue();
                    System.Console.WriteLine("Primera nave en la queue eliminada.");
                }
                else
                    System.Console.WriteLine("No hay naves en la queue.");
                break;
            case 3:
                if (stackNaves.Count > 0)
                {
                    stackNaves.Pop();
                    System.Console.WriteLine("Última nave en el stack eliminada.");
                }
                else
                    System.Console.WriteLine("No hay naves en el stack.");
                break;
            default:
                System.Console.WriteLine("Opción no válida.");
                break;
        }
    }
}

class Nave
{
    public string nombre { get; }
    public string modelo { get; }

    public Nave(string nombre, string modelo)
    {
        this.nombre = nombre;
        this.modelo = modelo;
    }
}
