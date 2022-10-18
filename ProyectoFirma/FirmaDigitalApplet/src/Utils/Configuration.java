package Utils;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;


 
public class Configuration {
    
    public String OCSPURL;
    public String TSACLIENT;
    public String TEMPLATEPDF;
    public String PCKCS11;
    
   
  //Log4j instanciacion
	
    
	public void getConfiguration() throws IOException{
        Properties prop = new Properties();
        InputStream is = null;
 
        try {
            /*is = new FileInputStream("C:\\SipCfia_Files\\configuracion.properties");
            prop.load(is);*/
        	
        	//set the properties value
    		prop.setProperty("OCSP_URL", "http://ocsp.sinpe.fi.cr/ocsp");
    		prop.setProperty("TSA_CLIENT", "http://tsa.sinpe.fi.cr/tsaHttp/");
    		prop.setProperty("TEMPLATEPDF", "C:\\Users\\isanchez\\Desktop\\Pruebas\\hojafirmas.pdf");
                prop.setProperty("PCKCS11", "C:\\Users\\isanchez\\Documents\\NetBeansProjects\\pdfSignerGSI\\pckcs11.cfg");
    		
    		//save properties to project root folder
    		prop.store(new FileOutputStream("config.properties"), null);
        
        
        } catch(IOException e) {
            System.out.println(e.toString());
            
        }
 
       // Acceder a las propiedades del archivo de configuracion
       /* user=prop.getProperty("epower.usuario");
        pass=prop.getProperty("epower.password");
        pathfile=prop.getProperty("epower.pathfile");
        IOR=prop.getProperty("epower.IOR") ;*/
       OCSPURL=prop.getProperty("OCSP_URL");
       TSACLIENT=prop.getProperty("TSA_CLIENT");
       TEMPLATEPDF=prop.getProperty("TEMPLATEPDF");
       PCKCS11=prop.getProperty("PCKCS11");
        
        
    }
}