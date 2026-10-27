using System;
using System.Text.RegularExpressions;
namespace SistemaEtiquetas.Negocio {
 public class EtiquetaService {
  private int secuencia = 10001;
  public string Validar(string linea, string producto, string lote, int cantidad) {
   if (string.IsNullOrWhiteSpace(linea)) return "Selecciona una línea de montaje.";
   if (string.IsNullOrWhiteSpace(producto)) return "Escribe el producto.";
   if (string.IsNullOrWhiteSpace(lote)) return "Escribe el lote.";
   if (!Regex.IsMatch(lote.Trim(), @"^[A-Za-z0-9\-]{2,20}$"))
    return "El lote debe tener de 2 a 20 caracteres: letras, números o guion.";
   if (cantidad < 1 || cantidad > 500) return "La cantidad debe estar entre 1 y 500.";
   return "";
  }
  public string Generar(string linea, string producto, string lote, int cantidad) {
   string error = Validar(linea, producto, lote, cantidad);
   if (error != "") throw new ArgumentException(error);
   return $"LÍNEA: {linea}\nPRODUCTO: {producto.Trim().ToUpper()}\nLOTE: {lote.Trim().ToUpper()}\nSECUENCIA: {secuencia++}\nCANTIDAD: {cantidad}";
  }
 }
}