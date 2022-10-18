package TESTING;

import SMART_CARD.Emisor;
import SMART_CARD.Sujeto;
import XML.*;

public class Test_XML_STR {
    public static void main(String[] args) {
        Emisor e = new Emisor("un cn", "un ou", "un o", "un c", "923023023");
        Sujeto s = new Sujeto("un cn", "un ou", "un o", "un c", "923023023");
        String f = ""; //Todo el contenido del formulario en formato xml va aquí.
        System.out.println(new XML_STR(e, s, f, new byte[0]).getXML_FILE_STR());
    }//main
}//main
