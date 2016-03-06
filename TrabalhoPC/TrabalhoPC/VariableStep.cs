using System;
using TrabalhoPC;
using System.Collections;
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
        double w = Alfa;
        ArrayList ultimateResp = new ArrayList();
        t = A;
        h = Hmax;
        Flag = true;
        Last = false;
        RespRK = RK4.Executa(h, w, t);
        Nflag = true;
        i = 4;
        t = RespRK[2].X + h;
        while (Flag)
        {
            temp1 = RK4.CalculaDiferencial(A, Alfa);
            temp2 = RK4.CalculaDiferencial(RespRK[i - 2].X, RespRK[i - 2].Y);
            temp3 = RK4.CalculaDiferencial(RespRK[i - 3].X, RespRK[i - 3].Y);
            temp4 = RK4.CalculaDiferencial(RespRK[i - 4].X, RespRK[i - 4].Y);
            Wp = RespRK[i - 2].Y + (h / 24) * (55 * temp1 - 59 * temp2 + 37 * temp3 - 9 * temp4);
            temp1 = RK4.CalculaDiferencial(t, Wp);
            temp2 = RK4.CalculaDiferencial(RespRK[i - 2].X, RespRK[i - 2].Y);
            temp3 = RK4.CalculaDiferencial(RespRK[i - 3].X, RespRK[i - 3].Y);
            temp4 = RK4.CalculaDiferencial(RespRK[i - 4].X, RespRK[i - 4].Y);
            Wc = RespRK[i - 2].Y + (h / 24) * (9 * temp1 + 19 * temp2 - 5 * temp3 + temp4);
            Sigma = 19 * Math.Abs(Wc - Wp) / 270 * h;
            if (Sigma <= Tol)
            {
                w = Wc;

                if (Nflag)
                {
                    saida = new double[4];
                    saida[0] = 0;
                    saida[1] = A;
                    saida[2] = RespRK[0].Y;
                    saida[3] = h;
                    ultimateResp.Insert(0, saida);
                    for (int j = i - 3; j < i-1; j++)
                    {
                        saida = new double[4];
                        saida[0] = j;
                        saida[1] = RespRK[j].X;
                        saida[2] = RespRK[j].Y;
                        saida[3] = h;
                        ultimateResp.Insert(j, saida);
                    }
                }
                else
                {
                    saida = new double[4];
                    saida[0] = i;
                    saida[1] = RespRK[i].X;
                    saida[2] = RespRK[i].Y;
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

                    if ((Sigma <= 0.1 * Tol) || ((RespRK[2].X + h) > B))
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

                        if ((RespRK[2].X + 4 * h) > B)
                        {
                            h = (B - RespRK[2].X) / 4;
                            Last = true;
                        }

                        RespRK = RK4.Executa(h, RespRK[2].Y, RespRK[2].X);

                        Nflag = true;
                        i = i + 3;
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

                            RespRK = RK4.Executa(h, RespRK[2].Y, RespRK[2].X);
                            i = i + 3;
                            Nflag = true;
                        }

                        t = RespRK[i - 1].X + h;
                    }
                }
            }
        }








        return ultimateResp;
    }
}
