using System;
using System.Collections.Generic;

namespace AuxiliarAbarrotes.AFIP
{
    public class FacturaElectronica : AFIP
    {
        public string CAE { get; set; }
        public string FechaCbte { get; set; }
        public int PtoVenta { get; set; }
        public int CbteTipo { get; set; }
        public int NroCbte { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public string ErrorMessage { get; set; }

        public String Dummy()
        {
            //ObtenerWSAA();
            
            wsfe.ServiceSoap cliente = new wsfe.ServiceSoapClient();

            wsfe.FEDummyRequest request = new wsfe.FEDummyRequest();

            request.Body = new wsfe.FEDummyRequestBody();

            wsfe.FEDummyResponse response = cliente.FEDummy(request);

            return response.Body.FEDummyResult.AppServer;
        }
        protected wsfe.FEAuthRequest getAuthRequest()
        {
            wsfe.FEAuthRequest authRequest = new wsfe.FEAuthRequest();

            ObtenerWSAA();

            authRequest.Token = getSafetyNodeValue("token", _docWSAA.DocumentElement);
            authRequest.Sign = getSafetyNodeValue("sign", _docWSAA.DocumentElement);
            authRequest.Cuit = this._cuit;

            return authRequest;
        }

        public String GetTiposMonedas()
        {
            wsfe.ServiceSoap cliente = new wsfe.ServiceSoapClient();

            wsfe.FEParamGetTiposMonedasRequest request = new wsfe.FEParamGetTiposMonedasRequest();

            request.Body = new wsfe.FEParamGetTiposMonedasRequestBody();

            request.Body.Auth = getAuthRequest();
            
            wsfe.FEParamGetTiposMonedasResponse response = cliente.FEParamGetTiposMonedas(request);
            
            if( this.HasError(response.Body.FEParamGetTiposMonedasResult.Errors)) { return ""; }

            wsfe.Moneda[] monedas = response.Body.FEParamGetTiposMonedasResult.ResultGet;

            String sMonedas = "";

            foreach(wsfe.Moneda moneda in monedas)
            {
                sMonedas += moneda.Desc + "\n";
            }

            return sMonedas;
        }

        public int[] GetPuntosVentas()
        {
            List<int> listaPtoVtas = new List<int>();

            wsfe.ServiceSoap cliente = new wsfe.ServiceSoapClient();

            wsfe.FEParamGetPtosVentaRequest request = new wsfe.FEParamGetPtosVentaRequest();

            request.Body = new wsfe.FEParamGetPtosVentaRequestBody();

            request.Body.Auth = getAuthRequest();

            wsfe.FEParamGetPtosVentaResponse response = cliente.FEParamGetPtosVenta(request);

            //if (this.HasError(response.Body.FEParamGetPtosVentaResult.Errors)) { throw new Exception(); }

            wsfe.PtoVenta[] ptoVentas = response.Body.FEParamGetPtosVentaResult.ResultGet;

            foreach (var ptoVenta in ptoVentas)
            {
                listaPtoVtas.Add(ptoVenta.Nro);
            }

            return listaPtoVtas.ToArray();
        }
        public int GetUltimoCbte(int ptoVta, int cbteTipo)
        {
            wsfe.ServiceSoap cliente = new wsfe.ServiceSoapClient();

            wsfe.FECompUltimoAutorizadoRequest request = new wsfe.FECompUltimoAutorizadoRequest();

            request.Body = new wsfe.FECompUltimoAutorizadoRequestBody();

            request.Body.Auth = getAuthRequest();

            request.Body.PtoVta = ptoVta;
            request.Body.CbteTipo = cbteTipo;

            wsfe.FECompUltimoAutorizadoResponse response = cliente.FECompUltimoAutorizado(request);

            if (this.HasError(response.Body.FECompUltimoAutorizadoResult.Errors)) { return -1; }

            var ultimoCbte = response.Body.FECompUltimoAutorizadoResult.CbteNro;

            return ultimoCbte;
        }
        public bool Facturar()
        {
            wsfe.ServiceSoap cliente = new wsfe.ServiceSoapClient();

            wsfe.FECAESolicitarRequest request = new wsfe.FECAESolicitarRequest();

            request.Body = new wsfe.FECAESolicitarRequestBody();

            request.Body.Auth = getAuthRequest();

            request.Body.FeCAEReq = new wsfe.FECAERequest();
            request.Body.FeCAEReq.FeCabReq = new wsfe.FECAECabRequest();

            request.Body.FeCAEReq.FeCabReq.PtoVta = this.PtoVenta;
            request.Body.FeCAEReq.FeCabReq.CantReg = 1;
            request.Body.FeCAEReq.FeCabReq.CbteTipo = this.CbteTipo;

            wsfe.FECAEDetRequest req= new wsfe.FECAEDetRequest();

            double impNeto = Math.Round( this.Monto / 1.21, 2);
            double impIVA = Math.Round( this.Monto - impNeto, 2);

            req.Concepto = 1;   // Producto: 1, Servicio: 2, Producto/Servicio: 3
            req.DocTipo = 99;  //Consumidor final
            req.DocNro = 0;
            req.CbteDesde = this.NroCbte;
            req.CbteHasta = this.NroCbte;
            req.CbteFch = this.Fecha.ToString("yyyyMMdd");
            req.ImpTotal = this.Monto;
            req.ImpTotConc = 0;
            req.ImpNeto = impNeto;
            req.ImpOpEx = 0;
            req.ImpIVA = impIVA;
            req.ImpTrib = 0;
            req.MonId = "PES";
            req.MonCotiz = 1;
            req.Iva = new wsfe.AlicIva[1];
            req.Iva[0] = new wsfe.AlicIva();
            req.Iva[0].Importe = impIVA;
            req.Iva[0].BaseImp = impNeto;
            req.Iva[0].Id = 5;

            request.Body.FeCAEReq.FeDetReq = new wsfe.FECAEDetRequest[1];

            request.Body.FeCAEReq.FeDetReq[0] = req;
            
            wsfe.FECAESolicitarResponse response = cliente.FECAESolicitar(request);

            if (this.HasError(response.Body.FECAESolicitarResult.Errors)) {
                foreach( var error in response.Body.FECAESolicitarResult.Errors)
                {
                    this.ErrorMessage = error.Msg;
                    //MessageBox.Show(error.Code.ToString() + " - " + error.Msg);
                }
                return false;
            }

            if(response.Body.FECAESolicitarResult.FeDetResp[0].CAE.Length <= 0)
            {
                foreach (var obs in response.Body.FECAESolicitarResult.FeDetResp[0].Observaciones)
                {
                    this.ErrorMessage = obs.Msg;
                    //MessageBox.Show(obs.Code.ToString() + " - " + obs.Msg);
                }
                return false;
            }
            this.CAE = response.Body.FECAESolicitarResult.FeDetResp[0].CAE;
            this.FechaCbte = response.Body.FECAESolicitarResult.FeDetResp[0].CbteFch;
            

            //this.CAE = "72531072235038";
            //this.FechaCbte = "10/01/2023";
            return true;
        }
    }
}
