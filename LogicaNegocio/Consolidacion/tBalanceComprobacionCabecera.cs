using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class tBalanceComprobacionCabecera
    {
        #region variables
        private string lstr_ledger;
        private string lstr_vista;
        private string lstr_version;
        private string lstr_ejercicio;
        private string lstr_periodo;
        private string lstr_unid_consol;
        private string lstr_plan_pos;
        #endregion

        #region obtencion y asignacion

        public string Lstr_ledger
        {
            get { return lstr_ledger; }
            set { lstr_ledger = value; }
        }

        public string Lstr_vista
        {
            get { return lstr_vista; }
            set { lstr_vista = value; }
        }
        public string Lstr_version
        {
            get { return lstr_version; }
            set { lstr_version = value; }
        }

        public string Lstr_ejercicio
        {
            get { return lstr_ejercicio; }
            set { lstr_ejercicio = value; }
        }

        public string Lstr_periodo
        {
            get { return lstr_periodo; }
            set { lstr_periodo = value; }
        }

        public string Lstr_unid_consol
        {
            get { return lstr_unid_consol; }
            set { lstr_unid_consol = value; }
        }

        public string Lstr_plan_pos
        {
            get { return lstr_plan_pos; }
            set { lstr_plan_pos = value; }
        }

        #endregion

        #region procedimientos

        private tBalanceComprobacionCabecera() { }

        public tBalanceComprobacionCabecera(string str_ledger, string str_vista, string str_version, string str_ejercicio, string str_periodo, string str_unid_consol, string str_plan_pos)
        {
            lstr_ledger = str_ledger;
            lstr_vista = str_vista;
            lstr_version = str_version;
            lstr_ejercicio = str_ejercicio;
            lstr_periodo = str_periodo;
            lstr_unid_consol = str_unid_consol;
            lstr_plan_pos = str_plan_pos;        
        }
        #endregion

    }
}