package TESTING;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;

public class Test_XML_EMPRESA_SIGN {
    public static void main(String[] args) {
        DigitalSigner dgs = new DigitalSigner();
        Certificado c = dgs.get_certificado_valido("XXXX");
        if (c != null) {
            String XML_EMPRESA_STR = dgs.sign_xml_AUTORIZACION(c, "123293; EMPRESA_NOMBRE ; +508838383; mail@gmail.com".trim());
            System.out.println("XML >> " + XML_EMPRESA_STR);
        } else {
            System.out.println("No se encontro ningún sertificado de firma digital !!!");
        }
    }//main
}//CLASE
