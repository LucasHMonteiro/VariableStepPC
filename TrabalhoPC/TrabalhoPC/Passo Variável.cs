using System.Windows.Forms;
using System;
using System.Collections;
using TrabalhoPC;
using System.Collections.Generic;

class TestForm : Form {
  public TextBox function;
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
    this.openHtml.Location = new System.Drawing.Point(240, 87);

    Button about = new Button();
    about.Text = "Sobre";
    about.Location = new System.Drawing.Point(265, 125);
    about.Click += new System.EventHandler(this.popUpAbout);

    Button gen = new Button();
    gen.Text = "Gerar";
    gen.Location = new System.Drawing.Point(265, 163);
    gen.Click += new System.EventHandler(this.generate);

    Label edo = new Label();
    edo.Text = "y' = ";
    edo.Size = new System.Drawing.Size(30, 20);
    edo.Location = new System.Drawing.Point(210, 15);
    this.function = new TextBox();
    this.function.Size = new System.Drawing.Size(100, 20);
    this.function.Location = new System.Drawing.Point(240, 13);

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

    this.Controls.Add(function);
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
    double a = 0, b = 0, alpha = 0, tol = 0, hmax = 0, hmin = 0;
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
    ArrayList resp;
    VariableStep VS = new VariableStep(this.function.Text);
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
    MessageBox.Show("Cálculo de EDO com passo múltipo de ordem 4 e H variável.\n\nCriado por: Gabriel Reis Carrara\n                     Lucas Monteiro\n                     Luiz Renato Vasconcelos\n                     Victor Olimpio");
  }
}

class App{
  static void Main(){
    Form form = new TestForm("Passo Múltiplo com H Variável");
    form.Size = new System.Drawing.Size(400, 250);
    Application.Run(form);
  }
}
