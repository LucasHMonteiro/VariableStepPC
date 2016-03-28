using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathos.Parser;

namespace TrabalhoPC{

    class CalculosAuxiliares{

        private String function;
        private MathParser parser = new MathParser();
        private double K1, K2, K3, K4;
        public double H { get; set; }
        public double V0 { get; set; }
        public double X0 { get; set; }

        public CalculosAuxiliares(String function){
          this.function = function;
        }

        public Ponto[] RungeKutta4(double H, double V0, double X0){
            double[] VectorX = new double[4];
            double[] VectorV = new double[4];
            VectorX[0] = X0;
            VectorV[0] = V0;
            for (int i = 1; i <= 3; i++){
                K1 = H * CalculaDiferencial(VectorX[i - 1], VectorV[i - 1]);
                K2 = H * CalculaDiferencial(VectorX[i - 1] + H / 2, VectorV[i - 1] + K1 / 2);
                K3 = H * CalculaDiferencial(VectorX[i - 1] + H / 2, VectorV[i - 1] + K2 / 2);
                K4 = H * CalculaDiferencial(VectorX[i - 1] + H, VectorV[i - 1] + K3);
                VectorV[i] = VectorV[i - 1] + (K1 + 2 * K2 + 2 * K3 + K4) / 6;
                VectorX[i] = VectorX[0] + i * H;
            }
            Ponto[] resp = new Ponto[3];
            for (int i = 1; i <= 3; i++){
                resp[i - 1] = new Ponto(VectorX[i], VectorV[i]);
            }
            return resp;
        }
        public double CalculaDiferencial(double X, double Y){
            String x = Convert.ToString(X);
            String y = Convert.ToString(Y);
            String curr = this.function.Replace("x", x).Replace("y", y);
            curr = curr.Replace(",", ".");
            //return Y - Math.Pow(X, 2) + 1;
            double response = parser.Parse(curr);
            return response;
            //  return 1+ Math.Pow((X-Y),2);
        }
    }
}
