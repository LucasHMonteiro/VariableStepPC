using System;
using TrabalhoPC;
using System.Collections;
using System.Collections.Generic;
public class VariableStep

{
    double A, B, Alfa, Sigma, Tol, Hmax, Hmin, Wp, Wc, q;

    double t, h;
    double[] saida;
    Boolean Flag, Last, Nflag;
    RungeKutta4 RK4 = new RungeKutta4();
    Ponto[] RespRK = new Ponto[4];
    int i;



    public ArrayList Executa(double extremoA, double extremoB, double alpha, double tolerancia, double Hmax, double Hmin)
    {
        A = extremoA;
        B = extremoB;
        Alfa = alpha;
        this.Hmax = Hmax;
        this.Hmin = Hmin;
        Tol = tolerancia;



        // saida[0] = t, saida[1] = w, saida[2] = h
        double temp1, temp2, temp3, temp4;
        //double w = Alfa;
        ArrayList ultimateResp = new ArrayList();
        List<Double> t = new List<Double>();
        List<Double> w = new List<Double>();
        
        t.Add(A);
        w.Add(Alfa);
        h = Hmax;
        Flag = true;
        Last = false;
        RespRK = RK4.Executa(h, w[0], t[0]);
        divideRespRK(t, w, RespRK);
        Nflag = true;
        i = 4;
        double Taux = t[3] + h;
        while (Flag)
        {
            
            temp1 = RK4.CalculaDiferencial(t[i-1], w[i-1]);
            temp2 = RK4.CalculaDiferencial(t[i-2], w[i-2]);
            temp3 = RK4.CalculaDiferencial(t[i-3], w[i-3]);
            temp4 = RK4.CalculaDiferencial(t[i-4], w[i-4]);
            Wp = w[i-1] + (h / 24) * ((55 * temp1) - (59 * temp2) + (37 * temp3) - (9 * temp4));
            temp1 = RK4.CalculaDiferencial(Taux, Wp);
            temp2 = RK4.CalculaDiferencial(t[i-1], w[i-1]);
            temp3 = RK4.CalculaDiferencial(t[i-2], w[i-2]);
            temp4 = RK4.CalculaDiferencial(t[i-3], w[i-3]);
            Wc = w[i-1] + (h / 24) * ((9 * temp1) + (19 * temp2) - (5 * temp3) + temp4);
            Sigma = 19 * (Math.Abs(Wc - Wp))/( 270*h); //Console.WriteLine(Sigma);
            if (Sigma <= Tol)
            {
                w[i] = Wc;
                t[i] = Taux;
                if (Nflag)
                {
                    saida = new double[4];
                    saida[0] = 0;
                    saida[1] = A;
                    saida[2] = w[0];
                    saida[3] = h;
                    ultimateResp.Insert(0, saida);
                    for (int j = i - 3; j < i - 1; j++)
                    {
                        saida = new double[4];
                        saida[0] = j;
                        saida[1] = t[j];
                        saida[2] = w[j];
                        saida[3] = h;
                        ultimateResp.Insert(j, saida);
                    }
                }
                else
                {
                    saida = new double[4];
                    saida[0] = i;
                    saida[1] = t[i];
                    saida[2] = w[i];
                    saida[3] = h;
                    ultimateResp.Insert(i, saida);
                }
                if (Last)
                {
                    Flag = false;

                }
                else
                {
                    i++;
                    Nflag = false;

                    if ((Sigma <= 0.1 * Tol) || ((t[i-1] + h) > B))
                    {
                        q = Math.Pow((Tol / (2 * Sigma)), 1 / 4);

                        if (q > 4)
                        {
                            h = 4 * h;
                        }
                        else
                        {
                            h = q * h;
                        }

                        if (h > Hmax)
                        {
                            h = Hmax;
                        }
                        

                        if ((t[i-1] + 4 * h) > B)
                        {
                            h = (B - t[i-1]) / 4;
                            Last = true;
                        }
                        
                        RespRK = RK4.Executa(h, w[i-1], t[i-1]); //passo 16
                        divideRespRK(t, w, RespRK);

                        Nflag = true;
                        i = i + 3;
                    }


                }
            }
            else
            {
                q = Math.Pow(Tol / (2 * Sigma), 1 / 4);
                if (q < 0.1)
                {
                    h = 0.1 / h;
                }
                else
                {
                    h = q * h;
                }

                if (h < Hmin)
                {
                    Flag = false;
                    Console.WriteLine("Him ultrapassado");
                }
                else
                {
                    if (Nflag)
                    {
                        i = i - 3;
                        
                    }
                    
                    RespRK = RK4.Executa(h, w[i-1], t[i-1]);
                    i = i + 3; 
                    Nflag = true;
                }
               // Console.WriteLine(i - 1);
                Taux = t[i-1] + h;
            }
        }








        return ultimateResp;
    }
    private void exibeResp(ArrayList ultimateResp)
    {
        double[] vaux = new double[4];
        for (int i = 0; i < ultimateResp.Count; i++)
        {
            vaux = (double[])ultimateResp[i];
            for (int j = 0; j < 4; j++)
            {
                Console.Write(vaux[j]+" ");
                Console.WriteLine("");
            }
        }
    }
    private void divideRespRK(List<Double> t, List<Double> w, Ponto[] RespRK)
    {
        for (int i = 0; i < RespRK.Length; i++)
        {
            t.Add(RespRK[i].X);
            w.Add(RespRK[i].Y);
        }
    }
}
