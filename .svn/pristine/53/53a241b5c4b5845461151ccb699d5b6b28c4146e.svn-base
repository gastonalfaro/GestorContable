package XML;

import SMART_CARD.*;

public class XML_STR {
    private String XML_FILE_STR;
    private final Sujeto SUJETO;
    private final Emisor EMISOR;
    private final String FORMULARIO;
    private final byte[] FIRMA;

    public XML_STR(Emisor EMISOR, Sujeto SUJETO, String FORMULARIO, byte[] FIRMA) {
        this.EMISOR = EMISOR;
        this.SUJETO = SUJETO;
        this.FORMULARIO = FORMULARIO;
        this.FIRMA = FIRMA;
        init_XML_STR();
    }//CONSTRUCTOR
    
    private void init_XML_STR() {
        TAG_STR st = new TAG_STR();
        this.XML_FILE_STR = st.FORMULARIO(st.CERTIFICADO(this.EMISOR, this.SUJETO) + this.FORMULARIO + st.FIRMADIGITAL(this.FIRMA));
    }//FUNCION

    public String getXML_FILE_STR() {
        return XML_FILE_STR;
    }//GET
    
}//CLASE


/*


"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<formulariofirmado>" +
                    "<certificado>" +
                        "<sujeto>" +
                            "<cn></cn>" +
                            "<ou></ou>" +
                            "<o></o>" +
                            "<c></c>" +
                            "<serialnumber></serialnumber>" +
                        "</sujeto>" +
                        "<emision>" +
                            "<cn></cn>" +
                            "<ou></ou>" +
                            "<o></o>" +
                            "<c></c>" +
                            "<serialnumber></serialnumber>" +
                        "</emision>" +
                    "</certificado>" +


*/
