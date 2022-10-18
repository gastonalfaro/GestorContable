package XML;

import SMART_CARD.*;
import java.util.Arrays;

public class TAG_STR {
    
    public String EMPRESA(String content){
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><empresa>" + content + "</empresa>";
    }//FUNCION
    
    public String AUTORIZACION(String content){
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><autorizacion>" + content + "</autorizacion>";
    }//FUNCION
    
    public String FORMULARIO(String content){
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><formulariofirmado>" + content + "</formulariofirmado>";
    }//FUNCION
    
    public String CERTIFICADO(Emisor e, Sujeto s) {
        return NEW_TAG("certificado", (SUJETO(s) + EMISOR(e)));
    }//FUNCION
    
    public String FIRMADIGITAL(byte[] firma){
        return NEW_TAG("firma_digital", NEW_TAG("firma", Arrays.toString(firma)));
    }//FUNCION
    
    private String SUJETO(Sujeto s) {
        return NEW_TAG("sujeto", NEW_TAG("cn", s.getCn()) + NEW_TAG("ou", s.getOu()) + NEW_TAG("o", s.getO()) + NEW_TAG("c", s.getC()) + NEW_TAG("serialnumber", s.getSerial_number()));
    }//FUNCION
    
    private String EMISOR(Emisor e) {
        return NEW_TAG("emisor", NEW_TAG("cn", e.getCn()) + NEW_TAG("ou", e.getOu()) + NEW_TAG("o", e.getO()) + NEW_TAG("c", e.getC()) + NEW_TAG("serialnumber", e.getSerial_number()));
    }//FUNCION
    
    public String NEW_TAG(String TAG_NAME, String CONTENT) {
        return String.format("<%s>%s</%s>", TAG_NAME, CONTENT, TAG_NAME);
    }//FUNCION
    
}//CLASE
