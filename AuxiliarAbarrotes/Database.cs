using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using FirebirdSql.Data.FirebirdClient;

using AuxiliarAbarrotes.Clases;
using System.Data.Common;

namespace AuxiliarAbarrotes
{
    public class Database : IDatabase
    {
        private FbConnection _db;
        private FbConnection _connection;
        public Database(string dbpath, string dbname)
        {
            string fullname = dbpath + "\\" + dbname;

            this._db = new FbConnection();

            this._db.ConnectionString = @"ServerType=1;User=sysdba;Password=masterkey;Database=" + fullname;

            this._db.Open();
            
        }
        public void AbrirBaseSistema()
        {
            if( this._connection == null )
            {
                Configuracion config = this.LeerConfiguracion();

                this._connection = new FbConnection(@"ServerType=1;User=sysdba;Password=masterkey;Database=" + config.pathDB);

                this._connection.Open();
            }
        }

        public IList<Interfaces.ICategoria> getCategorias()
        {
            List<Interfaces.ICategoria> categorias = new List<Interfaces.ICategoria>();

            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "SELECT ID, NOMBRE FROM DEPARTAMENTOS";
            cmd.CommandType = System.Data.CommandType.Text;

            FbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                categorias.Add(new Clases.Categoria { Id = r.GetInt32(0), Name = r.GetString(1) });
            }

            r.Close();

            return categorias;
        }
        public IList<Interfaces.ITicket> GetTickets(bool ultimos50)
        {
            List<Interfaces.ITicket> tickets = new List<Interfaces.ITicket>();

            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "SELECT FIRST 50 v.ID, v.NOMBRE, v.CREADO_EN, v.SUBTOTAL, " + 
                                "v.TOTAL, v.GANANCIA, v.VENDIDO_EN, COUNT(va.ID) " +
                                "FROM VENTATICKETS v INNER JOIN VENTATICKETS_ARTICULOS va " +
                                "ON v.ID=va.TICKET_ID GROUP BY v.ID, v.NOMBRE, v.CREADO_EN, " +
                                "v.SUBTOTAL, v.TOTAL, v.GANANCIA, v.VENDIDO_EN ORDER BY v.CREADO_EN DESC";

            FbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                tickets.Add(new Ticket
                {
                    Id = r.GetInt32(0),
                    Nombre = r.GetString(1),
                    Fecha = r.GetDateTime(2),
                    Subtotal = r.GetDouble(3),
                    Total = r.GetDouble(4),
                    Ganancia = r.GetDouble(5),
                    Cantidad = r.GetInt32(7)
                });
            }

            return tickets;
        }
        public IList<Interfaces.IProducto> getProductos(int categoria_id, string filtro)
        {
            List<Interfaces.IProducto> productos = new List<Interfaces.IProducto>();

            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "SELECT p.ID, p.CODIGO, p.DESCRIPCION, p.TVENTA, p.PCOSTO, " +
                                                "p.PVENTA, p.DEPT, p.PFINAL, d.NOMBRE " +
                                                "FROM PRODUCTOS p " +
                                                "LEFT JOIN DEPARTAMENTOS d ON p.DEPT=d.ID WHERE 1=1 ";
            cmd.CommandType = System.Data.CommandType.Text;

            if( categoria_id >= 0 )
            {
                cmd.CommandText += " AND p.DEPT = " + categoria_id.ToString();
            }

            if( filtro.Trim().Length > 0 )
            {
                cmd.CommandText += " AND (p.CODIGO LIKE '%" + filtro + "%'";
                cmd.CommandText += " OR LOWER(p.DESCRIPCION) LIKE '%" + filtro.ToLower() + "%')";
            }

            FbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                productos.Add( new Clases.Producto {
                                                      Codigo = r.IsDBNull(1) ? "" : r.GetString(1),
                                                      Descripcion = r.IsDBNull(2) ? "" : r.GetString(2),
                                                      TVenta = r.IsDBNull(3) ? "" : r.GetString(3),
                                                      PCosto = r.IsDBNull(4) ? 0.0 : r.GetDouble(4),
                                                      PVenta = r.IsDBNull(5) ? 0.0 : r.GetDouble(5),
                                                      Dept = r.IsDBNull(6) ? 0 : r.GetInt32(6),
                                                      PFinal = r.IsDBNull(7) ? 0.0 : r.GetDouble(7),
                                                      Departamento = r.IsDBNull(8) ? "" : r.GetString(8)
                });
            }

            r.Close();

            return productos;
        }
        public IList<Interfaces.IArticuloTicket> GetArticuloTickets(int ticket_id)
        {
            List<Interfaces.IArticuloTicket> articuloTickets = new List<Interfaces.IArticuloTicket>();

            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "SELECT ID, PRODUCTO_CODIGO, PRODUCTO_NOMBRE, " +
                                "CANTIDAD, PRECIO_USADO FROM VENTATICKETS_ARTICULOS " +
                                "WHERE TICKET_ID=" + ticket_id.ToString();

            cmd.CommandType = System.Data.CommandType.Text;

            FbDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                articuloTickets.Add(new Clases.ArticuloTicket {
                    Id = dr.GetInt32(0),
                    Codigo = dr.GetString(1),
                    Nombre =dr.GetString(2),
                    Cantidad = dr.GetInt32(3),
                    Precio = dr.GetDouble(4)
                });
            }

            dr.Close();

            return articuloTickets;
        }
        public void UpdatePrecioVenta(string codigo, double monto)
        {
            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "UPDATE PRODUCTOS SET PVENTA=@pventa WHERE CODIGO=@codigo";

            cmd.Parameters.AddWithValue("pventa", monto);
            cmd.Parameters.AddWithValue("codigo", codigo);

            cmd.CommandType = System.Data.CommandType.Text;

            cmd.ExecuteNonQuery();
        }
        public void UpdatePrecioFinal(string codigo, double monto)
        {
            var cmd = this._connection.CreateCommand();

            cmd.CommandText = "UPDATE PRODUCTOS SET PFINAL=@pfinal WHERE codigo=@codigo";

            cmd.Parameters.AddWithValue("pfinal", monto);
            cmd.Parameters.AddWithValue("codigo", codigo);

            cmd.CommandType = System.Data.CommandType.Text;

            cmd.ExecuteNonQuery();
        }
        public Configuracion LeerConfiguracion()
        {
            Configuracion config = new Configuracion();
            var cmd = this._db.CreateCommand();

            cmd.CommandText = "SELECT pathdb, impresora, certificado, clave_cert, " +
                                "punto_vta, ultimo_cbte FROM CONFIGURACION";

            FbDataReader dr = cmd.ExecuteReader();

            if( dr.Read() )
            {
                config.pathDB = dr.GetString(0);
                config.impresora = dr.GetString(1);
                config.certificado = dr.GetString(2);
                config.clave_cert = dr.GetString(3);
                config.punto_vta = dr.GetInt32(4);
                config.ultimo_cbte = dr.GetInt32(5);
            }
            else
            {
                config.pathDB = "";
                config.impresora = "";
                config.certificado = "";
                config.clave_cert = "";
                config.punto_vta = 1;
                config.ultimo_cbte = 1;
            }

            return config;
        }
        public void GrabarConfiguracion(Configuracion configuracion)
        {
            var cmd = this._db.CreateCommand();

            cmd.CommandText = "SELECT count(1) FROM configuracion";
            cmd.CommandType = System.Data.CommandType.Text;

            var value = cmd.ExecuteScalar();

            if( value == null || (int)value == 0 )
            {
                cmd.CommandText = "INSERT INTO configuracion(id, pathdb, impresora, " +
                                    "certificado, clave_cert, punto_vta, ultimo_cbte)" +
                                    "VALUES(@id, @pathdb, @impresora, @certificado, @clave_cert, " +
                                    "@punto_vta, @ultimo_cbte)";

                cmd.Parameters.AddWithValue("@id", 1);
                cmd.Parameters.AddWithValue("@pathdb", configuracion.pathDB);
                cmd.Parameters.AddWithValue("@impresora", configuracion.impresora);
                cmd.Parameters.AddWithValue("@certificado", configuracion.certificado);
                cmd.Parameters.AddWithValue("@clave_cert", configuracion.clave_cert);
                cmd.Parameters.AddWithValue("@punto_vta", configuracion.punto_vta);
                cmd.Parameters.AddWithValue("@ultimo_cbte", configuracion.ultimo_cbte);

                cmd.ExecuteNonQuery();
            } else
            {
                cmd.CommandText = "UPDATE configuracion SET pathdb=@pathdb, impresora=@impresora, " +
                                    "certificado=@certificado, clave_cert=@clave_cert, " +
                                    "punto_vta=@punto_vta, ultimo_cbte=@ultimo_cbte WHERE id=@id";

                cmd.Parameters.AddWithValue("@pathdb", configuracion.pathDB);
                cmd.Parameters.AddWithValue("@impresora", configuracion.impresora);
                cmd.Parameters.AddWithValue("@certificado", configuracion.certificado);
                cmd.Parameters.AddWithValue("@clave_cert", configuracion.clave_cert);
                cmd.Parameters.AddWithValue("@punto_vta", configuracion.punto_vta);
                cmd.Parameters.AddWithValue("@ultimo_cbte", configuracion.ultimo_cbte);
                cmd.Parameters.AddWithValue("@id", 1);

                cmd.ExecuteNonQuery();
            }
        }
        
        public DatosFactura leerDatosFactura()
        {
            DatosFactura datosFactura = new DatosFactura();
            var cmd = this._db.CreateCommand();

            cmd.CommandText = "SELECT id, razon_social, cuit, iibb, domicilio_1, " +
                                "domicilio_2, condicion_iva FROM datos_facturas";

            FbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                datosFactura.Id = dr.GetInt32(0);
                datosFactura.RazonSocial = dr.GetString(1);
                datosFactura.CUIT = dr.GetString(2);
                datosFactura.IIBB = dr.GetString(3);
                datosFactura.Domicilio1 = dr.GetString(4);
                datosFactura.Domicilio2 = dr.GetString(5);
                datosFactura.CondicionIVA = dr.IsDBNull(6) ? "" : dr.GetString(6);
            }
            else
            {
                datosFactura.Id = -1;
                datosFactura.RazonSocial = "";
                datosFactura.CUIT = "";
                datosFactura.IIBB = "";
                datosFactura.Domicilio1 = "";
                datosFactura.Domicilio2 = "";
                datosFactura.CondicionIVA = "";
            }
            return datosFactura;
        }
        public void GrabarDatosFactura(DatosFactura datosFactura)
        {
            var cmd = this._db.CreateCommand();

            cmd.CommandText = "SELECT count(1) FROM datos_facturas";
            cmd.CommandType = System.Data.CommandType.Text;

            var value = cmd.ExecuteScalar();

            if (value == null || (int)value == 0)
            {
                cmd.CommandText = "INSERT INTO datos_facturas(id, razon_social, " +
                                    "cuit, iibb, domicilio_1, domicilio_2, condicion_iva)" +
                                    "VALUES(@id, @razon_social, @cuit, @iibb, @domicilio_1, " +
                                    "@domicilio_2, @condicion_iva)";

                cmd.Parameters.AddWithValue("@razon_social", datosFactura.RazonSocial);
                cmd.Parameters.AddWithValue("@cuit", datosFactura.CUIT);
                cmd.Parameters.AddWithValue("@iibb", datosFactura.IIBB);
                cmd.Parameters.AddWithValue("@domicilio_1", datosFactura.Domicilio1);
                cmd.Parameters.AddWithValue("@domicilio_2", datosFactura.Domicilio2);
                cmd.Parameters.AddWithValue("@condicion_iva", datosFactura.CondicionIVA);
                cmd.Parameters.AddWithValue("@id", 1);

                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText = "UPDATE datos_facturas SET razon_social=@razon_social, " +
                                    "cuit=@cuit, iibb=@iibb, domicilio_1=@domicilio_1, " +
                                    "domicilio_2=@domicilio_2, condicion_iva=@condicion_iva " +
                                    "WHERE id=@id";

                cmd.Parameters.AddWithValue("@razon_social", datosFactura.RazonSocial);
                cmd.Parameters.AddWithValue("@cuit", datosFactura.CUIT);
                cmd.Parameters.AddWithValue("@iibb", datosFactura.IIBB);
                cmd.Parameters.AddWithValue("@domicilio_1", datosFactura.Domicilio1);
                cmd.Parameters.AddWithValue("@domicilio_2", datosFactura.Domicilio2);
                cmd.Parameters.AddWithValue("@condicion_iva", datosFactura.CondicionIVA);
                cmd.Parameters.AddWithValue("@id", 1);

                cmd.ExecuteNonQuery();
            }
        }

        public void GrabarNroComprobante(int nroCbte)
        {
            var cmd = this._db.CreateCommand();

            cmd.CommandText = "UPDATE configuracion SET ultimo_cbte=@ultimo_cbte WHERE id=@id";
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.Parameters.AddWithValue("@ultimo_cbte", nroCbte);
            cmd.Parameters.AddWithValue("@id", 1);

            cmd.ExecuteNonQuery();
        }
    }
}
