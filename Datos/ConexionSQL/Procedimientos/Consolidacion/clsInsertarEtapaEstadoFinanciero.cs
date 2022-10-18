﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsInsertarEtapaEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private int lint_IdEtapaEstadoFinanciero;

        private string lstr_NotaRazon;
        private string lstr_UsuarioEtapaEstado;
        private DateTime ldt_FechaDeEtapaEstado;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }
        
        public int Lint_Periodo
        {
            get { return lint_Periodo; }
            set { lint_Periodo = value; }
        }

        public string Lstr_UnidadTiempoPeriodo
        {
            get { return lstr_UnidadTiempoPeriodo; }
            set { lstr_UnidadTiempoPeriodo = value; }
        }

        public int Lint_IdEtapaEstadoFinanciero
        {
            get { return lint_IdEtapaEstadoFinanciero; }
            set { lint_IdEtapaEstadoFinanciero = value; }
        }

        public string Lstr_NotaRazon
        {
            get { return lstr_NotaRazon; }
            set { lstr_NotaRazon = value; }
        }
        public string Lstr_UsuarioEtapaEstado
        {
            get { return lstr_UsuarioEtapaEstado; }
            set { lstr_UsuarioEtapaEstado = value; }
        }

        public DateTime Ldt_FechaDeEtapaEstado
        {
            get { return ldt_FechaDeEtapaEstado; }
            set { ldt_FechaDeEtapaEstado = value; }
        }
       
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        #endregion

        #region procedimientos
        public clsInsertarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapaEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEntidad = str_IdEntidad;
            lint_Periodo = int_Periodo;
            lstr_UnidadTiempoPeriodo = str_UnidadTiempoPeriodo;
            lint_IdEtapaEstadoFinanciero = int_IdEtapaEstadoFinanciero;

            str_NotaRazon = lstr_NotaRazon;
            lstr_UsuarioEtapaEstado = str_UsuarioEtapaEstado;
            ldt_FechaDeEtapaEstado = dt_FechaDeEtapaEstado;

            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\InsertarEtapaEstadoFinanciero.config", this);
        }
        #endregion

    }
}