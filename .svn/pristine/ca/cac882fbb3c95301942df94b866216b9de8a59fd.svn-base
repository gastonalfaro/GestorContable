package TESTING;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;

public class Test_GET_CEDULA {
    public static void main(String[] args) {
        DigitalSigner dgs = new DigitalSigner();
        Certificado c = dgs.get_certificado_valido("XXXX");
        if (c != null) {
            String cedula = c.getSujeto().getSerial_number().replace('C', ' ').replace('P', ' ').replace('F', ' ').replace('-', ' ').trim();
            System.out.println("Numero de cédula >> " + cedula.substring(1, cedula.length()));
        } else {
            System.out.println("Certificado vacio !!!");
        }
    }//main
}//CLASE
