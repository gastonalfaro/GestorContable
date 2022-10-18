package XML;

import SMART_CARD.Emisor;
import SMART_CARD.Sujeto;

public class XML_AUTORIZACION {
    private String AUTORIZACION_XML_STR;
    private final Sujeto SUJETO;
    private final Emisor EMISOR;
    private final String AUTORIZACION_INFO;
    private final byte[] FIRMA;

    public XML_AUTORIZACION(Emisor EMISOR, Sujeto SUJETO, String AUTORIZACION_INFO, byte[] FIRMA) {
        this.EMISOR = EMISOR;
        this.SUJETO = SUJETO;
        this.AUTORIZACION_INFO = this.AUTORIZACION_TAG(AUTORIZACION_INFO);
        this.FIRMA = FIRMA;
        this.init_XML_EMPRESA();
    }//CONSTRUCTOR
    
    private void init_XML_EMPRESA() {
        TAG_STR st = new TAG_STR();
        this.AUTORIZACION_XML_STR = st.AUTORIZACION(st.CERTIFICADO(this.EMISOR, this.SUJETO) + this.AUTORIZACION_INFO + st.FIRMADIGITAL(this.FIRMA));
    }//FUNCION

    public String getAUTORIZACION_XML_STR() {
        return AUTORIZACION_XML_STR;
    }//GET
    
    public String AUTORIZACION_TAG(String AUTORIZACION_INFO) {
        TAG_STR st = new TAG_STR();
        //EL STRING QUE SE RECIVE DESDE DEL SISTEMA GESTOR ES >> CEDULA_PERSONA_QUE_AUTORIZA ; CEDULA_PERSONA_A_QUIEN_SE_AUTORIZA ; CEDULA EMPRESA DONDE SE AUTORIZA
        String[] INFO_SPLIT = AUTORIZACION_INFO.split(";");
        
        if (INFO_SPLIT.length > 0) {
            return st.NEW_TAG("info_autorizacion", 
                    st.NEW_TAG("persona_que_autoriza", 
                        st.NEW_TAG("cedula", INFO_SPLIT[0])) 
                            + 
                    st.NEW_TAG("persona_a_quien_se_autoriza", 
                            st.NEW_TAG("cedula", INFO_SPLIT[1])) 
                            +
                    st.NEW_TAG("empresa_donde_se_autoriza", 
                            st.NEW_TAG("cedula", INFO_SPLIT[2])));
        } else {
            return null;
        }
    }//FUNCION
    
}//CLASE