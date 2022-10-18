package SIGNER;
import java.io.IOException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.Provider;
import java.security.Security;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.security.cert.Certificate;
import java.security.*;
import java.util.Date;
import java.util.Enumeration;
import SMART_CARD.*;
import XML.DriverXML;
import XML.XML_AUTORIZACION;
import XML.XML_EMPRESA;
import XML.XML_STR;
import java.security.PrivateKey;
import java.security.UnrecoverableKeyException;
import java.util.ArrayList;
import java.util.List;

public class DigitalSigner {
    //=================================================
                        //VARIABLES
    //=================================================
    private Certificado certificado_firma_digital;
    private KeyStore keyStore;
    private final Provider pkcs11Provider = Security.getProvider("SunMSCAPI");//SunMSCAPI
    private String ERROR_DIGITAL_SIGNER, XML_FILE_PATH;
    //=================================================

    public DigitalSigner() {
        this.certificado_firma_digital = null;
        this.keyStore = null;
        Security.addProvider(this.pkcs11Provider);
        this.ERROR_DIGITAL_SIGNER = null;
    }//CONSTRUCTOR

    public Certificado getCertificado_firma_digital() {
        return certificado_firma_digital;
    }//GET

    public void setCertificado_firma_digital(Certificado certificado_firma_digital) {
        this.certificado_firma_digital = certificado_firma_digital;
    }//SET
    
    public String getXML_FILE_PATH() {
        return XML_FILE_PATH;
    }//GET

    public void setXML_FILE_PATH(String XML_FILE_PATH) {
        this.XML_FILE_PATH = XML_FILE_PATH;
    }//SET
    
    private boolean init_key_store() {
        try {
            //SE CREA UN INSTACIA DEL STORE DE LA MAQUINA EN EL KEYSTORE DE JAVA
            this.keyStore = KeyStore.getInstance("Windows-MY", this.pkcs11Provider);
            //this.keyStore = KeyStore.getInstance("PKCS11");
            //SE HACE LA CARGA DEL STORE
            this.keyStore.load(null, null);
            return true;
        } catch (IOException | NoSuchAlgorithmException | CertificateException | KeyStoreException ex) {
            this.ERROR_DIGITAL_SIGNER = ex.getMessage();
            return false;
        }
    }//FUNCION
    
    private Enumeration get_lst_certificados_alias() {
        try {
            Enumeration lista_alias = null;
            //SE OBTIENE LA LISTA DE TODOS LOS ALIAS DE LOS CERTIFICADOS ENCONTRADOS EN EL STORE
            lista_alias = keyStore.aliases();
            return lista_alias;
        } catch (KeyStoreException ex) {
            this.ERROR_DIGITAL_SIGNER = ex.getMessage();
            return null;
        }
    }//FUNCION
    
    public Certificado get_certificado_valido(String PIN) {
        if (this.init_key_store()) {
            Enumeration elist = get_lst_certificados_alias();
            if (elist != null) {
                while (elist.hasMoreElements()) {
                    String certAlias = (String) elist.nextElement();
                    KeyStore aliaskeystore=keyStore;
                    try {
                        //Si se conoce el nombre o alias del certificado se puede obtener del keystore
                        Certificate certificado = aliaskeystore.getCertificate(certAlias);
                        if (certificado instanceof X509Certificate) {
                            X509Certificate x509cert = (X509Certificate) certificado;
                            //SE OBTIENE EL NOMBRE DE LA ENTIDAD CERTIFICADORA QUE EMITIO EL CERTIFICADO
                            String issuerDN = x509cert.getIssuerDN().getName();
                            //SE HACE UN GET A LA FECHA EN QUE EXPIRA EL CERTIFICADO
                            Date fechaHastaVigencia = x509cert.getNotAfter();
                            //OTRO GET A LA FECHA EN LA QUE SE EMITIO EL CERTIFICADO
                            Date fechaDesdeVigencia = x509cert.getNotBefore();
                            //UN GET DE LA FECHA DEL DIA PRECENTE
                            Date fechaActual = new Date();
                            //SPLIT HECHO A LA INFORMACION EMITIDA EN EL CERTIFICADO
                            String[] arregloEntidad = issuerDN.split(",");
                            if (arregloEntidad.length >= 2) {//EVITA UNA EXEPCION DEL TIPO >> INDEX FUERA DE LOS LIMITES DEL ARRAY
                                String entCertificadora = arregloEntidad[2].trim();
                                if (certAlias.contains("FIRMA") && entCertificadora.equals("O=BANCO CENTRAL DE COSTA RICA")) {
                                    if (FechaEnPeriodo(fechaDesdeVigencia,fechaHastaVigencia,fechaActual)) {
                                        PrivateKey key = (PrivateKey)aliaskeystore.getKey(certAlias, PIN.toCharArray());
                                        //System.out.println("LLAVE LLAVE >> " + key.hashCode());
                                        Sujeto sujeto = this.get_sujeto(x509cert);
                                        Emisor emisor = this.get_emisor(x509cert);
                                        this.certificado_firma_digital = new Certificado(x509cert, certAlias, key, emisor, sujeto);
                                    } else {
                                        this.ERROR_DIGITAL_SIGNER = "El Certificado " + certAlias + " ha vencido !!!";
                                    }
                                    break;
                                }
                            }
                        }
                    } catch (KeyStoreException | NoSuchAlgorithmException | UnrecoverableKeyException ex) {
                        this.ERROR_DIGITAL_SIGNER = ex.getMessage();
                    }
                }
                return this.certificado_firma_digital;
            } else {
                return null;
            }
        } else {
            return null;
        }
    }//FUNCION
    
    public List<Certificado> get_lst_certificados_firma_validos() {
        if (this.init_key_store()) {
            Enumeration elist = get_lst_certificados_alias();
            if (elist != null) {
                List<Certificado> _LST_CERTIFICADOS_VALIDOS_ = new ArrayList<Certificado>();
                while (elist.hasMoreElements()) {
                    String certAlias = (String) elist.nextElement();
                    KeyStore aliaskeystore=keyStore;
                    try {
                        //Si se conoce el nombre o alias del certificado se puede obtener del keystore
                        Certificate certificado = aliaskeystore.getCertificate(certAlias);
                        if (certificado instanceof X509Certificate) {
                            X509Certificate x509cert = (X509Certificate) certificado;
                            //SE OBTIENE EL NOMBRE DE LA ENTIDAD CERTIFICADORA QUE EMITIO EL CERTIFICADO
                            String issuerDN = x509cert.getIssuerDN().getName();
                            //SE HACE UN GET A LA FECHA EN QUE EXPIRA EL CERTIFICADO
                            Date fechaHastaVigencia = x509cert.getNotAfter();
                            //OTRO GET A LA FECHA EN LA QUE SE EMITIO EL CERTIFICADO
                            Date fechaDesdeVigencia = x509cert.getNotBefore();
                            //UN GET DE LA FECHA DEL DIA PRECENTE
                            Date fechaActual = new Date();
                            //SPLIT HECHO A LA INFORMACION EMITIDA EN EL CERTIFICADO
                            String[] arregloEntidad = issuerDN.split(",");
                            if (arregloEntidad.length >= 2) {//EVITA UNA EXEPCION DEL TIPO >> INDEX FUERA DE LOS LIMITES DEL ARRAY
                                String entCertificadora = arregloEntidad[2].trim();
                                if (certAlias.contains("FIRMA") && entCertificadora.equals("O=BANCO CENTRAL DE COSTA RICA")) {
                                    if (FechaEnPeriodo(fechaDesdeVigencia,fechaHastaVigencia,fechaActual)) {
                                        PrivateKey key = (PrivateKey)aliaskeystore.getKey(certAlias, "XXXX".toCharArray());
                                        //System.out.println("LLAVE LLAVE >> " + key.hashCode());
                                        Sujeto sujeto = this.get_sujeto(x509cert);
                                        Emisor emisor = this.get_emisor(x509cert);
                                        _LST_CERTIFICADOS_VALIDOS_.add(new Certificado(x509cert, certAlias, key, emisor, sujeto));
                                    } else {
                                        this.ERROR_DIGITAL_SIGNER = "Se encontró certificado vencido";
                                    }
                                }
                            }
                        }
                    } catch (KeyStoreException | NoSuchAlgorithmException | UnrecoverableKeyException ex) {
                        this.ERROR_DIGITAL_SIGNER = ex.getMessage();
                    }
                }
                if (_LST_CERTIFICADOS_VALIDOS_.size() > 0) {
                    return _LST_CERTIFICADOS_VALIDOS_;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        } else {
            return null;
        }
    }//FUNCION
    
    public Certificado get_certificado_alias(String ALIAS) {
        if (this.init_key_store()) {
            Enumeration elist = get_lst_certificados_alias();
            if (elist != null) {
                while (elist.hasMoreElements()) {
                    String certAlias = (String) elist.nextElement();
                    KeyStore aliaskeystore=keyStore;
                    try {
                        //Si se conoce el nombre o alias del certificado se puede obtener del keystore
                        Certificate certificado = aliaskeystore.getCertificate(certAlias);
                        if (certificado instanceof X509Certificate) {
                            X509Certificate x509cert = (X509Certificate) certificado;
                            //SE OBTIENE EL NOMBRE DE LA ENTIDAD CERTIFICADORA QUE EMITIO EL CERTIFICADO
                            String issuerDN = x509cert.getIssuerDN().getName();
                            //SE HACE UN GET A LA FECHA EN QUE EXPIRA EL CERTIFICADO
                            Date fechaHastaVigencia = x509cert.getNotAfter();
                            //OTRO GET A LA FECHA EN LA QUE SE EMITIO EL CERTIFICADO
                            Date fechaDesdeVigencia = x509cert.getNotBefore();
                            //UN GET DE LA FECHA DEL DIA PRECENTE
                            Date fechaActual = new Date();
                            //SPLIT HECHO A LA INFORMACION EMITIDA EN EL CERTIFICADO
                            String[] arregloEntidad = issuerDN.split(",");
                            if (arregloEntidad.length >= 2) {//EVITA UNA EXEPCION DEL TIPO >> INDEX FUERA DE LOS LIMITES DEL ARRAY
                                String entCertificadora = arregloEntidad[2].trim();
                                if (certAlias.contains("FIRMA") && entCertificadora.equals("O=BANCO CENTRAL DE COSTA RICA")) {
                                    if (FechaEnPeriodo(fechaDesdeVigencia,fechaHastaVigencia,fechaActual)) {
                                        PrivateKey key = (PrivateKey)aliaskeystore.getKey(certAlias, "XXXX".toCharArray());
                                        //System.out.println("LLAVE LLAVE >> " + key.hashCode());
                                        Sujeto sujeto = this.get_sujeto(x509cert);
                                        Emisor emisor = this.get_emisor(x509cert);
                                        this.certificado_firma_digital = new Certificado(x509cert, certAlias, key, emisor, sujeto);
                                    } else {
                                        this.ERROR_DIGITAL_SIGNER = "El Certificado " + certAlias + " ha vencido !!!";
                                    }
                                    break;
                                }
                            }
                        }
                    } catch (KeyStoreException | NoSuchAlgorithmException | UnrecoverableKeyException ex) {
                        this.ERROR_DIGITAL_SIGNER = ex.getMessage();
                    }
                }
                return this.certificado_firma_digital;
            } else {
                return null;
            }
        } else {
            return null;
        }
    }//FUNCION
    
    public boolean validar_PIN(String PIN, KeyStore ks, String alias) {
        try {
            PrivateKey privateKey = (PrivateKey) ks.getKey(alias, PIN.toCharArray());
            System.out.println("Private key: " + privateKey);
            PrivateKey key = (PrivateKey)ks.getKey(alias, PIN.toCharArray());
            if (privateKey == key)
            {
                return true;
            } else {
                return false;
            }
        } catch (KeyStoreException | NoSuchAlgorithmException | UnrecoverableKeyException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return false;
        }
    }//FUNCION
    
    public static boolean FechaEnPeriodo(Date principioPeriodo, Date finalPeriodo, Date fechaActual) {
      if (fechaActual.after(principioPeriodo)  && fechaActual.before(finalPeriodo))
      {
          return true;
      }
      else
      {
          return false;
      }
  }//FUNCION

    public String getERROR_DIGITAL_SIGNER() {
        return ERROR_DIGITAL_SIGNER;
    }//GET
    
    public Sujeto get_sujeto(X509Certificate certificado) {
        if (certificado != null) {
            String[] info_cert_split = certificado.getSubjectDN().getName().split(",");
            String cn = info_cert_split[4].split("=")[1] + " " + info_cert_split[5].split("=")[1];
            String ou = info_cert_split[1].split("=")[1];
            String o = info_cert_split[2].split("=")[1];
            String c = info_cert_split[3].split("=")[1];
            String serial_number = info_cert_split[6].split("=")[1];
            return new Sujeto(cn, ou, o, c, serial_number);
        } else {
            return null;
        }
    }//FUNCION
    
    public Emisor get_emisor(X509Certificate certificado) {
        if (certificado != null) {
            String[] info_cert_split = certificado.getIssuerDN().getName().split(",");
            String cn = info_cert_split[0].split("=")[1];
            String ou = info_cert_split[1].split("=")[1];
            String o = info_cert_split[2].split("=")[1];
            String c = info_cert_split[3].split("=")[1];
            String serial_number = info_cert_split[4].split("=")[1];
            return new Emisor(cn, ou, o, c, serial_number);
        } else {
            return null;
        }
    }//FUNCION
    
    public boolean sign_file_xml(Certificado certificado, DriverXML xml_driver) {
        try {
            Signature signatureAlgorithm = Signature.getInstance("SHA1withRSA");
            signatureAlgorithm.initSign(certificado.getLlave());
            byte[] digitalSignature = signatureAlgorithm.sign();
            System.out.println("FIRMA INFO >> " + signatureAlgorithm.toString());
            signatureAlgorithm.update(digitalSignature);
            System.out.println("Tamaño del array >> " + digitalSignature.length);
            xml_driver.set_info_certificado(certificado.getEmisor(), certificado.getSujeto(), digitalSignature);
            return true;
        } catch (InvalidKeyException | NoSuchAlgorithmException | SignatureException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return false;
        }
    }//FUNCION
    
    public String sign_file_xml(Certificado certificado, String FORMULARIO_TAG) {
        try {
            Signature signatureAlgorithm = Signature.getInstance("SHA1withRSA");
            signatureAlgorithm.initSign(certificado.getLlave());
            byte[] digitalSignature = signatureAlgorithm.sign();
            signatureAlgorithm.update(digitalSignature);
            return new XML_STR(certificado.getEmisor(), certificado.getSujeto(), FORMULARIO_TAG, digitalSignature).getXML_FILE_STR();
        } catch (InvalidKeyException | NoSuchAlgorithmException | SignatureException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return null;
        }
    }//FUNCION
    
    public String sign_xml_EMPRESA(Certificado certificado, String INFO_EMPRESA_TAG) {
        try {
            Signature signatureAlgorithm = Signature.getInstance("SHA1withRSA");
            signatureAlgorithm.initSign(certificado.getLlave());
            byte[] digitalSignature = signatureAlgorithm.sign();
            signatureAlgorithm.update(digitalSignature);
            return new XML_EMPRESA(certificado.getEmisor(), certificado.getSujeto(), INFO_EMPRESA_TAG, digitalSignature).getEMPRESA_XML_STR();
        } catch (InvalidKeyException | NoSuchAlgorithmException | SignatureException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return null;
        }
    }//FUNCION
    
    public String sign_xml_AUTORIZACION(Certificado certificado, String INFO_AUTORIZACION_TAG) {
        try {
            Signature signatureAlgorithm = Signature.getInstance("SHA1withRSA");
            signatureAlgorithm.initSign(certificado.getLlave());
            byte[] digitalSignature = signatureAlgorithm.sign();
            signatureAlgorithm.update(digitalSignature);
            return new XML_AUTORIZACION(certificado.getEmisor(), certificado.getSujeto(), INFO_AUTORIZACION_TAG, digitalSignature).getAUTORIZACION_XML_STR();
        } catch (InvalidKeyException | NoSuchAlgorithmException | SignatureException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return null;
        }
    }//FUNCION
    
    public byte[] open_privateKey_from_certificate(Certificado certificado) {
        try {
            Signature signatureAlgorithm = Signature.getInstance("SHA1withRSA");
            signatureAlgorithm.initSign(certificado.getLlave());
            byte[] digitalSignature = signatureAlgorithm.sign();
            signatureAlgorithm.update(digitalSignature);
            return digitalSignature;
        } catch (NoSuchAlgorithmException | SignatureException | InvalidKeyException e) {
            this.ERROR_DIGITAL_SIGNER = e.getMessage();
            return null;
        }
    }//FUNCION
    
}//CLASE
