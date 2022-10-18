@echo off
echo ********************
echo *   Firmando JAR   *
echo ********************

echo Firmando pdfSignerGSI.jar .............................................
"C:\Program Files (x86)\Java\jdk1.7.0_40\bin\jarsigner" -keystore firmador -keypass aaa123 -storepass aaa123 pdfSignerGSI.jar firmador



echo ********************************
echo *        Fin de Proceso        *
echo ********************************

pause
