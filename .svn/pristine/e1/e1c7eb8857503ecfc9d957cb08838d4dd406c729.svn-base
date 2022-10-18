package TESTING;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;

public class Test_sign_file_xml_str {
    public static void main(String[] args) {
        DigitalSigner dgs = new DigitalSigner();
        Certificado certificado = dgs.get_certificado_valido("XXXX");
        
        if (certificado != null) {
            String xml_signed = dgs.sign_file_xml(dgs.get_certificado_valido("XXXX"), "");
            System.out.println("XML\n" + xml_signed);
        } else {
            System.out.println("ERROR >> No se ha conectado ningún smart card !!!");
        }
    }//main
}//CLASE
