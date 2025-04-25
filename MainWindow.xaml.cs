using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SEMANA05_EXAM
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            using SqlConnection connection = new SqlConnection("Data Source=DESKTOP-K5KLUQV\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True;TrustServerCertificate=True");

            List<Producto> productos = new List<Producto>();

            connection.Open();

            SqlCommand command = new SqlCommand("ListarProductos", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    IdProducto = Convert.ToInt32(reader["idproducto"]),
                    NombreProducto = reader["nombreProducto"]?.ToString(),
                    IdProveedor = reader["idProveedor"] == DBNull.Value ? null : Convert.ToInt32(reader["idProveedor"]),
                    IdCategoria = reader["idCategoria"] == DBNull.Value ? null : Convert.ToInt32(reader["idCategoria"]),
                    CantidadPorUnidad = reader["cantidadPorUnidad"]?.ToString(),
                    PrecioUnidad = reader["precioUnidad"] == DBNull.Value ? null : Convert.ToDecimal(reader["precioUnidad"]),
                    UnidadesEnExistencia = reader["unidadesEnExistencia"] == DBNull.Value ? null : Convert.ToInt16(reader["unidadesEnExistencia"]),
                    UnidadesEnPedido = reader["unidadesEnPedido"] == DBNull.Value ? null : Convert.ToInt16(reader["unidadesEnPedido"]),
                    NivelNuevoPedido = reader["nivelNuevoPedido"] == DBNull.Value ? null : Convert.ToInt16(reader["nivelNuevoPedido"]),
                    Suspendido = reader["suspendido"] == DBNull.Value ? null : Convert.ToInt16(reader["suspendido"]),
                    CategoriaProducto = reader["categoriaProducto"]?.ToString()
                });
            }

            reader.Close();
            connection.Close();

            dgProductos.ItemsSource = productos;
        }

        private void btn1_Click2(object sender, RoutedEventArgs e)
        {
            using SqlConnection connection = new SqlConnection("Data Source=DESKTOP-K5KLUQV\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True;TrustServerCertificate=True");
            List<Proveedor> proveedores = new List<Proveedor>();

            try
            {
                SqlCommand command = new SqlCommand("ListarProveedores", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    proveedores.Add(new Proveedor
                    {
                        IdProveedor = Convert.ToInt32(reader["idProveedor"]),
                        NombreCompañia = reader["nombreCompañia"]?.ToString(),
                        NombreContacto = reader["nombrecontacto"]?.ToString(),
                        CargoContacto = reader["cargocontacto"]?.ToString(),
                        Direccion = reader["direccion"]?.ToString(),
                        Ciudad = reader["ciudad"]?.ToString(),
                       // Region = reader["region"]?.ToString(),
                        CodPostal = reader["codPostal"]?.ToString(),
                        Pais = reader["pais"]?.ToString(),
                        Telefono = reader["telefono"]?.ToString(),
                        Fax = reader["fax"]?.ToString(),
                        PaginaPrincipal = reader["paginaprincipal"]?.ToString()
                    });
                }

                reader.Close();
                dgProductos.ItemsSource = proveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void btn1_Click3(object sender, RoutedEventArgs e)
        {
            using SqlConnection connection = new SqlConnection("Data Source=DESKTOP-K5KLUQV\\SQLEXPRESS2017;Initial Catalog=Neptuno;Integrated Security=True;TrustServerCertificate=True");

            List<Categorias> categorias = new List<Categorias>();

            try
            {
                SqlCommand command = new SqlCommand("ListarCaategorias", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categorias.Add(new Categorias
                    {
                        IdCategoria = Convert.ToInt32(reader["idcategoria"]),
                        NombreCategoria = reader["nombrecategoria"]?.ToString(),
                        Descripcion = reader["descripcion"]?.ToString(),
                        Activo = reader["Activo"]?.ToString(),// == DBNull.Value ? null : Convert.ToBoolean(reader["Activo"]),
                        CodCategoria = reader["CodCategoria"]?.ToString()
                    });
                }

                reader.Close();
                dgProductos.ItemsSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar categorías: " + ex.Message);
            }
        }
    }
}