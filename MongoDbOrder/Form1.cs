using MongoDbOrder.Entities;
using MongoDbOrder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoDbOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OrderOperation orderOperation = new OrderOperation();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var order = new Order
            {
                ClientName = txtClient.Text,
                TownName = txtTown.Text,
                CityName = txtCity.Text,
                TotalPrice = Convert.ToDecimal(txtTotalPrice.Text)
            };
            orderOperation.AddOrder(order);
            MessageBox.Show("Order added successfully");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Order> orders = orderOperation.GetAllOrders();
            dataGridView1.DataSource = orders;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string orderId = txtId.Text;    
            orderOperation.DeleteOrder(orderId);
            MessageBox.Show("Order deleted successfully");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            var updateOrder = new Order
            {
                ClientName = txtClient.Text,
                TownName = txtTown.Text,
                CityName = txtCity.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
                OrderId = id
            };
            orderOperation.updateOrder(updateOrder);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            Order orders = orderOperation.GetOrderById(id);
            dataGridView1.DataSource = new List<Order> { orders };
        }
    }
}
