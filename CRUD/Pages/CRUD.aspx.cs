using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD.Pages
{
    public partial class CRUD : Page
    {
        // Se establece la conexión a la base de datos utilizando la cadena de conexión definida en el archivo de configuración.
        readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        // Variables estáticas para almacenar el ID y la operación (crear, leer, actualizar, eliminar)
        public static string sID = "-1"; // Inicializa el ID con un valor por defecto
        public static string sOpc = ""; // Inicializa la operación con una cadena vacía

        // Método que se ejecuta al cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Si no es una solicitud de envío de formulario (postback), se verifica si se proporciona un ID en la URL.
                if (Request.QueryString["id"] != null)
                {
                    sID = Request.QueryString["id"].ToString(); // Obtiene el ID de la URL
                    CargarDatos(); // Carga los datos del registro con el ID proporcionado
                    tbdate.TextMode = TextBoxMode.DateTime; // Configura el modo de entrada de fecha en el TextBox
                }

                // Si se proporciona una operación (crear, leer, actualizar, eliminar) en la URL.
                if (Request.QueryString["op"] != null)
                {
                    sOpc = Request.QueryString["op"].ToString(); // Obtiene la operación de la URL

                    // Configura el título de la página y la visibilidad de los botones según la operación.
                    switch (sOpc)
                    {
                        case "C":
                            this.lbltitulo.Text = "Ingresar Usuario"; // Configura el título de la página
                            this.BtnCreate.Visible = true; // Hace visible el botón de crear
                            break;
                        case "R":
                            this.lbltitulo.Text = "Leer Datos Usuario"; // Configura el título de la página
                            break;
                        case "U":
                            this.lbltitulo.Text = "Modificar Info."; // Configura el título de la página
                            this.BtnUpdate.Visible = true; // Hace visible el botón de actualizar
                            break;
                        case "D":
                            this.lbltitulo.Text = "Eliminar usuario"; // Configura el título de la página
                            this.BtnDelete.Visible = true; // Hace visible el botón de eliminar
                            break;
                    }
                }
            }
        }

        // Método para cargar los datos del registro según el ID proporcionado
        void CargarDatos()
        {
            con.Open(); // Abre la conexión a la base de datos
            SqlDataAdapter da = new SqlDataAdapter("sp_read", con); // Crea un adaptador de datos con el procedimiento almacenado "sp_read"
            da.SelectCommand.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = sID; // Añade el parámetro ID al comando
            DataSet ds = new DataSet(); // Crea un conjunto de datos
            ds.Clear(); // Limpia el conjunto de datos
            da.Fill(ds); // Llena el conjunto de datos con los resultados de la consulta
            DataTable dt = ds.Tables[0]; // Obtiene la primera tabla del conjunto de datos
            DataRow row = dt.Rows[0]; // Obtiene la primera fila de la tabla
            tbnombre.Text = row[1].ToString(); // Asigna el valor del nombre al TextBox
            tbrut.Text = row[2].ToString(); // Asigna el valor del rut al TextBox
            tbemail.Text = row[3].ToString(); // Asigna el valor del correo electrónico al TextBox
            DateTime d = (DateTime)row[4]; // Obtiene la fecha de la fila y la convierte a DateTime
            tbdate.Text = d.ToString("dd/MM/yyyy"); // Asigna el valor de la fecha al TextBox en formato "dd/MM/yyyy"
            con.Close(); // Cierra la conexión a la base de datos
        }

        // Método para crear un nuevo registro
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            string rut = tbrut.Text;

            // Verifica si el guion está presente en el campo de RUT
            if (rut.Contains("-"))
            {
                // Si el guion ya está presente, no hace nada
            }
            else
            {
                // Si el guion no está presente, lo agrega al final del RUT
                rut = rut.Insert(rut.Length - 1, "-");
                // Actualiza el valor del campo de RUT con el guion insertado
                tbrut.Text = rut;
            }

            SqlCommand cmd = new SqlCommand("sp_create", con); // Crea un nuevo comando con el procedimiento almacenado "sp_create"
            con.Open(); // Abre la conexión a la base de datos
            cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = tbnombre.Text; // Añade parámetros al comando
            cmd.Parameters.Add("@Rut", SqlDbType.VarChar).Value = tbrut.Text;
            cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = tbemail.Text;
            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = tbdate.Text;
            cmd.ExecuteNonQuery(); // Ejecuta el comando
            con.Close(); // Cierra la conexión a la base de datos
            Response.Redirect("Index.aspx"); // Redirige a la página de índice
        }

        // Método para actualizar un registro existente
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_update", con); // Crea un nuevo comando con el procedimiento almacenado "sp_update"
            con.Open(); // Abre la conexión a la base de datos
            cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = sID; // Añade parámetros al comando
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = tbnombre.Text;
            cmd.Parameters.Add("@Rut", SqlDbType.VarChar).Value = tbrut.Text;
            cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = tbemail.Text;
            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = tbdate.Text;
            cmd.ExecuteNonQuery(); // Ejecuta el comando
            con.Close(); // Cierra la conexión a la base de datos
            Response.Redirect("Index.aspx"); // Redirige a la página de índice
        }

        // Método para eliminar un registro
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_delete", con); // Crea un nuevo comando con el procedimiento almacenado "sp_delete"
            con.Open(); // Abre la conexión a la base de datos
            cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = sID; // Añade parámetros al comando
            cmd.ExecuteNonQuery(); // Ejecuta el comando
            con.Close(); // Cierra la conexión a la base de datos
            Response.Redirect("Index.aspx"); // Redirige a la página de índice
        }

        // Método para volver a la página de índice
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx"); // Redirige a la página de índice
        }
    }
}
