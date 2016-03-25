using System;
using TrabalhoPC;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Web.UI;
public class VariableStep{

    double A, B, Alfa, Sigma, Tol, Hmax, Hmin, Wp, Wc, q, h;
    double[] saida;
    Boolean Flag, Last, Nflag;
    CalculosAuxiliares RK4 = new CalculosAuxiliares();
    Ponto[] RespRK = new Ponto[4];
    int i;

    public ArrayList Executa(double extremoA, double extremoB, double alpha, double tolerancia, double Hmax, double Hmin){
        A = extremoA;
        B = extremoB;
        Alfa = alpha;
        this.Hmax = Hmax;
        this.Hmin = Hmin;
        Tol = tolerancia;
        double temp1, temp2, temp3, temp4;
        ArrayList ultimateResp = new ArrayList();
        List<Double> t = new List<Double>();
        List<Double> w = new List<Double>();
        t.Add(A);
        w.Add(Alfa);
        h = Hmax;
        Flag = true;
        Last = false;
        RespRK = RK4.RungeKutta4(h, w[0], t[0]);
        divideRespRK(t, w, RespRK);
        Nflag = true;
        i = 4;
        double Taux = t[3] + h;
        while (Flag){//passo 4
            temp1 = RK4.CalculaDiferencial(t[i - 1], w[i - 1]); //passo 5
            temp2 = RK4.CalculaDiferencial(t[i - 2], w[i - 2]);
            temp3 = RK4.CalculaDiferencial(t[i - 3], w[i - 3]);
            temp4 = RK4.CalculaDiferencial(t[i - 4], w[i - 4]);
            Wp = w[i - 1] + (h / 24d) * ((55d * temp1) - (59d * temp2) + (37d * temp3) - (9d * temp4));
            temp1 = RK4.CalculaDiferencial(Taux, Wp);
            temp2 = RK4.CalculaDiferencial(t[i - 1], w[i - 1]);
            temp3 = RK4.CalculaDiferencial(t[i - 2], w[i - 2]);
            temp4 = RK4.CalculaDiferencial(t[i - 3], w[i - 3]);
            Wc = w[i - 1] + (h / 24d) * ((9d * temp1) + (19d * temp2) - (5d * temp3) + temp4);
            Sigma = 19d * (Math.Abs(Wc - Wp)) / (270d * h);
            Console.Write("h = " +h+"\n");
            Console.Write("wp = " +Wp+"\n");
            Console.Write("wc = " +Wc+"\n");
            Console.Write("sigma = " +Sigma+"\n");
            if (Sigma <= Tol){  //Passo 6
                w.Insert(i, Wc); //Passo 7
                t.Insert(i, Taux);
                if (Nflag){      //Passo 8
                    saida = new double[4];
                    saida[0] = i-4;
                    saida[1] = t[i - 4];
                    saida[2] = w[i - 4];
                    saida[3] = h;
                    ultimateResp.Add(saida);
                    for (int j = i - 3; j < i; j++){
                        saida = new double[4];
                        saida[0] = j;
                        saida[1] = t[j];
                        saida[2] = w[j];
                        saida[3] = h;
                        ultimateResp.Add(saida);
                    }
                }else{
                    saida = new double[4];
                    saida[0] = i-1;
                    saida[1] = t[i-1];
                    saida[2] = w[i-1];
                    saida[3] = h;
                    ultimateResp.Insert(i-1, saida);
                }
                if (Last){       //Passo 9
                    Flag = false;
                }else{
                    i++;
                    Nflag = false;
                    if ((Sigma <=( 0.1 * Tol)) || ((t[i - 1] + h) > B)){ //passo 11
                        double qAux = Tol / (2d * Sigma);
                        q = Math.Pow(qAux, 0.25);//passo 12
                        if (q > 4){//passo 13
                            h = 4d * h;
                        }else{
                            h = q * h;
                        }
                        if (h > Hmax){//passo 14
                            h = Hmax;
                        }
                        if ((t[i - 1] + 4d * h) > B){//passo 15
                            h = (B - t[i - 1]) / 4d;
                            Last = true;
                        }
                        RespRK = RK4.RungeKutta4(h, w[i - 1], t[i - 1]); //passo 16
                        divideRespRK(t, w, RespRK);
                        Nflag = true;
                        i = i + 3;
                    }
                }
            }else{  //Passo 17
                double qAux = Tol / (2d * Sigma);
                q = Math.Pow(qAux, 0.25);
                if (q < 0.1){        //Passo 18
                    h = 0.1 * h;
                }else{
                    h = q * h;
                }
                if (h < Hmin){      //passo 19
                    Flag = false;
                    return null;
                }
                else{
                    if (Nflag){
                        i = i - 3;
                        t.RemoveAt(i + 2);
                        t.RemoveAt(i + 1);
                        t.RemoveAt(i);
                        w.RemoveAt(i + 2);
                        w.RemoveAt(i + 1);
                        w.RemoveAt(i);
                    }
                    RespRK = RK4.RungeKutta4(h, w[i - 1], t[i - 1]);
                    divideRespRK(t, w, RespRK);
                    i = i + 3;
                    Nflag = true;
                }
            }
            Taux = t[i - 1] + h;        //Passo 20
        }
        saida = new double[4];
        saida[0] = i;
        saida[1] = t[i];
        saida[2] = w[i];
        saida[3] = h;
        ultimateResp.Insert(i, saida);
        return ultimateResp;
    }
    public void toString(ArrayList ultimateResp){
        double[] vaux = new double[4];
        for (int i = 0; i < ultimateResp.Count; i++){
            vaux = (double[])ultimateResp[i];
            Console.Write(vaux[0] + " "+vaux[1] + " " + vaux[2] + " " + vaux[3]);
            Console.WriteLine("");
        }
    }
    public void toXML(ArrayList ultimateResp, string fileName){
      if(ultimateResp == null){
        throw new System.Exception();
      }
      using(XmlWriter writer = XmlWriter.Create(fileName)){
        writer.WriteStartDocument();
        writer.WriteStartElement("solutions");
        foreach(Double[] solution in ultimateResp){
          writer.WriteStartElement("solution");
          writer.WriteElementString("i", solution[0].ToString());
          writer.WriteElementString("Ti", solution[1].ToString());
          writer.WriteElementString("Wi", solution[2].ToString());
          writer.WriteElementString("h", solution[3].ToString());
          writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.WriteEndDocument();
      }
    }
    public void toHTML(ArrayList ultimateResp, string fileName){
      if(ultimateResp == null){
        throw new System.Exception();
      }
      StringWriter stringWriter = new StringWriter();

      using(HtmlTextWriter writer = new HtmlTextWriter(stringWriter)){
        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
        writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%");
        writer.RenderBeginTag(HtmlTextWriterTag.Table);//abre table

        writer.RenderBeginTag(HtmlTextWriterTag.Tr);//abre headers
        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
        writer.RenderBeginTag(HtmlTextWriterTag.Th);
        writer.Write("i");
        writer.RenderEndTag();
        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
        writer.RenderBeginTag(HtmlTextWriterTag.Th);
        writer.Write("Ti");
        writer.RenderEndTag();
        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
        writer.RenderBeginTag(HtmlTextWriterTag.Th);
        writer.Write("Wi");
        writer.RenderEndTag();
        writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
        writer.RenderBeginTag(HtmlTextWriterTag.Th);
        writer.Write("h");
        writer.RenderEndTag();
        writer.RenderEndTag();//fecha headers
        foreach(Double[] solution in ultimateResp){
          writer.RenderBeginTag(HtmlTextWriterTag.Tr);//abre row
          writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
          writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
          writer.RenderBeginTag(HtmlTextWriterTag.Td);
          writer.Write(solution[0].ToString());
          writer.RenderEndTag();
          writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
          writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
          writer.RenderBeginTag(HtmlTextWriterTag.Td);
          writer.Write(solution[1].ToString());
          writer.RenderEndTag();
          writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
          writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
          writer.RenderBeginTag(HtmlTextWriterTag.Td);
          writer.Write(solution[2].ToString());
          writer.RenderEndTag();
          writer.AddAttribute(HtmlTextWriterAttribute.Border, "1px");
          writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
          writer.RenderBeginTag(HtmlTextWriterTag.Td);
          writer.Write(solution[3].ToString());
          writer.RenderEndTag();
          writer.RenderEndTag();//fecha row
        }
        writer.RenderEndTag();//fecha table
        File.WriteAllText(fileName, stringWriter.ToString());
      }
    }
    private void divideRespRK(List<Double> t, List<Double> w, Ponto[] RespRK){
        for (int i = 0; i < RespRK.Length; i++){
            t.Add(RespRK[i].X);
            w.Add(RespRK[i].Y);
        }
    }
}
