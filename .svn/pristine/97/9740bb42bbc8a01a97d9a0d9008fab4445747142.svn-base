/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package Utils;

import com.itextpdf.text.Rectangle;
import com.itextpdf.text.pdf.PdfAnnotation;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfFormField;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPCellEvent;
import com.itextpdf.text.pdf.PdfWriter;

/**
 *
 * @author isanchez
 */
public class MySignatureFieldEvent implements PdfPCellEvent { 
 public PdfFormField field; 
 public MySignatureFieldEvent(PdfFormField field) { 
 this.field = field; 
 } 

    MySignatureFieldEvent() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
 
 public void cellLayout(PdfPCell cell, Rectangle position, 
 PdfContentByte[] canvases) { 
 PdfWriter writer = canvases[0].getPdfWriter(); 
 field.setPage(); 
 field.setWidget(position, PdfAnnotation.HIGHLIGHT_INVERT); 
 writer.addAnnotation(field); 
 }

}
