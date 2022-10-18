package XML;

import SMART_CARD.*;

public class XML_EMPRESA {
    private String EMPRESA_XML_STR;
    private final Sujeto SUJETO;
    private final Emisor EMISOR;
    private final String EMPRESA_INFO;
    private final byte[] FIRMA;

    public XML_EMPRESA(Emisor EMISOR, Sujeto SUJETO, String EMPRESA_INFO, byte[] FIRMA) {
        this.EMISOR = EMISOR;
        this.SUJETO = SUJETO;
        this.EMPRESA_INFO = this.INFO_EMPRESA_TAG(EMPRESA_INFO);
        this.FIRMA = FIRMA;
        this.init_XML_EMPRESA();
    }//CONSTRUCTOR
    
    private void init_XML_EMPRESA() {
        TAG_STR st = new TAG_STR();
        this.EMPRESA_XML_STR = st.EMPRESA(st.CERTIFICADO(this.EMISOR, this.SUJETO) + this.EMPRESA_INFO + st.FIRMADIGITAL(this.FIRMA));
    }//FUNCION

    public String getEMPRESA_XML_STR() {
        return EMPRESA_XML_STR;
    }//GET
    
    public String INFO_EMPRESA_TAG(String INFO_EMPRESA) {
        TAG_STR st = new TAG_STR();
        //EL STRING QUE SE RECIVE DESDE DEL SISTEMA GESTOR ES >> CEDULA ; NOMBRE ; TELEFONO ; MAIL
        String[] INFO_SPLIT = INFO_EMPRESA.split(";");
        
        if (INFO_SPLIT.length > 0) {
            return st.NEW_TAG("info_empresa", 
                    st.NEW_TAG("numero_identificacion", INFO_SPLIT[0])
                            + 
                    st.NEW_TAG("nombre", INFO_SPLIT[1]) 
                            +
                    st.NEW_TAG("telefono", INFO_SPLIT[2]) 
                            + 
                    st.NEW_TAG("mail", INFO_SPLIT[3]));
        } else {
            return null;
        }
    }//FUNCION
    
}//CLASE