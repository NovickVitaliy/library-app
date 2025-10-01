# **Library Shop – Final Microservices Design**

---

## **1️⃣ Book Catalog Service (MongoDB)**

**Purpose:** Store all books and related info with **flexible schema**.

**Entities (4):**

1. **Book**

    * Flexible schema for different book types:

        * Physical: `{title, author, pages, shelfLocation, price, weight, shippingCost}`
        * E-book: `{title, author, fileFormat, price, downloadLink}`
        * Optional: `{illustrator, edition}`

2. **Genre** – `{GenreId, Name, Description}`

3. **Publisher** – `{PublisherId, Name, Address}`

4. **Review** – `{ReviewId, BookId, UserId, Rating, Text, CreatedDate}`

**MongoDB Flexibility:** Some documents have additional fields (illustrator, edition, shippingCost) to showcase **schema flexibility**.

---

## **2️⃣ Order & Inventory Service (SQL)**

**Purpose:** Manage orders, payments, and inventory with **full relational complexity**.

**Entities (6):**

1. **Member** – `{MemberId, Email (Unique), FirstName, LastName, PhoneNumber}`

2. **Order** – `{OrderId, MemberId (FK), OrderDate, Status}`

3. **OrderItem** – `{OrderItemId, OrderId (FK), BookId, Quantity, UnitPrice}`

4. **Inventory** – `{InventoryId, BookId (Unique, FK), StockQuantity, ReorderLevel}`

    * Check constraint: `StockQuantity >= 0`
    * Index: `IX_Inventory_StockQuantity`

5. **Payment** – `{PaymentId, OrderId (FK), Amount, PaidDate, PaymentMethod}`

6. **Staff** – `{StaffId, Name, Role}`

    * Many-to-many: Staff ↔ Orders via `StaffOrder` join table

**SQL Features Highlighted:**

* Relationships: **one-to-one, one-to-many, many-to-many**
* **Unique keys**: Member.Email, Inventory.BookId
* **Indexes:** OrderDate, StockQuantity
* **Constraints:** Foreign keys, check constraints, cascade deletes

---

## **3️⃣ Cart & Wishlist Service (SQL)**

**Purpose:** Let members add books to a **shopping cart or wishlist**, with **transactional business logic**.

**Entities (4):**

1. **Cart** – `{CartId, MemberId (FK), CreatedDate, Status (Active, CheckedOut)}`
2. **CartItem** – `{CartItemId, CartId (FK), BookId (FK), Quantity, AddedDate}`
3. **Wishlist** – `{WishlistId, MemberId (FK), Name, CreatedDate}`
4. **WishlistItem** – `{WishlistItemId, WishlistId (FK), BookId (FK), AddedDate}`

---

### **Transactional Business Logic Examples**

1. **Checkout Cart Transaction:**

```text
BEGIN TRANSACTION
  1. Create Order in Order & Inventory Service
  2. Add OrderItems for each CartItem
  3. Deduct stock in Inventory
  4. Update Cart.Status = CheckedOut
COMMIT TRANSACTION
ROLLBACK if any step fails
```

2. **Prevent Duplicate Cart Items:**

* Before inserting: check if the book is already in the cart.

3. **Wishlist-to-Cart Move:**

* Moving items from Wishlist to Cart happens in a transaction to ensure consistency.

---

## **Project Flow Overview**

1. **Book Catalog Service (MongoDB)**

    * Browse/search books, genres, publishers, and reviews.

2. **Cart & Wishlist Service (SQL)**

    * Users add books to cart or wishlist.
    * Cart operations are transactional, ensuring safe checkout.

3. **Order & Inventory Service (SQL)**

    * Checkout creates an **Order**, updates inventory, processes payment.
    * Staff can manage orders, many-to-many relationship.

---

### ✅ **Why This Design Works**

* **MongoDB:** Flexible book documents with optional fields.
* **Order & Inventory SQL:** Full relational integrity, indexes, unique keys, constraints, multiple relationship types.
* **Cart & Wishlist SQL:** Simple CRUD with **transactional business logic** for a realistic shopping experience.
* **Extensible:** Can add promotions, shipping rules, recommendations, or notifications later.


Services

Cart & Wishlist Service:

localhost:15000 - HTTP

localhost:16000 - HTTPS
