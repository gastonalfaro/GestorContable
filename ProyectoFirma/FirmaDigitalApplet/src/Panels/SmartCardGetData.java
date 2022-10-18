package Panels;
import Panels.*;
import Utils.Base64Utils;
import java.applet.Applet;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.*;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.ByteArrayInputStream;
import java.util.Arrays;
import java.util.Enumeration;
import java.util.List;
import java.security.*;
import java.security.cert.CertPath;
import java.security.cert.Certificate;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.lang.reflect.Constructor;

import netscape.javascript.JSException;
import netscape.javascript.JSObject;
//import Utils.pdfSigner;
import com.itextpdf.text.DocumentException;
import iaik.pkcs.pkcs11.Module;
import iaik.pkcs.pkcs11.Session;
import iaik.pkcs.pkcs11.Slot;
import iaik.pkcs.pkcs11.Token;
import iaik.pkcs.pkcs11.Token.SessionType;
import iaik.pkcs.pkcs11.objects.X509PublicKeyCertificate;

import java.io.*;
import java.util.Calendar;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.Provider;
import java.security.Security;
import java.security.cert.Certificate;
import java.security.cert.*;
import java.security.KeyStore;
import java.util.*;
import java.util.List;


public class SmartCardGetData {

    private static final String FILE_NAME_FIELD_PARAM = "fileNameField";
    private static final String CERT_CHAIN_FIELD_PARAM = "certificationChainField";
    private static final String SIGNATURE_FIELD_PARAM = "signatureField";
    private static final String SIGN_BUTTON_CAPTION_PARAM = "signButtonCaption";

   
    private static final String X509_CERTIFICATE_TYPE = "X.509";
    private static final String CERTIFICATION_CHAIN_ENCODING = "PkiPath";
    private static final String DIGITAL_SIGNATURE_ALGORITHM_NAME = "SHA1withRSA";
    
    static CertificationChainAndSignatureBase64 CertificationChainAndSignatureBase64;
    private static final String PKCS11_KEYSTORE_TYPE = "PKCS11";
    private static final String SUN_PKCS11_PROVIDER_CLASS = "sun.security.pkcs11.SunPKCS11";
     private String m_FileName;
    private String m_AutenticacionLabel = "NOT_SET";
    private String m_SignLabel = "NOT_SET";
    private String m_IssuerCertificate = "NOT_SET";

   public static String providerName = null;

    private int m_CurrentIndex = -1;

    private Button mSignButton;
    private boolean signingResult;
    public String nombreFirma = "";
    public String cedulaFirma = "";

   public String getNombre()
    {
       return nombreFirma;
   }

   public String getCedula()
    {
       return cedulaFirma;
   }


    
    public boolean firmaVerificar(KeyStore keyStore,String aPkcs11LibraryFileName,String valueCert)
    throws DocumentSignException, GeneralSecurityException, IOException, DocumentException, Exception {
        // Show a dialog for choosing PKCS#11 implementation library and smart card PIN
        PKCS11LibraryFileAndPINCodeDialog pkcs11Dialog =new PKCS11LibraryFileAndPINCodeDialog();
        boolean dialogConfirmed;
        try {
         dialogConfirmed = pkcs11Dialog.run();
        } finally {
            pkcs11Dialog.dispose();
        }

        if (dialogConfirmed) {
           //String oldButtonLabel = mSignButton.getLabel();
            //mSignButton.setLabel("procesando...");
            //mSignButton.setEnabled(false);
            try {
                String pkcs11LibraryFileName ="C:\\Windows\\System32\\asepkcs.dll"; //pkcs11Dialog.getLibraryFileName();
                String pinCode = pkcs11Dialog.getSmartCardPINCode();

                // Do the actual signing of the document with the smart card
                signingResult = firmaChecka(keyStore,pkcs11LibraryFileName, pinCode,valueCert);
                return signingResult;
            } catch(Exception ex) {
                //mSignButton.setLabel(oldButtonLabel);
                //mSignButton.setEnabled(true);
                return signingResult;
            }
        }
        else {
            return signingResult;
        }
    }
    /*Verifica certificado de la tarjeta*/
    private boolean firmaChecka(KeyStore ks,String aPkcs11LibraryFileName, String aPinCode,String selectCert) throws DocumentSignException, IOException, DocumentException, GeneralSecurityException, Exception {
         
       /* if (aPkcs11LibraryFileName.length() == 0) {
            String errorMessage = "Debe escoger un PCKS#11 " +
                "para la lectura y validación de la librería del smart card (.dll or .so file)!";
            throw new DocumentSignException(errorMessage);
        }*/
        if (aPkcs11LibraryFileName.length() == 0) {
            String errorMessage = "Debe escoger un PCKS#11 " +
                "para la lectura y validación de la librería del smart card (.dll or .so file)!";
            throw new DocumentSignException(errorMessage);
        }
        // Load the keystore from the smart card using the specified PIN code
//       loadKeyStoreFromSmartCard(ks,aPinCode.toCharArray(),selectCert);
      nombreFirma = loadKeyStoreFromSmartCard2(ks, selectCert,selectCert.getBytes(),aPinCode,selectCert);
     signingResult=true;



        return signingResult;
    }

   /** 
	 * Loads the keystore from the smart card using its PKCS#11 implementation
	 * library and the Sun PKCS#11 security provider. The PIN code for accessing
	 * the smart card is required.
	 *
	 * @param PKCS11LibraryFileName
	 * @param PIN
	 * @return
	 * @throws PKCS11KeyStoreException
	 */
   public  String loadKeyStoreFromSmartCard(KeyStore ks, char[] PIN,String alias) throws Exception,CertificateExpiredException, //boolean
                                   CertificateNotYetValidException{		
	
             X509Certificate cert = null;
             boolean result=true;
             String nombreCompletoFirma = "";
          //Validar certificado seleccionado contra PIN
             char[] PinEjemplo = new char[]{1,2,3,4};
            cert =   (X509Certificate) ks.getCertificate(alias);
   //System.out.println("Certificate: " + cert);
   PrivateKey privateKey =      (PrivateKey) ks.getKey(alias, PinEjemplo);
  System.out.println("Private key: " + privateKey);
   PrivateKey key = (PrivateKey)ks.getKey(alias, PIN);
            if (privateKey == key)
            {
                System.out.println("son iguales");
            }
            if(key!=null){
             cert=( X509Certificate ) ks.getCertificate( alias );
             String infoCert = cert.getSubjectDN().getName();
             String[] arregloInfoCert = infoCert.split(",");
             String nombreFirma = arregloInfoCert[4].split("=")[1];
             String apellidoFirma = arregloInfoCert[5].split("=")[1];
             nombreCompletoFirma = nombreFirma + " " + apellidoFirma;
             cedulaFirma = arregloInfoCert[6].split("=")[1];
             PublicKey certkey = cert.getPublicKey();
             String prueba = "";
             try
             {
                //X509PublicKeyCertificate certky = (X509PublicKeyCertificate)cert.getPublicKey();
                cert.checkValidity() ;//revisar validez de certificado*/
                
             }
             catch(Exception ex)
             {
                 
                 prueba = ex.getMessage();
             }
            OcspClient oscpClient = new OcspClient(cert.getSignature());
             //OcspClient oscpClient = new OcspClient(certky.getValue().getByteArrayValue());
                       try {
                            oscpClient.getPublicKeyCertificate().checkValidity();
                            CertificateStatus status = oscpClient.consultarEstadoDeCertificado(oscpClient.getPublicKeyCertificate(), oscpClient.leerCertificado("NOT_SET"));
                           if (status == CertificateStatus.Good) {                           // CertificateStatus status = oscpClient.consultarEstadoDeCertificado(cert.getPublicKey(), oscpClient.leerCertificado(m_IssuerCertificate));

                                //out_Error.append("OK");
                            //    result = true;
                           } else if (status == CertificateStatus.Revoked) {
                               // out_Error.append("Certificado Revocado");
                           } else {
                               // out_Error.append("Certificado/Respuesta Desconocido(s)");
                            }
                        }
                        catch(Exception ex) {
                            //out_Error.append( ex.getMessage());
                      }
               result=true;
             }
           
        return nombreCompletoFirma;
	//return result;
        }

   //Devuelve los datos del propietario de certificado, nulo en caso de que no sea posible accederlo
    protected String loadKeyStoreFromSmartCard2(KeyStore ks, String certName, byte[] cert, String aSmartCardPIN, String alias) throws GeneralSecurityException, IOException {
        // First configure the Sun PKCS#11 provider. It requires a stream (or file)
        // containing the configuration parameters - "name" and "library".
        File file = new File("C:\\Windows\\System32\\asepkcs.dll");
        String nombreCompletoFirma = "";
        if(!file.exists()){
            throw new IOException("Archivo de certificado no encontrado.");
        }
        String pkcs11ConfigSettings = "";
        pkcs11ConfigSettings = "name = SmartCard\n" + "library = " + file.getAbsolutePath();
        byte[] pkcs11ConfigBytes = pkcs11ConfigSettings.getBytes();
        ByteArrayInputStream confStream = new ByteArrayInputStream(pkcs11ConfigBytes);

        Class<?> sunPkcs11Class;
        Constructor<?> pkcs11Constr;
        // Instantiate the provider dynamically with Java reflection
        try
        {
        sunPkcs11Class = Class.forName(SUN_PKCS11_PROVIDER_CLASS);
        pkcs11Constr = sunPkcs11Class.getConstructor(java.io.InputStream.class);
        Provider pkcs11Provider = (Provider) pkcs11Constr.newInstance(confStream);
        providerName = pkcs11Provider.getName();
        Security.addProvider(pkcs11Provider);
        }
        catch (Exception ex)
        {}

        // Read the keystore form the smart card
        char[] pin = aSmartCardPIN.toCharArray();
        KeyStore keyStore = null;

        try
        {
        String tipoKs = KeyStore.getDefaultType();
        keyStore =  KeyStore.getInstance(PKCS11_KEYSTORE_TYPE);
        //Valida que pin concuerde con certificado
        keyStore.load(null, pin);
        PrivateKey key = (PrivateKey)ks.getKey(alias, pin);

        //Si key no es nulo se obtienen datos de certificado
                    if(key!=null){
             X509Certificate cert2=( X509Certificate ) ks.getCertificate( alias );
             String infoCert = cert2.getSubjectDN().getName();
             String[] arregloInfoCert = infoCert.split(",");
             String nombreFirma = arregloInfoCert[4].split("=")[1];
             String apellidoFirma = arregloInfoCert[5].split("=")[1];
             nombreCompletoFirma = nombreFirma + " " + apellidoFirma;
             cedulaFirma = arregloInfoCert[6].split("=")[1];}
        }
        catch(Exception ex){
        String error = ex.getMessage();
        }
        return nombreCompletoFirma;
}


    /**
     * @return private key and certification chain corresponding to it, extracted from
     * given keystore. The keystore is considered to have only one entry that contains
     * both certification chain and its corresponding private key. If the keystore has
     * no entries, an exception is thrown.
     */
   private PrivateKeyAndCertChain getPrivateKeyAndCertChain(
        KeyStore aKeyStore,char[] PIN)
    throws GeneralSecurityException {
        Enumeration aliasesEnum = aKeyStore.aliases();
        if (aliasesEnum.hasMoreElements()) {
            String alias = (String)aliasesEnum.nextElement();
            Certificate[] certificationChain = aKeyStore.getCertificateChain(alias);
            PrivateKey privateKey = (PrivateKey) aKeyStore.getKey(alias, PIN);
            PrivateKeyAndCertChain result = new PrivateKeyAndCertChain();
            result.mPrivateKey = privateKey;
            result.mCertificationChain = certificationChain;
            return result;
        } else {
            throw new KeyStoreException("El keystore esta vacio!");
        }
    }

    /**
     * @return Base64-encoded ASN.1 DER representation of given X.509 certification
     * chain.
     */
    private String encodeX509CertChainToBase64(Certificate[] aCertificationChain)
    throws CertificateException {
        List certList = Arrays.asList(aCertificationChain);
        CertificateFactory certFactory =
            CertificateFactory.getInstance(X509_CERTIFICATE_TYPE);
        CertPath certPath = certFactory.generateCertPath(certList);
        byte[] certPathEncoded = certPath.getEncoded(CERTIFICATION_CHAIN_ENCODING);
        String base64encodedCertChain = Base64Utils.base64Encode(certPathEncoded);
        return base64encodedCertChain;
    }

    /**
     * Reads the specified file into a byte array.
     */
    private byte[] readFileInByteArray(String aFileName)
    throws IOException {
        File file = new File(aFileName);
        FileInputStream fileStream = new FileInputStream(file);
        try {
            int fileSize = (int) file.length();
            byte[] data = new byte[fileSize];
            int bytesRead = 0;
            while (bytesRead < fileSize) {
                bytesRead += fileStream.read(data, bytesRead, fileSize-bytesRead);
            }
            return data;
        }
        finally {
            fileStream.close();
        }
    }

    /**
     * Signs given document with a given private key.
     */
    private byte[] signDocument(byte[] aDocument, PrivateKey aPrivateKey)
    throws GeneralSecurityException {
        Signature signatureAlgorithm =Signature.getInstance(DIGITAL_SIGNATURE_ALGORITHM_NAME);
        signatureAlgorithm.initSign(aPrivateKey);
        signatureAlgorithm.update(aDocument);
        byte[] digitalSignature = signatureAlgorithm.sign();
        return digitalSignature;
    }

    /**
     * Data structure that holds a pair of private key and
     * certification chain corresponding to this private key.
     */
    static class PrivateKeyAndCertChain {
        public PrivateKey mPrivateKey;
        public Certificate[] mCertificationChain;
    }

    /**
     * Data structure that holds a pair of Base64-encoded
     * certification chain and digital signature.
     */
    static class CertificationChainAndSignatureBase64 {
        public String mCertificationChain = null;
        public String mSignature = null;
    }

    /**
     * Exception class used for document signing errors.
     */
    static class DocumentSignException extends Exception {
        public DocumentSignException(String aMessage) {
            super(aMessage);
        }

        public DocumentSignException(String aMessage, Throwable aCause) {
            super(aMessage, aCause);
        }
    }

}