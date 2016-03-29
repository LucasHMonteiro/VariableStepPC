using System.Windows.Forms;
using System;
using System.Collections;
using TrabalhoPC;
using System.Collections.Generic;

class TestForm : Form {
  public TextBox k;
  public TextBox aValue;
  public TextBox bValue;
  public TextBox condValue;
  public TextBox tolValue;
  public TextBox hmaxValue;
  public TextBox hminValue;
  public Button openHtml;
  public Button about;

  public TestForm(string text){
    this.Text = text;
    this.MaximizeBox = false;

    this.openHtml = new Button();
    this.openHtml.Text = "Abrir Arquivo";
    this.openHtml.Size = new System.Drawing.Size(100, 20);
    this.openHtml.Click += new System.EventHandler(this.openFile);
    this.openHtml.Location = new System.Drawing.Point(240, 50);

    Button about = new Button();
    about.Text = "Sobre";
    about.Location = new System.Drawing.Point(265, 125);
    about.Click += new System.EventHandler(this.popUpAbout);

    Button gen = new Button();
    gen.Text = "Gerar";
    gen.Location = new System.Drawing.Point(265, 163);
    gen.Click += new System.EventHandler(this.generate);

    Button algorithm = new Button();
    algorithm.Text = "Mostrar Algoritmo";
    algorithm.Location = new System.Drawing.Point(230, 90);
    algorithm.Size = new System.Drawing.Size(110, 20);
    algorithm.Click += new System.EventHandler(this.showAlgorithm);

    Label edo = new Label();
    edo.Text = "y' = ";
    edo.Size = new System.Drawing.Size(30, 20);
    edo.Location = new System.Drawing.Point(210, 15);
    Label edo2 = new Label();
    edo2.Text = "y";
    edo2.Size = new System.Drawing.Size(30, 20);
    edo2.Location = new System.Drawing.Point(290, 15);
    this.k = new TextBox();
    this.k.Size = new System.Drawing.Size(50, 20);
    this.k.Location = new System.Drawing.Point(240, 13);

    Label a = new Label();
    a.Text = "Início do Intervalo: ";
    a.Size = new System.Drawing.Size(100, 20);
    a.Location = new System.Drawing.Point(15, 15);
    this.aValue = new TextBox();
    this.aValue.Size = new System.Drawing.Size(50, 20);
    this.aValue.Location = new System.Drawing.Point(115, 13);

    Label b = new Label();
    b.Text = "Final do Intervalo: ";
    b.Size = new System.Drawing.Size(100, 20);
    b.Location = new System.Drawing.Point(15, 45);
    this.bValue = new TextBox();
    this.bValue.Size = new System.Drawing.Size(50, 20);
    this.bValue.Location = new System.Drawing.Point(115, 43);

    Label condicao = new Label();
    condicao.Text = "Valor Inicial (y(a)): ";
    condicao.Size = new System.Drawing.Size(100, 20);
    condicao.Location = new System.Drawing.Point(15, 75);
    this.condValue = new TextBox();
    this.condValue.Size = new System.Drawing.Size(50, 20);
    this.condValue.Location = new System.Drawing.Point(115, 73);

    Label tol = new Label();
    tol.Text = "Tolerância: ";
    tol.Size = new System.Drawing.Size(80, 20);
    tol.Location = new System.Drawing.Point(15, 105);
    this.tolValue = new TextBox();
    this.tolValue.Size = new System.Drawing.Size(50, 20);
    this.tolValue.Location = new System.Drawing.Point(115, 103);

    Label hmax = new Label();
    hmax.Text = "H máximo: ";
    hmax.Size = new System.Drawing.Size(80, 20);
    hmax.Location = new System.Drawing.Point(15, 135);
    this.hmaxValue = new TextBox();
    this.hmaxValue.Size = new System.Drawing.Size(50, 20);
    this.hmaxValue.Location = new System.Drawing.Point(115, 133);

    Label hmin = new Label();
    hmin.Text = "H mínimo: ";
    hmin.Size = new System.Drawing.Size(80, 20);
    hmin.Location = new System.Drawing.Point(15, 165);
    this.hminValue = new TextBox();
    this.hminValue.Size = new System.Drawing.Size(50, 20);
    this.hminValue.Location = new System.Drawing.Point(115, 163);

    this.Controls.Add(algorithm);
    this.Controls.Add(edo2);
    this.Controls.Add(k);
    this.Controls.Add(aValue);
    this.Controls.Add(bValue);
    this.Controls.Add(condValue);
    this.Controls.Add(tolValue);
    this.Controls.Add(hmaxValue);
    this.Controls.Add(hminValue);
    this.Controls.Add(a);
    this.Controls.Add(b);
    this.Controls.Add(tol);
    this.Controls.Add(about);
    this.Controls.Add(edo);
    this.Controls.Add(gen);
    this.Controls.Add(condicao);
    this.Controls.Add(hmax);
    this.Controls.Add(hmin);
  }

  public void generate(object sender, System.EventArgs e){
    double a = 0, b = 0, alpha = 0, tol = 0, hmax = 0, hmin = 0, k = 0;
    try{
      a = Convert.ToDouble(this.aValue.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.aValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.aValue.Text);
    }

    try{
      b = Convert.ToDouble(this.bValue.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.bValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.bValue.Text);
    }

    try{
      alpha = Convert.ToDouble(this.condValue.Text);
    }catch(FormatException){
      MessageBox.Show(String.Format("Não foi possível converter '{0}' para double", this.condValue.Text), "Format Error");
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.condValue.Text);
    }

    try{
      tol =  Convert.ToDouble(this.tolValue.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.tolValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.tolValue.Text);
    }

    try{
      hmax = Convert.ToDouble(this.hmaxValue.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.hmaxValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.hmaxValue.Text);
    }

    try{
      hmin = Convert.ToDouble(this.hminValue.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.hminValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.hminValue.Text);
    }
    try{
      k = Convert.ToDouble(this.k.Text);
    }catch(FormatException){
      MessageBox.Show("Não foi possível converter '{0}' para double", this.hminValue.Text);
    }catch(OverflowException){
      MessageBox.Show("'{0}' é um número muito grande", this.hminValue.Text);
    }
    ArrayList resp;
    VariableStep VS = new VariableStep(k);
    resp = VS.Executa(a, b, alpha, tol, hmax, hmin);
    try{
      VS.toXML(resp, "solutions.xml");
      VS.toHTML(resp, "solutions.html");
      MessageBox.Show("Soluções geradas com sucesso!");
      this.Controls.Add(openHtml);
    }catch(System.Exception){
      MessageBox.Show("hmin ultrapassado!");
    }
  }

  public void openFile(object sender, System.EventArgs e){
    System.Diagnostics.Process.Start("solutions.html");
  }

  public void popUpAbout(object sender, System.EventArgs e){
    MessageBox.Show("Cálculo de EDOs da forma y'= ay com passo múltipo de ordem 4 e H variável.\n\nCriado por: Gabriel Reis Carrara\n                     Lucas Monteiro\n                     Luiz Renato Vasconcelos\n                     Victor Olimpio");
  }

  public void showAlgorithm(object sender, System.EventArgs e){
    System.IO.StreamWriter file = new System.IO.StreamWriter("algorithm.txt", true);
    file.WriteLine(@"para j = 1, 2, 3
    faça K1 = hf(xj-1, vj-1);
            K2 = hf(xj-1 + h/2, vj-1 + K1/2)
        K3 = hf(xj-1 + h/2, vj-1 + K2/2)
        K4 = hf(xj-1 + h, vj-1 + K3)
        vj = vj-1 + (K1 + 2K2 + 2K3 + K4)/6;
        xj = x0 + jh

Passo 2: Faça t0 = a;
          w0 = α;
                  h = hmax;
          FLAG = 1; (FLAG será usado para sair do loop no Passo 4.)
          LAST = 0: (LAST indica quando o último valor é calculado.)
SAÍDA (t0, w0)

Passo 3: Chame RK4(h, v0, t0, v1, t1, v2, t2, v3, t3);
         Faça NFLAG = 1;  (Indica cálculo a partir de RK4.)
        i = 4;
        i = t3 + h

Passo 4: Enquanto (FLAG = 1) siga os Passos 5-20.

                        Passo 5: Faça WP = wi-1 + h24[55f(ti-1, wi-1) - 59f(ti-2, wi-1) + 37f(ti-3, wi-3, - 9f(ti-4, wi-4)]; (Prediz wi)
                            WC = wi-1 + h24[9f(t, WP) + 19f(ti-1, wi-1) - 5f(ti-2, wi-2) + f(ti-3, wi-3)];  (Corrige wi)

                            σ = 19 | WC - WP |/(270h)

           Passo 6: Se σ  TOL então siga os passos 7-16 (Resultado aceito.) se não, siga os passos 17-19. (Resultado recusado.)

            Passo 7: Faça wi = WC; (Resultado aceito.)
                                   ti = t.

            Passo 8: Se NFLAG = 1 então para j = i - 3, i - 2, i - 1, i
                    SAÍDA(j, tj, wj, h);
(Os resultados anteriores também ja aceitos)
    se não, SAÍDA(i, ti, wi, h)

              Passo 9 : Se LAST = 1 então faça FLAG = 0 (Próximo passo é o 20.) e n siga os passos 10-16.

            Passo 10: Faça i = i + 1;
                      NFLAG = 0.

            Passo 11: Se σ  0,1 TOL ou ti-1 + h > b então siga os passos 12-16 (Aumenta h se é mais preciso que o requerido ou diminui h para incluir b como um ponto de rede.)

            Passo 12: Faça q = (TOL/(2σ))¼

            Passo 13: Se q > 4 então faça h = 4h
                      Se não, faça h = qh.

            Passo 14: Se h > hmax então faça h = hmax.

            Passo 15: Se ti-1 + 4h > b então
                      faça h = (b - ti-1)/4;
                    LAST = 1
            Passo 16: Chame RK4(h, wi-1, ti-1, wi, ti, wi+1, ti+1, wi+2, ti+2,);
                      Faça NFLAG = 1;
                      i = i + 3. (Ramo verdadeiro terminado. Próximo passo é o 20.)

        Passo 17: Faça q = (TOL/(2σ))¼. (Ramo falso desde o Passo 6: resultado rejeitado)

        Passo 18: Se q < 0,1 então faça h = 0,1h
                se não, faça h = qh.

        Passo 19: Se h < hmin então faça FLAG = 0;
                        SAÍDA(“hmin ultrapassado”)
                          se não
                        se NFLAG = 1 então faça i = i - 3;
                        (Resultados prévios também rejeitados.)
                        Chame RK4(h, wi-1, ti-1, wi, ti, wi+1, ti+1, wi+2, ti+2,);
                        faça i = i + 3;
                              NFLAG = 1.

        Passo 20: Faça t = ti-1 + h.

        Passo 21: PARE.");
        file.Close();
        System.Diagnostics.Process.Start("algorithm.txt");
  }
}

class App{
  static void Main(){
    Form form = new TestForm("Passo Múltiplo com H Variável");
    form.Size = new System.Drawing.Size(400, 250);
    Application.Run(form);
  }
}
