using System;
using TrabalhoPC;
public class VariableStep

{
    double A, B, Alfa, Sigma, Tol, Hmax, Hmin, Wp, Wc;

    double t, w, h;
    
    Boolean Flag, Last, Nflag;
    RungeKutta4 RK4 = new RungeKutta4();
    Ponto[] RespRK = new Ponto[4];
    int i;
   
	public VariableStep()

	{
        // saida[0] = t, saida[1] = w, saida[2] = h
        double temp1,temp2,temp3,temp4;
        w = Alfa;
        t = A;
        h = Hmax;
        Flag = true;
        Last = false;
        RespRK = RK4.Executa(h, w, t);
        Nflag = true;
        i = 4;
        t = RespRK[2].X + w;
        while (Flag)
        {
            temp1 = RK4.CalculaDiferencial(RespRK[i - 1].X, RespRK[i - 1].Y);
            temp2 = RK4.CalculaDiferencial(RespRK[i - 2].X, RespRK[i - 2].Y);
            temp3 = RK4.CalculaDiferencial(RespRK[i - 3].X, RespRK[i - 3].Y);
            temp4 = RK4.CalculaDiferencial(RespRK[i - 4].X, RespRK[i - 4].Y);
            Wp = RespRK[i - 1].Y + (h/24) * (55 * temp1 - 59 * temp2 + 37 * temp3 - 9 * temp4);
            temp1 = RK4.CalculaDiferencial(t, Wp);
            temp2 = RK4.CalculaDiferencial(RespRK[i - 1].X, RespRK[i - 1].Y);
            temp3 = RK4.CalculaDiferencial(RespRK[i - 2].X, RespRK[i - 2].Y);
            temp4 = RK4.CalculaDiferencial(RespRK[i - 3].X, RespRK[i - 3].Y);
            Wc = RespRK[i - 1].Y + (Hmax / 24) * (9 * temp1 + 19 * temp2 - 5 * temp3 + temp4);
            Sigma = 19 * Math.Abs(Wc - Wp) / 270 * h);
            if (Sigma <= Tol)
            {
                w = Wc;
                
            }
        }
        
	}   
}
