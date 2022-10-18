package TESTING;
import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;
import XML.DriverXML;

public class Test_Firmar_Archivo {
    public static void main(String[] args) {
        String file_input_xml = "C:\\Users\\ramses_user\\Desktop\\Firmado.xml";
        DriverXML xml_driver = new DriverXML(file_input_xml);
        DigitalSigner dg = new DigitalSigner();
        Certificado certificado = dg.get_certificado_valido("XXXX");
        if (certificado != null) {
            if (dg.sign_file_xml(certificado, xml_driver)) {
                System.out.println("Firma lograda !!!");
            } else {
                System.out.println("ERROR >> " + dg.getERROR_DIGITAL_SIGNER());
            }
        } else {
            System.out.println("No se ha conectado ningún Smartcard !!!");
        }//FUNCION
    }//main
}//CLASE
