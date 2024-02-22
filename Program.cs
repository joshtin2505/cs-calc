using System;
using System.IO;
using System.Security;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            string[] lines = GetInputFile();
            string outputFile = GetPath("Ingrese el nombre del archivo de salida (por ejemplo, Resultado.txt):");

            ConsoleLine();
            Console.WriteLine("Leyendo archivo de entrada...");

            // Si el archivo de salida existe, lo sobreescribimos 

            ConsoleLine();
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                Console.WriteLine("Realizando operaciones...");

                Operations(lines, writer);
            }
            ConsoleLine();

            Console.WriteLine("Operaciones realizadas con éxito. Puede revisar el archivo de salida.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error: " + ex.Message);
        }
    }
    static string[] GetInputFile()
    {
        string inputFile = GetPath("Ingrese el nombre del archivo de entrada (por ejemplo, Numero.txt):");
        try
        {
            while (!File.Exists(inputFile))
            {
                ConsoleWriteLineErr("El archivo no existe. Ingrese un archivo válido.");
                inputFile = GetPath("Ingrese el nombre del archivo de entrada (por ejemplo, Numero.txt):");
            }

            string[] lines = File.ReadAllLines(inputFile);
            while (lines.Length == 0)
            {
                ConsoleWriteLineErr("El archivo está vacío. Ingrese un archivo con contenido.");
                inputFile = GetPath("Ingrese el nombre del archivo de entrada (por ejemplo, Numero.txt):");
                lines = File.ReadAllLines(inputFile);
            }
            return lines;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Ocurrió un error: " + ex.Message);
        }
        
        return [];
    }
    static string GetPath(string placeholder)
    {
        Console.WriteLine(placeholder);
        string? file = Console.ReadLine();
        while (string.IsNullOrEmpty(file))
        {
            ConsoleWriteLineErr();
            Console.WriteLine(placeholder);
            file = Console.ReadLine();
        }
        return file;
    }
    static void Operations(string[] lines, StreamWriter writer)
    {
        foreach (string line in lines)
        {
            // Console.WriteLine(line);
            //por cada línea, separamos los números y el operador y si era 2+3 quedaria [2,3]
            string[] parts = line.Split(['+', '-', '*', '/']);
            
            if (parts.Length == 2) // Si hay dos partes, es una operación válida
            {
                if (double.TryParse(parts[0], out double num1) && double.TryParse(parts[1], out double num2)) // Si ambos números son válidos, realizamos la operación
                {
                    double result = 0;
                    char operation = line[parts[0].Length]; // El operador siempre estará en la posición de la longitud del primer número
                    switch (operation)
                    {
                        case '+':
                            result = num1 + num2;
                            break;
                        case '-':
                            result = num1 - num2;
                            break;
                        case '*':
                            result = num1 * num2;
                            break;
                        case '/':
                            if (num2 != 0)
                            {
                                result = num1 / num2;
                            }
                            else
                            {
                                writer.WriteLine("?");
                                continue;
                            }
                            break;
                        default:
                            writer.WriteLine("?");
                            continue;
                    }

                    writer.WriteLine(Math.Round(result,2));
                }
                else
                {
                    writer.WriteLine("?");
                }
            }
            else
            {
                writer.WriteLine("?");
            }
        }

    }
    static void ConsoleWriteLineErr(string message = "Los nombres de archivo no pueden estar vacíos.")
    {
        ConsoleLine();
        Console.WriteLine(message);
        ConsoleLine();
    }
    static void ConsoleLine()
    {
        Console.WriteLine("-----------------------------");
    }

}
