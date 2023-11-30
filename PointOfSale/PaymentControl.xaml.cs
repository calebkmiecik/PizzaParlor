using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using PizzaParlor.Data;

namespace PizzaParlor.PointOfSale
{
    public partial class PaymentControl : UserControl
    {
        public PaymentControl()
        {
            InitializeComponent();
        }
        private void FinalizePayment_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText("receipts.txt", GenerateReceipt());

            // Clear the order
            Order newOrder = new Order();
        }

        public string GenerateReceipt()
        {
            StringBuilder receipt = new StringBuilder();

            if (DataContext is Order order)
            {
                // Append order information
                receipt.AppendLine($"Order Number: {order.Number}");
                receipt.AppendLine($"Date: {DateTime.Now}");

                receipt.AppendLine("Items Ordered:");
                foreach (var item in order.OrderItems)
                {
                    receipt.AppendLine($"  Name: {item.Name}");
                    receipt.AppendLine($"  Price: ${item.Price:N2}");

                    receipt.AppendLine("  Special Instructions:");
                    foreach (var instruction in item.SpecialInstructions)
                    {
                        receipt.AppendLine($"    - {instruction}");
                    }

                    receipt.AppendLine();
                }

                receipt.AppendLine("Subtotal: " + order.Subtotal.ToString());
                receipt.AppendLine("Total: " + order.Total.ToString());
                receipt.AppendLine("Tax: " + order.Tax.ToString());
                receipt.AppendLine("Amount Paid: " + order.AmountGiven.ToString());
                receipt.AppendLine("Change: " + order.Change.ToString());

                receipt.AppendLine("-----------------"); // Separator

                return receipt.ToString();
            }
            return "";
        }

    }
}