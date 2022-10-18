package SMART_CARD;

/*
OBJETO QUE MANEJA LA INFORMACION DE LA PERSONA LIGADA AL CERTIFIDO DE FIRMA DIGITAL
*/


public class Emisor {
    private String cn, ou, o, c, serial_number;

    public Emisor(String cn, String ou, String o, String c, String serial_number) {
        this.cn = cn;
        this.ou = ou;
        this.o = o;
        this.c = c;
        this.serial_number = serial_number;
    }//CONSTRUCTOR

    public String getCn() {
        return cn;
    }//GET

    public void setCn(String cn) {
        this.cn = cn;
    }//SET

    public String getOu() {
        return ou;
    }//GET

    public void setOu(String ou) {
        this.ou = ou;
    }//SET

    public String getO() {
        return o;
    }//GET

    public void setO(String o) {
        this.o = o;
    }//SET

    public String getC() {
        return c;
    }//GET

    public void setC(String c) {
        this.c = c;
    }//SET

    public String getSerial_number() {
        return serial_number;
    }//GET

    public void setSerial_number(String serial_number) {
        this.serial_number = serial_number;
    }//SET
    
    
    
}//CLASE
