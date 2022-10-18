package TESTING;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;
import java.util.Arrays;

public class TestingOpenPrivateKey {
    public static void main(String[] args) {
        DigitalSigner dgs = new DigitalSigner();
        Certificado c = dgs.get_certificado_valido("XXXX");
        if (c != null) {
            if (dgs.open_privateKey_from_certificate(c) != null) {
                System.out.println("Private Key >>> " + Arrays.toString(dgs.open_privateKey_from_certificate(c)));
            } else {
                System.out.println("ERRROR >> " + dgs.getERROR_DIGITAL_SIGNER());
            }
        } else {
            System.out.println("NO SE HA CONECTADO NINGÚN SMART-CARD");
        }
    }//main
}//CLASE