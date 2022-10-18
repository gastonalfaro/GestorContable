/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package TESTING;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;
import java.applet.Applet;
import java.util.List;
import javax.swing.JOptionPane;

/**
 *
 * @author ramses_user
 */
public class Testing_applet_001 extends Applet {

    /**
     * Initialization method that will be called after the applet is loaded into
     * the browser.
     */
    public void init() {
        DigitalSigner dgs =  new DigitalSigner();
        List<Certificado> lst_c = dgs.get_lst_certificados_firma_validos();
        if (lst_c.size() > 0) {
            
        } else {
            System.out.println("No smart-card");
        }
    }
    
    
    public String input_box(String[] lst_opciones) {
        return (String) JOptionPane.showInputDialog(this, 
        "¿ Cual es su certificado de firma digital ?",
        "Lista Certificados Encontrados:",
        JOptionPane.QUESTION_MESSAGE, 
        null, 
        lst_opciones, 
        lst_opciones[0]);
    }//FUNCION
    
    public void mtr_msg(String msg) {
        JOptionPane.showMessageDialog(this, msg, "Alerta", JOptionPane.WARNING_MESSAGE);
    }//FUNCION
    
    public String[] lst_alias(List<Certificado>lista) {
        String[] alias = new String[lista.size()];
        for (int i = 0; i < lista.size(); i++) {
            alias[i] = lista.get(i).getAlias_del_certificado();
        }
        return alias;
    }//FUNCION
    
}
