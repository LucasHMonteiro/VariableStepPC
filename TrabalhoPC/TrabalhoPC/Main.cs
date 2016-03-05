using System;
using TrabalhoPC;

public static class Teste
{
    static void Main(String[] args)
    {
        Console.WriteLine("Passou");
        RungeKutta4 RK = new RungeKutta4(0.25, 0.5, 0);
        Ponto[] resp = RK.Executa();
        for (int i = 0; i < resp.Length; i++)
        {
            Console.WriteLine("("+resp[i].X +","+ resp[i].Y+")");
        }
        System.Threading.Thread.Sleep(50000);
        
        
        
    }
}
