using BlazorPurchaseOrders.Data;
using BlazorPurchaseOrders.Shared;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Pages
{
    public partial class ProductsPage
    {

        IEnumerable<Product> product;
        IEnumerable<Supplier> supplier;
        private List<ItemModel> Toolbaritems = new List<ItemModel>();

        SfDialog DialogAddEditProduct;
        Product addeditProduct = new Product();
        string HeaderText = "";

        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";

        public int SelectedProductId { get; set; } = 0;

        SfDialog DialogDeleteProduct;


        protected override async Task OnInitializedAsync()
        {
            //Populate the list of Product objects from the Product table.
            product = await ProductService.ProductList();
            supplier = await SupplierService.SupplierList();

            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Product", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit selected Product", PrefixIcon = "e-edit" });
            Toolbaritems.Add(new ItemModel() { Text = "Delete", TooltipText = "Delete selected Product", PrefixIcon = "e-delete" });
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                //Code for adding goes here
                addeditProduct = new Product();             // Ensures a blank form when adding
                HeaderText = "Add Product";
                await this.DialogAddEditProduct.Show();
            }
            if (args.Item.Text == "Edit")
            {
                //Code for editing goes here
                //Check that a Product Rate has been selected
                if (SelectedProductId == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Product from the grid.";
                    Warning.OpenDialog();
                }
                else
                {
                    //populate addeditProduct (temporary data set used for the editing process)
                    HeaderText = "Edit Product";
                    addeditProduct = await ProductService.Product_GetOne(SelectedProductId);
                    await this.DialogAddEditProduct.Show();
                }

            }
            if (args.Item.Text == "Delete")
            {
                //code for deleting goes here
                if (SelectedProductId == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Product from the grid.";
                    Warning.OpenDialog();
                }
                else
                {
                    //populate addeditProduct (temporary data set used for the editing process)
                    HeaderText = "Delete Product";
                    addeditProduct = await ProductService.Product_GetOne(SelectedProductId);
                    await this.DialogDeleteProduct.Show();
                }
            }
        }

        protected async Task ProductSave()
        {
            if (addeditProduct.ProductID == 0)
            {
                int Success = await ProductService.ProductInsert(
                    addeditProduct.ProductCode,
                    addeditProduct.ProductDescription,
                    addeditProduct.ProductUnitPrice,
                    addeditProduct.ProductSupplierID
                    );
                if (Success != 0)
                {
                    //Product Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Product Description already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else
                {
                    // Clears the dialog and is ready for another entry
                    // User must specifically close or cancel the dialog
                    addeditProduct = new Product();
                }
            }
            else
            {
                // Item is being edited
                int Success = await ProductService.ProductUpdate(
                    SelectedProductId,
                    addeditProduct.ProductCode,
                    addeditProduct.ProductDescription,
                    addeditProduct.ProductUnitPrice,
                    addeditProduct.ProductSupplierID,
                    addeditProduct.ProductIsArchived);
                if (Success != 0)
                {
                    //Product Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Product already exists; it cannot be added again.";
                    Warning.OpenDialog();
                }
                else
                {
                    await this.DialogAddEditProduct.Hide();
                    this.StateHasChanged();
                    addeditProduct = new Product();
                    SelectedProductId = 0;
                }
            }

            //Always refresh datagrid
            product = await ProductService.ProductList();
            StateHasChanged();
        }

        private async Task CloseDialog()
        {
            await this.DialogAddEditProduct.Hide();
        }

        public void RowSelectHandler(RowSelectEventArgs<Product> args)
        {
            //{args.Data} returns the current selected records.
            SelectedProductId = args.Data.ProductID;
        }

        public async void ConfirmDeleteNo()
        {
            await DialogDeleteProduct.Hide();
            SelectedProductId = 0;
        }

        public async void ConfirmDeleteYes()
        {
            int Success = await ProductService.ProductUpdate(
                SelectedProductId,
                addeditProduct.ProductCode,
                addeditProduct.ProductDescription,
                addeditProduct.ProductUnitPrice,
                addeditProduct.ProductSupplierID,
                addeditProduct.ProductIsArchived = true);
            if (Success != 0)
            {
                //Product Rate already exists - THis should never happen when marking a record 'IsArchived'.
                WarningHeaderMessage = "Warning!";
                WarningContentMessage = "Unknown error has occurred - the record has not been deleted!";
                Warning.OpenDialog();
            }
            else
            {
                await this.DialogDeleteProduct.Hide();
                product = await ProductService.ProductList();
                this.StateHasChanged();
                addeditProduct = new Product();
                SelectedProductId = 0;
            }
        }

    }
}
