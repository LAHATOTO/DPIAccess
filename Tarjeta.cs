using System;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;

namespace DPIAccessClass
    {
        public class Tarjeta
        {
            //public Desconectar();
            [DllImport("DPIAccess.dll")]
            public static extern int Disconnect();
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int Connect(string reader);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int GetReaderNames([In, Out] ref int count, [In, Out] char[] lpOutBuffer, [In, Out] ref int size);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int GetMRZ([In, Out] char[] mrz, [In, Out] ref int size);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int DllEntryPoint([In, Out] char[] puntoEntrada, [In, Out] ref int size);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int ReadEditableData([In, Out] char[] datosEditables, [In, Out] ref int size);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int ReadFixedData([In, Out] char[] datosEditables, [In, Out] ref int size);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
            internal static extern int GetCardSerialNumber([In, Out] byte[] numeroSerie, [In, Out] ref int size);
            [DllImport("DPIAccess.dll")]
            internal static extern int GetFingersInfo([In, Out] char[] numeroSerie);
            [DllImport("DPIAccess.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            internal static extern int GetFacialImage(byte[] imgData, ref long imgSize, ref long imgType);
            private enum IMAGE_FORMAT
            {
                JPEG,
                JPEG2000,
            }
        }
        public class lectura
        {
            public Object lecturaTarjeta()
            {
                TarjetaEntity tarjetaEntity = new TarjetaEntity();
                try
                {
                    int size = 0;
                    int count = 0;
                    Tarjeta.GetReaderNames(ref count, null, ref size);
                    char[] nombreTarjetas = new char[size];
                    Tarjeta.GetReaderNames(ref count, nombreTarjetas, ref size);
                    string[] nombresTarjeta = new string(nombreTarjetas).Split(new char[1]);
                    foreach (string pba in nombresTarjeta)
                    {
                        if (Tarjeta.Connect(pba) == 0)
                        {
                            Tarjeta.Connect(pba);
                            break;
                        }
                    }
                    Tarjeta.ReadEditableData(null, ref size);
                    char[] datosEditables = new char[size];
                    Tarjeta.ReadEditableData(datosEditables, ref size);
                    Tarjeta.ReadFixedData(null, ref size);
                    char[] datosNoEditables = new char[size];
                    Tarjeta.ReadFixedData(datosNoEditables, ref size);
                    Tarjeta.GetMRZ(null, ref size);
                    char[] mrz = new char[size];
                    Tarjeta.GetMRZ(mrz, ref size);
                    Tarjeta.GetCardSerialNumber(null, ref size);
                    byte[] serie = new byte[size];
                    Tarjeta.GetCardSerialNumber(serie, ref size);
                    byte[] numArray = new byte[65536];
                    long imgSize = 65536L;
                    long imgType = 0L;
                    Tarjeta.GetFacialImage(numArray, ref imgSize, ref imgType);
                    var imagen = new MemoryStream(numArray);
                    imagen.Position = 0;
                    string Neditables = new string(datosNoEditables);
                    string Editables = new string(datosEditables);
                    Tarjeta.Disconnect();

                    tarjetaEntity.cui = Neditables.Substring(0, 12);
                    tarjetaEntity.direccion = Editables.Substring(0, 80);
                    tarjetaEntity.direccion_municipio = Editables.Substring(80, 30);
                    tarjetaEntity.direccion_departamento = Editables.Substring(110, 45);
                    tarjetaEntity.emsion = Editables.Substring(110, 45);
                    tarjetaEntity.vencimiento = Neditables.Substring(257, 10);
                    tarjetaEntity.primer_nombre = Neditables.Substring(13, 25);
                    tarjetaEntity.segundo_nombre = Neditables.Substring(38, 25);
                    tarjetaEntity.primer_apellido = Neditables.Substring(63, 25);
                    tarjetaEntity.segundo_apellido = Neditables.Substring(88, 25);
                    tarjetaEntity.apellido_casada = Neditables.Substring(113, 25);
                    tarjetaEntity.mrz = new string(mrz);
                    tarjetaEntity.genero = Neditables.Substring(138, 9);
                    tarjetaEntity.estado_civil = Neditables.Substring(267, 7);
                    tarjetaEntity.nacionalidad = Editables.Substring(155, 30);
                    tarjetaEntity.profesion = Editables.Substring(185, 40);
                    tarjetaEntity.no_cedula = Editables.Substring(225, 15);
                    tarjetaEntity.municipio_cedula = Editables.Substring(240, 30);
                    tarjetaEntity.departamento_cedula = Editables.Substring(270, 30);
                    tarjetaEntity.vecindad_municipio = Neditables.Substring(274, 30);
                    tarjetaEntity.vecindad_departamento = Neditables.Substring(304, 30);
                    tarjetaEntity.nacimiento_fecha = Neditables.Substring(237, 10);
                    tarjetaEntity.nacimiento_municipio = Neditables.Substring(147, 30);
                    tarjetaEntity.nacimiento_departamento = Neditables.Substring(177, 30);
                    tarjetaEntity.nacimiento_pais = Neditables.Substring(207, 30);
                    tarjetaEntity.libro = Neditables.Substring(396, 5);
                    tarjetaEntity.folio = Neditables.Substring(401, 5);
                    tarjetaEntity.partida = Neditables.Substring(406, 5);
                    tarjetaEntity.sleer = Neditables.Substring(364, 1);
                    tarjetaEntity.sescribir = Neditables.Substring(365, 1);
                    tarjetaEntity.oficial_activo = Editables.Substring(300, 1);
                    tarjetaEntity.imagen = numArray;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return tarjetaEntity;
            }
        }
    }

