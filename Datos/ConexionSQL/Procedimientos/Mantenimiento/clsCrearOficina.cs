﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearOficina : clsProcedimientoAlmacenado
    {
        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomOficina;
        public string Lstr_NomOficina
        {
            get { return lstr_NomOficina; }
            set { lstr_NomOficina = value; }
        }


        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_CorreoNotifica;
        public string Lstr_CorreoNotifica
        {
            get { return lstr_CorreoNotifica; }
            set { lstr_CorreoNotifica = value; }
        }

        private char lstr_UsaExpediente;
        public char Lstr_UsaExpediente
        {
            get { return lstr_UsaExpediente; }
            set { lstr_UsaExpediente = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearOficina(string str_IdOficina, string str_IdSociedadGL, string str_NomOficina, string str_IdDireccion, string str_Estado, string str_UsrCreacion, string str_CorreoNotifica = "", char str_UsaExpediente = 'N')
        {
            lstr_IdOficina = str_IdOficina;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_NomOficina = str_NomOficina;
            lstr_IdDireccion = str_IdDireccion;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_CorreoNotifica = str_CorreoNotifica;
            lstr_UsaExpediente = str_UsaExpediente;

            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearOficina.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}