
/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Panels;
import java.io.*;
import java.net.URL;
import java.net.URLConnection;
import java.security.*;
import java.security.cert.*;
import java.util.*;
import java.security.cert.X509Certificate;
import java.security.cert.PKIXParameters;
/**
 *
 * @author IEUser
 */
public class ValidezCert {

    public static boolean CetificadoEsValido(X509Certificate certificado)
    {
        boolean esValido = false;
        try
        {
        Vector <X509Certificate> certs = new Vector<X509Certificate>();
        CertPath cp = null;
        certs.add(certificado);
        URL url = new URL("http://fdi.sinpe.fi.cr/repositorio/CA%20SINPE%20-%20PERSONA%20FISICA(1).crl");
        CertificateFactory cf = CertificateFactory.getInstance("X509");
        cp = (CertPath) cf.generateCertPath(certs);
        	    // load the root CA cert

	    // init trusted certs
	    TrustAnchor ta = new TrustAnchor(certificado, null);
	    Set<TrustAnchor> trustedCerts = new HashSet<TrustAnchor>();
	    trustedCerts.add(ta);

	    // init PKIX parameters
            PKIXParameters params = new PKIXParameters(trustedCerts);
	    if (url != null) {
		URLConnection connection = url.openConnection();
		connection.setDoInput(true);
		connection.setUseCaches(false);
		DataInputStream inStream =
		    new DataInputStream(connection.getInputStream());
		X509CRL crl = (X509CRL)cf.generateCRL(inStream);
		inStream.close();
	        params.addCertStore(CertStore.getInstance("Collection",
		    new CollectionCertStoreParameters(
			Collections.singletonList(crl))));
	    }

            	    // perform validation
	    CertPathValidator cpv = CertPathValidator.getInstance("PKIX");
	    PKIXCertPathValidatorResult cpv_result  =
		(PKIXCertPathValidatorResult) cpv.validate(cp, params);
	    X509Certificate trustedCert = (X509Certificate)
		cpv_result.getTrustAnchor().getTrustedCert();
            if (trustedCert == null) {
		esValido = false;
	    } else {
		esValido = true;
	    }
        }
        catch(Exception ex)
        {
            String error = ex.getMessage();
            String lol = "x";
        }
        return esValido;
    }


}
