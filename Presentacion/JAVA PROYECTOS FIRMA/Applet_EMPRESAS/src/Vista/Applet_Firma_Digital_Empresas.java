package Vista;

import SIGNER.DigitalSigner;
import SMART_CARD.Certificado;
import java.applet.Applet;
import java.awt.HeadlessException;
import java.security.AccessController;
import java.security.PrivilegedAction;
import java.util.List;
import javax.swing.JOptionPane;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;
import netscape.javascript.JSException;
import netscape.javascript.JSObject;

public class Applet_Firma_Digital_Empresas extends Applet {
    
    @Override
    public void init() {
        applicar_estilo_look_and_feel_windows();
    }//FUNCION
    
    public void applicar_estilo_look_and_feel_windows(){
        try {
            UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | UnsupportedLookAndFeelException ex) {
            System.out.println("ERRROR >> " + ex.getMessage());
        }
    }//FUNCION
    
    public String input_box(String[] lst_opciones) {
        try {
            return (String) JOptionPane.showInputDialog(this, 
            "¿ Cual es su certificado de firma digital ?",
            "Lista Certificados Encontrados:",
            JOptionPane.QUESTION_MESSAGE, 
            null, 
            lst_opciones, 
            lst_opciones[0]);
        } catch (HeadlessException e) {
            System.out.println("ERROR >> " + e.getMessage());
            return null;
        }
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
    
    public Certificado get_this_alias_certificado(String alias, List<Certificado>lista){
        Certificado c = null;
        for (int i = 0; i < lista.size(); i++) {
            if (lista.get(i).getAlias_del_certificado().equals(alias)) {
                c = lista.get(i);
                break;
            }
        }
        return c;
    }//FUNCION
    
    public void set_XML_STR_SIGNED(String XML_FILE_STR_SIGNED, String CEDULA) {
        try {
            JSObject window = JSObject.getWindow(this);
            window.setMember("XML_EMPRESA_SIGNED", XML_FILE_STR_SIGNED);
            window.setMember("CEDULA", CEDULA);
            window.eval("write_xml_signed_on_input()");
            mtr_msg("Firmado Digitalmente !!!");
        } catch (JSException e) {
            mtr_msg("Alerta !!! " + e.getMessage());
        }
    }//FUNCION
    
    public String cedula_con_formato(Certificado c) {
        return c.getSujeto().getSerial_number().replace('C', ' ').replace('P', ' ').replace('F', ' ').replace('-', ' ').trim();
    }//FUNCION
    
    public String xml_con_formato(String xml_signed) {
        return xml_signed.replace('<', '°').replace('>', '|');
    }//FUNCION
    
    public String get_EMPRESA_XML_STR() {
        try {
            return (String) JSObject.getWindow(this).eval("get_info_empresa()");
        } catch (JSException e) {
            mtr_msg("ALERTA !!! " + e.getMessage());
            return null;
        }
    }//FUNCION
    
    public void proceso_firma_digita() {
        AccessController.doPrivileged(new PrivilegedAction() {
            public Object run() {
                DigitalSigner dgs =  new DigitalSigner();
                List<Certificado> lst_c = dgs.get_lst_certificados_firma_validos();
                if (lst_c != null) {
                    String alias = input_box(lst_alias(lst_c));
                    if (alias != null) {
                        Certificado c_sign = get_this_alias_certificado(alias, lst_c);
                        String info_empresa = get_EMPRESA_XML_STR();
                        String xml_signed = dgs.sign_xml_EMPRESA(c_sign, info_empresa);
                        if (xml_signed != null) {
                            set_XML_STR_SIGNED(xml_con_formato(xml_signed), cedula_con_formato(c_sign));
                        } else {
                            mtr_msg("Se canceló la operación !!!");
                        }
                    } else {
                        mtr_msg("Se canceló la operación !!!");
                    }
                } else {
                    mtr_msg("No se ha detectado ningún certificado de firma digital !!!");
                }
                return null;
            }
        });
    }//FUNCION
    
}//CLASE
