using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaEtiquetas.Negocio;
namespace SistemaEtiquetas.Presentacion {
 public class FrmEtiquetas : Form {
  private EtiquetaService servicio = new EtiquetaService();
  private ComboBox linea;
  private TextBox producto, lote, cantidad;
  private Label vista;
  private Button imprimir;
  public FrmEtiquetas() {
   Text="Sistema de gestión de impresión láser de etiquetas";
   StartPosition=FormStartPosition.CenterScreen; Size=new Size(1080,700);
   MinimumSize=new Size(950,620); BackColor=Color.FromArgb(244,247,251);
   Font=new Font("Segoe UI",10); CrearInterfaz();
  }
  private void CrearInterfaz() {
   var side=new Panel{Dock=DockStyle.Left,Width=215,BackColor=Color.FromArgb(22,42,76)};
   Controls.Add(side);
   side.Controls.Add(new Label{Text="ETIQUETAS\\nLÁSER",ForeColor=Color.White,Font=new Font("Segoe UI",19,FontStyle.Bold),Location=new Point(22,28),Size=new Size(175,70)});
   side.Controls.Add(new Label{Text="▣  Vista previa e impresión\\n\\n▤  Líneas de montaje\\n\\n↻  Secuencias numéricas\\n\\n▥  Reportes",ForeColor=Color.White,Location=new Point(20,140),Size=new Size(185,230)});
   var header=new Panel{Dock=DockStyle.Top,Height=88,BackColor=Color.White}; Controls.Add(header); header.BringToFront();
   header.Controls.Add(new Label{Text="Vista previa de etiqueta",Font=new Font("Segoe UI",21,FontStyle.Bold),ForeColor=Color.FromArgb(26,43,70),Location=new Point(28,12),AutoSize=true});
   header.Controls.Add(new Label{Text="Configura los datos y revisa la etiqueta antes de imprimir.",ForeColor=Color.Gray,Location=new Point(31,53),AutoSize=true});
   var main=new Panel{Dock=DockStyle.Fill,Padding=new Padding(25)}; Controls.Add(main); main.BringToFront();
   main.Controls.Add(new Label{Text="DATOS DE PRODUCCIÓN",Font=new Font("Segoe UI",11,FontStyle.Bold),Location=new Point(25,20),AutoSize=true});
   LabelCampo(main,"Línea de montaje",28,58);
   linea=new ComboBox{Location=new Point(28,82),Size=new Size(370,32),DropDownStyle=ComboBoxStyle.DropDownList};
   linea.Items.AddRange(new object[]{"Línea 01 - Ensamble","Línea 02 - Empaque","Línea 03 - Inspección"}); linea.SelectedIndex=0; main.Controls.Add(linea);
   LabelCampo(main,"Código o nombre del producto",28,128);
   producto=new TextBox{Location=new Point(28,152),Size=new Size(370,30)}; main.Controls.Add(producto);
   LabelCampo(main,"Lote de producción",28,195);
   lote=new TextBox{Location=new Point(28,219),Size=new Size(370,30)}; main.Controls.Add(lote);
   LabelCampo(main,"Cantidad de etiquetas (1 a 500)",28,262);
   cantidad=new TextBox{Location=new Point(28,286),Size=new Size(370,30),Text="1"}; main.Controls.Add(cantidad);
   var generar=Boton("Generar vista previa",Color.FromArgb(40,95,180),Color.White,28,345,190); generar.Click+=GenerarClick; main.Controls.Add(generar);
   var limpiar=Boton("Limpiar",Color.White,Color.FromArgb(45,65,93),235,345,100); limpiar.Click+=(s,e)=>Limpiar(); main.Controls.Add(limpiar);
   var panel=new Panel{Location=new Point(455,20),Size=new Size(500,430),BackColor=Color.White,BorderStyle=BorderStyle.FixedSingle}; main.Controls.Add(panel);
   panel.Controls.Add(new Label{Text="VISTA PREVIA",Font=new Font("Segoe UI",11,FontStyle.Bold),Location=new Point(22,18),AutoSize=true});
   var tag=new Panel{Location=new Point(35,65),Size=new Size(425,250),BackColor=Color.FromArgb(252,252,252),BorderStyle=BorderStyle.FixedSingle}; panel.Controls.Add(tag);
   tag.Controls.Add(new Label{Text="ETIQUETA DE IDENTIFICACIÓN",Font=new Font("Consolas",12,FontStyle.Bold),Location=new Point(15,15),AutoSize=true});
   vista=new Label{Text="Genera una vista previa para\\nmostrar los datos de la etiqueta.",Font=new Font("Consolas",10),Location=new Point(18,55),Size=new Size(390,170)}; tag.Controls.Add(vista);
   panel.Controls.Add(new Label{Text="Verifica los datos antes de imprimir.",ForeColor=Color.Gray,Location=new Point(35,330),AutoSize=true});
   imprimir=Boton("Confirmar impresión",Color.FromArgb(22,145,105),Color.White,35,365,220); imprimir.Enabled=false;
   imprimir.Click+=(s,e)=>MessageBox.Show("Prototipo: la impresión física requiere configurar la impresora.","Impresión",MessageBoxButtons.OK,MessageBoxIcon.Information); panel.Controls.Add(imprimir);
   main.Controls.Add(new Label{Text="La capa de negocio valida los datos y genera la secuencia.",ForeColor=Color.DimGray,Location=new Point(28,410),AutoSize=true});
  }
  private void LabelCampo(Control p,string t,int x,int y)=>p.Controls.Add(new Label{Text=t,Location=new Point(x,y),AutoSize=true,ForeColor=Color.FromArgb(60,75,98)});
  private Button Boton(string t,Color bg,Color fg,int x,int y,int w)=>new Button{Text=t,Location=new Point(x,y),Size=new Size(w,42),BackColor=bg,ForeColor=fg,FlatStyle=FlatStyle.Flat,Cursor=Cursors.Hand};
  private void GenerarClick(object s,EventArgs e) {
   if(!int.TryParse(cantidad.Text,out int n)){MessageBox.Show("La cantidad debe ser un número entero.","Dato incorrecto");return;}
   try { vista.Text=servicio.Generar(linea.Text,producto.Text,lote.Text,n); imprimir.Enabled=true; }
   catch(ArgumentException ex){MessageBox.Show(ex.Message,"Revisa los datos",MessageBoxButtons.OK,MessageBoxIcon.Warning);imprimir.Enabled=false;}
  }
  private void Limpiar(){producto.Clear();lote.Clear();cantidad.Text="1";vista.Text="Genera una vista previa para\\nmostrar los datos de la etiqueta.";imprimir.Enabled=false;linea.SelectedIndex=0;}
 }
}