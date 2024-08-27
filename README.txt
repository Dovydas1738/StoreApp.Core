# Store Management App

## Overview

This application is designed to manage buyers, sellers, inventory and orders for products.
It provides features such as adding, updating and listing buyers, sellers, products and orders.
As well as counting the price of the whole buy order, cancelling orders and removing completed orders.

## Functionality Explanation

When creating an order, total order price will be counted based on the product and amount chosen.
You will not be able to create an order if ordering more items than there are in the storage.
Cancelling an order returns the ordered product amount back to storage.
Completing an order simply removes it from the list.

## API Endpoints

Order:

- POST /Order/Create an order
- GET /Order/Get all orders - all orders listed
- GET /Order/Get order by Id - finds an order by it's Id
- DELETE /Order/Delete cancelled order by Id - deletes an order and returns products back to storage
- DELETE /Order/Complete order by Id - deletes an order WITHOUT returning products back to storage
- PATCH /Order/Update an order - updates order data

Product:

- POST /Product/Add a product
- GET /Product/Get all products - all products listed
- GET /Product/Get product by Id - finds a product by it's Id
- DELETE /Product/Delete product by Id - deletes a product
- PATCH /Product/Update a product - updates product data

User:

- POST /User/Add a buyer
- GET /User/Get all buyers - all buyers listed
- GET /User/Get buyer by Id - finds a buyer by it's Id
- DELETE /User/Delete buyer by Id - deletes a buyer
- PATCH /User/Update a buyer - updates buyer data

- POST /User/Add a seller
- GET /User/Get all sellers - all sellers listed
- GET /User/Get seller by Id - finds a seller by it's Id
- DELETE /User/Delete seller by Id - deletes a seller
- PATCH /User/Update a seller - updates seller data