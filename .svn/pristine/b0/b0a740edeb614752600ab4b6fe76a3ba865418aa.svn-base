package XML;
import SMART_CARD.Emisor;
import SMART_CARD.Sujeto;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Arrays;
import org.jdom2.Document;
import org.jdom2.Element;
import org.jdom2.JDOMException;
import org.jdom2.input.SAXBuilder; 
import org.jdom2.output.Format;
import org.jdom2.output.XMLOutputter;


public class DriverXML {
    private String ERROR_DRIVER_XML, file_path;
    private SAXBuilder builder;
    private File xmlFile;
    private Document document;
    
    public DriverXML(String file_path) {
        this.ERROR_DRIVER_XML = null;
        this.file_path = file_path;
    }//CONSTRUCTOR
    
    public byte[] get_bytes_file(String file_path) {
        try {
            File f = new File(file_path);
            FileInputStream f_input_s = new FileInputStream(f);
            BufferedInputStream b_input_s = new BufferedInputStream(f_input_s);
            byte[] buffer = new byte[(int)f.length()];
            System.out.println("Tamaño del archivo >> " + f.length());
            b_input_s.read(buffer);
            return buffer;
        } catch (IOException e) {
            System.out.println("ERROR >> " + e.getMessage());
            this.ERROR_DRIVER_XML = e.getMessage();
            return null;
        }
    }//FUNCION
    
    public boolean make_file_from_bytes(String file_path, byte[] file_bytes) {
        try {
            File f = new File(file_path);
            FileOutputStream f_output_stream = new FileOutputStream(f);
            f_output_stream.write(file_bytes);
            f_output_stream.close();
            return true;
        } catch (IOException e) {
            this.ERROR_DRIVER_XML = e.getMessage();
            return false;
        }
    }//FUNCION

    public String getERROR_DRIVER_XML() {
        return ERROR_DRIVER_XML;
    }//GET

    public String getFile_path() {
        return file_path;
    }//GET

    public void setFile_path(String file_path) {
        this.file_path = file_path;
    }//SET
    
    private void set_info_emisor(Emisor emisor, Element rootNode) {
        rootNode.getChild("certificado").getChild("emision").getChild("cn").setText(emisor.getCn());
        rootNode.getChild("certificado").getChild("emision").getChild("ou").setText(emisor.getOu());
        rootNode.getChild("certificado").getChild("emision").getChild("o").setText(emisor.getO());
        rootNode.getChild("certificado").getChild("emision").getChild("c").setText(emisor.getC());
        rootNode.getChild("certificado").getChild("emision").getChild("serialnumber").setText(emisor.getSerial_number());
    }//FUNCION
    
    private void set_info_sujeto(Sujeto sujeto, Element rootNode) {
        rootNode.getChild("certificado").getChild("sujeto").getChild("cn").setText(sujeto.getCn());
        rootNode.getChild("certificado").getChild("sujeto").getChild("ou").setText(sujeto.getOu());
        rootNode.getChild("certificado").getChild("sujeto").getChild("o").setText(sujeto.getO());
        rootNode.getChild("certificado").getChild("sujeto").getChild("c").setText(sujeto.getC());
        rootNode.getChild("certificado").getChild("sujeto").getChild("serialnumber").setText(sujeto.getSerial_number());
    }//FUNCION
    
    //PARA USAR ESTA FUNCION HAY QUE INICIALIZAR LA VARIABLE GLOBAL file_path EN EL CONSTRUCTOR DEL OBJETO
    public boolean set_info_certificado(Emisor emisor, Sujeto sujeto, byte[] firma) {
        try {
            if (this.file_path != null) {
                this.xmlFile = new File(this.file_path);
                this.builder = new SAXBuilder();
                this.document = (Document) builder.build( xmlFile );
                Element rootNode = document.getRootElement();
                this.set_info_emisor(emisor, rootNode);
                this.set_info_sujeto(sujeto, rootNode);
                rootNode.getChild("firmaDigital").getChild("firma").setText(Arrays.toString(firma));
                document.removeContent();
                document.setRootElement(rootNode);
                XMLOutputter xmlOutput = new XMLOutputter();
                xmlOutput.setFormat(Format.getPrettyFormat());
                xmlOutput.output(document, System.out);
                xmlOutput.output(document, new FileOutputStream(xmlFile));
                return true;
            } else {
                this.ERROR_DRIVER_XML = "No se ha indicado una ruta de acceso al documento xml.\n Use >> setFile_path(String file_path)";
                return false;
            }
        } catch (IOException | JDOMException e) {
            this.ERROR_DRIVER_XML = e.getMessage();
            return false;
        }
    }//FUNCION
    
    public byte[] get_bytes_str(String str) {
        try {
            return str.getBytes();
        } catch (Exception e) {
            this.ERROR_DRIVER_XML = e.getMessage();
            return null;
        }
    }//FUNCION
    
}//CLASE
