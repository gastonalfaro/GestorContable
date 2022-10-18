package TESTING;

import SIGNER.*;
import SMART_CARD.*;
import java.util.*;

public class Testing_List_Certificados_Encontrados {
    public static void main(String[] args) {
        DigitalSigner dgs = new DigitalSigner();
        List<Certificado> lst_c = dgs.get_lst_certificados_firma_validos();
        if (lst_c != null) {
            for (int i = 0; i < lst_c.size(); i++) {
                System.out.println("Alias Certificado >> " + lst_c.get(i).getAlias_del_certificado());
            }
        } else {
            System.out.println("No se encontraron certificados de firma digital !!!");
        }
    }//main
}//CLASE