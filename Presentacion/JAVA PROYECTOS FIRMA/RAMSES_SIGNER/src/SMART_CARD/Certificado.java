package SMART_CARD;
import java.security.PrivateKey;
import java.security.cert.X509Certificate;

public class Certificado {
    private X509Certificate certificado_x509;
    private String alias_del_certificado;
    private PrivateKey llave;
    private Emisor emisor;
    private Sujeto sujeto;

    public Certificado(X509Certificate certificado_x509, String alias_del_certificado, PrivateKey llave, Emisor emisor, Sujeto sujeto) {
        this.certificado_x509 = certificado_x509;
        this.alias_del_certificado = alias_del_certificado;
        this.llave = llave;
        this.emisor = emisor;
        this.sujeto = sujeto;
    }//CONSTRUCTOR

    public Emisor getEmisor() {
        return emisor;
    }//GET

    public void setEmisor(Emisor emisor) {
        this.emisor = emisor;
    }//SET

    public Sujeto getSujeto() {
        return sujeto;
    }//GET

    public void setSujeto(Sujeto sujeto) {
        this.sujeto = sujeto;
    }//SET
    
    public X509Certificate getCertificado_x509() {
        return certificado_x509;
    }//GET

    public void setCertificado_x509(X509Certificate certificado_x509) {
        this.certificado_x509 = certificado_x509;
    }//SET

    public String getAlias_del_certificado() {
        return alias_del_certificado;
    }//GET

    public void setAlias_del_certificado(String alias_del_certificado) {
        this.alias_del_certificado = alias_del_certificado;
    }//SET

    public PrivateKey getLlave() {
        return llave;
    }//GET

    public void setLlave(PrivateKey llave) {
        this.llave = llave;
    }//SET
    
    
    
}//CLASE
