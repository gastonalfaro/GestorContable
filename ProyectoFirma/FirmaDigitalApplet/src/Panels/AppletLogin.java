/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Panels;

import Utils.Configuration;
//import Utils.SmartCardGetData;
import com.chilkatsoft.*;
import java.awt.Component;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertPath;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.DefaultListModel;



import com.itextpdf.text.DocumentException;
//import Utils.pdfSigner;



import java.awt.Toolkit;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.security.InvalidKeyException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.PrivateKey;
import java.security.SignatureException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateEncodingException;
import java.security.cert.CertificateException;
import java.util.ArrayList;
import java.util.Date;
import java.util.Enumeration;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.DefaultListModel;
import javax.swing.JFileChooser;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.filechooser.FileNameExtensionFilter;
import javax.swing.table.DefaultTableModel;
import sun.security.x509.*;

import javax.xml.namespace.QName;
import javax.xml.transform.Source;
import javax.xml.ws.Dispatch;
import javax.xml.transform.stream.StreamSource;
import javax.xml.ws.Service;
import java.io.StringReader;
import java.security.GeneralSecurityException;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.security.Provider;
import java.security.Security;
import java.util.ArrayList;
import java.util.List;
import javax.swing.*;
import sun.security.pkcs11.SunPKCS11;
import sun.security.mscapi.SunMSCAPI;
import netscape.javascript.*;
 /*
/**
 *
 * @author Administrador
 */
public class AppletLogin extends javax.swing.JApplet {
/**
     * Creates new form FirmarPDFFrm
     */
   
    DefaultListModel model=new DefaultListModel();
    private String resultLoadCert;
    
    private Object mensaje="Firma de pdf satisfactorio.";
    private Object mensaje2="Firma de pdf no se pudo realizar.";
    private ComboBoxModel modelo;
    private String selectlistitem;
    Object[] rowData;
    private int nroFilas = 0;
    public X509Certificate cert;
    public ArrayList<String> arrayPaths = new ArrayList<String>();
    Configuration config=new Configuration();
    private SmartCardGetData smartcard=new SmartCardGetData();
    private KeyStore aliaskeystore;
    private JLabel etiqueta = new JLabel("Hola");
    public String prueba = "prueba";
    public String cedulaUsuario = "";
    public String nombreUsuario = "";
    private Component frame;
    /**
     * Initializes the applet Certificados
     */
    @Override
    public void init() {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(AppletLogin.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(AppletLogin.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(AppletLogin.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(AppletLogin.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>
        
         /* Create and display the applet */
        try {
            java.awt.EventQueue.invokeAndWait(new Runnable() {
                public void run() {
                    
                    initComponents();
                    add(etiqueta);
                    
                }
            });
        } catch (Exception ex) {
            ex.printStackTrace();
        }
      //Cargar certificados
        aliaskeystore=CargarCert();
    }
  	public void cambia()
	{
		etiqueta.setText("Adios");
	}
  private KeyStore CargarCert(){
  
  KeyStore keyStore = null;
        // TODO add your handling code here:
        Provider pkcs11Provider = Security.getProvider("SunMSCAPI");//new sun.security.mscapi.SunMSCAPI();
        Security.addProvider(pkcs11Provider);
        try {
            keyStore = KeyStore.getInstance("Windows-MY", pkcs11Provider);
            keyStore.load(null,null);
        } catch (IOException ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
        } catch (NoSuchAlgorithmException ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
        } catch (CertificateException ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
        } catch (KeyStoreException ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
        }
        Enumeration elist = null;
        try {
            elist = keyStore.aliases();
        } catch (KeyStoreException ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
        }
          
        /* Se distingue el certificado de firma del DNIe gracias a la cadena de caracteres FIRMA que contiene */
        StringBuffer sb = new StringBuffer("AUTENTICACION");
        while (elist.hasMoreElements())
        {

            String certAlias = (String) elist.nextElement();
            this.aliaskeystore=keyStore;
            try
            {
            java.security.cert.Certificate certificado = aliaskeystore.getCertificate(certAlias);
            if (certificado instanceof X509Certificate)
                {
                    X509Certificate x509cert = (X509Certificate) certificado;
                    String issuerDN = x509cert.getIssuerDN().getName();
                    Date fechaHastaVigencia = x509cert.getNotAfter();
                    Date fechaDesdeVigencia = x509cert.getNotBefore();
                    Date fechaActual = new Date();
                    String[] arregloEntidad = issuerDN.split(",");
                    String entCertificadora = arregloEntidad[2].trim();
                    if (certAlias.contains(sb) && entCertificadora.equals("O=BANCO CENTRAL DE COSTA RICA") && FechaEnPeriodo(fechaDesdeVigencia,fechaHastaVigencia,fechaActual))
                    {
                        this.jComboBox1.addItem(certAlias );
                    }
                    }
            }
            catch (Exception ex)
            {}
       }
     return keyStore;
  }

    private boolean FechaEnPeriodo(Date principioPeriodo, Date finalPeriodo, Date fechaActual)
  {
      if (fechaActual.after(principioPeriodo)  && fechaActual.before(finalPeriodo))
      {
          return true;
      }
      else
      {
          return false;
      }
  }
    /**
     * This method is called from within the init() method to initialize the
     * form. WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        jLabel5 = new javax.swing.JLabel();
        jButtonCargar = new javax.swing.JButton();
        jComboBox1 = new javax.swing.JComboBox();

        setBackground(new java.awt.Color(204, 204, 204));

        jPanel2.setBackground(new java.awt.Color(255, 255, 255));
        jPanel2.setBorder(javax.swing.BorderFactory.createEtchedBorder(new java.awt.Color(51, 102, 255), null));
        jPanel2.setForeground(new java.awt.Color(255, 255, 255));

        jLabel5.setBackground(new java.awt.Color(255, 255, 255));
        jLabel5.setFont(new java.awt.Font("Tahoma", 1, 18));
        jLabel5.setText("Seleccionar Certificado");

        jButtonCargar.setBackground(new java.awt.Color(255, 255, 255));
        jButtonCargar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/GIF/Sello-CertificadoIcon.gif"))); // NOI18N
        jButtonCargar.setToolTipText("");
        jButtonCargar.setActionCommand("Mostrar certificados de smartcard");
        jButtonCargar.setBorder(new javax.swing.border.SoftBevelBorder(javax.swing.border.BevelBorder.RAISED));
        jButtonCargar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnmostrarCertificados(evt);
            }
        });

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(20, 20, 20)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(jButtonCargar, javax.swing.GroupLayout.PREFERRED_SIZE, 96, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                        .addComponent(jLabel5)
                        .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, 327, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(32, Short.MAX_VALUE))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createSequentialGroup()
                .addGap(22, 22, 22)
                .addComponent(jLabel5, javax.swing.GroupLayout.PREFERRED_SIZE, 33, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jButtonCargar)
                .addContainerGap(35, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jLabel1)
                .addGap(754, 754, 754))
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 381, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(425, 425, 425)
                .addComponent(jLabel1)
                .addContainerGap(318, Short.MAX_VALUE))
        );
    }// </editor-fold>//GEN-END:initComponents

    private void jbtnmostrarCertificados(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnmostrarCertificados
        
         try {
             KeyStore cc = null;
             try
             {
                 cc = KeyStore.getInstance("PKCS11");
                 cc.load(null, null);
             }
             catch (Exception ex)
                {ex.printStackTrace();}
            //Firmamos con smartcard 
             String valueCert = (String) jComboBox1.getSelectedItem();
             smartcard.firmaVerificar(aliaskeystore,null,valueCert);
             nombreUsuario = smartcard.getNombre();
             if (nombreUsuario != "")
             {
                        
             }
            else
             {
                 JOptionPane.showMessageDialog(frame,"Código o PIN incorrecto","Error",JOptionPane.ERROR_MESSAGE);
            }

             cedulaUsuario = smartcard.getCedula();
             JSObject window = JSObject.getWindow(this);
            window.setMember("userName", nombreUsuario);
            window.setMember("userID", cedulaUsuario);
            window.eval("pulsado()");


            // set JavaScript variable
           //return result=true;
        } catch (Exception ex) {
            Logger.getLogger(AppletLogin.class.getName()).log(Level.SEVERE, null, ex);
            //return result=false;
        }

    }//GEN-LAST:event_jbtnmostrarCertificados


    public static void main(String args[]) {
//crear un objeto f de la clase JFrame
        JFrame f = new JFrame("Test Applet/Aplicación");

        //crear una instancia de TestApplet
        AppletLogin ta = new AppletLogin();

        //añadir la instancia del applet al marco
        f.getContentPane().add("Center", ta);

        //inicializar las variables al ancho y el alto de la tag <applet>
        int width = 300;
        int height = 300;
        f.setSize(width, height);

        //llamar a init() y a start() si es necesario
        ta.init();
        ta.start();

        //hacer visible el marco
        f.setVisible(true);

    }
 
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButtonCargar;
    private javax.swing.JComboBox jComboBox1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JPanel jPanel2;
    // End of variables declaration//GEN-END:variables

    
}
