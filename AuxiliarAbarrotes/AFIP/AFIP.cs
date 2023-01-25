using System;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace AuxiliarAbarrotes.AFIP
{
    public class AFIP
    {
        protected String _pathWSAA = @".\wsa.xml";
        protected int _uniqueId;
        protected XmlDocument _docWSAA;

        protected String _claveCertificado;
        protected String _pathCertificado;

        public String ClaveCertificado
        {
            get { return this._claveCertificado; }
            set { this._claveCertificado = value; }
        }
        public String PathCertificado
        {
            get { return this._pathCertificado; }
            set { this._pathCertificado = value; }
        }
        protected long _cuit;
        public long Cuit
        {
            get { return this._cuit; }
            set { this._cuit = value; }
        }


        protected String getSafetyNodeValue(string nodeName, XmlElement rootElement)
        {
            if (this._docWSAA == null) return "";

            if (this._docWSAA.DocumentElement == null) return "";

            XmlNodeList lista = _docWSAA.DocumentElement.GetElementsByTagName(nodeName);

            if (lista.Count <= 0) { return ""; }

            return lista.Item(0).InnerText;
        }
        public byte[] firmarXML(byte[] xml, X509Certificate2 certificado)
        {
            ContentInfo contentInfo = new ContentInfo(xml);
            SignedCms signedCms = new SignedCms(contentInfo);

            CmsSigner cmsSigner = new CmsSigner(certificado);

            cmsSigner.IncludeOption = X509IncludeOption.EndCertOnly;

            signedCms.ComputeSignature(cmsSigner);

            return signedCms.Encode();
        }

        public byte[] getXmlLoginTicketRequest(int uniqueId, String service)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement loginTicketNode = xmlDoc.CreateElement("loginTicketRequest");

            xmlDoc.AppendChild(loginTicketNode);

            XmlNode headerNode = loginTicketNode.AppendChild(xmlDoc.CreateElement("header"));

            XmlNode uniqueIdNode = headerNode.AppendChild(xmlDoc.CreateElement("uniqueId"));

            uniqueIdNode.InnerText = uniqueId.ToString();

            XmlNode generationTimeNode = headerNode.AppendChild(xmlDoc.CreateElement("generationTime"));

            XmlNode expirationTimeNode = headerNode.AppendChild(xmlDoc.CreateElement("expirationTime"));

            XmlNode serviceNode = loginTicketNode.AppendChild(xmlDoc.CreateElement("service"));

            serviceNode.InnerText = service;

            generationTimeNode.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
            expirationTimeNode.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");

            return Encoding.UTF8.GetBytes(xmlDoc.OuterXml);
        }

        public X509Certificate2 getCertificado(String filename, String password)
        {
            SecureString claveCert = new SecureString();
            X509Certificate2 certificate2 = new X509Certificate2();

            foreach (char c in password)
            {
                claveCert.AppendChar(c);
            }

            certificate2.Import(System.IO.File.ReadAllBytes(filename), claveCert, X509KeyStorageFlags.PersistKeySet);

            return certificate2;
        }

        public bool ObtenerWSAA()
        {
            DateTime dateExpiration = new DateTime();

            this._docWSAA = new XmlDocument();

            if (System.IO.File.Exists(this._pathWSAA))
            {
                this._docWSAA.Load(this._pathWSAA);

                if (DateTime.TryParse(getSafetyNodeValue("expirationTime", this._docWSAA.DocumentElement),
                                            CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None,
                                            out dateExpiration))
                {
                    if (dateExpiration.CompareTo(DateTime.Now) > 0)
                    {
                        return true;
                    }
                }
            }

            byte[] bLoginTicket = getXmlLoginTicketRequest(_uniqueId++, "wsfe");
            //byte[] bLoginTicket = getXmlLoginTicketRequest(_uniqueId++, "ws_sr_padron_a5");

            X509Certificate2 certificado = getCertificado(this._pathCertificado, this._claveCertificado);

            byte[] xmlEncode = firmarXML(bLoginTicket, certificado);

            String sEncode = Convert.ToBase64String(xmlEncode);

            wsaa.LoginCMS loginCMS = new wsaa.LoginCMSClient();

            wsaa.loginCmsRequest request = new wsaa.loginCmsRequest();

            request.in0 = sEncode;

            wsaa.loginCmsResponse response = loginCMS.loginCms(request);

            System.IO.File.WriteAllText(this._pathWSAA, response.loginCmsReturn);

            this._docWSAA.LoadXml(response.loginCmsReturn);

            return true;
        }
        protected bool HasError(wsfe.Err[] errors)
        {

            if (errors == null) return false;

            if (errors.Length > 0) return true;

            return false;
        }
    }
}
