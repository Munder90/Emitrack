using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Integrador.Entities;
using System.IO;

namespace Integrador.Models
{
    public class ReportEsqueleto
    {
        public string crearPDF(ReportCabecera RC, List<ReportDetalle> RD)
        {
            var fncolor = new BaseColor(255, 255, 255);
            iTextSharp.text.Font letraTabNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, fncolor);
            iTextSharp.text.Font letraTab = FontFactory.GetFont(FontFactory.HELVETICA, 7);
            iTextSharp.text.Font negritaPeque = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
            iTextSharp.text.Font normalPeque = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            iTextSharp.text.Font normalMasPeque = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            iTextSharp.text.Font titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            PdfPTable tabCabecera = new PdfPTable(1);
            PdfPTable tablaDatos = new PdfPTable(1);
            PdfPTable tablaDatos1 = new PdfPTable(2);
            PdfPTable tablaDatos2 = new PdfPTable(4);
            PdfPTable tablaDatos3 = new PdfPTable(1);
            PdfPTable tablaDatos4 = new PdfPTable(6);

            string ruta = "";
            using (INTEGRAEntities db = new INTEGRAEntities())
            {
                DateTime fechaCreacion = DateTime.Now;
                string nombreArchivo = string.Format("{0}.pdf", RC.PEDIDO);
                string rutaCompleta = HttpContext.Current.Server.MapPath("~/PdfTemp/" + nombreArchivo);
                FileStream fsDocumento = new FileStream(rutaCompleta, FileMode.Create);
                Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 60f, 30f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, fsDocumento);

                try
                {
                    pdfDoc.Open();

                    iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/EmiTrack.png"));
                    PdfPCell celLineaCabecera = new PdfPCell { Image = imagen2, Border = 0, Padding = 3, FixedHeight = 60f, HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                    tabCabecera.AddCell(celLineaCabecera);
                    tabCabecera.TotalWidth = pdfDoc.PageSize.Width;
                    tabCabecera.WriteSelectedRows(0, -1, 0, pdfDoc.PageSize.Top, pdfWriter.DirectContent);

                    Paragraph frase1;

                    frase1 = new Paragraph("Factura", titulo) { Alignment = Element.ALIGN_LEFT }; pdfDoc.Add(frase1);
                    pdfDoc.Add(new Chunk(""));

                    //Cabecera
                    pdfDoc.Add(new Chunk(""));
                    tablaDatos1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tablaDatos1.SetWidthPercentage(new float[] { 100, 500 }, PageSize.A4);
                    PdfPCell celda1 = new PdfPCell(new Paragraph("Numero de Pedido: ", negritaPeque)); celda1.Border = 0; tablaDatos1.AddCell(celda1);
                    PdfPCell celda2 = new PdfPCell(new Paragraph(RC.PEDIDO, normalPeque)); celda2.Border = 0; tablaDatos1.AddCell(celda2);
                    PdfPCell celda3 = new PdfPCell(new Paragraph("Dirigido A: ", negritaPeque)); celda3.Border = 0; tablaDatos1.AddCell(celda3);
                    PdfPCell celda4 = new PdfPCell(new Paragraph(RC.USUARIO, normalPeque)); celda4.Border = 0; tablaDatos1.AddCell(celda4);
                    PdfPCell celda5 = new PdfPCell(new Paragraph("Fecha: ", negritaPeque)); celda5.Border = 0; tablaDatos1.AddCell(celda5);
                    PdfPCell celda6 = new PdfPCell(new Paragraph(RC.FECHA, normalPeque)); celda6.Border = 0; tablaDatos1.AddCell(celda6);
                    PdfPCell celda7 = new PdfPCell(new Paragraph("Metodo de Pago: ", negritaPeque)); celda7.Border = 0; tablaDatos1.AddCell(celda7);
                    PdfPCell celda8 = new PdfPCell(new Paragraph(RC.PAGO, normalPeque)); celda8.Border = 0; tablaDatos1.AddCell(celda8);
                    PdfPCell celda9 = new PdfPCell(new Paragraph("Direccion de Envío: ", negritaPeque)); celda9.Border = 0; tablaDatos1.AddCell(celda9);
                    PdfPCell celda10 = new PdfPCell(new Paragraph(RC.DIRECCION, normalPeque)); celda10.Border = 0; tablaDatos1.AddCell(celda10);
                    pdfDoc.Add(tablaDatos1);
                    pdfDoc.Add(new Chunk("\n"));

                    //Tabla de detalle
                    tablaDatos2.SetWidthPercentage(new float[] { 250, 100, 100, 100 }, PageSize.A4);
                    PdfPCell Titulo1 = new PdfPCell(new Paragraph(" ", letraTabNegrita)) { Border = 0, Colspan = 4, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Titulo1);
                    PdfPCell Cabecera1 = new PdfPCell(new Paragraph("PRODUCTO", letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cabecera1);
                    PdfPCell Cabecera2 = new PdfPCell(new Paragraph("CANTIDAD", letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cabecera2);
                    PdfPCell Cabecera3 = new PdfPCell(new Paragraph("PRECIO UNITARIO", letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cabecera3);
                    PdfPCell Cabecera4 = new PdfPCell(new Paragraph("IMPORTE", letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cabecera4);
                    bool col = true;
                    BaseColor fondo;
                    foreach (ReportDetalle det in RD)
                    {
                        if (col == true)
                        {
                            fondo = new BaseColor(230, 230, 230);
                            col = false;
                        }
                        else
                        {
                            fondo = new BaseColor(255, 255, 255);
                            col = true;
                        }
                        PdfPCell Cuerpo1 = new PdfPCell(new Paragraph(det.PRODUCTO1 + "\n" + (!string.IsNullOrEmpty(det.TEXTO) ? det.TEXTO : null), letraTab)) { Border = 1, BackgroundColor = fondo, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cuerpo1);
                        PdfPCell Cuerpo2 = new PdfPCell(new Paragraph(det.CANTIDAD, letraTab)) { Border = 1, BackgroundColor = fondo, HorizontalAlignment = Element.ALIGN_CENTER, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cuerpo2);
                        PdfPCell Cuerpo3 = new PdfPCell(new Paragraph("$" + det.PRECIO, letraTab)) { Border = 1, BackgroundColor = fondo, HorizontalAlignment = Element.ALIGN_RIGHT, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cuerpo3);
                        PdfPCell Cuerpo4 = new PdfPCell(new Paragraph("$" + det.IMPORTE, letraTab)) { Border = 1, BackgroundColor = fondo, HorizontalAlignment = Element.ALIGN_RIGHT, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Cuerpo4);
                    }
                    PdfPCell Total1 = new PdfPCell(new Paragraph("", letraTabNegrita)) { Border = 1, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Total1);
                    PdfPCell Total2 = new PdfPCell(new Paragraph("", letraTabNegrita)) { Border = 1, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Total2);
                    PdfPCell Total3 = new PdfPCell(new Paragraph("Total", letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Total3);
                    PdfPCell Total4 = new PdfPCell(new Paragraph("$" + RC.TOTAL, letraTabNegrita)) { Border = 1, BackgroundColor = new BaseColor(0, 53, 100), HorizontalAlignment = Element.ALIGN_RIGHT, BorderColor = new BaseColor(240, 240, 240) }; tablaDatos2.AddCell(Total4);
                    pdfDoc.Add(tablaDatos2);
                    pdfDoc.Add(new Chunk("\n"));
                    
                    pdfDoc.Close();

                    ruta = "/PdfTemp/" + nombreArchivo;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return ruta;
            }
        }
    }
}