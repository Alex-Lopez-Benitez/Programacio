using System;

abstract class Personaje
{
    protected static Random aleatorio = new Random();
    public string Nombre { get; protected set; }
    public int VidaMaxima { get; protected set; }
    public int VidaActual { get; protected set; }
    public int Nivel { get; protected set; }

    public Personaje(string nombre, int vida, int nivel)
    {
        Nombre = nombre;
        VidaMaxima = vida;
        VidaActual = vida;
        Nivel = nivel;
    }

    public virtual int Atacar()
    {
        return aleatorio.Next(1, 7) + aleatorio.Next(1, 7);
    }

    public virtual void RecibirDaño(int daño)
    {
        VidaActual -= daño;
        if (VidaActual < 0) VidaActual = 0;
    }

    public void MostrarEstado()
    {
        Console.WriteLine($"{Nombre} - Vida: {VidaActual}/{VidaMaxima} - Nivel: {Nivel}");
    }
}

class Jedi : Personaje
{
    public string ColorSable { get; private set; }
    public string EstiloCombate { get; private set; }

    public Jedi(string nombre, string colorSable, string estilo) : base(nombre, 12, 1)
    {
        ColorSable = colorSable;
        EstiloCombate = estilo;
    }

    public override int Atacar()
    {
        int baseAtaque = base.Atacar();
        if (EstiloCombate == "simple") return baseAtaque;
        if (EstiloCombate == "balanceado") return baseAtaque + 1;
        if (EstiloCombate == "inestable") return baseAtaque + 2;
        return baseAtaque;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Jedi {Nombre}, sable: {ColorSable}, estilo: {EstiloCombate}");
    }
}

class Soldado : Personaje
{
    public string Rango { get; private set; }

    public Soldado(string nombre, string rango) : base(nombre, 10, 1)
    {
        Rango = rango;
    }

    public override int Atacar()
    {
        return base.Atacar() + 1;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Soldado {Nombre}, tipo: {Rango}");
    }
}

class Enemigo : Personaje
{
    public Enemigo(string nombre) : base(nombre, aleatorio.Next(6, 13), aleatorio.Next(1, 6)) { }
}

class JuegoStarWars
{
    static void Main()
    {
        Console.WriteLine("Bienvenido a STAR WARS - Aventura por turnos");
        Console.Write("Introduce tu nombre: ");
        string nombre = Console.ReadLine();

        Console.WriteLine("Elige tu camino: (1) Jedi (2) Soldado");
        string faccion = Console.ReadLine();

        Personaje jugador;

        if (faccion == "1")
        {
            Console.WriteLine("Elige color de sable: azul, verde, rojo");
            string color = Console.ReadLine();
            Console.WriteLine("Elige estilo de combate: simple, balanceado, inestable");
            string estilo = Console.ReadLine();
            jugador = new Jedi(nombre, color, estilo);
            ((Jedi)jugador).MostrarInfo();
            Console.WriteLine("Empiezas como iniciado Jedi con estilo Shii-Cho");
        }
        else
        {
            Console.WriteLine("Elige tipo: recluta infanteria, tecnico, espia");
            string tipo = Console.ReadLine();
            jugador = new Soldado(nombre, tipo);
            ((Soldado)jugador).MostrarInfo();
            Console.WriteLine("Eres un recluta del Imperio");
        }

        Enemigo enemigo = new Enemigo("Droide Separatista");
        Console.WriteLine("Tu primer enemigo aparece: " + enemigo.Nombre);

        int turno = 1;
        while (jugador.VidaActual > 0 && enemigo.VidaActual > 0)
        {
            Console.WriteLine($"Turno {turno}");
            jugador.MostrarEstado();
            enemigo.MostrarEstado();

            Console.WriteLine("¿Deseas atacar este turno? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                int dañoJugador = jugador.Atacar();
                Console.WriteLine($"Atacas al enemigo y haces {dañoJugador} de daño");
                enemigo.RecibirDaño(dañoJugador);
            }

            if (enemigo.VidaActual > 0)
            {
                int dañoEnemigo = enemigo.Atacar();
                Console.WriteLine($"{enemigo.Nombre} te ataca y hace {dañoEnemigo} de daño");
                jugador.RecibirDaño(dañoEnemigo);
            }

            turno++;
        }

        if (jugador.VidaActual > 0)
        {
            Console.WriteLine("¡Victoria! Has derrotado a tu primer enemigo");
        }
        else
        {
            Console.WriteLine("Has sido derrotado... Que la Fuerza te acompañe");
        }
    }
}
